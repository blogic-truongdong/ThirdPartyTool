using System;

namespace ThirdPartyTool.Models
{
    public class PaymentRoleDetailEntity
    {
        public Guid ID { get; set; }

        public Guid SaleReceiptID { get; set; }

        public long SaleReceiptPaymentDetailID { get; set; }

        public short PaymentMethodID { get; set; }

        public short PaymentMethodType { get; set; }

        public short EmployeeID { get; set; }

        public short RolesID { get; set; }

        public decimal TipRate { get; set; }

        public decimal TipRateOT { get; set; }

        public decimal Gratuity { get; set; }

        public decimal SVCFeeForGratuity { get; set; }

        public decimal MerchantFeeForTip { get; set; }

        public DateTime DateCreated { get; set; }

        public decimal PartyServiceFeeAmount { get; set; }

        public decimal PartyServiceFeeTaxAmount { get; set; }

        public decimal SVCFeeForPartyServiceFeeAmount { get; set; }

        public decimal MerchantFeeForPartyServiceFeeAmount { get; set; }

        public decimal AutoGratuityAmount { get; set; }

        public decimal AutoGratuityTaxAmount { get; set; }

        public decimal SVCFeeForAutoGratuityAmount { get; set; }

        public decimal MerchantFeeForAutoGratuityAmount { get; set; }

        public decimal AreaFeeAmount { get; set; }

        public decimal AreaFeeTaxAmount { get; set; }

        public decimal SVCFeeForAreaFeeAmount { get; set; }

        public decimal MerchantFeeForAreaFeeAmount { get; set; }

        public decimal ServiceFee { get; set; }

        public decimal ServiceFeeTaxAmount { get; set; }

        public decimal SVCFeeForServiceFee { get; set; }

        public decimal MerchantFeeForServiceFee { get; set; }

    }


}
