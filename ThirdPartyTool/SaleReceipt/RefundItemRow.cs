using BLogicSystems.XPos.Utilites;
using Microsoft.Practices.CompositeUI.Utility;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using ThirdPartyTool.Models;

namespace ThirdPartyTool.SaleReceipt
{
    public partial class RefundItemRow : UserControl
    {
        /// <summary>Row tint for items picked for refund (shared with <see cref="RefundModiferRow"/>).</summary>
        public static readonly Color SelectedBackColor = Color.FromArgb(254, 242, 242);
        public static readonly Color SelectedAccent = Color.FromArgb(220, 38, 38);
        public static readonly Color DividerColor = Color.FromArgb(235, 235, 235);

        public bool IsSelectedItem = false;
        public decimal Quantity = 0;
        public decimal Price = 0;
        public decimal ExtendedPrice = 0;
        public decimal TaxAmount = 0;
        public object SaleReceiptDetailID = null;
        public string GroupID = string.Empty;

        /// <summary>How many of <see cref="Quantity"/> are refunded. Equals Quantity unless the user steps it down.</summary>
        public decimal RefundQuantity { get; private set; }

        /// <summary>Only whole quantities of 2 or more can be partly refunded (a 1.5 lb item is all or nothing).</summary>
        public bool CanRefundPartially => Quantity >= 2 && Quantity == decimal.Truncate(Quantity);

        public decimal RefundExtendedPrice => RefundMath.Portion(ExtendedPrice, RefundQuantity, Quantity);
        public decimal RefundTaxAmount => RefundMath.Portion(TaxAmount, RefundQuantity, Quantity);

        public event EventHandler<DataEventArgs<RequestModel>> SelectedItemChanges;
        /// <summary>Raised when − / + changes <see cref="RefundQuantity"/>.</summary>
        public event EventHandler RefundQuantityChanged;

        private readonly StepButton btnMinus = new StepButton(false);
        private readonly StepButton btnPlus = new StepButton(true);

        public RefundItemRow()
        {
            InitializeComponent();

            // The whole row toggles the selection, not only the name/price.
            this.Click += LblItemName_Click;
            lblItemName.Click += LblItemName_Click;
            lblPrice.Click += LblItemName_Click;
            txtQuantity.Click += LblItemName_Click;
            chkCheck.Click += LblItemName_Click;

            // − qty + lives inside the Qty cell; clicks on the buttons do not toggle the row.
            btnMinus.Location = new Point(6, (txtQuantity.Height - btnMinus.Height) / 2);
            btnPlus.Location = new Point(txtQuantity.Width - btnPlus.Width - 6, btnMinus.Top);
            btnPlus.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnMinus.Click += (s, e) => StepRefundQuantity(-1);
            btnPlus.Click += (s, e) => StepRefundQuantity(1);
            txtQuantity.Controls.Add(btnMinus);
            txtQuantity.Controls.Add(btnPlus);
        }

        /// <summary>Flips the selection exactly as a click on the row would.</summary>
        public void ToggleSelection()
        {
            LblItemName_Click(this, EventArgs.Empty);
        }

        private void LblItemName_Click(object sender, EventArgs e)
        {
            IsSelectedItem = !IsSelectedItem;
            RefundQuantity = Quantity; // (re)selecting always starts from the full quantity

            chkCheck.Checked = IsSelectedItem;

            this.BackColor = IsSelectedItem ? SelectedBackColor : Color.White;
            UpdateQuantityDisplay();

            RequestModel requestModel = new RequestModel();
            requestModel.GroupID = GroupID;
            requestModel.IsSelectedItem = IsSelectedItem;
            SelectedItemChanges?.Invoke(null, new DataEventArgs<RequestModel>(requestModel));
        }

        /// <summary>Called by the form when the group is (de)selected through another row.</summary>
        public void SetSelected(bool selected)
        {
            IsSelectedItem = selected;
            RefundQuantity = Quantity;
            chkCheck.Checked = selected;
            BackColor = selected ? SelectedBackColor : Color.White;
            UpdateQuantityDisplay();
        }

        private void StepRefundQuantity(int delta)
        {
            if (!IsSelectedItem || !CanRefundPartially)
                return;

            decimal next = RefundQuantity + delta;
            if (next < 1 || next > Quantity)
                return;

            RefundQuantity = next;
            UpdateQuantityDisplay();
            RefundQuantityChanged?.Invoke(this, EventArgs.Empty);
        }

        private void UpdateQuantityDisplay()
        {
            bool stepper = IsSelectedItem && CanRefundPartially;
            btnMinus.Visible = btnPlus.Visible = stepper;
            btnMinus.Enabled = stepper && RefundQuantity > 1;
            btnPlus.Enabled = stepper && RefundQuantity < Quantity;

            txtQuantity.Text = stepper ? $"{RefundQuantity:0.##} of {Quantity:0.##}" : Quantity.ToString("0.##");
            txtQuantity.ForeColor = stepper ? Color.FromArgb(33, 33, 33) : Color.FromArgb(66, 66, 66);

            decimal shown = IsSelectedItem ? RefundExtendedPrice : ExtendedPrice;
            lblPrice.Text = ExtendedPrice == 0 && Price == 0 ? "" : Utility.ConvertDecimalToMoneyString(shown);
            Invalidate();
        }

        protected override void OnBackColorChanged(EventArgs e)
        {
            base.OnBackColorChanged(e);
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            // Divider above each item groups it with the modifiers listed under it.
            using (Pen pen = new Pen(DividerColor))
                e.Graphics.DrawLine(pen, 0, 0, Width, 0);
            if (IsSelectedItem)
                using (SolidBrush brush = new SolidBrush(SelectedAccent))
                    e.Graphics.FillRectangle(brush, 0, 1, 3, Height - 1);
        }

        public void BindingValue(object saleReceiptDetailID, string itemName, decimal price, decimal quantity,
            decimal extendedPrice, decimal taxAmount, string groupID)
        {
            this.GroupID = groupID;
            this.SaleReceiptDetailID = saleReceiptDetailID;
            this.lblItemName.Text = itemName;
            this.Price = price;
            this.Quantity = quantity;
            this.ExtendedPrice = extendedPrice;
            this.TaxAmount = taxAmount;
            this.RefundQuantity = quantity;
            UpdateQuantityDisplay();
        }

        /// <summary>Small round − / + button drawn by hand so it matches the rest of the dialog.</summary>
        private sealed class StepButton : Control
        {
            private readonly bool plus;
            private bool hover;

            public StepButton(bool plus)
            {
                this.plus = plus;
                Size = new Size(26, 26);
                Cursor = Cursors.Hand;
                Visible = false;
                SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint
                    | ControlStyles.SupportsTransparentBackColor, true);
                BackColor = Color.Transparent;
            }

            protected override void OnMouseEnter(EventArgs e) { hover = true; Invalidate(); base.OnMouseEnter(e); }
            protected override void OnMouseLeave(EventArgs e) { hover = false; Invalidate(); base.OnMouseLeave(e); }
            protected override void OnEnabledChanged(EventArgs e) { Cursor = Enabled ? Cursors.Hand : Cursors.Default; Invalidate(); base.OnEnabledChanged(e); }

            protected override void OnPaint(PaintEventArgs e)
            {
                Graphics g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;
                Rectangle r = new Rectangle(0, 0, Width - 1, Height - 1);

                Color fill = !Enabled ? Color.FromArgb(243, 244, 246) : hover ? Color.FromArgb(254, 226, 226) : Color.White;
                Color line = !Enabled ? Color.FromArgb(229, 231, 235) : Color.FromArgb(220, 38, 38);
                Color glyph = !Enabled ? Color.FromArgb(189, 189, 189) : Color.FromArgb(220, 38, 38);

                using (SolidBrush b = new SolidBrush(fill)) g.FillEllipse(b, r);
                using (Pen p = new Pen(line)) g.DrawEllipse(p, r);
                using (Pen p = new Pen(glyph, 2f))
                {
                    int cx = Width / 2, cy = Height / 2;
                    g.DrawLine(p, cx - 5, cy, cx + 5, cy);
                    if (plus) g.DrawLine(p, cx, cy - 5, cx, cy + 5);
                }
            }
        }
    }

    /// <summary>Splits an amount for a partial-quantity refund (same rounding everywhere: UI, totals and DB).</summary>
    public static class RefundMath
    {
        public static decimal Portion(decimal value, decimal part, decimal whole)
        {
            if (whole == 0 || part >= whole)
                return value;
            return Math.Round(value * part / whole, 2, MidpointRounding.AwayFromZero);
        }
    }
}
