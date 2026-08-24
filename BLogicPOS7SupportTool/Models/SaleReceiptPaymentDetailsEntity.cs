using System;

namespace BLogicPOS7SupportTool.Models
{
    public class SaleReceiptPaymentDetailsEntity
    {
        public long SaleReceiptPaymentDetailID { get; set; }

        public Guid SaleReceiptID { get; set; }

        public int PaymentMethodID { get; set; }
        public int PaymentMethodType { get; set; }

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

        public decimal MerchantFeePercent { get; set; }

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

        public string HouseAccountTransactionID { get; set; }

        public decimal PaidHouseAccount { get; set; }

        public int CashAdjust { get; set; }

        public string PaymentCardType { get; set; }

    }


}
