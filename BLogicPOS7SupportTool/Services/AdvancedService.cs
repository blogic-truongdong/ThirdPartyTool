using BLogicPOS7SupportTool.Core;
using System;
using System.Globalization;

namespace BLogicPOS7SupportTool.Services
{
    // Advanced / maintenance operations exposed on the Advanced page.
    class AdvancedService
    {
        // Closes open ("OPER") sale receipts that were never paid (PaidAmount = 0)
        // within the given date range: sets TransactionStatusID = 'CLO'.
        // The connection already targets the configured BLOGICPOS7 database, so the
        // table is referenced without a database prefix (same as the other services).
        // Returns the number of rows updated, or -1 when the statement failed.
        public int ClearSalesData(DateTime startDate, DateTime endDate)
        {
            var inv = CultureInfo.InvariantCulture;
            string start = startDate.ToString("yyyy-MM-dd HH:mm:ss", inv);
            string end = endDate.ToString("yyyy-MM-dd HH:mm:ss", inv);

            string sql = $@"
                UPDATE dbo.SaleReceipts
                SET TransactionStatusID = 'CLO'
                WHERE TransactionStatusID = 'OPER'
                  AND DateCreated BETWEEN '{start}' AND '{end}'
                  AND PaidAmount = '0'";

            int rows = AppSession.ProcessNonQuery(true, sql);
            AppLogger.Info($"ClearSalesData({start} .. {end}): {rows} row(s) closed");
            return rows;
        }
    }
}
