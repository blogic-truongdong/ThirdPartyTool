using BLogicSystems.XPos.ClientUtilities;
using BLogicSystems.XPos.Utilites;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using ThirdPartyTool.Dialog;

namespace ThirdPartyTool.Employee
{
    public partial class EmployeeLinkPaycorView2 : UserControl
    {
        private DataTable dtEmployees = null;
        private DataTable dtRoles = null;
        private DataTable dtEmployeeRoles = null;
        private DataTable dtPaycorEmployees = null;
        private int CurrentEmployeeID = -1;
        private string CurrentEmployeeName = "";

        public EmployeeLinkPaycorView2()
        {
            InitializeComponent();

            btnRefresh.Click += BtnRefresh_Click;

            pnlSearch.Click += PnlSearch_Click;
            btnSearch.Click += BtnSearch_Click;

            this.Load += EmployeeView_Load;

            txtSearch.TextChanged += TxtSearch_TextChanged;

            dgvEmployees.CellFormatting += DgvEmployees_CellFormatting;
            dgvEmployees.SelectionChanged += DgvEmployees_SelectionChanged;
            dgvEmployees.CellClick += DgvEmployees_CellClick;

            btnReSync.Click += BtnReSync_Click;

        }

        private void BtnReSync_Click(object sender, EventArgs e)
        {
            ConfirmDialogForm confirmFirmDialog = new ConfirmDialogForm(Const.ConfirmTitle, $"Are you sure you want to sync Paycor Integration?");
            var result = confirmFirmDialog.ShowDialog();
            if (result == DialogResult.No)
                return;

            ProcessForm processForm = new ProcessForm();
            processForm.CurrentProcessName = ProcessNameEnum.EmployeePaycor_GetEmployeesFromPaycor;
            processForm.ShowDialog();

            if (!processForm.IsSuccess)
            {
                InformationDialogForm errorDialog = new InformationDialogForm("Sync Paycor Integration error. Please contact TruongDS");
                errorDialog.ShowDialog();
                return;
            }
            else
            {
                BtnRefresh_Click(null, null);
            }
        }

        private void DgvEmployees_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int employeeID = Utility.GetIntFromObject(dgvEmployees.Rows[e.RowIndex].Cells["EmployeeID"].Value);
                string employeeName = Utility.GetStringFromObject(dgvEmployees.Rows[e.RowIndex].Cells["EmployeeName"].Value);

                if (dgvEmployees.Columns[e.ColumnIndex].Name == "LinkPaycor" && e.RowIndex >= 0)
                {
                    string paycorEmployeeDetail = Utility.GetStringFromObject(dgvEmployees.Rows[e.RowIndex].Cells["PaycorEmployeeDetail"].Value);
                    if (!string.IsNullOrWhiteSpace(paycorEmployeeDetail))
                    {
                        ConfirmDialogForm confirmFirmDialog = new ConfirmDialogForm(Const.ConfirmTitle, $"Are you sure you want to Un-Link employee {employeeName}?");
                        var result = confirmFirmDialog.ShowDialog();
                        if (result == DialogResult.No)
                            return;

                        var drs = dtEmployees.Select($"EmployeeID = {employeeID}");
                        if (drs.Length > 0)
                        {
                            drs[0]["PaycorEmployeeDetail"] = "";
                        }

                        using (SqlCommand sqlCommand = new SqlCommand())
                        using (SqlConnection conn = new SqlConnection(Helper.BLOGICPOS7_Connection))
                        {
                            if (conn.State == ConnectionState.Closed)
                                conn.Open();

                            string sql = $"Update Employees set PaycorEmployeeID = '' where EmployeeID = '{employeeID}';";

                            sqlCommand.Connection = conn;
                            sqlCommand.CommandType = CommandType.Text;
                            sqlCommand.CommandText = sql;

                            var resultUpdate = sqlCommand.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        EmployeeLinkPaycorForm linkForm = new EmployeeLinkPaycorForm();
                        linkForm.dtPaycorEmployees = dtPaycorEmployees;

                        if (BLogicMessageBox.ShowPopUpFormWithDIMBackground(linkForm) == DialogResult.OK)
                        {
                            var drs = dtPaycorEmployees.Select($"PaycorEmployeeID = '{linkForm.LinkToPaycorEmployeeID}'");
                            if (drs.Length > 0)
                            {
                                var name = $"{drs[0]["EmployeeNumber"]} - {drs[0]["FirstName"]} {drs[0]["LastName"]} ";
                                drs = dtEmployees.Select($"EmployeeID = {employeeID}");
                                if (drs.Length > 0)
                                {
                                    drs[0]["PaycorEmployeeDetail"] = name;

                                    using (SqlCommand sqlCommand = new SqlCommand())
                                    using (SqlConnection conn = new SqlConnection(Helper.BLOGICPOS7_Connection))
                                    {
                                        if (conn.State == ConnectionState.Closed)
                                            conn.Open();

                                        string sql = $"Update Employees set PaycorEmployeeID = '{linkForm.LinkToPaycorEmployeeID}' where EmployeeID = '{employeeID}';";

                                        sqlCommand.Connection = conn;
                                        sqlCommand.CommandType = CommandType.Text;
                                        sqlCommand.CommandText = sql;

                                        sqlCommand.ExecuteNonQuery();
                                    }
                                }
                            }
                        }
                        else
                        {

                        }
                    }

                }
            }
            catch (Exception ex)
            {
                BLogicLogger.Exception("DgvEmployees_CellClick in EmployeeLinkPaycorView2", ex);
            }
        }

        private void DgvEmployees_SelectionChanged(object sender, EventArgs e)
        {

        }

        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtSearch.Text) || txtSearch.Text == "Search")
                    dtEmployees.DefaultView.RowFilter = "";
                else
                    dtEmployees.DefaultView.RowFilter = $"EmployeeCode LIKE '%{txtSearch.Text}%' or EmployeeName LIKE '%{txtSearch.Text}%'";
                dtEmployees.DefaultView.Sort = "EmployeeName";
            }
            catch (Exception ex)
            {
                BLogicLogger.Exception("TxtSearch_TextChanged in EmployeeView", ex);
            }
        }

        private void DgvEmployees_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (dgvEmployees.Columns[e.ColumnIndex].Name == "Delete")
                    e.Value = Properties.Resources.Icon_Delete;

                if (dgvEmployees.Columns[e.ColumnIndex].Name == "LinkPaycor")
                {
                    string paycorEmployeeDetail = Utility.GetStringFromObject(dgvEmployees.Rows[e.RowIndex].Cells["PaycorEmployeeDetail"].Value);
                    if (string.IsNullOrWhiteSpace(paycorEmployeeDetail))
                    {
                        e.Value = "Link";
                        e.CellStyle.BackColor = Color.White;
                        e.CellStyle.ForeColor = Color.Black;
                    }
                    else
                    {
                        e.Value = "Un-link";
                        e.CellStyle.BackColor = Color.Red;
                        e.CellStyle.ForeColor = Color.White;
                    }
                }


            }
            catch (Exception ex)
            {
                BLogicLogger.Exception("DgvEmployees_CellFormatting in EmployeeView", ex);
            }
        }

        private void EmployeeView_Load(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                BLogicLogger.Exception("EmployeeView_Load in EmployeeView", ex);
            }
        }

        private void PnlSearch_Click(object sender, EventArgs e)
        {
            txtSearch.Focus();
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {

        }

        public void BtnRefresh_Click(object sender, System.EventArgs e)
        {
            try
            {
                dgvEmployees.CellFormatting -= DgvEmployees_CellFormatting;
                dgvEmployees.SelectionChanged -= DgvEmployees_SelectionChanged;
                dgvEmployees.CellClick -= DgvEmployees_CellClick;

                ProcessForm processForm = new ProcessForm();
                processForm.CurrentProcessName = ProcessNameEnum.EmployeePaycor_GetEmployees;
                processForm.ShowDialog(this.ParentForm);

                if (!processForm.IsSuccess)
                {
                    InformationDialogForm errorDialog = new InformationDialogForm("Get Employees info error. Please contact TruongDS");
                    errorDialog.ShowDialog();
                    return;
                }

                dtEmployees = processForm.DataSetReturn.Tables["Employees"];
                dtRoles = processForm.DataSetReturn.Tables["Roles"];
                dtEmployeeRoles = processForm.DataSetReturn.Tables["EmployeeRoles"];
                dtPaycorEmployees = processForm.DataSetReturn.Tables["PaycorEmployees"];

                if (!dtEmployees.Columns.Contains("PaycorEmployeeDetail"))
                    dtEmployees.Columns.Add("PaycorEmployeeDetail", typeof(string));

                if (dtPaycorEmployees?.Rows.Count > 0)
                {
                    foreach (DataRow item in dtEmployees.Rows)
                    {
                        var drs = dtPaycorEmployees.Select($"PaycorEmployeeID = '{item["PaycorEmployeeID"]}'");
                        if (drs.Length > 0)
                        {
                            item["PaycorEmployeeDetail"] = $"{drs[0]["EmployeeNumber"]} - {drs[0]["FirstName"]} {drs[0]["LastName"]} ";
                        }
                    }
                }

                dgvEmployees.DataSource = dtEmployees;

                txtSearch.Focus();
            }
            catch (Exception ex)
            {
                BLogicLogger.Exception("BtnRefresh_Click in EmployeeView", ex);
            }
            finally
            {
                dgvEmployees.CellFormatting += DgvEmployees_CellFormatting;
                dgvEmployees.SelectionChanged += DgvEmployees_SelectionChanged;
                dgvEmployees.CellClick += DgvEmployees_CellClick;
            }
        }
    }
}
