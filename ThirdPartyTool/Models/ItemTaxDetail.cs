namespace ThirdPartyTool.Models
{
    public class ItemTaxDetail
    {
        public string ItemID { get; set; }
        public string TaxCodeID { get; set; }
        public decimal TaxRate { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal SubTotal { get; set; }
        public decimal ItemIndex { get; set; }
    }
}
