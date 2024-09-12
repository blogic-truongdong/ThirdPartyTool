using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThirdPartyTool.Models
{
    public class BatchEntity
    {
        public DateTime? Date { get; set; }

        public string ID { get; set; }

        public string Status { get; set; }

        public string MerchantID { get; set; }

        public int? Quantity { get; set; }

        public decimal? Total { get; set; }

        public string BatchUnique { get; set; }

        public decimal? TotalSVCFeeForTip { get; set; }

        public decimal? TotalSales { get; set; }

        public decimal? TotalReturns { get; set; }

        public decimal? TotalTips { get; set; }

        public decimal? TotalSVCFeeAmount { get; set; }

        public decimal? DepositAmount { get; set; }

        public string WorkStationName { get; set; }

        public decimal? MerchantFeeAmount { get; set; }

        public decimal? MerchantFeePercent { get; set; }

        public string ExpandInfo { get; set; }

    }

}
