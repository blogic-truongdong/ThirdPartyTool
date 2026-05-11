using System.Collections.Generic;

namespace ThirdPartyTool.Models._7Shifts
{
    public class UpdateReceiptFor7ShiftsModel
    {
        /// <summary>
        /// Receipt create date time. UTC date time in ISO8601 format
        /// </summary>
        public string receipt_date { get; set; }

        /// <summary>
        /// The net total of the receipt pre tax, post-discounts, pre tips. In cents
        /// </summary>
        public int net_total { get; set; }

        /// <summary>
        /// The gross total of the receipt in cents
        /// </summary>
        public int gross_total { get; set; }

        /// <summary>
        /// The total discounts of the receipt in cents
        /// </summary>
        public int total_receipt_discounts { get; set; }

        /// <summary>
        /// Total tips in cents
        /// </summary>
        public int tips { get; set; }

        /// <summary>
        /// External user ID of the sales receipt in your system (must be unique per 7shifts location).
        /// </summary>
        public string external_user_id { get; set; }

        /// <summary>
        /// Label for the revenue centre for the receipt
        /// </summary>
        public string revenue_center { get; set; }

        /// <summary>
        /// Receipt line items
        /// </summary>
        public List<CreateReceiptFor7ShiftsModel_SaleReceiptDetail> receipt_lines { get; set; } = new List<CreateReceiptFor7ShiftsModel_SaleReceiptDetail>();

        /// <summary>
        /// Tip line items
        /// </summary>
        public List<CreateReceiptFor7ShiftsModel_TipDetail> tip_details { get; set; } = new List<CreateReceiptFor7ShiftsModel_TipDetail>();

        /// <summary>
        /// Status of the receipt
        /// Allowed: open , closed, voided, deleted
        /// </summary>
        public string status { get; set; } // open , closed, voided, deleted

        /// <summary>
        /// Order type
        /// Allowed: dine_in, delivery, take_out
        /// </summary>
        public string order_type { get; set; }

        /// <summary>
        /// Dining option
        /// </summary>
        public string dining_option { get; set; }
    }
}
