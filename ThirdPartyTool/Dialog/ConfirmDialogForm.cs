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

            DialogLayout.Arrange(this, panel1, lblTitle, lblMessage, DialogKind.Confirm, btnYes, btnNo);
            // Enter does not confirm: these dialogs guard void/close/refund actions.
            DialogLayout.BindKeys(this, DialogResult.No, null);
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
