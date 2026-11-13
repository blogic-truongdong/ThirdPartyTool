using BLogicSystems.XPos.ClientUtilities;
using System;
using System.Data;
using System.Windows.Forms;
using ThirdPartyTool.Dialog;

namespace ThirdPartyTool.SaleReceipt
{
    public partial class SearchBatchSettledForm : Form
    {
        public string SelectedBatchUnique = string.Empty;
        public string SelectedMerchantID = string.Empty;
        public string SelectedBatchID = string.Empty;

        public SearchBatchSettledForm()
        {
            InitializeComponent();

            btnCancel.Click += BtnCancel_Click;

            this.Load += SearchBatchSettledForm_Load;

            btnSearch.Click += BtnSearch_Click;

            dgvBatchs.CellFormatting += DgvBatchs_CellFormatting;

            dgvBatchs.CellClick += DgvBatchs_CellClick;
        }

        private void DgvBatchs_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && dgvBatchs.Columns[e.ColumnIndex].Name == "SelectColumn")
                {
                    ConfirmDialogForm confirmDialogForm = new ConfirmDialogForm("Confirm", "Are you sure you want to select this batch?");
                    if (BLogicMessageBox.ShowPopUpFormWithDIMBackground(confirmDialogForm) == DialogResult.Yes)
                    {
                        SelectedBatchUnique = dgvBatchs.Rows[e.RowIndex].Cells["BatchUnique"].Value.ToString();
                        SelectedMerchantID = dgvBatchs.Rows[e.RowIndex].Cells["MerchantID"].Value.ToString();
                        SelectedBatchID = dgvBatchs.Rows[e.RowIndex].Cells["ID"].Value.ToString();

                        DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }

            }
            catch (Exception ex)
            {
                BLogicLogger.Exception("DgvBatchs_CellClick in SearchBatchSettledForm", ex);
            }
        }

        private void DgvBatchs_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // set text Select for datagridview column SelectColumn
            try
            {
                // set text Select for datagridview column SelectColumn
                if (dgvBatchs.Columns[e.ColumnIndex].Name == "SelectColumn")
                {
                    e.Value = "Select";
                }
            }
            catch (Exception ex)
            {
                BLogicLogger.Exception("DgvBatchs_CellFormatting in SearchBatchSettledForm", ex);
            }

        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtBatchs = Helper.ProcessQuery(false, $"select BatchUnique, Date as DateCreated, WorkStationName,MerchantID,ID,Quantity,Total  from Batch a where a.Date between '{startDate.Value:yyyy-MM-dd HH:mm:ss}' and '{endDate.EndValue:yyyy-MM-dd HH:mm:ss.fff}' order by a.Date desc", "Batch");

                dgvBatchs.DataSource = dtBatchs;
            }
            catch (Exception ex)
            {
                // log exception
                BLogicLogger.Exception("SearchBatchSettledForm.BtnSearch_Click", ex);
            }
        }

        private void SearchBatchSettledForm_Load(object sender, EventArgs e)
        {
            startDate.DateTimeValues = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            endDate.DateTimeValues = DateTime.Now;
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
