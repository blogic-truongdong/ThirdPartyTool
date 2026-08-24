using BLogicPOS7SupportTool.Core;
using BLogicPOS7SupportTool.Dashboard.Controls;
using BLogicPOS7SupportTool.Dashboard.Dialogs;
using BLogicPOS7SupportTool.Models;
using BLogicPOS7SupportTool.Services;
using System;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BLogicPOS7SupportTool.Dashboard.Pages
{
    // Lists the merchant batches the terminal never settled and closes them,
    // one at a time or all together.
    public partial class BatchManagerPage : UserControl
    {
        private readonly BatchService _service = new BatchService();

        public BatchManagerPage()
        {
            InitializeComponent();
        }

        // Called by MainForm each time the page is opened, so the list is never stale.
        public void LoadBatches()
        {
            SetBusy(true, "Loading...");

            Task.Run(() => _service.GetOpenBatches())
                .ContinueWith(t =>
                {
                    SetBusy(false, "");
                    if (t.IsFaulted)
                    {
                        AppLogger.Error("GetOpenBatches failed", t.Exception);
                        MessageBox.Show($"Could not load the open batches:\n{t.Exception?.GetBaseException().Message}", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    FillBatches(t.Result);
                }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        // One card per open batch; the cards are laid out by flpBatches.
        private void FillBatches(DataTable batches)
        {
            flpBatches.SuspendLayout();
            foreach (Control old in flpBatches.Controls)
                old.Dispose();
            flpBatches.Controls.Clear();

            foreach (DataRow row in batches.Rows)
            {
                var card = new BatchCard();
                card.Bind(row);
                card.SettleClicked += BatchCard_SettleClicked;
                flpBatches.Controls.Add(card);
            }
            flpBatches.ResumeLayout();

            lblEmpty.Visible = batches.Rows.Count == 0;
            lblSummary.Text = batches.Rows.Count == 0
                ? "No open batch to settle."
                : $"{batches.Rows.Count} open batch(es) waiting to be settled.";
            btnSettleAll.Enabled = batches.Rows.Count > 0;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadBatches();
        }

        private void BatchCard_SettleClicked(object sender, EventArgs e)
        {
            if (sender is BatchCard card)
                SettleBatch(card.MerchantID, card.BatchID);
        }

        private void btnSettleAll_Click(object sender, EventArgs e)
        {
            int count = flpBatches.Controls.Count;
            if (count == 0) return;

            var confirm = MessageBox.Show(
                $"Settle all {count} open batch(es)?\n\n" +
                "Each batch gets a closed Batch record and its transactions are marked as settled. " +
                "This cannot be undone.",
                "Settle All Batches", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm != DialogResult.Yes) return;

            SetBusy(true, "Settling batches...");
            Task.Run(() => _service.SettleAllBatches())
                .ContinueWith(t =>
                {
                    SetBusy(false, "");
                    if (t.IsFaulted)
                    {
                        AppLogger.Error("SettleAllBatches failed", t.Exception);
                        MessageBox.Show($"Settle failed:\n{t.Exception?.GetBaseException().Message}", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (t.Result.Count > 0)
                    {
                        MessageBox.Show("These batches could not be settled:\n" + string.Join("\n", t.Result) +
                            "\n\nSee the log for details.", "Settle All Batches", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        MessageBox.Show("All batches have been settled.", "Done",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    LoadBatches();
                }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        // Works out the Batch record first and shows it; only a confirmed preview is settled.
        private void SettleBatch(string merchantId, string batchId)
        {
            SetBusy(true, "Loading batch...");
            Task.Run(() => _service.PreviewBatch(merchantId, batchId))
                .ContinueWith(t =>
                {
                    SetBusy(false, "");
                    if (t.IsFaulted)
                    {
                        AppLogger.Error($"PreviewBatch failed for MerchantID={merchantId}, BatchID={batchId}", t.Exception);
                        MessageBox.Show($"Could not load batch {batchId}:\n{t.Exception?.GetBaseException().Message}", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    if (t.Result == null)
                    {
                        MessageBox.Show($"Batch {batchId} has nothing to settle — it has no approved transaction " +
                            "or was settled in the meantime.", "Settle Batch", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        LoadBatches();
                        return;
                    }

                    using (var dlg = new SettleBatchPreviewForm(t.Result))
                    {
                        if (DimOverlay.ShowDialog(ParentForm, dlg) == DialogResult.OK)
                            SettlePreviewedBatch(t.Result);
                    }
                }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private void SettlePreviewedBatch(BatchSettlePreviewEntity batch)
        {
            SetBusy(true, "Settling batch...");
            Task.Run(() => _service.SettleBatch(batch))
                .ContinueWith(t =>
                {
                    SetBusy(false, "");
                    if (t.IsFaulted)
                    {
                        AppLogger.Error($"SettleBatch failed for MerchantID={batch.MerchantID}, BatchID={batch.BatchID}", t.Exception);
                        MessageBox.Show($"Settle failed:\n{t.Exception?.GetBaseException().Message}", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (t.Result)
                    {
                        MessageBox.Show($"Batch {batch.BatchID} has been settled.", "Done",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show($"Batch {batch.BatchID} was not settled — its transactions changed after the " +
                            "preview, or it was settled in the meantime. Nothing was saved; open it again to review " +
                            "the current figures. See the log for details.",
                            "Settle Batch", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    LoadBatches();
                }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private void SetBusy(bool busy, string message)
        {
            lblLoading.Text = message;
            pnlLoading.Visible = busy;
            if (busy) pnlLoading.BringToFront();
            btnRefresh.Enabled = !busy;
            btnSettleAll.Enabled = !busy && flpBatches.Controls.Count > 0;
            flpBatches.Enabled = !busy;
        }
    }
}
