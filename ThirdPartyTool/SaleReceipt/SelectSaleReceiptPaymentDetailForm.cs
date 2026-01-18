using BLogicSystems.XPos.Utilites;
using Microsoft.Practices.CompositeUI.Utility;
using System.Data;
using System.Windows.Forms;

namespace ThirdPartyTool.SaleReceipt
{
    public partial class SelectSaleReceiptPaymentDetailForm : Form
    {
        public SelectSaleReceiptPaymentDetailForm()
        {
            InitializeComponent();
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

        }
    }
}
