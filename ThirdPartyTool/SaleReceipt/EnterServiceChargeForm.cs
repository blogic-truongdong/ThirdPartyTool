using System;
using System.Windows.Forms;

namespace ThirdPartyTool.SaleReceipt
{
    public partial class EnterServiceChargeForm : Form
    {
        public EnterServiceChargeForm()
        {
            InitializeComponent();

            btnCancel.Click += BtnCancel_Click;
            btnOK.Click += BtnOK_Click;

            this.Load += EnterServiceChargeForm_Load;
            this.Shown += EnterServiceChargeForm_Shown;
        }

        private void EnterServiceChargeForm_Shown(object sender, EventArgs e)
        {
            txtAmount.Focus();
        }

        private void EnterServiceChargeForm_Load(object sender, EventArgs e)
        {
            txtAmount.Focus();
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
