using System;
using System.Collections.Generic;

namespace ThirdPartyTool.Models
{
    public class GetEmployeeTimeCardPunchesResponse
    {
        public bool hasMoreResults { get; set; }
        public string continuationToken { get; set; }
        public string additionalResultsUrl { get; set; }
        public List<EmployeeTimeCardPunchesDetail> records { get; set; }
    }

    public class EmployeeTimeCardPunchesDetail
    {
        public string employeeId { get; set; }
        public int EmployeeNumber { get; set; }
        public int badgeNumber { get; set; }
        public DateTime displayDate { get; set; }
        public DateTime punchIn { get; set; }
        public DateTime punchOut { get; set; }
        public decimal hourAmount { get; set; }
        public int departmentCode { get; set; }
        public string departmentName { get; set; }
        public string earningCode { get; set; }

    }
}
