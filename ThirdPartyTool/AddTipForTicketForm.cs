using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ThirdPartyTool.Models;

namespace ThirdPartyTool
{
    public partial class AddTipForTicketForm : Form
    {
        public AddTipForTicketForm()
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
            string path = "newfile.sql";

            Dictionary<string, string> keyValues = new Dictionary<string, string>();
            keyValues.Add("263482", "15");
            keyValues.Add("263489", "28.95");
            keyValues.Add("263495", "5.28");
            keyValues.Add("263510", "5.03");
            keyValues.Add("263509", "5");
            keyValues.Add("261231", "10.85");
            keyValues.Add("263537", "56.66");
            keyValues.Add("261214", "5.37");
            if (File.Exists(path))
                File.Delete(path);
            using (FileStream fs = File.Create(path))
            using (StreamWriter writer = new StreamWriter(fs))
            {
                foreach (var keyValuePair in keyValues)
                {
                    txtTicketNumber.Text = keyValuePair.Key;
                    txtGratuity.Text = keyValuePair.Value;

                    StringBuilder stringBuilder = new StringBuilder();
                    try
                    {
                        string posCondition = string.Empty;
                        string crudScriptPOS = string.Empty;

                        string merchantCondition = string.Empty;
                        string crudScriptMerchant = string.Empty;

                        txtScript.Text = "";

                        if (string.IsNullOrWhiteSpace(txtTicketNumber.Text) || string.IsNullOrWhiteSpace(txtGratuity.Text))
                            return;

                        string creditCardTransactionID = string.Empty;

                        decimal.TryParse(txtGratuity.Text.Trim(), out decimal gratuity);

                        if (gratuity == 0)
                        {
                            return;
                        }

                        decimal svcFeeForTip = 0;
                        decimal merchantFeeForTip = 0;
                        decimal merchantFeeForPayment = 0;

                        decimal CashDiscount = 0;
                        decimal SVCFee = 0;
                        decimal BaseAmountFee = 0;
                        decimal MerchantFee = 0;
                        bool SVCFeeType = false;
                        using (var conn = new SqlConnection(Utility.BLOGICPOS7_Connection))
                        {
                            string sql = "select * from Configurations where ConfigurationName in (" +
                                "'CreditCardConfigValue.CashDiscount'," +
                                "'CreditCardConfigValue.SVCFee'," +
                                "'CreditCardConfigValue.SVCFeeType'," +
                                "'CreditCardConfigValue.MerchantFee'," +
                                "'CreditCardConfigValue.BaseAmountFee');";

                            var configurations = await conn.QueryAsync<ConfigurationEntity>(sql, "", null, 180, System.Data.CommandType.Text);
                            foreach (var item in configurations)
                            {
                                switch (item.ConfigurationName)
                                {
                                    case "CreditCardConfigValue.CashDiscount":
                                        CashDiscount = Utility.GetDecimalFromObject(item.ConfigurationValue);
                                        break;
                                    case "CreditCardConfigValue.SVCFee":
                                        SVCFee = Utility.GetDecimalFromObject(item.ConfigurationValue);
                                        break;
                                    case "CreditCardConfigValue.BaseAmountFee":
                                        BaseAmountFee = Utility.GetDecimalFromObject(item.ConfigurationValue);
                                        break;
                                    case "CreditCardConfigValue.MerchantFee":
                                        MerchantFee = Utility.GetDecimalFromObject(item.ConfigurationValue);
                                        break;
                                    case "CreditCardConfigValue.SVCFeeType":
                                        SVCFeeType = item.ConfigurationValue == "1";
                                        break;
                                }
                            }

                            if (SVCFee > 0 && BaseAmountFee == 0)
                            {
                                BaseAmountFee = MerchantFee;
                            }

                            sql = $"select s.CashierID as EmployeeID, s.SaleReceiptNumber, sp.*" +
                                $" from SaleReceiptPaymentDetails sp left join SaleReceipts s on sp.SaleReceiptID = s.SaleReceiptID" +
                                $" where s.SaleReceiptNumber = '{txtTicketNumber.Text.Trim()}'";

                            var listPayments = (await conn.QueryAsync<SaleReceiptPaymentDetailEntity>(sql, "", null, 180, System.Data.CommandType.Text))?.ToList();
                            SaleReceiptPaymentDetailEntity payment;
                            if (listPayments?.Count == 1)
                                payment = listPayments[0];
                            else
                            {
                                MessageBox.Show("Exist 2 payment details");
                                return;
                            }

                            if (!string.IsNullOrWhiteSpace(payment.SaleReceiptPaymentDetailID) && payment.Gratuity == 0)
                            {
                                sql = $"select top 1 * from PaymentRoleDetails where SaleReceiptID = '{listPayments[0].SaleReceiptID}'";
                                var paymentRoleDetail = await conn.QuerySingleOrDefaultAsync<PaymentRoleDetailEntity>(sql, "", null, 180, System.Data.CommandType.Text);

                                svcFeeForTip = Utility.CalculateSVCFeeAdjustTipWithPara(gratuity, SVCFee, BaseAmountFee);
                                merchantFeeForTip = Utility.CalculateMerchantFeeForTipWithParams(gratuity, SVCFee, MerchantFee, SVCFeeType);
                                merchantFeeForPayment = Utility.CalculateMerchantFeeWithParams(payment.AmountTendered + gratuity, SVCFee, MerchantFee, SVCFeeType);

                                var employeeClockInInfoEntity = await Utility.GetEmployeeClockInInfo(conn, payment.EmployeeID, payment.CreatedDate);

                                stringBuilder.AppendLine($"USE BLOGICPOS7");
                                stringBuilder.AppendLine($"IF EXISTS (SELECT * FROM SaleReceiptPaymentDetails where SaleReceiptPaymentDetailID = '{payment.SaleReceiptPaymentDetailID}' and Gratuity = 0)");
                                stringBuilder.AppendLine($"BEGIN");
                                stringBuilder.AppendLine($"    update SaleReceiptPaymentDetails set Gratuity = '{gratuity}', SVCFeeForGratuity = '{svcFeeForTip}', MerchantFeeForTip = '{merchantFeeForTip}', MerchantFee = MerchantFee + '{merchantFeeForTip}' where SaleReceiptPaymentDetailID = '{payment.SaleReceiptPaymentDetailID}';");
                                stringBuilder.AppendLine("");

                                if (paymentRoleDetail != null && paymentRoleDetail.ID != null && paymentRoleDetail.ID != Guid.Empty)
                                {
                                    stringBuilder.AppendLine($"    update PaymentRoleDetails set Gratuity = Gratuity + '{gratuity}' , SVCFeeForGratuity = SVCFeeForGratuity + '{svcFeeForTip}' , MerchantFeeForTip = MerchantFeeForTip + '{merchantFeeForTip}' where ID = '{paymentRoleDetail.ID}';");
                                }
                                else
                                {
                                    stringBuilder.AppendLine($"    insert into PaymentRoleDetails (ID,SaleReceiptID,SaleReceiptPaymentDetailID,PaymentMethodID,PaymentMethodType,EmployeeID,RolesID,TipRate,TipRateOT,Gratuity,SVCFeeForGratuity,MerchantFeeForTip,DateCreated,PartyServiceFeeAmount,PartyServiceFeeTaxAmount,SVCFeeForPartyServiceFeeAmount,MerchantFeeForPartyServiceFeeAmount,AutoGratuityAmount,AutoGratuityTaxAmount,SVCFeeForAutoGratuityAmount,MerchantFeeForAutoGratuityAmount,AreaFeeAmount,AreaFeeTaxAmount,SVCFeeForAreaFeeAmount,MerchantFeeForAreaFeeAmount,ServiceFee,ServiceFeeTaxAmount,SVCFeeForServiceFee,MerchantFeeForServiceFee) ");
                                    stringBuilder.AppendLine($"    values (NEWID(), '{payment.SaleReceiptID}', '{payment.SaleReceiptPaymentDetailID}','2','2', '{payment.EmployeeID}','{employeeClockInInfoEntity.RolesID}', '{employeeClockInInfoEntity.TipRate}','{employeeClockInInfoEntity.TipRateOT}','{gratuity}','{svcFeeForTip}','{merchantFeeForTip}','{payment.CreatedDate}', '{payment.PartyServiceFeeAmount}', '{payment.PartyServiceFeeTaxAmount}', '{payment.SVCFeeForPartyServiceFeeAmount}', '{payment.MerchantFeeForPartyServiceFeeAmount}', '{payment.AutoGratuityAmount}', '{payment.AutoGratuityTaxAmount}', '{payment.SVCFeeForAutoGratuityAmount}', '{payment.MerchantFeeForAutoGratuityAmount}', '{payment.AreaFeeAmount}', '{payment.AreaFeeTaxAmount}', '{payment.SVCFeeForAreaFeeAmount}', '{payment.MerchantFeeForAreaFeeAmount}', '{payment.ServiceFee}', '{payment.ServiceFeeTaxAmount}', '{payment.SVCFeeForServiceFee}', '{payment.MerchantFeeForServiceFee}');");

                                }

                                stringBuilder.AppendLine($"END");
                                stringBuilder.AppendLine($"GO");

                                creditCardTransactionID = payment.CreditCardTransactionID;
                            }
                            else
                            {
                                continue;
                                MessageBox.Show("Payment has gratuity");
                                txtScript.Text = string.Empty;
                                return;
                            }
                        }

                        if (string.IsNullOrWhiteSpace(creditCardTransactionID))
                            return;

                        using (var conn = new SqlConnection(Utility.Merchant_Connection))
                        {
                            var sql = $"select * from TransactionDetail where ID = '{creditCardTransactionID}' ";

                            var transactionDetail = await conn.QuerySingleAsync<TransactionDetailEntity>(sql, "", null, 180, System.Data.CommandType.Text);

                            merchantCondition = $" SELECT * FROM TransactionDetail where ID = '{creditCardTransactionID}' and Gratuity = 0 ";

                            crudScriptMerchant += $" update TransactionDetail set Gratuity = '{gratuity}', Total = Total + {gratuity} where ID = '{creditCardTransactionID}';{Environment.NewLine}";


                            stringBuilder.AppendLine($"");
                            stringBuilder.AppendLine($"USE MERCHANT");
                            stringBuilder.AppendLine($"IF EXISTS (SELECT * FROM TransactionDetail where ID = '{creditCardTransactionID}' and Gratuity = 0 and BatchStatus = 'Closed')");
                            stringBuilder.AppendLine($"BEGIN");
                            stringBuilder.AppendLine($"    update Batch set TotalTips = TotalTips + {gratuity}, DepositAmount = DepositAmount + {gratuity}, Total = Total + {gratuity}, TotalSVCFeeForTip = TotalSVCFeeForTip + '{svcFeeForTip}', MerchantFeeAmount = MerchantFeeAmount + '{merchantFeeForTip}' where BatchUnique = (SELECT t.BatchUnique FROM TransactionDetail t where t.ID = '{creditCardTransactionID}');");
                            stringBuilder.AppendLine($"    update TransactionDetail set BatchTotal = BatchTotal + {gratuity} where BatchUnique = (SELECT t.BatchUnique FROM TransactionDetail t where t.ID = '{creditCardTransactionID}') ;");
                            stringBuilder.AppendLine($"END");
                            stringBuilder.AppendLine($"GO");

                            stringBuilder.AppendLine($"");
                            stringBuilder.AppendLine($"USE MERCHANT");
                            stringBuilder.AppendLine($"IF EXISTS (SELECT * FROM TransactionDetail where ID = '{creditCardTransactionID}' and Gratuity = 0)");
                            stringBuilder.AppendLine($"BEGIN");
                            stringBuilder.AppendLine($"    update TransactionDetail set Gratuity = '{gratuity}', Total = Total + {gratuity}, SVCFeeForGratuity = '{svcFeeForTip}', MerchantFeeForTip = '{merchantFeeForTip}', MerchantFee = MerchantFee + '{merchantFeeForTip}' where ID = '{creditCardTransactionID}';");
                            stringBuilder.AppendLine($"END");
                            stringBuilder.AppendLine($"GO");

                        }

                        //txtScript.Text = stringBuilder.ToString();

                        //Clipboard.SetText(txtScript.Text);
                        writer.WriteLine(stringBuilder.ToString());
                    }
                    catch (Exception ex)
                    {
                        BLogicLogger.Error($"BtnGetScript_Click in UpdateTipForSaleReceiptFile_0_0 error with:  {ex.Message}", ex);
                    }
                }
            }

        }
    }
}
