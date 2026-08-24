using BLogicPOS7SupportTool.Models;
using BLogicPOS7SupportTool.Services;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace BLogicPOS7SupportTool.Dashboard.Dialogs
{
    partial class AddTipByTokenForm : Form
    {
        private readonly ReceiptService _service;
        private readonly string _mainReceiptId;
        private readonly string _mainReceiptNumber;
        private readonly ReceiptDetailView _detailView;
        private Point _dragStart;

        private TokenReceiptInfoEntity _token;

        public AddTipByTokenForm(ReceiptService service, string mainReceiptId, string mainReceiptNumber)
        {
            InitializeComponent();
            _service = service;
            _mainReceiptId = mainReceiptId;
            _mainReceiptNumber = mainReceiptNumber;

            lblMainTicket.Text = "Main Ticket: #" + _mainReceiptNumber;

            // Full ticket detail view (giống ReceiptPage), nhúng vào khung pnlDetail.
            _detailView = new ReceiptDetailView { Dock = DockStyle.Fill };
            pnlDetail.Controls.Add(_detailView);

            pnlTitleBar.MouseDown += (s, e) => { if (e.Button == MouseButtons.Left) _dragStart = e.Location; };
            pnlTitleBar.MouseMove += (s, e) => { if (e.Button == MouseButtons.Left) Location = new Point(Location.X + e.X - _dragStart.X, Location.Y + e.Y - _dragStart.Y); };

            btnCloseForm.Click += (s, e) => { DialogResult = DialogResult.Cancel; Close(); };
            btnCancel.Click += (s, e) => { DialogResult = DialogResult.Cancel; Close(); };
            btnSearch.Click += (s, e) => SearchToken();
            txtToken.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter) { e.SuppressKeyPress = true; SearchToken(); }
            };
            txtToken.TextChanged += (s, e) => { _token = null; btnApply.Enabled = false; lblTokenInfo.Text = ""; ClearDetail(); };
            btnApply.Click += (s, e) => ApplyTip();
        }

        private void SearchToken()
        {
            var number = txtToken.Text.Trim();
            if (string.IsNullOrWhiteSpace(number)) return;

            _token = null;
            btnApply.Enabled = false;
            ClearDetail();

            var info = _service.LookupTokenReceipt(number);

            if (!info.Found)
            {
                lblTokenInfo.ForeColor = Color.FromArgb(200, 50, 50);
                lblTokenInfo.Text = "Token ticket not found.";
                return;
            }

            if (string.Equals(info.SaleReceiptID, _mainReceiptId, StringComparison.OrdinalIgnoreCase))
            {
                lblTokenInfo.ForeColor = Color.FromArgb(200, 50, 50);
                lblTokenInfo.Text = "The token ticket cannot be the same as the main ticket.";
                return;
            }

            if (!info.HasPayments)
            {
                lblTokenInfo.ForeColor = Color.FromArgb(200, 50, 50);
                lblTokenInfo.Text = "The token ticket has no payment to transfer.";
                return;
            }

            _token = info;
            lblTokenInfo.ForeColor = Color.FromArgb(50, 50, 50);
            lblTokenInfo.Text = $"Token Ticket: #{info.SaleReceiptNumber}    Tip to transfer: {Money(info.TipAmount)}";
            btnApply.Enabled = true;

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

        private void ApplyTip()
        {
            if (_token == null) return;

            var confirm = MessageBox.Show(
                $"Apply {Money(_token.TipAmount)} tip from token #{_token.SaleReceiptNumber} to ticket #{_mainReceiptNumber}?\n\nThis action cannot be undone.",
                "Add Tip By Sale Token", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            bool ok = _service.AddTipByTokenReceipt(_mainReceiptId, _token.SaleReceiptID);
            if (ok)
            {
                MessageBox.Show(
                    $"Tip of {Money(_token.TipAmount)} from token #{_token.SaleReceiptNumber} was applied to ticket #{_mainReceiptNumber}.",
                    "Add Tip By Sale Token", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show("The tip could not be applied. No changes were made.",
                    "Add Tip By Sale Token", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static string Money(decimal v)
        {
            return (v < 0 ? "-$" : "$") + Math.Abs(v).ToString("F2");
        }
    }
}
