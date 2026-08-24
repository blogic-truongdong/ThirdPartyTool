using BLogicPOS7SupportTool.Core;
using BLogicPOS7SupportTool.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace BLogicPOS7SupportTool.Services
{
    // Reads the transaction template CSV and turns each line into a complete,
    // settled credit-card ticket spread across both databases:
    //
    //   BLOGICPOS7 : SaleReceipts, SaleReceiptDetails, SaleReceiptDetailTaxDetails,
    //                SaleReceiptPaymentDetails, SaleReceiptServiceChargeTaxDetails,
    //                PaymentRoleDetails
    //   Merchant   : Batch, TransactionDetail
    //
    // Every column mirrors the reference ticket that already sits in the database,
    // so a ticket restored from the template looks exactly like one the POS wrote.
    class TicketTemplateService
    {
        // ---- constants taken from the reference ticket ---------------------
        // The kiosk sale channel. Every ticket already in this store's SaleReceipts uses
        // this trio, so a restored ticket has to as well or it lands in the wrong
        // channel on every sales report.
        private const int SaleTypeID = 8;
        private const string SaleTypeName = "KIOSK";
        private const int SaleReceiptType = 27;
        private const string TicketComments = "TO GO";
        private const int PaymentMethodID = 2;         // Credit
        private const short PaymentMethodType = 2;
        private const byte TaxMethod = 2;
        private const int PartIndex = 2;
        private const int ItemDiscountType = 1;
        private const string ModifierTypeNormal = "Normal";
        private const string SeatAssignmentNA = "N/A";
        private const string CreditCardTransactionType = "Sale_Settled";
        private const short DefaultRolesID = 1;

        // Used when the template's EmployeeCode matches no employee.
        private const int DefaultEmployeeID = -1;
        private const string DefaultEmployeeName = "BLogic";

        // Used when the workstation has no closed shift opened before the ticket time.
        private const string DefaultShiftID = "00000000-0000-0000-0000-000000000000";

        public const string DefaultTemplateFileName = "TemplateInsertSalesData.xlsx";

        // Sheet names, and the columns each one must have. "Templates" is the input the
        // operator fills in; the others hold the figures its formulas work out, one
        // sheet per table, joined to Templates by ID.
        private const string SheetTemplates = "Templates";
        private const string SheetSaleReceipts = "SaleReceipts";
        private const string SheetSaleReceiptDetails = "SaleReceiptDetails";
        private const string SheetPaymentDetails = "SaleReceiptPaymentDetails";
        private const string SheetTransactionDetail = "TransactionDetail";

        private static readonly Dictionary<string, string[]> RequiredSheetColumns =
            new Dictionary<string, string[]>
            {
                [SheetTemplates] = new[]
                {
                    "ID", "TransactionDate", "TransactionTime", "CardNumber", "CardType", "AuthCode",
                    "TerminalID", "WorkstationName", "EmployeeCode", "Amount", "Gratuity", "Total",
                    "SVCFeePercent", "BaseAmountFee", "MerchantFeePercent", "TaxRate", "ItemName"
                },
                [SheetSaleReceipts] = new[] { "ID", "TotalAmount", "TaxAmount", "SVCFeeAmount", "SVCFeePaidAmount" },
                [SheetSaleReceiptDetails] = new[] { "ID", "ItemName", "TaxRate", "Price", "TaxAmount", "ExtendedPrice" },
                [SheetPaymentDetails] = new[]
                {
                    "ID", "AmountTendered", "Gratuity", "SVCFeeAmount", "SVCFeeForGratuity",
                    "MerchantFee", "MerchantFeeForTip"
                },
                [SheetTransactionDetail] = new[]
                {
                    "ID", "Amount", "Gratuity", "Total", "SVCFeeAmount", "SVCFeeForGratuity",
                    "MerchantFee", "MerchantFeeForTip"
                }
            };

        public static string DefaultTemplatePath
        {
            get
            {
                return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates", DefaultTemplateFileName);
            }
        }

        // The workbook already carries every fee figure, worked out on its own sheets,
        // so nothing here is recalculated - see Load.

        // ---- target table schemas -----------------------------------------
        // "Invalid column name" (error 207) is a compile-time failure of the whole
        // batch: one unknown column and not a single INSERT in the script runs. Support
        // databases sit on several schema versions, so the real column set of every
        // target table is read up front and each INSERT names only what exists.

        private static readonly string[] Pos7Tables =
        {
            "SaleReceipts", "SaleReceiptDetails", "SaleReceiptDetailTaxDetails",
            "SaleReceiptPaymentDetails", "SaleReceiptServiceChargeTaxDetails", "PaymentRoleDetails"
        };

        private static readonly string[] MerchantTables = { "Batch", "TransactionDetail" };

        // Tables an insert cannot do without. SaleReceiptServiceChargeTaxDetails is not
        // here: nothing is written to it, it only has to be cleared on rollback.
        private static readonly string[] RequiredTables =
        {
            "SaleReceipts", "SaleReceiptDetails", "SaleReceiptDetailTaxDetails",
            "SaleReceiptPaymentDetails", "PaymentRoleDetails", "Batch", "TransactionDetail"
        };

        private readonly Dictionary<string, HashSet<string>> _tableColumns =
            new Dictionary<string, HashSet<string>>(StringComparer.OrdinalIgnoreCase);

        private readonly List<string> _schemaNotes = new List<string>();

        // Columns that had to be skipped because the database does not have them, and
        // tables that are missing outright. Filled by Validate/Insert.
        public List<string> SchemaNotes { get { return _schemaNotes; } }

        // Reads sys.columns for every target table. Returns the required tables that do
        // not exist in this database.
        private List<string> LoadTableSchemas()
        {
            _tableColumns.Clear();
            _schemaNotes.Clear();

            foreach (var t in Pos7Tables) LoadTableColumns(true, t);
            foreach (var t in MerchantTables) LoadTableColumns(false, t);

            var missing = new List<string>();
            foreach (var t in RequiredTables)
            {
                bool isPos7 = Pos7Tables.Contains(t, StringComparer.OrdinalIgnoreCase);
                if (!TableExists(isPos7, t))
                {
                    string db = isPos7 ? AppSession.BLogicPOS7DatabaseName : AppSession.MerchantDatabaseName;
                    missing.Add($"{db}.dbo.{t}");
                }
            }
            if (missing.Count > 0)
                _schemaNotes.Add("Missing table(s): " + string.Join(", ", missing));

            return missing;
        }

        private void LoadTableColumns(bool isPos7, string table)
        {
            var dt = AppSession.ProcessQuery(isPos7,
                $"SELECT name FROM sys.columns WHERE object_id = OBJECT_ID('dbo.{table}')", "Cols");

            var set = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            foreach (DataRow r in dt.Rows) set.Add(AppSession.GetString(r, "name"));
            _tableColumns[TableKey(isPos7, table)] = set;
        }

        private static string TableKey(bool isPos7, string table)
        {
            return (isPos7 ? "P:" : "M:") + table;
        }

        private bool TableExists(bool isPos7, string table)
        {
            HashSet<string> cols;
            return _tableColumns.TryGetValue(TableKey(isPos7, table), out cols) && cols.Count > 0;
        }

        // Hands the operator the shipped workbook, formulas and all, so a new file
        // starts out calculating every derived figure the same way.
        public static void SaveTemplateTo(string path)
        {
            string source = DefaultTemplatePath;
            if (!File.Exists(source))
                throw new FileNotFoundException(
                    "The workbook shipped with the tool is missing: " + source, source);

            File.Copy(source, path, true);
            AppLogger.Info($"Ticket template copied to {path}");
        }

        // ================================================================
        //  1. Load
        // ================================================================

        // Reads the workbook: the Templates sheet plus one sheet per table, joined by
        // ID. Nothing is recalculated here - every amount is whatever the workbook's
        // formulas produced. Rows that cannot be used come back marked Invalid rather
        // than throwing, so the grid can show the operator exactly which one is wrong.
        public List<TicketTemplateRowEntity> Load(string path)
        {
            var sheets = new Dictionary<string, List<Dictionary<string, string>>>();
            foreach (var sheet in RequiredSheetColumns.Keys)
            {
                var data = XlsxReader.ReadSheet(path, sheet, "ID");
                if (data.Count > 0)
                {
                    var absent = RequiredSheetColumns[sheet].Where(c => !data[0].ContainsKey(c)).ToList();
                    if (absent.Count > 0)
                        throw new InvalidDataException(
                            $"Sheet \"{sheet}\" is missing these columns: {string.Join(", ", absent)}");
                }
                sheets[sheet] = data;
            }

            // The derived sheets are looked up by ID, not by position: their buffer rows
            // evaluate to "" and are dropped, so row numbers no longer line up.
            var receipts = ById(sheets[SheetSaleReceipts]);
            var details = ById(sheets[SheetSaleReceiptDetails]);
            var payments = ById(sheets[SheetPaymentDetails]);
            var transactions = ById(sheets[SheetTransactionDetail]);

            var rows = new List<TicketTemplateRowEntity>();
            int line = 1;

            foreach (var t in sheets[SheetTemplates])
            {
                line++;
                string id = Text(t, "ID");
                var row = new TicketTemplateRowEntity { Id = id, LineNumber = line, Selected = true };

                row.BankTransactionID = Text(t, "BankTransactionID");
                row.TranType = Text(t, "TranType");
                row.CardNumber = Text(t, "CardNumber");
                row.CardType = Text(t, "CardType");
                row.AuthCode = Text(t, "AuthCode");
                row.InvoiceNo = Text(t, "InvoiceNo");
                row.RefNo = Text(t, "RefNo");
                row.TerminalID = Text(t, "TerminalID");
                row.WorkstationName = Text(t, "WorkstationName");
                row.EmployeeCode = Text(t, "EmployeeCode");
                row.ItemName = Text(t, "ItemName");
                if (string.IsNullOrWhiteSpace(row.TranType)) row.TranType = "Sale";

                row.Amount = Money(t, "Amount");
                row.Gratuity = Money(t, "Gratuity");
                row.Total = Money(t, "Total");
                row.SVCFeePercent = Rate(t, "SVCFeePercent");
                row.BaseAmountFee = Rate(t, "BaseAmountFee");
                row.MerchantFeePercent = Rate(t, "MerchantFeePercent");
                row.TaxRate = Rate(t, "TaxRate");

                var problems = new List<string>();

                DateTime when;
                if (!TryParseDateTime(Text(t, "TransactionDate"), Text(t, "TransactionTime"), out when))
                    problems.Add($"cannot read the date/time \"{Text(t, "TransactionDate")} {Text(t, "TransactionTime")}\"");
                row.TransactionDateTime = when;

                Dictionary<string, string> sr, srd, srp, td;
                receipts.TryGetValue(id, out sr);
                details.TryGetValue(id, out srd);
                payments.TryGetValue(id, out srp);
                transactions.TryGetValue(id, out td);

                var absentFrom = new[]
                {
                    sr == null ? SheetSaleReceipts : null,
                    srd == null ? SheetSaleReceiptDetails : null,
                    srp == null ? SheetPaymentDetails : null,
                    td == null ? SheetTransactionDetail : null
                }.Where(s => s != null).ToList();

                if (absentFrom.Count > 0)
                {
                    problems.Add($"ID {id} is missing from sheet(s) {string.Join(", ", absentFrom)}");
                }
                else
                {
                    row.ReceiptTotalAmount = Money(sr, "TotalAmount");
                    row.ReceiptTaxAmount = Money(sr, "TaxAmount");
                    row.ReceiptSVCFeeAmount = Money(sr, "SVCFeeAmount");
                    row.ReceiptSVCFeePaidAmount = Money(sr, "SVCFeePaidAmount");

                    row.DetailPrice = Money(srd, "Price");
                    row.DetailTaxAmount = Money(srd, "TaxAmount");
                    row.DetailExtendedPrice = Money(srd, "ExtendedPrice");
                    if (!string.IsNullOrWhiteSpace(Text(srd, "ItemName"))) row.ItemName = Text(srd, "ItemName");
                    if (Rate(srd, "TaxRate") > 0m) row.TaxRate = Rate(srd, "TaxRate");

                    row.PayAmountTendered = Money(srp, "AmountTendered");
                    row.PaySVCFeeAmount = Money(srp, "SVCFeeAmount");
                    row.PaySVCFeeForGratuity = Money(srp, "SVCFeeForGratuity");
                    row.PayMerchantFee = Money(srp, "MerchantFee");
                    row.PayMerchantFeeForTip = Money(srp, "MerchantFeeForTip");

                    row.TranAmount = Money(td, "Amount");
                    row.TranTotal = Money(td, "Total");
                    row.TranSVCFee = Money(td, "SVCFeeAmount");
                    row.TranSVCFeeForGratuity = Money(td, "SVCFeeForGratuity");
                    row.TranMerchantFee = Money(td, "MerchantFee");
                    row.TranMerchantFeeForTip = Money(td, "MerchantFeeForTip");

                    // Arithmetic the workbook's formulas are meant to guarantee. A
                    // mismatch means one was overtyped, and inserting the row would put a
                    // ticket in the database whose own parts do not add up.
                    if (row.ReceiptTotalAmount <= 0m)
                        problems.Add("SaleReceipts.TotalAmount must be greater than 0");
                    if (row.DetailExtendedPrice + row.DetailTaxAmount != row.ReceiptTotalAmount)
                        problems.Add($"SaleReceiptDetails ExtendedPrice + TaxAmount " +
                                     $"({(row.DetailExtendedPrice + row.DetailTaxAmount).ToString("N2")}) does not equal " +
                                     $"SaleReceipts.TotalAmount ({row.ReceiptTotalAmount.ToString("N2")})");
                    if (row.PayAmountTendered != row.ReceiptTotalAmount)
                        problems.Add($"SaleReceiptPaymentDetails.AmountTendered ({row.PayAmountTendered.ToString("N2")}) " +
                                     $"does not equal SaleReceipts.TotalAmount ({row.ReceiptTotalAmount.ToString("N2")})");
                    if (row.TranAmount != row.ReceiptTotalAmount + row.ReceiptSVCFeeAmount)
                        problems.Add($"TransactionDetail.Amount ({row.TranAmount.ToString("N2")}) does not equal " +
                                     $"TotalAmount + SVCFeeAmount ({(row.ReceiptTotalAmount + row.ReceiptSVCFeeAmount).ToString("N2")})");
                    if (row.TranTotal != row.TranAmount + row.Gratuity)
                        problems.Add($"TransactionDetail.Total ({row.TranTotal.ToString("N2")}) does not equal " +
                                     $"Amount + Gratuity ({(row.TranAmount + row.Gratuity).ToString("N2")})");
                }

                if (string.IsNullOrWhiteSpace(row.ItemName)) problems.Add("ItemName is empty");
                if (string.IsNullOrWhiteSpace(row.WorkstationName)) problems.Add("WorkstationName is empty");

                if (problems.Count > 0)
                {
                    row.State = TicketTemplateRowState.Invalid;
                    row.Message = Capitalise(string.Join("; ", problems));
                    row.Selected = false;
                }

                rows.Add(row);
            }

            AppLogger.Info($"Ticket template loaded: {rows.Count} row(s) from {path}");
            return rows;
        }

        private static Dictionary<string, Dictionary<string, string>> ById(List<Dictionary<string, string>> rows)
        {
            var map = new Dictionary<string, Dictionary<string, string>>(StringComparer.OrdinalIgnoreCase);
            foreach (var r in rows)
            {
                string id = Text(r, "ID");
                if (id.Length > 0 && !map.ContainsKey(id)) map[id] = r;
            }
            return map;
        }

        private static string Text(Dictionary<string, string> row, string column)
        {
            string value;
            return row != null && row.TryGetValue(column, out value) ? (value ?? "").Trim() : "";
        }

        // Excel keeps money as a double, so a cell shown as 2.24 can arrive as
        // 2.2400000000000002. Every amount is taken at 2 decimals.
        private static decimal Money(Dictionary<string, string> row, string column)
        {
            return AppSession.RoundUp(ParseMoney(Text(row, column)));
        }

        // Percentages keep their full precision: BaseAmountFee is 3.84615385.
        private static decimal Rate(Dictionary<string, string> row, string column)
        {
            return ParseMoney(Text(row, column));
        }

        // ================================================================
        //  2. Validate - resolve every reference and spot duplicates
        // ================================================================

        public void Validate(List<TicketTemplateRowEntity> rows)
        {
            if (rows == null || rows.Count == 0) return;

            // Check the target tables before anything else: a missing table means no
            // row can be inserted at all, whatever else resolves.
            var missingTables = LoadTableSchemas();

            var workstations = LoadWorkstations();
            var employees = LoadEmployees();
            var items = LoadItems();
            var shifts = LoadShifts();

            foreach (var row in rows)
            {
                if (row.State == TicketTemplateRowState.Inserted) continue;

                // Load already rejected this row on the workbook's own contents; its
                // reason is more use than "no workstation named ''" would be.
                if (row.State == TicketTemplateRowState.Invalid) continue;

                var problems = new List<string>();   // blocks the row
                var notes = new List<string>();      // worth saying, but still insertable

                if (missingTables.Count > 0)
                    problems.Add("this database has no " + string.Join(", ", missingTables));

                WorkstationRef ws;
                if (workstations.TryGetValue(Key(row.WorkstationName), out ws))
                {
                    row.WorkstationID = ws.WorkstationID;
                    row.WorkstationCode = ws.WorkstationCode;
                    row.StoreID = ws.StoreID;
                    row.TerminalIPAddress = ws.TerminalIPAddress;
                    if (string.IsNullOrWhiteSpace(row.TerminalID)) row.TerminalID = ws.MerchantID;
                }
                else
                {
                    problems.Add($"no workstation named \"{row.WorkstationName}\"");
                }

                // The employee comes from EmployeeCode. An unknown code is not a reason
                // to reject the ticket - it falls back to the built-in BLogic account.
                EmployeeRef emp;
                if (employees.TryGetValue(Key(row.EmployeeCode), out emp))
                {
                    row.EmployeeID = emp.EmployeeID;
                    row.EmployeeFullName = emp.FullName;
                    row.RolesID = emp.RolesID > 0 ? emp.RolesID : DefaultRolesID;
                }
                else
                {
                    row.EmployeeID = DefaultEmployeeID;
                    row.EmployeeFullName = DefaultEmployeeName;
                    row.RolesID = DefaultRolesID;
                    notes.Add(string.IsNullOrWhiteSpace(row.EmployeeCode)
                        ? $"No EmployeeCode - using {DefaultEmployeeName} ({DefaultEmployeeID})."
                        : $"EmployeeCode \"{row.EmployeeCode}\" not found - using {DefaultEmployeeName} ({DefaultEmployeeID}).");
                }

                ItemRef item;
                if (items.TryGetValue(Key(row.ItemName), out item))
                {
                    row.ItemID = item.ItemID;
                    row.ItemLookupCode = item.LookupCode;
                    row.ItemTaxCodeID = item.TaxCodeID;
                }
                else
                {
                    problems.Add($"no item named \"{row.ItemName}\"");
                }

                row.ShiftID = row.WorkstationID == null
                    ? null
                    : PickShift(shifts, row.WorkstationID, row.TransactionDateTime);

                if (string.IsNullOrEmpty(row.ShiftID))
                {
                    row.ShiftID = DefaultShiftID;
                    notes.Add($"No earlier ticket on this workstation to take a shift from - ShiftID = {DefaultShiftID}.");
                }

                if (problems.Count > 0)
                {
                    row.State = TicketTemplateRowState.Invalid;
                    row.Message = Capitalise(string.Join("; ", problems));
                    row.Selected = false;
                    continue;
                }

                row.State = TicketTemplateRowState.Ready;
                row.Message = string.Join(" ", notes);
                row.Selected = true;
            }

            WarmSchemaNotes(rows.FirstOrDefault(r => r.State == TicketTemplateRowState.Ready));
        }

        // Builds the three scripts for one sample row and throws the SQL away. The point
        // is the side effect: Insert() only learns which columns this database is missing
        // while it builds its statements, and the operator has to see that in the summary
        // and in the confirmation dialog *before* committing, not afterwards.
        private void WarmSchemaNotes(TicketTemplateRowEntity sample)
        {
            if (sample == null) return;

            try
            {
                var batch = new BatchInfo
                {
                    BatchID = "0",
                    BatchUnique = Guid.Empty.ToString(),
                    MerchantID = sample.TerminalID,
                    WorkstationName = sample.WorkstationName,
                    Rows = new List<TicketTemplateRowEntity> { sample }
                };
                string probeId = Guid.Empty.ToString();

                BuildPos7Script(sample, Guid.Empty, probeId, "0");
                BuildBatchInsert(batch);
                BuildTransactionDetailInsert(sample, batch, probeId, "0", "0");
            }
            catch (Exception ex)
            {
                AppLogger.Error("WarmSchemaNotes failed", ex);
            }
        }

        // ================================================================
        //  3. Insert
        // ================================================================

        public class InsertResult
        {
            public int Inserted;
            public int Failed;
            public int Batches;
            public string ScriptFile;
            public List<string> Errors = new List<string>();
        }

        // Every statement that actually succeeded, kept apart by database so the export
        // can be replayed elsewhere: the run itself interleaves the two databases, but a
        // script file has to be runnable one connection at a time.
        private readonly List<string> _logPos7 = new List<string>();
        private readonly List<string> _logMerchantBatches = new List<string>();
        private readonly List<string> _logMerchantTransactions = new List<string>();
        private readonly List<string> _logMerchantRecalc = new List<string>();

        // Writes every Ready + selected row. Each ticket goes in as two scripts: one
        // transactional batch against BLOGICPOS7 and one against Merchant. When the
        // Merchant half fails the BLOGICPOS7 half is deleted again, so no half-ticket
        // is ever left behind (the two databases cannot share a single transaction
        // without a distributed transaction coordinator).
        public InsertResult Insert(List<TicketTemplateRowEntity> rows, Action<int, int> onProgress)
        {
            var result = new InsertResult();
            var todo = rows.Where(r => r.Selected && r.State == TicketTemplateRowState.Ready).ToList();
            if (todo.Count == 0) return result;

            // Re-read the schemas: the insert has to build its column lists from what
            // the database has right now, not from whatever Validate saw earlier.
            var missingTables = LoadTableSchemas();
            if (missingTables.Count > 0)
            {
                foreach (var row in todo)
                {
                    row.State = TicketTemplateRowState.Failed;
                    row.Message = "Missing table(s): " + string.Join(", ", missingTables);
                }
                result.Failed = todo.Count;
                result.Errors.Add("Nothing was inserted - missing table(s): " + string.Join(", ", missingTables));
                return result;
            }

            long nextBatchId = NextBatchId();
            long nextTranNumber = NextTransactionNumber();

            // One batch per (merchant account, business date) - the same grouping a
            // day-end settlement would have produced.
            var batches = todo
                .GroupBy(r => r.TerminalID + "|" + r.TransactionDateTime.Date.ToString("yyyyMMdd"))
                .Select(g => new BatchInfo
                {
                    BatchID = (nextBatchId++).ToString(CultureInfo.InvariantCulture),
                    BatchUnique = Guid.NewGuid().ToString(),
                    MerchantID = g.First().TerminalID,
                    WorkstationName = g.First().WorkstationName,
                    Rows = g.ToList()
                })
                .ToList();

            _logPos7.Clear();
            _logMerchantBatches.Clear();
            _logMerchantTransactions.Clear();
            _logMerchantRecalc.Clear();

            foreach (var batch in batches)
            {
                string batchSql = BuildBatchInsert(batch);
                if (AppSession.ProcessNonQuery(false, batchSql) < 0)
                {
                    foreach (var row in batch.Rows)
                    {
                        row.State = TicketTemplateRowState.Failed;
                        row.Message = "The settlement batch row could not be created.";
                    }
                    result.Failed += batch.Rows.Count;
                    result.Errors.Add($"Batch {batch.BatchID} ({batch.MerchantID}, {batch.BusinessDate:MM/dd/yyyy}) could not be created.");
                    batch.Skip = true;
                }
                else
                {
                    _logMerchantBatches.Add(
                        $"-- Batch {batch.BatchID} | merchant {batch.MerchantID} | {batch.BusinessDate:MM/dd/yyyy} | " +
                        $"{batch.Rows.Count} ticket(s)\r\n{batchSql}");
                }
            }

            int done = 0;
            foreach (var batch in batches.Where(b => !b.Skip))
            {
                foreach (var row in batch.Rows)
                {
                    onProgress?.Invoke(done, todo.Count);
                    done++;

                    var receiptId = Guid.NewGuid();
                    var transactionId = Guid.NewGuid().ToString();
                    string tranNumber = (nextTranNumber++).ToString(CultureInfo.InvariantCulture).PadLeft(6, '0');

                    try
                    {
                        string pos7Sql = BuildPos7Script(row, receiptId, transactionId, tranNumber);
                        if (AppSession.ProcessNonQuery(true, pos7Sql) < 0)
                        {
                            row.State = TicketTemplateRowState.Failed;
                            row.Message = "The ticket could not be written to BLOGICPOS7 (see the log for details).";
                            result.Failed++;
                            result.Errors.Add($"Line {row.LineNumber}: BLOGICPOS7 insert failed.");
                            continue;
                        }

                        // The ticket number is the SaleReceipts identity value, so it can
                        // only be read back after the insert; the Merchant row needs it.
                        var readBack = AppSession.ProcessQuery(true,
                            $"SELECT SaleReceiptNumber FROM dbo.SaleReceipts WHERE SaleReceiptID = '{receiptId}'",
                            "Receipt");
                        string ticketNumber = readBack.Rows.Count > 0 ? AppSession.GetString(readBack.Rows[0], "SaleReceiptNumber") : "";

                        string tranSql = BuildTransactionDetailInsert(row, batch, transactionId, tranNumber, ticketNumber);
                        if (AppSession.ProcessNonQuery(false, tranSql) < 0)
                        {
                            // Undo the BLOGICPOS7 half so the ticket doesn't survive
                            // without its credit-card transaction.
                            AppSession.ProcessNonQuery(true, BuildPos7Rollback(receiptId));
                            row.State = TicketTemplateRowState.Failed;
                            row.Message = "The Merchant transaction could not be written - the ticket was rolled back.";
                            result.Failed++;
                            result.Errors.Add($"Line {row.LineNumber}: Merchant insert failed, ticket rolled back.");
                            continue;
                        }

                        // Both halves are in, so this ticket is safe to replay elsewhere.
                        string head = $"-- Template line {row.LineNumber} | {row.TransactionDateTime:MM/dd/yyyy HH:mm:ss} | " +
                                      $"{row.WorkstationName} | {row.CardType} {row.CardLast4} | {row.TranTotal.ToString("N2", CultureInfo.InvariantCulture)}";
                        _logPos7.Add($"{head} | SaleReceiptID {receiptId}\r\n{pos7Sql}");
                        _logMerchantTransactions.Add($"{head} | TransactionDetail.ID {transactionId}\r\n{tranSql}");

                        row.SaleReceiptID = receiptId.ToString();
                        row.TransactionID = transactionId;
                        row.TicketNumber = ticketNumber;
                        row.State = TicketTemplateRowState.Inserted;
                        row.Message = $"Ticket #{ticketNumber}";
                        row.Selected = false;
                        result.Inserted++;
                    }
                    catch (Exception ex)
                    {
                        AppLogger.Error($"Insert ticket from template failed (line {row.LineNumber})", ex);
                        row.State = TicketTemplateRowState.Failed;
                        row.Message = ex.Message;
                        result.Failed++;
                        result.Errors.Add($"Line {row.LineNumber}: {ex.Message}");
                    }
                }
            }

            onProgress?.Invoke(todo.Count, todo.Count);

            // Restate every batch from the transactions that actually made it in, and
            // drop the batches that ended up empty.
            foreach (var batch in batches.Where(b => !b.Skip))
            {
                string recalcSql = BuildBatchRecalculate(batch);
                AppSession.ProcessNonQuery(false, recalcSql);
                _logMerchantRecalc.Add($"-- Batch {batch.BatchID}\r\n{recalcSql}");
            }

            result.Batches = batches.Count(b => !b.Skip &&
                b.Rows.Any(r => r.State == TicketTemplateRowState.Inserted));
            result.ScriptFile = WriteScriptFile(result);

            // dbo.AutoID is only read, never advanced: the Merchant application owns
            // that counter. NextTransactionNumber already takes MAX(Number) into
            // account, so the numbers written here cannot collide with existing rows.

            AppLogger.Info($"Insert tickets from template: {result.Inserted} inserted, {result.Failed} failed, {result.Batches} batch(es)");
            return result;
        }

        // Scripts go next to the executable so they sit beside the log, but that folder
        // is read-only when the tool is installed under Program Files - fall back to the
        // user's own AppData rather than losing the export.
        private static string ScriptFolder()
        {
            var candidates = new[]
            {
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs"),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                             "BLogicPOS7SupportTool", "logs")
            };

            Exception last = null;
            foreach (var folder in candidates)
            {
                try
                {
                    Directory.CreateDirectory(folder);

                    // CreateDirectory succeeding does not prove the folder is writable.
                    string probe = Path.Combine(folder, ".write-test");
                    File.WriteAllText(probe, "");
                    File.Delete(probe);
                    return folder;
                }
                catch (Exception ex) { last = ex; }
            }
            throw last ?? new IOException("No writable folder for the insert script.");
        }

        // Writes every statement that succeeded to one .sql file, split per database so
        // the same tickets can be re-created on another machine. Best-effort: a failure
        // to write the log must never undo a successful insert.
        private string WriteScriptFile(InsertResult result)
        {
            if (_logPos7.Count == 0 && _logMerchantBatches.Count == 0) return null;

            try
            {
                string name = "InsertTickets_" +
                              DateTime.Now.ToString("yyyyMMdd_HHmmss", CultureInfo.InvariantCulture) + ".sql";
                string path = Path.Combine(ScriptFolder(), name);

                var sb = new StringBuilder();
                sb.AppendLine("/* =====================================================================");
                sb.AppendLine("   Insert Tickets From Template - replay script");
                sb.AppendLine("   Generated : " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture));
                sb.AppendLine("   Source    : " + AppSession.BLogicPOS7DatabaseName + " + " + AppSession.MerchantDatabaseName);
                sb.AppendLine("   Contents  : " + result.Inserted + " ticket(s), " + result.Batches + " batch(es)");
                sb.AppendLine("");
                sb.AppendLine("   HOW TO RUN THIS ON ANOTHER MACHINE");
                sb.AppendLine("     1. Run SECTION 1 against that store's BLogicPOS7 database.");
                sb.AppendLine("     2. Run SECTION 2 against its Merchant database.");
                sb.AppendLine("     Keep the order inside each section - later statements depend on");
                sb.AppendLine("     earlier ones (batches before their transactions).");
                sb.AppendLine("");
                sb.AppendLine("   WHAT THE TARGET DATABASE DECIDES BY ITSELF");
                sb.AppendLine("     SaleReceipts.AutoID and SaleReceiptNumber (identity + trigger),");
                sb.AppendLine("     SaleReceiptPaymentDetails.SaleReceiptPaymentDetailID (identity,");
                sb.AppendLine("     picked up by SCOPE_IDENTITY inside each ticket), and");
                sb.AppendLine("     SaleReceiptOrder / ReceiptNumberByWorkstation (trigger).");
                sb.AppendLine("");
                sb.AppendLine("   WHAT STAYS FIXED - check these suit the target store");
                sb.AppendLine("     SaleReceiptID / TransactionDetail.ID and the other GUIDs, plus");
                sb.AppendLine("     WorkstationID, EmployeeID, ItemID, ShiftID, StoreID, MerchantID,");
                sb.AppendLine("     Batch.ID and TransactionDetail.Number. TicketNumber below still");
                sb.AppendLine("     holds the number this machine assigned.");
                sb.AppendLine("   ===================================================================== */");
                sb.AppendLine();

                sb.AppendLine("-- #####################################################################");
                sb.AppendLine("-- SECTION 1 of 2 - run against BLogicPOS7 (" + AppSession.BLogicPOS7DatabaseName + ")");
                sb.AppendLine("-- " + _logPos7.Count + " ticket(s)");
                sb.AppendLine("-- #####################################################################");
                sb.AppendLine();
                foreach (var script in _logPos7) { sb.AppendLine(script); sb.AppendLine("GO"); sb.AppendLine(); }

                sb.AppendLine("-- #####################################################################");
                sb.AppendLine("-- SECTION 2 of 2 - run against Merchant (" + AppSession.MerchantDatabaseName + ")");
                sb.AppendLine("-- " + _logMerchantBatches.Count + " batch(es), " + _logMerchantTransactions.Count + " transaction(s)");
                sb.AppendLine("-- #####################################################################");
                sb.AppendLine();
                sb.AppendLine("-- 2a. batches first: the transactions below reference them by BatchID");
                foreach (var script in _logMerchantBatches) { sb.AppendLine(script); sb.AppendLine("GO"); sb.AppendLine(); }

                sb.AppendLine("-- 2b. transactions");
                foreach (var script in _logMerchantTransactions) { sb.AppendLine(script); sb.AppendLine("GO"); sb.AppendLine(); }

                sb.AppendLine("-- 2c. restate each batch from the transactions that landed in it");
                foreach (var script in _logMerchantRecalc) { sb.AppendLine(script); sb.AppendLine("GO"); sb.AppendLine(); }

                File.WriteAllText(path, sb.ToString(), new UTF8Encoding(false));
                AppLogger.Info($"Insert script written to {path}");
                return path;
            }
            catch (Exception ex)
            {
                AppLogger.Error("WriteScriptFile failed", ex);
                result.Errors.Add("The tickets were inserted but the replay script could not be written: " + ex.Message);
                return null;
            }
        }

        // ---- BLOGICPOS7: the ticket tables --------------------------------

        private string BuildPos7Script(TicketTemplateRowEntity row, Guid receiptId, string transactionId, string tranNumber)
        {
            var detailId = Guid.NewGuid();
            var taxDetailId = Guid.NewGuid();
            var kdsId = Guid.NewGuid();
            var groupId = Guid.NewGuid();
            var roleId = Guid.NewGuid();

            DateTime when = row.TransactionDateTime;

            var sb = new StringBuilder();
            sb.AppendLine("SET ANSI_NULLS ON;");
            sb.AppendLine("SET QUOTED_IDENTIFIER ON;");
            sb.AppendLine($"DECLARE @receiptId uniqueidentifier = '{receiptId}';");
            sb.AppendLine("DECLARE @paymentId bigint, @receiptNoByWs int;");
            sb.AppendLine("BEGIN TRY");
            sb.AppendLine("BEGIN TRAN;");

            // The per-workstation receipt number comes from the highest one that
            // workstation has used so far. No AutoID table is touched at all: the
            // ticket number is the SaleReceipts identity and SaleReceiptOrder is left
            // empty, so nothing here needs a shared counter.
            sb.AppendLine($@"SELECT @receiptNoByWs = ISNULL(MAX(TRY_CONVERT(int, ReceiptNumberByWorkstation)), 0) + 1
                             FROM dbo.SaleReceipts WHERE WorkstationID = {S(row.WorkstationID)};");

            // -- SaleReceipts -------------------------------------------------
            var receipt = new List<KeyValuePair<string, string>>
            {
                Col("SaleReceiptID", "@receiptId"),
                Col("StoreID", S(row.StoreID)),
                Col("CashierID", I(row.EmployeeID)),
                Col("TransactionStatusID", "'FIN'"),
                Col("ShiftID", S(row.ShiftID)),
                Col("RefSaleReceiptID", "''"),
                Col("GlobalDiscountPercent", M(0)),
                Col("GlobalDiscountAmount", M(0)),
                Col("TaxAmount", M(row.ReceiptTaxAmount)),
                Col("TotalAmount", M(row.ReceiptTotalAmount)),
                Col("DateCreated", DT(when)),
                Col("StartDate", DT(when)),
                Col("PriceLevel", I(0)),
                Col("TotalDiscountAmount", M(0)),
                Col("Comments", S(TicketComments)),
                Col("SalesOrderFee", M(0)),
                Col("SoldQuantity", I(0)),
                Col("TotalQuantity", I(0)),
                Col("SaleReceiptType", I(SaleReceiptType)),
                Col("DepositAmount", M(0)),
                Col("DepositPaid", M(0)),
                Col("PaidAmount", M(row.PayAmountTendered)),
                Col("Version", I(1)),
                Col("DebitAmount", M(0)),
                Col("ClearDebitAmount", M(0)),
                // Nullable in the schema, but the POS never leaves them null and
                // downstream reads treat null as a broken ticket - always write 0.
                Col("DeliveryStatus", I(0)),
                Col("DeliveryFee", M(0)),
                Col("SplitStatus", I(0)),
                Col("AreaFee", M(0)),
                Col("ServiceFee", M(0)),
                Col("CustomerInfo", "''"),
                Col("GuestsNum", I(0)),
                Col("ServiceFeePercent", M(0)),
                Col("DetailSummary", S(BuildDetailSummary(row.ItemName))),
                Col("TicketType", I(0)),
                Col("IsPaidManyTimes", BIT(false)),
                Col("ServiceChargeTaxCodeID", "'0'"),
                Col("ServiceChargeTaxRate", M(0)),
                Col("ServiceChargeTaxAmount", M(0)),
                Col("RedeemPoints", M(0)),
                Col("RewardPoints", M(0)),
                Col("CheckingData", "''"),
                Col("EBTPaid", "'0'"),
                Col("IsRated", "''"),
                Col("IsSigned", "''"),
                Col("InvoiceNum", S(row.InvoiceNo)),
                Col("RedeemStatus", I(0)),
                Col("CustomerPoints", M(0)),
                // The ticket totals follow the payment line: PaidAmount is the sum of
                // AmountTendered and SVCFeePaidAmount the sum of the payments' SVC fee,
                // which with one payment per ticket is simply that payment's figures.
                Col("SVCFeePercent", M(row.SVCFeePercent)),
                Col("SVCFeeAmount", M(row.ReceiptSVCFeeAmount)),
                Col("SVCFeePaidAmount", M(row.ReceiptSVCFeePaidAmount)),
                Col("CashDiscountPercent", M(0)),
                Col("CashDiscountAmount", M(0)),
                Col("CashDiscountPaidAmount", M(0)),
                Col("CurrentCustomerName", "''"),
                Col("SaleTypeID", I(SaleTypeID)),
                Col("IsOpen", BIT(false)),
                Col("WorkstationID", S(row.WorkstationID)),
                Col("WorkstationName", S(row.WorkstationName)),
                Col("EmployeePaidID", I(row.EmployeeID)),
                Col("TicketNotes", "''"),
                Col("PartyServiceFee", M(0)),
                Col("PartyServiceFeeAmount", M(0)),
                Col("AreaFeeAmount", M(0)),
                Col("PartyServiceFeeTaxAmount", M(0)),
                Col("AreaFeeTaxAmount", M(0)),
                Col("ServiceFeeTaxAmount", M(0)),
                Col("IsOpenTab", BIT(false)),
                Col("OpenTabCreditCardTransactionID", "''"),
                Col("OpenTabCardNumber", "''"),
                Col("OpenTabCustomer", "''"),
                Col("OpenTabAmount", M(0)),
                Col("IsHold", BIT(false)),
                Col("CurrentCustomerPhone", "''"),
                Col("CurrentCustomerAddress", "''"),
                Col("RefNo", "''"),
                Col("CurrentCustomerEmail", "''"),
                Col("SaleTypeName", S(SaleTypeName)),
                Col("PhoneAppCustomerName", "''"),
                Col("PickUpDateTime", DT(when)),
                Col("StatusProcess", "''"),
                Col("DeliveryDateTime", DT(when)),
                Col("DiscountType", I(0)),
                Col("ExtraFeeAmount", M(0)),
                Col("ExtraFeeTaxAmount", M(0)),
                Col("IsPrintKitchen", BIT(false)),
                Col("ReceiptNumberByWorkstation", "CAST(@receiptNoByWs AS varchar(100))"),
                Col("AutoGratuityPercent", M(0)),
                Col("AutoGratuityAmount", M(0)),
                Col("AutoGratuityTaxAmount", M(0)),
                Col("LastModifyDateTime", DT(when)),
                Col("IsModifyTicket", BIT(false)),
                Col("DeliveryFeeTaxAmount", M(0)),
                Col("StatusSaleReceipt", I(0)),
                Col("OrderNumberForQWaitList", "''"),
                Col("SaleReceiptOrder", "''"),
                Col("IsDeposit", BIT(false)),
                Col("SaleReceiptOriginalInfo", "''"),
                Col("EmployeeIDDiscountTicket", I(row.EmployeeID)),
                // Left empty on purpose: it is a snapshot the POS takes of its own
                // session state, and a restored ticket has no such session to describe.
                Col("ReceiptInfo", "''"),
                Col("SaleReceiptJson", "''"),
                Col("SVCFeeTaxAmount", M(0)),
                Col("CashDiscountTaxAmount", M(0)),
                Col("SVCFeeTaxRate", M(0)),
                Col("SVCFeeTaxPaidAmount", M(0)),
                Col("CashDiscountTaxPaidAmount", M(0)),
                Col("SVCFeeTaxCodeID", "'0'"),
                Col("CoinDiscountAmount", M(0)),
                Col("FinalGratuity", M(row.Gratuity))
            };
            // SaleReceiptNumber is deliberately left out of the column list. It has no
            // default, so it lands NULL and the dbo.UpdateSalesNumber trigger fills it
            // with LTRIM(STR(AutoID)) - the same thing the POS relies on. SaleReceiptOrder
            // is inserted empty for the same reason: the trigger assigns it from
            // AutoIDs.TicketOrder and advances that counter itself.
            sb.AppendLine(Insert("dbo.SaleReceipts", receipt));

            // -- SaleReceiptDetails -------------------------------------------
            decimal lineTotal = row.DetailExtendedPrice + row.DetailTaxAmount;
            var detail = new List<KeyValuePair<string, string>>
            {
                Col("SaleReceiptDetailID", $"'{detailId}'"),
                Col("SaleReceiptID", "@receiptId"),
                Col("ItemID", S(row.ItemID)),
                Col("Quantity", M(1)),
                Col("Taxable", BIT(row.DetailTaxAmount > 0m)),
                Col("TaxCodeID", S(row.ItemTaxCodeID)),
                Col("GroupID", $"'{groupId}'"),
                Col("TaxRate", M(row.TaxRate)),
                Col("TaxMethod", I(TaxMethod)),
                Col("TaxAmount", M(row.DetailTaxAmount)),
                Col("Description", S(row.ItemName)),
                Col("Description2", "''"),
                Col("Price", M(row.DetailPrice)),
                Col("MarkDown", M(0)),
                Col("ItemUnitCost", M(0)),
                Col("ExtendedPrice", M(row.DetailExtendedPrice)),
                Col("Lookup_Code", S(row.ItemLookupCode)),
                Col("SalePersonID", I(row.EmployeeID)),
                Col("ReasonComment", "''"),
                Col("DiscountPercent", M(0)),
                Col("DiscountAmount", M(0)),
                Col("Attribute", "''"),
                Col("Size", "''"),
                Col("TotalSaleDiscountAmount", M(0)),
                Col("TotalSaleDiscountPercent", M(0)),
                Col("TotalSaleDiscountAmountBySystem", M(0)),
                Col("TotalSaleDiscountPercentBySystem", M(0)),
                Col("TotalSaleDiscountAmountByCustomer", M(0)),
                Col("TotalSaleDiscountPercentByCustomer", M(0)),
                Col("QuantitySold", M(0)),
                Col("RefSaleReceiptDetailID", "''"),
                Col("Status", I(0)),
                Col("InventoryUpdatable", BIT(false)),
                Col("isToGo", BIT(false)),
                Col("PartIndex", I(PartIndex)),
                Col("Range", I(0)),
                Col("ServiceFeeAmount", M(0)),
                Col("ServiceFeePercent", M(0)),
                Col("LastModifyTime", DT(when)),
                Col("EmployeeID", I(row.EmployeeID)),
                Col("EmployeeName", S(row.EmployeeFullName)),
                Col("CommissionRate", M(0)),
                Col("CommissionValue", M(0)),
                Col("OpenPriceValue", M(0)),
                Col("StoreDiscountRate", M(0)),
                Col("StoreDiscountValue", M(0)),
                Col("ItemdiscountRate", M(0)),
                Col("ItemDiscountValue", M(0)),
                Col("IssuePoints", M(0)),
                Col("RedeemPoints", M(0)),
                Col("RedeemStatus", I(0)),
                Col("EBTType", "'0'"),
                Col("EBTPaid", "''"),
                Col("ItemGroupID", I(0)),
                Col("PriceBeforeBulk", M(0)),
                Col("BulkID", "''"),
                Col("BulkGroupID", "''"),
                Col("PriceBeforeMM", M(0)),
                Col("MMID", "''"),
                Col("MMGroupID", "''"),
                Col("IsCompItem", BIT(false)),
                Col("EmployeeIDComp", I(row.EmployeeID)),
                Col("EmployeeNameCOMP", "''"),
                Col("COMPDateTime", DT(when)),
                Col("TPStatus", I(0)),
                Col("TPID", "''"),
                Col("PriceBeforeTP", M(0)),
                Col("TaxRateBeforeTP", M(0)),
                Col("TPTime", DT(when)),
                Col("QtyPerItem", M(1)),
                Col("CashDiscountPerItem", M(0)),
                Col("Comments", "''"),
                Col("Description3", "''"),
                Col("HoldDateTime", DT(when)),
                Col("HoldMinutes", I(0)),
                Col("IsSplitItems", BIT(false)),
                Col("DescriptionChinese", S(row.ItemName)),
                Col("KDSIDDetail", $"'{kdsId}'"),
                Col("QuantityKDS", M(1)),
                Col("KDSDoneDateTime", DT(when)),
                Col("StampDateTime", DT(when)),
                Col("OrderDateTime", DT(when)),
                Col("IsModifierCombine", BIT(false)),
                Col("IsOverwritePrintModifier", BIT(false)),
                Col("MMIsApplyItem", BIT(false)),
                Col("MMLeastExpensiveDiscountType", "''"),
                Col("MMLeastExpensiveDiscountValue", M(0)),
                Col("SeatAssignment", S(SeatAssignmentNA)),
                Col("SeatAssignmentIndex", "'0'"),
                Col("IsDeposit", BIT(false)),
                Col("ShortDescription", "''"),
                Col("ReasonCodeDiscountStore", "''"),
                Col("ReasonCodeDiscountCategory", "''"),
                Col("ReasonCodeDiscountTicket", "''"),
                Col("ReasonCodeDiscountItem", "''"),
                Col("ReasonCodeDiscountCustomer", "''"),
                Col("ReasonCodeDiscountLevelCustomer", "''"),
                Col("ItemDiscountByPriceLevelAmount", M(0)),
                Col("PriceBeforeRedeem", M(0)),
                Col("EmployeeIDDiscountTicket", I(row.EmployeeID)),
                Col("EmployeeIDDiscountItem", I(row.EmployeeID)),
                Col("RegularPrice", M(row.DetailPrice)),
                Col("DualPrice", M(row.DetailPrice)),
                Col("PriceCash", M(row.DetailPrice)),
                Col("PriceCC", M(row.DetailPrice)),
                Col("TaxType", I(0)),
                Col("ItemDiscountType", I(ItemDiscountType)),
                Col("CoinDiscountPerItem", M(0)),
                Col("ModifierType", S(ModifierTypeNormal)),
                Col("CategoryDiscountType", "''"),
                Col("FinalQuantity", M(1)),
                Col("FinalDiscountAmount", M(0)),
                Col("FinalCashDiscount", M(0)),
                Col("FinalCoinDiscount", M(0)),
                Col("FinalTaxAmount", M(row.DetailTaxAmount)),
                Col("FinalTotalSales", M(lineTotal)),
                Col("FinalGrossSales", M(row.DetailExtendedPrice)),
                Col("FinalNetSales", M(row.DetailExtendedPrice)),
                Col("FinalNetSalesTax", M(lineTotal))
            };
            sb.AppendLine(Insert("dbo.SaleReceiptDetails", detail));

            // -- SaleReceiptDetailTaxDetails ----------------------------------
            var taxDetail = new List<KeyValuePair<string, string>>
            {
                Col("SaleReceiptDetailTaxDetailID", $"'{taxDetailId}'"),
                Col("SaleReceiptDetailID", $"'{detailId}'"),
                Col("SaleReceiptID", "@receiptId"),
                Col("ItemID", S(row.ItemID)),
                Col("Quantity", M(1)),
                Col("TaxCodeID", S(row.ItemTaxCodeID)),
                Col("GroupID", $"'{groupId}'"),
                Col("TaxRate", M(row.TaxRate)),
                Col("TaxAmount", M(row.DetailTaxAmount)),
                Col("Description", S(row.ItemName)),
                Col("Price", M(row.DetailPrice)),
                Col("ExtendedPrice", M(row.DetailExtendedPrice)),
                Col("ReasonComment", "''"),
                Col("Status", I(0)),
                Col("Range", I(0)),
                Col("LastModifyTime", DT(when)),
                Col("EmployeeID", I(row.EmployeeID)),
                Col("EmployeeName", S(row.EmployeeFullName)),
                Col("IsCompItem", BIT(false)),
                Col("IsDeposit", BIT(false)),
                Col("TaxType", I(0)),
                Col("KDSIDDetail", $"'{kdsId}'")
            };
            sb.AppendLine(Insert("dbo.SaleReceiptDetailTaxDetails", taxDetail));

            // -- SaleReceiptPaymentDetails ------------------------------------
            var payment = new List<KeyValuePair<string, string>>
            {
                Col("SaleReceiptID", "@receiptId"),
                Col("PaymentMethodID", I(PaymentMethodID)),
                // The payment is what the card was charged less the custom fee, and the
                // fee columns mirror TransactionDetail exactly.
                Col("AmountTendered", M(row.PayAmountTendered)),
                Col("AmountReturn", M(0)),
                Col("CreatedDate", DT(when)),
                Col("IsDeposit", BIT(false)),
                Col("GiftExtra", M(0)),
                Col("Cashier", I(row.EmployeeID)),
                Col("ShiftID", S(row.ShiftID)),
                Col("CurrencyAmount", M(0)),
                Col("CurrencyID", I(1)),
                Col("PaymentType", I(0)),
                Col("Gratuity", M(row.Gratuity)),
                Col("CreditCardTransactionID", S(transactionId)),
                Col("CreditCardTransactionNumber", S(tranNumber)),
                Col("CreditCardTransactionCardNumber", S(MaskedCardNumber(row))),
                Col("CreditCardTransactionType", S(CreditCardTransactionType)),
                Col("PaidServiceFee", M(0)),
                Col("PaidPayout", M(0)),
                Col("CreditCardRefNo", S(row.RefNo)),
                Col("CreditCardAuthCode", S(row.AuthCode)),
                Col("SVCFeeAmount", M(row.PaySVCFeeAmount)),
                Col("SVCFeeForGratuity", M(row.PaySVCFeeForGratuity)),
                Col("SVCFeeForServiceCharge", M(0)),
                Col("CashDiscountAmount", M(0)),
                Col("MerchantFee", M(row.PayMerchantFee)),
                Col("PartyServiceFeeAmount", M(0)),
                Col("SVCFeeForPartyServiceFeeAmount", M(0)),
                Col("AreaFeeAmount", M(0)),
                Col("SVCFeeForAreaFeeAmount", M(0)),
                Col("ServiceFee", M(0)),
                Col("SVCFeeForServiceFee", M(0)),
                Col("MerchantFeeForTip", M(row.PayMerchantFeeForTip)),
                Col("MerchantFeeForServiceFee", M(0)),
                Col("MerchantFeeForAreaFeeAmount", M(0)),
                Col("MerchantFeeForPartyServiceFeeAmount", M(0)),
                Col("CardHolderName", "''"),
                Col("CardBalance", M(0)),
                Col("AutoGratuityAmount", M(0)),
                Col("SVCFeeForAutoGratuityAmount", M(0)),
                Col("MerchantFeeForAutoGratuityAmount", M(0)),
                Col("IsDecline", BIT(false)),
                Col("IsDepositPayment", BIT(false)),
                Col("HouseAccountNumber", "''"),
                Col("GiftCardTransactionID", "''"),
                Col("CreditCardCardType", S(NormaliseCardType(row.CardType))),
                Col("SVCFeeTaxAmount", M(0)),
                Col("CashDiscountTaxAmount", M(0)),
                Col("ServiceFeeTaxAmount", M(0)),
                Col("AreaFeeTaxAmount", M(0)),
                Col("PartyServiceFeeTaxAmount", M(0)),
                Col("AutoGratuityTaxAmount", M(0)),
                Col("TaxAmountByPaymentModeTaxCustomFee", M(0)),
                Col("IsCalculateTaxCustomFee", I(0)),
                Col("CoinDiscountAmount", M(0)),
                Col("CreditCardTransactionMerchantID", S(row.TerminalID)),
                Col("EmployeePaidID", I(row.EmployeeID)),
                Col("PaidHouseAccount", M(0)),
                Col("PaymentCardType", "''")
            };
            sb.AppendLine(Insert("dbo.SaleReceiptPaymentDetails", payment));
            sb.AppendLine("SET @paymentId = SCOPE_IDENTITY();");

            // SaleReceiptServiceChargeTaxDetails deliberately gets no row. That table
            // only holds the per-tax-code breakdown of service charges (area fee, auto
            // gratuity, party service fee, manual service fee, extra fee) and the
            // template carries none of them, so every service-charge amount on the
            // ticket is 0 - exactly like the reference ticket, which has no row there
            // either. The rollback below still clears the table so the day a
            // service-charge template appears, nothing is left orphaned.

            // -- PaymentRoleDetails -------------------------------------------
            var roleDetail = new List<KeyValuePair<string, string>>
            {
                Col("ID", $"'{roleId}'"),
                Col("SaleReceiptID", "@receiptId"),
                Col("SaleReceiptPaymentDetailID", "@paymentId"),
                Col("PaymentMethodID", I(PaymentMethodID)),
                Col("PaymentMethodType", I(PaymentMethodType)),
                Col("EmployeeID", I(row.EmployeeID)),
                Col("RolesID", I(row.RolesID)),
                Col("TipRate", M(0)),
                Col("TipRateOT", M(0)),
                Col("Gratuity", M(row.Gratuity)),
                Col("SVCFeeForGratuity", M(0)),
                Col("MerchantFeeForTip", M(0)),
                Col("DateCreated", DT(when)),
                Col("PartyServiceFeeAmount", M(0)),
                Col("PartyServiceFeeTaxAmount", M(0)),
                Col("SVCFeeForPartyServiceFeeAmount", M(0)),
                Col("MerchantFeeForPartyServiceFeeAmount", M(0)),
                Col("AutoGratuityAmount", M(0)),
                Col("AutoGratuityTaxAmount", M(0)),
                Col("SVCFeeForAutoGratuityAmount", M(0)),
                Col("MerchantFeeForAutoGratuityAmount", M(0)),
                Col("AreaFeeAmount", M(0)),
                Col("AreaFeeTaxAmount", M(0)),
                Col("SVCFeeForAreaFeeAmount", M(0)),
                Col("MerchantFeeForAreaFeeAmount", M(0)),
                Col("ServiceFee", M(0)),
                Col("ServiceFeeTaxAmount", M(0)),
                Col("SVCFeeForServiceFee", M(0)),
                Col("MerchantFeeForServiceFee", M(0))
            };
            sb.AppendLine(Insert("dbo.PaymentRoleDetails", roleDetail));

            sb.AppendLine("COMMIT;");
            sb.AppendLine("END TRY");
            sb.AppendLine("BEGIN CATCH");
            sb.AppendLine("IF @@TRANCOUNT > 0 ROLLBACK;");
            sb.AppendLine("THROW;");
            sb.AppendLine("END CATCH");
            return sb.ToString();
        }

        // Removes a ticket and everything hanging off it - used when the Merchant half
        // of the insert fails after BLOGICPOS7 has already committed.
        private string BuildPos7Rollback(Guid receiptId)
        {
            // Child rows first, and only from tables this database actually has -
            // naming a missing table would make the cleanup itself fail.
            var order = new[]
            {
                "PaymentRoleDetails", "SaleReceiptServiceChargeTaxDetails", "SaleReceiptPaymentDetails",
                "SaleReceiptDetailTaxDetails", "SaleReceiptDetails", "SaleReceipts"
            };

            var sb = new StringBuilder();
            foreach (var table in order)
                if (TableExists(true, table))
                    sb.AppendLine($"DELETE FROM dbo.{table} WHERE SaleReceiptID = '{receiptId}';");
            return sb.ToString();
        }

        // ---- Merchant: Batch + TransactionDetail --------------------------

        private string BuildBatchInsert(BatchInfo batch)
        {
            // Every figure is the sum of what the workbook worked out per ticket.
            decimal merchantFeeAmount = batch.Rows.Sum(r => r.TranMerchantFee);
            decimal total = batch.Rows.Sum(r => r.TranTotal);

            var cols = new List<KeyValuePair<string, string>>
            {
                Col("Date", DT(batch.SettledAt)),
                Col("ID", S(batch.BatchID)),
                Col("Status", "'Closed'"),
                Col("MerchantID", S(batch.MerchantID)),
                Col("Quantity", I(batch.Rows.Count)),
                Col("Total", M(total)),
                Col("BatchUnique", S(batch.BatchUnique)),
                Col("TotalSVCFeeForTip", M(batch.Rows.Sum(r => r.TranSVCFeeForGratuity))),
                Col("TotalSales", M(batch.Rows.Sum(r => r.ReceiptTotalAmount))),
                Col("TotalReturns", M(0)),
                Col("TotalTips", M(batch.Rows.Sum(r => r.Gratuity))),
                Col("TotalSVCFeeAmount", M(batch.Rows.Sum(r => r.TranSVCFee))),
                // What the merchant is actually paid out: the charged total less the
                // merchant fee. Recalculated from the real transactions after the run.
                Col("DepositAmount", M(total - merchantFeeAmount)),
                Col("WorkStationName", S(batch.WorkstationName)),
                Col("MerchantFeeAmount", M(merchantFeeAmount)),
                Col("MerchantFeePercent", M(batch.Rows[0].MerchantFeePercent))
            };
            return "SET QUOTED_IDENTIFIER ON;\r\n" + Insert("dbo.Batch", cols);
        }

        // Restates a batch from the transactions that really landed in it, and drops
        // the batch when every one of its tickets failed.
        private string BuildBatchRecalculate(BatchInfo batch)
        {
            string id = batch.BatchID.Replace("'", "''");
            return $@"
                UPDATE b SET
                    b.Quantity          = t.Cnt,
                    b.Total             = t.Total,
                    b.TotalSales        = t.TotalSales,
                    b.TotalTips         = t.TotalTips,
                    b.TotalSVCFeeAmount = t.TotalSVCFee,
                    b.TotalSVCFeeForTip = t.TotalSVCFeeForTip,
                    b.MerchantFeeAmount = t.MerchantFee,
                    b.DepositAmount     = t.Total - t.MerchantFee,
                    b.Date              = t.LastDate
                FROM dbo.Batch b
                CROSS APPLY (
                    SELECT COUNT(*) AS Cnt,
                           ISNULL(SUM(Total), 0) AS Total,
                           ISNULL(SUM(TotalAmount), 0) AS TotalSales,
                           ISNULL(SUM(Gratuity), 0) AS TotalTips,
                           ISNULL(SUM(SVCFee), 0) AS TotalSVCFee,
                           ISNULL(SUM(SVCFeeForGratuity), 0) AS TotalSVCFeeForTip,
                           ISNULL(SUM(MerchantFee), 0) AS MerchantFee,
                           ISNULL(MAX([Date]), b.[Date]) AS LastDate
                    FROM dbo.TransactionDetail WHERE BatchID = '{id}'
                ) t
                WHERE b.ID = '{id}' AND t.Cnt > 0;

                DELETE FROM dbo.Batch
                WHERE ID = '{id}'
                  AND NOT EXISTS (SELECT 1 FROM dbo.TransactionDetail WHERE BatchID = '{id}');";
        }

        private string BuildTransactionDetailInsert(TicketTemplateRowEntity row, BatchInfo batch,
            string transactionId, string tranNumber, string ticketNumber)
        {
            var cols = new List<KeyValuePair<string, string>>
            {
                Col("ID", S(transactionId)),
                Col("Number", S(tranNumber)),
                Col("Date", DT(row.TransactionDateTime)),
                Col("TranType", S(row.TranType)),
                Col("TenderType", "'CreditCard'"),
                Col("CardNumber", S(MaskedCardNumber(row))),
                Col("CardType", S(NormaliseCardType(row.CardType))),
                Col("ExpDate", "''"),
                Col("Result", "'Approved'"),
                Col("TextResponse", "'OK'"),
                Col("CaptureStatus", "''"),
                Col("AVSResult", "''"),
                Col("CVVResult", "''"),
                Col("InvoiceNo", S(row.InvoiceNo)),
                Col("CustomerName", "''"),
                Col("AuthCode", S(row.AuthCode)),
                Col("RefNo", S(row.RefNo)),
                Col("AuthorizeAmount", M(row.TranAmount)),
                Col("Amount", M(row.TranAmount)),
                Col("Gratuity", M(row.Gratuity)),
                Col("Total", M(row.TranTotal)),
                Col("BatchID", S(batch.BatchID)),
                Col("BatchTotal", M(batch.Rows.Sum(r => r.TranTotal))),
                Col("BatchStatus", "'Closed'"),
                Col("ItemID", "''"),
                Col("Token", "''"),
                Col("CCEntry", "''"),
                Col("Email", "''"),
                Col("Address", "''"),
                Col("ZipCode", "''"),
                Col("Description", "'Success'"),
                Col("ReturnCode", "'000000'"),
                Col("AcqRefData", "''"),
                Col("ProcessData", "''"),
                Col("UserTraceData", "''"),
                Col("MerchantID", S(row.TerminalID)),
                Col("ShiftID", "''"),
                Col("Register", "''"),
                Col("UserID", S(row.EmployeeID.ToString(CultureInfo.InvariantCulture))),
                Col("Cashier", S((row.EmployeeFullName ?? "").Trim())),
                Col("TranStatus", "'ACTIVE'"),
                Col("TableName", "''"),
                Col("TerminalName", S(row.WorkstationCode)),
                Col("ISOCode", "''"),
                Col("ProcessingSetName", "''"),
                Col("SecondaryResponseCode", "''"),
                Col("BankApprovalCode", "''"),
                Col("BankTransactionID", S(row.BankTransactionID)),
                Col("OriginalAmount", S(row.TranAmount.ToString(CultureInfo.InvariantCulture))),
                Col("OriginalTip", S(row.Gratuity.ToString(CultureInfo.InvariantCulture))),
                Col("WorkstationID", S(row.WorkstationID)),
                Col("WorkstationName", S(row.WorkstationName)),
                Col("IsChecked", "'1'"),
                Col("Returned", "''"),
                Col("Field7", "''"),
                Col("Field8", "''"),
                Col("OrderID", "'1'"),
                // Fees the POS would have charged, from the CreditCardConfigValue
                // settings: SVC fee against the ticket, merchant fee against what the
                // card was actually charged, each with its own share of the tip.
                Col("SVCFeeForGratuity", M(row.TranSVCFeeForGratuity)),
                Col("IsTipInLine", BIT(false)),
                Col("SVCFee", M(row.TranSVCFee)),
                Col("TicketNumber", S(ticketNumber)),
                Col("RegisterID", S(row.TerminalID)),
                Col("AID", "''"),
                Col("TC", "''"),
                Col("MerchantAccount", S(row.TerminalID)),
                Col("BatchUnique", S(batch.BatchUnique)),
                Col("ServerID", S(row.EmployeeID.ToString(CultureInfo.InvariantCulture))),
                Col("ServerName", S((row.EmployeeFullName ?? "").Trim())),
                Col("SubTotal", M(row.DetailExtendedPrice)),
                Col("TaxAmount", M(row.DetailTaxAmount)),
                Col("DiscountAmount", M(0)),
                Col("ServiceCharge", M(0)),
                Col("TotalAmount", M(row.ReceiptTotalAmount)),
                Col("PartyServiceFeeAmount", M(0)),
                Col("AreaFeeAmount", M(0)),
                Col("ServiceFee", M(0)),
                Col("TerminalIpAddress", S(row.TerminalIPAddress)),
                Col("MerchantFee", M(row.PayMerchantFee)),
                Col("MerchantFeeForTip", M(row.TranMerchantFeeForTip)),
                Col("IsTryVoidOpenTab", BIT(false)),
                Col("TicketCaptured", "''"),
                Col("AmountCaptured", M(-1)),
                Col("TipCaptured", M(-1)),
                Col("MerchantServer", "''"),
                Col("Pin", "''"),
                Col("IsPhone", BIT(false)),
                Col("AutoGratuityAmount", M(0)),
                // Same reasoning as SaleReceipts.ReceiptInfo: a snapshot of the terminal
                // settings at the time of sale, which this tool cannot reconstruct.
                Col("CreditCardInfo", "''"),
                // Mirrors SaleReceipts.SaleReceiptOrder, which is left empty.
                Col("TicketOrder", "''"),
                Col("SVCFeeTaxAmount", M(0)),
                Col("SVCFeeType", "'1'"),
                Col("MerchantFeePercent", M(row.MerchantFeePercent)),
                Col("AdjustTipInformation", "''"),
                Col("CardNumberMasked", S(row.CardNumber)),
                Col("PaymentCardType", "''")
            };

            var sb = new StringBuilder();
            sb.AppendLine("SET QUOTED_IDENTIFIER ON;");
            sb.AppendLine("BEGIN TRY");
            sb.AppendLine("BEGIN TRAN;");
            sb.AppendLine(Insert("dbo.TransactionDetail", cols));
            sb.AppendLine("COMMIT;");
            sb.AppendLine("END TRY");
            sb.AppendLine("BEGIN CATCH");
            sb.AppendLine("IF @@TRANCOUNT > 0 ROLLBACK;");
            sb.AppendLine("THROW;");
            sb.AppendLine("END CATCH");
            return sb.ToString();
        }

        // ================================================================
        //  Lookups
        // ================================================================

        private class WorkstationRef
        {
            public string WorkstationID;
            public string WorkstationCode;
            public string StoreID;
            public string MerchantID;
            public string TerminalIPAddress;
            public int WorkstationType;
        }

        private class EmployeeRef
        {
            public int EmployeeID;
            public string FullName;
            public short RolesID;
        }

        private class ItemRef
        {
            public string ItemID;
            public string LookupCode;
            public string TaxCodeID;
        }

        private class ShiftRef
        {
            public string ShiftID;
            public DateTime OpeningTime;
        }

        private class BatchInfo
        {
            public string BatchID;
            public string BatchUnique;
            public string MerchantID;
            public string WorkstationName;
            public List<TicketTemplateRowEntity> Rows;
            public bool Skip;

            public DateTime BusinessDate { get { return Rows[0].TransactionDateTime.Date; } }

            // Settlement happens just after the last sale of the day.
            public DateTime SettledAt { get { return Rows.Max(r => r.TransactionDateTime).AddSeconds(30); } }
        }

        private Dictionary<string, WorkstationRef> LoadWorkstations()
        {
            var map = new Dictionary<string, WorkstationRef>();
            var dt = AppSession.ProcessQuery(true, @"
                SELECT WorkstationID, WorkstationCode, WorkstationName, WorkstationType,
                       MerchantID, TerminalIPAddress, StoreID
                FROM dbo.Workstations
                WHERE ISNULL(IsDeleted, 0) = 0
                ORDER BY WorkstationCode", "Workstations");

            foreach (DataRow r in dt.Rows)
            {
                string key = Key(AppSession.GetString(r, "WorkstationName"));
                if (key.Length == 0 || map.ContainsKey(key)) continue;
                map[key] = new WorkstationRef
                {
                    WorkstationID = AppSession.GetString(r, "WorkstationID"),
                    WorkstationCode = AppSession.GetString(r, "WorkstationCode"),
                    StoreID = AppSession.GetString(r, "StoreID"),
                    MerchantID = AppSession.GetString(r, "MerchantID"),
                    TerminalIPAddress = AppSession.GetString(r, "TerminalIPAddress"),
                    WorkstationType = AppSession.GetInt(r, "WorkstationType")
                };
            }
            return map;
        }

        // Keyed by EmployeeCode. FullName is built the way the POS writes it into the
        // ticket tables: EmployeeFirstName + ' ' + EmployeeLastName, kept untrimmed so
        // an empty last name still leaves the trailing space the POS leaves.
        // Should a code somehow be duplicated, the highest EmployeeID wins.
        private Dictionary<string, EmployeeRef> LoadEmployees()
        {
            var map = new Dictionary<string, EmployeeRef>();
            var dt = AppSession.ProcessQuery(true, @"
                SELECT EmployeeID, EmployeeCode, EmployeeFirstName, EmployeeLastName, CurrentRolesID
                FROM dbo.Employees
                ORDER BY EmployeeID", "Employees");

            foreach (DataRow r in dt.Rows)
            {
                string code = Key(AppSession.GetString(r, "EmployeeCode"));
                if (code.Length == 0) continue;

                map[code] = new EmployeeRef
                {
                    EmployeeID = AppSession.GetInt(r, "EmployeeID"),
                    FullName = AppSession.GetString(r, "EmployeeFirstName").Trim() + " " +
                               AppSession.GetString(r, "EmployeeLastName").Trim(),
                    RolesID = AppSession.GetShort(r, "CurrentRolesID")
                };
            }
            return map;
        }

        private Dictionary<string, ItemRef> LoadItems()
        {
            var map = new Dictionary<string, ItemRef>();
            var dt = AppSession.ProcessQuery(true, @"
                SELECT ItemID, ItemName, LookupCode, TaxCodeID
                FROM dbo.Items
                WHERE ISNULL(IsDeleted, 0) = 0
                ORDER BY ItemName", "Items");

            foreach (DataRow r in dt.Rows)
            {
                string key = Key(AppSession.GetString(r, "ItemName"));
                if (key.Length == 0 || map.ContainsKey(key)) continue;
                map[key] = new ItemRef
                {
                    ItemID = AppSession.GetString(r, "ItemID"),
                    LookupCode = AppSession.GetString(r, "LookupCode"),
                    TaxCodeID = AppSession.GetString(r, "TaxCodeID")
                };
            }
            return map;
        }

        // Only closed shifts (Status = 0) are candidates, loaded once and grouped by
        // workstation. Ascending by OpeningTime so PickShift can take the last match.
        private Dictionary<string, List<ShiftRef>> LoadShifts()
        {
            var map = new Dictionary<string, List<ShiftRef>>(StringComparer.OrdinalIgnoreCase);

            // Shifts come from the tickets already in the database, not from dbo.Shift:
            // a restored ticket belongs to whichever shift that workstation was really
            // using at the time, and the first ticket of a shift marks when it opened.
            // Blank ids and the all-zero fallback are skipped - they are not shifts.
            var dt = AppSession.ProcessQuery(true, @"
                SELECT WorkstationID, ShiftID, MIN(DateCreated) AS OpeningTime
                FROM dbo.SaleReceipts
                WHERE ISNULL(WorkstationID, '') <> ''
                  AND ISNULL(ShiftID, '') <> ''
                  AND ShiftID <> '00000000-0000-0000-0000-000000000000'
                  AND DateCreated IS NOT NULL
                GROUP BY WorkstationID, ShiftID
                ORDER BY MIN(DateCreated)", "Shift");

            foreach (DataRow r in dt.Rows)
            {
                string ws = AppSession.GetString(r, "WorkstationID");
                if (ws.Length == 0) continue;

                if (!map.ContainsKey(ws)) map[ws] = new List<ShiftRef>();
                map[ws].Add(new ShiftRef
                {
                    ShiftID = AppSession.GetString(r, "ShiftID"),
                    OpeningTime = AppSession.GetDateTime(r, "OpeningTime")
                });
            }
            return map;
        }

        // The last shift the workstation had opened at or before the ticket time - the
        // in-memory equivalent of:
        //   SELECT TOP 1 ShiftID FROM (
        //       SELECT WorkstationID, ShiftID, MIN(DateCreated) AS OpeningTime
        //       FROM dbo.SaleReceipts GROUP BY WorkstationID, ShiftID) s
        //   WHERE WorkstationID = @ws AND OpeningTime <= @when
        //   ORDER BY OpeningTime DESC
        // Returns null when there is no match; the caller falls back to DefaultShiftID.
        private static string PickShift(Dictionary<string, List<ShiftRef>> shifts, string workstationId, DateTime when)
        {
            List<ShiftRef> list;
            if (!shifts.TryGetValue(workstationId, out list)) return null;

            var match = list.LastOrDefault(s => s.OpeningTime <= when);
            return match == null ? null : match.ShiftID;
        }

        private long NextBatchId()
        {
            var value = AppSession.ProcessQuerySingleValue(false,
                "SELECT ISNULL(MAX(TRY_CONVERT(bigint, ID)), 0) + 1 FROM dbo.Batch");
            return value == null || value == DBNull.Value ? 1L : Convert.ToInt64(value);
        }

        // The Merchant transaction Number is a 6-digit running counter kept both in
        // dbo.AutoID and in the rows themselves; take whichever is further along.
        private long NextTransactionNumber()
        {
            var value = AppSession.ProcessQuerySingleValue(false, @"
                SELECT CASE WHEN a.Value > t.MaxNo THEN a.Value ELSE t.MaxNo END
                FROM (SELECT ISNULL(MAX(Value), 0) AS Value FROM dbo.AutoID
                      WHERE ID = 'BLogicSystems.Merchant.Data.Model+TransactionDetailDataTable') a
                CROSS JOIN (SELECT ISNULL(MAX(TRY_CONVERT(bigint, Number)), 0) + 1 AS MaxNo
                            FROM dbo.TransactionDetail) t");
            long next = value == null || value == DBNull.Value ? 1L : Convert.ToInt64(value);
            return next < 1L ? 1L : next;
        }

        // ================================================================
        //  Small helpers
        // ================================================================

        private static string MaskedCardNumber(TicketTemplateRowEntity row)
        {
            return "xxxx" + row.CardLast4;
        }

        // The POS stores card brands as VISA / MASTERCARD / AMEX / DISCOVER.
        private static string NormaliseCardType(string cardType)
        {
            string t = (cardType ?? "").Trim().ToUpperInvariant();
            switch (t)
            {
                case "MASTER CARD":
                case "MASTER-CARD":
                case "MC":
                    return "MASTERCARD";
                case "AMERICAN EXPRESS":
                case "AMERICANEXPRESS":
                case "AMERICAN-EXPRESS":
                    return "AMEX";
                default:
                    return t;
            }
        }

        // Matches the POS summary: "(1) Open Price..." - quantity, item, truncated.
        private static string BuildDetailSummary(string itemName)
        {
            string summary = "(1) " + (itemName ?? "").Trim();
            return summary.Length > 15 ? summary.Substring(0, 15).TrimEnd() + "..." : summary;
        }

        private static string Key(string value)
        {
            return (value ?? "").Trim().ToUpperInvariant();
        }

        private static KeyValuePair<string, string> Col(string name, string sqlValue)
        {
            return new KeyValuePair<string, string>(name, sqlValue);
        }

        // Emits an INSERT naming only columns the table actually has. A column the
        // database does not know is dropped (and reported) instead of being sent and
        // failing the entire batch with "Invalid column name".
        private string Insert(string table, List<KeyValuePair<string, string>> columns)
        {
            string bare = table.StartsWith("dbo.", StringComparison.OrdinalIgnoreCase)
                ? table.Substring(4) : table;
            bool isPos7 = Pos7Tables.Contains(bare, StringComparer.OrdinalIgnoreCase);

            var kept = columns;
            HashSet<string> schema;
            if (_tableColumns.TryGetValue(TableKey(isPos7, bare), out schema) && schema.Count > 0)
            {
                kept = columns.Where(c => schema.Contains(c.Key)).ToList();

                if (kept.Count != columns.Count)
                {
                    var dropped = columns.Where(c => !schema.Contains(c.Key)).Select(c => c.Key).ToList();
                    string note = $"{bare}: {dropped.Count} column(s) not in this database, skipped - " +
                                  string.Join(", ", dropped);
                    if (!_schemaNotes.Contains(note))
                    {
                        _schemaNotes.Add(note);
                        AppLogger.Warn(note);
                    }
                }
            }

            string colList = string.Join(", ", kept.Select(c => "[" + c.Key + "]"));
            string valList = string.Join(", ", kept.Select(c => c.Value));
            return $"INSERT INTO {table} ({colList}) VALUES ({valList});";
        }

        private static string S(string value)
        {
            return "'" + (value ?? "").Replace("'", "''") + "'";
        }

        private static string M(decimal value)
        {
            return value.ToString(CultureInfo.InvariantCulture);
        }

        private static string I(int value)
        {
            return value.ToString(CultureInfo.InvariantCulture);
        }

        private static string DT(DateTime value)
        {
            return "'" + value.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture) + "'";
        }

        private static string BIT(bool value)
        {
            return value ? "1" : "0";
        }

        private static string Field(List<string> fields, Dictionary<string, int> index, string name)
        {
            int i;
            if (!index.TryGetValue(name, out i) || i >= fields.Count) return "";
            return (fields[i] ?? "").Trim();
        }

        private static decimal ParseMoney(string text)
        {
            text = (text ?? "").Trim().Replace("$", "").Replace(",", "").Replace("%", "");
            if (text.Length == 0) return 0m;

            bool negative = text.StartsWith("(") && text.EndsWith(")");
            if (negative) text = text.Substring(1, text.Length - 2);

            decimal value;
            if (!decimal.TryParse(text, NumberStyles.Number, CultureInfo.InvariantCulture, out value))
                return 0m;
            return negative ? -value : value;
        }

        // Date and time arrive either as text the operator typed ("08-01-2026",
        // "11:46:18") or as the numbers Excel stores behind a date/time format - whole
        // days since 1899-12-30 for a date, a fraction of a day for a time. A single
        // workbook holds both, so both are accepted.
        private static bool TryParseDateTime(string dateText, string timeText, out DateTime result)
        {
            result = DateTime.MinValue;
            var us = new CultureInfo("en-US");

            DateTime date;
            double serial;
            if (double.TryParse(dateText, NumberStyles.Any, CultureInfo.InvariantCulture, out serial) && serial > 0)
                date = ExcelEpoch.AddDays(serial);
            else if (!DateTime.TryParse(dateText, us, DateTimeStyles.None, out date) &&
                     !DateTime.TryParse(dateText, CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
                return false;

            TimeSpan time = date.TimeOfDay;   // an Excel serial date can carry the time too
            if (!string.IsNullOrWhiteSpace(timeText))
            {
                double fraction;
                DateTime parsedTime;
                if (double.TryParse(timeText, NumberStyles.Any, CultureInfo.InvariantCulture, out fraction))
                    time = TimeSpan.FromDays(fraction - Math.Floor(fraction));
                else if (DateTime.TryParse(timeText, us, DateTimeStyles.None, out parsedTime))
                    time = parsedTime.TimeOfDay;
                else if (!TimeSpan.TryParse(timeText, CultureInfo.InvariantCulture, out time))
                    return false;
            }

            // Excel time serials are not exact: 11:46:18 comes back a few ticks short.
            result = date.Date.AddSeconds(Math.Round(time.TotalSeconds));
            return true;
        }

        // Excel counts days from 1900-01-01 as day 1 but also believes 1900 was a leap
        // year, so the usable epoch is 1899-12-30.
        private static readonly DateTime ExcelEpoch = new DateTime(1899, 12, 30);

        // Minimal CSV reader: comma separated, double quotes optional, "" escapes a quote.
        private static List<string> SplitCsvLine(string line)
        {
            var fields = new List<string>();
            var current = new StringBuilder();
            bool inQuotes = false;

            for (int i = 0; i < line.Length; i++)
            {
                char c = line[i];
                if (inQuotes)
                {
                    if (c == '"')
                    {
                        if (i + 1 < line.Length && line[i + 1] == '"') { current.Append('"'); i++; }
                        else inQuotes = false;
                    }
                    else current.Append(c);
                }
                else if (c == '"') inQuotes = true;
                else if (c == ',') { fields.Add(current.ToString()); current.Clear(); }
                else current.Append(c);
            }
            fields.Add(current.ToString());
            return fields;
        }

        private static string Capitalise(string text)
        {
            if (string.IsNullOrEmpty(text)) return text;
            return char.ToUpperInvariant(text[0]) + text.Substring(1);
        }
    }
}
