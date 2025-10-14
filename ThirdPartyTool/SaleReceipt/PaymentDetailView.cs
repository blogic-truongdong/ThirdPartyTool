using System;
using System.Windows.Forms;

namespace ThirdPartyTool.SaleReceipt
{
    public partial class PaymentDetailView : UserControl
    {
        public decimal OldGratuity = 0;
        public string SaleReceiptID = string.Empty;
        public string SaleReceiptPaymentDetailID = string.Empty;

        public event EventHandler UpdateTipEvent = null;

        public PaymentDetailView()
        {
            InitializeComponent();

            btnUpdate.Click += BtnUpdate_Click;
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {

        }
    }
}
