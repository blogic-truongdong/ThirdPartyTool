namespace BLogicPOS7SupportTool.Models
{
    public class RolesEntity
    {
        public int RolesID { get; set; }

        public string RolesName { get; set; }

        public decimal TipRate { get; set; }

        public bool IsHourlyWage { get; set; }

        public decimal HourlyWage { get; set; }

        public bool IsDailyOverTime { get; set; }

        public double HourlyWageOT { get; set; }

        public int DailyOverTimeWorkedPeriod { get; set; }

        public decimal TipRateOT { get; set; }

        public decimal MaxTipHour { get; set; }

        public decimal MaxTipHourOT { get; set; }

        public string RoleCode { get; set; }

        public bool IsDailyOverTime2nd { get; set; }

        public double HourlyWageOT2nd { get; set; }

        public int DailyOverTimeWorkedPeriod2nd { get; set; }

        public int SevenShiftsRoleID { get; set; }

        public int SevenShiftsDepartmentID { get; set; }

        public decimal ServiceChargesRate { get; set; }

        public decimal ServiceChargesRateOT { get; set; }

        public decimal ServiceChargesMaxHour { get; set; }

        public decimal ServiceChargesMaxHourOT { get; set; }

        public decimal TrainingTipRate { get; set; }

        public decimal TrainingTipRateOT { get; set; }

        public decimal RetainPercentOfTip { get; set; }

        public string RemainingTipForRoles { get; set; }

        public decimal TipDistributePercent { get; set; }

        public bool IsDeleted { get; set; }

    }


}
