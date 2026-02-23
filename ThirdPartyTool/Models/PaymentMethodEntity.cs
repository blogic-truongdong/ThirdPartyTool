namespace ThirdPartyTool.Models
{
    public class PaymentMethodEntity
    {
        public int PaymentMethodID { get; set; }

        public string PaymentMethodName { get; set; }

        public int PaymentMethodType { get; set; }

        public bool Enable { get; set; }

        public bool IsNontaxable { get; set; }

        public bool OpenCashDrawer { get; set; }

        public int CashAdjust { get; set; }

        public bool Deposit { get; set; }

        public bool AccountReceivables { get; set; }

        public bool IsDualPrice { get; set; }
    }
}
