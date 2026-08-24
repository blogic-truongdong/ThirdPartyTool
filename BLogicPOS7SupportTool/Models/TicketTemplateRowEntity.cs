using System;

namespace BLogicPOS7SupportTool.Models
{
    // Where a template row stands after validation. Only Ready rows are inserted.
    enum TicketTemplateRowState
    {
        Ready,
        Invalid,       // the row itself is unusable (bad date, unknown workstation/item, missing sheet row)
        Inserted,
        Failed
    }

    // One ticket from TemplateInsertSalesData.xlsx. The workbook holds the input on the
    // Templates sheet and the figures worked out from it on one sheet per table, joined
    // by ID. This class carries all of it: nothing here is recalculated, the amounts are
    // whatever the workbook says.
    class TicketTemplateRowEntity
    {
        // ---- selection / status (grid) ------------------------------------
        public bool Selected { get; set; }
        public string Id { get; set; }
        public int LineNumber { get; set; }
        public TicketTemplateRowState State { get; set; }
        public string Message { get; set; }

        public string StateText
        {
            get
            {
                switch (State)
                {
                    case TicketTemplateRowState.Ready: return "Ready";
                    case TicketTemplateRowState.Invalid: return "Invalid";
                    case TicketTemplateRowState.Inserted: return "Inserted";
                    default: return "Failed";
                }
            }
        }

        // ---- sheet "Templates" --------------------------------------------
        public DateTime TransactionDateTime { get; set; }
        public string BankTransactionID { get; set; }
        public string TranType { get; set; }
        public string CardNumber { get; set; }        // masked as it appears in the file
        public string CardType { get; set; }
        public string AuthCode { get; set; }
        public string InvoiceNo { get; set; }
        public string RefNo { get; set; }
        public string TerminalID { get; set; }
        public string WorkstationName { get; set; }
        public string EmployeeCode { get; set; }
        public decimal Amount { get; set; }           // charged on the card, tip excluded
        public decimal Gratuity { get; set; }
        public decimal Total { get; set; }            // charged on the card, tip included
        public decimal SVCFeePercent { get; set; }
        public decimal BaseAmountFee { get; set; }
        public decimal MerchantFeePercent { get; set; }
        public decimal TaxRate { get; set; }
        public string ItemName { get; set; }

        // ---- sheet "SaleReceipts" -----------------------------------------
        public decimal ReceiptTotalAmount { get; set; }   // ticket total, custom fee excluded
        public decimal ReceiptTaxAmount { get; set; }
        public decimal ReceiptSVCFeeAmount { get; set; }
        public decimal ReceiptSVCFeePaidAmount { get; set; }

        // ---- sheet "SaleReceiptDetails" -----------------------------------
        public decimal DetailPrice { get; set; }
        public decimal DetailTaxAmount { get; set; }
        public decimal DetailExtendedPrice { get; set; }

        // ---- sheet "SaleReceiptPaymentDetails" ----------------------------
        public decimal PayAmountTendered { get; set; }
        public decimal PaySVCFeeAmount { get; set; }
        public decimal PaySVCFeeForGratuity { get; set; }
        public decimal PayMerchantFee { get; set; }
        public decimal PayMerchantFeeForTip { get; set; }

        // ---- sheet "TransactionDetail" ------------------------------------
        public decimal TranAmount { get; set; }
        public decimal TranTotal { get; set; }
        public decimal TranSVCFee { get; set; }
        public decimal TranSVCFeeForGratuity { get; set; }
        public decimal TranMerchantFee { get; set; }
        public decimal TranMerchantFeeForTip { get; set; }

        // ---- references resolved from the database ------------------------
        public string WorkstationID { get; set; }
        public string WorkstationCode { get; set; }
        public string StoreID { get; set; }
        public string TerminalIPAddress { get; set; }
        public int EmployeeID { get; set; }
        // The employee's name as the POS writes it ("first last").
        public string EmployeeFullName { get; set; }
        public short RolesID { get; set; }
        public string ItemID { get; set; }
        public string ItemLookupCode { get; set; }
        public string ItemTaxCodeID { get; set; }
        public string ShiftID { get; set; }

        // ---- derived ------------------------------------------------------
        public string CardLast4
        {
            get
            {
                var digits = (CardNumber ?? "").Trim();
                return digits.Length <= 4 ? digits : digits.Substring(digits.Length - 4);
            }
        }

        // ---- filled in once the row has been written ----------------------
        public string SaleReceiptID { get; set; }
        public string TransactionID { get; set; }
        public string TicketNumber { get; set; }

        // ---- display-only projections used by the grid --------------------
        public string DateText { get { return TransactionDateTime.ToString("MM/dd/yyyy HH:mm:ss"); } }
        public string AmountText { get { return Amount.ToString("N2"); } }
        public string GratuityText { get { return Gratuity.ToString("N2"); } }
        public string TotalText { get { return Total.ToString("N2"); } }
        public string TicketTotalText { get { return ReceiptTotalAmount.ToString("N2"); } }
        public string TaxText { get { return ReceiptTaxAmount.ToString("N2"); } }
        public string SVCFeeText { get { return ReceiptSVCFeeAmount.ToString("N2"); } }
        public string MerchantFeeText { get { return TranMerchantFee.ToString("N2"); } }
        public string TaxRateText { get { return TaxRate.ToString("N2"); } }
    }
}
