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

            pnlPartyGratuity.Click += (s, e) => txtAmount.Focus();
            bPanel_Rounded1.Click += (s, e) => txtTax.Focus();

            KeyPreview = true;
            KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Escape) BtnCancel_Click(s, e);
                else if (e.KeyCode == Keys.Enter) BtnOK_Click(s, e);
            };
        }

        /// <summary>Name of the charge being edited, shown as the title (e.g. "Party Gratuity").</summary>
        public string ChargeName
        {
            set { if (!string.IsNullOrWhiteSpace(value)) lblTitle.Text = value; }
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
