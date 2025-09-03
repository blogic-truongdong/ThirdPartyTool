using System;

namespace ThirdPartyTool.Models
{
    public class SaleReceiptServiceChargeTaxDetailEntity
    {
        public Guid SaleReceiptServiceChargeTaxDetailID { get; set; }

        public Guid SaleReceiptID { get; set; }

        public string ServiceChargeTaxCodeID { get; set; }

        public decimal ServiceChargeTaxRate { get; set; }

        public decimal ServiceFee { get; set; }

        public decimal ServiceFeeTaxAmount { get; set; }

        public decimal PartyServiceFeeAmount { get; set; }

        public decimal PartyServiceFeeTaxAmount { get; set; }

        public decimal AreaFeeAmount { get; set; }

        public decimal AreaFeeTaxAmount { get; set; }

        public decimal AutoGratuityAmount { get; set; }

        public decimal AutoGratuityTaxAmount { get; set; }

        public decimal ExtraFeeAmount { get; set; }

        public decimal ExtraFeeTaxAmount { get; set; }

        public decimal DeliveryFee { get; set; }

        public decimal DeliveryFeeTaxAmount { get; set; }

    }


}
