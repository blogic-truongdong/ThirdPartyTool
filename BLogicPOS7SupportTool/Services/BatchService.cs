using BLogicPOS7SupportTool.Core;
using BLogicPOS7SupportTool.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;

namespace BLogicPOS7SupportTool.Services
{
    // Settles merchant batches the terminal never closed (TransactionDetail rows
    // without a BatchUnique). Ported from ThirdPartyTool SettleBatchOfflineView +
    // ProcessForm.SettleBatchOfflineByMerchantID / AddBatch / ProcessBatchCloseLocal.
    class BatchService
    {
        // Transactions of a batch the settlement leaves alone: rows of the "no
        // workstation" placeholder and gift cards (ProcessBatchCloseLocal never
        // closes those, so counting them would keep re-creating a batch for them).
        private const string OpenTransactionFilter = @"
                ISNULL(BatchUnique, '') = ''
                AND WorkstationID <> '00000000-0000-0000-0000-000000000000'
                AND ISNULL(TenderType, '') <> 'GiftCard'";

        // One row per open (MerchantID, BatchID). The old view grouped by
        // MerchantID only and joined the BatchIDs into "1, 18", so its per-row
        // settle matched no transaction as soon as a merchant had two open batches.
        public DataTable GetOpenBatches()
        {
            string sql = $@"
                SELECT
                    MerchantID,
                    BatchID,
                    MAX(WorkstationName) AS WorkstationName,
                    MAX(TerminalIpAddress) AS TerminalIpAddress,
                    COUNT(ID) AS CountTicket,
                    SUM(CASE WHEN Result = 'Approved' AND (TranType = 'Sale' OR TranType = 'PreAuthCapture') AND TranStatus = 'ACTIVE' THEN Total ELSE 0 END)
                      - SUM(CASE WHEN Result = 'Approved' AND TranType = 'Return' THEN Total ELSE 0 END) AS Total,
                    MIN([Date]) AS FirstDate,
                    MAX([Date]) AS LastDate
                FROM dbo.TransactionDetail
                WHERE {OpenTransactionFilter}
                GROUP BY MerchantID, BatchID
                ORDER BY MerchantID, MIN([Date])";

            var dt = AppSession.ProcessQuery(false, sql, "Batch");
            AppLogger.Debug($"GetOpenBatches: {dt.Rows.Count} rows");
            return dt;
        }

        // Settles every open batch. Returns the batches that could not be settled.
        public List<string> SettleAllBatches()
        {
            var failed = new List<string>();
            foreach (DataRow row in GetOpenBatches().Rows)
            {
                string merchantId = AppSession.GetString(row, "MerchantID");
                string batchId = AppSession.GetString(row, "BatchID");
                var preview = PreviewBatch(merchantId, batchId);
                if (preview == null || !SettleBatch(preview))
                    failed.Add($"Merchant {merchantId} / Batch {batchId}");
            }
            return failed;
        }

        // The Batch row one open batch would be settled into. Writes nothing. Null when the
        // batch has no open transaction left or none of them is approved.
        public BatchSettlePreviewEntity PreviewBatch(string merchantId, string batchId)
        {
            string safeMerchant = (merchantId ?? "").Replace("'", "''");
            string safeBatch = (batchId ?? "").Replace("'", "''");

            var dtTran = AppSession.ProcessQuery(false, $@"
                SELECT * FROM dbo.TransactionDetail
                WHERE MerchantID = '{safeMerchant}' AND BatchID = '{safeBatch}' AND {OpenTransactionFilter}", "TransactionDetail");
            if (dtTran.Rows.Count == 0)
            {
                AppLogger.Info($"PreviewBatch: no open transaction for MerchantID={merchantId}, BatchID={batchId}");
                return null;
            }

            var batch = BuildBatch(dtTran);
            if (batch == null)
            {
                AppLogger.Info($"PreviewBatch: no approved transaction for MerchantID={merchantId}, BatchID={batchId}");
                return null;
            }

            batch.MerchantID = merchantId;
            batch.BatchID = batchId;
            batch.OpenRowCount = dtTran.Rows.Count;
            return batch;
        }

        // Closes a previewed batch: adds its Batch row and stamps the transactions with the
        // new BatchUnique in one SQL transaction, then rebalances the merchant fee. Refuses
        // (returns false, nothing written) when the batch gained or lost an open transaction
        // since the preview, so what gets saved is exactly what the user confirmed.
        public bool SettleBatch(BatchSettlePreviewEntity batch)
        {
            string safeMerchant = (batch.MerchantID ?? "").Replace("'", "''");
            string safeBatch = (batch.BatchID ?? "").Replace("'", "''");

            var inv = CultureInfo.InvariantCulture;
            string batchUnique = Guid.NewGuid().ToString();
            string sql = $@"
                SET XACT_ABORT ON;
                BEGIN TRAN;

                IF (SELECT COUNT(*) FROM dbo.TransactionDetail WITH (UPDLOCK, HOLDLOCK)
                    WHERE MerchantID = '{safeMerchant}' AND BatchID = '{safeBatch}' AND {OpenTransactionFilter}) <> {batch.OpenRowCount}
                    THROW 50001, 'The batch changed since it was previewed.', 1;

                INSERT INTO dbo.Batch
                    ([Date], ID, Status, MerchantID, Quantity, Total, BatchUnique, TotalSVCFeeForTip, TotalSales,
                     TotalReturns, TotalTips, TotalSVCFeeAmount, DepositAmount, WorkStationName, MerchantFeeAmount, MerchantFeePercent)
                VALUES
                    ('{batch.Date.ToString("yyyy-MM-dd HH:mm:ss.fff", inv)}', '{safeBatch}', 'Closed', '{safeMerchant}', {batch.TransactionCount},
                     {batch.Total.ToString(inv)}, '{batchUnique}', {batch.TotalSVCFeeForTip.ToString(inv)}, {batch.TotalSales.ToString(inv)},
                     {batch.TotalReturns.ToString(inv)}, {batch.TotalTips.ToString(inv)}, {batch.TotalSVCFeeAmount.ToString(inv)},
                     {batch.DepositAmount.ToString(inv)}, N'{batch.WorkStationName.Replace("'", "''")}', {batch.MerchantFeeAmount.ToString(inv)},
                     {batch.MerchantFeePercent.ToString(inv)});

                UPDATE dbo.TransactionDetail WITH (ROWLOCK)
                SET BatchStatus = 'Closed', BatchTotal = {batch.Total.ToString(inv)}, BatchUnique = '{batchUnique}'
                WHERE MerchantID = '{safeMerchant}' AND BatchID = '{safeBatch}' AND {OpenTransactionFilter};

                COMMIT;";

            if (AppSession.ProcessNonQuery(false, sql) <= 0)
            {
                AppLogger.Error($"SettleBatch: closing MerchantID={batch.MerchantID}, BatchID={batch.BatchID} failed");
                return false;
            }

            ReCalculateMerchantFee(batchUnique, batch);
            AppLogger.Info($"SettleBatch: MerchantID={batch.MerchantID}, BatchID={batch.BatchID} closed as {batchUnique}");
            return true;
        }

        // Batch totals over the approved transactions, as the terminal's
        // BatchManagerGUI.AddBatch (Leaf_New MerchantGUI) adds them up. The fee is worked
        // out per group of transactions sharing one MerchantFeePercent, each group at its
        // own rate. Null when the batch holds no approved transaction.
        private BatchSettlePreviewEntity BuildBatch(DataTable dtTran)
        {
            var batch = new BatchSettlePreviewEntity();
            decimal svcFeeForRefund = 0m;
            decimal flatMerchantFee = 0m;
            var feePercents = new List<decimal>();
            var svcFeeTypes = new HashSet<string>();
            bool percentMode = AppSession.SVCFeeType == 1;

            // An older Merchant database has no TransactionDetail.MerchantFeePercent: every row
            // then counts as taken at the rate configured in Configurations.
            bool hasFeePercentColumn = dtTran.Columns.Contains("MerchantFeePercent");
            if (!hasFeePercentColumn)
                AppLogger.Info("BuildBatch: TransactionDetail has no MerchantFeePercent column, using CreditCardConfigValue.MerchantFee");

            var feeGroups = dtTran.Rows.Cast<DataRow>()
                .GroupBy(r => GetMerchantFeePercent(r, hasFeePercentColumn))
                .OrderBy(g => g.Key);

            foreach (var group in feeGroups)
            {
                decimal groupTotal = 0m, groupReturns = 0m, groupRowFee = 0m;

                foreach (DataRow dr in group)
                {
                    if (AppSession.GetString(dr, "Result") != "Approved")
                        continue;

                    string tranType = AppSession.GetString(dr, "TranType");
                    decimal total = AppSession.GetDecimal(dr, "Total");
                    decimal amount = AppSession.GetDecimal(dr, "Amount");
                    decimal svcFee = AppSession.GetDecimal(dr, "SVCFee");

                    if (!feePercents.Contains(group.Key))
                        feePercents.Add(group.Key);
                    svcFeeTypes.Add(AppSession.GetString(dr, "SVCFeeType"));
                    batch.TransactionCount++;

                    string workstationName = AppSession.GetString(dr, "WorkstationName");
                    if (!string.IsNullOrEmpty(workstationName))
                        batch.WorkStationName = workstationName;

                    if (tranType == "Sale" || tranType == "PreAuthCapture")
                    {
                        if (AppSession.GetString(dr, "TranStatus") != "ACTIVE")
                            continue;

                        batch.Total += total;
                        groupTotal += total;
                        batch.TotalSales += amount - svcFee;
                        batch.TotalTips += AppSession.GetDecimal(dr, "Gratuity");
                        batch.TotalSVCFeeAmount += svcFee;
                        batch.TotalSVCFeeForTip += AppSession.GetDecimal(dr, "SVCFeeForGratuity");
                        batch.RowMerchantFee += AppSession.GetDecimal(dr, "MerchantFee");
                        groupRowFee += AppSession.GetDecimal(dr, "MerchantFee");
                        flatMerchantFee += AppSession.MerchantFee;
                    }
                    else if (tranType == "Return")
                    {
                        svcFeeForRefund += svcFee;
                        batch.Total -= total;
                        groupTotal -= total;
                        batch.TotalReturns += amount;
                        groupReturns += amount;
                        batch.TotalSVCFeeAmount -= svcFee;
                    }
                }

                if (!percentMode)
                    continue;

                decimal groupFee = CalculateMerchantFee(groupTotal + groupReturns, group.Key);
                batch.MerchantFeeAmount += groupFee;

                // Rounding leftover between the group fee and its per-row fees goes onto the
                // latest sale of the group that can absorb it.
                decimal remaining = groupFee - groupRowFee;
                if (remaining != 0)
                {
                    var absorb = group
                        .Where(r => AppSession.GetString(r, "Result") == "Approved" &&
                                    (AppSession.GetString(r, "TranType") == "Sale" || AppSession.GetString(r, "TranType") == "PreAuthCapture") &&
                                    AppSession.GetString(r, "TranStatus") == "ACTIVE" &&
                                    AppSession.GetDecimal(r, "MerchantFee") >= Math.Abs(remaining))
                        .OrderByDescending(r => AppSession.GetDateTime(r, "Date"))
                        .FirstOrDefault();
                    if (absorb != null)
                        batch.FeeAdjustments.Add(new KeyValuePair<string, decimal>(AppSession.GetString(absorb, "ID"), remaining));
                }
            }

            if (batch.TransactionCount == 0)
                return null;

            batch.TotalReturns -= svcFeeForRefund;
            if (!percentMode)
            {
                // Flat fee per sale: the rows already carry it, nothing left to rebalance.
                batch.RowMerchantFee = flatMerchantFee;
                batch.MerchantFeeAmount = flatMerchantFee;
            }
            batch.DepositAmount = batch.Total - batch.MerchantFeeAmount;
            batch.FirstDate = dtTran.Rows.Cast<DataRow>().Min(r => AppSession.GetDateTime(r, "Date"));
            batch.Date = dtTran.Rows.Cast<DataRow>().Max(r => AppSession.GetDateTime(r, "Date"));

            // The terminal stores the SUM of the distinct rates here (5.5 + 9.09 = 14.59 for a
            // mixed batch). One rate is kept as is; a mixed batch records the configured rate.
            batch.MerchantFeePercent = feePercents.Count == 1 ? feePercents[0] : AppSession.MerchantFee;

            // The fee rate or SVC fee mode differs from the current config: the stored fees
            // were taken at another rate, have the procedure recompute them all.
            batch.IsReCalculateMerchantFee = feePercents.Count > 1 || svcFeeTypes.Count > 1 ||
                (feePercents.Count == 1 && feePercents[0] != AppSession.MerchantFee) ||
                (svcFeeTypes.Count == 1 && svcFeeTypes.First() != AppSession.SVCFeeType.ToString());

            return batch;
        }

        // The rate a transaction was taken at. A row without a stored rate (no column, or 0)
        // counts as taken at CreditCardConfigValue.MerchantFee, the same fallback
        // RE_CALCULATE_MERCHANT_FEE_FOR_TRANSACTIONS_BY_BATCH uses. The terminal charges such a
        // row at 0% instead and then pulls its whole stored fee off one sale.
        private static decimal GetMerchantFeePercent(DataRow row, bool hasFeePercentColumn)
        {
            decimal percent = hasFeePercentColumn ? AppSession.GetDecimal(row, "MerchantFeePercent") : 0m;
            return percent != 0 ? percent : AppSession.MerchantFee;
        }

        // CHelper.CalculateMerchantFeeWthiParams of the terminal, percent mode.
        private static decimal CalculateMerchantFee(decimal total, decimal rate)
        {
            if (total <= 0) return 0m;
            return AppSession.RoundUp(total * (rate / 100));
        }

        // Ported from BatchManagerGUI: the per-group leftovers first, then
        // ReCalculateMerchantFeeTransactionDetailByBatch. The service actions they called
        // are these two Merchant DB procedures.
        private void ReCalculateMerchantFee(string batchUnique, BatchSettlePreviewEntity batch)
        {
            var inv = CultureInfo.InvariantCulture;
            string svcFeeType = AppSession.SVCFeeType.ToString(inv);

            foreach (var adjustment in batch.FeeAdjustments)
                AppSession.ProcessNonQuery(false,
                    $"EXEC dbo.UPDATE_MERCHANT_FEE_FOR_TRANSACTIONDETAIL_BY_TRANS_ID '{adjustment.Key.Replace("'", "''")}', " +
                    $"{adjustment.Value.ToString(inv)}");

            if (AppSession.MerchantFee == 0 && batch.RowMerchantFee != 0)
            {
                AppSession.ProcessNonQuery(false,
                    $"EXEC dbo.RE_CALCULATE_MERCHANT_FEE_FOR_TRANSACTIONS_BY_BATCH '{batchUnique}', 0, 0, '{svcFeeType}'");
            }
            else if ((AppSession.MerchantFee != 0 && batch.RowMerchantFee == 0) || batch.IsReCalculateMerchantFee)
            {
                AppSession.ProcessNonQuery(false,
                    $"EXEC dbo.RE_CALCULATE_MERCHANT_FEE_FOR_TRANSACTIONS_BY_BATCH '{batchUnique}', " +
                    $"{AppSession.MerchantFee.ToString(inv)}, {AppSession.BaseAmountFee.ToString(inv)}, '{svcFeeType}'");
            }
        }
    }
}
