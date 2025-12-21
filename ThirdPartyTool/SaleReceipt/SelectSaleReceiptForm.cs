using BLogicSystems.XPos.Utilites;
using System.Data;
using System.Windows.Forms;

namespace ThirdPartyTool.SaleReceipt
{
    public partial class SelectSaleReceiptForm : Form
    {
        public string SaleReceiptID = string.Empty;
        public string TransactionStatusID = string.Empty;
        public string SaleReceiptNumber = string.Empty;

        public SelectSaleReceiptForm()
        {
            InitializeComponent();

            this.Load += SelectSaleReceiptForm_Load;
        }

        private void SelectSaleReceiptForm_Load(object sender, System.EventArgs e)
        {
            RecalculatePaddingForTableLayoutPanel();
        }

        private void RecalculatePaddingForTableLayoutPanel()
        {
            try
            {
                int count = this.pnlListReceipt.Controls.Count;
                int width = count == 1 ? 270 : 540;
                int height = count <= 3 ? 160 : 320;
                int paddingLeft = (this.pnlListReceipt.Width - width) / 2;
                int paddingTop = (this.pnlListReceipt.Height - height) / 2;
                pnlListReceipt.Padding = new Padding(paddingLeft, 0, 0, 0);
            }
            catch (System.Exception ex)
            {
                BLogicLogger.Exception("RecalculatePaddingForTableLayoutPanel in SelectSaleReceiptForm", ex);
            }
        }

        public void BindingReceiptList(DataTable dtReceipt, DataTable dtSaleReceiptPaymentDetails)
        {
            try
            {
                pnlListReceipt.Controls.Clear();
                foreach (DataRow item in dtReceipt.Rows)
                {
                    ReceiptInfoControl receiptInfoControl = new ReceiptInfoControl();

                    string transactionStatus = Utility.GetStringFromRow("TransactionStatusID", item);
                    string saleReceiptID = Utility.GetStringFromRow("SaleReceiptID", item);

                    switch (transactionStatus)
                    {
                        case "OPER":
                            receiptInfoControl.lblStatusValue.Text = "Open";
                            break;
                        case "FIN":
                            receiptInfoControl.lblStatusValue.Text = "Finished";
                            break;
                        case "CANR":
                            receiptInfoControl.lblStatusValue.Text = "Cancelled";
                            break;
                        case "VOID":
                            receiptInfoControl.lblStatusValue.Text = "Voided";
                            break;
                        case "RET":
                            receiptInfoControl.lblStatusValue.Text = "Returned";
                            break;
                        default:
                            continue;
                    }
                    receiptInfoControl.CloseReceiptInfoEvent += ReceiptInfoControl_CloseReceiptInfoEvent;
                    receiptInfoControl.SaleReceiptID = saleReceiptID;
                    receiptInfoControl.TransactionStatusID = transactionStatus;
                    receiptInfoControl.SaleReceiptNumber = Utility.GetStringFromRow("SaleReceiptNumber", item);

                    receiptInfoControl.lblDateValue.Text = Utility.GetDateTimeFromRow("DateCreated", item).ToString();
                    receiptInfoControl.lblTicketNumberValue.Text = Utility.GetStringFromRow("SaleReceiptNumber", item);

                    receiptInfoControl.lblTotalValue.Text = Utility.ConvertDecimalToMoneyString(Utility.GetDecimalFromRow("TotalAmount", item) +
                        Utility.GetDecimalFromRow("SVCFeePaidAmount", item) -
                        Utility.GetDecimalFromRow("CashDiscountPaidAmount", item) -
                        Utility.GetDecimalFromRow("CoinDiscountAmount", item));

                    receiptInfoControl.lblTipValue.Text = Utility.ConvertDecimalToMoneyString(Utility.GetDecimalFromObject(dtSaleReceiptPaymentDetails?.Compute("SUM(Gratuity)", $"SaleReceiptID = '{saleReceiptID}'")));

                    receiptInfoControl.lblFinalTotalValue.Text = Utility.ConvertDecimalToMoneyString(Utility.GetDecimalFromRow("TotalAmount", item) +
                        Utility.GetDecimalFromRow("SVCFeePaidAmount", item) -
                        Utility.GetDecimalFromRow("CashDiscountPaidAmount", item) -
                        Utility.GetDecimalFromRow("CoinDiscountAmount", item) +
                        Utility.GetDecimalFromObject(dtSaleReceiptPaymentDetails?.Compute("SUM(Gratuity)", $"SaleReceiptID = '{saleReceiptID}'")));

                    pnlListReceipt.Controls.Add(receiptInfoControl);
                }

                RecalculatePaddingForTableLayoutPanel();
            }
            catch (System.Exception ex)
            {
                BLogicLogger.Exception("BindingReceiptList in SelectSaleReceiptForm", ex);
            }
        }

        private void ReceiptInfoControl_CloseReceiptInfoEvent(object sender, System.EventArgs e)
        {
            if (sender != null && (sender as ReceiptInfoControl) != null)
            {
                SaleReceiptID = Utility.GetStringFromObject((sender as ReceiptInfoControl).SaleReceiptID);
                TransactionStatusID = Utility.GetStringFromObject((sender as ReceiptInfoControl).TransactionStatusID);
                SaleReceiptNumber = Utility.GetStringFromObject((sender as ReceiptInfoControl).SaleReceiptNumber);
                this.Close();
            }

        }
    }
}
