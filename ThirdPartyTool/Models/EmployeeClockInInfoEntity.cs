using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThirdPartyTool.Models
{
    public class EmployeeClockInInfoEntity
    {
        public int RolesID { get; set; }
        public decimal TipRate { get; set; }
        public decimal TipRateOT { get; set; }
        public decimal TrainingTipRate { get; set; }
        public decimal TrainingTipRateOT { get; set; }
    }
}
