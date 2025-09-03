using System;

namespace ThirdPartyTool.Models
{
    public class SaleReceiptDetailTaxDetailEntity
    {
        public Guid SaleReceiptDetailTaxDetailID { get; set; }

        public Guid SaleReceiptDetailID { get; set; }

        public Guid SaleReceiptID { get; set; }

        public Guid ItemID { get; set; }

        public double Quantity { get; set; }

        public string TaxCodeID { get; set; }

        public string GroupID { get; set; }

        public decimal TaxRate { get; set; }

        public decimal TaxAmount { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public decimal ExtendedPrice { get; set; }

        public string PriceInitial { get; set; }

        public int ReturnCode { get; set; }

        public string ReasonComment { get; set; }

        public short Status { get; set; }

        public int Range { get; set; }

        public DateTime LastModifyTime { get; set; }

        public int EmployeeID { get; set; }

        public string EmployeeName { get; set; }

        public bool IsCompItem { get; set; }

        public bool IsDeposit { get; set; }

        public int TaxType { get; set; }

        public Guid KDSIDDetail { get; set; }

    }


}
