using BLogicPOS7SupportTool.Models;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace BLogicPOS7SupportTool.Dashboard.Dialogs
{
    // Shows the Batch record an open batch would be settled into. Settling only
    // starts once the user confirms here (DialogResult.OK); this form writes nothing.
    partial class SettleBatchPreviewForm : Form
    {
        private Point _dragStart;

        public SettleBatchPreviewForm(BatchSettlePreviewEntity batch)
        {
            InitializeComponent();

            pnlTitleBar.MouseDown += (s, e) => { if (e.Button == MouseButtons.Left) _dragStart = e.Location; };
            pnlTitleBar.MouseMove += (s, e) => { if (e.Button == MouseButtons.Left) Location = new Point(Location.X + e.X - _dragStart.X, Location.Y + e.Y - _dragStart.Y); };

            btnCloseForm.Click += (s, e) => { DialogResult = DialogResult.Cancel; Close(); };
            btnCancel.Click += (s, e) => { DialogResult = DialogResult.Cancel; Close(); };
            btnSettle.Click += (s, e) => { DialogResult = DialogResult.OK; Close(); };

            Bind(batch);
        }

        private void Bind(BatchSettlePreviewEntity batch)
        {
            lblFormTitle.Text = "Settle Batch #" + batch.BatchID;

            lblMerchantID.Text = batch.MerchantID;
            lblWorkstation.Text = batch.WorkStationName;
            lblTransactions.Text = batch.TransactionCount.ToString();
            lblPeriod.Text = batch.FirstDate.Date == batch.Date.Date
                ? batch.FirstDate.ToString("M/d/yyyy h:mm tt") + " – " + batch.Date.ToString("h:mm tt")
                : batch.FirstDate.ToString("M/d/yyyy h:mm tt") + " – " + batch.Date.ToString("M/d/yyyy h:mm tt");

            lblTotalSales.Text = Money(batch.TotalSales);
            lblTotalTips.Text = Money(batch.TotalTips);
            lblSVCFee.Text = Money(batch.TotalSVCFeeAmount);
            lblReturns.Text = Money(batch.TotalReturns);
            lblTotal.Text = Money(batch.Total);
            lblMerchantFee.Text = Money(batch.MerchantFeeAmount) + "  (" + batch.MerchantFeePercent.ToString("0.##") + "%)";
            lblDeposit.Text = Money(batch.DepositAmount);
        }

        private static string Money(decimal v)
        {
            return (v < 0 ? "-$" : "$") + Math.Abs(v).ToString("F2");
        }
    }
}
