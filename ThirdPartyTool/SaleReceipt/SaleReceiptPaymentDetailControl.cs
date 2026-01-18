using Microsoft.Practices.CompositeUI.Utility;
using System;
using System.Windows.Forms;

namespace ThirdPartyTool.SaleReceipt
{
    public partial class SaleReceiptPaymentDetailControl : UserControl
    {
        public bool IsSelected = false;
        public string SaleReceiptPaymentDetailID = string.Empty;
        public event EventHandler<DataEventArgs<string>> PaymentDetailSelected;

        public SaleReceiptPaymentDetailControl()
        {
            InitializeComponent();

            lblPaymentType.Click += LblPaymentType_Click;
            lblPaymentTypeValue.Click += LblPaymentType_Click;
            lblTipText.Click += LblPaymentType_Click;
            lblTipValue.Click += LblPaymentType_Click;
            lblTotal.Click += LblPaymentType_Click;
            lblTotalValue.Click += LblPaymentType_Click;
            lblFinalTotalText.Click += LblPaymentType_Click;
            lblFinalTotalValue.Click += LblPaymentType_Click;
            pnlContainer.Click += LblPaymentType_Click;
        }

        private void LblPaymentType_Click(object sender, System.EventArgs e)
        {
            PaymentDetailSelected?.Invoke(this, new DataEventArgs<string>(SaleReceiptPaymentDetailID));
        }
    }
}
