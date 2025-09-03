using BLogicSystems.XPos.Utilites;
using System;
using System.Data;

namespace ThirdPartyTool.Models
{
    public class SaleReceiptPaymentDetailEntity
    {
        public string SaleReceiptPaymentDetailID { get; set; }

        public Guid SaleReceiptID { get; set; }

        public int PaymentMethodID { get; set; }

        public decimal AmountTendered { get; set; }

        public decimal AmountReturn { get; set; }

        public DateTime CreatedDate { get; set; }

        public bool IsDeposit { get; set; }

        public decimal GiftExtra { get; set; }

        public int Cashier { get; set; }

        public string ShiftID { get; set; }

        public string GiftCardCode { get; set; }

        public decimal CurrencyAmount { get; set; }

        public int CurrencyID { get; set; }

        public Guid PaymentReceiptID { get; set; }

        public int PayByAccuPoint { get; set; }

        public byte PaymentType { get; set; }

        public decimal Gratuity { get; set; }

        public string CreditCardTransactionID { get; set; }

        public string CreditCardTransactionNumber { get; set; }

        public string CreditCardTransactionCardNumber { get; set; }

        public string CreditCardTransactionType { get; set; }

        public decimal PaidServiceFee { get; set; }

        public decimal PaidPayout { get; set; }

        public string CreditCardRefNo { get; set; }

        public string CreditCardAuthCode { get; set; }

        public decimal SVCFeeAmount { get; set; }

        public decimal SVCFeeForGratuity { get; set; }

        public decimal SVCFeeForServiceCharge { get; set; }

        public decimal CashDiscountAmount { get; set; }

        public decimal MerchantFee { get; set; }

        public decimal PartyServiceFeeAmount { get; set; }

        public decimal SVCFeeForPartyServiceFeeAmount { get; set; }

        public decimal AreaFeeAmount { get; set; }

        public decimal SVCFeeForAreaFeeAmount { get; set; }

        public decimal ServiceFee { get; set; }

        public decimal SVCFeeForServiceFee { get; set; }

        public decimal MerchantFeeForTip { get; set; }

        public decimal MerchantFeeForServiceFee { get; set; }

        public decimal MerchantFeeForAreaFeeAmount { get; set; }

        public decimal MerchantFeeForPartyServiceFeeAmount { get; set; }

        public string CardHolderName { get; set; }

        public decimal CardBalance { get; set; }

        public decimal AutoGratuityAmount { get; set; }

        public decimal SVCFeeForAutoGratuityAmount { get; set; }

        public decimal MerchantFeeForAutoGratuityAmount { get; set; }

        public bool IsDecline { get; set; }

        public bool IsDepositPayment { get; set; }

        public string HouseAccountNumber { get; set; }

        public string GiftCardTransactionID { get; set; }

        public string CreditCardCardType { get; set; }

        public decimal SVCFeeTaxAmount { get; set; }

        public decimal CashDiscountTaxAmount { get; set; }

        public decimal ServiceFeeTaxAmount { get; set; }

        public decimal AreaFeeTaxAmount { get; set; }

        public decimal PartyServiceFeeTaxAmount { get; set; }

        public decimal AutoGratuityTaxAmount { get; set; }

        public decimal TaxAmountByPaymentModeTaxCustomFee { get; set; }

        public int IsCalculateTaxCustomFee { get; set; }

        public decimal CoinDiscountAmount { get; set; }

        public string CreditCardTransactionMerchantID { get; set; }

        public int EmployeePaidID { get; set; }
        public int EmployeeID { get; set; }

        /// <summary>
        /// Updates the properties of this instance from a DataRow
        /// </summary>
        /// <param name="row">The DataRow containing the data</param>
        public void UpdateFromDataRow(DataRow row)
        {
            try
            {
                if (row == null) return;

                SaleReceiptPaymentDetailID = Utility.GetStringFromObject(row["SaleReceiptPaymentDetailID"]);
                SaleReceiptID = Utility.GetGuidFromObject(row["SaleReceiptID"]);
                PaymentMethodID = Utility.GetIntFromObject(row["PaymentMethodID"]);
                AmountTendered = Utility.GetDecimalFromObject(row["AmountTendered"]);
                AmountReturn = Utility.GetDecimalFromObject(row["AmountReturn"]);
                CreatedDate = Utility.GetDateTimeFromObject(row["CreatedDate"]);
                IsDeposit = Utility.GetBoolFromObject(row["IsDeposit"]);
                GiftExtra = Utility.GetDecimalFromObject(row["GiftExtra"]);
                Cashier = Utility.GetIntFromObject(row["Cashier"]);
                ShiftID = Utility.GetStringFromObject(row["ShiftID"]);
                GiftCardCode = Utility.GetStringFromObject(row["GiftCardCode"]);
                CurrencyAmount = Utility.GetDecimalFromObject(row["CurrencyAmount"]);
                CurrencyID = Utility.GetIntFromObject(row["CurrencyID"]);
                PaymentReceiptID = Utility.GetGuidFromObject(row["PaymentReceiptID"]);
                PayByAccuPoint = Utility.GetIntFromObject(row["PayByAccuPoint"]);
                PaymentType = (byte)Utility.GetIntFromObject(row["PaymentType"]);
                Gratuity = Utility.GetDecimalFromObject(row["Gratuity"]);
                CreditCardTransactionID = Utility.GetStringFromObject(row["CreditCardTransactionID"]);
                CreditCardTransactionNumber = Utility.GetStringFromObject(row["CreditCardTransactionNumber"]);
                CreditCardTransactionCardNumber = Utility.GetStringFromObject(row["CreditCardTransactionCardNumber"]);
                CreditCardTransactionType = Utility.GetStringFromObject(row["CreditCardTransactionType"]);
                PaidServiceFee = Utility.GetDecimalFromObject(row["PaidServiceFee"]);
                PaidPayout = Utility.GetDecimalFromObject(row["PaidPayout"]);
                CreditCardRefNo = Utility.GetStringFromObject(row["CreditCardRefNo"]);
                CreditCardAuthCode = Utility.GetStringFromObject(row["CreditCardAuthCode"]);
                SVCFeeAmount = Utility.GetDecimalFromObject(row["SVCFeeAmount"]);
                SVCFeeForGratuity = Utility.GetDecimalFromObject(row["SVCFeeForGratuity"]);
                SVCFeeForServiceCharge = Utility.GetDecimalFromObject(row["SVCFeeForServiceCharge"]);
                CashDiscountAmount = Utility.GetDecimalFromObject(row["CashDiscountAmount"]);
                MerchantFee = Utility.GetDecimalFromObject(row["MerchantFee"]);
                PartyServiceFeeAmount = Utility.GetDecimalFromObject(row["PartyServiceFeeAmount"]);
                SVCFeeForPartyServiceFeeAmount = Utility.GetDecimalFromObject(row["SVCFeeForPartyServiceFeeAmount"]);
                AreaFeeAmount = Utility.GetDecimalFromObject(row["AreaFeeAmount"]);
                SVCFeeForAreaFeeAmount = Utility.GetDecimalFromObject(row["SVCFeeForAreaFeeAmount"]);
                ServiceFee = Utility.GetDecimalFromObject(row["ServiceFee"]);
                SVCFeeForServiceFee = Utility.GetDecimalFromObject(row["SVCFeeForServiceFee"]);
                MerchantFeeForTip = Utility.GetDecimalFromObject(row["MerchantFeeForTip"]);
                MerchantFeeForServiceFee = Utility.GetDecimalFromObject(row["MerchantFeeForServiceFee"]);
                MerchantFeeForAreaFeeAmount = Utility.GetDecimalFromObject(row["MerchantFeeForAreaFeeAmount"]);
                MerchantFeeForPartyServiceFeeAmount = Utility.GetDecimalFromObject(row["MerchantFeeForPartyServiceFeeAmount"]);
                CardHolderName = Utility.GetStringFromObject(row["CardHolderName"]);
                CardBalance = Utility.GetDecimalFromObject(row["CardBalance"]);
                AutoGratuityAmount = Utility.GetDecimalFromObject(row["AutoGratuityAmount"]);
                SVCFeeForAutoGratuityAmount = Utility.GetDecimalFromObject(row["SVCFeeForAutoGratuityAmount"]);
                MerchantFeeForAutoGratuityAmount = Utility.GetDecimalFromObject(row["MerchantFeeForAutoGratuityAmount"]);
                IsDecline = Utility.GetBoolFromObject(row["IsDecline"]);
                IsDepositPayment = Utility.GetBoolFromObject(row["IsDepositPayment"]);
                HouseAccountNumber = Utility.GetStringFromObject(row["HouseAccountNumber"]);
                GiftCardTransactionID = Utility.GetStringFromObject(row["GiftCardTransactionID"]);
                CreditCardCardType = Utility.GetStringFromObject(row["CreditCardCardType"]);
                SVCFeeTaxAmount = Utility.GetDecimalFromObject(row["SVCFeeTaxAmount"]);
                CashDiscountTaxAmount = Utility.GetDecimalFromObject(row["CashDiscountTaxAmount"]);
                ServiceFeeTaxAmount = Utility.GetDecimalFromObject(row["ServiceFeeTaxAmount"]);
                AreaFeeTaxAmount = Utility.GetDecimalFromObject(row["AreaFeeTaxAmount"]);
                PartyServiceFeeTaxAmount = Utility.GetDecimalFromObject(row["PartyServiceFeeTaxAmount"]);
                AutoGratuityTaxAmount = Utility.GetDecimalFromObject(row["AutoGratuityTaxAmount"]);
                TaxAmountByPaymentModeTaxCustomFee = Utility.GetDecimalFromObject(row["TaxAmountByPaymentModeTaxCustomFee"]);
                IsCalculateTaxCustomFee = Utility.GetIntFromObject(row["IsCalculateTaxCustomFee"]);
                CoinDiscountAmount = Utility.GetDecimalFromObject(row["CoinDiscountAmount"]);
                CreditCardTransactionMerchantID = Utility.GetStringFromObject(row["CreditCardTransactionMerchantID"]);
                EmployeePaidID = Utility.GetIntFromObject(row["EmployeePaidID"]);
                EmployeeID = Utility.GetIntFromObject(row["EmployeeID"]);
            }
            catch (Exception ex)
            {
                BLogicLogger.Error("Error updating SaleReceiptPaymentDetailEntity from DataRow", ex);
            }
        }

        /// <summary>
        /// Creates a new instance of SaleReceiptPaymentDetailEntity from a DataRow
        /// </summary>
        /// <param name="row">The DataRow containing the data</param>
        /// <returns>A new SaleReceiptPaymentDetailEntity instance</returns>
        public static SaleReceiptPaymentDetailEntity FromDataRow(DataRow row)
        {
            var entity = new SaleReceiptPaymentDetailEntity();
            entity.UpdateFromDataRow(row);
            return entity;
        }
    }
}
