namespace ThirdPartyTool.SaleReceipt
{
    partial class SaleReceiptPaymentDetailControl
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
            this.pnlContainer = new ThirdPartyTool.CustomControl.BPanel_Rounded();
            this.lblPaymentType = new System.Windows.Forms.Label();
            this.lblPaymentTypeValue = new System.Windows.Forms.Label();
            this.flpBreakdown = new System.Windows.Forms.FlowLayoutPanel();
            this.lblTotal = new System.Windows.Forms.Label();
            this.lblTotalValue = new System.Windows.Forms.Label();
            this.lblTipText = new System.Windows.Forms.Label();
            this.lblTipValue = new System.Windows.Forms.Label();
            this.lblFinalTotalText = new System.Windows.Forms.Label();
            this.lblFinalTotalValue = new System.Windows.Forms.Label();
            this.pnlContainer.SuspendLayout();
            this.flpBreakdown.SuspendLayout();
            this.SuspendLayout();
            //
            // pnlContainer
            //
            this.pnlContainer.BackColor = System.Drawing.Color.White;
            this.pnlContainer.BackgroundColor = System.Drawing.Color.White;
            this.pnlContainer.BorderColor = System.Drawing.Color.FromArgb(224, 224, 224);
            this.pnlContainer.BorderRadius = 8;
            this.pnlContainer.BorderSize = 1;
            this.pnlContainer.Controls.Add(this.lblPaymentType);
            this.pnlContainer.Controls.Add(this.lblPaymentTypeValue);
            this.pnlContainer.Controls.Add(this.flpBreakdown);
            this.pnlContainer.Controls.Add(this.lblFinalTotalText);
            this.pnlContainer.Controls.Add(this.lblFinalTotalValue);
            this.pnlContainer.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pnlContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContainer.Name = "pnlContainer";
            this.pnlContainer.Size = new System.Drawing.Size(556, 66);
            this.pnlContainer.TabIndex = 0;
            //
            // lblPaymentType (kept for compatibility, not shown)
            //
            this.lblPaymentType.Name = "lblPaymentType";
            this.lblPaymentType.Text = "Payment Type:";
            this.lblPaymentType.Visible = false;
            //
            // lblPaymentTypeValue
            //
            this.lblPaymentTypeValue.AutoEllipsis = true;
            this.lblPaymentTypeValue.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Bold);
            this.lblPaymentTypeValue.ForeColor = System.Drawing.Color.FromArgb(33, 33, 33);
            this.lblPaymentTypeValue.Location = new System.Drawing.Point(48, 10);
            this.lblPaymentTypeValue.Name = "lblPaymentTypeValue";
            this.lblPaymentTypeValue.Size = new System.Drawing.Size(320, 24);
            this.lblPaymentTypeValue.TabIndex = 0;
            this.lblPaymentTypeValue.Text = "";
            //
            // flpBreakdown
            //
            this.flpBreakdown.AutoSize = true;
            this.flpBreakdown.Controls.Add(this.lblTotal);
            this.flpBreakdown.Controls.Add(this.lblTotalValue);
            this.flpBreakdown.Controls.Add(this.lblTipText);
            this.flpBreakdown.Controls.Add(this.lblTipValue);
            this.flpBreakdown.Location = new System.Drawing.Point(48, 36);
            this.flpBreakdown.Name = "flpBreakdown";
            this.flpBreakdown.Size = new System.Drawing.Size(240, 18);
            this.flpBreakdown.WrapContents = false;
            //
            // lblTotal
            //
            this.lblTotal.AutoSize = true;
            this.lblTotal.Font = new System.Drawing.Font("Roboto", 9.75F);
            this.lblTotal.ForeColor = System.Drawing.Color.FromArgb(117, 117, 117);
            this.lblTotal.Margin = new System.Windows.Forms.Padding(0);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Text = "Total ";
            //
            // lblTotalValue
            //
            this.lblTotalValue.AutoSize = true;
            this.lblTotalValue.Font = new System.Drawing.Font("Roboto", 9.75F);
            this.lblTotalValue.ForeColor = System.Drawing.Color.FromArgb(66, 66, 66);
            this.lblTotalValue.Margin = new System.Windows.Forms.Padding(0, 0, 14, 0);
            this.lblTotalValue.Name = "lblTotalValue";
            this.lblTotalValue.Text = "";
            //
            // lblTipText
            //
            this.lblTipText.AutoSize = true;
            this.lblTipText.Font = new System.Drawing.Font("Roboto", 9.75F);
            this.lblTipText.ForeColor = System.Drawing.Color.FromArgb(117, 117, 117);
            this.lblTipText.Margin = new System.Windows.Forms.Padding(0);
            this.lblTipText.Name = "lblTipText";
            this.lblTipText.Text = "Tip ";
            //
            // lblTipValue
            //
            this.lblTipValue.AutoSize = true;
            this.lblTipValue.Font = new System.Drawing.Font("Roboto", 9.75F);
            this.lblTipValue.ForeColor = System.Drawing.Color.FromArgb(66, 66, 66);
            this.lblTipValue.Margin = new System.Windows.Forms.Padding(0);
            this.lblTipValue.Name = "lblTipValue";
            this.lblTipValue.Text = "";
            //
            // lblFinalTotalText
            //
            this.lblFinalTotalText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFinalTotalText.Font = new System.Drawing.Font("Roboto", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblFinalTotalText.ForeColor = System.Drawing.Color.FromArgb(117, 117, 117);
            this.lblFinalTotalText.Location = new System.Drawing.Point(396, 12);
            this.lblFinalTotalText.Name = "lblFinalTotalText";
            this.lblFinalTotalText.Size = new System.Drawing.Size(144, 16);
            this.lblFinalTotalText.TabIndex = 1;
            this.lblFinalTotalText.Text = "FINAL TOTAL";
            this.lblFinalTotalText.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            //
            // lblFinalTotalValue
            //
            this.lblFinalTotalValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFinalTotalValue.Font = new System.Drawing.Font("Roboto", 13F, System.Drawing.FontStyle.Bold);
            this.lblFinalTotalValue.ForeColor = System.Drawing.Color.FromArgb(33, 33, 33);
            this.lblFinalTotalValue.Location = new System.Drawing.Point(396, 29);
            this.lblFinalTotalValue.Name = "lblFinalTotalValue";
            this.lblFinalTotalValue.Size = new System.Drawing.Size(144, 26);
            this.lblFinalTotalValue.TabIndex = 2;
            this.lblFinalTotalValue.Text = "";
            this.lblFinalTotalValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            //
            // SaleReceiptPaymentDetailControl
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.pnlContainer);
            this.Margin = new System.Windows.Forms.Padding(0, 0, 0, 10);
            this.Name = "SaleReceiptPaymentDetailControl";
            this.Size = new System.Drawing.Size(556, 66);
            this.pnlContainer.ResumeLayout(false);
            this.pnlContainer.PerformLayout();
            this.flpBreakdown.ResumeLayout(false);
            this.flpBreakdown.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private CustomControl.BPanel_Rounded pnlContainer;
        private System.Windows.Forms.FlowLayoutPanel flpBreakdown;
        private System.Windows.Forms.Label lblFinalTotalText;
        private System.Windows.Forms.Label lblTipText;
        private System.Windows.Forms.Label lblTotal;
        public System.Windows.Forms.Label lblFinalTotalValue;
        public System.Windows.Forms.Label lblTipValue;
        public System.Windows.Forms.Label lblTotalValue;
        public System.Windows.Forms.Label lblPaymentTypeValue;
        private System.Windows.Forms.Label lblPaymentType;
    }
}
