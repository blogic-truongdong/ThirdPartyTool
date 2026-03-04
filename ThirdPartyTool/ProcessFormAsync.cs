using BLogicSystems.Merchant.PAXTerminalHelper;
using BLogicSystems.XPos.SharedModule.Models;
using BLogicSystems.XPos.Utilites;
using Dapper;
using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows.Forms;
using ThirdPartyTool.Models;

namespace ThirdPartyTool
{
    public partial class ProcessFormAsync : Form
    {
        private bool ExistFinalGratuityInSaleReceipts = false;
        private bool ExistSVCFeeTaxAmountInSaleReceipts = false;
        private bool ExistTaxForServiceChargeInSaleReceipts = false;

        private DataTable CurrentTableInDatabaseBLogicPOS7 = null;
        public DataTable DataTableReturn = null;
        public DataSet DataSetReturn = null;
        BatchStatus batch = new BatchStatus();
        public decimal GetReceiptTemplateByTicketNumber_TotalAmount = 0;
        public bool GetReceiptTemplateByTicketNumber_Include_VOID_CLOSE_REFUND_OPER = false;
        public XtraReport ReceiptTemplate = null;
        public ProcessNameEnum CurrentProcessName = ProcessNameEnum.None;
        public bool IsSuccess = false;
        public string ErrorMessage = string.Empty;

        private decimal AddTipForPaymentDetail_Gratuity = 0;

        public DateTime EmployeeView_StartDate;
        public DateTime EmployeeView_EndDate;
        public int EmployeeView_EmployeeID;
        public int EmployeeView_RolesID;

        public DateTime Item_StartDate;
        public DateTime Item_EndDate;

        public string SaleReceiptID = string.Empty;
        public string SaleReceiptNumber = string.Empty;
        private string SaleReceiptPaymentDetailID = string.Empty;

        public string TokenSaleReceiptID = string.Empty;
        public string TokenSaleReceiptNumber = string.Empty;

        public string MerchantID = string.Empty;
        public string BatchID = string.Empty;

        public RequestModel requestModel = null;

        public ProcessFormAsync()
        {
            InitializeComponent();

            this.Load += ProcessForm_Load;
        }

        private void ProcessForm_Load(object sender, EventArgs e)
        {
            Task.Run(() => StartProcess());
        }

        private async Task StartProcess()
        {
            await GetListTableInDatabaseBLogicPOS7();

            try
            {
                switch (CurrentProcessName)
                {
                    case ProcessNameEnum.UpdateServiceChargeBySaleReceiptID:
                        await UpdateServiceChargeBySaleReceiptID();
                        break;
                }
            }
            catch (Exception ex)
            {
                BLogicLogger.Exception("BackgroundWorker_DoWork in ProcessFormAsync", ex, new System.Collections.Generic.List<object>() { CurrentProcessName });
            }
            finally
            {
                CloseForm();

            }
        }

        private void CloseForm()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new System.Action(() => this.Close()));
            }
        }

        private void UpdateFinalGratuityForSaleReceipts(SqlCommand sqlCommand, string saleID)
        {
            if (ExistFinalGratuityInSaleReceipts)
            {
                var scriptUpdate = $@";WITH g AS (
                                SELECT
                                    SaleReceiptID,
                                    SUM(Gratuity) AS TotalGratuity
                                FROM SaleReceiptPaymentDetails
                                GROUP BY SaleReceiptID
                            )
                            UPDATE sr
                            SET sr.FinalGratuity = g.TotalGratuity
                            FROM SaleReceipts sr
                            JOIN g ON g.SaleReceiptID = sr.SaleReceiptID
                            WHERE sr.TransactionStatusID = 'FIN' and sr.SaleReceiptID = '{saleID}';";

                RunQueryUpdate(sqlCommand, scriptUpdate);
            }
        }

        public void RunQueryUpdate(SqlCommand sqlCommand, string scriptUpdate, SqlParameter[] cmdParams = null)
        {
            BLogicLogger.Error($"Run Script for {sqlCommand.Connection.Database} : {scriptUpdate}");
            sqlCommand.CommandText = scriptUpdate;

            if (cmdParams != null)
            {
                for (int i = 0; i < cmdParams.Length; i++)
                {
                    SqlParameter parm = cmdParams[i];
                    sqlCommand.Parameters.Add(parm);
                }
            }

            var resultQueryUpdate = sqlCommand.ExecuteNonQuery();
            if (resultQueryUpdate < 0)
            {
                ErrorMessage = $"Something went wrong while run script update.{Environment.NewLine}Please try contact TruongDS.";
                BLogicLogger.Exception("RunQueryUpdate", null, new System.Collections.Generic.List<object>() { scriptUpdate });
                throw new Exception();
            }
        }

        private async Task RunQueryUpdateAsync(SqlConnection conn, string sql, object parameters = null)
        {
            try
            {
                var result = await conn.ExecuteAsync(sql, parameters, null, 180, System.Data.CommandType.Text);

                if (result < 0)
                {
                    ErrorMessage = $"Something went wrong while run script update.{Environment.NewLine}Please try contact TruongDS.";
                    BLogicLogger.Exception("RunQueryUpdateTryCatch in RunQueryUpdateAsync", null, new System.Collections.Generic.List<object>() { sql, parameters });
                }
            }
            catch (Exception ex)
            {
                BLogicLogger.Exception("RunQueryUpdateTryCatch in RunQueryUpdateAsync", ex, new List<object>() { sql, parameters });
            }

        }

        private async Task GetListTableInDatabaseBLogicPOS7()
        {
            try
            {
                CurrentTableInDatabaseBLogicPOS7 = new DataTable("Tables");

                DataTable dtSaleReceipts = new DataTable("SaleReceipts");

                using (SqlCommand sqlCommand = new SqlCommand())
                using (var conn = new SqlConnection(Helper.BLOGICPOS7_Connection))
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();

                    sqlCommand.Connection = conn;
                    sqlCommand.CommandType = CommandType.Text;

                    sqlCommand.CommandText = $"SELECT TABLE_NAME " +
                        $"FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE' and TABLE_NAME in (" +
                        $"'SaleReceiptDetailTaxDetails'," +
                        $"'SaleReceiptServiceChargeTaxDetails'," +
                        $"'DeliveryDetails', " +
                        $"'PaymentRoleDetails'," +
                        $"'PaycorEmployees') ";
                    var reader = sqlCommand.ExecuteReader();
                    CurrentTableInDatabaseBLogicPOS7.Load(reader);
                    reader.Close();

                    sqlCommand.CommandText = "SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'SaleReceipts'";
                    reader = sqlCommand.ExecuteReader();
                    dtSaleReceipts.Load(reader);
                    reader.Close();

                    ExistFinalGratuityInSaleReceipts = dtSaleReceipts.Select("COLUMN_NAME = 'FinalGratuity'", "")?.Length > 0;
                    ExistSVCFeeTaxAmountInSaleReceipts = dtSaleReceipts.Select("COLUMN_NAME = 'SVCFeeTaxAmount'", "")?.Length > 0;
                    ExistTaxForServiceChargeInSaleReceipts = dtSaleReceipts.Select("COLUMN_NAME = 'ServiceFeeTaxAmount'", "")?.Length > 0;
                }
            }
            catch (Exception ex)
            {
                BLogicLogger.Exception("GetListTableInDatabaseBLogicPOS7", ex);
            }
        }

        public async Task UpdateServiceChargeBySaleReceiptID()
        {
            try
            {
                string sql = string.Empty;

                using (SqlCommand sqlCommand = new SqlCommand())
                using (SqlConnection conn = new SqlConnection(Helper.BLOGICPOS7_Connection))
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();

                    sqlCommand.Connection = conn;
                    sqlCommand.CommandType = CommandType.Text;

                    int _svcFeeType = 0;
                    decimal _svcFee = 0;
                    decimal _merchantFee = 0;
                    decimal _cashDiscount = 0;
                    int _dTaxIncludeInSVCFee = 0;
                    string _ApplyCustomFeeWhenPurchaseGiftCard = "1";
                    string _EnableDualPrice = "0";
                    decimal _DualPricePercent = 0;
                    string sqlEmployeePaidID = string.Empty;

                    sql = "select * from Configurations " +
                        " where ConfigurationName in ('CreditCardConfigValue.SVCFee', 'CreditCardConfigValue.SVCFeeType', 'CreditCardConfigValue.MerchantFee', 'CreditCardConfigValue.CashDiscount', " +
                        " 'CreditCardConfigValue.TaxIncludeInSVCFee','CreditCardConfigValue.ApplyCustomFeeWhenPurchaseGiftCard','CreditCardConfigValue.DualPricePercent','SystemConfigValue.EnableDualPrice')";

                    var configuratios = await conn.QueryAsync<Models.ConfigurationsEntity>(sql, null, null, 180, System.Data.CommandType.Text);

                    foreach (var item in configuratios)
                    {
                        string _name = item.ConfigurationName;
                        string _value = item.ConfigurationValue;

                        switch (_name)
                        {
                            case "CreditCardConfigValue.SVCFee":
                                _svcFee = Utility.GetDecimalFromObject(_value);
                                break;
                            case "CreditCardConfigValue.SVCFeeType":
                                _svcFeeType = Utility.GetIntFromObject(_value);
                                break;
                            case "CreditCardConfigValue.MerchantFee":
                                _merchantFee = Utility.GetDecimalFromObject(_value);
                                break;
                            case "CreditCardConfigValue.CashDiscount":
                                _cashDiscount = Utility.GetDecimalFromObject(_value);
                                break;
                            case "CreditCardConfigValue.TaxIncludeInSVCFee":
                                _dTaxIncludeInSVCFee = Utility.GetIntFromObject(_value);
                                break;
                            case "CreditCardConfigValue.ApplyCustomFeeWhenPurchaseGiftCard":
                                _ApplyCustomFeeWhenPurchaseGiftCard = Utility.GetStringFromObject(_value);
                                break;
                            case "SystemConfigValue.EnableDualPrice":
                                _EnableDualPrice = Utility.GetStringFromObject(_value);
                                break;
                            case "CreditCardConfigValue.DualPricePercent":
                                _DualPricePercent = Utility.GetDecimalFromObject(_value);
                                break;
                        }
                    }

                    await UpdateServiceChargesForSaleReceipts(conn, ServiceChargeType.SVCFee);
                    await UpdateServiceChargesForSaleReceipts(conn, ServiceChargeType.CashDiscount);
                    await UpdateServiceChargesForSaleReceipts(conn, ServiceChargeType.ManualServiceFee);
                    await UpdateServiceChargesForSaleReceipts(conn, ServiceChargeType.AreaServiceFee);
                    await UpdateServiceChargesForSaleReceipts(conn, ServiceChargeType.PartyServiceFee);
                    await UpdateServiceChargesForSaleReceipts(conn, ServiceChargeType.AutoServiceFee);

                    sql = $@"select * from SaleReceipts where SaleReceiptID = '{requestModel.serviceChargesValues.SaleReceiptID}'";
                    var saleEntity = await conn.QueryFirstOrDefaultAsync<SaleReceiptEntity>(sql, null, null, 180, System.Data.CommandType.Text);

                    sql = $@"select * from SaleReceiptPaymentDetails where SaleReceiptID = '{requestModel.serviceChargesValues.SaleReceiptID}'";
                    var paymentEntity = await conn.QueryFirstOrDefaultAsync<SaleReceiptPaymentDetailEntity>(sql, null, null, 180, System.Data.CommandType.Text);

                    int cashAdjust = 1;
                    if (_EnableDualPrice == "1" && _DualPricePercent > 0)
                    {
                        sql = $"select IsDualPrice from PaymentMethods where PaymentMethodID = '{paymentEntity.PaymentMethodID}'";
                        var tempResult = await conn.QueryFirstOrDefaultAsync<PaymentMethodEntity>(sql, null, null, 180, System.Data.CommandType.Text);
                        cashAdjust = tempResult.IsDualPrice ? 2 : 1;
                    }
                    else
                    {
                        sql = $"select CashAdjust from PaymentMethods where PaymentMethodID = '{paymentEntity.PaymentMethodID}'";
                        var tempResult = await conn.QueryFirstOrDefaultAsync<PaymentMethodEntity>(sql, null, null, 180, System.Data.CommandType.Text);
                        cashAdjust = tempResult.CashAdjust;
                    }

                    bool isApplyTaxCustomFee = false;
                    SaleReceipts_ReceiptInfo receiptInfo = null;
                    if (!string.IsNullOrWhiteSpace(saleEntity.ReceiptInfo))
                    {
                        receiptInfo = Utility.ParseJsonToReceiptInfo(saleEntity.ReceiptInfo);
                        if (receiptInfo != null)
                            isApplyTaxCustomFee = receiptInfo.IsApplyTaxForSVCFee;
                    }

                    if (saleEntity.SVCFeeTaxAmount != 0)
                        isApplyTaxCustomFee = true;

                    if (paymentEntity.PaymentMethodID == 2)
                    {
                        if (isApplyTaxCustomFee)
                        {
                            decimal dtaxsc = paymentEntity.ServiceFeeTaxAmount + paymentEntity.PartyServiceFeeTaxAmount + paymentEntity.AreaFeeTaxAmount + paymentEntity.AutoGratuityTaxAmount;
                            paymentEntity.SVCFeeForServiceCharge = CalculateSVCFee(paymentEntity.PaidServiceFee - dtaxsc, isApplyTaxCustomFee, saleEntity.SVCFeeTaxRate, paymentEntity.SVCFeeTaxAmount, _dTaxIncludeInSVCFee, _svcFee, _svcFeeType);
                            paymentEntity.SVCFeeForServiceFee = CalculateSVCFee(paymentEntity.ServiceFee - paymentEntity.ServiceFeeTaxAmount, isApplyTaxCustomFee, saleEntity.SVCFeeTaxRate, paymentEntity.SVCFeeTaxAmount, _dTaxIncludeInSVCFee, _svcFee, _svcFeeType);
                            paymentEntity.SVCFeeForPartyServiceFeeAmount = CalculateSVCFee(paymentEntity.PartyServiceFeeAmount - paymentEntity.PartyServiceFeeTaxAmount, isApplyTaxCustomFee, saleEntity.SVCFeeTaxRate, paymentEntity.SVCFeeTaxAmount, _dTaxIncludeInSVCFee, _svcFee, _svcFeeType);
                            paymentEntity.SVCFeeForAreaFeeAmount = CalculateSVCFee(paymentEntity.AreaFeeAmount - paymentEntity.AreaFeeTaxAmount, isApplyTaxCustomFee, saleEntity.SVCFeeTaxRate, paymentEntity.SVCFeeTaxAmount, _dTaxIncludeInSVCFee, _svcFee, _svcFeeType);
                            paymentEntity.SVCFeeForAutoGratuityAmount = CalculateSVCFee(paymentEntity.AutoGratuityAmount - paymentEntity.AutoGratuityTaxAmount, isApplyTaxCustomFee, saleEntity.SVCFeeTaxRate, paymentEntity.SVCFeeTaxAmount, _dTaxIncludeInSVCFee, _svcFee, _svcFeeType);

                            paymentEntity.MerchantFeeForServiceFee = CalculateMerchantFee((paymentEntity.ServiceFee - paymentEntity.ServiceFeeTaxAmount) + paymentEntity.SVCFeeForServiceFee, _merchantFee);
                            paymentEntity.MerchantFeeForAreaFeeAmount = CalculateMerchantFee((paymentEntity.AreaFeeAmount - paymentEntity.AreaFeeTaxAmount) + paymentEntity.SVCFeeForAreaFeeAmount, _merchantFee);
                            paymentEntity.MerchantFeeForPartyServiceFeeAmount = CalculateMerchantFee((paymentEntity.PartyServiceFeeAmount - paymentEntity.PartyServiceFeeTaxAmount) + paymentEntity.SVCFeeForPartyServiceFeeAmount, _merchantFee);
                            paymentEntity.MerchantFeeForAutoGratuityAmount = CalculateMerchantFee((paymentEntity.AutoGratuityAmount - paymentEntity.AutoGratuityTaxAmount) + paymentEntity.SVCFeeForAutoGratuityAmount, _merchantFee);
                        }
                        else
                        {
                            paymentEntity.SVCFeeForServiceCharge = CalculateSVCFee(paymentEntity.PaidServiceFee, false, 0, 0, _dTaxIncludeInSVCFee, _svcFee, _svcFeeType);
                            paymentEntity.SVCFeeForServiceFee = CalculateSVCFee(paymentEntity.ServiceFee, false, 0, 0, _dTaxIncludeInSVCFee, _svcFee, _svcFeeType);
                            paymentEntity.SVCFeeForPartyServiceFeeAmount = CalculateSVCFee(paymentEntity.PartyServiceFeeAmount, false, 0, 0, _dTaxIncludeInSVCFee, _svcFee, _svcFeeType);
                            paymentEntity.SVCFeeForAreaFeeAmount = CalculateSVCFee(paymentEntity.AreaFeeAmount, false, 0, 0, _dTaxIncludeInSVCFee, _svcFee, _svcFeeType);
                            paymentEntity.SVCFeeForAutoGratuityAmount = CalculateSVCFee(paymentEntity.AutoGratuityAmount, false, 0, 0, _dTaxIncludeInSVCFee, _svcFee, _svcFeeType);

                            paymentEntity.MerchantFeeForServiceFee = CalculateMerchantFee(paymentEntity.ServiceFee + paymentEntity.SVCFeeForServiceFee, _merchantFee);
                            paymentEntity.MerchantFeeForAreaFeeAmount = CalculateMerchantFee(paymentEntity.AreaFeeAmount + paymentEntity.SVCFeeForAreaFeeAmount, _merchantFee);
                            paymentEntity.MerchantFeeForPartyServiceFeeAmount = CalculateMerchantFee(paymentEntity.PartyServiceFeeAmount + paymentEntity.SVCFeeForPartyServiceFeeAmount, _merchantFee);
                            paymentEntity.MerchantFeeForAutoGratuityAmount = CalculateMerchantFee(paymentEntity.AutoGratuityAmount + paymentEntity.SVCFeeForAutoGratuityAmount, _merchantFee);
                        }
                    }
                    else
                    {
                        paymentEntity.SVCFeeForServiceCharge = 0;
                        paymentEntity.SVCFeeForServiceFee = 0;
                        paymentEntity.SVCFeeForPartyServiceFeeAmount = 0;
                        paymentEntity.SVCFeeForAreaFeeAmount = 0;
                        paymentEntity.SVCFeeForAutoGratuityAmount = 0;

                        paymentEntity.MerchantFeeForServiceFee = 0;
                        paymentEntity.MerchantFeeForAreaFeeAmount = 0;
                        paymentEntity.MerchantFeeForPartyServiceFeeAmount = 0;
                        paymentEntity.MerchantFeeForAutoGratuityAmount = 0;
                    }

                    if (paymentEntity.PaymentMethodID != 2 && paymentEntity.CashAdjust != 2)
                    {
                        paymentEntity.MerchantFee = 0;
                    }
                    else
                    {
                        paymentEntity.MerchantFee = CalculateMerchantFee(paymentEntity.AmountTendered + paymentEntity.SVCFeeAmount + paymentEntity.Gratuity, _merchantFee);
                    }

                    sql = $"update SaleReceiptPaymentDetails set " +
                        $"SVCFeeForAreaFeeAmount = '{paymentEntity.SVCFeeForAreaFeeAmount}', " +
                        $"MerchantFeeForAreaFeeAmount = '{paymentEntity.MerchantFeeForAreaFeeAmount}', " +
                        $"SVCFeeForAutoGratuityAmount = '{paymentEntity.SVCFeeForAutoGratuityAmount}', " +
                        $"MerchantFeeForAutoGratuityAmount = '{paymentEntity.MerchantFeeForAutoGratuityAmount}', " +
                        $"SVCFeeForPartyServiceFeeAmount = '{paymentEntity.SVCFeeForPartyServiceFeeAmount}', " +
                        $"MerchantFeeForPartyServiceFeeAmount = '{paymentEntity.MerchantFeeForPartyServiceFeeAmount}', " +
                        $"MerchantFeeForServiceFee = '{paymentEntity.MerchantFeeForServiceFee}', " +
                        $"SVCFeeForServiceFee = '{paymentEntity.SVCFeeForServiceFee}', " +
                        $"SVCFeeForServiceCharge = '{paymentEntity.SVCFeeForServiceCharge}', " +
                        $"MerchantFee = '{paymentEntity.MerchantFee}' " +
                        $"where SaleReceiptPaymentDetailID = '{paymentEntity.SaleReceiptPaymentDetailID}';";

                    await RunQueryUpdateAsync(conn, sql);

                    if (!string.IsNullOrWhiteSpace(paymentEntity.CreditCardTransactionID))
                    {
                        sql = $@"select * from {Helper.Merchant_Name}.dbo.TransactionDetail where ID = '{paymentEntity.CreditCardTransactionID}'";
                        var transactionDetailEntity = await conn.QueryFirstOrDefaultAsync<TransactionDetailEntity>(sql, null, null, 180, System.Data.CommandType.Text);

                        sql = $"update {Helper.Merchant_Name}.dbo.TransactionDetail set " +
                            $"Amount = '{paymentEntity.AmountTendered + paymentEntity.SVCFeeAmount}'," +
                            $"AuthorizeAmount = '{paymentEntity.AmountTendered + paymentEntity.SVCFeeAmount}'," +
                            $"Total = '{paymentEntity.AmountTendered + paymentEntity.SVCFeeAmount + paymentEntity.Gratuity}'," +
                            $"SVCFee = '{paymentEntity.SVCFeeAmount}'," +
                            $"MerchantFee = '{paymentEntity.MerchantFee}'," +
                            $"TaxAmount = '{saleEntity.TaxAmount}'," +
                            $"ServiceCharge = '{saleEntity.SalesOrderFee}'," +
                            $"TotalAmount = '{saleEntity.TotalAmount}'," +
                            $"PartyServiceFeeAmount = '{saleEntity.PartyServiceFeeAmount}'," +
                            $"AreaFeeTaxAmount = '{saleEntity.AreaFeeTaxAmount}'," +
                            $"ServiceFee = '{saleEntity.ServiceFee}'," +
                            $"AutoGratuityTaxAmount = '{saleEntity.AutoGratuityTaxAmount}'," +
                            $"SVCFeeTaxAmount = '{saleEntity.SVCFeeTaxPaidAmount}' " +
                            $"where ID = '{paymentEntity.CreditCardTransactionID}'";

                        await RunQueryUpdateAsync(conn, sql);
                    }
                }

                IsSuccess = true;

            }
            catch (Exception ex)
            {
                BLogicLogger.Exception("UpdateServiceChargeBySaleReceiptID in ProcessFormAsync", ex);
            }
        }

        private async Task UpdateServiceChargesForSaleReceipts(SqlConnection conn, ServiceChargeType serviceChargeType)
        {
            string sql = $@"select * from SaleReceipts where SaleReceiptID = '{requestModel.serviceChargesValues.SaleReceiptID}'";
            var saleReceiptEntity = await conn.QueryFirstOrDefaultAsync<SaleReceiptEntity>(sql, null, null, 180, System.Data.CommandType.Text);

            sql = $@"select * from SaleReceiptPaymentDetails where SaleReceiptID = '{requestModel.serviceChargesValues.SaleReceiptID}'";
            var saleReceiptPaymentDetailEntity = await conn.QueryFirstOrDefaultAsync<SaleReceiptPaymentDetailEntity>(sql, null, null, 180, System.Data.CommandType.Text);

            decimal new_ServiceChargeAmount = 0;
            decimal new_ServiceChargeTaxAmount = 0;

            decimal balance_ServiceChargeAmount = 0;
            decimal balance_ServiceChargeTaxAmount = 0;

            decimal new_TotalAmount_In_SaleReceipts = 0;
            decimal new_SalesOrderFee_In_SaleReceipts = 0;
            decimal new_ServiceChargeTaxAmount_In_SaleReceipts = 0;
            decimal new_TaxAmount_In_SaleReceipts = 0;

            decimal new_AmountTendered_In_SaleReceiptPaymentDetails = 0;
            decimal new_ServiceChargeAmount_InSaleReceiptPaymentDetails = 0;
            decimal new_ServiceChargeTaxAmount_InSaleReceiptPaymentDetails = 0;
            decimal new_PaidServiceFee_InSaleReceiptPaymentDetails = 0;

            switch (serviceChargeType)
            {
                case ServiceChargeType.AreaServiceFee:

                    //if (requestModel.serviceChargesValues.AreaFeeAmount == saleReceiptEntity.AreaFeeAmount && requestModel.serviceChargesValues.AreaFeeTaxAmount == saleReceiptEntity.AreaFeeTaxAmount)
                    //    return;

                    new_ServiceChargeAmount = requestModel.serviceChargesValues.AreaFeeAmount;
                    new_ServiceChargeTaxAmount = requestModel.serviceChargesValues.AreaFeeTaxAmount;

                    balance_ServiceChargeAmount = saleReceiptEntity.AreaFeeAmount - new_ServiceChargeAmount;
                    balance_ServiceChargeTaxAmount = saleReceiptEntity.AreaFeeTaxAmount - new_ServiceChargeTaxAmount;

                    new_TotalAmount_In_SaleReceipts = saleReceiptEntity.TotalAmount - balance_ServiceChargeAmount - balance_ServiceChargeTaxAmount;
                    new_SalesOrderFee_In_SaleReceipts = saleReceiptEntity.SalesOrderFee - balance_ServiceChargeAmount;
                    new_ServiceChargeTaxAmount_In_SaleReceipts = saleReceiptEntity.ServiceChargeTaxAmount - balance_ServiceChargeTaxAmount;
                    new_TaxAmount_In_SaleReceipts = saleReceiptEntity.TaxAmount - balance_ServiceChargeTaxAmount;

                    new_PaidServiceFee_InSaleReceiptPaymentDetails = saleReceiptPaymentDetailEntity.PaidServiceFee - balance_ServiceChargeAmount - balance_ServiceChargeTaxAmount;
                    new_ServiceChargeAmount_InSaleReceiptPaymentDetails = saleReceiptPaymentDetailEntity.AreaFeeAmount - balance_ServiceChargeAmount - balance_ServiceChargeTaxAmount;
                    new_ServiceChargeTaxAmount_InSaleReceiptPaymentDetails = saleReceiptPaymentDetailEntity.AreaFeeTaxAmount - balance_ServiceChargeTaxAmount;
                    new_AmountTendered_In_SaleReceiptPaymentDetails = saleReceiptPaymentDetailEntity.AmountTendered - balance_ServiceChargeAmount - balance_ServiceChargeTaxAmount;

                    break;
                case ServiceChargeType.AutoServiceFee:
                    new_ServiceChargeAmount = requestModel.serviceChargesValues.AutoGratuityAmount;
                    new_ServiceChargeTaxAmount = requestModel.serviceChargesValues.AutoGratuityTaxAmount;

                    balance_ServiceChargeAmount = saleReceiptEntity.AutoGratuityAmount - new_ServiceChargeAmount;
                    balance_ServiceChargeTaxAmount = saleReceiptEntity.AutoGratuityTaxAmount - new_ServiceChargeTaxAmount;

                    new_TotalAmount_In_SaleReceipts = saleReceiptEntity.TotalAmount - balance_ServiceChargeAmount - balance_ServiceChargeTaxAmount;
                    new_SalesOrderFee_In_SaleReceipts = saleReceiptEntity.SalesOrderFee - balance_ServiceChargeAmount;
                    new_ServiceChargeTaxAmount_In_SaleReceipts = saleReceiptEntity.ServiceChargeTaxAmount - balance_ServiceChargeTaxAmount;
                    new_TaxAmount_In_SaleReceipts = saleReceiptEntity.TaxAmount - balance_ServiceChargeTaxAmount;

                    new_PaidServiceFee_InSaleReceiptPaymentDetails = saleReceiptPaymentDetailEntity.PaidServiceFee - balance_ServiceChargeAmount - balance_ServiceChargeTaxAmount;
                    new_ServiceChargeAmount_InSaleReceiptPaymentDetails = saleReceiptPaymentDetailEntity.AutoGratuityAmount - balance_ServiceChargeAmount - balance_ServiceChargeTaxAmount;
                    new_ServiceChargeTaxAmount_InSaleReceiptPaymentDetails = saleReceiptPaymentDetailEntity.AutoGratuityTaxAmount - balance_ServiceChargeTaxAmount;
                    new_AmountTendered_In_SaleReceiptPaymentDetails = saleReceiptPaymentDetailEntity.AmountTendered - balance_ServiceChargeAmount - balance_ServiceChargeTaxAmount;

                    break;
                case ServiceChargeType.PartyServiceFee:
                    new_ServiceChargeAmount = requestModel.serviceChargesValues.PartyServiceFeeAmount;
                    new_ServiceChargeTaxAmount = requestModel.serviceChargesValues.PartyServiceFeeTaxAmount;

                    balance_ServiceChargeAmount = saleReceiptEntity.PartyServiceFeeAmount - new_ServiceChargeAmount;
                    balance_ServiceChargeTaxAmount = saleReceiptEntity.PartyServiceFeeTaxAmount - new_ServiceChargeTaxAmount;

                    new_TotalAmount_In_SaleReceipts = saleReceiptEntity.TotalAmount - balance_ServiceChargeAmount - balance_ServiceChargeTaxAmount;
                    new_SalesOrderFee_In_SaleReceipts = saleReceiptEntity.SalesOrderFee - balance_ServiceChargeAmount;
                    new_ServiceChargeTaxAmount_In_SaleReceipts = saleReceiptEntity.ServiceChargeTaxAmount - balance_ServiceChargeTaxAmount;
                    new_TaxAmount_In_SaleReceipts = saleReceiptEntity.TaxAmount - balance_ServiceChargeTaxAmount;

                    new_PaidServiceFee_InSaleReceiptPaymentDetails = saleReceiptPaymentDetailEntity.PaidServiceFee - balance_ServiceChargeAmount - balance_ServiceChargeTaxAmount;
                    new_ServiceChargeAmount_InSaleReceiptPaymentDetails = saleReceiptPaymentDetailEntity.PartyServiceFeeAmount - balance_ServiceChargeAmount - balance_ServiceChargeTaxAmount;
                    new_ServiceChargeTaxAmount_InSaleReceiptPaymentDetails = saleReceiptPaymentDetailEntity.PartyServiceFeeTaxAmount - balance_ServiceChargeTaxAmount;
                    new_AmountTendered_In_SaleReceiptPaymentDetails = saleReceiptPaymentDetailEntity.AmountTendered - balance_ServiceChargeAmount - balance_ServiceChargeTaxAmount;

                    break;
                case ServiceChargeType.ManualServiceFee:

                    //if (requestModel.serviceChargesValues.ServiceFee == saleReceiptEntity.ServiceFee && requestModel.serviceChargesValues.ServiceFeeTaxAmount == saleReceiptEntity.ServiceFeeTaxAmount)
                    //    return;

                    new_ServiceChargeAmount = requestModel.serviceChargesValues.ServiceFee;
                    new_ServiceChargeTaxAmount = requestModel.serviceChargesValues.ServiceFeeTaxAmount;

                    balance_ServiceChargeAmount = saleReceiptEntity.ServiceFee - new_ServiceChargeAmount;
                    balance_ServiceChargeTaxAmount = saleReceiptEntity.ServiceFeeTaxAmount - new_ServiceChargeTaxAmount;

                    new_TotalAmount_In_SaleReceipts = saleReceiptEntity.TotalAmount - balance_ServiceChargeAmount - balance_ServiceChargeTaxAmount;
                    new_SalesOrderFee_In_SaleReceipts = saleReceiptEntity.SalesOrderFee - balance_ServiceChargeAmount;
                    new_ServiceChargeTaxAmount_In_SaleReceipts = saleReceiptEntity.ServiceChargeTaxAmount - balance_ServiceChargeTaxAmount;
                    new_TaxAmount_In_SaleReceipts = saleReceiptEntity.TaxAmount - balance_ServiceChargeTaxAmount;

                    new_PaidServiceFee_InSaleReceiptPaymentDetails = saleReceiptPaymentDetailEntity.PaidServiceFee - balance_ServiceChargeAmount - balance_ServiceChargeTaxAmount;
                    new_ServiceChargeAmount_InSaleReceiptPaymentDetails = saleReceiptPaymentDetailEntity.ServiceFee - balance_ServiceChargeAmount - balance_ServiceChargeTaxAmount;
                    new_ServiceChargeTaxAmount_InSaleReceiptPaymentDetails = saleReceiptPaymentDetailEntity.ServiceFeeTaxAmount - balance_ServiceChargeTaxAmount;
                    new_AmountTendered_In_SaleReceiptPaymentDetails = saleReceiptPaymentDetailEntity.AmountTendered - balance_ServiceChargeAmount - balance_ServiceChargeTaxAmount;

                    break;

                case ServiceChargeType.SVCFee:

                    //if (requestModel.serviceChargesValues.SVCFeePaidAmount == saleReceiptEntity.SVCFeePaidAmount &&
                    //    requestModel.serviceChargesValues.SVCFeeTaxPaidAmount == saleReceiptEntity.SVCFeeTaxPaidAmount)
                    //    return;

                    break;

                case ServiceChargeType.CashDiscount:

                    //if (requestModel.serviceChargesValues.CashDiscountPaidAmount == saleReceiptEntity.CashDiscountPaidAmount &&
                    //    requestModel.serviceChargesValues.CashDiscountTaxPaidAmount == saleReceiptEntity.CashDiscountTaxPaidAmount)
                    //    return;

                    break;

                default:
                    return;
            }

            switch (serviceChargeType)
            {
                case ServiceChargeType.AreaServiceFee:
                    sql = $"update SaleReceipts set " +
                        $"AreaFeeAmount = '{new_ServiceChargeAmount}', " +
                        $"TotalAmount = '{new_TotalAmount_In_SaleReceipts}', " +
                        $"PaidAmount = '{new_TotalAmount_In_SaleReceipts}', " +
                        $"SalesOrderFee = '{new_SalesOrderFee_In_SaleReceipts}', " +
                        $"TaxAmount = '{new_TaxAmount_In_SaleReceipts}' " +
                        $"where SaleReceiptID = '{saleReceiptEntity.SaleReceiptID}';";
                    await RunQueryUpdateAsync(conn, sql);

                    sql = $"update SaleReceiptPaymentDetails set " +
                        $"AmountTendered = '{new_AmountTendered_In_SaleReceiptPaymentDetails}', " +
                        $"PaidServiceFee = '{new_PaidServiceFee_InSaleReceiptPaymentDetails}', " +
                        $"AreaFeeAmount = '{new_ServiceChargeAmount_InSaleReceiptPaymentDetails}' " +
                        $"where SaleReceiptPaymentDetailID = '{saleReceiptPaymentDetailEntity.SaleReceiptPaymentDetailID}';";
                    await RunQueryUpdateAsync(conn, sql);

                    sql = $"update SaleReceipts set AreaFeeTaxAmount = '{new_ServiceChargeTaxAmount}', ServiceChargeTaxAmount = '{new_ServiceChargeTaxAmount_In_SaleReceipts}' where SaleReceiptID = '{saleReceiptEntity.SaleReceiptID}';";
                    await RunQueryUpdateAsync(conn, sql);

                    sql = $"update SaleReceiptPaymentDetails set AreaFeeTaxAmount = '{new_ServiceChargeTaxAmount_InSaleReceiptPaymentDetails}' where SaleReceiptPaymentDetailID = '{saleReceiptPaymentDetailEntity.SaleReceiptPaymentDetailID}';";
                    await RunQueryUpdateAsync(conn, sql);

                    if (saleReceiptEntity.SVCFeeTaxPaidAmount != 0)
                    {
                        sql = $"update SaleReceiptPaymentDetails set TaxAmountByPaymentModeTaxCustomFee = '{new_TaxAmount_In_SaleReceipts}' where SaleReceiptPaymentDetailID = '{saleReceiptPaymentDetailEntity.SaleReceiptPaymentDetailID}';";
                        await RunQueryUpdateAsync(conn, sql);
                    }

                    break;
                case ServiceChargeType.AutoServiceFee:
                    sql = $"update SaleReceipts set " +
                        $"AutoGratuityAmount = '{new_ServiceChargeAmount}', " +
                        $"TotalAmount = '{new_TotalAmount_In_SaleReceipts}', " +
                        $"PaidAmount = '{new_TotalAmount_In_SaleReceipts}', " +
                        $"SalesOrderFee = '{new_SalesOrderFee_In_SaleReceipts}', " +
                        $"TaxAmount = '{new_TaxAmount_In_SaleReceipts}' " +
                        $"where SaleReceiptID = '{saleReceiptEntity.SaleReceiptID}';";
                    await RunQueryUpdateAsync(conn, sql);

                    sql = $"update SaleReceiptPaymentDetails set " +
                        $"AmountTendered = '{new_AmountTendered_In_SaleReceiptPaymentDetails}', " +
                        $"PaidServiceFee = '{new_PaidServiceFee_InSaleReceiptPaymentDetails}', " +
                        $"AutoGratuityAmount = '{new_ServiceChargeAmount_InSaleReceiptPaymentDetails}' " +
                        $"where SaleReceiptPaymentDetailID = '{saleReceiptPaymentDetailEntity.SaleReceiptPaymentDetailID}';";
                    await RunQueryUpdateAsync(conn, sql);

                    sql = $"update SaleReceipts set AutoGratuityTaxAmount = '{new_ServiceChargeTaxAmount}', ServiceChargeTaxAmount = '{new_ServiceChargeTaxAmount_In_SaleReceipts}' where SaleReceiptID = '{saleReceiptEntity.SaleReceiptID}';";
                    await RunQueryUpdateAsync(conn, sql);

                    sql = $"update SaleReceiptPaymentDetails set AutoGratuityTaxAmount = '{new_ServiceChargeTaxAmount_InSaleReceiptPaymentDetails}' where SaleReceiptPaymentDetailID = '{saleReceiptPaymentDetailEntity.SaleReceiptPaymentDetailID}';";
                    await RunQueryUpdateAsync(conn, sql);

                    if (saleReceiptEntity.SVCFeeTaxPaidAmount != 0)
                    {
                        sql = $"update SaleReceiptPaymentDetails set TaxAmountByPaymentModeTaxCustomFee = '{new_TaxAmount_In_SaleReceipts}' where SaleReceiptPaymentDetailID = '{saleReceiptPaymentDetailEntity.SaleReceiptPaymentDetailID}';";
                        await RunQueryUpdateAsync(conn, sql);
                    }

                    break;
                case ServiceChargeType.PartyServiceFee:
                    sql = $"update SaleReceipts set " +
                        $"PartyServiceFeeAmount = '{new_ServiceChargeAmount}', " +
                        $"TotalAmount = '{new_TotalAmount_In_SaleReceipts}', " +
                        $"PaidAmount = '{new_TotalAmount_In_SaleReceipts}', " +
                        $"SalesOrderFee = '{new_SalesOrderFee_In_SaleReceipts}', " +
                        $"TaxAmount = '{new_TaxAmount_In_SaleReceipts}' " +
                        $"where SaleReceiptID = '{saleReceiptEntity.SaleReceiptID}';";
                    await RunQueryUpdateAsync(conn, sql);

                    sql = $"update SaleReceiptPaymentDetails set " +
                        $"AmountTendered = '{new_AmountTendered_In_SaleReceiptPaymentDetails}', " +
                        $"PaidServiceFee = '{new_PaidServiceFee_InSaleReceiptPaymentDetails}', " +
                        $"PartyServiceFeeAmount = '{new_ServiceChargeAmount_InSaleReceiptPaymentDetails}' " +
                        $"where SaleReceiptPaymentDetailID = '{saleReceiptPaymentDetailEntity.SaleReceiptPaymentDetailID}';";
                    await RunQueryUpdateAsync(conn, sql);

                    sql = $"update SaleReceipts set PartyServiceFeeTaxAmount = '{new_ServiceChargeTaxAmount}', ServiceChargeTaxAmount = '{new_ServiceChargeTaxAmount_In_SaleReceipts}' where SaleReceiptID = '{saleReceiptEntity.SaleReceiptID}';";
                    await RunQueryUpdateAsync(conn, sql);

                    sql = $"update SaleReceiptPaymentDetails set PartyServiceFeeTaxAmount = '{new_ServiceChargeTaxAmount_InSaleReceiptPaymentDetails}' where SaleReceiptPaymentDetailID = '{saleReceiptPaymentDetailEntity.SaleReceiptPaymentDetailID}';";
                    await RunQueryUpdateAsync(conn, sql);

                    if (saleReceiptEntity.SVCFeeTaxPaidAmount != 0)
                    {
                        sql = $"update SaleReceiptPaymentDetails set TaxAmountByPaymentModeTaxCustomFee = '{new_TaxAmount_In_SaleReceipts}' where SaleReceiptPaymentDetailID = '{saleReceiptPaymentDetailEntity.SaleReceiptPaymentDetailID}';";
                        await RunQueryUpdateAsync(conn, sql);
                    }

                    break;
                case ServiceChargeType.ManualServiceFee:

                    sql = $"update SaleReceipts set " +
                        $"ServiceFee = '{new_ServiceChargeAmount}', " +
                        $"TotalAmount = '{new_TotalAmount_In_SaleReceipts}', " +
                        $"PaidAmount = '{new_TotalAmount_In_SaleReceipts}', " +
                        $"SalesOrderFee = '{new_SalesOrderFee_In_SaleReceipts}', " +
                        $"TaxAmount = '{new_TaxAmount_In_SaleReceipts}' " +
                        $"where SaleReceiptID = '{saleReceiptEntity.SaleReceiptID}';";
                    await RunQueryUpdateAsync(conn, sql);

                    sql = $"update SaleReceiptPaymentDetails set " +
                        $"AmountTendered = '{new_AmountTendered_In_SaleReceiptPaymentDetails}', " +
                        $"PaidServiceFee = '{new_PaidServiceFee_InSaleReceiptPaymentDetails}', " +
                        $"ServiceFee = '{new_ServiceChargeAmount_InSaleReceiptPaymentDetails}' " +
                        $"where SaleReceiptPaymentDetailID = '{saleReceiptPaymentDetailEntity.SaleReceiptPaymentDetailID}';";
                    await RunQueryUpdateAsync(conn, sql);

                    sql = $"update SaleReceipts set ServiceFeeTaxAmount = '{new_ServiceChargeTaxAmount}', ServiceChargeTaxAmount = '{new_ServiceChargeTaxAmount_In_SaleReceipts}' where SaleReceiptID = '{saleReceiptEntity.SaleReceiptID}';";
                    await RunQueryUpdateAsync(conn, sql);

                    sql = $"update SaleReceiptPaymentDetails set ServiceFeeTaxAmount = '{new_ServiceChargeTaxAmount_InSaleReceiptPaymentDetails}' where SaleReceiptPaymentDetailID = '{saleReceiptPaymentDetailEntity.SaleReceiptPaymentDetailID}';";
                    await RunQueryUpdateAsync(conn, sql);

                    if (saleReceiptEntity.SVCFeeTaxPaidAmount != 0)
                    {
                        sql = $"update SaleReceiptPaymentDetails set TaxAmountByPaymentModeTaxCustomFee = '{new_TaxAmount_In_SaleReceipts}' where SaleReceiptPaymentDetailID = '{saleReceiptPaymentDetailEntity.SaleReceiptPaymentDetailID}';";
                        await RunQueryUpdateAsync(conn, sql);
                    }

                    break;

                case ServiceChargeType.SVCFee:

                    sql = $"update SaleReceipts set " +
                        $"SVCFeeAmount = '{requestModel.serviceChargesValues.SVCFeePaidAmount + requestModel.serviceChargesValues.SVCFeeTaxPaidAmount}', " +
                        $"SVCFeePaidAmount = '{requestModel.serviceChargesValues.SVCFeePaidAmount + requestModel.serviceChargesValues.SVCFeeTaxPaidAmount}' " +
                        $"where SaleReceiptID = '{saleReceiptEntity.SaleReceiptID}';";
                    await RunQueryUpdateAsync(conn, sql);

                    sql = $"update SaleReceipts set " +
                        $"SVCFeeTaxAmount = '{requestModel.serviceChargesValues.SVCFeeTaxPaidAmount}', " +
                        $"SVCFeeTaxPaidAmount = '{requestModel.serviceChargesValues.SVCFeeTaxPaidAmount}' " +
                        $"where SaleReceiptID = '{saleReceiptEntity.SaleReceiptID}';";
                    await RunQueryUpdateAsync(conn, sql);

                    sql = $"update SaleReceiptPaymentDetails set " +
                        $"SVCFeeAmount = '{requestModel.serviceChargesValues.SVCFeePaidAmount + requestModel.serviceChargesValues.SVCFeeTaxPaidAmount}' " +
                        $"where SaleReceiptPaymentDetailID = '{saleReceiptPaymentDetailEntity.SaleReceiptPaymentDetailID}';";
                    await RunQueryUpdateAsync(conn, sql);

                    sql = $"update SaleReceiptPaymentDetails set " +
                        $"SVCFeeTaxAmount = '{requestModel.serviceChargesValues.SVCFeeTaxPaidAmount}' " +
                        $"where SaleReceiptPaymentDetailID = '{saleReceiptPaymentDetailEntity.SaleReceiptPaymentDetailID}';";
                    await RunQueryUpdateAsync(conn, sql);

                    break;

                case ServiceChargeType.CashDiscount:

                    sql = $"update SaleReceipts set " +
                        $"CashDiscountAmount = '{requestModel.serviceChargesValues.CashDiscountPaidAmount + requestModel.serviceChargesValues.CashDiscountTaxPaidAmount}', " +
                        $"CashDiscountPaidAmount = '{requestModel.serviceChargesValues.CashDiscountPaidAmount + requestModel.serviceChargesValues.CashDiscountTaxPaidAmount}' " +
                        $"where SaleReceiptID = '{saleReceiptEntity.SaleReceiptID}';";
                    await RunQueryUpdateAsync(conn, sql);

                    sql = $"update SaleReceipts set " +
                        $"CashDiscountTaxAmount = '{requestModel.serviceChargesValues.CashDiscountTaxPaidAmount}', " +
                        $"CashDiscountTaxPaidAmount = '{requestModel.serviceChargesValues.CashDiscountTaxPaidAmount}' " +
                        $"where SaleReceiptID = '{saleReceiptEntity.SaleReceiptID}';";
                    await RunQueryUpdateAsync(conn, sql);

                    sql = $"update SaleReceiptPaymentDetails set " +
                        $"CashDiscountAmount = '{requestModel.serviceChargesValues.CashDiscountPaidAmount + requestModel.serviceChargesValues.CashDiscountTaxPaidAmount}' " +
                        $"where SaleReceiptPaymentDetailID = '{saleReceiptPaymentDetailEntity.SaleReceiptPaymentDetailID}';";
                    await RunQueryUpdateAsync(conn, sql);

                    sql = $"update SaleReceiptPaymentDetails set " +
                        $"CashDiscountTaxAmount = '{requestModel.serviceChargesValues.CashDiscountTaxPaidAmount}' " +
                        $"where SaleReceiptPaymentDetailID = '{saleReceiptPaymentDetailEntity.SaleReceiptPaymentDetailID}';";
                    await RunQueryUpdateAsync(conn, sql);

                    break;
            }
        }

        public decimal CalculateSVCFee(decimal amount, bool isApplyTaxCustomFee, decimal dSVCFeeTaxRate, decimal dSVCFeeTaxAmountByPayment,
            int sTaxIncludeInSVCFee, decimal svcFee, int svcFeeType)
        {
            decimal svcFeeReturn = 0;

            try
            {
                if (amount == 0)
                    return svcFeeReturn;

                if (isApplyTaxCustomFee)
                {
                    if (svcFeeType == 1)
                    {
                        svcFeeReturn = Utility.RoundUp(amount * (svcFee / 100));
                        if (sTaxIncludeInSVCFee == 1 && dSVCFeeTaxAmountByPayment != 0)
                        {
                            decimal tempTax = dSVCFeeTaxRate <= 0 ? 0 : Utility.RoundUp((svcFeeReturn / (1 + dSVCFeeTaxRate / 100)) * dSVCFeeTaxRate / 100);
                            svcFeeReturn -= tempTax;
                        }
                    }
                    else
                        svcFeeReturn = 0;
                }
                else
                {
                    if (svcFeeType == 1)
                        svcFeeReturn = Math.Round(amount * (svcFee / 100), 2);
                    else
                        svcFeeReturn = 0;
                }
            }
            catch (Exception ex)
            {
                svcFeeReturn = 0;
                BLogicLogger.Error("#### CalculateSVCFee error: " + ex);
            }
            return svcFeeReturn;
        }

        public decimal CalculateMerchantFee(decimal amount, decimal merchantFeeValue)
        {
            decimal merchantFeeAmount = 0;

            try
            {
                merchantFeeAmount = Math.Round(amount * (merchantFeeValue / 100), 2);
            }
            catch (Exception ex)
            {
                merchantFeeAmount = 0;
                BLogicLogger.Error("#### CalculateMerchantFee error: " + ex);
            }
            return merchantFeeAmount;
        }
    }
}
