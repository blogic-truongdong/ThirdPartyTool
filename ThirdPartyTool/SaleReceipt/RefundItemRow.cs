using BLogicSystems.XPos.Utilites;
using Microsoft.Practices.CompositeUI.Utility;
using System;
using System.Drawing;
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

        public event EventHandler<DataEventArgs<RequestModel>> SelectedItemChanges;
        public RefundItemRow()
        {
            InitializeComponent();

            txtQuantity.TextChanged += TxtQuantity_TextChanged;

            // The whole row toggles the selection, not only the name/price.
            this.Click += LblItemName_Click;
            lblItemName.Click += LblItemName_Click;
            lblPrice.Click += LblItemName_Click;
            txtQuantity.Click += LblItemName_Click;
            chkCheck.Click += LblItemName_Click;
        }

        /// <summary>Flips the selection exactly as a click on the row would.</summary>
        public void ToggleSelection()
        {
            LblItemName_Click(this, EventArgs.Empty);
        }

        private void LblItemName_Click(object sender, EventArgs e)
        {
            IsSelectedItem = !IsSelectedItem;

            chkCheck.Checked = IsSelectedItem;

            this.BackColor = IsSelectedItem ? SelectedBackColor : Color.White;

            RequestModel requestModel = new RequestModel();
            requestModel.GroupID = GroupID;
            requestModel.IsSelectedItem = IsSelectedItem;
            SelectedItemChanges?.Invoke(null, new DataEventArgs<RequestModel>(requestModel));
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

        private void TxtQuantity_TextChanged(object sender, EventArgs e)
        {

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
            this.lblPrice.Text = price == 0 || quantity == 0 ? "" : Utility.ConvertDecimalToMoneyString(price * quantity);
            this.txtQuantity.Text = quantity.ToString("0.##");
        }
    }
}
