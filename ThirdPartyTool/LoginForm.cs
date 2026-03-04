using BLogicSystems.XPos.ClientUtilities;
using System;
using System.Windows.Forms;
using ThirdPartyTool.Dialog;

namespace ThirdPartyTool
{
    public partial class LoginForm : Form
    {
        string userName = "admin";
        string password = "8559";

        public LoginForm()
        {
            InitializeComponent();

            btnOK.Click += BtnLogin_Click;

            btnCancel.Click += BtnCancel_Click;

            this.Load += LoginForm_Load;

            this.KeyPress += LoginForm_KeyPress;
        }

        private void LoginForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            // check if the Enter key is pressed
            if (e.KeyChar == (char)Keys.Enter)
            {
                BtnLogin_Click(sender, e);
            }
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
#if DEBUG
            txtUserName.Text = userName;
            txtPassword.Text = password;
#else
            txtUserName.Text = "";
            txtPassword.Text = "";
#endif

            txtUserName.Focus();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtUserName.Text))
                {
                    InformationDialogForm form = new InformationDialogForm("Please enter your username!");
                    BLogicMessageBox.ShowPopUpFormWithDIMBackground(form);
                    txtUserName.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtPassword.Text))
                {
                    InformationDialogForm form = new InformationDialogForm("Please enter your password!");
                    BLogicMessageBox.ShowPopUpFormWithDIMBackground(form);
                    txtPassword.Focus();
                    return;
                }

                if (txtPassword.Text != userName && txtPassword.Text != password)
                {
                    InformationDialogForm form = new InformationDialogForm("The username or password is incorrect");
                    BLogicMessageBox.ShowPopUpFormWithDIMBackground(form);
                    txtPassword.Focus();
                    return;
                }

                DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                BLogicLogger.Exception("BtnLogin_Click in LoginForm", ex);
            }
        }

    }
}
