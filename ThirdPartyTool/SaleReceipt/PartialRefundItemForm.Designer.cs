namespace ThirdPartyTool.SaleReceipt
{
    partial class PartialRefundItemForm
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
            this.pnlRefundItemContainer = new System.Windows.Forms.Panel();
            this.pnlColumns = new System.Windows.Forms.Panel();
            this.chkAll = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.pnlFooterDivider = new System.Windows.Forms.Panel();
            this.lblTotalCaption = new System.Windows.Forms.Label();
            this.lblTotalRefundItem_TotalAmount = new System.Windows.Forms.Label();
            this.flpBreakdown = new System.Windows.Forms.FlowLayoutPanel();
            this.lblTotalRefundItem_Amount = new System.Windows.Forms.Label();
            this.lblTotalRefundItem_Tax = new System.Windows.Forms.Label();
            this.btnCancel = new ThirdPartyTool.CustomControl.BLabel_Rounded();
            this.btnProcessRefundPartialItem = new ThirdPartyTool.CustomControl.BLabel_Rounded();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.pnlColumns.SuspendLayout();
            this.panel3.SuspendLayout();
            this.flpBreakdown.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            //
            // panel1
            //
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.pnlRefundItemContainer);
            this.panel1.Controls.Add(this.pnlColumns);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(1, 1);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(638, 558);
            this.panel1.TabIndex = 1;
            //
            // panel2
            //
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.lblTitle);
            this.panel2.Controls.Add(this.lblSubtitle);
            this.panel2.Controls.Add(this.btnClose);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(638, 72);
            this.panel2.TabIndex = 0;
            //
            // lblTitle
            //
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.lblTitle.Location = new System.Drawing.Point(22, 14);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(205, 23);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Select items to refund";
            //
            // lblSubtitle
            //
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new System.Drawing.Font("Roboto", 9.75F);
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(117)))), ((int)(((byte)(117)))));
            this.lblSubtitle.Location = new System.Drawing.Point(24, 43);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(320, 16);
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "Selecting an item also selects its modifiers.";
            //
            // btnClose
            //
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.Color.White;
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.Image = global::ThirdPartyTool.Properties.Resources.icon_close;
            this.btnClose.Location = new System.Drawing.Point(582, 16);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(40, 40);
            this.btnClose.TabIndex = 2;
            //
            // pnlColumns
            //
            this.pnlColumns.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.pnlColumns.Controls.Add(this.chkAll);
            this.pnlColumns.Controls.Add(this.label1);
            this.pnlColumns.Controls.Add(this.label2);
            this.pnlColumns.Controls.Add(this.label3);
            this.pnlColumns.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlColumns.Location = new System.Drawing.Point(0, 72);
            this.pnlColumns.Name = "pnlColumns";
            this.pnlColumns.Size = new System.Drawing.Size(638, 36);
            this.pnlColumns.TabIndex = 1;
            //
            // chkAll
            //
            this.chkAll.AutoSize = true;
            this.chkAll.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkAll.Location = new System.Drawing.Point(20, 11);
            this.chkAll.Name = "chkAll";
            this.chkAll.Size = new System.Drawing.Size(15, 14);
            this.chkAll.TabIndex = 0;
            this.chkAll.UseVisualStyleBackColor = false;
            //
            // label1
            //
            this.label1.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(97)))), ((int)(((byte)(97)))), ((int)(((byte)(97)))));
            this.label1.Location = new System.Drawing.Point(48, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(200, 36);
            this.label1.TabIndex = 1;
            this.label1.Text = "Item";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // label2
            //
            this.label2.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(97)))), ((int)(((byte)(97)))), ((int)(((byte)(97)))));
            this.label2.Location = new System.Drawing.Point(380, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(128, 36);
            this.label2.TabIndex = 2;
            this.label2.Text = "Qty";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // label3
            //
            this.label3.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(97)))), ((int)(((byte)(97)))), ((int)(((byte)(97)))));
            this.label3.Location = new System.Drawing.Point(508, 0);
            this.label3.Name = "label3";
            this.label3.Padding = new System.Windows.Forms.Padding(0, 0, 24, 0);
            this.label3.Size = new System.Drawing.Size(130, 36);
            this.label3.TabIndex = 3;
            this.label3.Text = "Amount";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            //
            // pnlRefundItemContainer
            //
            this.pnlRefundItemContainer.AutoScroll = true;
            this.pnlRefundItemContainer.BackColor = System.Drawing.Color.White;
            this.pnlRefundItemContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRefundItemContainer.Location = new System.Drawing.Point(0, 108);
            this.pnlRefundItemContainer.Name = "pnlRefundItemContainer";
            this.pnlRefundItemContainer.Size = new System.Drawing.Size(638, 354);
            this.pnlRefundItemContainer.TabIndex = 2;
            //
            // panel3
            //
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.Controls.Add(this.pnlFooterDivider);
            this.panel3.Controls.Add(this.lblTotalCaption);
            this.panel3.Controls.Add(this.lblTotalRefundItem_TotalAmount);
            this.panel3.Controls.Add(this.flpBreakdown);
            this.panel3.Controls.Add(this.btnCancel);
            this.panel3.Controls.Add(this.btnProcessRefundPartialItem);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 462);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(638, 96);
            this.panel3.TabIndex = 3;
            //
            // pnlFooterDivider
            //
            this.pnlFooterDivider.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.pnlFooterDivider.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFooterDivider.Location = new System.Drawing.Point(0, 0);
            this.pnlFooterDivider.Name = "pnlFooterDivider";
            this.pnlFooterDivider.Size = new System.Drawing.Size(638, 1);
            this.pnlFooterDivider.TabIndex = 0;
            //
            // lblTotalCaption
            //
            this.lblTotalCaption.AutoSize = true;
            this.lblTotalCaption.Font = new System.Drawing.Font("Roboto", 9.75F);
            this.lblTotalCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(117)))), ((int)(((byte)(117)))));
            this.lblTotalCaption.Location = new System.Drawing.Point(24, 12);
            this.lblTotalCaption.Name = "lblTotalCaption";
            this.lblTotalCaption.Size = new System.Drawing.Size(76, 16);
            this.lblTotalCaption.TabIndex = 1;
            this.lblTotalCaption.Text = "Refund total";
            //
            // lblTotalRefundItem_TotalAmount
            //
            this.lblTotalRefundItem_TotalAmount.AutoSize = true;
            this.lblTotalRefundItem_TotalAmount.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Bold);
            this.lblTotalRefundItem_TotalAmount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.lblTotalRefundItem_TotalAmount.Location = new System.Drawing.Point(21, 30);
            this.lblTotalRefundItem_TotalAmount.Name = "lblTotalRefundItem_TotalAmount";
            this.lblTotalRefundItem_TotalAmount.Size = new System.Drawing.Size(66, 26);
            this.lblTotalRefundItem_TotalAmount.TabIndex = 2;
            this.lblTotalRefundItem_TotalAmount.Text = "$0.00";
            //
            // flpBreakdown
            //
            this.flpBreakdown.AutoSize = true;
            this.flpBreakdown.Controls.Add(this.lblTotalRefundItem_Amount);
            this.flpBreakdown.Controls.Add(this.lblTotalRefundItem_Tax);
            this.flpBreakdown.Location = new System.Drawing.Point(22, 62);
            this.flpBreakdown.Name = "flpBreakdown";
            this.flpBreakdown.Size = new System.Drawing.Size(240, 18);
            this.flpBreakdown.TabIndex = 3;
            this.flpBreakdown.WrapContents = false;
            //
            // lblTotalRefundItem_Amount
            //
            this.lblTotalRefundItem_Amount.AutoSize = true;
            this.lblTotalRefundItem_Amount.Font = new System.Drawing.Font("Roboto", 9.75F);
            this.lblTotalRefundItem_Amount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(97)))), ((int)(((byte)(97)))), ((int)(((byte)(97)))));
            this.lblTotalRefundItem_Amount.Margin = new System.Windows.Forms.Padding(0, 0, 16, 0);
            this.lblTotalRefundItem_Amount.Name = "lblTotalRefundItem_Amount";
            this.lblTotalRefundItem_Amount.Size = new System.Drawing.Size(100, 16);
            this.lblTotalRefundItem_Amount.TabIndex = 0;
            this.lblTotalRefundItem_Amount.Text = "Subtotal $0.00";
            //
            // lblTotalRefundItem_Tax
            //
            this.lblTotalRefundItem_Tax.AutoSize = true;
            this.lblTotalRefundItem_Tax.Font = new System.Drawing.Font("Roboto", 9.75F);
            this.lblTotalRefundItem_Tax.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(97)))), ((int)(((byte)(97)))), ((int)(((byte)(97)))));
            this.lblTotalRefundItem_Tax.Margin = new System.Windows.Forms.Padding(0);
            this.lblTotalRefundItem_Tax.Name = "lblTotalRefundItem_Tax";
            this.lblTotalRefundItem_Tax.Size = new System.Drawing.Size(70, 16);
            this.lblTotalRefundItem_Tax.TabIndex = 1;
            this.lblTotalRefundItem_Tax.Text = "Tax $0.00";
            //
            // btnCancel
            //
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.BackColor = System.Drawing.Color.White;
            this.btnCancel.BackgroundColor = System.Drawing.Color.White;
            this.btnCancel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(189)))));
            this.btnCancel.BorderRadius = 5;
            this.btnCancel.BorderSize = 1;
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.Font = new System.Drawing.Font("Roboto", 11F, System.Drawing.FontStyle.Bold);
            this.btnCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.btnCancel.Location = new System.Drawing.Point(334, 28);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 44);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnCancel.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            //
            // btnProcessRefundPartialItem
            //
            this.btnProcessRefundPartialItem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnProcessRefundPartialItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.btnProcessRefundPartialItem.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.btnProcessRefundPartialItem.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.btnProcessRefundPartialItem.BorderRadius = 5;
            this.btnProcessRefundPartialItem.BorderSize = 1;
            this.btnProcessRefundPartialItem.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnProcessRefundPartialItem.Font = new System.Drawing.Font("Roboto", 11F, System.Drawing.FontStyle.Bold);
            this.btnProcessRefundPartialItem.ForeColor = System.Drawing.Color.White;
            this.btnProcessRefundPartialItem.Location = new System.Drawing.Point(444, 28);
            this.btnProcessRefundPartialItem.Name = "btnProcessRefundPartialItem";
            this.btnProcessRefundPartialItem.Size = new System.Drawing.Size(170, 44);
            this.btnProcessRefundPartialItem.TabIndex = 5;
            this.btnProcessRefundPartialItem.Text = "Refund";
            this.btnProcessRefundPartialItem.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnProcessRefundPartialItem.TextColor = System.Drawing.Color.White;
            //
            // PartialRefundItemForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(189)))));
            this.ClientSize = new System.Drawing.Size(640, 560);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "PartialRefundItemForm";
            this.Padding = new System.Windows.Forms.Padding(1);
            this.Text = "PartialRefundItemForm";
            this.panel1.ResumeLayout(false);
            this.pnlColumns.ResumeLayout(false);
            this.pnlColumns.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.flpBreakdown.ResumeLayout(false);
            this.flpBreakdown.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Label btnClose;
        private System.Windows.Forms.Panel pnlColumns;
        private System.Windows.Forms.CheckBox chkAll;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel pnlRefundItemContainer;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel pnlFooterDivider;
        private System.Windows.Forms.Label lblTotalCaption;
        private System.Windows.Forms.Label lblTotalRefundItem_TotalAmount;
        private System.Windows.Forms.FlowLayoutPanel flpBreakdown;
        private System.Windows.Forms.Label lblTotalRefundItem_Amount;
        private System.Windows.Forms.Label lblTotalRefundItem_Tax;
        private CustomControl.BLabel_Rounded btnCancel;
        private CustomControl.BLabel_Rounded btnProcessRefundPartialItem;
    }
}
