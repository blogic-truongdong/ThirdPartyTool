namespace ThirdPartyTool.Shift
{
    partial class UpdateShiftForm
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
            this.btnCancel = new ThirdPartyTool.CustomControl.BLabel_Rounded();
            this.txtFocus = new System.Windows.Forms.TextBox();
            this.pnlMerchant = new ThirdPartyTool.CustomControl.BPanel_Rounded();
            this.pnlBLogicPOS7 = new ThirdPartyTool.CustomControl.BPanel_Rounded();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnOK = new ThirdPartyTool.CustomControl.BLabel_Rounded();
            this.bTextBox1 = new ThirdPartyTool.CustomControl.BTextBox();
            this.txtOpeningAmount = new ThirdPartyTool.CustomControl.NumericTextBox();
            this.txtClosingAmount = new ThirdPartyTool.CustomControl.NumericTextBox();
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
            this.panel1.Controls.Add(this.btnOK);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(400, 250);
            this.panel1.TabIndex = 4;
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
            this.pnlMerchant.Controls.Add(this.txtClosingAmount);
            this.pnlMerchant.Location = new System.Drawing.Point(185, 123);
            this.pnlMerchant.Name = "pnlMerchant";
            this.pnlMerchant.Size = new System.Drawing.Size(170, 40);
            this.pnlMerchant.TabIndex = 3;
            // 
            // pnlBLogicPOS7
            // 
            this.pnlBLogicPOS7.BackgroundColor = System.Drawing.Color.Transparent;
            this.pnlBLogicPOS7.BorderColor = System.Drawing.Color.Gray;
            this.pnlBLogicPOS7.BorderRadius = 5;
            this.pnlBLogicPOS7.BorderSize = 1;
            this.pnlBLogicPOS7.Controls.Add(this.txtOpeningAmount);
            this.pnlBLogicPOS7.Location = new System.Drawing.Point(185, 73);
            this.pnlBLogicPOS7.Name = "pnlBLogicPOS7";
            this.pnlBLogicPOS7.Size = new System.Drawing.Size(170, 40);
            this.pnlBLogicPOS7.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(45, 135);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(123, 19);
            this.label2.TabIndex = 2;
            this.label2.Text = "Closing Amount";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(45, 85);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 19);
            this.label1.TabIndex = 2;
            this.label1.Text = "Opening Amount";
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
            this.lblTitle.Text = "Edit Shift";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(87)))), ((int)(((byte)(183)))));
            this.btnOK.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(87)))), ((int)(((byte)(183)))));
            this.btnOK.BorderColor = System.Drawing.Color.Gray;
            this.btnOK.BorderRadius = 5;
            this.btnOK.BorderSize = 1;
            this.btnOK.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Bold);
            this.btnOK.ForeColor = System.Drawing.Color.White;
            this.btnOK.Location = new System.Drawing.Point(210, 190);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(150, 40);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK";
            this.btnOK.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnOK.TextColor = System.Drawing.Color.White;
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
            // txtOpeningAmount
            // 
            this.txtOpeningAmount.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtOpeningAmount.Font = new System.Drawing.Font("Roboto", 12F);
            this.txtOpeningAmount.FormatSpecifier = "c2";
            this.txtOpeningAmount.Location = new System.Drawing.Point(10, 11);
            this.txtOpeningAmount.Margin = new System.Windows.Forms.Padding(5);
            this.txtOpeningAmount.MaxValue = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.txtOpeningAmount.Name = "txtOpeningAmount";
            this.txtOpeningAmount.Size = new System.Drawing.Size(150, 20);
            this.txtOpeningAmount.TabIndex = 34;
            this.txtOpeningAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtOpeningAmount.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // txtClosingAmount
            // 
            this.txtClosingAmount.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtClosingAmount.Font = new System.Drawing.Font("Roboto", 12F);
            this.txtClosingAmount.FormatSpecifier = "c2";
            this.txtClosingAmount.Location = new System.Drawing.Point(10, 11);
            this.txtClosingAmount.Margin = new System.Windows.Forms.Padding(5);
            this.txtClosingAmount.MaxValue = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.txtClosingAmount.Name = "txtClosingAmount";
            this.txtClosingAmount.Size = new System.Drawing.Size(150, 20);
            this.txtClosingAmount.TabIndex = 35;
            this.txtClosingAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtClosingAmount.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // UpdateShiftForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(400, 250);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.bTextBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "UpdateShiftForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "UpdateShiftForm";
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
        private CustomControl.BLabel_Rounded btnOK;
        private CustomControl.BTextBox bTextBox1;
        private System.Windows.Forms.Label label1;
        private CustomControl.BPanel_Rounded pnlBLogicPOS7;
        private CustomControl.BPanel_Rounded pnlMerchant;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox txtFocus;
        private CustomControl.BLabel_Rounded btnCancel;
        public CustomControl.NumericTextBox txtClosingAmount;
        public CustomControl.NumericTextBox txtOpeningAmount;
    }
}