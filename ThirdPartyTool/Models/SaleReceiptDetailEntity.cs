using System;

namespace ThirdPartyTool.Models
{
    public class SaleReceiptDetailEntity
    {
        public Guid SaleReceiptDetailID { get; set; }

        public Guid SaleReceiptID { get; set; }

        public Guid ItemID { get; set; }

        public double Quantity { get; set; }

        public bool Taxable { get; set; }

        public string TaxCodeID { get; set; }

        public string GroupID { get; set; }

        public decimal TaxRate { get; set; }

        public byte TaxMethod { get; set; }

        public decimal TaxAmount { get; set; }

        public string Description { get; set; }

        public string Description2 { get; set; }

        public decimal Price { get; set; }

        public double MarkDown { get; set; }

        public decimal ItemUnitCost { get; set; }

        public decimal ExtendedPrice { get; set; }

        public string PriceInitial { get; set; }

        public string Lookup_Code { get; set; }

        public int SalePersonID { get; set; }

        public int ReturnCode { get; set; }

        public string ReasonComment { get; set; }

        public double DiscountPercent { get; set; }

        public decimal DiscountAmount { get; set; }

        public string Attribute { get; set; }

        public string Size { get; set; }

        public decimal TotalSaleDiscountAmount { get; set; }

        public double TotalSaleDiscountPercent { get; set; }

        public decimal TotalSaleDiscountAmountBySystem { get; set; }

        public double TotalSaleDiscountPercentBySystem { get; set; }

        public decimal TotalSaleDiscountAmountByCustomer { get; set; }

        public double TotalSaleDiscountPercentByCustomer { get; set; }

        public int DiscountCode { get; set; }

        public double QuantitySold { get; set; }

        public string RefSaleReceiptDetailID { get; set; }

        public short Status { get; set; }

        public bool InventoryUpdatable { get; set; }

        public bool isToGo { get; set; }

        public int PartIndex { get; set; }

        public int Range { get; set; }

        public decimal ServiceFeeAmount { get; set; }

        public decimal ServiceFeePercent { get; set; }

        public DateTime LastModifyTime { get; set; }

        public int EmployeeID { get; set; }

        public string EmployeeName { get; set; }

        public double CommissionRate { get; set; }

        public decimal CommissionValue { get; set; }

        public decimal OpenPriceValue { get; set; }

        public double StoreDiscountRate { get; set; }

        public decimal StoreDiscountValue { get; set; }

        public double ItemdiscountRate { get; set; }

        public decimal ItemDiscountValue { get; set; }

        public string LastModifyShiftID { get; set; }

        public decimal IssuePoints { get; set; }

        public decimal RedeemPoints { get; set; }

        public int RedeemStatus { get; set; }

        public string EBTType { get; set; }

        public string EBTPaid { get; set; }

        public int ItemGroupID { get; set; }

        public decimal PriceBeforeBulk { get; set; }

        public string BulkID { get; set; }

        public string BulkGroupID { get; set; }

        public decimal PriceBeforeMM { get; set; }

        public string MMID { get; set; }

        public string MMGroupID { get; set; }

        public bool IsCompItem { get; set; }

        public int EmployeeIDComp { get; set; }

        public string EmployeeNameCOMP { get; set; }

        public DateTime COMPDateTime { get; set; }

        public short TPStatus { get; set; }

        public string TPID { get; set; }

        public decimal PriceBeforeTP { get; set; }

        public decimal TaxRateBeforeTP { get; set; }

        public DateTime TPTime { get; set; }

        public double QtyPerItem { get; set; }

        public decimal CashDiscountPerItem { get; set; }

        public string Comments { get; set; }

        public string Description3 { get; set; }

        public DateTime HoldDateTime { get; set; }

        public int HoldMinutes { get; set; }

        public bool IsSplitItems { get; set; }

        public string DescriptionChinese { get; set; }

        public Guid KDSIDDetail { get; set; }

        public double QuantityKDS { get; set; }

        public DateTime KDSDoneDateTime { get; set; }

        public DateTime StampDateTime { get; set; }

        public DateTime OrderDateTime { get; set; }

        public bool IsModifierCombine { get; set; }

        public bool IsOverwritePrintModifier { get; set; }

        public bool MMIsApplyItem { get; set; }

        public string MMLeastExpensiveDiscountType { get; set; }

        public decimal MMLeastExpensiveDiscountValue { get; set; }

        public string SeatAssignment { get; set; }

        public string SeatAssignmentIndex { get; set; }

        public bool IsDeposit { get; set; }

        public string ShortDescription { get; set; }

        public string ReasonCodeDiscountStore { get; set; }

        public string ReasonCodeDiscountCategory { get; set; }

        public string ReasonCodeDiscountTicket { get; set; }

        public string ReasonCodeDiscountItem { get; set; }

        public string ReasonCodeDiscountCustomer { get; set; }

        public string ReasonCodeDiscountLevelCustomer { get; set; }

        public decimal ItemDiscountByPriceLevelAmount { get; set; }

        public decimal PriceBeforeRedeem { get; set; }

        public int EmployeeIDDiscountTicket { get; set; }

        public int EmployeeIDDiscountItem { get; set; }

        public decimal RegularPrice { get; set; }

        public decimal DualPrice { get; set; }

        public decimal PriceCash { get; set; }

        public decimal PriceCC { get; set; }

        public int TaxType { get; set; }

        public int ItemDiscountType { get; set; }

        public decimal CoinDiscountPerItem { get; set; }

        public string ModifierType { get; set; }

        public string CategoryDiscountType { get; set; }

    }


}
