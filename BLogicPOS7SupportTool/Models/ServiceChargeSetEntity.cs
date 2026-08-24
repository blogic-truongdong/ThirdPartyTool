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
    class ServiceChargeSetEntity
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
        public decimal SVCFeePaidAmount { get; set; }
        public decimal SVCFeeTaxPaidAmount { get; set; }
        public decimal CashDiscountPaidAmount { get; set; }
        public decimal CashDiscountTaxPaidAmount { get; set; }
    }
}
