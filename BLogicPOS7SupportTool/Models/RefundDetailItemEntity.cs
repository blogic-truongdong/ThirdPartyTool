namespace BLogicPOS7SupportTool.Models
{
    class RefundDetailItemEntity
    {
        public string SaleReceiptDetailID { get; set; }
        public string GroupID { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public double Quantity { get; set; }
        public decimal ExtendedPrice { get; set; }
        public decimal TaxAmount { get; set; }
        public bool IsModifier { get; set; }

        // Extra fields used by the Item Detail edit form (Edit Items). TaxRate is the
        // raw stored value (scale preserved on save). The six discount amounts map to
        // the SaleReceiptDetails columns per the Item Detail form.
        public decimal TaxRate { get; set; }
        public decimal ItemDiscountValue { get; set; }               // ITEM DISCOUNT
        public decimal TotalSaleDiscountAmount { get; set; }         // TICKET DISCOUNT
        public decimal StoreDiscountValue { get; set; }              // STORE DISCOUNT
        public decimal DiscountAmount { get; set; }                  // CATEGORY DISCOUNT
        public decimal TotalSaleDiscountAmountByCustomer { get; set; } // CUSTOMER DISCOUNT
        public decimal ItemDiscountByPriceLevelAmount { get; set; }  // CUSTOMER LEVEL DISCOUNT

        // Sum of the six discount amounts (the ticket line's total discount).
        public decimal TotalDiscount =>
            ItemDiscountValue + TotalSaleDiscountAmount + StoreDiscountValue
            + DiscountAmount + TotalSaleDiscountAmountByCustomer + ItemDiscountByPriceLevelAmount;

        // Items and their modifiers share a GroupID; standalone rows fall back to
        // their own detail id so selecting one never toggles unrelated rows.
        public string GroupKey =>
            string.IsNullOrEmpty(GroupID) ? "ID:" + SaleReceiptDetailID : GroupID;
    }
}
