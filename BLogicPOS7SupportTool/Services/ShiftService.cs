using BLogicPOS7SupportTool.Core;
using System;
using System.Data;
using System.Globalization;

namespace BLogicPOS7SupportTool.Services
{
    class ShiftService
    {
        // Shifts opened within [startDate .. endDate] (whole days, inclusive).
        // Joins Workstations for the display name and skips the special/system
        // GUID rows the POS keeps in the table (matches the old tool's ViewShifts).
        public DataTable GetShifts(DateTime startDate, DateTime endDate)
        {
            string start = startDate.ToString("yyyy-MM-dd") + " 00:00:00";
            string end = endDate.ToString("yyyy-MM-dd") + " 23:59:59";

            string sql = $@"
                SELECT a.ShiftID, b.WorkstationName, a.OpeningTime, a.ClosingTime,
                       a.OpeningAmount, a.ClosingAmount
                FROM dbo.Shift a
                LEFT JOIN dbo.Workstations b ON a.WorkstationID = b.WorkstationID
                WHERE a.Status = 0
                  AND a.OpeningTime BETWEEN '{start}' AND '{end}'
                  AND a.ShiftID NOT IN ('33333333-3333-3333-3333-333333333333','00000000-0000-0000-0000-000000000000')
                  AND a.WorkstationID NOT IN ('33333333-3333-3333-3333-333333333333','00000000-0000-0000-0000-000000000000','44444444-4444-4444-4444-444444444444')
                ORDER BY a.ClosingTime DESC";

            var dt = AppSession.ProcessQuery(true, sql, "Shifts");
            AppLogger.Debug($"GetShifts({start} -> {end}): {dt.Rows.Count} rows");
            return dt;
        }

        // Updates the opening/closing cash amounts of a single shift.
        // Returns true when a row was actually changed.
        public bool UpdateShift(string shiftId, decimal openingAmount, decimal closingAmount)
        {
            var inv = CultureInfo.InvariantCulture;
            string safeId = (shiftId ?? "").Replace("'", "''");

            string sql = $@"
                UPDATE dbo.Shift
                SET OpeningAmount = {openingAmount.ToString(inv)},
                    ClosingAmount = {closingAmount.ToString(inv)}
                WHERE ShiftID = '{safeId}'";

            int rows = AppSession.ProcessNonQuery(true, sql);
            AppLogger.Info($"UpdateShift(ShiftID={shiftId}, Opening={openingAmount.ToString(inv)}, Closing={closingAmount.ToString(inv)}): {rows} row(s)");
            return rows > 0;
        }
    }
}
