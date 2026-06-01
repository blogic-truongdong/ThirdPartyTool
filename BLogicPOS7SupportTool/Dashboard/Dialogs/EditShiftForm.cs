using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace BLogicPOS7SupportTool.Dashboard.Dialogs
{
    public partial class EditShiftForm : Form
    {
        // Amounts the caller reads back after DialogResult.OK.
        public decimal OpeningAmount { get; private set; }
        public decimal ClosingAmount { get; private set; }

        private Point _dragScreenStart;

        public EditShiftForm(string workstation, string openingTime, string closingTime,
            decimal openingAmount, decimal closingAmount)
        {
            InitializeComponent();

            txtOpeningAmount.Leave += Amount_Leave;
            txtClosingAmount.Leave += Amount_Leave;

            var inv = CultureInfo.InvariantCulture;
            lblWorkstationValue.Text = string.IsNullOrWhiteSpace(workstation) ? "-" : workstation;
            lblOpeningTimeValue.Text = string.IsNullOrWhiteSpace(openingTime) ? "-" : openingTime;
            lblClosingTimeValue.Text = string.IsNullOrWhiteSpace(closingTime) ? "-" : closingTime;
            txtOpeningAmount.Text = openingAmount.ToString("0.00", inv);
            txtClosingAmount.Text = closingAmount.ToString("0.00", inv);

            OpeningAmount = openingAmount;
            ClosingAmount = closingAmount;
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
            if (!TryParseAmount(txtOpeningAmount.Text, out var opening))
            {
                MessageBox.Show("Opening Amount must be a valid number.",
                    "Invalid Amount", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtOpeningAmount.Focus();
                return;
            }

            if (!TryParseAmount(txtClosingAmount.Text, out var closing))
            {
                MessageBox.Show("Closing Amount must be a valid number.",
                    "Invalid Amount", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtClosingAmount.Focus();
                return;
            }

            OpeningAmount = opening;
            ClosingAmount = closing;
            DialogResult = DialogResult.OK;
            Close();
        }

        // Re-format an amount field to two decimals once the user leaves it.
        private void Amount_Leave(object sender, EventArgs e)
        {
            if (sender is TextBox tb && TryParseAmount(tb.Text, out var amount))
                tb.Text = amount.ToString("0.00", CultureInfo.InvariantCulture);
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
