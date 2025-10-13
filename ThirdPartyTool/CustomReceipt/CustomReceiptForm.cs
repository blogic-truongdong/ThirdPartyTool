using Dapper;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ThirdPartyTool.Models;

namespace ThirdPartyTool.CustomReceipt
{
    public partial class CustomReceiptForm : Form
    {
        public CustomReceiptForm()
        {
            InitializeComponent();

            txtSaleReceiptNumber.KeyDown += TxtSaleReceiptNumber_KeyDown;

            btnSearch.Click += BtnSearch_Click;
        }

        private async void BtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtSaleReceiptNumber.Text))
                {
                    MessageBox.Show("Ticket # cannot empty.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                using (SqlConnection conn = new SqlConnection(Utility.BLOGICPOS7_Connection))
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();

                    string sql = $"select * from SaleReceipts where SaleReceiptNumber = '{txtSaleReceiptNumber.Text.Trim()}'";
                    var listSaleReceipts = await conn.QueryAsync<SaleReceiptEntity>(sql, null, null, 180, System.Data.CommandType.Text);

                    SaleReceiptEntity mainSaleReceipt = null;
                    if (listSaleReceipts?.Count() > 1)
                    {

                    }
                    else if (listSaleReceipts?.Count() == 1)
                    {
                        mainSaleReceipt = listSaleReceipts.ToList()[0];
                    }

                    sql = $"select a.*,b.PaymentMethodName from SaleReceiptPaymentDetails a left join PaymentMethods b on a.PaymentMethodID = b.PaymentMethodID where SaleReceiptID = '{mainSaleReceipt.SaleReceiptID}'";
                    var listSaleReceiptPaymentDetails = await conn.QueryAsync<SaleReceiptPaymentDetailEntity>(sql, null, null, 180, System.Data.CommandType.Text);

                    sql = $"select * from SaleReceiptDetails where SaleReceiptID = '{mainSaleReceipt.SaleReceiptID}' and Status <> '2'";
                    var listSaleReceiptDetails = await conn.QueryAsync<SaleReceiptDetailEntity>(sql, null, null, 180, System.Data.CommandType.Text);

                    pnlDetails.Visible = true;

                    lblTicketNumber.Text = mainSaleReceipt.SaleReceiptNumber;


                    #region Receipt Details
                    decimal gratuity = listSaleReceiptPaymentDetails.Sum(x => x.Gratuity);

                    pnlReceiptDetails.Controls.Clear();

                    #region Final Total
                    ReceiptDetail_Row receiptDetail_Row = new ReceiptDetail_Row();
                    receiptDetail_Row.Dock = DockStyle.Top;
                    receiptDetail_Row.lblName.Text = "Final Total";
                    receiptDetail_Row.lblName.Font = new Font("Roboto", 10, FontStyle.Bold);

                    receiptDetail_Row.lblValue.Text = Utility.ConvertToMoneyString(mainSaleReceipt.TotalAmount + mainSaleReceipt.SVCFeePaidAmount - mainSaleReceipt.CashDiscountPaidAmount + gratuity);
                    receiptDetail_Row.lblValue.Font = new Font("Roboto", 10, FontStyle.Bold);

                    pnlReceiptDetails.Controls.Add(receiptDetail_Row);
                    #endregion

                    #region Tax
                    if (mainSaleReceipt.TaxAmount > 0)
                    {
                        receiptDetail_Row = new ReceiptDetail_Row();
                        receiptDetail_Row.Dock = DockStyle.Top;
                        receiptDetail_Row.lblName.Text = "Tax";
                        receiptDetail_Row.lblValue.Text = Utility.ConvertToMoneyString(mainSaleReceipt.TaxAmount);
                        pnlReceiptDetails.Controls.Add(receiptDetail_Row);
                    }
                    #endregion

                    #region Auto Service Charge
                    if (mainSaleReceipt.AutoGratuityAmount != 0)
                    {
                        receiptDetail_Row = new ReceiptDetail_Row();

                        receiptDetail_Row.Dock = DockStyle.Top;

                        receiptDetail_Row.lblName.Text = "Auto Service Charge";
                        receiptDetail_Row.lblValue.Text = Utility.ConvertToMoneyString(mainSaleReceipt.AutoGratuityAmount);

                        pnlReceiptDetails.Controls.Add(receiptDetail_Row);
                    }
                    #endregion

                    #region Party Service Fee
                    if (mainSaleReceipt.PartyServiceFeeAmount != 0)
                    {
                        receiptDetail_Row = new ReceiptDetail_Row();

                        receiptDetail_Row.Dock = DockStyle.Top;

                        receiptDetail_Row.lblName.Text = "Party Service Fee";
                        receiptDetail_Row.lblValue.Text = Utility.ConvertToMoneyString(mainSaleReceipt.PartyServiceFeeAmount);

                        pnlReceiptDetails.Controls.Add(receiptDetail_Row);
                    }
                    #endregion

                    #region Area Fee
                    if (mainSaleReceipt.AreaFeeAmount != 0)
                    {
                        receiptDetail_Row = new ReceiptDetail_Row();

                        receiptDetail_Row.Dock = DockStyle.Top;

                        receiptDetail_Row.lblName.Text = "Area Fee";
                        receiptDetail_Row.lblValue.Text = Utility.ConvertToMoneyString(mainSaleReceipt.AreaFeeAmount);

                        pnlReceiptDetails.Controls.Add(receiptDetail_Row);
                    }
                    #endregion

                    #region Service Charge
                    var serviceChargeAmount = mainSaleReceipt.ServiceFee + mainSaleReceipt.ExtraFeeAmount;
                    if (serviceChargeAmount != 0)
                    {
                        receiptDetail_Row = new ReceiptDetail_Row();

                        receiptDetail_Row.Dock = DockStyle.Top;

                        receiptDetail_Row.lblName.Text = "Service Charge";
                        receiptDetail_Row.lblValue.Text = Utility.ConvertToMoneyString(mainSaleReceipt.ServiceFee + mainSaleReceipt.ExtraFeeAmount);

                        pnlReceiptDetails.Controls.Add(receiptDetail_Row);
                    }
                    #endregion

                    #region Discount
                    var discountAmount = listSaleReceiptDetails.Sum(x => x.DiscountAmount + x.ItemDiscountByPriceLevelAmount + x.ItemDiscountValue + x.StoreDiscountValue + x.TotalSaleDiscountAmount + x.TotalSaleDiscountAmountByCustomer);
                    if (discountAmount != 0)
                    {
                        receiptDetail_Row = new ReceiptDetail_Row();

                        receiptDetail_Row.Dock = DockStyle.Top;

                        receiptDetail_Row.lblName.Text = "Discount";
                        receiptDetail_Row.lblValue.Text = Utility.ConvertToMoneyString(discountAmount * -1);

                        receiptDetail_Row.lblName.ForeColor = Color.Red;
                        receiptDetail_Row.lblValue.ForeColor = Color.Red;

                        pnlReceiptDetails.Controls.Add(receiptDetail_Row);
                    }
                    #endregion

                    #region SubTotal
                    receiptDetail_Row = new ReceiptDetail_Row();
                    receiptDetail_Row.Dock = DockStyle.Top;
                    receiptDetail_Row.lblName.Text = "SubTotal";
                    receiptDetail_Row.lblValue.Text = Utility.ConvertToMoneyString(listSaleReceiptDetails.Sum(x => x.ExtendedPrice));
                    pnlReceiptDetails.Controls.Add(receiptDetail_Row);
                    #endregion

                    #region Gratuity
                    if (gratuity > 0)
                    {
                        receiptDetail_Row = new ReceiptDetail_Row();
                        receiptDetail_Row.Dock = DockStyle.Top;
                        receiptDetail_Row.lblName.Text = "Tip";
                        receiptDetail_Row.lblValue.Text = Utility.ConvertToMoneyString(gratuity);
                        pnlReceiptDetails.Controls.Add(receiptDetail_Row);
                    }
                    #endregion

                    #region Payment method details
                    pnlPaymentMethodDetails.Controls.Clear();

                    foreach (var item in listSaleReceiptPaymentDetails)
                    {
                        var row = new ReceiptDetail_Row();
                        row.Dock = DockStyle.Bottom;
                        row.lblName.Text = item.CreditCardTransactionCardNumber?.Length >= 4 ? item.PaymentMethodName + $" (...{item.CreditCardTransactionCardNumber.Substring(item.CreditCardTransactionCardNumber.Length - 4)})" : item.PaymentMethodName;
                        row.lblValue.Text = Utility.ConvertToMoneyString(item.AmountTendered - item.AmountReturn + item.Gratuity);
                        pnlPaymentMethodDetails.Controls.Add(row);
                    }
                    #endregion

                    #endregion


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Search Error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                BLogicLogger.Error("BtnSearch_Click in CustomReceipt", ex);
            }
        }

        private void TxtSaleReceiptNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BtnSearch_Click(null, null);
            }
        }
    }
}
