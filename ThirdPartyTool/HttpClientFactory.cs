using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using ThirdPartyTool.Models;

namespace ThirdPartyTool
{
    public class HttpClientFactory
    {
        public async Task<bool> GeneratePaycorAccessToken()
        {
            bool result;
            try
            {
                string url = $"https://apis.paycor.com/sts/v1/common/token?subscription-key={Helper.PaycorAPImSubscriptionKey}";

                HttpClient httpClient = new HttpClient();
                httpClient.Timeout = TimeSpan.FromSeconds(45);

                var httpRequestMessage = new HttpRequestMessage();
                httpRequestMessage.Method = HttpMethod.Post;
                httpRequestMessage.RequestUri = new Uri(url);

                HttpContent content = new FormUrlEncodedContent(
                new List<KeyValuePair<string, string>> {
                    new KeyValuePair<string, string>("grant_type", "refresh_token"),
                    new KeyValuePair<string, string>("refresh_token", Helper.PaycorRefreshToken),
                    new KeyValuePair<string, string>("client_id", Helper.PaycorClientID),
                    new KeyValuePair<string, string>("client_secret", Helper.PaycorClientSecret)
                });

                content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

                content.Headers.ContentType.CharSet = "UTF-8";

                httpRequestMessage.Content = content;

                var httpResponse = await httpClient.SendAsync(httpRequestMessage);

                if (!httpResponse.IsSuccessStatusCode)
                {
                    BLogicLogger.Error($"**** Generate Access Token from Paycor Error ");
                    BLogicLogger.Error($"     URL: {url} ");
                    BLogicLogger.Error($"     refresh_token: {Helper.PaycorRefreshToken} ");
                    BLogicLogger.Error($"     client_id: {Helper.PaycorClientID} ");
                    BLogicLogger.Error($"     client_secret: {Helper.PaycorClientSecret} ");

                    result = false;
                }
                else
                {
                    string str = await httpResponse.Content.ReadAsStringAsync();

                    var settings = new JsonSerializerSettings();
                    settings.NullValueHandling = NullValueHandling.Ignore;

                    GeneratePaycorAccessTokenResponse accessTokenResponse = JsonConvert.DeserializeObject<GeneratePaycorAccessTokenResponse>(str, settings);

                    Helper.PaycorAccessToken = accessTokenResponse.access_token;

                    Helper.PaycorAuthorization = accessTokenResponse.token_type + " " + accessTokenResponse.access_token;

                    result = true;
                }
            }
            catch (Exception ex)
            {
                result = false;
                BLogicLogger.Error($"**** GeneratePaycorAccessToken with error: {ex.Message}", ex);
            }

            return result;
        }

        public async Task<GetEmployeeTimeCardPunchesResponse> GetPaycorTimeClocks(string urlParams)
        {
            GetEmployeeTimeCardPunchesResponse employeeTimeCardPunches = null;
            try
            {
                if (string.IsNullOrWhiteSpace(Helper.PaycorAccessToken))
                    return employeeTimeCardPunches;

                string url = $"{Helper.PaycorUrlProduction}{urlParams}";

                HttpClient httpClient = new HttpClient();
                httpClient.Timeout = TimeSpan.FromSeconds(45);

                var httpRequestMessage = new HttpRequestMessage();
                httpRequestMessage.Method = HttpMethod.Get;
                httpRequestMessage.RequestUri = new Uri(url);
                httpRequestMessage.Headers.Add("Authorization", Helper.PaycorAuthorization);
                httpRequestMessage.Headers.Add("Ocp-Apim-Subscription-Key", Helper.PaycorAPImSubscriptionKey);

                var httpResponse = await httpClient.SendAsync(httpRequestMessage);
                if (!httpResponse.IsSuccessStatusCode)
                {
                    BLogicLogger.Error($"**** Get Time Clocks from Paycor Error ");
                    BLogicLogger.Error($"     URL: {url} ");
                    BLogicLogger.Error($"     Authorization: {Helper.PaycorAuthorization} ");
                    BLogicLogger.Error($"     Ocp-Apim-Subscription-Key: {Helper.PaycorAPImSubscriptionKey} ");
                }
                else
                {
                    string str = await httpResponse.Content.ReadAsStringAsync();

                    var settings = new JsonSerializerSettings();
                    settings.NullValueHandling = NullValueHandling.Ignore;

                    employeeTimeCardPunches = JsonConvert.DeserializeObject<GetEmployeeTimeCardPunchesResponse>(str, settings);
                }

            }
            catch (Exception ex)
            {
                BLogicLogger.Error($"**** GetPaycorTimeClocks with erorr: {ex.Message}", ex);
            }

            return employeeTimeCardPunches;
        }

        public async Task<GetEmployeesByLegalEntityIDResponse> GetPaycorEmployees(string urlParams)
        {
            GetEmployeesByLegalEntityIDResponse employeesByLegal = null;
            try
            {
                string url = $"{Helper.PaycorUrlProduction}{urlParams}";

                HttpClient httpClient = new HttpClient();
                httpClient.Timeout = TimeSpan.FromSeconds(45);

                var httpRequestMessage = new HttpRequestMessage();
                httpRequestMessage.Method = HttpMethod.Get;
                httpRequestMessage.RequestUri = new Uri(url);
                httpRequestMessage.Headers.Add("Authorization", Helper.PaycorAuthorization);
                httpRequestMessage.Headers.Add("Ocp-Apim-Subscription-Key", Helper.PaycorAPImSubscriptionKey);

                var response = await httpClient.SendAsync(httpRequestMessage);

                if (!response.IsSuccessStatusCode)
                {
                    BLogicLogger.Error($"**** Get Employees from Paycor Error ");
                    BLogicLogger.Error($"     URL: {url} ");
                    BLogicLogger.Error($"     Authorization: {Helper.PaycorAuthorization} ");
                    BLogicLogger.Error($"     Ocp-Apim-Subscription-Key: {Helper.PaycorAPImSubscriptionKey} ");
                }
                else
                {
                    string str = await response.Content.ReadAsStringAsync();

                    var settings = new JsonSerializerSettings();
                    settings.NullValueHandling = NullValueHandling.Ignore;

                    employeesByLegal = JsonConvert.DeserializeObject<GetEmployeesByLegalEntityIDResponse>(str, settings);
                }

            }
            catch (Exception ex)
            {
                BLogicLogger.Error($"**** GetPaycorEmployees with error: {ex.Message}", ex);
            }

            return employeesByLegal;
        }
    }
}
