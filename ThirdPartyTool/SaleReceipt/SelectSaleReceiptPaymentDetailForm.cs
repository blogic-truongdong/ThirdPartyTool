using BLogicSystems.XPos.Utilites;
using Microsoft.Practices.CompositeUI.Utility;
using System.Data;
using System.Windows.Forms;

namespace ThirdPartyTool.SaleReceipt
{
    public partial class SelectSaleReceiptPaymentDetailForm : Form
    {
        public string SaleReceiptPaymentDetailID = string.Empty;
        public string SaleReceiptID = string.Empty;
        public SelectSaleReceiptPaymentDetailForm()
        {
            InitializeComponent();

            this.btnCancel.Click += BtnCancel_Click;
            this.btnOK.Click += BtnOK_Click;

            this.Load += SelectSaleReceiptPaymentDetailForm_Load;
        }

        private void SelectSaleReceiptPaymentDetailForm_Load(object sender, System.EventArgs e)
        {

        }

        private void BtnOK_Click(object sender, System.EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(SaleReceiptPaymentDetailID))
                return;

            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void BtnCancel_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        public void BindingData(DataTable dtSaleReceiptPaymentDetails)
        {
            try
            {
                foreach (DataRow item in dtSaleReceiptPaymentDetails.Rows)
                {
                    decimal amount = Utility.GetDecimalFromRow("AmountTendered", item) -
                        Utility.GetDecimalFromRow("AmountReturn", item) +
                        Utility.GetDecimalFromRow("SVCFeeAmount", item) -
                        Utility.GetDecimalFromRow("CashDiscountAmount", item) -
                        Utility.GetDecimalFromRow("CoinDiscountAmount", item);

                    decimal Gratuity = Utility.GetDecimalFromRow("Gratuity", item);

                    SaleReceiptPaymentDetailControl control = new SaleReceiptPaymentDetailControl();
                    control.lblPaymentTypeValue.Text = Utility.GetStringFromRow("PaymentMethodName", item);
                    control.lblTotalValue.Text = Utility.ConvertDecimalToMoneyString(amount);
                    control.lblTipValue.Text = Utility.ConvertDecimalToMoneyString(Gratuity);
                    control.lblFinalTotalValue.Text = Utility.ConvertDecimalToMoneyString(amount + Gratuity);
                    control.SaleReceiptPaymentDetailID = Utility.GetStringFromRow("SaleReceiptPaymentDetailID", item);

                    control.PaymentDetailSelected += Control_PaymentDetailSelected;

                    pnlListReceipt.Controls.Add(control);
                }

                // Fit up to 4 payments without scrolling.
                const int rowHeight = 76; // 66 card + 10 gap
                int rows = System.Math.Max(1, System.Math.Min(4, pnlListReceipt.Controls.Count));
                ClientSize = new System.Drawing.Size(ClientSize.Width, pnlHeader.Height + rows * rowHeight + pnlFooter.Height + Padding.Vertical);
            }
            catch (System.Exception ex)
            {
                BLogicLogger.Exception("SelectSaleReceiptPaymentDetailForm.BindingData", ex);
            }
        }

        private void Control_PaymentDetailSelected(object sender, DataEventArgs<string> e)
        {
            try
            {
                SaleReceiptPaymentDetailControl saleReceiptPayment = sender as SaleReceiptPaymentDetailControl;
                if (saleReceiptPayment == null)
                    return;

                SaleReceiptPaymentDetailID = saleReceiptPayment.SaleReceiptPaymentDetailID;

                foreach (SaleReceiptPaymentDetailControl item in pnlListReceipt.Controls)
                {
                    if (item.SaleReceiptPaymentDetailID != saleReceiptPayment.SaleReceiptPaymentDetailID)
                        item.UpdateSelectedItem(false);
                }

                // A payment is chosen: Continue becomes the primary button.
                System.Drawing.Color primary = System.Drawing.Color.FromArgb(42, 108, 228);
                btnOK.BackColor = btnOK.BackgroundColor = btnOK.BorderColor = primary;
                btnOK.ForeColor = btnOK.TextColor = System.Drawing.Color.White;
                btnOK.Cursor = Cursors.Hand;
                btnOK.Invalidate();
            }
            catch (System.Exception ex)
            {
                BLogicLogger.Exception("Control_PaymentDetailSelected in SelectSaleReceiptPaymentDetailForm", ex);
            }
        }
    }
}
