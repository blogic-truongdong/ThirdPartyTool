using BLogicPOS7SupportTool.Core;
using BLogicPOS7SupportTool.Models;
using BLogicSystems.XPos.Utilites;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;

namespace BLogicPOS7SupportTool.Services
{
    class ReceiptService
    {
        public DataTable GetStores()
        {
            string sql = "SELECT *e FROM dbo.Stores ORDER BY StoreName";
            var dt = AppSession.ProcessQuery(true, sql, "Stores");
            return dt;
        }

        public SaleReceiptsEntity GetSaleReceiptsEntity(string receiptId)
        {
            string sql = $"SELECT * FROM dbo.SaleReceipts WHERE SaleReceiptID = '{receiptId}'";
            var entity = AppSession.QueryFirstOrDefault<SaleReceiptsEntity>(true, sql);
            return entity;
        }

        public DataTable GetReceipts(string search)
        {
            string sql = $@"
                SELECT *
                FROM dbo.SaleReceipts sr
                WHERE sr.SaleReceiptNumber = '{search.Replace("'", "''")}' and TransactionStatusID in ('OPER','CANR','VOID','FIN','RET')
                ORDER BY sr.DateCreated DESC";

            var dt = AppSession.ProcessQuery(true, sql, "Receipts");
            AppLogger.Debug($"GetReceipts(search='{search}'): {dt.Rows.Count} rows");
            return dt;
        }

        public (ReceiptHeaderEntity Header, List<ReceiptLineItemEntity> Items, List<ReceiptTaxLineEntity> TaxLines, List<ReceiptPaymentLineEntity> PaymentLines) GetReceiptDetail(string receiptId)
        {
            // 1. SaleReceiptDetails
            var idt = AppSession.ProcessQuery(true, $@"
                SELECT *
                FROM dbo.SaleReceiptDetails
                WHERE SaleReceiptID = '{receiptId}' AND Status <> 2
                ORDER BY Range", "ReceiptDetails");

            var items = new List<ReceiptLineItemEntity>();
            foreach (DataRow row in idt.Rows)
                items.Add(new ReceiptLineItemEntity
                {
                    IsModifier = AppSession.GetString(row, "PriceInitial") == "p",
                    Description = AppSession.GetString(row, "Description"),
                    Quantity = AppSession.GetDouble(row, "Quantity"),
                    Price = AppSession.GetDecimal(row, "Price"),
                    ExtendedPrice = AppSession.GetDecimal(row, "ExtendedPrice"),
                    DiscountAmount = AppSession.GetDecimal(row, "ItemDiscountValue")
                        + AppSession.GetDecimal(row, "TotalSaleDiscountAmount")
                        + AppSession.GetDecimal(row, "StoreDiscountValue")
                        + AppSession.GetDecimal(row, "DiscountAmount")
                        + AppSession.GetDecimal(row, "TotalSaleDiscountAmountByCustomer")
                        + AppSession.GetDecimal(row, "ItemDiscountByPriceLevelAmount"),
                    IsCompItem = AppSession.GetBool(row, "IsCompItem"),
                    Status = AppSession.GetInt(row, "Status"),
                    TaxAmount = AppSession.GetDecimal(row, "TaxAmount"),
                    Taxable = AppSession.GetBool(row, "Taxable")
                });

            AppLogger.Debug($"GetReceiptDetail({receiptId}): {items.Count} line items");

            // 2. SaleReceipts
            var rdt = AppSession.ProcessQuery(true, $@"
                SELECT *
                FROM dbo.SaleReceipts
                WHERE SaleReceiptID = '{receiptId}'", "SaleReceipts");

            var header = new ReceiptHeaderEntity();
            if (rdt?.Rows.Count == 0) return (header, items, new List<ReceiptTaxLineEntity>(), new List<ReceiptPaymentLineEntity>());

            var r = rdt.Rows[0];
            header.SaleReceiptID = AppSession.GetGuid(r, "SaleReceiptID");
            header.SaleReceiptNumber = AppSession.GetString(r, "SaleReceiptNumber");
            header.TransactionStatusID = AppSession.GetString(r, "TransactionStatusID");
            header.DateCreated = AppSession.GetDateTime(r, "DateCreated");
            header.WorkstationName = AppSession.GetString(r, "WorkstationName");
            header.CurrentCustomerName = AppSession.GetString(r, "CurrentCustomerName");
            header.CurrentCustomerPhone = AppSession.GetString(r, "CurrentCustomerPhone");
            header.TotalAmount = AppSession.GetDecimal(r, "TotalAmount");
            header.TotalDiscountAmount = AppSession.GetDecimal(r, "TotalDiscountAmount");
            header.AutoGratuityAmount = AppSession.GetDecimal(r, "AutoGratuityAmount");
            header.AreaFeeAmount = AppSession.GetDecimal(r, "AreaFeeAmount");
            header.PartyServiceFeeAmount = AppSession.GetDecimal(r, "PartyServiceFeeAmount");
            header.SalesOrderFee = AppSession.GetDecimal(r, "SalesOrderFee");
            header.ServiceFee = AppSession.GetDecimal(r, "ServiceFee");
            header.ExtraFeeAmount = AppSession.GetDecimal(r, "ExtraFeeAmount");
            header.SVCFeeAmount = AppSession.GetDecimal(r, "SVCFeeAmount");
            header.SVCFeePaidAmount = AppSession.GetDecimal(r, "SVCFeePaidAmount");
            header.CashDiscountPaidAmount = AppSession.GetDecimal(r, "CashDiscountPaidAmount");
            header.TaxAmount = AppSession.GetDecimal(r, "TaxAmount");
            header.SVCFeeTaxRate = AppSession.GetDecimal(r, "SVCFeeTaxRate");
            header.SVCFeeTaxPaidAmount = AppSession.GetDecimal(r, "SVCFeeTaxPaidAmount");
            header.TipAmount = AppSession.GetDecimal(r, "FinalGratuity");
            header.FinalTotal = header.TotalAmount + header.SVCFeePaidAmount - header.CashDiscountPaidAmount;
            header.SubTotal = header.TotalAmount - header.TaxAmount - header.SalesOrderFee + header.TotalDiscountAmount;

            // 3. Stores (by StoreID from receipt)
            var storeId = AppSession.GetString(r, "StoreID");
            if (!string.IsNullOrEmpty(storeId))
            {
                var sdt = AppSession.ProcessQuery(true, $@"
                    SELECT *
                    FROM dbo.Stores
                    WHERE StoreID = '{storeId.Replace("'", "''")}'", "Stores");
                if (sdt?.Rows.Count > 0)
                {
                    var s = sdt.Rows[0];
                    header.StoreName = AppSession.GetString(s, "StoreName");
                    header.StoreAddress = AppSession.GetString(s, "StoreStreet");
                    header.StorePhone = AppSession.GetString(s, "StorePhone1");
                    header.StoreEmail = AppSession.GetString(s, "StoreEmail1");
                    header.StoreWebsite = AppSession.GetString(s, "StoreWebsite");
                }
            }

            // 4. Employees (by CashierID from receipt)
            var cashierId = AppSession.GetInt(r, "CashierID");
            if (cashierId > 0)
            {
                var edt = AppSession.ProcessQuery(true, $@"
                    SELECT *
                    FROM dbo.Employees
                    WHERE EmployeeID = {cashierId}", "Employees");
                if (edt?.Rows.Count > 0)
                {
                    var e = edt.Rows[0];
                    header.CashierName = (AppSession.GetString(e, "EmployeeFirstName") + " "
                        + AppSession.GetString(e, "EmployeeLastName")).Trim();
                }
            }

            // 5. Tables (by TableID from receipt)
            var tableId = AppSession.GetString(r, "TableID");
            if (!string.IsNullOrEmpty(tableId))
            {
                var tdt = AppSession.ProcessQuery(true, $@"
                    SELECT *
                    FROM dbo.Tables
                    WHERE TableID = '{tableId}'", "Tables");
                if (tdt?.Rows.Count > 0)
                    header.TableName = AppSession.GetString(tdt.Rows[0], "TableName");
            }

            var customerID = AppSession.GetString(r, "CustomerID");
            if (!string.IsNullOrWhiteSpace(customerID))
            {
                var edt = AppSession.ProcessQuery(true, $@"
                    SELECT *
                    FROM dbo.Customers
                    WHERE CustomerID = '{customerID}'", "Customers");

                if (edt?.Rows.Count > 0)
                {
                    var e = edt.Rows[0];

                    if (string.IsNullOrWhiteSpace(header.CurrentCustomerName))
                        header.CurrentCustomerName = (AppSession.GetString(e, "CustomerFirstName") + " "
                            + AppSession.GetString(e, "CustomerLastName")).Trim();

                    if (string.IsNullOrWhiteSpace(header.CurrentCustomerPhone))
                        header.CurrentCustomerPhone = AppSession.GetString(e, "CustomerPhone1");

                }
            }

            string sqlTaxLines = $@"
                    SELECT TaxRate, SUM(TaxAmount) AS TaxAmount
                    FROM dbo.SaleReceiptDetailTaxDetails
                    WHERE SaleReceiptID = '{receiptId}' AND TaxAmount > 0 and Status <> '2' AND IsCompItem = '0'
                    GROUP BY TaxRate
                    ORDER BY TaxRate";
            var dtTaxLines = AppSession.ProcessQuery(true, sqlTaxLines, "TaxLines");

            if (dtTaxLines == null || dtTaxLines.Rows.Count == 0)
            {
                string fallbackSql = $@"
                SELECT TaxRate, SUM(TaxAmount) AS TaxAmount
                FROM dbo.SaleReceiptDetails
                WHERE SaleReceiptID = '{receiptId}' AND TaxAmount > 0 AND Status <> 2 AND IsCompItem = '0'
                GROUP BY TaxRate
                ORDER BY TaxRate";

                dtTaxLines = AppSession.ProcessQuery(true, sqlTaxLines, "TaxLines");
            }

            var newRow = dtTaxLines.NewRow();
            if (header.SVCFeeTaxPaidAmount > 0)
            {
                newRow["TaxRate"] = header.SVCFeeTaxRate;
                newRow["TaxAmount"] = header.SVCFeeTaxPaidAmount;
                dtTaxLines.Rows.Add(newRow);
            }

            var sqlSaleReceiptServiceChargeTaxDetails = $"SELECT * FROM dbo.SaleReceiptServiceChargeTaxDetails WHERE SaleReceiptID = '{receiptId}'";
            var dtSaleReceiptServiceChargeTaxDetails = AppSession.ProcessQuery(true, sqlSaleReceiptServiceChargeTaxDetails, "SaleReceiptServiceChargeTaxDetails");
            if (dtSaleReceiptServiceChargeTaxDetails?.Rows.Count > 0)
            {
                foreach (DataRow taxLine in dtSaleReceiptServiceChargeTaxDetails.Rows)
                {
                    newRow = dtTaxLines.NewRow();
                    newRow["TaxRate"] = AppSession.GetDecimal(taxLine, "ServiceChargeTaxRate");
                    newRow["TaxAmount"] = AppSession.GetDecimal(taxLine, "ServiceFeeTaxAmount") +
                            AppSession.GetDecimal(taxLine, "PartyServiceFeeTaxAmount") +
                            AppSession.GetDecimal(taxLine, "AreaFeeTaxAmount") +
                            AppSession.GetDecimal(taxLine, "AutoGratuityTaxAmount") +
                            AppSession.GetDecimal(taxLine, "ExtraFeeTaxAmount") +
                            AppSession.GetDecimal(taxLine, "DeliveryFeeTaxAmount");

                    dtTaxLines.Rows.Add(newRow);
                }
            }

            var listTaxLines = new List<ReceiptTaxLineEntity>();

            var groups = dtTaxLines.AsEnumerable().GroupBy(x => AppSession.GetDecimal(x, "TaxRate"));
            foreach (var itemGroup in groups)
            {
                listTaxLines.Add(new ReceiptTaxLineEntity
                {
                    TaxRate = itemGroup.Key,
                    TaxAmount = itemGroup.Sum(x => AppSession.GetDecimal(x, "TaxAmount"))
                });
            }

            return (header, items, listTaxLines, GetPaymentLines(receiptId));
        }

        private List<ReceiptPaymentLineEntity> GetPaymentLines(string receiptId)
        {
            // 1. SaleReceiptPaymentDetails
            var pdt = AppSession.ProcessQuery(true, $@"
                SELECT *
                FROM dbo.SaleReceiptPaymentDetails
                WHERE SaleReceiptID = '{receiptId}'", "PaymentDetails");

            if (pdt == null || pdt.Rows.Count == 0) return new List<ReceiptPaymentLineEntity>();

            // 2. PaymentMethods — filter by IDs present in this receipt
            var ids = string.Join(",",
                pdt.Rows.Cast<DataRow>()
                   .Select(row => AppSession.GetInt(row, "PaymentMethodID"))
                   .Distinct());

            var mdt = AppSession.ProcessQuery(true, $@"
                SELECT *
                FROM dbo.PaymentMethods
                WHERE PaymentMethodID IN ({ids})", "PaymentMethods");

            var methodNames = new Dictionary<int, string>();
            if (mdt != null)
                foreach (DataRow row in mdt.Rows)
                    methodNames[AppSession.GetInt(row, "PaymentMethodID")] = AppSession.GetString(row, "PaymentMethodName");

            // 3. Map
            var lines = new List<ReceiptPaymentLineEntity>();
            foreach (DataRow row in pdt.Rows)
            {
                var amount = AppSession.GetDecimal(row, "AmountTendered")
                    - AppSession.GetDecimal(row, "AmountReturn")
                    + AppSession.GetDecimal(row, "Gratuity")
                    + AppSession.GetDecimal(row, "SVCFeeAmount")
                    - AppSession.GetDecimal(row, "CashDiscountAmount");

                var methodId = AppSession.GetInt(row, "PaymentMethodID");
                lines.Add(new ReceiptPaymentLineEntity
                {
                    PaymentMethodName = methodNames.TryGetValue(methodId, out var name) ? name : "Payment",
                    Amount = amount
                });
            }
            return lines;
        }

        public List<ReceiptPaymentDetailEntity> GetPaymentDetails(string receiptId)
        {
            const string sql = @"
                SELECT a.*, b.PaymentMethodName, b.CashAdjust
                FROM dbo.SaleReceiptPaymentDetails a
                LEFT JOIN dbo.PaymentMethods b ON a.PaymentMethodID = b.PaymentMethodID
                WHERE a.SaleReceiptID = @receiptId";

            return AppSession.Query<ReceiptPaymentDetailEntity>(true, sql, new { receiptId });
        }

        // Updates the editable money fields of one payment (Payment Details form).
        public bool UpdatePaymentDetail(long id, decimal amountTendered, decimal amountReturn,
            decimal svcFeeAmount, decimal svcFeeTaxAmount, decimal cashDiscountAmount,
            decimal cashDiscountTaxAmount, decimal gratuity, decimal svcFeeForGratuity,
            decimal merchantFee, decimal merchantFeeForTip, string CreditCardTransactionID)
        {
            svcFeeAmount += svcFeeTaxAmount;
            cashDiscountAmount += cashDiscountTaxAmount;

            var inv = CultureInfo.InvariantCulture;
            var listScript = new List<string>();

            string sql = $"select * from SaleReceiptPaymentDetails where SaleReceiptPaymentDetailID = '{id}'";
            var payment = AppSession.QueryFirstOrDefault<SaleReceiptPaymentDetailsEntity>(true, sql);

            if (payment != null)
            {
                if (payment.AmountTendered != amountTendered)
                    listScript.Add($"UPDATE dbo.SaleReceiptPaymentDetails SET AmountTendered = {amountTendered.ToString(inv)} WHERE SaleReceiptPaymentDetailID = {id}");

                if (payment.AmountReturn != amountReturn)
                    listScript.Add($@" UPDATE dbo.SaleReceiptPaymentDetails SET AmountReturn          = {amountReturn.ToString(inv)} WHERE SaleReceiptPaymentDetailID = {id}");

                if (payment.Gratuity != gratuity)
                    listScript.Add($@" UPDATE dbo.SaleReceiptPaymentDetails SET Gratuity              = {gratuity.ToString(inv)} WHERE SaleReceiptPaymentDetailID = {id}");

                if (payment.SVCFeeAmount != svcFeeAmount)
                    listScript.Add($@" UPDATE dbo.SaleReceiptPaymentDetails SET SVCFeeAmount          = {svcFeeAmount.ToString(inv)} WHERE SaleReceiptPaymentDetailID = {id}");

                if (payment.CashDiscountAmount != cashDiscountAmount)
                    listScript.Add($@" UPDATE dbo.SaleReceiptPaymentDetails SET CashDiscountAmount    = {cashDiscountAmount.ToString(inv)} WHERE SaleReceiptPaymentDetailID = {id}");

                if (payment.SVCFeeTaxAmount != svcFeeTaxAmount)
                    listScript.Add($@" UPDATE dbo.SaleReceiptPaymentDetails SET SVCFeeTaxAmount       = {svcFeeTaxAmount.ToString(inv)} WHERE SaleReceiptPaymentDetailID = {id}");

                if (payment.CashDiscountTaxAmount != cashDiscountTaxAmount)
                    listScript.Add($@" UPDATE dbo.SaleReceiptPaymentDetails SET CashDiscountTaxAmount = {cashDiscountTaxAmount.ToString(inv)} WHERE SaleReceiptPaymentDetailID = {id}");

                if (payment.SVCFeeForGratuity != svcFeeForGratuity)
                    listScript.Add($@" UPDATE dbo.SaleReceiptPaymentDetails SET SVCFeeForGratuity     = {svcFeeForGratuity.ToString(inv)} WHERE SaleReceiptPaymentDetailID = {id}");

                if (payment.MerchantFee != merchantFee)
                    listScript.Add($@" UPDATE dbo.SaleReceiptPaymentDetails SET MerchantFee     = {merchantFee.ToString(inv)} WHERE SaleReceiptPaymentDetailID = {id}");

                if (payment.MerchantFeeForTip != merchantFeeForTip)
                    listScript.Add($@" UPDATE dbo.SaleReceiptPaymentDetails SET MerchantFeeForTip     = {merchantFeeForTip.ToString(inv)} WHERE SaleReceiptPaymentDetailID = {id}");

                foreach (var script in listScript)
                {
                    AppSession.ProcessNonQuery(true, script);
                }
            }

            if (!string.IsNullOrWhiteSpace(CreditCardTransactionID))
            {
                sql = $"select * from TransactionDetail where ID = '{CreditCardTransactionID}'";
                var transaction = AppSession.QueryFirstOrDefault<TransactionDetailEntity>(false, sql);

                if (transaction != null)
                {
                    listScript = new List<string>();

                    if (transaction.Amount != amountTendered + svcFeeAmount)
                        listScript.Add($"UPDATE dbo.TransactionDetail SET Amount = '{amountTendered + svcFeeAmount}' WHERE ID = '{CreditCardTransactionID}';");

                    if (transaction.Gratuity != gratuity)
                    {
                        listScript.Add($"UPDATE dbo.TransactionDetail SET Gratuity = '{gratuity}' WHERE ID = '{CreditCardTransactionID}';");
                        listScript.Add($"UPDATE dbo.TransactionDetail SET OriginalTip = '{gratuity}' WHERE ID = '{CreditCardTransactionID}';");
                    }

                    if (transaction.Total != amountTendered + svcFeeAmount + gratuity)
                    {
                        listScript.Add($"UPDATE dbo.TransactionDetail SET Total = '{amountTendered + svcFeeAmount + gratuity}' WHERE ID = '{CreditCardTransactionID}';");
                        listScript.Add($"UPDATE dbo.TransactionDetail SET OriginalAmount = '{amountTendered + svcFeeAmount + gratuity}' WHERE ID = '{CreditCardTransactionID}';");
                    }

                    if (transaction.SVCFee != svcFeeAmount)
                        listScript.Add($"UPDATE dbo.TransactionDetail SET SVCFee = '{svcFeeAmount}' WHERE ID = '{CreditCardTransactionID}';");

                    if (transaction.SVCFeeTaxAmount != svcFeeTaxAmount)
                        listScript.Add($"UPDATE dbo.TransactionDetail SET SVCFeeTaxAmount = '{svcFeeTaxAmount}' WHERE ID = '{CreditCardTransactionID}';");

                    if (transaction.SVCFeeForGratuity != svcFeeForGratuity)
                        listScript.Add($"UPDATE dbo.TransactionDetail SET SVCFeeForGratuity = '{svcFeeForGratuity}' WHERE ID = '{CreditCardTransactionID}';");

                    if (transaction.MerchantFee != merchantFee)
                        listScript.Add($"UPDATE dbo.TransactionDetail SET MerchantFee = '{merchantFee}' WHERE ID = '{CreditCardTransactionID}';");

                    if (transaction.MerchantFeeForTip != merchantFeeForTip)
                        listScript.Add($"UPDATE dbo.TransactionDetail SET MerchantFeeForTip = '{merchantFeeForTip}' WHERE ID = '{CreditCardTransactionID}';");

                    foreach (var script in listScript)
                    {
                        AppSession.ProcessNonQuery(false, script);
                    }
                }

            }

            return true;


        }

        // ---- Sync Payment Time ---------------------------------------------

        // Aligns the payment time with the ticket time: every payment line of the
        // ticket gets CreatedDate = SaleReceipts.DateCreated (the time the ticket
        // was finished). Returns the number of payment lines updated, or -1 when
        // the statement failed.
        public int SyncPaymentTime(string receiptId)
        {
            if (string.IsNullOrEmpty(receiptId)) return -1;

            string sql = $@"
                UPDATE a
                SET a.CreatedDate = b.DateCreated
                FROM dbo.SaleReceiptPaymentDetails a
                INNER JOIN dbo.SaleReceipts b ON a.SaleReceiptID = b.SaleReceiptID
                WHERE b.SaleReceiptID = '{receiptId}'";

            int rows = AppSession.ProcessNonQuery(true, sql);
            AppLogger.Info($"SyncPaymentTime({receiptId}): {rows} payment line(s) updated");
            return rows;
        }

        // Same sync keyed by the ticket number printed on the receipt. A number can
        // be reused across tickets, so every matching ticket is synced.
        public int SyncPaymentTimeByTicketNumber(string ticketNumber)
        {
            if (string.IsNullOrWhiteSpace(ticketNumber)) return -1;

            string number = ticketNumber.Trim().Replace("'", "''");

            string sql = $@"
                UPDATE a
                SET a.CreatedDate = b.DateCreated
                FROM dbo.SaleReceiptPaymentDetails a
                INNER JOIN dbo.SaleReceipts b ON a.SaleReceiptID = b.SaleReceiptID
                WHERE b.SaleReceiptNumber = '{number}'";

            int rows = AppSession.ProcessNonQuery(true, sql);
            AppLogger.Info($"SyncPaymentTimeByTicketNumber({number}): {rows} payment line(s) updated");
            return rows;
        }

        // ---- Refund Items -------------------------------------------------

        // Returns the lines of a ticket that are still refundable (not already
        // returned and not voided). Items and modifiers are distinguished the
        // same way the receipt does: PriceInitial blank or "P" => item.
        public List<RefundDetailItemEntity> GetRefundableItems(string receiptId)
        {
            var dt = AppSession.ProcessQuery(true, $@"
                SELECT *
                FROM dbo.SaleReceiptDetails
                WHERE SaleReceiptID = '{receiptId}'
                  AND ISNULL(ReturnCode, '') = ''
                  AND Status <> 2
                ORDER BY Range", "RefundItems");

            var items = new List<RefundDetailItemEntity>();
            if (dt == null) return items;

            foreach (DataRow row in dt.Rows)
            {
                var priceInitial = AppSession.GetString(row, "PriceInitial");
                bool isItem = string.IsNullOrWhiteSpace(priceInitial) || priceInitial == "P";

                items.Add(new RefundDetailItemEntity
                {
                    SaleReceiptDetailID = AppSession.GetString(row, "SaleReceiptDetailID"),
                    GroupID = AppSession.GetString(row, "GroupID"),
                    Description = AppSession.GetString(row, "Description"),
                    Price = AppSession.GetDecimal(row, "Price"),
                    Quantity = AppSession.GetDouble(row, "Quantity"),
                    ExtendedPrice = AppSession.GetDecimal(row, "ExtendedPrice"),
                    TaxAmount = AppSession.GetDecimal(row, "TaxAmount"),
                    IsModifier = !isItem
                });
            }

            AppLogger.Debug($"GetRefundableItems({receiptId}): {items.Count} refundable lines");
            return items;
        }

        // Marks the selected lines as returned, reduces the ticket / payment
        // totals, and (for credit-card payments) reverses the merchant charge
        // and rebuilds the batch. Ported from ThirdPartyTool.PartialRefundItems.
        public bool ProcessPartialRefund(string receiptId, long paymentDetailId,
            List<string> detailIds, decimal totalAmount, decimal taxAmount,
            decimal svcfeeAmount, decimal svcFeeTaxAmount, decimal cashDiscountAmount, decimal cashDiscountTaxAmount)
        {
            if (string.IsNullOrEmpty(receiptId) || detailIds == null || detailIds.Count == 0)
                return false;

            string sql = $"SELECT * FROM dbo.SaleReceipts where SaleReceiptID = '{receiptId}'";
            var sale = AppSession.QueryFirstOrDefault<SaleReceiptsEntity>(true, sql);

            sql = $"SELECT * FROM dbo.SaleReceiptPaymentDetails where SaleReceiptPaymentDetailID = '{paymentDetailId}'";
            var payment = AppSession.QueryFirstOrDefault<SaleReceiptPaymentDetailsEntity>(true, sql);

            if (sale == null || payment == null)
                return false;

            var newTotalAmount = sale.TotalAmount - totalAmount;
            var newTaxAmount = sale.TaxAmount - taxAmount;
            var newSVCFeePaidAmount = sale.SVCFeePaidAmount - svcfeeAmount;
            var newSVCFeeTaxPaidAmount = sale.SVCFeeTaxPaidAmount - svcFeeTaxAmount;
            var newCashDiscountPaidAmount = sale.CashDiscountPaidAmount - cashDiscountAmount;
            var newCashDiscountTaxPaidAmount = sale.CashDiscountTaxPaidAmount - cashDiscountTaxAmount;

            var newAmountTendered = payment.AmountTendered - totalAmount;
            var newPay_SVCFeeAmount = payment.SVCFeeAmount - svcfeeAmount;
            var newPay_SVCFeeTaxAmount = payment.SVCFeeTaxAmount - svcFeeTaxAmount;
            var newPay_CashDiscountAmount = payment.CashDiscountAmount - cashDiscountAmount;
            var newPay_CashDiscountTaxAmount = payment.CashDiscountTaxAmount - cashDiscountTaxAmount;
            var newPay_MerchantFee = Utility.CalculateMerchantFee(newAmountTendered + newPay_SVCFeeAmount + payment.Gratuity);

            string idList = string.Join(",",
                detailIds.Select(id => "'" + id.Replace("'", "''") + "'"));

            sql = $"UPDATE dbo.SaleReceiptDetails SET ReturnCode = '-113', Status = '2' WHERE SaleReceiptDetailID IN ({idList});" +
                $"UPDATE dbo.SaleReceiptDetailTaxDetails SET ReturnCode = '-113', Status = '2' WHERE SaleReceiptDetailID IN ({idList});" +
                $"UPDATE dbo.SaleReceipts SET TotalAmount = '{newTotalAmount}', PaidAmount = '{newTotalAmount}', TaxAmount = '{newTaxAmount}', SVCFeeAmount = '{newSVCFeePaidAmount}', SVCFeePaidAmount = '{newSVCFeePaidAmount}',CashDiscountAmount = '{newCashDiscountPaidAmount}', CashDiscountPaidAmount = '{newCashDiscountTaxPaidAmount}' WHERE SaleReceiptID = '{receiptId}';" +
                $"UPDATE dbo.SaleReceipts SET SVCFeeTaxAmount = '{newSVCFeeTaxPaidAmount}', SVCFeeTaxPaidAmount = '{newSVCFeeTaxPaidAmount}', CashDiscountTaxAmount = '{newCashDiscountTaxPaidAmount}', CashDiscountTaxPaidAmount = '{newCashDiscountTaxPaidAmount}' WHERE SaleReceiptID = '{receiptId}';" +
                $"UPDATE sr SET sr.TotalDiscountAmount = d.DiscTotal, sr.GlobalDiscountAmount = d.DiscTotal FROM dbo.SaleReceipts sr CROSS APPLY ( SELECT ISNULL(SUM( ISNULL(ItemDiscountValue, 0) + ISNULL(TotalSaleDiscountAmount, 0) + ISNULL(StoreDiscountValue, 0) + ISNULL(DiscountAmount, 0) + ISNULL(TotalSaleDiscountAmountByCustomer, 0) + ISNULL(ItemDiscountByPriceLevelAmount, 0)), 0) AS DiscTotal FROM dbo.SaleReceiptDetails WHERE SaleReceiptID = '{receiptId}' AND Status <> 2 AND ISNULL(IsCompItem, 0) = 0 ) d WHERE sr.SaleReceiptID = '{receiptId}';" +
                $"UPDATE dbo.SaleReceiptPaymentDetails SET AmountTendered = '{newAmountTendered}', SVCFeeAmount = '{newPay_SVCFeeAmount}', CashDiscountAmount = '{newPay_CashDiscountAmount}', MerchantFee = '{newPay_MerchantFee}' WHERE SaleReceiptPaymentDetailID = '{paymentDetailId}';" +
                $"UPDATE dbo.SaleReceiptPaymentDetails SET SVCFeeTaxAmount = '{newPay_SVCFeeTaxAmount}', CashDiscountTaxAmount = '{newPay_CashDiscountTaxAmount}' WHERE SaleReceiptPaymentDetailID = '{paymentDetailId}';";

            if (AppSession.ProcessNonQuery(true, sql) < 0)
                return false;

            if (!string.IsNullOrWhiteSpace(payment.CreditCardTransactionID))
            {
                sql = $"UPDATE Merchant.dbo.TransactionDetail SET Amount = '{newAmountTendered + newPay_SVCFeeAmount}', Total = '{newAmountTendered + newPay_SVCFeeAmount + payment.Gratuity}', SVCFee = '{newPay_SVCFeeAmount}',MerchantFee = '{newPay_MerchantFee}' WHERE ID = '{payment.CreditCardTransactionID}';" +
                    $"UPDATE Merchant.dbo.TransactionDetail SET SVCFeeTaxAmount = '{newPay_SVCFeeTaxAmount}' WHERE ID = '{payment.CreditCardTransactionID}';";

                AppSession.ProcessNonQuery(false, sql);
            }

            // Merchant side — only for the credit-card payment that was refunded.
            RefundMerchantTransaction(paymentDetailId);
            return true;
        }

        private void RefundMerchantTransaction(long paymentDetailId)
        {
            var pdt = AppSession.ProcessQuery(true, $@"
                SELECT PaymentMethodID, CreditCardTransactionType, CreditCardTransactionID
                FROM dbo.SaleReceiptPaymentDetails
                WHERE SaleReceiptPaymentDetailID = {paymentDetailId}", "PaymentDetail");

            if (pdt == null || pdt.Rows.Count == 0) return;

            var row = pdt.Rows[0];
            if (AppSession.GetInt(row, "PaymentMethodID") != 2) return;
            if (!AppSession.GetString(row, "CreditCardTransactionType").Contains("Sale")) return;

            var transactionId = AppSession.GetString(row, "CreditCardTransactionID");
            if (string.IsNullOrWhiteSpace(transactionId)) return;
            var safeTranId = transactionId.Replace("'", "''");

            var tdt = AppSession.ProcessQuery(false, $@"
                SELECT BatchUnique FROM dbo.TransactionDetail WHERE ID = '{safeTranId}'", "Tran");
            if (tdt == null || tdt.Rows.Count == 0) return;

            var batchUnique = AppSession.GetString(tdt.Rows[0], "BatchUnique");

            if (!string.IsNullOrWhiteSpace(batchUnique))
                RecalculateMerchantBatch(batchUnique);
        }

        // Rebuilds Batch aggregates after a refund. Ported from
        // ThirdPartyTool.UpdateBatchByBatchUnique.
        private void RecalculateMerchantBatch(string batchUnique)
        {
            var safe = batchUnique.Replace("'", "''");

            var countDt = AppSession.ProcessQuery(false,
                $"SELECT COUNT(ID) AS Cnt FROM dbo.TransactionDetail WHERE BatchUnique = '{safe}'", "Cnt");
            int cnt = countDt?.Rows.Count > 0 ? AppSession.GetInt(countDt.Rows[0], "Cnt") : 0;

            if (cnt == 0)
            {
                AppSession.ProcessNonQuery(false, $"DELETE FROM dbo.Batch WHERE BatchUnique = '{safe}'");
                return;
            }

            string aggSql = $@"
                SELECT
                    SUM(CASE WHEN (TranType = 'Sale' OR TranType = 'PreAuthCapture') AND TranStatus = 'ACTIVE' THEN Total ELSE 0 END)
                      - SUM(CASE WHEN TranType = 'Return' THEN Total ELSE 0 END) AS Total,
                    SUM(CASE WHEN (TranType = 'Sale' OR TranType = 'PreAuthCapture') AND TranStatus = 'ACTIVE' THEN SVCFeeForGratuity ELSE 0 END) AS TotalSVCFeeForTip,
                    SUM(CASE WHEN (TranType = 'Sale' OR TranType = 'PreAuthCapture') AND TranStatus = 'ACTIVE' THEN Amount - SVCFee ELSE 0 END) AS TotalSales,
                    SUM(CASE WHEN (TranType = 'Sale' OR TranType = 'PreAuthCapture') AND TranStatus = 'ACTIVE' THEN Gratuity ELSE 0 END) AS TotalTips,
                    SUM(CASE WHEN (TranType = 'Sale' OR TranType = 'PreAuthCapture') AND TranStatus = 'ACTIVE' THEN SVCFee ELSE 0 END)
                      - SUM(CASE WHEN TranType = 'Return' THEN SVCFee ELSE 0 END) AS TotalSVCFeeAmount,
                    SUM(CASE WHEN (TranType = 'Sale' OR TranType = 'PreAuthCapture') AND TranStatus = 'ACTIVE' THEN Total - MerchantFee ELSE 0 END)
                      - SUM(CASE WHEN TranType = 'Return' THEN Total ELSE 0 END) AS DepositAmount,
                    SUM(CASE WHEN (TranType = 'Sale' OR TranType = 'PreAuthCapture') AND TranStatus = 'ACTIVE' THEN MerchantFee ELSE 0 END) AS MerchantFeeAmount,
                    COUNT(ID) AS TicketCount
                FROM dbo.TransactionDetail
                WHERE BatchUnique = '{safe}'";

            var agg = AppSession.ProcessQuery(false, aggSql, "Batch");
            if (agg == null || agg.Rows.Count == 0) return;

            var r = agg.Rows[0];
            var inv = CultureInfo.InvariantCulture;
            string total = AppSession.GetDecimal(r, "Total").ToString(inv);
            string totalSvcTip = AppSession.GetDecimal(r, "TotalSVCFeeForTip").ToString(inv);
            string totalSales = AppSession.GetDecimal(r, "TotalSales").ToString(inv);
            string totalTips = AppSession.GetDecimal(r, "TotalTips").ToString(inv);
            string totalSvc = AppSession.GetDecimal(r, "TotalSVCFeeAmount").ToString(inv);
            string deposit = AppSession.GetDecimal(r, "DepositAmount").ToString(inv);
            string merchantFee = AppSession.GetDecimal(r, "MerchantFeeAmount").ToString(inv);
            int ticketCount = AppSession.GetInt(r, "TicketCount");

            AppSession.ProcessNonQuery(false, $@"
                UPDATE dbo.Batch SET
                    Total = {total},
                    TotalSVCFeeForTip = {totalSvcTip},
                    TotalSales = {totalSales},
                    TotalTips = {totalTips},
                    TotalSVCFeeAmount = {totalSvc},
                    DepositAmount = {deposit},
                    MerchantFeeAmount = {merchantFee},
                    Quantity = {ticketCount}
                WHERE BatchUnique = '{safe}'");

            AppSession.ProcessNonQuery(false,
                $"UPDATE dbo.TransactionDetail SET BatchTotal = {total} WHERE BatchUnique = '{safe}'");
        }

        // ---- Edit Items ---------------------------------------------------

        // Active (Status <> 2) lines of a ticket, for the Edit Items form.
        public List<RefundDetailItemEntity> GetEditableItems(string receiptId)
        {
            var dt = AppSession.ProcessQuery(true, $@"
                SELECT *
                FROM dbo.SaleReceiptDetails
                WHERE SaleReceiptID = '{receiptId}' AND Status <> 2
                ORDER BY Range", "EditItems");

            var items = new List<RefundDetailItemEntity>();
            if (dt == null) return items;

            foreach (DataRow row in dt.Rows)
            {
                var priceInitial = AppSession.GetString(row, "PriceInitial");
                bool isItem = string.IsNullOrWhiteSpace(priceInitial) || priceInitial == "P";
                items.Add(new RefundDetailItemEntity
                {
                    SaleReceiptDetailID = AppSession.GetString(row, "SaleReceiptDetailID"),
                    GroupID = AppSession.GetString(row, "GroupID"),
                    Description = AppSession.GetString(row, "Description"),
                    Price = AppSession.GetDecimal(row, "Price"),
                    Quantity = AppSession.GetDouble(row, "Quantity"),
                    ExtendedPrice = AppSession.GetDecimal(row, "ExtendedPrice"),
                    TaxAmount = AppSession.GetDecimal(row, "TaxAmount"),
                    TaxRate = AppSession.GetDecimal(row, "TaxRate"),
                    ItemDiscountValue = AppSession.GetDecimal(row, "ItemDiscountValue"),
                    TotalSaleDiscountAmount = AppSession.GetDecimal(row, "TotalSaleDiscountAmount"),
                    StoreDiscountValue = AppSession.GetDecimal(row, "StoreDiscountValue"),
                    DiscountAmount = AppSession.GetDecimal(row, "DiscountAmount"),
                    TotalSaleDiscountAmountByCustomer = AppSession.GetDecimal(row, "TotalSaleDiscountAmountByCustomer"),
                    ItemDiscountByPriceLevelAmount = AppSession.GetDecimal(row, "ItemDiscountByPriceLevelAmount"),
                    IsModifier = !isItem
                });
            }
            return items;
        }

        // Edits one line's name/price/tax/status, then recomputes the ticket's
        // item-derived header totals (TotalAmount, TaxAmount, TotalDiscountAmount,
        // GlobalDiscountAmount). Payments are intentionally left untouched.
        public bool EditTicketItem(string receiptId, string detailId, string name,
            decimal price, decimal tax, ItemEditStatus status)
        {
            if (string.IsNullOrEmpty(receiptId) || string.IsNullOrEmpty(detailId)) return false;

            var inv = CultureInfo.InvariantCulture;
            string rid = receiptId.Replace("'", "''");
            string id = detailId.Replace("'", "''");
            string safeName = (name ?? "").Replace("'", "''");
            string p = price.ToString(inv);
            string t = tax.ToString(inv);

            int statusVal;
            string detailReturnCode;  // RHS for SaleReceiptDetails.ReturnCode
            bool completed = status == ItemEditStatus.Completed;
            switch (status)
            {
                case ItemEditStatus.Refund: statusVal = 2; detailReturnCode = "'-113'"; break;
                case ItemEditStatus.Cancel: statusVal = 2; detailReturnCode = "ReturnCode"; break; // leave as-is
                default: statusVal = 0; detailReturnCode = "NULL"; break;
            }

            const string discountExpr =
                "(ISNULL(ItemDiscountValue,0)+ISNULL(TotalSaleDiscountAmount,0)+ISNULL(StoreDiscountValue,0)"
                + "+ISNULL(DiscountAmount,0)+ISNULL(TotalSaleDiscountAmountByCustomer,0)+ISNULL(ItemDiscountByPriceLevelAmount,0))";

            // Keep the per-tax-code breakdown consistent when there is a single row.
            string taxMirror = completed
                ? $@"IF (SELECT COUNT(*) FROM dbo.SaleReceiptDetailTaxDetails WHERE SaleReceiptDetailID = '{id}') = 1
                        UPDATE dbo.SaleReceiptDetailTaxDetails SET TaxAmount = {t} WHERE SaleReceiptDetailID = '{id}';"
                : "";

            string sql = $@"
                SET XACT_ABORT ON;
                BEGIN TRANSACTION;

                UPDATE dbo.SaleReceiptDetails SET
                    Description   = N'{safeName}',
                    Price         = {p},
                    TaxAmount     = {t},
                    ExtendedPrice = ({p} * Quantity) - {discountExpr},
                    Status        = {statusVal},
                    ReturnCode    = {detailReturnCode}
                WHERE SaleReceiptDetailID = '{id}';

                IF OBJECT_ID('dbo.SaleReceiptDetailTaxDetails', 'U') IS NOT NULL
                BEGIN
                    UPDATE dbo.SaleReceiptDetailTaxDetails
                        SET Status = {statusVal}, ReturnCode = {detailReturnCode}
                        WHERE SaleReceiptDetailID = '{id}';
                    {taxMirror}
                END

                UPDATE sr SET
                    sr.TaxAmount            = d.ItemTax + ISNULL(sr.ServiceChargeTaxAmount, 0),
                    sr.TotalAmount          = d.ItemSubtotal + ISNULL(sr.SalesOrderFee, 0) + d.ItemTax + ISNULL(sr.ServiceChargeTaxAmount, 0),
                    sr.TotalDiscountAmount  = d.DiscountSum,
                    sr.GlobalDiscountAmount = d.DiscountSum
                FROM dbo.SaleReceipts sr
                CROSS APPLY (
                    SELECT
                        ISNULL(SUM(ExtendedPrice), 0) AS ItemSubtotal,
                        ISNULL(SUM(TaxAmount), 0) AS ItemTax,
                        ISNULL(SUM(
                            ISNULL(ItemDiscountValue,0)+ISNULL(TotalSaleDiscountAmount,0)+ISNULL(StoreDiscountValue,0)
                            +ISNULL(DiscountAmount,0)+ISNULL(TotalSaleDiscountAmountByCustomer,0)+ISNULL(ItemDiscountByPriceLevelAmount,0)), 0) AS DiscountSum
                    FROM dbo.SaleReceiptDetails
                    WHERE SaleReceiptID = '{rid}' AND Status <> 2 AND ISNULL(IsCompItem, 0) = 0
                ) d
                WHERE sr.SaleReceiptID = '{rid}';

                COMMIT;";

            return AppSession.ProcessNonQuery(true, sql) >= 0;
        }

        // Edits every editable field of one line from the Item Detail form (price,
        // quantity, tax amount, name, status and the six discount amounts).
        // ExtendedPrice = gross (Price * Quantity) - total discount.
        // Each group of columns is sent as its own small script (instead of one combined
        // transaction) and only when its values really differ from what is stored, so an
        // unchanged group produces no write at all.
        public bool EditTicketItemDetail(string receiptId, ItemEditDetailEntity edit)
        {
            if (string.IsNullOrEmpty(receiptId) || edit == null || string.IsNullOrEmpty(edit.SaleReceiptDetailID))
                return false;

            var inv = CultureInfo.InvariantCulture;
            string rid = receiptId.Replace("'", "''");
            string id = edit.SaleReceiptDetailID.Replace("'", "''");
            string safeName = (edit.Name ?? "").Replace("'", "''");

            decimal qty = (decimal)edit.Quantity;
            decimal gross = edit.Price * qty;
            decimal totalDiscount = edit.ItemDiscount + edit.TicketDiscount + edit.StoreDiscount
                + edit.CategoryDiscount + edit.CustomerDiscount + edit.CustomerLevelDiscount;
            decimal extendedPrice = gross - totalDiscount;
            decimal taxAmount = edit.TaxAmount; // tax entered as an amount, stored directly

            int statusVal;
            int isComp;
            string detailReturnCode;
            bool completed = edit.Status == ItemEditStatus.Completed;
            string reasonCode = edit.ReasonCodeId.ToString(inv);
            switch (edit.Status)
            {
                case ItemEditStatus.Refund: statusVal = 2; isComp = 0; detailReturnCode = reasonCode; break;
                case ItemEditStatus.Cancel: statusVal = 2; isComp = 0; detailReturnCode = reasonCode; break;
                case ItemEditStatus.Comp: statusVal = 0; isComp = 1; detailReturnCode = reasonCode; break;
                default: statusVal = 0; isComp = 0; detailReturnCode = "NULL"; break; // Completed
            }

            // Values currently stored, used to skip the scripts whose columns would not change.
            var curDt = AppSession.ProcessQuery(true,
                $@"SELECT Description, Price, Quantity, ExtendedPrice, TaxAmount,
                          ItemDiscountValue, TotalSaleDiscountAmount, StoreDiscountValue, DiscountAmount,
                          TotalSaleDiscountAmountByCustomer, ItemDiscountByPriceLevelAmount,
                          IsCompItem, Status, ReturnCode
                   FROM dbo.SaleReceiptDetails
                   WHERE SaleReceiptDetailID = '{id}'", "ItemDetail");

            if (curDt == null || curDt.Rows.Count == 0)
                return false; // the line is gone, nothing to edit

            var cur = curDt.Rows[0];

            // ReturnCode is nullable, so compare the text we would write ("NULL" or the reason code).
            string curReturnCode = cur.IsNull("ReturnCode")
                ? "NULL"
                : AppSession.GetInt(cur, "ReturnCode").ToString(inv);

            // 1. Name, price, quantity, extended price and tax amount.
            bool pricingChanged =
                AppSession.GetString(cur, "Description") != (edit.Name ?? "")
                || AppSession.GetDecimal(cur, "Price") != edit.Price
                || AppSession.GetDecimal(cur, "Quantity") != qty
                || AppSession.GetDecimal(cur, "ExtendedPrice") != extendedPrice
                || AppSession.GetDecimal(cur, "TaxAmount") != taxAmount;

            if (pricingChanged)
            {
                string sql = $@"
                    UPDATE dbo.SaleReceiptDetails SET
                        Description   = N'{safeName}',
                        Price         = {edit.Price.ToString(inv)},
                        Quantity      = {qty.ToString(inv)},
                        ExtendedPrice = {extendedPrice.ToString(inv)},
                        TaxAmount     = {taxAmount.ToString(inv)}
                    WHERE SaleReceiptDetailID = '{id}'";

                if (AppSession.ProcessNonQuery(true, sql) < 0)
                    return false;
            }

            // 2. The six discount amounts (rate/percent columns are left as-is).
            bool discountsChanged =
                AppSession.GetDecimal(cur, "ItemDiscountValue") != edit.ItemDiscount
                || AppSession.GetDecimal(cur, "TotalSaleDiscountAmount") != edit.TicketDiscount
                || AppSession.GetDecimal(cur, "StoreDiscountValue") != edit.StoreDiscount
                || AppSession.GetDecimal(cur, "DiscountAmount") != edit.CategoryDiscount
                || AppSession.GetDecimal(cur, "TotalSaleDiscountAmountByCustomer") != edit.CustomerDiscount
                || AppSession.GetDecimal(cur, "ItemDiscountByPriceLevelAmount") != edit.CustomerLevelDiscount;

            if (discountsChanged)
            {
                string sql = $@"
                    UPDATE dbo.SaleReceiptDetails SET
                        ItemDiscountValue                 = {edit.ItemDiscount.ToString(inv)},
                        TotalSaleDiscountAmount           = {edit.TicketDiscount.ToString(inv)},
                        StoreDiscountValue                = {edit.StoreDiscount.ToString(inv)},
                        DiscountAmount                    = {edit.CategoryDiscount.ToString(inv)},
                        TotalSaleDiscountAmountByCustomer = {edit.CustomerDiscount.ToString(inv)},
                        ItemDiscountByPriceLevelAmount    = {edit.CustomerLevelDiscount.ToString(inv)}
                    WHERE SaleReceiptDetailID = '{id}'";

                AppSession.ProcessNonQuery(true, sql);
            }

            // 3. Status, comp flag and reason code on the line.
            bool statusChanged =
                (AppSession.GetBool(cur, "IsCompItem") ? 1 : 0) != isComp
                || AppSession.GetInt(cur, "Status") != statusVal
                || curReturnCode != detailReturnCode;

            if (statusChanged)
            {
                string sql = $@"
                    UPDATE dbo.SaleReceiptDetails SET
                        IsCompItem = {isComp},
                        Status     = {statusVal},
                        ReturnCode = {detailReturnCode}
                    WHERE SaleReceiptDetailID = '{id}'";

                AppSession.ProcessNonQuery(true, sql);
            }

            // 4. Refresh the final* snapshot columns of the ticket, the way the POS server
            // does after a receipt changes, so reports built on them stay in sync.
            string transactionStatus = Convert.ToString(AppSession.ProcessQuerySingleValue(true,
                $"SELECT TransactionStatusID FROM dbo.SaleReceipts WHERE SaleReceiptID = '{rid}'"));
            UpdateFinalSaleReceipt(rid, transactionStatus);

            return true;
        }

        // Port of SaleReceiptHelperABS.UpdateFinalSaleReceipt (POS server).
        // Recomputes the final* columns of SaleReceiptDetails from the live values of the
        // ticket: each line is aggregated over its whole modifier group (same GroupID),
        // ignoring voided/returned (Status 2, 98) and comp lines. When the ticket is
        // finished, SaleReceipts.FinalGratuity is refreshed too
        // (SaleReceipt_New.UpdateFinalDataForSaleReceiptsInSession).
        // saleReceiptID must already be SQL-escaped. Databases without the final* columns
        // are simply left alone.
        private void UpdateFinalSaleReceipt(string saleReceiptID, string transactionStatus = "")
        {
            try
            {
                // Final value per line, aggregated over the group the line belongs to.
                var sql = $@"IF COL_LENGTH('dbo.SaleReceiptDetails', 'finalQuantity') IS NOT NULL
                            BEGIN
                            ;WITH FinalSaleReceiptDetails AS (
                                SELECT
                                    d.DepartmentID AS ID,
                                    d.DepartmentName AS Name,
                                    sdi.SaleReceiptDetailID,
                                    SUM(v.Quantity) AS FinalQuantity,
                                    SUM(v.DiscountAmount) AS FinalDiscountAmount,
                                    SUM(v.CashDiscount) AS FinalCashDiscountAmount,
                                    SUM(v.CoinDiscount) AS FinalCoinDiscountAmount,
                                    SUM(v.TaxAmount) AS FinalTaxAmount,
                                    SUM(v.TotalSales) AS FinalTotalSales,
                                    SUM(v.GrossSales) AS FinalGrossSales,
                                    SUM(v.NetSales) AS FinalNetSales,
                                    SUM(v.NetSalesTax) AS FinalNetSalesTax
                                FROM
                                     SaleReceiptDetails sdi
                                                    JOIN SaleReceipts s ON sdi.SaleReceiptID = s.SaleReceiptID
                                                    JOIN SaleReceiptDetails sda ON sdi.SaleReceiptID = sda.SaleReceiptID
                                                        AND sda.GroupID = sdi.GroupID
                                                        AND sda.Status NOT IN (2, 98)
                                                        AND sda.IsCompItem = 0
                                                    JOIN Items i ON sdi.ItemID = i.ItemID
                                                    RIGHT JOIN Departments d ON i.DeptDeptID = d.DepartmentID
                                                        AND d.DepartmentID != -4
                                                        AND d.[Level] = 1
                                    OUTER APPLY (
                                        SELECT
                                            CASE
                                                WHEN (sda.PriceInitial IS NULL OR sda.PriceInitial = 'P') THEN sdi.Quantity
                                                ELSE 0
                                            END AS Quantity,
                                            (sda.ItemDiscountValue + sda.TotalSaleDiscountAmount + sda.StoreDiscountValue
                                             + sda.DiscountAmount + sda.TotalSaleDiscountAmountByCustomer
                                             + sda.ItemDiscountByPriceLevelAmount) AS DiscountAmount,
                                            sda.CashDiscountPerItem AS CashDiscount,
                                            sda.CoinDiscountPerItem AS CoinDiscount,
                                            sda.TaxAmount AS TaxAmount,
                                            (sda.ExtendedPrice + sda.TaxAmount - sda.CashDiscountPerItem - sda.CoinDiscountPerItem) AS TotalSales,
                                            (sda.ExtendedPrice + sda.ItemDiscountValue + sda.TotalSaleDiscountAmount
                                             + sda.StoreDiscountValue + sda.DiscountAmount + sda.TotalSaleDiscountAmountByCustomer
                                             + sda.ItemDiscountByPriceLevelAmount) AS GrossSales,
                                            (sda.ExtendedPrice - CASE
                                                                    WHEN (s.CashDiscountAmount > s.SVCFeeAmount) THEN sda.CashDiscountPerItem
                                                                    ELSE 0
                                                                 END - sda.CoinDiscountPerItem) AS NetSales,
                                            (sda.TaxAmount + sda.ExtendedPrice - CASE
                                                                                    WHEN (s.CashDiscountAmount > s.SVCFeeAmount) THEN sda.CashDiscountPerItem
                                                                                    ELSE 0
                                                                                 END - sda.CoinDiscountPerItem) AS NetSalesTax
                                    ) v
                                WHERE s.SaleReceiptID = '{saleReceiptID}'
                                GROUP BY d.DepartmentID, d.DepartmentName, sdi.SaleReceiptDetailID
                            )";

                // Write the aggregated values back to SaleReceiptDetails.
                sql += @"
                            UPDATE srd
                            SET
                                srd.finalQuantity = fsd.FinalQuantity,
                                srd.finalDiscountAmount = fsd.FinalDiscountAmount,
                                srd.finalCashDiscount = fsd.FinalCashDiscountAmount,
                                srd.finalCoinDiscount = fsd.FinalCoinDiscountAmount,
                                srd.finalTaxAmount = fsd.FinalTaxAmount,
                                srd.finalTotalSales = fsd.FinalTotalSales,
                                srd.finalGrossSales = fsd.FinalGrossSales,
                                srd.finalNetSales = fsd.FinalNetSales,
                                srd.finalNetSalesTax = fsd.FinalNetSalesTax
                            FROM dbo.SaleReceiptDetails srd
                            INNER JOIN FinalSaleReceiptDetails fsd ON srd.SaleReceiptDetailID = fsd.SaleReceiptDetailID;
                            END";

                var result = AppSession.ProcessNonQuery(true, sql);
                if (result < 0)
                    AppLogger.Error($"UpdateFinalSaleReceipt failed for SaleReceiptID: {saleReceiptID}");
                else
                    AppLogger.Info($"UpdateFinalSaleReceipt: Update final values for SaleReceiptID = {saleReceiptID} successfully.");

                if (transactionStatus == "FIN")
                {
                    AppSession.ProcessNonQuery(true, $@"
                        IF COL_LENGTH('dbo.SaleReceipts', 'FinalGratuity') IS NOT NULL
                        BEGIN
                            ;WITH g AS (
                                SELECT SaleReceiptID, SUM(Gratuity) AS TotalGratuity
                                FROM dbo.SaleReceiptPaymentDetails GROUP BY SaleReceiptID
                            )
                            UPDATE sr SET sr.FinalGratuity = g.TotalGratuity
                            FROM dbo.SaleReceipts sr
                            JOIN g ON g.SaleReceiptID = sr.SaleReceiptID
                            WHERE sr.TransactionStatusID = 'FIN' AND sr.SaleReceiptID = '{saleReceiptID}';
                        END");
                }
            }
            catch (Exception ex)
            {
                AppLogger.Error("UpdateFinalSaleReceipt failed", ex);
            }
        }

        // Reason codes for the Item Detail status combobox, filtered by ReasonType
        // (Cancel/Comp -> 3, Refund -> 13). Ordered by name for display.
        public List<ReasonCodeEntity> GetReasonCodes(int reasonType)
        {
            var dt = AppSession.ProcessQuery(true,
                $"SELECT ReasonCodeID, ReasonCodeName FROM dbo.ReasonCodes WHERE ReasonType = '{reasonType}' ORDER BY ReasonCodeName",
                "Reasons");

            var list = new List<ReasonCodeEntity>();
            if (dt != null)
                foreach (DataRow r in dt.Rows)
                    list.Add(new ReasonCodeEntity
                    {
                        ReasonCodeID = AppSession.GetInt(r, "ReasonCodeID"),
                        ReasonCodeName = AppSession.GetString(r, "ReasonCodeName")
                    });
            return list;
        }

        // ---- Add Item -----------------------------------------------------

        // Items that can be added to a ticket (special department -2), per the
        // Add Item picker. Excludes the placeholder item and deleted items.
        public List<AddableItemEntity> GetAddableItems()
        {
            var dt = AppSession.ProcessQuery(true,
                @"SELECT * FROM dbo.Items
                  WHERE DeptDeptID = '-2'
                    AND ItemID <> '22222222-2222-2222-2222-222222222222'
                    AND IsDeleted = '0'
                  ORDER BY ItemName", "Items");

            var list = new List<AddableItemEntity>();
            if (dt != null)
                foreach (DataRow r in dt.Rows)
                    list.Add(new AddableItemEntity
                    {
                        ItemID = AppSession.GetString(r, "ItemID"),
                        ItemName = AppSession.GetString(r, "ItemName"),
                        LookupCode = AppSession.GetString(r, "LookupCode"),
                        RegularPrice = AppSession.GetDecimal(r, "RegularPrice"),
                        TaxCodeID = AppSession.GetString(r, "TaxCodeID"),
                        Taxable = AppSession.GetBool(r, "Taxable")
                    });
            return list;
        }

        // Inserts a new line into a ticket. Because SaleReceiptDetails has many
        // NOT NULL columns, an existing line of the same ticket is used as a
        // template (its values fill every column) and only the item-identity,
        // pricing, status and discount columns are overridden/reset. Tax and
        // discounts start at 0. The ticket header totals are then recomputed.
        public bool AddTicketItem(string receiptId, string itemId, string name,
            decimal price, double quantity, string lookupCode, string taxCodeId, bool taxable)
        {
            if (string.IsNullOrEmpty(receiptId) || string.IsNullOrEmpty(itemId))
                return false;

            var inv = CultureInfo.InvariantCulture;
            string rid = receiptId.Replace("'", "''");

            var dt = AppSession.ProcessQuery(true,
                $"SELECT TOP 1 * FROM dbo.SaleReceiptDetails WHERE SaleReceiptID = '{rid}' ORDER BY Range", "Tmpl");
            if (dt == null || dt.Columns.Count == 0 || dt.Rows.Count == 0)
                return false; // need a template row to supply valid NOT NULL defaults

            var tmpl = dt.Rows[0];
            string newId = Guid.NewGuid().ToString();
            decimal qtyDec = (decimal)quantity;
            decimal extended = price * qtyDec;

            int nextRange = 1;
            var rdt = AppSession.ProcessQuery(true,
                $"SELECT ISNULL(MAX(Range),0)+1 AS N FROM dbo.SaleReceiptDetails WHERE SaleReceiptID = '{rid}'", "R");
            if (rdt?.Rows.Count > 0) nextRange = AppSession.GetInt(rdt.Rows[0], "N");

            string p = price.ToString(inv);
            string q = qtyDec.ToString(inv);
            string ext = extended.ToString(inv);

            // Columns whose value we set explicitly; everything else inherits from
            // the template row. Keys are matched case-insensitively; keys that do
            // not exist in the table are simply ignored.
            var ov = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                ["SaleReceiptDetailID"] = $"'{newId}'",
                ["SaleReceiptID"] = $"'{rid}'",
                ["ItemID"] = $"'{itemId.Replace("'", "''")}'",
                ["Description"] = $"N'{(name ?? "").Replace("'", "''")}'",
                ["ShortDescription"] = $"N'{(name ?? "").Replace("'", "''")}'",
                ["Description2"] = "NULL",
                ["Description3"] = "NULL",
                ["DescriptionChinese"] = "NULL",
                ["Lookup_Code"] = string.IsNullOrEmpty(lookupCode) ? "NULL" : $"N'{lookupCode.Replace("'", "''")}'",
                ["Price"] = p,
                ["RegularPrice"] = p,
                ["OpenPriceValue"] = "0",
                ["Quantity"] = q,
                ["FinalQuantity"] = q,
                ["QuantitySold"] = q,
                ["QtyPerItem"] = "1",
                ["ExtendedPrice"] = ext,
                ["Range"] = nextRange.ToString(inv),
                ["GroupID"] = $"'{newId}'",
                ["PriceInitial"] = "NULL",
                ["Status"] = "0",
                ["IsCompItem"] = "0",
                ["ReturnCode"] = "NULL",
                ["ReasonComment"] = "NULL",
                ["RefSaleReceiptDetailID"] = "NULL",
                ["LastModifyShiftID"] = "NULL",
                ["TaxAmount"] = "0",
                ["TaxRate"] = "0",
                ["TaxCodeID"] = string.IsNullOrEmpty(taxCodeId) ? "NULL" : $"'{taxCodeId.Replace("'", "''")}'",
                ["Taxable"] = taxable ? "1" : "0",
                // discounts -> 0 (the six the header recompute reads, plus rates/percents)
                ["DiscountPercent"] = "0",
                ["DiscountAmount"] = "0",
                ["ItemdiscountRate"] = "0",
                ["ItemDiscountValue"] = "0",
                ["StoreDiscountRate"] = "0",
                ["StoreDiscountValue"] = "0",
                ["TotalSaleDiscountAmount"] = "0",
                ["TotalSaleDiscountPercent"] = "0",
                ["TotalSaleDiscountAmountBySystem"] = "0",
                ["TotalSaleDiscountPercentBySystem"] = "0",
                ["TotalSaleDiscountAmountByCustomer"] = "0",
                ["TotalSaleDiscountPercentByCustomer"] = "0",
                ["ItemDiscountByPriceLevelAmount"] = "0",
                ["CashDiscountPerItem"] = "0",
                ["CoinDiscountPerItem"] = "0",
                ["FinalDiscountAmount"] = "0",
                ["FinalCashDiscount"] = "0",
                ["FinalCoinDiscount"] = "0",
                ["FinalTaxAmount"] = "0",
                ["FinalTotalSales"] = ext,
                ["FinalGrossSales"] = ext,
                ["FinalNetSales"] = ext,
                ["FinalNetSalesTax"] = ext,
            };

            var cols = new StringBuilder();
            var vals = new StringBuilder();
            foreach (DataColumn c in dt.Columns)
            {
                cols.Append("[").Append(c.ColumnName).Append("],");
                vals.Append(ov.TryGetValue(c.ColumnName, out var v) ? v : SqlValue(c, tmpl[c])).Append(",");
            }
            cols.Length--; // drop trailing comma
            vals.Length--;

            string sql = $@"
                SET XACT_ABORT ON;
                BEGIN TRANSACTION;

                INSERT INTO dbo.SaleReceiptDetails ({cols}) VALUES ({vals});

                UPDATE sr SET
                    sr.TaxAmount            = d.ItemTax + ISNULL(sr.ServiceChargeTaxAmount, 0),
                    sr.TotalAmount          = d.ItemSubtotal + ISNULL(sr.SalesOrderFee, 0) + d.ItemTax + ISNULL(sr.ServiceChargeTaxAmount, 0),
                    sr.TotalDiscountAmount  = d.DiscountSum,
                    sr.GlobalDiscountAmount = d.DiscountSum
                FROM dbo.SaleReceipts sr
                CROSS APPLY (
                    SELECT
                        ISNULL(SUM(ExtendedPrice), 0) AS ItemSubtotal,
                        ISNULL(SUM(TaxAmount), 0) AS ItemTax,
                        ISNULL(SUM(
                            ISNULL(ItemDiscountValue,0)+ISNULL(TotalSaleDiscountAmount,0)+ISNULL(StoreDiscountValue,0)
                            +ISNULL(DiscountAmount,0)+ISNULL(TotalSaleDiscountAmountByCustomer,0)+ISNULL(ItemDiscountByPriceLevelAmount,0)), 0) AS DiscountSum
                    FROM dbo.SaleReceiptDetails
                    WHERE SaleReceiptID = '{rid}' AND Status <> 2 AND ISNULL(IsCompItem, 0) = 0
                ) d
                WHERE sr.SaleReceiptID = '{rid}';

                COMMIT;";

            return AppSession.ProcessNonQuery(true, sql) >= 0;
        }

        // Formats a DataRow value as a SQL literal for the INSERT built in AddTicketItem.
        private static string SqlValue(DataColumn col, object val)
        {
            if (val == null || val == DBNull.Value) return "NULL";
            var t = col.DataType;
            if (t == typeof(string)) return "N'" + val.ToString().Replace("'", "''") + "'";
            if (t == typeof(bool)) return ((bool)val) ? "1" : "0";
            if (t == typeof(DateTime)) return "'" + ((DateTime)val).ToString("yyyy-MM-dd HH:mm:ss") + "'";
            if (t == typeof(Guid)) return "'" + val + "'";
            return Convert.ToString(val, CultureInfo.InvariantCulture);
        }

        // ---- Close / Void / Cancel ----------------------------------------

        // Closes a ticket: status -> CLO, clears payment role details, and closes
        // the merchant batch(es) for its credit-card transactions.
        // Ported from ThirdPartyTool.CloseReceipt.
        public bool CloseReceipt(string receiptId)
        {
            if (string.IsNullOrEmpty(receiptId)) return false;

            string sql = $@"
                SET XACT_ABORT ON;
                BEGIN TRANSACTION;
                UPDATE dbo.SaleReceipts SET TransactionStatusID = 'CLO'
                    WHERE SaleReceiptID = '{receiptId}';
                COMMIT;";

            if (AppSession.ProcessNonQuery(true, sql) < 0)
                return false;

            sql = $@"
                SET XACT_ABORT ON;
                BEGIN TRANSACTION;
                IF OBJECT_ID('dbo.PaymentRoleDetails', 'U') IS NOT NULL
                    DELETE FROM dbo.PaymentRoleDetails WHERE SaleReceiptID = '{receiptId}';
                COMMIT;";

            AppSession.ProcessNonQuery(true, sql);

            CloseMerchantBatches(receiptId);
            return true;
        }

        public bool VoidReceipt(string receiptId)
        {
            if (string.IsNullOrEmpty(receiptId)) return false;

            string sql = $@"
                UPDATE dbo.SaleReceipts SET TransactionStatusID = 'VOID', ReturnCode = '-115'
                WHERE SaleReceiptID = '{receiptId}'";

            if (AppSession.ProcessNonQuery(true, sql) < 0)
                return false;

            sql = $@"
                UPDATE dbo.SaleReceiptDetails SET Status = '3'
                WHERE SaleReceiptID = '{receiptId}' and Status <> '2'";

            AppSession.ProcessNonQuery(true, sql);

            sql = $@"
                UPDATE dbo.SaleReceiptDetailTaxDetails SET Status = '3'
                WHERE SaleReceiptID = '{receiptId}' and Status <> '2'";

            AppSession.ProcessNonQuery(true, sql);

            VoidMerchantTransactions(receiptId);
            return true;
        }

        // Cancels an open ticket: status -> CANR, plus the same merchant reversal.
        // Ported from ThirdPartyTool.CancelReceipt (note: the original's
        // payment-detail delete loops over an empty table, so it is a no-op —
        // matched here by NOT deleting payment details).
        public bool CancelReceipt(string receiptId)
        {
            if (string.IsNullOrEmpty(receiptId)) return false;

            string sql = $@"
                UPDATE dbo.SaleReceipts SET TransactionStatusID = 'CANR', ReturnCode = '-106'
                WHERE SaleReceiptID = '{receiptId}'";

            if (AppSession.ProcessNonQuery(true, sql) < 0)
                return false;

            sql = $@"
                UPDATE dbo.SaleReceiptDetails SET Status = '98',ReturnCode = '-106' WHERE SaleReceiptID = '{receiptId}' and Status <> '2'";

            AppSession.ProcessNonQuery(true, sql);

            sql = $@"
                UPDATE dbo.SaleReceiptDetailTaxDetails SET Status = '98',ReturnCode = '-106' WHERE SaleReceiptID = '{receiptId}' and Status <> '2'";

            AppSession.ProcessNonQuery(true, sql);

            VoidMerchantTransactions(receiptId);
            return true;
        }

        private void CloseMerchantBatches(string receiptId)
        {
            var ids = GetCreditCardTransactionIds(receiptId);
            if (ids.Count == 0) return;

            string inList = string.Join(",", ids.Select(id => "'" + id.Replace("'", "''") + "'"));

            // Capture the batches before the transactions are reassigned.
            var bdt = AppSession.ProcessQuery(false, $@"
                SELECT DISTINCT BatchUnique FROM dbo.TransactionDetail
                WHERE ISNULL(BatchUnique, '') <> '' AND ID IN ({inList})", "Batches");

            var batches = bdt.Rows.Cast<DataRow>()
                .Select(r => AppSession.GetString(r, "BatchUnique"))
                .Where(s => !string.IsNullOrWhiteSpace(s)).Distinct().ToList();

            AppSession.ProcessNonQuery(false, $@"
                UPDATE dbo.TransactionDetail
                SET BatchStatus = 'Closed', BatchUnique = 'BLOGIC_' + ISNULL(BatchUnique,'')
                WHERE ID IN ({inList})");

            foreach (var b in batches)
                RecalculateMerchantBatch(b);
        }

        private void VoidMerchantTransactions(string receiptId)
        {
            var pdt = AppSession.ProcessQuery(true, $@"
                SELECT PaymentMethodID, CreditCardTransactionType, CreditCardTransactionID
                FROM dbo.SaleReceiptPaymentDetails WHERE SaleReceiptID = '{receiptId}'", "Pay");

            if (pdt == null || pdt.Rows.Count == 0) return;
            if (string.IsNullOrEmpty(AppSession.MerchantConnectionString)) return;

            using (var conn = new SqlConnection(AppSession.MerchantConnectionString))
            {
                conn.Open();
                foreach (DataRow row in pdt.Rows)
                {
                    if (AppSession.GetInt(row, "PaymentMethodID") != 2) continue;
                    if (!AppSession.GetString(row, "CreditCardTransactionType").Contains("Sale")) continue;

                    var tranId = AppSession.GetString(row, "CreditCardTransactionID");
                    if (string.IsNullOrWhiteSpace(tranId)) continue;

                    var dt = new DataTable();
                    using (var cmd = new SqlCommand("SELECT * FROM dbo.TransactionDetail WHERE ID = @id", conn))
                    {
                        cmd.Parameters.AddWithValue("@id", tranId);
                        using (var rdr = cmd.ExecuteReader()) dt.Load(rdr);
                    }
                    if (dt.Rows.Count == 0) continue;

                    using (var cmd = new SqlCommand(
                        "UPDATE dbo.TransactionDetail SET TranStatus = 'VOIDED' WHERE ID = @id", conn))
                    {
                        cmd.Parameters.AddWithValue("@id", tranId);
                        cmd.ExecuteNonQuery();

                    }

                    InsertVoidSaleClone(conn, dt);

                    var batchUnique = AppSession.GetString(dt.Rows[0], "BatchUnique");
                    if (!string.IsNullOrWhiteSpace(batchUnique))
                        RecalculateMerchantBatch(batchUnique);
                }
            }
        }

        // Inserts a copy of a merchant transaction with a fresh ID and
        // TranType = 'VoidSale' (the reversal audit row).
        private static void InsertVoidSaleClone(SqlConnection conn, DataTable dt)
        {
            var cols = dt.Columns.Cast<DataColumn>().ToList();
            string colList = string.Join(", ", cols.Select(c => "[" + c.ColumnName + "]"));
            string paramList = string.Join(", ", cols.Select((c, i) => "@p" + i));
            var src = dt.Rows[0];

            using (var cmd = new SqlCommand(
                $"INSERT INTO dbo.TransactionDetail ({colList}) VALUES ({paramList})", conn))
            {
                for (int i = 0; i < cols.Count; i++)
                {
                    object val;
                    if (i == 0) // first column = ID
                        val = Guid.NewGuid().ToString();
                    else if (string.Equals(cols[i].ColumnName, "TranType", StringComparison.OrdinalIgnoreCase))
                        val = "VoidSale";
                    else
                        val = src[i] ?? DBNull.Value;

                    cmd.Parameters.AddWithValue("@p" + i, val ?? DBNull.Value);
                }
                cmd.ExecuteNonQuery();
            }
        }

        private List<string> GetCreditCardTransactionIds(string receiptId)
        {
            var dt = AppSession.ProcessQuery(true, $@"
                SELECT CreditCardTransactionID FROM dbo.SaleReceiptPaymentDetails
                WHERE SaleReceiptID = '{receiptId}' AND ISNULL(CreditCardTransactionID, '') <> ''", "CC");

            return dt.Rows.Cast<DataRow>()
                .Select(r => AppSession.GetString(r, "CreditCardTransactionID"))
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .Distinct()
                .ToList();
        }

        // ---- Add Tip For Payment Detail -----------------------------------

        // Sets the tip (Gratuity) on a single payment, recomputing the SVC fee
        // and merchant fee on the tip, updating PaymentRoleDetails and the
        // receipt's FinalGratuity, and adjusting the merchant transaction +
        // batch. Ported from ThirdPartyTool.AddTipForPaymentDetail.
        public bool AddTipForPaymentDetail(string receiptId, long paymentDetailId, decimal gratuity)
        {
            if (string.IsNullOrEmpty(receiptId)) return false;

            decimal svcFeeCfg = GetConfigDecimal("CreditCardConfigValue.SVCFee");
            decimal merchantFeeCfg = GetConfigDecimal("CreditCardConfigValue.MerchantFee");

            var srDt = AppSession.ProcessQuery(true,
                $"SELECT CashierID, DateCreated FROM dbo.SaleReceipts WHERE SaleReceiptID = '{receiptId}'", "SR");
            if (srDt == null || srDt.Rows.Count == 0) return false;

            int cashierId = AppSession.GetInt(srDt.Rows[0], "CashierID");
            DateTime dateCreated = AppSession.GetDateTime(srDt.Rows[0], "DateCreated");

            var pdDt = AppSession.ProcessQuery(true, $@"
                SELECT a.*, b.PaymentMethodType
                FROM dbo.SaleReceiptPaymentDetails a
                LEFT JOIN dbo.PaymentMethods b ON a.PaymentMethodID = b.PaymentMethodID
                WHERE a.SaleReceiptPaymentDetailID = {paymentDetailId}", "PD");
            if (pdDt == null || pdDt.Rows.Count == 0) return false;

            var pd = pdDt.Rows[0];
            decimal amountTendered = AppSession.GetDecimal(pd, "AmountTendered");
            decimal amountReturn = AppSession.GetDecimal(pd, "AmountReturn");
            decimal svcFeeAmount = AppSession.GetDecimal(pd, "SVCFeeAmount");
            decimal merchantFee = AppSession.GetDecimal(pd, "MerchantFee");
            int paymentMethodId = AppSession.GetInt(pd, "PaymentMethodID");
            int paymentMethodType = AppSession.GetInt(pd, "PaymentMethodType");
            string ccTranId = AppSession.GetString(pd, "CreditCardTransactionID");

            // Fees are only re-derived when the payment already carries them.
            decimal svcFeeForGratuity = svcFeeAmount != 0 ? Math.Round(gratuity * svcFeeCfg / 100m, 2) : 0m;
            decimal newMerchantFee = 0m, merchantFeeForTip = 0m;
            if (merchantFee != 0)
            {
                newMerchantFee = Math.Round((amountTendered - amountReturn + gratuity) * merchantFeeCfg / 100m, 2);
                merchantFeeForTip = Math.Round(gratuity * merchantFeeCfg / 100m, 2);
            }

            var inv = CultureInfo.InvariantCulture;
            string g = gratuity.ToString(inv);
            string svcG = svcFeeForGratuity.ToString(inv);
            string mFee = newMerchantFee.ToString(inv);
            string mFeeTip = merchantFeeForTip.ToString(inv);

            // 1) Payment detail
            if (AppSession.ProcessNonQuery(true, $@"
                UPDATE dbo.SaleReceiptPaymentDetails
                SET Gratuity = {g}, SVCFeeForGratuity = {svcG},
                    MerchantFee = {mFee}, MerchantFeeForTip = {mFeeTip}
                WHERE SaleReceiptPaymentDetailID = {paymentDetailId}") < 0)
                return false;

            // 2) Payment role details (tip distribution)
            UpsertPaymentRoleDetail(receiptId, paymentDetailId, paymentMethodId, paymentMethodType,
                cashierId, dateCreated, gratuity, svcFeeForGratuity, merchantFeeForTip);

            // 3) Receipt FinalGratuity (only when the column exists)
            AppSession.ProcessNonQuery(true, $@"
                IF COL_LENGTH('dbo.SaleReceipts', 'FinalGratuity') IS NOT NULL
                BEGIN
                    ;WITH g AS (
                        SELECT SaleReceiptID, SUM(Gratuity) AS TotalGratuity
                        FROM dbo.SaleReceiptPaymentDetails GROUP BY SaleReceiptID
                    )
                    UPDATE sr SET sr.FinalGratuity = g.TotalGratuity
                    FROM dbo.SaleReceipts sr
                    JOIN g ON g.SaleReceiptID = sr.SaleReceiptID
                    WHERE sr.TransactionStatusID = 'FIN' AND sr.SaleReceiptID = '{receiptId}';
                END");

            // 4) Merchant transaction + batch
            if (!string.IsNullOrWhiteSpace(ccTranId))
            {
                var safe = ccTranId.Replace("'", "''");
                AppSession.ProcessNonQuery(false, $@"
                    UPDATE dbo.TransactionDetail
                    SET Gratuity = {g}, SVCFeeForGratuity = {svcG}, MerchantFee = {mFee},
                        Total = Amount + {g}, MerchantFeeForTip = {mFeeTip}
                    WHERE ID = '{safe}'");

                var bdt = AppSession.ProcessQuery(false,
                    $"SELECT DISTINCT BatchUnique FROM dbo.TransactionDetail WHERE ID = '{safe}'", "B");
                var batchUnique = bdt?.Rows.Count > 0 ? AppSession.GetString(bdt.Rows[0], "BatchUnique") : "";
                if (!string.IsNullOrWhiteSpace(batchUnique))
                    RecalculateMerchantBatch(batchUnique);
            }

            return true;
        }

        private void UpsertPaymentRoleDetail(string receiptId, long paymentDetailId, int paymentMethodId,
            int paymentMethodType, int cashierId, DateTime dateCreated, decimal gratuity,
            decimal svcFeeForGratuity, decimal merchantFeeForTip, RoleOption role = null)
        {
            var existsDt = AppSession.ProcessQuery(true,
                "SELECT 1 AS X WHERE OBJECT_ID('dbo.PaymentRoleDetails', 'U') IS NOT NULL", "X");
            if (existsDt == null || existsDt.Rows.Count == 0) return;

            var inv = CultureInfo.InvariantCulture;
            string g = gratuity.ToString(inv);
            string svcG = svcFeeForGratuity.ToString(inv);
            string mFeeTip = merchantFeeForTip.ToString(inv);

            var roleDt = AppSession.ProcessQuery(true, $@"
                SELECT TOP 1 ID FROM dbo.PaymentRoleDetails
                WHERE SaleReceiptID = '{receiptId}' AND SaleReceiptPaymentDetailID = {paymentDetailId}", "PRD");

            if (roleDt != null && roleDt.Rows.Count > 0)
            {
                var id = AppSession.GetString(roleDt.Rows[0], "ID");
                AppSession.ProcessNonQuery(true, $@"
                    UPDATE dbo.PaymentRoleDetails
                    SET Gratuity = {g}, SVCFeeForGratuity = {svcG}, MerchantFeeForTip = {mFeeTip}
                    WHERE ID = '{id}'");
                return;
            }

            var emp = role != null
                ? new RoleTipInfo { RolesID = role.RolesID, TipRate = role.TipRate, TipRateOT = role.TipRateOT }
                : GetEmployeeClockInInfo(cashierId, dateCreated);
            string dc = dateCreated.ToString("yyyy-MM-dd HH:mm:ss");

            AppSession.ProcessNonQuery(true, $@"
                INSERT INTO dbo.PaymentRoleDetails
                    (ID, SaleReceiptID, SaleReceiptPaymentDetailID, PaymentMethodID, PaymentMethodType,
                     EmployeeID, RolesID, TipRate, TipRateOT, Gratuity, SVCFeeForGratuity, MerchantFeeForTip, DateCreated,
                     PartyServiceFeeAmount, PartyServiceFeeTaxAmount, SVCFeeForPartyServiceFeeAmount, MerchantFeeForPartyServiceFeeAmount,
                     AutoGratuityAmount, AutoGratuityTaxAmount, SVCFeeForAutoGratuityAmount, MerchantFeeForAutoGratuityAmount,
                     AreaFeeAmount, AreaFeeTaxAmount, SVCFeeForAreaFeeAmount, MerchantFeeForAreaFeeAmount,
                     ServiceFee, ServiceFeeTaxAmount, SVCFeeForServiceFee, MerchantFeeForServiceFee)
                VALUES
                    (NEWID(), '{receiptId}', {paymentDetailId}, {paymentMethodId}, {paymentMethodType},
                     {cashierId}, {emp.RolesID}, {emp.TipRate.ToString(inv)}, {emp.TipRateOT.ToString(inv)}, {g}, {svcG}, {mFeeTip}, '{dc}',
                     0, 0, 0, 0,
                     0, 0, 0, 0,
                     0, 0, 0, 0,
                     0, 0, 0, 0)");
        }

        // Resolves the role / tip-rate to stamp on a PaymentRoleDetails row.
        // Ported from Helper.GetEmployeeClockInInfoEntity (training-mode branch is
        // effectively dead in the original — it never selects EnableTrainingMode —
        // so the non-training path is reproduced here).
        private RoleTipInfo GetEmployeeClockInInfo(int employeeId, DateTime startDate)
        {
            var info = new RoleTipInfo();
            try
            {
                string sd = startDate.ToString("yyyy-MM-dd HH:mm:ss");
                var tc = AppSession.ProcessQuery(true, $@"
                    SELECT TipRate, TipRateOT, RolesID FROM dbo.TimeClock
                    WHERE EmployeeID = {employeeId}
                      AND (ClockStatus = 1 OR (DateTimeIn <= '{sd}' AND '{sd}' <= DateTimeOut))
                    ORDER BY DateTimeIn DESC", "TC");
                if (tc != null && tc.Rows.Count > 0)
                {
                    info.RolesID = AppSession.GetInt(tc.Rows[0], "RolesID");
                    info.TipRate = AppSession.GetDecimal(tc.Rows[0], "TipRate");
                    info.TipRateOT = AppSession.GetDecimal(tc.Rows[0], "TipRateOT");
                    return info;
                }

                var emp = AppSession.ProcessQuery(true,
                    $"SELECT IsUsingRoleTipPool, TipRate, TipRateOT FROM dbo.Employees WHERE EmployeeID = {employeeId}", "Emp");
                var roles = AppSession.ProcessQuery(true, $@"
                    SELECT TOP 1 r.RolesID, r.TipRate, r.TipRateOT
                    FROM dbo.EmployeeRoles er LEFT JOIN dbo.Roles r ON er.RolesID = r.RolesID
                    WHERE er.EmployeeID = {employeeId} ORDER BY r.TipRate", "Roles");

                if (emp != null && emp.Rows.Count > 0 && roles != null && roles.Rows.Count > 0)
                {
                    info.RolesID = AppSession.GetInt(roles.Rows[0], "RolesID");
                    bool isUsingRoleTipPool = AppSession.GetBool(emp.Rows[0], "IsUsingRoleTipPool");
                    var src = isUsingRoleTipPool ? roles.Rows[0] : emp.Rows[0];
                    info.TipRate = AppSession.GetDecimal(src, "TipRate");
                    info.TipRateOT = AppSession.GetDecimal(src, "TipRateOT");
                }
            }
            catch (Exception ex)
            {
                AppLogger.Error("GetEmployeeClockInInfo failed", ex);
            }
            return info;
        }

        // Roles assigned to an employee, with the tip rates PaymentRoleDetails
        // gets for each. defaultRoleId is the role GetEmployeeClockInInfo would
        // pick on its own (the role clocked in with at 'date', else the
        // employee's role with the lowest tip rate) and keeps its rates;
        // 0 when that role is not in the list.
        public List<RoleOption> GetEmployeeRoleOptions(int employeeId, DateTime date, out int defaultRoleId)
        {
            var list = new List<RoleOption>();
            defaultRoleId = 0;
            try
            {
                var emp = AppSession.ProcessQuery(true,
                    $"SELECT IsUsingRoleTipPool, TipRate, TipRateOT FROM dbo.Employees WHERE EmployeeID = {employeeId}", "Emp");
                var roles = AppSession.ProcessQuery(true, $@"
                    SELECT r.RolesID, r.RolesName, r.TipRate, r.TipRateOT
                    FROM dbo.EmployeeRoles er INNER JOIN dbo.Roles r ON er.RolesID = r.RolesID
                    WHERE er.EmployeeID = {employeeId} AND ISNULL(er.IsDeleted, 0) = 0 AND ISNULL(r.IsDeleted, 0) = 0
                    ORDER BY r.RolesName", "Roles");
                if (roles == null) return list;

                DataRow empRow = emp != null && emp.Rows.Count > 0 ? emp.Rows[0] : null;
                bool isUsingRoleTipPool = empRow != null && AppSession.GetBool(empRow, "IsUsingRoleTipPool");
                foreach (DataRow r in roles.Rows)
                {
                    var src = isUsingRoleTipPool || empRow == null ? r : empRow;
                    list.Add(new RoleOption
                    {
                        RolesID = AppSession.GetInt(r, "RolesID"),
                        RolesName = AppSession.GetString(r, "RolesName"),
                        TipRate = AppSession.GetDecimal(src, "TipRate"),
                        TipRateOT = AppSession.GetDecimal(src, "TipRateOT")
                    });
                }

                var clockIn = GetEmployeeClockInInfo(employeeId, date);
                var clocked = list.FirstOrDefault(x => x.RolesID == clockIn.RolesID);
                if (clocked != null)
                {
                    defaultRoleId = clocked.RolesID;
                    clocked.TipRate = clockIn.TipRate;
                    clocked.TipRateOT = clockIn.TipRateOT;
                }
            }
            catch (Exception ex)
            {
                AppLogger.Error("GetEmployeeRoleOptions failed", ex);
            }
            return list;
        }

        // ---- Add Tip By Sale Token ----------------------------------------

        // Looks up a token ticket by its number and computes the tip value it
        // carries (its net payment total).
        public TokenReceiptInfoEntity LookupTokenReceipt(string ticketNumber)
        {
            var info = new TokenReceiptInfoEntity();
            if (string.IsNullOrWhiteSpace(ticketNumber)) return info;

            var dt = AppSession.ProcessQuery(true, $@"
                SELECT TOP 1 SaleReceiptID, SaleReceiptNumber, TransactionStatusID
                FROM dbo.SaleReceipts
                WHERE SaleReceiptNumber = '{ticketNumber.Replace("'", "''")}'
                ORDER BY DateCreated DESC", "Token");
            if (dt == null || dt.Rows.Count == 0) return info;

            info.Found = true;
            info.SaleReceiptID = AppSession.GetString(dt.Rows[0], "SaleReceiptID");
            info.SaleReceiptNumber = AppSession.GetString(dt.Rows[0], "SaleReceiptNumber");
            info.TransactionStatusID = AppSession.GetString(dt.Rows[0], "TransactionStatusID");

            var pdt = AppSession.ProcessQuery(true, $@"
                SELECT AmountTendered, AmountReturn, Gratuity, SVCFeeAmount, CashDiscountAmount
                FROM dbo.SaleReceiptPaymentDetails WHERE SaleReceiptID = '{info.SaleReceiptID}'", "Pay");

            decimal tip = 0m;
            if (pdt != null)
                foreach (DataRow r in pdt.Rows)
                    tip += AppSession.GetDecimal(r, "AmountTendered")
                         - AppSession.GetDecimal(r, "AmountReturn")
                         + AppSession.GetDecimal(r, "Gratuity")
                         + AppSession.GetDecimal(r, "SVCFeeAmount")
                         - AppSession.GetDecimal(r, "CashDiscountAmount");

            info.TipAmount = tip;
            info.HasPayments = pdt != null && pdt.Rows.Count > 0;
            return info;
        }

        // Transfers a token ticket's payment(s) onto the main ticket as a pure
        // tip: the token payment is re-parented (sale amount zeroed, full value
        // becomes gratuity), the token ticket is closed, PaymentRoleDetails and
        // FinalGratuity are updated, and the merchant transaction is rewritten.
        // Ported from ThirdPartyTool.UpdateTipByTokenReceipt.
        public bool AddTipByTokenReceipt(string mainReceiptId, string tokenReceiptId)
        {
            if (string.IsNullOrEmpty(mainReceiptId) || string.IsNullOrEmpty(tokenReceiptId))
                return false;

            var saleEntity = AppSession.GetSaleReceiptsByID(mainReceiptId);

            var listPayments = AppSession.GetListSaleReceiptPaymentDetailsBySaleReceiptID(tokenReceiptId);

            if (saleEntity == null || listPayments == null || listPayments.Count == 0)
                return false;

            // Close the token ticket.
            if (AppSession.ProcessNonQuery(true, $"UPDATE dbo.SaleReceipts SET TransactionStatusID = 'CLO' WHERE SaleReceiptID = '{tokenReceiptId}'") < 0)
                return false;

            int cashierId = saleEntity.CashierID;
            DateTime dateCreated = saleEntity.DateCreated;
            string mainNumber = saleEntity.SaleReceiptNumber;
            string mainInvoice = saleEntity.InvoiceNum;

            var inv = CultureInfo.InvariantCulture;
            var dc = saleEntity.DateCreated;

            var processed = new List<TokenTipLine>();

            foreach (var item in listPayments)
            {
                decimal svcFeeCfg = AppSession.GetSVCFeePercent(saleEntity.CreditCardFeeMode);
                decimal merchantFeeCfg = AppSession.GetMerchantFeePercent(saleEntity.CreditCardFeeMode, item.PaymentCardType);
                decimal baseAmountCfg = AppSession.GetBaseAmountFee(saleEntity.SVCFeePercent);

                long pdId = item.SaleReceiptPaymentDetailID;
                int pmId = item.PaymentMethodID;
                int pmType = item.PaymentMethodType;
                decimal svcFeeAmount = item.SVCFeeAmount;
                decimal merchantFee = item.MerchantFee;
                string ccTranId = item.CreditCardTransactionID;

                decimal gratuity = item.AmountTendered - item.AmountReturn + item.Gratuity + item.SVCFeeAmount - item.CashDiscountAmount;

                decimal svcFeeForGratuity = Utility.CalculateSVCFeeFromTotalWithParams(gratuity, svcFeeCfg, baseAmountCfg);
                decimal merchantFeeForTip = Utility.CalculateMerchantFeeWithPercent(gratuity, merchantFeeCfg);

                AppSession.ProcessNonQuery(true, $@"
                    UPDATE dbo.SaleReceiptPaymentDetails
                    SET CreatedDate = '{dc}', AmountTendered = 0, SaleReceiptID = '{mainReceiptId}',
                        SVCFeeAmount = 0, SVCFeeForGratuity = {svcFeeForGratuity}, Gratuity = {gratuity},
                        MerchantFee = {merchantFeeForTip}, MerchantFeeForTip = {merchantFeeForTip}
                    WHERE SaleReceiptPaymentDetailID = {pdId}");

                UpsertPaymentRoleDetail(mainReceiptId, pdId, pmId, pmType, cashierId, dateCreated,
                    gratuity, svcFeeForGratuity, merchantFeeForTip);

                processed.Add(new TokenTipLine
                {
                    Gratuity = gratuity,
                    SvcFeeForGratuity = svcFeeForGratuity,
                    MerchantFeeForTip = merchantFeeForTip,
                    CreditCardTransactionID = ccTranId
                });
            }

            AppSession.ProcessNonQuery(true, $@"
                IF COL_LENGTH('dbo.SaleReceipts', 'FinalGratuity') IS NOT NULL
                BEGIN
                    ;WITH g AS (
                        SELECT SaleReceiptID, SUM(Gratuity) AS TotalGratuity
                        FROM dbo.SaleReceiptPaymentDetails GROUP BY SaleReceiptID
                    )
                    UPDATE sr SET sr.FinalGratuity = g.TotalGratuity
                    FROM dbo.SaleReceipts sr
                    JOIN g ON g.SaleReceiptID = sr.SaleReceiptID
                    WHERE sr.TransactionStatusID = 'FIN' AND sr.SaleReceiptID = '{mainReceiptId}';
                END");

            var batches = new HashSet<string>();
            foreach (var p in processed)
            {
                if (string.IsNullOrWhiteSpace(p.CreditCardTransactionID)) continue;
                var safe = p.CreditCardTransactionID.Replace("'", "''");

                AppSession.ProcessNonQuery(false, $@"
                    UPDATE dbo.TransactionDetail
                    SET InvoiceNo = '{mainInvoice.Replace("'", "''")}', Amount = 0,
                        Gratuity = {p.Gratuity.ToString(inv)}, SubTotal = 0, TaxAmount = 0, TotalAmount = 0,
                        TicketNumber = '{mainNumber.Replace("'", "''")}', SVCFee = 0,
                        SVCFeeForGratuity = {p.SvcFeeForGratuity.ToString(inv)},
                        MerchantFee = {p.MerchantFeeForTip.ToString(inv)},
                        MerchantFeeForTip = {p.MerchantFeeForTip.ToString(inv)}
                    WHERE ID = '{safe}'");

                var bdt = AppSession.ProcessQuery(false,
                    $"SELECT DISTINCT BatchUnique FROM dbo.TransactionDetail WHERE ID = '{safe}'", "B");
                var bu = bdt?.Rows.Count > 0 ? AppSession.GetString(bdt.Rows[0], "BatchUnique") : "";
                if (!string.IsNullOrWhiteSpace(bu)) batches.Add(bu);
            }
            foreach (var b in batches)
                RecalculateMerchantBatch(b);

            return true;
        }

        private class TokenTipLine
        {
            public decimal Gratuity;
            public decimal SvcFeeForGratuity;
            public decimal MerchantFeeForTip;
            public string CreditCardTransactionID;
        }

        // ---- Merge Refund Payment -----------------------------------------

        // Looks up a Refund (RET) ticket by its number and computes the amount
        // that will be merged (the negated sum of its AmountReturn values).
        public RefundReceiptInfoEntity LookupRefundReceipt(string ticketNumber)
        {
            var info = new RefundReceiptInfoEntity();
            if (string.IsNullOrWhiteSpace(ticketNumber)) return info;

            var dt = AppSession.ProcessQuery(true, $@"
                SELECT TOP 1 SaleReceiptID, SaleReceiptNumber, TransactionStatusID
                FROM dbo.SaleReceipts
                WHERE SaleReceiptNumber = '{ticketNumber.Replace("'", "''")}'
                ORDER BY DateCreated DESC", "Refund");
            if (dt == null || dt.Rows.Count == 0) return info;

            info.Found = true;
            info.SaleReceiptID = AppSession.GetString(dt.Rows[0], "SaleReceiptID");
            info.SaleReceiptNumber = AppSession.GetString(dt.Rows[0], "SaleReceiptNumber");
            info.TransactionStatusID = AppSession.GetString(dt.Rows[0], "TransactionStatusID");

            var pdt = AppSession.ProcessQuery(true, $@"
                SELECT AmountReturn
                FROM dbo.SaleReceiptPaymentDetails WHERE SaleReceiptID = '{info.SaleReceiptID}'", "Pay");

            decimal refund = 0m;
            if (pdt != null)
                foreach (DataRow r in pdt.Rows)
                    refund += -Math.Abs(AppSession.GetDecimal(r, "AmountReturn"));

            info.RefundAmount = refund;
            info.HasPayments = pdt != null && pdt.Rows.Count > 0;
            return info;
        }

        // Merges every payment line of a Refund (RET) ticket onto a Finished
        // (FIN) ticket: the refund ticket is closed (CLO), each returned amount
        // becomes a negative tender (AmountTendered = -ABS(AmountReturn)), the
        // return is zeroed and the line is re-parented to the finished ticket.
        // Runs the three updates in one transaction; header totals are left
        // untouched (caller does not recalculate).
        public bool MergeRefundPayments(string finishReceiptId, string refundReceiptId)
        {
            if (string.IsNullOrEmpty(finishReceiptId) || string.IsNullOrEmpty(refundReceiptId))
                return false;

            string fin = finishReceiptId.Replace("'", "''");
            string refund = refundReceiptId.Replace("'", "''");

            // Order matters: reassign SaleReceiptID last so the earlier UPDATE
            // (by refund id) still matches all of the refund ticket's lines.
            string sql = $@"
                SET XACT_ABORT ON;
                BEGIN TRANSACTION;

                UPDATE dbo.SaleReceipts
                    SET TransactionStatusID = 'CLO'
                    WHERE SaleReceiptID = '{refund}';

                UPDATE dbo.SaleReceiptPaymentDetails
                    SET AmountTendered = ABS(AmountReturn) * -1
                    WHERE SaleReceiptID = '{refund}';

                UPDATE dbo.SaleReceiptPaymentDetails
                    SET AmountReturn = 0, SaleReceiptID = '{fin}'
                    WHERE SaleReceiptID = '{refund}';

                COMMIT;";

            return AppSession.ProcessNonQuery(true, sql) >= 0;
        }

        // ---- Update Service Charges ---------------------------------------

        public ServiceChargeSetEntity GetServiceCharges(string receiptId)
        {
            string sql = $@"
                SELECT *
                FROM dbo.SaleReceipts WHERE SaleReceiptID = '{receiptId}'";

            return AppSession.QueryFirstOrDefault<ServiceChargeSetEntity>(true, sql);
        }

        // Adjusts one service charge to (newAmount, newTax) and rebalances the
        // ticket and its single payment. Ported from the BackgroundWorker logic
        // in ThirdPartyTool.SelectServiceChargeToAdjstForm (single-payment only).
        public bool UpdateServiceCharge(string receiptId, ServiceChargeKind kind, decimal newAmount, decimal newTax)
        {
            if (string.IsNullOrEmpty(receiptId)) return false;

            List<string> listScript = new List<string>();

            string sql = $@"
                SELECT *
                FROM dbo.SaleReceipts WHERE SaleReceiptID = '{receiptId}'";

            var sale = AppSession.QueryFirstOrDefault<SaleReceiptsEntity>(true, sql);

            sql = $@"SELECT * FROM dbo.SaleReceiptDetails WHERE SaleReceiptID = '{receiptId}'";
            var saleDetails = AppSession.Query<SaleReceiptsEntity>(true, sql);

            if (sale == null && saleDetails != null)
                return false;

            var inv = CultureInfo.InvariantCulture;


            switch (kind)
            {
                case ServiceChargeKind.AreaServiceFee:

                    if (sale.AreaFeeAmount != newAmount)
                    {
                        listScript.Add($" UPDATE dbo.SaleReceipts SET AreaFeeAmount = {newAmount.ToString(inv)} WHERE SaleReceiptID = '{receiptId}';");
                        listScript.Add($" UPDATE dbo.SaleReceiptServiceChargeTaxDetails SET AreaFeeAmount = {newAmount.ToString(inv)} WHERE SaleReceiptID = '{receiptId}';");
                    }

                    if (sale.AreaFeeTaxAmount != newTax)
                        listScript.Add($" UPDATE dbo.SaleReceipts SET AreaFeeTaxAmount = {newTax.ToString(inv)} WHERE SaleReceiptID = '{receiptId}';");

                    break;
                case ServiceChargeKind.AutoServiceFee:
                    if (sale.AutoGratuityAmount != newAmount)
                    {
                        listScript.Add($" UPDATE dbo.SaleReceipts SET AutoGratuityAmount = {newAmount.ToString(inv)} WHERE SaleReceiptID = '{receiptId}';");
                        listScript.Add($" UPDATE dbo.SaleReceiptServiceChargeTaxDetails SET AutoGratuityAmount = {newAmount.ToString(inv)} WHERE SaleReceiptID = '{receiptId}';");
                    }

                    if (sale.AutoGratuityTaxAmount != newTax)
                        listScript.Add($" UPDATE dbo.SaleReceipts SET AutoGratuityTaxAmount = {newTax.ToString(inv)} WHERE SaleReceiptID = '{receiptId}';");

                    break;
                case ServiceChargeKind.PartyServiceFee:
                    if (sale.PartyServiceFeeAmount != newAmount)
                    {
                        listScript.Add($" UPDATE dbo.SaleReceipts SET PartyServiceFeeAmount = {newAmount.ToString(inv)} WHERE SaleReceiptID = '{receiptId}';");
                        listScript.Add($" UPDATE dbo.SaleReceiptServiceChargeTaxDetails SET PartyServiceFeeAmount = {newAmount.ToString(inv)} WHERE SaleReceiptID = '{receiptId}';");
                    }

                    if (sale.PartyServiceFeeTaxAmount != newTax)
                        listScript.Add($" UPDATE dbo.SaleReceipts SET PartyServiceFeeTaxAmount = {newTax.ToString(inv)} WHERE SaleReceiptID = '{receiptId}';");

                    break;
                case ServiceChargeKind.ExtraServiceFee:
                    if (sale.ExtraFeeAmount != newAmount)
                    {
                        listScript.Add($" UPDATE dbo.SaleReceipts SET ExtraFeeAmount = {newAmount.ToString(inv)} WHERE SaleReceiptID = '{receiptId}';");
                        listScript.Add($" UPDATE dbo.SaleReceiptServiceChargeTaxDetails SET ExtraFeeAmount = {newAmount.ToString(inv)} WHERE SaleReceiptID = '{receiptId}';");
                    }

                    if (sale.ExtraFeeTaxAmount != newTax)
                        listScript.Add($" UPDATE dbo.SaleReceipts SET ExtraFeeTaxAmount = {newTax.ToString(inv)} WHERE SaleReceiptID = '{receiptId}';");

                    break;
                case ServiceChargeKind.SVCFee:
                    newAmount += newTax;

                    if (sale.SVCFeePaidAmount != newAmount)
                    {
                        listScript.Add($" UPDATE dbo.SaleReceipts SET SVCFeeAmount = {newAmount.ToString(inv)} WHERE SaleReceiptID = '{receiptId}';");
                        listScript.Add($" UPDATE dbo.SaleReceipts SET SVCFeePaidAmount = {newAmount.ToString(inv)} WHERE SaleReceiptID = '{receiptId}';");
                    }

                    if (sale.SVCFeeTaxAmount != newTax)
                        listScript.Add($" UPDATE dbo.SaleReceipts SET SVCFeeTaxAmount = {newTax.ToString(inv)} WHERE SaleReceiptID = '{receiptId}';");

                    break;
                case ServiceChargeKind.CashDiscount:
                    newAmount += newTax;

                    if (sale.CashDiscountPaidAmount != newAmount)
                    {
                        listScript.Add($" UPDATE dbo.SaleReceipts SET CashDiscountAmount = {newAmount.ToString(inv)} WHERE SaleReceiptID = '{receiptId}';");
                        listScript.Add($" UPDATE dbo.SaleReceipts SET CashDiscountPaidAmount = {newAmount.ToString(inv)} WHERE SaleReceiptID = '{receiptId}';");
                    }

                    if (sale.CashDiscountTaxAmount != newTax)
                        listScript.Add($" UPDATE dbo.SaleReceipts SET CashDiscountTaxAmount = {newTax.ToString(inv)} WHERE SaleReceiptID = '{receiptId}';");

                    break;
                default: // ManualServiceFee
                    if (sale.ServiceFee != newAmount)
                    {
                        listScript.Add($" UPDATE dbo.SaleReceipts SET ServiceFee = {newAmount.ToString(inv)} WHERE SaleReceiptID = '{receiptId}';");
                        listScript.Add($" UPDATE dbo.SaleReceiptServiceChargeTaxDetails SET ServiceFee = {newAmount.ToString(inv)} WHERE SaleReceiptID = '{receiptId}';");
                    }

                    if (sale.ServiceFeeTaxAmount != newTax)
                        listScript.Add($" UPDATE dbo.SaleReceipts SET ServiceFeeTaxAmount = {newTax.ToString(inv)} WHERE SaleReceiptID = '{receiptId}';");

                    break;
            }

            foreach (var script in listScript)
            {
                AppSession.ProcessNonQuery(true, script);
            }

            return true;
        }

        public bool UpdateTotalServiceCharge(string receiptId)
        {
            if (string.IsNullOrEmpty(receiptId)) return false;

            List<string> listScript = new List<string>();

            string sql = $@"SELECT * FROM dbo.SaleReceipts WHERE SaleReceiptID = '{receiptId}'";

            var sale = AppSession.QueryFirstOrDefault<SaleReceiptsEntity>(true, sql);

            var newSalesOrderFee = sale.AutoGratuityAmount + sale.ServiceFee + sale.ExtraFeeAmount + sale.PartyServiceFeeAmount + sale.AreaFeeAmount;
            var newServiceChargeTaxAmount = sale.AutoGratuityTaxAmount + sale.ServiceFeeTaxAmount + sale.ExtraFeeTaxAmount + sale.PartyServiceFeeTaxAmount + sale.AreaFeeTaxAmount;

            var balanceSalesOrderFee = newSalesOrderFee - sale.SalesOrderFee;
            var balanceServiceChargeTaxAmount = newServiceChargeTaxAmount - sale.ServiceChargeTaxAmount;

            var newTotalAmount = sale.TotalAmount + balanceSalesOrderFee + balanceServiceChargeTaxAmount;
            var newTaxAmount = sale.TaxAmount + balanceServiceChargeTaxAmount;

            sql = $"UPDATE dbo.SaleReceipts SET SalesOrderFee = '{newSalesOrderFee}' WHERE SaleReceiptID = '{receiptId}';" +
                $"UPDATE dbo.SaleReceipts SET ServiceChargeTaxAmount = '{newServiceChargeTaxAmount}' WHERE SaleReceiptID = '{receiptId}';" +
                $"UPDATE dbo.SaleReceipts SET TotalAmount = '{newTotalAmount}' WHERE SaleReceiptID = '{receiptId}';" +
                $"UPDATE dbo.SaleReceipts SET TaxAmount = '{newTaxAmount}' WHERE SaleReceiptID = '{receiptId}';";

            AppSession.ProcessNonQuery(true, sql);

            return true;
        }

        // ---- Finish Ticket ------------------------------------------------

        public FinishTicketEntity GetFinishInfo(string receiptId)
        {
            var info = new FinishTicketEntity();
            var dt = AppSession.ProcessQuery(true,
                $"SELECT * FROM dbo.SaleReceipts WHERE SaleReceiptID = '{receiptId}'", "SR");
            if (dt == null || dt.Rows.Count == 0) return info;

            var r = dt.Rows[0];
            decimal totalAmount = AppSession.GetDecimal(r, "TotalAmount");
            decimal taxAmount = AppSession.GetDecimal(r, "TaxAmount");
            decimal paidAmount = AppSession.GetDecimal(r, "PaidAmount");
            decimal svcFeeAmount = AppSession.GetDecimal(r, "SVCFeeAmount");
            decimal cashDiscountAmount = AppSession.GetDecimal(r, "CashDiscountAmount");
            decimal svcFeePaidAmount = AppSession.GetDecimal(r, "SVCFeePaidAmount");
            decimal cashDiscountPaidAmount = AppSession.GetDecimal(r, "CashDiscountPaidAmount");

            info.Found = true;
            info.SaleReceiptNumber = AppSession.GetString(r, "SaleReceiptNumber");
            info.Base = totalAmount + (svcFeeAmount - cashDiscountAmount) - (svcFeePaidAmount - cashDiscountPaidAmount) - paidAmount;
            info.BaseNoTax = totalAmount - taxAmount - paidAmount;
            info.IncludeSVCFee = totalAmount + svcFeeAmount - paidAmount - svcFeePaidAmount;
            info.IncludeSVCFeeNoTax = totalAmount - taxAmount + svcFeeAmount - paidAmount - svcFeePaidAmount;
            info.IncludeCashDiscount = (totalAmount - cashDiscountAmount) - (paidAmount - cashDiscountPaidAmount);
            info.IncludeCashDiscountNoTax = (totalAmount - taxAmount - cashDiscountAmount) - (paidAmount - cashDiscountPaidAmount);
            return info;
        }

        public List<PaymentMethodOption> GetPaymentMethodOptions()
        {
            var list = new List<PaymentMethodOption>();
            var dt = AppSession.ProcessQuery(true,
                "SELECT PaymentMethodID, PaymentMethodName, CashAdjust FROM dbo.PaymentMethods ORDER BY PaymentMethodID", "PM");
            if (dt == null) return list;
            foreach (DataRow r in dt.Rows)
                list.Add(new PaymentMethodOption
                {
                    PaymentMethodID = AppSession.GetInt(r, "PaymentMethodID"),
                    PaymentMethodName = AppSession.GetString(r, "PaymentMethodName"),
                    CashAdjust = AppSession.GetInt(r, "CashAdjust")
                });
            return list;
        }

        public List<BatchOption> GetSettledBatches()
        {
            var list = new List<BatchOption>();
            if (string.IsNullOrEmpty(AppSession.MerchantConnectionString)) return list;

            var dt = AppSession.ProcessQuery(false, @"
                SELECT BatchUnique, [Date] AS DateCreated, MerchantID, ID, Total
                FROM dbo.Batch
                WHERE [Date] >= DATEADD(MONTH, -2, GETDATE())
                ORDER BY [Date] DESC", "Batch");
            if (dt == null) return list;
            foreach (DataRow r in dt.Rows)
                list.Add(new BatchOption
                {
                    BatchUnique = AppSession.GetString(r, "BatchUnique"),
                    MerchantID = AppSession.GetString(r, "MerchantID"),
                    BatchID = AppSession.GetString(r, "ID"),
                    DateCreated = AppSession.GetDateTime(r, "DateCreated"),
                    Total = AppSession.GetDecimal(r, "Total")
                });
            return list;
        }

        // Finishes an open ticket with a single payment. Ported from
        // ThirdPartyTool.FinishTicketWithPaymentDetailForm.BtnOK_Click, including
        // the FIN-completion block that was unreachable (after a stray `return;`)
        // in the original. Service charges are assigned to the single payment
        // (the multi-payment allocation engine is intentionally not ported).
        public bool FinishTicket(string receiptId, FinishTicketRequest req)
        {
            if (string.IsNullOrEmpty(receiptId) || req == null) return false;

            var srDt = AppSession.ProcessQuery(true,
                $"SELECT * FROM dbo.SaleReceipts WHERE SaleReceiptID = '{receiptId}'", "SR");
            if (srDt == null || srDt.Rows.Count == 0) return false;
            var sr = srDt.Rows[0];

            decimal merchantFeeCfg = GetConfigDecimal("CreditCardConfigValue.MerchantFee");

            int cashAdjust = 1;
            var pmDt = AppSession.ProcessQuery(true,
                $"SELECT CashAdjust FROM dbo.PaymentMethods WHERE PaymentMethodID = {req.PaymentMethodID}", "PM");
            if (pmDt != null && pmDt.Rows.Count > 0)
                cashAdjust = AppSession.GetInt(pmDt.Rows[0], "CashAdjust");

            // ---- read receipt fields ----
            string cashierId = AppSession.GetString(sr, "CashierID");
            string shiftId = AppSession.GetString(sr, "ShiftID");
            string invoiceNum = AppSession.GetString(sr, "InvoiceNum");
            string workstationId = AppSession.GetString(sr, "WorkstationID");
            string workstationName = AppSession.GetString(sr, "WorkstationName");
            string saleReceiptOrder = AppSession.GetString(sr, "SaleReceiptOrder");
            string saleReceiptNumber = AppSession.GetString(sr, "SaleReceiptNumber");

            decimal totalAmount = AppSession.GetDecimal(sr, "TotalAmount");
            decimal taxAmount = AppSession.GetDecimal(sr, "TaxAmount");
            decimal paidAmount = AppSession.GetDecimal(sr, "PaidAmount");
            decimal svcFeeAmount = AppSession.GetDecimal(sr, "SVCFeeAmount");
            decimal svcFeeTaxAmount = AppSession.GetDecimal(sr, "SVCFeeTaxAmount");
            decimal svcFeePaidAmount = AppSession.GetDecimal(sr, "SVCFeePaidAmount");
            decimal svcFeeTaxPaidAmount = AppSession.GetDecimal(sr, "SVCFeeTaxPaidAmount");
            decimal cashDiscountAmount = AppSession.GetDecimal(sr, "CashDiscountAmount");
            decimal cashDiscountTaxAmount = AppSession.GetDecimal(sr, "CashDiscountTaxAmount");
            decimal cashDiscountPaidAmount = AppSession.GetDecimal(sr, "CashDiscountPaidAmount");
            decimal cashDiscountTaxPaidAmount = AppSession.GetDecimal(sr, "CashDiscountTaxPaidAmount");
            decimal totalDiscountAmount = AppSession.GetDecimal(sr, "TotalDiscountAmount");
            decimal salesOrderFee = AppSession.GetDecimal(sr, "SalesOrderFee");
            decimal serviceChargeTaxAmount = AppSession.GetDecimal(sr, "ServiceChargeTaxAmount");
            decimal partyServiceFeeAmount = AppSession.GetDecimal(sr, "PartyServiceFeeAmount");
            decimal partyServiceFeeTaxAmount = AppSession.GetDecimal(sr, "PartyServiceFeeTaxAmount");
            decimal areaFeeAmount = AppSession.GetDecimal(sr, "AreaFeeAmount");
            decimal areaFeeTaxAmount = AppSession.GetDecimal(sr, "AreaFeeTaxAmount");
            decimal serviceFee = AppSession.GetDecimal(sr, "ServiceFee");
            decimal serviceFeeTaxAmount = AppSession.GetDecimal(sr, "ServiceFeeTaxAmount");
            decimal extraFeeAmount = AppSession.GetDecimal(sr, "ExtraFeeAmount");
            decimal extraFeeTaxAmount = AppSession.GetDecimal(sr, "ExtraFeeTaxAmount");
            decimal autoGratuityAmount = AppSession.GetDecimal(sr, "AutoGratuityAmount");
            decimal autoGratuityTaxAmount = AppSession.GetDecimal(sr, "AutoGratuityTaxAmount");

            // ---- compute amounts for the chosen mode ----
            decimal gratuity = req.Gratuity;
            decimal svcFeeForGratuity = CalcSVCFeeForTip(gratuity);
            decimal _paidAmount = 0, _svcFeeAmount = 0, _svcFeeTaxAmount = 0,
                    _cashDiscountAmount = 0, _cashDiscountTaxAmount = 0;
            decimal taxForRow = taxAmount; // zeroed for the "no tax" modes

            switch (req.Mode)
            {
                case FinishAmountMode.Base:
                    _paidAmount = totalAmount - paidAmount;
                    _svcFeeAmount = svcFeeAmount - svcFeePaidAmount;
                    _svcFeeTaxAmount = svcFeeTaxAmount - svcFeeTaxPaidAmount;
                    _cashDiscountAmount = cashDiscountAmount - cashDiscountPaidAmount;
                    _cashDiscountTaxAmount = cashDiscountTaxAmount - cashDiscountTaxPaidAmount;
                    break;
                case FinishAmountMode.IncludeSVCFee:
                    _paidAmount = totalAmount - paidAmount;
                    _svcFeeAmount = svcFeeAmount - svcFeePaidAmount;
                    _svcFeeTaxAmount = svcFeeTaxAmount - svcFeeTaxPaidAmount;
                    break;
                case FinishAmountMode.IncludeCashDiscount:
                    _paidAmount = totalAmount - paidAmount;
                    _cashDiscountAmount = cashDiscountAmount - cashDiscountPaidAmount;
                    _cashDiscountTaxAmount = cashDiscountTaxAmount - cashDiscountTaxPaidAmount;
                    svcFeeForGratuity = 0;
                    break;
                case FinishAmountMode.BaseNoTax:
                    _paidAmount = totalAmount - paidAmount - taxAmount;
                    taxForRow = 0;
                    break;
                case FinishAmountMode.IncludeSVCFeeNoTax:
                    _paidAmount = totalAmount - paidAmount - taxAmount;
                    taxForRow = 0;
                    _svcFeeAmount = svcFeeAmount - svcFeePaidAmount;
                    _svcFeeTaxAmount = svcFeeTaxAmount - svcFeeTaxPaidAmount;
                    break;
                case FinishAmountMode.IncludeCashDiscountNoTax:
                    _paidAmount = totalAmount - paidAmount - taxAmount;
                    taxForRow = 0;
                    _cashDiscountAmount = cashDiscountAmount - cashDiscountPaidAmount;
                    _cashDiscountTaxAmount = cashDiscountTaxAmount - cashDiscountTaxPaidAmount;
                    svcFeeForGratuity = 0;
                    break;
            }

            decimal subTotal = totalAmount - salesOrderFee - taxForRow + totalDiscountAmount;
            decimal merchantFee = CalcMerchantFee(_paidAmount + _svcFeeAmount + gratuity, merchantFeeCfg);
            decimal merchantFeeForTip = CalcMerchantFee(gratuity, merchantFeeCfg);

            if (cashAdjust == 1)
            {
                svcFeeForGratuity = 0;
                merchantFeeForTip = 0;
                merchantFee = 0;
            }

            var inv = CultureInfo.InvariantCulture;
            string createdDate = req.CreatedDate.ToString("yyyy-MM-dd HH:mm:ss");
            bool isCreditCard = req.PaymentMethodID == 2;

            // ---- merchant transaction (credit card only) ----
            string newTransactionId = Guid.NewGuid().ToString();
            if (isCreditCard)
            {
                if (string.IsNullOrEmpty(AppSession.MerchantConnectionString)) return false;

                var template = AppSession.ProcessQuery(false,
                    $"SELECT TOP 1 * FROM dbo.TransactionDetail WHERE BatchUnique = '{(req.BatchUnique ?? "").Replace("'", "''")}' ORDER BY [Date] DESC", "Tran");
                if (template == null || template.Columns.Count == 0) return false;

                string terminalIp = template.Rows.Count > 0 ? AppSession.GetString(template.Rows[0], "TerminalIpAddress") : "";
                string terminalName = template.Rows.Count > 0 ? AppSession.GetString(template.Rows[0], "TerminalName") : "";

                var numDt = AppSession.ProcessQuery(false,
                    "SELECT ISNULL(MAX(CAST(Number AS BIGINT)), 0) + 1 AS NextNo FROM dbo.TransactionDetail", "No");
                string number = numDt != null && numDt.Rows.Count > 0 ? AppSession.GetString(numDt.Rows[0], "NextNo") : "1";
                while (number.Length < 6) number = "0" + number;

                string cardNumber = string.IsNullOrWhiteSpace(req.CardNumber)
                    ? "xxxx0000"
                    : (req.CardNumber.Contains("xxxx") ? req.CardNumber : "xxxx" + req.CardNumber);

                var map = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
                {
                    ["Address"] = Q(terminalIp),
                    ["ID"] = Q(newTransactionId),
                    ["Number"] = Q(number),
                    ["Date"] = Q(createdDate),
                    ["TranType"] = "'Sale'",
                    ["TenderType"] = "'CreditCard'",
                    ["CardNumber"] = Q(cardNumber),
                    ["CardNumberMasked"] = Q(cardNumber),
                    ["CardType"] = Q((req.CardType ?? "").ToUpper()),
                    ["ExpDate"] = Q(req.ExpiryDate),
                    ["Result"] = "'Approved'",
                    ["TextResponse"] = "'OK'",
                    ["InvoiceNo"] = Q(invoiceNum),
                    ["CustomerName"] = "''",
                    ["AuthCode"] = Q(req.AuthCode),
                    ["RefNo"] = Q(req.ReferenceNumber),
                    ["AuthorizeAmount"] = Q((_paidAmount + _svcFeeAmount + gratuity).ToString(inv)),
                    ["OriginalAmount"] = Q((_paidAmount + _svcFeeAmount + gratuity).ToString(inv)),
                    ["Total"] = Q((_paidAmount + _svcFeeAmount + gratuity).ToString(inv)),
                    ["Amount"] = Q((_paidAmount + _svcFeeAmount).ToString(inv)),
                    ["Gratuity"] = Q(gratuity.ToString(inv)),
                    ["OriginalTip"] = Q(gratuity.ToString(inv)),
                    ["BatchID"] = Q(req.BatchID),
                    ["BatchTotal"] = "'0'",
                    ["BatchStatus"] = "'Closed'",
                    ["Token"] = "'DyncNueCs5MB8000'",
                    ["CCEntry"] = "'CONTACTLESS'",
                    ["Description"] = "'Success'",
                    ["ReturnCode"] = "'000000'",
                    ["MerchantID"] = Q(req.MerchantID),
                    ["RegisterID"] = Q(req.MerchantID),
                    ["MerchantAccount"] = Q(req.MerchantID),
                    ["UserID"] = Q(cashierId),
                    ["ServerID"] = Q(cashierId),
                    ["Cashier"] = Q(GetCashierName(cashierId)),
                    ["ServerName"] = Q(GetCashierName(cashierId)),
                    ["TrackNo"] = "null",
                    ["IsTipInLine"] = "null",
                    ["TranStatus"] = "'ACTIVE'",
                    ["TerminalName"] = Q(terminalName),
                    ["BankApprovalCode"] = "'610815501296'",
                    ["BankTransactionID"] = "'79043291'",
                    ["WorkstationID"] = Q(workstationId),
                    ["WorkstationName"] = Q(workstationName),
                    ["IsChecked"] = "'1'",
                    ["OrderID"] = Q(saleReceiptOrder),
                    ["TicketOrder"] = Q(saleReceiptOrder),
                    ["SVCFeeForGratuity"] = Q(svcFeeForGratuity.ToString(inv)),
                    ["SVCFee"] = Q(_svcFeeAmount.ToString(inv)),
                    ["TicketNumber"] = Q(saleReceiptNumber),
                    ["AID"] = "'A0000000031010'",
                    ["BatchUnique"] = Q(req.BatchUnique),
                    ["SubTotal"] = Q(subTotal.ToString(inv)),
                    ["TaxAmount"] = Q(taxForRow.ToString(inv)),
                    ["DiscountAmount"] = Q(totalDiscountAmount.ToString(inv)),
                    ["ServiceCharge"] = Q(salesOrderFee.ToString(inv)),
                    ["TotalAmount"] = Q(totalAmount.ToString(inv)),
                    ["PartyServiceFeeAmount"] = Q(partyServiceFeeAmount.ToString(inv)),
                    ["AreaFeeAmount"] = Q(areaFeeAmount.ToString(inv)),
                    ["ServiceFee"] = Q(serviceFee.ToString(inv)),
                    ["TerminalIpAddress"] = Q(terminalIp),
                    ["MerchantFee"] = Q(merchantFee.ToString(inv)),
                    ["MerchantFeeForTip"] = Q(merchantFeeForTip.ToString(inv)),
                    ["IsTryVoidOpenTab"] = "'0'",
                    ["AmountCaptured"] = "'-1'",
                    ["TipCaptured"] = "'-1'",
                    ["IsPhone"] = "'0'",
                    ["AutoGratuityAmount"] = Q(autoGratuityAmount.ToString(inv)),
                    ["CreditCardInfo"] = "null",
                    ["SVCFeeTaxAmount"] = Q(_svcFeeTaxAmount.ToString(inv)),
                    ["SVCFeeType"] = "'1'",
                    ["MerchantFeePercent"] = Q(merchantFeeCfg.ToString(inv)),
                    ["AdjustTipInformation"] = "null"
                };

                string merchantInsert = BuildColumnInsert("dbo.TransactionDetail", template, map, false);
                if (AppSession.ProcessNonQuery(false, merchantInsert) <= 0) return false;

                RecalculateMerchantBatch(req.BatchUnique);
            }

            // ---- payment detail (BLogicPOS7) ----
            var pdTemplate = AppSession.ProcessQuery(true,
                $"SELECT * FROM dbo.SaleReceiptPaymentDetails WHERE SaleReceiptID = '{receiptId}'", "PD");
            if (pdTemplate == null || pdTemplate.Columns.Count == 0) return false;

            var idDt = AppSession.ProcessQuery(true,
                "SELECT ISNULL(MAX(SaleReceiptPaymentDetailID), 0) + 1 AS NextId FROM dbo.SaleReceiptPaymentDetails", "Id");
            long newPaymentId = idDt != null && idDt.Rows.Count > 0 ? AppSession.GetLong(idDt.Rows[0], "NextId") : 1;

            var pdMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                ["SaleReceiptPaymentDetailID"] = Q(newPaymentId.ToString(inv)),
                ["SaleReceiptID"] = Q(receiptId),
                ["PaymentMethodID"] = Q(req.PaymentMethodID.ToString(inv)),
                ["AmountTendered"] = Q(_paidAmount.ToString(inv)),
                ["AmountReturn"] = "'0'",
                ["CreatedDate"] = Q(createdDate),
                ["IsDeposit"] = "'0'",
                ["GiftExtra"] = "'0'",
                ["Cashier"] = Q(cashierId),
                ["EmployeePaidID"] = Q(cashierId),
                ["ShiftID"] = string.IsNullOrWhiteSpace(shiftId) ? "null" : Q(shiftId),
                ["GiftCardCode"] = "null",
                ["CurrencyAmount"] = "'0'",
                ["CurrencyID"] = "'1'",
                ["PaymentReceiptID"] = "null",
                ["PayByAccuPoint"] = "null",
                ["PaymentType"] = "'0'",
                ["Gratuity"] = Q(gratuity.ToString(inv)),
                ["PaidServiceFee"] = "'0'",
                ["PaidPayout"] = "'0'",
                ["SVCFeeAmount"] = Q(_svcFeeAmount.ToString(inv)),
                ["SVCFeeForGratuity"] = Q(svcFeeForGratuity.ToString(inv)),
                ["SVCFeeForServiceCharge"] = "'0'",
                ["CashDiscountAmount"] = Q(_cashDiscountAmount.ToString(inv)),
                ["MerchantFee"] = Q(merchantFee.ToString(inv)),
                ["PartyServiceFeeAmount"] = "'0'",
                ["SVCFeeForPartyServiceFeeAmount"] = "'0'",
                ["AreaFeeAmount"] = "'0'",
                ["SVCFeeForAreaFeeAmount"] = "'0'",
                ["ServiceFee"] = "'0'",
                ["SVCFeeForServiceFee"] = "'0'",
                ["MerchantFeeForTip"] = Q(merchantFeeForTip.ToString(inv)),
                ["MerchantFeeForServiceFee"] = "'0'",
                ["MerchantFeeForAreaFeeAmount"] = "'0'",
                ["MerchantFeeForPartyServiceFeeAmount"] = "'0'",
                ["CardBalance"] = "'0'",
                ["AutoGratuityAmount"] = "'0'",
                ["SVCFeeForAutoGratuityAmount"] = "'0'",
                ["MerchantFeeForAutoGratuityAmount"] = "'0'",
                ["IsDecline"] = "'0'",
                ["IsDepositPayment"] = "'0'",
                ["SVCFeeTaxAmount"] = Q(_svcFeeTaxAmount.ToString(inv)),
                ["CashDiscountTaxAmount"] = Q(_cashDiscountTaxAmount.ToString(inv)),
                ["ServiceFeeTaxAmount"] = "'0'",
                ["AreaFeeTaxAmount"] = "'0'",
                ["PartyServiceFeeTaxAmount"] = "'0'",
                ["AutoGratuityTaxAmount"] = "'0'",
                ["TaxAmountByPaymentModeTaxCustomFee"] = "'0'",
                ["IsCalculateTaxCustomFee"] = _svcFeeTaxAmount > 0 ? "1" : "0",
                ["CoinDiscountAmount"] = "'0'",
                ["PaidHouseAccount"] = "'0'"
            };

            string pdInsert = "SET IDENTITY_INSERT dbo.SaleReceiptPaymentDetails ON; "
                + BuildColumnInsert("dbo.SaleReceiptPaymentDetails", pdTemplate, pdMap, false)
                + " SET IDENTITY_INSERT dbo.SaleReceiptPaymentDetails OFF;";
            if (AppSession.ProcessNonQuery(true, pdInsert) <= 0) return false;

            // ---- single-payment service-charge assignment ----
            AppSession.ProcessNonQuery(true, $@"
                UPDATE dbo.SaleReceiptPaymentDetails SET
                    PaidServiceFee = {(salesOrderFee + serviceChargeTaxAmount).ToString(inv)},
                    PartyServiceFeeAmount = {partyServiceFeeAmount.ToString(inv)},
                    PartyServiceFeeTaxAmount = {partyServiceFeeTaxAmount.ToString(inv)},
                    AreaFeeAmount = {areaFeeAmount.ToString(inv)},
                    AreaFeeTaxAmount = {areaFeeTaxAmount.ToString(inv)},
                    ServiceFee = {(serviceFee + extraFeeAmount).ToString(inv)},
                    ServiceFeeTaxAmount = {(serviceFeeTaxAmount + extraFeeTaxAmount).ToString(inv)},
                    AutoGratuityAmount = {autoGratuityAmount.ToString(inv)},
                    AutoGratuityTaxAmount = {autoGratuityTaxAmount.ToString(inv)}
                WHERE SaleReceiptPaymentDetailID = {newPaymentId}");

            // ---- credit-card fields on the new payment ----
            if (isCreditCard)
            {
                string cardNumber = string.IsNullOrWhiteSpace(req.CardNumber)
                    ? "xxxx0000"
                    : (req.CardNumber.Contains("xxxx") ? req.CardNumber : "xxxx" + req.CardNumber);
                AppSession.ProcessNonQuery(true, $@"
                    UPDATE dbo.SaleReceiptPaymentDetails SET
                        CreditCardTransactionID = '{newTransactionId}',
                        CreditCardTransactionCardNumber = '{cardNumber.Replace("'", "''")}',
                        CreditCardTransactionType = 'Sale_Settled',
                        CreditCardRefNo = '{(req.ReferenceNumber ?? "").Replace("'", "''")}',
                        CreditCardAuthCode = '{(req.AuthCode ?? "").Replace("'", "''")}',
                        CreditCardCardType = '{(req.CardType ?? "").ToUpper().Replace("'", "''")}',
                        CreditCardTransactionMerchantID = '{(req.MerchantID ?? "").Replace("'", "''")}'
                    WHERE SaleReceiptPaymentDetailID = {newPaymentId}");
            }

            // ---- finish the ticket (the originally-dead FIN block) ----
            return AppSession.ProcessNonQuery(true, $@"
                UPDATE dbo.SaleReceipts SET
                    PaidAmount = {(paidAmount + _paidAmount).ToString(inv)},
                    SVCFeePaidAmount = {(svcFeePaidAmount + _svcFeeAmount).ToString(inv)},
                    SVCFeeTaxPaidAmount = {(svcFeeTaxPaidAmount + _svcFeeTaxAmount).ToString(inv)},
                    CashDiscountPaidAmount = {(cashDiscountPaidAmount + _cashDiscountAmount).ToString(inv)},
                    CashDiscountTaxPaidAmount = {(cashDiscountTaxPaidAmount + _cashDiscountTaxAmount).ToString(inv)},
                    TransactionStatusID = 'FIN'
                WHERE SaleReceiptID = '{receiptId}'") >= 0;
        }

        // ---- Insert Payment -----------------------------------------------

        // How a card payment method is written, mirroring what the POS stores
        // (PaymentStandardForm2 / CreditCardPaymentProcessingFlat / CFacade).
        public class CardPaymentKind
        {
            public string CreditCardTransactionType;
            public string PaymentCardType;
            public string TenderType;
        }

        // Returns null for methods that are not card payments (Cash, Gift Card...).
        public static CardPaymentKind GetCardPaymentKind(string paymentMethodName)
        {
            switch ((paymentMethodName ?? "").Trim().ToUpperInvariant())
            {
                // The tool only inserts into settled batches, hence Sale_Settled (same as FinishTicket).
                case "CREDIT": return new CardPaymentKind { CreditCardTransactionType = "Sale_Settled", PaymentCardType = "Credit", TenderType = "CreditCard" };
                case "PIN DEBIT": return new CardPaymentKind { CreditCardTransactionType = "Debit", PaymentCardType = "Debit", TenderType = "DebitCard" };
                case "EBT CASH": return new CardPaymentKind { CreditCardTransactionType = "EBT_CASH", PaymentCardType = "EBT", TenderType = "EBT_Cash" };
                // POS leaves PaymentCardType empty for EBT Food.
                case "EBT FOOD": return new CardPaymentKind { CreditCardTransactionType = "EBT", PaymentCardType = "", TenderType = "EBT_Food" };
                default: return null;
            }
        }

        public InsertPaymentInfo GetInsertPaymentInfo(string receiptId)
        {
            var info = new InsertPaymentInfo();
            var sr = AppSession.GetSaleReceiptsByID(receiptId);
            if (sr == null) return info;

            info.Found = true;
            info.SaleReceiptNumber = sr.SaleReceiptNumber;
            info.TransactionStatusID = sr.TransactionStatusID;
            info.CashierID = sr.CashierID;
            info.DateCreated = sr.DateCreated;
            info.CreditCardFeeMode = sr.CreditCardFeeMode;
            info.SVCFeePercent = sr.SVCFeePercent != 0 ? sr.SVCFeePercent : AppSession.GetSVCFeePercent(sr.CreditCardFeeMode);
            info.CashDiscountPercent = sr.CashDiscountPercent != 0
                ? sr.CashDiscountPercent
                : POSConfig.Configuration.ConfigValue.CreditCardConfigValue.CashDiscount;
            info.SVCFeeTaxRate = sr.SVCFeeTaxRate;

            var dt = AppSession.ProcessQuery(true,
                $"SELECT ISNULL(TotalAmount, 0) - ISNULL(PaidAmount, 0) AS Balance FROM dbo.SaleReceipts WHERE SaleReceiptID = '{receiptId}'", "Bal");
            info.Balance = dt != null && dt.Rows.Count > 0 ? AppSession.GetDecimal(dt.Rows[0], "Balance") : 0m;
            return info;
        }

        // Employees that are still enabled, ordered by name.
        public List<EmployeeOption> GetActiveEmployees()
        {
            var list = new List<EmployeeOption>();
            var dt = AppSession.ProcessQuery(true, @"
                SELECT EmployeeID, LTRIM(RTRIM(ISNULL(EmployeeFirstName, '') + ' ' + ISNULL(EmployeeLastName, ''))) AS Name
                FROM dbo.Employees
                WHERE ISNULL(Enable, 0) = 0 AND EmployeeID > 0
                ORDER BY Name", "Emp");
            if (dt == null) return list;
            foreach (DataRow r in dt.Rows)
                list.Add(new EmployeeOption
                {
                    EmployeeID = AppSession.GetInt(r, "EmployeeID"),
                    EmployeeName = AppSession.GetString(r, "Name")
                });
            return list;
        }

        // Merchant fee % the POS would use for this kind of card on this ticket.
        public static decimal GetDefaultMerchantFeePercent(int creditCardFeeMode, string paymentMethodName)
        {
            var kind = GetCardPaymentKind(paymentMethodName);
            if (kind == null) return 0m;
            // POS hard-codes the merchant fee % of EBT Food to 0.
            if (kind.TenderType == "EBT_Food") return 0m;
            return AppSession.GetMerchantFeePercent(creditCardFeeMode, kind.PaymentCardType);
        }

        // Fills the fee amounts of the request from its percents. Shared by the
        // dialog preview and the insert so both always agree.
        //   SVCFeeAmount      = (Tendered - Return) x SVC%
        //   SVCFeeForGratuity = Gratuity x BaseAmountFee%, BaseAmountFee = SVC% / (100 + SVC%) x 100
        //   CashDiscount      = (Tendered - Return) x CashDiscount%  (no gratuity column exists for it)
        //   MerchantFee       = (Tendered - Return + SVCFeeAmount - CashDiscount + Gratuity) x Merchant%
        //   MerchantFeeForTip = Gratuity x Merchant%
        // In tip-only mode there is no amount, so only the gratuity parts remain
        // (MerchantFee then equals MerchantFeeForTip).
        public static void CalculateInsertPaymentFees(InsertPaymentRequest req)
        {
            if (req.TipOnly)
            {
                req.AmountTendered = req.AmountReturn = 0m;
                req.ApplyCashDiscount = req.CashDiscountManual = false;
                if (req.SVCFeeManual) req.SVCFeeAmount = req.SVCFeeTaxAmount = 0m;
            }

            decimal net = req.AmountTendered - req.AmountReturn;
            decimal svcPct = req.ApplySVCFee ? req.SVCFeePercent : 0m;
            decimal cashDiscountPct = req.ApplyCashDiscount ? req.CashDiscountPercent : 0m;
            decimal merchantPct = req.ApplyMerchantFee ? req.MerchantFeePercent : 0m;

            if (!req.ApplySVCFee)
            {
                req.SVCFeeAmount = req.SVCFeeForGratuity = req.SVCFeeTaxAmount = 0m;
            }
            else if (!req.SVCFeeManual)
            {
                req.SVCFeeAmount = svcPct > 0 && net > 0 ? RoundUp(net * svcPct / 100m) : 0m;
                decimal baseAmountFee = svcPct > 0 ? svcPct / (100m + svcPct) * 100m : 0m;
                req.SVCFeeForGratuity = baseAmountFee > 0 && req.Gratuity > 0 ? RoundUp(req.Gratuity * baseAmountFee / 100m) : 0m;
                req.SVCFeeTaxAmount = Math.Round(req.SVCFeeAmount * req.SVCFeeTaxPercent / 100m, 2, MidpointRounding.AwayFromZero);
            }

            if (!req.ApplyCashDiscount)
            {
                req.CashDiscountAmount = req.CashDiscountTaxAmount = 0m;
            }
            else if (!req.CashDiscountManual)
            {
                req.CashDiscountAmount = cashDiscountPct > 0 && net > 0 ? RoundUp(net * cashDiscountPct / 100m) : 0m;
                req.CashDiscountTaxAmount = Math.Round(req.CashDiscountAmount * req.CashDiscountTaxPercent / 100m, 2, MidpointRounding.AwayFromZero);
            }

            decimal merchantBase = net + req.SVCFeeAmount - req.CashDiscountAmount + req.Gratuity;
            if (!req.ApplyMerchantFee)
            {
                req.MerchantFee = req.MerchantFeeForTip = 0m;
            }
            else if (!req.MerchantFeeManual)
            {
                req.MerchantFee = merchantPct > 0 && merchantBase > 0 ? RoundUp(merchantBase * merchantPct / 100m) : 0m;
                req.MerchantFeeForTip = merchantPct > 0 && req.Gratuity > 0 ? RoundUp(req.Gratuity * merchantPct / 100m) : 0m;
            }
            if (req.TipOnly) req.MerchantFee = req.MerchantFeeForTip;
        }

        // Adds one payment line to a ticket. Card methods (Credit / Pin Debit /
        // EBT Cash / EBT Food) also get a TransactionDetail row in the chosen
        // batch, which is then recalculated. The ticket's paid totals are
        // increased by this payment; its status is left unchanged.
        public bool InsertPaymentDetail(string receiptId, InsertPaymentRequest req)
        {
            if (string.IsNullOrEmpty(receiptId) || req == null) return false;

            try
            {
                var sr = AppSession.GetSaleReceiptsByID(receiptId);
                if (sr == null) return false;
                if (req.FinishTicket && sr.TransactionStatusID != "OPER") return false;
                if (req.TipOnly && (sr.TransactionStatusID != "FIN" || req.Gratuity <= 0)) return false;

                CalculateInsertPaymentFees(req);

                var kind = GetCardPaymentKind(req.PaymentMethodName);
                bool isCard = kind != null;
                if (isCard && (string.IsNullOrEmpty(AppSession.MerchantConnectionString) || string.IsNullOrEmpty(req.BatchUnique)))
                    return false;

                var inv = CultureInfo.InvariantCulture;
                string createdDate = req.CreatedDate.ToString("yyyy-MM-dd HH:mm:ss", inv);
                decimal net = req.AmountTendered - req.AmountReturn;
                // The SVC fee / cash discount taxes are only recorded in their own
                // tax columns (payment line + ticket); they do not change any amount.
                decimal cardAmount = net + req.SVCFeeAmount - req.CashDiscountAmount;
                decimal cardTotal = cardAmount + req.Gratuity;

                string cardNumber = string.IsNullOrWhiteSpace(req.TransactionCardNumber)
                    ? "xxxx0000"
                    : (req.TransactionCardNumber.Contains("xxxx") ? req.TransactionCardNumber : "xxxx" + req.TransactionCardNumber);
                string cardType = (req.CardType ?? "").Trim().ToUpper();
                string transactionId = Guid.NewGuid().ToString();
                string transactionNumber = req.TransactionNumber;
                if (isCard && string.IsNullOrWhiteSpace(transactionNumber))
                {
                    var numDt = AppSession.ProcessQuery(false,
                        "SELECT ISNULL(MAX(CAST(Number AS BIGINT)), 0) + 1 AS NextNo FROM dbo.TransactionDetail", "No");
                    transactionNumber = numDt != null && numDt.Rows.Count > 0 ? AppSession.GetString(numDt.Rows[0], "NextNo") : "1";
                    while (transactionNumber.Length < 6) transactionNumber = "0" + transactionNumber;
                }

                int paymentMethodType = 0;
                var pmDt = AppSession.ProcessQuery(true,
                    $"SELECT PaymentMethodType FROM dbo.PaymentMethods WHERE PaymentMethodID = {req.PaymentMethodID}", "PM");
                if (pmDt != null && pmDt.Rows.Count > 0)
                    paymentMethodType = AppSession.GetInt(pmDt.Rows[0], "PaymentMethodType");

                // ---- payment detail (BLogicPOS7) ----
                var pdTemplate = AppSession.ProcessQuery(true, "SELECT TOP 0 * FROM dbo.SaleReceiptPaymentDetails", "PD");
                if (pdTemplate == null || pdTemplate.Columns.Count == 0) return false;

                var idDt = AppSession.ProcessQuery(true,
                    "SELECT ISNULL(MAX(SaleReceiptPaymentDetailID), 0) + 1 AS NextId FROM dbo.SaleReceiptPaymentDetails", "Id");
                long newPaymentId = idDt != null && idDt.Rows.Count > 0 ? AppSession.GetLong(idDt.Rows[0], "NextId") : 1;

                var pdMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
                {
                    ["SaleReceiptPaymentDetailID"] = Q(newPaymentId.ToString(inv)),
                    ["SaleReceiptID"] = Q(receiptId),
                    ["PaymentMethodID"] = Q(req.PaymentMethodID.ToString(inv)),
                    ["AmountTendered"] = Q(req.AmountTendered.ToString(inv)),
                    ["AmountReturn"] = Q(req.AmountReturn.ToString(inv)),
                    ["CreatedDate"] = Q(createdDate),
                    ["IsDeposit"] = "'0'",
                    ["GiftExtra"] = "'0'",
                    ["Cashier"] = Q(req.EmployeeID.ToString(inv)),
                    ["EmployeePaidID"] = Q(req.EmployeeID.ToString(inv)),
                    ["ShiftID"] = string.IsNullOrWhiteSpace(sr.ShiftID) ? "null" : Q(sr.ShiftID),
                    ["GiftCardCode"] = "null",
                    ["CurrencyAmount"] = "'0'",
                    ["CurrencyID"] = "'1'",
                    ["PaymentReceiptID"] = "null",
                    ["PayByAccuPoint"] = "null",
                    ["PaymentType"] = "'0'",
                    ["Gratuity"] = Q(req.Gratuity.ToString(inv)),
                    ["CreditCardTransactionID"] = isCard ? Q(transactionId) : "null",
                    ["CreditCardTransactionNumber"] = isCard ? Q(transactionNumber) : "null",
                    ["CreditCardTransactionCardNumber"] = isCard ? Q(cardNumber) : "null",
                    ["CreditCardTransactionType"] = isCard ? Q(kind.CreditCardTransactionType) : "null",
                    ["CreditCardRefNo"] = isCard ? Q(req.RefNo) : "null",
                    ["CreditCardAuthCode"] = isCard ? Q(req.AuthCode) : "null",
                    ["CreditCardCardType"] = isCard ? Q(cardType) : "null",
                    ["CreditCardTransactionMerchantID"] = isCard ? Q(req.MerchantID) : "null",
                    ["PaymentCardType"] = isCard ? Q(kind.PaymentCardType) : "''",
                    ["PaidServiceFee"] = "'0'",
                    ["PaidPayout"] = "'0'",
                    ["SVCFeeAmount"] = Q(req.SVCFeeAmount.ToString(inv)),
                    ["SVCFeeForGratuity"] = Q(req.SVCFeeForGratuity.ToString(inv)),
                    ["SVCFeeForServiceCharge"] = "'0'",
                    ["CashDiscountAmount"] = Q(req.CashDiscountAmount.ToString(inv)),
                    ["MerchantFee"] = Q(req.MerchantFee.ToString(inv)),
                    ["MerchantFeeForTip"] = Q(req.MerchantFeeForTip.ToString(inv)),
                    ["MerchantFeePercent"] = Q((req.ApplyMerchantFee ? req.MerchantFeePercent : 0m).ToString(inv)),
                    ["PartyServiceFeeAmount"] = "'0'",
                    ["SVCFeeForPartyServiceFeeAmount"] = "'0'",
                    ["AreaFeeAmount"] = "'0'",
                    ["SVCFeeForAreaFeeAmount"] = "'0'",
                    ["ServiceFee"] = "'0'",
                    ["SVCFeeForServiceFee"] = "'0'",
                    ["MerchantFeeForServiceFee"] = "'0'",
                    ["MerchantFeeForAreaFeeAmount"] = "'0'",
                    ["MerchantFeeForPartyServiceFeeAmount"] = "'0'",
                    ["CardHolderName"] = Q(req.CardHolderName),
                    ["CardBalance"] = "'0'",
                    ["AutoGratuityAmount"] = "'0'",
                    ["SVCFeeForAutoGratuityAmount"] = "'0'",
                    ["MerchantFeeForAutoGratuityAmount"] = "'0'",
                    ["IsDecline"] = "'0'",
                    ["IsDepositPayment"] = "'0'",
                    ["SVCFeeTaxAmount"] = Q(req.SVCFeeTaxAmount.ToString(inv)),
                    ["CashDiscountTaxAmount"] = Q(req.CashDiscountTaxAmount.ToString(inv)),
                    ["ServiceFeeTaxAmount"] = "'0'",
                    ["AreaFeeTaxAmount"] = "'0'",
                    ["PartyServiceFeeTaxAmount"] = "'0'",
                    ["AutoGratuityTaxAmount"] = "'0'",
                    ["TaxAmountByPaymentModeTaxCustomFee"] = "'0'",
                    ["IsCalculateTaxCustomFee"] = req.SVCFeeTaxAmount > 0 ? "1" : "0",
                    ["CoinDiscountAmount"] = "'0'",
                    ["PaidHouseAccount"] = "'0'",
                    ["MerchantFeeForServiceFeeTaxAmount"] = "'0'",
                    ["MerchantFeeForAreaFeeTaxAmount"] = "'0'",
                    ["MerchantFeeForPartyServiceFeeTaxAmount"] = "'0'",
                    ["MerchantFeeForAutoGratuityTaxAmount"] = "'0'"
                };

                string pdInsert = "SET IDENTITY_INSERT dbo.SaleReceiptPaymentDetails ON; "
                    + BuildColumnInsert("dbo.SaleReceiptPaymentDetails", pdTemplate, pdMap, false)
                    + " SET IDENTITY_INSERT dbo.SaleReceiptPaymentDetails OFF;";
                if (AppSession.ProcessNonQuery(true, pdInsert) <= 0) return false;

                // ---- merchant transaction (card methods only) ----
                if (isCard)
                {
                    string safeBatch = req.BatchUnique.Replace("'", "''");
                    var template = AppSession.ProcessQuery(false,
                        $"SELECT TOP 1 * FROM dbo.TransactionDetail WHERE BatchUnique = '{safeBatch}' ORDER BY [Date] DESC", "Tran");
                    if (template == null || template.Columns.Count == 0)
                        template = AppSession.ProcessQuery(false, "SELECT TOP 0 * FROM dbo.TransactionDetail", "Tran");

                    string terminalIp = template != null && template.Rows.Count > 0 ? AppSession.GetString(template.Rows[0], "TerminalIpAddress") : "";
                    string terminalName = template != null && template.Rows.Count > 0 ? AppSession.GetString(template.Rows[0], "TerminalName") : "";
                    string employeeName = req.EmployeeName ?? "";
                    decimal svcFeeType = GetConfigDecimal("CreditCardConfigValue.SVCFeeType");

                    var map = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
                    {
                        ["Address"] = "''",
                        ["ID"] = Q(transactionId),
                        ["Number"] = Q(transactionNumber),
                        ["Date"] = Q(createdDate),
                        ["TranType"] = "'Sale'",
                        ["TenderType"] = Q(kind.TenderType),
                        ["PaymentCardType"] = Q(kind.PaymentCardType),
                        ["CardNumber"] = Q(cardNumber),
                        ["CardNumberMasked"] = Q(cardNumber),
                        ["CardType"] = Q(cardType),
                        ["Result"] = "'Approved'",
                        ["TextResponse"] = "'OK'",
                        ["CaptureStatus"] = "''",
                        ["InvoiceNo"] = Q(sr.InvoiceNum),
                        ["CustomerName"] = "''",
                        ["AuthCode"] = Q(req.AuthCode),
                        ["RefNo"] = Q(req.RefNo),
                        ["ExpDate"] = Q(req.ExpDate),
                        ["CCEntry"] = Q(req.CCEntry),
                        ["BankApprovalCode"] = Q(req.BankApprovalCode),
                        ["BankTransactionID"] = Q(req.BankTransactionID),
                        ["AID"] = Q(req.AID),
                        ["AuthorizeAmount"] = Q(cardTotal.ToString(inv)),
                        ["OriginalAmount"] = Q(cardTotal.ToString(inv)),
                        ["Total"] = Q(cardTotal.ToString(inv)),
                        ["Amount"] = Q(cardAmount.ToString(inv)),
                        ["Gratuity"] = Q(req.Gratuity.ToString(inv)),
                        ["OriginalTip"] = Q(req.Gratuity.ToString(inv)),
                        ["BatchID"] = Q(req.BatchID),
                        ["BatchTotal"] = "'0'",
                        ["BatchStatus"] = "'Closed'",
                        ["Description"] = "'Success'",
                        ["ReturnCode"] = "'000000'",
                        ["MerchantID"] = Q(req.MerchantID),
                        ["RegisterID"] = Q(req.MerchantID),
                        ["MerchantAccount"] = Q(req.MerchantID),
                        ["UserID"] = Q(req.EmployeeID.ToString(inv)),
                        ["ServerID"] = Q(req.EmployeeID.ToString(inv)),
                        ["Cashier"] = Q(employeeName),
                        ["ServerName"] = Q(employeeName),
                        ["TrackNo"] = "''",
                        ["IsTipInLine"] = "0",
                        ["TranStatus"] = "'ACTIVE'",
                        ["TerminalName"] = Q(terminalName),
                        ["WorkstationID"] = Q(sr.WorkstationID),
                        ["WorkstationName"] = Q(sr.WorkstationName),
                        ["IsChecked"] = "'1'",
                        ["OrderID"] = Q(sr.SaleReceiptOrder),
                        ["TicketOrder"] = Q(sr.SaleReceiptOrder),
                        ["TicketNumber"] = Q(sr.SaleReceiptNumber),
                        ["SVCFee"] = Q(req.SVCFeeAmount.ToString(inv)),
                        ["SVCFeeForGratuity"] = Q(req.SVCFeeForGratuity.ToString(inv)),
                        ["SVCFeeTaxAmount"] = "'0'",
                        ["SVCFeeType"] = Q(svcFeeType.ToString(inv)),
                        ["BatchUnique"] = Q(req.BatchUnique),
                        ["TerminalIpAddress"] = Q(terminalIp),
                        ["MerchantFee"] = Q(req.MerchantFee.ToString(inv)),
                        ["MerchantFeeForTip"] = Q(req.MerchantFeeForTip.ToString(inv)),
                        ["MerchantFeePercent"] = Q((req.ApplyMerchantFee ? req.MerchantFeePercent : 0m).ToString(inv)),
                        ["IsTryVoidOpenTab"] = "'0'",
                        ["AmountCaptured"] = "'-1'",
                        ["TipCaptured"] = "'-1'",
                        ["IsPhone"] = "'0'",
                        ["CreditCardInfo"] = "null",
                        ["AdjustTipInformation"] = "null"
                    };

                    string merchantInsert = BuildColumnInsert("dbo.TransactionDetail", template, map, false);
                    if (AppSession.ProcessNonQuery(false, merchantInsert) <= 0)
                    {
                        // The two DBs cannot share a transaction: undo the POS7 row.
                        AppSession.ProcessNonQuery(true,
                            $"DELETE FROM dbo.SaleReceiptPaymentDetails WHERE SaleReceiptPaymentDetailID = {newPaymentId}");
                        return false;
                    }

                    RecalculateMerchantBatch(req.BatchUnique);
                }

                // ---- tip distribution ----
                if (req.Gratuity > 0)
                    UpsertPaymentRoleDetail(receiptId, newPaymentId, req.PaymentMethodID, paymentMethodType,
                        req.EmployeeID, req.CreatedDate, req.Gratuity, req.SVCFeeForGratuity, req.MerchantFeeForTip, req.Role);

                // ---- ticket paid totals ----
                AppSession.ProcessNonQuery(true, $@"
                    UPDATE dbo.SaleReceipts SET
                        PaidAmount = ISNULL(PaidAmount, 0) + {net.ToString(inv)},
                        SVCFeePaidAmount = ISNULL(SVCFeePaidAmount, 0) + {req.SVCFeeAmount.ToString(inv)},
                        SVCFeeTaxPaidAmount = ISNULL(SVCFeeTaxPaidAmount, 0) + {req.SVCFeeTaxAmount.ToString(inv)},
                        CashDiscountPaidAmount = ISNULL(CashDiscountPaidAmount, 0) + {req.CashDiscountAmount.ToString(inv)},
                        CashDiscountTaxPaidAmount = ISNULL(CashDiscountTaxPaidAmount, 0) + {req.CashDiscountTaxAmount.ToString(inv)}{(req.FinishTicket ? @",
                        TransactionStatusID = 'FIN'" : "")}
                    WHERE SaleReceiptID = '{receiptId}';
                    IF COL_LENGTH('dbo.SaleReceipts', 'FinalGratuity') IS NOT NULL
                        EXEC('UPDATE dbo.SaleReceipts SET FinalGratuity = (SELECT ISNULL(SUM(Gratuity), 0)
                              FROM dbo.SaleReceiptPaymentDetails WHERE SaleReceiptID = ''{receiptId}'')
                              WHERE SaleReceiptID = ''{receiptId}''');");

                if (req.FinishTicket)
                {
                    AppSession.CorrectDataSaleReceiptPaymentDetails(receiptId);
                }

                AppLogger.Info($"InsertPaymentDetail: payment {newPaymentId} ({req.PaymentMethodName}) added to ticket {sr.SaleReceiptNumber}"
                    + (isCard ? $", transaction {transactionNumber} in batch {req.BatchID}" : "")
                    + (req.FinishTicket ? ", ticket set to FIN" : "")
                    + (req.TipOnly ? $", tip only {req.Gratuity.ToString(inv)}" + (req.Role != null ? $", role {req.Role.RolesID}" : "") : ""));
                return true;
            }
            catch (Exception ex)
            {
                AppLogger.Error("InsertPaymentDetail failed", ex);
                return false;
            }
        }

        private string GetCashierName(string cashierId)
        {
            var dt = AppSession.ProcessQuery(true, $@"
                SELECT EmployeeFirstName + ' ' + EmployeeLastName AS Name
                FROM dbo.Employees WHERE EmployeeID = '{cashierId}'", "Emp");
            return dt != null && dt.Rows.Count > 0 ? AppSession.GetString(dt.Rows[0], "Name") : "";
        }

        // Inserts a brand-new credit-card transaction row into the Merchant
        // TransactionDetail table from values entered by hand (not tied to any
        // ticket). Only the fields the user supplies are meaningful; the rest are
        // filled with the same safe defaults the payment flow uses so the row is
        // a valid "Sale". Returns true when exactly one row was inserted.
        public bool InsertTransactionDetail(TransactionDetailEntity req)
        {
            try
            {
                if (string.IsNullOrEmpty(AppSession.MerchantConnectionString))
                    return false;

                // Schema-only template (0 rows) so the column list resolves even
                // when the table is empty.
                var template = AppSession.ProcessQuery(false,
                    "SELECT TOP 0 * FROM dbo.TransactionDetail", "Tran");
                if (template == null || template.Columns.Count == 0)
                    return false;

                var inv = CultureInfo.InvariantCulture;

                // Next sequential Number, 6-digit zero-padded (matches the payment flow).
                var numDt = AppSession.ProcessQuery(false,
                    "SELECT ISNULL(MAX(CAST(Number AS BIGINT)), 0) + 1 AS NextNo FROM dbo.TransactionDetail", "No");
                string number = numDt != null && numDt.Rows.Count > 0 ? AppSession.GetString(numDt.Rows[0], "NextNo") : "1";
                while (number.Length < 6) number = "0" + number;

                string createdDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", inv);

                // Mask the entered card number the same way the payment flow does.
                string cardNumber = string.IsNullOrWhiteSpace(req.CardNumber)
                    ? "xxxx0000"
                    : (req.CardNumber.Contains("xxxx") ? req.CardNumber : "xxxx" + req.CardNumber);

                decimal amount = req.Amount;
                decimal gratuity = req.Gratuity;
                decimal total = amount + gratuity;

                var map = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
                {
                    ["ID"] = Q(Guid.NewGuid().ToString()),
                    ["Number"] = Q(number),
                    ["Date"] = Q(createdDate),
                    ["TranType"] = "'Sale'",
                    ["TenderType"] = "'CreditCard'",
                    ["CardNumber"] = Q(cardNumber),
                    ["CardNumberMasked"] = Q(cardNumber),
                    ["CardType"] = Q((req.CardType ?? "").ToUpper()),
                    ["Result"] = "'Approved'",
                    ["TextResponse"] = "'OK'",
                    ["CaptureStatus"] = "''",
                    ["AuthCode"] = Q(req.AuthCode),
                    ["RefNo"] = Q(req.RefNo),
                    ["AuthorizeAmount"] = Q(total.ToString(inv)),
                    ["OriginalAmount"] = Q(total.ToString(inv)),
                    ["Total"] = Q(total.ToString(inv)),
                    ["Amount"] = Q(amount.ToString(inv)),
                    ["Gratuity"] = Q(gratuity.ToString(inv)),
                    ["OriginalTip"] = Q(gratuity.ToString(inv)),
                    ["BatchID"] = Q(req.BatchID),
                    ["BatchTotal"] = "'0'",
                    ["BatchStatus"] = "'Closed'",
                    ["Description"] = "'Success'",
                    ["ReturnCode"] = "'000000'",
                    ["MerchantID"] = Q(req.MerchantID),
                    ["RegisterID"] = Q(req.MerchantID),
                    ["MerchantAccount"] = Q(req.MerchantID),
                    ["TranStatus"] = "'ACTIVE'",
                    ["Address"] = Q(req.TerminalIpAddress),
                    ["TerminalIpAddress"] = Q(req.TerminalIpAddress),
                    // bit columns: '' cannot convert to bit, so set them explicitly.
                    ["IsTipInLine"] = "null",
                    ["IsChecked"] = "'1'",
                    ["IsTryVoidOpenTab"] = "'0'",
                    ["IsPhone"] = "'0'",
                    ["AmountCaptured"] = "'-1'",
                    ["TipCaptured"] = "'-1'",
                    ["SVCFeeType"] = "'1'"
                };

                string sql = BuildColumnInsert("dbo.TransactionDetail", template, map, false);
                int rows = AppSession.ProcessNonQuery(false, sql);
                AppLogger.Info($"InsertTransactionDetail: {rows} row(s) inserted (Number {number})");
                return rows > 0;
            }
            catch (Exception ex)
            {
                AppLogger.Error("InsertTransactionDetail failed", ex);
                return false;
            }
        }

        // Builds an INSERT covering every column of the template table, using
        // map[col] for known columns and '' for the rest (mirrors the old
        // GenerateQueryInsert_* helpers).
        private static string BuildColumnInsert(string table, DataTable template,
            Dictionary<string, string> map, bool unused)
        {
            var cols = template.Columns.Cast<DataColumn>().ToList();
            string colList = string.Join(", ", cols.Select(c => "[" + c.ColumnName.ToUpper() + "]"));
            string valList = string.Join(", ",
                cols.Select(c => map.TryGetValue(c.ColumnName, out var v) ? v : "''"));
            return $"INSERT INTO {table} ({colList}) VALUES ({valList});";
        }

        private static string Q(string value)
        {
            return value == null ? "null" : "'" + value.Replace("'", "''") + "'";
        }

        private decimal CalcMerchantFee(decimal amount, decimal merchantFeeValue)
        {
            if (merchantFeeValue == 0) return 0m;
            return RoundUp(amount * (merchantFeeValue / 100m));
        }

        private decimal CalcSVCFeeForTip(decimal total)
        {
            // SVCFeeType == 1 means the SVC fee is embedded in the total.
            int svcFeeType = (int)GetConfigDecimal("CreditCardConfigValue.SVCFeeType");
            if (svcFeeType != 1) return 0m;
            decimal svcFee = GetConfigDecimal("CreditCardConfigValue.SVCFee");
            return total - RoundUp(total / (1m + svcFee / 100m));
        }

        // Round-half-up to 2 decimals (ported from Utility.RoundUp).
        private static decimal RoundUp(decimal value)
        {
            decimal rounded = Math.Round(value, 2);
            if (rounded >= value) return rounded;
            if (value - rounded >= 0.005m) rounded += 0.01m;
            return rounded;
        }

        // Reads from the AppSession config cache (loaded once at startup).
        private decimal GetConfigDecimal(string configurationName)
        {
            return AppSession.GetConfigDecimal(configurationName);
        }

        private class RoleTipInfo
        {
            public int RolesID;
            public decimal TipRate;
            public decimal TipRateOT;
        }

        private static List<ReceiptTaxLineEntity> MapTaxLines(DataTable dt)
        {
            var lines = new List<ReceiptTaxLineEntity>();
            if (dt == null) return lines;
            foreach (DataRow row in dt.Rows)
                lines.Add(new ReceiptTaxLineEntity
                {
                    TaxRate = AppSession.GetDecimal(row, "TaxRate"),
                    TaxAmount = AppSession.GetDecimal(row, "TaxAmount")
                });
            return lines;
        }
    }
}
