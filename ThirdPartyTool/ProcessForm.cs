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
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using ThirdPartyTool.Dialog;
using ThirdPartyTool.Models;
using ThirdPartyTool.SaleReceipt;

namespace ThirdPartyTool
{
    public enum ServiceChargeType
    {
        ManualServiceFee,
        AreaServiceFee,
        AutoServiceFee,
        PartyServiceFee,
        SVCFee,
        CashDiscount
    }

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
        ViewShifts,
        UpdateShift,
        AddSpecialItem
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

        public RequestModel requestModel = new RequestModel();

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
            switch (CurrentProcessName)
            {
                case ProcessNameEnum.UpdateServiceChargeBySaleReceiptID:
                case ProcessNameEnum.ViewShifts:
                case ProcessNameEnum.UpdateShift:
                    Task.Run(() => StartProcess());
                    break;
                default:
                    backgroundWorker.RunWorkerAsync();
                    break;
            }
        }

        private async Task StartProcess()
        {
            try
            {
                switch (CurrentProcessName)
                {
                    case ProcessNameEnum.UpdateServiceChargeBySaleReceiptID:
                        await UpdateServiceChargeBySaleReceiptID();
                        break;
                    case ProcessNameEnum.ViewShifts:
                        await ViewShifts();
                        break;
                    case ProcessNameEnum.UpdateShift:
                        await UpdateShift();
                        break;
                }
            }
            catch (Exception ex)
            {
                BLogicLogger.Exception("StartProcess in ProcessForm", ex, new System.Collections.Generic.List<object>() { CurrentProcessName });
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

        private async void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {

                GetListTableInDatabaseBLogicPOS7();

                switch (CurrentProcessName)
                {
                    case ProcessNameEnum.PartialRefundItems:
                        PartialRefundItems();
                        break;
                    case ProcessNameEnum.AddSpecialItem:
                        AddSpecialItem();
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

        private async Task GetReceiptTemplateBySaleReceiptID(string _saleReceiptID)
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
                string sTaxCodeID = POSConfig.Configuration.ConfigValue.CreditCardConfigValue.TaxCodeIDForCustomFee;
                Utility.TaxPercentForSVCFee = 0;

                if (!string.IsNullOrWhiteSpace(sTaxCodeID) && !sTaxCodeID.Equals("0"))
                {
                    using (var conn = new SqlConnection(Helper.BLOGICPOS7_Connection))
                    {
                        if (conn.State == ConnectionState.Closed)
                            conn.Open();

                        string sql = $"select c.SingleTaxRatePercentage from TaxCodes a left join TaxCodeDetails b on a.TaxCodeID = b.TaxCodeID " +
                            $" left join TaxAuthorities c on b.TaxAuthorityID = c.TaxAuthorityID " +
                            $" where a.TaxCodeID = '{sTaxCodeID}'";

                        Utility.TaxPercentForSVCFee = conn.QuerySingle<decimal>(sql, null, null, 180, System.Data.CommandType.Text);
                    }
                }

                if (Utility.IsUseConfigDualPrice)
                    Utility.TaxPercentForSVCFee = 0;
                Utility.IsApplyTaxForSVCFee = !Utility.IsUseConfigDualPrice && POSConfig.Configuration.ConfigValue.CreditCardConfigValue.SVCFee > 0 && Utility.TaxPercentForSVCFee > 0;

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

        /// <summary>
        /// Refunds the selected item lines of a finished ticket. Same rules as the hand-written partial refund
        /// scripts (ITT-7927, runbook [DB-11]), generalised:
        ///  - one transaction for POS and Merchant (same SQL instance, Merchant reached by name), so nothing is
        ///    ever half-applied;
        ///  - every check runs on fresh data inside that transaction before anything is written;
        ///  - SaleReceiptOriginalInfo is stamped before the first refund, exactly like the POS refund screen;
        ///  - lines: Status 2 / ReturnCode -113 (partial quantities are split, like the POS);
        ///  - header, the chosen payment and the card Sale move down by the refunded amount; MerchantFee is
        ///    re-prorated and the batch recalculated when the Sale is in an open batch;
        ///  - before COMMIT the result is checked against the numbers read at the start; any gap rolls back.
        /// Stops (nothing written) when the ticket is not FINISHED, a line was already refunded, the payment
        /// carries a card surcharge (SVC fee), the payment would go negative, or the card batch is settled.
        /// </summary>
        public void PartialRefundItems()
        {
            try
            {
                string merchantDb = "[" + Helper.Merchant_Name.Replace("]", "]]") + "]";
                bool hasTaxDetails = CurrentTableInDatabaseBLogicPOS7.Select("TABLE_NAME = 'SaleReceiptDetailTaxDetails'", "").Length > 0;

                using (SqlConnection conn = new SqlConnection(Helper.BLOGICPOS7_Connection))
                {
                    conn.Open();
                    using (SqlTransaction transaction = conn.BeginTransaction())
                    using (SqlCommand sqlCommand = conn.CreateCommand())
                    {
                        sqlCommand.Transaction = transaction;
                        RefundCheck before = ReadRefundState(sqlCommand, merchantDb, hasTaxDetails);

                        // ---- guards: nothing has been written yet --------------------------------------------
                        DataRow receipt = before.Receipt;
                        DataRow payment = before.Payment;
                        if (receipt == null) throw new RefundStop("Ticket not found.");
                        if (payment == null) throw new RefundStop("Payment not found on this ticket.");

                        string status = Utility.GetStringFromRow("TransactionStatusID", receipt);
                        if (status != "FIN")
                            throw new RefundStop($"The ticket is {status}; only FINISHED tickets can be refunded.");

                        foreach (ObjectValues item in requestModel.objectValues)
                        {
                            DataRow line = before.Lines.Rows.Cast<DataRow>().FirstOrDefault(r => string.Equals(r["SaleReceiptDetailID"].ToString(), item.SaleReceiptDetailID?.ToString(), StringComparison.OrdinalIgnoreCase));
                            if (line == null || Utility.GetIntFromRow("Status", line) == 2 || !string.IsNullOrWhiteSpace(Utility.GetStringFromRow("ReturnCode", line)))
                                throw new RefundStop("An item was already refunded or removed. Reload the ticket and try again.");
                            if (Utility.GetDecimalFromRow("Quantity", line) != item.Quantity)
                                throw new RefundStop("An item changed since the list was opened. Reload the ticket and try again.");
                        }

                        if (Utility.GetDecimalFromRow("SVCFeeAmount", payment) != 0)
                            throw new RefundStop("This payment has a card surcharge (SVC fee). Refunding it needs a manual fix - contact TruongDS.");

                        decimal refundTotal = requestModel.TotalAmount;
                        decimal refundTax = requestModel.TaxAmount;
                        if (Utility.GetDecimalFromRow("AmountTendered", payment) - refundTotal < 0)
                            throw new RefundStop($"The refund ({Utility.ConvertDecimalToMoneyString(refundTotal)}) is more than the payment " +
                                $"({Utility.ConvertDecimalToMoneyString(Utility.GetDecimalFromRow("AmountTendered", payment))}).");

                        DataRow sale = before.Sale;
                        if (before.IsCard)
                        {
                            if (sale == null)
                                throw new RefundStop("The card transaction of this payment is not in the Merchant database.");
                            if (!string.Equals(Utility.GetStringFromRow("TranStatus", sale), "ACTIVE", StringComparison.OrdinalIgnoreCase)
                                || !string.IsNullOrWhiteSpace(Utility.GetStringFromRow("Returned", sale)))
                                throw new RefundStop("The card transaction is voided or already returned in the Merchant database.");
                            if (string.Equals(Utility.GetStringFromRow("BatchStatus", sale), "Closed", StringComparison.OrdinalIgnoreCase))
                                throw new RefundStop("The card batch of this payment is already settled. Refunding it needs a manual fix - contact TruongDS.");
                            if (Utility.GetDecimalFromRow("Amount", sale) - refundTotal < 0)
                                throw new RefundStop("The refund is more than the card transaction amount.");
                        }

                        // ---- 1. original totals, stamped once before the first refund (as the POS does) ----------
                        if (receipt.Table.Columns.Contains("SaleReceiptOriginalInfo") && string.IsNullOrWhiteSpace(Utility.GetStringFromRow("SaleReceiptOriginalInfo", receipt)))
                        {
                            ExecuteLogged(sqlCommand, "update SaleReceipts set SaleReceiptOriginalInfo = @info where SaleReceiptID = @id and ISNULL(SaleReceiptOriginalInfo, '') = ''",
                                new SqlParameter("@info", BuildOriginalInfo(sqlCommand, receipt)),
                                new SqlParameter("@id", receipt["SaleReceiptID"]));
                        }

                        // ---- 2. lines --------------------------------------------------------------------------
                        foreach (ObjectValues item in requestModel.objectValues)
                        {
                            if (item.IsPartialRefund)
                            {
                                SplitRefundedQuantity(sqlCommand, item, hasTaxDetails);
                                continue;
                            }

                            int rows = ExecuteLoggedCount(sqlCommand, "update SaleReceiptDetails set ReturnCode = -113, Status = 2 where SaleReceiptDetailID = @id and Status <> 2",
                                new SqlParameter("@id", item.SaleReceiptDetailID));
                            if (rows != 1)
                                throw new RefundStop("An item was refunded at the same time by someone else. Nothing was changed.");

                            if (hasTaxDetails)
                                ExecuteLogged(sqlCommand, "update SaleReceiptDetailTaxDetails set ReturnCode = -113, Status = 2 where SaleReceiptDetailID = @id",
                                    new SqlParameter("@id", item.SaleReceiptDetailID));
                        }

                        // ---- 3. header ------------------------------------------------------------------------
                        decimal newTotal = Utility.GetDecimalFromRow("TotalAmount", receipt) - refundTotal;
                        ExecuteLogged(sqlCommand, "update SaleReceipts set TotalAmount = @total, PaidAmount = @paid, TaxAmount = @tax where SaleReceiptID = @id",
                            new SqlParameter("@total", newTotal),
                            new SqlParameter("@paid", Utility.GetDecimalFromRow("PaidAmount", receipt) - refundTotal),
                            new SqlParameter("@tax", Utility.GetDecimalFromRow("TaxAmount", receipt) - refundTax),
                            new SqlParameter("@id", receipt["SaleReceiptID"]));

                        // ---- 4. the chosen payment -------------------------------------------------------------
                        ExecuteLogged(sqlCommand, "update SaleReceiptPaymentDetails set AmountTendered = @a where SaleReceiptPaymentDetailID = @id",
                            new SqlParameter("@a", Utility.GetDecimalFromRow("AmountTendered", payment) - refundTotal),
                            new SqlParameter("@id", payment["SaleReceiptPaymentDetailID"]));

                        // ---- 5. card Sale (Sale and PreAuth alike) + its batch ----------------------------------
                        if (before.IsCard)
                        {
                            decimal oldAmount = Utility.GetDecimalFromRow("Amount", sale);
                            decimal oldTotal = Utility.GetDecimalFromRow("Total", sale);
                            decimal newSaleTotal = oldTotal - refundTotal;
                            List<SqlParameter> p = new List<SqlParameter>
                            {
                                new SqlParameter("@amount", oldAmount - refundTotal),
                                new SqlParameter("@total", newSaleTotal),
                                new SqlParameter("@id", sale["ID"]),
                            };
                            string set = "Amount = @amount, Total = @total";
                            Action<string, decimal> follow = (column, value) =>
                            {
                                if (!sale.Table.Columns.Contains(column) || sale[column] == DBNull.Value) return;
                                set += $", [{column}] = @{column}";
                                p.Add(new SqlParameter("@" + column, value));
                            };
                            if (Utility.GetDecimalFromRow("AuthorizeAmount", sale) == oldAmount)
                                follow("AuthorizeAmount", oldAmount - refundTotal);
                            follow("SubTotal", Utility.GetDecimalFromRow("SubTotal", sale) - (refundTotal - refundTax));
                            follow("TaxAmount", Utility.GetDecimalFromRow("TaxAmount", sale) - refundTax);
                            follow("TotalAmount", Utility.GetDecimalFromRow("TotalAmount", sale) - refundTotal);
                            decimal oldFee = Utility.GetDecimalFromRow("MerchantFee", sale);
                            if (oldFee != 0 && oldTotal != 0)
                                follow("MerchantFee", Math.Round(oldFee * newSaleTotal / oldTotal, 2, MidpointRounding.AwayFromZero));

                            ExecuteLogged(sqlCommand, $"update {merchantDb}.dbo.TransactionDetail set {set} where ID = @id", p.ToArray());

                            string batchUnique = Utility.GetStringFromRow("BatchUnique", sale);
                            if (!string.IsNullOrWhiteSpace(batchUnique))
                                RecalculateBatch(sqlCommand, merchantDb, batchUnique);
                        }

                        // ---- 6. check the result against what was read at the start ------------------------------
                        RefundCheck after = ReadRefundState(sqlCommand, merchantDb, hasTaxDetails);
                        List<string> gaps = new List<string>();
                        Action<string, decimal, decimal> expect = (what, actual, expected) =>
                        {
                            if (Math.Abs(actual - expected) > 0.005m) gaps.Add($"{what}: {actual:0.00} (expected {expected:0.00})");
                        };
                        expect("Ticket total", Utility.GetDecimalFromRow("TotalAmount", after.Receipt), Utility.GetDecimalFromRow("TotalAmount", receipt) - refundTotal);
                        expect("Ticket tax", Utility.GetDecimalFromRow("TaxAmount", after.Receipt), Utility.GetDecimalFromRow("TaxAmount", receipt) - refundTax);
                        expect("Ticket paid", Utility.GetDecimalFromRow("PaidAmount", after.Receipt), Utility.GetDecimalFromRow("PaidAmount", receipt) - refundTotal);
                        expect("Kept lines (price + tax)", after.ActiveLinesTotal, before.ActiveLinesTotal - refundTotal);
                        expect("Kept lines tax", after.ActiveLinesTax, before.ActiveLinesTax - refundTax);
                        if (hasTaxDetails && before.ActiveTaxRowsTotal == before.ActiveLinesTax)
                            expect("Kept tax rows = kept lines tax", after.ActiveTaxRowsTotal, after.ActiveLinesTax);
                        expect("Payment", Utility.GetDecimalFromRow("AmountTendered", after.Payment), Utility.GetDecimalFromRow("AmountTendered", payment) - refundTotal);
                        if (before.IsCard)
                        {
                            expect("Card amount", Utility.GetDecimalFromRow("Amount", after.Sale), Utility.GetDecimalFromRow("Amount", sale) - refundTotal);
                            expect("Card total", Utility.GetDecimalFromRow("Total", after.Sale), Utility.GetDecimalFromRow("Total", sale) - refundTotal);
                            // The e-receipt pairs the POS payment with the card row: keep that link exactly as it was.
                            decimal linkBefore = Utility.GetDecimalFromRow("Amount", sale) - Utility.GetDecimalFromRow("AmountTendered", payment);
                            decimal linkAfter = Utility.GetDecimalFromRow("Amount", after.Sale) - Utility.GetDecimalFromRow("AmountTendered", after.Payment);
                            expect("Card amount - payment", linkAfter, linkBefore);
                        }
                        if (gaps.Count > 0)
                            throw new RefundStop("The refund did not add up, nothing was changed:" + Environment.NewLine + string.Join(Environment.NewLine, gaps));

                        transaction.Commit();
                    }
                }

                IsSuccess = true;
            }
            catch (RefundStop stop)
            {
                ErrorMessage = stop.Message;
                BLogicLogger.Error("PartialRefundItems stopped: " + stop.Message);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                BLogicLogger.Exception("PartialRefundItems in ProcessForm", ex);
            }
        }

        /// <summary>A refund rule was not met; the message is shown to the user as is.</summary>
        private sealed class RefundStop : Exception
        {
            public RefundStop(string message) : base(message) { }
        }

        /// <summary>Snapshot of everything a refund touches, read inside the refund transaction.</summary>
        private sealed class RefundCheck
        {
            public DataRow Receipt;
            public DataRow Payment;
            public DataRow Sale;
            public DataTable Lines;
            public bool IsCard;
            public decimal ActiveLinesTotal;
            public decimal ActiveLinesTax;
            public decimal ActiveTaxRowsTotal;
        }

        private RefundCheck ReadRefundState(SqlCommand sqlCommand, string merchantDb, bool hasTaxDetails)
        {
            RefundCheck state = new RefundCheck();
            // UPDLOCK: the rows stay ours until COMMIT, so nobody can refund the same lines in between.
            DataTable receipt = LoadRows(sqlCommand, "select * from SaleReceipts with (UPDLOCK) where SaleReceiptID = @id", requestModel.SaleReceiptID);
            state.Receipt = receipt.Rows.Count > 0 ? receipt.Rows[0] : null;

            DataTable payment = LoadRows(sqlCommand, "select * from SaleReceiptPaymentDetails with (UPDLOCK) where SaleReceiptPaymentDetailID = @id", requestModel.SaleReceiptPaymentDetailID);
            state.Payment = payment.Rows.Count > 0 && string.Equals(payment.Rows[0]["SaleReceiptID"].ToString(), requestModel.SaleReceiptID, StringComparison.OrdinalIgnoreCase)
                ? payment.Rows[0] : null;

            state.Lines = LoadRows(sqlCommand, "select * from SaleReceiptDetails with (UPDLOCK) where SaleReceiptID = @id", requestModel.SaleReceiptID);
            foreach (DataRow line in state.Lines.Rows)
            {
                if (Utility.GetIntFromRow("Status", line) == 2) continue;
                state.ActiveLinesTotal += Utility.GetDecimalFromRow("ExtendedPrice", line) + Utility.GetDecimalFromRow("TaxAmount", line);
                state.ActiveLinesTax += Utility.GetDecimalFromRow("TaxAmount", line);
            }

            if (hasTaxDetails)
            {
                DataTable tax = LoadRows(sqlCommand, "select TaxAmount, Status from SaleReceiptDetailTaxDetails where SaleReceiptID = @id", requestModel.SaleReceiptID);
                foreach (DataRow row in tax.Rows)
                    if (Utility.GetIntFromRow("Status", row) != 2)
                        state.ActiveTaxRowsTotal += Utility.GetDecimalFromRow("TaxAmount", row);
            }

            string cardID = state.Payment == null ? "" : Utility.GetStringFromRow("CreditCardTransactionID", state.Payment);
            state.IsCard = state.Payment != null && Utility.GetIntFromRow("PaymentMethodID", state.Payment) == 2 && !string.IsNullOrWhiteSpace(cardID);
            if (state.IsCard)
            {
                DataTable sale = LoadRows(sqlCommand, $"select * from {merchantDb}.dbo.TransactionDetail with (UPDLOCK) where ID = @id", cardID);
                state.Sale = sale.Rows.Count > 0 ? sale.Rows[0] : null;
            }
            return state;
        }

        /// <summary>The JSON the POS refund screen stamps into SaleReceiptOriginalInfo (SaleReceipt_New.ConvertSaleMouleToString).</summary>
        private static string BuildOriginalInfo(SqlCommand sqlCommand, DataRow receipt)
        {
            Func<string, decimal> money = column => receipt.Table.Columns.Contains(column) ? Utility.GetDecimalFromRow(column, receipt) : 0;
            decimal taxAmountVAT = 0;
            try
            {
                sqlCommand.Parameters.Clear();
                sqlCommand.CommandText = "select sum(TaxAmount) from SaleReceiptDetails where SaleReceiptID = @id and TaxType = 1 and (Status <> 2 or ISNULL(ReturnCode, '') = '113')";
                sqlCommand.Parameters.AddWithValue("@id", receipt["SaleReceiptID"]);
                taxAmountVAT = Utility.GetDecimalFromObject(sqlCommand.ExecuteScalar());
            }
            catch (SqlException)
            {
                taxAmountVAT = 0; // older databases without TaxType
            }
            finally
            {
                sqlCommand.Parameters.Clear();
            }

            SaleReciptModule original = new SaleReciptModule
            {
                SaleReceiptNumber = Utility.GetStringFromRow("SaleReceiptNumber", receipt),
                TotalAmount = money("TotalAmount"),
                TotalDiscountAmount = money("TotalDiscountAmount"),
                TaxAmount = money("TaxAmount"),
                SalesOrderFee = money("SalesOrderFee"),
                ServiceFee = money("ServiceFee"),
                ServiceFeeTaxAmount = money("ServiceFeeTaxAmount"),
                AreaFeeAmount = money("AreaFeeAmount"),
                AreaFeeTaxAmount = money("AreaFeeTaxAmount"),
                PartyServiceFeeAmount = money("PartyServiceFeeAmount"),
                PartyServiceFeeTaxAmount = money("PartyServiceFeeTaxAmount"),
                AutoGratuityAmount = money("AutoGratuityAmount"),
                AutoGratuityTaxAmount = money("AutoGratuityTaxAmount"),
                ExtraFeeAmount = money("ExtraFeeAmount"),
                ExtraFeeTaxAmount = money("ExtraFeeTaxAmount"),
                ServiceChargeTaxAmount = money("ServiceChargeTaxAmount"),
                SVCFeeAmount = money("SVCFeePaidAmount"),
                SVCFeeTaxAmount = money("SVCFeeTaxPaidAmount"),
                CashDiscountAmount = money("CashDiscountPaidAmount"),
                CashDiscountTaxAmount = money("CashDiscountTaxPaidAmount"),
                TaxAmountVAT = taxAmountVAT,
            };
            return Newtonsoft.Json.JsonConvert.SerializeObject(original, Newtonsoft.Json.Formatting.Indented,
                new Newtonsoft.Json.JsonSerializerSettings { DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Ignore });
        }

        /// <summary>Same totals as UpdateBatchByBatchUnique, but inside the refund transaction.</summary>
        private static void RecalculateBatch(SqlCommand sqlCommand, string merchantDb, string batchUnique)
        {
            string td = merchantDb + ".dbo.TransactionDetail";
            ExecuteLogged(sqlCommand, $@"
;with t as (
    select sum(case when (TranType = 'Sale' or TranType = 'PreAuthCapture') and TranStatus = 'ACTIVE' then Total else 0 end) - sum(case when TranType = 'Return' then Total else 0 end) as Total,
           sum(case when (TranType = 'Sale' or TranType = 'PreAuthCapture') and TranStatus = 'ACTIVE' then SVCFeeForGratuity else 0 end) as TotalSVCFeeForTip,
           sum(case when (TranType = 'Sale' or TranType = 'PreAuthCapture') and TranStatus = 'ACTIVE' then Amount - SVCFee else 0 end) as TotalSales,
           sum(case when (TranType = 'Sale' or TranType = 'PreAuthCapture') and TranStatus = 'ACTIVE' then Gratuity else 0 end) as TotalTips,
           sum(case when (TranType = 'Sale' or TranType = 'PreAuthCapture') and TranStatus = 'ACTIVE' then SVCFee else 0 end) - sum(case when TranType = 'Return' then SVCFee else 0 end) as TotalSVCFeeAmount,
           sum(case when (TranType = 'Sale' or TranType = 'PreAuthCapture') and TranStatus = 'ACTIVE' then Total - MerchantFee else 0 end) - sum(case when TranType = 'Return' then Total else 0 end) as DepositAmount,
           sum(case when (TranType = 'Sale' or TranType = 'PreAuthCapture') and TranStatus = 'ACTIVE' then MerchantFee else 0 end) as MerchantFeeAmount,
           count(ID) as TicketCount
      from {td} where BatchUnique = @b)
update b set Total = t.Total, TotalSVCFeeForTip = t.TotalSVCFeeForTip, TotalSales = t.TotalSales, TotalTips = t.TotalTips,
             TotalSVCFeeAmount = t.TotalSVCFeeAmount, DepositAmount = t.DepositAmount, MerchantFeeAmount = t.MerchantFeeAmount, Quantity = t.TicketCount
  from {merchantDb}.dbo.Batch b cross join t
 where b.BatchUnique = @b;
update {td} set BatchTotal = (select Total from {merchantDb}.dbo.Batch where BatchUnique = @b) where BatchUnique = @b
   and exists (select 1 from {merchantDb}.dbo.Batch where BatchUnique = @b);", new SqlParameter("@b", batchUnique));
        }

        private static int ExecuteLoggedCount(SqlCommand sqlCommand, string sql, params SqlParameter[] parameters)
        {
            BLogicLogger.Error($"Run Script for {sqlCommand.Connection.Database} : {sql} [{string.Join(", ", parameters.Select(p => p.ParameterName + "=" + p.Value))}]");
            sqlCommand.Parameters.Clear();
            sqlCommand.CommandText = sql;
            sqlCommand.Parameters.AddRange(parameters);
            int rows = sqlCommand.ExecuteNonQuery();
            sqlCommand.Parameters.Clear();
            return rows;
        }


        /// <summary>
        /// Partial-quantity refund, done the way the POS does it: the original row keeps the remaining
        /// quantity and the refunded share is saved as a copy with Status = 2 / ReturnCode = -113.
        /// Its SaleReceiptDetailTaxDetails rows are split the same way.
        /// </summary>
        private void SplitRefundedQuantity(SqlCommand sqlCommand, ObjectValues item, bool hasTaxDetails)
        {
            DataTable dtDetail = LoadRows(sqlCommand, "select * from SaleReceiptDetails where SaleReceiptDetailID = @id", item.SaleReceiptDetailID);
            if (dtDetail.Rows.Count == 0)
                throw new Exception($"SaleReceiptDetail {item.SaleReceiptDetailID} not found.");

            DataRow detail = dtDetail.Rows[0];
            decimal quantity = Utility.GetDecimalFromRow("Quantity", detail);
            decimal extendedPrice = Utility.GetDecimalFromRow("ExtendedPrice", detail);
            decimal taxAmount = Utility.GetDecimalFromRow("TaxAmount", detail);
            Guid refundedDetailID = Guid.NewGuid();
            Dictionary<string, decimal> refundedFinals = SplitFinalColumns(detail, item, quantity, extendedPrice, taxAmount);

            Dictionary<string, object> copy = new Dictionary<string, object>
            {
                ["SaleReceiptDetailID"] = refundedDetailID,
                ["Quantity"] = item.RefundQuantity,
                ["ExtendedPrice"] = item.RefundExtendedPrice,
                ["TaxAmount"] = item.RefundTaxAmount,
                ["ReturnCode"] = "-113",
                ["Status"] = "2",
                ["LastModifyTime"] = DateTime.Now,
            };
            foreach (var final in refundedFinals)
                copy[final.Key] = final.Value;
            InsertCopy(sqlCommand, "SaleReceiptDetails", detail, copy);

            List<SqlParameter> keep = new List<SqlParameter>
            {
                new SqlParameter("@q", quantity - item.RefundQuantity),
                new SqlParameter("@e", extendedPrice - item.RefundExtendedPrice),
                new SqlParameter("@t", taxAmount - item.RefundTaxAmount),
                new SqlParameter("@id", detail["SaleReceiptDetailID"]),
            };
            string keepFinals = "";
            foreach (var final in refundedFinals)
            {
                string p = "@f" + keep.Count;
                keepFinals += $", [{final.Key}] = {p}";
                keep.Add(new SqlParameter(p, Utility.GetDecimalFromRow(final.Key, detail) - final.Value));
            }
            ExecuteLogged(sqlCommand, $"update SaleReceiptDetails set Quantity = @q, ExtendedPrice = @e, TaxAmount = @t{keepFinals} where SaleReceiptDetailID = @id",
                keep.ToArray());

            if (!hasTaxDetails)
                return;

            DataTable dtTax = LoadRows(sqlCommand, "select * from SaleReceiptDetailTaxDetails where SaleReceiptDetailID = @id", item.SaleReceiptDetailID);
            if (dtTax.Rows.Count == 0)
                return;

            // Split each tax line by the same share; when the lines add up to the row's tax, push the
            // rounding cent onto the largest line so the copies add up to exactly what the UI showed.
            decimal[] refundTax = new decimal[dtTax.Rows.Count];
            decimal taxLinesTotal = 0, refundTaxTotal = 0;
            int largest = 0;
            for (int i = 0; i < dtTax.Rows.Count; i++)
            {
                decimal lineTax = Utility.GetDecimalFromRow("TaxAmount", dtTax.Rows[i]);
                refundTax[i] = RefundMathPortion(lineTax, item);
                taxLinesTotal += lineTax;
                refundTaxTotal += refundTax[i];
                if (Math.Abs(lineTax) > Math.Abs(Utility.GetDecimalFromRow("TaxAmount", dtTax.Rows[largest])))
                    largest = i;
            }
            if (taxLinesTotal == taxAmount)
                refundTax[largest] += item.RefundTaxAmount - refundTaxTotal;

            for (int i = 0; i < dtTax.Rows.Count; i++)
            {
                DataRow line = dtTax.Rows[i];
                decimal lineQuantity = Utility.GetDecimalFromRow("Quantity", line);
                decimal lineExtended = Utility.GetDecimalFromRow("ExtendedPrice", line);
                decimal lineTax = Utility.GetDecimalFromRow("TaxAmount", line);

                decimal refundLineQuantity = lineQuantity == quantity ? item.RefundQuantity : Math.Round(lineQuantity * item.RefundPart / item.RefundWhole, 2, MidpointRounding.AwayFromZero);
                decimal refundLineExtended = lineExtended == extendedPrice ? item.RefundExtendedPrice : RefundMathPortion(lineExtended, item);

                InsertCopy(sqlCommand, "SaleReceiptDetailTaxDetails", line, new Dictionary<string, object>
                {
                    ["SaleReceiptDetailTaxDetailID"] = Guid.NewGuid(),
                    ["SaleReceiptDetailID"] = refundedDetailID,
                    ["Quantity"] = refundLineQuantity,
                    ["ExtendedPrice"] = refundLineExtended,
                    ["TaxAmount"] = refundTax[i],
                    ["ReturnCode"] = "-113",
                    ["Status"] = "2",
                    ["LastModifyTime"] = DateTime.Now,
                });

                ExecuteLogged(sqlCommand, "update SaleReceiptDetailTaxDetails set Quantity = @q, ExtendedPrice = @e, TaxAmount = @t where SaleReceiptDetailTaxDetailID = @id",
                    new SqlParameter("@q", lineQuantity - refundLineQuantity),
                    new SqlParameter("@e", lineExtended - refundLineExtended),
                    new SqlParameter("@t", lineTax - refundTax[i]),
                    new SqlParameter("@id", line["SaleReceiptDetailTaxDetailID"]));
            }
        }

        /// <summary>The Final* report columns of SaleReceiptDetails (totals per line) that a split must share out.</summary>
        private static readonly string[] FinalColumns = { "FinalQuantity", "FinalDiscountAmount", "FinalCashDiscount", "FinalCoinDiscount",
            "FinalTaxAmount", "FinalTotalSales", "FinalGrossSales", "FinalNetSales", "FinalNetSalesTax" };

        /// <summary>
        /// Refunded share of each Final* column. Values that equal the row's quantity / price / tax / price+tax
        /// take exactly the refunded share of those, so the split row stays consistent with itself.
        /// </summary>
        private static Dictionary<string, decimal> SplitFinalColumns(DataRow row, ObjectValues item, decimal quantity, decimal extendedPrice, decimal taxAmount)
        {
            Dictionary<string, decimal> parts = new Dictionary<string, decimal>();
            foreach (string name in FinalColumns)
            {
                if (!row.Table.Columns.Contains(name) || row[name] == DBNull.Value)
                    continue;

                decimal value = Utility.GetDecimalFromRow(name, row);
                decimal part;
                if (name == "FinalQuantity")
                    part = value == quantity ? item.RefundQuantity : Math.Round(value * item.RefundPart / item.RefundWhole, 2, MidpointRounding.AwayFromZero);
                else if (value == extendedPrice + taxAmount)
                    part = item.RefundExtendedPrice + item.RefundTaxAmount;
                else if (value == extendedPrice)
                    part = item.RefundExtendedPrice;
                else if (value == taxAmount)
                    part = item.RefundTaxAmount;
                else
                    part = RefundMathPortion(value, item);
                parts[name] = part;
            }
            return parts;
        }

        private static decimal RefundMathPortion(decimal value, ObjectValues item)
        {
            return SaleReceipt.RefundMath.Portion(value, item.RefundPart, item.RefundWhole);
        }

        private static DataTable LoadRows(SqlCommand sqlCommand, string sql, object id)
        {
            DataTable table = new DataTable();
            sqlCommand.Parameters.Clear();
            sqlCommand.CommandText = sql;
            sqlCommand.Parameters.AddWithValue("@id", id ?? DBNull.Value);
            using (SqlDataReader reader = sqlCommand.ExecuteReader())
                table.Load(reader);
            sqlCommand.Parameters.Clear();
            return table;
        }

        private static void ExecuteLogged(SqlCommand sqlCommand, string sql, params SqlParameter[] parameters)
        {
            BLogicLogger.Error($"Run Script for {sqlCommand.Connection.Database} : {sql} [{string.Join(", ", parameters.Select(p => p.ParameterName + "=" + p.Value))}]");
            sqlCommand.Parameters.Clear();
            sqlCommand.CommandText = sql;
            sqlCommand.Parameters.AddRange(parameters);
            sqlCommand.ExecuteNonQuery();
            sqlCommand.Parameters.Clear();
        }

        /// <summary>
        /// Inserts a copy of <paramref name="source"/> (a "select *" row) with some columns overridden.
        /// Identity, computed and rowversion columns are left to SQL Server; overrides for columns the
        /// table does not have are ignored, so older POS databases work too.
        /// </summary>
        private static void InsertCopy(SqlCommand sqlCommand, string table, DataRow source, IDictionary<string, object> overrides)
        {
            HashSet<string> skip = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            sqlCommand.Parameters.Clear();
            sqlCommand.CommandText = "select c.name from sys.columns c where c.object_id = OBJECT_ID(@t) " +
                "and (c.is_identity = 1 or c.is_computed = 1 or TYPE_NAME(c.system_type_id) = 'timestamp')";
            sqlCommand.Parameters.AddWithValue("@t", table);
            using (SqlDataReader reader = sqlCommand.ExecuteReader())
                while (reader.Read())
                    skip.Add(reader.GetString(0));
            sqlCommand.Parameters.Clear();

            Dictionary<string, object> values = new Dictionary<string, object>(overrides, StringComparer.OrdinalIgnoreCase);
            List<string> columns = new List<string>();
            List<string> names = new List<string>();
            List<SqlParameter> parameters = new List<SqlParameter>();
            foreach (DataColumn column in source.Table.Columns)
            {
                if (skip.Contains(column.ColumnName))
                    continue;

                object value = values.TryGetValue(column.ColumnName, out object v) ? ConvertForColumn(v, column.DataType) : source[column];
                SqlParameter parameter = new SqlParameter("@p" + parameters.Count, value ?? DBNull.Value);
                if (column.DataType == typeof(byte[]))
                    parameter.SqlDbType = SqlDbType.VarBinary;
                columns.Add("[" + column.ColumnName + "]");
                names.Add(parameter.ParameterName);
                parameters.Add(parameter);
            }

            ExecuteLogged(sqlCommand, $"insert into {table} ({string.Join(", ", columns)}) values ({string.Join(", ", names)})", parameters.ToArray());
        }

        private static object ConvertForColumn(object value, Type type)
        {
            if (value == null || value == DBNull.Value)
                return DBNull.Value;
            if (type == typeof(Guid))
                return value is Guid ? value : new Guid(value.ToString());
            if (type == typeof(string))
                return value is Guid g ? g.ToString() : Convert.ToString(value, System.Globalization.CultureInfo.InvariantCulture);
            return Convert.ChangeType(value, type, System.Globalization.CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Adds an Open Price / Negative Price item (no tax) to a finished ticket and moves the chosen
        /// payment by the same amount. For a card payment the Merchant TransactionDetail follows and its
        /// batch is recalculated. POS writes are one transaction; the Merchant write is committed right after.
        /// </summary>
        public void AddSpecialItem()
        {
            SpecialItemValues values = requestModel.specialItemValues;
            try
            {
                object batchUnique = null;

                using (SqlConnection pos = new SqlConnection(Helper.BLOGICPOS7_Connection))
                using (SqlConnection merchant = new SqlConnection(Helper.Merchant_Connection))
                {
                    pos.Open();
                    SqlTransaction posTransaction = pos.BeginTransaction();
                    SqlCommand posCommand = pos.CreateCommand();
                    posCommand.Transaction = posTransaction;

                    DataTable dtReceipt = LoadRows(posCommand, "select * from SaleReceipts where SaleReceiptID = @id", values.SaleReceiptID);
                    DataTable dtItem = LoadRows(posCommand, "select * from Items where ItemID = @id", values.ItemID);
                    DataTable dtPayment = LoadRows(posCommand, "select * from SaleReceiptPaymentDetails where SaleReceiptPaymentDetailID = @id", values.SaleReceiptPaymentDetailID);
                    // An existing item line of the ticket is the template, so every POS column gets a sensible value.
                    DataTable dtTemplate = LoadRows(posCommand, "select top 1 * from SaleReceiptDetails where SaleReceiptID = @id and ISNULL(PriceInitial,'') in ('','P') " +
                        "order by case when Status = 2 then 1 else 0 end, Range", values.SaleReceiptID);

                    if (dtReceipt.Rows.Count == 0) throw new Exception("Ticket not found.");
                    if (dtItem.Rows.Count == 0) throw new Exception("Special item not found.");
                    if (dtPayment.Rows.Count == 0) throw new Exception("Payment not found.");
                    if (dtTemplate.Rows.Count == 0) throw new Exception("The ticket has no item line to copy from.");

                    posCommand.Parameters.Clear();
                    posCommand.CommandText = "select ISNULL(MAX(Range), -1) from SaleReceiptDetails where SaleReceiptID = @id";
                    posCommand.Parameters.AddWithValue("@id", values.SaleReceiptID);
                    int nextRange = Convert.ToInt32(posCommand.ExecuteScalar()) + 1;
                    posCommand.Parameters.Clear();

                    DateTime now = DateTime.Now;
                    decimal amount = values.Amount;
                    DataRow template = dtTemplate.Rows[0];
                    InsertCopy(posCommand, "SaleReceiptDetails", template, BuildSpecialItemLine(template, dtItem.Rows[0], values, nextRange, now));

                    // Ticket totals
                    DataRow receipt = dtReceipt.Rows[0];
                    decimal newTotal = Utility.GetDecimalFromRow("TotalAmount", receipt) + amount;
                    List<SqlParameter> receiptParams = new List<SqlParameter>
                    {
                        new SqlParameter("@total", newTotal),
                        new SqlParameter("@paid", Utility.GetDecimalFromRow("PaidAmount", receipt) + amount),
                        new SqlParameter("@id", receipt["SaleReceiptID"]),
                    };
                    string receiptSet = "TotalAmount = @total, PaidAmount = @paid";
                    if (receipt.Table.Columns.Contains("TotalQuantity"))
                        receiptSet += ", TotalQuantity = ISNULL(TotalQuantity, 0) + 1";
                    if (receipt.Table.Columns.Contains("DetailSummary"))
                    {
                        string summary = Utility.GetStringFromRow("DetailSummary", receipt);
                        string line = $"(1) {values.ItemName}";
                        receiptSet += ", DetailSummary = @summary";
                        receiptParams.Add(new SqlParameter("@summary", string.IsNullOrWhiteSpace(summary) ? line : summary + ", " + line));
                    }
                    if (receipt.Table.Columns.Contains("LastModifyDateTime"))
                    {
                        receiptSet += ", LastModifyDateTime = @now";
                        receiptParams.Add(new SqlParameter("@now", now));
                    }
                    ExecuteLogged(posCommand, $"update SaleReceipts set {receiptSet} where SaleReceiptID = @id", receiptParams.ToArray());

                    // Payment moves by the same amount
                    DataRow payment = dtPayment.Rows[0];
                    decimal newTendered = Utility.GetDecimalFromRow("AmountTendered", payment) + amount;
                    if (newTendered < 0)
                        throw new Exception("The payment amount cannot go below zero.");
                    ExecuteLogged(posCommand, "update SaleReceiptPaymentDetails set AmountTendered = @a where SaleReceiptPaymentDetailID = @id",
                        new SqlParameter("@a", newTendered),
                        new SqlParameter("@id", payment["SaleReceiptPaymentDetailID"]));

                    // Card payment: the Merchant transaction follows (Sale and PreAuth alike)
                    SqlTransaction merchantTransaction = null;
                    string cardTransactionID = Utility.GetStringFromRow("CreditCardTransactionID", payment);
                    if (Utility.GetIntFromRow("PaymentMethodID", payment) == 2 && !string.IsNullOrWhiteSpace(cardTransactionID))
                    {
                        merchant.Open();
                        merchantTransaction = merchant.BeginTransaction();
                        SqlCommand merchantCommand = merchant.CreateCommand();
                        merchantCommand.Transaction = merchantTransaction;

                        DataTable dtTransaction = LoadRows(merchantCommand, "select * from TransactionDetail where ID = @id", cardTransactionID);
                        if (dtTransaction.Rows.Count > 0)
                        {
                            DataRow transaction = dtTransaction.Rows[0];
                            decimal oldAmount = Utility.GetDecimalFromRow("Amount", transaction);
                            List<SqlParameter> tranParams = new List<SqlParameter>
                            {
                                new SqlParameter("@amount", oldAmount + amount),
                                new SqlParameter("@total", Utility.GetDecimalFromRow("Total", transaction) + amount),
                                new SqlParameter("@id", transaction["ID"]),
                            };
                            string tranSet = "Amount = @amount, Total = @total";
                            if (transaction.Table.Columns.Contains("AuthorizeAmount") && Utility.GetDecimalFromRow("AuthorizeAmount", transaction) == oldAmount)
                            {
                                tranSet += ", AuthorizeAmount = @amount";
                            }
                            if (transaction.Table.Columns.Contains("TotalAmount"))
                            {
                                tranSet += ", TotalAmount = @ticketTotal";
                                tranParams.Add(new SqlParameter("@ticketTotal", newTotal));
                            }
                            ExecuteLogged(merchantCommand, $"update TransactionDetail set {tranSet} where ID = @id", tranParams.ToArray());

                            if (!string.IsNullOrWhiteSpace(Utility.GetStringFromRow("BatchUnique", transaction)))
                                batchUnique = transaction["BatchUnique"];
                        }
                    }

                    posTransaction.Commit();
                    merchantTransaction?.Commit();
                }

                // Runs on its own connection, after the TransactionDetail change is committed.
                if (batchUnique != null)
                    UpdateBatchByBatchUnique(batchUnique);

                IsSuccess = true;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                BLogicLogger.Exception("AddSpecialItem in ProcessForm", ex);
            }
        }

        /// <summary>Column values for the new special-item line, overriding what is copied from the template line.</summary>
        private static Dictionary<string, object> BuildSpecialItemLine(DataRow template, DataRow item, SpecialItemValues values, int range, DateTime now)
        {
            Dictionary<string, object> line = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
            Func<string, object> fromItem = name => item.Table.Columns.Contains(name) ? item[name] : DBNull.Value;

            // Nothing about discounts, commissions, weights or "price before X" carries over from the template line.
            foreach (DataColumn column in template.Table.Columns)
            {
                bool numeric = column.DataType == typeof(decimal) || column.DataType == typeof(double) || column.DataType == typeof(float)
                    || column.DataType == typeof(int) || column.DataType == typeof(short) || column.DataType == typeof(long) || column.DataType == typeof(byte);
                if (numeric && System.Text.RegularExpressions.Regex.IsMatch(column.ColumnName, "Discount|Commission|ServiceFee|Before|Redeem|Weight|MarkDown|OpenPriceValue",
                        System.Text.RegularExpressions.RegexOptions.IgnoreCase))
                    line[column.ColumnName] = 0;
            }

            line["SaleReceiptDetailID"] = Guid.NewGuid();
            line["ItemID"] = item["ItemID"];
            line["Description"] = values.ItemName;
            foreach (string name in new[] { "Description2", "Description3", "Comments", "ReasonComment", "Attribute", "Size", "RefSaleReceiptDetailID",
                "EBTPaid", "BulkID", "BulkGroupID", "MMID", "MMGroupID", "TPID", "DiscountCode", "CategoryDiscountType", "MMLeastExpensiveDiscountType" })
            {
                if (template.Table.Columns.Contains(name))
                    line[name] = template.Table.Columns[name].DataType == typeof(string) ? (object)"" : DBNull.Value;
            }
            line["DescriptionChinese"] = fromItem("DescriptionChinese");
            line["ShortDescription"] = fromItem("ShortDescription");
            line["Lookup_Code"] = fromItem("LookupCode");
            line["ItemUnitCost"] = fromItem("ItemCost");
            line["ItemGroupID"] = fromItem("ItemGroupID");
            line["EBTType"] = fromItem("EBTType");
            line["InventoryUpdatable"] = false;

            foreach (string name in new[] { "Quantity", "QtyPerItem", "FinalQuantity", "QuantityKDS" })
                line[name] = 1;
            line["QuantitySold"] = 0;

            foreach (string name in new[] { "Price", "RegularPrice", "DualPrice", "PriceCash", "PriceCC", "ExtendedPrice",
                "FinalTotalSales", "FinalGrossSales", "FinalNetSales", "FinalNetSalesTax" })
                line[name] = values.Amount;

            // Special items are added without tax; TaxCodeID stays as on the template, like the POS does for non-taxable lines.
            line["Taxable"] = false;
            line["TaxRate"] = 0;
            line["TaxAmount"] = 0;
            line["FinalTaxAmount"] = 0;

            line["GroupID"] = Guid.NewGuid().ToString();
            line["Range"] = range;
            line["KDSIDDetail"] = Guid.NewGuid();
            line["PriceInitial"] = DBNull.Value;
            line["ReturnCode"] = DBNull.Value;
            line["ModifierLevelID"] = DBNull.Value;
            line["ModifierType"] = "Normal";
            if (Utility.GetIntFromRow("Status", template) == 2)
                line["Status"] = 0;
            foreach (string name in new[] { "IsCompItem", "IsDeposit", "IsSplitItems", "IsModifierCombine", "IsOverwritePrintModifier", "MMIsApplyItem",
                "IsLockDiscount", "IsManualWeight" })
                line[name] = false;
            line["TPStatus"] = 0;
            line["HoldMinutes"] = 0;
            foreach (string name in new[] { "LastModifyTime", "OrderDateTime", "StampDateTime", "HoldDateTime", "KDSDoneDateTime", "COMPDateTime", "TPTime" })
                line[name] = now;

            return line;
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
                            paymentEntity.SVCFeeForServiceCharge = Helper.CalculateSVCFee(paymentEntity.PaidServiceFee - dtaxsc, isApplyTaxCustomFee, saleEntity.SVCFeeTaxRate, paymentEntity.SVCFeeTaxAmount, _dTaxIncludeInSVCFee, _svcFee, _svcFeeType);
                            paymentEntity.SVCFeeForServiceFee = Helper.CalculateSVCFee(paymentEntity.ServiceFee - paymentEntity.ServiceFeeTaxAmount, isApplyTaxCustomFee, saleEntity.SVCFeeTaxRate, paymentEntity.SVCFeeTaxAmount, _dTaxIncludeInSVCFee, _svcFee, _svcFeeType);
                            paymentEntity.SVCFeeForPartyServiceFeeAmount = Helper.CalculateSVCFee(paymentEntity.PartyServiceFeeAmount - paymentEntity.PartyServiceFeeTaxAmount, isApplyTaxCustomFee, saleEntity.SVCFeeTaxRate, paymentEntity.SVCFeeTaxAmount, _dTaxIncludeInSVCFee, _svcFee, _svcFeeType);
                            paymentEntity.SVCFeeForAreaFeeAmount = Helper.CalculateSVCFee(paymentEntity.AreaFeeAmount - paymentEntity.AreaFeeTaxAmount, isApplyTaxCustomFee, saleEntity.SVCFeeTaxRate, paymentEntity.SVCFeeTaxAmount, _dTaxIncludeInSVCFee, _svcFee, _svcFeeType);
                            paymentEntity.SVCFeeForAutoGratuityAmount = Helper.CalculateSVCFee(paymentEntity.AutoGratuityAmount - paymentEntity.AutoGratuityTaxAmount, isApplyTaxCustomFee, saleEntity.SVCFeeTaxRate, paymentEntity.SVCFeeTaxAmount, _dTaxIncludeInSVCFee, _svcFee, _svcFeeType);

                            paymentEntity.MerchantFeeForServiceFee = Helper.CalculateMerchantFee((paymentEntity.ServiceFee - paymentEntity.ServiceFeeTaxAmount) + paymentEntity.SVCFeeForServiceFee, _merchantFee);
                            paymentEntity.MerchantFeeForAreaFeeAmount = Helper.CalculateMerchantFee((paymentEntity.AreaFeeAmount - paymentEntity.AreaFeeTaxAmount) + paymentEntity.SVCFeeForAreaFeeAmount, _merchantFee);
                            paymentEntity.MerchantFeeForPartyServiceFeeAmount = Helper.CalculateMerchantFee((paymentEntity.PartyServiceFeeAmount - paymentEntity.PartyServiceFeeTaxAmount) + paymentEntity.SVCFeeForPartyServiceFeeAmount, _merchantFee);
                            paymentEntity.MerchantFeeForAutoGratuityAmount = Helper.CalculateMerchantFee((paymentEntity.AutoGratuityAmount - paymentEntity.AutoGratuityTaxAmount) + paymentEntity.SVCFeeForAutoGratuityAmount, _merchantFee);
                        }
                        else
                        {
                            paymentEntity.SVCFeeForServiceCharge = Helper.CalculateSVCFee(paymentEntity.PaidServiceFee, false, 0, 0, _dTaxIncludeInSVCFee, _svcFee, _svcFeeType);
                            paymentEntity.SVCFeeForServiceFee = Helper.CalculateSVCFee(paymentEntity.ServiceFee, false, 0, 0, _dTaxIncludeInSVCFee, _svcFee, _svcFeeType);
                            paymentEntity.SVCFeeForPartyServiceFeeAmount = Helper.CalculateSVCFee(paymentEntity.PartyServiceFeeAmount, false, 0, 0, _dTaxIncludeInSVCFee, _svcFee, _svcFeeType);
                            paymentEntity.SVCFeeForAreaFeeAmount = Helper.CalculateSVCFee(paymentEntity.AreaFeeAmount, false, 0, 0, _dTaxIncludeInSVCFee, _svcFee, _svcFeeType);
                            paymentEntity.SVCFeeForAutoGratuityAmount = Helper.CalculateSVCFee(paymentEntity.AutoGratuityAmount, false, 0, 0, _dTaxIncludeInSVCFee, _svcFee, _svcFeeType);

                            paymentEntity.MerchantFeeForServiceFee = Helper.CalculateMerchantFee(paymentEntity.ServiceFee + paymentEntity.SVCFeeForServiceFee, _merchantFee);
                            paymentEntity.MerchantFeeForAreaFeeAmount = Helper.CalculateMerchantFee(paymentEntity.AreaFeeAmount + paymentEntity.SVCFeeForAreaFeeAmount, _merchantFee);
                            paymentEntity.MerchantFeeForPartyServiceFeeAmount = Helper.CalculateMerchantFee(paymentEntity.PartyServiceFeeAmount + paymentEntity.SVCFeeForPartyServiceFeeAmount, _merchantFee);
                            paymentEntity.MerchantFeeForAutoGratuityAmount = Helper.CalculateMerchantFee(paymentEntity.AutoGratuityAmount + paymentEntity.SVCFeeForAutoGratuityAmount, _merchantFee);
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
                        paymentEntity.MerchantFee = Helper.CalculateMerchantFee(paymentEntity.AmountTendered + paymentEntity.SVCFeeAmount + paymentEntity.Gratuity, _merchantFee);
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

                        if (transactionDetailEntity != null)
                        {
                            sql = $"update {Helper.Merchant_Name}.dbo.TransactionDetail set " +
                            $"Amount = '{paymentEntity.AmountTendered + paymentEntity.SVCFeeAmount}'," +
                            $"AuthorizeAmount = '{paymentEntity.AmountTendered + paymentEntity.SVCFeeAmount}'," +
                            $"Total = '{paymentEntity.AmountTendered + paymentEntity.SVCFeeAmount + paymentEntity.Gratuity}'," +
                            $"SVCFee = '{paymentEntity.SVCFeeAmount}'," +
                            $"MerchantFee = '{paymentEntity.MerchantFee}'," +
                            $"TaxAmount = '{saleEntity.TaxAmount}'," +
                            $"TotalAmount = '{saleEntity.TotalAmount}'" +
                            $"where ID = '{paymentEntity.CreditCardTransactionID}'";

                            await RunQueryUpdateAsync(conn, sql);

                            if (!string.IsNullOrWhiteSpace(transactionDetailEntity.BatchUnique))
                                UpdateBatchByBatchUnique(transactionDetailEntity.BatchUnique);
                        }

                    }
                }

                IsSuccess = true;

            }
            catch (Exception ex)
            {
                BLogicLogger.Exception("UpdateServiceChargeBySaleReceiptID in ProcessFormAsync", ex);
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
                    oPaymentDetail.SVCFeeForAutoGratuityAmount = Helper.CalculateSVCFee(oPaymentDetail.AutoGratuityAmount - oPaymentDetail.AutoGratuityTaxAmount, _svcFee, _svcFeeType, _dTaxIncludeInSVCFee, isApplyTaxCustomFee, oReceipt.SVCFeeTaxRate, oPaymentDetail.SVCFeeTaxAmount);
                    oPaymentDetail.SVCFeeForAreaFeeAmount = Helper.CalculateSVCFee(oPaymentDetail.AreaFeeAmount - oPaymentDetail.AreaFeeTaxAmount, _svcFee, _svcFeeType, _dTaxIncludeInSVCFee, isApplyTaxCustomFee, oReceipt.SVCFeeTaxRate, oPaymentDetail.SVCFeeTaxAmount);
                    oPaymentDetail.SVCFeeForPartyServiceFeeAmount = Helper.CalculateSVCFee(oPaymentDetail.PartyServiceFeeAmount - oPaymentDetail.PartyServiceFeeTaxAmount, _svcFee, _svcFeeType, _dTaxIncludeInSVCFee, isApplyTaxCustomFee, oReceipt.SVCFeeTaxRate, oPaymentDetail.SVCFeeTaxAmount);
                    oPaymentDetail.SVCFeeForServiceFee = Helper.CalculateSVCFee(oPaymentDetail.ServiceFee - oPaymentDetail.ServiceFeeTaxAmount, _svcFee, _svcFeeType, _dTaxIncludeInSVCFee, isApplyTaxCustomFee, oReceipt.SVCFeeTaxRate, oPaymentDetail.SVCFeeTaxAmount);
                    oPaymentDetail.SVCFeeForServiceCharge = Helper.CalculateSVCFee(oPaymentDetail.PaidServiceFee - taxsc, _svcFee, _svcFeeType, _dTaxIncludeInSVCFee, isApplyTaxCustomFee, oReceipt.SVCFeeTaxRate, oPaymentDetail.SVCFeeTaxAmount);
                }
                else
                {
                    oPaymentDetail.SVCFeeForServiceCharge = Helper.CalculateSVCFee(oPaymentDetail.PaidServiceFee, _svcFee, _svcFeeType, _dTaxIncludeInSVCFee);
                    oPaymentDetail.SVCFeeForServiceFee = Helper.CalculateSVCFee(oPaymentDetail.ServiceFee, _svcFee, _svcFeeType, _dTaxIncludeInSVCFee);
                    oPaymentDetail.SVCFeeForPartyServiceFeeAmount = Helper.CalculateSVCFee(oPaymentDetail.PartyServiceFeeAmount, _svcFee, _svcFeeType, _dTaxIncludeInSVCFee);
                    oPaymentDetail.SVCFeeForAreaFeeAmount = Helper.CalculateSVCFee(oPaymentDetail.AreaFeeAmount, _svcFee, _svcFeeType, _dTaxIncludeInSVCFee);
                    oPaymentDetail.SVCFeeForAutoGratuityAmount = Helper.CalculateSVCFee(oPaymentDetail.AutoGratuityAmount, _svcFee, _svcFeeType, _dTaxIncludeInSVCFee);
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
                        tempMerchantFeeForServiceCharge = Helper.CalculateMerchantFee((oPaymentDetail.PaidServiceFee - taxsc) + oPaymentDetail.SVCFeeForServiceCharge, _merchantFee);

                        oPaymentDetail.MerchantFeeForServiceFee = Helper.CalculateMerchantFee((oPaymentDetail.ServiceFee - oPaymentDetail.ServiceFeeTaxAmount) + oPaymentDetail.SVCFeeForServiceFee, _merchantFee);
                        oPaymentDetail.MerchantFeeForAreaFeeAmount = Helper.CalculateMerchantFee((oPaymentDetail.AreaFeeAmount - oPaymentDetail.AreaFeeTaxAmount) + oPaymentDetail.SVCFeeForAreaFeeAmount, _merchantFee);
                        oPaymentDetail.MerchantFeeForPartyServiceFeeAmount = Helper.CalculateMerchantFee((oPaymentDetail.PartyServiceFeeAmount - oPaymentDetail.PartyServiceFeeTaxAmount) + oPaymentDetail.SVCFeeForPartyServiceFeeAmount, _merchantFee);
                        oPaymentDetail.MerchantFeeForAutoGratuityAmount = Helper.CalculateMerchantFee((oPaymentDetail.AutoGratuityAmount - oPaymentDetail.AutoGratuityTaxAmount) + oPaymentDetail.SVCFeeForAutoGratuityAmount, _merchantFee);
                    }
                    else
                    {
                        tempMerchantFeeForServiceCharge = Helper.CalculateMerchantFee(oPaymentDetail.PaidServiceFee + oPaymentDetail.SVCFeeForServiceCharge, _merchantFee);

                        oPaymentDetail.MerchantFeeForServiceFee = Helper.CalculateMerchantFee(oPaymentDetail.ServiceFee + oPaymentDetail.SVCFeeForServiceFee, _merchantFee);
                        oPaymentDetail.MerchantFeeForAreaFeeAmount = Helper.CalculateMerchantFee(oPaymentDetail.AreaFeeAmount + oPaymentDetail.SVCFeeForAreaFeeAmount, _merchantFee);
                        oPaymentDetail.MerchantFeeForPartyServiceFeeAmount = Helper.CalculateMerchantFee(oPaymentDetail.PartyServiceFeeAmount + oPaymentDetail.SVCFeeForPartyServiceFeeAmount, _merchantFee);
                        oPaymentDetail.MerchantFeeForAutoGratuityAmount = Helper.CalculateMerchantFee(oPaymentDetail.AutoGratuityAmount + oPaymentDetail.SVCFeeForAutoGratuityAmount, _merchantFee);
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
                        tempMerchantFeeForServiceCharge = Helper.CalculateMerchantFee(oPaymentDetail.PaidServiceFee - taxsc, _merchantFee);
                        oPaymentDetail.MerchantFeeForServiceFee = Helper.CalculateMerchantFee(oPaymentDetail.ServiceFee - oPaymentDetail.ServiceFeeTaxAmount, _merchantFee);
                        oPaymentDetail.MerchantFeeForAreaFeeAmount = Helper.CalculateMerchantFee(oPaymentDetail.AreaFeeAmount - oPaymentDetail.AreaFeeTaxAmount, _merchantFee);
                        oPaymentDetail.MerchantFeeForPartyServiceFeeAmount = Helper.CalculateMerchantFee(oPaymentDetail.PartyServiceFeeAmount - oPaymentDetail.PartyServiceFeeTaxAmount, _merchantFee);
                        oPaymentDetail.MerchantFeeForAutoGratuityAmount = Helper.CalculateMerchantFee(oPaymentDetail.AutoGratuityAmount - oPaymentDetail.AutoGratuityTaxAmount, _merchantFee);
                    }
                    else
                    {
                        tempMerchantFeeForServiceCharge = Helper.CalculateMerchantFee(oPaymentDetail.PaidServiceFee, _merchantFee);
                        oPaymentDetail.MerchantFeeForServiceFee = Helper.CalculateMerchantFee(oPaymentDetail.ServiceFee, _merchantFee);
                        oPaymentDetail.MerchantFeeForAreaFeeAmount = Helper.CalculateMerchantFee(oPaymentDetail.AreaFeeAmount, _merchantFee);
                        oPaymentDetail.MerchantFeeForPartyServiceFeeAmount = Helper.CalculateMerchantFee(oPaymentDetail.PartyServiceFeeAmount, _merchantFee);
                        oPaymentDetail.MerchantFeeForAutoGratuityAmount = Helper.CalculateMerchantFee(oPaymentDetail.AutoGratuityAmount, _merchantFee);
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

        private async Task RunQueryUpdateAsync(SqlConnection conn, string sql, object parameters = null)
        {
            try
            {
                BLogicLogger.Error($"Run Script for {conn.Database} : {sql}");
                var result = await conn.ExecuteAsync(sql, parameters, null, 180, System.Data.CommandType.Text);

                if (result < 0)
                {
                    ErrorMessage = $"Something went wrong while run script update.{Environment.NewLine}Please try contact TruongDS.";
                    BLogicLogger.Exception("RunQueryUpdateAsync in ProcessForm", null, new System.Collections.Generic.List<object>() { sql, parameters });
                }
            }
            catch (Exception ex)
            {
                BLogicLogger.Exception("RunQueryUpdateAsync in ProcessForm", ex, new List<object>() { sql, parameters });
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
                    //    sql = $"update SaleReceipts set " +
                    //        $"AreaFeeAmount = '{new_ServiceChargeAmount}', " +
                    //        $"TotalAmount = '{new_TotalAmount_In_SaleReceipts}', " +
                    //        $"PaidAmount = '{new_TotalAmount_In_SaleReceipts}', " +
                    //        $"SalesOrderFee = '{new_SalesOrderFee_In_SaleReceipts}', " +
                    //        $"TaxAmount = '{new_TaxAmount_In_SaleReceipts}' " +
                    //        $"where SaleReceiptID = '{saleReceiptEntity.SaleReceiptID}';";
                    //    await RunQueryUpdateAsync(conn, sql);

                    //    sql = $"update SaleReceiptPaymentDetails set " +
                    //        $"AmountTendered = '{new_AmountTendered_In_SaleReceiptPaymentDetails}', " +
                    //        $"PaidServiceFee = '{new_PaidServiceFee_InSaleReceiptPaymentDetails}', " +
                    //        $"AreaFeeAmount = '{new_ServiceChargeAmount_InSaleReceiptPaymentDetails}' " +
                    //        $"where SaleReceiptPaymentDetailID = '{saleReceiptPaymentDetailEntity.SaleReceiptPaymentDetailID}';";
                    //    await RunQueryUpdateAsync(conn, sql);

                    //    sql = $"update SaleReceipts set AreaFeeTaxAmount = '{new_ServiceChargeTaxAmount}', ServiceChargeTaxAmount = '{new_ServiceChargeTaxAmount_In_SaleReceipts}' where SaleReceiptID = '{saleReceiptEntity.SaleReceiptID}';";
                    //    await RunQueryUpdateAsync(conn, sql);

                    //    sql = $"update SaleReceiptPaymentDetails set AreaFeeTaxAmount = '{new_ServiceChargeTaxAmount_InSaleReceiptPaymentDetails}' where SaleReceiptPaymentDetailID = '{saleReceiptPaymentDetailEntity.SaleReceiptPaymentDetailID}';";
                    //    await RunQueryUpdateAsync(conn, sql);

                    //    if (saleReceiptEntity.SVCFeeTaxPaidAmount != 0)
                    //    {
                    //        sql = $"update SaleReceiptPaymentDetails set TaxAmountByPaymentModeTaxCustomFee = '{new_TaxAmount_In_SaleReceipts}' where SaleReceiptPaymentDetailID = '{saleReceiptPaymentDetailEntity.SaleReceiptPaymentDetailID}';";
                    //        await RunQueryUpdateAsync(conn, sql);
                    //    }

                    //    break;
                    //case ServiceChargeType.AutoServiceFee:
                    //    sql = $"update SaleReceipts set " +
                    //        $"AutoGratuityAmount = '{new_ServiceChargeAmount}', " +
                    //        $"TotalAmount = '{new_TotalAmount_In_SaleReceipts}', " +
                    //        $"PaidAmount = '{new_TotalAmount_In_SaleReceipts}', " +
                    //        $"SalesOrderFee = '{new_SalesOrderFee_In_SaleReceipts}', " +
                    //        $"TaxAmount = '{new_TaxAmount_In_SaleReceipts}' " +
                    //        $"where SaleReceiptID = '{saleReceiptEntity.SaleReceiptID}';";
                    //    await RunQueryUpdateAsync(conn, sql);

                    //    sql = $"update SaleReceiptPaymentDetails set " +
                    //        $"AmountTendered = '{new_AmountTendered_In_SaleReceiptPaymentDetails}', " +
                    //        $"PaidServiceFee = '{new_PaidServiceFee_InSaleReceiptPaymentDetails}', " +
                    //        $"AutoGratuityAmount = '{new_ServiceChargeAmount_InSaleReceiptPaymentDetails}' " +
                    //        $"where SaleReceiptPaymentDetailID = '{saleReceiptPaymentDetailEntity.SaleReceiptPaymentDetailID}';";
                    //    await RunQueryUpdateAsync(conn, sql);

                    //    sql = $"update SaleReceipts set AutoGratuityTaxAmount = '{new_ServiceChargeTaxAmount}', ServiceChargeTaxAmount = '{new_ServiceChargeTaxAmount_In_SaleReceipts}' where SaleReceiptID = '{saleReceiptEntity.SaleReceiptID}';";
                    //    await RunQueryUpdateAsync(conn, sql);

                    //    sql = $"update SaleReceiptPaymentDetails set AutoGratuityTaxAmount = '{new_ServiceChargeTaxAmount_InSaleReceiptPaymentDetails}' where SaleReceiptPaymentDetailID = '{saleReceiptPaymentDetailEntity.SaleReceiptPaymentDetailID}';";
                    //    await RunQueryUpdateAsync(conn, sql);

                    //    if (saleReceiptEntity.SVCFeeTaxPaidAmount != 0)
                    //    {
                    //        sql = $"update SaleReceiptPaymentDetails set TaxAmountByPaymentModeTaxCustomFee = '{new_TaxAmount_In_SaleReceipts}' where SaleReceiptPaymentDetailID = '{saleReceiptPaymentDetailEntity.SaleReceiptPaymentDetailID}';";
                    //        await RunQueryUpdateAsync(conn, sql);
                    //    }

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

                    //sql = $"update SaleReceipts set " +
                    //    $"ServiceFee = '{new_ServiceChargeAmount}', " +
                    //    $"TotalAmount = '{new_TotalAmount_In_SaleReceipts}', " +
                    //    $"PaidAmount = '{new_TotalAmount_In_SaleReceipts}', " +
                    //    $"SalesOrderFee = '{new_SalesOrderFee_In_SaleReceipts}', " +
                    //    $"TaxAmount = '{new_TaxAmount_In_SaleReceipts}' " +
                    //    $"where SaleReceiptID = '{saleReceiptEntity.SaleReceiptID}';";
                    //await RunQueryUpdateAsync(conn, sql);

                    //sql = $"update SaleReceiptPaymentDetails set " +
                    //    $"AmountTendered = '{new_AmountTendered_In_SaleReceiptPaymentDetails}', " +
                    //    $"PaidServiceFee = '{new_PaidServiceFee_InSaleReceiptPaymentDetails}', " +
                    //    $"ServiceFee = '{new_ServiceChargeAmount_InSaleReceiptPaymentDetails}' " +
                    //    $"where SaleReceiptPaymentDetailID = '{saleReceiptPaymentDetailEntity.SaleReceiptPaymentDetailID}';";
                    //await RunQueryUpdateAsync(conn, sql);

                    //sql = $"update SaleReceipts set ServiceFeeTaxAmount = '{new_ServiceChargeTaxAmount}', ServiceChargeTaxAmount = '{new_ServiceChargeTaxAmount_In_SaleReceipts}' where SaleReceiptID = '{saleReceiptEntity.SaleReceiptID}';";
                    //await RunQueryUpdateAsync(conn, sql);

                    //sql = $"update SaleReceiptPaymentDetails set ServiceFeeTaxAmount = '{new_ServiceChargeTaxAmount_InSaleReceiptPaymentDetails}' where SaleReceiptPaymentDetailID = '{saleReceiptPaymentDetailEntity.SaleReceiptPaymentDetailID}';";
                    //await RunQueryUpdateAsync(conn, sql);

                    //if (saleReceiptEntity.SVCFeeTaxPaidAmount != 0)
                    //{
                    //    sql = $"update SaleReceiptPaymentDetails set TaxAmountByPaymentModeTaxCustomFee = '{new_TaxAmount_In_SaleReceipts}' where SaleReceiptPaymentDetailID = '{saleReceiptPaymentDetailEntity.SaleReceiptPaymentDetailID}';";
                    //    await RunQueryUpdateAsync(conn, sql);
                    //}

                    break;

                case ServiceChargeType.SVCFee:

                    //sql = $"update SaleReceipts set " +
                    //    $"SVCFeeAmount = '{requestModel.serviceChargesValues.SVCFeePaidAmount + requestModel.serviceChargesValues.SVCFeeTaxPaidAmount}', " +
                    //    $"SVCFeePaidAmount = '{requestModel.serviceChargesValues.SVCFeePaidAmount + requestModel.serviceChargesValues.SVCFeeTaxPaidAmount}' " +
                    //    $"where SaleReceiptID = '{saleReceiptEntity.SaleReceiptID}';";
                    //await RunQueryUpdateAsync(conn, sql);

                    //sql = $"update SaleReceipts set " +
                    //    $"SVCFeeTaxAmount = '{requestModel.serviceChargesValues.SVCFeeTaxPaidAmount}', " +
                    //    $"SVCFeeTaxPaidAmount = '{requestModel.serviceChargesValues.SVCFeeTaxPaidAmount}' " +
                    //    $"where SaleReceiptID = '{saleReceiptEntity.SaleReceiptID}';";
                    //await RunQueryUpdateAsync(conn, sql);

                    //sql = $"update SaleReceiptPaymentDetails set " +
                    //    $"SVCFeeAmount = '{requestModel.serviceChargesValues.SVCFeePaidAmount + requestModel.serviceChargesValues.SVCFeeTaxPaidAmount}' " +
                    //    $"where SaleReceiptPaymentDetailID = '{saleReceiptPaymentDetailEntity.SaleReceiptPaymentDetailID}';";
                    //await RunQueryUpdateAsync(conn, sql);

                    //sql = $"update SaleReceiptPaymentDetails set " +
                    //    $"SVCFeeTaxAmount = '{requestModel.serviceChargesValues.SVCFeeTaxPaidAmount}' " +
                    //    $"where SaleReceiptPaymentDetailID = '{saleReceiptPaymentDetailEntity.SaleReceiptPaymentDetailID}';";
                    //await RunQueryUpdateAsync(conn, sql);

                    break;

                case ServiceChargeType.CashDiscount:

                    //sql = $"update SaleReceipts set " +
                    //    $"CashDiscountAmount = '{requestModel.serviceChargesValues.CashDiscountPaidAmount + requestModel.serviceChargesValues.CashDiscountTaxPaidAmount}', " +
                    //    $"CashDiscountPaidAmount = '{requestModel.serviceChargesValues.CashDiscountPaidAmount + requestModel.serviceChargesValues.CashDiscountTaxPaidAmount}' " +
                    //    $"where SaleReceiptID = '{saleReceiptEntity.SaleReceiptID}';";
                    //await RunQueryUpdateAsync(conn, sql);

                    //sql = $"update SaleReceipts set " +
                    //    $"CashDiscountTaxAmount = '{requestModel.serviceChargesValues.CashDiscountTaxPaidAmount}', " +
                    //    $"CashDiscountTaxPaidAmount = '{requestModel.serviceChargesValues.CashDiscountTaxPaidAmount}' " +
                    //    $"where SaleReceiptID = '{saleReceiptEntity.SaleReceiptID}';";
                    //await RunQueryUpdateAsync(conn, sql);

                    //sql = $"update SaleReceiptPaymentDetails set " +
                    //    $"CashDiscountAmount = '{requestModel.serviceChargesValues.CashDiscountPaidAmount + requestModel.serviceChargesValues.CashDiscountTaxPaidAmount}' " +
                    //    $"where SaleReceiptPaymentDetailID = '{saleReceiptPaymentDetailEntity.SaleReceiptPaymentDetailID}';";
                    //await RunQueryUpdateAsync(conn, sql);

                    //sql = $"update SaleReceiptPaymentDetails set " +
                    //    $"CashDiscountTaxAmount = '{requestModel.serviceChargesValues.CashDiscountTaxPaidAmount}' " +
                    //    $"where SaleReceiptPaymentDetailID = '{saleReceiptPaymentDetailEntity.SaleReceiptPaymentDetailID}';";
                    //await RunQueryUpdateAsync(conn, sql);

                    break;
            }
        }

        public async Task ViewShifts()
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

                    sql = $"select a.ShiftID,b.WorkstationName,a.OpeningTime,a.ClosingTime,a.OpeningAmount,a.ClosingAmount from Shift a " +
                        $" left join Workstations b on a.WorkstationID = b.WorkstationID " +
                        $" where a.OpeningAmount >= 0 and (OpeningTime between '{requestModel.StartDate}' and '{requestModel.EndDate}' or a.ClosingTime is null)" +
                        $" and ShiftID not in ('33333333-3333-3333-3333-333333333333','00000000-0000-0000-0000-000000000000') " +
                        $" and a.WorkstationID not in ('33333333-3333-3333-3333-333333333333','00000000-0000-0000-0000-000000000000','44444444-4444-4444-4444-444444444444')";

                    ProcessQueryDatatable(sqlCommand, sql, "Shifts");
                    DataTableReturn = ProcessQueryDatatable(sqlCommand, sql, "Shifts");
                }

                IsSuccess = true;

            }
            catch (Exception ex)
            {
                BLogicLogger.Exception("UpdateServiceChargeBySaleReceiptID in ProcessFormAsync", ex);
            }
        }

        public async Task UpdateShift()
        {
            try
            {

                using (SqlCommand sqlCommand = new SqlCommand())
                using (SqlConnection conn = new SqlConnection(Helper.BLOGICPOS7_Connection))
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();

                    sqlCommand.Connection = conn;
                    sqlCommand.CommandType = CommandType.Text;

                    string sql = $"update Shift set OpeningAmount = '{requestModel.OpeningAmount}', ClosingAmount = '{requestModel.ClosingAmount}' where ShiftID = '{requestModel.ShiftID}'";

                    await RunQueryUpdateAsync(conn, sql);
                }

                IsSuccess = true;

            }
            catch (Exception ex)
            {
                BLogicLogger.Exception("UpdateServiceChargeBySaleReceiptID in ProcessFormAsync", ex);
            }
        }

        private void ReCalculateServiceChargesInSaleReceiptPaymentDetails()
        {
            try
            {
                DataSet dsSaleReceipt = new DataSet();
                DataTable dtSaleReceipts = Helper.ProcessQuery(true, $"select * from SaleReceipts where SaleReceiptID = '{SaleReceiptID}'", "SaleReceipts");
                DataTable dtSaleReceiptDetails = Helper.ProcessQuery(true, $"select * from SaleReceiptDetails where SaleReceiptID = '{SaleReceiptID}'", "SaleReceiptDetails");
                DataTable dtSaleReceiptServiceChargeTaxDetails = Helper.ProcessQuery(true, $"select * from SaleReceiptServiceChargeTaxDetails where SaleReceiptID = '{SaleReceiptID}'", "SaleReceiptDetails");
                DataTable tbPaymentTable = Helper.ProcessQuery(true, $"select * from SaleReceiptPaymentDetails where SaleReceiptID = '{SaleReceiptID}'", "SaleReceiptPaymentDetails");

                dsSaleReceipt.Tables.Add(dtSaleReceipts);
                dsSaleReceipt.Tables.Add(dtSaleReceiptDetails);
                dsSaleReceipt.Tables.Add(dtSaleReceiptServiceChargeTaxDetails);

                var listSaleReceipts = Helper.ProcessQueryEntity<SaleReceiptEntity>(true, $"select * from SaleReceipts where SaleReceiptID = '{SaleReceiptID}'");
                if (listSaleReceipts == null || listSaleReceipts.Count() == 0)
                    return;
                SaleReceiptEntity oReceipt = listSaleReceipts.First();
                oReceipt.SaleReceiptPaymentList = new List<SaleReceiptPaymentDetailEntity>();

                foreach (DataRow dr in tbPaymentTable.Rows)
                {
                    oReceipt.SaleReceiptPaymentList.Add(new SaleReceiptPaymentDetailEntity()
                    {
                        SaleReceiptID = Utility.GetGuidFromRow("SaleReceiptID", dr),
                        SaleReceiptPaymentDetailID = Utility.GetLongFromRow("SaleReceiptPaymentDetailID", dr),
                    });
                }


                SaleReceiptPaymentDetailEntity srpdeGiftCardPayment;

                bool IsPayBySplitEvenly;
                decimal dTotalAmountTender = 0;
                decimal dTotalPaidServiceFee = 0;
                decimal dTotalServiceFee = 0;
                decimal dPartyServiceFeeAmount = 0;
                decimal dAreaFeeAmount = 0;
                decimal dServiceFee = 0;
                decimal dOpenTabAmount = 0;
                decimal dAutoGratuityAmount = 0;

                decimal dServiceFeeTaxAmount = 0;
                decimal dAreaFeeTaxAmount = 0;
                decimal dPartyServiceFeeTaxAmount = 0;
                decimal dAutoGratuityTaxAmount = 0;

                decimal tempPaidAmount = 0;




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

                decimal taxitem = oReceipt.TaxAmount - oReceipt.ServiceChargeTaxAmount;
                dTotalServiceFee = (decimal)oReceipt.SalesOrderFee;

                decimal tempAmount = oReceipt.TotalAmount + taxitem - oReceipt.TaxAmount;
                decimal dTotalAmountExcludeServiceFee = tempAmount - dTotalServiceFee;

                decimal dBaseAmount = 0;
                if (true)//dsSaleReceipt.Tables.Contains("dtIsPayBySplitEvently"))
                {
                    IsPayBySplitEvenly = true;
                    if (IsFinishSale(oReceipt))
                    {
                        tempAmount = oReceipt.TotalAmount + taxitem - oReceipt.TaxAmount;
                        dTotalAmountExcludeServiceFee = tempAmount - dTotalServiceFee;
                    }
                    else
                    {
                        if (true)//dsSaleReceipt.Tables.Contains("SaleReceiptDetails"))
                        {
                            var result = dtSaleReceiptDetails.AsEnumerable().Where(dr => Utility.GetStringFromObject(dr.Field<object>("Status")) != "2");
                            if (result != null)
                                dBaseAmount = result.Sum(dr => Utility.GetDecimalFromObject(dr.Field<object>("ExtendedPrice"))) + result.Sum(dr => Utility.GetDecimalFromObject(dr.Field<object>("TaxAmount")));

                            dTotalAmountExcludeServiceFee = dBaseAmount;
                        }
                        //tempAmount = oReceipt.TotalAmount + dTotalAmountTender;
                        //decimal stempAmountSC = tempAmount < dTotalServiceFee ? 0 : tempAmount - dTotalServiceFee;
                        //dTotalAmountExcludeServiceFee = tempAmount < dTotalServiceFee ? tempAmount : tempAmount - stempAmountSC;
                    }
                }

                bool isPreAuth = false;
                bool IsAutoGratuityAmount = false;
                bool IsPartyServiceFeeAmount = false;
                bool IsAreaFeeAmount = false;
                bool IsServiceFee = false;
                decimal tempTaxServiceCharge = 0;

                decimal dServiceFeeNew = Utility.GetDecimalFromObject(oReceipt.ServiceFee) + oReceipt.ExtraFeeAmount;
                decimal sServiceChargeTaxRate = Utility.GetDecimalFromObject(oReceipt.ServiceChargeTaxRate);

                int sCountRows = 0;
                //decimal m_TotalTaxServiceCharge = dServiceFeeTaxAmount + dAreaFeeTaxAmount + dPartyServiceFeeTaxAmount + dAutoGratuityTaxAmount;

                int _svcFeeType = 0;
                decimal _svcFee = 0;
                decimal _merchantFee = 0;
                decimal _cashDiscount = 0;
                int _dTaxIncludeInSVCFee = 0;
                string _ApplyCustomFeeWhenPurchaseGiftCard = "1";
                string _EnableDualPrice = "0";
                decimal _DualPricePercent = 0;
                string sqlEmployeePaidID = string.Empty;

                string sql = "select * from Configurations " +
                    " where ConfigurationName in ('CreditCardConfigValue.SVCFee', 'CreditCardConfigValue.SVCFeeType', 'CreditCardConfigValue.MerchantFee', 'CreditCardConfigValue.CashDiscount', " +
                    " 'CreditCardConfigValue.TaxIncludeInSVCFee','CreditCardConfigValue.ApplyCustomFeeWhenPurchaseGiftCard','CreditCardConfigValue.DualPricePercent','SystemConfigValue.EnableDualPrice')";
                var listConfig = Helper.ProcessQueryEntity<ConfigurationsEntity>(true, sql);

                foreach (var item in listConfig)
                {
                    ConfigurationsEntity entity = item as ConfigurationsEntity;
                    string _name = (item as ConfigurationsEntity).ConfigurationName;
                    switch (entity.ConfigurationName)
                    {
                        case "CreditCardConfigValue.SVCFee":
                            _svcFee = Utility.GetDecimalFromObject(entity.ConfigurationValue);
                            break;
                        case "CreditCardConfigValue.SVCFeeType":
                            _svcFeeType = Utility.GetIntFromObject(entity.ConfigurationValue);
                            break;
                        case "CreditCardConfigValue.MerchantFee":
                            _merchantFee = Utility.GetDecimalFromObject(entity.ConfigurationValue);
                            break;
                        case "CreditCardConfigValue.CashDiscount":
                            _cashDiscount = Utility.GetDecimalFromObject(entity.ConfigurationValue);
                            break;
                        case "CreditCardConfigValue.TaxIncludeInSVCFee":
                            _dTaxIncludeInSVCFee = Utility.GetIntFromObject(entity.ConfigurationValue);
                            break;
                        case "CreditCardConfigValue.ApplyCustomFeeWhenPurchaseGiftCard":
                            _ApplyCustomFeeWhenPurchaseGiftCard = Utility.GetStringFromObject(entity.ConfigurationValue);
                            break;
                        case "SystemConfigValue.EnableDualPrice":
                            _EnableDualPrice = Utility.GetStringFromObject(entity.ConfigurationValue);
                            break;
                        case "CreditCardConfigValue.DualPricePercent":
                            _DualPricePercent = Utility.GetDecimalFromObject(entity.ConfigurationValue);
                            break;
                    }
                }

                //Issue House Account
                bool bIssueHouseAccount = false;
                DateTime? vLastTransDate = null;
                decimal dTotalBaland = 0;
                string sIssueTenderType = "";
                Guid vID = Guid.NewGuid();

                if (!string.IsNullOrEmpty(oReceipt.OpenTabCardNumber) && oReceipt.SaleTypeID == Utility.HouseAccountSaleType)
                    bIssueHouseAccount = true;

                foreach (DataRow rPaymentRow in tbPaymentTable.Rows)
                {
                    sCountRows++;
                    isPreAuth = false;
                    //Create salePaymentDetail and add to receipt
                    SaleReceiptPaymentDetailEntity oPaymentDetail = new SaleReceiptPaymentDetailEntity(); ;
                    oPaymentDetail.PaidServiceFee = 0;
                    oPaymentDetail.AreaFeeAmount = 0;
                    oPaymentDetail.PartyServiceFeeAmount = 0;
                    oPaymentDetail.ServiceFee = 0;

                    if (tbPaymentTable.Columns.Contains("CreditCardRefNo"))
                    {
                        oPaymentDetail.CreditCardRefNo = rPaymentRow["CreditCardRefNo"]?.ToString();
                    }
                    tempPaidAmount = dTotalAmountTender;
                    dTotalAmountTender += oPaymentDetail.AmountTendered - oPaymentDetail.AmountReturn;

                    //add by tuanph.11.18.2019
                    if (oPaymentDetail.CreditCardTransactionType == "PreAuth")
                    {
                        dOpenTabAmount = oPaymentDetail.AmountTendered - oPaymentDetail.AmountReturn;
                        isPreAuth = true;
                    }

                    decimal dTempAmountTotal = oReceipt.TotalAmount;
                    if (IsPayBySplitEvenly && !IsFinishSale(oReceipt)) // Pay by split evenly
                    {
                        dTempAmountTotal = dBaseAmount + (decimal)oReceipt.SalesOrderFee + oReceipt.ServiceChargeTaxAmount;
                    }

                    if (dTempAmountTotal > dTotalAmountTender)
                        dTempAmountTotal = dTotalAmountTender;

                    if (dTotalAmountExcludeServiceFee <= dTotalAmountTender && !isPreAuth)
                    {
                        decimal dCurrentServiceFee = 0;

                        oPaymentDetail.PaidServiceFee = dTempAmountTotal - dTotalAmountExcludeServiceFee - dTotalPaidServiceFee;// + oReceipt.ServiceChargeTaxAmount;
                        //if (IsFinishSale(oReceipt) && sCountRows == tbPaymentTable.Rows.Count)
                        //    oPaymentDetail.PaidServiceFee = dTempAmountTotal - dTotalAmountExcludeServiceFee - dTotalPaidServiceFee - taxitem;
                        //else
                        //{
                        //    oPaymentDetail.PaidServiceFee = dTempAmountTotal - dTotalAmountExcludeServiceFee - dTotalPaidServiceFee;
                        //    if (oPaymentDetail.PaidServiceFee > oReceipt.SalesOrderFee + oReceipt.ServiceChargeTaxAmount)
                        //        oPaymentDetail.PaidServiceFee = oReceipt.SalesOrderFee + oReceipt.ServiceChargeTaxAmount;
                        //}

                        dTotalPaidServiceFee += oPaymentDetail.PaidServiceFee;

                        //add by tuanph.09.23.2019
                        dCurrentServiceFee = oPaymentDetail.PaidServiceFee;

                        if (oReceipt.PartyServiceFeeAmount > 0 && oReceipt.PartyServiceFeeAmount + oReceipt.PartyServiceFeeTaxAmount > dPartyServiceFeeAmount && dCurrentServiceFee > 0)
                        {
                            decimal tempPartyFeeAmountNeeded = RoundingUtility.Rounding.RoundUp(oReceipt.PartyServiceFeeAmount - dPartyServiceFeeAmount);
                            decimal tempPartyFeeAmount = dCurrentServiceFee > tempPartyFeeAmountNeeded ? tempPartyFeeAmountNeeded : dCurrentServiceFee;

                            oPaymentDetail.PartyServiceFeeAmount = tempPartyFeeAmount;
                            dPartyServiceFeeAmount += tempPartyFeeAmount;
                            dCurrentServiceFee -= tempPartyFeeAmount;

                            if (!IsPartyServiceFeeAmount && dCurrentServiceFee > 0)
                            {
                                tempTaxServiceCharge = dCurrentServiceFee > oReceipt.PartyServiceFeeTaxAmount ? oReceipt.PartyServiceFeeTaxAmount : dCurrentServiceFee;
                                oPaymentDetail.PartyServiceFeeAmount += tempTaxServiceCharge;// oReceipt.PartyServiceFeeTaxAmount;
                                dPartyServiceFeeAmount += tempTaxServiceCharge;
                                dCurrentServiceFee -= tempTaxServiceCharge;

                                if (oPaymentDetail.PartyServiceFeeAmount >= oReceipt.PartyServiceFeeAmount + oReceipt.PartyServiceFeeTaxAmount)
                                    IsPartyServiceFeeAmount = true;
                            }

                            //oPaymentDetail.PartyServiceFeeTaxAmount = CalculateTaxServiceCharge(oPaymentDetail.PartyServiceFeeAmount, ratioSC, oReceipt.PartyServiceFeeTaxAmount);
                            oPaymentDetail.PartyServiceFeeTaxAmount = CalculateTaxServiceCharge(oPaymentDetail.PartyServiceFeeAmount, sServiceChargeTaxRate, oReceipt.PartyServiceFeeTaxAmount, dsSaleReceipt);
                            dPartyServiceFeeTaxAmount += oPaymentDetail.PartyServiceFeeTaxAmount;
                        }

                        if (oReceipt.AreaFeeAmount > 0 && oReceipt.AreaFeeAmount + oReceipt.AreaFeeTaxAmount > dAreaFeeAmount && dCurrentServiceFee > 0)
                        {
                            decimal tempAreaFeeAmountNeeded = RoundingUtility.Rounding.RoundUp(oReceipt.AreaFeeAmount - dAreaFeeAmount);
                            decimal tempAreaFeeAmount = dCurrentServiceFee > tempAreaFeeAmountNeeded ? tempAreaFeeAmountNeeded : dCurrentServiceFee;

                            oPaymentDetail.AreaFeeAmount = tempAreaFeeAmount;
                            dAreaFeeAmount += tempAreaFeeAmount;
                            dCurrentServiceFee -= tempAreaFeeAmount;

                            if (!IsAreaFeeAmount && dCurrentServiceFee > 0)
                            {
                                tempTaxServiceCharge = dCurrentServiceFee > oReceipt.AreaFeeTaxAmount ? oReceipt.AreaFeeTaxAmount : dCurrentServiceFee;
                                oPaymentDetail.AreaFeeAmount += tempTaxServiceCharge;// oReceipt.AreaFeeTaxAmount;
                                dAreaFeeAmount += tempTaxServiceCharge;
                                dCurrentServiceFee -= tempTaxServiceCharge;

                                if (oPaymentDetail.AreaFeeAmount >= oReceipt.AreaFeeAmount + oReceipt.AreaFeeTaxAmount)
                                    IsAreaFeeAmount = true;
                            }

                            //oPaymentDetail.AreaFeeTaxAmount = CalculateTaxServiceCharge(oPaymentDetail.AreaFeeAmount, ratioSC, oReceipt.AreaFeeTaxAmount);
                            oPaymentDetail.AreaFeeTaxAmount = CalculateTaxServiceCharge(oPaymentDetail.AreaFeeAmount, sServiceChargeTaxRate, oReceipt.AreaFeeTaxAmount, dsSaleReceipt);
                            dAreaFeeTaxAmount += oPaymentDetail.AreaFeeTaxAmount;
                        }

                        if (dServiceFeeNew > 0 && dServiceFeeNew + oReceipt.ServiceFeeTaxAmount + oReceipt.ExtraFeeTaxAmount > dServiceFee && dCurrentServiceFee > 0)
                        {
                            decimal tempServiceFeeAmountNeeded = RoundingUtility.Rounding.RoundUp((decimal)dServiceFeeNew - dServiceFee);
                            decimal tempServiceFeeAmount = dCurrentServiceFee > tempServiceFeeAmountNeeded ? tempServiceFeeAmountNeeded : dCurrentServiceFee;

                            oPaymentDetail.ServiceFee = tempServiceFeeAmount;
                            dServiceFee += tempServiceFeeAmount;
                            dCurrentServiceFee -= tempServiceFeeAmount;

                            if (!IsServiceFee && dCurrentServiceFee > 0)
                            {
                                tempTaxServiceCharge = dCurrentServiceFee > oReceipt.ServiceFeeTaxAmount + oReceipt.ExtraFeeTaxAmount ? oReceipt.ServiceFeeTaxAmount + oReceipt.ExtraFeeTaxAmount : dCurrentServiceFee;
                                oPaymentDetail.ServiceFee += tempTaxServiceCharge; // oReceipt.ServiceFeeTaxAmount + oReceipt.ExtraFeeTaxAmount;
                                dServiceFee += tempTaxServiceCharge;
                                dCurrentServiceFee -= tempTaxServiceCharge;

                                if (oPaymentDetail.ServiceFee >= dServiceFeeNew + oReceipt.ServiceFeeTaxAmount + oReceipt.ExtraFeeTaxAmount)
                                    IsServiceFee = true;
                            }

                            //oPaymentDetail.ServiceFeeTaxAmount = CalculateTaxServiceCharge(oPaymentDetail.ServiceFee, ratioSC, oReceipt.ServiceFeeTaxAmount + oReceipt.ExtraFeeTaxAmount);
                            oPaymentDetail.ServiceFeeTaxAmount = CalculateTaxServiceCharge(oPaymentDetail.ServiceFee, sServiceChargeTaxRate, oReceipt.ServiceFeeTaxAmount + oReceipt.ExtraFeeTaxAmount, dsSaleReceipt);
                            dServiceFeeTaxAmount += oPaymentDetail.ServiceFeeTaxAmount;
                        }

                        if (oReceipt.AutoGratuityAmount > 0 && oReceipt.AutoGratuityAmount + oReceipt.AutoGratuityTaxAmount > dAutoGratuityAmount && dCurrentServiceFee > 0)
                        {
                            decimal tempAutoGratuityNeeded = RoundingUtility.Rounding.RoundUp(oReceipt.AutoGratuityAmount - dAutoGratuityAmount);
                            decimal tempAutoGratuityAmount = dCurrentServiceFee > tempAutoGratuityNeeded ? tempAutoGratuityNeeded : dCurrentServiceFee;

                            oPaymentDetail.AutoGratuityAmount = tempAutoGratuityAmount;

                            dAutoGratuityAmount += tempAutoGratuityAmount;
                            dCurrentServiceFee -= tempAutoGratuityAmount;

                            if (!IsAutoGratuityAmount && dCurrentServiceFee > 0)
                            {
                                tempTaxServiceCharge = dCurrentServiceFee > oReceipt.AutoGratuityTaxAmount ? oReceipt.AutoGratuityTaxAmount : dCurrentServiceFee;
                                oPaymentDetail.AutoGratuityAmount += tempTaxServiceCharge;// oReceipt.AutoGratuityTaxAmount;
                                dAutoGratuityAmount += tempTaxServiceCharge;
                                dCurrentServiceFee -= tempTaxServiceCharge;

                                if (oPaymentDetail.AutoGratuityAmount >= oReceipt.AutoGratuityAmount + oReceipt.AutoGratuityTaxAmount)
                                    IsAutoGratuityAmount = true;
                            }

                            //oPaymentDetail.AutoGratuityTaxAmount = CalculateTaxServiceCharge(oPaymentDetail.AutoGratuityAmount, ratioSC, oReceipt.AutoGratuityTaxAmount);
                            oPaymentDetail.AutoGratuityTaxAmount = CalculateTaxServiceCharge(oPaymentDetail.AutoGratuityAmount, sServiceChargeTaxRate, oReceipt.AutoGratuityTaxAmount, dsSaleReceipt);
                            dAutoGratuityTaxAmount += oPaymentDetail.AutoGratuityTaxAmount;
                        }

                        #region
                        //if (oPaymentDetail.PaidServiceFee == oPaymentDetail.AmountTendered - oPaymentDetail.AmountReturn) // Compare
                        //{
                        //    decimal tempVal = oPaymentDetail.TaxAmountByPaymentModeTaxCustomFee - (oPaymentDetail.AutoGratuityTaxAmount + oPaymentDetail.AreaFeeTaxAmount + oPaymentDetail.PartyServiceFeeTaxAmount + oPaymentDetail.ServiceFeeTaxAmount);
                        //    if (tempVal != 0)
                        //    {
                        //        if (oPaymentDetail.ServiceFeeTaxAmount != 0 && tempVal != 0)
                        //        {
                        //            oPaymentDetail.ServiceFeeTaxAmount += tempVal;
                        //            tempVal = 0;
                        //        }
                        //        if (oPaymentDetail.PartyServiceFeeTaxAmount != 0 && tempVal != 0)
                        //        {
                        //            oPaymentDetail.PartyServiceFeeTaxAmount += tempVal;
                        //            tempVal = 0;
                        //        }
                        //        if (oPaymentDetail.AreaFeeTaxAmount != 0 && tempVal != 0)
                        //        {
                        //            oPaymentDetail.AreaFeeTaxAmount += tempVal;
                        //            tempVal = 0;
                        //        }
                        //        if (oPaymentDetail.AutoGratuityTaxAmount != 0 && tempVal != 0)
                        //        {
                        //            oPaymentDetail.ServiceFeeTaxAmount += tempVal;
                        //            tempVal = 0;
                        //        }
                        //    }
                        //}

                        //==========================================================================

                        #region Tax Service Charge

                        //if (oReceipt.AutoGratuityAmount > 0 && oReceipt.AutoGratuityAmount + oReceipt.AutoGratuityTaxAmount > dAutoGratuityAmount && dCurrentServiceFee > 0)
                        //{
                        //    if (!IsAutoGratuityAmount && dCurrentServiceFee > 0)
                        //    {
                        //        tempTaxServiceCharge = dCurrentServiceFee > oReceipt.AutoGratuityTaxAmount ? oReceipt.AutoGratuityTaxAmount : dCurrentServiceFee;
                        //        oPaymentDetail.AutoGratuityAmount += tempTaxServiceCharge;
                        //        dAutoGratuityAmount += tempTaxServiceCharge;
                        //        dCurrentServiceFee -= tempTaxServiceCharge;

                        //        if (oPaymentDetail.AutoGratuityAmount >= oReceipt.AutoGratuityAmount + oReceipt.AutoGratuityTaxAmount)
                        //            IsAutoGratuityAmount = true;

                        //        oPaymentDetail.AutoGratuityTaxAmount = tempTaxServiceCharge;
                        //        dAutoGratuityTaxAmount += oPaymentDetail.AutoGratuityTaxAmount;
                        //    }
                        //}

                        //if (oReceipt.AreaFeeAmount > 0 && oReceipt.AreaFeeAmount + oReceipt.AreaFeeTaxAmount > dAreaFeeAmount && dCurrentServiceFee > 0)
                        //{
                        //    if (!IsAreaFeeAmount && dCurrentServiceFee > 0)
                        //    {
                        //        tempTaxServiceCharge = dCurrentServiceFee > oReceipt.AreaFeeTaxAmount ? oReceipt.AreaFeeTaxAmount : dCurrentServiceFee;
                        //        oPaymentDetail.AreaFeeAmount += tempTaxServiceCharge;
                        //        dAreaFeeAmount += tempTaxServiceCharge;
                        //        dCurrentServiceFee -= tempTaxServiceCharge;

                        //        if (oPaymentDetail.AreaFeeAmount >= oReceipt.AreaFeeAmount + oReceipt.AreaFeeTaxAmount)
                        //            IsAreaFeeAmount = true;

                        //        oPaymentDetail.AreaFeeTaxAmount = tempTaxServiceCharge;
                        //        dAreaFeeTaxAmount += oPaymentDetail.AreaFeeTaxAmount;
                        //    }
                        //}

                        //if (oReceipt.PartyServiceFeeAmount > 0 && oReceipt.PartyServiceFeeAmount + oReceipt.PartyServiceFeeTaxAmount > dPartyServiceFeeAmount && dCurrentServiceFee > 0)
                        //{
                        //    if (!IsPartyServiceFeeAmount && dCurrentServiceFee > 0)
                        //    {
                        //        tempTaxServiceCharge = dCurrentServiceFee > oReceipt.PartyServiceFeeTaxAmount ? oReceipt.PartyServiceFeeTaxAmount : dCurrentServiceFee;
                        //        oPaymentDetail.PartyServiceFeeAmount += tempTaxServiceCharge;
                        //        dPartyServiceFeeAmount += tempTaxServiceCharge;
                        //        dCurrentServiceFee -= tempTaxServiceCharge;

                        //        if (oPaymentDetail.PartyServiceFeeAmount >= oReceipt.PartyServiceFeeAmount + oReceipt.PartyServiceFeeTaxAmount)
                        //            IsPartyServiceFeeAmount = true;

                        //        oPaymentDetail.PartyServiceFeeTaxAmount = tempTaxServiceCharge;
                        //        dPartyServiceFeeTaxAmount += oPaymentDetail.PartyServiceFeeTaxAmount;
                        //    }
                        //}

                        //if (dServiceFeeNew > 0 && dServiceFeeNew + oReceipt.ServiceFeeTaxAmount + oReceipt.ExtraFeeTaxAmount > dServiceFee && dCurrentServiceFee > 0)
                        //{
                        //    if (!IsServiceFee && dCurrentServiceFee > 0)
                        //    {
                        //        tempTaxServiceCharge = dCurrentServiceFee > oReceipt.ServiceFeeTaxAmount + oReceipt.ExtraFeeTaxAmount ? oReceipt.ServiceFeeTaxAmount + oReceipt.ExtraFeeTaxAmount : dCurrentServiceFee;
                        //        oPaymentDetail.ServiceFee += tempTaxServiceCharge;
                        //        dServiceFee += tempTaxServiceCharge;
                        //        dCurrentServiceFee -= tempTaxServiceCharge;

                        //        if (oPaymentDetail.ServiceFee >= dServiceFeeNew + oReceipt.ServiceFeeTaxAmount + oReceipt.ExtraFeeTaxAmount)
                        //            IsServiceFee = true;

                        //        oPaymentDetail.ServiceFeeTaxAmount = tempTaxServiceCharge;
                        //        dServiceFeeTaxAmount += oPaymentDetail.ServiceFeeTaxAmount;
                        //    }
                        //}

                        #endregion

                        #endregion
                    }

                    if (oPaymentDetail.CreatedDate == null || oPaymentDetail.CreatedDate == DateTime.MinValue)
                        oPaymentDetail.CreatedDate = DateTime.Now;

                    //Trung Add
                    vLastTransDate = oPaymentDetail.CreatedDate;

                    oReceipt.SaleReceiptPaymentList.Add(oPaymentDetail);

                    if (IsFinishSale(oReceipt) && sCountRows == tbPaymentTable.Rows.Count) // Finish sale
                    {
                        UpdateTaxServiceChargeinPayment(dAutoGratuityTaxAmount, dAreaFeeTaxAmount, dPartyServiceFeeTaxAmount, dServiceFeeTaxAmount, oReceipt);
                    }

                    //get giftCardPayment 
                    //if (oPaymentDetail.PaymentMethod.PaymentMethodid == (int)PaymentTypes.GIFTCARD
                    //    && rPaymentRow["GiftCardCode"] != null && rPaymentRow["GiftCardCode"].ToString() != "")
                    if (oPaymentDetail.PaymentMethodID == (int)PaymentTypes.GIFTCARD)
                    {
                        oPaymentDetail.GiftCardCode = Utility.GetStringFromRow("GiftCardCode", rPaymentRow);
                        srpdeGiftCardPayment = oPaymentDetail;
                    }
                    if (oReceipt.TransactionStatusID == TransactionStatus.RETURN)
                        UpdateValuePaymentDetailsForRETURNSaleReceipt(oPaymentDetail);
                    //dTotalAmountTender += oPaymentDetail.AmountTendered;

                    if (dTotalAmountTender >= oReceipt.TotalAmount)
                        oPaymentDetail.PaidPayout = GetPaidPayout(dsSaleReceipt);

                    int cashAdjust = 1;
                    if (_EnableDualPrice == "1" && _DualPricePercent > 0)
                    {
                        sql = $"select IsDualPrice from PaymentMethods where PaymentMethodID = '{oPaymentDetail.PaymentMethodID}'";
                        var tempResult = Helper.CreateQuerySelectUnique(true, sql);
                        cashAdjust = Utility.GetBoolFromObject(tempResult) ? 2 : 1;
                    }
                    else
                    {
                        sql = $"select CashAdjust from PaymentMethods where PaymentMethodID = '{oPaymentDetail.PaymentMethodID}'";
                        var tempResult = Helper.CreateQuerySelectUnique(true, sql);
                        cashAdjust = Utility.GetIntFromObject(tempResult);
                    }

                    #region Commnet

                    //if (oPaymentDetail.PaymentMethod.PaymentMethodid == 2 || cashAdjust == 2 || (_svcFee > 0 && _cashDiscount == 0 && oPaymentDetail.PaymentMethod.PaymentMethodid != -1 && oPaymentDetail.PaymentMethod.PaymentMethodid != -2 && oPaymentDetail.PaymentMethod.PaymentMethodid != -3))
                    //{
                    //    if (oPaymentDetail.PaidServiceFee == 0 || oPaymentDetail.SVCFeeAmount == 0 || (oPaymentDetail.PaymentMethod.PaymentMethodid == 3 && _ApplyCustomFeeWhenPurchaseGiftCard != "1"))
                    //    {
                    //        oPaymentDetail.SVCFeeForServiceCharge = oPaymentDetail.SVCFeeForServiceFee = oPaymentDetail.SVCFeeForPartyServiceFeeAmount = oPaymentDetail.SVCFeeForAreaFeeAmount = oPaymentDetail.SVCFeeForAutoGratuityAmount = 0;
                    //    }
                    //    else if (isApplyTaxCustomFee) // Apply tax custom fee
                    //    {
                    //        decimal taxsc = oPaymentDetail.AutoGratuityTaxAmount + oPaymentDetail.AreaFeeTaxAmount + oPaymentDetail.PartyServiceFeeTaxAmount + oPaymentDetail.ServiceFeeTaxAmount;
                    //        oPaymentDetail.SVCFeeForAutoGratuityAmount = CalculateSVCFee(oPaymentDetail.AutoGratuityAmount - oPaymentDetail.AutoGratuityTaxAmount, _svcFee, _svcFeeType, _dTaxIncludeInSVCFee, isApplyTaxCustomFee, oReceipt.SVCFeeTaxRate, oPaymentDetail.SVCFeeTaxAmount);
                    //        oPaymentDetail.SVCFeeForAreaFeeAmount = CalculateSVCFee(oPaymentDetail.AreaFeeAmount - oPaymentDetail.AreaFeeTaxAmount, _svcFee, _svcFeeType, _dTaxIncludeInSVCFee, isApplyTaxCustomFee, oReceipt.SVCFeeTaxRate, oPaymentDetail.SVCFeeTaxAmount);
                    //        oPaymentDetail.SVCFeeForPartyServiceFeeAmount = CalculateSVCFee(oPaymentDetail.PartyServiceFeeAmount - oPaymentDetail.PartyServiceFeeTaxAmount, _svcFee, _svcFeeType, _dTaxIncludeInSVCFee, isApplyTaxCustomFee, oReceipt.SVCFeeTaxRate, oPaymentDetail.SVCFeeTaxAmount);
                    //        oPaymentDetail.SVCFeeForServiceFee = CalculateSVCFee(oPaymentDetail.ServiceFee - oPaymentDetail.ServiceFeeTaxAmount, _svcFee, _svcFeeType, _dTaxIncludeInSVCFee, isApplyTaxCustomFee, oReceipt.SVCFeeTaxRate, oPaymentDetail.SVCFeeTaxAmount);
                    //        oPaymentDetail.SVCFeeForServiceCharge = CalculateSVCFee(oPaymentDetail.PaidServiceFee - taxsc, _svcFee, _svcFeeType, _dTaxIncludeInSVCFee, isApplyTaxCustomFee, oReceipt.SVCFeeTaxRate, oPaymentDetail.SVCFeeTaxAmount);
                    //    }
                    //    else
                    //    {
                    //        oPaymentDetail.SVCFeeForServiceCharge = CalculateSVCFee(oPaymentDetail.PaidServiceFee, _svcFee, _svcFeeType, _dTaxIncludeInSVCFee);
                    //        oPaymentDetail.SVCFeeForServiceFee = CalculateSVCFee(oPaymentDetail.ServiceFee, _svcFee, _svcFeeType, _dTaxIncludeInSVCFee);
                    //        oPaymentDetail.SVCFeeForPartyServiceFeeAmount = CalculateSVCFee(oPaymentDetail.PartyServiceFeeAmount, _svcFee, _svcFeeType, _dTaxIncludeInSVCFee);
                    //        oPaymentDetail.SVCFeeForAreaFeeAmount = CalculateSVCFee(oPaymentDetail.AreaFeeAmount, _svcFee, _svcFeeType, _dTaxIncludeInSVCFee);
                    //        oPaymentDetail.SVCFeeForAutoGratuityAmount = CalculateSVCFee(oPaymentDetail.AutoGratuityAmount, _svcFee, _svcFeeType, _dTaxIncludeInSVCFee);
                    //    }

                    //    #region Compare Custom Fee For Service Charge
                    //    decimal tempTotalSVCFee = oPaymentDetail.SVCFeeForServiceFee + oPaymentDetail.SVCFeeForPartyServiceFeeAmount +
                    //        oPaymentDetail.SVCFeeForAreaFeeAmount + oPaymentDetail.SVCFeeForAutoGratuityAmount;

                    //    if (oPaymentDetail.SVCFeeForServiceCharge != tempTotalSVCFee)
                    //    {
                    //        decimal tempVal = tempTotalSVCFee - oPaymentDetail.SVCFeeForServiceCharge;

                    //        if (oPaymentDetail.SVCFeeForPartyServiceFeeAmount > 0 && tempVal != 0)
                    //        {
                    //            oPaymentDetail.SVCFeeForPartyServiceFeeAmount -= tempVal;
                    //            tempVal = 0;
                    //        }
                    //        else if (oPaymentDetail.SVCFeeForAreaFeeAmount > 0 && tempVal != 0)
                    //        {
                    //            oPaymentDetail.SVCFeeForAreaFeeAmount -= tempVal;
                    //            tempVal = 0;
                    //        }
                    //        else if (oPaymentDetail.SVCFeeForServiceFee > 0 && tempVal != 0)
                    //        {
                    //            oPaymentDetail.SVCFeeForServiceFee -= tempVal;
                    //            tempVal = 0;
                    //        }
                    //        else if (oPaymentDetail.SVCFeeForAutoGratuityAmount > 0 && tempVal != 0)
                    //        {
                    //            oPaymentDetail.SVCFeeForAutoGratuityAmount -= tempVal;
                    //            tempVal = 0;
                    //        }
                    //    }

                    //    #endregion

                    //    if (oPaymentDetail.PaymentMethod.PaymentMethodid == 1 || (cashAdjust != 2 && _svcFee > 0 && _cashDiscount == 0)) // Cash
                    //    {
                    //        oPaymentDetail.MerchantFeeForServiceFee = oPaymentDetail.MerchantFeeForAreaFeeAmount = oPaymentDetail.MerchantFeeForPartyServiceFeeAmount = oPaymentDetail.MerchantFeeForAutoGratuityAmount = 0;
                    //    }
                    //    else
                    //    {
                    //        decimal tempMerchantFeeForServiceCharge = 0;
                    //        if (isApplyTaxCustomFee) // Apply tax custom fee
                    //        {
                    //            decimal taxsc = oPaymentDetail.AutoGratuityTaxAmount + oPaymentDetail.AreaFeeTaxAmount + oPaymentDetail.PartyServiceFeeTaxAmount + oPaymentDetail.ServiceFeeTaxAmount;
                    //            tempMerchantFeeForServiceCharge = CalculateMerchantFee(oPaymentDetail.PaidServiceFee - taxsc, _merchantFee);

                    //            oPaymentDetail.MerchantFeeForServiceFee = CalculateMerchantFee((oPaymentDetail.ServiceFee - oPaymentDetail.ServiceFeeTaxAmount) + oPaymentDetail.SVCFeeForServiceFee, _merchantFee);
                    //            oPaymentDetail.MerchantFeeForAreaFeeAmount = CalculateMerchantFee((oPaymentDetail.AreaFeeAmount - oPaymentDetail.AreaFeeTaxAmount) + oPaymentDetail.SVCFeeForAreaFeeAmount, _merchantFee);
                    //            oPaymentDetail.MerchantFeeForPartyServiceFeeAmount = CalculateMerchantFee((oPaymentDetail.PartyServiceFeeAmount - oPaymentDetail.PartyServiceFeeTaxAmount) + oPaymentDetail.SVCFeeForPartyServiceFeeAmount, _merchantFee);
                    //            oPaymentDetail.MerchantFeeForAutoGratuityAmount = CalculateMerchantFee((oPaymentDetail.AutoGratuityAmount - oPaymentDetail.AutoGratuityTaxAmount) + oPaymentDetail.SVCFeeForAutoGratuityAmount, _merchantFee);
                    //        }
                    //        else
                    //        {
                    //            tempMerchantFeeForServiceCharge = CalculateMerchantFee(oPaymentDetail.PaidServiceFee, _merchantFee);

                    //            oPaymentDetail.MerchantFeeForServiceFee = CalculateMerchantFee(oPaymentDetail.ServiceFee + oPaymentDetail.SVCFeeForServiceFee, _merchantFee);
                    //            oPaymentDetail.MerchantFeeForAreaFeeAmount = CalculateMerchantFee(oPaymentDetail.AreaFeeAmount + oPaymentDetail.SVCFeeForAreaFeeAmount, _merchantFee);
                    //            oPaymentDetail.MerchantFeeForPartyServiceFeeAmount = CalculateMerchantFee(oPaymentDetail.PartyServiceFeeAmount + oPaymentDetail.SVCFeeForPartyServiceFeeAmount, _merchantFee);
                    //            oPaymentDetail.MerchantFeeForAutoGratuityAmount = CalculateMerchantFee(oPaymentDetail.AutoGratuityAmount + oPaymentDetail.SVCFeeForAutoGratuityAmount, _merchantFee);
                    //        }

                    //        #region Compare Merchant Fee For Service Charge

                    //        decimal tempTotalMerchantFee = oPaymentDetail.MerchantFeeForServiceFee + oPaymentDetail.MerchantFeeForAreaFeeAmount +
                    //        oPaymentDetail.MerchantFeeForPartyServiceFeeAmount + oPaymentDetail.MerchantFeeForAutoGratuityAmount;

                    //        if (tempMerchantFeeForServiceCharge != tempTotalMerchantFee)
                    //        {
                    //            decimal tempVal = tempTotalMerchantFee - tempMerchantFeeForServiceCharge;

                    //            if (oPaymentDetail.MerchantFeeForPartyServiceFeeAmount > 0 && tempVal != 0)
                    //            {
                    //                oPaymentDetail.MerchantFeeForPartyServiceFeeAmount -= tempVal;
                    //                tempVal = 0;
                    //            }
                    //            else if (oPaymentDetail.MerchantFeeForAreaFeeAmount > 0 && tempVal != 0)
                    //            {
                    //                oPaymentDetail.MerchantFeeForAreaFeeAmount -= tempVal;
                    //                tempVal = 0;
                    //            }
                    //            else if (oPaymentDetail.MerchantFeeForServiceFee > 0 && tempVal != 0)
                    //            {
                    //                oPaymentDetail.MerchantFeeForServiceFee -= tempVal;
                    //                tempVal = 0;
                    //            }
                    //            else if (oPaymentDetail.MerchantFeeForAutoGratuityAmount > 0 && tempVal != 0)
                    //            {
                    //                oPaymentDetail.MerchantFeeForAutoGratuityAmount -= tempVal;
                    //                tempVal = 0;
                    //            }
                    //        }

                    //        #endregion
                    //    }

                    //    if (!string.IsNullOrWhiteSpace(oReceipt.ReceiptInfo))
                    //    {
                    //        var tempModel = Utility.ParseJsonToReceiptInfo(oReceipt.ReceiptInfo);
                    //        if (tempModel.RemoveSVCFeeAndCashDiscount)
                    //        {
                    //            oPaymentDetail.SVCFeeForServiceCharge = 0;
                    //            oPaymentDetail.SVCFeeForServiceFee = 0;
                    //            oPaymentDetail.SVCFeeForPartyServiceFeeAmount = 0;
                    //            oPaymentDetail.SVCFeeForAreaFeeAmount = 0;
                    //            oPaymentDetail.SVCFeeForAutoGratuityAmount = 0;
                    //        }
                    //    }
                    //}
                    //else
                    //{
                    //    if (oPaymentDetail.PaymentMethod.PaymentMethodid == -2 || oPaymentDetail.PaymentMethod.PaymentMethodid == -3)
                    //    {
                    //        decimal tempMerchantFeeForServiceCharge = 0;
                    //        if (isApplyTaxCustomFee)
                    //        {
                    //            decimal taxsc = oPaymentDetail.AutoGratuityTaxAmount + oPaymentDetail.AreaFeeTaxAmount + oPaymentDetail.PartyServiceFeeTaxAmount + oPaymentDetail.ServiceFeeTaxAmount;
                    //            tempMerchantFeeForServiceCharge = CalculateMerchantFee(oPaymentDetail.PaidServiceFee - taxsc, _merchantFee);
                    //            oPaymentDetail.MerchantFeeForServiceFee = CalculateMerchantFee(oPaymentDetail.ServiceFee - oPaymentDetail.ServiceFeeTaxAmount, _merchantFee);
                    //            oPaymentDetail.MerchantFeeForAreaFeeAmount = CalculateMerchantFee(oPaymentDetail.AreaFeeAmount - oPaymentDetail.AreaFeeTaxAmount, _merchantFee);
                    //            oPaymentDetail.MerchantFeeForPartyServiceFeeAmount = CalculateMerchantFee(oPaymentDetail.PartyServiceFeeAmount - oPaymentDetail.PartyServiceFeeTaxAmount, _merchantFee);
                    //            oPaymentDetail.MerchantFeeForAutoGratuityAmount = CalculateMerchantFee(oPaymentDetail.AutoGratuityAmount - oPaymentDetail.AutoGratuityTaxAmount, _merchantFee);
                    //        }
                    //        else
                    //        {
                    //            tempMerchantFeeForServiceCharge = CalculateMerchantFee(oPaymentDetail.PaidServiceFee, _merchantFee);
                    //            oPaymentDetail.MerchantFeeForServiceFee = CalculateMerchantFee(oPaymentDetail.ServiceFee, _merchantFee);
                    //            oPaymentDetail.MerchantFeeForAreaFeeAmount = CalculateMerchantFee(oPaymentDetail.AreaFeeAmount, _merchantFee);
                    //            oPaymentDetail.MerchantFeeForPartyServiceFeeAmount = CalculateMerchantFee(oPaymentDetail.PartyServiceFeeAmount, _merchantFee);
                    //            oPaymentDetail.MerchantFeeForAutoGratuityAmount = CalculateMerchantFee(oPaymentDetail.AutoGratuityAmount, _merchantFee);
                    //        }

                    //        #region Compare Merchant Fee For Service Charge

                    //        decimal tempTotalMerchantFee = oPaymentDetail.MerchantFeeForServiceFee + oPaymentDetail.MerchantFeeForAreaFeeAmount +
                    //        oPaymentDetail.MerchantFeeForPartyServiceFeeAmount + oPaymentDetail.MerchantFeeForAutoGratuityAmount;

                    //        if (tempMerchantFeeForServiceCharge != tempTotalMerchantFee)
                    //        {
                    //            decimal tempVal = tempTotalMerchantFee - tempMerchantFeeForServiceCharge;
                    //            if (oPaymentDetail.MerchantFeeForPartyServiceFeeAmount > 0 && tempVal != 0)
                    //            {
                    //                oPaymentDetail.MerchantFeeForPartyServiceFeeAmount -= tempVal;
                    //                tempVal = 0;
                    //            }
                    //            else if (oPaymentDetail.MerchantFeeForAreaFeeAmount > 0 && tempVal != 0)
                    //            {
                    //                oPaymentDetail.MerchantFeeForAreaFeeAmount -= tempVal;
                    //                tempVal = 0;
                    //            }
                    //            else if (oPaymentDetail.MerchantFeeForServiceFee > 0 && tempVal != 0)
                    //            {
                    //                oPaymentDetail.MerchantFeeForServiceFee -= tempVal;
                    //                tempVal = 0;
                    //            }
                    //            else if (oPaymentDetail.MerchantFeeForAutoGratuityAmount > 0 && tempVal != 0)
                    //            {
                    //                oPaymentDetail.MerchantFeeForAutoGratuityAmount -= tempVal;
                    //                tempVal = 0;
                    //            }
                    //        }

                    //        #endregion
                    //    }
                    //    else
                    //    {
                    //        oPaymentDetail.MerchantFeeForServiceFee = 0;
                    //        oPaymentDetail.MerchantFeeForAreaFeeAmount = 0;
                    //        oPaymentDetail.MerchantFeeForPartyServiceFeeAmount = 0;
                    //        oPaymentDetail.MerchantFeeForAutoGratuityAmount = 0;
                    //    }
                    //}

                    #endregion

                    //Calculate SVC Fee for Gratuity, Service Charge
                    CalculateCustomFeeForServiceCharge(oPaymentDetail, oReceipt, isApplyTaxCustomFee, cashAdjust, _svcFee, _cashDiscount, _merchantFee, _svcFeeType, _ApplyCustomFeeWhenPurchaseGiftCard, _dTaxIncludeInSVCFee);

                    // tuanph.11.18.2019 delete PreAuth when captured
                    if (oPaymentDetail.CreditCardTransactionType == "PreAuthCaptured")
                    {
                        //RemoveCreditCardSaleReceiptPaymentDetails(session, oPaymentDetail.CreditCardAuthCode);
                        oPaymentDetail.CreatedDate = DateTime.Now;
                        BLogicServerLogger.Error($"# ProccessPayment => SavePaymentDetail => DeletePreAuthTransaction");
                    }
                    BLogicServerLogger.Error($"# ProccessPayment => SavePaymentDetail => session.Save(oPaymentDetail)");
                    //Trung Add 2024.06.03 Update Balance (House Account)

                }

            }
            catch (Exception ex)
            {
                BLogicLogger.Exception("", ex, new List<object>() { SaleReceiptID });
            }
        }

        private bool IsFinishSale(SaleReceiptEntity sre)
        {
            if (sre == null)
                return false;
            if (sre.TransactionStatusID == null)
                return false;
            if (sre.TransactionStatusID == TransactionStatus.FINISHED)
                return true;

            return false;
        }

        public decimal CalculateTaxServiceCharge(decimal sAmount, decimal sPercent, decimal sTotalTaxByServiceCharge, DataSet dsSaleReceipt)
        {
            decimal stax = 0;
            try
            {
                DataTable dtSaleReceiptServiceChargeTaxDetails = null;
                if (dsSaleReceipt.Tables.Contains("SaleReceiptServiceChargeTaxDetails"))
                    dtSaleReceiptServiceChargeTaxDetails = dsSaleReceipt.Tables["SaleReceiptServiceChargeTaxDetails"];

                decimal temp = 0;
                if (dtSaleReceiptServiceChargeTaxDetails == null || dtSaleReceiptServiceChargeTaxDetails.Rows.Count == 0)
                {
                    if (sAmount == 0 || sTotalTaxByServiceCharge == 0 || sPercent == 0)
                        return 0;
                    temp = (sAmount / (1 + sPercent / 100));
                    stax = Utility.RoundUp(temp * sPercent / 100);
                    return stax;
                }
                else
                {
                    foreach (DataRow dataRow in dtSaleReceiptServiceChargeTaxDetails.Rows)
                    {
                        decimal sPercentByServiceCharge = Utility.GetDecimalFromRow("ServiceChargeTaxRate", dataRow);
                        if (sAmount == 0 || sTotalTaxByServiceCharge == 0 || sPercentByServiceCharge == 0)
                        {
                            stax += 0;
                        }
                        else
                        {
                            temp = sAmount * sPercentByServiceCharge / 100;
                            stax += Utility.RoundUp(temp / (1 + sPercent / 100));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                BLogicLogger.Error("#### CalculateTaxServiceCharge with error: ", ex.Message, ex);
            }
            return stax;
        }

        public void UpdateTaxServiceChargeinPayment(decimal dAutoGratuityTaxAmount, decimal dAreaFeeTaxAmount, decimal dPartyServiceFeeTaxAmount, decimal dServiceFeeTaxAmount,
                              SaleReceiptEntity oReceipt)
        {
            try
            {
                if (oReceipt == null)
                    return;

                if (oReceipt.SalesOrderFee == 0 || oReceipt.ServiceChargeTaxRate == 0)
                    return;
                if (oReceipt.SaleReceiptPaymentList == null)
                    return;

                decimal tempsc = 0;

                #region ServiceFee

                if (dServiceFeeTaxAmount != 0 && dServiceFeeTaxAmount != oReceipt.ServiceFeeTaxAmount + oReceipt.ExtraFeeTaxAmount)
                {
                    tempsc = oReceipt.ServiceFeeTaxAmount + oReceipt.ExtraFeeTaxAmount - dServiceFeeTaxAmount;
                    if (tempsc != 0)
                    {
                        for (int i = oReceipt.SaleReceiptPaymentList.Count - 1; i >= 0; i--)
                        {
                            SaleReceiptPaymentDetailEntity payment = oReceipt.SaleReceiptPaymentList[i] as SaleReceiptPaymentDetailEntity;
                            if (payment == null) continue;
                            if (payment.ServiceFeeTaxAmount != 0)
                            {
                                payment.ServiceFeeTaxAmount += tempsc;
                                break;
                            }
                        }
                    }
                }
                else if (dServiceFeeTaxAmount == 0 && dServiceFeeTaxAmount != oReceipt.ServiceFeeTaxAmount + oReceipt.ExtraFeeTaxAmount)
                {
                    tempsc = oReceipt.ServiceFeeTaxAmount + oReceipt.ExtraFeeTaxAmount - dServiceFeeTaxAmount;
                    if (tempsc != 0)
                    {
                        for (int i = oReceipt.SaleReceiptPaymentList.Count - 1; i >= 0; i--)
                        {
                            SaleReceiptPaymentDetailEntity payment = oReceipt.SaleReceiptPaymentList[i] as SaleReceiptPaymentDetailEntity;
                            if (payment == null) continue;
                            if (payment.ServiceFee != 0)
                            {
                                payment.ServiceFeeTaxAmount += tempsc;
                                break;
                            }
                        }
                    }
                }
                #endregion

                #region PartyServiceFee
                if (dPartyServiceFeeTaxAmount != 0 && dPartyServiceFeeTaxAmount != oReceipt.PartyServiceFeeTaxAmount)
                {
                    tempsc = oReceipt.PartyServiceFeeTaxAmount - dPartyServiceFeeTaxAmount;
                    if (tempsc != 0)
                    {
                        for (int i = oReceipt.SaleReceiptPaymentList.Count - 1; i >= 0; i--)
                        {
                            SaleReceiptPaymentDetailEntity payment = oReceipt.SaleReceiptPaymentList[i] as SaleReceiptPaymentDetailEntity;
                            if (payment == null) continue;
                            if (payment.PartyServiceFeeTaxAmount != 0)
                            {
                                payment.PartyServiceFeeTaxAmount += tempsc;
                                break;
                            }
                        }
                    }
                }
                else if (dPartyServiceFeeTaxAmount == 0 && dPartyServiceFeeTaxAmount != oReceipt.PartyServiceFeeTaxAmount)
                {
                    tempsc = oReceipt.PartyServiceFeeTaxAmount - dPartyServiceFeeTaxAmount;
                    if (tempsc != 0)
                    {
                        for (int i = oReceipt.SaleReceiptPaymentList.Count - 1; i >= 0; i--)
                        {
                            SaleReceiptPaymentDetailEntity payment = oReceipt.SaleReceiptPaymentList[i] as SaleReceiptPaymentDetailEntity;
                            if (payment == null) continue;
                            if (payment.PartyServiceFeeAmount != 0)
                            {
                                payment.PartyServiceFeeTaxAmount += tempsc;
                                break;
                            }
                        }
                    }
                }
                #endregion

                #region AreaFee
                if (dAreaFeeTaxAmount != 0 && dAreaFeeTaxAmount != oReceipt.AreaFeeTaxAmount)
                {
                    tempsc = oReceipt.AreaFeeTaxAmount - dAreaFeeTaxAmount;
                    if (tempsc != 0)
                    {
                        for (int i = oReceipt.SaleReceiptPaymentList.Count - 1; i >= 0; i--)
                        {
                            SaleReceiptPaymentDetailEntity payment = oReceipt.SaleReceiptPaymentList[i] as SaleReceiptPaymentDetailEntity;
                            if (payment == null) continue;
                            if (payment.AreaFeeTaxAmount != 0)
                            {
                                payment.AreaFeeTaxAmount += tempsc;
                                break;
                            }
                        }
                    }
                }
                else if (dAreaFeeTaxAmount == 0 && dAreaFeeTaxAmount != oReceipt.AreaFeeTaxAmount)
                {
                    tempsc = oReceipt.AreaFeeTaxAmount - dAreaFeeTaxAmount;
                    if (tempsc != 0)
                    {
                        for (int i = oReceipt.SaleReceiptPaymentList.Count - 1; i >= 0; i--)
                        {
                            SaleReceiptPaymentDetailEntity payment = oReceipt.SaleReceiptPaymentList[i] as SaleReceiptPaymentDetailEntity;
                            if (payment == null) continue;
                            if (payment.AreaFeeAmount != 0)
                            {
                                payment.AreaFeeTaxAmount += tempsc;
                                break;
                            }
                        }
                    }
                }
                #endregion

                #region AutoGratuity

                if (dAutoGratuityTaxAmount != 0 && dAutoGratuityTaxAmount != oReceipt.AutoGratuityTaxAmount)
                {
                    tempsc = oReceipt.AutoGratuityTaxAmount - dAutoGratuityTaxAmount;
                    if (tempsc != 0)
                    {
                        for (int i = oReceipt.SaleReceiptPaymentList.Count - 1; i >= 0; i--)
                        {
                            SaleReceiptPaymentDetailEntity payment = oReceipt.SaleReceiptPaymentList[i] as SaleReceiptPaymentDetailEntity;
                            if (payment == null) continue;
                            if (payment.AutoGratuityTaxAmount != 0)
                            {
                                payment.AutoGratuityTaxAmount += tempsc;
                                break;
                            }
                        }
                    }
                }
                else if (dAutoGratuityTaxAmount == 0 && dAutoGratuityTaxAmount != oReceipt.AutoGratuityTaxAmount)
                {
                    tempsc = oReceipt.AutoGratuityTaxAmount - dAutoGratuityTaxAmount;
                    if (tempsc != 0)
                    {
                        for (int i = oReceipt.SaleReceiptPaymentList.Count - 1; i >= 0; i--)
                        {
                            SaleReceiptPaymentDetailEntity payment = oReceipt.SaleReceiptPaymentList[i] as SaleReceiptPaymentDetailEntity;
                            if (payment == null) continue;
                            if (payment.AutoGratuityAmount != 0)
                            {
                                payment.AutoGratuityTaxAmount += tempsc;
                                break;
                            }
                        }
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                BLogicLogger.Error("#### UpdateTaxServiceChargeinPayment with error: ", ex.Message, ex);
            }
        }

        protected virtual void UpdateValuePaymentDetailsForRETURNSaleReceipt(SaleReceiptPaymentDetailEntity srpde)
        {
            srpde.AmountReturn *= -1;
            srpde.AmountTendered *= -1;
        }

        private decimal GetPaidPayout(DataSet dsSaleReceipt)//SaleReceiptEntity  oReceipt)
        {
            decimal dReturn = 0;
            decimal dTemp = 0;
            string sSRDname = "SaleReceiptDetails";
            if (dsSaleReceipt == null)
                return dReturn;
            if (!dsSaleReceipt.Tables.Contains(sSRDname))
                return dReturn;

            DataTable dtDetail = dsSaleReceipt.Tables[sSRDname];
            if (dtDetail == null || dtDetail.Rows.Count == 0)
                return dReturn;

            DataTable dtItem = null;
            if (dsSaleReceipt.Tables.Contains("Items"))
                dtItem = dsSaleReceipt.Tables["Items"];

            string itemid = string.Empty;
            DataRow[] drs;
            foreach (DataRow row in dtDetail.Rows)
            {
                dTemp = Utility.GetDecimalFromRow("ExtendedPrice", row);
                bool isPaidOut = false;
                if (dTemp < 0)
                {
                    itemid = Utility.GetStringFromRow("ItemID", row);
                    if (dtItem != null && dtItem.Rows.Count > 0)
                    {
                        drs = dtItem.Select("ItemID='" + itemid + "' AND (DeptDeptID='-1' OR DeptDeptID='-3')");
                        if (drs.Length > 0)
                            isPaidOut = true;
                    }

                    if (isPaidOut)
                        dReturn += dTemp;
                }
            }

            //foreach (SaleReceiptDetailEntity srde in oReceipt.SaleReceiptDetailList )
            //{
            //    if (srde.ExtendedPrice < 0)
            //        dReturn += srde.ExtendedPrice;
            //}
            return dReturn;
        }

    }
}
