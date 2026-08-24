using BLogicPOS7SupportTool.Models;
using BLogicPOS7SupportTool.Services;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace BLogicPOS7SupportTool.Dashboard.Dialogs
{
    partial class FinishTicketForm : Form
    {
        private readonly ReceiptService _service;
        private readonly string _receiptId;
        private string _receiptNumber;
        private Point _dragStart;
        private FinishTicketEntity _info;

        public FinishTicketForm(ReceiptService service, string receiptId, string receiptNumber)
        {
            InitializeComponent();
            _service = service;
            _receiptId = receiptId;
            _receiptNumber = receiptNumber;

            pnlTitleBar.MouseDown += (s, e) => { if (e.Button == MouseButtons.Left) _dragStart = e.Location; };
            pnlTitleBar.MouseMove += (s, e) => { if (e.Button == MouseButtons.Left) Location = new Point(Location.X + e.X - _dragStart.X, Location.Y + e.Y - _dragStart.Y); };

            btnCloseForm.Click += (s, e) => { DialogResult = DialogResult.Cancel; Close(); };
            btnCancel.Click += (s, e) => { DialogResult = DialogResult.Cancel; Close(); };
            btnOK.Click += (s, e) => Finish();

            cmbPaymentMethod.SelectedIndexChanged += (s, e) => UpdateCardPanel();
            txtGratuity.KeyPress += MoneyKeyPress;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            dtpDateTime.Value = DateTime.Now;

            _info = _service.GetFinishInfo(_receiptId);
            _receiptNumber = string.IsNullOrEmpty(_info.SaleReceiptNumber) ? _receiptNumber : _info.SaleReceiptNumber;
            lblTicket.Text = "Ticket #" + _receiptNumber;

            rbBase.Text = "Base  —  " + Money(_info.Base);
            rbBaseNoTax.Text = "No Tax  —  " + Money(_info.BaseNoTax);
            rbIncludeSVCFee.Text = "With SVC Fee  —  " + Money(_info.IncludeSVCFee);
            rbIncludeSVCFeeNoTax.Text = "No Tax + SVC Fee  —  " + Money(_info.IncludeSVCFeeNoTax);
            rbIncludeCashDiscount.Text = "With Cash Discount  —  " + Money(_info.IncludeCashDiscount);
            rbIncludeCashDiscountNoTax.Text = "No Tax + Cash Discount  —  " + Money(_info.IncludeCashDiscountNoTax);

            cmbPaymentMethod.DataSource = _service.GetPaymentMethodOptions();
            cmbBatch.DataSource = _service.GetSettledBatches();

            UpdateCardPanel();
        }

        private PaymentMethodOption SelectedMethod => cmbPaymentMethod.SelectedItem as PaymentMethodOption;

        private void UpdateCardPanel()
        {
            pnlCard.Visible = SelectedMethod != null && SelectedMethod.PaymentMethodID == 2;
        }

        private FinishAmountMode SelectedMode()
        {
            if (rbBaseNoTax.Checked) return FinishAmountMode.BaseNoTax;
            if (rbIncludeSVCFee.Checked) return FinishAmountMode.IncludeSVCFee;
            if (rbIncludeSVCFeeNoTax.Checked) return FinishAmountMode.IncludeSVCFeeNoTax;
            if (rbIncludeCashDiscount.Checked) return FinishAmountMode.IncludeCashDiscount;
            if (rbIncludeCashDiscountNoTax.Checked) return FinishAmountMode.IncludeCashDiscountNoTax;
            return FinishAmountMode.Base;
        }

        private void Finish()
        {
            var method = SelectedMethod;
            if (method == null)
            {
                MessageBox.Show("Please select a payment method.", "Finish Ticket",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtGratuity.Text, out var gratuity) || gratuity < 0)
            {
                MessageBox.Show("Please enter a valid gratuity amount.", "Finish Ticket",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var batch = cmbBatch.SelectedItem as BatchOption;
            if (method.PaymentMethodID == 2 && batch == null)
            {
                MessageBox.Show("Please select a batch for the credit card payment.", "Finish Ticket",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var mode = SelectedMode();
            decimal amount = _info.AmountFor(mode);

            var confirm = MessageBox.Show(
                $"Finish ticket #{_receiptNumber} with a {method.PaymentMethodName} payment of {Money(amount)}" +
                (gratuity > 0 ? $" (+ {Money(gratuity)} tip)" : "") + "?\n\nThis action cannot be undone.",
                "Finish Ticket", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            var req = new FinishTicketRequest
            {
                PaymentMethodID = method.PaymentMethodID,
                Mode = mode,
                Gratuity = gratuity,
                CreatedDate = dtpDateTime.Value,
                CardNumber = txtCardNumber.Text.Trim(),
                CardType = txtCardType.Text.Trim(),
                ExpiryDate = txtExpiry.Text.Trim(),
                AuthCode = txtAuthCode.Text.Trim(),
                ReferenceNumber = txtReference.Text.Trim(),
                Note = string.IsNullOrWhiteSpace(txtNote.Text) ? "Insert by BLogic Tools" : txtNote.Text.Trim(),
                BatchUnique = batch?.BatchUnique,
                MerchantID = batch?.MerchantID,
                BatchID = batch?.BatchID
            };

            bool ok = _service.FinishTicket(_receiptId, req);
            if (ok)
            {
                MessageBox.Show($"Ticket #{_receiptNumber} has been finished.", "Finish Ticket",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show("The ticket could not be finished. No changes were made.", "Finish Ticket",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MoneyKeyPress(object sender, KeyPressEventArgs e)
        {
            var txt = (TextBox)sender;
            if (char.IsControl(e.KeyChar)) return;
            if (char.IsDigit(e.KeyChar)) return;
            if (e.KeyChar == '.' && !txt.Text.Contains(".")) return;
            e.Handled = true;
        }

        private static string Money(decimal v)
        {
            return (v < 0 ? "-$" : "$") + Math.Abs(v).ToString("F2");
        }
    }
}
