using Dapper;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using ThirdPartyTool.Models;

namespace ThirdPartyTool
{
    public partial class AddTipForTicketByPaymentDetailForm : Form
    {
        public AddTipForTicketByPaymentDetailForm()
        {
            InitializeComponent();

            btnGenerateScript.Click += BtnGenerateScript_Click;

            this.btnClose.Click += BtnClose_Click;

            btnCopyScript.Click += BtnCopyScript_Click;
        }

        private void BtnCopyScript_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(txtScript.Text);
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void BtnGenerateScript_Click(object sender, EventArgs e)
        {
            txtScript.Text = string.Empty;
            string sql = string.Empty;
            StringBuilder stringBuilder = new StringBuilder();
            try
            {
                if (string.IsNullOrWhiteSpace(txtMainTicketNumber.Text) || string.IsNullOrWhiteSpace(txtTokenTicketNumber.Text))
                {
                    MessageBox.Show("Ticket Number can not be empty.");
                    return;
                }

                string mainSaleReceiptNumber = txtMainTicketNumber.Text;
                string tokenSaleReceiptNumber = txtTokenTicketNumber.Text;

                using (SqlConnection conn = new SqlConnection(Utility.BLOGICPOS7_Connection))
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();

                    sql = $"select * from SaleReceipts where SaleReceiptNumber = '{mainSaleReceiptNumber}' and TransactionStatusID = 'FIN'";
                    var mainSaleReceipts = await conn.QueryAsync<SaleReceiptEntity>(sql, null, null, 180, System.Data.CommandType.Text);

                    sql = $"select * from SaleReceipts where SaleReceiptNumber = '{tokenSaleReceiptNumber}' and TransactionStatusID = 'FIN'";
                    var tokenSaleReceipts = await conn.QueryAsync<SaleReceiptEntity>(sql, null, null, 180, System.Data.CommandType.Text);

                    SaleReceiptEntity mainReceiptEntity = mainSaleReceipts.First();
                    SaleReceiptEntity tokenReceiptEntity = tokenSaleReceipts.First();

                    sql = $"select * from SaleReceiptPaymentDetails where SaleReceiptID = '{tokenReceiptEntity.SaleReceiptID}'";
                    var saleReceiptPaymentDetails = await conn.QueryAsync<SaleReceiptPaymentDetailEntity>(sql, null, null, 180, System.Data.CommandType.Text);

                    #region verify Receipt
                    if (mainSaleReceipts == null)
                    {
                        MessageBox.Show($"Can not found any Receipt with SaleReceiptNumber: {mainSaleReceiptNumber} ");
                        return;
                    }

                    if (mainSaleReceipts?.Count() > 1)
                    {
                        MessageBox.Show($"Exist more 1 ticket with SaleReceiptNumber: {mainSaleReceiptNumber} ");
                        return;
                    }

                    if (tokenSaleReceipts == null)
                    {
                        MessageBox.Show($"Can not found any Receipt with SaleReceiptNumber: {tokenSaleReceiptNumber} ");
                        return;
                    }

                    if (tokenSaleReceipts?.Count() > 1)
                    {
                        MessageBox.Show($"Exist more 1 ticket with SaleReceiptNumber: {tokenSaleReceiptNumber} ");
                        return;
                    }

                    if (!(saleReceiptPaymentDetails?.Count() > 0))
                    {
                        MessageBox.Show($"Can not found any SaleReceiptPaymentDetaisl with SaleReceiptNumber: {tokenSaleReceiptNumber} ");
                        return;
                    }
                    #endregion

                    var employeeClockInInfoEntity = await Utility.GetEmployeeClockInInfo(conn, mainReceiptEntity.CashierID, mainReceiptEntity.DateCreated);

                    if (saleReceiptPaymentDetails?.Count() > 0)
                    {
                        stringBuilder.AppendLine($"USE BLOGICPOS7");
                        stringBuilder.AppendLine($"IF EXISTS (SELECT * FROM dbo.SaleReceipts WHERE SaleReceiptID='{tokenReceiptEntity.SaleReceiptID}' and TransactionStatusID = 'FIN')");
                        stringBuilder.AppendLine($"BEGIN");
                        stringBuilder.AppendLine($"    update SaleReceipts set TransactionStatusID = 'CLO' where SaleReceiptID = '{tokenReceiptEntity.SaleReceiptID}';");
                        stringBuilder.AppendLine($"    update SaleReceiptPaymentDetails set Gratuity = AmountTendered where SaleReceiptID = '{tokenReceiptEntity.SaleReceiptID}';");
                        stringBuilder.AppendLine($"    update SaleReceiptPaymentDetails set SaleReceiptID = '{mainReceiptEntity.SaleReceiptID}', AmountTendered = 0 where SaleReceiptID = '{tokenReceiptEntity.SaleReceiptID}';");
                        stringBuilder.AppendLine("");

                        foreach (var payment in saleReceiptPaymentDetails)
                        {
                            stringBuilder.AppendLine($"    INSERT INTO PaymentRoleDetails (ID,SaleReceiptID,SaleReceiptPaymentDetailID,PaymentMethodID,PaymentMethodType,EmployeeID,RolesID,TipRate,TipRateOT,Gratuity,SVCFeeForGratuity,MerchantFeeForTip,DateCreated,PartyServiceFeeAmount,PartyServiceFeeTaxAmount,SVCFeeForPartyServiceFeeAmount,MerchantFeeForPartyServiceFeeAmount,AutoGratuityAmount,AutoGratuityTaxAmount,SVCFeeForAutoGratuityAmount,MerchantFeeForAutoGratuityAmount,AreaFeeAmount,AreaFeeTaxAmount,SVCFeeForAreaFeeAmount,MerchantFeeForAreaFeeAmount,ServiceFee,ServiceFeeTaxAmount,SVCFeeForServiceFee,MerchantFeeForServiceFee)");
                            stringBuilder.AppendLine($"    VALUES (NEWID(), '{mainReceiptEntity.SaleReceiptID}', '{payment.SaleReceiptPaymentDetailID}', '{payment.PaymentMethodID}', '2', '{mainReceiptEntity.CashierID}', '{employeeClockInInfoEntity.RolesID}', '{employeeClockInInfoEntity.TipRate}', '{employeeClockInInfoEntity.TipRateOT}', '{payment.AmountTendered}', '{payment.SVCFeeAmount}', '{payment.MerchantFee}', '{payment.CreatedDate}', '{payment.PartyServiceFeeAmount}', '{payment.PartyServiceFeeTaxAmount}', '{payment.SVCFeeForPartyServiceFeeAmount}', '{payment.MerchantFeeForPartyServiceFeeAmount}', '{payment.AutoGratuityAmount}', '{payment.AutoGratuityTaxAmount}', '{payment.SVCFeeForAutoGratuityAmount}', '{payment.MerchantFeeForAutoGratuityAmount}', '{payment.AreaFeeAmount}', '{payment.AreaFeeTaxAmount}', '{payment.SVCFeeForAreaFeeAmount}', '{payment.MerchantFeeForAreaFeeAmount}', '{payment.ServiceFee}', '{payment.ServiceFeeTaxAmount}', '{payment.SVCFeeForServiceFee}', '{payment.MerchantFeeForServiceFee}'  )");
                        }

                        stringBuilder.AppendLine($"END");
                        stringBuilder.AppendLine($"GO");
                    }

                    foreach (var item in saleReceiptPaymentDetails)
                    {

                        stringBuilder.AppendLine("");
                        stringBuilder.AppendLine("USE MERCHANT");
                        stringBuilder.AppendLine($"IF EXISTS (SELECT * FROM TransactionDetail where ID = '{item.CreditCardTransactionID}' and Gratuity = 0 and BatchStatus = 'Closed')");
                        stringBuilder.AppendLine("BEGIN");
                        stringBuilder.AppendLine($"     update Batch set TotalTips = TotalTips + '{Math.Round(item.AmountTendered, 2)}', TotalSales = TotalSales - '{Math.Round(item.AmountTendered, 2)}' where BatchUnique = (SELECT t.BatchUnique FROM TransactionDetail t where t.ID = '{item.CreditCardTransactionID}');");
                        stringBuilder.AppendLine($"END");
                        stringBuilder.AppendLine($"GO");

                        stringBuilder.AppendLine("");
                        stringBuilder.AppendLine("USE MERCHANT");
                        stringBuilder.AppendLine($"IF EXISTS (SELECT * FROM TransactionDetail where ID = '{item.CreditCardTransactionID}' and Gratuity = 0)");
                        stringBuilder.AppendLine("BEGIN");
                        stringBuilder.AppendLine($"     update TransactionDetail set Amount = 0, Gratuity = '{Math.Round(item.AmountTendered, 2)}', SubTotal= '0', TaxAmount = '0', TotalAmount = '0' where ID = '{item.CreditCardTransactionID}'");
                        stringBuilder.AppendLine($"END");
                        stringBuilder.AppendLine($"GO");
                    }
                }
            }
            catch (Exception)
            {
                stringBuilder.Clear();
            }
            finally
            {

            }

            txtScript.Text = stringBuilder.ToString();
        }
    }
}
