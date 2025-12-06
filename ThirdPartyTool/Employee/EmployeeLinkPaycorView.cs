using BLogicSystems.XPos.Utilites;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using ThirdPartyTool.Dialog;

namespace ThirdPartyTool.Employee
{
    public partial class EmployeeLinkPaycorView : UserControl
    {
        private bool IsUsingPaycor = false;
        public EmployeeLinkPaycorView()
        {
            InitializeComponent();

            btnUpdate.Click += BtnUpdate_Click;
        }

        private void BtnUpdate_Click(object sender, System.EventArgs e)
        {
            if (!IsUsingPaycor)
                return;

            ConfirmDialogForm confirmFirmDialog = new ConfirmDialogForm(Const.ConfirmTitle, $"Are you sure you want to sync Paycor Integration?");
            var result = confirmFirmDialog.ShowDialog();
            if (result == DialogResult.No)
                return;

            ProcessForm processForm = new ProcessForm();
            processForm.CurrentProcessName = ProcessNameEnum.EmployeePaycor_GetEmployeesFromPaycor;
            processForm.ShowDialog();
        }

        public void Init()
        {
            try
            {
                ProcessForm processForm = new ProcessForm();
                processForm.CurrentProcessName = ProcessNameEnum.EmployeePaycor_GetEmployees;
                processForm.ShowDialog();
                if (processForm.IsSuccess)
                {
                    DataSet dsReturn = processForm.DataSetReturn;

                    if (!string.IsNullOrWhiteSpace(Helper.PaycorRefreshToken))
                    {
                        if (dsReturn.Tables["PaycorEmployees"] == null)
                        {
                            btnUpdate.Text = "The database does not have the table PaycorEmployees. Please contact TruongDS.";
                            IsUsingPaycor = false;
                            btnUpdate.Visible = true;
                        }
                        else
                        {
                            if (dsReturn.Tables["PaycorEmployees"].Rows.Count > 0)
                            {
                                btnUpdate.Visible = false;
                                RefreshEmployee(dsReturn.Tables["Employees"], dsReturn.Tables["PaycorEmployees"]);
                            }
                            else
                            {
                                btnUpdate.Text = "Sync Employee Paycor Now";
                                IsUsingPaycor = true;
                                btnUpdate.Visible = true;
                            }
                        }

                    }
                    else
                    {
                        btnUpdate.Text = "The Store not integration with Paycor.";
                        IsUsingPaycor = false;
                        btnUpdate.Visible = true;
                    }
                }
                else
                {
                    InformationDialogForm infoDialog = new InformationDialogForm("Get Paycor configuration Fail. Please contact TruongDS");
                    infoDialog.ShowDialog();
                }
            }
            catch (System.Exception ex)
            {
                BLogicLogger.Exception("EmployeeLinkPaycorView.Init", ex);
            }

        }

        private void RefreshEmployee(DataTable dtEmployees = null, DataTable dtPaycorEmployees = null)
        {
            try
            {
                pnlContainer.Controls.Clear();
                DataRow[] drs = null;
                foreach (DataRow item in dtEmployees.Rows)
                {
                    EmployeeLinkRow employee = new EmployeeLinkRow();
                    employee.Dock = DockStyle.Top;
                    employee.POS_EmployeeNumber.Text = Utility.GetStringFromRow("EmployeeCode", item);
                    employee.POS_FirstName.Text = Utility.GetStringFromRow("EmployeeFirstName", item);
                    employee.POS_LastName.Text = Utility.GetStringFromRow("EmployeeLastName", item);
                    employee.POS_RolesName.Text = "";
                    employee.POS_EmployeeID = Utility.GetStringFromRow("EmployeeID", item);

                    drs = dtPaycorEmployees?.Select($"PaycorEmployeeID = '{item["PaycorEmployeeID"]}'");
                    if (drs?.Length > 0)
                    {
                        employee.ThirdParty_EmployeeNumber.Text = Utility.GetStringFromRow("EmployeeNumber", drs[0]);
                        employee.ThirdParty_AlternateEmployeeNumber.Text = Utility.GetStringFromRow("AlternateEmployeeNumber", drs[0]);
                        employee.ThirdParty_BadgeNumber.Text = Utility.GetStringFromRow("BadgeNumber", drs[0]);
                        employee.ThirdParty_FirstName.Text = Utility.GetStringFromRow("FirstName", drs[0]);
                        employee.ThirdParty_LastName.Text = Utility.GetStringFromRow("LastName", drs[0]);
                        employee.ThirdParty_EmployeeID = Utility.GetStringFromRow("PaycorEmployeeID", drs[0]);

                        employee.btnLinks.Text = "UnLink";
                    }
                    else
                    {
                        employee.btnLinks.Text = "Browse";
                        employee.btnLinks.BackColor = Color.Red;
                    }
                    pnlContainer.Controls.Add(employee);
                }
            }
            catch (System.Exception ex)
            {
                BLogicLogger.Exception("LoadEmployee in EmployeeLinkPaycorView", ex);
            }
        }
    }
}
