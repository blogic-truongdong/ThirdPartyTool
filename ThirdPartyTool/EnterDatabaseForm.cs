using System;
using System.Windows.Forms;
using ThirdPartyTool.Dialog;

namespace ThirdPartyTool
{
    public partial class EnterDatabaseForm : Form
    {
        public EnterDatabaseForm()
        {
            InitializeComponent();

            btnYes.Click += BtnYes_Click;

            pnlBLogicPOS7.Click += PnlBLogicPOS7_Click;
            pnlMerchant.Click += PnlMerchant_Click;
            btnCancel.Click += BtnCancel_Click;
            this.Load += EnterDatabaseForm_Load;
        }

        private void BtnCancel_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void EnterDatabaseForm_Load(object sender, System.EventArgs e)
        {
#if DEBUG
            txtBLogicPOS7.Text = "BLogicPOS7_Testing";
#endif
            var time = DateTime.Now.ToShortTimeString();

            txtFocus.Focus();
        }

        private void PnlMerchant_Click(object sender, System.EventArgs e)
        {
            txtMerchant.Focus();
        }

        private void PnlBLogicPOS7_Click(object sender, System.EventArgs e)
        {
            txtBLogicPOS7.Focus();
        }

        private void BtnYes_Click(object sender, System.EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBLogicPOS7.Text))
            {
                InformationDialogForm informationDialog = new InformationDialogForm("Database BLogicPOS7 can't be empty.");
                txtBLogicPOS7.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtMerchant.Text))
            {
                InformationDialogForm informationDialog = new InformationDialogForm("Database Merchant can't be empty.");
                txtMerchant.Focus();
                return;
            }

            DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
