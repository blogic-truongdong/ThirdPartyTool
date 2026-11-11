using RJCodeAdvance.RJControls;

namespace ThirdPartyTool
{
    partial class LoginForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnOK = new RJCodeAdvance.RJControls.RJButton();
            this.btnCancel = new RJCodeAdvance.RJControls.RJButton();
            this.txtPassword = new RJCodeAdvance.RJControls.RJTextBox();
            this.txtUserName = new RJCodeAdvance.RJControls.RJTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.btnOK);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.txtPassword);
            this.panel1.Controls.Add(this.txtUserName);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(500, 450);
            this.panel1.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.Image = global::ThirdPartyTool.Properties.Resources.icon_password_2;
            this.label2.Location = new System.Drawing.Point(70, 226);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(24, 24);
            this.label2.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.Image = global::ThirdPartyTool.Properties.Resources.icon_user_24x24;
            this.label1.Location = new System.Drawing.Point(70, 128);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(24, 24);
            this.label1.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("BLogic", 20F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(50, 30);
            this.label3.Margin = new System.Windows.Forms.Padding(5);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(400, 39);
            this.label3.TabIndex = 6;
            this.label3.Text = "Welcome Back";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.Color.FromArgb(42, 108, 228);
            this.btnOK.BackgroundColor = System.Drawing.Color.FromArgb(42, 108, 228);
            this.btnOK.BorderColor = System.Drawing.Color.FromArgb(42, 108, 228);
            this.btnOK.BorderRadius = 8;
            this.btnOK.BorderSize = 1;
            this.btnOK.FlatAppearance.BorderSize = 0;
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOK.Font = new System.Drawing.Font("BLogic", 14F, System.Drawing.FontStyle.Bold);
            this.btnOK.ForeColor = System.Drawing.Color.White;
            this.btnOK.Location = new System.Drawing.Point(60, 301);
            this.btnOK.Margin = new System.Windows.Forms.Padding(5);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(380, 44);
            this.btnOK.TabIndex = 5;
            this.btnOK.Text = "Login";
            this.btnOK.TextColor = System.Drawing.Color.White;
            this.btnOK.UseVisualStyleBackColor = false;
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.White;
            this.btnCancel.BackgroundColor = System.Drawing.Color.White;
            this.btnCancel.BorderColor = System.Drawing.Color.FromArgb(189, 189, 189);
            this.btnCancel.BorderRadius = 8;
            this.btnCancel.BorderSize = 1;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("BLogic", 14F, System.Drawing.FontStyle.Bold);
            this.btnCancel.ForeColor = System.Drawing.Color.Black;
            this.btnCancel.Location = new System.Drawing.Point(60, 361);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(5);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(380, 44);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.TextColor = System.Drawing.Color.Black;
            this.btnCancel.UseVisualStyleBackColor = false;
            // 
            // txtPassword
            // 
            this.txtPassword.BackColor = System.Drawing.Color.White;
            this.txtPassword.BorderColor = System.Drawing.Color.FromArgb(189, 189, 189);
            this.txtPassword.BorderFocusColor = System.Drawing.Color.FromArgb(42, 108, 228);
            this.txtPassword.BorderRadius = 8;
            this.txtPassword.BorderSize = 1;
            this.txtPassword.Font = new System.Drawing.Font("BLogic", 14F);
            this.txtPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtPassword.Location = new System.Drawing.Point(60, 217);
            this.txtPassword.Margin = new System.Windows.Forms.Padding(5);
            this.txtPassword.Multiline = false;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Padding = new System.Windows.Forms.Padding(45, 9, 10, 7);
            this.txtPassword.PasswordChar = true;
            this.txtPassword.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.txtPassword.PlaceholderText = "Enter your password";
            this.txtPassword.Size = new System.Drawing.Size(380, 44);
            this.txtPassword.TabIndex = 3;
            this.txtPassword.Texts = "";
            this.txtPassword.UnderlinedStyle = false;
            // 
            // txtUserName
            // 
            this.txtUserName.BackColor = System.Drawing.Color.White;
            this.txtUserName.BorderColor = System.Drawing.Color.FromArgb(189, 189, 189);
            this.txtUserName.BorderFocusColor = System.Drawing.Color.FromArgb(42, 108, 228);
            this.txtUserName.BorderRadius = 8;
            this.txtUserName.BorderSize = 1;
            this.txtUserName.Font = new System.Drawing.Font("BLogic", 14F);
            this.txtUserName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtUserName.Location = new System.Drawing.Point(60, 119);
            this.txtUserName.Margin = new System.Windows.Forms.Padding(5);
            this.txtUserName.Multiline = false;
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Padding = new System.Windows.Forms.Padding(45, 9, 10, 7);
            this.txtUserName.PasswordChar = false;
            this.txtUserName.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.txtUserName.PlaceholderText = "Enter your username";
            this.txtUserName.Size = new System.Drawing.Size(380, 44);
            this.txtUserName.TabIndex = 2;
            this.txtUserName.Texts = "";
            this.txtUserName.UnderlinedStyle = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("BLogic", 12F);
            this.label4.Location = new System.Drawing.Point(56, 91);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 23);
            this.label4.TabIndex = 9;
            this.label4.Text = "Username";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("BLogic", 12F);
            this.label5.Location = new System.Drawing.Point(56, 189);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(78, 23);
            this.label5.TabIndex = 10;
            this.label5.Text = "Password";
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(500, 450);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LoginForm";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private RJButton btnOK;
        private RJButton btnCancel;
        private RJTextBox txtPassword;
        private RJTextBox txtUserName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
    }
}