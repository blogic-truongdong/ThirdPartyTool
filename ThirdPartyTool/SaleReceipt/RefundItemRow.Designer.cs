namespace ThirdPartyTool.SaleReceipt
{
    partial class RefundItemRow
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.chkCheck = new System.Windows.Forms.CheckBox();
            this.lblItemName = new System.Windows.Forms.Label();
            this.txtQuantity = new System.Windows.Forms.Label();
            this.lblPrice = new System.Windows.Forms.Label();
            this.SuspendLayout();
            //
            // chkCheck
            //
            this.chkCheck.AutoSize = true;
            this.chkCheck.BackColor = System.Drawing.Color.Transparent;
            this.chkCheck.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkCheck.Location = new System.Drawing.Point(20, 16);
            this.chkCheck.Name = "chkCheck";
            this.chkCheck.Size = new System.Drawing.Size(15, 14);
            this.chkCheck.TabIndex = 0;
            this.chkCheck.UseVisualStyleBackColor = false;
            //
            // lblItemName
            //
            this.lblItemName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblItemName.AutoEllipsis = true;
            this.lblItemName.BackColor = System.Drawing.Color.Transparent;
            this.lblItemName.Font = new System.Drawing.Font("Roboto", 11F, System.Drawing.FontStyle.Bold);
            this.lblItemName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.lblItemName.Location = new System.Drawing.Point(48, 0);
            this.lblItemName.Name = "lblItemName";
            this.lblItemName.Size = new System.Drawing.Size(294, 46);
            this.lblItemName.TabIndex = 1;
            this.lblItemName.Text = "Item Name";
            this.lblItemName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // txtQuantity
            //
            this.txtQuantity.BackColor = System.Drawing.Color.Transparent;
            this.txtQuantity.Dock = System.Windows.Forms.DockStyle.Right;
            this.txtQuantity.Font = new System.Drawing.Font("Roboto", 11F);
            this.txtQuantity.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.txtQuantity.Location = new System.Drawing.Point(342, 0);
            this.txtQuantity.Name = "txtQuantity";
            this.txtQuantity.Size = new System.Drawing.Size(128, 46);
            this.txtQuantity.TabIndex = 2;
            this.txtQuantity.Text = "1";
            this.txtQuantity.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // lblPrice
            //
            this.lblPrice.BackColor = System.Drawing.Color.Transparent;
            this.lblPrice.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblPrice.Font = new System.Drawing.Font("Roboto", 11F, System.Drawing.FontStyle.Bold);
            this.lblPrice.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.lblPrice.Location = new System.Drawing.Point(470, 0);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Padding = new System.Windows.Forms.Padding(0, 0, 24, 0);
            this.lblPrice.Size = new System.Drawing.Size(130, 46);
            this.lblPrice.TabIndex = 3;
            this.lblPrice.Text = "Price";
            this.lblPrice.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            //
            // RefundItemRow
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.chkCheck);
            this.Controls.Add(this.lblItemName);
            this.Controls.Add(this.txtQuantity);
            this.Controls.Add(this.lblPrice);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Name = "RefundItemRow";
            this.Size = new System.Drawing.Size(600, 46);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label lblItemName;
        public System.Windows.Forms.Label lblPrice;
        public System.Windows.Forms.Label txtQuantity;
        public System.Windows.Forms.CheckBox chkCheck;
    }
}
