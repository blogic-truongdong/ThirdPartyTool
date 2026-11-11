using BLogicSystems.XPos.Utilites;
using System;
using System.Data;
using System.Windows.Forms;
using ThirdPartyTool.Dialog;

namespace ThirdPartyTool.Employee
{
    public partial class EmployeeView : UserControl
    {
        private DataTable dtEmployees = null;
        private DataTable dtRoles = null;
        private DataTable dtEmployeeRoles = null;
        private int CurrentEmployeeID = -1;
        private string CurrentEmployeeName = "";

        public EmployeeView()
        {
            InitializeComponent();

            btnRefresh.Click += BtnRefresh_Click;

            pnlSearch.Click += PnlSearch_Click;
            btnSearch.Click += BtnSearch_Click;

            this.Load += EmployeeView_Load;

            dgvEmployees.CellFormatting += DgvEmployees_CellFormatting;
            txtSearch.TextChanged += TxtSearch_TextChanged;

            cbbSyncRoles.SelectedIndexChanged += CbbSyncRoles_SelectedIndexChanged;
            dgvEmployees.SelectionChanged += DgvEmployees_SelectionChanged;

            startDate.ValueChanged += StartDate_ValueChanged;
            endDate.ValueChanged += EndDate_ValueChanged;

            lblStartDate.Click += LblStartDate_Click;
            lblEndDate.Click += LblEndDate_Click;
            lblSyncRoles.Click += LblSyncRoles_Click;

            btnSyncRoles.Click += BtnSyncRoles_Click;
        }

        private void BtnSyncRoles_Click(object sender, EventArgs e)
        {
            try
            {
                if (CurrentEmployeeID < 0)
                {
                    InformationDialogForm informationDialog = new InformationDialogForm("Please select an employee before syncing a role.");
                    informationDialog.ShowDialog();
                    return;
                }

                if (cbbSyncRoles.SelectedIndex <= 0)
                {
                    InformationDialogForm informationDialog = new InformationDialogForm("Please select Role before process sync role.");
                    informationDialog.ShowDialog();
                    return;
                }

                ConfirmDialogForm confirmFirmDialog = new ConfirmDialogForm(Const.ConfirmTitle, $"Are you sure you want to sync the ‘{cbbSyncRoles.Text}’ role for employee {CurrentEmployeeName}?");
                var result = confirmFirmDialog.ShowDialog();
                if (result == DialogResult.No)
                    return;

                ProcessForm processForm = new ProcessForm();
                processForm.CurrentProcessName = ProcessNameEnum.ProcessSyncRoleForEmployee;
                processForm.EmployeeView_EmployeeID = CurrentEmployeeID;
                processForm.EmployeeView_RolesID = Utility.GetIntFromObject(cbbSyncRoles.SelectedValue);
                processForm.EmployeeView_StartDate = new DateTime(startDate.Value.Year, startDate.Value.Month, startDate.Value.Day, 0, 0, 0);
                processForm.EmployeeView_EndDate = new DateTime(endDate.Value.Year, endDate.Value.Month, endDate.Value.Day, 23, 59, 59, 999);
                processForm.ShowDialog();

                if (processForm.IsSuccess)
                {
                    InformationDialogForm informationDialog = new InformationDialogForm("Sync role for employee successfully.");
                    informationDialog.ShowDialog();
                }
                else
                {
                    InformationDialogForm informationDialog = new InformationDialogForm("Sync role for employee failed. Please contact TruongDS.");
                    informationDialog.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                BLogicLogger.Exception("BtnSyncRoles_Click in EmployeeView", ex);
            }
        }

        private void LblSyncRoles_Click(object sender, EventArgs e)
        {
            cbbSyncRoles.DroppedDown = true;
        }

        private void LblEndDate_Click(object sender, EventArgs e)
        {
            endDate.Select(); // focus vào control
            SendKeys.Send("%{DOWN}");
        }

        private void LblStartDate_Click(object sender, EventArgs e)
        {
            startDate.Select(); // focus vào control
            SendKeys.Send("%{DOWN}");
        }

        private void EndDate_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                lblEndDate.Text = endDate.Value.ToShortDateString();
            }
            catch (Exception ex)
            {
                BLogicLogger.Exception("EndDate_ValueChanged in EmployeeView", ex);

            }
        }

        private void StartDate_ValueChanged(object sender, EventArgs e)
        {
            try
            {

                lblStartDate.Text = startDate.Value.ToShortDateString();
            }
            catch (Exception ex)
            {
                BLogicLogger.Exception("StartDate_ValueChanged in EmployeeView", ex);
            }
        }

        private void DgvEmployees_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbbSyncRoles.DataSource != null)
                    cbbSyncRoles.SelectedIndex = 0;

                var currentRow = dgvEmployees.CurrentRow;
                if (currentRow == null)
                {
                    ShowSelectedEmployee(null);
                    return;
                }
                CurrentEmployeeID = Utility.GetIntFromObject(currentRow.Cells["EmployeeID"].Value);
                CurrentEmployeeName = Utility.GetStringFromObject(currentRow.Cells["EmployeeName"].Value);
                ShowSelectedEmployee(currentRow);
                startDate.Value = endDate.Value = DateTime.Now;
            }
            catch (Exception ex)
            {
                BLogicLogger.Exception("DgvEmployees_SelectionChanged in EmployeeView", ex);
            }
        }

        private void CbbSyncRoles_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                lblSyncRoles.Text = cbbSyncRoles.Text;

            }
            catch (Exception ex)
            {
                BLogicLogger.Exception("CbbSyncRoles_SelectedIndexChanged in EmployeeView", ex);
            }
        }

        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (dtEmployees == null)
                    return;

                string keyword = txtSearch.GetRealText().Trim().Replace("'", "''").Replace("[", "[[]").Replace("%", "[%]").Replace("*", "[*]");
                if (string.IsNullOrWhiteSpace(keyword))
                    dtEmployees.DefaultView.RowFilter = "";
                else
                    dtEmployees.DefaultView.RowFilter = $"EmployeeCode LIKE '%{keyword}%' or EmployeeName LIKE '%{keyword}%'";
                UpdateCount();
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
                ProcessForm processForm = new ProcessForm();
                processForm.CurrentProcessName = ProcessNameEnum.EmployeeView_GetEmployeeInfos;
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

                dgvEmployees.DataSource = dtEmployees;

                DataRow dataRow = dtRoles.NewRow();
                dataRow["RolesID"] = "-1";
                dataRow["RolesName"] = "N/A";
                dtRoles.Rows.InsertAt(dataRow, 0);

                cbbSyncRoles.DataSource = dtRoles;
                cbbSyncRoles.ValueMember = "RolesID";
                cbbSyncRoles.DisplayMember = "RolesName";
                cbbSyncRoles.SelectedIndex = 0;

                startDate.Value = endDate.Value = DateTime.Now;
                UpdateCount();
                txtSearch.Focus();
            }
            catch (Exception ex)
            {
                BLogicLogger.Exception("BtnRefresh_Click in EmployeeView", ex);
            }
        }

        private void UpdateCount()
        {
            if (dtEmployees == null)
            {
                lblCount.Text = "";
                return;
            }

            int total = dtEmployees.Rows.Count;
            int shown = dtEmployees.DefaultView.Count;
            string noun = total == 1 ? "employee" : "employees";
            lblCount.Text = shown == total ? $"{total} {noun}" : $"{shown} of {total} {noun}";
        }

        private void ShowSelectedEmployee(DataGridViewRow row)
        {
            if (row == null)
            {
                CurrentEmployeeID = -1;
                CurrentEmployeeName = "";
                lblCurrentEmployeeName.Text = "No employee selected";
                lblCurrentEmployeeMeta.Text = "Pick one from the list.";
                return;
            }

            string code = Utility.GetStringFromObject(row.Cells["EmployeeCode"].Value);
            string role = Utility.GetStringFromObject(row.Cells["RoleName"].Value);
            lblCurrentEmployeeName.Text = CurrentEmployeeName;
            lblCurrentEmployeeMeta.Text = string.IsNullOrWhiteSpace(role)
                ? $"ID {code}"
                : $"ID {code}  ·  Current role: {role}";
        }
    }
}
