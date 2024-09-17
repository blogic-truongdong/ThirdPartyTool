using Dapper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ThirdPartyTool.Models;
using static System.Net.Mime.MediaTypeNames;

namespace ThirdPartyTool
{
    public partial class VoidSaleReceipt : Form
    {
        public const int TimeOutQuery = 180;
        private decimal MerchantFeeValue = 3.84620000m;

        private string BLOGICPOSConnection = "data source=.;Initial Catalog=BLogicPOS7;integrated security = SSPI";
        private string MerchantConnection = "data source=.;Initial Catalog=Merchant;integrated security = SSPI";

        public VoidSaleReceipt()
        {
            InitializeComponent();

            btnConnect.Click += BtnConnect_Click;

            btnGetScript.Click += BtnGetScript_Click;
        }

        private async void BtnGetScript_Click(object sender, EventArgs e)
        {
            txtScript.Text = "";

            if (string.IsNullOrWhiteSpace(txtTicketNumber.Text))
                return;

            string creditCardTransactionIDs = string.Empty;

            string text = string.Empty;

            using (var conn = new SqlConnection(txtPOSConnection.Text.Trim()))
            {
                var sql = $"select *" +
                    $" from SaleReceipts s" +
                    $" where s.SaleReceiptNumber = '{txtTicketNumber.Text.Trim()}'";

                var saleReceipt = await conn.QuerySingleAsync<SaleReceiptEntity>(sql, "", null, 180, System.Data.CommandType.Text);

                if (saleReceipt?.SaleReceiptID != null && saleReceipt.TransactionStatusID == "FIN")
                {
                    sql = $"select MAX(SaleReceiptPaymentDetailID) + 1 from SaleReceiptPaymentDetails";
                    var maxSaleReceiptPaymentDetailID = await conn.QuerySingleAsync<int>(sql, "", null, 180, System.Data.CommandType.Text);

                    Guid newSaleID = Guid.NewGuid();

                    sql = $"select * from SaleReceiptPaymentDetails where SaleReceiptID = '{saleReceipt.SaleReceiptID}'";

                    var payments = await conn.QueryAsync<SaleReceiptPaymentDetailEntity>(sql, "", null, 180, System.Data.CommandType.Text);

                    creditCardTransactionIDs = string.Join(",", payments.Select(i => { return "'" + i.CreditCardTransactionID + "'"; }));

                    text = $"use BLogicPOS7{Environment.NewLine}";

                    text += $"IF EXISTS (SELECT * FROM SaleReceipts where SaleReceiptID = '{saleReceipt.SaleReceiptID}' and TransactionStatusID = 'FIN') {Environment.NewLine}";
                    text += $"BEGIN {Environment.NewLine}";
                    text += $"{Environment.NewLine}";
                    text += $" update SaleReceipts set TransactionStatusID = 'VOID', Version = Version + 1 where SaleReceiptID  ='{saleReceipt.SaleReceiptID}';";
                    text += $"{Environment.NewLine}{Environment.NewLine}";

                    text += $" SET IDENTITY_INSERT SaleReceipts ON; {Environment.NewLine}{Environment.NewLine}";
                    text += $" insert into SaleReceipts (" +
                        $" SaleReceiptID," +
                        $" SaleReceiptNumber," +
                        $" StoreID," +
                        $" CashierID," +
                        $" TransactionStatusID," +
                        $" ShiftID," +
                        $" CustomerID," +
                        $" ShippingID," +
                        $" RefSaleReceiptID," +
                        $" ReasonCode," +
                        $" GlobalDiscountPercent," +
                        $" GlobalDiscountAmount," +
                        $" TaxAmount," +
                        $" TotalAmount," +
                        $" DateCreated," +
                        $" StartDate," +
                        $" PriceLevel," +
                        $" TotalDiscountAmount," +
                        $" Comments," +
                        $" SalesOrderShipDate," +
                        $" SalesOrderCancelDate," +
                        $" SalesOrderFee," +
                        $" SoldQuantity," +
                        $" TotalQuantity," +
                        $" AssociateSalePersonID," +
                        $" SaleReceiptType," +
                        $" AutoID," +
                        $" DepositAmount," +
                        $" DepositPaid," +
                        $" PaidAmount," +
                        $" ReturnCode," +
                        $" Version," +
                        $" DebitAmount," +
                        $" ClearDebitAmount," +
                        $" ARDueDate," +
                        $" DueIn," +
                        $" MoneyToGetPoint," +
                        $" AccuSaleAmount," +
                        $" AccuPoint," +
                        $" RemainAccuPoint," +
                        $" DeliveryStatus," +
                        $" DeliveryFee," +
                        $" SplitStatus," +
                        $" DrivingTripID," +
                        $" TableID," +
                        $" AreaFee," +
                        $" ServiceFee," +
                        $" CustomerInfo," +
                        $" GuestsNum," +
                        $" ServiceFeePercent," +
                        $" CCTranType," +
                        $" CCTranID," +
                        $" DateSync," +
                        $" EmployeeList," +
                        $" DetailSummary," +
                        $" TicketType," +
                        $" IsPaidManyTimes," +
                        $" ServiceChargeTaxCodeID," +
                        $" ServiceChargeTaxRate," +
                        $" ServiceChargeTaxAmount," +
                        $" RedeemPoints," +
                        $" RewardPoints," +
                        $" CheckingData," +
                        $" EBTPaid," +
                        $" IsRated," +
                        $" IsSigned," +
                        $" InvoiceNum," +
                        $" RedeemStatus," +
                        $" CustomerPoints," +
                        $" SVCFeePercent," +
                        $" SVCFeeAmount," +
                        $" SVCFeePaidAmount," +
                        $" CashDiscountPercent," +
                        $" CashDiscountAmount," +
                        $" CashDiscountPaidAmount," +
                        $" CurrentCustomerName," +
                        $" SaleTypeID," +
                        $" IsOpen," +
                        $" WorkstationID," +
                        $" WorkstationName," +
                        $" EmployeePaidID," +
                        $" TicketNotes," +
                        $" PartyServiceFee," +
                        $" PartyServiceFeeAmount," +
                        $" AreaFeeAmount," +
                        $" PartyServiceFeeTaxAmount," +
                        $" AreaFeeTaxAmount," +
                        $" ServiceFeeTaxAmount," +
                        $" IsOpenTab," +
                        $" OpenTabCreditCardTransactionID," +
                        $" OpenTabCardNumber," +
                        $" OpenTabCustomer," +
                        $" OpenTabAmount," +
                        $" IsHold," +
                        $" CurrentCustomerPhone," +
                        $" CurrentCustomerAddress," +
                        $" RefNo," +
                        $" CurrentCustomerEmail," +
                        $" SaleTypeName," +
                        $" PhoneAppCustomerName," +
                        $" PickUpDateTime," +
                        $" StatusProcess," +
                        $" DeliveryDateTime," +
                        $" DiscountType," +
                        $" ExtraFeeAmount," +
                        $" ExtraFeeTaxAmount," +
                        $" IsPrintKitchen," +
                        $" ReceiptNumberByWorkstation," +
                        $" AutoGratuityPercent," +
                        $" AutoGratuityAmount," +
                        $" AutoGratuityTaxAmount," +
                        $" LastModifyDateTime," +
                        $" IsModifyTicket," +
                        $" DeliveryFeeTaxAmount," +
                        $" StatusSaleReceipt," +
                        $" OrderNumberForQWaitList," +
                        $" SaleReceiptOrder," +
                        $" IsDeposit," +
                        $" SaleReceiptOriginalInfo," +
                        $" EmployeeIDDiscountTicket," +
                        $" ReceiptInfo," +
                        $" SaleReceiptJson," +
                        $" SVCFeeTaxAmount," +
                        $" CashDiscountTaxAmount," +
                        $" SVCFeeTaxRate," +
                        $" SVCFeeTaxPaidAmount," +
                        $" CashDiscountTaxPaidAmount," +
                        $" SVCFeeTaxCodeID," +
                        $" CoinDiscountAmount )" +
                        $"{Environment.NewLine} VALUES (" +
                        $" '{newSaleID}'," +
                        $" 'V_{saleReceipt.SaleReceiptNumber}'," +
                        $" '{saleReceipt.StoreID}'," +
                        $" '{saleReceipt.CashierID}'," +
                        $" 'REVOI'," +
                        $" '{saleReceipt.ShiftID}'," +
                        (string.IsNullOrWhiteSpace(saleReceipt.CustomerID) ? "NULL," : $"'{saleReceipt.CustomerID}',") +
                        $" null," +
                        $" '{saleReceipt.SaleReceiptID}'," +
                        $" null," +
                        $" '{saleReceipt.GlobalDiscountPercent}'," +
                        $" '{saleReceipt.GlobalDiscountAmount}'," +
                        $" '{saleReceipt.TaxAmount}'," +
                        $" '{saleReceipt.TotalAmount}'," +
                        $" '{saleReceipt.DateCreated}'," +
                        $" '{saleReceipt.StartDate}'," +
                        $" '{saleReceipt.PriceLevel}'," +
                        $" '{saleReceipt.TotalDiscountAmount}'," +
                        $" '-RETURN.OF.VOID-'," +
                        $" null," +
                        $" null," +
                        $" '{saleReceipt.SalesOrderFee}'," +
                        $" '{saleReceipt.SoldQuantity}'," +
                        $" '{saleReceipt.TotalQuantity}'," +
                        (string.IsNullOrWhiteSpace(saleReceipt.AssociateSalePersonID) ? "NULL," : $"'{saleReceipt.AssociateSalePersonID}',") +
                        $" '{saleReceipt.SaleReceiptType}'," +
                        $" '{saleReceipt.AutoID}'," +
                        $" '{saleReceipt.DepositAmount}'," +
                        $" '{saleReceipt.DepositPaid}'," +
                        $" '{saleReceipt.PaidAmount}'," +
                        $" null," +
                        $" '{saleReceipt.Version}'," +
                        $" '{saleReceipt.DebitAmount}'," +
                        $" '{saleReceipt.ClearDebitAmount}'," +
                        $" null," +
                        $" null," +
                        $" null," +
                        $" null," +
                        $" null," +
                        $" null," +
                        $" null," +
                        $" null," +
                        $" null," +
                        $" null," +
                        (saleReceipt.TableID == null || saleReceipt.TableID == Guid.Empty ? "NULL," : $"'{saleReceipt.TableID}',") +
                        $" '{saleReceipt.AreaFee}'," +
                        $" '{saleReceipt.ServiceFee}'," +
                        $" '{saleReceipt.CustomerInfo}'," +
                        $" '{saleReceipt.GuestsNum}'," +
                        $" '{saleReceipt.ServiceFeePercent}'," +
                        $" null," +
                        $" null," +
                        $" null," +
                        $" null," +
                        $" null," +
                        $" '{saleReceipt.TicketType}'," +
                        (saleReceipt.IsPaidManyTimes ? "1," : "0,") +
                        $" '{saleReceipt.ServiceChargeTaxCodeID}'," +
                        $" '{saleReceipt.ServiceChargeTaxRate}'," +
                        $" '{saleReceipt.ServiceChargeTaxAmount}'," +
                        $" '{saleReceipt.RedeemPoints}'," +
                        $" '{saleReceipt.RewardPoints}'," +
                        $" '{saleReceipt.CheckingData}'," +
                        $" '{saleReceipt.EBTPaid}'," +
                        $" '{saleReceipt.IsRated}'," +
                        $" '{saleReceipt.IsSigned}'," +
                        $" '{saleReceipt.InvoiceNum}'," +
                        $" '{saleReceipt.RedeemStatus}'," +
                        $" '{saleReceipt.CustomerPoints}'," +
                        $" '{saleReceipt.SVCFeePercent}'," +
                        $" '{saleReceipt.SVCFeeAmount}'," +
                        $" '{saleReceipt.SVCFeePaidAmount}'," +
                        $" '{saleReceipt.CashDiscountPercent}'," +
                        $" '{saleReceipt.CashDiscountAmount}'," +
                        $" '{saleReceipt.CashDiscountPaidAmount}'," +
                        $" '{saleReceipt.CurrentCustomerName}'," +
                        $" '{saleReceipt.SaleTypeID}'," +
                        (saleReceipt.IsOpen ? "1," : "0,") +
                        $" '{saleReceipt.WorkstationID}'," +
                        $" '{saleReceipt.WorkstationName}'," +
                        $" '{saleReceipt.EmployeePaidID}'," +
                        $" '{saleReceipt.TicketNotes}'," +
                        $" '{saleReceipt.PartyServiceFee}'," +
                        $" '{saleReceipt.PartyServiceFeeAmount}'," +
                        $" '{saleReceipt.AreaFeeAmount}'," +
                        $" '{saleReceipt.PartyServiceFeeTaxAmount}'," +
                        $" '{saleReceipt.AreaFeeTaxAmount}'," +
                        $" '{saleReceipt.ServiceFeeTaxAmount}'," +
                        (saleReceipt.IsOpenTab ? "1," : "0,") +
                        $" '{saleReceipt.OpenTabCreditCardTransactionID}'," +
                        $" '{saleReceipt.OpenTabCardNumber}'," +
                        $" '{saleReceipt.OpenTabCustomer}'," +
                        $" '{saleReceipt.OpenTabAmount}'," +
                        (saleReceipt.IsHold ? "1," : "0,") +
                        $" '{saleReceipt.CurrentCustomerPhone}'," +
                        $" '{saleReceipt.CurrentCustomerAddress}'," +
                        $" '{saleReceipt.RefNo}'," +
                        $" '{saleReceipt.CurrentCustomerEmail}'," +
                        $" '{saleReceipt.SaleTypeName}'," +
                        $" '{saleReceipt.PhoneAppCustomerName}'," +
                        $" '{saleReceipt.PickUpDateTime}'," +
                        $" '{saleReceipt.StatusProcess}'," +
                        $" '{saleReceipt.DeliveryDateTime}'," +
                        $" '{saleReceipt.DiscountType}'," +
                        $" '{saleReceipt.ExtraFeeAmount}'," +
                        $" '{saleReceipt.ExtraFeeTaxAmount}'," +
                        (saleReceipt.IsPrintKitchen ? "1," : "0,") +
                        $" '{saleReceipt.ReceiptNumberByWorkstation}'," +
                        $" '{saleReceipt.AutoGratuityPercent}'," +
                        $" '{saleReceipt.AutoGratuityAmount}'," +
                        $" '{saleReceipt.AutoGratuityTaxAmount}'," +
                        $" '{saleReceipt.LastModifyDateTime}'," +
                        (saleReceipt.IsModifyTicket ? "1," : "0,") +
                        $" '{saleReceipt.DeliveryFeeTaxAmount}'," +
                        $" '{saleReceipt.StatusSaleReceipt}'," +
                        $" '{saleReceipt.OrderNumberForQWaitList}'," +
                        $" '{saleReceipt.SaleReceiptOrder}'," +
                        (saleReceipt.IsDeposit ? "1," : "0,") +
                        $" ''," +
                        $" '{saleReceipt.EmployeeIDDiscountTicket}'," +
                        $" '{saleReceipt.ReceiptInfo}'," +
                        $" '{saleReceipt.SaleReceiptJson}'," +
                        $" '{saleReceipt.SVCFeeTaxAmount}'," +
                        $" '{saleReceipt.CashDiscountTaxAmount}'," +
                        $" '{saleReceipt.SVCFeeTaxRate}'," +
                        $" '{saleReceipt.SVCFeeTaxPaidAmount}'," +
                        $" '{saleReceipt.CashDiscountTaxPaidAmount}'," +
                        $" '{saleReceipt.SVCFeeTaxCodeID}'," +
                        $" '{saleReceipt.CoinDiscountAmount}'); {Environment.NewLine}{Environment.NewLine}";

                    text += $"  SET IDENTITY_INSERT SaleReceipts OFF; {Environment.NewLine}{Environment.NewLine}";

                    text += $" SET IDENTITY_INSERT SaleReceiptPaymentDetails ON; ";

                    foreach (SaleReceiptPaymentDetailEntity item in payments)
                    {
                        text += $"{Environment.NewLine}{Environment.NewLine}" +
                        $" insert into SaleReceiptPaymentDetails (" +
                        $" SaleReceiptPaymentDetailID," +
                        $" SaleReceiptID," +
                        $" PaymentMethodID," +
                        $" AmountTendered," +
                        $" AmountReturn," +
                        $" CreatedDate," +
                        $" IsDeposit," +
                        $" GiftExtra," +
                        $" Cashier," +
                        $" ShiftID," +
                        $" GiftCardCode," +
                        $" CurrencyAmount," +
                        $" CurrencyID," +
                        $" PaymentReceiptID," +
                        $" PayByAccuPoint," +
                        $" PaymentType," +
                        $" Gratuity," +
                        $" CreditCardTransactionID," +
                        $" CreditCardTransactionNumber," +
                        $" CreditCardTransactionCardNumber," +
                        $" CreditCardTransactionType," +
                        $" PaidServiceFee," +
                        $" PaidPayout," +
                        $" CreditCardRefNo," +
                        $" CreditCardAuthCode," +
                        $" SVCFeeAmount," +
                        $" SVCFeeForGratuity," +
                        $" SVCFeeForServiceCharge," +
                        $" CashDiscountAmount," +
                        $" MerchantFee," +
                        $" PartyServiceFeeAmount," +
                        $" SVCFeeForPartyServiceFeeAmount," +
                        $" AreaFeeAmount," +
                        $" SVCFeeForAreaFeeAmount," +
                        $" ServiceFee," +
                        $" SVCFeeForServiceFee," +
                        $" MerchantFeeForTip," +
                        $" MerchantFeeForServiceFee," +
                        $" MerchantFeeForAreaFeeAmount," +
                        $" MerchantFeeForPartyServiceFeeAmount," +
                        $" CardHolderName," +
                        $" CardBalance," +
                        $" AutoGratuityAmount," +
                        $" SVCFeeForAutoGratuityAmount," +
                        $" MerchantFeeForAutoGratuityAmount," +
                        $" IsDecline," +
                        $" IsDepositPayment," +
                        $" HouseAccountNumber," +
                        $" GiftCardTransactionID," +
                        $" CreditCardCardType," +
                        $" SVCFeeTaxAmount," +
                        $" CashDiscountTaxAmount," +
                        $" ServiceFeeTaxAmount," +
                        $" AreaFeeTaxAmount," +
                        $" PartyServiceFeeTaxAmount," +
                        $" AutoGratuityTaxAmount," +
                        $" TaxAmountByPaymentModeTaxCustomFee," +
                        $" IsCalculateTaxCustomFee," +
                        $" CoinDiscountAmount," +
                        $" CreditCardTransactionMerchantID )" +
                        $"{Environment.NewLine} VALUES (" +
                        $" '{maxSaleReceiptPaymentDetailID}'," +
                        $" '{newSaleID}'," +
                        $" '{item.PaymentMethodID}'," +
                        $" '0'," +
                        $" '{item.AmountTendered}'," +
                        $" '{item.CreatedDate}'," +
                        $" '{item.IsDeposit}'," +
                        $" '{item.GiftExtra}'," +
                        $" '{item.Cashier}'," +
                        $" '{item.ShiftID}'," +
                        $" null," +
                        $" '{item.CurrencyAmount}'," +
                        $" '{item.CurrencyID}'," +
                        $" null," +
                        $" '{item.PayByAccuPoint}'," +
                        $" '{item.PaymentType}'," +
                        $" '0'," +
                        $" ''," +
                        $" ''," +
                        $" ''," +
                        $" ''," +
                        $" '0'," +
                        $" '0'," +
                        $" ''," +
                        $" ''," +
                        $" '0'," +
                        $" '0'," +
                        $" '0'," +
                        $" '0'," +
                        $" '0'," +
                        $" '0'," +
                        $" '0'," +
                        $" '0'," +
                        $" '0'," +
                        $" '0'," +
                        $" '0'," +
                        $" '0'," +
                        $" '0'," +
                        $" '0'," +
                        $" '0'," +
                        $" ''," +
                        $" '0'," +
                        $" '0'," +
                        $" '0'," +
                        $" '0'," +
                        $" '0'," +
                        $" '0'," +
                        $" ''," +
                        $" ''," +
                        $" ''," +
                        $" '0'," +
                        $" '0'," +
                        $" '0'," +
                        $" '0'," +
                        $" '0'," +
                        $" '0'," +
                        $" '0'," +
                        $" '0'," +
                        $" '0'," +
                        $" '');";

                        maxSaleReceiptPaymentDetailID++;
                    }

                    text += $" {Environment.NewLine}{Environment.NewLine}";
                    text += $"  SET IDENTITY_INSERT SaleReceiptPaymentDetails OFF {Environment.NewLine}";
                    text += $"{Environment.NewLine}END {Environment.NewLine}";
                    text += $"GO {Environment.NewLine}";
                }
            }

            if (string.IsNullOrWhiteSpace(creditCardTransactionIDs))
                return;

            using (var conn = new SqlConnection(txtMerchantConnection.Text.Trim()))
            {
                var sql = $"select * from TransactionDetail where ID in ({creditCardTransactionIDs}) ";

                var transactionDetails = await conn.QueryAsync<TransactionDetailEntity>(sql, "", null, 180, System.Data.CommandType.Text);

                text += $"{Environment.NewLine}use MERCHANT ";

                foreach (TransactionDetailEntity item in transactionDetails)
                {
                    text += $"{Environment.NewLine}IF EXISTS (SELECT * FROM TransactionDetail where ID = '{item.ID}' and TranStatus = 'VOIDED') ";
                    text += $"{Environment.NewLine}BEGIN ";

                    text += $"{Environment.NewLine} update TransactionDetail set TranStatus = 'VOIDED' where ID = '{item.ID}';";

                    text += $" {Environment.NewLine}{Environment.NewLine} insert into TransactionDetail (" +
                        $" ID," +
                        $" Number," +
                        $" Date," +
                        $" TranType," +
                        $" TenderType," +
                        $" CardNumber," +
                        $" CardType," +
                        $" ExpDate," +
                        $" Result," +
                        $" TextResponse," +
                        $" CaptureStatus," +
                        $" AVSResult," +
                        $" CVVResult," +
                        $" InvoiceNo," +
                        $" CustomerName," +
                        $" AuthCode," +
                        $" RefNo," +
                        $" AuthorizeAmount," +
                        $" Amount," +
                        $" Gratuity," +
                        $" Total," +
                        $" BatchID," +
                        $" BatchTotal," +
                        $" BatchStatus," +
                        $" ItemID," +
                        $" Token," +
                        $" CCEntry," +
                        $" Email," +
                        $" Address," +
                        $" ZipCode," +
                        $" Description," +
                        $" ReturnCode," +
                        $" AcqRefData," +
                        $" ProcessData," +
                        $" UserTraceData," +
                        $" MerchantID," +
                        $" ShiftID," +
                        $" Register," +
                        $" UserID," +
                        $" Cashier," +
                        $" TrackNo," +
                        $" TranStatus," +
                        $" TableName," +
                        $" TerminalName," +
                        $" ISOCode," +
                        $" ProcessingSetName," +
                        $" SecondaryResponseCode," +
                        $" BankApprovalCode," +
                        $" BankTransactionID," +
                        $" OriginalAmount," +
                        $" OriginalTip," +
                        $" WorkstationID," +
                        $" WorkstationName," +
                        $" IsChecked," +
                        $" Returned," +
                        $" Field7," +
                        $" Field8," +
                        $" OrderID," +
                        $" SVCFeeForGratuity," +
                        $" IsTipInLine," +
                        $" SVCFee," +
                        $" TicketNumber," +
                        $" RegisterID," +
                        $" AID," +
                        $" TC," +
                        $" MerchantAccount," +
                        $" BatchUnique," +
                        $" ServerID," +
                        $" ServerName," +
                        $" SubTotal," +
                        $" TaxAmount," +
                        $" DiscountAmount," +
                        $" ServiceCharge," +
                        $" TotalAmount," +
                        $" PartyServiceFeeAmount," +
                        $" AreaFeeAmount," +
                        $" ServiceFee," +
                        $" TerminalIpAddress," +
                        $" MerchantFee," +
                        $" MerchantFeeForTip," +
                        $" IsTryVoidOpenTab," +
                        $" TicketCaptured," +
                        $" AmountCaptured," +
                        $" TipCaptured," +
                        $" MerchantServer," +
                        $" Pin," +
                        $" IsPhone," +
                        $" AutoGratuityAmount," +
                        $" CreditCardInfo," +
                        $" TicketOrder," +
                        $" SVCFeeTaxAmount," +
                        $" AdjustTipInformation," +
                        $" SVCFeeType," +
                        $" MerchantFeePercent)" +
                        $"{Environment.NewLine} VALUES (" +
                        $" NEWID()," +
                        $" 'V_{item.Number}'," +
                        $" '{item.Date}'," +
                        $" 'VoidSale'," +
                        $" '{item.TenderType}'," +
                        $" '{item.CardNumber}'," +
                        $" '{item.CardType}'," +
                        $" '{item.ExpDate}'," +
                        $" '{item.Result}'," +
                        $" '{item.TextResponse}'," +
                        $" '{item.CaptureStatus}'," +
                        $" '{item.AVSResult}'," +
                        $" '{item.CVVResult}'," +
                        $" '{item.InvoiceNo}'," +
                        $" '{item.CustomerName}'," +
                        $" '{item.AuthCode}'," +
                        $" '{item.RefNo}'," +
                        $" '{item.Amount + item.Gratuity}'," +
                        $" '{item.Amount}'," +
                        $" '{item.Gratuity}'," +
                        $" '{item.Total}'," +
                        $" '{item.BatchID}'," +
                        $" '{item.BatchTotal}'," +
                        $" '{item.BatchStatus}'," +
                        $" '{item.ItemID}'," +
                        $" '{item.Token}'," +
                        $" '{item.CCEntry}'," +
                        $" '{item.Email}'," +
                        $" '{item.Address}'," +
                        $" '{item.ZipCode}'," +
                        $" '{item.Description}'," +
                        $" '{item.ReturnCode}'," +
                        $" '{item.AcqRefData}'," +
                        $" '{item.ProcessData}'," +
                        $" '{item.UserTraceData}'," +
                        $" '{item.MerchantID}'," +
                        $" '{item.ShiftID}'," +
                        $" '{item.Register}'," +
                        $" '{item.UserID}'," +
                        $" '{item.Cashier}'," +
                        $" '{item.TrackNo}'," +
                        $" 'ACTIVE'," +
                        $" '{item.TableName}'," +
                        $" '{item.TerminalName}'," +
                        $" '{item.ISOCode}'," +
                        $" '{item.ProcessingSetName}'," +
                        $" '{item.SecondaryResponseCode}'," +
                        $" '{item.BankApprovalCode}'," +
                        $" '{item.BankTransactionID}'," +
                        $" '{item.OriginalAmount}'," +
                        $" '{item.Gratuity}'," +
                        $" '{item.WorkstationID}'," +
                        $" '{item.WorkstationName}'," +
                        $" '{item.IsChecked}'," +
                        $" '{item.Returned}'," +
                        $" '{item.Field7}'," +
                        $" '{item.Field8}'," +
                        $" '{item.OrderID}'," +
                        $" '{item.SVCFeeForGratuity}'," +
                        (item.IsTipInLine ? "1," : "0,")+
                        $" '{item.SVCFee}'," +
                        $" '{item.TicketNumber}'," +
                        $" '{item.RegisterID}'," +
                        $" ''," +
                        $" ''," +
                        $" '{item.MerchantAccount}'," +
                        $" '{item.BatchUnique}'," +
                        $" '{item.ServerID}'," +
                        $" '{item.ServerName}'," +
                        $" '{item.SubTotal}'," +
                        $" '{item.TaxAmount}'," +
                        $" '{item.DiscountAmount}'," +
                        $" '{item.ServiceCharge}'," +
                        $" '{item.TotalAmount}'," +
                        $" '{item.PartyServiceFeeAmount}'," +
                        $" '{item.AreaFeeAmount}'," +
                        $" '{item.ServiceFee}'," +
                        $" '{item.TerminalIpAddress}'," +
                        $" '{item.MerchantFee}'," +
                        $" '{item.MerchantFeeForTip}'," +
                        (item.IsTryVoidOpenTab ? "1," : "0,") +
                        $" '{item.TicketCaptured}'," +
                        $" '{item.AmountCaptured}'," +
                        $" '{item.TipCaptured}'," +
                        $" '{item.MerchantServer}'," +
                        $" '{item.Pin}'," +
                        (item.IsPhone ? "1," : "0,") +
                        $" '{item.AutoGratuityAmount}'," +
                        $" '{item.CreditCardInfo}'," +
                        $" '{item.TicketOrder}'," +
                        $" '{item.SVCFeeTaxAmount}'," +
                        $" ''," +
                        $" '{item.SVCFeeType}'," +
                        $" '{item.MerchantFeePercent}');";


                    if (item.BatchStatus == "Closed")
                    {
                        text += $"{Environment.NewLine}{Environment.NewLine} update Batch set " +
                            $" Total = Total - {item.Total}, " +
                            $" TotalSVCFeeForTip = TotalSVCFeeForTip - {item.SVCFeeForGratuity}, " +
                            $" TotalSales = TotalSales - {item.Amount}, " +
                            $" TotalTips = TotalTips - {item.Gratuity}, " +
                            $" TotalSVCFeeAmount = TotalSVCFeeAmount - {item.SVCFee}, " +
                            $" DepositAmount = DepositAmount - {item.Total}, " +
                            $" Quantity = Quantity + 1, " +
                            $" MerchantFeeAmount = MerchantFeeAmount - {item.MerchantFee} " +
                            $" where BatchUnique = '{item.BatchUnique}';";

                        text += $"{Environment.NewLine}{Environment.NewLine} update TransactionDetail set " +
                            $" BatchTotal = BatchTotal - {item.Total} " +
                            $" where BatchUnique = '{item.BatchUnique}';";
                    }

                    text += $"{Environment.NewLine}END ";
                    text += $"{Environment.NewLine}GO ";
                }
            }

            txtScript.Text += text;

        }

        private async void BtnConnect_Click(object sender, EventArgs e)
        {
            using (var conn = new SqlConnection(txtPOSConnection.Text.Trim()))
            {
                var sql = $"select * from Stores ";

                var listStore = await conn.QueryAsync<StoreEntity>(sql, "", null, 180, System.Data.CommandType.Text);

                lblStatusPOS.Visible = listStore.Any();
                txtPOSConnection.Enabled = !listStore.Any();
            }

            using (var conn = new SqlConnection(txtMerchantConnection.Text.Trim()))
            {
                var sql = $"select top 1 * from Batch ";

                var listStore = await conn.QueryAsync<BatchEntity>(sql, "", null, 180, System.Data.CommandType.Text);

                lblStatusMerchant.Visible = listStore.Any();
                txtMerchantConnection.Enabled = !listStore.Any();
            }

            btnConnect.Enabled = !lblStatusMerchant.Visible || !lblStatusPOS.Visible;
        }

        private async void BtnStart_Click(object sender, EventArgs e)
        {
            try
            {
                BLogicLogger.Debug("----- START ADD BATCH DETAIL -----");
                BLogicLogger.Debug("Connect Merchant Database");
                using (var conn = new SqlConnection(MerchantConnection))
                {
                    var sql = $"select * from TransactionDetail t where t.BatchUnique = '1b4deaac-15ae-4fe5-973e-d6778c9d5c72' ";

                    var listTransactionDetails = await conn.QueryAsync<TransactionDetailEntity>(sql, "", null, 180, System.Data.CommandType.Text);

                    BLogicLogger.Debug($"Get TransactionDetail -> return {listTransactionDetails.Count()} transaction");
                    if (listTransactionDetails.Count() == 0)
                    {
                        MessageBox.Show("Can not found any transaction to add new Batch", "Error");
                        this.Close();
                    }

                    var groupTransactions = listTransactionDetails.GroupBy(x => new { x.BatchUnique });

                    BLogicLogger.Debug($"Start insert new batch & update TransactionDetail");

                    foreach (var group in groupTransactions)
                    {
                        var listTransactions = group.OrderByDescending(x => x.Date).ToList();

                        int iCount = 0;
                        decimal dTotal = 0;
                        decimal dTotalSales = 0;
                        decimal dTotalTips = 0;
                        decimal dSVCFeeAmount = 0;
                        decimal dSVCFeeForTip = 0;
                        string sWorkstationName = string.Empty;
                        decimal dTotalReturns = 0;
                        decimal dTotalMerchantFee = 0;
                        foreach (var item in listTransactions)
                        {
                            string result = item.Result;
                            string type = item.TranType;
                            string sMerchantID = item.MerchantID;
                            decimal subTotal = item.Total;
                            decimal subAmount = item.Amount;
                            decimal subSVCFee = item.SVCFee;
                            decimal subTip = item.Gratuity;
                            decimal subSVCFeeAmount = item.SVCFee;
                            decimal subSVCFeeForTip = item.SVCFeeForGratuity;
                            decimal subReturn = item.Amount;
                            string tranStatus = item.TranStatus;
                            if (result == "Approved")
                            {
                                var BatchID = item.BatchID;
                                iCount++;
                                if (!string.IsNullOrEmpty(item.WorkstationName))
                                    sWorkstationName = item.WorkstationName;
                                if (type == "Sale" || type == "PreAuthCapture")
                                {
                                    if (tranStatus == "ACTIVE")
                                    {
                                        dTotal += subTotal;
                                        dTotalSales += (subAmount - subSVCFee);
                                        dTotalTips += subTip;
                                        dSVCFeeAmount += subSVCFeeAmount;
                                        dSVCFeeForTip += subSVCFeeForTip;
                                        dTotalMerchantFee += item.MerchantFee;
                                    }
                                }
                                else if (type == "Return")
                                {
                                    dTotal -= subTotal;
                                    dTotalReturns += subReturn;
                                    dTotalMerchantFee -= item.MerchantFee;
                                }
                                else
                                {

                                }
                            }
                            else
                            {

                            }
                        }
                        var TotalReturns = dTotalReturns;
                        var TotalSales = dTotalSales;
                        var TotalTips = dTotalTips;
                        var WorkStationName = sWorkstationName;
                        var TransactionCount = iCount;
                        var Total = dTotal;
                        var MerchantFeePercent = MerchantFeeValue;
                        var MerchantFeeAmount = dTotalMerchantFee;// CalculateMerchantFee(dTotal + dTotalReturns);
                        var DepositAmount = dTotal - MerchantFeeAmount;
                        var TotalSVCFeeAmount = dSVCFeeAmount;
                        var TotalSVCFeeForTip = dSVCFeeForTip;

                        if (TransactionCount <= 0)
                            continue;
                        return;
                        Guid batchUnique = Guid.NewGuid();

                        DateTime date = listTransactions[0].Date.Date + new TimeSpan(23, 55, 00);

                        sql = $"INSERT INTO Batch (Date, ID, Status, MerchantID, Quantity, Total, BatchUnique, TotalSVCFeeForTip, TotalSales, TotalReturns, TotalTips, TotalSVCFeeAmount, DepositAmount, WorkStationName, MerchantFeeAmount, MerchantFeePercent, ExpandInfo) " +
                            $"values ('{date}'," +
                            $"'{listTransactions[0].BatchID}'," +
                            $"'Closed'," +
                            $"'{listTransactions[0].MerchantID}'," +
                            $"'{TransactionCount}'," +
                            $"'{Total}'," +
                            $"'{batchUnique}'," +
                            $"'{TotalSVCFeeForTip}'," +
                            $"'{TotalSales}'," +
                            $"'{TotalReturns}'," +
                            $"'{TotalTips}'," +
                            $"'{TotalSVCFeeAmount}'," +
                            $"'{DepositAmount}'," +
                            $"'{sWorkstationName}'," +
                            $"'{MerchantFeeAmount}'," +
                            $"'{MerchantFeeValue}'," +
                        $"NULL)";

                        var resultInsert = await conn.ExecuteAsync(sql, null, null, TimeOutQuery, System.Data.CommandType.Text);

                        if (resultInsert > 0)
                        {
                            string ids = String.Join(",", listTransactions.Select(i => { return "'" + i.ID + "'"; }));

                            sql = $"update TransactionDetail set BatchUnique = '{batchUnique}', BatchTotal = '{Total}' where ID in ({ids})";

                            var resultUpdate = await conn.ExecuteAsync(sql, null, null, TimeOutQuery, System.Data.CommandType.Text);

                            if (resultInsert <= 0)
                            {
                                BLogicLogger.Error($"Update TransactionDetail error with query: {sql}");
                            }
                        }
                        else
                        {
                            BLogicLogger.Error($"Insert new Batch error with query: {sql}");
                        }
                    }

                    MessageBox.Show("Add new Batch & update TransactionDetail -> Success.", "Information");

                    BLogicLogger.Debug($"Close Form");
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                BLogicLogger.Error($"BtnStart_Click error with: {ex.Message}", ex);
                this.Close();
            }

        }

        public decimal CalculateMerchantFee(decimal total)
        {
            if (total <= 0)
                return 0;
            return RoundUp(total * (MerchantFeeValue / 100));

            //if (CSetting.SVCFeeType == "1")
            //{
            //    return RoundUp(total * (CSetting.MerchantFeeValue / 100));
            //}
            //else
            //{
            //    return 0;
            //}

        }

        public static decimal RoundUp(decimal dInValue)
        {
            decimal dReturn = Math.Round(dInValue, 2);
            if (dReturn >= dInValue)
                return dReturn;
            //dReturn < dInValue
            decimal dSub = dInValue - dReturn;
            if (dSub >= (decimal)0.005)
                dReturn += (decimal)0.01;
            return dReturn;
        }
    }
}
