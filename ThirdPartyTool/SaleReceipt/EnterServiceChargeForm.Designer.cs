using RJCodeAdvance.RJControls;

namespace ThirdPartyTool.SaleReceipt
{
    partial class EnterServiceChargeForm
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
            this.btnCancel = new RJCodeAdvance.RJControls.RJButton();
            this.btnOK = new RJCodeAdvance.RJControls.RJButton();
            this.label5 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.detailServiceCharge = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.bPanel_Rounded1 = new ThirdPartyTool.CustomControl.BPanel_Rounded();
            this.txtTax = new ThirdPartyTool.CustomControl.NumericTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.pnlPartyGratuity = new ThirdPartyTool.CustomControl.BPanel_Rounded();
            this.txtAmount = new ThirdPartyTool.CustomControl.NumericTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.bPanel_Rounded1.SuspendLayout();
            this.pnlPartyGratuity.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btnOK);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.bPanel_Rounded1);
            this.panel1.Controls.Add(this.pnlPartyGratuity);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(400, 366);
            this.panel1.TabIndex = 1;
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.White;
            this.btnCancel.BackgroundColor = System.Drawing.Color.White;
            this.btnCancel.BorderColor = System.Drawing.Color.Gray;
            this.btnCancel.BorderRadius = 8;
            this.btnCancel.BorderSize = 1;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("BLogic", 14F, System.Drawing.FontStyle.Bold);
            this.btnCancel.ForeColor = System.Drawing.Color.Black;
            this.btnCancel.Location = new System.Drawing.Point(55, 291);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(5);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(140, 50);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.TabStop = false;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.TextColor = System.Drawing.Color.Black;
            this.btnCancel.UseVisualStyleBackColor = false;
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(87)))), ((int)(((byte)(208)))));
            this.btnOK.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(87)))), ((int)(((byte)(208)))));
            this.btnOK.BorderColor = System.Drawing.Color.Gray;
            this.btnOK.BorderRadius = 8;
            this.btnOK.BorderSize = 1;
            this.btnOK.FlatAppearance.BorderSize = 0;
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOK.Font = new System.Drawing.Font("BLogic", 14F, System.Drawing.FontStyle.Bold);
            this.btnOK.ForeColor = System.Drawing.Color.White;
            this.btnOK.Location = new System.Drawing.Point(205, 291);
            this.btnOK.Margin = new System.Windows.Forms.Padding(5);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(140, 50);
            this.btnOK.TabIndex = 4;
            this.btnOK.TabStop = false;
            this.btnOK.Text = "OK";
            this.btnOK.TextColor = System.Drawing.Color.White;
            this.btnOK.UseVisualStyleBackColor = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("BLogic", 14F);
            this.label5.Location = new System.Drawing.Point(50, 189);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 27);
            this.label5.TabIndex = 13;
            this.label5.Text = "Tax";
            this.label5.UseMnemonic = false;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(87)))), ((int)(((byte)(208)))));
            this.panel2.Controls.Add(this.lblTitle);
            this.panel2.Controls.Add(this.detailServiceCharge);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(398, 84);
            this.panel2.TabIndex = 12;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("BLogic", 15.75F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(15, 15);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(5);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(217, 30);
            this.lblTitle.TabIndex = 6;
            this.lblTitle.Text = "Enter Service Charge";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // detailServiceCharge
            // 
            this.detailServiceCharge.AutoSize = true;
            this.detailServiceCharge.Font = new System.Drawing.Font("BLogic", 10F);
            this.detailServiceCharge.ForeColor = System.Drawing.Color.White;
            this.detailServiceCharge.Location = new System.Drawing.Point(16, 50);
            this.detailServiceCharge.Name = "detailServiceCharge";
            this.detailServiceCharge.Size = new System.Drawing.Size(288, 19);
            this.detailServiceCharge.TabIndex = 9;
            this.detailServiceCharge.Text = "Enter the Amount & Tax for this service charge.";
            this.detailServiceCharge.UseMnemonic = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("BLogic", 14F);
            this.label2.Location = new System.Drawing.Point(50, 99);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 27);
            this.label2.TabIndex = 11;
            this.label2.Text = "Amount";
            this.label2.UseMnemonic = false;
            // 
            // bPanel_Rounded1
            // 
            this.bPanel_Rounded1.BackgroundColor = System.Drawing.Color.White;
            this.bPanel_Rounded1.BorderColor = System.Drawing.Color.Gray;
            this.bPanel_Rounded1.BorderRadius = 5;
            this.bPanel_Rounded1.BorderSize = 1;
            this.bPanel_Rounded1.Controls.Add(this.txtTax);
            this.bPanel_Rounded1.Controls.Add(this.label4);
            this.bPanel_Rounded1.Location = new System.Drawing.Point(55, 221);
            this.bPanel_Rounded1.Margin = new System.Windows.Forms.Padding(5);
            this.bPanel_Rounded1.Name = "bPanel_Rounded1";
            this.bPanel_Rounded1.Size = new System.Drawing.Size(290, 40);
            this.bPanel_Rounded1.TabIndex = 10;
            // 
            // txtTax
            // 
            this.txtTax.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtTax.Font = new System.Drawing.Font("BLogic", 14F);
            this.txtTax.FormatSpecifier = "c";
            this.txtTax.Location = new System.Drawing.Point(39, 8);
            this.txtTax.MaxValue = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.txtTax.Name = "txtTax";
            this.txtTax.Size = new System.Drawing.Size(240, 23);
            this.txtTax.TabIndex = 2;
            this.txtTax.Text = "0.00";
            this.txtTax.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtTax.Value = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            // 
            // label4
            // 
            this.label4.Image = global::ThirdPartyTool.Properties.Resources.icon_tax_24x24;
            this.label4.Location = new System.Drawing.Point(9, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(24, 24);
            this.label4.TabIndex = 7;
            // 
            // pnlPartyGratuity
            // 
            this.pnlPartyGratuity.BackgroundColor = System.Drawing.Color.White;
            this.pnlPartyGratuity.BorderColor = System.Drawing.Color.Gray;
            this.pnlPartyGratuity.BorderRadius = 5;
            this.pnlPartyGratuity.BorderSize = 1;
            this.pnlPartyGratuity.Controls.Add(this.txtAmount);
            this.pnlPartyGratuity.Controls.Add(this.label1);
            this.pnlPartyGratuity.Location = new System.Drawing.Point(55, 131);
            this.pnlPartyGratuity.Margin = new System.Windows.Forms.Padding(5);
            this.pnlPartyGratuity.Name = "pnlPartyGratuity";
            this.pnlPartyGratuity.Size = new System.Drawing.Size(290, 40);
            this.pnlPartyGratuity.TabIndex = 10;
            // 
            // txtAmount
            // 
            this.txtAmount.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtAmount.Font = new System.Drawing.Font("BLogic", 14F);
            this.txtAmount.FormatSpecifier = "c";
            this.txtAmount.Location = new System.Drawing.Point(39, 8);
            this.txtAmount.MaxValue = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(240, 23);
            this.txtAmount.TabIndex = 1;
            this.txtAmount.Text = "0.00";
            this.txtAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtAmount.Value = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            // 
            // label1
            // 
            this.label1.Image = global::ThirdPartyTool.Properties.Resources.icon_amount_24x24;
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(24, 24);
            this.label1.TabIndex = 7;
            // 
            // EnterServiceChargeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(400, 366);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "EnterServiceChargeForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LoginForm";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.bPanel_Rounded1.ResumeLayout(false);
            this.bPanel_Rounded1.PerformLayout();
            this.pnlPartyGratuity.ResumeLayout(false);
            this.pnlPartyGratuity.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label detailServiceCharge;
        private CustomControl.BPanel_Rounded pnlPartyGratuity;
        private CustomControl.BPanel_Rounded bPanel_Rounded1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private RJButton btnOK;
        private RJButton btnCancel;
        public CustomControl.NumericTextBox txtAmount;
        public CustomControl.NumericTextBox txtTax;
    }
}