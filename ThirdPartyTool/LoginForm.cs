using System;
using System.Windows.Forms;

namespace ThirdPartyTool
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();

            btn0.Click += Btn_Click;
            btn1.Click += Btn_Click;
            btn2.Click += Btn_Click;
            btn3.Click += Btn_Click;
            btn4.Click += Btn_Click;
            btn5.Click += Btn_Click;
            btn6.Click += Btn_Click;
            btn7.Click += Btn_Click;
            btn8.Click += Btn_Click;
            btn9.Click += Btn_Click;

            btnBack.Click += BtnBack_Click;
            btnClear.Click += BtnClear_Click;
            btnLogin.Click += BtnLogin_Click;

            TxtInputID.KeyUp += TxtInputID_KeyUp;
        }

        private void TxtInputID_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BtnLogin_Click(null, null);
            }
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(TxtInputID.Text))
                {
                    lblIncorrectPassword.Visible = false;
                    return;
                }

                if (TxtInputID.Text == "8559")
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    lblIncorrectPassword.Visible = true;
                }
            }
            catch (Exception ex)
            {
                BLogicLogger.Exception("BtnLogin_Click in LoginForm", ex);
            }
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            TxtInputID.Text = string.Empty;
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            try
            {
                int len = TxtInputID.TextLength;

                if (len == 1)
                {
                    TxtInputID.Text = string.Empty;
                    return;
                }

                TxtInputID.SelectionStart = len - 1;
                TxtInputID.SelectionLength = 1;
                TxtInputID.SelectedText = string.Empty;
                TxtInputID.SelectionStart = TxtInputID.TextLength;
                TxtInputID.SelectionLength = 0;
            }
            catch (Exception ex)
            {
                BLogicLogger.Exception("BtnBack_Click in LoginForm", ex);
            }
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            try
            {
                Label btn = sender as Label;
                TxtInputID.AppendText(btn.Text);
            }
            catch (Exception ex)
            {
                BLogicLogger.Exception("Btn_Click in LoginForm", ex);
            }
        }
    }
}
