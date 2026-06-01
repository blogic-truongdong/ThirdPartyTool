using System;

namespace BLogicPOS7SupportTool.Models
{
    public class Items
    {
        public Guid ItemID { get; set; }

        public string ItemName { get; set; }

        public string TaxCodeID { get; set; }

        public int DeptDeptID { get; set; }

        public string ItemTypeCode { get; set; }

        public short UnitOfMeasureID { get; set; }

        public int ItemStyleID { get; set; }

        public Guid ItemLocationID { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateLastReceived { get; set; }

        public DateTime DateLastOrdered { get; set; }

        public decimal ItemCost { get; set; }

        public string Attribute { get; set; }

        public string Size { get; set; }

        public string Image { get; set; }

        public string Description1 { get; set; }

        public string Description2 { get; set; }

        public double MSRP { get; set; }

        public double Margin { get; set; }

        public double Markup { get; set; }

        public double OrderQty { get; set; }

        public bool PrintTags { get; set; }

        public double ReorderPoint { get; set; }

        public int DefaultOrderQty { get; set; }

        public double CoReorderPoint { get; set; }

        public bool Unorderable { get; set; }

        public bool SerialNoTracking { get; set; }

        public double PriceLevelMarkdown1 { get; set; }

        public double PriceLevelMarkdown2 { get; set; }

        public double PriceLevelMarkdown3 { get; set; }

        public double PriceLevelMarkdown4 { get; set; }

        public decimal PriceValue1 { get; set; }

        public decimal PriceValue2 { get; set; }

        public decimal PriceValue3 { get; set; }

        public decimal PriceValue4 { get; set; }

        public string LookupCode { get; set; }

        public bool Taxable { get; set; }

        public byte CommissionMethod { get; set; }

        public double ItemFixCommissionAmount { get; set; }

        public double ItemPercentCommissionAmount { get; set; }

        public double RegularPrice { get; set; }

        public double Quantity { get; set; }

        public string BarCode { get; set; }

        public int SerialNumber { get; set; }

        public bool IsDeleted { get; set; }

        public long Version { get; set; }

        public string SerialTrackingType { get; set; }

        public bool InventoryUpdatable { get; set; }

        public int ItemOrder { get; set; }

        public string ForcedModifier { get; set; }

        public short LabelPrinterID { get; set; }

        public string ShortDescription { get; set; }

        public string TaxCodeIDFullService { get; set; }

        public bool ChangePrice { get; set; }

        public string ItemNotes { get; set; }

        public string AlternateSKU1 { get; set; }

        public string AlternateSKU2 { get; set; }

        public bool AppendModifierToItem { get; set; }

        public decimal IssuePoints { get; set; }

        public decimal RedeemPoints { get; set; }

        public int AgeRequired { get; set; }

        public bool IsDefault { get; set; }

        public Guid ForceModifierTemplateID { get; set; }

        public string EBTType { get; set; }

        public bool IsSoldByWeight { get; set; }

        public int ItemGroupID { get; set; }

        public string DetailImage { get; set; }

        public string DetailDescription { get; set; }

        public string BackgroundColor { get; set; }

        public string TextColor { get; set; }

        public string Font { get; set; }

        public int ButtonHeight { get; set; }

        public int ButtonWidth { get; set; }

        public Guid ServiceChargeTemplateID { get; set; }

        public double FullServicePrice { get; set; }

        public bool UseTimePrice { get; set; }

        public bool ApplyRegularPriceAnyTime { get; set; }

        public int Indent { get; set; }

        public bool IsShowOnPOS { get; set; }

        public bool UseSOrder { get; set; }

        public bool UsePhone { get; set; }

        public string Description3 { get; set; }

        public string ItemName2 { get; set; }

        public string Description4 { get; set; }

        public string Description5 { get; set; }

        public bool IsVisible { get; set; }

        public string DescriptionChinese { get; set; }

        public string TaxCodeIDDoorDash { get; set; }

        public string TaxCodeIDGrubHub { get; set; }

        public string TaxCodeIDUberEat { get; set; }

        public string TaxCodeIDDelivery { get; set; }

        public string TaxCodeIDPostmates { get; set; }

        public bool IsLockDiscount { get; set; }

        public double InventoryQty { get; set; }

        public string TaxCodeIDBento { get; set; }

        public string TaxCodeIDBentocart { get; set; }

        public string TaxCodeIDFuanTuan { get; set; }

        public string TaxCodeIDTock { get; set; }

        public string TaxCodeIDTuango { get; set; }

        public bool IsModifierCombine { get; set; }

        public string UseImageByType { get; set; }

        public string TaxCodeIDWalkin { get; set; }

        public bool IsOverwritePrintModifier { get; set; }

        public string TaxCodeIDKiosk { get; set; }

        public int InventoryStatus { get; set; }

        public bool DepositItem { get; set; }

        public double DualPrice { get; set; }

        public string TaxCodeIDOtter { get; set; }

        public string ItemInternalNotes { get; set; }

        public int ModifierMultiplierType { get; set; }

        public double ModifierMultiplicationFactor { get; set; }

        public string TaxCodeIDKioskDineIn { get; set; }

        public string ModifierType { get; set; }

        public double TareWeight { get; set; }

        public string ThirdPartyImage { get; set; }

        public string BackgroundColorLight { get; set; }

        public string TextColorLight { get; set; }

    }

}
