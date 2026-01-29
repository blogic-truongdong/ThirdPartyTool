using System.Collections.Generic;

namespace ThirdPartyTool.Models
{
    public class RequestModel
    {
        public string GroupID { get; set; }
        public string SaleReceiptID { get; set; }
        public string SaleReceiptPaymentDetailID { get; set; }
        public bool IsSelectedItem { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public RefundValues refundValues { get; set; } = new RefundValues();
        public List<ObjectValues> objectValues { get; set; } = new List<ObjectValues>();
    }

    public class ObjectValues
    {
        public object SaleReceiptDetailID { get; set; }
        public decimal Quantity { get; set; }
        public decimal ExtendedPrice { get; set; }
    }

    public class RefundValues
    {
        public string SaleReceiptID { get; set; }
        public decimal AreaFeeAmount { get; set; }
        public decimal AreaFeeTaxAmount { get; set; }
        public decimal PartyServiceFeeAmount { get; set; }
        public decimal PartyServiceFeeTaxAmount { get; set; }
        public decimal AutoGratuityAmount { get; set; }
        public decimal AutoGratuityTaxAmount { get; set; }
        public decimal ServiceFee { get; set; }
        public decimal ServiceFeeTaxAmount { get; set; }
        public decimal SVCFeePaidAmount { get; set; }
        public decimal SVCFeeTaxPaidAmount { get; set; }
        public decimal CashDiscountPaidAmount { get; set; }
        public decimal CashDiscountTaxPaidAmount { get; set; }
    }
}
