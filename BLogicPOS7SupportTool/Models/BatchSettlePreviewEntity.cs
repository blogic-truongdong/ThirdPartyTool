using System;
using System.Collections.Generic;

namespace BLogicPOS7SupportTool.Models
{
    // The Batch row an open merchant batch would be settled into, worked out by
    // BatchService.PreviewBatch and shown to the user before anything is written.
    class BatchSettlePreviewEntity
    {
        public string MerchantID { get; set; }
        public string BatchID { get; set; }
        public DateTime FirstDate { get; set; }
        public DateTime Date { get; set; }
        public int TransactionCount { get; set; }
        public string WorkStationName { get; set; } = "";
        public decimal Total { get; set; }
        public decimal TotalSales { get; set; }
        public decimal TotalReturns { get; set; }
        public decimal TotalTips { get; set; }
        public decimal TotalSVCFeeAmount { get; set; }
        public decimal TotalSVCFeeForTip { get; set; }
        public decimal MerchantFeeAmount { get; set; }
        public decimal MerchantFeePercent { get; set; }
        public decimal DepositAmount { get; set; }

        // Settlement bookkeeping, not shown: every open row the preview was built from
        // (settling refuses when the batch changed since), the stored per-row fees and the
        // fee corrections to apply afterwards.
        public int OpenRowCount { get; set; }
        public decimal RowMerchantFee { get; set; }
        public bool IsReCalculateMerchantFee { get; set; }
        public List<KeyValuePair<string, decimal>> FeeAdjustments { get; } = new List<KeyValuePair<string, decimal>>();
    }
}
