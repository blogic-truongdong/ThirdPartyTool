using System.Windows.Forms;

namespace ThirdPartyTool.Dialog
{
    public partial class ConfirmDialogForm : Form
    {
        public ConfirmDialogForm(string title, string message)
        {
            InitializeComponent();

            lblTitle.Text = title;
            lblMessage.Text = message;

            btnYes.Click += BtnYes_Click;
            btnNo.Click += BtnNo_Click;
        }

        private void BtnNo_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.No;
            Close();
        }

        private void BtnYes_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.Yes;
            Close();
        }
    }
}
