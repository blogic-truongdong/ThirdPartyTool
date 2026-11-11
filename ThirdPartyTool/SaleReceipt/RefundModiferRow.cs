using BLogicSystems.XPos.Utilites;
using Microsoft.Practices.CompositeUI.Utility;
using System;
using System.Drawing;
using System.Windows.Forms;
using ThirdPartyTool.Models;

namespace ThirdPartyTool.SaleReceipt
{
    public partial class RefundModiferRow : UserControl
    {
        public bool IsSelectedItem = false;
        public decimal Quantity = 0;
        public decimal Price = 0;
        public decimal ExtendedPrice = 0;
        public decimal TaxAmount = 0;
        public object SaleReceiptDetailID = null;
        public string GroupID = "";

        // Share of the parent item being refunded (e.g. 2 of 3). Modifiers follow their item.
        public decimal PortionPart { get; private set; } = 1;
        public decimal PortionWhole { get; private set; } = 1;
        public bool IsPartial => PortionPart < PortionWhole;

        public decimal RefundQuantity => IsPartial ? Math.Round(Quantity * PortionPart / PortionWhole, 2, MidpointRounding.AwayFromZero) : Quantity;
        public decimal RefundExtendedPrice => RefundMath.Portion(ExtendedPrice, PortionPart, PortionWhole);
        public decimal RefundTaxAmount => RefundMath.Portion(TaxAmount, PortionPart, PortionWhole);

        public event EventHandler<DataEventArgs<RequestModel>> SelectedItemChanges;
        public RefundModiferRow()
        {
            InitializeComponent();

            this.Click += LblItemName_Click;
            lblItemName.Click += LblItemName_Click;
            lblPrice.Click += LblItemName_Click;
            txtQuantity.Click += LblItemName_Click;
        }

        private void LblItemName_Click(object sender, EventArgs e)
        {
            IsSelectedItem = !IsSelectedItem;

            this.BackColor = IsSelectedItem ? RefundItemRow.SelectedBackColor : Color.White;

            RequestModel requestModel = new RequestModel();
            requestModel.GroupID = GroupID;
            requestModel.IsSelectedItem = IsSelectedItem;
            SelectedItemChanges?.Invoke(null, new DataEventArgs<RequestModel>(requestModel));
        }

        /// <summary>Called by the form when the group is (de)selected or its item's refund quantity changes.</summary>
        public void SetSelected(bool selected, decimal part, decimal whole)
        {
            IsSelectedItem = selected;
            PortionPart = selected ? part : 1;
            PortionWhole = selected ? whole : 1;
            BackColor = selected ? RefundItemRow.SelectedBackColor : Color.White;
            UpdateDisplay();
        }

        private void UpdateDisplay()
        {
            txtQuantity.Text = IsSelectedItem && IsPartial ? $"{RefundQuantity:0.##} of {Quantity:0.##}" : Quantity.ToString("0.##");
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
            if (IsSelectedItem)
                using (SolidBrush brush = new SolidBrush(RefundItemRow.SelectedAccent))
                    e.Graphics.FillRectangle(brush, 0, 0, 3, Height);
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
            UpdateDisplay();
        }
    }
}
