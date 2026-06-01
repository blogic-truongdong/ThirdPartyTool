using System;
using System.Windows.Forms;

namespace BLogicPOS7SupportTool.Login
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();

            btnCancel.Click += BtnCancel_Click;
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
#if DEBUG
            DialogResult = DialogResult.OK;
            Close();
            return;
#endif
            if (txtUsername.Text == "admin" && txtPassword.Text == "8559")
            {
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                lblError.Visible = true;
                txtPassword.Clear();
                txtPassword.Focus();
            }
        }
    }
}
