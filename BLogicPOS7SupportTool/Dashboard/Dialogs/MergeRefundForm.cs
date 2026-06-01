using BLogicPOS7SupportTool.Models;
using BLogicPOS7SupportTool.Services;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace BLogicPOS7SupportTool.Dashboard.Dialogs
{
    partial class MergeRefundForm : Form
    {
        private readonly ReceiptService _service;
        private readonly string _mainReceiptId;
        private readonly string _mainReceiptNumber;
        private readonly ReceiptDetailView _detailView;
        private Point _dragStart;

        private RefundReceiptInfo _refund;

        public MergeRefundForm(ReceiptService service, string mainReceiptId, string mainReceiptNumber)
        {
            InitializeComponent();
            _service = service;
            _mainReceiptId = mainReceiptId;
            _mainReceiptNumber = mainReceiptNumber;

            lblMainTicket.Text = "Finished Ticket: #" + _mainReceiptNumber;

            // Full ticket detail view (giống ReceiptPage), nhúng vào khung pnlDetail.
            _detailView = new ReceiptDetailView { Dock = DockStyle.Fill };
            pnlDetail.Controls.Add(_detailView);

            pnlTitleBar.MouseDown += (s, e) => { if (e.Button == MouseButtons.Left) _dragStart = e.Location; };
            pnlTitleBar.MouseMove += (s, e) => { if (e.Button == MouseButtons.Left) Location = new Point(Location.X + e.X - _dragStart.X, Location.Y + e.Y - _dragStart.Y); };

            btnCloseForm.Click += (s, e) => { DialogResult = DialogResult.Cancel; Close(); };
            btnCancel.Click += (s, e) => { DialogResult = DialogResult.Cancel; Close(); };
            btnSearch.Click += (s, e) => SearchRefund();
            txtRefund.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter) { e.SuppressKeyPress = true; SearchRefund(); }
            };
            txtRefund.TextChanged += (s, e) => { _refund = null; btnMerge.Enabled = false; lblRefundInfo.Text = ""; ClearDetail(); };
            btnMerge.Click += (s, e) => MergeRefund();
        }

        private void SearchRefund()
        {
            var number = txtRefund.Text.Trim();
            if (string.IsNullOrWhiteSpace(number)) return;

            _refund = null;
            btnMerge.Enabled = false;
            ClearDetail();

            var info = _service.LookupRefundReceipt(number);

            if (!info.Found)
            {
                lblRefundInfo.ForeColor = Color.FromArgb(200, 50, 50);
                lblRefundInfo.Text = "Refund ticket not found.";
                return;
            }

            if (string.Equals(info.SaleReceiptID, _mainReceiptId, StringComparison.OrdinalIgnoreCase))
            {
                lblRefundInfo.ForeColor = Color.FromArgb(200, 50, 50);
                lblRefundInfo.Text = "The refund ticket cannot be the same as the finished ticket.";
                return;
            }

            if (info.TransactionStatusID != "RET")
            {
                lblRefundInfo.ForeColor = Color.FromArgb(200, 50, 50);
                lblRefundInfo.Text = "Only Refund (RET) tickets can be merged.";
                return;
            }

            if (!info.HasPayments)
            {
                lblRefundInfo.ForeColor = Color.FromArgb(200, 50, 50);
                lblRefundInfo.Text = "The refund ticket has no payment to merge.";
                return;
            }

            _refund = info;
            lblRefundInfo.ForeColor = Color.FromArgb(50, 50, 50);
            lblRefundInfo.Text = $"Refund Ticket: #{info.SaleReceiptNumber}    Amount to merge: {Money(info.RefundAmount)}";
            btnMerge.Enabled = true;

            PopulateDetail(info.SaleReceiptID);
        }

        private void ClearDetail()
        {
            lblDetailHeader.Visible = false;
            pnlDetail.Visible = false;
            _detailView?.Clear();
        }

        private void PopulateDetail(string receiptId)
        {
            var detail = _service.GetReceiptDetail(receiptId);
            _detailView.Populate(detail.Header, detail.Items, detail.TaxLines, detail.PaymentLines);

            lblDetailHeader.Visible = true;
            pnlDetail.Visible = true;
        }

        private void MergeRefund()
        {
            if (_refund == null) return;

            var confirm = MessageBox.Show(
                $"Merge the payment(s) of refund ticket #{_refund.SaleReceiptNumber} ({Money(_refund.RefundAmount)}) into ticket #{_mainReceiptNumber}?\n\n" +
                "The refund ticket will be closed (CLO) and its payment lines will be moved to the finished ticket.\n\n" +
                "This action cannot be undone.",
                "Merge Refund Payment", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            bool ok = _service.MergeRefundPayments(_mainReceiptId, _refund.SaleReceiptID);
            if (ok)
            {
                MessageBox.Show(
                    $"Payment(s) of refund ticket #{_refund.SaleReceiptNumber} were merged into ticket #{_mainReceiptNumber}.",
                    "Merge Refund Payment", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show("The refund payment could not be merged. No changes were made.",
                    "Merge Refund Payment", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static string Money(decimal v)
        {
            return (v < 0 ? "-$" : "$") + Math.Abs(v).ToString("F2");
        }
    }
}
