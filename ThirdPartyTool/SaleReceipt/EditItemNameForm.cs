using System;
using System.Windows.Forms;

namespace ThirdPartyTool.SaleReceipt
{
    public partial class EditItemNameForm : Form
    {
        public string ItemName { get => txtItemName.Text; set => txtItemName.Text = value; }

        public EditItemNameForm()
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
