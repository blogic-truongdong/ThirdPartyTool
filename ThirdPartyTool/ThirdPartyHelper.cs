using BLogicSystems.XPos.SharedModule.Models;
using BLogicSystems.XPos.Utilites;
using BLogicSystems.XPos.Utilites.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using ThirdPartyTool.Models;

namespace ThirdPartyTool
{
    public class ThirdPartyHelper
    {
        public static readonly string SaleReceiptsTableName = "SaleReceipts";
        public static readonly string CustomerID = "CustomerID";
        public static readonly string ShiftID = "ShiftID";
        public static readonly string SaleReceiptPaymentDetails = "SaleReceiptPaymentDetails";
        public static readonly string Customers = "Customers";
        public static readonly int ItemDescriptionMaxLeng = 300;

        public SaleReceiptPaymentDetailEntity SavePaymentDetail(DataSet dsSaleReceipt, SaleReceiptEntity oReceipt)
        {
            #region Init table SaleReceiptPaymentDetailsReturn
            BLogicLogger.Debug("SaleReceiptHelperABS.SavePaymentDetail start 0 ");

            DataTable dtPaymentReturn = new DataTable("SaleReceiptPaymentDetailsReturn");
            dtPaymentReturn.Columns.Add("SaleReceiptPaymentDetailID", typeof(string));
            dtPaymentReturn.Columns.Add("Gratuity", typeof(decimal));
            dtPaymentReturn.Columns.Add("SVCFeeForGratuity", typeof(decimal));
            dtPaymentReturn.Columns.Add("MerchantFeeForTip", typeof(decimal));
            dtPaymentReturn.Columns.Add("PaymentMethodID", typeof(string));
            dtPaymentReturn.Columns.Add("PartyServiceFeeAmount", typeof(decimal));
            dtPaymentReturn.Columns.Add("PartyServiceFeeTaxAmount", typeof(decimal));
            dtPaymentReturn.Columns.Add("SVCFeeForPartyServiceFeeAmount", typeof(decimal));
            dtPaymentReturn.Columns.Add("MerchantFeeForPartyServiceFeeAmount", typeof(decimal));
            dtPaymentReturn.Columns.Add("AutoGratuityAmount", typeof(decimal));
            dtPaymentReturn.Columns.Add("AutoGratuityTaxAmount", typeof(decimal));
            dtPaymentReturn.Columns.Add("SVCFeeForAutoGratuityAmount", typeof(decimal));
            dtPaymentReturn.Columns.Add("MerchantFeeForAutoGratuityAmount", typeof(decimal));
            dtPaymentReturn.Columns.Add("AreaFeeAmount", typeof(decimal));
            dtPaymentReturn.Columns.Add("AreaFeeTaxAmount", typeof(decimal));
            dtPaymentReturn.Columns.Add("SVCFeeForAreaFeeAmount", typeof(decimal));
            dtPaymentReturn.Columns.Add("MerchantFeeForAreaFeeAmount", typeof(decimal));
            dtPaymentReturn.Columns.Add("ServiceFee", typeof(decimal));
            dtPaymentReturn.Columns.Add("ServiceFeeTaxAmount", typeof(decimal));
            dtPaymentReturn.Columns.Add("SVCFeeForServiceFee", typeof(decimal));
            dtPaymentReturn.Columns.Add("MerchantFeeForServiceFee", typeof(decimal));
            dtPaymentReturn.Columns.Add("CreatedDate", typeof(DateTime));
            dsSaleReceipt.Tables.Add(dtPaymentReturn);
            #endregion

            //HouseAccountTransactions Returm
            DataTable dtHouseAccountTransactions = HouseAccount.CreateHouseAccountTransactionsTable();
            dsSaleReceipt.Tables.Add(dtHouseAccountTransactions);

            SaleReceiptPaymentDetailEntity srpdeGiftCardPayment = null;
            decimal dTotalAmountTender = 0;
            decimal dTotalPaidServiceFee = 0;
            decimal dTotalServiceFee = 0;
            decimal dPartyServiceFeeAmount = 0;
            decimal dAreaFeeAmount = 0;
            decimal dServiceFee = 0;
            decimal dOpenTabAmount = 0;
            decimal dAutoGratuityAmount = 0;

            decimal dServiceFeeTaxAmount = 0;
            decimal dAreaFeeTaxAmount = 0;
            decimal dPartyServiceFeeTaxAmount = 0;
            decimal dAutoGratuityTaxAmount = 0;

            decimal tempPaidAmount = 0;
            BLogicServerLogger.Error($"# ProccessPayment => SavePaymentDetail => Calculate_TotalTender_TotalPaidServiceFee");
            Calculate_TotalTender_TotalPaidServiceFee(oReceipt, out dTotalAmountTender, out dTotalPaidServiceFee, out dPartyServiceFeeAmount, out dAreaFeeAmount, out dServiceFee, out dAutoGratuityAmount,
                out dServiceFeeTaxAmount, out dAreaFeeTaxAmount, out dPartyServiceFeeTaxAmount, out dAutoGratuityTaxAmount);

            //oReceipt.IsPaidManyTimes = false;
            //if (dsSaleReceipt.Tables.Contains(SaleReceiptPaymentDetails) &&
            //    dsSaleReceipt.Tables[SaleReceiptPaymentDetails].Rows.Count > 0)
            DataTable tbPaymentTable = dsSaleReceipt.Tables[SaleReceiptPaymentDetails];

            if (oReceipt.SaleReceiptPaymentList == null)
                oReceipt.SaleReceiptPaymentList = new List<SaleReceiptPaymentDetailEntity>();
            //dTotalServiceFee = (decimal)oReceipt.SalesOrderFee + oReceipt.ServiceChargeTaxAmount; //(decimal)oReceipt.ServiceFee + 
            //decimal dTotalAmountExcludeServiceFee = oReceipt.TotalAmount < dTotalServiceFee ? 0 : oReceipt.TotalAmount - dTotalServiceFee;

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

            decimal taxitem = oReceipt.TaxAmount - oReceipt.ServiceChargeTaxAmount;
            dTotalServiceFee = (decimal)oReceipt.SalesOrderFee;

            decimal tempAmount = oReceipt.TotalAmount + taxitem - oReceipt.TaxAmount;
            decimal dTotalAmountExcludeServiceFee = tempAmount - dTotalServiceFee;

            bool IsPayBySplitEvenly = false;
            decimal dBaseAmount = 0;
            if (dsSaleReceipt.Tables.Contains("dtIsPayBySplitEvently"))
            {
                IsPayBySplitEvenly = true;
                if (IsFinishSale(oReceipt))
                {
                    tempAmount = oReceipt.TotalAmount + taxitem - oReceipt.TaxAmount;
                    dTotalAmountExcludeServiceFee = tempAmount - dTotalServiceFee;
                }
                else
                {
                    if (dsSaleReceipt.Tables.Contains("SaleReceiptDetails"))
                    {
                        DataTable dtSaleReceiptDetails = dsSaleReceipt.Tables["SaleReceiptDetails"];
                        var result = dtSaleReceiptDetails.AsEnumerable().Where(dr => Utility.GetStringFromObject(dr.Field<object>("Status")) != "2");
                        if (result != null)
                            dBaseAmount = result.Sum(dr => Utility.GetDecimalFromObject(dr.Field<object>("ExtendedPrice"))) + result.Sum(dr => Utility.GetDecimalFromObject(dr.Field<object>("TaxAmount")));

                        dTotalAmountExcludeServiceFee = dBaseAmount;
                    }
                    //tempAmount = oReceipt.TotalAmount + dTotalAmountTender;
                    //decimal stempAmountSC = tempAmount < dTotalServiceFee ? 0 : tempAmount - dTotalServiceFee;
                    //dTotalAmountExcludeServiceFee = tempAmount < dTotalServiceFee ? tempAmount : tempAmount - stempAmountSC;
                }
            }

            bool isPreAuth = false;
            bool IsAutoGratuityAmount = false;
            bool IsPartyServiceFeeAmount = false;
            bool IsAreaFeeAmount = false;
            bool IsServiceFee = false;
            decimal tempTaxServiceCharge = 0;

            decimal dServiceFeeNew = Utility.GetDecimalFromObject(oReceipt.ServiceFee) + oReceipt.ExtraFeeAmount;
            decimal sServiceChargeTaxRate = Utility.GetDecimalFromObject(oReceipt.ServiceChargeTaxRate);

            int sCountRows = 0;
            //decimal m_TotalTaxServiceCharge = dServiceFeeTaxAmount + dAreaFeeTaxAmount + dPartyServiceFeeTaxAmount + dAutoGratuityTaxAmount;

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

            DataTable listConfig = new DataTable("ConfigurationsEntity");

            using (SqlCommand sqlCommand = new SqlCommand())
            using (SqlConnection conn = new SqlConnection(Helper.BLOGICPOS7_Connection))
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                sqlCommand.Connection = conn;
                sqlCommand.CommandType = CommandType.Text;

                sqlCommand.CommandText = sql;
                var reader = sqlCommand.ExecuteReader();
                listConfig.Load(reader);
                reader.Close();
            }
            foreach (DataRow item in listConfig.Rows)
            {
                ConfigurationsEntity entity = new ConfigurationsEntity
                {
                    ConfigurationName = Utility.GetStringFromRow("ConfigurationName", item),
                    ConfigurationValue = Utility.GetStringFromRow("ConfigurationValue", item)
                };
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

            //Issue House Account
            bool bIssueHouseAccount = false;
            DateTime? vLastTransDate = null;
            decimal dTotalBaland = 0;
            string sIssueTenderType = "";
            Guid vID = Guid.NewGuid();

            if (!string.IsNullOrEmpty(oReceipt.OpenTabCardNumber) && oReceipt.SaleTypeID == Utility.HouseAccountSaleType)
                bIssueHouseAccount = true;

            foreach (DataRow rPaymentRow in tbPaymentTable.Rows)
            {
                sCountRows++;
                isPreAuth = false;
                //Create salePaymentDetail and add to receipt
                SaleReceiptPaymentDetailEntity oPaymentDetail = HouseAccount.CreatePaymentDetail(rPaymentRow, oReceipt);
                oPaymentDetail.PaidServiceFee = 0;
                oPaymentDetail.AreaFeeAmount = 0;
                oPaymentDetail.PartyServiceFeeAmount = 0;
                oPaymentDetail.ServiceFee = 0;

                if (tbPaymentTable.Columns.Contains("CreditCardRefNo"))
                {
                    oPaymentDetail.CreditCardRefNo = rPaymentRow["CreditCardRefNo"]?.ToString();
                }
                tempPaidAmount = dTotalAmountTender;
                dTotalAmountTender += oPaymentDetail.AmountTendered - oPaymentDetail.AmountReturn;

                //add by tuanph.11.18.2019
                if (oPaymentDetail.CreditCardTransactionType == "PreAuth")
                {
                    dOpenTabAmount = oPaymentDetail.AmountTendered - oPaymentDetail.AmountReturn;
                    isPreAuth = true;
                }

                decimal dTempAmountTotal = oReceipt.TotalAmount;
                if (IsPayBySplitEvenly && !IsFinishSale(oReceipt)) // Pay by split evenly
                {
                    dTempAmountTotal = dBaseAmount + (decimal)oReceipt.SalesOrderFee + oReceipt.ServiceChargeTaxAmount;
                }

                if (dTempAmountTotal > dTotalAmountTender)
                    dTempAmountTotal = dTotalAmountTender;

                if (dTotalAmountExcludeServiceFee <= dTotalAmountTender && !isPreAuth)
                {
                    decimal dCurrentServiceFee = 0;

                    oPaymentDetail.PaidServiceFee = dTempAmountTotal - dTotalAmountExcludeServiceFee - dTotalPaidServiceFee;// + oReceipt.ServiceChargeTaxAmount;
                                                                                                                            //if (IsFinishSale(oReceipt) && sCountRows == tbPaymentTable.Rows.Count)
                                                                                                                            //    oPaymentDetail.PaidServiceFee = dTempAmountTotal - dTotalAmountExcludeServiceFee - dTotalPaidServiceFee - taxitem;
                                                                                                                            //else
                                                                                                                            //{
                                                                                                                            //    oPaymentDetail.PaidServiceFee = dTempAmountTotal - dTotalAmountExcludeServiceFee - dTotalPaidServiceFee;
                                                                                                                            //    if (oPaymentDetail.PaidServiceFee > oReceipt.SalesOrderFee + oReceipt.ServiceChargeTaxAmount)
                                                                                                                            //        oPaymentDetail.PaidServiceFee = oReceipt.SalesOrderFee + oReceipt.ServiceChargeTaxAmount;
                                                                                                                            //}

                    dTotalPaidServiceFee += oPaymentDetail.PaidServiceFee;

                    //add by tuanph.09.23.2019
                    dCurrentServiceFee = oPaymentDetail.PaidServiceFee;

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
                        oPaymentDetail.PartyServiceFeeTaxAmount = CalculateTaxServiceCharge(oPaymentDetail.PartyServiceFeeAmount, sServiceChargeTaxRate, oReceipt.PartyServiceFeeTaxAmount, dsSaleReceipt);
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
                        oPaymentDetail.AreaFeeTaxAmount = CalculateTaxServiceCharge(oPaymentDetail.AreaFeeAmount, sServiceChargeTaxRate, oReceipt.AreaFeeTaxAmount, dsSaleReceipt);
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
                        oPaymentDetail.ServiceFeeTaxAmount = CalculateTaxServiceCharge(oPaymentDetail.ServiceFee, sServiceChargeTaxRate, oReceipt.ServiceFeeTaxAmount + oReceipt.ExtraFeeTaxAmount, dsSaleReceipt);
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
                        oPaymentDetail.AutoGratuityTaxAmount = CalculateTaxServiceCharge(oPaymentDetail.AutoGratuityAmount, sServiceChargeTaxRate, oReceipt.AutoGratuityTaxAmount, dsSaleReceipt);
                        dAutoGratuityTaxAmount += oPaymentDetail.AutoGratuityTaxAmount;
                    }

                    #region
                    //if (oPaymentDetail.PaidServiceFee == oPaymentDetail.AmountTendered - oPaymentDetail.AmountReturn) // Compare
                    //{
                    //    decimal tempVal = oPaymentDetail.TaxAmountByPaymentModeTaxCustomFee - (oPaymentDetail.AutoGratuityTaxAmount + oPaymentDetail.AreaFeeTaxAmount + oPaymentDetail.PartyServiceFeeTaxAmount + oPaymentDetail.ServiceFeeTaxAmount);
                    //    if (tempVal != 0)
                    //    {
                    //        if (oPaymentDetail.ServiceFeeTaxAmount != 0 && tempVal != 0)
                    //        {
                    //            oPaymentDetail.ServiceFeeTaxAmount += tempVal;
                    //            tempVal = 0;
                    //        }
                    //        if (oPaymentDetail.PartyServiceFeeTaxAmount != 0 && tempVal != 0)
                    //        {
                    //            oPaymentDetail.PartyServiceFeeTaxAmount += tempVal;
                    //            tempVal = 0;
                    //        }
                    //        if (oPaymentDetail.AreaFeeTaxAmount != 0 && tempVal != 0)
                    //        {
                    //            oPaymentDetail.AreaFeeTaxAmount += tempVal;
                    //            tempVal = 0;
                    //        }
                    //        if (oPaymentDetail.AutoGratuityTaxAmount != 0 && tempVal != 0)
                    //        {
                    //            oPaymentDetail.ServiceFeeTaxAmount += tempVal;
                    //            tempVal = 0;
                    //        }
                    //    }
                    //}

                    //==========================================================================

                    #region Tax Service Charge

                    //if (oReceipt.AutoGratuityAmount > 0 && oReceipt.AutoGratuityAmount + oReceipt.AutoGratuityTaxAmount > dAutoGratuityAmount && dCurrentServiceFee > 0)
                    //{
                    //    if (!IsAutoGratuityAmount && dCurrentServiceFee > 0)
                    //    {
                    //        tempTaxServiceCharge = dCurrentServiceFee > oReceipt.AutoGratuityTaxAmount ? oReceipt.AutoGratuityTaxAmount : dCurrentServiceFee;
                    //        oPaymentDetail.AutoGratuityAmount += tempTaxServiceCharge;
                    //        dAutoGratuityAmount += tempTaxServiceCharge;
                    //        dCurrentServiceFee -= tempTaxServiceCharge;

                    //        if (oPaymentDetail.AutoGratuityAmount >= oReceipt.AutoGratuityAmount + oReceipt.AutoGratuityTaxAmount)
                    //            IsAutoGratuityAmount = true;

                    //        oPaymentDetail.AutoGratuityTaxAmount = tempTaxServiceCharge;
                    //        dAutoGratuityTaxAmount += oPaymentDetail.AutoGratuityTaxAmount;
                    //    }
                    //}

                    //if (oReceipt.AreaFeeAmount > 0 && oReceipt.AreaFeeAmount + oReceipt.AreaFeeTaxAmount > dAreaFeeAmount && dCurrentServiceFee > 0)
                    //{
                    //    if (!IsAreaFeeAmount && dCurrentServiceFee > 0)
                    //    {
                    //        tempTaxServiceCharge = dCurrentServiceFee > oReceipt.AreaFeeTaxAmount ? oReceipt.AreaFeeTaxAmount : dCurrentServiceFee;
                    //        oPaymentDetail.AreaFeeAmount += tempTaxServiceCharge;
                    //        dAreaFeeAmount += tempTaxServiceCharge;
                    //        dCurrentServiceFee -= tempTaxServiceCharge;

                    //        if (oPaymentDetail.AreaFeeAmount >= oReceipt.AreaFeeAmount + oReceipt.AreaFeeTaxAmount)
                    //            IsAreaFeeAmount = true;

                    //        oPaymentDetail.AreaFeeTaxAmount = tempTaxServiceCharge;
                    //        dAreaFeeTaxAmount += oPaymentDetail.AreaFeeTaxAmount;
                    //    }
                    //}

                    //if (oReceipt.PartyServiceFeeAmount > 0 && oReceipt.PartyServiceFeeAmount + oReceipt.PartyServiceFeeTaxAmount > dPartyServiceFeeAmount && dCurrentServiceFee > 0)
                    //{
                    //    if (!IsPartyServiceFeeAmount && dCurrentServiceFee > 0)
                    //    {
                    //        tempTaxServiceCharge = dCurrentServiceFee > oReceipt.PartyServiceFeeTaxAmount ? oReceipt.PartyServiceFeeTaxAmount : dCurrentServiceFee;
                    //        oPaymentDetail.PartyServiceFeeAmount += tempTaxServiceCharge;
                    //        dPartyServiceFeeAmount += tempTaxServiceCharge;
                    //        dCurrentServiceFee -= tempTaxServiceCharge;

                    //        if (oPaymentDetail.PartyServiceFeeAmount >= oReceipt.PartyServiceFeeAmount + oReceipt.PartyServiceFeeTaxAmount)
                    //            IsPartyServiceFeeAmount = true;

                    //        oPaymentDetail.PartyServiceFeeTaxAmount = tempTaxServiceCharge;
                    //        dPartyServiceFeeTaxAmount += oPaymentDetail.PartyServiceFeeTaxAmount;
                    //    }
                    //}

                    //if (dServiceFeeNew > 0 && dServiceFeeNew + oReceipt.ServiceFeeTaxAmount + oReceipt.ExtraFeeTaxAmount > dServiceFee && dCurrentServiceFee > 0)
                    //{
                    //    if (!IsServiceFee && dCurrentServiceFee > 0)
                    //    {
                    //        tempTaxServiceCharge = dCurrentServiceFee > oReceipt.ServiceFeeTaxAmount + oReceipt.ExtraFeeTaxAmount ? oReceipt.ServiceFeeTaxAmount + oReceipt.ExtraFeeTaxAmount : dCurrentServiceFee;
                    //        oPaymentDetail.ServiceFee += tempTaxServiceCharge;
                    //        dServiceFee += tempTaxServiceCharge;
                    //        dCurrentServiceFee -= tempTaxServiceCharge;

                    //        if (oPaymentDetail.ServiceFee >= dServiceFeeNew + oReceipt.ServiceFeeTaxAmount + oReceipt.ExtraFeeTaxAmount)
                    //            IsServiceFee = true;

                    //        oPaymentDetail.ServiceFeeTaxAmount = tempTaxServiceCharge;
                    //        dServiceFeeTaxAmount += oPaymentDetail.ServiceFeeTaxAmount;
                    //    }
                    //}

                    #endregion

                    #endregion
                }

                if (oPaymentDetail.CreatedDate == null || oPaymentDetail.CreatedDate == DateTime.MinValue)
                    oPaymentDetail.CreatedDate = DateTime.Now;

                //Trung Add
                vLastTransDate = oPaymentDetail.CreatedDate;

                oReceipt.SaleReceiptPaymentList.Add(oPaymentDetail);

                if (IsFinishSale(oReceipt) && sCountRows == tbPaymentTable.Rows.Count) // Finish sale
                {
                    UpdateTaxServiceChargeinPayment(dAutoGratuityTaxAmount, dAreaFeeTaxAmount, dPartyServiceFeeTaxAmount, dServiceFeeTaxAmount, oReceipt);
                }

                //get giftCardPayment 
                //if (oPaymentDetail.PaymentMethod.PaymentMethodid == (int)PaymentTypes.GIFTCARD
                //    && rPaymentRow["GiftCardCode"] != null && rPaymentRow["GiftCardCode"].ToString() != "")
                if (oPaymentDetail.PaymentMethodID == (int)PaymentTypes.GIFTCARD)
                {
                    oPaymentDetail.GiftCardCode = Utility.GetStringFromRow("GiftCardCode", rPaymentRow);
                    srpdeGiftCardPayment = oPaymentDetail;
                }
                if (oReceipt.TransactionStatusID == TransactionStatus.RETURN)
                    UpdateValuePaymentDetailsForRETURNSaleReceipt(oPaymentDetail);
                //dTotalAmountTender += oPaymentDetail.AmountTendered;

                if (dTotalAmountTender >= oReceipt.TotalAmount)
                    oPaymentDetail.PaidPayout = GetPaidPayout(dsSaleReceipt);

                int cashAdjust = 1;
                if (_EnableDualPrice == "1" && _DualPricePercent > 0)
                {
                    sql = $"select IsDualPrice from PaymentMethods where PaymentMethodID = '{oPaymentDetail.PaymentMethodID}'";
                    //var tempResult = DBUtility.CreateQuerySelectUnique(session, sql);
                    //cashAdjust = Utility.GetBoolFromObject(tempResult) ? 2 : 1;
                }
                else
                {
                    sql = $"select CashAdjust from PaymentMethods where PaymentMethodID = '{oPaymentDetail.PaymentMethodID}'";
                    //var tempResult = DBUtility.CreateQuerySelectUnique(session, sql);
                    //cashAdjust = Utility.GetIntFromObject(tempResult);
                }

                #region Commnet

                //if (oPaymentDetail.PaymentMethod.PaymentMethodid == 2 || cashAdjust == 2 || (_svcFee > 0 && _cashDiscount == 0 && oPaymentDetail.PaymentMethod.PaymentMethodid != -1 && oPaymentDetail.PaymentMethod.PaymentMethodid != -2 && oPaymentDetail.PaymentMethod.PaymentMethodid != -3))
                //{
                //    if (oPaymentDetail.PaidServiceFee == 0 || oPaymentDetail.SVCFeeAmount == 0 || (oPaymentDetail.PaymentMethod.PaymentMethodid == 3 && _ApplyCustomFeeWhenPurchaseGiftCard != "1"))
                //    {
                //        oPaymentDetail.SVCFeeForServiceCharge = oPaymentDetail.SVCFeeForServiceFee = oPaymentDetail.SVCFeeForPartyServiceFeeAmount = oPaymentDetail.SVCFeeForAreaFeeAmount = oPaymentDetail.SVCFeeForAutoGratuityAmount = 0;
                //    }
                //    else if (isApplyTaxCustomFee) // Apply tax custom fee
                //    {
                //        decimal taxsc = oPaymentDetail.AutoGratuityTaxAmount + oPaymentDetail.AreaFeeTaxAmount + oPaymentDetail.PartyServiceFeeTaxAmount + oPaymentDetail.ServiceFeeTaxAmount;
                //        oPaymentDetail.SVCFeeForAutoGratuityAmount = CalculateSVCFee(oPaymentDetail.AutoGratuityAmount - oPaymentDetail.AutoGratuityTaxAmount, _svcFee, _svcFeeType, _dTaxIncludeInSVCFee, isApplyTaxCustomFee, oReceipt.SVCFeeTaxRate, oPaymentDetail.SVCFeeTaxAmount);
                //        oPaymentDetail.SVCFeeForAreaFeeAmount = CalculateSVCFee(oPaymentDetail.AreaFeeAmount - oPaymentDetail.AreaFeeTaxAmount, _svcFee, _svcFeeType, _dTaxIncludeInSVCFee, isApplyTaxCustomFee, oReceipt.SVCFeeTaxRate, oPaymentDetail.SVCFeeTaxAmount);
                //        oPaymentDetail.SVCFeeForPartyServiceFeeAmount = CalculateSVCFee(oPaymentDetail.PartyServiceFeeAmount - oPaymentDetail.PartyServiceFeeTaxAmount, _svcFee, _svcFeeType, _dTaxIncludeInSVCFee, isApplyTaxCustomFee, oReceipt.SVCFeeTaxRate, oPaymentDetail.SVCFeeTaxAmount);
                //        oPaymentDetail.SVCFeeForServiceFee = CalculateSVCFee(oPaymentDetail.ServiceFee - oPaymentDetail.ServiceFeeTaxAmount, _svcFee, _svcFeeType, _dTaxIncludeInSVCFee, isApplyTaxCustomFee, oReceipt.SVCFeeTaxRate, oPaymentDetail.SVCFeeTaxAmount);
                //        oPaymentDetail.SVCFeeForServiceCharge = CalculateSVCFee(oPaymentDetail.PaidServiceFee - taxsc, _svcFee, _svcFeeType, _dTaxIncludeInSVCFee, isApplyTaxCustomFee, oReceipt.SVCFeeTaxRate, oPaymentDetail.SVCFeeTaxAmount);
                //    }
                //    else
                //    {
                //        oPaymentDetail.SVCFeeForServiceCharge = CalculateSVCFee(oPaymentDetail.PaidServiceFee, _svcFee, _svcFeeType, _dTaxIncludeInSVCFee);
                //        oPaymentDetail.SVCFeeForServiceFee = CalculateSVCFee(oPaymentDetail.ServiceFee, _svcFee, _svcFeeType, _dTaxIncludeInSVCFee);
                //        oPaymentDetail.SVCFeeForPartyServiceFeeAmount = CalculateSVCFee(oPaymentDetail.PartyServiceFeeAmount, _svcFee, _svcFeeType, _dTaxIncludeInSVCFee);
                //        oPaymentDetail.SVCFeeForAreaFeeAmount = CalculateSVCFee(oPaymentDetail.AreaFeeAmount, _svcFee, _svcFeeType, _dTaxIncludeInSVCFee);
                //        oPaymentDetail.SVCFeeForAutoGratuityAmount = CalculateSVCFee(oPaymentDetail.AutoGratuityAmount, _svcFee, _svcFeeType, _dTaxIncludeInSVCFee);
                //    }

                //    #region Compare Custom Fee For Service Charge
                //    decimal tempTotalSVCFee = oPaymentDetail.SVCFeeForServiceFee + oPaymentDetail.SVCFeeForPartyServiceFeeAmount +
                //        oPaymentDetail.SVCFeeForAreaFeeAmount + oPaymentDetail.SVCFeeForAutoGratuityAmount;

                //    if (oPaymentDetail.SVCFeeForServiceCharge != tempTotalSVCFee)
                //    {
                //        decimal tempVal = tempTotalSVCFee - oPaymentDetail.SVCFeeForServiceCharge;

                //        if (oPaymentDetail.SVCFeeForPartyServiceFeeAmount > 0 && tempVal != 0)
                //        {
                //            oPaymentDetail.SVCFeeForPartyServiceFeeAmount -= tempVal;
                //            tempVal = 0;
                //        }
                //        else if (oPaymentDetail.SVCFeeForAreaFeeAmount > 0 && tempVal != 0)
                //        {
                //            oPaymentDetail.SVCFeeForAreaFeeAmount -= tempVal;
                //            tempVal = 0;
                //        }
                //        else if (oPaymentDetail.SVCFeeForServiceFee > 0 && tempVal != 0)
                //        {
                //            oPaymentDetail.SVCFeeForServiceFee -= tempVal;
                //            tempVal = 0;
                //        }
                //        else if (oPaymentDetail.SVCFeeForAutoGratuityAmount > 0 && tempVal != 0)
                //        {
                //            oPaymentDetail.SVCFeeForAutoGratuityAmount -= tempVal;
                //            tempVal = 0;
                //        }
                //    }

                //    #endregion

                //    if (oPaymentDetail.PaymentMethod.PaymentMethodid == 1 || (cashAdjust != 2 && _svcFee > 0 && _cashDiscount == 0)) // Cash
                //    {
                //        oPaymentDetail.MerchantFeeForServiceFee = oPaymentDetail.MerchantFeeForAreaFeeAmount = oPaymentDetail.MerchantFeeForPartyServiceFeeAmount = oPaymentDetail.MerchantFeeForAutoGratuityAmount = 0;
                //    }
                //    else
                //    {
                //        decimal tempMerchantFeeForServiceCharge = 0;
                //        if (isApplyTaxCustomFee) // Apply tax custom fee
                //        {
                //            decimal taxsc = oPaymentDetail.AutoGratuityTaxAmount + oPaymentDetail.AreaFeeTaxAmount + oPaymentDetail.PartyServiceFeeTaxAmount + oPaymentDetail.ServiceFeeTaxAmount;
                //            tempMerchantFeeForServiceCharge = CalculateMerchantFee(oPaymentDetail.PaidServiceFee - taxsc, _merchantFee);

                //            oPaymentDetail.MerchantFeeForServiceFee = CalculateMerchantFee((oPaymentDetail.ServiceFee - oPaymentDetail.ServiceFeeTaxAmount) + oPaymentDetail.SVCFeeForServiceFee, _merchantFee);
                //            oPaymentDetail.MerchantFeeForAreaFeeAmount = CalculateMerchantFee((oPaymentDetail.AreaFeeAmount - oPaymentDetail.AreaFeeTaxAmount) + oPaymentDetail.SVCFeeForAreaFeeAmount, _merchantFee);
                //            oPaymentDetail.MerchantFeeForPartyServiceFeeAmount = CalculateMerchantFee((oPaymentDetail.PartyServiceFeeAmount - oPaymentDetail.PartyServiceFeeTaxAmount) + oPaymentDetail.SVCFeeForPartyServiceFeeAmount, _merchantFee);
                //            oPaymentDetail.MerchantFeeForAutoGratuityAmount = CalculateMerchantFee((oPaymentDetail.AutoGratuityAmount - oPaymentDetail.AutoGratuityTaxAmount) + oPaymentDetail.SVCFeeForAutoGratuityAmount, _merchantFee);
                //        }
                //        else
                //        {
                //            tempMerchantFeeForServiceCharge = CalculateMerchantFee(oPaymentDetail.PaidServiceFee, _merchantFee);

                //            oPaymentDetail.MerchantFeeForServiceFee = CalculateMerchantFee(oPaymentDetail.ServiceFee + oPaymentDetail.SVCFeeForServiceFee, _merchantFee);
                //            oPaymentDetail.MerchantFeeForAreaFeeAmount = CalculateMerchantFee(oPaymentDetail.AreaFeeAmount + oPaymentDetail.SVCFeeForAreaFeeAmount, _merchantFee);
                //            oPaymentDetail.MerchantFeeForPartyServiceFeeAmount = CalculateMerchantFee(oPaymentDetail.PartyServiceFeeAmount + oPaymentDetail.SVCFeeForPartyServiceFeeAmount, _merchantFee);
                //            oPaymentDetail.MerchantFeeForAutoGratuityAmount = CalculateMerchantFee(oPaymentDetail.AutoGratuityAmount + oPaymentDetail.SVCFeeForAutoGratuityAmount, _merchantFee);
                //        }

                //        #region Compare Merchant Fee For Service Charge

                //        decimal tempTotalMerchantFee = oPaymentDetail.MerchantFeeForServiceFee + oPaymentDetail.MerchantFeeForAreaFeeAmount +
                //        oPaymentDetail.MerchantFeeForPartyServiceFeeAmount + oPaymentDetail.MerchantFeeForAutoGratuityAmount;

                //        if (tempMerchantFeeForServiceCharge != tempTotalMerchantFee)
                //        {
                //            decimal tempVal = tempTotalMerchantFee - tempMerchantFeeForServiceCharge;

                //            if (oPaymentDetail.MerchantFeeForPartyServiceFeeAmount > 0 && tempVal != 0)
                //            {
                //                oPaymentDetail.MerchantFeeForPartyServiceFeeAmount -= tempVal;
                //                tempVal = 0;
                //            }
                //            else if (oPaymentDetail.MerchantFeeForAreaFeeAmount > 0 && tempVal != 0)
                //            {
                //                oPaymentDetail.MerchantFeeForAreaFeeAmount -= tempVal;
                //                tempVal = 0;
                //            }
                //            else if (oPaymentDetail.MerchantFeeForServiceFee > 0 && tempVal != 0)
                //            {
                //                oPaymentDetail.MerchantFeeForServiceFee -= tempVal;
                //                tempVal = 0;
                //            }
                //            else if (oPaymentDetail.MerchantFeeForAutoGratuityAmount > 0 && tempVal != 0)
                //            {
                //                oPaymentDetail.MerchantFeeForAutoGratuityAmount -= tempVal;
                //                tempVal = 0;
                //            }
                //        }

                //        #endregion
                //    }

                //    if (!string.IsNullOrWhiteSpace(oReceipt.ReceiptInfo))
                //    {
                //        var tempModel = Utility.ParseJsonToReceiptInfo(oReceipt.ReceiptInfo);
                //        if (tempModel.RemoveSVCFeeAndCashDiscount)
                //        {
                //            oPaymentDetail.SVCFeeForServiceCharge = 0;
                //            oPaymentDetail.SVCFeeForServiceFee = 0;
                //            oPaymentDetail.SVCFeeForPartyServiceFeeAmount = 0;
                //            oPaymentDetail.SVCFeeForAreaFeeAmount = 0;
                //            oPaymentDetail.SVCFeeForAutoGratuityAmount = 0;
                //        }
                //    }
                //}
                //else
                //{
                //    if (oPaymentDetail.PaymentMethod.PaymentMethodid == -2 || oPaymentDetail.PaymentMethod.PaymentMethodid == -3)
                //    {
                //        decimal tempMerchantFeeForServiceCharge = 0;
                //        if (isApplyTaxCustomFee)
                //        {
                //            decimal taxsc = oPaymentDetail.AutoGratuityTaxAmount + oPaymentDetail.AreaFeeTaxAmount + oPaymentDetail.PartyServiceFeeTaxAmount + oPaymentDetail.ServiceFeeTaxAmount;
                //            tempMerchantFeeForServiceCharge = CalculateMerchantFee(oPaymentDetail.PaidServiceFee - taxsc, _merchantFee);
                //            oPaymentDetail.MerchantFeeForServiceFee = CalculateMerchantFee(oPaymentDetail.ServiceFee - oPaymentDetail.ServiceFeeTaxAmount, _merchantFee);
                //            oPaymentDetail.MerchantFeeForAreaFeeAmount = CalculateMerchantFee(oPaymentDetail.AreaFeeAmount - oPaymentDetail.AreaFeeTaxAmount, _merchantFee);
                //            oPaymentDetail.MerchantFeeForPartyServiceFeeAmount = CalculateMerchantFee(oPaymentDetail.PartyServiceFeeAmount - oPaymentDetail.PartyServiceFeeTaxAmount, _merchantFee);
                //            oPaymentDetail.MerchantFeeForAutoGratuityAmount = CalculateMerchantFee(oPaymentDetail.AutoGratuityAmount - oPaymentDetail.AutoGratuityTaxAmount, _merchantFee);
                //        }
                //        else
                //        {
                //            tempMerchantFeeForServiceCharge = CalculateMerchantFee(oPaymentDetail.PaidServiceFee, _merchantFee);
                //            oPaymentDetail.MerchantFeeForServiceFee = CalculateMerchantFee(oPaymentDetail.ServiceFee, _merchantFee);
                //            oPaymentDetail.MerchantFeeForAreaFeeAmount = CalculateMerchantFee(oPaymentDetail.AreaFeeAmount, _merchantFee);
                //            oPaymentDetail.MerchantFeeForPartyServiceFeeAmount = CalculateMerchantFee(oPaymentDetail.PartyServiceFeeAmount, _merchantFee);
                //            oPaymentDetail.MerchantFeeForAutoGratuityAmount = CalculateMerchantFee(oPaymentDetail.AutoGratuityAmount, _merchantFee);
                //        }

                //        #region Compare Merchant Fee For Service Charge

                //        decimal tempTotalMerchantFee = oPaymentDetail.MerchantFeeForServiceFee + oPaymentDetail.MerchantFeeForAreaFeeAmount +
                //        oPaymentDetail.MerchantFeeForPartyServiceFeeAmount + oPaymentDetail.MerchantFeeForAutoGratuityAmount;

                //        if (tempMerchantFeeForServiceCharge != tempTotalMerchantFee)
                //        {
                //            decimal tempVal = tempTotalMerchantFee - tempMerchantFeeForServiceCharge;
                //            if (oPaymentDetail.MerchantFeeForPartyServiceFeeAmount > 0 && tempVal != 0)
                //            {
                //                oPaymentDetail.MerchantFeeForPartyServiceFeeAmount -= tempVal;
                //                tempVal = 0;
                //            }
                //            else if (oPaymentDetail.MerchantFeeForAreaFeeAmount > 0 && tempVal != 0)
                //            {
                //                oPaymentDetail.MerchantFeeForAreaFeeAmount -= tempVal;
                //                tempVal = 0;
                //            }
                //            else if (oPaymentDetail.MerchantFeeForServiceFee > 0 && tempVal != 0)
                //            {
                //                oPaymentDetail.MerchantFeeForServiceFee -= tempVal;
                //                tempVal = 0;
                //            }
                //            else if (oPaymentDetail.MerchantFeeForAutoGratuityAmount > 0 && tempVal != 0)
                //            {
                //                oPaymentDetail.MerchantFeeForAutoGratuityAmount -= tempVal;
                //                tempVal = 0;
                //            }
                //        }

                //        #endregion
                //    }
                //    else
                //    {
                //        oPaymentDetail.MerchantFeeForServiceFee = 0;
                //        oPaymentDetail.MerchantFeeForAreaFeeAmount = 0;
                //        oPaymentDetail.MerchantFeeForPartyServiceFeeAmount = 0;
                //        oPaymentDetail.MerchantFeeForAutoGratuityAmount = 0;
                //    }
                //}

                #endregion

                //Calculate SVC Fee for Gratuity, Service Charge
                CalculateCustomFeeForServiceCharge(oPaymentDetail, oReceipt, isApplyTaxCustomFee, cashAdjust, _svcFee, _cashDiscount, _merchantFee, _svcFeeType, _ApplyCustomFeeWhenPurchaseGiftCard, _dTaxIncludeInSVCFee);

                // tuanph.11.18.2019 delete PreAuth when captured
                if (oPaymentDetail.CreditCardTransactionType == "PreAuthCaptured")
                {
                    //RemoveCreditCardSaleReceiptPaymentDetails(session, oPaymentDetail.CreditCardAuthCode);
                    oPaymentDetail.CreatedDate = DateTime.Now;
                    BLogicServerLogger.Error($"# ProccessPayment => SavePaymentDetail => DeletePreAuthTransaction");
                    //DeletePreAuthTransaction(session, oPaymentDetail.CreditCardAuthCode, oReceipt, oPaymentDetail.CreditCardRefNo);
                }
                BLogicServerLogger.Error($"# ProccessPayment => SavePaymentDetail => session.Save(oPaymentDetail)");
                //Trung Add 2024.06.03 Update Balance (House Account)

            }

            //add by tuanph.11.18.2019. Not included OpenTabAmount to Paid Amount
            oReceipt.PaidAmount = dTotalAmountTender - dOpenTabAmount;
            if (dTotalAmountTender < oReceipt.TotalAmount
                && !IsFinishSale(oReceipt))
                oReceipt.IsPaidManyTimes = true;


            //UpdateDebitAmountAndClearDebitAmount(session, Receipt, SaleReceiptData);
            BLogicLogger.Debug("SaleReceiptHelperABS.SavePaymentDetail End ");

            return srpdeGiftCardPayment;
        }

        public void Calculate_TotalTender_TotalPaidServiceFee(SaleReceiptEntity oReceipt, out decimal dTotalAmountTender, out decimal dTotalPaidServiceFee, out decimal dPartyServiceFeeAmount, out decimal dAreaFeeAmount, out decimal dServiceFee, out decimal dAutoGratuityAmount,
           out decimal dServiceFeeTaxAmount, out decimal dAreaFeeTaxAmount, out decimal dPartyServiceFeeTaxAmount, out decimal dAutoGratuityTaxAmount)
        {
            dTotalAmountTender = 0;
            dTotalPaidServiceFee = 0;
            //add by tuanph.09.23.2019
            dPartyServiceFeeAmount = 0;
            dAreaFeeAmount = 0;
            dServiceFee = 0;
            dAutoGratuityAmount = 0;

            dServiceFeeTaxAmount = 0;
            dAreaFeeTaxAmount = 0;
            dPartyServiceFeeTaxAmount = 0;
            dAutoGratuityTaxAmount = 0;

            IList paymentLst = null;
            if (oReceipt == null)
                return;
            paymentLst = oReceipt.SaleReceiptPaymentList;
            if (paymentLst != null)
                foreach (SaleReceiptPaymentDetailEntity srpde in paymentLst)
                {
                    if (srpde.CreditCardTransactionType != "PreAuth")
                    {
                        dTotalPaidServiceFee += srpde.PaidServiceFee;
                        dTotalAmountTender += srpde.AmountTendered - srpde.AmountReturn;
                        //add by tuanph.09.23.2019
                        dPartyServiceFeeAmount += srpde.PartyServiceFeeAmount;
                        dAreaFeeAmount += srpde.AreaFeeAmount;
                        dServiceFee += srpde.ServiceFee;
                        dAutoGratuityAmount += srpde.AutoGratuityAmount;

                        dServiceFeeTaxAmount += srpde.ServiceFeeTaxAmount;
                        dAreaFeeTaxAmount += srpde.AreaFeeTaxAmount;
                        dPartyServiceFeeTaxAmount += srpde.PartyServiceFeeTaxAmount;
                        dAutoGratuityTaxAmount += srpde.AutoGratuityTaxAmount;
                    }
                }
        }

        public bool HasNewPaymentDetail(DataSet dsSaleReceipt)
        {
            if (dsSaleReceipt.Tables.Contains(SaleReceiptPaymentDetails) &&
               dsSaleReceipt.Tables[SaleReceiptPaymentDetails].Rows.Count > 0)
            {
                return true;
            }
            return false;
        }

        private bool IsFinishSale(SaleReceiptEntity sre)
        {
            if (sre == null)
                return false;
            if (sre.TransactionStatusID == null)
                return false;
            if (sre.TransactionStatusID == TransactionStatus.FINISHED)
                return true;

            return false;
        }

        public decimal CalculateTaxServiceCharge(decimal sAmount, decimal sPercent, decimal sTotalTaxByServiceCharge, DataSet dsSaleReceipt)
        {
            decimal stax = 0;
            try
            {
                DataTable dtSaleReceiptServiceChargeTaxDetails = null;
                if (dsSaleReceipt.Tables.Contains("SaleReceiptServiceChargeTaxDetails"))
                    dtSaleReceiptServiceChargeTaxDetails = dsSaleReceipt.Tables["SaleReceiptServiceChargeTaxDetails"];

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

        public void UpdateTaxServiceChargeinPayment(decimal dAutoGratuityTaxAmount, decimal dAreaFeeTaxAmount, decimal dPartyServiceFeeTaxAmount, decimal dServiceFeeTaxAmount,
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

        protected virtual void UpdateValuePaymentDetailsForRETURNSaleReceipt(SaleReceiptPaymentDetailEntity srpde)
        {
            srpde.AmountReturn *= -1;
            srpde.AmountTendered *= -1;
        }

        private decimal GetPaidPayout(DataSet dsSaleReceipt)//SaleReceiptEntity  oReceipt)
        {
            decimal dReturn = 0;
            decimal dTemp = 0;
            string sSRDname = "SaleReceiptDetails";
            if (dsSaleReceipt == null)
                return dReturn;
            if (!dsSaleReceipt.Tables.Contains(sSRDname))
                return dReturn;

            DataTable dtDetail = dsSaleReceipt.Tables[sSRDname];
            if (dtDetail == null || dtDetail.Rows.Count == 0)
                return dReturn;

            DataTable dtItem = null;
            if (dsSaleReceipt.Tables.Contains("Items"))
                dtItem = dsSaleReceipt.Tables["Items"];

            string itemid = string.Empty;
            DataRow[] drs;
            foreach (DataRow row in dtDetail.Rows)
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

        public void CalculateCustomFeeForServiceCharge(SaleReceiptPaymentDetailEntity oPaymentDetail, SaleReceiptEntity oReceipt, bool isApplyTaxCustomFee, int cashAdjust, decimal _svcFee,
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

        public decimal CalculateSVCFee(decimal amount, decimal svcFee, int svcFeeType, int sTaxIncludeInSVCFee,
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

        public decimal CalculateMerchantFee(decimal amount, decimal merchantFeeValue)
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

    }
}

