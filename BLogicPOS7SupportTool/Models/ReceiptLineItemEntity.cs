namespace BLogicPOS7SupportTool.Models
{
    class ReceiptLineItemEntity
    {
        public string Description { get; set; }
        public double Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal ExtendedPrice { get; set; }
        public decimal DiscountAmount { get; set; }
        public bool IsCompItem { get; set; }
        public int Status { get; set; }
        public decimal TaxAmount { get; set; }
        public bool Taxable { get; set; }
        public bool IsModifier { get; set; }
    }
}
