using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace ThirdPartyTool.Models._7Shifts
{
    public class SevenShifts_Receipt
    {
        [JsonProperty("id")]
        public string ID { get; set; }

        [JsonProperty("company_id")]
        public int CompanyID { get; set; }

        [JsonProperty("location_id")]
        public int LocationID { get; set; }

        [JsonProperty("pos_id")]
        public int PosID { get; set; }

        [JsonProperty("receipt_id")]
        public string ReceiptID { get; set; }

        [JsonProperty("receipt_date")]
        public DateTimeOffset ReceiptDate { get; set; }

        [JsonProperty("net_total")]
        public int NetTotal { get; set; }

        [JsonProperty("gross_total")]
        public int GrossTotal { get; set; }

        [JsonProperty("tips")]
        public int Tips { get; set; }

        [JsonProperty("total_receipt_discounts")]
        public int TotalReceiptDiscounts { get; set; }

        [JsonProperty("total_item_discounts")]
        public int TotalItemDiscounts { get; set; }

        [JsonProperty("external_user_id")]
        public string ExternalUserID { get; set; }

        [JsonProperty("revenue_center")]
        public string RevenueCenter { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("created_date")]
        public DateTimeOffset CreatedDate { get; set; }

        [JsonProperty("modified_date")]
        public DateTimeOffset ModifiedDate { get; set; }

        [JsonProperty("tip_details")]
        public List<SevenShifts_Receipt_Tip> TipDetails { get; set; }
    }
}
