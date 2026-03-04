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
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnProcessRefundPartialItem = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblTotalRefundItem_Amount = new System.Windows.Forms.Label();
            this.lblTotalRefundItem_Tax = new System.Windows.Forms.Label();
            this.lblTotalRefundItem_TotalAmount = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Label();
            this.pnlRefundItemContainer = new System.Windows.Forms.Panel();
            this.refundItemRow1 = new ThirdPartyTool.SaleReceipt.RefundItemRow();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.pnlRefundItemContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.pnlRefundItemContainer);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(602, 500);
            this.panel1.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.Controls.Add(this.lblTotalRefundItem_Amount);
            this.panel3.Controls.Add(this.btnProcessRefundPartialItem);
            this.panel3.Controls.Add(this.lblTotalRefundItem_Tax);
            this.panel3.Controls.Add(this.lblTotalRefundItem_TotalAmount);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 408);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(600, 90);
            this.panel3.TabIndex = 3;
            // 
            // btnProcessRefundPartialItem
            // 
            this.btnProcessRefundPartialItem.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnProcessRefundPartialItem.BackColor = System.Drawing.Color.Red;
            this.btnProcessRefundPartialItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold);
            this.btnProcessRefundPartialItem.ForeColor = System.Drawing.Color.White;
            this.btnProcessRefundPartialItem.Location = new System.Drawing.Point(200, 40);
            this.btnProcessRefundPartialItem.Name = "btnProcessRefundPartialItem";
            this.btnProcessRefundPartialItem.Size = new System.Drawing.Size(200, 40);
            this.btnProcessRefundPartialItem.TabIndex = 2;
            this.btnProcessRefundPartialItem.Text = "Refund";
            this.btnProcessRefundPartialItem.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.btnClose);
            this.panel2.Controls.Add(this.lblTitle);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(0, 0, 0, 30);
            this.panel2.Size = new System.Drawing.Size(600, 80);
            this.panel2.TabIndex = 2;
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.White;
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitle.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(600, 50);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "Select Items to Refund";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTotalRefundItem_Amount
            // 
            this.lblTotalRefundItem_Amount.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblTotalRefundItem_Amount.Font = new System.Drawing.Font("Roboto", 11F, System.Drawing.FontStyle.Bold);
            this.lblTotalRefundItem_Amount.ForeColor = System.Drawing.Color.Black;
            this.lblTotalRefundItem_Amount.Location = new System.Drawing.Point(30, 10);
            this.lblTotalRefundItem_Amount.Name = "lblTotalRefundItem_Amount";
            this.lblTotalRefundItem_Amount.Size = new System.Drawing.Size(180, 23);
            this.lblTotalRefundItem_Amount.TabIndex = 36;
            this.lblTotalRefundItem_Amount.Text = "Amount: 0";
            this.lblTotalRefundItem_Amount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTotalRefundItem_Tax
            // 
            this.lblTotalRefundItem_Tax.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblTotalRefundItem_Tax.Font = new System.Drawing.Font("Roboto", 11F, System.Drawing.FontStyle.Bold);
            this.lblTotalRefundItem_Tax.ForeColor = System.Drawing.Color.Black;
            this.lblTotalRefundItem_Tax.Location = new System.Drawing.Point(210, 10);
            this.lblTotalRefundItem_Tax.Name = "lblTotalRefundItem_Tax";
            this.lblTotalRefundItem_Tax.Size = new System.Drawing.Size(180, 23);
            this.lblTotalRefundItem_Tax.TabIndex = 35;
            this.lblTotalRefundItem_Tax.Text = "Sales Tax: 0";
            this.lblTotalRefundItem_Tax.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTotalRefundItem_TotalAmount
            // 
            this.lblTotalRefundItem_TotalAmount.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblTotalRefundItem_TotalAmount.Font = new System.Drawing.Font("Roboto", 11F, System.Drawing.FontStyle.Bold);
            this.lblTotalRefundItem_TotalAmount.ForeColor = System.Drawing.Color.Black;
            this.lblTotalRefundItem_TotalAmount.Location = new System.Drawing.Point(390, 10);
            this.lblTotalRefundItem_TotalAmount.Name = "lblTotalRefundItem_TotalAmount";
            this.lblTotalRefundItem_TotalAmount.Size = new System.Drawing.Size(180, 23);
            this.lblTotalRefundItem_TotalAmount.TabIndex = 34;
            this.lblTotalRefundItem_TotalAmount.Text = "Total Refund: 0";
            this.lblTotalRefundItem_TotalAmount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.Color.White;
            this.btnClose.Image = global::ThirdPartyTool.Properties.Resources.icon_close;
            this.btnClose.Location = new System.Drawing.Point(555, 5);
            this.btnClose.Margin = new System.Windows.Forms.Padding(3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(40, 40);
            this.btnClose.TabIndex = 0;
            // 
            // pnlRefundItemContainer
            // 
            this.pnlRefundItemContainer.AutoScroll = true;
            this.pnlRefundItemContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlRefundItemContainer.Controls.Add(this.refundItemRow1);
            this.pnlRefundItemContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRefundItemContainer.Location = new System.Drawing.Point(0, 80);
            this.pnlRefundItemContainer.Name = "pnlRefundItemContainer";
            this.pnlRefundItemContainer.Padding = new System.Windows.Forms.Padding(0, 0, 0, 110);
            this.pnlRefundItemContainer.Size = new System.Drawing.Size(600, 328);
            this.pnlRefundItemContainer.TabIndex = 4;
            // 
            // refundItemRow1
            // 
            this.refundItemRow1.BackColor = System.Drawing.Color.White;
            this.refundItemRow1.Dock = System.Windows.Forms.DockStyle.Top;
            this.refundItemRow1.Location = new System.Drawing.Point(0, 0);
            this.refundItemRow1.Name = "refundItemRow1";
            this.refundItemRow1.Size = new System.Drawing.Size(598, 40);
            this.refundItemRow1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(39, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 19);
            this.label1.TabIndex = 37;
            this.label1.Text = "Item Name";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(418, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 19);
            this.label2.TabIndex = 37;
            this.label2.Text = "Quantity";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(517, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 19);
            this.label3.TabIndex = 37;
            this.label3.Text = "Amount";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PartialRefundItemForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(602, 500);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "PartialRefundItemForm";
            this.Text = "PartialRefundItemForm";
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.pnlRefundItemContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label btnProcessRefundPartialItem;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label btnClose;
        private System.Windows.Forms.Label lblTotalRefundItem_Amount;
        private System.Windows.Forms.Label lblTotalRefundItem_Tax;
        private System.Windows.Forms.Label lblTotalRefundItem_TotalAmount;
        private System.Windows.Forms.Panel pnlRefundItemContainer;
        private RefundItemRow refundItemRow1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}