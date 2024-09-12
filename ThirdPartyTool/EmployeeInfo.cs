using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ThirdPartyTool
{
    public partial class EmployeeInfo : UserControl
    {
        public string Token = string.Empty;
        public string CompanyID =string.Empty;
        public string parameters = string.Empty;
        public string SevenShiftsUserID = string.Empty;
        public EmployeeInfo()
        {
            InitializeComponent();

            btnUpdate.Click += BtnUpdate_Click;
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(EmployeeID.Text))
                    return;

                string url = $"users/{SevenShiftsUserID}";

                UpdateEmployee7ShiftsModel updateEmployee7Shifts = new UpdateEmployee7ShiftsModel
                {
                    employee_id = EmployeeID.Text,
                };

                string jsonData = JsonConvert.SerializeObject(updateEmployee7Shifts);

                string baseUrl = $"https://api.7shifts.com/v2/company/{CompanyID}";
                HttpClient httpClient = new HttpClient();
                httpClient.Timeout = TimeSpan.FromSeconds(45);

                var httpRequestMessage = new HttpRequestMessage();
                httpRequestMessage.Method = HttpMethod.Put;
                httpRequestMessage.RequestUri = new Uri($"{baseUrl}/{url}{parameters}");
                httpRequestMessage.Headers.Add("Authorization", $"Bearer {Token}");

                if (!string.IsNullOrWhiteSpace(jsonData))
                {
                    var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                    httpRequestMessage.Content = content;
                }

                var resp = httpClient.SendAsync(httpRequestMessage).Result;
            }
            catch (Exception)
            {

            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
