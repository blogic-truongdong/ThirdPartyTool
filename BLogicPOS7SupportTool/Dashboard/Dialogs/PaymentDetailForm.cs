using BLogicPOS7SupportTool.Core;
using BLogicPOS7SupportTool.Models;
using BLogicPOS7SupportTool.Services;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace BLogicPOS7SupportTool.Dashboard.Dialogs
{
    partial class PaymentDetailForm : Form
    {
        private readonly ReceiptService _service;
        private readonly ReceiptPaymentDetail _detail;
        private Point _dragStart;

        public PaymentDetailForm(ReceiptService service, ReceiptPaymentDetail detail)
        {
            InitializeComponent();
            _service = service;
            _detail = detail;

            pnlTitleBar.MouseDown += (s, e) => { if (e.Button == MouseButtons.Left) _dragStart = e.Location; };
            pnlTitleBar.MouseMove += (s, e) => { if (e.Button == MouseButtons.Left) Location = new Point(Location.X + e.X - _dragStart.X, Location.Y + e.Y - _dragStart.Y); };

            btnCloseForm.Click += (s, e) => { DialogResult = DialogResult.Cancel; Close(); };
            btnCancel.Click += (s, e) => { DialogResult = DialogResult.Cancel; Close(); };
            btnUpdate.Click += (s, e) => ApplyUpdate();

            this.Shown += PaymentDetailForm_Shown;
        }

        private void PaymentDetailForm_Shown(object sender, EventArgs e)
        {

            foreach (var t in new[] { txtTender, txtReturn, txtSvcFee, txtSvcFeeTax, txtCashDisc, txtCashDiscTax, txtGratuity, txtSvcGratuity })
            {
                t.KeyPress += MoneyKeyPress;
                t.Leave += MoneyLeave;
            }

            txtTender.TextChanged += (s, ee) => UpdateTransactionTotal();
            txtReturn.TextChanged += (s, ee) => UpdateTransactionTotal();
            txtSvcFee.TextChanged += TxtSvcFee_TextChanged;
            txtCashDisc.TextChanged += TxtCashDisc_TextChanged;
            txtGratuity.TextChanged += TxtGratuity_TextChanged;

        }

        private void TxtSvcFee_TextChanged(object sender, EventArgs e)
        {
            decimal.TryParse(txtSvcFee.Text, out decimal svcFee);
            txtSvcFeeTax.Text = AppSession.CalculateSVCFeeTaxAmountFromSVCFeeAmount(svcFee).ToString("F2");

            UpdateTransactionTotal();
        }

        private void TxtCashDisc_TextChanged(object sender, EventArgs e)
        {
            decimal.TryParse(txtCashDisc.Text, out decimal cashDiscount);
            txtCashDiscTax.Text = AppSession.CalculateSVCFeeTaxAmountFromSVCFeeAmount(cashDiscount).ToString("F2");

            UpdateTransactionTotal();
        }

        private void TxtGratuity_TextChanged(object sender, EventArgs e)
        {
            decimal.TryParse(txtGratuity.Text, out decimal gratuity);
            txtSvcGratuity.Text = AppSession.CalculateSVCFeeFromTotalUseBaseAmountFee(gratuity).ToString("F2");

            UpdateTransactionTotal();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            txtMethod.Text = _detail.PaymentMethodName;
            txtCard.Text = _detail.CreditCardTransactionCardNumber;
            txtTender.Text = _detail.AmountTendered.ToString("F2");
            txtReturn.Text = _detail.AmountReturn.ToString("F2");
            txtSvcFee.Text = _detail.SVCFeeAmount.ToString("F2");
            txtSvcFeeTax.Text = _detail.SVCFeeTaxAmount.ToString("F2");
            txtCashDisc.Text = _detail.CashDiscountAmount.ToString("F2");
            txtCashDiscTax.Text = _detail.CashDiscountTaxAmount.ToString("F2");
            txtGratuity.Text = _detail.Gratuity.ToString("F2");
            txtSvcGratuity.Text = _detail.SVCFeeForGratuity.ToString("F2");

            UpdateTransactionTotal();
        }

        private void UpdateTransactionTotal()
        {
            decimal.TryParse(txtTender.Text, out var tender);
            decimal.TryParse(txtReturn.Text, out var ret);
            decimal.TryParse(txtSvcFee.Text, out var svcFee);
            decimal.TryParse(txtCashDisc.Text, out var cashDiscount);
            decimal.TryParse(txtGratuity.Text, out var gratuity);
            decimal total = tender - ret + svcFee - cashDiscount + gratuity;
            lblTotalValue.Text = (total < 0 ? "-$ " : "$ ") + Math.Abs(total).ToString("N2");
        }

        private void ApplyUpdate()
        {
            if (!decimal.TryParse(txtTender.Text, out var tender) || tender < 0 ||
                !decimal.TryParse(txtReturn.Text, out var ret) || ret < 0 ||
                !decimal.TryParse(txtSvcFee.Text, out var svcFee) || svcFee < 0 ||
                !decimal.TryParse(txtSvcFeeTax.Text, out var svcFeeTax) || svcFeeTax < 0 ||
                !decimal.TryParse(txtCashDisc.Text, out var cashDisc) || cashDisc < 0 ||
                !decimal.TryParse(txtCashDiscTax.Text, out var cashDiscTax) || cashDiscTax < 0 ||
                !decimal.TryParse(txtGratuity.Text, out var gratuity) || gratuity < 0 ||
                !decimal.TryParse(txtSvcGratuity.Text, out var svcGratuity) || svcGratuity < 0)
            {
                MessageBox.Show("Please enter valid, non-negative amounts in all fields.", "Payment Details",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal merchantFee = AppSession.CalculateMerchantFeeAmount(tender - ret + svcFee + gratuity);
            decimal merchantFeeForTip = AppSession.CalculateMerchantFeeAmount(gratuity);

            if (_detail.CashAdjust == 1)
            {
                svcGratuity = 0;
                merchantFee = 0;
                merchantFeeForTip = 0;
            }

            if (_detail.CashAdjust == 2)
            {
                cashDisc = 0;
                cashDiscTax = 0;
            }

            bool ok = _service.UpdatePaymentDetail(_detail.SaleReceiptPaymentDetailID,
                tender, ret, svcFee, svcFeeTax, cashDisc, cashDiscTax, gratuity, svcGratuity, merchantFee, merchantFeeForTip, _detail.CreditCardTransactionID);

            if (ok)
            {
                MessageBox.Show($"'{_detail.PaymentMethodName}' payment has been updated.", "Payment Details",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show("The payment could not be updated. No changes were made.", "Payment Details",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Re-format a money field to two decimals once the user leaves it.
        private void MoneyLeave(object sender, EventArgs e)
        {
            var txt = (TextBox)sender;
            if (decimal.TryParse(txt.Text, out var value))
                txt.Text = value.ToString("F2");
            else
                txt.Text = "0.00";
        }

        private void MoneyKeyPress(object sender, KeyPressEventArgs e)
        {
            var txt = (TextBox)sender;
            if (char.IsControl(e.KeyChar)) return;
            if (char.IsDigit(e.KeyChar)) return;
            if (e.KeyChar == '.' && !txt.Text.Contains(".")) return;
            e.Handled = true;
        }
    }
}
