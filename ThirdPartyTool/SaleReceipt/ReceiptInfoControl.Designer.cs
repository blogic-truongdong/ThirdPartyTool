namespace ThirdPartyTool.SaleReceipt
{
    partial class ReceiptInfoControl
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
            this.flpTitle = new System.Windows.Forms.FlowLayoutPanel();
            this.lblTicketNumberText = new System.Windows.Forms.Label();
            this.lblTicketNumberValue = new System.Windows.Forms.Label();
            this.lblStatusValue = new System.Windows.Forms.Label();
            this.flpDate = new System.Windows.Forms.FlowLayoutPanel();
            this.lblDateText = new System.Windows.Forms.Label();
            this.lblDateValue = new System.Windows.Forms.Label();
            this.lblStatusText = new System.Windows.Forms.Label();
            this.tlpAmounts = new System.Windows.Forms.TableLayoutPanel();
            this.lblTotalText = new System.Windows.Forms.Label();
            this.lblTotalValue = new System.Windows.Forms.Label();
            this.lblTipText = new System.Windows.Forms.Label();
            this.lblTipValue = new System.Windows.Forms.Label();
            this.lblFinalTotalText = new System.Windows.Forms.Label();
            this.lblFinalTotalValue = new System.Windows.Forms.Label();
            this.pnlContainer.SuspendLayout();
            this.flpTitle.SuspendLayout();
            this.flpDate.SuspendLayout();
            this.tlpAmounts.SuspendLayout();
            this.SuspendLayout();
            //
            // pnlContainer
            //
            this.pnlContainer.BackColor = System.Drawing.Color.White;
            this.pnlContainer.BackgroundColor = System.Drawing.Color.White;
            this.pnlContainer.BorderColor = System.Drawing.Color.FromArgb(224, 224, 224);
            this.pnlContainer.BorderRadius = 8;
            this.pnlContainer.BorderSize = 1;
            this.pnlContainer.Controls.Add(this.flpTitle);
            this.pnlContainer.Controls.Add(this.flpDate);
            this.pnlContainer.Controls.Add(this.tlpAmounts);
            this.pnlContainer.Controls.Add(this.lblStatusText);
            this.pnlContainer.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pnlContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContainer.Name = "pnlContainer";
            this.pnlContainer.Size = new System.Drawing.Size(556, 78);
            this.pnlContainer.TabIndex = 0;
            //
            // flpTitle
            //
            this.flpTitle.AutoSize = true;
            this.flpTitle.BackColor = System.Drawing.Color.White;
            this.flpTitle.Controls.Add(this.lblTicketNumberText);
            this.flpTitle.Controls.Add(this.lblTicketNumberValue);
            this.flpTitle.Controls.Add(this.lblStatusValue);
            this.flpTitle.Location = new System.Drawing.Point(16, 14);
            this.flpTitle.Name = "flpTitle";
            this.flpTitle.Size = new System.Drawing.Size(250, 24);
            this.flpTitle.WrapContents = false;
            //
            // lblTicketNumberText
            //
            this.lblTicketNumberText.AutoSize = true;
            this.lblTicketNumberText.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Bold);
            this.lblTicketNumberText.ForeColor = System.Drawing.Color.FromArgb(33, 33, 33);
            this.lblTicketNumberText.Margin = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.lblTicketNumberText.Name = "lblTicketNumberText";
            this.lblTicketNumberText.Text = "Ticket #";
            //
            // lblTicketNumberValue
            //
            this.lblTicketNumberValue.AutoSize = true;
            this.lblTicketNumberValue.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Bold);
            this.lblTicketNumberValue.ForeColor = System.Drawing.Color.FromArgb(33, 33, 33);
            this.lblTicketNumberValue.Margin = new System.Windows.Forms.Padding(0, 2, 10, 0);
            this.lblTicketNumberValue.Name = "lblTicketNumberValue";
            this.lblTicketNumberValue.Text = "";
            //
            // lblStatusValue
            //
            this.lblStatusValue.AutoSize = true;
            this.lblStatusValue.BackColor = System.Drawing.Color.FromArgb(238, 238, 238);
            this.lblStatusValue.Font = new System.Drawing.Font("Roboto", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblStatusValue.ForeColor = System.Drawing.Color.FromArgb(97, 97, 97);
            this.lblStatusValue.Margin = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.lblStatusValue.Name = "lblStatusValue";
            this.lblStatusValue.Padding = new System.Windows.Forms.Padding(6, 2, 6, 2);
            this.lblStatusValue.Text = "";
            //
            // flpDate
            //
            this.flpDate.AutoSize = true;
            this.flpDate.BackColor = System.Drawing.Color.White;
            this.flpDate.Controls.Add(this.lblDateText);
            this.flpDate.Controls.Add(this.lblDateValue);
            this.flpDate.Location = new System.Drawing.Point(16, 44);
            this.flpDate.Name = "flpDate";
            this.flpDate.Size = new System.Drawing.Size(250, 18);
            this.flpDate.WrapContents = false;
            //
            // lblDateText
            //
            this.lblDateText.AutoSize = true;
            this.lblDateText.Font = new System.Drawing.Font("Roboto", 9.75F);
            this.lblDateText.ForeColor = System.Drawing.Color.FromArgb(117, 117, 117);
            this.lblDateText.Margin = new System.Windows.Forms.Padding(0);
            this.lblDateText.Name = "lblDateText";
            this.lblDateText.Text = "Created ";
            //
            // lblDateValue
            //
            this.lblDateValue.AutoSize = true;
            this.lblDateValue.Font = new System.Drawing.Font("Roboto", 9.75F);
            this.lblDateValue.ForeColor = System.Drawing.Color.FromArgb(117, 117, 117);
            this.lblDateValue.Margin = new System.Windows.Forms.Padding(0);
            this.lblDateValue.Name = "lblDateValue";
            this.lblDateValue.Text = "";
            //
            // lblStatusText (kept for compatibility, not shown: the pill already says the status)
            //
            this.lblStatusText.Name = "lblStatusText";
            this.lblStatusText.Text = "Status:";
            this.lblStatusText.Visible = false;
            //
            // tlpAmounts
            //
            this.tlpAmounts.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tlpAmounts.BackColor = System.Drawing.Color.White;
            this.tlpAmounts.ColumnCount = 2;
            this.tlpAmounts.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpAmounts.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.tlpAmounts.Controls.Add(this.lblTotalText, 0, 0);
            this.tlpAmounts.Controls.Add(this.lblTotalValue, 1, 0);
            this.tlpAmounts.Controls.Add(this.lblTipText, 0, 1);
            this.tlpAmounts.Controls.Add(this.lblTipValue, 1, 1);
            this.tlpAmounts.Controls.Add(this.lblFinalTotalText, 0, 2);
            this.tlpAmounts.Controls.Add(this.lblFinalTotalValue, 1, 2);
            this.tlpAmounts.Location = new System.Drawing.Point(366, 9);
            this.tlpAmounts.Name = "tlpAmounts";
            this.tlpAmounts.RowCount = 3;
            this.tlpAmounts.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 19F));
            this.tlpAmounts.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 19F));
            this.tlpAmounts.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tlpAmounts.Size = new System.Drawing.Size(174, 60);
            //
            // lblTotalText
            //
            this.lblTotalText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTotalText.Font = new System.Drawing.Font("Roboto", 9.75F);
            this.lblTotalText.ForeColor = System.Drawing.Color.FromArgb(117, 117, 117);
            this.lblTotalText.Margin = new System.Windows.Forms.Padding(0);
            this.lblTotalText.Name = "lblTotalText";
            this.lblTotalText.Text = "Total";
            this.lblTotalText.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            //
            // lblTotalValue
            //
            this.lblTotalValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTotalValue.Font = new System.Drawing.Font("Roboto", 9.75F);
            this.lblTotalValue.ForeColor = System.Drawing.Color.FromArgb(66, 66, 66);
            this.lblTotalValue.Margin = new System.Windows.Forms.Padding(0);
            this.lblTotalValue.Name = "lblTotalValue";
            this.lblTotalValue.Text = "";
            this.lblTotalValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            //
            // lblTipText
            //
            this.lblTipText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTipText.Font = new System.Drawing.Font("Roboto", 9.75F);
            this.lblTipText.ForeColor = System.Drawing.Color.FromArgb(117, 117, 117);
            this.lblTipText.Margin = new System.Windows.Forms.Padding(0);
            this.lblTipText.Name = "lblTipText";
            this.lblTipText.Text = "Tip";
            this.lblTipText.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            //
            // lblTipValue
            //
            this.lblTipValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTipValue.Font = new System.Drawing.Font("Roboto", 9.75F);
            this.lblTipValue.ForeColor = System.Drawing.Color.FromArgb(66, 66, 66);
            this.lblTipValue.Margin = new System.Windows.Forms.Padding(0);
            this.lblTipValue.Name = "lblTipValue";
            this.lblTipValue.Text = "";
            this.lblTipValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            //
            // lblFinalTotalText
            //
            this.lblFinalTotalText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblFinalTotalText.Font = new System.Drawing.Font("Roboto", 10.5F, System.Drawing.FontStyle.Bold);
            this.lblFinalTotalText.ForeColor = System.Drawing.Color.FromArgb(33, 33, 33);
            this.lblFinalTotalText.Margin = new System.Windows.Forms.Padding(0);
            this.lblFinalTotalText.Name = "lblFinalTotalText";
            this.lblFinalTotalText.Text = "Final total";
            this.lblFinalTotalText.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            //
            // lblFinalTotalValue
            //
            this.lblFinalTotalValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblFinalTotalValue.Font = new System.Drawing.Font("Roboto", 10.5F, System.Drawing.FontStyle.Bold);
            this.lblFinalTotalValue.ForeColor = System.Drawing.Color.FromArgb(33, 33, 33);
            this.lblFinalTotalValue.Margin = new System.Windows.Forms.Padding(0);
            this.lblFinalTotalValue.Name = "lblFinalTotalValue";
            this.lblFinalTotalValue.Text = "";
            this.lblFinalTotalValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            //
            // ReceiptInfoControl
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.pnlContainer);
            this.Margin = new System.Windows.Forms.Padding(0, 0, 0, 10);
            this.Name = "ReceiptInfoControl";
            this.Size = new System.Drawing.Size(556, 78);
            this.pnlContainer.ResumeLayout(false);
            this.pnlContainer.PerformLayout();
            this.flpTitle.ResumeLayout(false);
            this.flpTitle.PerformLayout();
            this.flpDate.ResumeLayout(false);
            this.flpDate.PerformLayout();
            this.tlpAmounts.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private CustomControl.BPanel_Rounded pnlContainer;
        private System.Windows.Forms.FlowLayoutPanel flpTitle;
        private System.Windows.Forms.FlowLayoutPanel flpDate;
        private System.Windows.Forms.TableLayoutPanel tlpAmounts;
        private System.Windows.Forms.Label lblTicketNumberText;
        private System.Windows.Forms.Label lblFinalTotalText;
        private System.Windows.Forms.Label lblTipText;
        private System.Windows.Forms.Label lblTotalText;
        private System.Windows.Forms.Label lblStatusText;
        public System.Windows.Forms.Label lblFinalTotalValue;
        public System.Windows.Forms.Label lblTipValue;
        public System.Windows.Forms.Label lblTotalValue;
        public System.Windows.Forms.Label lblStatusValue;
        public System.Windows.Forms.Label lblTicketNumberValue;
        public System.Windows.Forms.Label lblDateValue;
        private System.Windows.Forms.Label lblDateText;
    }
}
