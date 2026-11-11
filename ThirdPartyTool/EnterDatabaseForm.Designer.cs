namespace ThirdPartyTool
{
    partial class EnterDatabaseForm
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlBLogicPOS7 = new ThirdPartyTool.CustomControl.BPanel_Rounded();
            this.txtBLogicPOS7 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.pnlMerchant = new ThirdPartyTool.CustomControl.BPanel_Rounded();
            this.txtMerchant = new System.Windows.Forms.TextBox();
            this.btnCancel = new ThirdPartyTool.CustomControl.BLabel_Rounded();
            this.btnYes = new ThirdPartyTool.CustomControl.BLabel_Rounded();
            this.txtFocus = new System.Windows.Forms.TextBox();
            this.bTextBox1 = new ThirdPartyTool.CustomControl.BTextBox();
            this.panel1.SuspendLayout();
            this.pnlBLogicPOS7.SuspendLayout();
            this.pnlMerchant.SuspendLayout();
            this.SuspendLayout();
            //
            // panel1
            //
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.lblTitle);
            this.panel1.Controls.Add(this.lblSubtitle);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.pnlBLogicPOS7);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.pnlMerchant);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btnYes);
            this.panel1.Controls.Add(this.txtFocus);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(1, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(398, 308);
            this.panel1.TabIndex = 4;
            //
            // lblTitle
            //
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(33, 33, 33);
            this.lblTitle.Location = new System.Drawing.Point(22, 18);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "Database";
            //
            // lblSubtitle
            //
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new System.Drawing.Font("Roboto", 9.75F);
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(117, 117, 117);
            this.lblSubtitle.Location = new System.Drawing.Point(24, 46);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.TabIndex = 2;
            this.lblSubtitle.Text = "Enter the database names on this SQL Server.";
            //
            // label1
            //
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(97, 97, 97);
            this.label1.Location = new System.Drawing.Point(24, 82);
            this.label1.Name = "label1";
            this.label1.TabIndex = 2;
            this.label1.Text = "BLogicPOS7 database";
            //
            // pnlBLogicPOS7
            //
            this.pnlBLogicPOS7.BackColor = System.Drawing.Color.White;
            this.pnlBLogicPOS7.BackgroundColor = System.Drawing.Color.White;
            this.pnlBLogicPOS7.BorderColor = System.Drawing.Color.FromArgb(189, 189, 189);
            this.pnlBLogicPOS7.BorderRadius = 5;
            this.pnlBLogicPOS7.BorderSize = 1;
            this.pnlBLogicPOS7.Controls.Add(this.txtBLogicPOS7);
            this.pnlBLogicPOS7.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.pnlBLogicPOS7.Location = new System.Drawing.Point(24, 104);
            this.pnlBLogicPOS7.Name = "pnlBLogicPOS7";
            this.pnlBLogicPOS7.Size = new System.Drawing.Size(350, 40);
            this.pnlBLogicPOS7.TabIndex = 3;
            //
            // txtBLogicPOS7
            //
            this.txtBLogicPOS7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBLogicPOS7.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBLogicPOS7.Font = new System.Drawing.Font("Roboto", 12F);
            this.txtBLogicPOS7.Location = new System.Drawing.Point(12, 10);
            this.txtBLogicPOS7.Name = "txtBLogicPOS7";
            this.txtBLogicPOS7.Size = new System.Drawing.Size(326, 20);
            this.txtBLogicPOS7.TabIndex = 0;
            this.txtBLogicPOS7.Text = "BLogicPOS7";
            //
            // label2
            //
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(97, 97, 97);
            this.label2.Location = new System.Drawing.Point(24, 158);
            this.label2.Name = "label2";
            this.label2.TabIndex = 2;
            this.label2.Text = "Merchant database";
            //
            // pnlMerchant
            //
            this.pnlMerchant.BackColor = System.Drawing.Color.White;
            this.pnlMerchant.BackgroundColor = System.Drawing.Color.White;
            this.pnlMerchant.BorderColor = System.Drawing.Color.FromArgb(189, 189, 189);
            this.pnlMerchant.BorderRadius = 5;
            this.pnlMerchant.BorderSize = 1;
            this.pnlMerchant.Controls.Add(this.txtMerchant);
            this.pnlMerchant.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.pnlMerchant.Location = new System.Drawing.Point(24, 180);
            this.pnlMerchant.Name = "pnlMerchant";
            this.pnlMerchant.Size = new System.Drawing.Size(350, 40);
            this.pnlMerchant.TabIndex = 4;
            //
            // txtMerchant
            //
            this.txtMerchant.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMerchant.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMerchant.Font = new System.Drawing.Font("Roboto", 12F);
            this.txtMerchant.Location = new System.Drawing.Point(12, 10);
            this.txtMerchant.Name = "txtMerchant";
            this.txtMerchant.Size = new System.Drawing.Size(326, 20);
            this.txtMerchant.TabIndex = 0;
            this.txtMerchant.Text = "Merchant";
            //
            // btnCancel
            //
            this.btnCancel.BackColor = System.Drawing.Color.White;
            this.btnCancel.BackgroundColor = System.Drawing.Color.White;
            this.btnCancel.BorderColor = System.Drawing.Color.FromArgb(189, 189, 189);
            this.btnCancel.BorderRadius = 5;
            this.btnCancel.BorderSize = 1;
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.Font = new System.Drawing.Font("Roboto", 11F, System.Drawing.FontStyle.Bold);
            this.btnCancel.ForeColor = System.Drawing.Color.FromArgb(66, 66, 66);
            this.btnCancel.Location = new System.Drawing.Point(144, 246);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 40);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnCancel.TextColor = System.Drawing.Color.FromArgb(66, 66, 66);
            //
            // btnYes
            //
            this.btnYes.BackColor = System.Drawing.Color.FromArgb(42, 108, 228);
            this.btnYes.BackgroundColor = System.Drawing.Color.FromArgb(42, 108, 228);
            this.btnYes.BorderColor = System.Drawing.Color.FromArgb(42, 108, 228);
            this.btnYes.BorderRadius = 5;
            this.btnYes.BorderSize = 1;
            this.btnYes.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnYes.Font = new System.Drawing.Font("Roboto", 11F, System.Drawing.FontStyle.Bold);
            this.btnYes.ForeColor = System.Drawing.Color.White;
            this.btnYes.Location = new System.Drawing.Point(254, 246);
            this.btnYes.Name = "btnYes";
            this.btnYes.Size = new System.Drawing.Size(120, 40);
            this.btnYes.TabIndex = 6;
            this.btnYes.Text = "Connect";
            this.btnYes.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnYes.TextColor = System.Drawing.Color.White;
            //
            // txtFocus
            //
            this.txtFocus.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtFocus.Location = new System.Drawing.Point(109, -50);
            this.txtFocus.Name = "txtFocus";
            this.txtFocus.Size = new System.Drawing.Size(180, 20);
            this.txtFocus.TabIndex = 7;
            //
            // bTextBox1
            //
            this.bTextBox1.Location = new System.Drawing.Point(20, 20);
            this.bTextBox1.Name = "bTextBox1";
            this.bTextBox1.Size = new System.Drawing.Size(100, 20);
            this.bTextBox1.TabIndex = 8;
            //
            // EnterDatabaseForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(189, 189, 189);
            this.ClientSize = new System.Drawing.Size(400, 310);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.bTextBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "EnterDatabaseForm";
            this.Padding = new System.Windows.Forms.Padding(1);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EnterDatabaseForm";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnlBLogicPOS7.ResumeLayout(false);
            this.pnlBLogicPOS7.PerformLayout();
            this.pnlMerchant.ResumeLayout(false);
            this.pnlMerchant.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private CustomControl.BLabel_Rounded btnYes;
        private CustomControl.BTextBox bTextBox1;
        private System.Windows.Forms.Label label1;
        private CustomControl.BPanel_Rounded pnlBLogicPOS7;
        private CustomControl.BPanel_Rounded pnlMerchant;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox txtMerchant;
        public System.Windows.Forms.TextBox txtBLogicPOS7;
        public System.Windows.Forms.TextBox txtFocus;
        private CustomControl.BLabel_Rounded btnCancel;
    }
}
