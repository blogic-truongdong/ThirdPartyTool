namespace BLogicPOS7SupportTool.Models
{
    // A row from dbo.Items shown in the Add Item picker (DeptDeptID = -2 items).
    class AddableItemEntity
    {
        public string ItemID { get; set; }
        public string ItemName { get; set; }
        public string LookupCode { get; set; }
        public decimal RegularPrice { get; set; }
        public string TaxCodeID { get; set; }
        public bool Taxable { get; set; }
    }
}
