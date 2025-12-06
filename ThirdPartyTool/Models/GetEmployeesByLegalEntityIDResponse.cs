using System.Collections.Generic;

namespace ThirdPartyTool.Models
{
    public class GetEmployeesByLegalEntityIDResponse
    {
        public bool hasMoreResults { get; set; }
        public string continuationToken { get; set; }
        public string additionalResultsUrl { get; set; }
        public List<EmployeeDetail> records { get; set; }
    }

    public class EmployeeDetail
    {
        public string id { get; set; }
        public string employeeNumber { get; set; }
        public string alternateEmployeeNumber { get; set; }
        public string badgeNumber { get; set; }
        public string firstName { get; set; }
        public string middleName { get; set; }
        public string lastName { get; set; }
        public PositionDetail positionData { get; set; }
    }

    public class PositionDetail
    {
        public string jobTitle { get; set; }
    }
}
