using System;
using System.Data;
using System.Windows.Forms;

namespace ThirdPartyTool.Employee
{
    public partial class EmployeeView : UserControl
    {
        private DataTable dtEmployees = null;
        private DataTable dtRoles = null;
        private DataTable dtEmployeeRoles = null;
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
                cbbSyncRoles.SelectedIndex = dtRoles.Rows.Count - 1;
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
                if (string.IsNullOrWhiteSpace(txtSearch.Text) || txtSearch.Text == "Search")
                    dtEmployees.DefaultView.RowFilter = "";
                else
                    dtEmployees.DefaultView.RowFilter = $"EmployeeCode LIKE '%{txtSearch.Text}%' or EmployeeName LIKE '%{txtSearch.Text}%'";
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
                startDate.Value = endDate.Value = DateTime.Now;
                BtnRefresh_Click(sender, e);
                txtSearch.Focus();
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

        private void BtnRefresh_Click(object sender, System.EventArgs e)
        {
            try
            {
                ProcessForm processForm = new ProcessForm();
                processForm.CurrentProcessName = ProcessNameEnum.EmployeeView_GetEmployeeInfos;
                processForm.ShowDialog(this.ParentForm);

                dtEmployees = processForm.DataSetReturn.Tables["Employees"];
                dtRoles = processForm.DataSetReturn.Tables["Roles"];
                dtEmployeeRoles = processForm.DataSetReturn.Tables["EmployeeRoles"];

                dgvEmployees.DataSource = dtEmployees;

                dtRoles.Rows.Add("999999", "N/A");
                cbbSyncRoles.DataSource = dtRoles;
                cbbSyncRoles.ValueMember = "RolesID";
                cbbSyncRoles.DisplayMember = "RolesName";
                cbbSyncRoles.SelectedIndex = dtRoles.Rows.Count - 1;

            }
            catch (Exception ex)
            {
                BLogicLogger.Exception("BtnRefresh_Click in EmployeeView", ex);
            }
        }

        private void BindingEmployeeData()
        {
            try
            {
                if (dgvEmployees.SelectedRows.Count > 0)
                {
                    var row = dgvEmployees.SelectedRows[0];
                }
                else
                {
                }
            }
            catch (Exception ex)
            {
                BLogicLogger.Exception("BindingEmployeeData in EmployeeView", ex);
            }
        }
    }
}
