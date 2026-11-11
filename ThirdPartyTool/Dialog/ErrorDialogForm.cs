using System.Windows.Forms;

namespace ThirdPartyTool.Dialog
{
    public partial class ErrorDialogForm : Form
    {
        public ErrorDialogForm(string message)
        {
            InitializeComponent();

            lblMessage.Text = message;

            btnYes.Click += BtnYes_Click;

            DialogLayout.Arrange(this, panel1, lblTitle, lblMessage, DialogKind.Error, btnYes);
            DialogLayout.BindKeys(this, DialogResult.Yes, DialogResult.Yes);
        }

        private void BtnYes_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.Yes;
            Close();
        }
    }
}