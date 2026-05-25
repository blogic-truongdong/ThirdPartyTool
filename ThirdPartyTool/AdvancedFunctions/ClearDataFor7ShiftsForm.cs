using BLogicSystems.XPos.ClientUtilities;
using BLogicSystems.XPos.Utilites;
using BLogicSystems.XPos.Utilities.HttpClientHelper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Windows.Forms;
using ThirdPartyTool.Dialog;
using ThirdPartyTool.Models._7Shifts;

namespace ThirdPartyTool.AdvancedFunctions
{
    public partial class ClearDataFor7ShiftsForm : Form
    {
        #region Constants
        private string storeID = string.Empty;
        private bool IsCancelBackgroundWorker = false;
        private const int CENTS_MULTIPLIER = 100;
        private const string STORE_ID_20201742 = "20201742";
        private const string STORE_ID_20201568 = "20201568";
        private const string STORE_ID_20201413 = "20201413";

        private const string STATUS_FINISHED = "FIN";
        private const string STATUS_VOID = "VOID";
        private const string STATUS_RETURN = "RET";

        private const string SEVEN_SHIFTS_STATUS_CLOSED = "closed";
        private const string SEVEN_SHIFTS_STATUS_VOIDED = "voided";
        private const string SEVEN_SHIFTS_STATUS_DELETED = "deleted";

        private const string ORDER_TYPE_DINE_IN = "dine_in";
        private const string ORDER_TYPE_TAKE_OUT = "take_out";
        private const int DINE_IN_RECEIPT_TYPE = 16;

        private const string PAYMENT_TYPE_CREDIT_CARD = "cc";
        private const string PAYMENT_TYPE_CASH = "cash";
        private const int CREDIT_CARD_PAYMENT_METHOD_ID = 2;

        #endregion

        #region Fields

        private BackgroundWorker backgroundWorker;

        #endregion

        #region Constructor

        public ClearDataFor7ShiftsForm()
        {
            InitializeComponent();

            InitializeEvents();
            InitializeBackgroundWorker();
        }

        #endregion

        #region Initialization

        private void InitializeEvents()
        {
            this.Load += CloseUnPaidTicketForm_Load;
            btnCancel.Click += BtnCancel_Click;
            btnOK.Click += BtnOK_Click;
            btnCancelProcess.Click += BtnCancelProcess_Click;
        }

        private void BtnCancelProcess_Click(object sender, EventArgs e)
        {
            IsCancelBackgroundWorker = true;
        }

        private void InitializeBackgroundWorker()
        {
            backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += BackgroundWorker_DoWork;
            backgroundWorker.RunWorkerCompleted += BackgroundWorker_RunWorkerCompleted;
        }

        #endregion

        #region Event Handlers

        private void CloseUnPaidTicketForm_Load(object sender, EventArgs e)
        {
            StartDate.Value = new DateTime(DateTime.Now.Year, 1, 1);
            EndDate.Value = DateTime.Now;
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ConfirmSyncOperation())
                    return;

                StartProcess();
                Task.Run(() => backgroundWorker.RunWorkerAsync());

            }
            catch (Exception ex)
            {
                ShowErrorDialog("Unable to sync data due to a system error. Please try again later or contact TruongDS.");
                BLogicLogger.Exception("BtnOK_Click in ReSyncDataFor7ShiftsForm", ex);
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private async void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            decimal sumTotal = 0;
            decimal sumTip = 0;
            int totalReceipt = 0;
            try
            {
                string accessToken = "37303131323238622d336636632d343733362d616531312d343261343533343261666136";

                var _httpClient = new HttpClient();
                _httpClient.BaseAddress = new Uri("https://api.7shifts.com/");
                _httpClient.DefaultRequestHeaders.Accept.Clear();
                _httpClient.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", accessToken);

                string companyId = "337570";
                string locationId = "411979";

                string url =
                 "https://api.7shifts.com/v2/company/337570/receipts" +
                 "?location_id=411979" +
                 "&limit=100";

            ProcessRetry:

                HttpResponseMessage response = await _httpClient.GetAsync(url);

                string responseBody = await response.Content.ReadAsStringAsync();

                var listSaleReceipt = JsonConvert.DeserializeObject<SevenShifts_ListReceipt>(responseBody);

                foreach (var item in listSaleReceipt.data)
                {
                    if (item.ReceiptDate.Date > new DateTime(2026, 05, 11))
                        continue;

                    UpdateReceiptFor7ShiftsModel receiptModel = new UpdateReceiptFor7ShiftsModel()
                    {
                        net_total = 0,
                        gross_total = 0,
                        total_receipt_discounts = 0,
                        tips = 0,
                        status = item.Status,
                        order_type = ORDER_TYPE_DINE_IN,
                        dining_option = "",
                        external_user_id = "",
                        revenue_center = "",
                        receipt_date = item.ReceiptDate.ToString("yyyy-MM-ddTHH:mm:ssZ")
                    };

                    string jsonData = JsonConvert.SerializeObject(receiptModel);

                    HttpClientFactoryResponse httpClientFactoryResponse = Helper.ProcessRequest_7Shifts_Sync($"receipts/{item.ID}", System.Net.Http.HttpMethod.Put, jsonData);

                }

                if (!string.IsNullOrWhiteSpace(listSaleReceipt?.meta?.cursor?.next))
                {
                    url =
                    $"https://api.7shifts.com/v2/company/{companyId}/receipts" +
                    $"?location_id={locationId}" +
                    $"&limit=100" +
                    $"&cursor={listSaleReceipt?.meta?.cursor?.next.Substring(15)}";

                    goto ProcessRetry;

                }
                else
                {

                }

            }
            catch (Exception ex)
            {
                BLogicLogger.Exception("ReSyncDataFor7ShiftsForm.BackgroundWorker_DoWork", ex);
            }
        }

        private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                EndProcess();
            }
            catch (Exception ex)
            {
                BLogicLogger.Exception("ReSyncDataFor7ShiftsForm.BackgroundWorker_RunWorkerCompleted", ex);
            }
        }

        #endregion

        #region Main Processing Logic

        private void ProcessSevenShiftsSync()
        {
            LoadSevenShiftsConfiguration();

            if (!ConfigureStoreSpecificSettings())
                return;


        }

        public async Task<string> GetReceiptsAsync()
        {
            string accessToken = "<ACCESS_TOKEN>";

            string url =
                "https://api.7shifts.com/v2/company/337570/receipts" +
                "?location_id=411979" +
                "&receipt_date%5Blte%5D=2026-05-01" +
                "&limit=100";

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", accessToken);

                HttpResponseMessage response = await client.GetAsync(url);
                string responseBody = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception(
                        "7shifts API error: " +
                        response.StatusCode +
                        " - " +
                        responseBody);
                }

                return responseBody;
            }
        }

        private void ProcessSaleReceipts(DataTable dtSaleReceipts)
        {
            foreach (DataRow row in dtSaleReceipts.Rows)
            {
                try
                {
                    ProcessSingleSaleReceipt(row);
                }
                catch (Exception ex)
                {
                    string saleReceiptId = Utility.GetStringFromRow("SaleReceiptID", row);
                    BLogicLogger.Exception($"Error processing SaleReceiptID: {saleReceiptId}", ex);
                }

                if (IsCancelBackgroundWorker)
                {
                    break;
                }
            }
        }

        private void ProcessSingleSaleReceipt(DataRow saleReceiptRow)
        {
            string saleReceiptID = Utility.GetStringFromRow("SaleReceiptID", saleReceiptRow);

            DataTable dtSaleReceiptDetails = LoadSaleReceiptDetails(saleReceiptID);
            DataTable dtSaleReceiptPaymentDetails = LoadSaleReceiptPaymentDetails(saleReceiptID);

            CreateReceiptFor7ShiftsModel receiptModel = BuildReceiptModel(saleReceiptRow, dtSaleReceiptDetails, dtSaleReceiptPaymentDetails);

            SendReceiptToSevenShifts(receiptModel, saleReceiptID);
        }

        #endregion

        #region Data Loading

        private void LoadSevenShiftsConfiguration()
        {
            string sql = "SELECT * FROM Configurations " +
                        "WHERE ConfigurationName IN (" +
                        "'SystemConfigValue.SevenShiftAPIKey', " +
                        "'SystemConfigValue.SevenShiftLocation', " +
                        "'SystemConfigValue.SevenShiftCompanyID', " +
                        "'SystemConfigValue.CurrentSevenShiftLocation', " +
                        "'SystemConfigValue.SevenShiftAccessToken')";

            DataTable dtConfiguration = Helper.ProcessQuery(true, sql, "Configurations");

            PopulateConfigurationValues(dtConfiguration);
        }

        private void PopulateConfigurationValues(DataTable dtConfiguration)
        {
            POSConfig.Configuration.ConfigValue.SystemConfigValue.SevenShiftAPIKey =
                GetConfigValue(dtConfiguration, "SystemConfigValue.SevenShiftAPIKey");

            POSConfig.Configuration.ConfigValue.SystemConfigValue.SevenShiftCompanyID =
                GetConfigValue(dtConfiguration, "SystemConfigValue.SevenShiftCompanyID");

            POSConfig.Configuration.ConfigValue.SystemConfigValue.SevenShiftLocation =
                Utility.GetIntFromObject(GetConfigValue(dtConfiguration, "SystemConfigValue.SevenShiftLocation"));

            POSConfig.Configuration.ConfigValue.SystemConfigValue.SevenShiftAccessToken =
                GetConfigValue(dtConfiguration, "SystemConfigValue.SevenShiftAccessToken");
        }

        private string GetConfigValue(DataTable dtConfiguration, string configName)
        {
            DataRow[] rows = dtConfiguration.Select($"ConfigurationName ='{configName}'");
            return rows.Length > 0 ? rows[0]["ConfigurationValue"].ToString() : string.Empty;
        }

        private bool ConfigureStoreSpecificSettings()
        {
            string sql = "SELECT * FROM Stores";
            DataTable dtStores = Helper.ProcessQuery(true, sql, "Stores");

            if (dtStores?.Rows.Count == 0)
                return false;

            storeID = Utility.GetStringFromRow("StoreID", dtStores.Rows[0]);
            // TODO: Remove this hardcoded override

            return ApplyStoreConfiguration(storeID);
        }

        private bool ApplyStoreConfiguration(string storeID)
        {
            switch (storeID)
            {
                case STORE_ID_20201742:
                    SetSevenShiftsConfig("", "337570", 411980, "30393837636664642d633865302d346237372d613839332d666332653633373861366238");
                    return true;

                case STORE_ID_20201568:
                    SetSevenShiftsConfig("", "337570", 434330, "30393837636664642d633865302d346237372d613839332d666332653633373861366238");
                    return true;

                case STORE_ID_20201413:
                    SetSevenShiftsConfig("", "337570", 411979, "30393837636664642d633865302d346237372d613839332d666332653633373861366238");
                    return true;

                default:
                    ShowErrorDialog("Cannot sync data for this store");
                    BLogicLogger.Error($"Unsupported Store ID for 7Shifts sync: {storeID}");
                    return false;
            }
        }

        private void SetSevenShiftsConfig(string apiKey, string companyId, int locationId, string accessToken)
        {
            POSConfig.Configuration.ConfigValue.SystemConfigValue.SevenShiftAPIKey = apiKey;
            POSConfig.Configuration.ConfigValue.SystemConfigValue.SevenShiftCompanyID = companyId;
            POSConfig.Configuration.ConfigValue.SystemConfigValue.SevenShiftLocation = locationId;
            POSConfig.Configuration.ConfigValue.SystemConfigValue.SevenShiftAccessToken = accessToken;
        }

        private DataTable LoadSaleReceipts()
        {
            string sql = $"SELECT top 100 a.SaleReceiptID, a.TransactionStatusID, a.SaleReceiptType, " +
                        $"a.TotalAmount, a.TaxAmount, a.SalesOrderFee, a.TotalDiscountAmount, a.DateCreated " +
                        $"FROM SaleReceipts a " +
                        $"WHERE a.DateCreated >= '{StartDate.Value}' AND a.DateCreated <= '{EndDate.Value}' " +
                        $"AND a.TransactionStatusID IN ('{STATUS_FINISHED}', '{STATUS_VOID}', '{STATUS_RETURN}') " +
                        $"AND a.SaleReceiptID NOT IN (SELECT b.SaleReceiptID FROM SaleReceiptThirdPartyMappings b) " +
                        $"ORDER BY a.DateCreated";

            return Helper.ProcessQuery(true, sql, "SaleReceipts");
        }

        private DataTable LoadSaleReceiptDetails(string saleReceiptID)
        {
            string sql = $"SELECT c.CategoryIDUnique, a.* " +
                        $"FROM SaleReceiptDetails a " +
                        $"LEFT JOIN Items b ON a.ItemID = b.ItemID " +
                        $"LEFT JOIN Departments c ON b.DeptDeptID = c.DepartmentID " +
                        $"WHERE a.SaleReceiptID = '{saleReceiptID}'";

            return Helper.ProcessQuery(true, sql, "SaleReceiptDetails");
        }

        private DataTable LoadSaleReceiptPaymentDetails(string saleReceiptID)
        {
            string sql = $"SELECT * FROM SaleReceiptPaymentDetails WHERE SaleReceiptID = '{saleReceiptID}'";
            return Helper.ProcessQuery(true, sql, "SaleReceiptPaymentDetails");
        }

        #endregion

        #region Model Building

        private CreateReceiptFor7ShiftsModel BuildReceiptModel(DataRow saleReceiptRow, DataTable dtDetails, DataTable dtPayments)
        {
            string saleReceiptID = Utility.GetStringFromRow("SaleReceiptID", saleReceiptRow);
            string transactionStatus = Utility.GetStringFromRow("TransactionStatusID", saleReceiptRow);
            int saleReceiptType = Utility.GetIntFromRow("SaleReceiptType", saleReceiptRow);

            decimal gratuity = Utility.GetDecimalFromObject(dtPayments.Compute("SUM(Gratuity)", ""));

            CreateReceiptFor7ShiftsModel model = new CreateReceiptFor7ShiftsModel
            {
                receipt_id = saleReceiptID,
                location_id = POSConfig.Configuration.ConfigValue.SystemConfigValue.SevenShiftLocation,
                receipt_date = Utility.GetDateTimeFromRow("DateCreated", saleReceiptRow).ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ssZ"),
                net_total = CalculateNetTotal(saleReceiptRow, transactionStatus),
                gross_total = CalculateGrossTotal(saleReceiptRow),
                total_receipt_discounts = ConvertToCents(Utility.GetDecimalFromRow("TotalDiscountAmount", saleReceiptRow)),
                tips = ConvertToCents(gratuity),
                status = MapTransactionStatus(transactionStatus),
                order_type = saleReceiptType == DINE_IN_RECEIPT_TYPE ? ORDER_TYPE_DINE_IN : ORDER_TYPE_TAKE_OUT
            };

            AddTipDetails(model, dtPayments);
            AddReceiptLines(model, dtDetails, transactionStatus);

            return model;
        }

        private int CalculateNetTotal(DataRow row, string transactionStatus)
        {
            decimal netTotal = (Utility.GetDecimalFromRow("TotalAmount", row) -
                               Utility.GetDecimalFromRow("TaxAmount", row) -
                               Utility.GetDecimalFromRow("SalesOrderFee", row));

            if (transactionStatus == STATUS_RETURN)
                netTotal *= -1;

            return ConvertToCents(netTotal);
        }

        private int CalculateGrossTotal(DataRow row)
        {
            decimal grossTotal = Utility.GetDecimalFromRow("TotalAmount", row) -
                                Utility.GetDecimalFromRow("TaxAmount", row);
            return ConvertToCents(grossTotal);
        }

        private void AddTipDetails(CreateReceiptFor7ShiftsModel model, DataTable dtPayments)
        {
            foreach (DataRow payment in dtPayments.Rows)
            {
                int paymentMethodId = Utility.GetIntFromRow("PaymentMethodID", payment);
                decimal gratuity = Utility.GetDecimalFromRow("Gratuity", payment);

                model.tip_details.Add(new CreateReceiptFor7ShiftsModel_TipDetail
                {
                    type = paymentMethodId == CREDIT_CARD_PAYMENT_METHOD_ID ? PAYMENT_TYPE_CREDIT_CARD : PAYMENT_TYPE_CASH,
                    value = ConvertToCents(gratuity)
                });
            }
        }

        private void AddReceiptLines(CreateReceiptFor7ShiftsModel model, DataTable dtDetails, string transactionStatus)
        {
            foreach (DataRow detail in dtDetails.Rows)
            {
                CreateReceiptFor7ShiftsModel_SaleReceiptDetail line = BuildReceiptLine(detail, transactionStatus);
                model.receipt_lines.Add(line);
            }
        }

        private CreateReceiptFor7ShiftsModel_SaleReceiptDetail BuildReceiptLine(DataRow detail, string transactionStatus)
        {
            int itemStatus = Utility.GetIntFromRow("Status", detail);
            decimal quantity = Utility.GetDecimalFromRow("Quantity", detail);
            decimal price = Utility.GetDecimalFromRow("Price", detail);
            decimal extendedPrice = Utility.GetDecimalFromRow("ExtendedPrice", detail);
            decimal itemDiscount = CalculateItemDiscount(quantity, price, extendedPrice);

            var line = new CreateReceiptFor7ShiftsModel_SaleReceiptDetail
            {
                created = Utility.GetDateTimeFromRow("OrderDateTime", detail).ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ssZ"),
                external_item_id = Utility.GetStringFromRow("ItemID", detail),
                gross_item_price = ConvertToCents(extendedPrice),
                net_item_price = ConvertToCents(extendedPrice),
                price = ConvertToCents(price),
                item_discount = ConvertToCents(itemDiscount),
                quantity = Utility.GetIntFromRow("Quantity", detail),
                status = MapItemStatus(itemStatus, transactionStatus),
                external_category_ids = new List<string> { Utility.GetStringFromRow("CategoryIDUnique", detail) }
            };

            return line;
        }

        private decimal CalculateItemDiscount(decimal quantity, decimal price, decimal extendedPrice)
        {
            decimal discount = (quantity * price) - extendedPrice;
            return discount < 0 ? 0 : discount;
        }

        #endregion

        #region 7Shifts API Integration

        private void SendReceiptToSevenShifts(CreateReceiptFor7ShiftsModel receiptModel, string saleReceiptID)
        {
            string jsonData = JsonConvert.SerializeObject(receiptModel);
            var response = Helper.ProcessRequest_7Shifts_Sync("receipts", System.Net.Http.HttpMethod.Post, jsonData);

            if (response.IsSuccess)
            {
                ProcessSuccessfulResponse(response.Message, saleReceiptID);
            }
            else
            {
                BLogicLogger.Error($"Failed to sync receipt {saleReceiptID} to 7Shifts: {response.Message}");
            }
        }

        private void ProcessSuccessfulResponse(string responseMessage, string saleReceiptID)
        {
            try
            {
                var responseModel = JsonConvert.DeserializeObject<CreateReceiptFor7ShiftsResponseModel>(responseMessage);

                if (!string.IsNullOrWhiteSpace(responseModel?.data?.receipt_id))
                {
                    SaveSevenShiftsMapping(saleReceiptID, responseModel.data.uuid);
                }
            }
            catch (Exception ex)
            {
                BLogicLogger.Exception($"Error processing 7Shifts response for SaleReceiptID: {saleReceiptID}", ex);
            }
        }

        private void SaveSevenShiftsMapping(string saleReceiptID, string sevenShiftsUuid)
        {
            string sql = $"INSERT INTO SaleReceiptThirdPartyMappings (SaleReceiptID, SaleReceiptID_7Shifts) " +
                        $"VALUES ('{saleReceiptID}', '{sevenShiftsUuid}')";

            Helper.RunQueryUpdate(true, sql);
        }

        #endregion

        #region Helper Methods

        private bool ConfirmSyncOperation()
        {
            ConfirmDialogForm confirmDialog = new ConfirmDialogForm(
                Const.ConfirmTitle,
                $"Are you sure you want to sync sales data {storeID} for 7Shifts?"
            );

            var result = BLogicMessageBox.ShowPopUpFormWithDIMBackground(confirmDialog, this);
            return result != DialogResult.No;
        }

        private void ShowErrorDialog(string message)
        {
            ErrorDialogForm errorDialog = new ErrorDialogForm(message);
            BLogicMessageBox.ShowPopUpFormWithDIMBackground(errorDialog, this);
        }

        private bool IsSevenShiftsAccessTokenValid()
        {
            return !string.IsNullOrWhiteSpace(POSConfig.Configuration.ConfigValue.SystemConfigValue.SevenShiftAccessToken);
        }

        private string MapTransactionStatus(string transactionStatus)
        {
            return (transactionStatus == STATUS_FINISHED || transactionStatus == STATUS_RETURN)
                ? SEVEN_SHIFTS_STATUS_CLOSED
                : SEVEN_SHIFTS_STATUS_VOIDED;
        }

        private string MapItemStatus(int itemStatus, string transactionStatus)
        {
            if (itemStatus == 2)
                return SEVEN_SHIFTS_STATUS_DELETED;

            return transactionStatus == STATUS_VOID
                ? SEVEN_SHIFTS_STATUS_VOIDED
                : SEVEN_SHIFTS_STATUS_CLOSED;
        }

        private int ConvertToCents(decimal amount)
        {
            return (int)(amount * CENTS_MULTIPLIER);
        }

        #endregion

        #region UI State Management

        private void StartProcess()
        {
            if (pnlContainer.InvokeRequired)
            {
                pnlContainer.Invoke(new MethodInvoker(delegate
                {
                    SetProcessingState(true);
                }));
            }
            else
            {
                SetProcessingState(true);
            }
        }

        private void EndProcess()
        {
            if (pnlContainer.InvokeRequired)
            {
                pnlContainer.Invoke(new MethodInvoker(delegate
                {
                    SetProcessingState(false);
                }));
            }
            else
            {
                SetProcessingState(false);
            }
        }

        private void SetProcessingState(bool isProcessing)
        {
            pnlContainer.Enabled = !isProcessing;
            pnlLoadingContainer.Visible = isProcessing;
        }

        #endregion
    }

}
