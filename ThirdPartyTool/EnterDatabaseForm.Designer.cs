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
            this.txtFocus = new System.Windows.Forms.TextBox();
            this.pnlMerchant = new ThirdPartyTool.CustomControl.BPanel_Rounded();
            this.txtMerchant = new System.Windows.Forms.TextBox();
            this.pnlBLogicPOS7 = new ThirdPartyTool.CustomControl.BPanel_Rounded();
            this.txtBLogicPOS7 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnYes = new ThirdPartyTool.CustomControl.BLabel_Rounded();
            this.bTextBox1 = new ThirdPartyTool.CustomControl.BTextBox();
            this.btnCancel = new ThirdPartyTool.CustomControl.BLabel_Rounded();
            this.panel1.SuspendLayout();
            this.pnlMerchant.SuspendLayout();
            this.pnlBLogicPOS7.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.txtFocus);
            this.panel1.Controls.Add(this.pnlMerchant);
            this.panel1.Controls.Add(this.pnlBLogicPOS7);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.lblTitle);
            this.panel1.Controls.Add(this.btnYes);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(400, 250);
            this.panel1.TabIndex = 4;
            // 
            // txtFocus
            // 
            this.txtFocus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFocus.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtFocus.Font = new System.Drawing.Font("Roboto", 12F);
            this.txtFocus.Location = new System.Drawing.Point(109, -50);
            this.txtFocus.Name = "txtFocus";
            this.txtFocus.Size = new System.Drawing.Size(180, 20);
            this.txtFocus.TabIndex = 4;
            // 
            // pnlMerchant
            // 
            this.pnlMerchant.BackgroundColor = System.Drawing.Color.Transparent;
            this.pnlMerchant.BorderColor = System.Drawing.Color.Gray;
            this.pnlMerchant.BorderRadius = 5;
            this.pnlMerchant.BorderSize = 1;
            this.pnlMerchant.Controls.Add(this.txtMerchant);
            this.pnlMerchant.Location = new System.Drawing.Point(155, 123);
            this.pnlMerchant.Name = "pnlMerchant";
            this.pnlMerchant.Size = new System.Drawing.Size(200, 40);
            this.pnlMerchant.TabIndex = 3;
            // 
            // txtMerchant
            // 
            this.txtMerchant.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMerchant.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMerchant.Font = new System.Drawing.Font("Roboto", 12F);
            this.txtMerchant.Location = new System.Drawing.Point(10, 11);
            this.txtMerchant.Name = "txtMerchant";
            this.txtMerchant.Size = new System.Drawing.Size(180, 20);
            this.txtMerchant.TabIndex = 0;
            this.txtMerchant.Text = "Merchant";
            // 
            // pnlBLogicPOS7
            // 
            this.pnlBLogicPOS7.BackgroundColor = System.Drawing.Color.Transparent;
            this.pnlBLogicPOS7.BorderColor = System.Drawing.Color.Gray;
            this.pnlBLogicPOS7.BorderRadius = 5;
            this.pnlBLogicPOS7.BorderSize = 1;
            this.pnlBLogicPOS7.Controls.Add(this.txtBLogicPOS7);
            this.pnlBLogicPOS7.Location = new System.Drawing.Point(155, 73);
            this.pnlBLogicPOS7.Name = "pnlBLogicPOS7";
            this.pnlBLogicPOS7.Size = new System.Drawing.Size(200, 40);
            this.pnlBLogicPOS7.TabIndex = 3;
            // 
            // txtBLogicPOS7
            // 
            this.txtBLogicPOS7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBLogicPOS7.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBLogicPOS7.Font = new System.Drawing.Font("Roboto", 12F);
            this.txtBLogicPOS7.Location = new System.Drawing.Point(10, 11);
            this.txtBLogicPOS7.Name = "txtBLogicPOS7";
            this.txtBLogicPOS7.Size = new System.Drawing.Size(180, 20);
            this.txtBLogicPOS7.TabIndex = 0;
            this.txtBLogicPOS7.Text = "BLogicPOS7";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(45, 135);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 19);
            this.label2.TabIndex = 2;
            this.label2.Text = "Merchant";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(45, 85);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 19);
            this.label1.TabIndex = 2;
            this.label1.Text = "BLogicPOS7";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTitle
            // 
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("Roboto", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.lblTitle.Size = new System.Drawing.Size(398, 50);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "Database";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnYes
            // 
            this.btnYes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(87)))), ((int)(((byte)(183)))));
            this.btnYes.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(87)))), ((int)(((byte)(183)))));
            this.btnYes.BorderColor = System.Drawing.Color.Gray;
            this.btnYes.BorderRadius = 5;
            this.btnYes.BorderSize = 1;
            this.btnYes.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Bold);
            this.btnYes.ForeColor = System.Drawing.Color.White;
            this.btnYes.Location = new System.Drawing.Point(210, 190);
            this.btnYes.Name = "btnYes";
            this.btnYes.Size = new System.Drawing.Size(150, 40);
            this.btnYes.TabIndex = 0;
            this.btnYes.Text = "OK";
            this.btnYes.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnYes.TextColor = System.Drawing.Color.White;
            // 
            // bTextBox1
            // 
            this.bTextBox1.ForeColor = System.Drawing.Color.Gray;
            this.bTextBox1.Location = new System.Drawing.Point(159, 145);
            this.bTextBox1.Name = "bTextBox1";
            this.bTextBox1.PlaceholderColor = System.Drawing.Color.Gray;
            this.bTextBox1.PlaceholderText = "Enter text here...";
            this.bTextBox1.Size = new System.Drawing.Size(100, 20);
            this.bTextBox1.TabIndex = 3;
            this.bTextBox1.Text = "Enter text here...";
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.White;
            this.btnCancel.BackgroundColor = System.Drawing.Color.White;
            this.btnCancel.BorderColor = System.Drawing.Color.Gray;
            this.btnCancel.BorderRadius = 5;
            this.btnCancel.BorderSize = 1;
            this.btnCancel.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Bold);
            this.btnCancel.ForeColor = System.Drawing.Color.Black;
            this.btnCancel.Location = new System.Drawing.Point(40, 190);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(150, 40);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnCancel.TextColor = System.Drawing.Color.Black;
            // 
            // EnterDatabaseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(400, 250);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.bTextBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "EnterDatabaseForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EnterDatabaseForm";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnlMerchant.ResumeLayout(false);
            this.pnlMerchant.PerformLayout();
            this.pnlBLogicPOS7.ResumeLayout(false);
            this.pnlBLogicPOS7.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblTitle;
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