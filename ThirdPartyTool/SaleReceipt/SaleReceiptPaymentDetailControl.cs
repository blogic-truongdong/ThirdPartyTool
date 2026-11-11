using Microsoft.Practices.CompositeUI.Utility;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ThirdPartyTool.SaleReceipt
{
    public partial class SaleReceiptPaymentDetailControl : UserControl
    {
        public bool IsSelected = false;
        public string SaleReceiptPaymentDetailID = string.Empty;
        public event EventHandler<DataEventArgs<string>> PaymentDetailSelected;

        private static readonly Color Primary = Color.FromArgb(42, 108, 228);
        private static readonly Color SelectedBack = Color.FromArgb(247, 250, 255);
        private static readonly Color Border = Color.FromArgb(224, 224, 224);
        private static readonly Color RadioIdle = Color.FromArgb(189, 189, 189);

        // BPanel_Rounded fills its background after raising Paint, so the radio is its own child control.
        private readonly Label radio = new Label { Size = new Size(22, 22), Text = "" };

        public SaleReceiptPaymentDetailControl()
        {
            InitializeComponent();

            radio.Location = new Point(14, (pnlContainer.Height - radio.Height) / 2);
            radio.Paint += Radio_Paint;
            pnlContainer.Controls.Add(radio);

            foreach (Control control in new Control[] { pnlContainer, flpBreakdown, lblPaymentType, lblPaymentTypeValue,
                lblTipText, lblTipValue, lblTotal, lblTotalValue, lblFinalTotalText, lblFinalTotalValue, radio })
            {
                control.Click += LblPaymentType_Click;
                control.Cursor = Cursors.Hand;
            }
        }

        /// <summary>Radio indicator on the left: ring when idle, filled dot when selected.</summary>
        private void Radio_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            Rectangle ring = new Rectangle(2, 2, 18, 18);
            using (Pen pen = new Pen(IsSelected ? Primary : RadioIdle, 2))
                g.DrawEllipse(pen, ring);
            if (IsSelected)
            {
                using (SolidBrush dot = new SolidBrush(Primary))
                    g.FillEllipse(dot, ring.X + 5, ring.Y + 5, 8, 8);
            }
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

            Color back = status ? SelectedBack : Color.White;
            pnlContainer.BackColor = back;
            pnlContainer.BackgroundColor = back;
            pnlContainer.BorderColor = status ? Primary : Border;
            pnlContainer.BorderSize = status ? 2 : 1;
            pnlContainer.Invalidate(true);
        }
    }
}
