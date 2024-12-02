using Dapper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ThirdPartyTool.Models;

namespace ThirdPartyTool.AddTipForPayment
{
    public partial class AddTipForPaymentForm : Form
    {
        private decimal MerchantFee = 0;
        private decimal SVCFee = 0;

        public const int TimeOutQuery = 180;
        private decimal MerchantFeeValue = 3.84620000m;

        //private string BLOGICPOSConnection = "data source=.;Initial Catalog=BLogicPOS7;integrated security = SSPI";
        //private string MerchantConnection = "data source=.;Initial Catalog=Merchant;integrated security = SSPI";

        private string BLOGICPOSConnection = "data source=.\\SQLEXPRESS;Initial Catalog=BLogicPOS7;integrated security = SSPI";
        private string MerchantConnection = "data source=.\\SQLEXPRESS;Initial Catalog=Merchant;integrated security = SSPI";

        public AddTipForPaymentForm()
        {
            InitializeComponent();

            btnConnect.Click += BtnConnect_Click;

            //btnGetScript.Click += BtnGetScript_Click;

            btnClose.Click += BtnClose_Click;

            btnCopy.Click += BtnCopy_Click;

            this.Load += UpdateTipForSaleReceiptFile_0_0_Load;

            btnSearch.Click += BtnSearch_Click;
        }

        private async void BtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                pnlPaymentDetails.Controls.Clear();

                if (string.IsNullOrWhiteSpace(txtTicketNumber.Text))
                    MessageBox.Show("Enter Ticket Number di ");

                using (var conn = new SqlConnection(txtPOSConnection.Text.Trim()))
                {
                    var sql = $"select * from SaleReceipts where SaleReceiptNumber = '{txtTicketNumber.Text}'";

                    var saleReceipt = await conn.QueryFirstOrDefaultAsync<SaleReceiptEntity>(sql, "", null, 180, System.Data.CommandType.Text);

                    if (saleReceipt == null || saleReceipt.SaleReceiptID == null || saleReceipt.SaleReceiptID == Guid.Empty)
                    {
                        MessageBox.Show("Not Found");
                        return;
                    }

                    sql = $"select sp.*, p.PaymentMethodName " +
                        $" from SaleReceiptPaymentDetails sp " +
                        $" left join PaymentMethods p on sp.PaymentMethodID = p.PaymentMethodID" +
                        $" where SaleReceiptID = '{saleReceipt.SaleReceiptID}'";

                    var saleReceiptPaymentDetails = await conn.QueryAsync<SaleReceiptPaymentDetailEntity>(sql, "", null, 180, System.Data.CommandType.Text);

                    if (saleReceiptPaymentDetails?.Count() == 0)
                    {
                        MessageBox.Show("Payment detail Not Found ");
                        return;
                    }

                    foreach (SaleReceiptPaymentDetailEntity item in saleReceiptPaymentDetails)
                    {
                        AddTipForPaymentRow paymentRow = new AddTipForPaymentRow();
                        paymentRow.SaleReceiptPaymentDetailID.Text = item.SaleReceiptPaymentDetailID;
                        paymentRow.PaymentMethodID = item.PaymentMethodID;
                        paymentRow.PaymentMethodName.Text = item.PaymentMethodName;
                        paymentRow.AmountTendered.Text = item.AmountTendered.ToString("c2");
                        paymentRow.CreditCardTransactionCardNumber.Text = item.CreditCardTransactionCardNumber;
                        paymentRow.Gratuity.Text = item.Gratuity.ToString("c2");
                        paymentRow.Total.Text = (item.AmountTendered + item.Gratuity).ToString("c2");
                        paymentRow.CreditCardTransactionID = item.CreditCardTransactionID;
                        paymentRow.btnGenerateScript.Click += BtnGenerateScript_Click;
                        paymentRow.Enabled = item.Gratuity == 0;

                        pnlPaymentDetails.Controls.Add(paymentRow);
                    }
                }
            }
            catch (Exception ex)
            {

            }

        }

        private async void BtnGenerateScript_Click(object sender, EventArgs e)
        {
            try
            {
                AddTipForPaymentRow paymentDetail = (sender as Control).Parent as AddTipForPaymentRow;
                paymentDetail.Enabled = false;

                decimal totalTip = Utility.GetDecimalFromObject(paymentDetail.Gratuity.Text);
                if (totalTip <= 0)
                {
                    paymentDetail.Enabled = true;
                    return;
                }

                decimal merchantFeeForTip = Utility.CalculateMerchantFeeWithPercent(totalTip, MerchantFee);
                decimal svcFeeForTip = Utility.CalculateSVCFeeWithPercent(totalTip, SVCFee);

                string posCondition = $"select * from SaleReceiptPaymentDetails where SaleReceiptPaymentDetailID = '{paymentDetail.SaleReceiptPaymentDetailID.Text}' and Gratuity = 0";
                string crudScriptPOS = $"     update SaleReceiptPaymentDetails set Gratuity = '{totalTip}', SVCFeeForGratuity = '{svcFeeForTip}', MerchantFeeForTip = '{merchantFeeForTip}' where SaleReceiptPaymentDetailID = '{paymentDetail.SaleReceiptPaymentDetailID.Text}';{Environment.NewLine} ";

                string merchantCondition = $" SELECT * FROM TransactionDetail where ID = '{paymentDetail.CreditCardTransactionID}' and Gratuity = 0 ";
                string crudScriptMerchant = $" update TransactionDetail set " +
                    $"Gratuity = '{totalTip}', " +
                    $"Total = Total + {totalTip}," +
                    $"MerchantFee = MerchantFee + '{merchantFeeForTip}'," +
                    $"SVCFeeForGratuity = SVCFeeForGratuity + '{svcFeeForTip}'," +
                    $"MerchantFeeForTip = MerchantFeeForTip + '{merchantFeeForTip}' where ID = '{paymentDetail.CreditCardTransactionID}';{Environment.NewLine}";


                string batchUnique = string.Empty;

                using (var conn = new SqlConnection(txtMerchantConnection.Text.Trim()))
                {
                    var sql = $"select * from TransactionDetail where ID = '{paymentDetail.CreditCardTransactionID}' ";

                    var transactionDetail = await conn.QueryFirstOrDefaultAsync<TransactionDetailEntity>(sql, "", null, 180, System.Data.CommandType.Text);

                    if (transactionDetail == null)
                        return;

                    batchUnique = transactionDetail.BatchUnique;

                    sql = $"select * from Batch where BatchUnique = '{batchUnique}'";

                    var batchDetail = await conn.QueryFirstOrDefaultAsync<BatchEntity>(sql, "", null, 180, System.Data.CommandType.Text);

                    if (batchDetail != null && batchDetail.Status == "Closed")
                    {
                        crudScriptMerchant += $"update TransactionDetail set BatchTotal = BatchTotal + '{totalTip}' where BatchUnique = '{batchUnique}';{Environment.NewLine}";
                        crudScriptMerchant += $"update Batch set Total = Total + '{totalTip}'," +
                            $" TotalSVCFeeForTip = TotalSVCFeeForTip + '{svcFeeForTip}'," +
                            $" TotalTips = TotalTips + '{totalTip}'," +
                            $" MerchantFeeAmount = MerchantFeeAmount + '{merchantFeeForTip}'" +
                            $" where BatchUnique = '{batchUnique}';{Environment.NewLine}";
                    }
                }

                string templateScriptPOS = $"use BLogicPOS7{Environment.NewLine}" +
                    $"IF EXISTS ({posCondition}) {Environment.NewLine}" +
                    $"BEGIN {Environment.NewLine}" +
                    $"{crudScriptPOS}" +
                    $"END {Environment.NewLine}" +
                    $"GO {Environment.NewLine}";

                string templateScriptMerchant = $"use Merchant{Environment.NewLine}" +
                    $"IF EXISTS ({merchantCondition}) {Environment.NewLine}" +
                    $"BEGIN {Environment.NewLine}" +
                    $"{crudScriptMerchant}" +
                    $"END {Environment.NewLine}" +
                    $"GO {Environment.NewLine}";

                txtScript.Text = $"{templateScriptPOS}{Environment.NewLine}{templateScriptMerchant}";
            }
            catch (Exception)
            {

                throw;
            }
        }

        private async void UpdateTipForSaleReceiptFile_0_0_Load(object sender, EventArgs e)
        {
            txtPOSConnection.Text = BLOGICPOSConnection;
            txtMerchantConnection.Text = MerchantConnection;

            try
            {
                using (var conn = new SqlConnection(txtPOSConnection.Text.Trim()))
                {
                    var sql = $"select * " +
                        $" from Configurations " +
                        $" where ConfigurationName in (" +
                        $" 'CreditCardConfigValue.MerchantFee'," +
                        $" 'CreditCardConfigValue.SVCFee')";

                    var listPayments = await conn.QueryAsync<ConfigurationEntity>(sql, "", null, 180, System.Data.CommandType.Text);

                    foreach (ConfigurationEntity payment in listPayments)
                    {
                        switch (payment.ConfigurationName)
                        {
                            case "CreditCardConfigValue.MerchantFee":
                                MerchantFee = Utility.GetDecimalFromObject(payment.ConfigurationValue);
                                break;
                            case "CreditCardConfigValue.SVCFee":
                                SVCFee = Utility.GetDecimalFromObject(payment.ConfigurationValue);
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void BtnCopy_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(txtScript.Text);
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void BtnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                using (var conn = new SqlConnection(txtPOSConnection.Text.Trim()))
                using (var connMerchant = new SqlConnection(txtMerchantConnection.Text.Trim()))
                {
                    try
                    {
                        conn.Open();
                    }
                    catch (Exception ex)
                    {
                        lblStatusPOS.Text = "Connection to the POS database => Error.";
                        lblStatusPOS.ForeColor = Color.Red;
                        return;
                    }
                    lblStatusPOS.Text = "Connection to the POS database => Success.";
                    lblStatusPOS.ForeColor = Color.Green;

                    try
                    {
                        connMerchant.Open();
                    }
                    catch (Exception ex)
                    {
                        lblStatusMerchant.Text = "Connection to the Merchant database => Error.";
                        lblStatusMerchant.ForeColor = Color.Red;
                        return;
                    }
                    lblStatusMerchant.Text = "Connection to the Merchant database => Success.";
                    lblStatusMerchant.ForeColor = Color.Green;
                }

                lblTicketNumber.Visible = true;
                txtTicketNumber.Visible = true;
                btnSearch.Visible = true;   
                pnlPaymentDetails.Visible = true;
                pnlDetail.Visible = true;

                using (var conn = new SqlConnection(txtPOSConnection.Text.Trim()))
                {
                    var sql = $"select * from Stores ";

                    var listStore = await conn.QueryAsync<StoreEntity>(sql, "", null, 180, System.Data.CommandType.Text);

                    lblStatusPOS.Visible = listStore.Any();
                    txtPOSConnection.Enabled = !listStore.Any();
                }

                using (var conn = new SqlConnection(txtMerchantConnection.Text.Trim()))
                {
                    var sql = $"select top 1 * from Batch ";

                    var listStore = await conn.QueryAsync<BatchEntity>(sql, "", null, 180, System.Data.CommandType.Text);

                    lblStatusMerchant.Visible = listStore.Any();
                    txtMerchantConnection.Enabled = !listStore.Any();
                }

                btnConnect.Enabled = !lblStatusMerchant.Visible || !lblStatusPOS.Visible;
            }
            catch (Exception ex)
            {
                BLogicLogger.Error($"BtnConnect_Click in UpdateTipForSaleReceiptFile_0_0 error with:  {ex.Message}", ex);
            }
        }

        private async void BtnStart_Click(object sender, EventArgs e)
        {
            try
            {
                BLogicLogger.Debug("----- START ADD BATCH DETAIL -----");
                BLogicLogger.Debug("Connect Merchant Database");
                using (var conn = new SqlConnection(MerchantConnection))
                {
                    var sql = $"select * from TransactionDetail t where t.BatchUnique = '1b4deaac-15ae-4fe5-973e-d6778c9d5c72' ";

                    var listTransactionDetails = await conn.QueryAsync<TransactionDetailEntity>(sql, "", null, 180, System.Data.CommandType.Text);

                    BLogicLogger.Debug($"Get TransactionDetail -> return {listTransactionDetails.Count()} transaction");
                    if (listTransactionDetails.Count() == 0)
                    {
                        MessageBox.Show("Can not found any transaction to add new Batch", "Error");
                        this.Close();
                    }

                    var groupTransactions = listTransactionDetails.GroupBy(x => new { x.BatchUnique });

                    BLogicLogger.Debug($"Start insert new batch & update TransactionDetail");

                    foreach (var group in groupTransactions)
                    {
                        var listTransactions = group.OrderByDescending(x => x.Date).ToList();

                        int iCount = 0;
                        decimal dTotal = 0;
                        decimal dTotalSales = 0;
                        decimal dTotalTips = 0;
                        decimal dSVCFeeAmount = 0;
                        decimal dSVCFeeForTip = 0;
                        string sWorkstationName = string.Empty;
                        decimal dTotalReturns = 0;
                        decimal dTotalMerchantFee = 0;
                        foreach (var item in listTransactions)
                        {
                            string result = item.Result;
                            string type = item.TranType;
                            string sMerchantID = item.MerchantID;
                            decimal subTotal = item.Total;
                            decimal subAmount = item.Amount;
                            decimal subSVCFee = item.SVCFee;
                            decimal subTip = item.Gratuity;
                            decimal subSVCFeeAmount = item.SVCFee;
                            decimal subSVCFeeForTip = item.SVCFeeForGratuity;
                            decimal subReturn = item.Amount;
                            string tranStatus = item.TranStatus;
                            if (result == "Approved")
                            {
                                var BatchID = item.BatchID;
                                iCount++;
                                if (!string.IsNullOrEmpty(item.WorkstationName))
                                    sWorkstationName = item.WorkstationName;
                                if (type == "Sale" || type == "PreAuthCapture")
                                {
                                    if (tranStatus == "ACTIVE")
                                    {
                                        dTotal += subTotal;
                                        dTotalSales += (subAmount - subSVCFee);
                                        dTotalTips += subTip;
                                        dSVCFeeAmount += subSVCFeeAmount;
                                        dSVCFeeForTip += subSVCFeeForTip;
                                        dTotalMerchantFee += item.MerchantFee;
                                    }
                                }
                                else if (type == "Return")
                                {
                                    dTotal -= subTotal;
                                    dTotalReturns += subReturn;
                                    dTotalMerchantFee -= item.MerchantFee;
                                }
                                else
                                {

                                }
                            }
                            else
                            {

                            }
                        }
                        var TotalReturns = dTotalReturns;
                        var TotalSales = dTotalSales;
                        var TotalTips = dTotalTips;
                        var WorkStationName = sWorkstationName;
                        var TransactionCount = iCount;
                        var Total = dTotal;
                        var MerchantFeePercent = MerchantFeeValue;
                        var MerchantFeeAmount = dTotalMerchantFee;// CalculateMerchantFee(dTotal + dTotalReturns);
                        var DepositAmount = dTotal - MerchantFeeAmount;
                        var TotalSVCFeeAmount = dSVCFeeAmount;
                        var TotalSVCFeeForTip = dSVCFeeForTip;

                        if (TransactionCount <= 0)
                            continue;
                        return;
                        Guid batchUnique = Guid.NewGuid();

                        DateTime date = listTransactions[0].Date.Date + new TimeSpan(23, 55, 00);

                        sql = $"INSERT INTO Batch (Date, ID, Status, MerchantID, Quantity, Total, BatchUnique, TotalSVCFeeForTip, TotalSales, TotalReturns, TotalTips, TotalSVCFeeAmount, DepositAmount, WorkStationName, MerchantFeeAmount, MerchantFeePercent, ExpandInfo) " +
                            $"values ('{date}'," +
                            $"'{listTransactions[0].BatchID}'," +
                            $"'Closed'," +
                            $"'{listTransactions[0].MerchantID}'," +
                            $"'{TransactionCount}'," +
                            $"'{Total}'," +
                            $"'{batchUnique}'," +
                            $"'{TotalSVCFeeForTip}'," +
                            $"'{TotalSales}'," +
                            $"'{TotalReturns}'," +
                            $"'{TotalTips}'," +
                            $"'{TotalSVCFeeAmount}'," +
                            $"'{DepositAmount}'," +
                            $"'{sWorkstationName}'," +
                            $"'{MerchantFeeAmount}'," +
                            $"'{MerchantFeeValue}'," +
                        $"NULL)";

                        var resultInsert = await conn.ExecuteAsync(sql, null, null, TimeOutQuery, System.Data.CommandType.Text);

                        if (resultInsert > 0)
                        {
                            string ids = String.Join(",", listTransactions.Select(i => { return "'" + i.ID + "'"; }));

                            sql = $"update TransactionDetail set BatchUnique = '{batchUnique}', BatchTotal = '{Total}' where ID in ({ids})";

                            var resultUpdate = await conn.ExecuteAsync(sql, null, null, TimeOutQuery, System.Data.CommandType.Text);

                            if (resultInsert <= 0)
                            {
                                BLogicLogger.Error($"Update TransactionDetail error with query: {sql}");
                            }
                        }
                        else
                        {
                            BLogicLogger.Error($"Insert new Batch error with query: {sql}");
                        }
                    }

                    MessageBox.Show("Add new Batch & update TransactionDetail -> Success.", "Information");

                    BLogicLogger.Debug($"Close Form");
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                BLogicLogger.Error($"BtnStart_Click error with: {ex.Message}", ex);
                this.Close();
            }

        }

        public decimal CalculateMerchantFee(decimal total)
        {
            if (total <= 0)
                return 0;
            return RoundUp(total * (MerchantFeeValue / 100));

            //if (CSetting.SVCFeeType == "1")
            //{
            //    return RoundUp(total * (CSetting.MerchantFeeValue / 100));
            //}
            //else
            //{
            //    return 0;
            //}

        }

        public static decimal RoundUp(decimal dInValue)
        {
            decimal dReturn = Math.Round(dInValue, 2);
            if (dReturn >= dInValue)
                return dReturn;
            //dReturn < dInValue
            decimal dSub = dInValue - dReturn;
            if (dSub >= (decimal)0.005)
                dReturn += (decimal)0.01;
            return dReturn;
        }
    }
}
