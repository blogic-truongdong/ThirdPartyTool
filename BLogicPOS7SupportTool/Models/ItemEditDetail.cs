namespace BLogicPOS7SupportTool.Models
{
    // Values entered on the Item Detail edit form (Edit Items), passed to
    // ReceiptService.EditTicketItemDetail. Derived amounts (gross, total discount,
    // ExtendedPrice, TaxAmount, stored TaxRate scale) are computed in the service.
    class ItemEditDetail
    {
        public string SaleReceiptDetailID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public double Quantity { get; set; }
        public decimal TaxAmount { get; set; }        // TAX amount (USD) entered on the form
        public ItemEditStatus Status { get; set; }
        public int ReasonCodeId { get; set; }        // ReturnCode written for Cancel/Refund/Comp (0 = none)

        // Discount amounts — mapping to SaleReceiptDetails columns:
        public decimal ItemDiscount { get; set; }          // ItemDiscountValue
        public decimal TicketDiscount { get; set; }        // TotalSaleDiscountAmount
        public decimal StoreDiscount { get; set; }         // StoreDiscountValue
        public decimal CategoryDiscount { get; set; }      // DiscountAmount
        public decimal CustomerDiscount { get; set; }      // TotalSaleDiscountAmountByCustomer
        public decimal CustomerLevelDiscount { get; set; } // ItemDiscountByPriceLevelAmount
    }
}
