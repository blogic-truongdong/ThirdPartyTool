namespace BLogicPOS7SupportTool.Setup
{
    partial class DatabaseSetupForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.pnlTitleBar = new System.Windows.Forms.Panel();
            this.lblFormTitle = new System.Windows.Forms.Label();
            this.lblServer = new System.Windows.Forms.Label();
            this.txtServer = new System.Windows.Forms.TextBox();
            this.lblDatabase1 = new System.Windows.Forms.Label();
            this.txtDatabase1 = new System.Windows.Forms.TextBox();
            this.lblDatabase2 = new System.Windows.Forms.Label();
            this.txtDatabase2 = new System.Windows.Forms.TextBox();
            this.btnContinue = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.pnlTitleBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTitleBar
            // 
            this.pnlTitleBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(87)))), ((int)(((byte)(208)))));
            this.pnlTitleBar.Controls.Add(this.lblFormTitle);
            this.pnlTitleBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTitleBar.Location = new System.Drawing.Point(0, 0);
            this.pnlTitleBar.Name = "pnlTitleBar";
            this.pnlTitleBar.Size = new System.Drawing.Size(400, 40);
            this.pnlTitleBar.TabIndex = 0;
            // 
            // lblFormTitle
            // 
            this.lblFormTitle.AutoSize = true;
            this.lblFormTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblFormTitle.ForeColor = System.Drawing.Color.White;
            this.lblFormTitle.Location = new System.Drawing.Point(16, 10);
            this.lblFormTitle.Name = "lblFormTitle";
            this.lblFormTitle.Size = new System.Drawing.Size(129, 21);
            this.lblFormTitle.TabIndex = 0;
            this.lblFormTitle.Text = "Database Setup";
            // 
            // lblServer
            // 
            this.lblServer.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblServer.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.lblServer.Location = new System.Drawing.Point(40, 60);
            this.lblServer.Name = "lblServer";
            this.lblServer.Size = new System.Drawing.Size(400, 16);
            this.lblServer.TabIndex = 1;
            this.lblServer.Text = "Server Name";
            // 
            // txtServer
            // 
            this.txtServer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtServer.BackColor = System.Drawing.Color.White;
            this.txtServer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtServer.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtServer.Location = new System.Drawing.Point(40, 78);
            this.txtServer.Name = "txtServer";
            this.txtServer.Size = new System.Drawing.Size(320, 27);
            this.txtServer.TabIndex = 2;
            this.txtServer.Text = ".";
            // 
            // lblDatabase1
            // 
            this.lblDatabase1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDatabase1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblDatabase1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.lblDatabase1.Location = new System.Drawing.Point(40, 120);
            this.lblDatabase1.Name = "lblDatabase1";
            this.lblDatabase1.Size = new System.Drawing.Size(320, 16);
            this.lblDatabase1.TabIndex = 3;
            this.lblDatabase1.Text = "BLogicPOS7 Database";
            // 
            // txtDatabase1
            // 
            this.txtDatabase1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDatabase1.BackColor = System.Drawing.Color.White;
            this.txtDatabase1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDatabase1.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtDatabase1.Location = new System.Drawing.Point(40, 138);
            this.txtDatabase1.Name = "txtDatabase1";
            this.txtDatabase1.Size = new System.Drawing.Size(320, 27);
            this.txtDatabase1.TabIndex = 4;
            this.txtDatabase1.Text = "BLogicPOS7";
            // 
            // lblDatabase2
            // 
            this.lblDatabase2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDatabase2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblDatabase2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.lblDatabase2.Location = new System.Drawing.Point(40, 180);
            this.lblDatabase2.Name = "lblDatabase2";
            this.lblDatabase2.Size = new System.Drawing.Size(320, 16);
            this.lblDatabase2.TabIndex = 5;
            this.lblDatabase2.Text = "Merchant Database";
            // 
            // txtDatabase2
            // 
            this.txtDatabase2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDatabase2.BackColor = System.Drawing.Color.White;
            this.txtDatabase2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDatabase2.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtDatabase2.Location = new System.Drawing.Point(40, 198);
            this.txtDatabase2.Name = "txtDatabase2";
            this.txtDatabase2.Size = new System.Drawing.Size(320, 27);
            this.txtDatabase2.TabIndex = 6;
            this.txtDatabase2.Text = "Merchant";
            // 
            // btnContinue
            // 
            this.btnContinue.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnContinue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(87)))), ((int)(((byte)(208)))));
            this.btnContinue.FlatAppearance.BorderSize = 0;
            this.btnContinue.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnContinue.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnContinue.ForeColor = System.Drawing.Color.White;
            this.btnContinue.Location = new System.Drawing.Point(205, 252);
            this.btnContinue.Name = "btnContinue";
            this.btnContinue.Size = new System.Drawing.Size(155, 44);
            this.btnContinue.TabIndex = 7;
            this.btnContinue.Text = "Continue";
            this.btnContinue.UseVisualStyleBackColor = false;
            this.btnContinue.Click += new System.EventHandler(this.btnContinue_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.White;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.btnCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.btnCancel.Location = new System.Drawing.Point(40, 252);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(155, 44);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // DatabaseSetupForm
            // 
            this.AcceptButton = this.btnContinue;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(400, 312);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnContinue);
            this.Controls.Add(this.txtDatabase2);
            this.Controls.Add(this.lblDatabase2);
            this.Controls.Add(this.txtDatabase1);
            this.Controls.Add(this.lblDatabase1);
            this.Controls.Add(this.txtServer);
            this.Controls.Add(this.lblServer);
            this.Controls.Add(this.pnlTitleBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "DatabaseSetupForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Database Setup";
            this.pnlTitleBar.ResumeLayout(false);
            this.pnlTitleBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlTitleBar;
        private System.Windows.Forms.Label lblFormTitle;
        private System.Windows.Forms.Label lblServer;
        private System.Windows.Forms.TextBox txtServer;
        private System.Windows.Forms.Label lblDatabase1;
        private System.Windows.Forms.TextBox txtDatabase1;
        private System.Windows.Forms.Label lblDatabase2;
        private System.Windows.Forms.TextBox txtDatabase2;
        private System.Windows.Forms.Button btnContinue;
        private System.Windows.Forms.Button btnCancel;
    }
}
