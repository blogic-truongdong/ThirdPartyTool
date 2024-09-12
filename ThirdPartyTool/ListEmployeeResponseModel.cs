using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThirdPartyTool
{
    public class ListEmployeeResponseModel
    {
        public List<ListEmployeeResponseModel_Data> data { get; set; }
        public ListEmployeeResponseModel_Meta meta { get; set; }

        public class ListEmployeeResponseModel_Data
        {
            public int id { get; set; }
            public string first_name { get; set; }
            public string last_name { get; set; }
            public string email { get; set; }
            public string mobile_number { get; set; }
            public string home_number { get; set; }
            public string address { get; set; }
            public string postal_zip { get; set; }
            public string city { get; set; }
            public string prov_state { get; set; }
            public bool active { get; set; }
            public string notes { get; set; }
            public string punch_id { get; set; }
            public string birth_date { get; set; }
            public string hire_date { get; set; }
            public string employee_id { get; set; }
        }

        public class ListEmployeeResponseModel_Meta
        {
            public ListEmployeeResponseModel_Cursor cursor { get; set; }
        }

        public class ListEmployeeResponseModel_Cursor
        {
            public string current { get; set; }
            public string prev { get; set; }
            public string next { get; set; }
            public string count { get; set; }
        }
    }
}
