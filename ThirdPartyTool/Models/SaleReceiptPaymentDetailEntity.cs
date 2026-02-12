using BLogicSystems.XPos.Utilites;
using System;
using System.Data;

namespace ThirdPartyTool.Models
{
    public class SaleReceiptPaymentDetailEntity
    {
        public long SaleReceiptPaymentDetailID { get; set; }

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
        public int PaymentMethodType { get; set; }

        public string HouseAccountTransactionID { get; set; }

        public decimal PaidHouseAccount { get; set; }

        /// <summary>
        /// Updates the properties of this instance from a DataRow
        /// </summary>
        /// <param name="row">The DataRow containing the data</param>
        public void UpdateFromDataRow(DataRow row)
        {
            try
            {
                if (row == null) return;

                SaleReceiptPaymentDetailID = Utility.GetLongFromRow("SaleReceiptPaymentDetailID", row);
                SaleReceiptID = Utility.GetGuidFromObject(row["SaleReceiptID"]);
                PaymentMethodID = Utility.GetIntFromRow("PaymentMethodID", row);
                AmountTendered = Utility.GetDecimalFromRow("AmountTendered", row);
                AmountReturn = Utility.GetDecimalFromRow("AmountReturn", row);
                CreatedDate = Utility.GetDateTimeFromRow("CreatedDate", row);
                IsDeposit = Utility.GetBoolFromRow("IsDeposit", row);
                GiftExtra = Utility.GetDecimalFromRow("GiftExtra", row);
                Cashier = Utility.GetIntFromRow("Cashier", row);
                ShiftID = Utility.GetStringFromRow("ShiftID", row);
                GiftCardCode = Utility.GetStringFromRow("GiftCardCode", row);
                CurrencyAmount = Utility.GetDecimalFromRow("CurrencyAmount", row);
                CurrencyID = Utility.GetIntFromRow("CurrencyID", row);
                PaymentReceiptID = Utility.GetGuidFromObject(row["PaymentReceiptID"]);
                PayByAccuPoint = Utility.GetIntFromRow("PayByAccuPoint", row);
                PaymentType = (byte)Utility.GetIntFromRow("PaymentType", row);
                Gratuity = Utility.GetDecimalFromRow("Gratuity", row);
                CreditCardTransactionID = Utility.GetStringFromRow("CreditCardTransactionID", row);
                CreditCardTransactionNumber = Utility.GetStringFromRow("CreditCardTransactionNumber", row);
                CreditCardTransactionCardNumber = Utility.GetStringFromRow("CreditCardTransactionCardNumber", row);
                CreditCardTransactionType = Utility.GetStringFromRow("CreditCardTransactionType", row);
                PaidServiceFee = Utility.GetDecimalFromRow("PaidServiceFee", row);
                PaidPayout = Utility.GetDecimalFromRow("PaidPayout", row);
                CreditCardRefNo = Utility.GetStringFromRow("CreditCardRefNo", row);
                CreditCardAuthCode = Utility.GetStringFromRow("CreditCardAuthCode", row);
                SVCFeeAmount = Utility.GetDecimalFromRow("SVCFeeAmount", row);
                SVCFeeForGratuity = Utility.GetDecimalFromRow("SVCFeeForGratuity", row);
                SVCFeeForServiceCharge = Utility.GetDecimalFromRow("SVCFeeForServiceCharge", row);
                CashDiscountAmount = Utility.GetDecimalFromRow("CashDiscountAmount", row);
                MerchantFee = Utility.GetDecimalFromRow("MerchantFee", row);
                PartyServiceFeeAmount = Utility.GetDecimalFromRow("PartyServiceFeeAmount", row);
                SVCFeeForPartyServiceFeeAmount = Utility.GetDecimalFromRow("SVCFeeForPartyServiceFeeAmount", row);
                AreaFeeAmount = Utility.GetDecimalFromRow("AreaFeeAmount", row);
                SVCFeeForAreaFeeAmount = Utility.GetDecimalFromRow("SVCFeeForAreaFeeAmount", row);
                ServiceFee = Utility.GetDecimalFromRow("ServiceFee", row);
                SVCFeeForServiceFee = Utility.GetDecimalFromRow("SVCFeeForServiceFee", row);
                MerchantFeeForTip = Utility.GetDecimalFromRow("MerchantFeeForTip", row);
                MerchantFeeForServiceFee = Utility.GetDecimalFromRow("MerchantFeeForServiceFee", row);
                MerchantFeeForAreaFeeAmount = Utility.GetDecimalFromRow("MerchantFeeForAreaFeeAmount", row);
                MerchantFeeForPartyServiceFeeAmount = Utility.GetDecimalFromRow("MerchantFeeForPartyServiceFeeAmount", row);
                CardHolderName = Utility.GetStringFromRow("CardHolderName", row);
                CardBalance = Utility.GetDecimalFromRow("CardBalance", row);
                AutoGratuityAmount = Utility.GetDecimalFromRow("AutoGratuityAmount", row);
                SVCFeeForAutoGratuityAmount = Utility.GetDecimalFromRow("SVCFeeForAutoGratuityAmount", row);
                MerchantFeeForAutoGratuityAmount = Utility.GetDecimalFromRow("MerchantFeeForAutoGratuityAmount", row);
                IsDecline = Utility.GetBoolFromRow("IsDecline", row);
                IsDepositPayment = Utility.GetBoolFromRow("IsDepositPayment", row);
                HouseAccountNumber = Utility.GetStringFromRow("HouseAccountNumber", row);
                GiftCardTransactionID = Utility.GetStringFromRow("GiftCardTransactionID", row);
                CreditCardCardType = Utility.GetStringFromRow("CreditCardCardType", row);
                SVCFeeTaxAmount = Utility.GetDecimalFromRow("SVCFeeTaxAmount", row);
                CashDiscountTaxAmount = Utility.GetDecimalFromRow("CashDiscountTaxAmount", row);
                ServiceFeeTaxAmount = Utility.GetDecimalFromRow("ServiceFeeTaxAmount", row);
                AreaFeeTaxAmount = Utility.GetDecimalFromRow("AreaFeeTaxAmount", row);
                PartyServiceFeeTaxAmount = Utility.GetDecimalFromRow("PartyServiceFeeTaxAmount", row);
                AutoGratuityTaxAmount = Utility.GetDecimalFromRow("AutoGratuityTaxAmount", row);
                TaxAmountByPaymentModeTaxCustomFee = Utility.GetDecimalFromRow("TaxAmountByPaymentModeTaxCustomFee", row);
                IsCalculateTaxCustomFee = Utility.GetIntFromRow("IsCalculateTaxCustomFee", row);
                CoinDiscountAmount = Utility.GetDecimalFromRow("CoinDiscountAmount", row);
                CreditCardTransactionMerchantID = Utility.GetStringFromRow("CreditCardTransactionMerchantID", row);
                EmployeePaidID = Utility.GetIntFromRow("EmployeePaidID", row);
                EmployeeID = Utility.GetIntFromRow("EmployeeID", row);
                PaymentMethodType = Utility.GetIntFromRow("PaymentMethodType", row);
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
