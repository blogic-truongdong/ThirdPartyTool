using System;
using System.Collections.Generic;

namespace ThirdPartyTool.Models
{
    public class SaleReceiptEntity
    {
        public Guid SaleReceiptID { get; set; }

        public string SaleReceiptNumber { get; set; }

        public string StoreID { get; set; }

        public int CashierID { get; set; }

        public string TransactionStatusID { get; set; }

        public string ShiftID { get; set; }

        public string CustomerID { get; set; }

        public string ShippingID { get; set; }

        public string RefSaleReceiptID { get; set; }

        public int ReasonCode { get; set; }

        public double GlobalDiscountPercent { get; set; }

        public decimal GlobalDiscountAmount { get; set; }

        public decimal TaxAmount { get; set; }

        public decimal TotalAmount { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime StartDate { get; set; }

        public int PriceLevel { get; set; }

        public decimal TotalDiscountAmount { get; set; }

        public string Comments { get; set; }

        public DateTime SalesOrderShipDate { get; set; }

        public DateTime SalesOrderCancelDate { get; set; }

        public decimal SalesOrderFee { get; set; }

        public int SoldQuantity { get; set; }

        public int TotalQuantity { get; set; }

        public int AssociateSalePersonID { get; set; }

        public int SaleReceiptType { get; set; }

        public long AutoID { get; set; }

        public decimal DepositAmount { get; set; }

        public decimal DepositPaid { get; set; }

        public decimal PaidAmount { get; set; }

        public int ReturnCode { get; set; }

        public int Version { get; set; }

        public decimal DebitAmount { get; set; }

        public decimal ClearDebitAmount { get; set; }

        public DateTime ARDueDate { get; set; }

        public int DueIn { get; set; }

        public decimal MoneyToGetPoint { get; set; }

        public decimal AccuSaleAmount { get; set; }

        public int AccuPoint { get; set; }

        public int RemainAccuPoint { get; set; }

        public short DeliveryStatus { get; set; }

        public decimal DeliveryFee { get; set; }

        public short SplitStatus { get; set; }

        public Guid DrivingTripID { get; set; }

        public Guid TableID { get; set; }

        public decimal AreaFee { get; set; }

        public decimal ServiceFee { get; set; }

        public string CustomerInfo { get; set; }

        public short GuestsNum { get; set; }

        public decimal ServiceFeePercent { get; set; }

        public string CCTranType { get; set; }

        public string CCTranID { get; set; }

        public string DateSync { get; set; }

        public string EmployeeList { get; set; }

        public string DetailSummary { get; set; }

        public int TicketType { get; set; }

        public bool IsPaidManyTimes { get; set; }

        public string ServiceChargeTaxCodeID { get; set; }

        public decimal ServiceChargeTaxRate { get; set; }

        public decimal ServiceChargeTaxAmount { get; set; }

        public decimal RedeemPoints { get; set; }

        public decimal RewardPoints { get; set; }

        public string CheckingData { get; set; }

        public string EBTPaid { get; set; }

        public string IsRated { get; set; }

        public string IsSigned { get; set; }

        public string InvoiceNum { get; set; }

        public int RedeemStatus { get; set; }

        public decimal CustomerPoints { get; set; }

        public decimal SVCFeePercent { get; set; }

        public decimal SVCFeeAmount { get; set; }

        public decimal SVCFeePaidAmount { get; set; }

        public decimal CashDiscountPercent { get; set; }

        public decimal CashDiscountAmount { get; set; }

        public decimal CashDiscountPaidAmount { get; set; }

        public string CurrentCustomerName { get; set; }

        public int SaleTypeID { get; set; }

        public bool IsOpen { get; set; }

        public string WorkstationID { get; set; }

        public string WorkstationName { get; set; }

        public int EmployeePaidID { get; set; }

        public string TicketNotes { get; set; }

        public decimal PartyServiceFee { get; set; }

        public decimal PartyServiceFeeAmount { get; set; }

        public decimal AreaFeeAmount { get; set; }

        public decimal PartyServiceFeeTaxAmount { get; set; }

        public decimal AreaFeeTaxAmount { get; set; }

        public decimal ServiceFeeTaxAmount { get; set; }

        public bool IsOpenTab { get; set; }

        public string OpenTabCreditCardTransactionID { get; set; }

        public string OpenTabCardNumber { get; set; }

        public string OpenTabCustomer { get; set; }

        public decimal OpenTabAmount { get; set; }

        public bool IsHold { get; set; }

        public string CurrentCustomerPhone { get; set; }

        public string CurrentCustomerAddress { get; set; }

        public string RefNo { get; set; }

        public string CurrentCustomerEmail { get; set; }

        public string SaleTypeName { get; set; }

        public string PhoneAppCustomerName { get; set; }

        public DateTime PickUpDateTime { get; set; }

        public string StatusProcess { get; set; }

        public DateTime DeliveryDateTime { get; set; }

        public int DiscountType { get; set; }

        public decimal ExtraFeeAmount { get; set; }

        public decimal ExtraFeeTaxAmount { get; set; }

        public bool IsPrintKitchen { get; set; }

        public string ReceiptNumberByWorkstation { get; set; }

        public decimal AutoGratuityPercent { get; set; }

        public decimal AutoGratuityAmount { get; set; }

        public decimal AutoGratuityTaxAmount { get; set; }

        public DateTime LastModifyDateTime { get; set; }

        public bool IsModifyTicket { get; set; }

        public decimal DeliveryFeeTaxAmount { get; set; }

        public int StatusSaleReceipt { get; set; }

        public string OrderNumberForQWaitList { get; set; }

        public string SaleReceiptOrder { get; set; }

        public bool IsDeposit { get; set; }

        public string SaleReceiptOriginalInfo { get; set; }

        public int EmployeeIDDiscountTicket { get; set; }

        public string ReceiptInfo { get; set; }

        public string SaleReceiptJson { get; set; }

        public decimal SVCFeeTaxAmount { get; set; }

        public decimal CashDiscountTaxAmount { get; set; }

        public decimal SVCFeeTaxRate { get; set; }

        public decimal SVCFeeTaxPaidAmount { get; set; }

        public decimal CashDiscountTaxPaidAmount { get; set; }

        public string SVCFeeTaxCodeID { get; set; }

        public decimal CoinDiscountAmount { get; set; }

        public decimal FinalGratuity { get; set; }

        public List<SaleReceiptPaymentDetailEntity> SaleReceiptPaymentList { get; set; }
    }

}
