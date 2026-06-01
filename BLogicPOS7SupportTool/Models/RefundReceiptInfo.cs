namespace BLogicPOS7SupportTool.Models
{
    // Kết quả tra cứu một ticket Refund (RET) để merge payment sang ticket FIN.
    class RefundReceiptInfo
    {
        public bool Found { get; set; }
        public string SaleReceiptID { get; set; }
        public string SaleReceiptNumber { get; set; }
        public string TransactionStatusID { get; set; }
        // Số tiền refund sẽ được merge (âm) = -SUM(ABS(AmountReturn)) của các payment detail.
        public decimal RefundAmount { get; set; }
        public bool HasPayments { get; set; }
    }
}
