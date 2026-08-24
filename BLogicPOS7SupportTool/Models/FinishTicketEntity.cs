using System;

namespace BLogicPOS7SupportTool.Models
{
    // The six "amount mode" options from the old FinishTicketWithPaymentDetailForm.
    enum FinishAmountMode
    {
        Base,
        BaseNoTax,
        IncludeSVCFee,
        IncludeSVCFeeNoTax,
        IncludeCashDiscount,
        IncludeCashDiscountNoTax
    }

    // Amounts shown for each mode (the remaining balance to finish).
    class FinishTicketEntity
    {
        public bool Found { get; set; }
        public string SaleReceiptNumber { get; set; }
        public decimal Base { get; set; }
        public decimal BaseNoTax { get; set; }
        public decimal IncludeSVCFee { get; set; }
        public decimal IncludeSVCFeeNoTax { get; set; }
        public decimal IncludeCashDiscount { get; set; }
        public decimal IncludeCashDiscountNoTax { get; set; }

        public decimal AmountFor(FinishAmountMode mode)
        {
            switch (mode)
            {
                case FinishAmountMode.BaseNoTax: return BaseNoTax;
                case FinishAmountMode.IncludeSVCFee: return IncludeSVCFee;
                case FinishAmountMode.IncludeSVCFeeNoTax: return IncludeSVCFeeNoTax;
                case FinishAmountMode.IncludeCashDiscount: return IncludeCashDiscount;
                case FinishAmountMode.IncludeCashDiscountNoTax: return IncludeCashDiscountNoTax;
                default: return Base;
            }
        }
    }

    class PaymentMethodOption
    {
        public int PaymentMethodID { get; set; }
        public string PaymentMethodName { get; set; }
        public int CashAdjust { get; set; }
        public override string ToString() => PaymentMethodName;
    }

    class BatchOption
    {
        public string BatchUnique { get; set; }
        public string MerchantID { get; set; }
        public string BatchID { get; set; }
        public DateTime DateCreated { get; set; }
        public decimal Total { get; set; }
        public override string ToString()
            => $"Batch {BatchID} — {DateCreated:M/d/yyyy} (${Total:F2})";
    }

    class FinishTicketRequest
    {
        public int PaymentMethodID { get; set; }
        public FinishAmountMode Mode { get; set; }
        public decimal Gratuity { get; set; }
        public DateTime CreatedDate { get; set; }

        // Credit-card fields (PaymentMethodID == 2)
        public string CardNumber { get; set; }
        public string CardType { get; set; }
        public string ExpiryDate { get; set; }
        public string AuthCode { get; set; }
        public string ReferenceNumber { get; set; }
        public string Note { get; set; }

        public string BatchUnique { get; set; }
        public string MerchantID { get; set; }
        public string BatchID { get; set; }
    }
}
