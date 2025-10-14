using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using ThirdPartyTool.Dialog;

namespace ThirdPartyTool.Transactions
{
    public partial class SettleBatchOfflineForm : Form
    {
        public SettleBatchOfflineForm()
        {
            InitializeComponent();

            btnClose.Click += BtnClose_Click;
            btnSettle.Click += BtnSettle_Click;

            this.Load += SettleBatchOfflineForm_Load;
        }

        private void SettleBatchOfflineForm_Load(object sender, EventArgs e)
        {
            GetRemainingBatch();
        }

        private void BtnSettle_Click(object sender, EventArgs e)
        {
            try
            {
                ConfirmDialogForm confirmFirmDialog = new ConfirmDialogForm(Const.ConfirmTitle, "Are you sure you want to settle all batchs?");
                var result = confirmFirmDialog.ShowDialog();
                if (result == DialogResult.No)
                    return;

                ProcessForm processForm = new ProcessForm();
                processForm.InitSettleBatchOffline();
                processForm.ShowDialog();

                if (processForm.IsSuccess)
                {
                    InformationDialogForm infoDialog = new InformationDialogForm("Settle batchs successfully.");
                    infoDialog.ShowDialog();

                    GetRemainingBatch();
                }
                else
                {
                    InformationDialogForm infoDialog = new InformationDialogForm("Settle batchs Fail. Please contact TruongDS");
                    infoDialog.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                BLogicLogger.Exception("BtnSettle_Click in SettleBatchOfflineForm", ex);
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void GetRemainingBatch()
        {
            try
            {
                DataTable dtGroupSettleBatch = new DataTable("TransactionDetail");
                using (SqlCommand sqlCommand = new SqlCommand())
                using (SqlConnection conn = new SqlConnection(Helper.Merchant_Connection))
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();

                    string sql = "select distinct MerchantID, BatchID,WorkstationID from TransactionDetail where ISNULL(BatchUnique,'') = '';";

                    sqlCommand.Connection = conn;
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.CommandText = sql;
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    dtGroupSettleBatch.Load(reader);
                    reader.Close();
                }

                lblRemainingBatch.Text = $"There are still {dtGroupSettleBatch.Rows.Count} batches that haven’t been settled yet.";
            }
            catch (Exception ex)
            {
                BLogicLogger.Exception("GetRemainingBatch in SettleBatchOfflineForm", ex);
            }
        }
    }
}
