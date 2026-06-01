using BLogicPOS7SupportTool.Models;
using BLogicPOS7SupportTool.Services;
using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace BLogicPOS7SupportTool.Dashboard.Dialogs
{
    partial class InsertTransactionForm : Form
    {
        private readonly ReceiptService _service = new ReceiptService();
        private Point _dragStart;

        public InsertTransactionForm()
        {
            InitializeComponent();

            pnlTitleBar.MouseDown += (s, e) => { if (e.Button == MouseButtons.Left) _dragStart = e.Location; };
            pnlTitleBar.MouseMove += (s, e) => { if (e.Button == MouseButtons.Left) Location = new Point(Location.X + e.X - _dragStart.X, Location.Y + e.Y - _dragStart.Y); };

            btnCloseForm.Click += (s, e) => { DialogResult = DialogResult.Cancel; Close(); };
            btnCancel.Click += (s, e) => { DialogResult = DialogResult.Cancel; Close(); };
            btnInsert.Click += (s, e) => InsertTransaction();
        }

        private void InsertTransaction()
        {
            var inv = CultureInfo.InvariantCulture;

            // Amount is required and must be a non-negative number.
            if (!TryParseMoney(txtAmount.Text, out decimal amount) || amount < 0)
            {
                MessageBox.Show("Please enter a valid Amount (a non-negative number).",
                    "Insert Transaction Detail", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAmount.Focus();
                return;
            }

            // Gratuity is optional; blank counts as 0.
            decimal gratuity = 0m;
            if (!string.IsNullOrWhiteSpace(txtGratuity.Text) &&
                (!TryParseMoney(txtGratuity.Text, out gratuity) || gratuity < 0))
            {
                MessageBox.Show("Please enter a valid Gratuity (a non-negative number), or leave it blank.",
                    "Insert Transaction Detail", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtGratuity.Focus();
                return;
            }

            var req = new TransactionDetail
            {
                Amount = amount,
                Gratuity = gratuity,
                CardNumber = txtCardNumber.Text.Trim(),
                CardType = txtCardType.Text.Trim(),
                AuthCode = txtAuthCode.Text.Trim(),
                RefNo = txtRefNo.Text.Trim(),
                BatchID = txtBatchID.Text.Trim(),
                MerchantID = txtMerchantID.Text.Trim(),
                TerminalIpAddress = txtTerminalIP.Text.Trim()
            };

            var confirm = MessageBox.Show(
                $"Insert a new transaction of {Money(amount + gratuity)} into the Merchant database?\n\n" +
                "This adds a new row to TransactionDetail. This action cannot be undone.",
                "Insert Transaction Detail", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            bool ok;
            try
            {
                ok = _service.InsertTransactionDetail(req);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"The transaction could not be inserted.\n\n{ex.Message}\n\nNo changes were made.",
                    "Insert Transaction Detail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (ok)
            {
                MessageBox.Show("The transaction was inserted into the Merchant database.",
                    "Insert Transaction Detail", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show("The transaction could not be inserted. No changes were made.",
                    "Insert Transaction Detail", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Parses a money value, tolerating a leading "$" and thousands separators.
        private static bool TryParseMoney(string text, out decimal value)
        {
            text = (text ?? "").Trim().Replace("$", "");
            return decimal.TryParse(text, NumberStyles.Number, CultureInfo.InvariantCulture, out value)
                || decimal.TryParse(text, NumberStyles.Number, CultureInfo.CurrentCulture, out value);
        }

        private static string Money(decimal v)
        {
            return (v < 0 ? "-$" : "$") + Math.Abs(v).ToString("F2");
        }
    }
}
