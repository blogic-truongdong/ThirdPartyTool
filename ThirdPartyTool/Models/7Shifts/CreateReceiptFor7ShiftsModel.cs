using System.Collections.Generic;

namespace ThirdPartyTool.Models._7Shifts
{
    public enum ReceiptStatus_7Shifts
    {
        open,
        closed,
        voided,
        deleted
    }

    public enum ReceiptOrderType_7Shifts
    {
        dine_in,
        delivery,
        take_out
    }
    public enum TipType_7Shifts
    {
        cc, cash, declared, net, total, gratuity
    }

    public class CreateReceiptFor7ShiftsModel
    {
        /// <summary>
        /// ID available to the client in the POS UI. Be it a GUID, a receipt number, a composite of date and ID and terminal, etc
        /// </summary>
        public string receipt_id { get; set; }

        /// <summary>
        /// The 7shifts location_id this receipt belongs to
        /// </summary>
        public int location_id { get; set; }

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

    public class CreateReceiptFor7ShiftsModel_SaleReceiptDetail
    {
        /// <summary>
        /// The item ID available to the client
        /// </summary>
        public string external_item_id { get; set; }

        /// <summary>
        /// The list of category IDs available to the client
        /// </summary>
        public List<string> external_category_ids { get; set; } = new List<string>();

        /// <summary>
        /// The quantity of this item
        /// </summary>
        public int quantity { get; set; }

        /// <summary>
        /// The item price in cents
        /// </summary>
        public int price { get; set; }

        /// <summary>
        /// The item gross price in cents
        /// </summary>
        public int gross_item_price { get; set; }

        /// <summary>
        /// The item net price pre tax, post-discounts, pre tips. In cents
        /// </summary>
        public int net_item_price { get; set; }

        /// <summary>
        /// The item discount. In cents
        /// </summary>
        public int item_discount { get; set; }

        /// <summary>
        /// The item status 
        /// Allowed: open, closed, voided, deleted
        /// </summary>
        public string status { get; set; }

        /// <summary>
        /// Line item create date time. UTC date time in ISO8601 format
        /// </summary>
        public string created { get; set; }
    }

    public class CreateReceiptFor7ShiftsModel_TipDetail
    {
        /// <summary>
        /// Tip type 
        /// Allowed: cc cash declared net total gratuity
        /// </summary>
        public string type { get; set; }

        /// <summary>
        /// Tip value. In cents
        /// </summary>
        public int value { get; set; }
    }
}
