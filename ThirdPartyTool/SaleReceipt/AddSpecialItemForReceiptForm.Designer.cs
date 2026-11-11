namespace ThirdPartyTool.SaleReceipt
{
    partial class AddSpecialItemForReceiptForm
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
            this.lblEmpty = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlFooter = new System.Windows.Forms.Panel();
            this.lblFooterLine = new System.Windows.Forms.Label();
            this.lblAmountCaption = new System.Windows.Forms.Label();
            this.pnlAmount = new System.Windows.Forms.Panel();
            this.pnlAmountInner = new System.Windows.Forms.Panel();
            this.lblCurrency = new System.Windows.Forms.Label();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.lblPaymentCaption = new System.Windows.Forms.Label();
            this.cboPayment = new System.Windows.Forms.ComboBox();
            this.lblPreview = new System.Windows.Forms.Label();
            this.btnCancel = new ThirdPartyTool.CustomControl.BLabel_Rounded();
            this.btnAdd = new ThirdPartyTool.CustomControl.BLabel_Rounded();
            this.label3 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.pnlFooter.SuspendLayout();
            this.pnlAmount.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            //
            // panel1
            //
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.lblEmpty);
            this.panel1.Controls.Add(this.flowLayoutPanel1);
            this.panel1.Controls.Add(this.pnlFooter);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(1, 1);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(560, 578);
            this.panel1.TabIndex = 0;
            //
            // panel2 (header)
            //
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.lblSubtitle);
            this.panel2.Controls.Add(this.btnClose);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(560, 72);
            this.panel2.TabIndex = 0;
            //
            // label2
            //
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(33, 33, 33);
            this.label2.Location = new System.Drawing.Point(22, 14);
            this.label2.Name = "label2";
            this.label2.TabIndex = 0;
            this.label2.Text = "Add special item";
            //
            // lblSubtitle
            //
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.BackColor = System.Drawing.Color.White;
            this.lblSubtitle.Font = new System.Drawing.Font("Roboto", 9.75F);
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(117, 117, 117);
            this.lblSubtitle.Location = new System.Drawing.Point(24, 43);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "Added without tax. The payment changes by the same amount.";
            //
            // btnClose
            //
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.Color.White;
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.Image = global::ThirdPartyTool.Properties.Resources.icon_close;
            this.btnClose.Location = new System.Drawing.Point(504, 16);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(40, 40);
            this.btnClose.TabIndex = 2;
            //
            // label3 (divider under the header)
            //
            this.label3.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.Location = new System.Drawing.Point(0, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(560, 1);
            this.label3.TabIndex = 1;
            //
            // flowLayoutPanel1 (item list)
            //
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.White;
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.WrapContents = false;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 73);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(20, 8, 20, 12);
            this.flowLayoutPanel1.Size = new System.Drawing.Size(560, 333);
            this.flowLayoutPanel1.TabIndex = 2;
            //
            // lblEmpty
            //
            this.lblEmpty.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblEmpty.BackColor = System.Drawing.Color.White;
            this.lblEmpty.Font = new System.Drawing.Font("Roboto", 11F);
            this.lblEmpty.ForeColor = System.Drawing.Color.FromArgb(117, 117, 117);
            this.lblEmpty.Location = new System.Drawing.Point(0, 73);
            this.lblEmpty.Name = "lblEmpty";
            this.lblEmpty.Size = new System.Drawing.Size(560, 333);
            this.lblEmpty.TabIndex = 3;
            this.lblEmpty.Text = "No Open Price or Negative Price items are set up in this store.";
            this.lblEmpty.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblEmpty.Visible = false;
            //
            // pnlFooter
            //
            this.pnlFooter.BackColor = System.Drawing.Color.White;
            this.pnlFooter.Controls.Add(this.lblFooterLine);
            this.pnlFooter.Controls.Add(this.lblAmountCaption);
            this.pnlFooter.Controls.Add(this.pnlAmount);
            this.pnlFooter.Controls.Add(this.lblPaymentCaption);
            this.pnlFooter.Controls.Add(this.cboPayment);
            this.pnlFooter.Controls.Add(this.lblPreview);
            this.pnlFooter.Controls.Add(this.btnCancel);
            this.pnlFooter.Controls.Add(this.btnAdd);
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Location = new System.Drawing.Point(0, 406);
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Size = new System.Drawing.Size(560, 172);
            this.pnlFooter.TabIndex = 4;
            //
            // lblFooterLine
            //
            this.lblFooterLine.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
            this.lblFooterLine.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblFooterLine.Name = "lblFooterLine";
            this.lblFooterLine.Size = new System.Drawing.Size(560, 1);
            this.lblFooterLine.TabIndex = 0;
            //
            // lblAmountCaption
            //
            this.lblAmountCaption.AutoSize = true;
            this.lblAmountCaption.Font = new System.Drawing.Font("Roboto", 9F, System.Drawing.FontStyle.Bold);
            this.lblAmountCaption.ForeColor = System.Drawing.Color.FromArgb(97, 97, 97);
            this.lblAmountCaption.Location = new System.Drawing.Point(22, 16);
            this.lblAmountCaption.Name = "lblAmountCaption";
            this.lblAmountCaption.TabIndex = 1;
            this.lblAmountCaption.Text = "Amount";
            //
            // pnlAmount (bordered input)
            //
            this.pnlAmount.BackColor = System.Drawing.Color.FromArgb(189, 189, 189);
            this.pnlAmount.Controls.Add(this.pnlAmountInner);
            this.pnlAmount.Location = new System.Drawing.Point(24, 38);
            this.pnlAmount.Name = "pnlAmount";
            this.pnlAmount.Padding = new System.Windows.Forms.Padding(1);
            this.pnlAmount.Size = new System.Drawing.Size(200, 40);
            this.pnlAmount.TabIndex = 2;
            //
            // pnlAmountInner (white field inside the 1px border)
            //
            this.pnlAmountInner.BackColor = System.Drawing.Color.White;
            this.pnlAmountInner.Controls.Add(this.txtAmount);
            this.pnlAmountInner.Controls.Add(this.lblCurrency);
            this.pnlAmountInner.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlAmountInner.Name = "pnlAmountInner";
            this.pnlAmountInner.TabIndex = 0;
            //
            // lblCurrency
            //
            this.lblCurrency.BackColor = System.Drawing.Color.White;
            this.lblCurrency.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblCurrency.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Bold);
            this.lblCurrency.ForeColor = System.Drawing.Color.FromArgb(97, 97, 97);
            this.lblCurrency.Name = "lblCurrency";
            this.lblCurrency.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.lblCurrency.Size = new System.Drawing.Size(34, 38);
            this.lblCurrency.TabIndex = 0;
            this.lblCurrency.Text = "$";
            this.lblCurrency.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // txtAmount
            //
            this.txtAmount.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtAmount.Font = new System.Drawing.Font("Roboto", 13F);
            this.txtAmount.ForeColor = System.Drawing.Color.FromArgb(33, 33, 33);
            this.txtAmount.Location = new System.Drawing.Point(36, 8);
            this.txtAmount.MaxLength = 10;
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(156, 21);
            this.txtAmount.TabIndex = 0;
            //
            // lblPaymentCaption
            //
            this.lblPaymentCaption.AutoSize = true;
            this.lblPaymentCaption.Font = new System.Drawing.Font("Roboto", 9F, System.Drawing.FontStyle.Bold);
            this.lblPaymentCaption.ForeColor = System.Drawing.Color.FromArgb(97, 97, 97);
            this.lblPaymentCaption.Location = new System.Drawing.Point(244, 16);
            this.lblPaymentCaption.Name = "lblPaymentCaption";
            this.lblPaymentCaption.TabIndex = 3;
            this.lblPaymentCaption.Text = "Payment to change";
            //
            // cboPayment
            //
            this.cboPayment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPayment.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboPayment.Font = new System.Drawing.Font("Roboto", 12F);
            this.cboPayment.ForeColor = System.Drawing.Color.FromArgb(33, 33, 33);
            this.cboPayment.Location = new System.Drawing.Point(246, 42);
            this.cboPayment.Name = "cboPayment";
            this.cboPayment.Size = new System.Drawing.Size(290, 27);
            this.cboPayment.TabIndex = 4;
            //
            // lblPreview
            //
            this.lblPreview.Font = new System.Drawing.Font("Roboto", 9.75F);
            this.lblPreview.ForeColor = System.Drawing.Color.FromArgb(97, 97, 97);
            this.lblPreview.Location = new System.Drawing.Point(22, 88);
            this.lblPreview.Name = "lblPreview";
            this.lblPreview.Size = new System.Drawing.Size(516, 20);
            this.lblPreview.TabIndex = 5;
            this.lblPreview.Text = "Pick an item above.";
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
            this.btnCancel.Location = new System.Drawing.Point(246, 116);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 44);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnCancel.TextColor = System.Drawing.Color.FromArgb(66, 66, 66);
            //
            // btnAdd
            //
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(229, 231, 235);
            this.btnAdd.BackgroundColor = System.Drawing.Color.FromArgb(229, 231, 235);
            this.btnAdd.BorderColor = System.Drawing.Color.FromArgb(229, 231, 235);
            this.btnAdd.BorderRadius = 5;
            this.btnAdd.BorderSize = 1;
            this.btnAdd.Font = new System.Drawing.Font("Roboto", 11F, System.Drawing.FontStyle.Bold);
            this.btnAdd.ForeColor = System.Drawing.Color.FromArgb(156, 163, 175);
            this.btnAdd.Location = new System.Drawing.Point(356, 116);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(180, 44);
            this.btnAdd.TabIndex = 7;
            this.btnAdd.Text = "Add item";
            this.btnAdd.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnAdd.TextColor = System.Drawing.Color.FromArgb(156, 163, 175);
            //
            // AddSpecialItemForReceiptForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(189, 189, 189);
            this.ClientSize = new System.Drawing.Size(562, 580);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "AddSpecialItemForReceiptForm";
            this.Padding = new System.Windows.Forms.Padding(1);
            this.Text = "AddSpecialItemForReceiptForm";
            this.panel1.ResumeLayout(false);
            this.pnlFooter.ResumeLayout(false);
            this.pnlFooter.PerformLayout();
            this.pnlAmount.ResumeLayout(false);
            this.pnlAmount.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label btnClose;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label lblEmpty;
        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.Label lblFooterLine;
        private System.Windows.Forms.Label lblAmountCaption;
        private System.Windows.Forms.Panel pnlAmount;
        private System.Windows.Forms.Panel pnlAmountInner;
        private System.Windows.Forms.Label lblCurrency;
        private System.Windows.Forms.TextBox txtAmount;
        private System.Windows.Forms.Label lblPaymentCaption;
        private System.Windows.Forms.ComboBox cboPayment;
        private System.Windows.Forms.Label lblPreview;
        private CustomControl.BLabel_Rounded btnCancel;
        private CustomControl.BLabel_Rounded btnAdd;
    }
}
