using System;
using System.Windows.Forms;

namespace ThirdPartyTool.SaleReceipt
{
    public partial class EditItemPriceForm : Form
    {
        public decimal ItemPrice { get => txtItemPrice.Value; set => txtItemPrice.Value = value; }

        public EditItemPriceForm()
        {
            InitializeComponent();

            btnClose.Click += BtnClose_Click;
            btnOK.Click += BtnOK_Click;
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
