using BLogicPOS7SupportTool.Core;
using System;
using System.Data;
using System.Globalization;

namespace BLogicPOS7SupportTool.Services
{
    // Cash In / Cash Out entries live in dbo.DropPayout. Within the Type IN (2,3)
    // set the app filters on, Type 3 = Cash In and Type 2 = Cash Out.
    class CashInOutService
    {
        public const short TypeCashIn = 3;
        public const short TypeCashOut = 2;

        // Cash In / Out entries created within [startDate .. endDate] (whole days,
        // inclusive). Joins Employees for the display name.
        public DataTable GetCashInOuts(DateTime startDate, DateTime endDate)
        {
            string start = startDate.ToString("yyyy-MM-dd") + " 00:00:00";
            string end = endDate.ToString("yyyy-MM-dd") + " 23:59:59";

            string sql = $@"
                SELECT a.DropPayoutID,
                       a.Type,
                       (CASE WHEN a.Type = '3' THEN 'Cash In' ELSE 'Cash Out' END) AS TypeName,
                       (b.EmployeeFirstName + ' ' + b.EmployeeLastName) AS Employee,
                       a.Amount,
                       a.Comment
                FROM dbo.DropPayout a
                LEFT JOIN dbo.Employees b ON a.EmployeeID = b.EmployeeID
                WHERE a.Type IN ('2','3')
                  AND a.DateCreated BETWEEN '{start}' AND '{end}'
                ORDER BY a.DateCreated DESC";

            var dt = AppSession.ProcessQuery(true, sql, "CashInOut");
            AppLogger.Debug($"GetCashInOuts({start} -> {end}): {dt.Rows.Count} rows");
            return dt;
        }

        // Updates the type / amount / comment of a single Cash In-Out entry.
        // Returns true when a row was actually changed.
        public bool UpdateCashInOut(string dropPayoutId, short type, decimal amount, string comment)
        {
            var inv = CultureInfo.InvariantCulture;
            string safeId = (dropPayoutId ?? "").Replace("'", "''");
            string safeComment = (comment ?? "").Replace("'", "''");

            string sql = $@"
                UPDATE dbo.DropPayout
                SET Type = {type.ToString(inv)},
                    Amount = {amount.ToString(inv)},
                    Comment = '{safeComment}'
                WHERE DropPayoutID = '{safeId}'";

            int rows = AppSession.ProcessNonQuery(true, sql);
            AppLogger.Info($"UpdateCashInOut(DropPayoutID={dropPayoutId}, Type={type}, Amount={amount.ToString(inv)}): {rows} row(s)");
            return rows > 0;
        }
    }
}
