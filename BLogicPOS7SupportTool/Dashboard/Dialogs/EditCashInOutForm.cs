using BLogicPOS7SupportTool.Services;
using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace BLogicPOS7SupportTool.Dashboard.Dialogs
{
    public partial class EditCashInOutForm : Form
    {
        // Values the caller reads back after DialogResult.OK.
        public short Type { get; private set; }
        public decimal Amount { get; private set; }
        public string Comment { get; private set; }

        private Point _dragScreenStart;

        public EditCashInOutForm(string employee, short type, decimal amount, string comment)
        {
            InitializeComponent();

            txtAmount.Leave += TxtAmount_Leave;

            var inv = CultureInfo.InvariantCulture;
            lblEmployeeValue.Text = string.IsNullOrWhiteSpace(employee) ? "-" : employee;
            cmbType.Items.Add("Cash In");
            cmbType.Items.Add("Cash Out");
            cmbType.SelectedIndex = type == CashInOutService.TypeCashIn ? 0 : 1;
            txtAmount.Text = amount.ToString("0.00", inv);
            txtComment.Text = comment ?? "";

            Type = type;
            Amount = amount;
            Comment = comment ?? "";
        }

        private void TitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                _dragScreenStart = Control.MousePosition;
        }

        private void TitleBar_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                var curr = Control.MousePosition;
                Location = new Point(
                    Location.X + curr.X - _dragScreenStart.X,
                    Location.Y + curr.Y - _dragScreenStart.Y);
                _dragScreenStart = curr;
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (!TryParseAmount(txtAmount.Text, out var amount))
            {
                MessageBox.Show("Amount must be a valid number.",
                    "Invalid Amount", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAmount.Focus();
                return;
            }

            Type = cmbType.SelectedIndex == 0 ? CashInOutService.TypeCashIn : CashInOutService.TypeCashOut;
            Amount = amount;
            Comment = txtComment.Text.Trim();
            DialogResult = DialogResult.OK;
            Close();
        }

        // Re-format the amount to two decimals once the user leaves the field.
        private void TxtAmount_Leave(object sender, EventArgs e)
        {
            if (TryParseAmount(txtAmount.Text, out var amount))
                txtAmount.Text = amount.ToString("0.00", CultureInfo.InvariantCulture);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnCloseDlg_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        // Accepts both the invariant ("1234.50") and the current-culture form.
        private static bool TryParseAmount(string text, out decimal value)
        {
            text = (text ?? "").Trim();
            const NumberStyles styles = NumberStyles.Number;
            return decimal.TryParse(text, styles, CultureInfo.InvariantCulture, out value)
                || decimal.TryParse(text, styles, CultureInfo.CurrentCulture, out value);
        }
    }
}
