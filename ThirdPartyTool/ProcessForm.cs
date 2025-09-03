using BLogicSystems.Merchant.PAXClient;
using BLogicSystems.Merchant.PAXTerminalHelper;
using BLogicSystems.XPos.DTO;
using BLogicSystems.XPos.SharedModule.Models;
using BLogicSystems.XPos.Utilites;
using BLogicSystems.XPos.XPOSReports;
using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Forms;
using ThirdPartyTool.Dialog;
using ThirdPartyTool.Models;
using ThirdPartyTool.SaleReceipt;

namespace ThirdPartyTool
{
    public enum ProcessNameEnum
    {
        None,
        GetReceiptTemplateByTicketNumber = 1,
        AddTipByTokenReceipt = 2,
        AddTipForPaymentDetail = 3,
        SettleBatchOffline = 4
    }
    public partial class ProcessForm : Form
    {
        BatchStatus batch = new BatchStatus();
        public string SaleReceiptNumber = string.Empty;
        public string SaleReceiptID = string.Empty;
        public XtraReport ReceiptTemplate = null;
        private BackgroundWorker backgroundWorker;
        private ProcessNameEnum CurrentProcessName = ProcessNameEnum.None;
        public bool IsSuccess = false;
        public string ErrorMessage = string.Empty;

        private string AddTipByTokenReceipt_MainSaleReceiptID = string.Empty;
        private string AddTipByTokenReceipt_TokenSaleReceiptID = string.Empty;

        private string AddTipByTokenReceipt_MainSaleReceiptNumber = string.Empty;
        private string AddTipByTokenReceipt_TokenSaleReceiptNumber = string.Empty;

        private string AddTipForPaymentDetail_SaleReceiptID = string.Empty;
        private string AddTipForPaymentDetail_SaleReceiptPaymentDetailID = string.Empty;
        private decimal AddTipForPaymentDetail_Gratuity = 0;

        public ProcessForm()
        {
            InitializeComponent();

            backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += BackgroundWorker_DoWork;
            backgroundWorker.RunWorkerCompleted += BackgroundWorker_RunWorkerCompleted;

            this.Load += ProcessForm_Load;
        }

        public void InitGetReceiptTemplate(string ticketNumber)
        {
            CurrentProcessName = ProcessNameEnum.GetReceiptTemplateByTicketNumber;
            this.SaleReceiptNumber = ticketNumber;
        }

        public void InitAddTipByTokenReceipt(string saleReceiptID, string tokenSaleReceiptID)
        {
            CurrentProcessName = ProcessNameEnum.AddTipByTokenReceipt;
            this.AddTipByTokenReceipt_MainSaleReceiptID = saleReceiptID;
            this.AddTipByTokenReceipt_TokenSaleReceiptID = tokenSaleReceiptID;
        }

        public void InitSettleBatchOffline()
        {
            CurrentProcessName = ProcessNameEnum.SettleBatchOffline;
        }

        public void InitAddTipForPaymentDetail(string saleReceiptID, string saleReceiptPaymentDetailID, decimal gratuity)
        {
            CurrentProcessName = ProcessNameEnum.AddTipForPaymentDetail;
            this.AddTipForPaymentDetail_SaleReceiptID = saleReceiptID;
            this.AddTipForPaymentDetail_SaleReceiptPaymentDetailID = saleReceiptPaymentDetailID;
            this.AddTipForPaymentDetail_Gratuity = gratuity;
        }

        private void ProcessForm_Load(object sender, EventArgs e)
        {
            backgroundWorker.RunWorkerAsync();
        }

        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                switch (CurrentProcessName)
                {
                    case ProcessNameEnum.GetReceiptTemplateByTicketNumber:
                        GetReceiptTemplateByTicketNumber(SaleReceiptNumber);
                        break;
                    case ProcessNameEnum.AddTipByTokenReceipt:
                        UpdateTipByTokenReceipt();
                        break;
                    case ProcessNameEnum.AddTipForPaymentDetail:
                        AddTipForPaymentDetail();
                        break;
                    case ProcessNameEnum.SettleBatchOffline:
                        ProcessSettleBatchOffline();
                        break;
                }
            }
            catch (Exception ex)
            {
                // log exception
                BLogicLogger.Exception("BackgroundWorker_DoWork", ex, new System.Collections.Generic.List<object>() { CurrentProcessName });
            }
        }

        private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (!IsSuccess)
                {
                    InformationDialogForm errorDialog = new InformationDialogForm(ErrorMessage);
                    FormHelper.ShowPopUpFormWithDIMBackground(errorDialog);
                    this.Close();
                    return;
                }

                string message = string.Empty;
                switch (CurrentProcessName)
                {
                    case ProcessNameEnum.AddTipByTokenReceipt:
                        message = $"Update Tip Ticket# {AddTipByTokenReceipt_MainSaleReceiptNumber} done. Refresh Now";
                        break;
                    default:
                        break;
                }

                if (!string.IsNullOrWhiteSpace(message))
                {
                    InformationDialogForm informationDialog = new InformationDialogForm(message);
                    informationDialog.ShowDialog();
                }

                this.Close();
            }
            catch (Exception ex)
            {
                // log exception
                BLogicLogger.Exception("BackgroundWorker_RunWorkerCompleted", ex, new System.Collections.Generic.List<object>() { CurrentProcessName });
            }
        }

        private void GetReceiptTemplateByTicketNumber(string ticketNumber)
        {
            try
            {
                DataRow[] drs = null;

                SaleReceiptDTO saleReceiptDTO = new SaleReceiptDTO();
                StoreDTO storeDTO = new StoreDTO();

                DataTable dtSaleReceips = new DataTable("SaleReceipts");
                DataTable dtTaxCodes = new DataTable("TaxCodes");
                DataTable dtSaleReceiptPaymentDetails = new DataTable("SaleReceiptPaymentDetails");
                DataTable dtCustomer = new DataTable("Customers");
                DataTable dtCashierID = new DataTable("CashierID");
                DataTable dtDeliveryDetails = new DataTable("DeliveryDetails");
                DataTable dtEmployeePaidID = new DataTable("EmployeePaidID");
                DataTable dtTables = new DataTable("Tables");
                DataTable dtTablesInDatabase = new DataTable("Tables");

                string TransactionStatusID = string.Empty;
                string currentCustomerName = string.Empty;
                string currentCustomerPhone = string.Empty;
                string customerID = string.Empty;
                int employeePaidID = 0;
                int cashierID = 0;
                string tableID = string.Empty;
                object saleID = null;
                string EmployeeName = "";
                string EmployeePaidName = string.Empty;
                DataTable dtConfigurations = new DataTable("Configurations");
                DataTable dtStores = new DataTable("Stores");
                SaleReceiptEntity saleReceiptEntity = null;
                using (SqlCommand sqlCommand = new SqlCommand())
                using (var conn = new SqlConnection(Helper.BLOGICPOS7_Connection))
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();

                    sqlCommand.Connection = conn;
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.CommandText = "select * from Configurations";
                    var reader = sqlCommand.ExecuteReader();

                    dtConfigurations.Load(reader);
                    reader.Close();

                    sqlCommand.CommandText = "select * from Stores";
                    reader = sqlCommand.ExecuteReader();
                    dtStores.Load(reader);
                    reader.Close();

                    sqlCommand.CommandText = $"select * from SaleReceipts where SaleReceiptNumber like '{ticketNumber}' and TransactionStatusID = 'FIN'";
                    reader = sqlCommand.ExecuteReader();
                    saleReceiptDTO.SaleReceipts.Load(reader);
                    reader.Close();

                    sqlCommand.CommandText = $"SELECT TABLE_NAME " +
                        $"FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE' and TABLE_NAME in ('SaleReceiptDetailTaxDetails'," +
                        $"'SaleReceiptServiceChargeTaxDetails'," +
                        $"'DeliveryDetails', 'PaymentRoleDetails') ";
                    reader = sqlCommand.ExecuteReader();
                    dtTablesInDatabase.Load(reader);
                    reader.Close();

                    if (saleReceiptDTO.SaleReceipts?.Count == 1)
                    {

                    }
                    else if (saleReceiptDTO.SaleReceipts?.Count > 1)
                    {
                        SelectSaleReceiptForm selectSaleReceiptForm = new SelectSaleReceiptForm();

                    }
                    else
                    {
                        ErrorMessage = "Ticket not found.";
                        return;
                    }

                    saleID = Utility.GetStringFromRow("SaleReceiptID", saleReceiptDTO.SaleReceipts.Rows[0]);
                    customerID = Utility.GetStringFromRow("CustomerID", saleReceiptDTO.SaleReceipts.Rows[0]);
                    employeePaidID = Utility.GetIntFromRow("EmployeePaidID", saleReceiptDTO.SaleReceipts.Rows[0]);
                    cashierID = Utility.GetIntFromRow("CashierID", saleReceiptDTO.SaleReceipts.Rows[0]);
                    currentCustomerPhone = Utility.GetStringFromRow("CurrentCustomerPhone", saleReceiptDTO.SaleReceipts.Rows[0]);
                    currentCustomerName = Utility.GetStringFromRow("CurrentCustomerName", saleReceiptDTO.SaleReceipts.Rows[0]);
                    tableID = Utility.GetStringFromRow("TableID", saleReceiptDTO.SaleReceipts.Rows[0]);
                    TransactionStatusID = Utility.GetStringFromRow("TransactionStatusID", saleReceiptDTO.SaleReceipts.Rows[0]);

                    switch (TransactionStatusID)
                    {
                        case "OPER":
                            ErrorMessage = "Ticket is currently open. Please finish it before updating.";
                            break;
                        case "FIN":
                            break;
                        default:
                            ErrorMessage = "Ticket not found.";
                            break;

                    }

                    if (!string.IsNullOrWhiteSpace(ErrorMessage))
                    {
                        return;
                    }

                    sqlCommand.CommandText = $"select * from SaleReceiptDetails where SaleReceiptID = '{saleID}' and Status <> '2'";
                    reader = sqlCommand.ExecuteReader();
                    saleReceiptDTO.SaleReceiptDetails.Load(reader);
                    reader.Close();

                    drs = dtTablesInDatabase.Select("TABLE_NAME = 'SaleReceiptDetailTaxDetails'", "");
                    if (drs.Length > 0)
                    {
                        sqlCommand.CommandText = $"select * from SaleReceiptDetailTaxDetails where SaleReceiptID = '{saleID}' and Status <> '2'";
                        reader = sqlCommand.ExecuteReader();
                        saleReceiptDTO.SaleReceiptDetailTaxDetails.Load(reader);
                        reader.Close();
                    }

                    drs = dtTablesInDatabase.Select("TABLE_NAME = 'SaleReceiptServiceChargeTaxDetails'", "");
                    if (drs.Length > 0)
                    {
                        sqlCommand.CommandText = $"select * from SaleReceiptServiceChargeTaxDetails where SaleReceiptID = '{saleID}'";
                        reader = sqlCommand.ExecuteReader();
                        saleReceiptDTO.SaleReceiptServiceChargeTaxDetails.Load(reader);
                        reader.Close();
                    }

                    sqlCommand.CommandText = $"select a.*,b.PaymentMethodName from SaleReceiptPaymentDetails a left join PaymentMethods b on a.PaymentMethodID = b.PaymentMethodID where SaleReceiptID = '{saleID}'";
                    reader = sqlCommand.ExecuteReader();
                    dtSaleReceiptPaymentDetails.Load(reader);
                    reader.Close();

                    drs = dtTablesInDatabase.Select("TABLE_NAME = 'DeliveryDetails'", "");
                    if (drs.Length > 0)
                    {
                        sqlCommand.CommandText = $"select ExternalDeliveryID, sum(Fee) as Fee, sum(Tip) as Tip from DeliveryDetails where SaleReceiptID = '{saleID}' group by ExternalDeliveryID";
                        reader = sqlCommand.ExecuteReader();
                        dtDeliveryDetails.Load(reader);
                        reader.Close();
                    }

                    sqlCommand.CommandText = $"select * from TaxCodes";
                    reader = sqlCommand.ExecuteReader();
                    dtTaxCodes.Load(reader);
                    reader.Close();

                    sqlCommand.CommandText = $"select * from Stores";
                    reader = sqlCommand.ExecuteReader();
                    storeDTO.Stores.Load(reader);
                    reader.Close();

                    if (!string.IsNullOrWhiteSpace(customerID))
                    {
                        sqlCommand.CommandText = $"select* from Customers where CustomerID = '{customerID}'";
                        reader = sqlCommand.ExecuteReader();
                        dtCustomer.Load(reader);
                        reader.Close();
                    }

                    if (employeePaidID > 0)
                    {
                        sqlCommand.CommandText = $"select* from Employees where EmployeeID = '{employeePaidID}'";
                        reader = sqlCommand.ExecuteReader();
                        dtEmployeePaidID.Load(reader);
                        reader.Close();
                    }

                    if (cashierID > 0 && cashierID != employeePaidID)
                    {
                        sqlCommand.CommandText = $"select* from Employees where EmployeeID = '{cashierID}'";
                        reader = sqlCommand.ExecuteReader();
                        dtCashierID.Load(reader);
                        reader.Close();
                    }

                    if (!string.IsNullOrWhiteSpace(tableID))
                    {
                        sqlCommand.CommandText = $"select* from Tables where TableID = '{tableID}'";
                        reader = sqlCommand.ExecuteReader();
                        dtTables.Load(reader);
                        reader.Close();
                    }

                }

                Utility.IsCredit = true;
                POSConfig.Configuration.ConfigValue.GetValueFromTable(dtConfigurations);
                Utility.IsUseConfigDualPrice = Utility.IsCheckUseDualPrice();
                Utility.IsCurrentPriceUseDualPrice = Utility.IsShowPriceUseDualPrice();
                Utility.IsShowCreditPriceOnScreen = POSConfig.Configuration.ConfigValue.SystemConfigValue.ShowItemPriceOnDualPriceMode == "1";
                if (!Utility.IsUseConfigDualPrice)
                    Utility.IsShowCreditPriceOnScreen = false;

                if (dtEmployeePaidID?.Rows.Count > 0)
                {
                    if (POSConfig.Configuration.ConfigValue.SystemConfigValue.PrintEmployeeNameOnReceipt.Equals("1")) // Only Last name
                        EmployeePaidName = Utility.GetStringFromRow("EmployeeLastName", dtEmployeePaidID.Rows[0]);
                    else if (POSConfig.Configuration.ConfigValue.SystemConfigValue.PrintEmployeeNameOnReceipt.Equals("2")) // First name, Last name
                        EmployeePaidName = Utility.GetStringFromRow("EmployeeFirstName", dtEmployeePaidID.Rows[0]) + ", " + Utility.GetStringFromRow("EmployeeLastName", dtEmployeePaidID.Rows[0]);
                    else if (POSConfig.Configuration.ConfigValue.SystemConfigValue.PrintEmployeeNameOnReceipt.Equals("3")) // Last name, First name
                        EmployeePaidName = Utility.GetStringFromRow("EmployeeLastName", dtEmployeePaidID.Rows[0]) + ", " + Utility.GetStringFromRow("EmployeeFirstName", dtEmployeePaidID.Rows[0]);
                    else
                        EmployeePaidName = Utility.GetStringFromRow("EmployeeFirstName", dtEmployeePaidID.Rows[0]);
                }

                if (dtCashierID?.Rows.Count > 0)
                {
                    if (POSConfig.Configuration.ConfigValue.SystemConfigValue.PrintEmployeeNameOnReceipt.Equals("1")) // Only Last name
                        EmployeeName = Utility.GetStringFromRow("EmployeeLastName", dtCashierID.Rows[0]);
                    else if (POSConfig.Configuration.ConfigValue.SystemConfigValue.PrintEmployeeNameOnReceipt.Equals("2")) // First name, Last name
                        EmployeeName = Utility.GetStringFromRow("EmployeeFirstName", dtCashierID.Rows[0]) + ", " + Utility.GetStringFromRow("EmployeeLastName", dtCashierID.Rows[0]);
                    else if (POSConfig.Configuration.ConfigValue.SystemConfigValue.PrintEmployeeNameOnReceipt.Equals("3")) // Last name, First name
                        EmployeeName = Utility.GetStringFromRow("EmployeeLastName", dtCashierID.Rows[0]) + ", " + Utility.GetStringFromRow("EmployeeFirstName", dtCashierID.Rows[0]);
                    else
                        EmployeeName = Utility.GetStringFromRow("EmployeeFirstName", dtCashierID.Rows[0]);
                }


                DataSet dsParentID = new DataSet();
                dsParentID.Tables.Add(new DataTable());
                dsParentID.Tables[0].Columns.Add(new DataColumn("SaleReceipt"));
                dsParentID.Tables[0].Rows.Add(new object[] { saleID });
                Utility.AddSessionFullTable(dsParentID);

                //var dsResult = Helper.ProcessRequest("SaleReceiptFacade", "SelectChildAndAssociated", dsParentID);
                //DataTable dtAssosiateTable = dsResult.Tables["AssosiateTable"];

                DataRow ReceiptRow = saleReceiptDTO.SaleReceipts.Rows[0];
                //string sPrintServer = POSConfig.Configuration.ConfigValue.SystemConfigValue.PrintServer;
                string WorkstationID = string.Empty;
                //string WorkStationCode = dtAssosiateTable.Rows[0]["WorkStationCode"].ToString();
                decimal TotalAmount = Utility.GetDecimalFromObject(dtSaleReceiptPaymentDetails.Compute("SUM(AmountTendered)", ""));
                decimal TotalCharge = Utility.GetDecimalFromObject(dtSaleReceiptPaymentDetails.Compute("SUM(AmountReturn)", ""));
                //string sCustPhone = dtAssosiateTable.Rows[0]["CustomerPhone"].ToString();
                string sCustAddress = string.Empty;
                //string sCustCompany = dtAssosiateTable.Rows[0]["CustomerCompany"].ToString();
                //string sTableName = dtAssosiateTable.Rows[0]["TableName"].ToString();
                //decimal DeliveryFee = decimal.Parse(dtAssosiateTable.Rows[0]["DeliveryFee"].ToString());
                //short SplitStatus = short.Parse(dtAssosiateTable.Rows[0]["SplitStatus"].ToString());
                //EmployeePaidName = Convert.IsDBNull(dtAssosiateTable.Rows[0]["EmployeePaidName"]) ? "" : dtAssosiateTable.Rows[0]["EmployeePaidName"].ToString();
                int ReceiptType = Utility.GetIntFromObject(ReceiptRow["SaleReceiptType"]);
                decimal ExtraFee = Utility.GetDecimalFromObject(ReceiptRow["SalesOrderFee"]);
                string sTableName = dtTables?.Rows.Count > 0 ? Utility.GetStringFromRow("TableName", dtTables.Rows[0]) : "";
                decimal DeliveryFee = Utility.GetDecimalFromObject(ReceiptRow["DeliveryFee"]);
                decimal DeliveryTip = 0;
                if (dtDeliveryDetails?.Rows.Count > 0)
                {
                    DeliveryFee = Utility.GetDecimalFromObject(dtDeliveryDetails.Rows[0]["Fee"]);
                    DeliveryTip = Utility.GetDecimalFromObject(dtDeliveryDetails.Rows[0]["Tip"]);
                }

                SaleReceipts_ReceiptInfo receiptInfoModel = Utility.ParseJsonToReceiptInfo(Utility.GetStringFromRow("ReceiptInfo", saleReceiptDTO.SaleReceipts.Rows[0]));
                if (!string.IsNullOrWhiteSpace(receiptInfoModel.LastWorkstationName))
                    WorkstationID = receiptInfoModel.LastWorkstationName;
                else
                {
                    if (!string.IsNullOrWhiteSpace(Utility.GetStringFromRow("SaleReceiptID", saleReceiptDTO.SaleReceipts.Rows[0])) &&
                    !string.IsNullOrWhiteSpace(Utility.GetStringFromRow("WorkstationName", saleReceiptDTO.SaleReceipts.Rows[0])))
                        WorkstationID = Utility.GetStringFromRow("WorkstationName", saleReceiptDTO.SaleReceipts.Rows[0]);
                }

                if (!dtTaxCodes.Columns.Contains("SortOrder"))
                    dtTaxCodes.Columns.Add("SortOrder", typeof(int));

                saleReceiptDTO.Merge(dtTaxCodes);

                ReceiptTemplate = new Templates().PrintSaleReceiptForBarResWithPrinterName(saleReceiptDTO, null, storeDTO,
                    new DataSet(), "Receipt.repx", "", EmployeeName, "POS", ReceiptType, TotalAmount,
                    TotalCharge, false, currentCustomerPhone, "", sTableName, ExtraFee, DeliveryFee, false,
                    0, dtSaleReceiptPaymentDetails, null, null, new DataTable(), EmployeePaidName, false);

                IsSuccess = true;
                SaleReceiptID = Utility.GetStringFromRow("SaleReceiptID", saleReceiptDTO.SaleReceipts.Rows[0]);
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Something went wrong while searching for the ticket.{Environment.NewLine}Please try again or contact TruongDS.";
                BLogicLogger.Exception("GetReceiptTemplateByTicketNumber", ex, new System.Collections.Generic.List<object>() { ticketNumber });
            }
        }

        private void UpdateTipByTokenReceipt()
        {
            DataRow[] drs = null;
            string scriptUpdate = string.Empty;
            string scriptUpdatePaymentRoleDetails = string.Empty;
            string sql = string.Empty;
            int resultQueryUpdate = 0;

            try
            {
                DataTable dtSaleReceipts = new DataTable("SaleReceipts");
                DataTable dtSaleReceiptPaymentDetails = new DataTable("SaleReceiptPaymentDetails");

                DataTable dtTokenSaleReceipts = new DataTable("SaleReceipts");
                DataTable dtTokenSaleReceiptPaymentDetails = new DataTable("SaleReceiptPaymentDetails");

                DataTable dtTablesInDatabase = new DataTable("Tables");
                DataTable dtPaymentRoleDetails = new DataTable("PaymentRoleDetails");
                using (SqlCommand sqlCommand = new SqlCommand())
                using (SqlConnection conn = new SqlConnection(Helper.BLOGICPOS7_Connection))
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();

                    sqlCommand.Connection = conn;
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.CommandText = $"select * from SaleReceipts where SaleReceiptID = '{AddTipByTokenReceipt_MainSaleReceiptID}'";
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    dtSaleReceipts.Load(reader);
                    reader.Close();

                    sqlCommand.CommandText = $"select * from SaleReceipts where SaleReceiptID = '{AddTipByTokenReceipt_TokenSaleReceiptID}'";
                    reader = sqlCommand.ExecuteReader();
                    dtTokenSaleReceipts.Load(reader);
                    reader.Close();

                    sqlCommand.CommandText = $"select * from SaleReceiptPaymentDetails where SaleReceiptID = '{AddTipByTokenReceipt_MainSaleReceiptID}'";
                    reader = sqlCommand.ExecuteReader();
                    dtSaleReceiptPaymentDetails.Load(reader);
                    reader.Close();

                    sqlCommand.CommandText = $"select * from SaleReceiptPaymentDetails where SaleReceiptID = '{AddTipByTokenReceipt_TokenSaleReceiptID}'";
                    reader = sqlCommand.ExecuteReader();
                    dtTokenSaleReceiptPaymentDetails.Load(reader);
                    reader.Close();

                    sqlCommand.CommandText = $"SELECT TABLE_NAME " +
                       $"FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE' and TABLE_NAME in ('SaleReceiptDetailTaxDetails'," +
                       $"'SaleReceiptServiceChargeTaxDetails'," +
                       $"'DeliveryDetails', 'PaymentRoleDetails') ";
                    reader = sqlCommand.ExecuteReader();
                    dtTablesInDatabase.Load(reader);
                    reader.Close();
                    #region verify Receipt
                    if (dtTokenSaleReceiptPaymentDetails == null || dtTokenSaleReceiptPaymentDetails.Rows.Count == 0)
                    {
                        ErrorMessage = "The Token Receipt don't exist any Payment Detail.";
                        return;
                    }
                    #endregion

                    var CashierID = Utility.GetIntFromRow("CashierID", dtSaleReceipts.Rows[0]);
                    var DateCreated = Utility.GetDateTimeFromRow("DateCreated", dtSaleReceipts.Rows[0]);

                    var employeeClockInInfoEntity = Helper.GetEmployeeClockInInfoEntity(sqlCommand, CashierID, DateCreated);

                    DataTable dtSumSaleReceiptPaymentDetails = new DataTable("SaleReceiptPaymentDetails");

                    if (dtTokenSaleReceiptPaymentDetails?.Rows.Count > 0)
                    {
                        foreach (DataRow item in dtTokenSaleReceiptPaymentDetails.Rows)
                        {
                        }

                        drs = dtTablesInDatabase.Select("TABLE_NAME = 'PaymentRoleDetails'", "");
                        if (drs.Length > 0)
                        {
                            sqlCommand.CommandText = $"select top 1 * from PaymentRoleDetails where SaleReceiptID = '{AddTipByTokenReceipt_MainSaleReceiptID}'";
                            reader = sqlCommand.ExecuteReader();
                            dtPaymentRoleDetails.Load(reader);
                            reader.Close();
                        }

                        scriptUpdate = string.Empty;
                        scriptUpdate += $"    update {Helper.BLOGICPOS7_Name}.dbo.SaleReceipts set TransactionStatusID = 'CLO' where SaleReceiptID = '{AddTipByTokenReceipt_TokenSaleReceiptID}';";
                        scriptUpdate += $"    update {Helper.BLOGICPOS7_Name}.dbo.SaleReceiptPaymentDetails set SVCFeeForGratuity = ROUND(AmountTendered * {POSConfig.Configuration.ConfigValue.CreditCardConfigValue.SVCFee} / 100, 2), Gratuity = AmountTendered, MerchantFeeForTip = MerchantFee where SaleReceiptID = '{AddTipByTokenReceipt_TokenSaleReceiptID}';";
                        scriptUpdate += $"    update {Helper.BLOGICPOS7_Name}.dbo.SaleReceiptPaymentDetails set SaleReceiptID = '{AddTipByTokenReceipt_MainSaleReceiptID}', AmountTendered = 0 where SaleReceiptID = '{AddTipByTokenReceipt_TokenSaleReceiptID}';";

                        sqlCommand.CommandText = scriptUpdate;
                        resultQueryUpdate = sqlCommand.ExecuteNonQuery();
                        if (resultQueryUpdate <= 0)
                        {
                            ErrorMessage = $"Something went wrong while Updating for the ticket.{Environment.NewLine}Please try contact TruongDS.";
                            BLogicLogger.Exception("UpdateTipByTokenReceipt", null, new System.Collections.Generic.List<object>() { scriptUpdate });
                            return;
                        }

                        sqlCommand.CommandText = $"SELECT ISNULL(SUM(SVCFeeAmount), 0) as SumSVCFeeAmount," +
                            $"ISNULL(SUM(SVCFeeForGratuity), 0) as SumSVCFeeForGratuity," +
                            $"ISNULL(SUM(MerchantFee), 0) as SumMerchantFee," +
                            $"ISNULL(SUM(MerchantFeeForTip), 0) as SumMerchantFeeForTip," +
                            $"ISNULL(SUM(Gratuity), 0) as SumGratuity " +
                            $"FROM SaleReceiptPaymentDetails WHERE SaleReceiptID = '{AddTipByTokenReceipt_MainSaleReceiptID}'";
                        reader = sqlCommand.ExecuteReader();
                        dtSumSaleReceiptPaymentDetails.Load(reader);
                        reader.Close();

                        scriptUpdate = string.Empty;
                        foreach (DataRow payment in dtTokenSaleReceiptPaymentDetails.Rows)
                        {
                            SaleReceiptPaymentDetailEntity paymentDetailEntity = SaleReceiptPaymentDetailEntity.FromDataRow(payment);

                            decimal svcFeeForGratuity = Math.Round(paymentDetailEntity.AmountTendered * POSConfig.Configuration.ConfigValue.CreditCardConfigValue.SVCFee / 100, 2);

                            scriptUpdate += $"    INSERT INTO PaymentRoleDetails (ID,SaleReceiptID,SaleReceiptPaymentDetailID,PaymentMethodID,PaymentMethodType,EmployeeID,RolesID,TipRate,TipRateOT,Gratuity,SVCFeeForGratuity,MerchantFeeForTip,DateCreated,PartyServiceFeeAmount,PartyServiceFeeTaxAmount,SVCFeeForPartyServiceFeeAmount,MerchantFeeForPartyServiceFeeAmount,AutoGratuityAmount,AutoGratuityTaxAmount,SVCFeeForAutoGratuityAmount,MerchantFeeForAutoGratuityAmount,AreaFeeAmount,AreaFeeTaxAmount,SVCFeeForAreaFeeAmount,MerchantFeeForAreaFeeAmount,ServiceFee,ServiceFeeTaxAmount,SVCFeeForServiceFee,MerchantFeeForServiceFee)";
                            scriptUpdate += $"    VALUES (NEWID(), '{AddTipByTokenReceipt_MainSaleReceiptID}', '{payment["SaleReceiptPaymentDetailID"]}', '{payment["PaymentMethodID"]}', '2', '{dtSaleReceipts.Rows[0]["CashierID"]}', '{employeeClockInInfoEntity.RolesID}', '{employeeClockInInfoEntity.TipRate}', '{employeeClockInInfoEntity.TipRateOT}', '{payment["AmountTendered"]}', '{svcFeeForGratuity}', '{payment["MerchantFee"]}', '{payment["CreatedDate"]}', '{payment["PartyServiceFeeAmount"]}', '{payment["PartyServiceFeeTaxAmount"]}', '{payment["SVCFeeForPartyServiceFeeAmount"]}', '{payment["MerchantFeeForPartyServiceFeeAmount"]}', '{payment["AutoGratuityAmount"]}', '{payment["AutoGratuityTaxAmount"]}', '{payment["SVCFeeForAutoGratuityAmount"]}', '{payment["MerchantFeeForAutoGratuityAmount"]}', '{payment["AreaFeeAmount"]}', '{payment["AreaFeeTaxAmount"]}', '{payment["SVCFeeForAreaFeeAmount"]}', '{payment["MerchantFeeForAreaFeeAmount"]}', '{payment["ServiceFee"]}', '{payment["ServiceFeeTaxAmount"]}', '{payment["SVCFeeForServiceFee"]}', '{payment["MerchantFeeForServiceFee"]}'  )";
                        }

                        sqlCommand.CommandText = scriptUpdate;
                        resultQueryUpdate = sqlCommand.ExecuteNonQuery();
                        if (resultQueryUpdate <= 0)
                        {
                            ErrorMessage = $"Something went wrong while Updating for the ticket.{Environment.NewLine}Please try contact TruongDS.";
                            BLogicLogger.Exception("UpdateTipByTokenReceipt", null, new System.Collections.Generic.List<object>() { scriptUpdate });
                            return;
                        }
                    }

                }

                DataTable dtTransactionDetail = new DataTable("TransactionDetail");
                using (SqlCommand sqlCommand = new SqlCommand())
                using (SqlConnection conn = new SqlConnection(Helper.Merchant_Connection))
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();


                    sqlCommand.Connection = conn;
                    sqlCommand.CommandType = CommandType.Text;

                    foreach (DataRow item in dtTokenSaleReceiptPaymentDetails.Rows)
                    {
                        scriptUpdate = $"     update TransactionDetail set Amount = 0, Gratuity = '{item["AmountTendered"]}', SubTotal= '0', TaxAmount = '0', TotalAmount = '0', TicketNumber = '{AddTipByTokenReceipt_MainSaleReceiptNumber}' where ID = '{item["CreditCardTransactionID"]}'";


                        sql = $"select * from TransactionDetail where BatchUnique in (select distinct a.BatchUnique from MERCHANT.dbo.TransactionDetail a where a.ID = '{item["CreditCardTransactionID"]}' and ISNULL(a.BatchUnique, '') <> '')";
                        sqlCommand.CommandText = sql;
                        SqlDataReader reader = sqlCommand.ExecuteReader();
                        dtTransactionDetail.Load(reader);
                        reader.Close();

                        if (dtTransactionDetail?.Rows.Count > 0)
                        {
                            scriptUpdate = $"     update Batch set TotalTips = TotalTips + '{item["AmountTendered"]}', TotalSales = TotalSales - '{item["AmountTendered"]}' where BatchUnique = (SELECT t.BatchUnique FROM TransactionDetail t where t.ID = '{item["CreditCardTransactionID"]}');";

                        }
                    }




                }
                //scriptUpdate = string.Empty;
                //foreach (DataRow item in dtTokenSaleReceiptPaymentDetails.Rows)
                //{
                //    reader = sqlCommand.ExecuteReader();
                //    dtSumSaleReceiptPaymentDetails.Load(reader);
                //    reader.Close();



                //    stringBuilder.AppendLine("");
                //    stringBuilder.AppendLine("USE MERCHANT");
                //    stringBuilder.AppendLine($"IF EXISTS (SELECT * FROM TransactionDetail where ID = '{item["CreditCardTransactionID"]}' and Gratuity = 0)");
                //    stringBuilder.AppendLine("BEGIN");
                //    stringBuilder.AppendLine($"END");
                //    stringBuilder.AppendLine($"GO");
                //}

                //sqlCommand.Connection = conn;
                //sqlCommand.CommandType = CommandType.Text;
                //sqlCommand.CommandText = stringBuilder.ToString();
                //var resultUpdate2 = sqlCommand.ExecuteNonQuery();

                //if (resultUpdate2 <= 0)
                //{
                //    BLogicLogger.Error($"Run Script Error: {stringBuilder.ToString()}");
                //    ErrorMessage = "Update Ticket Error. Please contact TruongDS";
                //}
                //else
                //{
                //    IsSuccess = true;
                //}
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Something went wrong while searching for the ticket.{Environment.NewLine}Please try again or contact TruongDS.";
                BLogicLogger.Exception("UpdateTipByTokenReceipt", ex, new System.Collections.Generic.List<object>() { AddTipByTokenReceipt_MainSaleReceiptID, AddTipByTokenReceipt_TokenSaleReceiptID });
            }
            finally
            {

            }
        }

        private void AddTipForPaymentDetail()
        {
            ConfirmDialogForm confirmFirmDialog = new ConfirmDialogForm(Const.ConfirmTitle, "Are you sure you want to update this receipt?");
            var result = FormHelper.ShowPopUpFormWithDIMBackground(confirmFirmDialog);
            if (result == DialogResult.No)
                return;

            try
            {
                string posCondition = string.Empty;
                string crudScriptPOS = string.Empty;

                string merchantCondition = string.Empty;
                string crudScriptMerchant = string.Empty;

                var txtScript = "";

                string creditCardTransactionID = string.Empty;

                decimal svcFeeForTip = 0;
                decimal merchantFeeForTip = 0;
                decimal merchantFeeForPayment = 0;

                decimal CashDiscount = 0;
                decimal SVCFee = 0;
                decimal BaseAmountFee = 0;
                decimal MerchantFee = 0;
                bool SVCFeeType = false;

                DataTable dtConfigurations = new DataTable("Configurations");
                DataTable dtSaleReceiptPaymentDetails = new DataTable("SaleReceiptPaymentDetails");
                DataTable dtTablesInDatabase = new DataTable("Tables");
                DataTable dtPaymentRoleDetails = new DataTable("PaymentRoleDetails");
                DataTable dtSaleReceipts = new DataTable("SaleReceipts");
                DataTable dtTransactionDetail = new DataTable("TransactionDetail");

                DataRow[] drs = null;
                StringBuilder stringBuilder = new StringBuilder();

                using (SqlCommand sqlCommand = new SqlCommand())
                using (SqlConnection conn = new SqlConnection(Helper.BLOGICPOS7_Connection))
                {
                    string sql = "select * from Configurations where ConfigurationName in (" +
                        "'CreditCardConfigValue.CashDiscount'," +
                        "'CreditCardConfigValue.SVCFee'," +
                        "'CreditCardConfigValue.SVCFeeType'," +
                        "'CreditCardConfigValue.MerchantFee'," +
                        "'CreditCardConfigValue.BaseAmountFee');";

                    sqlCommand.Connection = conn;
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.CommandText = sql;
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    dtConfigurations.Load(reader);
                    reader.Close();

                    foreach (DataRow item in dtConfigurations.Rows)
                    {
                        switch (Utility.GetStringFromRow("ConfigurationName", item))
                        {
                            case "CreditCardConfigValue.CashDiscount":
                                CashDiscount = Utility.GetDecimalFromObject(item["ConfigurationValue"]);
                                break;
                            case "CreditCardConfigValue.SVCFee":
                                SVCFee = Utility.GetDecimalFromObject(item["ConfigurationValue"]);
                                break;
                            case "CreditCardConfigValue.BaseAmountFee":
                                BaseAmountFee = Utility.GetDecimalFromObject(item["ConfigurationValue"]);
                                break;
                            case "CreditCardConfigValue.MerchantFee":
                                MerchantFee = Utility.GetDecimalFromObject(item["ConfigurationValue"]);
                                break;
                            case "CreditCardConfigValue.SVCFeeType":
                                SVCFeeType = Utility.GetStringFromRow("ConfigurationValue", item) == "1";
                                break;
                        }
                    }

                    if (SVCFee > 0 && BaseAmountFee == 0)
                    {
                        BaseAmountFee = MerchantFee;
                    }

                    sqlCommand.CommandText = $"select * from SaleReceipts where SaleReceiptID = '{AddTipForPaymentDetail_SaleReceiptID}'";
                    reader = sqlCommand.ExecuteReader();
                    dtSaleReceipts.Load(reader);
                    reader.Close();

                    sql = $"select s.CashierID as EmployeeID, s.SaleReceiptNumber, sp.*" +
                        $" from SaleReceiptPaymentDetails sp left join SaleReceipts s on sp.SaleReceiptID = s.SaleReceiptID" +
                        $" where sp.SaleReceiptPaymentDetailID = '{AddTipForPaymentDetail_SaleReceiptPaymentDetailID}'";

                    sqlCommand.CommandText = sql;
                    reader = sqlCommand.ExecuteReader();
                    dtSaleReceiptPaymentDetails.Load(reader);
                    reader.Close();

                    if (dtSaleReceiptPaymentDetails?.Rows.Count > 0)
                    {
                        var paymentEntity = SaleReceiptPaymentDetailEntity.FromDataRow(dtSaleReceiptPaymentDetails.Rows[0]);

                        DataRow payment = dtSaleReceiptPaymentDetails.Rows[0];
                        drs = dtTablesInDatabase.Select("TABLE_NAME = 'PaymentRoleDetails'", "");
                        if (drs.Length > 0)
                        {
                            sqlCommand.CommandText = $"select top 1 * from PaymentRoleDetails where SaleReceiptID = '{AddTipForPaymentDetail_SaleReceiptID}'";
                            reader = sqlCommand.ExecuteReader();
                            dtPaymentRoleDetails.Load(reader);
                            reader.Close();
                        }

                        decimal amountTendered = Utility.GetDecimalFromRow("AmountTendered", dtSaleReceiptPaymentDetails.Rows[0]);
                        decimal employeeID = Utility.GetIntFromRow("EmployeeID", dtSaleReceiptPaymentDetails.Rows[0]);
                        DateTime createdDate = Utility.GetDateTimeFromRow("CreatedDate", dtSaleReceiptPaymentDetails.Rows[0]);
                        svcFeeForTip = Utility.CalculateSVCFeeAdjustTipWithPara(AddTipForPaymentDetail_Gratuity, SVCFee, BaseAmountFee);
                        merchantFeeForTip = Utility.CalculateMerchantFeeForTipWithParams(AddTipForPaymentDetail_Gratuity, SVCFee, MerchantFee, SVCFeeType);
                        merchantFeeForPayment = Utility.CalculateMerchantFeeWithParams(amountTendered + AddTipForPaymentDetail_Gratuity, SVCFee, MerchantFee, SVCFeeType);
                        decimal merchantFeeAmount = Utility.CalculateMerchantFeeForTipWithParams(paymentEntity.AmountTendered - paymentEntity.AmountReturn + AddTipForPaymentDetail_Gratuity, SVCFee, MerchantFee, SVCFeeType);

                        var employeeClockInInfoEntity = Helper.GetEmployeeClockInInfoEntity(sqlCommand, employeeID, createdDate);

                        stringBuilder.AppendLine($"    ");
                        sqlCommand.CommandText = $"update {Helper.BLOGICPOS7_Name}.dbo.SaleReceiptPaymentDetails set Gratuity = '{AddTipForPaymentDetail_Gratuity}', SVCFeeForGratuity = '{svcFeeForTip}', MerchantFeeForTip = '{merchantFeeForTip}', MerchantFee = '{merchantFeeAmount}' where SaleReceiptPaymentDetailID = '{paymentEntity.SaleReceiptPaymentDetailID}';";
                        var resultUpdate = sqlCommand.ExecuteNonQuery();
                        if (resultUpdate <= 0)
                        {
                            ErrorMessage = "Update Ticket Error. Please contact TruongDS";
                            return;
                        }

                        var totalGratuityOfTicket = Utility.GetDecimalFromObject(sqlCommand.ExecuteScalar());

                        if (dtPaymentRoleDetails?.Rows.Count > 0)
                        {
                            stringBuilder.AppendLine($"    update PaymentRoleDetails set Gratuity = '{totalGratuityOfTicket}' , SVCFeeForGratuity = SVCFeeForGratuity + '{payment["SVCFeeAmount"]}' , MerchantFeeForTip = MerchantFeeForTip + '{payment["MerchantFee"]}' where ID = '{dtPaymentRoleDetails.Rows[0]["ID"]}';");
                        }
                        else
                        {
                            stringBuilder.AppendLine($"    INSERT INTO PaymentRoleDetails (ID,SaleReceiptID,SaleReceiptPaymentDetailID,PaymentMethodID,PaymentMethodType,EmployeeID,RolesID,TipRate,TipRateOT,Gratuity,SVCFeeForGratuity,MerchantFeeForTip,DateCreated,PartyServiceFeeAmount,PartyServiceFeeTaxAmount,SVCFeeForPartyServiceFeeAmount,MerchantFeeForPartyServiceFeeAmount,AutoGratuityAmount,AutoGratuityTaxAmount,SVCFeeForAutoGratuityAmount,MerchantFeeForAutoGratuityAmount,AreaFeeAmount,AreaFeeTaxAmount,SVCFeeForAreaFeeAmount,MerchantFeeForAreaFeeAmount,ServiceFee,ServiceFeeTaxAmount,SVCFeeForServiceFee,MerchantFeeForServiceFee)");
                            stringBuilder.AppendLine($"    VALUES (NEWID(), '{AddTipByTokenReceipt_MainSaleReceiptID}', '{payment["SaleReceiptPaymentDetailID"]}', '{payment["PaymentMethodID"]}', '2', '{dtSaleReceipts.Rows[0]["CashierID"]}', '{employeeClockInInfoEntity.RolesID}', '{employeeClockInInfoEntity.TipRate}', '{employeeClockInInfoEntity.TipRateOT}', '{payment["AmountTendered"]}', '{payment["SVCFeeAmount"]}', '{payment["MerchantFee"]}', '{payment["CreatedDate"]}', '{payment["PartyServiceFeeAmount"]}', '{payment["PartyServiceFeeTaxAmount"]}', '{payment["SVCFeeForPartyServiceFeeAmount"]}', '{payment["MerchantFeeForPartyServiceFeeAmount"]}', '{payment["AutoGratuityAmount"]}', '{payment["AutoGratuityTaxAmount"]}', '{payment["SVCFeeForAutoGratuityAmount"]}', '{payment["MerchantFeeForAutoGratuityAmount"]}', '{payment["AreaFeeAmount"]}', '{payment["AreaFeeTaxAmount"]}', '{payment["SVCFeeForAreaFeeAmount"]}', '{payment["MerchantFeeForAreaFeeAmount"]}', '{payment["ServiceFee"]}', '{payment["ServiceFeeTaxAmount"]}', '{payment["SVCFeeForServiceFee"]}', '{payment["MerchantFeeForServiceFee"]}'  )");
                        }

                        stringBuilder.AppendLine($"END");
                        stringBuilder.AppendLine($"GO");

                        creditCardTransactionID = Utility.GetStringFromRow("CreditCardTransactionID", dtSaleReceiptPaymentDetails.Rows[0]);
                    }

                }

                if (string.IsNullOrWhiteSpace(creditCardTransactionID))
                    return;

                using (SqlCommand sqlCommand = new SqlCommand())
                using (var conn = new SqlConnection(Helper.Merchant_Connection))
                {
                    var sql = $"select * from TransactionDetail where ID = '{creditCardTransactionID}' ";

                    sqlCommand.Connection = conn;
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.CommandText = sql;
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    dtTransactionDetail.Load(reader);
                    reader.Close();

                    merchantCondition = $" SELECT * FROM TransactionDetail where ID = '{creditCardTransactionID}' and Gratuity = 0 ";

                    crudScriptMerchant += $" update TransactionDetail set Gratuity = '{AddTipForPaymentDetail_Gratuity}', Total = Total + {AddTipForPaymentDetail_Gratuity} where ID = '{creditCardTransactionID}';{Environment.NewLine}";


                    stringBuilder.AppendLine($"");
                    stringBuilder.AppendLine($"USE MERCHANT");
                    stringBuilder.AppendLine($"IF EXISTS (SELECT * FROM TransactionDetail where ID = '{creditCardTransactionID}' and Gratuity = 0 and BatchStatus = 'Closed')");
                    stringBuilder.AppendLine($"BEGIN");
                    stringBuilder.AppendLine($"    update Batch set TotalTips = TotalTips + {AddTipForPaymentDetail_Gratuity}, DepositAmount = DepositAmount + {AddTipForPaymentDetail_Gratuity}, Total = Total + {AddTipForPaymentDetail_Gratuity}, TotalSVCFeeForTip = TotalSVCFeeForTip + '{svcFeeForTip}', MerchantFeeAmount = MerchantFeeAmount + '{merchantFeeForTip}' where BatchUnique = (SELECT t.BatchUnique FROM TransactionDetail t where t.ID = '{creditCardTransactionID}');");
                    stringBuilder.AppendLine($"    update TransactionDetail set BatchTotal = BatchTotal + {AddTipForPaymentDetail_Gratuity} where BatchUnique = (SELECT t.BatchUnique FROM TransactionDetail t where t.ID = '{creditCardTransactionID}') ;");
                    stringBuilder.AppendLine($"END");
                    stringBuilder.AppendLine($"GO");

                    stringBuilder.AppendLine($"");
                    stringBuilder.AppendLine($"USE MERCHANT");
                    stringBuilder.AppendLine($"IF EXISTS (SELECT * FROM TransactionDetail where ID = '{creditCardTransactionID}' and Gratuity = 0)");
                    stringBuilder.AppendLine($"BEGIN");
                    stringBuilder.AppendLine($"    update TransactionDetail set Gratuity = '{AddTipForPaymentDetail_Gratuity}', Total = Total + {AddTipForPaymentDetail_Gratuity}, SVCFeeForGratuity = '{svcFeeForTip}', MerchantFeeForTip = '{merchantFeeForTip}', MerchantFee = MerchantFee + '{merchantFeeForTip}' where ID = '{creditCardTransactionID}';");
                    stringBuilder.AppendLine($"END");
                    stringBuilder.AppendLine($"GO");

                }

            }
            catch (Exception ex)
            {
                BLogicLogger.Error($"BtnGetScript_Click in UpdateTipForSaleReceiptFile_0_0 error with:  {ex.Message}", ex);
            }
        }

        private void ProcessSettleBatchOffline()
        {

            try
            {
                CSetting.Init();

                DataTable dtTransactionDetail = new DataTable("TransactionDetail");
                DataTable dtGroupSettleBatch = new DataTable("TransactionDetail");

                using (SqlCommand sqlCommand = new SqlCommand())
                using (SqlConnection conn = new SqlConnection(Helper.Merchant_Connection))
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();

                    string sql = "select distinct MerchantID, BatchID,WorkstationID from TransactionDetail where ISNULL(BatchUnique,'') = '';";

                    sqlCommand.Connection = conn;
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.CommandText = sql;
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    dtGroupSettleBatch.Load(reader);
                    reader.Close();
                }

                foreach (DataRow item in dtGroupSettleBatch.Rows)
                {
                    dtTransactionDetail.Rows.Clear();
                    using (SqlCommand sqlCommand = new SqlCommand())
                    using (SqlConnection conn = new SqlConnection(Helper.Merchant_Connection))
                    {
                        if (conn.State == ConnectionState.Closed)
                            conn.Open();

                        string sql = $"select * from TransactionDetail where MerchantID = '{item["MerchantID"]}' and BatchID = '{item["BatchID"]}' and WorkstationID = '{item["WorkstationID"]}';";

                        sqlCommand.Connection = conn;
                        sqlCommand.CommandType = CommandType.Text;
                        sqlCommand.CommandText = sql;
                        SqlDataReader reader = sqlCommand.ExecuteReader();
                        dtTransactionDetail.Load(reader);
                        reader.Close();
                    }

                    object[] objResult = this.AddBatch(dtTransactionDetail);

                    bool sIsResult = false;
                    string sBatchUnique = string.Empty;
                    decimal sTotalMerchantFee = 0;
                    string stransID = string.Empty;
                    decimal sRemainingMerchantFee = 0;
                    bool sIsReCaculateMerchantFee = false;
                    if (objResult.Length >= 6)
                    {
                        sIsResult = Convert.ToBoolean(objResult[0]);
                        sBatchUnique = objResult[1].ToString();
                        sTotalMerchantFee = Convert.ToDecimal(objResult[2]);
                        stransID = objResult[3].ToString();
                        sRemainingMerchantFee = Convert.ToDecimal(objResult[4]);
                        sIsReCaculateMerchantFee = Convert.ToBoolean(objResult[5]);
                    }

                    ProcessBatchCloseLocal(batch.BatchID, batch.Total, batch.BatchUnique, batch.MerchantID);

                    ReCalculateMerchantFeeTransactionDetailByBatch(sBatchUnique, sIsReCaculateMerchantFee, sTotalMerchantFee, stransID, sRemainingMerchantFee);
                }

                IsSuccess = true;
            }
            catch (Exception ex)
            {
                BLogicLogger.Exception("ProcessSettleBatchOffline", ex);
            }
        }

        public object[] AddBatch(DataTable dtTransactionDetail) //public bool AddBatch()
        {
            batch = new BatchStatus();
            string sBatchUnique = string.Empty;
            string stransID = string.Empty;
            decimal sRemainingMerchantFee = 0;
            bool sIsReCaculateMerchantFee = false;
            decimal dTotalMerchantFee = 0;
            try
            {
                int iCount = 0;
                decimal dTotal = 0;
                decimal dEBT_FoodAmount = 0;
                decimal dSVCFeeAmount = 0;
                decimal dSVCFeeForTip = 0;
                decimal dTotalSales = 0;
                decimal dTotalTips = 0;
                decimal dTotalReturns = 0;
                string sWorkstationName = string.Empty;
                //int iOrderID = 0;

                decimal dSVCFeeAmountForRefund = 0;
                //decimal sCalMerchantFeeForTip = 0;
                List<decimal> lstMerchantFeePercent = new List<decimal>();
                List<string> lstSVCFeeType = new List<string>();
                decimal tempTotalMerchantFeeAmount_Current = 0;

                if (dtTransactionDetail?.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtTransactionDetail.Rows)
                    {
                        string result = dr["Result"].ToString();
                        string type = dr["TranType"].ToString();
                        string sMerchantID = dr["MerchantID"].ToString();
                        decimal subTotal = Convert.ToDecimal(dr["Total"].ToString());
                        decimal subAmount = Convert.ToDecimal(dr["Amount"].ToString());
                        decimal subSVCFeeAmount = Convert.ToDecimal(dr["SVCFee"].ToString());
                        decimal subSVCFeeForTip = Convert.ToDecimal(dr["SVCFeeForGratuity"].ToString());
                        decimal subReturn = Convert.ToDecimal(dr["Amount"].ToString());
                        decimal subTip = Convert.ToDecimal(dr["Gratuity"].ToString());
                        string tranStatus = dr["TranStatus"].ToString();
                        decimal subMerchantFee = Convert.ToDecimal(dr["MerchantFee"].ToString());

                        string dSVCFeeType = dr["SVCFeeType"].ToString();
                        decimal dMerchantFeePercent = Convert.ToDecimal(dr["MerchantFeePercent"].ToString());

                        //if (dr["OrderID"] != null && !string.IsNullOrEmpty(dr["OrderID"].ToString()))
                        //{
                        //    int.TryParse(dr["OrderID"].ToString(), out iOrderID);
                        //}
                        // && iOrderID < 9001 && iOrderID < 9999

                        if (result == "Approved")
                        {
                            if (!lstMerchantFeePercent.Contains(dMerchantFeePercent))
                                lstMerchantFeePercent.Add(dMerchantFeePercent);
                            if (!lstSVCFeeType.Contains(dSVCFeeType))
                                lstSVCFeeType.Add(dSVCFeeType);

                            if (string.IsNullOrEmpty(batch.BatchID))
                            {
                                batch.BatchID = dr["BatchID"].ToString();
                                batch.MerchantID = dr["MerchantID"].ToString();
                            }
                            iCount++;
                            if (!string.IsNullOrEmpty(dr["WorkstationName"].ToString()))
                                sWorkstationName = dr["WorkstationName"].ToString();

                            if (type == "Sale" || type == "PreAuthCapture")
                            {
                                if (tranStatus == "ACTIVE")
                                {
                                    dTotal += subTotal;
                                    dTotalSales += (subAmount - subSVCFeeAmount);
                                    dTotalTips += subTip;
                                    dSVCFeeAmount += subSVCFeeAmount;
                                    dSVCFeeForTip += subSVCFeeForTip;
                                    dTotalMerchantFee += subMerchantFee;

                                    tempTotalMerchantFeeAmount_Current += POSConfig.Configuration.ConfigValue.CreditCardConfigValue.MerchantFee;
                                    //sCalMerchantFeeForTip += CHelper.CalculateMerchantFeeForTip(subTip);
                                    if (dr["TenderType"].ToString() == "EBT_Food")
                                    {
                                        dEBT_FoodAmount += subAmount;
                                    }
                                }

                            }
                            else if (type == "Return")
                            {
                                dSVCFeeAmountForRefund += subSVCFeeAmount;
                                dTotal -= subTotal;
                                dTotalReturns += subReturn;
                                dSVCFeeAmount -= subSVCFeeAmount;
                            }
                        }
                    }

                    batch.TotalReturns = dTotalReturns - dSVCFeeAmountForRefund;
                    batch.TotalSales = dTotalSales;
                    batch.TotalTips = dTotalTips;
                    batch.WorkStationName = sWorkstationName;
                    batch.TransactionCount = iCount;
                    batch.Total = dTotal;
                    batch.MerchantFeePercent = POSConfig.Configuration.ConfigValue.CreditCardConfigValue.MerchantFee;
                    //batch.MerchantFeeAmount = CHelper.CalculateMerchantFee(dTotal + dTotalReturns);
                    if (POSConfig.Configuration.ConfigValue.CreditCardConfigValue.SVCFeeType == 1)
                        batch.MerchantFeeAmount = CHelper.CalculateMerchantFee(dTotal + dTotalReturns); //- dEBT_FoodAmount // edit by tuanph.2023.03.30
                    else
                    {
                        dTotalMerchantFee = tempTotalMerchantFeeAmount_Current;
                        batch.MerchantFeeAmount = dTotalMerchantFee;
                    }

                    batch.DepositAmount = dTotal - batch.MerchantFeeAmount;
                    batch.TotalSVCFeeAmount = dSVCFeeAmount;
                    batch.TotalSVCFeeForTip = dSVCFeeForTip;


                    sRemainingMerchantFee = batch.MerchantFeeAmount - dTotalMerchantFee;
                    if (sRemainingMerchantFee != 0)
                    {
                        DataRow[] drsID = dtTransactionDetail.Select(" (TranType='Sale' OR TranType='PreAuthCapture') AND TranStatus='ACTIVE' AND MerchantFee >= " + Math.Abs(sRemainingMerchantFee) + "", "Date DESC");
                        if (drsID.Length > 0)
                            stransID = Convert.ToString(drsID[0]["ID"]);
                    }
                }
                if (iCount == 0)
                {
                    return new object[] { true, string.Empty, 0, string.Empty, 0, false }; //return true;
                }

                DateTime minDate = Utility.GetDateTimeFromObject(dtTransactionDetail.Compute("MAX(Date)", ""));

                batch.Status = "Closed";
                batch.BatchUnique = Guid.NewGuid().ToString();

                DataSet dsResult = new DataSet();
                DataSet dsParams = new DataSet();
                DataTable dtbatch = CModel.Convert.BatchToDataTable(batch, batch.MerchantID);
                dtbatch.Rows[0]["Date"] = minDate;

                dsParams.Tables.Add(dtbatch);
                BusinessServiceClientModel.Helper.Parameters.SetService(BusinessServiceClientModel.Helper.ServiceNames.DATABASE_SERVICE, dsParams);
                BusinessServiceClientModel.Helper.Parameters.SetAction(BusinessServiceClientModel.Helper.Actions.ADD_BATCH_ACTION, dsParams);
                dsResult = CFacade.DoAction(dsParams);

                sIsReCaculateMerchantFee = false;
                if (lstMerchantFeePercent != null)
                {
                    if (lstMerchantFeePercent.Count > 1)
                        sIsReCaculateMerchantFee = true;
                    else if (lstMerchantFeePercent.Count == 1)
                        sIsReCaculateMerchantFee = Convert.ToDecimal(lstMerchantFeePercent[0]) != POSConfig.Configuration.ConfigValue.CreditCardConfigValue.MerchantFee;
                }
                if (lstSVCFeeType != null && !sIsReCaculateMerchantFee)
                {
                    if (lstSVCFeeType.Count > 1)
                        sIsReCaculateMerchantFee = true;
                    else if (lstSVCFeeType.Count == 1)
                        sIsReCaculateMerchantFee = Convert.ToString(lstSVCFeeType[0]) != POSConfig.Configuration.ConfigValue.CreditCardConfigValue.SVCFeeType.ToString();
                }
                sBatchUnique = batch.BatchUnique;

            }
            catch (Exception ex)
            {
                return new object[] { false, string.Empty, 0, string.Empty, 0, false };
            }
            return new object[] { true, sBatchUnique, dTotalMerchantFee, stransID, sRemainingMerchantFee, sIsReCaculateMerchantFee };
        }

        public void ProcessBatchCloseLocal(object BatchID, object BatchTotal, object BatchUnique, object MerchantID)
        {
            try
            {
                using (SqlCommand sqlCommand = new SqlCommand())
                using (SqlConnection conn = new SqlConnection(Helper.Merchant_Connection))
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();

                    string sql = $"UPDATE dbo.transactiondetail WITH (ROWLOCK) " +
                        $"	SET " +
                        $"		BatchStatus = 'Closed'," +
                        $"		BatchTotal='{BatchTotal}'," +
                        $"		BatchUnique='{BatchUnique}'" +
                        $"	WHERE MerchantID = '{MerchantID}' and BatchID = '{BatchID}'" +
                        $"	AND TenderType!='GiftCard'";

                    sqlCommand.Connection = conn;
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.CommandText = sql;
                    var result = sqlCommand.ExecuteNonQuery();

                    if (result <= 0)
                    {
                        BLogicLogger.Error($"ProcessBatchCloseLocal error with:  {sql}");
                    }
                }
            }
            catch (Exception ex)
            {
                // log exception
                BLogicLogger.Exception("ProcessBatchCloseLocal", ex);
            }
        }

        public void ReCalculateMerchantFeeTransactionDetailByBatch(string sBatchUnique, bool IsReCaculateMerchantFee, decimal dTotalMerchantFee, string stransID, decimal dRemainingMerchantFee)
        {
            if (!string.IsNullOrEmpty(sBatchUnique) && POSConfig.Configuration.ConfigValue.CreditCardConfigValue.MerchantFee == 0 && dTotalMerchantFee != 0)
            {
                UpdateMerchantFeeAmountByMerchantFeePercent(sBatchUnique, 0, 0, POSConfig.Configuration.ConfigValue.CreditCardConfigValue.SVCFeeType.ToString());
            }
            else
            {
                if (!string.IsNullOrEmpty(sBatchUnique) && ((POSConfig.Configuration.ConfigValue.CreditCardConfigValue.MerchantFee != 0 && dTotalMerchantFee == 0) || IsReCaculateMerchantFee))
                    UpdateMerchantFeeAmountByMerchantFeePercent(sBatchUnique, POSConfig.Configuration.ConfigValue.CreditCardConfigValue.MerchantFee, POSConfig.Configuration.ConfigValue.CreditCardConfigValue.BaseAmountFee, POSConfig.Configuration.ConfigValue.CreditCardConfigValue.SVCFeeType.ToString());
                else
                    if (!string.IsNullOrEmpty(stransID))
                    UpdateMerchantFeeAmountByTransID(stransID, dRemainingMerchantFee);
            }
        }

        public DataSet UpdateMerchantFeeAmountByMerchantFeePercent(string dBatchUnique, decimal dCurrentMerchantFeePercent, decimal dBaseAmountFee, string dSVCFeeType)
        {
            DataSet dsResult = new DataSet();
            DataSet dsParams = new DataSet();
            DataTable dt = new DataTable("TransactionDetail");
            dt.Columns.Add("BatchUnique", typeof(string));
            dt.Columns.Add("MerchantFeePercent", typeof(decimal));
            dt.Columns.Add("BaseAmountFee", typeof(decimal));
            dt.Columns.Add("SVCFeeType", typeof(string));
            dt.Rows.Add(dBatchUnique, dCurrentMerchantFeePercent, dBaseAmountFee, dSVCFeeType);
            dsParams.Tables.Add(dt);
            BusinessServiceClientModel.Helper.Parameters.SetService(BusinessServiceClientModel.Helper.ServiceNames.DATABASE_SERVICE, dsParams);
            BusinessServiceClientModel.Helper.Parameters.SetAction(BusinessServiceClientModel.Helper.Actions.RE_CALCULATE_MERCHANT_FEE_FOR_TRANSACTIONS_BY_BATCH, dsParams);
            dsResult = CFacade.DoAction(dsParams);
            return dsResult;
        }

        public DataSet UpdateMerchantFeeAmountByTransID(string dTransID, decimal dMerchantFee)
        {
            DataSet dsResult = new DataSet();
            DataSet dsParams = new DataSet();
            DataTable dt = new DataTable("TransactionDetail");
            dt.Columns.Add("ID", typeof(string));
            dt.Columns.Add("MerchantFee", typeof(decimal));
            dt.Rows.Add(dTransID, dMerchantFee);
            dsParams.Tables.Add(dt);
            BusinessServiceClientModel.Helper.Parameters.SetService(BusinessServiceClientModel.Helper.ServiceNames.DATABASE_SERVICE, dsParams);
            BusinessServiceClientModel.Helper.Parameters.SetAction(BusinessServiceClientModel.Helper.Actions.UPDATE_MERCHANT_FEE_FOR_TRANSACTIONDETAIL_BY_TRANS_ID, dsParams);
            dsResult = CFacade.DoAction(dsParams);
            return dsResult;
        }
    }
}
