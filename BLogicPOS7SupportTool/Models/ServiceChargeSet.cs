namespace BLogicPOS7SupportTool.Models
{
    enum ServiceChargeKind
    {
        ManualServiceFee,
        AreaServiceFee,
        PartyServiceFee,
        AutoServiceFee,
        ExtraServiceFee,
        SVCFee,
        CashDiscount
    }

    // Current service-charge amounts/taxes on a receipt (the four adjustable in
    // the old SelectServiceChargeToAdjstForm, plus Extra Fee, SVC Fee and Cash Discount).
    class ServiceChargeSet
    {
        public decimal ServiceFee { get; set; }
        public decimal ServiceFeeTaxAmount { get; set; }
        public decimal AreaFeeAmount { get; set; }
        public decimal AreaFeeTaxAmount { get; set; }
        public decimal PartyServiceFeeAmount { get; set; }
        public decimal PartyServiceFeeTaxAmount { get; set; }
        public decimal AutoGratuityAmount { get; set; }
        public decimal AutoGratuityTaxAmount { get; set; }
        public decimal ExtraFeeAmount { get; set; }
        public decimal ExtraFeeTaxAmount { get; set; }
        public decimal SVCFeeAmount { get; set; }
        public decimal SVCFeeTaxAmount { get; set; }
        public decimal CashDiscountAmount { get; set; }
        public decimal CashDiscountTaxAmount { get; set; }
    }
}
