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
        public List<ObjectValues> objectValues { get; set; } = new List<ObjectValues>();
    }

    public class ObjectValues
    {
        public object SaleReceiptDetailID { get; set; }
        public decimal Quantity { get; set; }
        public decimal ExtendedPrice { get; set; }
    }
}
