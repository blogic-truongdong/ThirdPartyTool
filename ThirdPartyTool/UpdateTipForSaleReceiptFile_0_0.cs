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

namespace ThirdPartyTool
{
    public partial class UpdateTipForSaleReceiptFile_0_0 : Form
    {
        public const int TimeOutQuery = 180;
        private decimal MerchantFeeValue = 3.84620000m;

        private string BLOGICPOSConnection = "data source=.;Initial Catalog=BLogicPOS7;integrated security = SSPI";
        private string MerchantConnection = "data source=.;Initial Catalog=Merchant;integrated security = SSPI";

        public UpdateTipForSaleReceiptFile_0_0()
        {
            InitializeComponent();

            btnConnect.Click += BtnConnect_Click;

            btnGetScript.Click += BtnGetScript_Click;
        }

        private async void BtnGetScript_Click(object sender, EventArgs e)
        {
            txtScript.Text = "";

            if (string.IsNullOrWhiteSpace(txtTicketNumber.Text) || string.IsNullOrWhiteSpace(txtGratuity.Text))
                return;

            string creditCardTransactionID = string.Empty;

            decimal.TryParse(txtGratuity.Text.Trim(), out decimal gratuity);

            if (gratuity == 0)
                return;

            using (var conn = new SqlConnection(txtPOSConnection.Text.Trim()))
            {
                var sql = $"select sp.Gratuity, sp.SVCFeeForGratuity,sp.CreditCardTransactionID, s.SaleReceiptNumber, sp.*" +
                    $" from SaleReceiptPaymentDetails sp left join SaleReceipts s on sp.SaleReceiptID = s.SaleReceiptID" +
                    $" where s.SaleReceiptNumber = '{txtTicketNumber.Text.Trim()}'";

                var payment = await conn.QuerySingleAsync<SaleReceiptPaymentDetailEntity>(sql, "", null, 180, System.Data.CommandType.Text);

                if (!string.IsNullOrWhiteSpace(payment.SaleReceiptPaymentDetailID) && payment.Gratuity == 0)
                {
                    #region get Role ID
                    string roleID = "0";

                    sql = $"select * from EmployeeRoles where EmployeeID = '{payment.Cashier}'";
                    var listRole = (await conn.QueryAsync<EmployeeRoleEntity>(sql, "", null, 180, System.Data.CommandType.Text)).ToList();

                    if (listRole.Count == 1)
                        roleID = listRole[0].RolesID;
                    else
                    {
                        sql = $"select top 1 EmployeeID, RolesID " +
                            $" from TimeClock " +
                            $" where EmployeeID = '{payment.Cashier}' " +
                            $" and DateTimeIn <= '{payment.CreatedDate}' and DateTimeOut >= '{payment.CreatedDate}' " +
                            $" order by DateTimeIn desc";
                        var timeClockEntity = await conn.QuerySingleAsync<EmployeeRoleEntity>(sql, "", null, 180, System.Data.CommandType.Text);

                        if (!string.IsNullOrWhiteSpace(timeClockEntity.RolesID))
                            roleID = timeClockEntity.RolesID;
                        else
                        {
                            sql = $"select top 1 EmployeeID, RolesID " +
                                $" from TimeClock " +
                                $" where EmployeeID = '{payment.Cashier}' " +
                                $" order by DateTimeIn desc";
                            timeClockEntity = await conn.QuerySingleAsync<EmployeeRoleEntity>(sql, "", null, 180, System.Data.CommandType.Text);

                            if (!string.IsNullOrWhiteSpace(timeClockEntity.RolesID))
                                roleID = timeClockEntity.RolesID;
                        }
                    }
                    #endregion

                    #region Get employee Info
                    decimal tipRate = 0;
                    decimal tipRateOT = 0;

                    sql = $"select * from Employees where EmployeeID = '{payment.Cashier}'";
                    var employee = await conn.QuerySingleAsync<EmployeeEntity>(sql, "", null, 180, System.Data.CommandType.Text);

                    sql = $"select * from Roles where RolesID = '{roleID}'";
                    var role = await conn.QuerySingleAsync<RoleEntity>(sql, "", null, 180, System.Data.CommandType.Text);
                    
                    if(employee.IsUsingRoleTipPool)
                    {
                        tipRate = employee.TipRate;
                        tipRateOT = employee.TipRateOT;
                    }
                    else
                    {
                        tipRate = role.TipRate;
                        tipRateOT = role.TipRateOT;
                    }
                    #endregion

                    string text = $"use BLogicPOS7{Environment.NewLine}";

                    text += $"IF EXISTS (SELECT * FROM SaleReceiptPaymentDetails where SaleReceiptPaymentDetailID = '{payment.SaleReceiptPaymentDetailID}' and Gratuity = 0) {Environment.NewLine}";
                    text += $"BEGIN {Environment.NewLine}";
                    text += $" update SaleReceiptPaymentDetails set Gratuity = '{gratuity}' where SaleReceiptPaymentDetailID  ='{payment.SaleReceiptPaymentDetailID}';";

                    text += $"{Environment.NewLine}{Environment.NewLine}" +
                        $" insert into PaymentRoleDetails (ID,SaleReceiptID,SaleReceiptPaymentDetailID,PaymentMethodID,PaymentMethodType,EmployeeID,RolesID,TipRate,TipRateOT,Gratuity,SVCFeeForGratuity,MerchantFeeForTip,DateCreated,PartyServiceFeeAmount,PartyServiceFeeTaxAmount,SVCFeeForPartyServiceFeeAmount,MerchantFeeForPartyServiceFeeAmount,AutoGratuityAmount,AutoGratuityTaxAmount,SVCFeeForAutoGratuityAmount,MerchantFeeForAutoGratuityAmount,AreaFeeAmount,AreaFeeTaxAmount,SVCFeeForAreaFeeAmount,MerchantFeeForAreaFeeAmount,ServiceFee,ServiceFeeTaxAmount,SVCFeeForServiceFee,MerchantFeeForServiceFee) {Environment.NewLine}" +
                        $" values (NEWID(), '{payment.SaleReceiptID}', '{payment.SaleReceiptPaymentDetailID}','2','2', '{payment.Cashier}','{roleID}', '{tipRate}','{tipRateOT}','{gratuity}','0','0','{payment.CreatedDate}', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0'); {Environment.NewLine}";
                    text += $"END {Environment.NewLine}";
                    text += $"GO {Environment.NewLine}";

                    creditCardTransactionID = payment.CreditCardTransactionID;

                    txtScript.Text += text;
                }
                else
                {
                    txtScript.Text = string.Empty;
                    return;
                }
            }

            if (string.IsNullOrWhiteSpace(creditCardTransactionID))
                return;

            using (var conn = new SqlConnection(txtMerchantConnection.Text.Trim()))
            {
                var sql = $"select * from TransactionDetail where ID = '{creditCardTransactionID}' ";

                var transactionDetail = await conn.QuerySingleAsync<TransactionDetailEntity>(sql, "", null, 180, System.Data.CommandType.Text);

                string text = $"{Environment.NewLine}use MERCHANT{Environment.NewLine}";

                text += $"IF EXISTS (SELECT * FROM TransactionDetail where ID = '{creditCardTransactionID}' and Gratuity = 0) {Environment.NewLine}";
                text += $"BEGIN {Environment.NewLine}";
                text += $" update TransactionDetail set Gratuity = '{gratuity}', Total = Total + {gratuity} where ID = '{creditCardTransactionID}';{Environment.NewLine}";
                text += $" update Batch set TotalTips = TotalTips + {gratuity}, DepositAmount = DepositAmount + {gratuity}, Total = Total + {gratuity} where BatchUnique = '{transactionDetail.BatchUnique}';{Environment.NewLine}";
                text += $" update TransactionDetail set BatchTotal = BatchTotal + {gratuity} where BatchUnique = '{transactionDetail.BatchUnique}' ; {Environment.NewLine}";
                text += $"END {Environment.NewLine}";
                text += $"GO {Environment.NewLine}";

                txtScript.Text += text;
            }
        }

        private async void BtnConnect_Click(object sender, EventArgs e)
        {
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
