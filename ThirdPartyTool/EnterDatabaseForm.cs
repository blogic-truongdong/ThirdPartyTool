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

            KeyPreview = true;
            KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter) BtnYes_Click(s, e);
                else if (e.KeyCode == Keys.Escape) BtnCancel_Click(s, e);
            };
        }

        private void BtnCancel_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        /// <summary>Opens the form pre-filled with the databases in use (from the login screen).</summary>
        public EnterDatabaseForm(string posDatabase, string merchantDatabase) : this()
        {
            txtBLogicPOS7.Text = posDatabase;
            txtMerchant.Text = merchantDatabase;
            btnYes.Text = "Save";
        }

        private void EnterDatabaseForm_Load(object sender, System.EventArgs e)
        {
            txtBLogicPOS7.Focus();
            txtBLogicPOS7.SelectAll();
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
                informationDialog.ShowDialog();
                txtBLogicPOS7.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtMerchant.Text))
            {
                InformationDialogForm informationDialog = new InformationDialogForm("Database Merchant can't be empty.");
                informationDialog.ShowDialog();
                txtMerchant.Focus();
                return;
            }

            DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
