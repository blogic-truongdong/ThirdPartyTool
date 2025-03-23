using Dapper;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
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

                    if (mainSaleReceipts == null || mainSaleReceipts.Count() > 1)
                    {
                        MessageBox.Show($"Exist more 1 ticket with SaleReceiptNumber: {mainSaleReceiptNumber} ");
                        return;
                    }

                    SaleReceiptEntity mainReceiptEntity = mainSaleReceipts.First();
                    SaleReceiptEntity tokenReceiptEntity = tokenSaleReceipts.First();

                    if (tokenSaleReceipts == null || tokenSaleReceipts.Count() > 1)
                    {
                        MessageBox.Show($"Exist more 1 ticket with SaleReceiptNumber: {tokenSaleReceiptNumber} ");
                        return;
                    }

                    var employeeClockInInfoEntity = await Utility.GetEmployeeClockInInfo(conn, mainReceiptEntity.CashierID, mainReceiptEntity.DateCreated);

                    sql = $"select * from SaleReceiptPaymentDetails where SaleReceiptID = '{tokenReceiptEntity.SaleReceiptID}'";
                    var saleReceiptPaymentDetails = await conn.QueryAsync<SaleReceiptPaymentDetailEntity>(sql, null, null, 180, System.Data.CommandType.Text);

                    if(saleReceiptPaymentDetails?.Count() > 0)
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
                            stringBuilder.AppendLine("");
                        }

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
