using System;

namespace BLogicPOS7SupportTool.Models
{
    public class TransactionDetailEntity
    {
        public string ID { get; set; }

        public string Number { get; set; }

        public DateTime Date { get; set; }

        public string TranType { get; set; }

        public string TenderType { get; set; }

        public string CardNumber { get; set; }

        public string CardType { get; set; }

        public string ExpDate { get; set; }

        public string Result { get; set; }

        public string TextResponse { get; set; }

        public string CaptureStatus { get; set; }

        public string AVSResult { get; set; }

        public string CVVResult { get; set; }

        public string InvoiceNo { get; set; }

        public string CustomerName { get; set; }

        public string AuthCode { get; set; }

        public string RefNo { get; set; }

        public decimal AuthorizeAmount { get; set; }

        public decimal Amount { get; set; }

        public decimal Gratuity { get; set; }

        public decimal Total { get; set; }

        public string BatchID { get; set; }

        public decimal BatchTotal { get; set; }

        public string BatchStatus { get; set; }

        public string ItemID { get; set; }

        public string Token { get; set; }

        public string CCEntry { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public string ZipCode { get; set; }

        public string Description { get; set; }

        public string ReturnCode { get; set; }

        public string AcqRefData { get; set; }

        public string ProcessData { get; set; }

        public string UserTraceData { get; set; }

        public string MerchantID { get; set; }

        public string ShiftID { get; set; }

        public string Register { get; set; }

        public string UserID { get; set; }

        public string Cashier { get; set; }

        public string TrackNo { get; set; }

        public string TranStatus { get; set; }

        public string TableName { get; set; }

        public string TerminalName { get; set; }

        public string ISOCode { get; set; }

        public string ProcessingSetName { get; set; }

        public string SecondaryResponseCode { get; set; }

        public string BankApprovalCode { get; set; }

        public string BankTransactionID { get; set; }

        public string OriginalAmount { get; set; }

        public string OriginalTip { get; set; }

        public string WorkstationID { get; set; }

        public string WorkstationName { get; set; }

        public string IsChecked { get; set; }

        public string Returned { get; set; }

        public string Field7 { get; set; }

        public string Field8 { get; set; }

        public string OrderID { get; set; }

        public decimal SVCFeeForGratuity { get; set; }

        public bool IsTipInLine { get; set; }

        public decimal SVCFee { get; set; }

        public string TicketNumber { get; set; }

        public string RegisterID { get; set; }

        public string AID { get; set; }

        public string TC { get; set; }

        public string MerchantAccount { get; set; }

        public string BatchUnique { get; set; }

        public string ServerID { get; set; }

        public string ServerName { get; set; }

        public decimal SubTotal { get; set; }

        public decimal TaxAmount { get; set; }

        public decimal DiscountAmount { get; set; }

        public decimal ServiceCharge { get; set; }

        public decimal TotalAmount { get; set; }

        public decimal PartyServiceFeeAmount { get; set; }

        public decimal AreaFeeAmount { get; set; }

        public decimal ServiceFee { get; set; }

        public string TerminalIpAddress { get; set; }

        public decimal MerchantFee { get; set; }

        public decimal MerchantFeeForTip { get; set; }

        public bool IsTryVoidOpenTab { get; set; }

        public string TicketCaptured { get; set; }

        public decimal AmountCaptured { get; set; }

        public decimal TipCaptured { get; set; }

        public string MerchantServer { get; set; }

        public string Pin { get; set; }

        public bool IsPhone { get; set; }

        public decimal AutoGratuityAmount { get; set; }

        public string CreditCardInfo { get; set; }

        public string TicketOrder { get; set; }

        public decimal SVCFeeTaxAmount { get; set; }

        public string SVCFeeType { get; set; }

        public decimal MerchantFeePercent { get; set; }

        public string AdjustTipInformation { get; set; }

        public string CardNumberMasked { get; set; }
    }
}
