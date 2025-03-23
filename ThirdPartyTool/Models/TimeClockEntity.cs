using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThirdPartyTool.Models
{
    public class TimeClockEntity
    {
        public string TimeClockID { get; set; }

        public int EmployeeID { get; set; }

        public int ManagerID { get; set; }

        public string StoreID { get; set; }

        public string ShiftID { get; set; }

        public DateTime DateTimeIn { get; set; }

        public DateTime DateTimeOut { get; set; }

        public string TimeElapsed { get; set; }

        public bool ClockStatus { get; set; }

        public decimal HourlyWage { get; set; }

        public decimal HourlyWageOT { get; set; }

        public bool IsDailyOverTime { get; set; }

        public int PaidBreakLength { get; set; }

        public int PaidBreakCount { get; set; }

        public int MealBreakLength { get; set; }

        public int MealBreakCount { get; set; }

        public int RegularWorkingTime { get; set; }

        public int OverWorkingTime { get; set; }

        public int RolesID { get; set; }

        public decimal TipRate { get; set; }

        public decimal TipRateOT { get; set; }

        public decimal MaxTipHour { get; set; }

        public decimal MaxTipHourOT { get; set; }

        public int SevenShiftID { get; set; }

        public bool IsDailyOverTime2nd { get; set; }

        public float HourlyWageOT2nd { get; set; }

        public int OverWorkingTime2nd { get; set; }

        public string SevenShiftPunchID { get; set; }

        public int DefineShiftTip { get; set; }

        public decimal TipAmount { get; set; }

        public decimal ServiceChargesRate { get; set; }

        public decimal ServiceChargesRateOT { get; set; }

        public decimal ServiceChargesMaxHour { get; set; }

        public decimal ServiceChargesMaxHourOT { get; set; }

        public DateTime SevenShiftsClockOutEnd { get; set; }

        public bool EnableTrainingMode { get; set; }

    }
}
