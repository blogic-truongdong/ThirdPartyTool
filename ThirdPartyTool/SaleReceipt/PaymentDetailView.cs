using System;
using System.Drawing;
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
            pnlSearch.Click += (s, e) => txtGratuity.Focus();
        }

        /// <summary>
        /// Card payments show the card grid; cash and other tenders show only the
        /// method, amount and tip row. The card resizes to fit, so no blank gap is left.
        /// </summary>
        public void ShowCardDetails(bool isCard)
        {
            foreach (Control control in new Control[] { lblCard, lblCardValue, lblTotal, lblTotalValue, lblTypeCaption, lblCardType,
                lblEntryCaption, lblEntryMode, lblApproval, lblApprovalValue, lblBatch, lblBatchValue })
                control.Visible = isCard;

            // Terminal is not filled by the caller today; hide the row while it is empty.
            bool showTerminal = isCard && !string.IsNullOrWhiteSpace(lblTerminalValue.Text);
            lblTerminal.Visible = lblTerminalValue.Visible = showTerminal;

            FitHeight();
        }

        private void FitHeight()
        {
            int gridHeight = tableLayoutPanel1.GetPreferredSize(new Size(tableLayoutPanel1.Width, 0)).Height;
            Height = Padding.Vertical + bPanel_Rounded1.Padding.Vertical
                + gridHeight + pnlGap.Height + pnlDivider.Height + panel1.Height + 2;
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {

        }
    }
}
