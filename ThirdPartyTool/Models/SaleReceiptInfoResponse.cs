using System.Collections.Generic;

namespace ThirdPartyTool.Models
{
    public class SaleReceiptInfoResponse
    {
        public SaleReceiptEntity SaleReceipt { get; set; }
        public List<SaleReceiptEntity> ListSaleReceipts { get; set; }
        public List<SaleReceiptDetailEntity> ListSaleReceiptDetails { get; set; }
        public List<SaleReceiptDetailTaxDetailEntity> ListSaleReceiptDetailTaxDetails { get; set; }
        public List<SaleReceiptServiceChargeTaxDetailEntity> ListSaleReceiptServiceChargeTaxDetails { get; set; }
        public List<SaleReceiptPaymentDetailEntity> ListSaleReceiptPaymentDetails { get; set; }
        public List<TransactionDetailEntity> ListTransactionDetails { get; set; }
        public bool IsSuccess { get; set; } = true;
        public bool IsMultiReceipts { get; set; }
        public string Message { get; set; }
    }
}
