using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace ThirdPartyTool
{
    public partial class SevenShiftsForm : Form
    {
        string Token = "32653664313461612d616664632d343030612d613333352d613934333262313434656665";
        string CompanyID = "178808";
        string LocationID = "306707";
        public SevenShiftsForm()
        {
            InitializeComponent();

            btnGetEmployee.Click += BtnGetEmployee_Click;
        }

        private void BtnGetEmployee_Click(object sender, EventArgs e)
        {
            string jsonData = string.Empty;
            string parameters = string.Empty;
            string url = $"users?limit=200&location_id={LocationID}";

            try
            {
                string baseUrl = $"https://api.7shifts.com/v2/company/{CompanyID}";
                HttpClient httpClient = new HttpClient();
                httpClient.Timeout = TimeSpan.FromSeconds(45);

                var httpRequestMessage = new HttpRequestMessage();
                httpRequestMessage.Method = HttpMethod.Get;
                httpRequestMessage.RequestUri = new Uri($"{baseUrl}/{url}{parameters}");
                httpRequestMessage.Headers.Add("Authorization", $"Bearer {Token}");

                if (!string.IsNullOrWhiteSpace(jsonData))
                {
                    var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                    httpRequestMessage.Content = content;
                }

                var resp = httpClient.SendAsync(httpRequestMessage).Result;

                string str = resp.Content.ReadAsStringAsync().Result;

                ListEmployeeResponseModel modelResponse = JsonConvert.DeserializeObject<ListEmployeeResponseModel>(str);
                pnlContainer.Controls.Clear();
                int index = 1;
                foreach (var item in modelResponse.data)
                {
                    EmployeeInfo empInfo = new EmployeeInfo();
                    empInfo.index.Text = index.ToString();
                    empInfo.FirstName.Text = item.first_name;
                    empInfo.LastName.Text = item.last_name;
                    empInfo.EmployeeCode.Text = item.employee_id + " - " + item.punch_id;
                    empInfo.Token = Token;
                    empInfo.CompanyID = CompanyID;
                    empInfo.SevenShiftsUserID = item.id.ToString();
                    empInfo.EmployeeID.Text = item.punch_id;
                    index++;

                    pnlContainer.Controls.Add(empInfo);
                }
            }
            catch (Exception ex)
            {
            }

        }
    }
}
