using Microsoft.Practices.CompositeUI.Utility;
using System;
using System.Drawing;
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
            if (IsSelected)
                return;

            IsSelected = true;

            UpdateSelectedItem(true);

            PaymentDetailSelected?.Invoke(this, new DataEventArgs<string>(SaleReceiptPaymentDetailID));
        }

        public void UpdateSelectedItem(bool status)
        {
            IsSelected = status;

            this.pnlContainer.BackColor = status ? ColorTranslator.FromHtml("#233449") : Color.White;
            lblPaymentType.ForeColor = status ? Color.White : Color.Black;
            lblPaymentTypeValue.ForeColor = status ? Color.White : Color.Black;
            lblTotal.ForeColor = status ? Color.White : Color.Black;
            lblTotalValue.ForeColor = status ? Color.White : Color.Black;
            lblTipText.ForeColor = status ? Color.White : Color.Black;
            lblTipValue.ForeColor = status ? Color.White : Color.Black;
            lblFinalTotalText.ForeColor = status ? Color.White : Color.Black;
            lblFinalTotalValue.ForeColor = status ? Color.White : Color.Black;
        }
    }
}
