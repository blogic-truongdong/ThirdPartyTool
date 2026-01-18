using BLogicSystems.XPos.Utilites;
using Microsoft.Practices.CompositeUI.Utility;
using System;
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

        public event EventHandler<DataEventArgs<RequestModel>> SelectedItemChanges;
        public RefundModiferRow()
        {
            InitializeComponent();

            txtQuantity.TextChanged += TxtQuantity_TextChanged;

            lblItemName.Click += LblItemName_Click;
            lblPrice.Click += LblItemName_Click;
        }

        private void LblItemName_Click(object sender, EventArgs e)
        {
            IsSelectedItem = !IsSelectedItem;

            this.BackColor = IsSelectedItem ? System.Drawing.Color.Red : System.Drawing.Color.White;

            RequestModel requestModel = new RequestModel();
            requestModel.GroupID = GroupID;
            requestModel.IsSelectedItem = IsSelectedItem;
            SelectedItemChanges?.Invoke(null, new DataEventArgs<RequestModel>(requestModel));
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
