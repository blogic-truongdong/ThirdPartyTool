namespace BLogicPOS7SupportTool.Models
{
    class TokenReceiptInfo
    {
        public bool Found { get; set; }
        public string SaleReceiptID { get; set; }
        public string SaleReceiptNumber { get; set; }
        public string TransactionStatusID { get; set; }
        public decimal TipAmount { get; set; }
        public bool HasPayments { get; set; }
    }
}
