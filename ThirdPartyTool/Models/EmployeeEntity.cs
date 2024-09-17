using System;

namespace ThirdPartyTool.Models
{
    public class EmployeeEntity
    {
        public int EmployeeID { get; set; }

        public string StoreID { get; set; }

        public string EmployeeCode { get; set; }

        public string EmployeeCard { get; set; }

        public string LoginName { get; set; }

        public string LoginPassword { get; set; }

        public float CommissionRate { get; set; }

        public string EmployeeFirstName { get; set; }

        public string EmployeeMiddleName { get; set; }

        public string EmployeeLastName { get; set; }

        public string EmployeePhone1 { get; set; }

        public string EmployeePhone2 { get; set; }

        public string EmployeeEmail { get; set; }

        public string EmployeeAddress { get; set; }

        public string EmployeeCity { get; set; }

        public string EmployeeState { get; set; }

        public string EmployeeCountry { get; set; }

        public string EmployeeZipCode { get; set; }

        public string Description { get; set; }

        public bool IsSalePerson { get; set; }

        public DateTime BirthDate { get; set; }

        public DateTime StartWorkDate { get; set; }

        public bool Enable { get; set; }

        public int ScheduleIndex { get; set; }

        public decimal HourlyWage { get; set; }

        public float HourlyWageOT { get; set; }

        public string EmployeeImage { get; set; }

        public bool IsHourlyWage { get; set; }

        public bool IsDailyOverTime { get; set; }

        public int DailyOverTimeWorkedPeriod { get; set; }

        public bool IsCommission { get; set; }

        public bool IsPaidRestBreak { get; set; }

        public int RestBreakWorkedPeriod { get; set; }

        public int RestBreakLength { get; set; }

        public bool IsSimpleMealBreak { get; set; }

        public int MealBreakWorkedPeriod { get; set; }

        public int MealBreakLength { get; set; }

        public bool IsAutoClockOut { get; set; }

        public string MaximumAutoClockOut { get; set; }

        public bool IsRestBreak { get; set; }

        public bool IsMealBreak { get; set; }

        public bool DoNotAllowUserToClockOutBeforeCloseAllTickets { get; set; }

        public string FingerPrintID { get; set; }

        public string FingerPrintIDImage { get; set; }

        public string FingerPrintID2 { get; set; }

        public string FingerPrintID2Image { get; set; }

        public string Language { get; set; }

        public int CurrentRolesID { get; set; }

        public bool IsUsingRoleWage { get; set; }

        public string SocialSecurityNumber { get; set; }

        public string EndWorkDate { get; set; }

        public bool IsUsingRoleTipPool { get; set; }

        public decimal TipRate { get; set; }

        public decimal TipRateOT { get; set; }

        public decimal MaxTipHour { get; set; }

        public decimal MaxTipHourOT { get; set; }

        public bool IsDailyOverTime2nd { get; set; }

        public float HourlyWageOT2nd { get; set; }

        public int DailyOverTimeWorkedPeriod2nd { get; set; }

        public bool IsSelected { get; set; }

        public int SevenShiftsUserID { get; set; }

        public int DepartmentID { get; set; }

        public string Exempt { get; set; }

        public string SSN { get; set; }

        public decimal ServiceChargesRate { get; set; }

        public decimal ServiceChargesRateOT { get; set; }

        public decimal ServiceChargesMaxHour { get; set; }

        public decimal ServiceChargesMaxHourOT { get; set; }

        public string PayrollEmployeeID { get; set; }

        public bool EnableTrainingMode { get; set; }

        public decimal TrainingTipRate { get; set; }

        public decimal TrainingTipRateOT { get; set; }

        public string LuxanEmployeeID { get; set; }

        public string ExpandInfo { get; set; }

        public bool IsOverrideTipRetain { get; set; }

        public short RetainPercentOfTip { get; set; }

    }
}
