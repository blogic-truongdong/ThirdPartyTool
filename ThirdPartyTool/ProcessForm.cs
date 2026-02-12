using BLogicSystems.Merchant.PAXClient;
using BLogicSystems.Merchant.PAXTerminalHelper;
using BLogicSystems.XPos.DTO;
using BLogicSystems.XPos.SharedModule.Models;
using BLogicSystems.XPos.Utilites;
using BLogicSystems.XPos.Utilites.Logging;
using BLogicSystems.XPos.XPOSReports;
using Dapper;
using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
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
        SettleBatchOffline = 4,
        EmployeeView_GetEmployeeInfos = 5,
        GetRemainingBatch = 6,
        ProcessSyncRoleForEmployee = 7,
        CloseReceipt = 8,
        HotFix_UpgradeTaxForTicket = 9,
        HotFix_UpdownTaxForTicket = 10,
        ReSyncItemType,
        EmployeePaycor_GetEmployees,
        EmployeePaycor_GetEmployeesFromPaycor,
        SaleReceipt_RefundFullAmount,
        GetReceiptTemplateBySaleReceiptID,
        VoidReceipt,
        CancelReceipt,
        RefundTicket,
        SettleBatchOfflineByMerchantID,
        PartialRefundItems,
        UpdateServiceChargeBySaleReceiptID,
    }
    public partial class ProcessForm : Form
    {
        private bool ExistFinalGratuityInSaleReceipts = false;
        private bool ExistSVCFeeTaxAmountInSaleReceipts = false;
        private bool ExistServiceFeeTaxAmountInSaleReceipts = false;

        private DataTable CurrentTableInDatabaseBLogicPOS7 = null;
        public DataTable DataTableReturn = null;
        public DataSet DataSetReturn = null;
        BatchStatus batch = new BatchStatus();
        public decimal GetReceiptTemplateByTicketNumber_TotalAmount = 0;
        public bool GetReceiptTemplateByTicketNumber_Include_VOID_CLOSE_REFUND_OPER = false;
        public XtraReport ReceiptTemplate = null;
        private BackgroundWorker backgroundWorker;
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

        public ProcessForm()
        {
            InitializeComponent();

            backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += BackgroundWorker_DoWork;
            backgroundWorker.RunWorkerCompleted += BackgroundWorker_RunWorkerCompleted;

            this.Load += ProcessForm_Load;
        }

        public void InitGetReceiptTemplate(string ticketNumber, bool allStatus = false)
        {
            CurrentProcessName = ProcessNameEnum.GetReceiptTemplateByTicketNumber;
            this.SaleReceiptNumber = ticketNumber;
        }

        public void InitAddTipByTokenReceipt(string saleReceiptID, string saleReceiptNumber, string tokenSaleReceiptID, string tokenSaleReceiptNumber)
        {
            CurrentProcessName = ProcessNameEnum.AddTipByTokenReceipt;
            this.SaleReceiptID = saleReceiptID;
            this.SaleReceiptNumber = saleReceiptNumber;
            this.TokenSaleReceiptID = tokenSaleReceiptID;
            this.TokenSaleReceiptNumber = tokenSaleReceiptNumber;
        }

        public void InitGetRemainingBatch()
        {
            CurrentProcessName = ProcessNameEnum.GetRemainingBatch;
        }

        public void InitSettleBatchOffline()
        {
            CurrentProcessName = ProcessNameEnum.SettleBatchOffline;
        }

        public void InitAddTipForPaymentDetail(string saleReceiptID, string saleReceiptPaymentDetailID, decimal gratuity)
        {
            CurrentProcessName = ProcessNameEnum.AddTipForPaymentDetail;
            this.SaleReceiptID = saleReceiptID;
            this.SaleReceiptPaymentDetailID = saleReceiptPaymentDetailID;
            this.AddTipForPaymentDetail_Gratuity = gratuity;
        }

        private void ProcessForm_Load(object sender, EventArgs e)
        {
            backgroundWorker.RunWorkerAsync();
        }

        private async void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {

                GetListTableInDatabaseBLogicPOS7();

                switch (CurrentProcessName)
                {
                    case ProcessNameEnum.UpdateServiceChargeBySaleReceiptID:
                        await UpdateServiceChargeBySaleReceiptID();
                        break;
                    case ProcessNameEnum.PartialRefundItems:
                        PartialRefundItems();
                        break;
                    case ProcessNameEnum.CancelReceipt:
                        CancelReceipt();
                        break;
                    case ProcessNameEnum.VoidReceipt:
                        VoidReceipt();
                        break;
                    case ProcessNameEnum.GetReceiptTemplateBySaleReceiptID:
                        GetReceiptTemplateBySaleReceiptID(SaleReceiptID);
                        break;
                    case ProcessNameEnum.SaleReceipt_RefundFullAmount:
                        RefundFullAmount();
                        break;
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
                    case ProcessNameEnum.SettleBatchOfflineByMerchantID:
                        SettleBatchOfflineByMerchantID(MerchantID, BatchID);
                        break;
                    case ProcessNameEnum.EmployeeView_GetEmployeeInfos:
                        GetEmployeeInfos();
                        break;
                    case ProcessNameEnum.GetRemainingBatch:
                        GetRemainingBatch();
                        break;
                    case ProcessNameEnum.ProcessSyncRoleForEmployee:
                        ProcessSyncRoleForEmployee();
                        break;
                    case ProcessNameEnum.CloseReceipt:
                        CloseReceipt();
                        break;
                    case ProcessNameEnum.HotFix_UpgradeTaxForTicket:
                        HotFix_UpgradeTaxForTicket_v3();
                        break;
                    case ProcessNameEnum.HotFix_UpdownTaxForTicket:
                        HotFix_UpdownTaxForTicket();
                        break;
                    case ProcessNameEnum.ReSyncItemType:
                        ReSyncItemType();
                        break;
                    case ProcessNameEnum.EmployeePaycor_GetEmployees:
                        EmployeePaycor_GetEmployees();
                        break;
                    case ProcessNameEnum.EmployeePaycor_GetEmployeesFromPaycor:
                        EmployeePaycor_GetEmployeesFromPaycor();
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
                GetReceiptTemplateByTicketNumber_Include_VOID_CLOSE_REFUND_OPER = false;

                if (!IsSuccess)
                {
                    this.Close();
                    return;
                }

                string message = string.Empty;
                switch (CurrentProcessName)
                {
                    case ProcessNameEnum.AddTipByTokenReceipt:
                        message = $"Update Tip Ticket# {SaleReceiptNumber} done. Refresh Now";
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
                BLogicLogger.Exception("BackgroundWorker_RunWorkerCompleted", ex, new System.Collections.Generic.List<object>() { CurrentProcessName });
            }
        }

        public void ProcessSyncRoleForEmployee()
        {
            try
            {
                DataTable dtEmployee = new DataTable("Employees");
                DataTable dtRoles = new DataTable("Roles");

                using (SqlCommand sqlCommand = new SqlCommand())
                using (SqlConnection conn = new SqlConnection(Helper.BLOGICPOS7_Connection))
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    sqlCommand.Connection = conn;
                    sqlCommand.CommandType = CommandType.Text;

                    int employeeID = EmployeeView_EmployeeID;
                    int roleID = EmployeeView_RolesID;
                    DateTime startDate = EmployeeView_StartDate;
                    DateTime endDate = EmployeeView_EndDate;

                    sqlCommand.CommandText = $"Select * from Employees where EmployeeID = '{employeeID}'";
                    var reader = sqlCommand.ExecuteReader();
                    dtEmployee.Load(reader);
                    reader.Close();

                    sqlCommand.CommandText = $"Select * from Roles where RolesID = '{roleID}'";
                    reader = sqlCommand.ExecuteReader();
                    dtRoles.Load(reader);
                    reader.Close();

                    DataRow employeeRow = dtEmployee.Rows[0];

                    bool IsHourlyWage = Utility.GetBoolFromObject(employeeRow["IsHourlyWage"]);
                    decimal dHourlyWage = Utility.GetDecimalFromObject(employeeRow["HourlyWage"]);
                    decimal dHourlyWageOT = Utility.GetDecimalFromObject(employeeRow["HourlyWageOT"]);
                    bool bIsDailyOverTime = Utility.GetBoolFromObject(employeeRow["IsDailyOverTime"]);
                    bool isUsingRoleWage = Utility.GetBoolFromObject(employeeRow["IsUsingRoleWage"]);

                    bool isUsingRoleTipPool = Utility.GetBoolFromObject(employeeRow["IsUsingRoleTipPool"]);
                    decimal dTipRate = Utility.GetDecimalFromObject(employeeRow["TipRate"]);
                    decimal dTipRateOT = Utility.GetDecimalFromObject(employeeRow["TipRateOT"]);
                    decimal dTrainingTipRate = Utility.GetDecimalFromObject(employeeRow["TrainingTipRate"]);
                    decimal dTrainingTipRateOT = Utility.GetDecimalFromObject(employeeRow["TrainingTipRateOT"]);
                    decimal dMaxTipHour = Utility.GetDecimalFromObject(employeeRow["MaxTipHour"]);
                    decimal dMaxTipHourOT = Utility.GetDecimalFromObject(employeeRow["MaxTipHourOT"]);
                    decimal dHourlyWageOT2nd = Utility.GetDecimalFromObject(employeeRow["HourlyWageOT2nd"]);
                    bool bIsDailyOverTime2nd = Utility.GetBoolFromObject(employeeRow["IsDailyOverTime2nd"]);

                    decimal dServiceChargesRate = Utility.GetDecimalFromObject(employeeRow["ServiceChargesRate"]);
                    decimal dServiceChargesRateOT = Utility.GetDecimalFromObject(employeeRow["ServiceChargesRateOT"]);
                    decimal dServiceChargesMaxHour = Utility.GetDecimalFromObject(employeeRow["ServiceChargesMaxHour"]);
                    decimal dServiceChargesMaxHourOT = Utility.GetDecimalFromObject(employeeRow["ServiceChargesMaxHourOT"]);
                    bool dEnableTrainingMode = Utility.GetBoolFromObject(employeeRow["EnableTrainingMode"]);
                    if (dEnableTrainingMode)
                    {
                        dTipRate = dTrainingTipRate;
                        dTipRateOT = dTrainingTipRateOT;
                    }

                    if (!IsHourlyWage)
                        dHourlyWage = 0;

                    DataRow drRole = dtRoles.Rows[0];
                    if (isUsingRoleWage)
                    {
                        IsHourlyWage = Utility.GetBoolFromObject(drRole["IsHourlyWage"]);
                        dHourlyWage = Utility.GetDecimalFromObject(drRole["HourlyWage"]);
                        dHourlyWageOT = Utility.GetDecimalFromObject(drRole["HourlyWageOT"]);
                        bIsDailyOverTime = Utility.GetBoolFromObject(drRole["IsDailyOverTime"]);
                        bIsDailyOverTime2nd = Utility.GetBoolFromObject(drRole["IsDailyOverTime2nd"]);
                        dHourlyWageOT2nd = Utility.GetDecimalFromObject(drRole["HourlyWageOT2nd"]);

                        if (!IsHourlyWage)
                            dHourlyWage = 0;
                    }

                    if (isUsingRoleTipPool)
                    {
                        if (dEnableTrainingMode)
                        {
                            dTipRate = Utility.GetDecimalFromObject(drRole["TrainingTipRate"]);
                            dTipRateOT = Utility.GetDecimalFromObject(drRole["TrainingTipRateOT"]);
                        }
                        else
                        {
                            dTipRate = Utility.GetDecimalFromObject(drRole["TipRate"]);
                            dTipRateOT = Utility.GetDecimalFromObject(drRole["TipRateOT"]);
                        }

                        dMaxTipHour = Utility.GetDecimalFromObject(drRole["MaxTipHour"]);
                        dMaxTipHourOT = Utility.GetDecimalFromObject(drRole["MaxTipHourOT"]);

                        dServiceChargesRate = Utility.GetDecimalFromObject(drRole["ServiceChargesRate"]);
                        dServiceChargesRateOT = Utility.GetDecimalFromObject(drRole["ServiceChargesRateOT"]);
                        dServiceChargesMaxHour = Utility.GetDecimalFromObject(drRole["ServiceChargesMaxHour"]);
                        dServiceChargesMaxHourOT = Utility.GetDecimalFromObject(drRole["ServiceChargesMaxHourOT"]);
                    }

                    var sql = $"update TimeClock set " +
                        $"HourlyWage = '{dHourlyWage}', HourlyWageOT = '{dHourlyWageOT}', HourlyWageOT2nd = '{dHourlyWageOT2nd}', " +
                        $"IsDailyOverTime = '{bIsDailyOverTime}', IsDailyOverTime2nd = '{bIsDailyOverTime2nd}', " +
                        $"TipRate = '{dTipRate}', TipRateOT = '{dTipRateOT}', " +
                        $"MaxTipHour = '{dMaxTipHour}', MaxTipHourOT = '{dMaxTipHourOT}', " +
                        $"ServiceChargesRate = '{dServiceChargesRate}', ServiceChargesRateOT = '{dServiceChargesRateOT}', " +
                        $"ServiceChargesMaxHour = '{dServiceChargesMaxHour}', ServiceChargesMaxHourOT = '{dServiceChargesMaxHourOT}', " +
                        $"RolesID = '{roleID}' " +
                        $"where EmployeeID = '{employeeID}' and DateTimeIn >= '{startDate}' and DateTimeIn <= '{endDate}'";
                    RunQueryUpdate(sqlCommand, sql);

                    sql = $"update PaymentRoleDetails set TipRate = '{dTipRate}', TipRateOT = '{dTipRateOT}', RolesID = '{roleID}' " +
                        $"where EmployeeID = '{employeeID}' and DateCreated >= '{startDate}' and DateCreated <= '{endDate}'";
                    RunQueryUpdate(sqlCommand, sql);

                    IsSuccess = true;
                }

            }
            catch (Exception ex)
            {
                BLogicLogger.Exception("SyncEmployeeData in TimeClock", ex);
            }
        }

        private void GetEmployeeInfos()
        {
            DataTable dtEmployees = new DataTable("Employees");
            DataTable dtRoles = new DataTable("Roles");
            DataTable dtEmployeeRoles = new DataTable("EmployeeRoles");
            try
            {
                using (SqlCommand sqlCommand = new SqlCommand())
                using (SqlConnection conn = new SqlConnection(Helper.BLOGICPOS7_Connection))
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    sqlCommand.Connection = conn;
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.CommandText = "select EmployeeID, EmployeeCode, TRIM( EmployeeFirstName) + ' ' +  TRIM(EmployeeLastName) as EmployeeName from Employees where EmployeeID > 0 and Enable = 0";

                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    dtEmployees.Load(reader);
                    reader.Close();

                    sqlCommand.CommandText = "select RolesID, RolesName from Roles where IsDeleted = '0' or IsDeleted is null";
                    reader = sqlCommand.ExecuteReader();
                    dtRoles.Load(reader);
                    reader.Close();

                    sqlCommand.CommandText = "select * from EmployeeRoles where IsDeleted = '0' or IsDeleted is null";
                    reader = sqlCommand.ExecuteReader();
                    dtEmployeeRoles.Load(reader);
                    reader.Close();

                    dtEmployees.Columns.Add("RoleName", typeof(string));
                    foreach (DataRow item in dtEmployees.Rows)
                    {
                        DataRow[] drs = dtEmployeeRoles.Select($"EmployeeID = '{Utility.GetIntFromRow("EmployeeID", item)}'", "");
                        if (drs.Length > 0)
                        {
                            List<string> roleNames = new List<string>();
                            foreach (DataRow dr in drs)
                            {
                                int roleID = Utility.GetIntFromRow("RolesID", dr);
                                DataRow[] roleRows = dtRoles.Select($"RolesID = '{roleID}'", "");
                                if (roleRows.Length > 0)
                                {
                                    roleNames.Add(Utility.GetStringFromRow("RolesName", roleRows[0]));
                                }
                            }
                            item["RoleName"] = string.Join(", ", roleNames);
                        }
                    }

                    DataSetReturn = new DataSet();
                    DataSetReturn.Tables.Add(dtEmployees);
                    DataSetReturn.Tables.Add(dtRoles);
                    DataSetReturn.Tables.Add(dtEmployeeRoles);
                }

                IsSuccess = true;
            }
            catch (Exception ex)
            {
                ErrorMessage = "Error retrieving Employees data from Blogicpos7.";
                BLogicLogger.Exception("GetEmployees", ex);
            }
        }


        private void GetReceiptTemplateByTicketNumber(string ticketNumber)
        {
            try
            {
                DataRow[] drs = null;

                SaleReceiptDTO saleReceiptDTO = new SaleReceiptDTO();
                StoreDTO storeDTO = new StoreDTO();

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
                string saleID = null;
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
                    if (GetReceiptTemplateByTicketNumber_Include_VOID_CLOSE_REFUND_OPER)
                        sqlCommand.CommandText = $"select * from SaleReceipts where SaleReceiptNumber like '{ticketNumber}' and TransactionStatusID in ('OPER','FIN','RET','VOID')";

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
                            if (GetReceiptTemplateByTicketNumber_Include_VOID_CLOSE_REFUND_OPER &&
                                (TransactionStatusID == "OPER" || TransactionStatusID == "VOID" || TransactionStatusID == "CLO" || TransactionStatusID == "RET"))
                                ErrorMessage = string.Empty;
                            else
                                ErrorMessage = "Ticket not found.";
                            break;

                    }

                    if (!string.IsNullOrWhiteSpace(ErrorMessage))
                    {
                        return;
                    }
                }

                GetReceiptTemplateBySaleReceiptID(saleID);
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Something went wrong while searching for the ticket.{Environment.NewLine}Please try again or contact TruongDS.";
                BLogicLogger.Exception("GetReceiptTemplateByTicketNumber", ex, new System.Collections.Generic.List<object>() { ticketNumber });
            }
        }

        private void GetReceiptTemplateBySaleReceiptID(string _saleReceiptID)
        {
            try
            {
                DataRow[] drs = null;

                SaleReceiptDTO saleReceiptDTO = new SaleReceiptDTO();
                StoreDTO storeDTO = new StoreDTO();

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

                    sqlCommand.CommandText = $"select * from SaleReceipts where SaleReceiptID = '{_saleReceiptID}' ";
                    if (GetReceiptTemplateByTicketNumber_Include_VOID_CLOSE_REFUND_OPER)
                        sqlCommand.CommandText = $"select * from SaleReceipts where SaleReceiptNumber = '{_saleReceiptID}' ";

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
                GetReceiptTemplateByTicketNumber_TotalAmount = Utility.GetDecimalFromObject(saleReceiptDTO.SaleReceipts.Compute("SUM(TotalAmount)", "")) +
                    Utility.GetDecimalFromObject(saleReceiptDTO.SaleReceipts.Compute("SUM(SVCFeePaidAmount)", "")) -
                    Utility.GetDecimalFromObject(saleReceiptDTO.SaleReceipts.Compute("SUM(CashDiscountPaidAmount)", "")) +
                    Utility.GetDecimalFromObject(dtSaleReceiptPaymentDetails.Compute("SUM(Gratuity)", ""));
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Something went wrong while searching for the ticket.{Environment.NewLine}Please try again or contact TruongDS.";
                BLogicLogger.Exception("GetReceiptTemplateBySaleReceiptID", ex, new System.Collections.Generic.List<object>() { _saleReceiptID });
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

                DataTable dtPaymentRoleDetails = new DataTable("PaymentRoleDetails");
                using (SqlCommand sqlCommand = new SqlCommand())
                using (SqlConnection conn = new SqlConnection(Helper.BLOGICPOS7_Connection))
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();

                    sqlCommand.Connection = conn;
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.CommandText = $"select * from SaleReceipts where SaleReceiptID = '{SaleReceiptID}'";
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    dtSaleReceipts.Load(reader);
                    reader.Close();

                    sqlCommand.CommandText = $"select * from SaleReceipts where SaleReceiptID = '{TokenSaleReceiptID}'";
                    reader = sqlCommand.ExecuteReader();
                    dtTokenSaleReceipts.Load(reader);
                    reader.Close();

                    sqlCommand.CommandText = $"select * from SaleReceiptPaymentDetails where SaleReceiptID = '{SaleReceiptID}'";
                    reader = sqlCommand.ExecuteReader();
                    dtSaleReceiptPaymentDetails.Load(reader);
                    reader.Close();

                    sqlCommand.CommandText = $"select a.*, b.PaymentMethodType from " +
                        $" SaleReceiptPaymentDetails a left join PaymentMethods b on a.PaymentMethodID = b.PaymentMethodID " +
                        $" where SaleReceiptID = '{TokenSaleReceiptID}'";
                    reader = sqlCommand.ExecuteReader();
                    dtTokenSaleReceiptPaymentDetails.Load(reader);
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
                        scriptUpdate = $"    update SaleReceipts set TransactionStatusID = 'CLO' where SaleReceiptID = '{TokenSaleReceiptID}';";
                        RunQueryUpdate(sqlCommand, scriptUpdate);

                        foreach (DataRow item in dtTokenSaleReceiptPaymentDetails.Rows)
                        {
                            item["CreatedDate"] = DateCreated;
                            item["Gratuity"] = Utility.GetDecimalFromObject(item["AmountTendered"]) - Utility.GetDecimalFromObject(item["AmountReturn"]) + Utility.GetDecimalFromObject(item["Gratuity"]) + Utility.GetDecimalFromObject(item["SVCFeeAmount"]) - Utility.GetDecimalFromObject(item["CashDiscountAmount"]);

                            if (Utility.GetDecimalFromRow("SVCFeeAmount", item) != 0)
                                item["SVCFeeForGratuity"] = Math.Round(Utility.GetDecimalFromObject(item["Gratuity"]) * POSConfig.Configuration.ConfigValue.CreditCardConfigValue.SVCFee / 100, 2);
                            else
                                item["SVCFeeForGratuity"] = 0;

                            item["SVCFeeAmount"] = 0;

                            if (Utility.GetDecimalFromRow("MerchantFee", item) != 0)
                            {
                                item["MerchantFee"] = Math.Round(Utility.GetDecimalFromObject(item["Gratuity"]) * POSConfig.Configuration.ConfigValue.CreditCardConfigValue.MerchantFee / 100, 2);
                                item["MerchantFeeForTip"] = item["MerchantFee"];
                            }
                            else
                            {
                                item["MerchantFee"] = 0;
                                item["MerchantFeeForTip"] = 0;
                            }


                            scriptUpdate = $"    update SaleReceiptPaymentDetails set CreatedDate = '{DateCreated}', AmountTendered = '0', SaleReceiptID = '{SaleReceiptID}', SVCFeeAmount = '{item["SVCFeeAmount"]}', SVCFeeForGratuity = '{item["SVCFeeForGratuity"]}', Gratuity = '{item["Gratuity"]}', MerchantFee = '{item["MerchantFee"]}', MerchantFeeForTip = '{item["MerchantFeeForTip"]}' where SaleReceiptPaymentDetailID = '{item["SaleReceiptPaymentDetailID"]}';";

                            RunQueryUpdate(sqlCommand, scriptUpdate);
                        }

                        drs = CurrentTableInDatabaseBLogicPOS7.Select("TABLE_NAME = 'PaymentRoleDetails'", "");
                        if (drs.Length > 0)
                        {
                            sqlCommand.CommandText = $"select top 1 * from PaymentRoleDetails where SaleReceiptID = '{SaleReceiptID}'";
                            reader = sqlCommand.ExecuteReader();
                            dtPaymentRoleDetails.Load(reader);
                            reader.Close();

                            scriptUpdate = string.Empty;
                            foreach (DataRow item in dtTokenSaleReceiptPaymentDetails.Rows)
                            {
                                drs = dtPaymentRoleDetails.Select($"SaleReceiptID = '{item["SaleReceiptID"]}' and SaleReceiptPaymentDetailID = '{item["SaleReceiptPaymentDetailID"]}'", "");
                                if (drs.Length > 0)
                                {
                                    scriptUpdate = $"update PaymentRoleDetails set Gratuity = '{item["Gratuity"]}', SVCFeeForGratuity = '{item["SVCFeeForGratuity"]}', MerchantFeeForTip = '{item["MerchantFeeForTip"]}' where ID = '{drs[0]["ID"]}';";
                                }
                                else
                                {
                                    scriptUpdate = $"    INSERT INTO PaymentRoleDetails (ID,SaleReceiptID,SaleReceiptPaymentDetailID,PaymentMethodID,PaymentMethodType,EmployeeID,RolesID,TipRate,TipRateOT,Gratuity,SVCFeeForGratuity,MerchantFeeForTip,DateCreated,PartyServiceFeeAmount,PartyServiceFeeTaxAmount,SVCFeeForPartyServiceFeeAmount,MerchantFeeForPartyServiceFeeAmount,AutoGratuityAmount,AutoGratuityTaxAmount,SVCFeeForAutoGratuityAmount,MerchantFeeForAutoGratuityAmount,AreaFeeAmount,AreaFeeTaxAmount,SVCFeeForAreaFeeAmount,MerchantFeeForAreaFeeAmount,ServiceFee,ServiceFeeTaxAmount,SVCFeeForServiceFee,MerchantFeeForServiceFee)";
                                    scriptUpdate += $"    VALUES (NEWID(), '{SaleReceiptID}', '{item["SaleReceiptPaymentDetailID"]}', '{item["PaymentMethodID"]}', '{item["PaymentMethodType"]}', '{dtSaleReceipts.Rows[0]["CashierID"]}', '{employeeClockInInfoEntity.RolesID}', '{employeeClockInInfoEntity.TipRate}', '{employeeClockInInfoEntity.TipRateOT}', '{item["Gratuity"]}', '{item["SVCFeeForGratuity"]}', '{item["MerchantFeeForTip"]}', '{DateCreated}', '{0}', '{0}', '{0}', '{0}', '{0}', '{0}', '{0}', '{0}', '{0}', '{0}', '{0}', '{0}', '{0}', '{0}', '{0}', '{0}'  )";
                                }

                                RunQueryUpdate(sqlCommand, scriptUpdate);
                            }
                        }
                    }

                    UpdateFinalGratuityForSaleReceipts(sqlCommand, SaleReceiptID);
                }

                if (dtTokenSaleReceiptPaymentDetails?.Rows.Count > 0)
                {
                    string batchUnique = string.Empty;
                    DataTable dtTransactionDetail = new DataTable("TransactionDetail");
                    using (SqlCommand sqlCommand = new SqlCommand())
                    using (SqlConnection conn = new SqlConnection(Helper.Merchant_Connection))
                    {
                        if (conn.State == ConnectionState.Closed)
                            conn.Open();

                        sqlCommand.Connection = conn;
                        sqlCommand.CommandType = CommandType.Text;

                        scriptUpdate = string.Empty;

                        foreach (DataRow item in dtTokenSaleReceiptPaymentDetails.Rows)
                        {
                            if (!string.IsNullOrWhiteSpace(Utility.GetStringFromRow("CreditCardTransactionID", item)))
                                scriptUpdate += $"update TransactionDetail set InvoiceNo = '{dtSaleReceipts.Rows[0]["InvoiceNum"]}', Amount = 0, Gratuity = '{item["Gratuity"]}', SubTotal= '0', TaxAmount = '0', TotalAmount = '0', TicketNumber = '{SaleReceiptNumber}', SVCFee = '0',SVCFeeForGratuity = '{item["SVCFeeForGratuity"]}',MerchantFee = '{item["MerchantFeeForTip"]}', MerchantFeeForTip = '{item["MerchantFeeForTip"]}' where ID = '{item["CreditCardTransactionID"]}'";
                        }

                        RunQueryUpdate(sqlCommand, scriptUpdate);
                    }

                    foreach (DataRow item in dtTokenSaleReceiptPaymentDetails.Rows)
                    {
                        using (SqlCommand sqlCommand = new SqlCommand())
                        using (SqlConnection conn = new SqlConnection(Helper.Merchant_Connection))
                        {
                            if (conn.State == ConnectionState.Closed)
                                conn.Open();

                            sqlCommand.Connection = conn;
                            sqlCommand.CommandType = CommandType.Text;

                            if (!string.IsNullOrWhiteSpace(Utility.GetStringFromRow("CreditCardTransactionID", item)))
                            {
                                sql = $"select distinct BatchUnique from MERCHANT.dbo.TransactionDetail where ID = '{dtTokenSaleReceiptPaymentDetails.Rows[0]["CreditCardTransactionID"]}'";
                                sqlCommand.CommandText = sql;
                                batchUnique = Utility.GetStringFromObject(sqlCommand.ExecuteScalar());
                            }
                        }

                        if (!string.IsNullOrWhiteSpace(batchUnique))
                            UpdateBatchByBatchUnique(batchUnique);
                    }
                }

                IsSuccess = true;

            }
            catch (Exception ex)
            {
                ErrorMessage = $"Something went wrong while searching for the ticket.{Environment.NewLine}Please try again or contact TruongDS.";
                BLogicLogger.Exception("UpdateTipByTokenReceipt", ex, new System.Collections.Generic.List<object>() { SaleReceiptID, TokenSaleReceiptID });
            }
            finally
            {

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

        private void RunQueryUpdate(SqlCommand sqlCommand, string scriptUpdate, SqlParameter[] cmdParams = null)
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

        private void UpdateBatchByBatchUnique(object batchUnique)
        {
            DataTable dtCalculateBatch = new DataTable("Batch");
            DataTable dtTransactionDetail = new DataTable("TransactionDetail");

            using (SqlCommand sqlCommand = new SqlCommand())
            using (SqlConnection conn = new SqlConnection(Helper.Merchant_Connection))
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();

                sqlCommand.Connection = conn;
                sqlCommand.CommandType = CommandType.Text;

                var sql = $"select * from TransactionDetail where BatchUnique = '{batchUnique}'";
                sqlCommand.CommandText = sql;
                SqlDataReader reader = sqlCommand.ExecuteReader();
                dtTransactionDetail.Load(reader);
                reader.Close();

                if (dtTransactionDetail?.Rows.Count > 0)
                {
                    sql = $@"select sum(case when (TranType = 'Sale' or TranType = 'PreAuthCapture') and TranStatus = 'ACTIVE' then Total else 0 end ) -	sum(case when TranType = 'Return' then Total else 0 end ) as Total,
                       sum(case when (TranType = 'Sale' or TranType = 'PreAuthCapture') and TranStatus = 'ACTIVE' then SVCFeeForGratuity else 0 end ) as TotalSVCFeeForTip, 
                       sum(case when (TranType = 'Sale' or TranType = 'PreAuthCapture') and TranStatus = 'ACTIVE' then Amount - SVCFee else 0 end ) as TotalSales, 
                       sum(case when TranType = 'Return' then Amount - SVCFee else 0 end ) as TotalReturns, 
                       sum(case when (TranType = 'Sale' or TranType = 'PreAuthCapture') and TranStatus = 'ACTIVE' then Gratuity else 0 end ) as TotalTips, 
                       sum(case when (TranType = 'Sale' or TranType = 'PreAuthCapture') and TranStatus = 'ACTIVE' then SVCFee else 0 end ) - sum(case when TranType = 'Return' then SVCFee else 0 end ) as TotalSVCFeeAmount,
                       sum(case when (TranType = 'Sale' or TranType = 'PreAuthCapture') and TranStatus = 'ACTIVE' then Total - MerchantFee else 0 end ) -	sum(case when TranType = 'Return' then Total else 0 end ) as DepositAmount, 
                       sum(case when (TranType = 'Sale' or TranType = 'PreAuthCapture') and TranStatus = 'ACTIVE' then MerchantFee else 0 end ) as MerchantFeeAmount,
                       count(ID) as TicketCount 
                       from TransactionDetail where BatchUnique = '{batchUnique}'";

                    sqlCommand.CommandText = sql;
                    reader = sqlCommand.ExecuteReader();
                    dtCalculateBatch.Load(reader);
                    reader.Close();

                    if (dtCalculateBatch?.Rows.Count > 0)
                    {
                        sql = $"update Batch set " +
                            $" Total = '{dtCalculateBatch.Rows[0]["Total"]}'," +
                            $" TotalSVCFeeForTip = {dtCalculateBatch.Rows[0]["TotalSVCFeeForTip"]} ," +
                            $" TotalSales = {dtCalculateBatch.Rows[0]["TotalSales"]} ," +
                            $" TotalTips = '{dtCalculateBatch.Rows[0]["TotalTips"]}', " +
                            $" TotalSVCFeeAmount = {dtCalculateBatch.Rows[0]["TotalSVCFeeAmount"]}," +
                            $" DepositAmount = {dtCalculateBatch.Rows[0]["DepositAmount"]}," +
                            $" MerchantFeeAmount = {dtCalculateBatch.Rows[0]["MerchantFeeAmount"]}," +
                            $" Quantity = '{dtCalculateBatch.Rows[0]["TicketCount"]}'" +
                            $" where BatchUnique = '{batchUnique}';";

                        RunQueryUpdate(sqlCommand, sql);

                        sql = $"update TransactionDetail set " +
                            $" BatchTotal = '{dtCalculateBatch.Rows[0]["Total"]}'" +
                            $" where BatchUnique = '{batchUnique}';";

                        RunQueryUpdate(sqlCommand, sql);

                    }
                }
                else
                {
                    sql = $"delete from Batch where BatchUnique = '{batchUnique}';";

                    RunQueryUpdate(sqlCommand, sql);
                }

            }
        }

        private void AddTipForPaymentDetail()
        {
            DataRow[] drs = null;
            string scriptUpdate = string.Empty;
            string scriptUpdatePaymentRoleDetails = string.Empty;
            string sql = string.Empty;
            int resultQueryUpdate = 0;

            try
            {
                SaleReceiptPaymentDetailEntity paymentDetailEntity = null;

                DataTable dtSaleReceipts = new DataTable("SaleReceipts");
                DataTable dtSaleReceiptPaymentDetails = new DataTable("SaleReceiptPaymentDetails");

                DataTable dtPaymentRoleDetails = new DataTable("PaymentRoleDetails");
                using (SqlCommand sqlCommand = new SqlCommand())
                using (SqlConnection conn = new SqlConnection(Helper.BLOGICPOS7_Connection))
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();

                    sqlCommand.Connection = conn;
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.CommandText = $"select * from SaleReceipts where SaleReceiptID = '{SaleReceiptID}'";
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    dtSaleReceipts.Load(reader);
                    reader.Close();

                    sqlCommand.CommandText = $"select a.*, b.PaymentMethodType from " +
                        $" SaleReceiptPaymentDetails a left join PaymentMethods b on a.PaymentMethodID = b.PaymentMethodID " +
                        $" where SaleReceiptPaymentDetailID = '{SaleReceiptPaymentDetailID}'";
                    reader = sqlCommand.ExecuteReader();
                    dtSaleReceiptPaymentDetails.Load(reader);
                    reader.Close();

                    #region verify Receipt
                    if (dtSaleReceiptPaymentDetails == null || dtSaleReceiptPaymentDetails.Rows.Count == 0)
                    {
                        ErrorMessage = "The Receipt don't exist any Payment Detail.";
                        return;
                    }

                    paymentDetailEntity = SaleReceiptPaymentDetailEntity.FromDataRow(dtSaleReceiptPaymentDetails.Rows[0]);

                    paymentDetailEntity.Gratuity = AddTipForPaymentDetail_Gratuity;

                    if (paymentDetailEntity.SVCFeeAmount != 0)
                        paymentDetailEntity.SVCFeeForGratuity = Math.Round(paymentDetailEntity.Gratuity * POSConfig.Configuration.ConfigValue.CreditCardConfigValue.SVCFee / 100, 2);
                    else
                        paymentDetailEntity.SVCFeeForGratuity = 0;

                    if (paymentDetailEntity.MerchantFee != 0)
                    {
                        paymentDetailEntity.MerchantFee = Math.Round((paymentDetailEntity.AmountTendered - paymentDetailEntity.AmountReturn + paymentDetailEntity.Gratuity) * POSConfig.Configuration.ConfigValue.CreditCardConfigValue.MerchantFee / 100, 2);
                        paymentDetailEntity.MerchantFeeForTip = Math.Round(paymentDetailEntity.Gratuity * POSConfig.Configuration.ConfigValue.CreditCardConfigValue.MerchantFee / 100, 2);
                    }
                    else
                    {
                        paymentDetailEntity.MerchantFee = 0;
                        paymentDetailEntity.MerchantFeeForTip = 0;
                    }

                    #endregion

                    var CashierID = Utility.GetIntFromRow("CashierID", dtSaleReceipts.Rows[0]);
                    var DateCreated = Utility.GetDateTimeFromRow("DateCreated", dtSaleReceipts.Rows[0]);

                    var employeeClockInInfoEntity = Helper.GetEmployeeClockInInfoEntity(sqlCommand, CashierID, DateCreated);

                    if (dtSaleReceiptPaymentDetails?.Rows.Count > 0)
                    {
                        scriptUpdate = $"    update SaleReceiptPaymentDetails set Gratuity = '{paymentDetailEntity.Gratuity}', " +
                            $" SVCFeeForGratuity = '{paymentDetailEntity.SVCFeeForGratuity}', " +
                            $" MerchantFee = '{paymentDetailEntity.MerchantFee}', " +
                            $" MerchantFeeForTip = '{paymentDetailEntity.MerchantFeeForTip}' " +
                            $" where SaleReceiptPaymentDetailID = '{paymentDetailEntity.SaleReceiptPaymentDetailID}';";

                        RunQueryUpdate(sqlCommand, scriptUpdate);

                        drs = CurrentTableInDatabaseBLogicPOS7.Select("TABLE_NAME = 'PaymentRoleDetails'", "");
                        if (drs.Length > 0)
                        {
                            sqlCommand.CommandText = $"select top 1 * from PaymentRoleDetails where SaleReceiptID = '{SaleReceiptID}' and SaleReceiptPaymentDetailID = '{SaleReceiptPaymentDetailID}'";
                            reader = sqlCommand.ExecuteReader();
                            dtPaymentRoleDetails.Load(reader);
                            reader.Close();

                            scriptUpdate = string.Empty;

                            drs = dtPaymentRoleDetails.Select($"SaleReceiptID = '{paymentDetailEntity.SaleReceiptID}' and SaleReceiptPaymentDetailID = '{paymentDetailEntity.SaleReceiptPaymentDetailID}'", "");
                            if (drs.Length > 0)
                            {
                                scriptUpdate += $"update PaymentRoleDetails set Gratuity = '{paymentDetailEntity.Gratuity}', SVCFeeForGratuity = '{paymentDetailEntity.SVCFeeForGratuity}', MerchantFeeForTip = '{paymentDetailEntity.MerchantFeeForTip}' where ID = '{drs[0]["ID"]}';";
                            }
                            else
                            {
                                scriptUpdate += $"    INSERT INTO PaymentRoleDetails (ID,SaleReceiptID,SaleReceiptPaymentDetailID,PaymentMethodID,PaymentMethodType,EmployeeID,RolesID,TipRate,TipRateOT,Gratuity,SVCFeeForGratuity,MerchantFeeForTip,DateCreated,PartyServiceFeeAmount,PartyServiceFeeTaxAmount,SVCFeeForPartyServiceFeeAmount,MerchantFeeForPartyServiceFeeAmount,AutoGratuityAmount,AutoGratuityTaxAmount,SVCFeeForAutoGratuityAmount,MerchantFeeForAutoGratuityAmount,AreaFeeAmount,AreaFeeTaxAmount,SVCFeeForAreaFeeAmount,MerchantFeeForAreaFeeAmount,ServiceFee,ServiceFeeTaxAmount,SVCFeeForServiceFee,MerchantFeeForServiceFee)";
                                scriptUpdate += $"    VALUES (NEWID(), '{paymentDetailEntity.SaleReceiptID}', '{paymentDetailEntity.SaleReceiptPaymentDetailID}', '{paymentDetailEntity.PaymentMethodID}', '{paymentDetailEntity.PaymentMethodType}', '{dtSaleReceipts.Rows[0]["CashierID"]}', '{employeeClockInInfoEntity.RolesID}', '{employeeClockInInfoEntity.TipRate}', '{employeeClockInInfoEntity.TipRateOT}', '{paymentDetailEntity.Gratuity}', '{paymentDetailEntity.SVCFeeForGratuity}', '{paymentDetailEntity.MerchantFeeForTip}', '{DateCreated}', '{0}', '{0}', '{0}', '{0}', '{0}', '{0}', '{0}', '{0}', '{0}', '{0}', '{0}', '{0}', '{0}', '{0}', '{0}', '{0}'  )";
                            }

                            RunQueryUpdate(sqlCommand, scriptUpdate);
                        }
                    }

                    UpdateFinalGratuityForSaleReceipts(sqlCommand, SaleReceiptID);

                }

                string batchUnique = string.Empty;
                if (!string.IsNullOrWhiteSpace(paymentDetailEntity.CreditCardTransactionID))
                {
                    DataTable dtTransactionDetail = new DataTable("TransactionDetail");
                    using (SqlCommand sqlCommand = new SqlCommand())
                    using (SqlConnection conn = new SqlConnection(Helper.Merchant_Connection))
                    {
                        if (conn.State == ConnectionState.Closed)
                            conn.Open();

                        sqlCommand.Connection = conn;
                        sqlCommand.CommandType = CommandType.Text;

                        scriptUpdate = $"update TransactionDetail set Gratuity = '{paymentDetailEntity.Gratuity}'," +
                            $" SVCFeeForGratuity = '{paymentDetailEntity.SVCFeeForGratuity}'," +
                            $" MerchantFee = '{paymentDetailEntity.MerchantFee}'," +
                            $" Total = Amount + {paymentDetailEntity.Gratuity}," +
                            $" MerchantFeeForTip = '{paymentDetailEntity.MerchantFeeForTip}' " +
                            $" where ID = '{paymentDetailEntity.CreditCardTransactionID}'";

                        RunQueryUpdate(sqlCommand, scriptUpdate);

                        sql = $"select distinct BatchUnique from MERCHANT.dbo.TransactionDetail where ID = '{paymentDetailEntity.CreditCardTransactionID}'";
                        sqlCommand.CommandText = sql;
                        batchUnique = Utility.GetStringFromObject(sqlCommand.ExecuteScalar());
                    }
                }


                if (!string.IsNullOrWhiteSpace(batchUnique))
                    UpdateBatchByBatchUnique(batchUnique);

                IsSuccess = true;

            }
            catch (Exception ex)
            {
                ErrorMessage = $"Something went wrong while searching for the ticket.{Environment.NewLine}Please try again or contact TruongDS.";
                BLogicLogger.Exception("AddTipForPaymentDetail in ProcessForm", ex, new System.Collections.Generic.List<object>() { SaleReceiptID, SaleReceiptPaymentDetailID, AddTipForPaymentDetail_Gratuity });
            }
            finally
            {

            }
        }

        private void ProcessSettleBatchOffline()
        {

            try
            {
                DataTable dtTransactionDetail = new DataTable("TransactionDetail");
                DataTable dtGroupSettleBatch = new DataTable("TransactionDetail");

                using (SqlCommand sqlCommand = new SqlCommand())
                using (SqlConnection conn = new SqlConnection(Helper.Merchant_Connection))
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();

                    string sql = $"select distinct MerchantID, BatchID from TransactionDetail where ISNULL(BatchUnique,'') = '' and WorkstationID <> '00000000-0000-0000-0000-000000000000';";

                    sqlCommand.Connection = conn;
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.CommandText = sql;
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    dtGroupSettleBatch.Load(reader);
                    reader.Close();
                }

                foreach (DataRow item in dtGroupSettleBatch.Rows)
                {
                    SettleBatchOfflineByMerchantID(item["MerchantID"], item["BatchID"]);
                }

                IsSuccess = true;
            }
            catch (Exception ex)
            {
                BLogicLogger.Exception("ProcessSettleBatchOffline", ex);
            }
        }

        private void SettleBatchOfflineByMerchantID(object merchantID, object batchID)
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

                    string sql = $"select distinct MerchantID, BatchID from TransactionDetail where ISNULL(BatchUnique,'') = '' and MerchantID = '{merchantID}' and BatchID = '{batchID}' ;";

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

                        string sql = $"select * from TransactionDetail where MerchantID = '{item["MerchantID"]}' and BatchID = '{item["BatchID"]}' and ISNULL(BatchUnique,'') = '';";

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
                        decimal subTotal = Utility.GetDecimalFromRow("Total", dr);
                        decimal subAmount = Utility.GetDecimalFromRow("Amount", dr);
                        decimal subSVCFeeAmount = Utility.GetDecimalFromRow("SVCFee", dr);
                        decimal subSVCFeeForTip = Utility.GetDecimalFromRow("SVCFeeForGratuity", dr);
                        decimal subReturn = Utility.GetDecimalFromRow("Amount", dr);
                        decimal subTip = Utility.GetDecimalFromRow("Gratuity", dr);
                        string tranStatus = dr["TranStatus"].ToString();
                        decimal subMerchantFee = Utility.GetDecimalFromRow("MerchantFee", dr);

                        string dSVCFeeType = dr["SVCFeeType"].ToString();
                        decimal dMerchantFeePercent = Utility.GetDecimalFromRow("MerchantFeePercent", dr);

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
                        sIsReCaculateMerchantFee = lstMerchantFeePercent[0] != POSConfig.Configuration.ConfigValue.CreditCardConfigValue.MerchantFee;
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
                        $"	WHERE MerchantID = '{MerchantID}' and BatchID = '{BatchID}' and ISNULL(BatchUnique,'') = ''" +
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

        public void GetRemainingBatch()
        {
            try
            {
                IsSuccess = false;
                DataTable dtGroupSettleBatch = new DataTable("TransactionDetail");
                using (SqlCommand sqlCommand = new SqlCommand())
                using (SqlConnection conn = new SqlConnection(Helper.Merchant_Connection))
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();

                    string sql = $@";WITH F AS (
                                        SELECT MerchantID, BatchID,WorkstationName, TerminalIpAddress, ID
                                        FROM TransactionDetail
                                        WHERE ISNULL(BatchUnique, '') = ''
                                          AND WorkstationID <> '00000000-0000-0000-0000-000000000000'
                                    ),
                                    Batches AS (
                                        SELECT
                                            MerchantID,
                                            STRING_AGG(CONVERT(varchar(50), BatchID), ', ') AS BatchIDs
                                        FROM (
                                            SELECT DISTINCT MerchantID, BatchID
                                            FROM F
                                        ) d
                                        GROUP BY MerchantID
                                    ),
                                    Agg AS (
                                        SELECT
                                            MerchantID,
                                            MAX(TerminalIpAddress) AS TerminalIpAddress,
                                            COUNT(ID) AS CountTicket,
                                            MAX(WorkstationName) AS WorkstationName
                                        FROM F
                                        GROUP BY MerchantID
                                    )
                                    SELECT
                                        a.MerchantID,
                                        b.BatchIDs AS BatchID,
                                        a.TerminalIpAddress,
                                        a.CountTicket,
                                        a.WorkstationName 
                                    FROM Agg a
                                    JOIN Batches b ON b.MerchantID = a.MerchantID
                                    ORDER BY a.MerchantID;
                                    ";

                    sqlCommand.Connection = conn;
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.CommandText = sql;
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    dtGroupSettleBatch.Load(reader);
                    reader.Close();
                }

                DataTableReturn = dtGroupSettleBatch;

                IsSuccess = true;

            }
            catch (Exception ex)
            {
                BLogicLogger.Exception("GetRemainingBatch in SettleBatchOfflineForm", ex);
            }
        }

        public void CloseReceipt()
        {
            try
            {
                DataTable dtEmployee = new DataTable("Employees");
                DataTable dtRoles = new DataTable("Roles");

                DataTable dtBatch = new DataTable("TransactionDetail");

                using (SqlCommand sqlCommand = new SqlCommand())
                using (SqlConnection conn = new SqlConnection(Helper.BLOGICPOS7_Connection))
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    sqlCommand.Connection = conn;
                    sqlCommand.CommandType = CommandType.Text;

                    var sql = $"update SaleReceipts set TransactionStatusID = 'CLO' where SaleReceiptID = '{SaleReceiptID}'; ";

                    RunQueryUpdate(sqlCommand, sql);

                    DataRow[] drs = CurrentTableInDatabaseBLogicPOS7.Select("TABLE_NAME = 'PaymentRoleDetails'", "");
                    if (drs.Length > 0)
                    {
                        sql = $"delete from PaymentRoleDetails where SaleReceiptID = '{SaleReceiptID}'; ";

                        RunQueryUpdate(sqlCommand, sql);
                    }
                }

                using (SqlCommand sqlCommand = new SqlCommand())
                using (SqlConnection conn = new SqlConnection(Helper.Merchant_Connection))
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();

                    sqlCommand.Connection = conn;
                    sqlCommand.CommandType = CommandType.Text;

                    var sql = $"select distinct BatchUnique from TransactionDetail where ISNULL(BatchUnique, '') <> '' and " +
                        $" ID in (select CreditCardTransactionID from {Helper.BLOGICPOS7_Name}.dbo.SaleReceiptPaymentDetails a where a.SaleReceiptID = '{SaleReceiptID}' and ISNULL(CreditCardTransactionID, '') <> '')";
                    sqlCommand.CommandText = sql;
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    dtBatch.Load(reader);
                    reader.Close();

                    sql = $"update TransactionDetail set BatchStatus = 'Closed', BatchUnique = '{Guid.Empty}' " +
                        $"where ID in (select CreditCardTransactionID from {Helper.BLOGICPOS7_Name}.dbo.SaleReceiptPaymentDetails a where a.SaleReceiptID = '{SaleReceiptID}' and ISNULL(CreditCardTransactionID, '') <> '')";

                    RunQueryUpdate(sqlCommand, sql);

                }

                foreach (DataRow item in dtBatch.Rows)
                {
                    UpdateBatchByBatchUnique(item["BatchUnique"]);
                }
                IsSuccess = true;

            }
            catch (Exception ex)
            {
                BLogicLogger.Exception("CloseReceipt in ProcessForm", ex);
            }
        }

        public void HotFix_UpgradeTaxForTicket()
        {
            try
            {
                string sql = string.Empty;
                decimal oldTax = 7.75m;
                decimal newTax = 10.5m;
                DateTime startDate = new DateTime(2025, 10, 23, 0, 0, 0);
                DateTime endDate = new DateTime(2025, 10, 24, 23, 59, 59);
                DataRow[] drs = null;
                DataTable dtSaleReceiptUpdates = new DataTable("SaleReceipts");

                using (SqlCommand sqlCommand = new SqlCommand())
                using (SqlConnection conn = new SqlConnection(Helper.BLOGICPOS7_Connection))
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    sqlCommand.Connection = conn;
                    sqlCommand.CommandType = CommandType.Text;

                    drs = CurrentTableInDatabaseBLogicPOS7.Select("TABLE_NAME = 'SaleReceiptDetailTaxDetails'", "");
                    if (drs.Length > 0)
                    {
                        sql = $"select a.SaleReceiptID" +
                       $" from SaleReceipts a left join SaleReceiptDetailTaxDetails b on a.SaleReceiptID = b.SaleReceiptID " +
                       $" where b.TaxRate = {oldTax} and a.TransactionStatusID in ('FIN','CANR','VOID') " +
                       $" group by a.SaleReceiptID";
                    }
                    else
                    {
                        sql = $"select a.SaleReceiptID" +
                       $" from SaleReceipts a left join SaleReceiptDetails b on a.SaleReceiptID = b.SaleReceiptID " +
                       $" where b.TaxRate = {oldTax} and a.TransactionStatusID in ('FIN','CANR','VOID') " +
                       $" group by a.SaleReceiptID";
                    }

                    sqlCommand.CommandText = sql;
                    var reader = sqlCommand.ExecuteReader();
                    dtSaleReceiptUpdates.Load(reader);

                    reader.Close();
                }

                using (SqlCommand sqlCommand = new SqlCommand())
                using (SqlConnection conn = new SqlConnection(Helper.BLOGICPOS7_Connection))
                {
                    foreach (DataRow item in dtSaleReceiptUpdates.Rows)
                    {
                        DataTable dtSaleReceipts = new DataTable("SaleReceipts");
                        DataTable dtOLDSaleReceiptDetails = new DataTable("SaleReceiptDetails");
                        DataTable dtOLDSaleReceiptDetailTaxDetails = new DataTable("SaleReceiptDetailTaxDetails");
                        DataTable dtSaleReceiptDetails = new DataTable("SaleReceiptDetails");
                        DataTable dtSaleReceiptDetailTaxDetails = new DataTable("SaleReceiptDetailTaxDetails");

                        if (conn.State == ConnectionState.Closed)
                            conn.Open();
                        sqlCommand.Connection = conn;
                        sqlCommand.CommandType = CommandType.Text;

                        sql = $"select * " +
                            $" from SaleReceipts " +
                            $" where SaleReceiptID = '{item["SaleReceiptID"]}' ";

                        sqlCommand.CommandText = sql;
                        var reader = sqlCommand.ExecuteReader();
                        dtSaleReceipts.Load(reader);
                        reader.Close();

                        sql = $"select * " +
                            $" from SaleReceiptDetails " +
                            $" where SaleReceiptID = '{item["SaleReceiptID"]}' ";

                        sqlCommand.CommandText = sql;
                        reader = sqlCommand.ExecuteReader();
                        dtOLDSaleReceiptDetails.Load(reader);
                        reader.Close();

                        if (Utility.GetDecimalFromRow("TaxAmount", dtSaleReceipts.Rows[0]) == 0)
                        {
                            sql = $"update SaleReceiptDetails set TaxRate = '{newTax}' where TaxRate = '{oldTax}' and SaleReceiptID = '{item["SaleReceiptID"]}';";
                            RunQueryUpdate(sqlCommand, sql);

                            drs = CurrentTableInDatabaseBLogicPOS7.Select("TABLE_NAME = 'SaleReceiptDetailTaxDetails'", "");
                            if (drs.Length > 0)
                            {
                                sql = $"update SaleReceiptDetailTaxDetails set TaxRate = '{newTax}' where TaxRate = '{oldTax}' and SaleReceiptID = '{item["SaleReceiptID"]}';";
                                RunQueryUpdate(sqlCommand, sql);
                            }
                            continue;
                        }

                        sql = $"update SaleReceiptDetails set TaxRate = '{newTax}', TaxAmount = ROUND(CAST(ExtendedPrice AS decimal(19,4)) * {newTax} / 100, 2) where TaxRate = '{oldTax}' and SaleReceiptID = '{item["SaleReceiptID"]}';";
                        RunQueryUpdate(sqlCommand, sql);

                        sql = $"select * " +
                                $" from SaleReceiptDetails " +
                                $" where SaleReceiptID = '{item["SaleReceiptID"]}' and TaxAmount <> 0 and (Status = 0 or Status = 98) and IsCompItem = 0";

                        sqlCommand.CommandText = sql;
                        reader = sqlCommand.ExecuteReader();
                        dtSaleReceiptDetails.Load(reader);
                        reader.Close();

                        drs = CurrentTableInDatabaseBLogicPOS7.Select("TABLE_NAME = 'SaleReceiptDetailTaxDetails'", "");
                        if (drs.Length > 0)
                        {
                            sql = $"select * " +
                                $" from SaleReceiptDetailTaxDetails " +
                                $" where SaleReceiptID = '{item["SaleReceiptID"]}' ";

                            sqlCommand.CommandText = sql;
                            reader = sqlCommand.ExecuteReader();
                            dtOLDSaleReceiptDetailTaxDetails.Load(reader);
                            reader.Close();

                            sql = $"update SaleReceiptDetailTaxDetails set TaxRate = '{newTax}', TaxAmount = ROUND(CAST(ExtendedPrice AS decimal(19,4)) * {newTax} / 100, 2) where TaxRate = '{oldTax}' and SaleReceiptID = '{item["SaleReceiptID"]}';";
                            RunQueryUpdate(sqlCommand, sql);

                            sql = $"select * " +
                                $" from SaleReceiptDetailTaxDetails " +
                                $" where SaleReceiptID = '{item["SaleReceiptID"]}' and TaxAmount <> 0 and (Status = 0 or Status = 98) and IsCompItem = 0";

                            sqlCommand.CommandText = sql;
                            reader = sqlCommand.ExecuteReader();
                            dtSaleReceiptDetailTaxDetails.Load(reader);
                            reader.Close();

                            if (dtSaleReceiptDetailTaxDetails?.Rows.Count > 0)
                            {
                                decimal subTotal = Utility.GetDecimalFromObject(dtSaleReceiptDetailTaxDetails.Compute("SUM(ExtendedPrice)", ""));
                                decimal oldTotalTax = Utility.GetDecimalFromObject(dtSaleReceiptDetailTaxDetails.Compute("SUM(TaxAmount)", ""));
                                decimal newTotalTax = Math.Round(subTotal * newTax / 100, 2);

                                var balance = newTotalTax - oldTotalTax;
                                if (balance != 0)
                                {
                                    foreach (DataRow row in dtSaleReceiptDetailTaxDetails.Rows)
                                    {
                                        decimal taxAmount = Utility.GetDecimalFromObject(row["TaxAmount"]);
                                        if (taxAmount > balance)
                                        {
                                            row["TaxAmount"] = taxAmount + balance;

                                            sql = $"Update SaleReceiptDetailTaxDetails set TaxAmount = '{row["TaxAmount"]}' where SaleReceiptDetailTaxDetailID = '{row["SaleReceiptDetailTaxDetailID"]}';";

                                            RunQueryUpdate(sqlCommand, sql);

                                            break;

                                        }
                                    }
                                }

                                sql = string.Empty;
                                foreach (DataRow row in dtOLDSaleReceiptDetails.Rows)
                                {
                                    decimal oldItemTax = Utility.GetDecimalFromObject(row["TaxAmount"]);
                                    decimal oldExtendedPrice = Utility.GetDecimalFromObject(row["ExtendedPrice"]);
                                    decimal newItemTax = Utility.GetDecimalFromObject(dtSaleReceiptDetailTaxDetails.Compute("SUM(TaxAmount)", $"SaleReceiptDetailID = '{row["SaleReceiptDetailID"]}'"));

                                    balance = Math.Abs(oldItemTax - newItemTax);
                                    if (balance != 0)
                                    {
                                        row["ExtendedPrice"] = Utility.GetDecimalFromObject(row["ExtendedPrice"]) - balance;
                                        row["TaxAmount"] = newItemTax;
                                        row["DiscountCode"] = 1;
                                        row["ItemDiscountValue"] = Utility.GetDecimalFromObject(row["ItemDiscountValue"]) + balance;
                                        row["ItemDiscountType"] = 2;
                                        row["ItemdiscountRate"] = Math.Round(balance / (Utility.GetDecimalFromObject(row["Quantity"]) * Utility.GetDecimalFromObject(row["Price"])) * 100, 13);

                                        sql += $"Update SaleReceiptDetails set " +
                                            $" ExtendedPrice = {row["ExtendedPrice"]}, " +
                                            $" TaxAmount = '{row["TaxAmount"]}', " +
                                            $" DiscountCode = '{row["DiscountCode"]}', " +
                                            $" ItemDiscountType = '{row["ItemDiscountType"]}', " +
                                            $" ReasonCodeDiscountItem = '-105', " +
                                            $" ItemDiscountValue = '{row["ItemDiscountValue"]}', " +
                                            $" ItemdiscountRate = '{row["ItemdiscountRate"]}' " +
                                            $" where SaleReceiptDetailID = '{row["SaleReceiptDetailID"]}';";

                                        sql += $"Update SaleReceiptDetailTaxDetails set ExtendedPrice = {row["ExtendedPrice"]} where SaleReceiptDetailID = '{row["SaleReceiptDetailID"]}';";
                                    }

                                }
                                RunQueryUpdate(sqlCommand, sql);

                                var newDiscount = Utility.GetDecimalFromObject(dtOLDSaleReceiptDetails.Compute("SUM(ItemDiscountValue)", ""));
                                var oldDiscount = Utility.GetDecimalFromObject(dtSaleReceipts.Rows[0]["TotalDiscountAmount"]);
                                sql = $"update SaleReceipts set TaxAmount = '{newTotalTax}', ReturnCode = '-105', GlobalDiscountAmount = '{oldDiscount + newDiscount}', TotalDiscountAmount = '{oldDiscount + newDiscount}' where SaleReceiptID = '{dtSaleReceipts.Rows[0]["SaleReceiptID"]}';";
                                RunQueryUpdate(sqlCommand, sql);
                            }
                        }
                        else
                        {
                            if (dtSaleReceiptDetails?.Rows.Count > 0)
                            {
                                decimal subTotal = Utility.GetDecimalFromObject(dtSaleReceiptDetails.Compute("SUM(ExtendedPrice)", ""));
                                decimal oldTotalTax = Utility.GetDecimalFromObject(dtSaleReceiptDetails.Compute("SUM(TaxAmount)", ""));
                                decimal newTotalTax = Math.Round(subTotal * newTax / 100, 2);

                                var balance = newTotalTax - oldTotalTax;
                                if (balance != 0)
                                {
                                    foreach (DataRow row in dtSaleReceiptDetails.Rows)
                                    {
                                        decimal taxAmount = Utility.GetDecimalFromObject(row["TaxAmount"]);
                                        if (taxAmount > balance)
                                        {
                                            row["TaxAmount"] = taxAmount + balance;

                                            sql = $"Update SaleReceiptDetails set TaxAmount = '{row["TaxAmount"]}' where SaleReceiptDetailID = '{row["SaleReceiptDetailID"]}';";

                                            RunQueryUpdate(sqlCommand, sql);

                                            break;

                                        }
                                    }
                                }

                                sql = string.Empty;
                                foreach (DataRow row in dtOLDSaleReceiptDetails.Rows)
                                {
                                    decimal oldItemTax = Utility.GetDecimalFromObject(row["TaxAmount"]);
                                    decimal oldExtendedPrice = Utility.GetDecimalFromObject(row["ExtendedPrice"]);
                                    decimal newItemTax = Utility.GetDecimalFromObject(dtSaleReceiptDetails.Compute("SUM(TaxAmount)", $"SaleReceiptDetailID = '{row["SaleReceiptDetailID"]}'"));

                                    balance = Math.Abs(oldItemTax - newItemTax);
                                    if (balance != 0)
                                    {
                                        row["ExtendedPrice"] = Utility.GetDecimalFromObject(row["ExtendedPrice"]) - balance;
                                        row["TaxAmount"] = newItemTax;
                                        row["DiscountCode"] = 1;
                                        row["ItemDiscountValue"] = Utility.GetDecimalFromObject(row["ItemDiscountValue"]) + balance;
                                        row["ItemDiscountType"] = 2;
                                        row["ItemdiscountRate"] = Math.Round(balance / (Utility.GetDecimalFromObject(row["Quantity"]) * Utility.GetDecimalFromObject(row["Price"])) * 100, 13);

                                        sql += $"Update SaleReceiptDetails set " +
                                            $" ExtendedPrice = {row["ExtendedPrice"]}, " +
                                            $" TaxAmount = '{row["TaxAmount"]}', " +
                                            $" DiscountCode = '{row["DiscountCode"]}', " +
                                            $" ItemDiscountType = '{row["ItemDiscountType"]}', " +
                                            $" ReasonCodeDiscountItem = '-105', " +
                                            $" ItemDiscountValue = '{row["ItemDiscountValue"]}', " +
                                            $" ItemdiscountRate = '{row["ItemdiscountRate"]}' " +
                                            $" where SaleReceiptDetailID = '{row["SaleReceiptDetailID"]}';";
                                    }

                                }
                                RunQueryUpdate(sqlCommand, sql);

                                var newDiscount = Utility.GetDecimalFromObject(dtOLDSaleReceiptDetails.Compute("SUM(ItemDiscountValue)", ""));
                                var oldDiscount = Utility.GetDecimalFromObject(dtSaleReceipts.Rows[0]["TotalDiscountAmount"]);
                                sql = $"update SaleReceipts set TaxAmount = '{newTotalTax}', ReturnCode = '-105', GlobalDiscountAmount = '{oldDiscount + newDiscount}', TotalDiscountAmount = '{oldDiscount + newDiscount}' where SaleReceiptID = '{dtSaleReceipts.Rows[0]["SaleReceiptID"]}';";
                                RunQueryUpdate(sqlCommand, sql);
                            }
                        }
                    }

                    IsSuccess = true;
                }
            }
            catch (Exception ex)
            {
                BLogicLogger.Exception("HotFix_UpgradeTaxForTicket in ProcessForm", ex);
            }
        }

        public void HotFix_UpgradeTaxForTicket_v2()
        {
            try
            {
                string sql = string.Empty;
                decimal oldTax = 7.75m;
                decimal newTax = 10.5m;
                DateTime startDate = new DateTime(2025, 10, 23, 0, 0, 0);
                DateTime endDate = new DateTime(2025, 10, 24, 23, 59, 59);
                DataRow[] drs = null;
                DataTable dtSaleReceiptUpdates = new DataTable("SaleReceipts");

                using (SqlCommand sqlCommand = new SqlCommand())
                using (SqlConnection conn = new SqlConnection(Helper.BLOGICPOS7_Connection))
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    sqlCommand.Connection = conn;
                    sqlCommand.CommandType = CommandType.Text;

                    drs = CurrentTableInDatabaseBLogicPOS7.Select("TABLE_NAME = 'SaleReceiptDetailTaxDetails'", "");
                    if (drs.Length > 0)
                    {
                        sql = $"select a.SaleReceiptID" +
                       $" from SaleReceipts a left join SaleReceiptDetailTaxDetails b on a.SaleReceiptID = b.SaleReceiptID " +
                       $" where b.TaxRate = {oldTax} and a.TransactionStatusID in ('FIN','CANR','VOID') " +
                       $" group by a.SaleReceiptID";
                    }
                    else
                    {
                        sql = $"select a.SaleReceiptID" +
                       $" from SaleReceipts a left join SaleReceiptDetails b on a.SaleReceiptID = b.SaleReceiptID " +
                       $" where b.TaxRate = {oldTax} and a.TransactionStatusID in ('FIN','CANR','VOID') " +
                       $" group by a.SaleReceiptID";
                    }

                    sqlCommand.CommandText = sql;
                    var reader = sqlCommand.ExecuteReader();
                    dtSaleReceiptUpdates.Load(reader);

                    reader.Close();
                }

                using (SqlCommand sqlCommand = new SqlCommand())
                using (SqlConnection conn = new SqlConnection(Helper.BLOGICPOS7_Connection))
                {
                    foreach (DataRow item in dtSaleReceiptUpdates.Rows)
                    {
                        DataTable dtSaleReceipts = new DataTable("SaleReceipts");
                        DataTable dtOLDSaleReceiptDetails = new DataTable("SaleReceiptDetails");
                        DataTable dtOLDSaleReceiptDetailTaxDetails = new DataTable("SaleReceiptDetailTaxDetails");
                        DataTable dtSaleReceiptDetails = new DataTable("SaleReceiptDetails");
                        DataTable dtSaleReceiptDetailTaxDetails = new DataTable("SaleReceiptDetailTaxDetails");

                        if (conn.State == ConnectionState.Closed)
                            conn.Open();
                        sqlCommand.Connection = conn;
                        sqlCommand.CommandType = CommandType.Text;

                        sql = $"select * " +
                            $" from SaleReceipts " +
                            $" where SaleReceiptID = '{item["SaleReceiptID"]}' ";

                        sqlCommand.CommandText = sql;
                        var reader = sqlCommand.ExecuteReader();
                        dtSaleReceipts.Load(reader);
                        reader.Close();

                        sql = $"select * " +
                            $" from SaleReceiptDetails " +
                            $" where SaleReceiptID = '{item["SaleReceiptID"]}' ";

                        sqlCommand.CommandText = sql;
                        reader = sqlCommand.ExecuteReader();
                        dtOLDSaleReceiptDetails.Load(reader);
                        reader.Close();

                        sql = $"select * " +
                                $" from SaleReceiptDetailTaxDetails " +
                                $" where SaleReceiptID = '{item["SaleReceiptID"]}' ";

                        sqlCommand.CommandText = sql;
                        reader = sqlCommand.ExecuteReader();
                        dtOLDSaleReceiptDetailTaxDetails.Load(reader);
                        reader.Close();

                        if (Utility.GetDecimalFromRow("TaxAmount", dtSaleReceipts.Rows[0]) == 0)
                        {
                            sql = $"update SaleReceiptDetails set TaxRate = '{newTax}' where TaxRate = '{oldTax}' and SaleReceiptID = '{item["SaleReceiptID"]}';";
                            RunQueryUpdate(sqlCommand, sql);

                            drs = CurrentTableInDatabaseBLogicPOS7.Select("TABLE_NAME = 'SaleReceiptDetailTaxDetails'", "");
                            if (drs.Length > 0)
                            {
                                sql = $"update SaleReceiptDetailTaxDetails set TaxRate = '{newTax}' where TaxRate = '{oldTax}' and SaleReceiptID = '{item["SaleReceiptID"]}';";
                                RunQueryUpdate(sqlCommand, sql);
                            }
                            continue;
                        }

                        drs = CurrentTableInDatabaseBLogicPOS7.Select("TABLE_NAME = 'SaleReceiptDetailTaxDetails'", "");
                        if (drs.Length > 0)
                        {
                            sql = string.Empty;
                            foreach (DataRow row in dtOLDSaleReceiptDetailTaxDetails.Rows)
                            {
                                decimal extendedPrice = Utility.GetDecimalFromObject(row["ExtendedPrice"]);
                                decimal taxAmount = Utility.GetDecimalFromObject(row["TaxAmount"]);
                                decimal newExtendedPrice = Math.Round((extendedPrice + taxAmount) / 1.105m, 2);
                                decimal newTaxAmount = (extendedPrice + taxAmount) - newExtendedPrice;
                                decimal taxRate = Utility.GetDecimalFromObject(row["TaxRate"]);
                                decimal quantity = Utility.GetDecimalFromObject(row["Quantity"]);
                                decimal price = Utility.GetDecimalFromObject(row["Price"]);
                                decimal newPrice = price - ((newTaxAmount - taxAmount) / quantity);

                                if (taxRate == newTax)
                                    continue;

                                if (taxAmount == 0)
                                {
                                    sql += $"update SaleReceiptDetailTaxDetails set TaxRate = '{newTax}' where SaleReceiptDetailTaxDetailID = '{row["SaleReceiptDetailTaxDetailID"]}'";
                                    continue;
                                }

                                row["ExtendedPrice"] = newExtendedPrice;
                                row["TaxAmount"] = newTaxAmount;

                                sql += $"update SaleReceiptDetailTaxDetails set Price = '{newPrice}', ExtendedPrice = '{newExtendedPrice}', TaxAmount = '{newTaxAmount}', TaxRate = '{newTax}' where SaleReceiptDetailTaxDetailID = '{row["SaleReceiptDetailTaxDetailID"]}'";
                            }
                            RunQueryUpdate(sqlCommand, sql);

                            sql = string.Empty;
                            foreach (DataRow row in dtOLDSaleReceiptDetails.Rows)
                            {

                                decimal extendedPrice = Utility.GetDecimalFromObject(row["ExtendedPrice"]);
                                decimal taxAmount = Utility.GetDecimalFromObject(row["TaxAmount"]);
                                decimal newTaxAmount = Utility.GetDecimalFromObject(dtOLDSaleReceiptDetailTaxDetails.Compute("SUM(TaxAmount)", $"SaleReceiptDetailID = '{row["SaleReceiptDetailID"]}'"));
                                decimal newExtendedPrice = (extendedPrice + taxAmount) - newTaxAmount;
                                decimal quantity = Utility.GetDecimalFromObject(row["Quantity"]);
                                decimal price = Utility.GetDecimalFromObject(row["Price"]);
                                decimal newPrice = price - ((newTaxAmount - taxAmount) / quantity);

                                if (taxAmount == 0)
                                {
                                    sql += $"update SaleReceiptDetails set TaxRate = '{newTax}' where SaleReceiptDetailID = '{row["SaleReceiptDetailID"]}'";
                                    continue;
                                }

                                row["ExtendedPrice"] = newExtendedPrice;
                                row["TaxAmount"] = newTaxAmount;

                                sql += $"update SaleReceiptDetails set Price = '{newPrice}', RegularPrice = '{newPrice}', DualPrice = '{newPrice}', PriceCash = '{newPrice}', PriceCC = '{newPrice}', ExtendedPrice = '{newExtendedPrice}', TaxAmount = '{newTaxAmount}', TaxRate = '{newTax}' where SaleReceiptDetailID = '{row["SaleReceiptDetailID"]}'";
                            }
                            RunQueryUpdate(sqlCommand, sql);

                            object newTotalTax = dtOLDSaleReceiptDetailTaxDetails.Compute("SUM(TaxAmount)", "");
                            sql = $"update SaleReceipts set TaxAmount = '{newTotalTax}' where SaleReceiptID = '{item["SaleReceiptID"]}';";
                            RunQueryUpdate(sqlCommand, sql);
                        }
                        else
                        {
                            IsSuccess = false;
                            return;
                        }
                    }

                    IsSuccess = true;
                }
            }
            catch (Exception ex)
            {
                BLogicLogger.Exception("HotFix_UpgradeTaxForTicket in ProcessForm", ex);
            }
        }

        public void HotFix_UpgradeTaxForTicket_v3()
        {
            try
            {
                string sql = string.Empty;
                decimal oldTax = 7.75m;
                decimal newTax = 10.5m;
                DateTime startDate = new DateTime(2025, 10, 23, 0, 0, 0);
                DateTime endDate = new DateTime(2025, 10, 24, 23, 59, 59);
                DataRow[] drs = null;
                DataTable dtSaleReceiptUpdates = new DataTable("SaleReceipts");

                using (SqlCommand sqlCommand = new SqlCommand())
                using (SqlConnection conn = new SqlConnection(Helper.BLOGICPOS7_Connection))
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    sqlCommand.Connection = conn;
                    sqlCommand.CommandType = CommandType.Text;

                    drs = CurrentTableInDatabaseBLogicPOS7.Select("TABLE_NAME = 'SaleReceiptDetailTaxDetails'", "");
                    if (drs.Length > 0)
                    {
                        sql = $"select a.SaleReceiptID" +
                       $" from SaleReceipts a left join SaleReceiptDetailTaxDetails b on a.SaleReceiptID = b.SaleReceiptID " +
                       $" where b.TaxRate = {oldTax} and a.TransactionStatusID in ('FIN','CANR','VOID') " +
                       $" group by a.SaleReceiptID";
                    }
                    else
                    {
                        sql = $"select a.SaleReceiptID" +
                       $" from SaleReceipts a left join SaleReceiptDetails b on a.SaleReceiptID = b.SaleReceiptID " +
                       $" where b.TaxRate = {oldTax} and a.TransactionStatusID in ('FIN','CANR','VOID') " +
                       $" group by a.SaleReceiptID";
                    }

                    sqlCommand.CommandText = sql;
                    var reader = sqlCommand.ExecuteReader();
                    dtSaleReceiptUpdates.Load(reader);

                    reader.Close();
                }

                using (SqlCommand sqlCommand = new SqlCommand())
                using (SqlConnection conn = new SqlConnection(Helper.BLOGICPOS7_Connection))
                {
                    foreach (DataRow item in dtSaleReceiptUpdates.Rows)
                    {
                        DataTable dtSaleReceipts = new DataTable("SaleReceipts");
                        DataTable dtOLDSaleReceiptDetails = new DataTable("SaleReceiptDetails");
                        DataTable dtOLDSaleReceiptDetailTaxDetails = new DataTable("SaleReceiptDetailTaxDetails");
                        DataTable dtSaleReceiptDetails = new DataTable("SaleReceiptDetails");
                        DataTable dtSaleReceiptDetailTaxDetails = new DataTable("SaleReceiptDetailTaxDetails");

                        if (conn.State == ConnectionState.Closed)
                            conn.Open();
                        sqlCommand.Connection = conn;
                        sqlCommand.CommandType = CommandType.Text;

                        sql = $"select * " +
                            $" from SaleReceipts " +
                            $" where SaleReceiptID = '{item["SaleReceiptID"]}' ";

                        sqlCommand.CommandText = sql;
                        var reader = sqlCommand.ExecuteReader();
                        dtSaleReceipts.Load(reader);
                        reader.Close();

                        sql = $"select * " +
                            $" from SaleReceiptDetails " +
                            $" where SaleReceiptID = '{item["SaleReceiptID"]}' ";

                        sqlCommand.CommandText = sql;
                        reader = sqlCommand.ExecuteReader();
                        dtOLDSaleReceiptDetails.Load(reader);
                        reader.Close();

                        sql = $"select * " +
                                $" from SaleReceiptDetailTaxDetails " +
                                $" where SaleReceiptID = '{item["SaleReceiptID"]}' ";

                        sqlCommand.CommandText = sql;
                        reader = sqlCommand.ExecuteReader();
                        dtOLDSaleReceiptDetailTaxDetails.Load(reader);
                        reader.Close();

                        if (Utility.GetDecimalFromRow("TaxAmount", dtSaleReceipts.Rows[0]) == 0)
                        {
                            sql = $"update SaleReceiptDetails set TaxRate = '{newTax}' where TaxRate = '{oldTax}' and SaleReceiptID = '{item["SaleReceiptID"]}';";
                            RunQueryUpdate(sqlCommand, sql);

                            drs = CurrentTableInDatabaseBLogicPOS7.Select("TABLE_NAME = 'SaleReceiptDetailTaxDetails'", "");
                            if (drs.Length > 0)
                            {
                                sql = $"update SaleReceiptDetailTaxDetails set TaxRate = '{newTax}' where TaxRate = '{oldTax}' and SaleReceiptID = '{item["SaleReceiptID"]}';";
                                RunQueryUpdate(sqlCommand, sql);
                            }
                            continue;
                        }

                        sql = $"update SaleReceiptDetailTaxDetails set TaxRate = '{newTax}', TaxAmount = ROUND(CAST(ExtendedPrice AS decimal(19,4)) * {newTax} / 100, 2) where TaxRate = '{oldTax}' and SaleReceiptID = '{item["SaleReceiptID"]}';";
                        RunQueryUpdate(sqlCommand, sql);

                        sql = $"select * " +
                                $" from SaleReceiptDetailTaxDetails " +
                                $" where SaleReceiptID = '{item["SaleReceiptID"]}' ";

                        sqlCommand.CommandText = sql;
                        reader = sqlCommand.ExecuteReader();
                        dtSaleReceiptDetailTaxDetails.Load(reader);
                        reader.Close();

                        decimal subTotal = Utility.GetDecimalFromObject(dtSaleReceiptDetailTaxDetails.Compute("SUM(ExtendedPrice)", ""));
                        decimal oldTotalTax = Utility.GetDecimalFromObject(dtSaleReceiptDetailTaxDetails.Compute("SUM(TaxAmount)", ""));
                        decimal newTotalTax = Math.Round(subTotal * newTax / 100, 2);

                        var balance = newTotalTax - oldTotalTax;
                        if (balance != 0)
                        {
                            foreach (DataRow row in dtSaleReceiptDetailTaxDetails.Rows)
                            {
                                decimal taxAmount = Utility.GetDecimalFromObject(row["TaxAmount"]);
                                if (taxAmount > balance)
                                {
                                    row["TaxAmount"] = taxAmount + balance;

                                    sql = $"Update SaleReceiptDetailTaxDetails set TaxAmount = '{row["TaxAmount"]}' where SaleReceiptDetailTaxDetailID = '{row["SaleReceiptDetailTaxDetailID"]}';";

                                    RunQueryUpdate(sqlCommand, sql);

                                    break;

                                }
                            }
                        }

                        sql = string.Empty;
                        foreach (DataRow row in dtOLDSaleReceiptDetails.Rows)
                        {
                            decimal oldItemTax = Utility.GetDecimalFromObject(row["TaxAmount"]);
                            decimal oldExtendedPrice = Utility.GetDecimalFromObject(row["ExtendedPrice"]);
                            decimal newItemTax = Utility.GetDecimalFromObject(dtSaleReceiptDetailTaxDetails.Compute("SUM(TaxAmount)", $"SaleReceiptDetailID = '{row["SaleReceiptDetailID"]}'"));

                            balance = Math.Abs(oldItemTax - newItemTax);
                            if (balance != 0)
                            {
                                decimal ItemDiscountValue = Utility.GetDecimalFromObject(row["ItemDiscountValue"]) + balance;

                                row["ExtendedPrice"] = Utility.GetDecimalFromObject(row["ExtendedPrice"]) - balance;
                                row["TaxAmount"] = newItemTax;
                                row["DiscountCode"] = 1;
                                row["ItemDiscountValue"] = ItemDiscountValue;
                                row["ItemDiscountType"] = 2;
                                row["ItemdiscountRate"] = Math.Round(ItemDiscountValue / (Utility.GetDecimalFromObject(row["Quantity"]) * Utility.GetDecimalFromObject(row["Price"])) * 100, 13);

                                sql += $"Update SaleReceiptDetails set " +
                                    $" ExtendedPrice = {row["ExtendedPrice"]}, " +
                                    $" TaxAmount = '{row["TaxAmount"]}', " +
                                    $" DiscountCode = '{row["DiscountCode"]}', " +
                                    $" ItemDiscountType = '{row["ItemDiscountType"]}', " +
                                    $" ReasonCodeDiscountItem = '-105', " +
                                    $" ItemDiscountValue = '{row["ItemDiscountValue"]}', " +
                                    $" ItemdiscountRate = '{row["ItemdiscountRate"]}' " +
                                    $" where SaleReceiptDetailID = '{row["SaleReceiptDetailID"]}';";

                                sql += $"Update SaleReceiptDetailTaxDetails set ExtendedPrice = {row["ExtendedPrice"]} where SaleReceiptDetailID = '{row["SaleReceiptDetailID"]}';";
                            }

                        }
                        RunQueryUpdate(sqlCommand, sql);

                        var newDiscount = Utility.GetDecimalFromObject(dtOLDSaleReceiptDetails.Compute("SUM(ItemDiscountValue)", ""));
                        var oldDiscount = Utility.GetDecimalFromObject(dtSaleReceipts.Rows[0]["TotalDiscountAmount"]);
                        sql = $"update SaleReceipts set TaxAmount = '{newTotalTax}', ReturnCode = '-105', GlobalDiscountAmount = '{oldDiscount + newDiscount}', TotalDiscountAmount = '{oldDiscount + newDiscount}' where SaleReceiptID = '{dtSaleReceipts.Rows[0]["SaleReceiptID"]}';";
                        RunQueryUpdate(sqlCommand, sql);
                    }

                    IsSuccess = true;
                }
            }
            catch (Exception ex)
            {
                BLogicLogger.Exception("HotFix_UpgradeTaxForTicket in ProcessForm", ex);
            }
        }

        public void HotFix_UpdownTaxForTicket()
        {
            try
            {

            }
            catch (Exception ex)
            {
                BLogicLogger.Exception("HotFix_UpdownTaxForTicket in ProcessForm", ex);
            }
        }


        private void GetListTableInDatabaseBLogicPOS7()
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
                    ExistServiceFeeTaxAmountInSaleReceipts = dtSaleReceipts.Select("COLUMN_NAME = 'ServiceFeeTaxAmount'", "")?.Length > 0;
                }
            }
            catch (Exception ex)
            {
                BLogicLogger.Exception("GetListTableInDatabaseBLogicPOS7", ex);
            }
        }

        private void ReSyncItemType()
        {
            try
            {
                DataTable dtItems = new DataTable("Tables");

                using (SqlCommand sqlCommand = new SqlCommand())
                using (var conn = new SqlConnection(Helper.BLOGICPOS7_Connection))
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();

                    sqlCommand.Connection = conn;
                    sqlCommand.CommandType = CommandType.Text;

                    sqlCommand.CommandText = $"select distinct a.GroupID, c.ItemGroupID from SaleReceiptDetails a left join SaleReceipts b on a.SaleReceiptID = b.SaleReceiptID left join Items c on a.ItemID = c.ItemID" +
                        $" where(a.PriceInitial is null or a.PriceInitial = 'P') and a.ItemGroupID = '0' and b.StartDate >= '{Item_StartDate}' and b.StartDate <= '{Item_EndDate}' ";
                    var reader = sqlCommand.ExecuteReader();
                    dtItems.Load(reader);
                    reader.Close();

                    foreach (DataRow item in dtItems.Rows)
                    {
                        RunQueryUpdate(sqlCommand, $"update SaleReceiptDetails set ItemGroupID = '{item["ItemGroupID"]}' where GroupID = '{item["GroupID"]}'");
                    }

                    IsSuccess = true;
                }
            }
            catch (Exception ex)
            {
                BLogicLogger.Exception("GetListTableInDatabaseBLogicPOS7", ex);
            }
        }

        private void EmployeePaycor_GetEmployees()
        {
            DataRow[] drs = null;
            DataTable dtEmployees = new DataTable("Employees");
            DataTable dtRoles = new DataTable("Roles");
            DataTable dtEmployeeRoles = new DataTable("EmployeeRoles");
            try
            {
                using (SqlCommand sqlCommand = new SqlCommand())
                using (SqlConnection conn = new SqlConnection(Helper.BLOGICPOS7_Connection))
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    sqlCommand.Connection = conn;
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.CommandText = "select EmployeeID, EmployeeCode, TRIM( EmployeeFirstName) + ' ' +  TRIM(EmployeeLastName) as EmployeeName, PaycorEmployeeID from Employees where EmployeeID > 0 and Enable = 0 order by EmployeeFirstName";

                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    dtEmployees.Load(reader);
                    reader.Close();

                    sqlCommand.CommandText = "select RolesID, RolesName from Roles where IsDeleted = '0' or IsDeleted is null";
                    reader = sqlCommand.ExecuteReader();
                    dtRoles.Load(reader);
                    reader.Close();

                    sqlCommand.CommandText = "select * from EmployeeRoles where IsDeleted = '0' or IsDeleted is null";
                    reader = sqlCommand.ExecuteReader();
                    dtEmployeeRoles.Load(reader);
                    reader.Close();

                    dtEmployees.Columns.Add("RoleName", typeof(string));
                    foreach (DataRow item in dtEmployees.Rows)
                    {
                        drs = dtEmployeeRoles.Select($"EmployeeID = '{Utility.GetIntFromRow("EmployeeID", item)}'", "");
                        if (drs.Length > 0)
                        {
                            List<string> roleNames = new List<string>();
                            foreach (DataRow dr in drs)
                            {
                                int roleID = Utility.GetIntFromRow("RolesID", dr);
                                DataRow[] roleRows = dtRoles.Select($"RolesID = '{roleID}'", "");
                                if (roleRows.Length > 0)
                                {
                                    roleNames.Add(Utility.GetStringFromRow("RolesName", roleRows[0]));
                                }
                            }
                            item["RoleName"] = string.Join(", ", roleNames);
                        }
                    }

                    DataSetReturn = new DataSet();
                    DataSetReturn.Tables.Add(dtEmployees);
                    DataSetReturn.Tables.Add(dtRoles);
                    DataSetReturn.Tables.Add(dtEmployeeRoles);

                    drs = CurrentTableInDatabaseBLogicPOS7.Select("TABLE_NAME = 'PaycorEmployees'", "");
                    if (drs.Length > 0)
                    {
                        DataTable dtPaycorEmployees = new DataTable("PaycorEmployees");
                        sqlCommand.CommandText = "select * , FirstName + ' ' + LastName as EmployeeName from PaycorEmployees";
                        reader = sqlCommand.ExecuteReader();
                        dtPaycorEmployees.Load(reader);
                        reader.Close();
                        DataSetReturn.Tables.Add(dtPaycorEmployees);
                    }

                    IsSuccess = true;
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = "Error retrieving Employees data from Blogicpos7.";
                BLogicLogger.Exception("EmployeePaycor_GetEmployees", ex);
            }
        }

        private void EmployeePaycor_GetEmployeesFromPaycor()
        {
            try
            {
                DataTable dtEmployees = new DataTable("Employees");

                using (SqlCommand sqlCommand = new SqlCommand())
                using (var conn = new SqlConnection(Helper.BLOGICPOS7_Connection))
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();

                    sqlCommand.Connection = conn;
                    sqlCommand.CommandType = CommandType.Text;

                    sqlCommand.CommandText = "select * from Employees where EmployeeID > 0 and Enable = 0 order by EmployeeFirstName desc";
                    var reader = sqlCommand.ExecuteReader();
                    dtEmployees.Load(reader);
                    reader.Close();

                    RunQueryUpdate(sqlCommand, "delete from PaycorEmployees");
                    RunQueryUpdate(sqlCommand, "update Employees set PaycorEmployeeID = ''");

                    string sql = $"select * from Configurations where ConfigurationName in (" +
                    $"'SystemConfigValue.PaycorRefreshToken'," +
                    $"'SystemConfigValue.PaycorClientID'," +
                    $"'SystemConfigValue.PaycorClientSecret'," +
                    $"'SystemConfigValue.PaycorAPImSubscriptionKey'," +
                    $"'SystemConfigValue.PaycorLegalEntityID'," +
                    $"'SystemConfigValue.EnableStream'," +
                    $"'SystemConfigValue.SevenShiftAccessToken'" +
                    $")";

                    sqlCommand.CommandText = sql;
                    reader = sqlCommand.ExecuteReader();
                    DataTable dtConfigurations = new DataTable("Configurations");
                    dtConfigurations.Load(reader);
                    reader.Close();

                    foreach (DataRow dr in dtConfigurations.Rows)
                    {
                        string name = Utility.GetStringFromRow("ConfigurationName", dr);
                        switch (name)
                        {
                            case "SystemConfigValue.PaycorRefreshToken":
                                Helper.PaycorRefreshToken = Utility.GetStringFromRow("ConfigurationValue", dr);
                                break;
                            case "SystemConfigValue.PaycorClientID":
                                Helper.PaycorClientID = Utility.GetStringFromRow("ConfigurationValue", dr);
                                break;
                            case "SystemConfigValue.PaycorClientSecret":
                                Helper.PaycorClientSecret = Utility.GetStringFromRow("ConfigurationValue", dr);
                                break;
                            case "SystemConfigValue.PaycorAPImSubscriptionKey":
                                Helper.PaycorAPImSubscriptionKey = Utility.GetStringFromRow("ConfigurationValue", dr);
                                break;
                            case "SystemConfigValue.PaycorLegalEntityID":
                                Helper.PaycorLegalEntityID = Utility.GetStringFromRow("ConfigurationValue", dr);
                                break;
                            default:
                                break;
                        }
                    }

                    HttpClientFactory factory = new HttpClientFactory();
                    bool resultGenerateAccessToken = factory.GeneratePaycorAccessToken().Result;

                    if (!resultGenerateAccessToken || string.IsNullOrWhiteSpace(Helper.PaycorAccessToken))
                    {
                        BLogicLogger.Error("=> Generate Access Token from Paycor Error -> Close Application ");
                        return;
                    }

                    try
                    {
                        BLogicLogger.Debug("=> Get Employee info from Paycor & Update to local parameter");

                        var urlParam = $"/v1/legalEntities/{Helper.PaycorLegalEntityID}/employees?include=Position";

                    RetryGetEmployee:
                        var paycorEmployees = factory.GetPaycorEmployees(urlParam).Result;

                        if (paycorEmployees?.records.Count > 0)
                        {
                            foreach (var employee in paycorEmployees.records)
                            {
                                DataRow[] drs = dtEmployees.Select($"EmployeeCode = '{employee.employeeNumber}' and isnull(PaycorEmployeeID,'') = ''", "");
                                if (drs.Length > 0)
                                {
                                    drs[0]["PaycorEmployeeID"] = employee.id;

                                    RunQueryUpdate(sqlCommand, $"Update Employees set PaycorEmployeeID = '{employee.id}' where EmployeeID = '{drs[0]["EmployeeID"]}'");
                                }

                                RunQueryUpdate(sqlCommand, $"insert into PaycorEmployees (PaycorEmployeeID,EmployeeNumber,AlternateEmployeeNumber,BadgeNumber,FirstName,LastName, JobTitle) " +
                                    $"values ('{employee.id}', '{employee.employeeNumber}', '{employee.alternateEmployeeNumber}', '{employee.badgeNumber}', '{employee.firstName}', '{employee.lastName}', '{employee.positionData?.jobTitle}' );");
                            }

                            if (paycorEmployees.hasMoreResults && !string.IsNullOrWhiteSpace(paycorEmployees.additionalResultsUrl))
                            {
                                urlParam = paycorEmployees.additionalResultsUrl;
                                goto RetryGetEmployee;
                            }
                        }
                        else
                        {
                            BLogicLogger.Error("=> Get Employees info from Paycor -> Return NULL or Empty data");
                            BLogicLogger.Error($"   URL Params: {urlParam}");
                        }

                    }
                    catch (Exception ex)
                    {
                        BLogicLogger.Error("Get EmployeeInfo From Paycor in FormMain", ex);
                    }

                    try
                    {
                        BLogicLogger.Debug("=> Get Employee info from Paycor & Update to local parameter");

                        var urlParam = $"/v1/legalEntities/{Helper.PaycorLegalEntityID}/employees?include=Position";

                    RetryGetEmployee2:
                        var paycorEmployees = factory.GetPaycorEmployees(urlParam).Result;

                        if (paycorEmployees?.records.Count > 0)
                        {
                            foreach (var employee in paycorEmployees.records)
                            {
                                DataRow[] drs = dtEmployees.Select($"EmployeeCode = '{employee.alternateEmployeeNumber}' and isnull(PaycorEmployeeID,'') = ''", "");
                                if (drs.Length > 0)
                                {
                                    drs[0]["PaycorEmployeeID"] = employee.id;

                                    RunQueryUpdate(sqlCommand, $"Update Employees set PaycorEmployeeID = '{employee.id}' where EmployeeID = '{drs[0]["EmployeeID"]}'");
                                }
                            }

                            if (paycorEmployees.hasMoreResults && !string.IsNullOrWhiteSpace(paycorEmployees.additionalResultsUrl))
                            {
                                urlParam = paycorEmployees.additionalResultsUrl;
                                goto RetryGetEmployee2;
                            }
                        }
                        else
                        {
                            BLogicLogger.Error("=> Get Employees info from Paycor -> Return NULL or Empty data");
                            BLogicLogger.Error($"   URL Params: {urlParam}");
                        }

                    }
                    catch (Exception ex)
                    {
                        BLogicLogger.Error("Get EmployeeInfo From Paycor in FormMain", ex);
                    }
                }

                IsSuccess = true;
            }
            catch (Exception ex)
            {
                BLogicLogger.Exception("GetListTableInDatabaseBLogicPOS7", ex);
            }
        }

        public void RefundFullAmount()
        {
            try
            {
                DataTable dtEmployee = new DataTable("Employees");
                DataTable dtRoles = new DataTable("Roles");

                DataTable dtBatch = new DataTable("TransactionDetail");

                using (SqlCommand sqlCommand = new SqlCommand())
                using (SqlConnection conn = new SqlConnection(Helper.BLOGICPOS7_Connection))
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    sqlCommand.Connection = conn;
                    sqlCommand.CommandType = CommandType.Text;

                    var sql = $"update SaleReceipts set TransactionStatusID = 'CLO' where SaleReceiptID = '{SaleReceiptID}'; ";

                    RunQueryUpdate(sqlCommand, sql);

                    sql = $"delete from PaymentRoleDetails SaleReceiptID = '{SaleReceiptID}'; ";

                    RunQueryUpdate(sqlCommand, sql);
                }

                using (SqlCommand sqlCommand = new SqlCommand())
                using (SqlConnection conn = new SqlConnection(Helper.Merchant_Connection))
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();

                    sqlCommand.Connection = conn;
                    sqlCommand.CommandType = CommandType.Text;

                    var sql = $"select distinct BatchUnique from TransactionDetail where ISNULL(BatchUnique, '') <> '' and " +
                        $" ID in (select CreditCardTransactionID from {Helper.BLOGICPOS7_Name}.dbo.SaleReceiptPaymentDetails a where a.SaleReceiptID = '{SaleReceiptID}' and ISNULL(CreditCardTransactionID, '') <> '')";
                    sqlCommand.CommandText = sql;
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    dtBatch.Load(reader);
                    reader.Close();

                    sql = $"update TransactionDetail set BatchStatus = 'Closed', BatchUnique = '{Guid.Empty}' " +
                        $"where ID in (select CreditCardTransactionID from {Helper.BLOGICPOS7_Name}.dbo.SaleReceiptPaymentDetails a where a.SaleReceiptID = '{SaleReceiptID}' and ISNULL(CreditCardTransactionID, '') <> '')";

                    RunQueryUpdate(sqlCommand, sql);

                }

                foreach (DataRow item in dtBatch.Rows)
                {
                    UpdateBatchByBatchUnique(item["BatchUnique"]);
                }
                IsSuccess = true;

            }
            catch (Exception ex)
            {
                BLogicLogger.Exception("CloseReceipt in ProcessForm", ex);
            }
        }

        public void RefundReceipt(string _saleReceiptID)
        {
            try
            {
                string sql = string.Empty;
                DataTable dtSaleReceiptPaymentDetails = new DataTable("Employees");
                DataTable dtRoles = new DataTable("Roles");

                using (SqlCommand sqlCommand = new SqlCommand())
                using (SqlConnection conn = new SqlConnection(Helper.BLOGICPOS7_Connection))
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    sqlCommand.Connection = conn;
                    sqlCommand.CommandType = CommandType.Text;

                    sql = $" update SaleReceipts set TransactionStatusID = 'VOID', ReturnCode = '-115' where SaleReceiptID = '{_saleReceiptID}'; ";

                    RunQueryUpdate(sqlCommand, sql);

                    DataRow[] drs = CurrentTableInDatabaseBLogicPOS7.Select("TABLE_NAME = 'PaymentRoleDetails'", "");
                    if (drs.Length > 0)
                    {
                        sql = $"delete from PaymentRoleDetails where SaleReceiptID = '{_saleReceiptID}'; ";

                        RunQueryUpdate(sqlCommand, sql);
                    }

                    sqlCommand.CommandText = $"select * from SaleReceiptPaymentDetails where SaleReceiptID = '{_saleReceiptID}'";
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    dtSaleReceiptPaymentDetails.Load(reader);
                    reader.Close();
                }

                using (SqlCommand sqlCommand = new SqlCommand())
                using (SqlConnection conn = new SqlConnection(Helper.Merchant_Connection))
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();

                    sqlCommand.Connection = conn;
                    sqlCommand.CommandType = CommandType.Text;

                    foreach (DataRow item in dtSaleReceiptPaymentDetails.Rows)
                    {
                        if (Utility.GetIntFromRow("PaymentMethodID", item) == 2 &&
                            Utility.GetStringFromRow("CreditCardTransactionType", item).Contains("Sale"))
                        {
                            DataTable dtTransactionDetail = new DataTable("TransactionDetail");
                            sqlCommand.CommandText = $"select * from TransactionDetail where ID = '{item["CreditCardTransactionID"]}'";
                            var readerMerchant = sqlCommand.ExecuteReader();
                            dtTransactionDetail.Load(readerMerchant);
                            readerMerchant.Close();

                            if (dtTransactionDetail?.Rows.Count > 0)
                            {
                                sql = $"update MERCHANT.dbo.TransactionDetail set TranStatus = 'VOIDED' where ID = '{dtTransactionDetail.Rows[0]["ID"]}'";
                                RunQueryUpdate(sqlCommand, sql);

                                sql = GetSqlInsert(dtTransactionDetail);
                                SqlParameter[] parparms = GetCreateParameters(dtTransactionDetail);

                                int colCount = dtTransactionDetail.Columns.Count;

                                for (int i = 0; i < colCount; i++)
                                {
                                    if (i == 0)
                                        parparms[i].Value = Guid.NewGuid().ToString();
                                    else if (dtTransactionDetail.Columns[i].ColumnName == "TranType")
                                        parparms[i].Value = "VoidSale";
                                    else
                                        parparms[i].Value = dtTransactionDetail.Rows[0][i];
                                }

                                RunQueryUpdate(sqlCommand, sql, parparms);

                                if (!string.IsNullOrWhiteSpace(Utility.GetStringFromRow("BatchUnique", dtTransactionDetail.Rows[0])))
                                    UpdateBatchByBatchUnique(dtTransactionDetail.Rows[0]["BatchUnique"]);
                            }
                        }
                    }
                }

                IsSuccess = true;

            }
            catch (Exception ex)
            {
                BLogicLogger.Exception("CloseReceipt in ProcessForm", ex);
            }
        }

        public void VoidReceipt()
        {
            try
            {
                string sql = string.Empty;
                DataTable dtSaleReceiptPaymentDetails = new DataTable("Employees");
                DataTable dtRoles = new DataTable("Roles");

                using (SqlCommand sqlCommand = new SqlCommand())
                using (SqlConnection conn = new SqlConnection(Helper.BLOGICPOS7_Connection))
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    sqlCommand.Connection = conn;
                    sqlCommand.CommandType = CommandType.Text;

                    sql = $" update SaleReceipts set TransactionStatusID = 'VOID', ReturnCode = '-115' where SaleReceiptID = '{SaleReceiptID}'; ";

                    RunQueryUpdate(sqlCommand, sql);
                }

                VoidTransactionDetail();

                IsSuccess = true;

            }
            catch (Exception ex)
            {
                BLogicLogger.Exception("CloseReceipt in ProcessForm", ex);
            }
        }

        public void CancelReceipt()
        {
            try
            {
                string sql = string.Empty;
                DataTable dtSaleReceiptPaymentDetails = new DataTable("SaleReceiptPaymentDetails");

                using (SqlCommand sqlCommand = new SqlCommand())
                using (SqlConnection conn = new SqlConnection(Helper.BLOGICPOS7_Connection))
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    sqlCommand.Connection = conn;
                    sqlCommand.CommandType = CommandType.Text;

                    sql = $" update SaleReceipts set TransactionStatusID = 'CANR', ReturnCode = '-106' where SaleReceiptID = '{SaleReceiptID}'; ";

                    RunQueryUpdate(sqlCommand, sql);
                }

                VoidTransactionDetail();

                using (SqlCommand sqlCommand = new SqlCommand())
                using (SqlConnection conn = new SqlConnection(Helper.BLOGICPOS7_Connection))
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    sqlCommand.Connection = conn;
                    sqlCommand.CommandType = CommandType.Text;

                    foreach (DataRow item in dtSaleReceiptPaymentDetails.Rows)
                    {
                        sql = $"delete from SaleReceiptPaymentDetails where SaleReceiptPaymentDetailID = '{item["SaleReceiptPaymentDetailID"]}'";
                        RunQueryUpdate(sqlCommand, sql);
                    }
                }

                IsSuccess = true;

            }
            catch (Exception ex)
            {
                BLogicLogger.Exception("CloseReceipt in ProcessForm", ex);
            }
        }

        private void VoidTransactionDetail()
        {
            string sql = string.Empty;

            DataTable dtSaleReceiptPaymentDetails = new DataTable("SaleReceiptPaymentDetails");

            using (SqlCommand sqlCommand = new SqlCommand())
            using (SqlConnection conn = new SqlConnection(Helper.BLOGICPOS7_Connection))
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                sqlCommand.Connection = conn;
                sqlCommand.CommandType = CommandType.Text;

                DataRow[] drs = CurrentTableInDatabaseBLogicPOS7.Select("TABLE_NAME = 'PaymentRoleDetails'", "");
                if (drs.Length > 0)
                {
                    sql = $"delete from PaymentRoleDetails where SaleReceiptID = '{SaleReceiptID}'; ";

                    RunQueryUpdate(sqlCommand, sql);
                }

                sqlCommand.CommandText = $"select * from SaleReceiptPaymentDetails where SaleReceiptID = '{SaleReceiptID}'";
                SqlDataReader reader = sqlCommand.ExecuteReader();
                dtSaleReceiptPaymentDetails.Load(reader);
                reader.Close();
            }

            using (SqlCommand sqlCommand = new SqlCommand())
            using (SqlConnection conn = new SqlConnection(Helper.Merchant_Connection))
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();

                sqlCommand.Connection = conn;
                sqlCommand.CommandType = CommandType.Text;

                foreach (DataRow item in dtSaleReceiptPaymentDetails.Rows)
                {
                    if (Utility.GetIntFromRow("PaymentMethodID", item) == 2 &&
                        Utility.GetStringFromRow("CreditCardTransactionType", item).Contains("Sale"))
                    {
                        DataTable dtTransactionDetail = new DataTable("TransactionDetail");
                        sqlCommand.CommandText = $"select * from TransactionDetail where ID = '{item["CreditCardTransactionID"]}'";
                        var readerMerchant = sqlCommand.ExecuteReader();
                        dtTransactionDetail.Load(readerMerchant);
                        readerMerchant.Close();

                        if (dtTransactionDetail?.Rows.Count > 0)
                        {
                            sql = $"update MERCHANT.dbo.TransactionDetail set TranStatus = 'VOIDED' where ID = '{dtTransactionDetail.Rows[0]["ID"]}'";
                            RunQueryUpdate(sqlCommand, sql);

                            sql = GetSqlInsert(dtTransactionDetail);
                            SqlParameter[] parparms = GetCreateParameters(dtTransactionDetail);

                            int colCount = dtTransactionDetail.Columns.Count;

                            for (int i = 0; i < colCount; i++)
                            {
                                if (i == 0)
                                    parparms[i].Value = Guid.NewGuid().ToString();
                                else if (dtTransactionDetail.Columns[i].ColumnName == "TranType")
                                    parparms[i].Value = "VoidSale";
                                else
                                    parparms[i].Value = dtTransactionDetail.Rows[0][i];
                            }

                            RunQueryUpdate(sqlCommand, sql, parparms);

                            if (!string.IsNullOrWhiteSpace(Utility.GetStringFromRow("BatchUnique", dtTransactionDetail.Rows[0])))
                                UpdateBatchByBatchUnique(dtTransactionDetail.Rows[0]["BatchUnique"]);
                        }
                    }
                }
            }
        }

        private string GetSqlInsert(DataTable dt)
        {
            string strInsert = string.Empty;
            try
            {
                strInsert = "INSERT INTO dbo." + dt.TableName + " (";

                int colCount = dt.Columns.Count;
                for (int i = 0; i < colCount; i++)
                {
                    strInsert += "[" + dt.Columns[i].ColumnName.ToUpper() + "]";
                    if (i < colCount - 1)
                        strInsert += ", ";
                }
                strInsert += ") VALUES (";
                for (int i = 0; i < colCount; i++)
                {
                    strInsert += "@" + dt.Columns[i].ColumnName.ToUpper();
                    if (i < colCount - 1)
                        strInsert += ", ";
                }
                strInsert += "); ";
            }
            catch (Exception ex)
            {
                BLogicLogger.Exception("GetSqlInsert", ex);
            }
            return strInsert;
        }

        private SqlParameter[] GetCreateParameters(DataTable dt)
        {
            SqlParameter[] parms = null;
            try
            {
                int colCount = dt.Columns.Count;
                parms = new SqlParameter[colCount];
                for (int i = 0; i < colCount; i++)
                {
                    parms[i] = new SqlParameter("@" + dt.Columns[i].ColumnName.ToUpper(), ToDbType(dt.Columns[i].DataType));
                }

            }
            catch (Exception ex)
            {
                BLogicLogger.Exception("GetCreateParameters", ex);
            }
            return parms;
        }

        public static SqlDbType ToDbType(System.Type type)
        {
            switch (type.Name)
            {
                case "String":
                    return SqlDbType.NVarChar;
                case "Int":
                    return SqlDbType.Int;
                case "Int32":
                    return SqlDbType.Int;
                case "Boolean":
                    return SqlDbType.Bit;
                case "Decimal":
                    return SqlDbType.Decimal;
                case "Double":
                    return SqlDbType.Money;
                case "DateTime":
                    return SqlDbType.DateTime;
                case "Byte":
                    return SqlDbType.TinyInt;
                case "Short":
                    return SqlDbType.SmallInt;
                case "Long":
                    return SqlDbType.BigInt;
                default:
                    throw new Exception("Can't convert to " + type.Name);
            }
        }

        public void PartialRefundItems()
        {
            try
            {
                string sql = string.Empty;
                DataTable dtSaleReceipts = new DataTable("SaleReceipts");
                DataTable dtSaleReceiptPaymentDetails = new DataTable("SaleReceiptPaymentDetails");

                using (SqlCommand sqlCommand = new SqlCommand())
                using (SqlConnection conn = new SqlConnection(Helper.BLOGICPOS7_Connection))
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();

                    sqlCommand.Connection = conn;
                    sqlCommand.CommandType = CommandType.Text;

                    sql = $"select * from SaleReceipts where SaleReceiptID = '{requestModel.SaleReceiptID}';";
                    sqlCommand.CommandText = sql;
                    var reader = sqlCommand.ExecuteReader();
                    dtSaleReceipts.Load(reader);
                    reader.Close();

                    sql = $"select * from SaleReceiptPaymentDetails where SaleReceiptPaymentDetailID = '{requestModel.SaleReceiptPaymentDetailID}' ";
                    sqlCommand.CommandText = sql;
                    reader = sqlCommand.ExecuteReader();
                    dtSaleReceiptPaymentDetails.Load(reader);
                    reader.Close();

                    foreach (var item in requestModel.objectValues)
                    {
                        sql = $"update SaleReceiptDetails set ReturnCode = '-113',Status = '2' where SaleReceiptDetailID = '{item.SaleReceiptDetailID}';";
                        RunQueryUpdate(sqlCommand, sql);
                    }

                    DataRow[] drs = CurrentTableInDatabaseBLogicPOS7.Select("TABLE_NAME = 'SaleReceiptDetailTaxDetails'", "");
                    if (drs.Length > 0)
                    {
                        foreach (var item in requestModel.objectValues)
                        {
                            sql = $"update SaleReceiptDetailTaxDetails set ReturnCode = '-113',Status = '2' where SaleReceiptDetailID = '{item.SaleReceiptDetailID}';";
                            RunQueryUpdate(sqlCommand, sql);
                        }
                    }

                    if (dtSaleReceipts?.Rows.Count > 0)
                    {
                        decimal _TotalAmount = Utility.GetDecimalFromRow("TotalAmount", dtSaleReceipts.Rows[0]) - requestModel.TotalAmount;
                        decimal _TaxAmount = Utility.GetDecimalFromRow("TaxAmount", dtSaleReceipts.Rows[0]) - requestModel.TaxAmount;

                        sql = $"update SaleReceipts set TotalAmount = {_TotalAmount}, PaidAmount = {_TotalAmount}, TaxAmount = {_TaxAmount} where SaleReceiptID = '{requestModel.SaleReceiptID}'";
                        RunQueryUpdate(sqlCommand, sql);
                    }

                    if (dtSaleReceiptPaymentDetails?.Rows.Count > 0)
                    {
                        decimal _AmountTendered = Utility.GetDecimalFromRow("AmountTendered", dtSaleReceiptPaymentDetails.Rows[0]) - requestModel.TotalAmount;

                        sql = $"update SaleReceiptPaymentDetails set AmountTendered = {_AmountTendered} where SaleReceiptPaymentDetailID = '{requestModel.SaleReceiptPaymentDetailID}'";
                        RunQueryUpdate(sqlCommand, sql);
                    }
                }

                using (SqlCommand sqlCommand = new SqlCommand())
                using (SqlConnection conn = new SqlConnection(Helper.Merchant_Connection))
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();

                    sqlCommand.Connection = conn;
                    sqlCommand.CommandType = CommandType.Text;

                    foreach (DataRow item in dtSaleReceiptPaymentDetails.Rows)
                    {
                        if (Utility.GetIntFromRow("PaymentMethodID", item) == 2 &&
                            Utility.GetStringFromRow("CreditCardTransactionType", item).Contains("Sale"))
                        {
                            DataTable dtTransactionDetail = new DataTable("TransactionDetail");
                            sqlCommand.CommandText = $"select * from TransactionDetail where ID = '{item["CreditCardTransactionID"]}'";
                            var readerMerchant = sqlCommand.ExecuteReader();
                            dtTransactionDetail.Load(readerMerchant);
                            readerMerchant.Close();

                            if (dtTransactionDetail?.Rows.Count > 0)
                            {
                                decimal _Amount = Utility.GetDecimalFromRow("Amount", dtTransactionDetail.Rows[0]) - requestModel.TotalAmount;
                                decimal _Total = Utility.GetDecimalFromRow("Total", dtTransactionDetail.Rows[0]) - requestModel.TotalAmount;

                                sql = $"update Merchant.dbo.TransactionDetail set Amount = {_Amount}, Total = {_Total} where ID = '{item["CreditCardTransactionID"]}';";
                                RunQueryUpdate(sqlCommand, sql);

                                if (!string.IsNullOrWhiteSpace(Utility.GetStringFromRow("BatchUnique", dtTransactionDetail.Rows[0])))
                                    UpdateBatchByBatchUnique(dtTransactionDetail.Rows[0]["BatchUnique"]);
                            }
                        }
                    }
                }

                IsSuccess = true;

            }
            catch (Exception ex)
            {
                BLogicLogger.Exception("CloseReceipt in ProcessForm", ex);
            }
        }

        public async Task UpdateServiceChargeBySaleReceiptID()
        {
            try
            {
                string sql = string.Empty;
                DataTable dtSaleReceiptPaymentDetails = new DataTable("SaleReceiptPaymentDetails");
                DataTable dtSaleReceiptDetails = new DataTable("SaleReceiptDetails");
                DataTable dtSaleReceipts = new DataTable("SaleReceipts");
                DataTable dtConfigurations = new DataTable("Configurations");

                using (SqlCommand sqlCommand = new SqlCommand())
                using (SqlConnection conn = new SqlConnection(Helper.BLOGICPOS7_Connection))
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();

                    sqlCommand.Connection = conn;
                    sqlCommand.CommandType = CommandType.Text;

                    sql = $@"select * from SaleReceipts where SaleReceiptID = '{requestModel.refundValues.SaleReceiptID}'";
                    dtSaleReceipts = ProcessQueryDatatable(sqlCommand, sql, "SaleReceipts");

                    sql = $@"select * from SaleReceiptPaymentDetails where SaleReceiptID = '{requestModel.refundValues.SaleReceiptID}'";
                    dtSaleReceiptPaymentDetails = ProcessQueryDatatable(sqlCommand, sql, "SaleReceiptPaymentDetails");

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
                    dtConfigurations = ProcessQueryDatatable(sqlCommand, sql, "Configurations");

                    foreach (DataRow item in dtConfigurations.Rows)
                    {
                        string _name = Utility.GetStringFromRow("ConfigurationName", item);
                        string _value = Utility.GetStringFromRow("ConfigurationValue", item);
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

                    if (dtSaleReceipts?.Rows.Count > 0)
                    {
                        #region Auto Gratuity Amount
                        decimal new_AutoGratuityAmount = requestModel.refundValues.AutoGratuityAmount;
                        decimal new_AutoGratuityTaxAmount = requestModel.refundValues.AutoGratuityTaxAmount;

                        decimal balance_AutoGratuityAmount = Utility.GetDecimalFromRow("AutoGratuityAmount", dtSaleReceipts.Rows[0]) - new_AutoGratuityAmount;
                        decimal balance_AutoGratuityTaxAmount = Utility.GetDecimalFromRow("AutoGratuityTaxAmount", dtSaleReceipts.Rows[0]) - new_AutoGratuityTaxAmount;

                        decimal new_TotalAmount_In_SaleReceipts = Utility.GetDecimalFromRow("TotalAmount", dtSaleReceipts.Rows[0]) - balance_AutoGratuityAmount - balance_AutoGratuityTaxAmount;
                        decimal new_SalesOrderFee_In_SaleReceipts = Utility.GetDecimalFromRow("SalesOrderFee", dtSaleReceipts.Rows[0]) - balance_AutoGratuityAmount;
                        decimal new_ServiceChargeTaxAmount_In_SaleReceipts = Utility.GetDecimalFromRow("ServiceChargeTaxAmount", dtSaleReceipts.Rows[0]) - balance_AutoGratuityTaxAmount;
                        decimal new_TaxAmount_In_SaleReceipts = Utility.GetDecimalFromRow("TaxAmount", dtSaleReceipts.Rows[0]) - balance_AutoGratuityTaxAmount;

                        decimal new_AmountTendered_In_SaleReceiptPaymentDetails = Utility.GetDecimalFromRow("AmountTendered", dtSaleReceiptPaymentDetails.Rows[0]) - balance_AutoGratuityAmount - balance_AutoGratuityTaxAmount;

                        sql = $"update SaleReceipts set AutoGratuityAmount = '{new_AutoGratuityAmount}', TotalAmount = '{new_TotalAmount_In_SaleReceipts}', PaidAmount = '{new_TotalAmount_In_SaleReceipts}', SalesOrderFee = '{new_SalesOrderFee_In_SaleReceipts}', TaxAmount = '{new_TaxAmount_In_SaleReceipts}' where SaleReceiptID = '{requestModel.refundValues.SaleReceiptID}';";
                        sql += $"update SaleReceiptPaymentDetails set AmountTendered = '{new_AmountTendered_In_SaleReceiptPaymentDetails}' where SaleReceiptPaymentDetailID = '{dtSaleReceiptPaymentDetails.Rows[0]["SaleReceiptPaymentDetailID"]}';";

                        RunQueryUpdate(sqlCommand, sql);

                        if (ExistSVCFeeTaxAmountInSaleReceipts)
                        {
                            sql = $"update SaleReceipts set AutoGratuityTaxAmount = '{new_AutoGratuityTaxAmount}', ServiceChargeTaxAmount = '{new_ServiceChargeTaxAmount_In_SaleReceipts}' where SaleReceiptID = '{requestModel.refundValues.SaleReceiptID}';";

                            RunQueryUpdate(sqlCommand, sql);
                        }
                        #endregion

                        #region Party Service Fee Amount
                        decimal new_PartyServiceFeeAmount = requestModel.refundValues.PartyServiceFeeAmount;
                        decimal new_PartyServiceFeeTaxAmount = requestModel.refundValues.PartyServiceFeeTaxAmount;

                        decimal balance_PartyServiceFeeAmount = Utility.GetDecimalFromRow("PartyServiceFeeAmount", dtSaleReceipts.Rows[0]) - new_PartyServiceFeeAmount;
                        decimal balance_PartyServiceFeeTaxAmount = Utility.GetDecimalFromRow("PartyServiceFeeTaxAmount", dtSaleReceipts.Rows[0]) - new_PartyServiceFeeTaxAmount;

                        new_TotalAmount_In_SaleReceipts = Utility.GetDecimalFromRow("TotalAmount", dtSaleReceipts.Rows[0]) - balance_PartyServiceFeeAmount - balance_PartyServiceFeeTaxAmount;
                        new_SalesOrderFee_In_SaleReceipts = Utility.GetDecimalFromRow("SalesOrderFee", dtSaleReceipts.Rows[0]) - balance_PartyServiceFeeAmount;
                        new_ServiceChargeTaxAmount_In_SaleReceipts = Utility.GetDecimalFromRow("ServiceChargeTaxAmount", dtSaleReceipts.Rows[0]) - balance_PartyServiceFeeTaxAmount;
                        new_TaxAmount_In_SaleReceipts = Utility.GetDecimalFromRow("TaxAmount", dtSaleReceipts.Rows[0]) - balance_PartyServiceFeeTaxAmount;

                        new_AmountTendered_In_SaleReceiptPaymentDetails = Utility.GetDecimalFromRow("AmountTendered", dtSaleReceiptPaymentDetails.Rows[0]) - balance_PartyServiceFeeAmount - balance_PartyServiceFeeTaxAmount;

                        sql = $"update SaleReceipts set PartyServiceFeeAmount = '{new_PartyServiceFeeAmount}', TotalAmount = '{new_TotalAmount_In_SaleReceipts}', PaidAmount = '{new_TotalAmount_In_SaleReceipts}', SalesOrderFee = '{new_SalesOrderFee_In_SaleReceipts}', TaxAmount = '{new_TaxAmount_In_SaleReceipts}' where SaleReceiptID = '{requestModel.refundValues.SaleReceiptID}';";
                        sql += $"update SaleReceiptPaymentDetails set AmountTendered = '{new_AmountTendered_In_SaleReceiptPaymentDetails}' where SaleReceiptPaymentDetailID = '{dtSaleReceiptPaymentDetails.Rows[0]["SaleReceiptPaymentDetailID"]}';";

                        RunQueryUpdate(sqlCommand, sql);

                        if (ExistSVCFeeTaxAmountInSaleReceipts)
                        {
                            sql = $"update SaleReceipts set PartyServiceFeeTaxAmount = '{new_PartyServiceFeeTaxAmount}', ServiceChargeTaxAmount = '{new_ServiceChargeTaxAmount_In_SaleReceipts}' where SaleReceiptID = '{requestModel.refundValues.SaleReceiptID}';";

                            RunQueryUpdate(sqlCommand, sql);
                        }
                        #endregion

                        #region Area Fee Amount
                        decimal new_AreaFeeAmount = requestModel.refundValues.AreaFeeAmount;
                        decimal new_AreaFeeTaxAmount = requestModel.refundValues.AreaFeeTaxAmount;

                        decimal balance_AreaFeeAmount = Utility.GetDecimalFromRow("AreaFeeAmount", dtSaleReceipts.Rows[0]) - new_AreaFeeAmount;
                        decimal balance_AreaFeeTaxAmount = Utility.GetDecimalFromRow("AreaFeeTaxAmount", dtSaleReceipts.Rows[0]) - new_AreaFeeTaxAmount;

                        new_TotalAmount_In_SaleReceipts = Utility.GetDecimalFromRow("TotalAmount", dtSaleReceipts.Rows[0]) - balance_AreaFeeAmount - balance_AreaFeeTaxAmount;
                        new_SalesOrderFee_In_SaleReceipts = Utility.GetDecimalFromRow("SalesOrderFee", dtSaleReceipts.Rows[0]) - balance_AreaFeeAmount;
                        new_ServiceChargeTaxAmount_In_SaleReceipts = Utility.GetDecimalFromRow("ServiceChargeTaxAmount", dtSaleReceipts.Rows[0]) - balance_AreaFeeTaxAmount;
                        new_TaxAmount_In_SaleReceipts = Utility.GetDecimalFromRow("TaxAmount", dtSaleReceipts.Rows[0]) - balance_AreaFeeTaxAmount;

                        new_AmountTendered_In_SaleReceiptPaymentDetails = Utility.GetDecimalFromRow("AmountTendered", dtSaleReceiptPaymentDetails.Rows[0]) - balance_AreaFeeAmount - balance_AreaFeeTaxAmount;

                        sql = $"update SaleReceipts set AreaFeeAmount = '{new_AreaFeeAmount}', TotalAmount = '{new_TotalAmount_In_SaleReceipts}', PaidAmount = '{new_TotalAmount_In_SaleReceipts}', SalesOrderFee = '{new_SalesOrderFee_In_SaleReceipts}', TaxAmount = '{new_TaxAmount_In_SaleReceipts}' where SaleReceiptID = '{requestModel.refundValues.SaleReceiptID}';";
                        sql += $"update SaleReceiptPaymentDetails set AmountTendered = '{new_AmountTendered_In_SaleReceiptPaymentDetails}' where SaleReceiptPaymentDetailID = '{dtSaleReceiptPaymentDetails.Rows[0]["SaleReceiptPaymentDetailID"]}';";

                        RunQueryUpdate(sqlCommand, sql);

                        if (ExistSVCFeeTaxAmountInSaleReceipts)
                        {
                            sql = $"update SaleReceipts set AreaFeeTaxAmount = '{new_AreaFeeTaxAmount}', ServiceChargeTaxAmount = '{new_ServiceChargeTaxAmount_In_SaleReceipts}' where SaleReceiptID = '{requestModel.refundValues.SaleReceiptID}';";

                            RunQueryUpdate(sqlCommand, sql);
                        }
                        #endregion

                        #region Service Fee 
                        decimal new_ServiceFee = requestModel.refundValues.ServiceFee;
                        decimal new_ServiceFeeTaxAmount = requestModel.refundValues.ServiceFeeTaxAmount;

                        decimal balance_ServiceFee = Utility.GetDecimalFromRow("ServiceFee", dtSaleReceipts.Rows[0]) - new_ServiceFee;
                        decimal balance_ServiceFeeTaxAmount = Utility.GetDecimalFromRow("ServiceFeeTaxAmount", dtSaleReceipts.Rows[0]) - new_ServiceFeeTaxAmount;

                        new_TotalAmount_In_SaleReceipts = Utility.GetDecimalFromRow("TotalAmount", dtSaleReceipts.Rows[0]) - balance_ServiceFee - balance_ServiceFeeTaxAmount;
                        new_SalesOrderFee_In_SaleReceipts = Utility.GetDecimalFromRow("SalesOrderFee", dtSaleReceipts.Rows[0]) - balance_ServiceFee;
                        new_ServiceChargeTaxAmount_In_SaleReceipts = Utility.GetDecimalFromRow("ServiceChargeTaxAmount", dtSaleReceipts.Rows[0]) - balance_ServiceFeeTaxAmount;
                        new_TaxAmount_In_SaleReceipts = Utility.GetDecimalFromRow("TaxAmount", dtSaleReceipts.Rows[0]) - balance_ServiceFeeTaxAmount;

                        new_AmountTendered_In_SaleReceiptPaymentDetails = Utility.GetDecimalFromRow("AmountTendered", dtSaleReceiptPaymentDetails.Rows[0]) - balance_ServiceFee - balance_ServiceFeeTaxAmount;

                        sql = $"update SaleReceipts set ServiceFee = '{new_ServiceFee}', TotalAmount = '{new_TotalAmount_In_SaleReceipts}', PaidAmount = '{new_TotalAmount_In_SaleReceipts}', SalesOrderFee = '{new_SalesOrderFee_In_SaleReceipts}', TaxAmount = '{new_TaxAmount_In_SaleReceipts}' where SaleReceiptID = '{requestModel.refundValues.SaleReceiptID}';";
                        sql += $"update SaleReceiptPaymentDetails set AmountTendered = '{new_AmountTendered_In_SaleReceiptPaymentDetails}' where SaleReceiptPaymentDetailID = '{dtSaleReceiptPaymentDetails.Rows[0]["SaleReceiptPaymentDetailID"]}';";

                        RunQueryUpdate(sqlCommand, sql);

                        if (ExistSVCFeeTaxAmountInSaleReceipts)
                        {
                            sql = $"update SaleReceipts set ServiceFeeTaxAmount = '{new_ServiceFeeTaxAmount}', ServiceChargeTaxAmount = '{new_ServiceChargeTaxAmount_In_SaleReceipts}' where SaleReceiptID = '{requestModel.refundValues.SaleReceiptID}';";

                            RunQueryUpdate(sqlCommand, sql);
                        }
                        #endregion

                        #region SVC Fee & SVC Fee Tax Amount
                        decimal new_SVCFeeAmount = requestModel.refundValues.SVCFeePaidAmount + requestModel.refundValues.SVCFeeTaxPaidAmount;
                        sql = $"update SaleReceipts set SVCFeeAmount = '{new_SVCFeeAmount}', SVCFeePaidAmount = '{new_SVCFeeAmount}' where SaleReceiptID = '{requestModel.refundValues.SaleReceiptID}';";
                        sql += $"update SaleReceiptPaymentDetails set SVCFeeAmount = '{new_SVCFeeAmount}' where SaleReceiptPaymentDetailID = '{dtSaleReceiptPaymentDetails.Rows[0]["SaleReceiptPaymentDetailID"]}';";

                        RunQueryUpdate(sqlCommand, sql);

                        if (ExistSVCFeeTaxAmountInSaleReceipts)
                        {
                            decimal new_SVCFeeTaxAmount = requestModel.refundValues.SVCFeeTaxPaidAmount;
                            sql = $"update SaleReceipts set SVCFeeTaxAmount = '{new_SVCFeeTaxAmount}', SVCFeeTaxPaidAmount = '{new_SVCFeeTaxAmount}' where SaleReceiptID = '{requestModel.refundValues.SaleReceiptID}';";
                            sql += $"update SaleReceiptPaymentDetails set SVCFeeTaxAmount = '{new_SVCFeeTaxAmount}' where SaleReceiptPaymentDetailID = '{dtSaleReceiptPaymentDetails.Rows[0]["SaleReceiptPaymentDetailID"]}';";

                            RunQueryUpdate(sqlCommand, sql);
                        }
                        #endregion

                        #region Cash Discount Amount & Cash Discount Tax Amount
                        decimal new_CashDiscountAmount = requestModel.refundValues.CashDiscountPaidAmount + requestModel.refundValues.CashDiscountTaxPaidAmount;
                        sql = $"update SaleReceipts set CashDiscountAmount = '{new_CashDiscountAmount}', CashDiscountPaidAmount = '{new_CashDiscountAmount}' where SaleReceiptID = '{requestModel.refundValues.SaleReceiptID}';";
                        sql += $"update SaleReceiptPaymentDetails set CashDiscountAmount = '{new_CashDiscountAmount}' where SaleReceiptPaymentDetailID = '{dtSaleReceiptPaymentDetails.Rows[0]["SaleReceiptPaymentDetailID"]}';";

                        RunQueryUpdate(sqlCommand, sql);

                        if (ExistSVCFeeTaxAmountInSaleReceipts)
                        {
                            decimal new_CashDiscountTaxAmount = requestModel.refundValues.CashDiscountTaxPaidAmount;
                            sql = $"update SaleReceipts set CashDiscountTaxAmount = '{new_CashDiscountTaxAmount}', CashDiscountTaxPaidAmount = '{new_CashDiscountTaxAmount}' where SaleReceiptID = '{requestModel.refundValues.SaleReceiptID}';";
                            sql += $"update SaleReceiptPaymentDetails set CashDiscountTaxAmount = '{new_CashDiscountTaxAmount}' where SaleReceiptPaymentDetailID = '{dtSaleReceiptPaymentDetails.Rows[0]["SaleReceiptPaymentDetailID"]}';";

                            RunQueryUpdate(sqlCommand, sql);
                        }
                        #endregion


                    }


                    sql = $@"select * from SaleReceiptPaymentDetails where SaleReceiptID = '{requestModel.refundValues.SaleReceiptID}'";
                    var oPaymentDetail = await conn.QueryFirstOrDefaultAsync<SaleReceiptPaymentDetailEntity>(sql, null, null, 180, System.Data.CommandType.Text);

                    sql = $@"select * from SaleReceipts where SaleReceiptID = '{requestModel.refundValues.SaleReceiptID}'";
                    var oReceipt = await conn.QueryFirstOrDefaultAsync<SaleReceiptEntity>(sql, null, null, 180, System.Data.CommandType.Text);

                    int cashAdjust = 1;
                    if (_EnableDualPrice == "1" && _DualPricePercent > 0)
                    {
                        sql = $"select IsDualPrice from PaymentMethods where PaymentMethodID = '{oPaymentDetail.PaymentMethodID}'";
                        cashAdjust = await conn.QuerySingleAsync<int>(sql, null, null, 180, System.Data.CommandType.Text);
                    }
                    else
                    {
                        sql = $"select CashAdjust from PaymentMethods where PaymentMethodID = '{oPaymentDetail.PaymentMethodID}'";
                        cashAdjust = await conn.QuerySingleAsync<int>(sql, null, null, 180, System.Data.CommandType.Text);
                    }

                    bool isApplyTaxCustomFee = false;
                    SaleReceipts_ReceiptInfo receiptInfo = null;
                    if (!string.IsNullOrWhiteSpace(oReceipt.ReceiptInfo))
                    {
                        receiptInfo = Utility.ParseJsonToReceiptInfo(oReceipt.ReceiptInfo);
                        if (receiptInfo != null)
                            isApplyTaxCustomFee = receiptInfo.IsApplyTaxForSVCFee;
                    }

                    if (oReceipt.SVCFeeTaxAmount != 0)
                        isApplyTaxCustomFee = true;

                    if (oPaymentDetail != null && oReceipt != null)
                    {
                        CalculateCustomFeeForServiceCharge(oPaymentDetail, oReceipt, isApplyTaxCustomFee, cashAdjust, _svcFee, _cashDiscount, _merchantFee, _svcFeeType, _ApplyCustomFeeWhenPurchaseGiftCard, _dTaxIncludeInSVCFee);

                        sql = $@"update SaleReceiptPaymentDetails set 
                                AreaFeeAmount = '{oPaymentDetail.AreaFeeAmount}',
                                AreaFeeTaxAmount = '{oPaymentDetail.AreaFeeTaxAmount}',
                                SVCFeeForAreaFeeAmount = '{oPaymentDetail.SVCFeeForAreaFeeAmount}',
                                MerchantFeeForAreaFeeAmount = '{oPaymentDetail.MerchantFeeForAreaFeeAmount}',
                                AutoGratuityAmount = '{oPaymentDetail.AutoGratuityAmount}',
                                AutoGratuityTaxAmount = '{oPaymentDetail.AutoGratuityTaxAmount}',
                                SVCFeeForAutoGratuityAmount = '{oPaymentDetail.SVCFeeForAutoGratuityAmount}',
                                MerchantFeeForAutoGratuityAmount = '{oPaymentDetail.MerchantFeeForAutoGratuityAmount}',
                                PartyServiceFeeAmount = '{oPaymentDetail.PartyServiceFeeAmount}',
                                PartyServiceFeeTaxAmount = '{oPaymentDetail.PartyServiceFeeTaxAmount}',
                                SVCFeeForPartyServiceFeeAmount = '{oPaymentDetail.SVCFeeForPartyServiceFeeAmount}',
                                MerchantFeeForPartyServiceFeeAmount = '{oPaymentDetail.MerchantFeeForPartyServiceFeeAmount}',
                                ServiceFee = '{oPaymentDetail.ServiceFee}',
                                ServiceFeeTaxAmount = '{oPaymentDetail.ServiceFeeTaxAmount}',
                                SVCFeeForServiceFee = '{oPaymentDetail.SVCFeeForServiceFee}',
                                MerchantFeeForServiceFee = '{oPaymentDetail.MerchantFeeForServiceFee}',
                                SVCFeeForServiceCharge = '{oPaymentDetail.SVCFeeForServiceCharge}',
                                SVCFeeAmount = '{oPaymentDetail.SVCFeeAmount}',
                                SVCFeeTaxAmount = '{oPaymentDetail.SVCFeeTaxAmount}',
                                MerchantFee = '{oPaymentDetail.MerchantFee}'
                                where SaleReceiptPaymentDetailID = '{oPaymentDetail.SaleReceiptPaymentDetailID}'";

                        RunQueryUpdate(sqlCommand, sql);
                    }

                }

                IsSuccess = true;

            }
            catch (Exception ex)
            {
                BLogicLogger.Exception("CloseReceipt in ProcessForm", ex);
            }
        }

        public void CalculateCustomFeeForServiceCharge(SaleReceiptPaymentDetailEntity oPaymentDetail, SaleReceiptEntity oReceipt, bool isApplyTaxCustomFee, int cashAdjust, decimal _svcFee,
            decimal _cashDiscount, decimal _merchantFee, int _svcFeeType, string _ApplyCustomFeeWhenPurchaseGiftCard, int _dTaxIncludeInSVCFee)
        {
            if (oPaymentDetail.PaymentMethodID == 2 || cashAdjust == 2 || (_svcFee > 0 && _cashDiscount == 0 && oPaymentDetail.PaymentMethodID != -1 && oPaymentDetail.PaymentMethodID != -2 && oPaymentDetail.PaymentMethodID != -3))
            {
                if (oPaymentDetail.PaidServiceFee == 0 || oPaymentDetail.SVCFeeAmount == 0 || (oPaymentDetail.PaymentMethodID == 3 && _ApplyCustomFeeWhenPurchaseGiftCard != "1"))
                {
                    oPaymentDetail.SVCFeeForServiceCharge = oPaymentDetail.SVCFeeForServiceFee = oPaymentDetail.SVCFeeForPartyServiceFeeAmount = oPaymentDetail.SVCFeeForAreaFeeAmount = oPaymentDetail.SVCFeeForAutoGratuityAmount = 0;
                }
                else if (isApplyTaxCustomFee) // Apply tax custom fee
                {
                    decimal taxsc = oPaymentDetail.AutoGratuityTaxAmount + oPaymentDetail.AreaFeeTaxAmount + oPaymentDetail.PartyServiceFeeTaxAmount + oPaymentDetail.ServiceFeeTaxAmount;
                    oPaymentDetail.SVCFeeForAutoGratuityAmount = CalculateSVCFee(oPaymentDetail.AutoGratuityAmount - oPaymentDetail.AutoGratuityTaxAmount, _svcFee, _svcFeeType, _dTaxIncludeInSVCFee, isApplyTaxCustomFee, oReceipt.SVCFeeTaxRate, oPaymentDetail.SVCFeeTaxAmount);
                    oPaymentDetail.SVCFeeForAreaFeeAmount = CalculateSVCFee(oPaymentDetail.AreaFeeAmount - oPaymentDetail.AreaFeeTaxAmount, _svcFee, _svcFeeType, _dTaxIncludeInSVCFee, isApplyTaxCustomFee, oReceipt.SVCFeeTaxRate, oPaymentDetail.SVCFeeTaxAmount);
                    oPaymentDetail.SVCFeeForPartyServiceFeeAmount = CalculateSVCFee(oPaymentDetail.PartyServiceFeeAmount - oPaymentDetail.PartyServiceFeeTaxAmount, _svcFee, _svcFeeType, _dTaxIncludeInSVCFee, isApplyTaxCustomFee, oReceipt.SVCFeeTaxRate, oPaymentDetail.SVCFeeTaxAmount);
                    oPaymentDetail.SVCFeeForServiceFee = CalculateSVCFee(oPaymentDetail.ServiceFee - oPaymentDetail.ServiceFeeTaxAmount, _svcFee, _svcFeeType, _dTaxIncludeInSVCFee, isApplyTaxCustomFee, oReceipt.SVCFeeTaxRate, oPaymentDetail.SVCFeeTaxAmount);
                    oPaymentDetail.SVCFeeForServiceCharge = CalculateSVCFee(oPaymentDetail.PaidServiceFee - taxsc, _svcFee, _svcFeeType, _dTaxIncludeInSVCFee, isApplyTaxCustomFee, oReceipt.SVCFeeTaxRate, oPaymentDetail.SVCFeeTaxAmount);
                }
                else
                {
                    oPaymentDetail.SVCFeeForServiceCharge = CalculateSVCFee(oPaymentDetail.PaidServiceFee, _svcFee, _svcFeeType, _dTaxIncludeInSVCFee);
                    oPaymentDetail.SVCFeeForServiceFee = CalculateSVCFee(oPaymentDetail.ServiceFee, _svcFee, _svcFeeType, _dTaxIncludeInSVCFee);
                    oPaymentDetail.SVCFeeForPartyServiceFeeAmount = CalculateSVCFee(oPaymentDetail.PartyServiceFeeAmount, _svcFee, _svcFeeType, _dTaxIncludeInSVCFee);
                    oPaymentDetail.SVCFeeForAreaFeeAmount = CalculateSVCFee(oPaymentDetail.AreaFeeAmount, _svcFee, _svcFeeType, _dTaxIncludeInSVCFee);
                    oPaymentDetail.SVCFeeForAutoGratuityAmount = CalculateSVCFee(oPaymentDetail.AutoGratuityAmount, _svcFee, _svcFeeType, _dTaxIncludeInSVCFee);
                }

                #region Compare Custom Fee For Service Charge
                decimal tempTotalSVCFee = oPaymentDetail.SVCFeeForServiceFee + oPaymentDetail.SVCFeeForPartyServiceFeeAmount +
                    oPaymentDetail.SVCFeeForAreaFeeAmount + oPaymentDetail.SVCFeeForAutoGratuityAmount;

                if (oPaymentDetail.SVCFeeForServiceCharge != tempTotalSVCFee)
                {
                    decimal tempVal = tempTotalSVCFee - oPaymentDetail.SVCFeeForServiceCharge;

                    if (oPaymentDetail.SVCFeeForPartyServiceFeeAmount > 0 && tempVal != 0)
                    {
                        oPaymentDetail.SVCFeeForPartyServiceFeeAmount -= tempVal;
                        tempVal = 0;
                    }
                    else if (oPaymentDetail.SVCFeeForAreaFeeAmount > 0 && tempVal != 0)
                    {
                        oPaymentDetail.SVCFeeForAreaFeeAmount -= tempVal;
                        tempVal = 0;
                    }
                    else if (oPaymentDetail.SVCFeeForServiceFee > 0 && tempVal != 0)
                    {
                        oPaymentDetail.SVCFeeForServiceFee -= tempVal;
                        tempVal = 0;
                    }
                    else if (oPaymentDetail.SVCFeeForAutoGratuityAmount > 0 && tempVal != 0)
                    {
                        oPaymentDetail.SVCFeeForAutoGratuityAmount -= tempVal;
                        tempVal = 0;
                    }
                }

                #endregion

                if (oPaymentDetail.PaymentMethodID == 1 || (cashAdjust != 2 && _svcFee > 0 && _cashDiscount == 0)) // Cash
                {
                    oPaymentDetail.MerchantFeeForServiceFee = oPaymentDetail.MerchantFeeForAreaFeeAmount = oPaymentDetail.MerchantFeeForPartyServiceFeeAmount = oPaymentDetail.MerchantFeeForAutoGratuityAmount = 0;
                }
                else
                {
                    decimal tempMerchantFeeForServiceCharge = 0;
                    if (isApplyTaxCustomFee) // Apply tax custom fee
                    {
                        decimal taxsc = oPaymentDetail.AutoGratuityTaxAmount + oPaymentDetail.AreaFeeTaxAmount + oPaymentDetail.PartyServiceFeeTaxAmount + oPaymentDetail.ServiceFeeTaxAmount;
                        tempMerchantFeeForServiceCharge = CalculateMerchantFee((oPaymentDetail.PaidServiceFee - taxsc) + oPaymentDetail.SVCFeeForServiceCharge, _merchantFee);

                        oPaymentDetail.MerchantFeeForServiceFee = CalculateMerchantFee((oPaymentDetail.ServiceFee - oPaymentDetail.ServiceFeeTaxAmount) + oPaymentDetail.SVCFeeForServiceFee, _merchantFee);
                        oPaymentDetail.MerchantFeeForAreaFeeAmount = CalculateMerchantFee((oPaymentDetail.AreaFeeAmount - oPaymentDetail.AreaFeeTaxAmount) + oPaymentDetail.SVCFeeForAreaFeeAmount, _merchantFee);
                        oPaymentDetail.MerchantFeeForPartyServiceFeeAmount = CalculateMerchantFee((oPaymentDetail.PartyServiceFeeAmount - oPaymentDetail.PartyServiceFeeTaxAmount) + oPaymentDetail.SVCFeeForPartyServiceFeeAmount, _merchantFee);
                        oPaymentDetail.MerchantFeeForAutoGratuityAmount = CalculateMerchantFee((oPaymentDetail.AutoGratuityAmount - oPaymentDetail.AutoGratuityTaxAmount) + oPaymentDetail.SVCFeeForAutoGratuityAmount, _merchantFee);
                    }
                    else
                    {
                        tempMerchantFeeForServiceCharge = CalculateMerchantFee(oPaymentDetail.PaidServiceFee + oPaymentDetail.SVCFeeForServiceCharge, _merchantFee);

                        oPaymentDetail.MerchantFeeForServiceFee = CalculateMerchantFee(oPaymentDetail.ServiceFee + oPaymentDetail.SVCFeeForServiceFee, _merchantFee);
                        oPaymentDetail.MerchantFeeForAreaFeeAmount = CalculateMerchantFee(oPaymentDetail.AreaFeeAmount + oPaymentDetail.SVCFeeForAreaFeeAmount, _merchantFee);
                        oPaymentDetail.MerchantFeeForPartyServiceFeeAmount = CalculateMerchantFee(oPaymentDetail.PartyServiceFeeAmount + oPaymentDetail.SVCFeeForPartyServiceFeeAmount, _merchantFee);
                        oPaymentDetail.MerchantFeeForAutoGratuityAmount = CalculateMerchantFee(oPaymentDetail.AutoGratuityAmount + oPaymentDetail.SVCFeeForAutoGratuityAmount, _merchantFee);
                    }

                    #region Compare Merchant Fee For Service Charge

                    decimal tempTotalMerchantFee = oPaymentDetail.MerchantFeeForServiceFee + oPaymentDetail.MerchantFeeForAreaFeeAmount +
                    oPaymentDetail.MerchantFeeForPartyServiceFeeAmount + oPaymentDetail.MerchantFeeForAutoGratuityAmount;

                    if (tempMerchantFeeForServiceCharge != tempTotalMerchantFee)
                    {
                        decimal tempVal = tempTotalMerchantFee - tempMerchantFeeForServiceCharge;

                        if (oPaymentDetail.MerchantFeeForPartyServiceFeeAmount > 0 && tempVal != 0)
                        {
                            oPaymentDetail.MerchantFeeForPartyServiceFeeAmount -= tempVal;
                            tempVal = 0;
                        }
                        else if (oPaymentDetail.MerchantFeeForAreaFeeAmount > 0 && tempVal != 0)
                        {
                            oPaymentDetail.MerchantFeeForAreaFeeAmount -= tempVal;
                            tempVal = 0;
                        }
                        else if (oPaymentDetail.MerchantFeeForServiceFee > 0 && tempVal != 0)
                        {
                            oPaymentDetail.MerchantFeeForServiceFee -= tempVal;
                            tempVal = 0;
                        }
                        else if (oPaymentDetail.MerchantFeeForAutoGratuityAmount > 0 && tempVal != 0)
                        {
                            oPaymentDetail.MerchantFeeForAutoGratuityAmount -= tempVal;
                            tempVal = 0;
                        }
                    }

                    #endregion
                }

                if (!string.IsNullOrWhiteSpace(oReceipt.ReceiptInfo))
                {
                    var tempModel = Utility.ParseJsonToReceiptInfo(oReceipt.ReceiptInfo);
                    if (tempModel.RemoveSVCFeeAndCashDiscount)
                    {
                        oPaymentDetail.SVCFeeForServiceCharge = 0;
                        oPaymentDetail.SVCFeeForServiceFee = 0;
                        oPaymentDetail.SVCFeeForPartyServiceFeeAmount = 0;
                        oPaymentDetail.SVCFeeForAreaFeeAmount = 0;
                        oPaymentDetail.SVCFeeForAutoGratuityAmount = 0;
                    }
                }
            }
            else
            {
                if (oPaymentDetail.PaymentMethodID == -2 || oPaymentDetail.PaymentMethodID == -3)
                {
                    decimal tempMerchantFeeForServiceCharge = 0;
                    if (isApplyTaxCustomFee)
                    {
                        decimal taxsc = oPaymentDetail.AutoGratuityTaxAmount + oPaymentDetail.AreaFeeTaxAmount + oPaymentDetail.PartyServiceFeeTaxAmount + oPaymentDetail.ServiceFeeTaxAmount;
                        tempMerchantFeeForServiceCharge = CalculateMerchantFee(oPaymentDetail.PaidServiceFee - taxsc, _merchantFee);
                        oPaymentDetail.MerchantFeeForServiceFee = CalculateMerchantFee(oPaymentDetail.ServiceFee - oPaymentDetail.ServiceFeeTaxAmount, _merchantFee);
                        oPaymentDetail.MerchantFeeForAreaFeeAmount = CalculateMerchantFee(oPaymentDetail.AreaFeeAmount - oPaymentDetail.AreaFeeTaxAmount, _merchantFee);
                        oPaymentDetail.MerchantFeeForPartyServiceFeeAmount = CalculateMerchantFee(oPaymentDetail.PartyServiceFeeAmount - oPaymentDetail.PartyServiceFeeTaxAmount, _merchantFee);
                        oPaymentDetail.MerchantFeeForAutoGratuityAmount = CalculateMerchantFee(oPaymentDetail.AutoGratuityAmount - oPaymentDetail.AutoGratuityTaxAmount, _merchantFee);
                    }
                    else
                    {
                        tempMerchantFeeForServiceCharge = CalculateMerchantFee(oPaymentDetail.PaidServiceFee, _merchantFee);
                        oPaymentDetail.MerchantFeeForServiceFee = CalculateMerchantFee(oPaymentDetail.ServiceFee, _merchantFee);
                        oPaymentDetail.MerchantFeeForAreaFeeAmount = CalculateMerchantFee(oPaymentDetail.AreaFeeAmount, _merchantFee);
                        oPaymentDetail.MerchantFeeForPartyServiceFeeAmount = CalculateMerchantFee(oPaymentDetail.PartyServiceFeeAmount, _merchantFee);
                        oPaymentDetail.MerchantFeeForAutoGratuityAmount = CalculateMerchantFee(oPaymentDetail.AutoGratuityAmount, _merchantFee);
                    }

                    #region Compare Merchant Fee For Service Charge

                    decimal tempTotalMerchantFee = oPaymentDetail.MerchantFeeForServiceFee + oPaymentDetail.MerchantFeeForAreaFeeAmount +
                    oPaymentDetail.MerchantFeeForPartyServiceFeeAmount + oPaymentDetail.MerchantFeeForAutoGratuityAmount;

                    if (tempMerchantFeeForServiceCharge != tempTotalMerchantFee)
                    {
                        decimal tempVal = tempTotalMerchantFee - tempMerchantFeeForServiceCharge;
                        if (oPaymentDetail.MerchantFeeForPartyServiceFeeAmount > 0 && tempVal != 0)
                        {
                            oPaymentDetail.MerchantFeeForPartyServiceFeeAmount -= tempVal;
                            tempVal = 0;
                        }
                        else if (oPaymentDetail.MerchantFeeForAreaFeeAmount > 0 && tempVal != 0)
                        {
                            oPaymentDetail.MerchantFeeForAreaFeeAmount -= tempVal;
                            tempVal = 0;
                        }
                        else if (oPaymentDetail.MerchantFeeForServiceFee > 0 && tempVal != 0)
                        {
                            oPaymentDetail.MerchantFeeForServiceFee -= tempVal;
                            tempVal = 0;
                        }
                        else if (oPaymentDetail.MerchantFeeForAutoGratuityAmount > 0 && tempVal != 0)
                        {
                            oPaymentDetail.MerchantFeeForAutoGratuityAmount -= tempVal;
                            tempVal = 0;
                        }
                    }

                    #endregion
                }
                else
                {
                    oPaymentDetail.MerchantFeeForServiceFee = 0;
                    oPaymentDetail.MerchantFeeForAreaFeeAmount = 0;
                    oPaymentDetail.MerchantFeeForPartyServiceFeeAmount = 0;
                    oPaymentDetail.MerchantFeeForAutoGratuityAmount = 0;
                }
            }
        }


        public decimal CalculateSVCFee(decimal amount, decimal svcFee, int svcFeeType, int sTaxIncludeInSVCFee,
            bool isApplyTaxCustomFee = false, decimal dSVCFeeTaxRate = 0, decimal dSVCFeeTaxAmountByPayment = 0)
        {
            decimal svcFeeReturn = 0;
            try
            {
                if (amount == 0 || svcFee == 0)
                    return svcFeeReturn;
                //Configuration conf = new Configuration();
                //entity = conf.GetConfigurationEntityByName("CreditCardConfigValue.SVCFee");
                //if (entity != null)
                //    svcFee = Utility.GetDecimalFromObject(entity.ConfigurationValue);

                //entity = conf.GetConfigurationEntityByName("CreditCardConfigValue.SVCFeeType");
                //if (entity != null)
                //    svcFeeType = Utility.GetIntFromObject(entity.ConfigurationValue);


                if (isApplyTaxCustomFee)
                {
                    if (svcFeeType == 1)
                    {

                        //entity = conf.GetConfigurationEntityByName("CreditCardConfigValue.TaxIncludeInSVCFee");
                        //if (entity != null)
                        //    sTaxIncludeInSVCFee = Utility.GetIntFromObject(entity.ConfigurationValue);

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
                        svcFeeReturn = Utility.RoundUp(amount * (svcFee / 100));
                    else
                        svcFeeReturn = 0;
                }
            }
            catch (Exception ex)
            {
                BLogicServerLogger.Error($"####=> CalculateSVCFee - Exception - ", ex);
                svcFeeReturn = 0;
                BLogicLogger.Error("#### CalculateSVCFee error: " + ex);
            }
            return svcFeeReturn;
        }

        public decimal CalculateMerchantFee(decimal amount, decimal merchantFeeValue)
        {
            decimal merchantFeeAmount = 0;
            //decimal merchantFeeValue = 0;
            //ConfigurationsEntity entity = null;

            //Configuration conf = new Configuration();
            //entity = conf.GetConfigurationEntityByName("CreditCardConfigValue.MerchantFee");
            //if (entity != null)
            //    merchantFeeValue = Utility.GetDecimalFromObject(entity.ConfigurationValue);

            try
            {

                if (merchantFeeValue == 0)
                    return 0;
                merchantFeeAmount = Utility.RoundUp(amount * (merchantFeeValue / 100));
            }
            catch (Exception ex)
            {
                BLogicServerLogger.Error($"####=> CalculateMerchantFee - Exception", ex);
                merchantFeeAmount = 0;
                BLogicLogger.Error("#### CalculateMerchantFee error: " + ex);
            }
            return merchantFeeAmount;
        }

        private DataTable ProcessQueryDatatable(SqlCommand sqlCommand, string sql, string tableName)
        {
            DataTable dtReturn = new DataTable(tableName);
            sqlCommand.CommandText = sql;
            sqlCommand.CommandType = CommandType.Text;
            var reader = sqlCommand.ExecuteReader();
            dtReturn.Load(reader);
            reader.Close();

            return dtReturn;
        }
    }
}
