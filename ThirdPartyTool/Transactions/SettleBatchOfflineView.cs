using BLogicSystems.XPos.Utilites;
using System;
using System.Data;
using System.Windows.Forms;
using ThirdPartyTool.Dialog;

namespace ThirdPartyTool.Transactions
{
    public partial class SettleBatchOfflineView : UserControl
    {
        DataTable dtBatchDetail = null;
        public SettleBatchOfflineView()
        {
            InitializeComponent();

            btnSettle.Click += BtnSettle_Click;

            this.Load += SettleBatchOfflineForm_Load;

            btnRefresh.Click += BtnRefresh_Click;

            dgvBatch.CellClick += DgvBatch_CellClick;
            dgvBatch.CellFormatting += DgvBatch_CellFormatting;
        }

        private void DgvBatch_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0)
                    return;

                if (e.ColumnIndex == dgvBatch.Columns["SettleBatch"].Index)
                {
                    e.Value = "Settle Now";
                }
            }
            catch (Exception ex)
            {
                BLogicLogger.Exception("DgvBatch_CellFormatting in SettleBatchOfflineForm", ex);
            }
        }

        private void DgvBatch_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0)
                    return;

                if (e.ColumnIndex == dgvBatch.Columns["SettleBatch"].Index)
                {
                    ConfirmDialogForm confirmFirmDialog = new ConfirmDialogForm(Const.ConfirmTitle, "Are you sure you want to settle this batch?");
                    var result = confirmFirmDialog.ShowDialog();
                    if (result == DialogResult.No)
                        return;

                    ProcessForm processForm = new ProcessForm();
                    processForm.CurrentProcessName = ProcessNameEnum.SettleBatchOfflineByMerchantID;
                    processForm.MerchantID = Utility.GetStringFromObject(dgvBatch.Rows[e.RowIndex].Cells["MerchantID"].Value);
                    processForm.BatchID = Utility.GetStringFromObject(dgvBatch.Rows[e.RowIndex].Cells["BatchID"].Value);
                    processForm.WorkstationID = Utility.GetStringFromObject(dgvBatch.Rows[e.RowIndex].Cells["WorkstationID"].Value);
                    processForm.ShowDialog();

                    if (processForm.IsSuccess)
                    {
                        InformationDialogForm infoDialog = new InformationDialogForm("Settle batch successfully.");
                        infoDialog.ShowDialog();

                        BtnRefresh_Click(sender, e);
                    }
                    else
                    {
                        InformationDialogForm infoDialog = new InformationDialogForm("Settle batch Fail. Please contact TruongDS");
                        infoDialog.ShowDialog();
                    }
                }
            }
            catch (Exception ex)
            {
                BLogicLogger.Exception("DgvBatch_CellClick in SettleBatchOfflineForm", ex);
            }
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
                    dtBatchDetail = processForm.DataTableReturn.Copy();
                    dtBatchDetail.Columns.Add("SettleBatch", typeof(string));
                    dgvBatch.DataSource = dtBatchDetail;
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
                if (dtBatchDetail?.Rows.Count > 0)
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
