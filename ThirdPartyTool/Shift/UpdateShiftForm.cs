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
