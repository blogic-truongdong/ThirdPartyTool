using BLogicPOS7SupportTool.Core;
using BLogicPOS7SupportTool.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Text;

namespace BLogicPOS7SupportTool.Services
{
    class TableService
    {
        // Active sitting areas, alphabetical.
        public List<TableArea> GetAreas()
        {
            const string sql = @"
                SELECT AreaID, AreaName
                FROM dbo.SittingAreas
                WHERE isEnable = '1'
                ORDER BY AreaName";

            var result = new List<TableArea>();
            var dt = AppSession.ProcessQuery(true, sql, "SittingAreas");
            foreach (DataRow row in dt.Rows)
                result.Add(new TableArea
                {
                    AreaID = row["AreaID"]?.ToString() ?? "",
                    AreaName = row["AreaName"]?.ToString() ?? ""
                });

            AppLogger.Debug($"GetAreas: {result.Count} rows");
            return result;
        }

        // Non-deleted tables in an area (optionally filtered by name), with a
        // friendly status label mapped from the TableStatus enum.
        public DataTable GetTables(string areaId, string search)
        {
            string safeArea = (areaId ?? "").Replace("'", "''");
            string where = string.IsNullOrWhiteSpace(search)
                ? ""
                : $" AND TableName LIKE '%{search.Replace("'", "''")}%'";

            string sql = $@"
                SELECT TableID, AreaID, TableName, Status,
                       (CASE Status
                            WHEN -2 THEN 'Unknown'
                            WHEN -1 THEN 'Reserved'
                            WHEN 0 THEN 'Available'
                            WHEN 1 THEN 'In Use'
                            WHEN 2 THEN 'In Use (Temp Bill)'
                            WHEN 3 THEN 'Seated'
                            WHEN 4 THEN 'Hold'
                            WHEN 5 THEN 'QR In Use'
                            ELSE CAST(Status AS varchar(10))
                        END) AS StatusName
                FROM dbo.Tables
                WHERE isDeleted = '0'
                  AND AreaID = '{safeArea}'{where}
                ORDER BY TableName";

            var dt = AppSession.ProcessQuery(true, sql, "Tables");
            AppLogger.Debug($"GetTables(area={areaId}, search='{search}'): {dt.Rows.Count} rows");
            return dt;
        }

        // Clears a stuck table: clones the whole row with a fresh TableID and
        // Status = 0 (Available), then soft-deletes the original. Any state that
        // was linked to the old TableID stays on the archived row, so the POS
        // sees a clean table. Returns true when the clone + archive succeeded.
        public bool ClearTable(string tableId)
        {
            string safeId = (tableId ?? "").Replace("'", "''");

            var dt = AppSession.ProcessQuery(true,
                $"SELECT * FROM dbo.Tables WHERE TableID = '{safeId}'", "Tables");
            if (dt.Rows.Count == 0)
                return false;

            string insertSql = BuildCloneInsert(dt);
            if (string.IsNullOrEmpty(insertSql))
                return false;

            // Insert the fresh copy first; only archive the old row if it worked.
            if (AppSession.ProcessNonQuery(true, insertSql) <= 0)
                return false;

            AppSession.ProcessNonQuery(true,
                $"UPDATE dbo.Tables SET isDeleted = '1' WHERE TableID = '{safeId}'");

            AppLogger.Info($"ClearTable: archived TableID={tableId}, inserted fresh copy");
            return true;
        }

        // Builds "INSERT INTO dbo.Tables ([col],...) VALUES (val,...)" from a single
        // fetched row, overriding TableID -> NEWID() and Status -> 0.
        private static string BuildCloneInsert(DataTable dt)
        {
            var row = dt.Rows[0];
            var cols = new StringBuilder();
            var vals = new StringBuilder();

            for (int i = 0; i < dt.Columns.Count; i++)
            {
                var col = dt.Columns[i];
                if (i > 0)
                {
                    cols.Append(", ");
                    vals.Append(", ");
                }
                cols.Append('[').Append(col.ColumnName).Append(']');
                vals.Append(FormatValue(col, row[col]));
            }

            return $"INSERT INTO dbo.Tables ({cols}) VALUES ({vals})";
        }

        private static string FormatValue(DataColumn col, object val)
        {
            if (col.ColumnName.Equals("TableID", StringComparison.OrdinalIgnoreCase))
                return "NEWID()";
            if (col.ColumnName.Equals("Status", StringComparison.OrdinalIgnoreCase))
                return "'0'";

            if (val == null || val == DBNull.Value)
                return "NULL";

            var inv = CultureInfo.InvariantCulture;
            switch (col.DataType.Name)
            {
                case "Boolean":
                    return Convert.ToBoolean(val) ? "'1'" : "'0'";
                case "DateTime":
                    return "'" + ((DateTime)val).ToString("yyyy-MM-dd HH:mm:ss.fff", inv) + "'";
                case "Decimal":
                    return ((decimal)val).ToString(inv);
                case "Double":
                    return ((double)val).ToString(inv);
                case "Single":
                    return ((float)val).ToString(inv);
                default:
                    return "'" + val.ToString().Replace("'", "''") + "'";
            }
        }
    }
}
