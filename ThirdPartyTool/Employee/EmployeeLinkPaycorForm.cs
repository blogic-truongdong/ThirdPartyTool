using BLogicSystems.XPos.Utilites;
using System;
using System.Data;
using System.Windows.Forms;
using ThirdPartyTool.Dialog;

namespace ThirdPartyTool.Employee
{
    public partial class EmployeeLinkPaycorForm : Form
    {
        public DataTable dtPaycorEmployees = null;
        public string LinkToPaycorEmployeeID = string.Empty;
        public EmployeeLinkPaycorForm()
        {
            InitializeComponent();

            this.Load += EmployeeLinkPaycorForm_Load;

            dgvEmployees.CellClick += DgvEmployees_CellClick;

            btnClose.Click += BtnClose_Click;

            txtSearch.TextChanged += TxtSearch_TextChanged;
        }

        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtSearch.Text) || txtSearch.Text == "Search")
                    dtPaycorEmployees.DefaultView.RowFilter = "";
                else
                    dtPaycorEmployees.DefaultView.RowFilter = $"EmployeeNumber LIKE '%{txtSearch.Text}%' or EmployeeName LIKE '%{txtSearch.Text}%'";
            }
            catch (Exception ex)
            {
                BLogicLogger.Exception("TxtSearch_TextChanged in EmployeeView", ex);
            }
        }


        private void BtnClose_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void DgvEmployees_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ConfirmDialogForm confirmFirmDialog = new ConfirmDialogForm(Const.ConfirmTitle, $"Are you sure you want to link to {dgvEmployees.Rows[e.RowIndex].Cells["EmployeeName"].Value}?");
            var result = confirmFirmDialog.ShowDialog();
            if (result == DialogResult.No)
                return;
            DialogResult = DialogResult.OK;
            LinkToPaycorEmployeeID = Utility.GetStringFromObject(dgvEmployees.Rows[e.RowIndex].Cells["PaycorEmployeeID"].Value);
            this.Close();
        }

        private void EmployeeLinkPaycorForm_Load(object sender, System.EventArgs e)
        {
            dgvEmployees.DataSource = dtPaycorEmployees;
        }
    }
}
