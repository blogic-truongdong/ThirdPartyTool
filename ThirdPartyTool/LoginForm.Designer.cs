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
            this.picLogo = new System.Windows.Forms.PictureBox();
            this.lblVersion = new System.Windows.Forms.Label();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.lblUserName = new System.Windows.Forms.Label();
            this.txtUserName = new ThirdPartyTool.LoginFieldBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtPassword = new ThirdPartyTool.LoginFieldBox();
            this.lblMessage = new System.Windows.Forms.Label();
            this.btnOK = new ThirdPartyTool.CustomControl.BLabel_Rounded();
            this.dbCard = new ThirdPartyTool.LoginDatabaseCard();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            this.pnlMain.SuspendLayout();
            this.SuspendLayout();
            //
            // pnlMain
            //
            this.pnlMain.BackColor = System.Drawing.Color.White;
            this.pnlMain.Controls.Add(this.picLogo);
            this.pnlMain.Controls.Add(this.lblVersion);
            this.pnlMain.Controls.Add(this.btnClose);
            this.pnlMain.Controls.Add(this.lblTitle);
            this.pnlMain.Controls.Add(this.lblSubtitle);
            this.pnlMain.Controls.Add(this.lblUserName);
            this.pnlMain.Controls.Add(this.txtUserName);
            this.pnlMain.Controls.Add(this.lblPassword);
            this.pnlMain.Controls.Add(this.txtPassword);
            this.pnlMain.Controls.Add(this.lblMessage);
            this.pnlMain.Controls.Add(this.btnOK);
            this.pnlMain.Controls.Add(this.dbCard);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(498, 618);
            this.pnlMain.TabIndex = 1;
            //
            // picLogo
            //
            this.picLogo.Image = global::ThirdPartyTool.Properties.Resources.BlogicSystems_Black;
            this.picLogo.Location = new System.Drawing.Point(56, 30);
            this.picLogo.Name = "picLogo";
            this.picLogo.Size = new System.Drawing.Size(118, 33);
            this.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picLogo.TabStop = false;
            //
            // lblVersion
            //
            this.lblVersion.Font = new System.Drawing.Font("Roboto", 9F);
            this.lblVersion.ForeColor = System.Drawing.Color.FromArgb(156, 163, 175);
            this.lblVersion.Location = new System.Drawing.Point(56, 542);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(388, 20);
            this.lblVersion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // btnClose
            //
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.Image = global::ThirdPartyTool.Properties.Resources.icon_close;
            this.btnClose.Location = new System.Drawing.Point(450, 10);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(38, 38);
            this.btnClose.TabIndex = 10;
            //
            // lblTitle
            //
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Roboto", 22F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(17, 24, 39);
            this.lblTitle.Location = new System.Drawing.Point(52, 80);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Sign in";
            //
            // lblSubtitle
            //
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new System.Drawing.Font("Roboto", 10.5F);
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblSubtitle.Location = new System.Drawing.Point(55, 122);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.TabIndex = 0;
            this.lblSubtitle.Text = "Use your support account to continue.";
            //
            // lblUserName
            //
            this.lblUserName.AutoSize = true;
            this.lblUserName.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblUserName.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81);
            this.lblUserName.Location = new System.Drawing.Point(54, 166);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.TabIndex = 0;
            this.lblUserName.Text = "Username";
            //
            // txtUserName
            //
            this.txtUserName.Location = new System.Drawing.Point(56, 188);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Placeholder = "Enter your username";
            this.txtUserName.Size = new System.Drawing.Size(388, 46);
            this.txtUserName.TabIndex = 1;
            //
            // lblPassword
            //
            this.lblPassword.AutoSize = true;
            this.lblPassword.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblPassword.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81);
            this.lblPassword.Location = new System.Drawing.Point(54, 250);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.TabIndex = 0;
            this.lblPassword.Text = "Password";
            //
            // txtPassword
            //
            this.txtPassword.IsPassword = true;
            this.txtPassword.Location = new System.Drawing.Point(56, 272);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Placeholder = "Enter your password";
            this.txtPassword.Size = new System.Drawing.Size(388, 46);
            this.txtPassword.TabIndex = 2;
            //
            // lblMessage
            //
            this.lblMessage.Font = new System.Drawing.Font("Roboto", 9.75F);
            this.lblMessage.ForeColor = System.Drawing.Color.FromArgb(220, 38, 38);
            this.lblMessage.Location = new System.Drawing.Point(54, 324);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(390, 20);
            this.lblMessage.TabIndex = 0;
            //
            // btnOK
            //
            this.btnOK.BackColor = System.Drawing.Color.FromArgb(42, 108, 228);
            this.btnOK.BackgroundColor = System.Drawing.Color.FromArgb(42, 108, 228);
            this.btnOK.BorderColor = System.Drawing.Color.FromArgb(42, 108, 228);
            this.btnOK.BorderRadius = 8;
            this.btnOK.BorderSize = 1;
            this.btnOK.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOK.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Bold);
            this.btnOK.ForeColor = System.Drawing.Color.White;
            this.btnOK.Location = new System.Drawing.Point(56, 350);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(388, 48);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "Sign in";
            this.btnOK.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnOK.TextColor = System.Drawing.Color.White;
            //
            // dbCard
            //
            this.dbCard.Location = new System.Drawing.Point(56, 430);
            this.dbCard.Name = "dbCard";
            this.dbCard.Size = new System.Drawing.Size(388, 96);
            this.dbCard.TabIndex = 4;
            //
            // LoginForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(189, 189, 189);
            this.ClientSize = new System.Drawing.Size(500, 582);
            this.Controls.Add(this.pnlMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "LoginForm";
            this.Padding = new System.Windows.Forms.Padding(1);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BLogic Third Party Tool - Sign in";
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox picLogo;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Label btnClose;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Label lblUserName;
        private LoginFieldBox txtUserName;
        private System.Windows.Forms.Label lblPassword;
        private LoginFieldBox txtPassword;
        private System.Windows.Forms.Label lblMessage;
        private CustomControl.BLabel_Rounded btnOK;
        private LoginDatabaseCard dbCard;
    }
}
