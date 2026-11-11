using ThirdPartyTool.CustomControl;

namespace ThirdPartyTool.SaleReceipt
{
    partial class PaymentDetailView
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
            this.bPanel_Rounded1 = new ThirdPartyTool.CustomControl.BPanel_Rounded();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblPaymentMethod = new System.Windows.Forms.Label();
            this.lblPaymentValue = new System.Windows.Forms.Label();
            this.lblCard = new System.Windows.Forms.Label();
            this.lblCardValue = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.lblTotalValue = new System.Windows.Forms.Label();
            this.lblTypeCaption = new System.Windows.Forms.Label();
            this.lblCardType = new System.Windows.Forms.Label();
            this.lblEntryCaption = new System.Windows.Forms.Label();
            this.lblEntryMode = new System.Windows.Forms.Label();
            this.lblApproval = new System.Windows.Forms.Label();
            this.lblApprovalValue = new System.Windows.Forms.Label();
            this.lblBatch = new System.Windows.Forms.Label();
            this.lblBatchValue = new System.Windows.Forms.Label();
            this.lblTerminal = new System.Windows.Forms.Label();
            this.lblTerminalValue = new System.Windows.Forms.Label();
            this.pnlGap = new System.Windows.Forms.Panel();
            this.pnlDivider = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlSearch = new ThirdPartyTool.CustomControl.BPanel_Rounded();
            this.txtGratuity = new ThirdPartyTool.CustomControl.NumericTextBox();
            this.btnUpdate = new ThirdPartyTool.CustomControl.BLabel_Rounded();
            this.bPanel_Rounded1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pnlSearch.SuspendLayout();
            this.SuspendLayout();
            //
            // bPanel_Rounded1
            //
            this.bPanel_Rounded1.BackColor = System.Drawing.Color.White;
            this.bPanel_Rounded1.BackgroundColor = System.Drawing.Color.White;
            this.bPanel_Rounded1.BorderColor = System.Drawing.Color.FromArgb(224, 224, 224);
            this.bPanel_Rounded1.BorderRadius = 8;
            this.bPanel_Rounded1.BorderSize = 1;
            this.bPanel_Rounded1.Controls.Add(this.panel1);
            this.bPanel_Rounded1.Controls.Add(this.pnlDivider);
            this.bPanel_Rounded1.Controls.Add(this.pnlGap);
            this.bPanel_Rounded1.Controls.Add(this.tableLayoutPanel1);
            this.bPanel_Rounded1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bPanel_Rounded1.Name = "bPanel_Rounded1";
            this.bPanel_Rounded1.Padding = new System.Windows.Forms.Padding(16, 12, 16, 8);
            this.bPanel_Rounded1.Size = new System.Drawing.Size(500, 188);
            this.bPanel_Rounded1.TabIndex = 0;
            //
            // tableLayoutPanel1
            //
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 72F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 16F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 72F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.lblPaymentMethod, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblPaymentValue, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblCard, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblCardValue, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblTotal, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblTotalValue, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblTypeCaption, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.lblCardType, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.lblEntryCaption, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.lblEntryMode, 4, 2);
            this.tableLayoutPanel1.Controls.Add(this.lblApproval, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.lblApprovalValue, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.lblBatch, 3, 3);
            this.tableLayoutPanel1.Controls.Add(this.lblBatchValue, 4, 3);
            this.tableLayoutPanel1.Controls.Add(this.lblTerminal, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.lblTerminalValue, 1, 4);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(468, 120);
            this.tableLayoutPanel1.TabIndex = 0;
            //
            // lblPaymentMethod
            //
            this.lblPaymentMethod.AutoEllipsis = true;
            this.tableLayoutPanel1.SetColumnSpan(this.lblPaymentMethod, 2);
            this.lblPaymentMethod.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPaymentMethod.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Bold);
            this.lblPaymentMethod.ForeColor = System.Drawing.Color.FromArgb(33, 33, 33);
            this.lblPaymentMethod.Margin = new System.Windows.Forms.Padding(0, 0, 0, 6);
            this.lblPaymentMethod.Name = "lblPaymentMethod";
            this.lblPaymentMethod.Size = new System.Drawing.Size(200, 26);
            this.lblPaymentMethod.TabIndex = 0;
            this.lblPaymentMethod.Text = "Payment";
            this.lblPaymentMethod.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // lblPaymentValue
            //
            this.tableLayoutPanel1.SetColumnSpan(this.lblPaymentValue, 2);
            this.lblPaymentValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPaymentValue.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Bold);
            this.lblPaymentValue.ForeColor = System.Drawing.Color.FromArgb(33, 33, 33);
            this.lblPaymentValue.Margin = new System.Windows.Forms.Padding(0, 0, 0, 6);
            this.lblPaymentValue.Name = "lblPaymentValue";
            this.lblPaymentValue.Size = new System.Drawing.Size(200, 26);
            this.lblPaymentValue.TabIndex = 1;
            this.lblPaymentValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            //
            // lblCard
            //
            this.lblCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCard.Font = new System.Drawing.Font("Roboto", 10F);
            this.lblCard.ForeColor = System.Drawing.Color.FromArgb(117, 117, 117);
            this.lblCard.Margin = new System.Windows.Forms.Padding(0);
            this.lblCard.Name = "lblCard";
            this.lblCard.Size = new System.Drawing.Size(72, 22);
            this.lblCard.Text = "Card";
            this.lblCard.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // lblCardValue
            //
            this.lblCardValue.AutoEllipsis = true;
            this.lblCardValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCardValue.Font = new System.Drawing.Font("Roboto", 10F);
            this.lblCardValue.ForeColor = System.Drawing.Color.FromArgb(33, 33, 33);
            this.lblCardValue.Margin = new System.Windows.Forms.Padding(0);
            this.lblCardValue.Name = "lblCardValue";
            this.lblCardValue.Size = new System.Drawing.Size(150, 22);
            this.lblCardValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // lblTotal
            //
            this.lblTotal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTotal.Font = new System.Drawing.Font("Roboto", 10F);
            this.lblTotal.ForeColor = System.Drawing.Color.FromArgb(117, 117, 117);
            this.lblTotal.Margin = new System.Windows.Forms.Padding(0);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(72, 22);
            this.lblTotal.Text = "Total";
            this.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // lblTotalValue
            //
            this.lblTotalValue.AutoEllipsis = true;
            this.lblTotalValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTotalValue.Font = new System.Drawing.Font("Roboto", 10F);
            this.lblTotalValue.ForeColor = System.Drawing.Color.FromArgb(33, 33, 33);
            this.lblTotalValue.Margin = new System.Windows.Forms.Padding(0);
            this.lblTotalValue.Name = "lblTotalValue";
            this.lblTotalValue.Size = new System.Drawing.Size(150, 22);
            this.lblTotalValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // lblTypeCaption
            //
            this.lblTypeCaption.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTypeCaption.Font = new System.Drawing.Font("Roboto", 10F);
            this.lblTypeCaption.ForeColor = System.Drawing.Color.FromArgb(117, 117, 117);
            this.lblTypeCaption.Margin = new System.Windows.Forms.Padding(0);
            this.lblTypeCaption.Name = "lblTypeCaption";
            this.lblTypeCaption.Size = new System.Drawing.Size(72, 22);
            this.lblTypeCaption.Text = "Type";
            this.lblTypeCaption.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // lblCardType
            //
            this.lblCardType.AutoEllipsis = true;
            this.lblCardType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCardType.Font = new System.Drawing.Font("Roboto", 10F);
            this.lblCardType.ForeColor = System.Drawing.Color.FromArgb(33, 33, 33);
            this.lblCardType.Margin = new System.Windows.Forms.Padding(0);
            this.lblCardType.Name = "lblCardType";
            this.lblCardType.Size = new System.Drawing.Size(150, 22);
            this.lblCardType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // lblEntryCaption
            //
            this.lblEntryCaption.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblEntryCaption.Font = new System.Drawing.Font("Roboto", 10F);
            this.lblEntryCaption.ForeColor = System.Drawing.Color.FromArgb(117, 117, 117);
            this.lblEntryCaption.Margin = new System.Windows.Forms.Padding(0);
            this.lblEntryCaption.Name = "lblEntryCaption";
            this.lblEntryCaption.Size = new System.Drawing.Size(72, 22);
            this.lblEntryCaption.Text = "Entry";
            this.lblEntryCaption.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // lblEntryMode
            //
            this.lblEntryMode.AutoEllipsis = true;
            this.lblEntryMode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblEntryMode.Font = new System.Drawing.Font("Roboto", 10F);
            this.lblEntryMode.ForeColor = System.Drawing.Color.FromArgb(33, 33, 33);
            this.lblEntryMode.Margin = new System.Windows.Forms.Padding(0);
            this.lblEntryMode.Name = "lblEntryMode";
            this.lblEntryMode.Size = new System.Drawing.Size(150, 22);
            this.lblEntryMode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // lblApproval
            //
            this.lblApproval.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblApproval.Font = new System.Drawing.Font("Roboto", 10F);
            this.lblApproval.ForeColor = System.Drawing.Color.FromArgb(117, 117, 117);
            this.lblApproval.Margin = new System.Windows.Forms.Padding(0);
            this.lblApproval.Name = "lblApproval";
            this.lblApproval.Size = new System.Drawing.Size(72, 22);
            this.lblApproval.Text = "Approval";
            this.lblApproval.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // lblApprovalValue
            //
            this.lblApprovalValue.AutoEllipsis = true;
            this.lblApprovalValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblApprovalValue.Font = new System.Drawing.Font("Roboto", 10F);
            this.lblApprovalValue.ForeColor = System.Drawing.Color.FromArgb(33, 33, 33);
            this.lblApprovalValue.Margin = new System.Windows.Forms.Padding(0);
            this.lblApprovalValue.Name = "lblApprovalValue";
            this.lblApprovalValue.Size = new System.Drawing.Size(150, 22);
            this.lblApprovalValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // lblBatch
            //
            this.lblBatch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblBatch.Font = new System.Drawing.Font("Roboto", 10F);
            this.lblBatch.ForeColor = System.Drawing.Color.FromArgb(117, 117, 117);
            this.lblBatch.Margin = new System.Windows.Forms.Padding(0);
            this.lblBatch.Name = "lblBatch";
            this.lblBatch.Size = new System.Drawing.Size(72, 22);
            this.lblBatch.Text = "Batch";
            this.lblBatch.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // lblBatchValue
            //
            this.lblBatchValue.AutoEllipsis = true;
            this.lblBatchValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblBatchValue.Font = new System.Drawing.Font("Roboto", 10F);
            this.lblBatchValue.ForeColor = System.Drawing.Color.FromArgb(33, 33, 33);
            this.lblBatchValue.Margin = new System.Windows.Forms.Padding(0);
            this.lblBatchValue.Name = "lblBatchValue";
            this.lblBatchValue.Size = new System.Drawing.Size(150, 22);
            this.lblBatchValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // lblTerminal
            //
            this.lblTerminal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTerminal.Font = new System.Drawing.Font("Roboto", 10F);
            this.lblTerminal.ForeColor = System.Drawing.Color.FromArgb(117, 117, 117);
            this.lblTerminal.Margin = new System.Windows.Forms.Padding(0);
            this.lblTerminal.Name = "lblTerminal";
            this.lblTerminal.Size = new System.Drawing.Size(72, 22);
            this.lblTerminal.Text = "Terminal";
            this.lblTerminal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // lblTerminalValue
            //
            this.lblTerminalValue.AutoEllipsis = true;
            this.lblTerminalValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTerminalValue.Font = new System.Drawing.Font("Roboto", 10F);
            this.lblTerminalValue.ForeColor = System.Drawing.Color.FromArgb(33, 33, 33);
            this.lblTerminalValue.Margin = new System.Windows.Forms.Padding(0);
            this.lblTerminalValue.Name = "lblTerminalValue";
            this.lblTerminalValue.Size = new System.Drawing.Size(150, 22);
            this.lblTerminalValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // pnlGap
            //
            this.pnlGap.BackColor = System.Drawing.Color.White;
            this.pnlGap.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlGap.Name = "pnlGap";
            this.pnlGap.Size = new System.Drawing.Size(468, 10);
            //
            // pnlDivider
            //
            this.pnlDivider.BackColor = System.Drawing.Color.FromArgb(235, 235, 235);
            this.pnlDivider.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlDivider.Name = "pnlDivider";
            this.pnlDivider.Size = new System.Drawing.Size(468, 1);
            //
            // panel1
            //
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.pnlSearch);
            this.panel1.Controls.Add(this.btnUpdate);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(468, 56);
            this.panel1.TabIndex = 1;
            //
            // label1
            //
            this.label1.Font = new System.Drawing.Font("Roboto", 11F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(33, 33, 33);
            this.label1.Location = new System.Drawing.Point(0, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 40);
            this.label1.TabIndex = 0;
            this.label1.Text = "Tip";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // pnlSearch
            //
            this.pnlSearch.BackColor = System.Drawing.Color.White;
            this.pnlSearch.BackgroundColor = System.Drawing.Color.White;
            this.pnlSearch.BorderColor = System.Drawing.Color.FromArgb(189, 189, 189);
            this.pnlSearch.BorderRadius = 5;
            this.pnlSearch.BorderSize = 1;
            this.pnlSearch.Controls.Add(this.txtGratuity);
            this.pnlSearch.Location = new System.Drawing.Point(44, 10);
            this.pnlSearch.Name = "pnlSearch";
            this.pnlSearch.Size = new System.Drawing.Size(160, 40);
            this.pnlSearch.TabIndex = 1;
            //
            // txtGratuity
            //
            this.txtGratuity.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtGratuity.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtGratuity.Font = new System.Drawing.Font("Roboto", 11F);
            this.txtGratuity.FormatSpecifier = "c2";
            this.txtGratuity.Location = new System.Drawing.Point(12, 11);
            this.txtGratuity.MaxValue = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.txtGratuity.Name = "txtGratuity";
            this.txtGratuity.Size = new System.Drawing.Size(136, 18);
            this.txtGratuity.TabIndex = 0;
            this.txtGratuity.Text = "0.00";
            this.txtGratuity.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtGratuity.Value = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            //
            // btnUpdate
            //
            this.btnUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUpdate.BackColor = System.Drawing.Color.FromArgb(42, 108, 228);
            this.btnUpdate.BackgroundColor = System.Drawing.Color.FromArgb(42, 108, 228);
            this.btnUpdate.BorderColor = System.Drawing.Color.FromArgb(42, 108, 228);
            this.btnUpdate.BorderRadius = 5;
            this.btnUpdate.BorderSize = 1;
            this.btnUpdate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUpdate.Font = new System.Drawing.Font("Roboto", 11F, System.Drawing.FontStyle.Bold);
            this.btnUpdate.ForeColor = System.Drawing.Color.White;
            this.btnUpdate.Location = new System.Drawing.Point(358, 10);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(110, 40);
            this.btnUpdate.TabIndex = 2;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnUpdate.TextColor = System.Drawing.Color.White;
            //
            // PaymentDetailView
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.bPanel_Rounded1);
            this.Name = "PaymentDetailView";
            this.Padding = new System.Windows.Forms.Padding(0, 0, 0, 12);
            this.Size = new System.Drawing.Size(500, 200);
            this.bPanel_Rounded1.ResumeLayout(false);
            this.bPanel_Rounded1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.pnlSearch.ResumeLayout(false);
            this.pnlSearch.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public BPanel_Rounded bPanel_Rounded1;
        public System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        public System.Windows.Forms.Label lblPaymentMethod;
        public System.Windows.Forms.Label lblPaymentValue;
        public System.Windows.Forms.Label lblCard;
        public System.Windows.Forms.Label lblCardValue;
        public System.Windows.Forms.Label lblTotal;
        public System.Windows.Forms.Label lblTotalValue;
        public System.Windows.Forms.Label lblTypeCaption;
        public System.Windows.Forms.Label lblCardType;
        public System.Windows.Forms.Label lblEntryCaption;
        public System.Windows.Forms.Label lblEntryMode;
        public System.Windows.Forms.Label lblApproval;
        public System.Windows.Forms.Label lblApprovalValue;
        public System.Windows.Forms.Label lblBatch;
        public System.Windows.Forms.Label lblBatchValue;
        public System.Windows.Forms.Label lblTerminal;
        public System.Windows.Forms.Label lblTerminalValue;
        private System.Windows.Forms.Panel pnlGap;
        private System.Windows.Forms.Panel pnlDivider;
        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.Label label1;
        private BPanel_Rounded pnlSearch;
        public NumericTextBox txtGratuity;
        public BLabel_Rounded btnUpdate;
    }
}
