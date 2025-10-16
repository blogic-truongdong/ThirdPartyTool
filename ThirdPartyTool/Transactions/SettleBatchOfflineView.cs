using System;
using System.Data;
using System.Windows.Forms;
using ThirdPartyTool.Dialog;

namespace ThirdPartyTool.Transactions
{
    public partial class SettleBatchOfflineView : UserControl
    {
        public SettleBatchOfflineView()
        {
            InitializeComponent();

            btnSettle.Click += BtnSettle_Click;

            this.Load += SettleBatchOfflineForm_Load;

            btnRefresh.Click += BtnRefresh_Click;
        }

        public void BtnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                ProcessForm processForm = new ProcessForm();
                processForm.InitGetRemainingBatch();
                processForm.ShowDialog();
                if (processForm.IsSuccess)
                {
                    DataTable dtGroupSettleBatch = processForm.DataTableReturn;
                    lblRemainingBatch.Text = $"There are still {dtGroupSettleBatch.Rows.Count} batches that haven’t been settled yet.";
                }
                else
                {
                    InformationDialogForm infoDialog = new InformationDialogForm("Get Remaining Batch Fail. Please contact TruongDS");
                    infoDialog.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                BLogicLogger.Exception("BtnSettle_Click in SettleBatchOfflineForm", ex);
            }
        }

        private void SettleBatchOfflineForm_Load(object sender, EventArgs e)
        {

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

                    BtnRefresh_Click(sender, e);
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

        }


    }
}
