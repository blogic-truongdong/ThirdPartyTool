using System.Windows.Forms;

namespace ThirdPartyTool.Dialog
{
    public partial class InformationDialogForm : Form
    {
        public InformationDialogForm(string message)
        {
            InitializeComponent();

            lblMessage.Text = message;

            btnYes.Click += BtnYes_Click;

            DialogLayout.Arrange(this, panel1, lblTitle, lblMessage, DialogKind.Information, btnYes);
            DialogLayout.BindKeys(this, DialogResult.Yes, DialogResult.Yes);
        }

        private void BtnYes_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.Yes;
            Close();
        }
    }
}