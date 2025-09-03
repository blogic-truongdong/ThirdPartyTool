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
        }

        private void BtnYes_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.Yes;
            Close();
        }
    }
}
