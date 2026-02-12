using BLogicSystems.XPos.Utilites;
using Microsoft.Practices.CompositeUI.Utility;
using System.Data;
using System.Windows.Forms;

namespace ThirdPartyTool.SaleReceipt
{
    public partial class SelectSaleReceiptPaymentDetailForm : Form
    {
        public string SaleReceiptPaymentDetailID = string.Empty;

        public SelectSaleReceiptPaymentDetailForm()
        {
            InitializeComponent();

            this.btnCancel.Click += BtnCancel_Click;
            this.btnOK.Click += BtnOK_Click;
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
            }
            catch (System.Exception ex)
            {
                BLogicLogger.Exception("Control_PaymentDetailSelected in SelectSaleReceiptPaymentDetailForm", ex);
            }
        }
    }
}
