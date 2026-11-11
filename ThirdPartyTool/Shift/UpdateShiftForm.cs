using System;
using System.Windows.Forms;

namespace ThirdPartyTool.Shift
{
    public partial class UpdateShiftForm : Form
    {
        public UpdateShiftForm()
        {
            InitializeComponent();

            this.btnCancel.Click += BtnCancel_Click;
            this.btnOK.Click += BtnOK_Click;

            // Clicking the box focuses the amount inside it.
            pnlBLogicPOS7.Click += (s, e) => txtOpeningAmount.Focus();
            pnlMerchant.Click += (s, e) => txtClosingAmount.Focus();

            KeyPreview = true;
            KeyDown += UpdateShiftForm_KeyDown;
        }

        /// <summary>Shown under the title, e.g. "Register 1 · Opened 11/11/2026 08:02 AM".</summary>
        public string ShiftDescription
        {
            get { return lblSubtitle.Text; }
            set { if (!string.IsNullOrWhiteSpace(value)) lblSubtitle.Text = value; }
        }

        private void UpdateShiftForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                BtnCancel_Click(sender, e);
            else if (e.KeyCode == Keys.Enter)
                BtnOK_Click(sender, e);
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
