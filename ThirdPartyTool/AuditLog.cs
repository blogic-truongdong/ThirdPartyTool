using BLogicSystems.XPos.ClientUtilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace ThirdPartyTool
{
    /// <summary>
    /// Keeps the tool from hurting the live POS: the tool always loses a deadlock (DEADLOCK_PRIORITY LOW), and
    /// the transactional functions give up after a short lock wait instead of queueing behind POS work.
    /// </summary>
    public static class SqlSafety
    {
        /// <summary>Prefix for statements of the older functions: the tool is the deadlock victim, never the POS.</summary>
        public const string LowPriority = "SET DEADLOCK_PRIORITY LOW;\r\n";

        /// <summary>Lock wait (ms) for the transactional functions before they roll back and retry.</summary>
        public const int LockTimeoutMs = 5000;

        public const int MaxAttempts = 3;

        /// <summary>Session settings for a connection that will run a transaction (Refund items, Add special item).</summary>
        public static void PrepareTransactionalSession(SqlConnection conn)
        {
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = $"SET DEADLOCK_PRIORITY LOW; SET LOCK_TIMEOUT {LockTimeoutMs};";
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>Deadlock victim (1205) or lock wait timeout (1222): the transaction was rolled back and can be retried.</summary>
        public static bool IsBusy(Exception ex)
        {
            for (Exception e = ex; e != null; e = e.InnerException)
                if (e is SqlException sql && sql.Errors.Cast<SqlError>().Any(x => x.Number == 1205 || x.Number == 1222))
                    return true;
            return false;
        }

        public const string BusyMessage = "The database is busy (the POS was using the same ticket or batch). Nothing was changed - please try again in a moment.";

        /// <summary>Runs a whole transactional operation, retrying it when SQL Server reports a deadlock or lock timeout.</summary>
        public static void RunWithRetry(Action operation)
        {
            for (int attempt = 1; ; attempt++)
            {
                try
                {
                    operation();
                    return;
                }
                catch (Exception ex) when (IsBusy(ex) && attempt < MaxAttempts)
                {
                    BLogicLogger.Error($"SqlSafety: attempt {attempt} hit a deadlock/lock timeout, retrying. {ex.Message}");
                    System.Threading.Thread.Sleep(400 * attempt);
                }
            }
        }
    }

    /// <summary>
    /// One transaction for code that talks to the database through the static Helper functions
    /// (ProcessQuery, ProcessQueryEntity, CreateQuerySelectUnique, RunQueryUpdate(bool, ...)).
    /// While a unit is open those functions run on its single POS connection and transaction; Merchant statements
    /// run there too through <c>EXEC [Merchant].sys.sp_executesql</c>, so the original SQL text is unchanged.
    /// The first failing statement rolls everything back at once (no locks kept while an error is shown) and
    /// <see cref="Complete"/> then refuses to commit. On success Complete writes one audit row and commits.
    /// <code>using (SqlUnit unit = SqlUnit.Begin("Finish ticket", saleReceiptID)) { ...; unit.Complete(); }</code>
    /// </summary>
    public sealed class SqlUnit : IDisposable
    {
        private static readonly System.Threading.AsyncLocal<SqlUnit> current = new System.Threading.AsyncLocal<SqlUnit>();

        /// <summary>The open unit of this flow, or null.</summary>
        public static SqlUnit Current => current.Value;

        private readonly bool owner;
        private readonly string action, saleReceiptID;
        private SqlConnection connection;
        private SqlTransaction transaction;
        private bool done;

        public string MerchantDb { get; }
        public bool Failed { get; private set; }
        public Exception Error { get; private set; }

        private SqlUnit(string action, string saleReceiptID, bool owner)
        {
            this.action = action;
            this.saleReceiptID = saleReceiptID;
            this.owner = owner;
            MerchantDb = "[" + Helper.Merchant_Name.Replace("]", "]]") + "]";
        }

        public static SqlUnit Begin(string action, string saleReceiptID = null)
        {
            if (current.Value != null)
                return new SqlUnit(action, saleReceiptID, owner: false); // nested: joins the outer unit

            SqlUnit unit = new SqlUnit(action, saleReceiptID, owner: true);
            unit.connection = new SqlConnection(Helper.BLOGICPOS7_Connection);
            unit.connection.Open();
            SqlSafety.PrepareTransactionalSession(unit.connection);
            unit.transaction = unit.connection.BeginTransaction();
            AuditLog.BeginCapture();
            current.Value = unit;
            return unit;
        }

        /// <summary>A command on the unit for a POS statement, or (pos = false) a Merchant statement run through sp_executesql.</summary>
        public SqlCommand Command(bool pos, string sql, SqlParameter[] parameters = null)
        {
            if (Failed)
                throw new InvalidOperationException("The transaction was already rolled back: " + Error?.Message);

            SqlCommand cmd = connection.CreateCommand();
            cmd.Transaction = transaction;
            cmd.CommandType = CommandType.Text;
            if (pos)
            {
                cmd.CommandText = sql;
                if (parameters != null) cmd.Parameters.AddRange(parameters);
            }
            else
            {
                if (parameters != null && parameters.Length > 0)
                    throw new NotSupportedException("Merchant statements with parameters are not supported inside a SqlUnit.");
                cmd.CommandText = $"EXEC {MerchantDb}.sys.sp_executesql @stmt";
                cmd.Parameters.Add("@stmt", SqlDbType.NVarChar, -1).Value = sql;
            }
            return cmd;
        }

        /// <summary>Dapper access for ProcessQueryEntity (POS statements only run directly; Merchant through sp_executesql).</summary>
        public List<T> Query<T>(bool pos, string sql)
        {
            if (Failed)
                throw new InvalidOperationException("The transaction was already rolled back: " + Error?.Message);
            return pos
                ? Dapper.SqlMapper.Query<T>(connection, sql, null, transaction, true, 180, CommandType.Text).ToList()
                : Dapper.SqlMapper.Query<T>(connection, $"EXEC {MerchantDb}.sys.sp_executesql @stmt", new { stmt = sql }, transaction, true, 180, CommandType.Text).ToList();
        }

        /// <summary>Called by the Helper functions when a statement fails: roll back now and remember why.</summary>
        public void MarkFailed(Exception ex)
        {
            SqlUnit root = owner ? this : current.Value;
            if (root == null || root.Failed) return;
            root.Failed = true;
            root.Error = ex;
            try { root.transaction?.Rollback(); } catch { /* already rolled back by SQL Server */ }
        }

        /// <summary>Commits (with one audit row) unless a statement failed. Returns true when saved.</summary>
        public bool Complete(string summary = null, object before = null, object after = null)
        {
            if (!owner) return !(current.Value?.Failed ?? false);
            if (done) return !Failed;
            done = true;
            string sqlText = AuditLog.EndCapture();
            if (Failed)
                return false;
            try
            {
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.Transaction = transaction;
                    string number = null;
                    if (!string.IsNullOrWhiteSpace(saleReceiptID))
                    {
                        cmd.CommandText = "select SaleReceiptNumber from SaleReceipts where SaleReceiptID = @id";
                        cmd.Parameters.AddWithValue("@id", saleReceiptID);
                        number = Convert.ToString(cmd.ExecuteScalar());
                        cmd.Parameters.Clear();
                    }
                    if (!string.IsNullOrWhiteSpace(sqlText))
                    {
                        int statements = sqlText.Split(new[] { "-- [" }, StringSplitOptions.None).Length - 1;
                        AuditLog.Write(cmd, action, saleReceiptID, number,
                            summary ?? $"{action}" + (string.IsNullOrWhiteSpace(number) ? "" : $" - ticket #{number}") + $" - {statements} statement{(statements == 1 ? "" : "s")} (see SqlText)",
                            before, after, sqlText);
                    }
                }
                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                MarkFailed(ex);
                BLogicLogger.Exception("SqlUnit.Complete " + action, ex);
                return false;
            }
        }

        public void Dispose()
        {
            if (!owner) return;
            if (!done)
            {
                AuditLog.EndCapture();
                try { if (!Failed) transaction?.Rollback(); } catch { }
            }
            transaction?.Dispose();
            connection?.Dispose();
            if (current.Value == this) current.Value = null;
        }
    }

    /// <summary>
    /// Records every data change the tool makes in dbo.UpdateToolAuditLog of the POS database.
    /// The table belongs to this tool only (the POS UpdateScript is not involved): it is created on start-up
    /// when missing, otherwise rows are just inserted.
    ///
    /// Two ways in:
    ///  - <see cref="Write"/>: one complete row (summary, before/after JSON, all SQL) written inside the caller's
    ///    transaction, so the change and its audit row are committed or rolled back together
    ///    (Refund items, Add special item). Statements run in between are gathered with
    ///    <see cref="BeginCapture"/> / <see cref="Capture"/> / <see cref="EndCapture"/>.
    ///  - <see cref="Statement"/>: one row per executed statement, for the older functions that run statements
    ///    one by one. Written on its own connection; a failure is only logged, it never breaks the function.
    /// </summary>
    public static class AuditLog
    {
        public const string TableName = "UpdateToolAuditLog";

        /// <summary>False when the table could not be created (e.g. no permission); audit rows are then skipped.</summary>
        public static bool Enabled { get; private set; }
        public static string LastError { get; private set; }

        private const string CreateTableSql = @"
IF OBJECT_ID(N'dbo.UpdateToolAuditLog', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.UpdateToolAuditLog
    (
        AuditID           bigint IDENTITY(1,1) NOT NULL CONSTRAINT PK_UpdateToolAuditLog PRIMARY KEY,
        CreatedAt         datetime       NOT NULL CONSTRAINT DF_UpdateToolAuditLog_CreatedAt DEFAULT (GETDATE()),
        MachineName       nvarchar(100)  NULL,
        WindowsUser       nvarchar(150)  NULL,
        ToolVersion       nvarchar(50)   NULL,
        Action            nvarchar(150)  NOT NULL,
        DatabaseName      nvarchar(128)  NULL,
        SaleReceiptID     nvarchar(50)   NULL,
        SaleReceiptNumber nvarchar(50)   NULL,
        Summary           nvarchar(1000) NULL,
        BeforeJson        nvarchar(max)  NULL,
        AfterJson         nvarchar(max)  NULL,
        SqlText           nvarchar(max)  NULL
    );
    CREATE INDEX IX_UpdateToolAuditLog_SaleReceiptID ON dbo.UpdateToolAuditLog (SaleReceiptID, CreatedAt);
    CREATE INDEX IX_UpdateToolAuditLog_CreatedAt ON dbo.UpdateToolAuditLog (CreatedAt);
END";

        /// <summary>Creates dbo.UpdateToolAuditLog in the POS database when it does not exist yet.</summary>
        public static bool EnsureTable(string posConnectionString)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(posConnectionString))
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    cmd.CommandText = CreateTableSql;
                    cmd.ExecuteNonQuery();
                }
                Enabled = true;
                LastError = null;
            }
            catch (Exception ex)
            {
                Enabled = false;
                LastError = ex.Message;
                BLogicLogger.Exception("AuditLog.EnsureTable", ex);
            }
            return Enabled;
        }

        // ---- gathering the statements of one complete operation -------------------------------------------

        // AsyncLocal (not ThreadStatic): the statements of one operation are gathered even across awaits.
        private static readonly System.Threading.AsyncLocal<List<string>> captured = new System.Threading.AsyncLocal<List<string>>();

        public static bool IsCapturing => captured.Value != null;

        public static void BeginCapture() => captured.Value = new List<string>();

        public static string EndCapture()
        {
            List<string> list = captured.Value;
            captured.Value = null;
            return list == null || list.Count == 0 ? null : string.Join(Environment.NewLine + Environment.NewLine, list);
        }

        public static void Capture(string databaseName, string sql, IEnumerable<SqlParameter> parameters = null)
        {
            captured.Value?.Add(Describe(databaseName, sql, parameters));
        }

        /// <summary>
        /// One audit row for one user action of the older functions (Void, Add tip, Update service charges...):
        /// every statement run inside the scope is gathered and written as a single row when the scope ends.
        /// Use with <c>using (AuditLog.Group("Void ticket", saleReceiptID)) { ... }</c>. Nested scopes join the outer one.
        /// </summary>
        public static IDisposable Group(string action, string saleReceiptID = null, string saleReceiptNumber = null, bool singleTicket = true)
        {
            return new GroupScope(action, saleReceiptID, saleReceiptNumber, singleTicket);
        }

        private sealed class GroupScope : IDisposable
        {
            private readonly string action, saleReceiptID, saleReceiptNumber;
            private readonly bool owner, singleTicket;
            private List<string> list;

            public GroupScope(string action, string saleReceiptID, string saleReceiptNumber, bool singleTicket)
            {
                this.singleTicket = singleTicket;
                this.action = action;
                this.saleReceiptID = saleReceiptID;
                this.saleReceiptNumber = saleReceiptNumber;
                owner = !IsCapturing;
                if (owner)
                {
                    BeginCapture();
                    list = captured.Value;
                }
            }

            public void Dispose()
            {
                if (!owner || list == null) return;
                List<string> statements = list;
                list = null;
                if (captured.Value == statements) captured.Value = null;
                WriteGroup(action, saleReceiptID, saleReceiptNumber, statements, singleTicket);
            }
        }

        /// <summary>Writes the gathered statements of one action as one row (own connection; never throws).</summary>
        private static void WriteGroup(string action, string saleReceiptID, string saleReceiptNumber, List<string> statements, bool singleTicket)
        {
            if (!Enabled || statements == null || statements.Count == 0 || string.IsNullOrWhiteSpace(Helper.BLOGICPOS7_Connection))
                return;
            try
            {
                string sqlText = string.Join(Environment.NewLine + Environment.NewLine, statements);
                // Actions over many tickets (Advanced tools) are not pinned to the first ticket that appears in the SQL.
                string ticket = !string.IsNullOrWhiteSpace(saleReceiptID) ? saleReceiptID : singleTicket ? FindSaleReceiptID(sqlText, null) : null;
                using (SqlConnection conn = new SqlConnection(Helper.BLOGICPOS7_Connection))
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    string number = saleReceiptNumber;
                    if (string.IsNullOrWhiteSpace(number) && !string.IsNullOrWhiteSpace(ticket))
                    {
                        cmd.CommandText = "select SaleReceiptNumber from SaleReceipts where SaleReceiptID = @id";
                        cmd.Parameters.AddWithValue("@id", ticket);
                        number = Convert.ToString(cmd.ExecuteScalar());
                        cmd.Parameters.Clear();
                    }
                    string summary = $"{action}" + (string.IsNullOrWhiteSpace(number) ? "" : $" - ticket #{number}") +
                        $" - {statements.Count} statement{(statements.Count == 1 ? "" : "s")} (see SqlText)";
                    Insert(cmd, action, conn.Database, ticket, number, summary, null, null, sqlText);
                }
            }
            catch (Exception ex)
            {
                BLogicLogger.Exception("AuditLog.WriteGroup", ex, new List<object> { action });
            }
        }

        /// <summary>Readable action name for a ProcessForm process (the enum name when there is none).</summary>
        public static string ActionName(string processName)
        {
            switch (processName)
            {
                case "CloseReceipt": return "Close ticket";
                case "VoidReceipt": return "Void ticket";
                case "CancelReceipt": return "Cancel ticket";
                case "RefundTicket":
                case "SaleReceipt_RefundFullAmount": return "Refund full amount";
                case "AddTipByTokenReceipt": return "Add tip by sale token";
                case "AddTipForPaymentDetail": return "Add tip to payment";
                case "UpdateServiceChargeBySaleReceiptID": return "Update service charges";
                case "SettleBatchOffline": return "Settle offline batches";
                case "SettleBatchOfflineByMerchantID": return "Settle offline batch";
                case "ProcessSyncRoleForEmployee": return "Sync employee role";
                case "ReSyncItemType": return "Re-sync item types";
                case "UpdateShift": return "Update shift";
                case "HotFix_UpgradeTaxForTicket":
                case "HotFix_UpdownTaxForTicket": return "Hot fix: ticket tax";
                default: return processName;
            }
        }

        // ---- complete row, inside the caller's transaction ---------------------------------------------------

        /// <summary>Writes one audit row on the caller's POS connection/transaction. Throws on failure (the caller rolls back).</summary>
        public static void Write(SqlCommand posCommand, string action, object saleReceiptID, object saleReceiptNumber,
            string summary, object before, object after, string sqlText)
        {
            if (!Enabled) return;
            using (SqlCommand cmd = posCommand.Connection.CreateCommand())
            {
                cmd.Transaction = posCommand.Transaction;
                Insert(cmd, action, posCommand.Connection.Database, saleReceiptID?.ToString(), saleReceiptNumber?.ToString(),
                    summary, ToJson(before), ToJson(after), sqlText);
            }
        }

        // ---- one row per statement, for the older functions --------------------------------------------------

        /// <summary>
        /// Records one executed statement. Inside a capture it is only gathered; otherwise it is written on its own
        /// connection. Never throws.
        /// </summary>
        public static void Statement(string databaseName, string sql, IEnumerable<SqlParameter> parameters = null,
            string action = null, string saleReceiptID = null)
        {
            if (IsCapturing)
            {
                Capture(databaseName, sql, parameters);
                return;
            }
            if (!Enabled || string.IsNullOrWhiteSpace(Helper.BLOGICPOS7_Connection))
                return;

            try
            {
                using (SqlConnection conn = new SqlConnection(Helper.BLOGICPOS7_Connection))
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    string ticket = FindSaleReceiptID(sql, parameters);
                    if (string.IsNullOrWhiteSpace(ticket)) ticket = string.IsNullOrWhiteSpace(saleReceiptID) ? null : saleReceiptID;
                    Insert(cmd, string.IsNullOrWhiteSpace(action) ? CallerName() : action, databaseName,
                        ticket, null, null, null, null, Describe(databaseName, sql, parameters));
                }
            }
            catch (Exception ex)
            {
                BLogicLogger.Exception("AuditLog.Statement", ex, new List<object> { sql });
            }
        }

        // ---- helpers ------------------------------------------------------------------------------------------

        private static void Insert(SqlCommand cmd, string action, string databaseName, string saleReceiptID, string saleReceiptNumber,
            string summary, string beforeJson, string afterJson, string sqlText)
        {
            cmd.CommandText = "insert into dbo.UpdateToolAuditLog (MachineName, WindowsUser, ToolVersion, Action, DatabaseName, SaleReceiptID, " +
                "SaleReceiptNumber, Summary, BeforeJson, AfterJson, SqlText) values (@machine, @user, @version, @action, @db, @sid, @number, " +
                "@summary, @before, @after, @sql)";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@machine", SqlDbType.NVarChar, 100).Value = Clip(Environment.MachineName, 100);
            cmd.Parameters.Add("@user", SqlDbType.NVarChar, 150).Value = Clip(Environment.UserDomainName + "\\" + Environment.UserName, 150);
            cmd.Parameters.Add("@version", SqlDbType.NVarChar, 50).Value = Clip(Helper.VersionText, 50);
            cmd.Parameters.Add("@action", SqlDbType.NVarChar, 150).Value = Clip(action, 150) ?? "Unknown";
            cmd.Parameters.Add("@db", SqlDbType.NVarChar, 128).Value = (object)Clip(databaseName, 128) ?? DBNull.Value;
            cmd.Parameters.Add("@sid", SqlDbType.NVarChar, 50).Value = (object)Clip(saleReceiptID, 50) ?? DBNull.Value;
            cmd.Parameters.Add("@number", SqlDbType.NVarChar, 50).Value = (object)Clip(saleReceiptNumber, 50) ?? DBNull.Value;
            cmd.Parameters.Add("@summary", SqlDbType.NVarChar, 1000).Value = (object)Clip(summary, 1000) ?? DBNull.Value;
            cmd.Parameters.Add("@before", SqlDbType.NVarChar, -1).Value = (object)beforeJson ?? DBNull.Value;
            cmd.Parameters.Add("@after", SqlDbType.NVarChar, -1).Value = (object)afterJson ?? DBNull.Value;
            cmd.Parameters.Add("@sql", SqlDbType.NVarChar, -1).Value = (object)sqlText ?? DBNull.Value;
            cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
        }

        private static string Describe(string databaseName, string sql, IEnumerable<SqlParameter> parameters)
        {
            string text = $"-- [{databaseName}]{Environment.NewLine}{sql?.Trim()}";
            List<SqlParameter> list = parameters?.ToList();
            if (list != null && list.Count > 0)
                text += Environment.NewLine + "-- " + string.Join(", ", list.Select(p => p.ParameterName + " = " + FormatValue(p.Value)));
            return text;
        }

        private static string FormatValue(object value)
        {
            if (value == null || value == DBNull.Value) return "NULL";
            if (value is DateTime d) return d.ToString("yyyy-MM-dd HH:mm:ss");
            if (value is byte[]) return "<binary>";
            string s = Convert.ToString(value, System.Globalization.CultureInfo.InvariantCulture);
            return s.Length > 200 ? s.Substring(0, 200) + "…" : s;
        }

        private static readonly Regex SaleReceiptIdPattern = new Regex(@"SaleReceiptID\s*=\s*'([0-9a-fA-F]{8}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{12})'",
            RegexOptions.IgnoreCase | RegexOptions.Compiled);

        /// <summary>Picks the ticket from "SaleReceiptID = '...'" in the statement (or from a @SaleReceiptID parameter).</summary>
        private static string FindSaleReceiptID(string sql, IEnumerable<SqlParameter> parameters)
        {
            Match m = SaleReceiptIdPattern.Match(sql ?? "");
            if (m.Success) return m.Groups[1].Value;
            SqlParameter p = parameters?.FirstOrDefault(x => x.ParameterName.TrimStart('@').Equals("SaleReceiptID", StringComparison.OrdinalIgnoreCase));
            return p?.Value?.ToString();
        }

        /// <summary>"Type.Method" of the first caller outside the SQL helpers (async state machines resolved to their method).</summary>
        private static string CallerName()
        {
            try
            {
                foreach (StackFrame frame in new StackTrace(2, false).GetFrames() ?? new StackFrame[0])
                {
                    MethodBase method = frame.GetMethod();
                    Type type = method?.DeclaringType;
                    if (type == null || type == typeof(AuditLog)) continue;
                    string name = method.Name;
                    // async/iterator: <BtnSave_Click>d__12.MoveNext -> BtnSave_Click on the outer type
                    if (type.Name.StartsWith("<") && type.DeclaringType != null)
                    {
                        name = type.Name.Substring(1, type.Name.IndexOf('>') - 1);
                        type = type.DeclaringType;
                    }
                    if (name.StartsWith("RunQueryUpdate") || name.StartsWith("ExecuteLogged") || name.StartsWith("<"))
                        continue;
                    if (!type.Namespace?.StartsWith("ThirdPartyTool") ?? true)
                        continue;
                    return type.Name + "." + name;
                }
            }
            catch
            {
                // naming the caller is best effort
            }
            return "ThirdPartyTool";
        }

        /// <summary>Dapper parameter object (anonymous type / DynamicParameters) as a "-- a = 1, b = 2" line.</summary>
        public static string DapperParams(object parameters)
        {
            if (parameters == null) return "";
            try
            {
                IEnumerable<string> pairs;
                if (parameters is Dapper.DynamicParameters dynamicParameters)
                    pairs = dynamicParameters.ParameterNames.Select(n => n + " = " + FormatValue(dynamicParameters.Get<object>(n)));
                else
                    pairs = parameters.GetType().GetProperties().Select(p => p.Name + " = " + FormatValue(p.GetValue(parameters, null)));
                string line = string.Join(", ", pairs);
                return line.Length == 0 ? "" : Environment.NewLine + "-- " + line;
            }
            catch
            {
                return "";
            }
        }

        private static string ToJson(object value)
        {
            if (value == null) return null;
            if (value is string s) return s;
            return Newtonsoft.Json.JsonConvert.SerializeObject(value, Newtonsoft.Json.Formatting.Indented);
        }

        private static string Clip(string value, int max)
        {
            if (value == null) return null;
            return value.Length <= max ? value : value.Substring(0, max);
        }

        /// <summary>DataRow -> dictionary of the given columns (only those the table has), for before/after JSON.</summary>
        public static Dictionary<string, object> Pick(DataRow row, params string[] columns)
        {
            if (row == null) return null;
            Dictionary<string, object> result = new Dictionary<string, object>();
            foreach (string column in columns)
                if (row.Table.Columns.Contains(column))
                    result[column] = row[column] == DBNull.Value ? null : row[column];
            return result;
        }
    }
}
