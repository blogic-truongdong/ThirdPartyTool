using System;
using System.Drawing;
using System.Windows.Forms;

namespace ThirdPartyTool.SaleReceipt
{
    public partial class ReceiptInfoControl : UserControl
    {
        public string SaleReceiptID = string.Empty;
        public string TransactionStatusID = string.Empty;
        public string SaleReceiptNumber = string.Empty;

        public event EventHandler CloseReceiptInfoEvent;

        private static readonly Color Border = Color.FromArgb(224, 224, 224);
        private static readonly Color BorderHover = Color.FromArgb(42, 108, 228);

        public ReceiptInfoControl()
        {
            InitializeComponent();

            // Every part of the row picks the ticket and shows the hover border.
            foreach (Control control in new Control[] { pnlContainer, flpTitle, flpDate, tlpAmounts,
                lblTicketNumberText, lblTicketNumberValue, lblStatusText, lblStatusValue, lblTotalText, lblTotalValue,
                lblTipText, lblTipValue, lblFinalTotalText, lblFinalTotalValue, lblDateText, lblDateValue })
            {
                control.Click += PnlContainer_Click;
                control.Cursor = Cursors.Hand;
                control.MouseEnter += (s, e) => SetHover(true);
                control.MouseLeave += (s, e) =>
                {
                    if (!pnlContainer.ClientRectangle.Contains(pnlContainer.PointToClient(Cursor.Position)))
                        SetHover(false);
                };
            }

            lblStatusValue.TextChanged += (s, e) => ColorStatus();
        }

        private void SetHover(bool hover)
        {
            pnlContainer.BorderColor = hover ? BorderHover : Border;
            pnlContainer.Invalidate();
        }

        /// <summary>Tints the status pill from its text (Open, Finished, Voided, ...).</summary>
        private void ColorStatus()
        {
            string status = (lblStatusValue.Text ?? "").ToUpperInvariant();
            Color back, fore;
            switch (status)
            {
                case "OPEN":
                    back = Color.FromArgb(232, 240, 254); fore = Color.FromArgb(42, 108, 228); break;
                case "FINISHED":
                    back = Color.FromArgb(220, 252, 231); fore = Color.FromArgb(22, 101, 52); break;
                case "VOIDED":
                case "CANCELLED":
                    back = Color.FromArgb(254, 226, 226); fore = Color.FromArgb(185, 28, 28); break;
                case "RETURNED":
                    back = Color.FromArgb(255, 243, 205); fore = Color.FromArgb(133, 77, 14); break;
                default:
                    back = Color.FromArgb(238, 238, 238); fore = Color.FromArgb(97, 97, 97); break;
            }
            lblStatusValue.BackColor = back;
            lblStatusValue.ForeColor = fore;
            if (lblStatusValue.Text != status)
                lblStatusValue.Text = status; // re-enters once; the second pass changes nothing
        }

        private void PnlContainer_Click(object sender, EventArgs e)
        {
            CloseReceiptInfoEvent?.Invoke(this, EventArgs.Empty);
        }
    }
}
