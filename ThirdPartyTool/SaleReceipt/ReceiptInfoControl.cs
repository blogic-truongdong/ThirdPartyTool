using System;
using System.Windows.Forms;

namespace ThirdPartyTool.SaleReceipt
{
    public partial class ReceiptInfoControl : UserControl
    {
        public string SaleReceiptID = string.Empty;
        public string TransactionStatusID = string.Empty;
        public string SaleReceiptNumber = string.Empty;

        public event EventHandler CloseReceiptInfoEvent;

        public ReceiptInfoControl()
        {
            InitializeComponent();

            pnlContainer.Click += PnlContainer_Click;
            lblTicketNumberText.Click += PnlContainer_Click;
            lblTicketNumberValue.Click += PnlContainer_Click;
            lblStatusText.Click += PnlContainer_Click;
            lblStatusValue.Click += PnlContainer_Click;
            lblTotalText.Click += PnlContainer_Click;
            lblTotalValue.Click += PnlContainer_Click;
            lblTipText.Click += PnlContainer_Click;
            lblTipValue.Click += PnlContainer_Click;
            lblFinalTotalText.Click += PnlContainer_Click;
            lblFinalTotalValue.Click += PnlContainer_Click;
            lblDateText.Click += PnlContainer_Click;
            lblDateValue.Click += PnlContainer_Click;
        }

        private void PnlContainer_Click(object sender, EventArgs e)
        {
            CloseReceiptInfoEvent?.Invoke(this, EventArgs.Empty);
        }
    }
}
