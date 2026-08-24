using System;

namespace BLogicPOS7SupportTool.Models
{
    class ReceiptHeaderEntity
    {
        public Guid SaleReceiptID { get; set; }
        public string SaleReceiptNumber { get; set; }
        public DateTime DateCreated { get; set; }
        public string WorkstationName { get; set; }
        public string TransactionStatusID { get; set; }
        public string TableName { get; set; }
        public string CashierName { get; set; }
        public string CurrentCustomerName { get; set; }
        public string CurrentCustomerPhone { get; set; }
        public string StoreName { get; set; }
        public string StoreAddress { get; set; }
        public string StorePhone { get; set; }
        public string StoreEmail { get; set; }
        public string StoreWebsite { get; set; }

        public decimal TotalAmount { get; set; }
        public decimal TotalDiscountAmount { get; set; }
        public decimal AutoGratuityAmount { get; set; }
        public decimal AreaFeeAmount { get; set; }
        public decimal PartyServiceFeeAmount { get; set; }
        public decimal SalesOrderFee { get; set; }
        public decimal ServiceFee { get; set; }
        public decimal SVCFeeAmount { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal TipAmount { get; set; }
        public decimal FinalTotal { get; set; }
        public decimal SubTotal { get; set; }
        public decimal ExtraFeeAmount { get; set; }
        public decimal SVCFeePaidAmount { get; set; }
        public decimal CashDiscountPaidAmount { get; set; }
        public decimal SVCFeeTaxRate { get; set; }
        public decimal SVCFeeTaxPaidAmount { get; set; }
    }
}
