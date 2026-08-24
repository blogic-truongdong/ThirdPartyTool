using BLogicPOS7SupportTool.Models;
using BLogicSystems.XPos.Utilites;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace BLogicPOS7SupportTool.Core
{
    static class AppSession
    {
        private static bool IsUseConfigDualPrice = false;
        private static bool IsApplyTaxForSVCFee = false;
        public static string BLogicPOS7DatabaseName { get; private set; }
        public static string BLogicPOS7ConnectionString { get; private set; }

        public static string MerchantDatabaseName { get; private set; }
        public static string MerchantConnectionString { get; private set; }

        public static void SetBLogicPOS7(string connectionString, string databaseName)
        {
            BLogicPOS7ConnectionString = connectionString;
            BLogicPOS7DatabaseName = databaseName;
        }

        public static void SetMerchant(string connectionString, string databaseName)
        {
            MerchantConnectionString = connectionString;
            MerchantDatabaseName = databaseName;
        }

        // ---- Dapper connections ----------------------------------------------
        // Open connections for Dapper queries, e.g.:
        //   using (var conn = AppSession.OpenBLogicPOS7())
        //       var rows = conn.Query<SaleReceipt>("SELECT * FROM SaleReceipts WHERE ...", new { ... });
        // Caller is responsible for disposing (use a `using` block).

        public static SqlConnection OpenBLogicPOS7()
        {
            var conn = new SqlConnection(BLogicPOS7ConnectionString);
            conn.Open();
            return conn;
        }

        public static SqlConnection OpenMerchant()
        {
            var conn = new SqlConnection(MerchantConnectionString);
            conn.Open();
            return conn;
        }

        // Dapper equivalent of ProcessQuery: runs a parameterized query and maps
        // the rows to T. Best-effort (logs + returns empty on error). Example:
        //   var list = AppSession.Query<ReceiptPaymentDetail>(true,
        //       "SELECT * FROM dbo.SaleReceiptPaymentDetails WHERE SaleReceiptID = @id",
        //       new { id = receiptId });
        public static List<T> Query<T>(bool isRunOnBLogicPOS7, string sql, object param = null)
        {
            string connStr = isRunOnBLogicPOS7 ? BLogicPOS7ConnectionString : MerchantConnectionString;
            try
            {
                using (var conn = new SqlConnection(connStr))
                    return conn.Query<T>(sql, param).AsList();
            }
            catch (Exception ex)
            {
                AppLogger.Error($"Query<{typeof(T).Name}> failed", ex);
                return new List<T>();
            }
        }

        // Single-row Dapper query (returns default(T) when none / on error).
        public static T QueryFirstOrDefault<T>(bool isRunOnBLogicPOS7, string sql, object param = null)
        {
            string connStr = isRunOnBLogicPOS7 ? BLogicPOS7ConnectionString : MerchantConnectionString;
            try
            {
                using (var conn = new SqlConnection(connStr))
                    return conn.QueryFirstOrDefault<T>(sql, param);
            }
            catch (Exception ex)
            {
                AppLogger.Error($"QueryFirstOrDefault<{typeof(T).Name}> failed", ex);
                return default(T);
            }
        }

        // ---- Configurations cache --------------------------------------------

        // ConfigurationName -> ConfigurationValue, loaded once at startup.
        private static readonly Dictionary<string, string> _configurations =
            new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        // ---- Configuration values as flat static fields --------------------
        // Named by the suffix of ConfigurationName (the part after the prefix),
        // bound at startup in BindConfigFields() via reflection. Numeric configs
        // are typed; the rest are raw strings. (OpenCashDrawer exists under both
        // SystemConfigValue and HideFeatureValue and shares the one field below.)
#pragma warning disable 649 // assigned via reflection in BindConfigFields()

        // CreditCardConfigValue.*
        public static decimal SVCFee;
        public static int SVCFeeType;
        public static decimal MerchantFee;
        public static decimal CashDiscount;
        public static decimal DualPricePercent;
        public static int TaxIncludeInSVCFee;
        public static decimal MerchantFeePercentDualPrice;
        public static decimal BaseAmountFee;
        public static decimal BaseAmountFeePercentDualPrice;
        public static decimal MinPreAuthAmount;
        public static decimal CCMinimumVolume;
        public static string TipAmountKiosk1;
        public static string TipAmountKiosk2;
        public static string TipAmountKiosk3;
        public static string TipAmountKiosk4;
        public static string TipPercentKiosk1;
        public static string TipPercentKiosk2;
        public static string TipPercentKiosk3;
        public static string TipPercentKiosk4;
        public static string SuggestedTipsAmountValue;
        public static string SuggestedTipsAmountValuePCD;
        public static string ShowCustomFeeOnAllReport;
        public static string PrintCashDiscountOnCashDiscount_2_0;
        public static string MerchantFeeLabelKiosk;
        public static string ServiceFeeCalculationToServer;
        public static string LabelCreditTotal;
        public static string ApplyCustomFeeWhenPurchaseGiftCard;
        public static string ApplyCustomFeeWhenPurchaseHouseAccount;
        public static string CCStatement;
        public static string UsingTipInLine;
        public static string SignatureRequiredKiosk;
        public static string SignatureRequiredLimitKiosk;
        public static string PromTipsBeforeProcessingCcKiosk;
        public static string CashDiscountFlatFeeKiosk;
        public static string CashDiscountKiosk;
        public static string PartyServiceFeeCalculationToServer;
        public static string PrintCCStoreReceipt;
        public static string ShowCloseBatchButton;
        public static string TipSuggestionKiosk;
        public static string TipSuggestionCalculate;
        public static string TipSuggestionCalculateKiosk;
        public static string AutoSettlementKiosk;
        public static string AutoSettleTimeKiosk;
        public static string UseCashOwed;
        public static string MerchantFeeKiosk;
        public static string MerchantFeeLabel;
        public static string MerchantFeeLabelWeb;
        public static string MerchantFeeLabelPhone;
        public static string CCSuggestedTips;
        public static string CCSuggestedTipsPCD;
        public static string CCSuggestedTipsPhoneApp;
        public static string CCSuggestedTipsAmount;
        public static string CCSuggestedTipsAmountPCD;
        public static string ApplyCashDiscountForTipSuggestion;
        public static string IsTableAfterCapture;
        public static string CheckDuplicateTransactions;
        public static string ShowSVCFeeOnCustomerDisplay;
        public static string ShowCashDiscountOnCustomerDisplay;
        public static string SettlementReportKiosk;
        public static string EmailSettlementReportKiosk;
        public static string EnablePrintSettlementReportKiosk;
        public static string EnableSendEmailSettlementReportKiosk;
        public static string IsUseSuggestedTipsAmount;
        public static string IsUseSuggestedTipsAmountPCD;
        public static string SvcFeeLabelPhone;
        public static string SvcFeeLabelWeb;
        public static string SvcFeeLabelKiosk;
        public static string SvcFeeModeKiosk;
        public static string SvcFeeKiosk;
        public static string SvcFlatAmountKiosk;
        public static string SVCFeeLabel;
        public static string PrintSVCFeeForGuestCheck;
        public static string CapturedType;
        public static string IsCredit;
        public static string AutoClosePageThankYouTimeKiosk;
        public static string UsingOpenTab;
        public static string UsingOpenTabForQRCode;
        public static string HideNoTipButtonKiosk;
        public static string LabelCashTotal;
        public static string IsShowSVCFeeAndMerChantFeeInReport;
        public static string AskTipBeforeProcessCreditCard;
        public static string AskTipBeforeProcessCreditCardQOrder;
        public static string TipCalculationToServer;
        public static string ShowTipSuggestionPriorToGiftCardPayment;
        public static string DepositAmountLabel;
        public static string DepositAmountLabelKiosk;
        public static string AutoGetTerminalIPAddress;
        public static string TaxCodeIDForCustomFee;
        public static string EnableToken;
        public static string EnableTokenizationForKiosk;
        public static string BypassChangingAmount;
        public static string UseTipInlineKiosk;
        public static string PrintCustomFee;
        public static string CCReceiptMessage;
        public static string CCCopyMessage;
        public static string ShowPrintReceiptAfterCCKiosk;
        public static string CalculateCommissionBasedOnCashPrice;
        public static string MaxAmountKiosk;
        public static string EnableTipSuggestionKiosk;
        public static string AutoGratuityFeeCalculationToServer;
        public static string AreaFeeCalculationToServer;
        public static string PaymentType;

        // SystemConfigValue.*
        public static string EnableDualPrice;
        public static string LabelCreditTotalOnSOrder;
        public static string LabelCreditTotalOnSOrderByItemPriceMode;
        public static string LabelCreditTotalOnQOrder;
        public static string LabelCreditTotalOnQOrderByItemPriceMode;
        public static string LabelCreditTotalOnPOS;
        public static string LabelCreditTotalOnPOSByItemPriceMode;
        public static string LabelCashTotalOnSOrder;
        public static string LabelCashTotalOnSOrderByItemPriceMode;
        public static string LabelCashTotalOnQOrder;
        public static string LabelCashTotalOnQOrderByItemPriceMode;
        public static string LabelCashTotalOnPOS;
        public static string LabelCashTotalOnPOSByItemPriceMode;
        public static string PaymentByCashOnly;
        public static string ShowHideCashOwedToEmployeeEmployeeReport;
        public static string ShowHideCashOwedToEmployeeShiftReport;
        public static string ShowHideCreditCardInSalesReportBreakdown;
        public static string ShowHideCreditCardInSalesReport;
        public static string ShowHideCreditCardShiftDetailsReport;
        public static string ShowHideCreditCardShiftReport;
        public static string PrintSubtotalInCreditCardReceipt;
        public static string CreditCardPrintCancel;
        public static string CreditCardPrintCopy;
        public static string FulfillModifiersIndividually;
        public static string ShowHideCashShiftDetailsReport;
        public static string ShowHideCashShiftReport;
        public static string PrintReceiptPaidByCash;
        public static string OpenCashDrawer;
        public static string ShowHideCashOnHandInSalesReportBreakdown;
        public static string ShowHideCashOnHandInSalesReport;
        public static string PrintPayByCashInReceiptWithFont;
        public static string PrintPayByCashInReceipt;
        public static string IsShowCashTotalOnPOS;
        public static string IsShowCashTotalOnQOrder;
        public static string IsShowCashTotalOnSOrder;
        public static string IsUpdateDataBaseCalculateMerchantFee;
        public static string ShowHideCashCCOnTopInSalesReportBreakdown;
        public static string ShowHideCashCCOnTopInSalesReport;
        public static string PrintGuestCheckForDualPriceOption;
        public static string EmailSupportMerchant;
        public static string SendEmailSupportMerchant;
        public static string PrintCashierNameOnEndOfShiftReport;
        public static string ShowTipPayCashForSOrder;
        public static string ShowHideCashOwedInSalesReportBreakdown;
        public static string ShowHideCashOwedInSalesReport;
        public static string EnableMerchantSetting;
        public static string ShowHideCashDrawerShiftDetailsReport;
        public static string ShowHideCashDrawerShiftReport;
        public static string ShowHideCashOwedEmployeeReport;
        public static string ShowHideCashOwedShiftReport;
        public static string ShowHideCashOwedShiftDetailsReport;
        public static string SkipCustomerProcessOn2ndScreenCash;
        public static string PrintCashDiscount;
        public static string BtnCreditCardVoid;
        public static string ShowButtonRemoveSVCFeeInPOS;
        public static string PrintTipLineForCreditCard;
        public static string SRDVPayWithCashStyle;
        public static string ShowItemPriceOnDualPriceMode;
        public static string PrintGuestReceiptForCreditCard;
        public static string ShowHideCashInSalesReportBreakdown;
        public static string ShowHideCashInSalesReport;
        public static string ApplyCreditPriceWhenPurchasingGiftCard;
        public static string AutomaticPrintGuestCheckForDualPrice;
        public static string AutomaticPrintShortGuestCheckForDualPrice;
        public static string AlwaysAskPermissionToOpenCashDrawer;
        public static string UpdateCashierID;
        public static string DoNotPrintSaleReceiptPaidByCreditCard;

        // HideFeatureValue.*  (OpenCashDrawer shares the field above)
        public static string CashDrop;
        public static string Gratuity;

        // BOConfigValue.*
        public static string ReportSalesByExtendCashier;

        // SystemConfigValue.* — tips / gratuity / suggestion / reports (added later)
        public static string PrintSuggestTipInCCReceipt;
        public static string PrintSuggestTip;
        public static string PrintSuggestTipPCD;
        public static string ShowSuggestTipPhoneApp;
        public static string ShowSuggestTipForQRCodeDineIn;
        public static string SuggestionTipsForQRCodeDineIn;
        public static string ForcedSuggestionTipsForQRCodeDineIn;
        public static string ForcedSuggestionTipsForOrderOnline;
        public static string TipSuggestionLabel;
        public static string AllowToOverwriteForcedTipForOrderOnline;
        public static string AllowToOverwriteForcedTipForQRCodeDineIn;
        public static string AllowToAdjustTip;
        public static string EnterTipWhenClockOutOnPOS;
        public static string MustReviewAllUnTipTransactions;
        public static string HideCustomTipButton;
        public static string HideNoTipButton;
        public static string ExtraTipLabel;
        public static string ExtraTipLabelForQOrder;
        public static string CalculateTipShareOption;
        public static string ShowGratuityInPaymentScreen;
        public static string ShowServiceChargesOnTipAndSignature2ndScreen;
        public static string GratuityName;
        public static string GratuityPercent;
        public static string AutoGratuityName;
        public static string AutoGratuityPercent;
        public static string AutoGratuityBelongToStore;
        public static string ElectronicScaleMultiplyValue;
        public static string ShowHideTipsInSalesReport;
        public static string ShowHideTipsInSalesReportBreakdown;
        public static string ShowHideTipsEmployeeReport;
        public static string ShowHideTipsShiftReport;
        public static string ShowHideTipsShiftDetailsReport;
        public static string ShowHideTipPaidToEmployeeShiftReport;
        public static string ShowHideTipPaidToEmployeeShiftDetailsReport;
        public static string ShowHideTipPaidToEmployeeInSalesReport;
        public static string ShowHideTipPaidToEmployeeInSalesReportBreakdown;
        public static string ShowHideTipsPaidToEmployeeEmployeeReport;
        public static string ShowHideAreaGratuityInSalesReport;
        public static string ShowHideAreaGratuityInSalesReportBreakdown;
        public static string ShowHideAreaGratuityTaxInSalesReport;
        public static string ShowHideAreaGratuityTaxInSalesReportBreakdown;
        public static string ShowHideAreaGratuityEmployeeReport;
        public static string ShowHidePrintAreaGratuityShiftReport;
        public static string ShowHidePrintAreaGratuityShiftDetailsReport;
        public static string ShowHidePartyGratuityInSalesReport;
        public static string ShowHidePartyGratuityInSalesReportBreakdown;
        public static string ShowHidePartyGratuityTaxInSalesReport;
        public static string ShowHidePartyGratuityTaxInSalesReportBreakdown;
        public static string ShowHidePartyGratuityEmployeeReport;
        public static string ShowHidePartyGratuityShiftReport;
        public static string ShowHidePartyGratuityShiftDetailsReport;
        public static string ShowHideAutoGratuityEmployeeReport;
        public static string ShowHidePrintAutoGratuityShiftReport;
        public static string ShowHidePrintAutoGratuityShiftDetailReport;

        // SystemConfigValue.MultipleLocationReport.Indexes (suffix after last dot)
        public static string Indexes;

#pragma warning restore 649

        // Derived (not a raw config): tax rate % of the custom-fee tax code
        // (CreditCardConfigValue.TaxCodeIDForCustomFee). Computed after configs load.
        public static decimal TaxPercentForSVCFee;

        // Loads every row of dbo.Configurations into memory so the app can read
        // config values without hitting the DB each time. Best-effort.
        public static void LoadConfigurations()
        {
            if (string.IsNullOrEmpty(BLogicPOS7ConnectionString)) return;

            try
            {
                _configurations.Clear();
                var dt = ProcessQuery(true,
                    "SELECT ConfigurationName, ConfigurationValue FROM dbo.Configurations", "Configurations");

                if (dt != null)
                    foreach (DataRow row in dt.Rows)
                    {
                        var name = GetString(row, "ConfigurationName");
                        if (!string.IsNullOrEmpty(name))
                            _configurations[name] = GetString(row, "ConfigurationValue");
                    }

                BindConfigFields();
                LoadTaxPercentForSVCFee();

                IsUseConfigDualPrice = EnableDualPrice == "1" && DualPricePercent > 0m;
                IsApplyTaxForSVCFee = !IsUseConfigDualPrice && SVCFee > 0 && TaxPercentForSVCFee > 0;

                if (BaseAmountFee == 0 && SVCFeeType == 1 && SVCFee > 0)
                {
                    BaseAmountFee = ReCalcuateBaseAmountFeeFromPercent(SVCFee);
                }

                AppLogger.Info($"Loaded {_configurations.Count} configurations into AppSession");
            }
            catch (Exception ex)
            {
                AppLogger.Error("LoadConfigurations failed", ex);
            }
        }

        private static decimal ReCalcuateBaseAmountFeeFromPercent(decimal percent)
        {
            return Math.Round(percent / (100m + percent) * 100m, 8);
        }

        // Looks up the tax rate (%) of the custom-fee tax code and stores it in
        // TaxPercentForSVCFee. Uses the TaxCodeIDForCustomFee config just loaded.
        private static void LoadTaxPercentForSVCFee()
        {
            TaxPercentForSVCFee = 0m;
            try
            {
                if (string.IsNullOrWhiteSpace(TaxCodeIDForCustomFee)) return;

                var dt = ProcessQuery(true, $@"
                    SELECT ISNULL(SUM(c.SingleTaxRatePercentage), 0) AS TaxPercent
                    FROM dbo.TaxCodes a
                    LEFT JOIN dbo.TaxCodeDetails b ON a.TaxCodeID = b.TaxCodeID
                    LEFT JOIN dbo.TaxAuthorities c ON b.TaxAuthorityID = c.TaxAuthorityID
                    WHERE a.TaxCodeID = '{TaxCodeIDForCustomFee.Replace("'", "''")}'", "TaxPercentForSVCFee");

                if (dt != null && dt.Rows.Count > 0)
                    TaxPercentForSVCFee = GetDecimal(dt.Rows[0], "TaxPercent");
            }
            catch (Exception ex)
            {
                AppLogger.Error("LoadTaxPercentForSVCFee failed", ex);
            }
        }

        // Binds the static config fields from the cache: each config's suffix
        // (text after the last '.') is matched to a public static field of the
        // same name; the value is parsed to the field's type.
        private static void BindConfigFields()
        {
            foreach (var kvp in _configurations)
            {
                int dot = kvp.Key.LastIndexOf('.');
                string suffix = dot >= 0 ? kvp.Key.Substring(dot + 1) : kvp.Key;

                var field = typeof(AppSession).GetField(suffix, BindingFlags.Public | BindingFlags.Static);
                if (field == null) continue;

                try { field.SetValue(null, ParseConfigValue(kvp.Value, field.FieldType)); }
                catch { /* leave default on parse/type mismatch */ }
            }
        }

        private static object ParseConfigValue(string raw, Type type)
        {
            raw = raw ?? "";
            if (type == typeof(string)) return raw;
            if (type == typeof(decimal))
            {
                decimal.TryParse(raw, NumberStyles.Any, CultureInfo.InvariantCulture, out var d);
                return d;
            }
            if (type == typeof(int))
            {
                int.TryParse(raw, NumberStyles.Any, CultureInfo.InvariantCulture, out var i);
                return i;
            }
            if (type == typeof(bool))
                return raw == "1" || string.Equals(raw, "true", StringComparison.OrdinalIgnoreCase);

            return null;
        }

        public static string GetConfig(string name, string defaultValue = "")
        {
            if (string.IsNullOrEmpty(name)) return defaultValue;
            return _configurations.TryGetValue(name, out var value) ? value : defaultValue;
        }

        public static decimal GetConfigDecimal(string name, decimal defaultValue = 0m)
        {
            return decimal.TryParse(GetConfig(name), NumberStyles.Any, CultureInfo.InvariantCulture, out var v)
                ? v : defaultValue;
        }

        public static int GetConfigInt(string name, int defaultValue = 0)
        {
            return int.TryParse(GetConfig(name), NumberStyles.Any, CultureInfo.InvariantCulture, out var v)
                ? v : defaultValue;
        }

        public static bool GetConfigBool(string name, bool defaultValue = false)
        {
            var v = GetConfig(name);
            if (string.IsNullOrWhiteSpace(v)) return defaultValue;
            if (v == "1") return true;
            if (v == "0") return false;
            return bool.TryParse(v, out var b) ? b : defaultValue;
        }

        // Runs Scripts/CreateScriptLogTable.sql against BLOGICPOS7 at startup so the
        // audit table always exists. Best-effort: never throws (a missing file or
        // permission issue must not block the app). The DDL is idempotent.
        public static void EnsureScriptLogTable()
        {
            if (string.IsNullOrEmpty(BLogicPOS7ConnectionString)) return;

            try
            {
                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Scripts", "CreateScriptLogTable.sql");
                if (!File.Exists(path))
                {
                    AppLogger.Warn($"Script log DDL not found, skipping: {path}");
                    return;
                }

                string script = File.ReadAllText(path);

                // ADO.NET can't run "GO" batch separators — split and run each batch.
                var batches = Regex.Split(script, @"^\s*GO\s*$", RegexOptions.Multiline | RegexOptions.IgnoreCase);

                using (var conn = new SqlConnection(BLogicPOS7ConnectionString))
                {
                    conn.Open();
                    foreach (var batch in batches)
                    {
                        if (string.IsNullOrWhiteSpace(batch)) continue;
                        using (var cmd = new SqlCommand(batch, conn))
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.ExecuteNonQuery();
                        }
                    }
                }

                AppLogger.Info("Script log table ensured (BLogicSupportToolScriptLogs)");
            }
            catch (Exception ex)
            {
                AppLogger.Error("EnsureScriptLogTable failed", ex);
            }
        }

        public static DataTable ProcessQuery(bool isRunOnBLogicPOS7, string sql, string tableName)
        {
            var dtReturn = new DataTable(tableName);
            string connStr = isRunOnBLogicPOS7 ? BLogicPOS7ConnectionString : MerchantConnectionString;
            try
            {
                using (var conn = new SqlConnection(connStr))
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.CommandType = CommandType.Text;
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                        dtReturn.Load(reader);
                }
            }
            catch (Exception ex)
            {
                AppLogger.Error($"ProcessQuery failed — table: {tableName}, db: {(isRunOnBLogicPOS7 ? BLogicPOS7DatabaseName : MerchantDatabaseName)}", ex);
            }
            return dtReturn;
        }

        public static object ProcessQuerySingleValue(bool isRunOnBLogicPOS7, string sql)
        {
            object valReturn = null;
            string connStr = isRunOnBLogicPOS7 ? BLogicPOS7ConnectionString : MerchantConnectionString;
            try
            {
                using (var conn = new SqlConnection(connStr))
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.CommandType = CommandType.Text;
                    conn.Open();
                    var dtReturn = new DataTable();

                    using (var reader = cmd.ExecuteReader())
                        dtReturn.Load(reader);

                    if (dtReturn?.Rows.Count > 0)
                    {
                        valReturn = dtReturn.Rows[0][0];
                    }
                }
            }
            catch (Exception ex)
            {
                AppLogger.Error($"ProcessQuerySingleValue failed, db: {(isRunOnBLogicPOS7 ? BLogicPOS7DatabaseName : MerchantDatabaseName)}", ex);
            }
            return valReturn;
        }

        public static int ProcessNonQuery(bool isRunOnBLogicPOS7, string sql)
        {
            string connStr = isRunOnBLogicPOS7 ? BLogicPOS7ConnectionString : MerchantConnectionString;
            string dbName = isRunOnBLogicPOS7 ? BLogicPOS7DatabaseName : MerchantDatabaseName;

            int rows = -1;
            bool success = false;
            string error = null;
            try
            {
                using (var conn = new SqlConnection(connStr))
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.CommandType = CommandType.Text;
                    conn.Open();
                    rows = cmd.ExecuteNonQuery();
                    success = true;
                }
            }
            catch (Exception ex)
            {
                error = ex.Message;
                AppLogger.Error("ProcessNonQuery failed", ex);
                rows = -1;
            }

            // Audit every non-query (all tools) into BLogicSupportToolScriptLogs.
            LogScript(sql, dbName, success ? rows : (int?)null, success, error);
            return rows;
        }

        // ---- Script audit log --------------------------------------------------

        [ThreadStatic] private static bool _isLogging;

        // Records a mutation script into dbo.BLogicSupportToolScriptLogs (always in
        // BLOGICPOS7). Best-effort: never throws, so auditing can't break the
        // operation (e.g. before the log table is created).
        private static void LogScript(string sql, string databaseName, int? rowsAffected, bool isSuccess, string errorMessage)
        {
            if (_isLogging) return;                                  // guard against recursion
            if (string.IsNullOrEmpty(BLogicPOS7ConnectionString)) return;

            _isLogging = true;
            try
            {
                using (var conn = new SqlConnection(BLogicPOS7ConnectionString))
                using (var cmd = new SqlCommand(@"
                    INSERT INTO dbo.BLogicSupportToolScriptLogs
                        (DatabaseName, ScriptText, RowsAffected, IsSuccess, ErrorMessage)
                    VALUES
                        (@DatabaseName, @ScriptText, @RowsAffected, @IsSuccess, @ErrorMessage)", conn))
                {
                    cmd.Parameters.AddWithValue("@DatabaseName", (object)databaseName ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@ScriptText", (object)(sql ?? "") ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@RowsAffected", (object)rowsAffected ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@IsSuccess", isSuccess);
                    cmd.Parameters.AddWithValue("@ErrorMessage", (object)errorMessage ?? DBNull.Value);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch
            {
                // Auditing must never break the real operation.
            }
            finally
            {
                _isLogging = false;
            }
        }

        public static string GetString(DataRow row, string col, string defaultValue = "")
        {
            if (row == null || !row.Table.Columns.Contains(col)) return defaultValue;
            return row[col] == DBNull.Value ? defaultValue : (row[col]?.ToString() ?? defaultValue);
        }

        public static int GetInt(DataRow row, string col, int defaultValue = 0)
        {
            if (row == null || !row.Table.Columns.Contains(col)) return defaultValue;
            return row[col] == DBNull.Value ? defaultValue : Convert.ToInt32(row[col]);
        }

        public static long GetLong(DataRow row, string col, long defaultValue = 0L)
        {
            if (row == null || !row.Table.Columns.Contains(col)) return defaultValue;
            return row[col] == DBNull.Value ? defaultValue : Convert.ToInt64(row[col]);
        }

        public static short GetShort(DataRow row, string col, short defaultValue = 0)
        {
            if (row == null || !row.Table.Columns.Contains(col)) return defaultValue;
            return row[col] == DBNull.Value ? defaultValue : Convert.ToInt16(row[col]);
        }

        public static byte GetByte(DataRow row, string col, byte defaultValue = 0)
        {
            if (row == null || !row.Table.Columns.Contains(col)) return defaultValue;
            return row[col] == DBNull.Value ? defaultValue : Convert.ToByte(row[col]);
        }

        public static decimal GetDecimal(DataRow row, string col, decimal defaultValue = 0m)
        {
            if (row == null || !row.Table.Columns.Contains(col)) return defaultValue;
            return row[col] == DBNull.Value ? defaultValue : Convert.ToDecimal(row[col]);
        }

        public static double GetDouble(DataRow row, string col, double defaultValue = 0.0)
        {
            if (row == null || !row.Table.Columns.Contains(col)) return defaultValue;
            return row[col] == DBNull.Value ? defaultValue : Convert.ToDouble(row[col]);
        }

        public static float GetFloat(DataRow row, string col, float defaultValue = 0f)
        {
            if (row == null || !row.Table.Columns.Contains(col)) return defaultValue;
            return row[col] == DBNull.Value ? defaultValue : Convert.ToSingle(row[col]);
        }

        public static bool GetBool(DataRow row, string col, bool defaultValue = false)
        {
            if (row == null || !row.Table.Columns.Contains(col)) return defaultValue;
            return row[col] == DBNull.Value ? defaultValue : Convert.ToBoolean(row[col]);
        }

        public static DateTime GetDateTime(DataRow row, string col, DateTime defaultValue = default(DateTime))
        {
            if (row == null || !row.Table.Columns.Contains(col)) return defaultValue;
            return row[col] is DateTime dt ? dt : defaultValue;
        }

        public static Guid GetGuid(DataRow row, string col, Guid defaultValue = default(Guid))
        {
            if (row == null || !row.Table.Columns.Contains(col)) return defaultValue;
            if (row[col] == DBNull.Value) return defaultValue;
            return row[col] is Guid g ? g : Guid.Parse(row[col].ToString());
        }

        public static decimal CalculateSVCFeeTaxAmount(decimal svcFeeBaseAmount)
        {
            if (svcFeeBaseAmount <= 0)
                return 0;

            if (!IsApplyTaxForSVCFee || SVCFee == 0)
                return 0;
            else
            {
                return RoundUp(TaxPercentForSVCFee * svcFeeBaseAmount / 100);
            }
        }

        public static decimal CalculateCashDiscountTaxAmount(decimal cashDiscountBaseAmount)
        {
            if (cashDiscountBaseAmount <= 0)
                return 0;

            if (!IsApplyTaxForSVCFee || SVCFee == 0)
                return 0;
            else
            {
                return RoundUp(TaxPercentForSVCFee * cashDiscountBaseAmount / 100);
            }
        }

        public static decimal CalculateSVCFeeTaxAmountFromSVCFeeAmount(decimal total)
        {
            if (total <= 0)
                return 0;

            if (!IsApplyTaxForSVCFee || SVCFee == 0)
                return 0;
            else
            {
                decimal svcBase = RoundUp(total / (1 + TaxPercentForSVCFee / 100));
                return total - svcBase;
            }
        }

        public static decimal CalculateSVCFeeFromTotalUseBaseAmountFee(decimal total)
        {
            if (total <= 0 || SVCFee == 0)
                return 0;
            if (SVCFeeType == 1)
            {
                decimal dBaseAmountFee = BaseAmountFee;
                return RoundUp(total * (dBaseAmountFee / 100));
            }
            else
            {
                return SVCFee;// Amount mode
            }

        }

        public static decimal CalculateMerchantFeeAmount(decimal total)
        {
            if (SVCFeeType == 1)
            {
                decimal svcMerchantFee = MerchantFee;
                return RoundUp(total * (svcMerchantFee / 100));
            }
            else
            {
                return 0;
            }
        }

        public static decimal CalculateMerchantFeeAmountWithParams(decimal total, decimal svcMerchantFee)
        {
            if (SVCFeeType == 1)
            {
                return RoundUp(total * (svcMerchantFee / 100));
            }
            else
            {
                return 0;
            }
        }

        public static decimal RoundUp(decimal dInValue, int intPlaces = 2)
        {
            if (dInValue >= 0)
            {
                decimal dReturn = Math.Round(dInValue, intPlaces);
                if (dReturn >= dInValue)
                    return dReturn;
                //dReturn < dInValue
                decimal dSub = dInValue - dReturn;
                if (dSub >= (decimal)0.005)
                    dReturn += (decimal)0.01;
                return dReturn;
            }
            else
            {
                dInValue = dInValue * -1;
                decimal dReturn = Math.Round(dInValue, 2);
                if (dReturn >= dInValue)
                {
                    dReturn = dReturn * -1;
                    return dReturn;
                }
                //dReturn < dInValue
                decimal dSub = dInValue - dReturn;
                if (dSub >= (decimal)0.005)
                    dReturn += (decimal)0.01;

                dReturn = dReturn * -1;
                return dReturn;
            }
        }

        public static void CorrectAllDataOfSaleReceipts(string receiptId)
        {
            try
            {
                CorrectDataSaleReceipts(receiptId);
                CorrectDataSaleReceiptDetailTaxDetails(receiptId);
                CorrectDataSaleReceiptServiceChargeTaxDetails(receiptId);
                CorrectDataPaymentRoleDetails(receiptId);
                CorrectDataSaleReceiptPaymentDetails(receiptId);
                CorrectDataTransactionDetail(receiptId);
            }
            catch (Exception ex)
            {

            }
        }

        public static void CorrectDataSaleReceipts(string receiptId)
        {
            try
            {
                var listScript = new List<string>();

                string sql = $@"SELECT * FROM dbo.SaleReceipts WHERE SaleReceiptID = '{receiptId}'";

                var sale = QueryFirstOrDefault<SaleReceiptsEntity>(true, sql);

                sql = $@"SELECT * FROM dbo.SaleReceiptDetails WHERE SaleReceiptID = '{receiptId}'";
                var saleDetails = Query<SaleReceiptDetailsEntity>(true, sql);

                sql = $@"SELECT a.*, b.CashAdjust FROM dbo.SaleReceiptPaymentDetails a left join PaymentMethods b on a.PaymentMethodID = b.PaymentMethodID WHERE SaleReceiptID = '{receiptId}'";
                var saleReceiptPaymentDetails = Query<SaleReceiptPaymentDetailsEntity>(true, sql);

                if (sale == null || saleDetails == null)
                {
                    return;
                }

                if (saleReceiptPaymentDetails == null)
                    saleReceiptPaymentDetails = new List<SaleReceiptPaymentDetailsEntity>();

                var newServiceChargeTaxAmount = sale.AreaFeeTaxAmount + sale.PartyServiceFeeTaxAmount + sale.ServiceFeeTaxAmount + sale.AutoGratuityTaxAmount + sale.ExtraFeeTaxAmount;

                var newSalesOrderFee = sale.AreaFeeAmount + sale.PartyServiceFeeAmount + sale.ServiceFee + sale.AutoGratuityAmount + sale.ExtraFeeAmount;
                var newTaxAmount = saleDetails.Where(x => x.Status != 2 && !x.IsCompItem).Sum(x => x.TaxAmount) + newServiceChargeTaxAmount;

                var newTotalAmount = saleDetails.Where(x => x.Status != 2 && !x.IsCompItem).Sum(x => x.ExtendedPrice) + newTaxAmount + newSalesOrderFee;

                var newPaidAmount = saleReceiptPaymentDetails.Sum(x => x.AmountTendered) - saleReceiptPaymentDetails.Sum(x => x.AmountReturn);
                var newSVCFeePaidAmount = saleReceiptPaymentDetails.Sum(x => x.SVCFeeAmount);
                var newSVCFeeTaxPaidAmount = saleReceiptPaymentDetails.Sum(x => x.SVCFeeTaxAmount);
                var newCashDiscountPaidAmount = saleReceiptPaymentDetails.Where(x => x.CashAdjust == 1).Sum(x => x.CashDiscountAmount);
                var newCashDiscountTaxPaidAmount = saleReceiptPaymentDetails.Where(x => x.CashAdjust == 1).Sum(x => x.CashDiscountTaxAmount);
                var newFinalGratuity = saleReceiptPaymentDetails.Sum(x => x.Gratuity);
                var newTotalDiscountAmount = saleDetails.Where(x => x.Status != 2 && !x.IsCompItem).Sum(x => x.StoreDiscountValue + x.DiscountAmount + x.TotalSaleDiscountAmountByCustomer + x.ItemDiscountByPriceLevelAmount + x.ItemDiscountValue + x.TotalSaleDiscountAmount);

                if (sale.TotalAmount != newTotalAmount)
                    listScript.Add($"Update dbo.SaleReceipts SET TotalAmount = '{newTotalAmount}' WHERE SaleReceiptID = '{receiptId}'");

                if (sale.TransactionStatusID == "FIN" || sale.TransactionStatusID == "VOID" || sale.TransactionStatusID == "RET" || sale.TransactionStatusID == "CANR")
                {
                    if (sale.PaidAmount != newTotalAmount)
                        listScript.Add($"Update dbo.SaleReceipts SET PaidAmount = '{newTotalAmount}' WHERE SaleReceiptID = '{receiptId}'");
                }
                else
                {
                    if (sale.PaidAmount != newPaidAmount)
                        listScript.Add($"Update dbo.SaleReceipts SET PaidAmount = '{newPaidAmount}' WHERE SaleReceiptID = '{receiptId}'");
                }

                if (sale.TransactionStatusID == "OPER" && newTotalAmount != newPaidAmount && newTotalAmount > 0 && newPaidAmount > 0)
                    listScript.Add($"Update dbo.SaleReceipts SET TransactionStatusID = 'FIN' WHERE SaleReceiptID = '{receiptId}'");

                if (sale.TotalDiscountAmount != newTotalDiscountAmount)
                    listScript.Add($"Update dbo.SaleReceipts SET TotalDiscountAmount = '{newTotalDiscountAmount}' WHERE SaleReceiptID = '{receiptId}'");

                if (sale.GlobalDiscountAmount != newTotalDiscountAmount)
                    listScript.Add($"Update dbo.SaleReceipts SET GlobalDiscountAmount = '{newTotalDiscountAmount}' WHERE SaleReceiptID = '{receiptId}'");

                if (sale.TaxAmount != newTaxAmount)
                    listScript.Add($"Update dbo.SaleReceipts SET TaxAmount = '{newTaxAmount}' WHERE SaleReceiptID = '{receiptId}'");

                if (sale.SalesOrderFee != newSalesOrderFee)
                    listScript.Add($"Update dbo.SaleReceipts SET SalesOrderFee = '{newSalesOrderFee}' WHERE SaleReceiptID = '{receiptId}'");

                if (sale.ServiceChargeTaxAmount != newServiceChargeTaxAmount)
                    listScript.Add($"Update dbo.SaleReceipts SET ServiceChargeTaxAmount = '{newServiceChargeTaxAmount}' WHERE SaleReceiptID = '{receiptId}'");

                if (sale.TransactionStatusID == "FIN" || sale.TransactionStatusID == "VOID" || sale.TransactionStatusID == "RET" || sale.TransactionStatusID == "CANR")
                {
                    if (sale.SVCFeePaidAmount != newSVCFeePaidAmount)
                        listScript.Add($"Update dbo.SaleReceipts SET SVCFeeAmount = '{newSVCFeePaidAmount}', SVCFeePaidAmount = '{newSVCFeePaidAmount}' WHERE SaleReceiptID = '{receiptId}'");

                    if (sale.SVCFeeTaxPaidAmount != newSVCFeeTaxPaidAmount)
                        listScript.Add($"Update dbo.SaleReceipts SET SVCFeeTaxAmount = '{newSVCFeeTaxPaidAmount}', SVCFeeTaxPaidAmount = '{newSVCFeeTaxPaidAmount}' WHERE SaleReceiptID = '{receiptId}'");

                    if (sale.CashDiscountPaidAmount != newCashDiscountPaidAmount)
                        listScript.Add($"Update dbo.SaleReceipts SET CashDiscountAmount = '{newCashDiscountPaidAmount}', CashDiscountPaidAmount = '{newCashDiscountPaidAmount}' WHERE SaleReceiptID = '{receiptId}'");

                    if (sale.CashDiscountTaxPaidAmount != newCashDiscountTaxPaidAmount)
                        listScript.Add($"Update dbo.SaleReceipts SET CashDiscountTaxAmount = '{newCashDiscountTaxPaidAmount}', CashDiscountTaxPaidAmount = '{newCashDiscountTaxPaidAmount}' WHERE SaleReceiptID = '{receiptId}'");
                }
                else
                {
                    if (sale.SVCFeePaidAmount != newSVCFeePaidAmount)
                        listScript.Add($"Update dbo.SaleReceipts SET SVCFeePaidAmount = '{newSVCFeePaidAmount}' WHERE SaleReceiptID = '{receiptId}'");

                    if (sale.SVCFeeTaxPaidAmount != newSVCFeeTaxPaidAmount)
                        listScript.Add($"Update dbo.SaleReceipts SET SVCFeeTaxPaidAmount = '{newSVCFeeTaxPaidAmount}' WHERE SaleReceiptID = '{receiptId}'");

                    if (sale.CashDiscountPaidAmount != newCashDiscountPaidAmount)
                        listScript.Add($"Update dbo.SaleReceipts SET CashDiscountPaidAmount = '{newCashDiscountPaidAmount}' WHERE SaleReceiptID = '{receiptId}'");

                    if (sale.CashDiscountTaxPaidAmount != newCashDiscountTaxPaidAmount)
                        listScript.Add($"Update dbo.SaleReceipts SET CashDiscountTaxPaidAmount = '{newCashDiscountTaxPaidAmount}' WHERE SaleReceiptID = '{receiptId}'");
                }

                if (sale.FinalGratuity != newFinalGratuity)
                    listScript.Add($"Update dbo.SaleReceipts SET FinalGratuity = '{newFinalGratuity}' WHERE SaleReceiptID = '{receiptId}'");

                foreach (var script in listScript)
                {
                    ProcessNonQuery(true, script);
                }
            }
            catch (Exception ex)
            {

            }
        }

        public static void CorrectDataSaleReceiptServiceChargeTaxDetails(string receiptId)
        {
            try
            {
                string sql = $@"SELECT * FROM dbo.SaleReceipts WHERE SaleReceiptID = '{receiptId}'";
                var sale = QueryFirstOrDefault<SaleReceiptsEntity>(true, sql);

                sql = $@"SELECT * FROM dbo.SaleReceiptServiceChargeTaxDetails WHERE SaleReceiptID = '{receiptId}' order by ServiceChargeTaxRate";
                var details = Query<SaleReceiptServiceChargeTaxDetailsEntity>(true, sql);

                if (sale == null || details == null || details.Count == 0)
                    return;

                // sumRate <= 0 (không lưu ServiceChargeTaxRate) không return nữa: AllocateTaxByRate
                // sẽ chia đều thuế cho các dòng thay vì phân bổ theo tỉ lệ.
                var sumRate = details.Sum(x => x.ServiceChargeTaxRate);

                // Lưu lại giá trị cũ (5 field thuế) để chỉ update khi thực sự thay đổi.
                var oldTax = details.ToDictionary(
                    d => d.SaleReceiptServiceChargeTaxDetailID,
                    d => new
                    {
                        d.ServiceFeeTaxAmount,
                        d.AreaFeeTaxAmount,
                        d.PartyServiceFeeTaxAmount,
                        d.AutoGratuityTaxAmount,
                        d.ExtraFeeTaxAmount
                    });

                // Phân bổ thuế tổng của từng loại service charge (trên SaleReceipts) về các dòng
                // detail theo tỉ lệ ServiceChargeTaxRate; phần lẻ do làm tròn được dồn vào dòng có
                // thuế lớn nhất để tổng các dòng khớp đúng tổng trên SaleReceipts.
                AllocateTaxByRate(details, sumRate, sale.ServiceFeeTaxAmount,
                    (d, v) => d.ServiceFeeTaxAmount = v, d => d.ServiceFeeTaxAmount);
                AllocateTaxByRate(details, sumRate, sale.AreaFeeTaxAmount,
                    (d, v) => d.AreaFeeTaxAmount = v, d => d.AreaFeeTaxAmount);
                AllocateTaxByRate(details, sumRate, sale.PartyServiceFeeTaxAmount,
                    (d, v) => d.PartyServiceFeeTaxAmount = v, d => d.PartyServiceFeeTaxAmount);
                AllocateTaxByRate(details, sumRate, sale.AutoGratuityTaxAmount,
                    (d, v) => d.AutoGratuityTaxAmount = v, d => d.AutoGratuityTaxAmount);
                AllocateTaxByRate(details, sumRate, sale.ExtraFeeTaxAmount,
                    (d, v) => d.ExtraFeeTaxAmount = v, d => d.ExtraFeeTaxAmount);

                var inv = CultureInfo.InvariantCulture;
                foreach (var item in details)
                {
                    var old = oldTax[item.SaleReceiptServiceChargeTaxDetailID];

                    // Chỉ đưa vào câu UPDATE những cột thực sự đổi so với dữ liệu cũ.
                    var sets = new List<string>();
                    if (item.ServiceFeeTaxAmount != old.ServiceFeeTaxAmount)
                        sets.Add($"ServiceFeeTaxAmount = {item.ServiceFeeTaxAmount.ToString(inv)}");
                    if (item.AreaFeeTaxAmount != old.AreaFeeTaxAmount)
                        sets.Add($"AreaFeeTaxAmount = {item.AreaFeeTaxAmount.ToString(inv)}");
                    if (item.PartyServiceFeeTaxAmount != old.PartyServiceFeeTaxAmount)
                        sets.Add($"PartyServiceFeeTaxAmount = {item.PartyServiceFeeTaxAmount.ToString(inv)}");
                    if (item.AutoGratuityTaxAmount != old.AutoGratuityTaxAmount)
                        sets.Add($"AutoGratuityTaxAmount = {item.AutoGratuityTaxAmount.ToString(inv)}");
                    if (item.ExtraFeeTaxAmount != old.ExtraFeeTaxAmount)
                        sets.Add($"ExtraFeeTaxAmount = {item.ExtraFeeTaxAmount.ToString(inv)}");

                    if (sets.Count == 0)
                        continue;

                    string upd = $@"UPDATE dbo.SaleReceiptServiceChargeTaxDetails SET {string.Join(", ", sets)}
                        WHERE SaleReceiptServiceChargeTaxDetailID = '{item.SaleReceiptServiceChargeTaxDetailID}'";
                    ProcessNonQuery(true, upd);
                }

                // Khi chỉ có đúng 1 dòng detail: đồng bộ các field amount (không phải thuế) từ SaleReceipts.
                // Cũng chỉ update những cột thực sự khác giá trị cũ trên dòng detail đó.
                if (details.Count == 1)
                {
                    var single = details[0];

                    var sets = new List<string>();
                    if (single.ServiceFee != sale.ServiceFee)
                        sets.Add($"ServiceFee = {sale.ServiceFee.ToString(inv)}");
                    if (single.PartyServiceFeeAmount != sale.PartyServiceFeeAmount)
                        sets.Add($"PartyServiceFeeAmount = {sale.PartyServiceFeeAmount.ToString(inv)}");
                    if (single.AreaFeeAmount != sale.AreaFeeAmount)
                        sets.Add($"AreaFeeAmount = {sale.AreaFeeAmount.ToString(inv)}");
                    if (single.AutoGratuityAmount != sale.AutoGratuityAmount)
                        sets.Add($"AutoGratuityAmount = {sale.AutoGratuityAmount.ToString(inv)}");
                    if (single.ExtraFeeAmount != sale.ExtraFeeAmount)
                        sets.Add($"ExtraFeeAmount = {sale.ExtraFeeAmount.ToString(inv)}");

                    if (sets.Count > 0)
                    {
                        sql = $@"
                            IF OBJECT_ID('dbo.SaleReceiptServiceChargeTaxDetails','U') IS NOT NULL
                               AND (SELECT COUNT(*) FROM dbo.SaleReceiptServiceChargeTaxDetails WHERE SaleReceiptID = '{receiptId}') = 1
                            BEGIN
                                UPDATE dbo.SaleReceiptServiceChargeTaxDetails SET {string.Join(", ", sets)}
                                WHERE SaleReceiptID = '{receiptId}'
                            END";

                        ProcessNonQuery(true, sql);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        // Chia total cho các dòng detail theo tỉ lệ ServiceChargeTaxRate, rồi bù phần lẻ (do làm
        // tròn) vào dòng có giá trị lớn nhất để tổng các dòng khớp đúng total. Trường hợp không lưu
        // ServiceChargeTaxRate (sumRate <= 0) thì chia đều total cho các dòng hiện có.
        public static void AllocateTaxByRate(
            List<SaleReceiptServiceChargeTaxDetailsEntity> details, decimal sumRate, decimal total,
            Action<SaleReceiptServiceChargeTaxDetailsEntity, decimal> setter,
            Func<SaleReceiptServiceChargeTaxDetailsEntity, decimal> getter)
        {
            if (sumRate > 0)
            {
                // Có lưu ServiceChargeTaxRate: phân bổ theo tỉ lệ.
                foreach (var item in details)
                    setter(item, Math.Round(item.ServiceChargeTaxRate * total / sumRate, 2));
            }
            else
            {
                // Không lưu ServiceChargeTaxRate (tổng = 0): chia đều total cho các dòng.
                var each = Math.Round(total / details.Count, 2);
                foreach (var item in details)
                    setter(item, each);
            }

            var balance = total - details.Sum(getter);
            if (balance == 0)
                return;

            // Dồn phần chênh lệch vào dòng có thuế lớn nhất; nếu mọi dòng đều 0 thì dồn vào dòng đầu.
            var target = details.Where(x => getter(x) != 0).OrderByDescending(getter).FirstOrDefault()
                         ?? details[0];
            setter(target, getter(target) + balance);
        }

        // Đồng bộ TaxAmount của SaleReceiptDetailTaxDetails theo TaxAmount của từng SaleReceiptDetails
        // (line item): với mỗi item, phân bổ TaxAmount xuống các dòng tax detail theo tỉ lệ TaxRate,
        // phần lẻ dồn vào dòng lớn nhất — logic tương tự CorrectDataSaleReceiptServiceChargeTaxDetails
        // nhưng áp cho từng item. Trường hợp không lưu TaxRate (tổng TaxRate = 0) thì chia đều
        // TaxAmount cho các dòng hiện có thay vì bỏ qua.
        public static void CorrectDataSaleReceiptDetailTaxDetails(string receiptId)
        {
            try
            {
                var details = Query<SaleReceiptDetailsEntity>(true,
                    $@"SELECT * FROM dbo.SaleReceiptDetails WHERE SaleReceiptID = '{receiptId}'");
                var taxDetails = Query<SaleReceiptDetailTaxDetailsEntity>(true,
                    $@"SELECT * FROM dbo.SaleReceiptDetailTaxDetails WHERE SaleReceiptID = '{receiptId}'");

                if (details == null || details.Count == 0 || taxDetails == null || taxDetails.Count == 0)
                    return;

                var inv = CultureInfo.InvariantCulture;

                foreach (var group in taxDetails.GroupBy(x => x.SaleReceiptDetailID))
                {
                    var item = details.FirstOrDefault(x => x.SaleReceiptDetailID == group.Key);
                    if (item == null)
                        continue;

                    var rows = group.ToList();
                    var sumRate = rows.Sum(x => x.TaxRate);

                    // Lưu lại giá trị cũ để chỉ update khi TaxAmount mới thực sự khác dữ liệu hiện có.
                    var oldTaxAmounts = rows.ToDictionary(
                        x => x.SaleReceiptDetailTaxDetailID);

                    if (sumRate > 0)
                    {
                        // Có lưu TaxRate: phân bổ TaxAmount của item xuống các dòng theo tỉ lệ TaxRate.
                        foreach (var r in rows)
                        {
                            r.TaxAmount = Math.Round(r.TaxRate * item.TaxAmount / sumRate, 2);
                            r.Price = item.Price;
                            r.ExtendedPrice = item.ExtendedPrice;
                            r.Status = item.Status;
                        }
                    }
                    else
                    {
                        // Không lưu TaxRate (tổng TaxRate = 0): chia đều TaxAmount của item cho các dòng.
                        var each = Math.Round(item.TaxAmount / rows.Count, 2);
                        foreach (var r in rows)
                        {
                            r.TaxAmount = each;
                            r.Price = item.Price;
                            r.ExtendedPrice = item.ExtendedPrice;
                            r.Status = item.Status;
                        }
                    }

                    // Bù phần lẻ do làm tròn vào dòng có thuế lớn nhất để tổng khớp item.TaxAmount.
                    var balance = item.TaxAmount - rows.Sum(x => x.TaxAmount);
                    if (balance != 0)
                    {
                        var target = rows.Where(x => x.TaxAmount != 0).OrderByDescending(x => x.TaxRate).FirstOrDefault()
                                     ?? rows[0];
                        target.TaxAmount += balance;
                    }

                    // Chỉ update dòng nào có TaxAmount mới khác giá trị cũ; dòng không đổi thì bỏ qua.
                    foreach (var r in rows)
                    {
                        var oldRow = oldTaxAmounts[r.SaleReceiptDetailTaxDetailID];
                        if (r.TaxAmount != oldRow.TaxAmount ||
                            r.Price != oldRow.Price ||
                            r.ExtendedPrice != oldRow.ExtendedPrice ||
                            r.Status != oldRow.Status)
                            continue;

                        ProcessNonQuery(true,
                            $@"UPDATE dbo.SaleReceiptDetailTaxDetails SET TaxAmount = '{r.TaxAmount.ToString(inv)}', Price = '{r.Price.ToString(inv)}', ExtendedPrice = '{r.ExtendedPrice.ToString(inv)}', Status = '{r.Status}'
                               WHERE SaleReceiptDetailTaxDetailID = '{r.SaleReceiptDetailTaxDetailID}'");
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        // Re-syncs the SaleReceiptDetailTaxDetails rows of ONE line item with that item's
        // TaxAmount / Price / ExtendedPrice / Status. Same rule as
        // CorrectDataSaleReceiptDetailTaxDetails but scoped to a single SaleReceiptDetailID,
        // so an edit of one item does not rewrite the whole ticket: the item's TaxAmount is
        // spread over its tax rows by TaxRate, or split evenly when no TaxRate is stored,
        // and the rounding remainder is pushed onto the largest row.
        public static void CorrectDataSaleReceiptDetailTaxDetailsByDetailID(string saleReceiptDetailId)
        {
            try
            {
                if (string.IsNullOrEmpty(saleReceiptDetailId))
                    return;

                var item = QueryFirstOrDefault<SaleReceiptDetailsEntity>(true,
                    $@"SELECT * FROM dbo.SaleReceiptDetails WHERE SaleReceiptDetailID = '{saleReceiptDetailId}'");
                var rows = Query<SaleReceiptDetailTaxDetailsEntity>(true,
                    $@"SELECT * FROM dbo.SaleReceiptDetailTaxDetails WHERE SaleReceiptDetailID = '{saleReceiptDetailId}'");

                if (item == null || rows == null || rows.Count == 0)
                    return;

                var inv = CultureInfo.InvariantCulture;
                var sumRate = rows.Sum(x => x.TaxRate);

                // Snapshot the current values so only rows that really change get written.
                var oldValues = rows.ToDictionary(
                    x => x.SaleReceiptDetailTaxDetailID,
                    x => new { x.TaxAmount, x.Price, x.ExtendedPrice, x.Status });

                if (sumRate > 0)
                {
                    // TaxRate is stored: spread the item's TaxAmount proportionally.
                    foreach (var r in rows)
                        r.TaxAmount = Math.Round(r.TaxRate * item.TaxAmount / sumRate, 2);
                }
                else
                {
                    // No TaxRate stored (sum = 0): split the item's TaxAmount evenly.
                    var each = Math.Round(item.TaxAmount / rows.Count, 2);
                    foreach (var r in rows)
                        r.TaxAmount = each;
                }

                foreach (var r in rows)
                {
                    r.Price = item.Price;
                    r.ExtendedPrice = item.ExtendedPrice;
                    r.Status = item.Status;
                }

                // Add the rounding remainder to the largest row so the rows add up to item.TaxAmount.
                var balance = item.TaxAmount - rows.Sum(x => x.TaxAmount);
                if (balance != 0)
                {
                    var target = rows.Where(x => x.TaxAmount != 0).OrderByDescending(x => x.TaxRate).FirstOrDefault()
                                 ?? rows[0];
                    target.TaxAmount += balance;
                }

                // One small script per row; unchanged rows are skipped.
                foreach (var r in rows)
                {
                    var old = oldValues[r.SaleReceiptDetailTaxDetailID];
                    if (r.TaxAmount == old.TaxAmount && r.Price == old.Price &&
                        r.ExtendedPrice == old.ExtendedPrice && r.Status == old.Status)
                        continue;

                    ProcessNonQuery(true,
                        $@"UPDATE dbo.SaleReceiptDetailTaxDetails SET TaxAmount = '{r.TaxAmount.ToString(inv)}', Price = '{r.Price.ToString(inv)}', ExtendedPrice = '{r.ExtendedPrice.ToString(inv)}', Status = '{r.Status}'
                           WHERE SaleReceiptDetailTaxDetailID = '{r.SaleReceiptDetailTaxDetailID}'");
                }
            }
            catch (Exception ex)
            {
                AppLogger.Error($"CorrectDataSaleReceiptDetailTaxDetailsByDetailID failed, detail: {saleReceiptDetailId}", ex);
            }
        }

        // Phân bổ service charge (Party / Area / Service+Extra / Auto) và thuế của chúng xuống
        // từng SaleReceiptPaymentDetails theo phần PaidServiceFee mà mỗi payment gánh — port từ
        // logic phân bổ trong Planet SaleReceiptHelperABS.SavePaymentDetail. Hỗ trợ nhiều payment
        // (chia tuần tự); bỏ qua PreAuth và các nhánh đặc thù (split-evenly / house account /
        // gift card / SVCFee-MerchantFee của service charge).
        public static void CorrectDataSaleReceiptPaymentDetails(string receiptId)
        {
            try
            {
                var sale = QueryFirstOrDefault<SaleReceiptsEntity>(true,
                    $@"SELECT * FROM dbo.SaleReceipts WHERE SaleReceiptID = '{receiptId}'");

                var payments = Query<SaleReceiptPaymentDetailsEntity>(true,
                    $@"SELECT * FROM dbo.SaleReceiptPaymentDetails WHERE SaleReceiptID = '{receiptId}'
                       ORDER BY CreatedDate, SaleReceiptPaymentDetailID");

                if (sale == null || payments == null || payments.Count == 0)
                    return;

                // Snapshot the columns this method writes, before the allocation below mutates
                // the entities, so each update group is only sent when its values really change.
                var oldValues = payments.ToDictionary(
                    x => x.SaleReceiptPaymentDetailID,
                    x => new
                    {
                        x.PaidServiceFee,
                        x.PartyServiceFeeAmount,
                        x.AreaFeeAmount,
                        x.ServiceFee,
                        x.AutoGratuityAmount,
                        x.PartyServiceFeeTaxAmount,
                        x.AreaFeeTaxAmount,
                        x.ServiceFeeTaxAmount,
                        x.AutoGratuityTaxAmount,
                        x.SVCFeeForServiceCharge,
                        x.SVCFeeForServiceFee,
                        x.SVCFeeForPartyServiceFeeAmount,
                        x.SVCFeeForAreaFeeAmount,
                        x.SVCFeeForAutoGratuityAmount,
                        x.MerchantFeeForServiceFee,
                        x.MerchantFeeForPartyServiceFeeAmount,
                        x.MerchantFeeForAreaFeeAmount,
                        x.MerchantFeeForAutoGratuityAmount
                    });

                // Breakdown thuế theo từng ServiceChargeTaxRate, dùng để tách phần thuế cho mỗi payment.
                var taxDetails = Query<SaleReceiptServiceChargeTaxDetailsEntity>(true,
                    $@"SELECT * FROM dbo.SaleReceiptServiceChargeTaxDetails WHERE SaleReceiptID = '{receiptId}'");

                decimal rate = sale.ServiceChargeTaxRate;
                bool isApplyTaxCustomFee = IsApplyTaxForSVCFee || sale.SVCFeeTaxAmount != 0;

                // Phần tiền hàng (không gồm service charge). Tương đương Planet:
                // taxitem = TaxAmount - ServiceChargeTaxAmount; tempAmount = TotalAmount + taxitem - TaxAmount.
                decimal amountExcludeServiceFee = (sale.TotalAmount - sale.ServiceChargeTaxAmount) - sale.SalesOrderFee;

                // Service gộp cả Extra Fee (payment không có cột Extra riêng).
                decimal serviceFeeNew = sale.ServiceFee + sale.ExtraFeeAmount;
                decimal serviceFeeTaxNew = sale.ServiceFeeTaxAmount + sale.ExtraFeeTaxAmount;

                // Tổng đã phân bổ, cộng dồn qua các payment.
                decimal totalTender = 0, totalPaidServiceFee = 0;
                decimal dParty = 0, dArea = 0, dService = 0, dAuto = 0;
                bool doneParty = false, doneArea = false, doneService = false, doneAuto = false;

                foreach (var p in payments)
                {
                    // Luôn xoá service charge cũ trên payment trước khi phân bổ lại.
                    p.PaidServiceFee = 0;
                    p.PartyServiceFeeAmount = 0; p.PartyServiceFeeTaxAmount = 0;
                    p.AreaFeeAmount = 0; p.AreaFeeTaxAmount = 0;
                    p.ServiceFee = 0; p.ServiceFeeTaxAmount = 0;
                    p.AutoGratuityAmount = 0; p.AutoGratuityTaxAmount = 0;
                    p.SVCFeeForServiceCharge = 0; p.SVCFeeForServiceFee = 0;
                    p.SVCFeeForPartyServiceFeeAmount = 0; p.SVCFeeForAreaFeeAmount = 0;
                    p.SVCFeeForAutoGratuityAmount = 0;

                    totalTender += p.AmountTendered - p.AmountReturn;

                    if (p.CreditCardTransactionType == "PreAuth")
                        continue;
                    if (amountExcludeServiceFee > totalTender)
                        continue;

                    decimal tempAmountTotal = sale.TotalAmount > totalTender ? totalTender : sale.TotalAmount;

                    p.PaidServiceFee = tempAmountTotal - amountExcludeServiceFee - totalPaidServiceFee;
                    totalPaidServiceFee += p.PaidServiceFee;

                    decimal cur = p.PaidServiceFee;

                    // Party
                    if (sale.PartyServiceFeeAmount > 0 && sale.PartyServiceFeeAmount + sale.PartyServiceFeeTaxAmount > dParty && cur > 0)
                    {
                        decimal need = RoundUp(sale.PartyServiceFeeAmount - dParty);
                        decimal take = cur > need ? need : cur;
                        p.PartyServiceFeeAmount = take; dParty += take; cur -= take;
                        if (!doneParty && cur > 0)
                        {
                            decimal tt = cur > sale.PartyServiceFeeTaxAmount ? sale.PartyServiceFeeTaxAmount : cur;
                            p.PartyServiceFeeAmount += tt; dParty += tt; cur -= tt;
                            if (p.PartyServiceFeeAmount >= sale.PartyServiceFeeAmount + sale.PartyServiceFeeTaxAmount) doneParty = true;
                        }
                        p.PartyServiceFeeTaxAmount = CalculateTaxServiceCharge(p.PartyServiceFeeAmount, rate, sale.PartyServiceFeeTaxAmount, taxDetails);
                    }

                    // Area
                    if (sale.AreaFeeAmount > 0 && sale.AreaFeeAmount + sale.AreaFeeTaxAmount > dArea && cur > 0)
                    {
                        decimal need = RoundUp(sale.AreaFeeAmount - dArea);
                        decimal take = cur > need ? need : cur;
                        p.AreaFeeAmount = take; dArea += take; cur -= take;
                        if (!doneArea && cur > 0)
                        {
                            decimal tt = cur > sale.AreaFeeTaxAmount ? sale.AreaFeeTaxAmount : cur;
                            p.AreaFeeAmount += tt; dArea += tt; cur -= tt;
                            if (p.AreaFeeAmount >= sale.AreaFeeAmount + sale.AreaFeeTaxAmount) doneArea = true;
                        }
                        p.AreaFeeTaxAmount = CalculateTaxServiceCharge(p.AreaFeeAmount, rate, sale.AreaFeeTaxAmount, taxDetails);
                    }

                    // Service (gộp Extra Fee)
                    if (serviceFeeNew > 0 && serviceFeeNew + serviceFeeTaxNew > dService && cur > 0)
                    {
                        decimal need = RoundUp(serviceFeeNew - dService);
                        decimal take = cur > need ? need : cur;
                        p.ServiceFee = take; dService += take; cur -= take;
                        if (!doneService && cur > 0)
                        {
                            decimal tt = cur > serviceFeeTaxNew ? serviceFeeTaxNew : cur;
                            p.ServiceFee += tt; dService += tt; cur -= tt;
                            if (p.ServiceFee >= serviceFeeNew + serviceFeeTaxNew) doneService = true;
                        }
                        p.ServiceFeeTaxAmount = CalculateTaxServiceCharge(p.ServiceFee, rate, serviceFeeTaxNew, taxDetails);
                    }

                    // Auto
                    if (sale.AutoGratuityAmount > 0 && sale.AutoGratuityAmount + sale.AutoGratuityTaxAmount > dAuto && cur > 0)
                    {
                        decimal need = RoundUp(sale.AutoGratuityAmount - dAuto);
                        decimal take = cur > need ? need : cur;
                        p.AutoGratuityAmount = take; dAuto += take; cur -= take;
                        if (!doneAuto && cur > 0)
                        {
                            decimal tt = cur > sale.AutoGratuityTaxAmount ? sale.AutoGratuityTaxAmount : cur;
                            p.AutoGratuityAmount += tt; dAuto += tt; cur -= tt;
                            if (p.AutoGratuityAmount >= sale.AutoGratuityAmount + sale.AutoGratuityTaxAmount) doneAuto = true;
                        }
                        p.AutoGratuityTaxAmount = CalculateTaxServiceCharge(p.AutoGratuityAmount, rate, sale.AutoGratuityTaxAmount, taxDetails);
                    }

                    // SVC fee (custom fee) tính trên từng phần service charge của payment.
                    CalculateSVCFeeForServiceCharge(p, sale, isApplyTaxCustomFee);
                }

                var inv = CultureInfo.InvariantCulture;
                foreach (var p in payments)
                {
                    var old = oldValues[p.SaleReceiptPaymentDetailID];

                    // Service charge amounts carried by this payment.
                    if (p.PaidServiceFee != old.PaidServiceFee
                        || p.PartyServiceFeeAmount != old.PartyServiceFeeAmount
                        || p.AreaFeeAmount != old.AreaFeeAmount
                        || p.ServiceFee != old.ServiceFee
                        || p.AutoGratuityAmount != old.AutoGratuityAmount)
                    {
                        string upd = $@"
                            UPDATE dbo.SaleReceiptPaymentDetails SET
                                PaidServiceFee           = {p.PaidServiceFee.ToString(inv)},
                                PartyServiceFeeAmount    = {p.PartyServiceFeeAmount.ToString(inv)},
                                AreaFeeAmount            = {p.AreaFeeAmount.ToString(inv)},
                                ServiceFee               = {p.ServiceFee.ToString(inv)},
                                AutoGratuityAmount       = {p.AutoGratuityAmount.ToString(inv)}
                            WHERE SaleReceiptPaymentDetailID = {p.SaleReceiptPaymentDetailID}";
                        ProcessNonQuery(true, upd);
                    }

                    // Tax of each service charge part.
                    if (p.PartyServiceFeeTaxAmount != old.PartyServiceFeeTaxAmount
                        || p.AreaFeeTaxAmount != old.AreaFeeTaxAmount
                        || p.ServiceFeeTaxAmount != old.ServiceFeeTaxAmount
                        || p.AutoGratuityTaxAmount != old.AutoGratuityTaxAmount)
                    {
                        string upd = $@"
                            UPDATE dbo.SaleReceiptPaymentDetails SET
                                PartyServiceFeeTaxAmount = {p.PartyServiceFeeTaxAmount.ToString(inv)},
                                AreaFeeTaxAmount         = {p.AreaFeeTaxAmount.ToString(inv)},
                                ServiceFeeTaxAmount      = {p.ServiceFeeTaxAmount.ToString(inv)},
                                AutoGratuityTaxAmount    = {p.AutoGratuityTaxAmount.ToString(inv)}
                            WHERE SaleReceiptPaymentDetailID = {p.SaleReceiptPaymentDetailID}";
                        ProcessNonQuery(true, upd);
                    }

                    // SVC fee (custom fee) per service charge part.
                    if (p.SVCFeeForServiceCharge != old.SVCFeeForServiceCharge
                        || p.SVCFeeForServiceFee != old.SVCFeeForServiceFee
                        || p.SVCFeeForPartyServiceFeeAmount != old.SVCFeeForPartyServiceFeeAmount
                        || p.SVCFeeForAreaFeeAmount != old.SVCFeeForAreaFeeAmount
                        || p.SVCFeeForAutoGratuityAmount != old.SVCFeeForAutoGratuityAmount)
                    {
                        string upd = $@"
                            UPDATE dbo.SaleReceiptPaymentDetails SET
                                SVCFeeForServiceCharge           = {p.SVCFeeForServiceCharge.ToString(inv)},
                                SVCFeeForServiceFee              = {p.SVCFeeForServiceFee.ToString(inv)},
                                SVCFeeForPartyServiceFeeAmount   = {p.SVCFeeForPartyServiceFeeAmount.ToString(inv)},
                                SVCFeeForAreaFeeAmount           = {p.SVCFeeForAreaFeeAmount.ToString(inv)},
                                SVCFeeForAutoGratuityAmount      = {p.SVCFeeForAutoGratuityAmount.ToString(inv)}
                            WHERE SaleReceiptPaymentDetailID = {p.SaleReceiptPaymentDetailID}";
                        ProcessNonQuery(true, upd);
                    }

                    // Merchant fee per service charge part.
                    if (p.MerchantFeeForServiceFee != old.MerchantFeeForServiceFee
                        || p.MerchantFeeForPartyServiceFeeAmount != old.MerchantFeeForPartyServiceFeeAmount
                        || p.MerchantFeeForAreaFeeAmount != old.MerchantFeeForAreaFeeAmount
                        || p.MerchantFeeForAutoGratuityAmount != old.MerchantFeeForAutoGratuityAmount)
                    {
                        string upd = $@"
                            UPDATE dbo.SaleReceiptPaymentDetails SET
                                MerchantFeeForServiceFee              = {p.MerchantFeeForServiceFee.ToString(inv)},
                                MerchantFeeForPartyServiceFeeAmount   = {p.MerchantFeeForPartyServiceFeeAmount.ToString(inv)},
                                MerchantFeeForAreaFeeAmount           = {p.MerchantFeeForAreaFeeAmount.ToString(inv)},
                                MerchantFeeForAutoGratuityAmount      = {p.MerchantFeeForAutoGratuityAmount.ToString(inv)}
                            WHERE SaleReceiptPaymentDetailID = {p.SaleReceiptPaymentDetailID}";
                        ProcessNonQuery(true, upd);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private static void CorrectDataPaymentRoleDetails(string receiptId)
        {
            try
            {
                List<string> listScript = new List<string>();

                var sale = QueryFirstOrDefault<SaleReceiptsEntity>(true,
                    $@"SELECT * FROM dbo.SaleReceipts WHERE SaleReceiptID = '{receiptId}'");

                var payments = Query<SaleReceiptPaymentDetailsEntity>(true,
                    $@"SELECT a.*, b.PaymentMethodType FROM SaleReceiptPaymentDetails a left join PaymentMethods b on a.PaymentMethodID = b.PaymentMethodID WHERE SaleReceiptID = '{receiptId}'");

                var paymentRoleDetails = Query<PaymentRoleDetailsEntity>(true,
                    $@"SELECT * FROM dbo.PaymentRoleDetails WHERE SaleReceiptID = '{receiptId}'");

                var tables = Query<QueryModelEntity>(true, $@"SELECT t.name AS TableName FROM sys.tables t ORDER BY t.name;");

                if (sale == null || tables.Exists(x => x.TableName == "PaymentRoleDetails") == false || payments == null || payments.Count == 0)
                {
                    return;
                }

                var employeeClockInInfoEntity = GetEmployeeClockInInfoEntity(sale.CashierID, sale.DateCreated);

                foreach (var pay in payments)
                {
                    var paymentRole = paymentRoleDetails.FirstOrDefault(x => x.SaleReceiptPaymentDetailID == pay.SaleReceiptPaymentDetailID);

                    string sql = string.Empty;

                    if (paymentRole != null)
                    {
                        if (paymentRole.Gratuity != pay.Gratuity)
                            listScript.Add($"update PaymentRoleDetails set Gratuity = '{pay.Gratuity}' where ID = '{paymentRole.ID}';");

                        if (paymentRole.SVCFeeForGratuity != pay.SVCFeeForGratuity)
                            listScript.Add($"update PaymentRoleDetails set SVCFeeForGratuity = '{pay.SVCFeeForGratuity}' where ID = '{paymentRole.ID}';");

                        if (paymentRole.MerchantFeeForTip != pay.MerchantFeeForTip)
                            listScript.Add($"update PaymentRoleDetails set MerchantFeeForTip = '{pay.MerchantFeeForTip}' where ID = '{paymentRole.ID}';");

                        if (paymentRole.PartyServiceFeeAmount != pay.PartyServiceFeeAmount)
                            listScript.Add($"update PaymentRoleDetails set PartyServiceFeeAmount = '{pay.PartyServiceFeeAmount}' where ID = '{paymentRole.ID}';");

                        if (paymentRole.PartyServiceFeeTaxAmount != pay.PartyServiceFeeTaxAmount)
                            listScript.Add($"update PaymentRoleDetails set PartyServiceFeeTaxAmount = '{pay.PartyServiceFeeTaxAmount}' where ID = '{paymentRole.ID}';");

                        if (paymentRole.SVCFeeForPartyServiceFeeAmount != pay.SVCFeeForPartyServiceFeeAmount)
                            listScript.Add($"update PaymentRoleDetails set SVCFeeForPartyServiceFeeAmount = '{pay.SVCFeeForPartyServiceFeeAmount}' where ID = '{paymentRole.ID}';");

                        if (paymentRole.MerchantFeeForPartyServiceFeeAmount != pay.MerchantFeeForPartyServiceFeeAmount)
                            listScript.Add($"update PaymentRoleDetails set MerchantFeeForPartyServiceFeeAmount = '{pay.MerchantFeeForPartyServiceFeeAmount}' where ID = '{paymentRole.ID}';");

                        if (paymentRole.AutoGratuityAmount != pay.AutoGratuityAmount)
                            listScript.Add($"update PaymentRoleDetails set AutoGratuityAmount = '{pay.AutoGratuityAmount}' where ID = '{paymentRole.ID}';");

                        if (paymentRole.AutoGratuityTaxAmount != pay.AutoGratuityTaxAmount)
                            listScript.Add($"update PaymentRoleDetails set AutoGratuityTaxAmount = '{pay.AutoGratuityTaxAmount}' where ID = '{paymentRole.ID}';");

                        if (paymentRole.SVCFeeForAutoGratuityAmount != pay.SVCFeeForAutoGratuityAmount)
                            listScript.Add($"update PaymentRoleDetails set SVCFeeForAutoGratuityAmount = '{pay.SVCFeeForAutoGratuityAmount}' where ID = '{paymentRole.ID}';");

                        if (paymentRole.MerchantFeeForAutoGratuityAmount != pay.MerchantFeeForAutoGratuityAmount)
                            listScript.Add($"update PaymentRoleDetails set MerchantFeeForAutoGratuityAmount = '{pay.MerchantFeeForAutoGratuityAmount}' where ID = '{paymentRole.ID}';");

                        if (paymentRole.AreaFeeAmount != pay.AreaFeeAmount)
                            listScript.Add($"update PaymentRoleDetails set AreaFeeAmount = '{pay.AreaFeeAmount}' where ID = '{paymentRole.ID}';");

                        if (paymentRole.AreaFeeTaxAmount != pay.AreaFeeTaxAmount)
                            listScript.Add($"update PaymentRoleDetails set AreaFeeTaxAmount = '{pay.AreaFeeTaxAmount}' where ID = '{paymentRole.ID}';");

                        if (paymentRole.SVCFeeForAreaFeeAmount != pay.SVCFeeForAreaFeeAmount)
                            listScript.Add($"update PaymentRoleDetails set SVCFeeForAreaFeeAmount = '{pay.SVCFeeForAreaFeeAmount}' where ID = '{paymentRole.ID}';");

                        if (paymentRole.MerchantFeeForAreaFeeAmount != pay.MerchantFeeForAreaFeeAmount)
                            listScript.Add($"update PaymentRoleDetails set MerchantFeeForAreaFeeAmount = '{pay.MerchantFeeForAreaFeeAmount}' where ID = '{paymentRole.ID}';");

                        if (paymentRole.ServiceFee != pay.ServiceFee)
                            listScript.Add($"update PaymentRoleDetails set ServiceFee = '{pay.ServiceFee}' where ID = '{paymentRole.ID}';");

                        if (paymentRole.ServiceFeeTaxAmount != pay.ServiceFeeTaxAmount)
                            listScript.Add($"update PaymentRoleDetails set ServiceFeeTaxAmount = '{pay.ServiceFeeTaxAmount}' where ID = '{paymentRole.ID}';");

                        if (paymentRole.SVCFeeForServiceFee != pay.SVCFeeForServiceFee)
                            listScript.Add($"update PaymentRoleDetails set SVCFeeForServiceFee = '{pay.SVCFeeForServiceFee}' where ID = '{paymentRole.ID}';");

                        if (paymentRole.MerchantFeeForServiceFee != pay.MerchantFeeForServiceFee)
                            listScript.Add($"update PaymentRoleDetails set MerchantFeeForServiceFee = '{pay.MerchantFeeForServiceFee}' where ID = '{paymentRole.ID}';");
                    }
                    else
                    {
                        sql = $"    INSERT INTO PaymentRoleDetails (ID,SaleReceiptID,SaleReceiptPaymentDetailID,PaymentMethodID,PaymentMethodType,EmployeeID,RolesID,TipRate,TipRateOT,Gratuity,SVCFeeForGratuity,MerchantFeeForTip,DateCreated,PartyServiceFeeAmount,PartyServiceFeeTaxAmount,SVCFeeForPartyServiceFeeAmount,MerchantFeeForPartyServiceFeeAmount,AutoGratuityAmount,AutoGratuityTaxAmount,SVCFeeForAutoGratuityAmount,MerchantFeeForAutoGratuityAmount,AreaFeeAmount,AreaFeeTaxAmount,SVCFeeForAreaFeeAmount,MerchantFeeForAreaFeeAmount,ServiceFee,ServiceFeeTaxAmount,SVCFeeForServiceFee,MerchantFeeForServiceFee)";
                        sql += $"    VALUES (NEWID(), '{pay.SaleReceiptID}', '{pay.SaleReceiptPaymentDetailID}', '{pay.PaymentMethodID}', '{pay.PaymentMethodType}', '{sale.CashierID}', '{employeeClockInInfoEntity.RolesID}', '{employeeClockInInfoEntity.TipRate}', '{employeeClockInInfoEntity.TipRateOT}', '{pay.Gratuity}', '{pay.SVCFeeForGratuity}', '{pay.MerchantFeeForTip}', '{pay.CreatedDate}', '{pay.PartyServiceFeeAmount}', '{pay.PartyServiceFeeTaxAmount}', '{pay.SVCFeeForPartyServiceFeeAmount}', '{pay.MerchantFeeForPartyServiceFeeAmount}', '{pay.AutoGratuityAmount}', '{pay.AutoGratuityTaxAmount}', '{pay.SVCFeeForAutoGratuityAmount}', '{pay.MerchantFeeForAutoGratuityAmount}', '{pay.AreaFeeAmount}', '{pay.AreaFeeTaxAmount}', '{pay.SVCFeeForAreaFeeAmount}', '{pay.MerchantFeeForAreaFeeAmount}', '{pay.ServiceFee}', '{pay.ServiceFeeTaxAmount}', '{pay.SVCFeeForServiceFee}', '{pay.MerchantFeeForServiceFee}'  )";

                        listScript.Add(sql);
                    }

                    foreach (var script in listScript)
                    {
                        ProcessNonQuery(true, script);
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private static void CorrectDataTransactionDetail(string receiptId)
        {
            try
            {
                string sql = string.Empty;

                var payments = Query<SaleReceiptPaymentDetailsEntity>(true,
                    $@"SELECT * FROM SaleReceiptPaymentDetails WHERE SaleReceiptID = '{receiptId}'");

                if (payments?.Count > 0)
                {
                    foreach (var item in payments)
                    {
                        if (!string.IsNullOrWhiteSpace(item.CreditCardTransactionID))
                        {
                            var inv = CultureInfo.InvariantCulture;

                            // Đọc dữ liệu hiện tại trước để so sánh cũ/mới, chỉ update cột thực sự đổi.
                            sql = $"select * from TransactionDetail where ID = '{item.CreditCardTransactionID}'";
                            var transactionDetail = QueryFirstOrDefault<TransactionDetailEntity>(false, sql);
                            if (transactionDetail == null)
                                continue;

                            var newAmount = item.AmountTendered - item.AmountReturn + item.SVCFeeAmount;
                            var newGratuity = item.Gratuity;
                            var newTotal = newAmount + item.Gratuity;

                            // Tách từng cột: chỉ đưa vào UPDATE cột nào khác giá trị cũ.
                            var sets = new List<string>();
                            if (transactionDetail.Amount != newAmount)
                                sets.Add($"Amount = '{newAmount.ToString(inv)}'");
                            if (transactionDetail.Gratuity != newGratuity)
                            {
                                sets.Add($"Gratuity = '{newGratuity.ToString(inv)}'");
                                sets.Add($"OriginalTip = '{newGratuity.ToString(inv)}'");

                            }
                            if (transactionDetail.Total != newTotal)
                            {
                                sets.Add($"Total = '{newTotal.ToString(inv)}'");
                                sets.Add($"AuthorizeAmount = '{newTotal.ToString(inv)}'");
                                sets.Add($"OriginalAmount = '{newTotal.ToString(inv)}'");
                            }
                            if (transactionDetail.Total != newTotal)
                                sets.Add($"SVCFee = '{item.SVCFeeAmount.ToString(inv)}'");
                            if (transactionDetail.Total != newTotal)
                                sets.Add($"SVCFeeTaxAmount = '{item.SVCFeeTaxAmount.ToString(inv)}'");
                            if (transactionDetail.Total != newTotal)
                                sets.Add($"SVCFeeForGratuity = '{item.SVCFeeForGratuity.ToString(inv)}'");
                            if (transactionDetail.Total != newTotal)
                                sets.Add($"MerchantFee = '{item.MerchantFee.ToString(inv)}'");
                            if (transactionDetail.Total != newTotal)
                                sets.Add($"MerchantFeeForTip = '{item.MerchantFeeForTip.ToString(inv)}'");

                            if (sets.Count > 0)
                            {
                                sql = $"UPDATE TransactionDetail SET {string.Join(", ", sets)} WHERE ID = '{item.CreditCardTransactionID}';";
                                ProcessNonQuery(false, sql);
                            }

                            if (sets.Count > 0 && !string.IsNullOrWhiteSpace(transactionDetail.BatchUnique))
                            {
                                var safe = transactionDetail?.BatchUnique.Replace("'", "''");

                                var countDt = ProcessQuery(false,
                                    $"SELECT COUNT(ID) AS Cnt FROM dbo.TransactionDetail WHERE BatchUnique = '{safe}'", "Cnt");
                                int cnt = countDt?.Rows.Count > 0 ? GetInt(countDt.Rows[0], "Cnt") : 0;

                                if (cnt == 0)
                                {
                                    ProcessNonQuery(false, $"DELETE FROM dbo.Batch WHERE BatchUnique = '{safe}'");
                                    return;
                                }

                                string aggSql = $@"
                                        SELECT
                                            SUM(CASE WHEN (TranType = 'Sale' OR TranType = 'PreAuthCapture') AND TranStatus = 'ACTIVE' THEN Total ELSE 0 END)
                                              - SUM(CASE WHEN TranType = 'Return' THEN Total ELSE 0 END) AS Total,
                                            SUM(CASE WHEN (TranType = 'Sale' OR TranType = 'PreAuthCapture') AND TranStatus = 'ACTIVE' THEN SVCFeeForGratuity ELSE 0 END) AS TotalSVCFeeForTip,
                                            SUM(CASE WHEN (TranType = 'Sale' OR TranType = 'PreAuthCapture') AND TranStatus = 'ACTIVE' THEN Amount - SVCFee ELSE 0 END) AS TotalSales,
                                            SUM(CASE WHEN (TranType = 'Sale' OR TranType = 'PreAuthCapture') AND TranStatus = 'ACTIVE' THEN Gratuity ELSE 0 END) AS TotalTips,
                                            SUM(CASE WHEN (TranType = 'Sale' OR TranType = 'PreAuthCapture') AND TranStatus = 'ACTIVE' THEN SVCFee ELSE 0 END)
                                              - SUM(CASE WHEN TranType = 'Return' THEN SVCFee ELSE 0 END) AS TotalSVCFeeAmount,
                                            SUM(CASE WHEN (TranType = 'Sale' OR TranType = 'PreAuthCapture') AND TranStatus = 'ACTIVE' THEN Total - MerchantFee ELSE 0 END)
                                              - SUM(CASE WHEN TranType = 'Return' THEN Total ELSE 0 END) AS DepositAmount,
                                            SUM(CASE WHEN (TranType = 'Sale' OR TranType = 'PreAuthCapture') AND TranStatus = 'ACTIVE' THEN MerchantFee ELSE 0 END) AS MerchantFeeAmount,
                                            COUNT(ID) AS TicketCount
                                        FROM dbo.TransactionDetail
                                        WHERE BatchUnique = '{safe}'";

                                sql = $@"
                                        SELECT
                                            SUM(CASE WHEN (TranType = 'Sale' OR TranType = 'PreAuthCapture') AND TranStatus = 'ACTIVE' THEN Total ELSE 0 END)
                                              - SUM(CASE WHEN TranType = 'Return' THEN Total ELSE 0 END) AS Total
                                        FROM dbo.TransactionDetail
                                        WHERE BatchUnique = '{safe}'";

                                object total = ProcessQuerySingleValue(false, sql);

                                sql = $@"
                                        SELECT SUM(CASE WHEN (TranType = 'Sale' OR TranType = 'PreAuthCapture') AND TranStatus = 'ACTIVE' THEN SVCFeeForGratuity ELSE 0 END) AS TotalSVCFeeForTip
                                        FROM dbo.TransactionDetail
                                        WHERE BatchUnique = '{safe}'";

                                object totalSvcTip = ProcessQuerySingleValue(false, sql);

                                sql = $@"SELECT SUM(CASE WHEN (TranType = 'Sale' OR TranType = 'PreAuthCapture') AND TranStatus = 'ACTIVE' THEN Amount - SVCFee ELSE 0 END) AS TotalSales
                                        FROM dbo.TransactionDetail
                                        WHERE BatchUnique = '{safe}'";

                                object totalSales = ProcessQuerySingleValue(false, sql);

                                sql = $@"SELECT SUM(CASE WHEN (TranType = 'Sale' OR TranType = 'PreAuthCapture') AND TranStatus = 'ACTIVE' THEN Gratuity ELSE 0 END) AS TotalTips
                                        FROM dbo.TransactionDetail
                                        WHERE BatchUnique = '{safe}'";

                                object totalTips = ProcessQuerySingleValue(false, sql);

                                sql = $@"SELECT SUM(CASE WHEN (TranType = 'Sale' OR TranType = 'PreAuthCapture') AND TranStatus = 'ACTIVE' THEN SVCFee ELSE 0 END)
                                              - SUM(CASE WHEN TranType = 'Return' THEN SVCFee ELSE 0 END) AS TotalSVCFeeAmount
                                        FROM dbo.TransactionDetail
                                        WHERE BatchUnique = '{safe}'";

                                object totalSvc = ProcessQuerySingleValue(false, sql);

                                sql = $@"SELECT SUM(CASE WHEN (TranType = 'Sale' OR TranType = 'PreAuthCapture') AND TranStatus = 'ACTIVE' THEN Total - MerchantFee ELSE 0 END)
                                              - SUM(CASE WHEN TranType = 'Return' THEN Total ELSE 0 END) AS DepositAmount
                                        FROM dbo.TransactionDetail
                                        WHERE BatchUnique = '{safe}'";

                                object deposit = ProcessQuerySingleValue(false, sql);

                                sql = $@"SELECT SUM(CASE WHEN (TranType = 'Sale' OR TranType = 'PreAuthCapture') AND TranStatus = 'ACTIVE' THEN MerchantFee ELSE 0 END) AS MerchantFeeAmount
                                        FROM dbo.TransactionDetail
                                        WHERE BatchUnique = '{safe}'";

                                object merchantFee = ProcessQuerySingleValue(false, sql);

                                sql = $@"SELECT COUNT(ID) AS TicketCount
                                        FROM dbo.TransactionDetail
                                        WHERE BatchUnique = '{safe}'";

                                object ticketCount = ProcessQuerySingleValue(false, sql);

                                //var agg = ProcessQuery(false, aggSql, "Batch");
                                //if (agg == null || agg.Rows.Count == 0) return;

                                //var r = agg.Rows[0];

                                ProcessNonQuery(false, $@"
                                        UPDATE dbo.Batch SET
                                            Total = {total},
                                            TotalSVCFeeForTip = {totalSvcTip},
                                            TotalSales = {totalSales},
                                            TotalTips = {totalTips},
                                            TotalSVCFeeAmount = {totalSvc},
                                            DepositAmount = {deposit},
                                            MerchantFeeAmount = {merchantFee},
                                            Quantity = {ticketCount}
                                        WHERE BatchUnique = '{safe}'");

                                ProcessNonQuery(false, $@"UPDATE dbo.Batch SET Total = {total} WHERE BatchUnique = '{safe}'");
                                ProcessNonQuery(false, $@"UPDATE dbo.Batch SET TotalSVCFeeForTip = {totalSvcTip} WHERE BatchUnique = '{safe}'");
                                ProcessNonQuery(false, $@"UPDATE dbo.Batch SET TotalSales = {totalSales} WHERE BatchUnique = '{safe}'");
                                ProcessNonQuery(false, $@"UPDATE dbo.Batch SET TotalTips = {totalTips} WHERE BatchUnique = '{safe}'");
                                ProcessNonQuery(false, $@"UPDATE dbo.Batch SET Total = {total} WHERE BatchUnique = '{safe}'");
                                ProcessNonQuery(false, $@"UPDATE dbo.Batch SET Total = {total} WHERE BatchUnique = '{safe}'");
                                ProcessNonQuery(false, $@"UPDATE dbo.Batch SET Total = {total} WHERE BatchUnique = '{safe}'");
                                ProcessNonQuery(false, $@"UPDATE dbo.Batch SET Total = {total} WHERE BatchUnique = '{safe}'");
                                ProcessNonQuery(false, $@"UPDATE dbo.Batch SET Total = {total} WHERE BatchUnique = '{safe}'");

                                ProcessNonQuery(false,
                                    $"UPDATE dbo.TransactionDetail SET BatchTotal = {total} WHERE BatchUnique = '{safe}'");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        // Tách phần thuế từ số tiền đã gồm thuế (amount), theo từng ServiceChargeTaxRate trong
        // SaleReceiptServiceChargeTaxDetails nếu có; ngược lại dùng percent tổng. Port từ
        // Planet Helper.CalculateTaxServiceCharge.
        private static decimal CalculateTaxServiceCharge(decimal amount, decimal percent,
            decimal totalTaxByServiceCharge, List<SaleReceiptServiceChargeTaxDetailsEntity> taxDetails)
        {
            if (amount == 0 || totalTaxByServiceCharge == 0)
                return 0;

            if (taxDetails == null || taxDetails.Count == 0)
            {
                if (percent == 0)
                    return 0;
                decimal baseAmount = amount / (1 + percent / 100);
                return RoundUp(baseAmount * percent / 100);
            }

            decimal tax = 0;
            foreach (var d in taxDetails)
            {
                if (d.ServiceChargeTaxRate == 0)
                    continue;
                decimal temp = amount * d.ServiceChargeTaxRate / 100;
                tax += RoundUp(temp / (1 + percent / 100));
            }
            return tax;
        }

        // Tính SVC fee (custom fee) trên từng phần service charge của một payment — port từ
        // Planet SaleReceiptHelperABS.CalculateCustomFeeForServiceCharge (chỉ phần SVCFeeForXxx).
        private static void CalculateSVCFeeForServiceCharge(SaleReceiptPaymentDetailsEntity p, SaleReceiptsEntity sale, bool isApplyTaxCustomFee)
        {
            // Chỉ áp cho credit card / cash-adjust hoặc khi có cấu hình SVC fee (không phải cash discount).
            bool applies = p.PaymentMethodID == 2 || p.CashAdjust == 2
                || (SVCFee > 0 && CashDiscount == 0 && p.PaymentMethodID != -1 && p.PaymentMethodID != -2 && p.PaymentMethodID != -3);
            if (!applies)
                return;

            // Payment không gánh service charge hoặc không có SVC fee thì để 0.
            if (p.PaidServiceFee == 0 || p.SVCFeeAmount == 0
                || (p.PaymentMethodID == 3 && ApplyCustomFeeWhenPurchaseGiftCard != "1"))
                return;

            if (isApplyTaxCustomFee)
            {
                decimal taxsc = p.AutoGratuityTaxAmount + p.AreaFeeTaxAmount + p.PartyServiceFeeTaxAmount + p.ServiceFeeTaxAmount;
                p.SVCFeeForAutoGratuityAmount = CalculateSVCFee(p.AutoGratuityAmount - p.AutoGratuityTaxAmount, true, sale.SVCFeeTaxRate, p.SVCFeeTaxAmount);
                p.SVCFeeForAreaFeeAmount = CalculateSVCFee(p.AreaFeeAmount - p.AreaFeeTaxAmount, true, sale.SVCFeeTaxRate, p.SVCFeeTaxAmount);
                p.SVCFeeForPartyServiceFeeAmount = CalculateSVCFee(p.PartyServiceFeeAmount - p.PartyServiceFeeTaxAmount, true, sale.SVCFeeTaxRate, p.SVCFeeTaxAmount);
                p.SVCFeeForServiceFee = CalculateSVCFee(p.ServiceFee - p.ServiceFeeTaxAmount, true, sale.SVCFeeTaxRate, p.SVCFeeTaxAmount);
                p.SVCFeeForServiceCharge = CalculateSVCFee(p.PaidServiceFee - taxsc, true, sale.SVCFeeTaxRate, p.SVCFeeTaxAmount);
            }
            else
            {
                p.SVCFeeForServiceCharge = CalculateSVCFee(p.PaidServiceFee, false, 0, 0);
                p.SVCFeeForServiceFee = CalculateSVCFee(p.ServiceFee, false, 0, 0);
                p.SVCFeeForPartyServiceFeeAmount = CalculateSVCFee(p.PartyServiceFeeAmount, false, 0, 0);
                p.SVCFeeForAreaFeeAmount = CalculateSVCFee(p.AreaFeeAmount, false, 0, 0);
                p.SVCFeeForAutoGratuityAmount = CalculateSVCFee(p.AutoGratuityAmount, false, 0, 0);
            }

            // Bù chênh lệch để tổng các phần khớp đúng SVCFeeForServiceCharge (port phần Compare).
            decimal tempTotal = p.SVCFeeForServiceFee + p.SVCFeeForPartyServiceFeeAmount
                + p.SVCFeeForAreaFeeAmount + p.SVCFeeForAutoGratuityAmount;
            decimal diff = tempTotal - p.SVCFeeForServiceCharge;
            if (diff != 0)
            {
                if (p.SVCFeeForPartyServiceFeeAmount > 0) p.SVCFeeForPartyServiceFeeAmount -= diff;
                else if (p.SVCFeeForAreaFeeAmount > 0) p.SVCFeeForAreaFeeAmount -= diff;
                else if (p.SVCFeeForServiceFee > 0) p.SVCFeeForServiceFee -= diff;
                else if (p.SVCFeeForAutoGratuityAmount > 0) p.SVCFeeForAutoGratuityAmount -= diff;
            }

            decimal tempMerchantFeeForServiceCharge = 0;
            if (isApplyTaxCustomFee)
            {
                decimal taxsc = p.AutoGratuityTaxAmount + p.AreaFeeTaxAmount + p.PartyServiceFeeTaxAmount + p.ServiceFeeTaxAmount;
                tempMerchantFeeForServiceCharge = CalculateMerchantFee((p.PaidServiceFee - taxsc) + p.SVCFeeForServiceCharge, p.MerchantFeePercent);

                p.MerchantFeeForAutoGratuityAmount = CalculateMerchantFee(p.AutoGratuityAmount - p.AutoGratuityTaxAmount + p.SVCFeeForAutoGratuityAmount, p.MerchantFeePercent);
                p.MerchantFeeForAreaFeeAmount = CalculateMerchantFee(p.AreaFeeAmount - p.AreaFeeTaxAmount + p.SVCFeeForAreaFeeAmount, p.MerchantFeePercent);
                p.MerchantFeeForPartyServiceFeeAmount = CalculateMerchantFee(p.PartyServiceFeeAmount - p.PartyServiceFeeTaxAmount + p.SVCFeeForPartyServiceFeeAmount, p.MerchantFeePercent);
                p.MerchantFeeForServiceFee = CalculateMerchantFee(p.ServiceFee - p.ServiceFeeTaxAmount + p.SVCFeeForServiceFee, p.MerchantFeePercent);
            }
            else
            {
                tempMerchantFeeForServiceCharge = CalculateMerchantFee(p.PaidServiceFee + p.SVCFeeForServiceCharge, p.MerchantFeePercent);
                p.MerchantFeeForAutoGratuityAmount = CalculateMerchantFee(p.AutoGratuityAmount + p.SVCFeeForAutoGratuityAmount, p.MerchantFeePercent);
                p.MerchantFeeForAreaFeeAmount = CalculateMerchantFee(p.AreaFeeAmount + p.SVCFeeForAreaFeeAmount, p.MerchantFeePercent);
                p.MerchantFeeForPartyServiceFeeAmount = CalculateMerchantFee(p.PartyServiceFeeAmount + p.SVCFeeForPartyServiceFeeAmount, p.MerchantFeePercent);
                p.MerchantFeeForServiceFee = CalculateMerchantFee(p.ServiceFee + p.SVCFeeForServiceFee, p.MerchantFeePercent);
            }

            tempTotal = p.MerchantFeeForAutoGratuityAmount + p.MerchantFeeForAreaFeeAmount
               + p.MerchantFeeForPartyServiceFeeAmount + p.MerchantFeeForServiceFee;
            diff = tempTotal - p.SVCFeeForServiceCharge;
            if (diff != 0)
            {
                if (p.SVCFeeForPartyServiceFeeAmount > 0) p.SVCFeeForPartyServiceFeeAmount -= diff;
                else if (p.SVCFeeForAreaFeeAmount > 0) p.SVCFeeForAreaFeeAmount -= diff;
                else if (p.SVCFeeForServiceFee > 0) p.SVCFeeForServiceFee -= diff;
                else if (p.SVCFeeForAutoGratuityAmount > 0) p.SVCFeeForAutoGratuityAmount -= diff;
            }
        }

        // Tính SVC fee cho một khoản (percent mode). Port từ Planet CalculateSVCFee: chỉ áp dụng khi
        // SVCFeeType == 1; nếu apply-tax-custom-fee và TaxIncludeInSVCFee == 1 thì trừ phần thuế.
        private static decimal CalculateSVCFee(decimal amount, bool isApplyTaxCustomFee, decimal svcFeeTaxRate, decimal svcFeeTaxAmountByPayment)
        {
            if (amount == 0 || SVCFee == 0 || SVCFeeType != 1)
                return 0;

            decimal svc = RoundUp(amount * (SVCFee / 100));
            if (isApplyTaxCustomFee && TaxIncludeInSVCFee == 1 && svcFeeTaxAmountByPayment != 0)
            {
                decimal tempTax = svcFeeTaxRate <= 0 ? 0 : RoundUp((svc / (1 + svcFeeTaxRate / 100)) * svcFeeTaxRate / 100);
                svc -= tempTax;
            }
            return svc;
        }

        public static decimal CalculateMerchantFee(decimal amount, decimal merchantFeeValue)
        {
            decimal merchantFeeAmount = 0;

            try
            {

                if (merchantFeeValue == 0)
                    merchantFeeValue = MerchantFee;

                merchantFeeAmount = RoundUp(amount * (merchantFeeValue / 100));
            }
            catch (Exception ex)
            {
                merchantFeeAmount = 0;
            }
            return merchantFeeAmount;
        }

        public static EmployeeClockInInfoEntity GetEmployeeClockInInfoEntity(object employeeID, DateTime startDate)
        {
            EmployeeClockInInfoEntity employeeClockInInfoEntity = new EmployeeClockInInfoEntity();
            try
            {

                string sql = $"select * from TimeClock where EmployeeID = '{employeeID}' and (ClockStatus = 1 OR (DateTimeIn <= '{startDate}' and '{startDate}' <= DateTimeOut )) order by DateTimeIn desc";
                var timeClock = QueryFirstOrDefault<TimeClockEntity>(true, sql);

                if (!string.IsNullOrWhiteSpace(timeClock?.TimeClockID))
                {
                    employeeClockInInfoEntity.RolesID = timeClock.RolesID;
                    employeeClockInInfoEntity.TipRate = timeClock.TipRate;
                    employeeClockInInfoEntity.TipRateOT = timeClock.TipRate;

                    return employeeClockInInfoEntity;
                }

                sql = $"select IsUsingRoleTipPool,TipRate,TipRateOT from Employees where EmployeeID = '{employeeID}'";
                var employee = QueryFirstOrDefault<EmployeesEntity>(true, sql);

                sql = $"select top 1 r.RolesID,TipRate,TipRateOT  from EmployeeRoles er left join Roles r on er.RolesID = r.RolesID where er.EmployeeID = '{employeeID}' order by TipRate";
                var role = QueryFirstOrDefault<Models.RolesEntity>(true, sql);

                if (employee != null && role != null)
                {
                    employeeClockInInfoEntity.RolesID = role.RolesID;

                    bool IsUsingRoleTipPool = employee.IsUsingRoleTipPool;
                    bool EnableTrainingMode = employee.EnableTrainingMode;
                    if (IsUsingRoleTipPool)
                    {
                        if (EnableTrainingMode)
                        {
                            employeeClockInInfoEntity.TrainingTipRate = role.TrainingTipRate;
                            employeeClockInInfoEntity.TrainingTipRateOT = role.TrainingTipRateOT;
                        }
                        else
                        {
                            employeeClockInInfoEntity.TipRate = role.TipRate;
                            employeeClockInInfoEntity.TipRateOT = role.TipRateOT;
                        }
                    }
                    else
                    {
                        if (EnableTrainingMode)
                        {

                            employeeClockInInfoEntity.TrainingTipRate = employee.TrainingTipRate;
                            employeeClockInInfoEntity.TrainingTipRateOT = employee.TrainingTipRateOT;
                        }
                        else
                        {
                            employeeClockInInfoEntity.TipRate = employee.TipRate;
                            employeeClockInInfoEntity.TipRateOT = employee.TipRateOT;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // log exception

            }

            return employeeClockInInfoEntity;
        }

        public static void LoadBLogicPOS7Configurations()
        {
            if (string.IsNullOrEmpty(BLogicPOS7ConnectionString)) return;

            try
            {
                var dt = ProcessQuery(true, "SELECT ConfigurationName, ConfigurationValue FROM dbo.Configurations", "Configurations");

                POSConfig.Configuration.ConfigValue.GetValueFromTable(dt);

                string sql = $"SELECT b.SingleTaxRatePercentage FROM dbo.TaxCodeDetails a LEFT JOIN dbo.TaxAuthorities b ON a.TaxAuthorityID = b.TaxAuthorityID WHERE a.TaxCodeID = '{POSConfig.Configuration.ConfigValue.CreditCardConfigValue.TaxCodeIDForCustomFee}'";

                Utility.TaxPercentForSVCFee = Utility.GetDecimalFromObject(ProcessQuerySingleValue(true, sql));

                Utility.IsCredit = true;
                Utility.IsUseConfigDualPrice = Utility.IsCheckUseDualPrice();
                Utility.IsApplyTaxForSVCFee = !Utility.IsUseConfigDualPrice && POSConfig.Configuration.ConfigValue.CreditCardConfigValue.SVCFee > 0 && Utility.TaxPercentForSVCFee > 0;
                Utility.IsShowCreditPriceOnScreen = POSConfig.Configuration.ConfigValue.SystemConfigValue.ShowItemPriceOnDualPriceMode == "1";
                if (!Utility.IsUseConfigDualPrice)
                    Utility.IsShowCreditPriceOnScreen = false;
                Utility.IsCurrentPriceUseDualPrice = Utility.IsShowPriceUseDualPrice();

                Utility.EnableSurcharge = POSConfig.Configuration.ConfigValue.SystemConfigValue.EnableSurcharge;
                if (Utility.EnableSurcharge)
                {
                    Utility.IsApplyTaxForSVCFee = true;

                    POSConfig.Configuration.ConfigValue.CreditCardConfigValue.SVCFee = POSConfig.Configuration.ConfigValue.CreditCardConfigValue.SurchargeFee;
                    POSConfig.Configuration.ConfigValue.CreditCardConfigValue.CashDiscount = 0;

                    POSConfig.Configuration.ConfigValue.SystemConfigValue.PrintCustomFee = "1";
                    POSConfig.Configuration.ConfigValue.CreditCardConfigValue.PrintCustomFee = "1";
                    POSConfig.Configuration.ConfigValue.CreditCardConfigValue.PrintSVCFeeForGuestCheck = "1";
                    POSConfig.Configuration.ConfigValue.CreditCardConfigValue.ShowSVCFeeOnCustomerDisplay = "1";
                }
            }
            catch (Exception ex)
            {
                AppLogger.Error("LoadBLogicPOS7Configurations failed", ex);
            }
        }

        public static SaleReceiptsEntity GetSaleReceiptsByID(string receiptId)
        {
            return QueryFirstOrDefault<SaleReceiptsEntity>(true, $"SELECT * FROM dbo.SaleReceipts WHERE SaleReceiptID = '{receiptId}'");
        }

        public static SaleReceiptPaymentDetailsEntity GetSaleReceiptPaymentDetailsByID(string saleReceiptPaymentDetailID)
        {
            return QueryFirstOrDefault<SaleReceiptPaymentDetailsEntity>(true, $"SELECT * FROM dbo.SaleReceiptPaymentDetails WHERE SaleReceiptPaymentDetailID = '{saleReceiptPaymentDetailID}'");
        }

        public static List<SaleReceiptPaymentDetailsEntity> GetListSaleReceiptPaymentDetailsBySaleReceiptID(string receiptId)
        {
            return Query<SaleReceiptPaymentDetailsEntity>(true, $"SELECT * FROM dbo.SaleReceiptPaymentDetails WHERE SaleReceiptID = '{receiptId}'");
        }

        public static decimal GetSVCFeePercent(int CreditCardFeeMode)
        {
            switch (CreditCardFeeMode)
            {
                case 0:
                    return POSConfig.Configuration.ConfigValue.CreditCardConfigValue.SVCFee;
                case 1:
                    return 0;
                case 2:
                    return POSConfig.Configuration.ConfigValue.CreditCardConfigValue.SurchargeFee;
                default:
                    return POSConfig.Configuration.ConfigValue.CreditCardConfigValue.SVCFee;
            }
        }

        public static decimal GetMerchantFeePercent(int CreditCardFeeMode, string PaymentCardType)
        {
            switch (CreditCardFeeMode)
            {
                case 0:
                case 1:
                    return POSConfig.Configuration.ConfigValue.CreditCardConfigValue.MerchantFee;
                case 2:
                    if (string.Equals(PaymentCardType, "Debit", StringComparison.OrdinalIgnoreCase))
                        return POSConfig.Configuration.ConfigValue.CreditCardConfigValue.MerchantFeePercentForDebit;
                    else if (string.Equals(PaymentCardType, "EBT", StringComparison.OrdinalIgnoreCase))
                        return POSConfig.Configuration.ConfigValue.CreditCardConfigValue.MerchantFeePercentForEBT;
                    else
                        return POSConfig.Configuration.ConfigValue.CreditCardConfigValue.MerchantFee;
                default:
                    return POSConfig.Configuration.ConfigValue.CreditCardConfigValue.MerchantFee;
            }
        }

        public static decimal GetBaseAmountFee(decimal SVCFeePercent)
        {
            if (SVCFeePercent == 0)
                return POSConfig.Configuration.ConfigValue.CreditCardConfigValue.BaseAmountFee;
            else
                return SVCFeePercent / (100 + SVCFeePercent) * 100;
        }
    }
}
