using System;

namespace BLogicPOS7SupportTool.Models
{
    // What the Insert Payment dialog is used for.
    //   Insert: add a payment line, status unchanged
    //   Finish: pay the balance of an OPER ticket and set it to FIN
    //   AddTip: add a tip-only payment line (amount 0) to a FIN ticket
    enum InsertPaymentMode
    {
        Insert,
        Finish,
        AddTip
    }

    // Ticket values the Insert Payment dialog needs to pre-fill its fields.
    class InsertPaymentInfo
    {
        public bool Found { get; set; }
        public string SaleReceiptNumber { get; set; }
        public string TransactionStatusID { get; set; }
        public int CashierID { get; set; }
        public DateTime DateCreated { get; set; }
        public int CreditCardFeeMode { get; set; }

        // Balance still owed on the ticket (TotalAmount - PaidAmount).
        public decimal Balance { get; set; }

        // SVC fee % stamped on the ticket, or the config value when the ticket has none.
        public decimal SVCFeePercent { get; set; }

        // Cash discount % stamped on the ticket, or the config value when the ticket has none.
        public decimal CashDiscountPercent { get; set; }

        // Tax % applied to the SVC fee (SaleReceipts.SVCFeeTaxRate).
        public decimal SVCFeeTaxRate { get; set; }
    }

    class EmployeeOption
    {
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public override string ToString() => EmployeeName;
    }

    // A role of an employee, with the tip rates PaymentRoleDetails gets for it
    // (role rates when the employee uses the role tip pool, else the employee's).
    class RoleOption
    {
        public int RolesID { get; set; }
        public string RolesName { get; set; }
        public decimal TipRate { get; set; }
        public decimal TipRateOT { get; set; }
        public override string ToString() => RolesName;
    }

    class InsertPaymentRequest
    {
        public int PaymentMethodID { get; set; }
        public string PaymentMethodName { get; set; }
        public decimal AmountTendered { get; set; }
        public decimal AmountReturn { get; set; }
        public decimal Gratuity { get; set; }
        public DateTime CreatedDate { get; set; }
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }

        // Finish mode: the ticket (must be OPER) is set to FIN after the insert.
        public bool FinishTicket { get; set; }

        // Add Tip mode: the ticket (must be FIN) only gets a gratuity, so the
        // amount, the SVC fee on amount and the cash discount are all 0.
        public bool TipOnly { get; set; }

        // Role stamped on PaymentRoleDetails; null = resolved from the time clock.
        public RoleOption Role { get; set; }

        // Card fields (Credit / EBT Cash / EBT Food / Pin Debit only)
        public string BatchUnique { get; set; }
        public string BatchID { get; set; }
        public string CardType { get; set; }
        public string TransactionNumber { get; set; }
        public string TransactionCardNumber { get; set; }
        public string RefNo { get; set; }
        public string AuthCode { get; set; }
        public string MerchantID { get; set; }
        public string ExpDate { get; set; }
        public string CCEntry { get; set; }
        public string BankApprovalCode { get; set; }
        public string BankTransactionID { get; set; }
        public string AID { get; set; }
        // SaleReceiptPaymentDetails only; TransactionDetail has no such column.
        public string CardHolderName { get; set; }

        // Fees, already calculated by the dialog from the chosen percents.
        // When a *Manual flag is set, the amounts of that group were typed by
        // the user and are kept as-is instead of being calculated.
        public bool SVCFeeManual { get; set; }
        public bool CashDiscountManual { get; set; }
        public bool MerchantFeeManual { get; set; }
        public bool ApplySVCFee { get; set; }
        public decimal SVCFeePercent { get; set; }
        public decimal SVCFeeAmount { get; set; }
        public decimal SVCFeeForGratuity { get; set; }
        // Tax on the SVC fee / cash discount. Only written to the tax columns of
        // SaleReceiptPaymentDetails and SaleReceipts; no amount includes it.
        public decimal SVCFeeTaxPercent { get; set; }
        public decimal SVCFeeTaxAmount { get; set; }
        public bool ApplyCashDiscount { get; set; }
        public decimal CashDiscountPercent { get; set; }
        public decimal CashDiscountAmount { get; set; }
        public decimal CashDiscountTaxPercent { get; set; }
        public decimal CashDiscountTaxAmount { get; set; }
        public bool ApplyMerchantFee { get; set; }
        public decimal MerchantFeePercent { get; set; }
        public decimal MerchantFee { get; set; }
        public decimal MerchantFeeForTip { get; set; }
    }
}
