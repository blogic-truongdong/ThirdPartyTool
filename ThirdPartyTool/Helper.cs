using BLogicSystems.XPos.Utilites;
using Dapper;
using Newtonsoft.Json;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using ThirdPartyTool.Models;

namespace ThirdPartyTool
{

    public class Helper
    {
        public static string PaycorAPImSubscriptionKey = string.Empty;
        public static string PaycorRefreshToken = string.Empty;
        public static string PaycorClientID = string.Empty;
        public static string PaycorClientSecret = string.Empty;
        public static string PaycorAccessToken = string.Empty;
        public static string PaycorLegalEntityID = string.Empty;
        public static string PaycorAuthorization = string.Empty;
        public static string PaycorUrlProduction = "https://apis.paycor.com";

        public static string LicenseBaseUrl = string.Empty;
        public static string HOST_NAME = string.Empty;
        public static string KIOSK_REG = @"HKEY_LOCAL_MACHINE\SOFTWARE\BLogic Systems\BLogic Kiosk";
        static string mySecret = "Blogicsystems - Custom Solutions Perfected For Your Business";
        static string myIssuer = "https://blogicsystems.com";
        static string myAudience = "https://blogicsystems.com";

        public static string BLOGICPOS7_Connection = "";
        public static string Merchant_Connection = "";

        public static string BLOGICPOS7_Name = "";
        public static string Merchant_Name = "";

        public static async Task<EmployeeClockInInfoEntity> GetEmployeeClockInInfo(SqlConnection conn, object employeeID, DateTime startDate)
        {
            EmployeeClockInInfoEntity employeeClockInInfoEntity = new EmployeeClockInInfoEntity();
            try
            {
                string sql = $"select TipRate, TipRateOT, RolesID from TimeClock where EmployeeID = '{employeeID}' and (ClockStatus = 1 OR (DateTimeIn <= '{startDate}' and '{startDate}' <= DateTimeOut )) order by DateTimeIn desc";
                TimeClockEntity timeClockEntity = await conn.QueryFirstOrDefaultAsync<TimeClockEntity>(sql, null, null, 180, System.Data.CommandType.Text);

                if (timeClockEntity != null)
                {
                    employeeClockInInfoEntity.RolesID = timeClockEntity.RolesID;
                    employeeClockInInfoEntity.TipRate = timeClockEntity.TipRate;
                    employeeClockInInfoEntity.TipRateOT = timeClockEntity.TipRateOT;
                }
                else
                {
                    sql = $"select IsUsingRoleTipPool,TipRate,TipRateOT from Employees where EmployeeID = '{employeeID}'";
                    EmployeeEntity employeeEntity = await conn.QueryFirstOrDefaultAsync<EmployeeEntity>(sql, null, null, 180, System.Data.CommandType.Text);

                    sql = $"select top 1 r.RolesID,TipRate,TipRateOT  from EmployeeRoles er left join Roles r on er.RolesID = r.RolesID where er.EmployeeID = '{employeeID}' order by TipRate";
                    var roleEntities = await conn.QueryAsync<RoleEntity>(sql, null, null, 180, System.Data.CommandType.Text);
                    RoleEntity roleEntity = null;
                    if (employeeEntity != null)
                    {
                        if (roleEntities?.Count() > 0)
                        {
                            roleEntity = roleEntities.ToList()[0];
                            employeeClockInInfoEntity.RolesID = roleEntity.RolesID;
                        }

                        if (employeeEntity.IsUsingRoleTipPool && roleEntity != null)
                        {
                            if (employeeEntity.EnableTrainingMode)
                            {
                                employeeClockInInfoEntity.TrainingTipRate = roleEntity.TrainingTipRate;
                                employeeClockInInfoEntity.TrainingTipRateOT = roleEntity.TrainingTipRateOT;
                            }
                            else
                            {
                                employeeClockInInfoEntity.TipRate = roleEntity.TipRate;
                                employeeClockInInfoEntity.TipRateOT = roleEntity.TipRateOT;
                            }
                        }
                        else
                        {
                            if (employeeEntity.EnableTrainingMode)
                            {

                                employeeClockInInfoEntity.TrainingTipRate = employeeEntity.TrainingTipRate;
                                employeeClockInInfoEntity.TrainingTipRateOT = employeeEntity.TrainingTipRateOT;
                            }
                            else
                            {
                                employeeClockInInfoEntity.TipRate = employeeEntity.TipRate;
                                employeeClockInInfoEntity.TipRateOT = employeeEntity.TipRateOT;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("GetEmployeeClockInInfo Exception");
            }

            return employeeClockInInfoEntity;
        }

        public static EmployeeClockInInfoEntity GetEmployeeClockInInfoEntity(SqlCommand sqlCommand, object employeeID, DateTime startDate)
        {
            EmployeeClockInInfoEntity employeeClockInInfoEntity = new EmployeeClockInInfoEntity();
            try
            {

                DataTable dtTimeClock = new DataTable("TimeClock");
                DataTable dtEmployees = new DataTable("Employees");
                DataTable dtRoles = new DataTable("Roles");

                sqlCommand.CommandText = $"select TipRate, TipRateOT, RolesID from TimeClock where EmployeeID = '{employeeID}' and (ClockStatus = 1 OR (DateTimeIn <= '{startDate}' and '{startDate}' <= DateTimeOut )) order by DateTimeIn desc";
                var reader = sqlCommand.ExecuteReader();
                dtTimeClock.Load(reader);
                reader.Close();

                if (dtTimeClock?.Rows.Count > 0)
                {
                    employeeClockInInfoEntity.RolesID = Utility.GetIntFromRow("RolesID", dtTimeClock.Rows[0]);
                    employeeClockInInfoEntity.TipRate = Utility.GetIntFromRow("TipRate", dtTimeClock.Rows[0]);
                    employeeClockInInfoEntity.TipRateOT = Utility.GetIntFromRow("TipRateOT", dtTimeClock.Rows[0]);

                    return employeeClockInInfoEntity;
                }

                sqlCommand.CommandText = $"select IsUsingRoleTipPool,TipRate,TipRateOT from Employees where EmployeeID = '{employeeID}'";
                reader = sqlCommand.ExecuteReader();
                dtEmployees.Load(reader);
                reader.Close();

                sqlCommand.CommandText = $"select top 1 r.RolesID,TipRate,TipRateOT  from EmployeeRoles er left join Roles r on er.RolesID = r.RolesID where er.EmployeeID = '{employeeID}' order by TipRate";
                reader = sqlCommand.ExecuteReader();
                dtRoles.Load(reader);
                reader.Close();

                if (dtEmployees?.Rows.Count > 0 && dtRoles?.Rows.Count > 0)
                {
                    employeeClockInInfoEntity.RolesID = Utility.GetIntFromRow("RolesID", dtRoles.Rows[0]);

                    bool IsUsingRoleTipPool = Utility.GetBoolFromRow("IsUsingRoleTipPool", dtEmployees?.Rows[0]);
                    bool EnableTrainingMode = Utility.GetBoolFromRow("EnableTrainingMode", dtEmployees?.Rows[0]);
                    if (IsUsingRoleTipPool)
                    {
                        if (EnableTrainingMode)
                        {
                            employeeClockInInfoEntity.TrainingTipRate = Utility.GetDecimalFromRow("TrainingTipRate", dtRoles?.Rows[0]);
                            employeeClockInInfoEntity.TrainingTipRateOT = Utility.GetDecimalFromRow("TrainingTipRateOT", dtRoles?.Rows[0]);
                        }
                        else
                        {
                            employeeClockInInfoEntity.TipRate = Utility.GetDecimalFromRow("TipRate", dtRoles?.Rows[0]);
                            employeeClockInInfoEntity.TipRateOT = Utility.GetDecimalFromRow("TipRateOT", dtRoles?.Rows[0]);
                        }
                    }
                    else
                    {
                        if (EnableTrainingMode)
                        {

                            employeeClockInInfoEntity.TrainingTipRate = Utility.GetDecimalFromRow("TrainingTipRate", dtEmployees?.Rows[0]);
                            employeeClockInInfoEntity.TrainingTipRateOT = Utility.GetDecimalFromRow("TrainingTipRateOT", dtEmployees?.Rows[0]);
                        }
                        else
                        {
                            employeeClockInInfoEntity.TipRate = Utility.GetDecimalFromRow("TipRate", dtEmployees?.Rows[0]);
                            employeeClockInInfoEntity.TipRateOT = Utility.GetDecimalFromRow("TipRateOT", dtEmployees?.Rows[0]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // log exception

            }

            return employeeClockInInfoEntity;
        }

        public static async Task<SaleReceiptInfoResponse> GetSaleReceiptInfoBySaleReceiptNumber(string saleReceiptNumber)
        {
            SaleReceiptInfoResponse response = new SaleReceiptInfoResponse();
            try
            {
                string creditCardTransactionID = string.Empty;

                using (var conn = new SqlConnection(Helper.BLOGICPOS7_Connection))
                {
                    var sql = $"select * from SaleReceipts where SaleReceiptNumber = '{saleReceiptNumber}'";

                    var listSaleReceipt = await conn.QueryAsync<SaleReceiptEntity>(sql, "", null, 180, System.Data.CommandType.Text);

                    if (listSaleReceipt == null || listSaleReceipt.Count() == 0)
                    {
                        response.IsSuccess = false;
                        response.Message = "SaleReceipt not found";
                        return response;
                    }

                    if (listSaleReceipt.Count() > 1)
                    {
                        response.ListSaleReceipts = listSaleReceipt.ToList();
                        response.IsMultiReceipts = true;
                        response.Message = "SaleReceipt not found";
                        return response;
                    }

                    response.SaleReceipt = listSaleReceipt.ToList()[0];

                    var saleReceiptEntity = listSaleReceipt.ToList()[0];

                    sql = $"select * from SaleReceiptDetails where SaleReceiptID = '{saleReceiptEntity.SaleReceiptID}' order by Range";
                    response.ListSaleReceiptDetails = (await conn.QueryAsync<SaleReceiptDetailEntity>(sql, "", null, 180, System.Data.CommandType.Text))?.ToList();

                    if (SystemConfigurations.ListTableNameInDatabase.Exists(x => x == "SaleReceiptDetailTaxDetails"))
                    {
                        sql = $"select * from SaleReceiptDetailTaxDetails where SaleReceiptID = '{saleReceiptEntity.SaleReceiptID}'";
                        response.ListSaleReceiptDetailTaxDetails = (await conn.QueryAsync<SaleReceiptDetailTaxDetailEntity>(sql, "", null, 180, System.Data.CommandType.Text))?.ToList();
                    }

                    if (SystemConfigurations.ListTableNameInDatabase.Exists(x => x == "SaleReceiptServiceChargeTaxDetails"))
                    {
                        sql = $"select * from SaleReceiptServiceChargeTaxDetails where SaleReceiptID = '{saleReceiptEntity.SaleReceiptID}'";
                        response.ListSaleReceiptServiceChargeTaxDetails = (await conn.QueryAsync<SaleReceiptServiceChargeTaxDetailEntity>(sql, "", null, 180, System.Data.CommandType.Text))?.ToList();
                    }

                    sql = $"select * from SaleReceiptPaymentDetails where SaleReceiptID = '{saleReceiptEntity.SaleReceiptID}'";
                    response.ListSaleReceiptPaymentDetails = (await conn.QueryAsync<SaleReceiptPaymentDetailEntity>(sql, "", null, 180, System.Data.CommandType.Text))?.ToList();
                }

                using (var conn = new SqlConnection(Helper.Merchant_Connection))
                {
                    string ids = string.Join(",", response.ListSaleReceiptPaymentDetails.Where(x => !string.IsNullOrWhiteSpace(x.CreditCardTransactionID)).Select(x => $"'{x.SaleReceiptID}'"));

                    var sql = $"select * from TransactionDetail where ID = '{ids}' ";

                    response.ListTransactionDetails = (await conn.QueryAsync<TransactionDetailEntity>(sql, "", null, 180, System.Data.CommandType.Text))?.ToList();
                }

            }
            catch (Exception ex)
            {
                BLogicLogger.Error($"BtnGetScript_Click in UpdateTipForSaleReceiptFile_0_0 error with:  {ex.Message}", ex);
            }
            return response;
        }

        public static string GetMACAddress()
        {
            String sMacAddress = string.Empty;
            try
            {
                NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();

                foreach (NetworkInterface adapter in nics)
                {
                    if (sMacAddress == String.Empty)// only return MAC Address from first card  
                    {
                        IPInterfaceProperties properties = adapter.GetIPProperties();
                        sMacAddress = adapter.GetPhysicalAddress().ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                BLogicLogger.Error($"GetMACAddress Exception: {ex.Message}", ex);
            }
            return sMacAddress;
        }
        public static string GetExternalIPAddress()
        {
            try
            {
                string externalIP;
                externalIP = (new WebClient()).DownloadString("http://checkip.dyndns.org/");
                externalIP = (new Regex(@"\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}"))
                             .Matches(externalIP)[0].ToString();
                return externalIP;
            }
            catch (Exception ex)
            {
                BLogicLogger.Error($"GetExternalIPAddress Exception: {ex.Message}", ex);
                return string.Empty;
            }
        }

        public static string GetIPAddress()
        {
            string sAdd = string.Empty;
            try
            {
                string host = Dns.GetHostName();
                IPHostEntry ipEntry = Dns.GetHostEntry(host);
                foreach (IPAddress ip in ipEntry.AddressList)
                {
                    if (ip.AddressFamily.ToString() == "InterNetwork")
                    {
                        sAdd = ip.ToString();
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                BLogicLogger.Error($"UpdateNeverExpriedLocalLicense Exception: {ex.Message}", ex);
            }
            return sAdd;
        }

        private const string BLOGIC_SYSTEMS = "blogicsystems";
        public static string Decrypt(string toDecrypt, bool useHashing)
        {
            byte[] keyArray;
            byte[] toEncryptArray = Convert.FromBase64String(toDecrypt);
            if (useHashing)
            {
                var hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(Encoding.UTF8.GetBytes(BLOGIC_SYSTEMS));
            }
            else keyArray = Encoding.UTF8.GetBytes(BLOGIC_SYSTEMS);
            var tdes = new TripleDESCryptoServiceProvider
            {
                Key = keyArray,
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };
            ICryptoTransform cTransform = tdes.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            return Encoding.UTF8.GetString(resultArray);
        }
        public static string Encrypt(string toEncrypt, bool useHashing)
        {
            byte[] keyArray;
            byte[] toEncryptArray = Encoding.UTF8.GetBytes(toEncrypt);
            if (useHashing)
            {
                var hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(Encoding.UTF8.GetBytes(BLOGIC_SYSTEMS));
            }
            else keyArray = Encoding.UTF8.GetBytes(BLOGIC_SYSTEMS);
            var tdes = new TripleDESCryptoServiceProvider
            {
                Key = keyArray,
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };
            ICryptoTransform cTransform = tdes.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        public static Stream GenerateStreamFromString(string s)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

        public static string GetKioskConnectionFromRegistry()
        {
            string host = Microsoft.Win32.Registry.GetValue(KIOSK_REG, "URL", "") as string;

            if (string.IsNullOrEmpty(host))
            {
                SetKioskConnectionToRegistry("http://localhost/BLogicService/Service.asmx");
                HOST_NAME = "http://localhost/BLogicService/Service.asmx";
                return "http://localhost/BLogicService/Service.asmx";
            }
            if (host.Contains("http://"))
            {
                SetKioskConnectionToRegistry(host);
                HOST_NAME = host;
                return host;
            }
            string IpAddress = @"http://" + host + "/BLogicService/Service.asmx";
            HOST_NAME = IpAddress;
            return IpAddress;
        }
        public static string GetKioskConnectionFromRegistry2()
        {
            string host = Microsoft.Win32.Registry.GetValue(KIOSK_REG, "URL", "") as string;
            return host;
        }


        public static void SetKioskConnectionToRegistry(string sValue)
        {
            BLogicLogger.Debug($"SetKioskConnectionToRegistry: {sValue}");
            Uri myUri = new Uri(sValue);
            string host = myUri.Host;
            HOST_NAME = sValue;
            Microsoft.Win32.Registry.SetValue(KIOSK_REG, "URL", host);
        }

        public static void SetHostNameFromRegistryForKiosk()
        {
            string sURL = GetKioskConnectionFromRegistry();
            if (string.IsNullOrEmpty(sURL))
                return;
            HOST_NAME = sURL;
        }

        public static bool PingServer()
        {
            var result = ProcessRequest("Ping", "Ping", new DataSet());
            if (result != null && result.Tables.Contains("Ping"))
                return true;
            else
                return false;
        }

        public static DataSet ProcessRequest(string sTargetFacade, string sTargetAction, DataSet dsParams)
        {
            try
            {
                XmlDocument xml = new XmlDocument();
                // var rootBase = HOST_NAME + "/NewProcessRequest";
                var rootBase = HOST_NAME + "/NewProcessRequest";
                var strJson = JsonConvert.SerializeObject(dsParams);
                using (var wc = new WebClient())
                {
                    wc.Encoding = Encoding.UTF8;
                    wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                    strJson = WebUtility.UrlEncode(strJson);
                    var body = $"sTargetFacade={sTargetFacade}&sTargetAction={sTargetAction}&dsParamsNew={strJson}";
                    var resultPaymentStr = wc.UploadString(rootBase, body);
                    if (string.IsNullOrEmpty(resultPaymentStr))
                    {
                        return null;
                    }
                    else
                    {
                        XmlReaderSettings settings = new XmlReaderSettings();
                        settings.IgnoreWhitespace = true;

                        using (XmlReader reader = XmlReader.Create(GenerateStreamFromString(resultPaymentStr)))
                        {
                            while (reader.Read())
                            {
                                if (reader.NodeType == XmlNodeType.Text)
                                {
                                    string x = reader.Value;
                                    return JsonConvert.DeserializeObject<DataSet>(x);
                                }
                            }
                        }

                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                BLogicLogger.Error($"ProcessRequest with Error: {ex.Message}", ex);
                return null;
            }
        }

        public static string GetMyPublicIp()
        {
            try
            {
                string externalIP = "";
                externalIP = (new WebClient()).DownloadString(@"http://checkip.dyndns.org/").Trim();
                externalIP = (new Regex(@"\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}"))
                                               .Matches(externalIP)[0].ToString();
                return externalIP;
            }
            catch (Exception ex)
            {
                BLogicLogger.Error($"GetMyPublicIp Exception {ex.Message}", ex);
                return null;
            }

        }

        public static string GetIPServerAddressFromRegistry()
        {
            string host = Microsoft.Win32.Registry.GetValue(KIOSK_REG, "URL", "") as string;

            if (string.IsNullOrEmpty(host) || host.Contains("localhost"))
            {
                return GetIPAddress();
            }
            if (host.Contains("http://"))
            {
                BLogicLogger.Debug($"SetKioskConnectionToRegistry: {host}");
                Uri myUri = new Uri(host);
                return myUri.Host;
            }
            return host;
        }

        public static DialogResult ShowPopUpFormWithDIMBackground(Form childForm)
        {
            BaseForm frmParent = new BaseForm(childForm);
            frmParent.ShowDialog();
            return childForm.DialogResult;
        }

    }
}
