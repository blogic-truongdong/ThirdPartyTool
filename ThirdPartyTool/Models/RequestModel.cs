using System;
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
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string ShiftID { get; set; }
        public decimal OpeningAmount { get; set; }
        public decimal ClosingAmount { get; set; }
        public RefundValues refundValues { get; set; } = new RefundValues();
        public ServiceChargeValues serviceChargesValues { get; set; } = new ServiceChargeValues();
        public List<ObjectValues> objectValues { get; set; } = new List<ObjectValues>();
        public SpecialItemValues specialItemValues { get; set; } = new SpecialItemValues();
    }

    /// <summary>An Open Price / Negative Price item added to a finished ticket.</summary>
    public class SpecialItemValues
    {
        public string SaleReceiptID { get; set; }
        public string SaleReceiptPaymentDetailID { get; set; }
        public string ItemID { get; set; }
        public string ItemName { get; set; }
        /// <summary>Signed line amount: positive for Open Price, negative for Negative Price items. No tax.</summary>
        public decimal Amount { get; set; }
    }

    public class ObjectValues
    {
        public object SaleReceiptDetailID { get; set; }
        public decimal Quantity { get; set; }
        public decimal ExtendedPrice { get; set; }

        // Partial-quantity refund (e.g. 2 of 3). When RefundPart < RefundWhole the row is split:
        // the original keeps the rest and the refunded share is saved as a new row.
        public decimal RefundQuantity { get; set; }
        public decimal RefundExtendedPrice { get; set; }
        public decimal RefundTaxAmount { get; set; }
        public decimal RefundPart { get; set; } = 1;
        public decimal RefundWhole { get; set; } = 1;
        public bool IsPartialRefund => RefundPart < RefundWhole;
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

    public class ServiceChargeValues
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
