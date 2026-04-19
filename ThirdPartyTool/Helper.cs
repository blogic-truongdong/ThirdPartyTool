using BLogicSystems.XPos.SharedModule.Models;
using BLogicSystems.XPos.Utilites;
using BLogicSystems.XPos.Utilites.Logging;
using Dapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
        public static Form MainForm = null;
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

        public static async Task<DataTable> QueryToDataTableAsync(SqlConnection conn, string sql, string tableName, object param, int timeout = 180)
        {
            using (var reader = await conn.ExecuteReaderAsync(sql, param, null, timeout, System.Data.CommandType.Text))
            {
                var dt = new DataTable(tableName);
                dt.Load(reader);
                return dt;
            }
        }

        public static string GenerateQueryInsert_Merchant_TransactionDetail(DataTable dt,
            object ID, object Date, object CardNumber, object CardType, object ExpDate, object InvoiceNo,
            object AuthCode, object RefNo, object Amount, object Gratuity, object Total, object BatchID,
            object BatchTotal, object BatchStatus, object MerchantID, object BankApprovalCode, object WorkstationID,
            object WorkstationName, object SVCFeeForGratuity, object SVCFee, object TicketNumber, object BatchUnique,
            object SubTotal, object TaxAmount, object DiscountAmount, object ServiceCharge, object TotalAmount, object PartyServiceFeeAmount,
            object AreaFeeAmount, object ServiceFee, object MerchantFee, object MerchantFeeForTip, object AutoGratuityAmount,
            object SVCFeeTaxAmount, object MerchantFeePercent, object CardNumberMasked)
        {
            string strInsert = $"INSERT INTO {Merchant_Name}.dbo.TransactionDetail (";

            try
            {
                int colCount = dt.Columns.Count;
                for (int i = 0; i < colCount; i++)
                {
                    strInsert += "[" + dt.Columns[i].ColumnName.ToUpper() + "]";
                    if (i < colCount - 1)
                        strInsert += ", ";
                }
                strInsert += ") VALUES (";
                for (int i = 0; i < colCount; i++)
                {
                    switch (dt.Columns[i].ColumnName)
                    {
                        case "ID":
                            strInsert += $"'{ID}'";
                            break;
                        case "Number":
                            strInsert += $"(select max(a.Number)+1  from Merchant.dbo.TransactionDetail a)";
                            break;
                        case "Date":
                            strInsert += $"'{Date}'";
                            break;
                        case "TranType":
                            strInsert += $"'Sale'";
                            break;
                        case "TenderType":
                            strInsert += $"'CreditCard'";
                            break;
                        case "CardNumber":
                            strInsert += $"'{CardNumber}'";
                            break;
                        case "CardType":
                            strInsert += $"'{CardType}'";
                            break;
                        case "ExpDate":
                            strInsert += $"'{ExpDate}'";
                            break;
                        case "Result":
                            strInsert += $"'Approved'";
                            break;
                        case "TextResponse":
                            strInsert += $"'OK'";
                            break;
                        case "CaptureStatus":
                        case "AVSResult":
                        case "CVVResult":
                        case "CustomerName":
                        case "ItemID":
                        case "Token":
                        case "Email":
                        case "Address":
                        case "ZipCode":
                        case "AcqRefData":
                        case "ProcessData":
                        case "UserTraceData":
                        case "ShiftID":
                        case "Register":
                        case "TrackNo":
                        case "TableName":
                        case "TerminalName":
                        case "ISOCode":
                        case "ProcessingSetName":
                        case "SecondaryResponseCode":
                        case "BankTransactionID":
                        case "IsChecked":
                        case "Returned":
                        case "Field7":
                        case "Field8":
                        case "AID":
                        case "TC":
                        case "TicketCaptured":
                        case "MerchantServer":
                        case "Pin":
                        case "CreditCardInfo":
                        case "AdjustTipInformation":
                            strInsert += $"''";
                            break;
                        case "InvoiceNo":
                            strInsert += $"'{InvoiceNo}'";
                            break;
                        case "AuthCode":
                            strInsert += $"'{AuthCode}'";
                            break;
                        case "RefNo":
                            strInsert += $"'{RefNo}'";
                            break;
                        case "AuthorizeAmount":
                        case "Amount":
                        case "OriginalAmount":
                            strInsert += $"'{Amount}'";
                            break;
                        case "Gratuity":
                        case "OriginalTip":
                            strInsert += $"'{Gratuity}'";
                            break;
                        case "Total":
                            strInsert += $"'{Total}'";
                            break;
                        case "BatchID":
                            strInsert += $"'{BatchID}'";
                            break;
                        case "BatchTotal":
                            strInsert += $"'{BatchTotal}'";
                            break;
                        case "BatchStatus":
                            strInsert += $"'{BatchStatus}'";
                            break;
                        case "CCEntry":
                            strInsert += $"'CONTACTLESS'";
                            break;
                        case "Description":
                            strInsert += $"'Success'";
                            break;
                        case "ReturnCode":
                            strInsert += $"'000000'";
                            break;
                        case "MerchantID":
                        case "RegisterID":
                        case "MerchantAccount":
                            strInsert += $"'{MerchantID}'";
                            break;
                        case "UserID":
                        case "ServerID":
                        case "AmountCaptured":
                        case "TipCaptured":
                            strInsert += $"'-1'";
                            break;
                        case "Cashier":
                        case "ServerName":
                            strInsert += $"'BLogic'";
                            break;
                        case "TranStatus":
                            strInsert += $"'ACTIVE'";
                            break;
                        case "BankApprovalCode":
                            strInsert += $"'{BankApprovalCode}'";
                            break;
                        case "WorkstationID":
                            strInsert += $"'{WorkstationID}'";
                            break;
                        case "WorkstationName":
                            strInsert += $"'{WorkstationName}'";
                            break;
                        case "OrderID":
                            strInsert += $"(select max(a.OrderID)+1  from Merchant.dbo.TransactionDetail a)";
                            break;
                        case "SVCFeeForGratuity":
                            strInsert += $"'{SVCFeeForGratuity}'";
                            break;
                        case "IsTipInLine":
                            strInsert += $"'0'";
                            break;
                        case "SVCFee":
                            strInsert += $"'{SVCFee}'";
                            break;
                        case "TicketNumber":
                            strInsert += $"'{TicketNumber}'";
                            break;
                        case "BatchUnique":
                            strInsert += $"'{BatchUnique}'";
                            break;
                        case "SubTotal":
                            strInsert += $"'{SubTotal}'";
                            break;
                        case "TaxAmount":
                            strInsert += $"'{TaxAmount}'";
                            break;
                        case "DiscountAmount":
                            strInsert += $"'{DiscountAmount}'";
                            break;
                        case "ServiceCharge":
                            strInsert += $"'{ServiceCharge}'";
                            break;
                        case "TotalAmount":
                            strInsert += $"'{TotalAmount}'";
                            break;
                        case "PartyServiceFeeAmount":
                            strInsert += $"'{PartyServiceFeeAmount}'";
                            break;
                        case "AreaFeeAmount":
                            strInsert += $"'{AreaFeeAmount}'";
                            break;
                        case "ServiceFee":
                            strInsert += $"'{ServiceFee}'";
                            break;
                        case "TerminalIpAddress":
                            strInsert += $"''";
                            break;
                        case "MerchantFee":
                            strInsert += $"'{MerchantFee}'";
                            break;
                        case "MerchantFeeForTip":
                            strInsert += $"'{MerchantFeeForTip}'";
                            break;
                        case "IsTryVoidOpenTab":
                        case "IsPhone":
                            strInsert += $"'0'";
                            break;
                        case "AutoGratuityAmount":
                            strInsert += $"'{AutoGratuityAmount}'";
                            break;
                        case "TicketOrder":
                            strInsert += $"(select max(a.TicketOrder)+1  from Merchant.dbo.TransactionDetail a)";
                            break;
                        case "SVCFeeTaxAmount":
                            strInsert += $"'{SVCFeeTaxAmount}'";
                            break;
                        case "SVCFeeType":
                            strInsert += $"'1'";
                            break;
                        case "MerchantFeePercent":
                            strInsert += $"'{MerchantFeePercent}'";
                            break;
                        case "CardNumberMasked":
                            strInsert += $"'{CardNumberMasked}'";
                            break;
                        default:
                            //strInsert += "@" + dt.Columns[i].ColumnName.ToUpper();
                            break;
                    }

                    if (i < colCount - 1)
                        strInsert += ", ";
                }
                strInsert += "); ";
            }
            catch (Exception ex)
            {
                BLogicLogger.Exception("GetQueryInsert_Merchant_TransactionDetail", ex);
            }

            return strInsert;
        }

        public static string GenerateQueryInsert_BLogicPOS7_SaleReceiptPaymentDetails(DataTable dt,
            object SaleReceiptID, object PaymentMethodID, object AmountTendered, object CreatedDate,
            object Cashier,
            object ShiftID, object Gratuity, object CreditCardTransactionID, object CreditCardTransactionNumber,
            object CreditCardTransactionCardNumber, object CreditCardTransactionType, object PaidServiceFee,
            object CreditCardRefNo, object CreditCardAuthCode, object SVCFeeAmount, object SVCFeeForGratuity,
            object SVCFeeForServiceCharge, object CashDiscountAmount, object MerchantFee, object PartyServiceFeeAmount,
            object SVCFeeForPartyServiceFeeAmount, object AreaFeeAmount, object SVCFeeForAreaFeeAmount, object ServiceFee,
            object SVCFeeForServiceFee, object MerchantFeeForTip, object MerchantFeeForServiceFee, object MerchantFeeForAreaFeeAmount,
            object MerchantFeeForPartyServiceFeeAmount, object AutoGratuityAmount, object SVCFeeForAutoGratuityAmount,
            object MerchantFeeForAutoGratuityAmount, object CreditCardCardType, object SVCFeeTaxAmount, object CashDiscountTaxAmount,
            object ServiceFeeTaxAmount, object AreaFeeTaxAmount, object PartyServiceFeeTaxAmount, object AutoGratuityTaxAmount,
            object TaxAmountByPaymentModeTaxCustomFee, bool IsCalculateTaxCustomFee, object CreditCardTransactionMerchantID)
        {
            string strInsert = $"SET IDENTITY_INSERT {BLOGICPOS7_Name}.dbo.[SaleReceiptPaymentDetails] ON {Environment.NewLine}{Environment.NewLine}" +
                $"INSERT INTO {BLOGICPOS7_Name}.dbo.SaleReceiptPaymentDetails (";

            try
            {
                int colCount = dt.Columns.Count;
                for (int i = 0; i < colCount; i++)
                {
                    strInsert += "[" + dt.Columns[i].ColumnName.ToUpper() + "]";
                    if (i < colCount - 1)
                        strInsert += ", ";
                }
                strInsert += ") VALUES (";
                for (int i = 0; i < colCount; i++)
                {
                    switch (dt.Columns[i].ColumnName)
                    {
                        case "SaleReceiptPaymentDetailID":
                            strInsert += $"(select max(a.SaleReceiptPaymentDetailID)+1 from {BLOGICPOS7_Name}.dbo.SaleReceiptPaymentDetails a)";
                            break;
                        case "SaleReceiptID":
                            strInsert += $"'{SaleReceiptID}'";
                            break;
                        case "PaymentMethodID":
                            strInsert += $"'{PaymentMethodID}'";
                            break;
                        case "AmountTendered":
                            strInsert += $"'{AmountTendered}'";
                            break;
                        case "AmountReturn":
                            strInsert += $"'0'";
                            break;
                        case "CreatedDate":
                            strInsert += $"'{CreatedDate}'";
                            break;
                        case "Cashier":
                            strInsert += $"'{Cashier}'";
                            break;
                        case "ShiftID":
                            strInsert += $"'{ShiftID}'";
                            break;
                        case "Gratuity":
                            strInsert += $"'{Gratuity}'";
                            break;
                        case "CreditCardTransactionID":
                            strInsert += $"'{CreditCardTransactionID}'";
                            break;
                        case "CreditCardTransactionNumber":
                            strInsert += $"'{CreditCardTransactionNumber}'";
                            break;
                        case "CreditCardTransactionCardNumber":
                            strInsert += $"'{CreditCardTransactionCardNumber}'";
                            break;
                        case "CreditCardTransactionType":
                            strInsert += $"'{CreditCardTransactionType}'";
                            break;
                        case "PaidServiceFee":
                            strInsert += $"'{PaidServiceFee}'";
                            break;
                        case "CreditCardRefNo":
                            strInsert += $"'{CreditCardRefNo}'";
                            break;
                        case "CreditCardAuthCode":
                            strInsert += $"'{CreditCardAuthCode}'";
                            break;
                        case "SVCFeeAmount":
                            strInsert += $"'{SVCFeeAmount}'";
                            break;
                        case "SVCFeeForGratuity":
                            strInsert += $"'{SVCFeeForGratuity}'";
                            break;
                        case "SVCFeeForServiceCharge":
                            strInsert += $"'{SVCFeeForServiceCharge}'";
                            break;
                        case "CashDiscountAmount":
                            strInsert += $"'{CashDiscountAmount}'";
                            break;
                        case "MerchantFee":
                            strInsert += $"'{MerchantFee}'";
                            break;
                        case "PartyServiceFeeAmount":
                            strInsert += $"'{PartyServiceFeeAmount}'";
                            break;
                        case "SVCFeeForPartyServiceFeeAmount":
                            strInsert += $"'{SVCFeeForPartyServiceFeeAmount}'";
                            break;
                        case "AreaFeeAmount":
                            strInsert += $"'{AreaFeeAmount}'";
                            break;
                        case "SVCFeeForAreaFeeAmount":
                            strInsert += $"'{SVCFeeForAreaFeeAmount}'";
                            break;
                        case "ServiceFee":
                            strInsert += $"'{ServiceFee}'";
                            break;
                        case "SVCFeeForServiceFee":
                            strInsert += $"'{SVCFeeForServiceFee}'";
                            break;
                        case "MerchantFeeForTip":
                            strInsert += $"'{MerchantFeeForTip}'";
                            break;
                        case "MerchantFeeForServiceFee":
                            strInsert += $"'{MerchantFeeForServiceFee}'";
                            break;
                        case "MerchantFeeForAreaFeeAmount":
                            strInsert += $"'{MerchantFeeForAreaFeeAmount}'";
                            break;
                        case "MerchantFeeForPartyServiceFeeAmount":
                            strInsert += $"'{MerchantFeeForPartyServiceFeeAmount}'";
                            break;
                        case "AutoGratuityAmount":
                            strInsert += $"'{AutoGratuityAmount}'";
                            break;
                        case "SVCFeeForAutoGratuityAmount":
                            strInsert += $"'{SVCFeeForAutoGratuityAmount}'";
                            break;
                        case "MerchantFeeForAutoGratuityAmount":
                            strInsert += $"'{MerchantFeeForAutoGratuityAmount}'";
                            break;
                        case "CreditCardCardType":
                            strInsert += $"'{CreditCardCardType}'";
                            break;
                        case "SVCFeeTaxAmount":
                            strInsert += $"'{SVCFeeTaxAmount}'";
                            break;
                        case "CashDiscountTaxAmount":
                            strInsert += $"'{CashDiscountTaxAmount}'";
                            break;
                        case "ServiceFeeTaxAmount":
                            strInsert += $"'{ServiceFeeTaxAmount}'";
                            break;
                        case "AreaFeeTaxAmount":
                            strInsert += $"'{AreaFeeTaxAmount}'";
                            break;
                        case "PartyServiceFeeTaxAmount":
                            strInsert += $"'{PartyServiceFeeTaxAmount}'";
                            break;
                        case "AutoGratuityTaxAmount":
                            strInsert += $"'{AutoGratuityTaxAmount}'";
                            break;
                        case "TaxAmountByPaymentModeTaxCustomFee":
                            strInsert += $"'{TaxAmountByPaymentModeTaxCustomFee}'";
                            break;
                        case "IsCalculateTaxCustomFee":
                            strInsert += IsCalculateTaxCustomFee ? "'1'" : $"'0'";
                            break;
                        case "CreditCardTransactionMerchantID":
                            strInsert += $"'{CreditCardTransactionMerchantID}'";
                            break;

                        case "IsDeposit":
                        case "GiftExtra":
                        case "CurrencyAmount":
                        case "PaymentType":
                        case "PaidPayout":
                        case "CardBalance":
                        case "IsDecline":
                        case "IsDepositPayment":
                        case "CoinDiscountAmount":
                        case "PaidHouseAccount":
                            strInsert += $"'0'";
                            break;
                        case "CurrencyID":
                            strInsert += $"'1'";
                            break;
                        case "EmployeePaidID":
                            strInsert += $"'-1'";
                            break;
                        case "GiftCardCode":
                        case "PaymentReceiptID":
                        case "PayByAccuPoint":
                        case "HouseAccountTransactionID":
                            strInsert += $"null";
                            break;
                        case "CardHolderName":
                        case "HouseAccountNumber":
                        case "GiftCardTransactionID":
                            strInsert += $"''";
                            break;
                        default:
                            //strInsert += "@" + dt.Columns[i].ColumnName.ToUpper();
                            break;
                    }

                    if (i < colCount - 1)
                        strInsert += ", ";
                }
                strInsert += $"); {Environment.NewLine}{Environment.NewLine}" +
                    $"SET IDENTITY_INSERT {BLOGICPOS7_Name}.dbo.[SaleReceiptPaymentDetails] OFF";
            }
            catch (Exception ex)
            {
                BLogicLogger.Exception("GetQueryInsert_Merchant_TransactionDetail", ex);
            }

            return strInsert;
        }



        public static decimal CalculateSVCFee(decimal amount, decimal svcFee, int svcFeeType, int sTaxIncludeInSVCFee,
            bool isApplyTaxCustomFee = false, decimal dSVCFeeTaxRate = 0, decimal dSVCFeeTaxAmountByPayment = 0)
        {
            decimal svcFeeReturn = 0;
            try
            {
                if (amount == 0 || svcFee == 0)
                    return svcFeeReturn;
                //Configuration conf = new Configuration();
                //entity = conf.GetConfigurationEntityByName("CreditCardConfigValue.SVCFee");
                //if (entity != null)
                //    svcFee = Utility.GetDecimalFromObject(entity.ConfigurationValue);

                //entity = conf.GetConfigurationEntityByName("CreditCardConfigValue.SVCFeeType");
                //if (entity != null)
                //    svcFeeType = Utility.GetIntFromObject(entity.ConfigurationValue);


                if (isApplyTaxCustomFee)
                {
                    if (svcFeeType == 1)
                    {

                        //entity = conf.GetConfigurationEntityByName("CreditCardConfigValue.TaxIncludeInSVCFee");
                        //if (entity != null)
                        //    sTaxIncludeInSVCFee = Utility.GetIntFromObject(entity.ConfigurationValue);

                        svcFeeReturn = Utility.RoundUp(amount * (svcFee / 100));
                        if (sTaxIncludeInSVCFee == 1 && dSVCFeeTaxAmountByPayment != 0)
                        {
                            decimal tempTax = dSVCFeeTaxRate <= 0 ? 0 : Utility.RoundUp((svcFeeReturn / (1 + dSVCFeeTaxRate / 100)) * dSVCFeeTaxRate / 100);
                            svcFeeReturn -= tempTax;
                        }
                    }
                    else
                        svcFeeReturn = 0;
                }
                else
                {
                    if (svcFeeType == 1)
                        svcFeeReturn = Utility.RoundUp(amount * (svcFee / 100));
                    else
                        svcFeeReturn = 0;
                }
            }
            catch (Exception ex)
            {
                BLogicServerLogger.Error($"####=> CalculateSVCFee - Exception - ", ex);
                svcFeeReturn = 0;
                BLogicLogger.Error("#### CalculateSVCFee error: " + ex);
            }
            return svcFeeReturn;
        }

        public static decimal CalculateSVCFee(decimal amount, bool isApplyTaxCustomFee, decimal dSVCFeeTaxRate, decimal dSVCFeeTaxAmountByPayment,
            int sTaxIncludeInSVCFee, decimal svcFee, int svcFeeType)
        {
            decimal svcFeeReturn = 0;

            try
            {
                if (amount == 0)
                    return svcFeeReturn;

                if (isApplyTaxCustomFee)
                {
                    if (svcFeeType == 1)
                    {
                        svcFeeReturn = Utility.RoundUp(amount * (svcFee / 100));
                        if (sTaxIncludeInSVCFee == 1 && dSVCFeeTaxAmountByPayment != 0)
                        {
                            decimal tempTax = dSVCFeeTaxRate <= 0 ? 0 : Utility.RoundUp((svcFeeReturn / (1 + dSVCFeeTaxRate / 100)) * dSVCFeeTaxRate / 100);
                            svcFeeReturn -= tempTax;
                        }
                    }
                    else
                        svcFeeReturn = 0;
                }
                else
                {
                    if (svcFeeType == 1)
                        svcFeeReturn = Math.Round(amount * (svcFee / 100), 2);
                    else
                        svcFeeReturn = 0;
                }
            }
            catch (Exception ex)
            {
                svcFeeReturn = 0;
                BLogicLogger.Error("#### CalculateSVCFee error: " + ex);
            }
            return svcFeeReturn;
        }

        public static decimal CalculateMerchantFee(decimal amount, decimal merchantFeeValue)
        {
            decimal merchantFeeAmount = 0;
            //decimal merchantFeeValue = 0;
            //ConfigurationsEntity entity = null;

            //Configuration conf = new Configuration();
            //entity = conf.GetConfigurationEntityByName("CreditCardConfigValue.MerchantFee");
            //if (entity != null)
            //    merchantFeeValue = Utility.GetDecimalFromObject(entity.ConfigurationValue);

            try
            {

                if (merchantFeeValue == 0)
                    return 0;
                merchantFeeAmount = Utility.RoundUp(amount * (merchantFeeValue / 100));
            }
            catch (Exception ex)
            {
                BLogicServerLogger.Error($"####=> CalculateMerchantFee - Exception", ex);
                merchantFeeAmount = 0;
                BLogicLogger.Error("#### CalculateMerchantFee error: " + ex);
            }
            return merchantFeeAmount;
        }


        public static async Task RunQueryUpdateAsync(SqlConnection conn, string sql, object parameters = null)
        {
            try
            {
                var result = await conn.ExecuteAsync(sql, parameters, null, 180, System.Data.CommandType.Text);

                if (result < 0)
                {
                    BLogicLogger.Exception("RunQueryUpdateAsync in ProcessForm", null, new System.Collections.Generic.List<object>() { sql, parameters });
                }
            }
            catch (Exception ex)
            {
                BLogicLogger.Exception("RunQueryUpdateAsync in ProcessForm", ex, new List<object>() { sql, parameters });
            }

        }

        public static int RunQueryUpdate(SqlCommand sqlCommand, string scriptUpdate, SqlParameter[] cmdParams = null)
        {
            int result = -1;
            BLogicLogger.Error($"Run Script for {sqlCommand.Connection.Database} : {scriptUpdate}");
            sqlCommand.CommandText = scriptUpdate;
            sqlCommand.CommandType = CommandType.Text;

            if (cmdParams != null)
            {
                for (int i = 0; i < cmdParams.Length; i++)
                {
                    SqlParameter parm = cmdParams[i];
                    sqlCommand.Parameters.Add(parm);
                }
            }

            result = sqlCommand.ExecuteNonQuery();
            if (result < 0)
            {
                BLogicLogger.Exception("RunQueryUpdate", null, new System.Collections.Generic.List<object>() { scriptUpdate });
            }

            return result;
        }

        public static DataTable ProcessQuery(bool isRunOnBLogicPOS7, string sql, string tableName)
        {
            DataTable dtReturn = new DataTable(tableName);

            try
            {
                using (SqlCommand sqlCommand = new SqlCommand())
                using (SqlConnection conn = new SqlConnection(isRunOnBLogicPOS7 ? BLOGICPOS7_Connection : Merchant_Connection))
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    sqlCommand.Connection = conn;
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.CommandText = sql;

                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    dtReturn.Load(reader);
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                BLogicLogger.Exception("ProcessQuery in Helper", ex, new List<object>() { sql });
            }

            return dtReturn;
        }

        public static object CreateQuerySelectUnique(bool isRunOnBLogicPOS7, string sql)
        {
            object result = null;

            DataTable dtReturn = new DataTable();

            try
            {
                using (SqlCommand sqlCommand = new SqlCommand())
                using (SqlConnection conn = new SqlConnection(isRunOnBLogicPOS7 ? BLOGICPOS7_Connection : Merchant_Connection))
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    sqlCommand.Connection = conn;
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.CommandText = sql;

                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    dtReturn.Load(reader);
                    reader.Close();

                    if (dtReturn?.Rows.Count > 0)
                    {
                        result = dtReturn.Rows[0][0];
                    }
                }
            }
            catch (Exception ex)
            {
                BLogicLogger.Exception("ProcessQuery in Helper", ex, new List<object>() { sql });
            }

            return result;
        }

        public static List<T> ProcessQueryEntity<T>(bool isRunOnBLogicPOS7, string sql)
        {
            List<T> result = null;
            try
            {
                using (SqlConnection conn = new SqlConnection(isRunOnBLogicPOS7 ? BLOGICPOS7_Connection : Merchant_Connection))
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();

                    result = conn.QueryAsync<T>(sql, null, null, 180, System.Data.CommandType.Text).Result?.ToList();
                }
            }
            catch (Exception ex)
            {
                BLogicLogger.Exception("ProcessQueryEntity in Helper", ex, new List<object>() { sql });
            }

            return result;
        }

        public static int RunQueryUpdate(bool isRunOnBLogicPOS7, string scriptUpdate, SqlParameter[] cmdParams = null)
        {
            int result = -1;

            try
            {
                BLogicLogger.Error($"Run Update Script: {scriptUpdate}");

                using (SqlCommand sqlCommand = new SqlCommand())
                using (SqlConnection conn = new SqlConnection(isRunOnBLogicPOS7 ? BLOGICPOS7_Connection : Merchant_Connection))
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();

                    sqlCommand.Connection = conn;
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.CommandText = scriptUpdate;

                    if (cmdParams != null)
                    {
                        for (int i = 0; i < cmdParams.Length; i++)
                        {
                            SqlParameter parm = cmdParams[i];
                            sqlCommand.Parameters.Add(parm);
                        }
                    }

                    result = sqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                BLogicLogger.Exception("RunQueryUpdate in Helper", ex, new System.Collections.Generic.List<object>() { scriptUpdate });
            }

            return result;
        }

        public static decimal CalculateSVCFeeAmountForTip(decimal total)
        {

            if (POSConfig.Configuration.ConfigValue.CreditCardConfigValue.SVCFeeType == 1)
            {
                decimal sVCFee = POSConfig.Configuration.ConfigValue.CreditCardConfigValue.SVCFee;
                return total - Utility.RoundUp(total / (1m + sVCFee / 100m));
            }

            return 0m;
        }

        public static void UpdateBatchByBatchUnique(object batchUnique)
        {
            try
            {
                var sql = $"select * from TransactionDetail where BatchUnique = '{batchUnique}'";

                DataTable dtTransactionDetail = ProcessQuery(false, sql, "TransactionDetail");

                if (dtTransactionDetail?.Rows.Count > 0)
                {
                    sql = $@"select sum(case when (TranType = 'Sale' or TranType = 'PreAuthCapture') and TranStatus = 'ACTIVE' then Total else 0 end ) -	sum(case when TranType = 'Return' then Total else 0 end ) as Total,
                       sum(case when (TranType = 'Sale' or TranType = 'PreAuthCapture') and TranStatus = 'ACTIVE' then SVCFeeForGratuity else 0 end ) as TotalSVCFeeForTip, 
                       sum(case when (TranType = 'Sale' or TranType = 'PreAuthCapture') and TranStatus = 'ACTIVE' then Amount - SVCFee else 0 end ) as TotalSales, 
                       sum(case when TranType = 'Return' then Amount - SVCFee else 0 end ) as TotalReturns, 
                       sum(case when (TranType = 'Sale' or TranType = 'PreAuthCapture') and TranStatus = 'ACTIVE' then Gratuity else 0 end ) as TotalTips, 
                       sum(case when (TranType = 'Sale' or TranType = 'PreAuthCapture') and TranStatus = 'ACTIVE' then SVCFee else 0 end ) - sum(case when TranType = 'Return' then SVCFee else 0 end ) as TotalSVCFeeAmount,
                       sum(case when (TranType = 'Sale' or TranType = 'PreAuthCapture') and TranStatus = 'ACTIVE' then Total - MerchantFee else 0 end ) -	sum(case when TranType = 'Return' then Total else 0 end ) as DepositAmount, 
                       sum(case when (TranType = 'Sale' or TranType = 'PreAuthCapture') and TranStatus = 'ACTIVE' then MerchantFee else 0 end ) as MerchantFeeAmount,
                       count(ID) as TicketCount 
                       from TransactionDetail where BatchUnique = '{batchUnique}'";

                    DataTable dtCalculateBatch = ProcessQuery(false, sql, "TransactionDetail");

                    if (dtCalculateBatch?.Rows.Count > 0)
                    {
                        sql = $"update Batch set " +
                            $" Total = '{dtCalculateBatch.Rows[0]["Total"]}'," +
                            $" TotalSVCFeeForTip = {dtCalculateBatch.Rows[0]["TotalSVCFeeForTip"]} ," +
                            $" TotalSales = {dtCalculateBatch.Rows[0]["TotalSales"]} ," +
                            $" TotalTips = '{dtCalculateBatch.Rows[0]["TotalTips"]}', " +
                            $" TotalSVCFeeAmount = {dtCalculateBatch.Rows[0]["TotalSVCFeeAmount"]}," +
                            $" DepositAmount = {dtCalculateBatch.Rows[0]["DepositAmount"]}," +
                            $" MerchantFeeAmount = {dtCalculateBatch.Rows[0]["MerchantFeeAmount"]}," +
                            $" Quantity = '{dtCalculateBatch.Rows[0]["TicketCount"]}'" +
                            $" where BatchUnique = '{batchUnique}';";

                        RunQueryUpdate(false, sql);

                        sql = $"update TransactionDetail set " +
                            $" BatchTotal = '{dtCalculateBatch.Rows[0]["Total"]}'" +
                            $" where BatchUnique = '{batchUnique}';";

                        RunQueryUpdate(false, sql);

                    }
                }
                else
                {
                    sql = $"delete from Batch where BatchUnique = '{batchUnique}';";

                    RunQueryUpdate(false, sql);
                }
            }
            catch (Exception ex)
            {
                BLogicLogger.Exception("UpdateBatchByBatchUnique in Helper", ex, new List<object>() { batchUnique });
            }
        }

        public static void ReCalculateServiceChargesInSaleReceiptPaymentDetails(object saleReceiptID)
        {
            try
            {
                DataTable dtSaleReceipts = ProcessQuery(true, $"select * from SaleReceipts where SaleReceiptID = '{saleReceiptID}'", "SaleReceipts");
                DataTable dtSaleReceiptDetails = ProcessQuery(true, $"select * from SaleReceiptDetails where SaleReceiptID = '{saleReceiptID}'", "SaleReceiptDetails");
                var listSaleReceipts = ProcessQueryEntity<SaleReceiptEntity>(true, $"select * from SaleReceipts where SaleReceiptID = '{saleReceiptID}'");
                DataTable dtSaleReceiptPaymentDetails = ProcessQuery(true, $"select * from SaleReceiptPaymentDetails where SaleReceiptID = '{saleReceiptID}' order by CreatedDate", "dtSaleReceiptPaymentDetails");
                var listSaleReceiptPaymentDetails = ProcessQueryEntity<SaleReceiptPaymentDetailEntity>(true, $"select * from SaleReceiptPaymentDetails where SaleReceiptID = '{saleReceiptID}'");
                DataTable dtSaleReceiptServiceChargeTaxDetails = ProcessQuery(true, $"select * from SaleReceiptServiceChargeTaxDetails where SaleReceiptID = '{saleReceiptID}'", "SaleReceiptServiceChargeTaxDetails");
                DataTable dtItems = ProcessQuery(true, $"select distinct b.* from Items a left join SaleReceiptDetails b on a.ItemID = b.ItemID where SaleReceiptID = '{saleReceiptID}'", "Items");

                int _svcFeeType = 0;
                decimal _svcFee = 0;
                decimal _merchantFee = 0;
                decimal _cashDiscount = 0;
                int _dTaxIncludeInSVCFee = 0;
                string _ApplyCustomFeeWhenPurchaseGiftCard = "1";
                string _EnableDualPrice = "0";
                decimal _DualPricePercent = 0;
                string sqlEmployeePaidID = string.Empty;

                string sql = "select * from Configurations " +
                    " where ConfigurationName in ('CreditCardConfigValue.SVCFee', 'CreditCardConfigValue.SVCFeeType', 'CreditCardConfigValue.MerchantFee', 'CreditCardConfigValue.CashDiscount', " +
                    " 'CreditCardConfigValue.TaxIncludeInSVCFee','CreditCardConfigValue.ApplyCustomFeeWhenPurchaseGiftCard','CreditCardConfigValue.DualPricePercent','SystemConfigValue.EnableDualPrice')";
                var listConfig = ProcessQueryEntity<ConfigurationsEntity>(true, sql);

                foreach (var item in listConfig)
                {
                    ConfigurationsEntity entity = item as ConfigurationsEntity;
                    string _name = (item as ConfigurationsEntity).ConfigurationName;
                    switch (entity.ConfigurationName)
                    {
                        case "CreditCardConfigValue.SVCFee":
                            _svcFee = Utility.GetDecimalFromObject(entity.ConfigurationValue);
                            break;
                        case "CreditCardConfigValue.SVCFeeType":
                            _svcFeeType = Utility.GetIntFromObject(entity.ConfigurationValue);
                            break;
                        case "CreditCardConfigValue.MerchantFee":
                            _merchantFee = Utility.GetDecimalFromObject(entity.ConfigurationValue);
                            break;
                        case "CreditCardConfigValue.CashDiscount":
                            _cashDiscount = Utility.GetDecimalFromObject(entity.ConfigurationValue);
                            break;
                        case "CreditCardConfigValue.TaxIncludeInSVCFee":
                            _dTaxIncludeInSVCFee = Utility.GetIntFromObject(entity.ConfigurationValue);
                            break;
                        case "CreditCardConfigValue.ApplyCustomFeeWhenPurchaseGiftCard":
                            _ApplyCustomFeeWhenPurchaseGiftCard = Utility.GetStringFromObject(entity.ConfigurationValue);
                            break;
                        case "SystemConfigValue.EnableDualPrice":
                            _EnableDualPrice = Utility.GetStringFromObject(entity.ConfigurationValue);
                            break;
                        case "CreditCardConfigValue.DualPricePercent":
                            _DualPricePercent = Utility.GetDecimalFromObject(entity.ConfigurationValue);
                            break;
                    }
                }

                if (dtSaleReceipts?.Rows.Count > 0 && listSaleReceipts?.Count() > 0)
                {
                    DataRow saleReceiptRow = dtSaleReceipts.Rows[0];
                    SaleReceiptEntity oReceipt = listSaleReceipts.FirstOrDefault();
                    oReceipt.SaleReceiptPaymentList = listSaleReceiptPaymentDetails.ToList();

                    bool isApplyTaxCustomFee = false;
                    SaleReceipts_ReceiptInfo receiptInfo = null;
                    if (!string.IsNullOrWhiteSpace(oReceipt.ReceiptInfo))
                    {
                        receiptInfo = Utility.ParseJsonToReceiptInfo(oReceipt.ReceiptInfo);
                        if (receiptInfo != null)
                            isApplyTaxCustomFee = receiptInfo.IsApplyTaxForSVCFee;
                    }

                    if (oReceipt.SVCFeeTaxAmount != 0)
                        isApplyTaxCustomFee = true;

                    decimal SalesOrderFee = Utility.GetDecimalFromRow("SalesOrderFee", saleReceiptRow);
                    decimal TotalAmount = Utility.GetDecimalFromRow("TotalAmount", saleReceiptRow);
                    decimal ServiceChargeTaxAmount = Utility.GetDecimalFromRow("ServiceChargeTaxAmount", saleReceiptRow);
                    decimal TaxAmount = Utility.GetDecimalFromRow("TaxAmount", saleReceiptRow);

                    decimal taxitem = TaxAmount - ServiceChargeTaxAmount;
                    decimal tempAmount = TotalAmount + taxitem - TaxAmount;
                    decimal dTotalAmountExcludeServiceFee = tempAmount - SalesOrderFee;
                    decimal dTotalPaidServiceFee = 0;
                    decimal dPartyServiceFeeAmount = 0;
                    bool IsPartyServiceFeeAmount = false;
                    decimal tempTaxServiceCharge = 0;
                    decimal dTotalAmountTender = 0;
                    decimal dTotalServiceFee = 0;
                    decimal dAreaFeeAmount = 0;
                    decimal dServiceFee = 0;
                    decimal dOpenTabAmount = 0;
                    decimal dAutoGratuityAmount = 0;

                    decimal dServiceFeeTaxAmount = 0;
                    decimal dAreaFeeTaxAmount = 0;
                    decimal dPartyServiceFeeTaxAmount = 0;
                    decimal dAutoGratuityTaxAmount = 0;
                    bool IsAutoGratuityAmount = false;
                    bool IsAreaFeeAmount = false;
                    bool IsServiceFee = false;

                    decimal dServiceFeeNew = Utility.GetDecimalFromObject(oReceipt.ServiceFee) + oReceipt.ExtraFeeAmount;
                    decimal sServiceChargeTaxRate = Utility.GetDecimalFromObject(oReceipt.ServiceChargeTaxRate);

                    foreach (var oPaymentDetail in oReceipt.SaleReceiptPaymentList)
                    {
                        dTotalAmountTender += oPaymentDetail.AmountTendered;

                        decimal dTempAmountTotal = TotalAmount;

                        if (dTempAmountTotal > dTotalAmountTender)
                            dTempAmountTotal = dTotalAmountTender;

                        if (dTotalAmountExcludeServiceFee <= dTotalAmountTender)
                        {
                            decimal dCurrentServiceFee = dTempAmountTotal - dTotalAmountExcludeServiceFee - dTotalPaidServiceFee;

                            oPaymentDetail.PaidServiceFee = dCurrentServiceFee;

                            dTotalPaidServiceFee += dCurrentServiceFee;

                            if (oReceipt.PartyServiceFeeAmount > 0 && oReceipt.PartyServiceFeeAmount + oReceipt.PartyServiceFeeTaxAmount > dPartyServiceFeeAmount && dCurrentServiceFee > 0)
                            {
                                decimal tempPartyFeeAmountNeeded = RoundingUtility.Rounding.RoundUp(oReceipt.PartyServiceFeeAmount - dPartyServiceFeeAmount);
                                decimal tempPartyFeeAmount = dCurrentServiceFee > tempPartyFeeAmountNeeded ? tempPartyFeeAmountNeeded : dCurrentServiceFee;

                                oPaymentDetail.PartyServiceFeeAmount = tempPartyFeeAmount;
                                dPartyServiceFeeAmount += tempPartyFeeAmount;
                                dCurrentServiceFee -= tempPartyFeeAmount;

                                if (!IsPartyServiceFeeAmount && dCurrentServiceFee > 0)
                                {
                                    tempTaxServiceCharge = dCurrentServiceFee > oReceipt.PartyServiceFeeTaxAmount ? oReceipt.PartyServiceFeeTaxAmount : dCurrentServiceFee;
                                    oPaymentDetail.PartyServiceFeeAmount += tempTaxServiceCharge;// oReceipt.PartyServiceFeeTaxAmount;
                                    dPartyServiceFeeAmount += tempTaxServiceCharge;
                                    dCurrentServiceFee -= tempTaxServiceCharge;

                                    if (oPaymentDetail.PartyServiceFeeAmount >= oReceipt.PartyServiceFeeAmount + oReceipt.PartyServiceFeeTaxAmount)
                                        IsPartyServiceFeeAmount = true;
                                }

                                //oPaymentDetail.PartyServiceFeeTaxAmount = CalculateTaxServiceCharge(oPaymentDetail.PartyServiceFeeAmount, ratioSC, oReceipt.PartyServiceFeeTaxAmount);
                                oPaymentDetail.PartyServiceFeeTaxAmount = CalculateTaxServiceCharge(oPaymentDetail.PartyServiceFeeAmount, sServiceChargeTaxRate, oReceipt.PartyServiceFeeTaxAmount, dtSaleReceiptServiceChargeTaxDetails);
                                dPartyServiceFeeTaxAmount += oPaymentDetail.PartyServiceFeeTaxAmount;
                            }

                            if (oReceipt.AreaFeeAmount > 0 && oReceipt.AreaFeeAmount + oReceipt.AreaFeeTaxAmount > dAreaFeeAmount && dCurrentServiceFee > 0)
                            {
                                decimal tempAreaFeeAmountNeeded = RoundingUtility.Rounding.RoundUp(oReceipt.AreaFeeAmount - dAreaFeeAmount);
                                decimal tempAreaFeeAmount = dCurrentServiceFee > tempAreaFeeAmountNeeded ? tempAreaFeeAmountNeeded : dCurrentServiceFee;

                                oPaymentDetail.AreaFeeAmount = tempAreaFeeAmount;
                                dAreaFeeAmount += tempAreaFeeAmount;
                                dCurrentServiceFee -= tempAreaFeeAmount;

                                if (!IsAreaFeeAmount && dCurrentServiceFee > 0)
                                {
                                    tempTaxServiceCharge = dCurrentServiceFee > oReceipt.AreaFeeTaxAmount ? oReceipt.AreaFeeTaxAmount : dCurrentServiceFee;
                                    oPaymentDetail.AreaFeeAmount += tempTaxServiceCharge;// oReceipt.AreaFeeTaxAmount;
                                    dAreaFeeAmount += tempTaxServiceCharge;
                                    dCurrentServiceFee -= tempTaxServiceCharge;

                                    if (oPaymentDetail.AreaFeeAmount >= oReceipt.AreaFeeAmount + oReceipt.AreaFeeTaxAmount)
                                        IsAreaFeeAmount = true;
                                }

                                //oPaymentDetail.AreaFeeTaxAmount = CalculateTaxServiceCharge(oPaymentDetail.AreaFeeAmount, ratioSC, oReceipt.AreaFeeTaxAmount);
                                oPaymentDetail.AreaFeeTaxAmount = CalculateTaxServiceCharge(oPaymentDetail.AreaFeeAmount, sServiceChargeTaxRate, oReceipt.AreaFeeTaxAmount, dtSaleReceiptServiceChargeTaxDetails);
                                dAreaFeeTaxAmount += oPaymentDetail.AreaFeeTaxAmount;
                            }

                            if (dServiceFeeNew > 0 && dServiceFeeNew + oReceipt.ServiceFeeTaxAmount + oReceipt.ExtraFeeTaxAmount > dServiceFee && dCurrentServiceFee > 0)
                            {
                                decimal tempServiceFeeAmountNeeded = RoundingUtility.Rounding.RoundUp((decimal)dServiceFeeNew - dServiceFee);
                                decimal tempServiceFeeAmount = dCurrentServiceFee > tempServiceFeeAmountNeeded ? tempServiceFeeAmountNeeded : dCurrentServiceFee;

                                oPaymentDetail.ServiceFee = tempServiceFeeAmount;
                                dServiceFee += tempServiceFeeAmount;
                                dCurrentServiceFee -= tempServiceFeeAmount;

                                if (!IsServiceFee && dCurrentServiceFee > 0)
                                {
                                    tempTaxServiceCharge = dCurrentServiceFee > oReceipt.ServiceFeeTaxAmount + oReceipt.ExtraFeeTaxAmount ? oReceipt.ServiceFeeTaxAmount + oReceipt.ExtraFeeTaxAmount : dCurrentServiceFee;
                                    oPaymentDetail.ServiceFee += tempTaxServiceCharge; // oReceipt.ServiceFeeTaxAmount + oReceipt.ExtraFeeTaxAmount;
                                    dServiceFee += tempTaxServiceCharge;
                                    dCurrentServiceFee -= tempTaxServiceCharge;

                                    if (oPaymentDetail.ServiceFee >= dServiceFeeNew + oReceipt.ServiceFeeTaxAmount + oReceipt.ExtraFeeTaxAmount)
                                        IsServiceFee = true;
                                }

                                //oPaymentDetail.ServiceFeeTaxAmount = CalculateTaxServiceCharge(oPaymentDetail.ServiceFee, ratioSC, oReceipt.ServiceFeeTaxAmount + oReceipt.ExtraFeeTaxAmount);
                                oPaymentDetail.ServiceFeeTaxAmount = CalculateTaxServiceCharge(oPaymentDetail.ServiceFee, sServiceChargeTaxRate, oReceipt.ServiceFeeTaxAmount + oReceipt.ExtraFeeTaxAmount, dtSaleReceiptServiceChargeTaxDetails);
                                dServiceFeeTaxAmount += oPaymentDetail.ServiceFeeTaxAmount;
                            }

                            if (oReceipt.AutoGratuityAmount > 0 && oReceipt.AutoGratuityAmount + oReceipt.AutoGratuityTaxAmount > dAutoGratuityAmount && dCurrentServiceFee > 0)
                            {
                                decimal tempAutoGratuityNeeded = RoundingUtility.Rounding.RoundUp(oReceipt.AutoGratuityAmount - dAutoGratuityAmount);
                                decimal tempAutoGratuityAmount = dCurrentServiceFee > tempAutoGratuityNeeded ? tempAutoGratuityNeeded : dCurrentServiceFee;

                                oPaymentDetail.AutoGratuityAmount = tempAutoGratuityAmount;

                                dAutoGratuityAmount += tempAutoGratuityAmount;
                                dCurrentServiceFee -= tempAutoGratuityAmount;

                                if (!IsAutoGratuityAmount && dCurrentServiceFee > 0)
                                {
                                    tempTaxServiceCharge = dCurrentServiceFee > oReceipt.AutoGratuityTaxAmount ? oReceipt.AutoGratuityTaxAmount : dCurrentServiceFee;
                                    oPaymentDetail.AutoGratuityAmount += tempTaxServiceCharge;// oReceipt.AutoGratuityTaxAmount;
                                    dAutoGratuityAmount += tempTaxServiceCharge;
                                    dCurrentServiceFee -= tempTaxServiceCharge;

                                    if (oPaymentDetail.AutoGratuityAmount >= oReceipt.AutoGratuityAmount + oReceipt.AutoGratuityTaxAmount)
                                        IsAutoGratuityAmount = true;
                                }

                                //oPaymentDetail.AutoGratuityTaxAmount = CalculateTaxServiceCharge(oPaymentDetail.AutoGratuityAmount, ratioSC, oReceipt.AutoGratuityTaxAmount);
                                oPaymentDetail.AutoGratuityTaxAmount = CalculateTaxServiceCharge(oPaymentDetail.AutoGratuityAmount, sServiceChargeTaxRate, oReceipt.AutoGratuityTaxAmount, dtSaleReceiptServiceChargeTaxDetails);
                                dAutoGratuityTaxAmount += oPaymentDetail.AutoGratuityTaxAmount;
                            }


                        }

                        UpdateTaxServiceChargeinPayment(dAutoGratuityTaxAmount, dAreaFeeTaxAmount, dPartyServiceFeeTaxAmount, dServiceFeeTaxAmount, oReceipt);

                        if (oReceipt.TransactionStatusID == TransactionStatus.RETURN)
                        {
                            oPaymentDetail.AmountReturn *= -1;
                            oPaymentDetail.AmountTendered *= -1;
                        }

                        if (dTotalAmountTender >= oReceipt.TotalAmount)
                            oPaymentDetail.PaidPayout = GetPaidPayout(dtSaleReceiptDetails, dtItems);

                        int cashAdjust = 1;
                        if (_EnableDualPrice == "1" && _DualPricePercent > 0)
                        {
                            sql = $"select IsDualPrice from PaymentMethods where PaymentMethodID = '{oPaymentDetail.PaymentMethodID}'";
                            var tempResult = CreateQuerySelectUnique(true, sql);
                            cashAdjust = Utility.GetBoolFromObject(tempResult) ? 2 : 1;
                        }
                        else
                        {
                            sql = $"select CashAdjust from PaymentMethods where PaymentMethodID = '{oPaymentDetail.PaymentMethodID}'";
                            var tempResult = CreateQuerySelectUnique(true, sql);
                            cashAdjust = Utility.GetIntFromObject(tempResult);
                        }

                        CalculateCustomFeeForServiceCharge(oPaymentDetail, oReceipt, isApplyTaxCustomFee, cashAdjust, _svcFee, _cashDiscount, _merchantFee, _svcFeeType, _ApplyCustomFeeWhenPurchaseGiftCard, _dTaxIncludeInSVCFee);

                        oPaymentDetail.TaxAmountByPaymentModeTaxCustomFee = CalculateTaxAmountByPaymentModeTaxCustomFee(oReceipt.TotalAmount, oReceipt.TaxAmount, oPaymentDetail.AmountTendered, isApplyTaxCustomFee);

                        var resultUpdate = RunQueryUpdate(true, $"update SaleReceiptPaymentDetails set AutoGratuityAmount = '{oPaymentDetail.AutoGratuityAmount}' where SaleReceiptPaymentDetailID = '{oPaymentDetail.SaleReceiptPaymentDetailID}';");
                        resultUpdate = RunQueryUpdate(true, $"update SaleReceiptPaymentDetails set AutoGratuityTaxAmount = '{oPaymentDetail.AutoGratuityTaxAmount}' where SaleReceiptPaymentDetailID = '{oPaymentDetail.SaleReceiptPaymentDetailID}';");
                        resultUpdate = RunQueryUpdate(true, $"update SaleReceiptPaymentDetails set SVCFeeForAutoGratuityAmount = '{oPaymentDetail.SVCFeeForAutoGratuityAmount}' where SaleReceiptPaymentDetailID = '{oPaymentDetail.SaleReceiptPaymentDetailID}';");
                        resultUpdate = RunQueryUpdate(true, $"update SaleReceiptPaymentDetails set MerchantFeeForAutoGratuityAmount = '{oPaymentDetail.MerchantFeeForAutoGratuityAmount}' where SaleReceiptPaymentDetailID = '{oPaymentDetail.SaleReceiptPaymentDetailID}';");
                        resultUpdate = RunQueryUpdate(true, $"update SaleReceiptPaymentDetails set PartyServiceFeeAmount = '{oPaymentDetail.PartyServiceFeeAmount}' where SaleReceiptPaymentDetailID = '{oPaymentDetail.SaleReceiptPaymentDetailID}';");
                        resultUpdate = RunQueryUpdate(true, $"update SaleReceiptPaymentDetails set PartyServiceFeeTaxAmount = '{oPaymentDetail.PartyServiceFeeTaxAmount}' where SaleReceiptPaymentDetailID = '{oPaymentDetail.SaleReceiptPaymentDetailID}';");
                        resultUpdate = RunQueryUpdate(true, $"update SaleReceiptPaymentDetails set SVCFeeForPartyServiceFeeAmount = '{oPaymentDetail.SVCFeeForPartyServiceFeeAmount}' where SaleReceiptPaymentDetailID = '{oPaymentDetail.SaleReceiptPaymentDetailID}';");
                        resultUpdate = RunQueryUpdate(true, $"update SaleReceiptPaymentDetails set MerchantFeeForPartyServiceFeeAmount = '{oPaymentDetail.MerchantFeeForPartyServiceFeeAmount}' where SaleReceiptPaymentDetailID = '{oPaymentDetail.SaleReceiptPaymentDetailID}';");
                        resultUpdate = RunQueryUpdate(true, $"update SaleReceiptPaymentDetails set AreaFeeAmount = '{oPaymentDetail.AreaFeeAmount}' where SaleReceiptPaymentDetailID = '{oPaymentDetail.SaleReceiptPaymentDetailID}';");
                        resultUpdate = RunQueryUpdate(true, $"update SaleReceiptPaymentDetails set AreaFeeTaxAmount = '{oPaymentDetail.AreaFeeTaxAmount}' where SaleReceiptPaymentDetailID = '{oPaymentDetail.SaleReceiptPaymentDetailID}';");
                        resultUpdate = RunQueryUpdate(true, $"update SaleReceiptPaymentDetails set SVCFeeForAreaFeeAmount = '{oPaymentDetail.SVCFeeForAreaFeeAmount}' where SaleReceiptPaymentDetailID = '{oPaymentDetail.SaleReceiptPaymentDetailID}';");
                        resultUpdate = RunQueryUpdate(true, $"update SaleReceiptPaymentDetails set MerchantFeeForAreaFeeAmount = '{oPaymentDetail.MerchantFeeForAreaFeeAmount}' where SaleReceiptPaymentDetailID = '{oPaymentDetail.SaleReceiptPaymentDetailID}';");
                        resultUpdate = RunQueryUpdate(true, $"update SaleReceiptPaymentDetails set ServiceFee = '{oPaymentDetail.ServiceFee}' where SaleReceiptPaymentDetailID = '{oPaymentDetail.SaleReceiptPaymentDetailID}';");
                        resultUpdate = RunQueryUpdate(true, $"update SaleReceiptPaymentDetails set ServiceFeeTaxAmount = '{oPaymentDetail.ServiceFeeTaxAmount}' where SaleReceiptPaymentDetailID = '{oPaymentDetail.SaleReceiptPaymentDetailID}';");
                        resultUpdate = RunQueryUpdate(true, $"update SaleReceiptPaymentDetails set SVCFeeForServiceFee = '{oPaymentDetail.SVCFeeForServiceFee}' where SaleReceiptPaymentDetailID = '{oPaymentDetail.SaleReceiptPaymentDetailID}';");
                        resultUpdate = RunQueryUpdate(true, $"update SaleReceiptPaymentDetails set MerchantFeeForServiceFee = '{oPaymentDetail.MerchantFeeForServiceFee}' where SaleReceiptPaymentDetailID = '{oPaymentDetail.SaleReceiptPaymentDetailID}';");
                        resultUpdate = RunQueryUpdate(true, $"update SaleReceiptPaymentDetails set SVCFeeForServiceCharge = '{oPaymentDetail.SVCFeeForServiceCharge}' where SaleReceiptPaymentDetailID = '{oPaymentDetail.SaleReceiptPaymentDetailID}';");
                        resultUpdate = RunQueryUpdate(true, $"update SaleReceiptPaymentDetails set PaidServiceFee = '{oPaymentDetail.PaidServiceFee}' where SaleReceiptPaymentDetailID = '{oPaymentDetail.SaleReceiptPaymentDetailID}';");
                        resultUpdate = RunQueryUpdate(true, $"update SaleReceiptPaymentDetails set PaidPayout = '{oPaymentDetail.PaidPayout}' where SaleReceiptPaymentDetailID = '{oPaymentDetail.SaleReceiptPaymentDetailID}';");
                        resultUpdate = RunQueryUpdate(true, $"update SaleReceiptPaymentDetails set TaxAmountByPaymentModeTaxCustomFee = '{oPaymentDetail.TaxAmountByPaymentModeTaxCustomFee}' where SaleReceiptPaymentDetailID = '{oPaymentDetail.SaleReceiptPaymentDetailID}';");

                    }
                }
            }
            catch (Exception ex)
            {
                BLogicLogger.Exception("ReCalculateServiceChargesInSaleReceiptPaymentDetails in Helper", ex);
            }
        }

        public static decimal CalculateTaxServiceCharge(decimal sAmount, decimal sPercent, decimal sTotalTaxByServiceCharge, DataTable dtSaleReceiptServiceChargeTaxDetails)
        {
            decimal stax = 0;
            try
            {
                decimal temp = 0;
                if (dtSaleReceiptServiceChargeTaxDetails == null || dtSaleReceiptServiceChargeTaxDetails.Rows.Count == 0)
                {
                    if (sAmount == 0 || sTotalTaxByServiceCharge == 0 || sPercent == 0)
                        return 0;
                    temp = (sAmount / (1 + sPercent / 100));
                    stax = Utility.RoundUp(temp * sPercent / 100);
                    return stax;
                }
                else
                {
                    foreach (DataRow dataRow in dtSaleReceiptServiceChargeTaxDetails.Rows)
                    {
                        decimal sPercentByServiceCharge = Utility.GetDecimalFromRow("ServiceChargeTaxRate", dataRow);
                        if (sAmount == 0 || sTotalTaxByServiceCharge == 0 || sPercentByServiceCharge == 0)
                        {
                            stax += 0;
                        }
                        else
                        {
                            temp = sAmount * sPercentByServiceCharge / 100;
                            stax += Utility.RoundUp(temp / (1 + sPercent / 100));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                BLogicLogger.Error("#### CalculateTaxServiceCharge with error: ", ex.Message, ex);
            }
            return stax;
        }

        public static void UpdateTaxServiceChargeinPayment(decimal dAutoGratuityTaxAmount, decimal dAreaFeeTaxAmount, decimal dPartyServiceFeeTaxAmount, decimal dServiceFeeTaxAmount,
                              SaleReceiptEntity oReceipt)
        {
            try
            {
                if (oReceipt == null)
                    return;

                if (oReceipt.SalesOrderFee == 0 || oReceipt.ServiceChargeTaxRate == 0)
                    return;
                if (oReceipt.SaleReceiptPaymentList == null)
                    return;

                decimal tempsc = 0;

                #region ServiceFee

                if (dServiceFeeTaxAmount != 0 && dServiceFeeTaxAmount != oReceipt.ServiceFeeTaxAmount + oReceipt.ExtraFeeTaxAmount)
                {
                    tempsc = oReceipt.ServiceFeeTaxAmount + oReceipt.ExtraFeeTaxAmount - dServiceFeeTaxAmount;
                    if (tempsc != 0)
                    {
                        for (int i = oReceipt.SaleReceiptPaymentList.Count - 1; i >= 0; i--)
                        {
                            SaleReceiptPaymentDetailEntity payment = oReceipt.SaleReceiptPaymentList[i] as SaleReceiptPaymentDetailEntity;
                            if (payment == null) continue;
                            if (payment.ServiceFeeTaxAmount != 0)
                            {
                                payment.ServiceFeeTaxAmount += tempsc;
                                break;
                            }
                        }
                    }
                }
                else if (dServiceFeeTaxAmount == 0 && dServiceFeeTaxAmount != oReceipt.ServiceFeeTaxAmount + oReceipt.ExtraFeeTaxAmount)
                {
                    tempsc = oReceipt.ServiceFeeTaxAmount + oReceipt.ExtraFeeTaxAmount - dServiceFeeTaxAmount;
                    if (tempsc != 0)
                    {
                        for (int i = oReceipt.SaleReceiptPaymentList.Count - 1; i >= 0; i--)
                        {
                            SaleReceiptPaymentDetailEntity payment = oReceipt.SaleReceiptPaymentList[i] as SaleReceiptPaymentDetailEntity;
                            if (payment == null) continue;
                            if (payment.ServiceFee != 0)
                            {
                                payment.ServiceFeeTaxAmount += tempsc;
                                break;
                            }
                        }
                    }
                }
                #endregion

                #region PartyServiceFee
                if (dPartyServiceFeeTaxAmount != 0 && dPartyServiceFeeTaxAmount != oReceipt.PartyServiceFeeTaxAmount)
                {
                    tempsc = oReceipt.PartyServiceFeeTaxAmount - dPartyServiceFeeTaxAmount;
                    if (tempsc != 0)
                    {
                        for (int i = oReceipt.SaleReceiptPaymentList.Count - 1; i >= 0; i--)
                        {
                            SaleReceiptPaymentDetailEntity payment = oReceipt.SaleReceiptPaymentList[i] as SaleReceiptPaymentDetailEntity;
                            if (payment == null) continue;
                            if (payment.PartyServiceFeeTaxAmount != 0)
                            {
                                payment.PartyServiceFeeTaxAmount += tempsc;
                                break;
                            }
                        }
                    }
                }
                else if (dPartyServiceFeeTaxAmount == 0 && dPartyServiceFeeTaxAmount != oReceipt.PartyServiceFeeTaxAmount)
                {
                    tempsc = oReceipt.PartyServiceFeeTaxAmount - dPartyServiceFeeTaxAmount;
                    if (tempsc != 0)
                    {
                        for (int i = oReceipt.SaleReceiptPaymentList.Count - 1; i >= 0; i--)
                        {
                            SaleReceiptPaymentDetailEntity payment = oReceipt.SaleReceiptPaymentList[i] as SaleReceiptPaymentDetailEntity;
                            if (payment == null) continue;
                            if (payment.PartyServiceFeeAmount != 0)
                            {
                                payment.PartyServiceFeeTaxAmount += tempsc;
                                break;
                            }
                        }
                    }
                }
                #endregion

                #region AreaFee
                if (dAreaFeeTaxAmount != 0 && dAreaFeeTaxAmount != oReceipt.AreaFeeTaxAmount)
                {
                    tempsc = oReceipt.AreaFeeTaxAmount - dAreaFeeTaxAmount;
                    if (tempsc != 0)
                    {
                        for (int i = oReceipt.SaleReceiptPaymentList.Count - 1; i >= 0; i--)
                        {
                            SaleReceiptPaymentDetailEntity payment = oReceipt.SaleReceiptPaymentList[i] as SaleReceiptPaymentDetailEntity;
                            if (payment == null) continue;
                            if (payment.AreaFeeTaxAmount != 0)
                            {
                                payment.AreaFeeTaxAmount += tempsc;
                                break;
                            }
                        }
                    }
                }
                else if (dAreaFeeTaxAmount == 0 && dAreaFeeTaxAmount != oReceipt.AreaFeeTaxAmount)
                {
                    tempsc = oReceipt.AreaFeeTaxAmount - dAreaFeeTaxAmount;
                    if (tempsc != 0)
                    {
                        for (int i = oReceipt.SaleReceiptPaymentList.Count - 1; i >= 0; i--)
                        {
                            SaleReceiptPaymentDetailEntity payment = oReceipt.SaleReceiptPaymentList[i] as SaleReceiptPaymentDetailEntity;
                            if (payment == null) continue;
                            if (payment.AreaFeeAmount != 0)
                            {
                                payment.AreaFeeTaxAmount += tempsc;
                                break;
                            }
                        }
                    }
                }
                #endregion

                #region AutoGratuity

                if (dAutoGratuityTaxAmount != 0 && dAutoGratuityTaxAmount != oReceipt.AutoGratuityTaxAmount)
                {
                    tempsc = oReceipt.AutoGratuityTaxAmount - dAutoGratuityTaxAmount;
                    if (tempsc != 0)
                    {
                        for (int i = oReceipt.SaleReceiptPaymentList.Count - 1; i >= 0; i--)
                        {
                            SaleReceiptPaymentDetailEntity payment = oReceipt.SaleReceiptPaymentList[i] as SaleReceiptPaymentDetailEntity;
                            if (payment == null) continue;
                            if (payment.AutoGratuityTaxAmount != 0)
                            {
                                payment.AutoGratuityTaxAmount += tempsc;
                                break;
                            }
                        }
                    }
                }
                else if (dAutoGratuityTaxAmount == 0 && dAutoGratuityTaxAmount != oReceipt.AutoGratuityTaxAmount)
                {
                    tempsc = oReceipt.AutoGratuityTaxAmount - dAutoGratuityTaxAmount;
                    if (tempsc != 0)
                    {
                        for (int i = oReceipt.SaleReceiptPaymentList.Count - 1; i >= 0; i--)
                        {
                            SaleReceiptPaymentDetailEntity payment = oReceipt.SaleReceiptPaymentList[i] as SaleReceiptPaymentDetailEntity;
                            if (payment == null) continue;
                            if (payment.AutoGratuityAmount != 0)
                            {
                                payment.AutoGratuityTaxAmount += tempsc;
                                break;
                            }
                        }
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                BLogicLogger.Error("#### UpdateTaxServiceChargeinPayment with error: ", ex.Message, ex);
            }
        }

        private static decimal GetPaidPayout(DataTable dtSaleReceiptDetails, DataTable dtItem)//SaleReceiptEntity  oReceipt)
        {
            decimal dReturn = 0;
            decimal dTemp = 0;
            string sSRDname = "SaleReceiptDetails";
            if (dtSaleReceiptDetails == null || dtSaleReceiptDetails.Rows.Count == 0)
                return dReturn;

            string itemid = string.Empty;
            DataRow[] drs;
            foreach (DataRow row in dtSaleReceiptDetails.Rows)
            {
                dTemp = Utility.GetDecimalFromRow("ExtendedPrice", row);
                bool isPaidOut = false;
                if (dTemp < 0)
                {
                    itemid = Utility.GetStringFromRow("ItemID", row);
                    if (dtItem != null && dtItem.Rows.Count > 0)
                    {
                        drs = dtItem.Select("ItemID='" + itemid + "' AND (DeptDeptID='-1' OR DeptDeptID='-3')");
                        if (drs.Length > 0)
                            isPaidOut = true;
                    }

                    if (isPaidOut)
                        dReturn += dTemp;
                }
            }

            //foreach (SaleReceiptDetailEntity srde in oReceipt.SaleReceiptDetailList )
            //{
            //    if (srde.ExtendedPrice < 0)
            //        dReturn += srde.ExtendedPrice;
            //}
            return dReturn;
        }

        public static void CalculateCustomFeeForServiceCharge(SaleReceiptPaymentDetailEntity oPaymentDetail, SaleReceiptEntity oReceipt, bool isApplyTaxCustomFee, int cashAdjust, decimal _svcFee,
            decimal _cashDiscount, decimal _merchantFee, int _svcFeeType, string _ApplyCustomFeeWhenPurchaseGiftCard, int _dTaxIncludeInSVCFee)
        {
            if (oPaymentDetail.PaymentMethodID == 2 || cashAdjust == 2 || (_svcFee > 0 && _cashDiscount == 0 && oPaymentDetail.PaymentMethodID != -1 && oPaymentDetail.PaymentMethodID != -2 && oPaymentDetail.PaymentMethodID != -3))
            {
                if (oPaymentDetail.PaidServiceFee == 0 || oPaymentDetail.SVCFeeAmount == 0 || (oPaymentDetail.PaymentMethodID == 3 && _ApplyCustomFeeWhenPurchaseGiftCard != "1"))
                {
                    oPaymentDetail.SVCFeeForServiceCharge = oPaymentDetail.SVCFeeForServiceFee = oPaymentDetail.SVCFeeForPartyServiceFeeAmount = oPaymentDetail.SVCFeeForAreaFeeAmount = oPaymentDetail.SVCFeeForAutoGratuityAmount = 0;
                }
                else if (isApplyTaxCustomFee) // Apply tax custom fee
                {
                    decimal taxsc = oPaymentDetail.AutoGratuityTaxAmount + oPaymentDetail.AreaFeeTaxAmount + oPaymentDetail.PartyServiceFeeTaxAmount + oPaymentDetail.ServiceFeeTaxAmount;
                    oPaymentDetail.SVCFeeForAutoGratuityAmount = CalculateSVCFee(oPaymentDetail.AutoGratuityAmount - oPaymentDetail.AutoGratuityTaxAmount, _svcFee, _svcFeeType, _dTaxIncludeInSVCFee, isApplyTaxCustomFee, oReceipt.SVCFeeTaxRate, oPaymentDetail.SVCFeeTaxAmount);
                    oPaymentDetail.SVCFeeForAreaFeeAmount = CalculateSVCFee(oPaymentDetail.AreaFeeAmount - oPaymentDetail.AreaFeeTaxAmount, _svcFee, _svcFeeType, _dTaxIncludeInSVCFee, isApplyTaxCustomFee, oReceipt.SVCFeeTaxRate, oPaymentDetail.SVCFeeTaxAmount);
                    oPaymentDetail.SVCFeeForPartyServiceFeeAmount = CalculateSVCFee(oPaymentDetail.PartyServiceFeeAmount - oPaymentDetail.PartyServiceFeeTaxAmount, _svcFee, _svcFeeType, _dTaxIncludeInSVCFee, isApplyTaxCustomFee, oReceipt.SVCFeeTaxRate, oPaymentDetail.SVCFeeTaxAmount);
                    oPaymentDetail.SVCFeeForServiceFee = CalculateSVCFee(oPaymentDetail.ServiceFee - oPaymentDetail.ServiceFeeTaxAmount, _svcFee, _svcFeeType, _dTaxIncludeInSVCFee, isApplyTaxCustomFee, oReceipt.SVCFeeTaxRate, oPaymentDetail.SVCFeeTaxAmount);
                    oPaymentDetail.SVCFeeForServiceCharge = CalculateSVCFee(oPaymentDetail.PaidServiceFee - taxsc, _svcFee, _svcFeeType, _dTaxIncludeInSVCFee, isApplyTaxCustomFee, oReceipt.SVCFeeTaxRate, oPaymentDetail.SVCFeeTaxAmount);
                }
                else
                {
                    oPaymentDetail.SVCFeeForServiceCharge = CalculateSVCFee(oPaymentDetail.PaidServiceFee, _svcFee, _svcFeeType, _dTaxIncludeInSVCFee);
                    oPaymentDetail.SVCFeeForServiceFee = CalculateSVCFee(oPaymentDetail.ServiceFee, _svcFee, _svcFeeType, _dTaxIncludeInSVCFee);
                    oPaymentDetail.SVCFeeForPartyServiceFeeAmount = CalculateSVCFee(oPaymentDetail.PartyServiceFeeAmount, _svcFee, _svcFeeType, _dTaxIncludeInSVCFee);
                    oPaymentDetail.SVCFeeForAreaFeeAmount = CalculateSVCFee(oPaymentDetail.AreaFeeAmount, _svcFee, _svcFeeType, _dTaxIncludeInSVCFee);
                    oPaymentDetail.SVCFeeForAutoGratuityAmount = CalculateSVCFee(oPaymentDetail.AutoGratuityAmount, _svcFee, _svcFeeType, _dTaxIncludeInSVCFee);
                }

                #region Compare Custom Fee For Service Charge
                decimal tempTotalSVCFee = oPaymentDetail.SVCFeeForServiceFee + oPaymentDetail.SVCFeeForPartyServiceFeeAmount +
                    oPaymentDetail.SVCFeeForAreaFeeAmount + oPaymentDetail.SVCFeeForAutoGratuityAmount;

                if (oPaymentDetail.SVCFeeForServiceCharge != tempTotalSVCFee)
                {
                    decimal tempVal = tempTotalSVCFee - oPaymentDetail.SVCFeeForServiceCharge;

                    if (oPaymentDetail.SVCFeeForPartyServiceFeeAmount > 0 && tempVal != 0)
                    {
                        oPaymentDetail.SVCFeeForPartyServiceFeeAmount -= tempVal;
                        tempVal = 0;
                    }
                    else if (oPaymentDetail.SVCFeeForAreaFeeAmount > 0 && tempVal != 0)
                    {
                        oPaymentDetail.SVCFeeForAreaFeeAmount -= tempVal;
                        tempVal = 0;
                    }
                    else if (oPaymentDetail.SVCFeeForServiceFee > 0 && tempVal != 0)
                    {
                        oPaymentDetail.SVCFeeForServiceFee -= tempVal;
                        tempVal = 0;
                    }
                    else if (oPaymentDetail.SVCFeeForAutoGratuityAmount > 0 && tempVal != 0)
                    {
                        oPaymentDetail.SVCFeeForAutoGratuityAmount -= tempVal;
                        tempVal = 0;
                    }
                }

                #endregion

                if (oPaymentDetail.PaymentMethodID == 1 || (cashAdjust != 2 && _svcFee > 0 && _cashDiscount == 0)) // Cash
                {
                    oPaymentDetail.MerchantFeeForServiceFee = oPaymentDetail.MerchantFeeForAreaFeeAmount = oPaymentDetail.MerchantFeeForPartyServiceFeeAmount = oPaymentDetail.MerchantFeeForAutoGratuityAmount = 0;
                }
                else
                {
                    decimal tempMerchantFeeForServiceCharge = 0;
                    if (isApplyTaxCustomFee) // Apply tax custom fee
                    {
                        decimal taxsc = oPaymentDetail.AutoGratuityTaxAmount + oPaymentDetail.AreaFeeTaxAmount + oPaymentDetail.PartyServiceFeeTaxAmount + oPaymentDetail.ServiceFeeTaxAmount;
                        tempMerchantFeeForServiceCharge = CalculateMerchantFee((oPaymentDetail.PaidServiceFee - taxsc) + oPaymentDetail.SVCFeeForServiceCharge, _merchantFee);

                        oPaymentDetail.MerchantFeeForServiceFee = CalculateMerchantFee((oPaymentDetail.ServiceFee - oPaymentDetail.ServiceFeeTaxAmount) + oPaymentDetail.SVCFeeForServiceFee, _merchantFee);
                        oPaymentDetail.MerchantFeeForAreaFeeAmount = CalculateMerchantFee((oPaymentDetail.AreaFeeAmount - oPaymentDetail.AreaFeeTaxAmount) + oPaymentDetail.SVCFeeForAreaFeeAmount, _merchantFee);
                        oPaymentDetail.MerchantFeeForPartyServiceFeeAmount = CalculateMerchantFee((oPaymentDetail.PartyServiceFeeAmount - oPaymentDetail.PartyServiceFeeTaxAmount) + oPaymentDetail.SVCFeeForPartyServiceFeeAmount, _merchantFee);
                        oPaymentDetail.MerchantFeeForAutoGratuityAmount = CalculateMerchantFee((oPaymentDetail.AutoGratuityAmount - oPaymentDetail.AutoGratuityTaxAmount) + oPaymentDetail.SVCFeeForAutoGratuityAmount, _merchantFee);
                    }
                    else
                    {
                        tempMerchantFeeForServiceCharge = CalculateMerchantFee(oPaymentDetail.PaidServiceFee + oPaymentDetail.SVCFeeForServiceCharge, _merchantFee);

                        oPaymentDetail.MerchantFeeForServiceFee = CalculateMerchantFee(oPaymentDetail.ServiceFee + oPaymentDetail.SVCFeeForServiceFee, _merchantFee);
                        oPaymentDetail.MerchantFeeForAreaFeeAmount = CalculateMerchantFee(oPaymentDetail.AreaFeeAmount + oPaymentDetail.SVCFeeForAreaFeeAmount, _merchantFee);
                        oPaymentDetail.MerchantFeeForPartyServiceFeeAmount = CalculateMerchantFee(oPaymentDetail.PartyServiceFeeAmount + oPaymentDetail.SVCFeeForPartyServiceFeeAmount, _merchantFee);
                        oPaymentDetail.MerchantFeeForAutoGratuityAmount = CalculateMerchantFee(oPaymentDetail.AutoGratuityAmount + oPaymentDetail.SVCFeeForAutoGratuityAmount, _merchantFee);
                    }

                    #region Compare Merchant Fee For Service Charge

                    decimal tempTotalMerchantFee = oPaymentDetail.MerchantFeeForServiceFee + oPaymentDetail.MerchantFeeForAreaFeeAmount +
                    oPaymentDetail.MerchantFeeForPartyServiceFeeAmount + oPaymentDetail.MerchantFeeForAutoGratuityAmount;

                    if (tempMerchantFeeForServiceCharge != tempTotalMerchantFee)
                    {
                        decimal tempVal = tempTotalMerchantFee - tempMerchantFeeForServiceCharge;

                        if (oPaymentDetail.MerchantFeeForPartyServiceFeeAmount > 0 && tempVal != 0)
                        {
                            oPaymentDetail.MerchantFeeForPartyServiceFeeAmount -= tempVal;
                            tempVal = 0;
                        }
                        else if (oPaymentDetail.MerchantFeeForAreaFeeAmount > 0 && tempVal != 0)
                        {
                            oPaymentDetail.MerchantFeeForAreaFeeAmount -= tempVal;
                            tempVal = 0;
                        }
                        else if (oPaymentDetail.MerchantFeeForServiceFee > 0 && tempVal != 0)
                        {
                            oPaymentDetail.MerchantFeeForServiceFee -= tempVal;
                            tempVal = 0;
                        }
                        else if (oPaymentDetail.MerchantFeeForAutoGratuityAmount > 0 && tempVal != 0)
                        {
                            oPaymentDetail.MerchantFeeForAutoGratuityAmount -= tempVal;
                            tempVal = 0;
                        }
                    }

                    #endregion
                }

                if (!string.IsNullOrWhiteSpace(oReceipt.ReceiptInfo))
                {
                    var tempModel = Utility.ParseJsonToReceiptInfo(oReceipt.ReceiptInfo);
                    if (tempModel.RemoveSVCFeeAndCashDiscount)
                    {
                        oPaymentDetail.SVCFeeForServiceCharge = 0;
                        oPaymentDetail.SVCFeeForServiceFee = 0;
                        oPaymentDetail.SVCFeeForPartyServiceFeeAmount = 0;
                        oPaymentDetail.SVCFeeForAreaFeeAmount = 0;
                        oPaymentDetail.SVCFeeForAutoGratuityAmount = 0;
                    }
                }
            }
            else
            {
                if (oPaymentDetail.PaymentMethodID == -2 || oPaymentDetail.PaymentMethodID == -3)
                {
                    decimal tempMerchantFeeForServiceCharge = 0;
                    if (isApplyTaxCustomFee)
                    {
                        decimal taxsc = oPaymentDetail.AutoGratuityTaxAmount + oPaymentDetail.AreaFeeTaxAmount + oPaymentDetail.PartyServiceFeeTaxAmount + oPaymentDetail.ServiceFeeTaxAmount;
                        tempMerchantFeeForServiceCharge = CalculateMerchantFee(oPaymentDetail.PaidServiceFee - taxsc, _merchantFee);
                        oPaymentDetail.MerchantFeeForServiceFee = CalculateMerchantFee(oPaymentDetail.ServiceFee - oPaymentDetail.ServiceFeeTaxAmount, _merchantFee);
                        oPaymentDetail.MerchantFeeForAreaFeeAmount = CalculateMerchantFee(oPaymentDetail.AreaFeeAmount - oPaymentDetail.AreaFeeTaxAmount, _merchantFee);
                        oPaymentDetail.MerchantFeeForPartyServiceFeeAmount = CalculateMerchantFee(oPaymentDetail.PartyServiceFeeAmount - oPaymentDetail.PartyServiceFeeTaxAmount, _merchantFee);
                        oPaymentDetail.MerchantFeeForAutoGratuityAmount = CalculateMerchantFee(oPaymentDetail.AutoGratuityAmount - oPaymentDetail.AutoGratuityTaxAmount, _merchantFee);
                    }
                    else
                    {
                        tempMerchantFeeForServiceCharge = CalculateMerchantFee(oPaymentDetail.PaidServiceFee, _merchantFee);
                        oPaymentDetail.MerchantFeeForServiceFee = CalculateMerchantFee(oPaymentDetail.ServiceFee, _merchantFee);
                        oPaymentDetail.MerchantFeeForAreaFeeAmount = CalculateMerchantFee(oPaymentDetail.AreaFeeAmount, _merchantFee);
                        oPaymentDetail.MerchantFeeForPartyServiceFeeAmount = CalculateMerchantFee(oPaymentDetail.PartyServiceFeeAmount, _merchantFee);
                        oPaymentDetail.MerchantFeeForAutoGratuityAmount = CalculateMerchantFee(oPaymentDetail.AutoGratuityAmount, _merchantFee);
                    }

                    #region Compare Merchant Fee For Service Charge

                    decimal tempTotalMerchantFee = oPaymentDetail.MerchantFeeForServiceFee + oPaymentDetail.MerchantFeeForAreaFeeAmount +
                    oPaymentDetail.MerchantFeeForPartyServiceFeeAmount + oPaymentDetail.MerchantFeeForAutoGratuityAmount;

                    if (tempMerchantFeeForServiceCharge != tempTotalMerchantFee)
                    {
                        decimal tempVal = tempTotalMerchantFee - tempMerchantFeeForServiceCharge;
                        if (oPaymentDetail.MerchantFeeForPartyServiceFeeAmount > 0 && tempVal != 0)
                        {
                            oPaymentDetail.MerchantFeeForPartyServiceFeeAmount -= tempVal;
                            tempVal = 0;
                        }
                        else if (oPaymentDetail.MerchantFeeForAreaFeeAmount > 0 && tempVal != 0)
                        {
                            oPaymentDetail.MerchantFeeForAreaFeeAmount -= tempVal;
                            tempVal = 0;
                        }
                        else if (oPaymentDetail.MerchantFeeForServiceFee > 0 && tempVal != 0)
                        {
                            oPaymentDetail.MerchantFeeForServiceFee -= tempVal;
                            tempVal = 0;
                        }
                        else if (oPaymentDetail.MerchantFeeForAutoGratuityAmount > 0 && tempVal != 0)
                        {
                            oPaymentDetail.MerchantFeeForAutoGratuityAmount -= tempVal;
                            tempVal = 0;
                        }
                    }

                    #endregion
                }
                else
                {
                    oPaymentDetail.MerchantFeeForServiceFee = 0;
                    oPaymentDetail.MerchantFeeForAreaFeeAmount = 0;
                    oPaymentDetail.MerchantFeeForPartyServiceFeeAmount = 0;
                    oPaymentDetail.MerchantFeeForAutoGratuityAmount = 0;
                }
            }
        }

        public static decimal CalculateTaxAmountByPaymentModeTaxCustomFee(decimal totalamount, decimal dTaxAmount, decimal amountbypayment, bool isApplyTaxCustomFee)
        {
            decimal dTaxAmountByPaymentModeTaxCustomFee = 0;
            try
            {
                if (!isApplyTaxCustomFee || totalamount <= 0 || dTaxAmount == 0)
                    return 0;
                decimal dratio = dTaxAmount / totalamount;
                dTaxAmountByPaymentModeTaxCustomFee = Utility.RoundUp(dratio * amountbypayment);
            }
            catch (Exception ex)
            {
                BLogicLogger.Error("#### CalculateTaxAmountByPaymentModeTaxCustomFee error: " + ex.Message, ex.InnerException);
                return 0;
            }
            return dTaxAmountByPaymentModeTaxCustomFee;
        }
    }
}
