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
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlBLogicPOS7 = new ThirdPartyTool.CustomControl.BPanel_Rounded();
            this.txtOpeningAmount = new ThirdPartyTool.CustomControl.NumericTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.pnlMerchant = new ThirdPartyTool.CustomControl.BPanel_Rounded();
            this.txtClosingAmount = new ThirdPartyTool.CustomControl.NumericTextBox();
            this.btnCancel = new ThirdPartyTool.CustomControl.BLabel_Rounded();
            this.btnOK = new ThirdPartyTool.CustomControl.BLabel_Rounded();
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
            this.panel1.Controls.Add(this.btnOK);
            this.panel1.Controls.Add(this.txtFocus);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(1, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(378, 310);
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
            this.lblTitle.Text = "Edit shift";
            //
            // lblSubtitle
            //
            this.lblSubtitle.AutoEllipsis = true;
            this.lblSubtitle.Font = new System.Drawing.Font("Roboto", 9.75F);
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(117, 117, 117);
            this.lblSubtitle.Location = new System.Drawing.Point(24, 46);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(330, 18);
            this.lblSubtitle.TabIndex = 2;
            this.lblSubtitle.Text = "Change the cash counted at open and close.";
            //
            // label1
            //
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(97, 97, 97);
            this.label1.Location = new System.Drawing.Point(24, 82);
            this.label1.Name = "label1";
            this.label1.TabIndex = 2;
            this.label1.Text = "Opening amount";
            //
            // pnlBLogicPOS7
            //
            this.pnlBLogicPOS7.BackColor = System.Drawing.Color.White;
            this.pnlBLogicPOS7.BackgroundColor = System.Drawing.Color.White;
            this.pnlBLogicPOS7.BorderColor = System.Drawing.Color.FromArgb(189, 189, 189);
            this.pnlBLogicPOS7.BorderRadius = 5;
            this.pnlBLogicPOS7.BorderSize = 1;
            this.pnlBLogicPOS7.Controls.Add(this.txtOpeningAmount);
            this.pnlBLogicPOS7.Location = new System.Drawing.Point(24, 104);
            this.pnlBLogicPOS7.Name = "pnlBLogicPOS7";
            this.pnlBLogicPOS7.Size = new System.Drawing.Size(330, 40);
            this.pnlBLogicPOS7.TabIndex = 3;
            //
            // txtOpeningAmount
            //
            this.txtOpeningAmount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOpeningAmount.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtOpeningAmount.Font = new System.Drawing.Font("Roboto", 12F);
            this.txtOpeningAmount.FormatSpecifier = "c2";
            this.txtOpeningAmount.Location = new System.Drawing.Point(12, 10);
            this.txtOpeningAmount.MaxValue = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.txtOpeningAmount.Name = "txtOpeningAmount";
            this.txtOpeningAmount.Size = new System.Drawing.Size(306, 20);
            this.txtOpeningAmount.TabIndex = 0;
            this.txtOpeningAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtOpeningAmount.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            //
            // label2
            //
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(97, 97, 97);
            this.label2.Location = new System.Drawing.Point(24, 158);
            this.label2.Name = "label2";
            this.label2.TabIndex = 2;
            this.label2.Text = "Closing amount";
            //
            // pnlMerchant
            //
            this.pnlMerchant.BackColor = System.Drawing.Color.White;
            this.pnlMerchant.BackgroundColor = System.Drawing.Color.White;
            this.pnlMerchant.BorderColor = System.Drawing.Color.FromArgb(189, 189, 189);
            this.pnlMerchant.BorderRadius = 5;
            this.pnlMerchant.BorderSize = 1;
            this.pnlMerchant.Controls.Add(this.txtClosingAmount);
            this.pnlMerchant.Location = new System.Drawing.Point(24, 180);
            this.pnlMerchant.Name = "pnlMerchant";
            this.pnlMerchant.Size = new System.Drawing.Size(330, 40);
            this.pnlMerchant.TabIndex = 4;
            //
            // txtClosingAmount
            //
            this.txtClosingAmount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtClosingAmount.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtClosingAmount.Font = new System.Drawing.Font("Roboto", 12F);
            this.txtClosingAmount.FormatSpecifier = "c2";
            this.txtClosingAmount.Location = new System.Drawing.Point(12, 10);
            this.txtClosingAmount.MaxValue = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.txtClosingAmount.Name = "txtClosingAmount";
            this.txtClosingAmount.Size = new System.Drawing.Size(306, 20);
            this.txtClosingAmount.TabIndex = 0;
            this.txtClosingAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtClosingAmount.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
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
            // btnOK
            //
            this.btnOK.BackColor = System.Drawing.Color.FromArgb(42, 108, 228);
            this.btnOK.BackgroundColor = System.Drawing.Color.FromArgb(42, 108, 228);
            this.btnOK.BorderColor = System.Drawing.Color.FromArgb(42, 108, 228);
            this.btnOK.BorderRadius = 5;
            this.btnOK.BorderSize = 1;
            this.btnOK.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOK.Font = new System.Drawing.Font("Roboto", 11F, System.Drawing.FontStyle.Bold);
            this.btnOK.ForeColor = System.Drawing.Color.White;
            this.btnOK.Location = new System.Drawing.Point(254, 246);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(100, 40);
            this.btnOK.TabIndex = 6;
            this.btnOK.Text = "Save";
            this.btnOK.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnOK.TextColor = System.Drawing.Color.White;
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
            // UpdateShiftForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(189, 189, 189);
            this.ClientSize = new System.Drawing.Size(380, 312);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.bTextBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "UpdateShiftForm";
            this.Padding = new System.Windows.Forms.Padding(1);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "UpdateShiftForm";
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
