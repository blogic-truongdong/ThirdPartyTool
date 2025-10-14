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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblTerminalValue = new System.Windows.Forms.Label();
            this.lblTerminal = new System.Windows.Forms.Label();
            this.lblBatchValue = new System.Windows.Forms.Label();
            this.lblBatch = new System.Windows.Forms.Label();
            this.lblApprovalValue = new System.Windows.Forms.Label();
            this.lblApproval = new System.Windows.Forms.Label();
            this.lblEntryMode = new System.Windows.Forms.Label();
            this.lblCardType = new System.Windows.Forms.Label();
            this.lblTotalValue = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.lblCardValue = new System.Windows.Forms.Label();
            this.lblCard = new System.Windows.Forms.Label();
            this.lblPaymentMethod = new System.Windows.Forms.Label();
            this.lblPaymentValue = new System.Windows.Forms.Label();
            this.bPanel_Rounded1 = new ThirdPartyTool.CustomControl.BPanel_Rounded();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnUpdate = new ThirdPartyTool.CustomControl.BLabel_Rounded();
            this.pnlSearch = new ThirdPartyTool.CustomControl.BPanel_Rounded();
            this.txtGratuity = new ThirdPartyTool.CustomControl.NumericTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.bPanel_Rounded1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pnlSearch.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.lblTerminalValue, 4, 3);
            this.tableLayoutPanel1.Controls.Add(this.lblTerminal, 3, 3);
            this.tableLayoutPanel1.Controls.Add(this.lblBatchValue, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.lblBatch, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.lblApprovalValue, 4, 2);
            this.tableLayoutPanel1.Controls.Add(this.lblApproval, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.lblEntryMode, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.lblCardType, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.lblTotalValue, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblTotal, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblCardValue, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblCard, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblPaymentMethod, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblPaymentValue, 3, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(364, 76);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // lblTerminalValue
            // 
            this.lblTerminalValue.BackColor = System.Drawing.Color.White;
            this.lblTerminalValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTerminalValue.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTerminalValue.Location = new System.Drawing.Point(283, 57);
            this.lblTerminalValue.Name = "lblTerminalValue";
            this.lblTerminalValue.Size = new System.Drawing.Size(78, 19);
            this.lblTerminalValue.TabIndex = 19;
            this.lblTerminalValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTerminal
            // 
            this.lblTerminal.BackColor = System.Drawing.Color.White;
            this.lblTerminal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTerminal.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTerminal.Location = new System.Drawing.Point(203, 57);
            this.lblTerminal.Name = "lblTerminal";
            this.lblTerminal.Size = new System.Drawing.Size(74, 19);
            this.lblTerminal.TabIndex = 18;
            this.lblTerminal.Text = "Terminal";
            this.lblTerminal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblBatchValue
            // 
            this.lblBatchValue.BackColor = System.Drawing.Color.White;
            this.lblBatchValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblBatchValue.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBatchValue.Location = new System.Drawing.Point(93, 57);
            this.lblBatchValue.Name = "lblBatchValue";
            this.lblBatchValue.Size = new System.Drawing.Size(94, 19);
            this.lblBatchValue.TabIndex = 16;
            this.lblBatchValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblBatch
            // 
            this.lblBatch.BackColor = System.Drawing.Color.White;
            this.lblBatch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblBatch.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBatch.Location = new System.Drawing.Point(3, 57);
            this.lblBatch.Name = "lblBatch";
            this.lblBatch.Size = new System.Drawing.Size(84, 19);
            this.lblBatch.TabIndex = 15;
            this.lblBatch.Text = "Batch";
            this.lblBatch.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblApprovalValue
            // 
            this.lblApprovalValue.BackColor = System.Drawing.Color.White;
            this.lblApprovalValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblApprovalValue.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblApprovalValue.Location = new System.Drawing.Point(283, 38);
            this.lblApprovalValue.Name = "lblApprovalValue";
            this.lblApprovalValue.Size = new System.Drawing.Size(78, 19);
            this.lblApprovalValue.TabIndex = 14;
            this.lblApprovalValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblApproval
            // 
            this.lblApproval.BackColor = System.Drawing.Color.White;
            this.lblApproval.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblApproval.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblApproval.Location = new System.Drawing.Point(203, 38);
            this.lblApproval.Name = "lblApproval";
            this.lblApproval.Size = new System.Drawing.Size(74, 19);
            this.lblApproval.TabIndex = 13;
            this.lblApproval.Text = "Approval";
            this.lblApproval.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblEntryMode
            // 
            this.lblEntryMode.BackColor = System.Drawing.Color.White;
            this.lblEntryMode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblEntryMode.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEntryMode.Location = new System.Drawing.Point(93, 38);
            this.lblEntryMode.Name = "lblEntryMode";
            this.lblEntryMode.Size = new System.Drawing.Size(94, 19);
            this.lblEntryMode.TabIndex = 11;
            this.lblEntryMode.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCardType
            // 
            this.lblCardType.BackColor = System.Drawing.Color.White;
            this.lblCardType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCardType.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCardType.Location = new System.Drawing.Point(3, 38);
            this.lblCardType.Name = "lblCardType";
            this.lblCardType.Size = new System.Drawing.Size(84, 19);
            this.lblCardType.TabIndex = 10;
            this.lblCardType.Text = "Card Type";
            this.lblCardType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTotalValue
            // 
            this.lblTotalValue.BackColor = System.Drawing.Color.White;
            this.lblTotalValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTotalValue.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalValue.Location = new System.Drawing.Point(283, 19);
            this.lblTotalValue.Name = "lblTotalValue";
            this.lblTotalValue.Size = new System.Drawing.Size(78, 19);
            this.lblTotalValue.TabIndex = 9;
            this.lblTotalValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTotal
            // 
            this.lblTotal.BackColor = System.Drawing.Color.White;
            this.lblTotal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTotal.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.Location = new System.Drawing.Point(203, 19);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(74, 19);
            this.lblTotal.TabIndex = 8;
            this.lblTotal.Text = "Total";
            this.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCardValue
            // 
            this.lblCardValue.BackColor = System.Drawing.Color.White;
            this.lblCardValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCardValue.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCardValue.Location = new System.Drawing.Point(93, 19);
            this.lblCardValue.Name = "lblCardValue";
            this.lblCardValue.Size = new System.Drawing.Size(94, 19);
            this.lblCardValue.TabIndex = 6;
            this.lblCardValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCard
            // 
            this.lblCard.BackColor = System.Drawing.Color.White;
            this.lblCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCard.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCard.Location = new System.Drawing.Point(3, 19);
            this.lblCard.Name = "lblCard";
            this.lblCard.Size = new System.Drawing.Size(84, 19);
            this.lblCard.TabIndex = 5;
            this.lblCard.Text = "Card";
            this.lblCard.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPaymentMethod
            // 
            this.lblPaymentMethod.AutoSize = true;
            this.lblPaymentMethod.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanel1.SetColumnSpan(this.lblPaymentMethod, 2);
            this.lblPaymentMethod.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPaymentMethod.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPaymentMethod.Location = new System.Drawing.Point(3, 0);
            this.lblPaymentMethod.Name = "lblPaymentMethod";
            this.lblPaymentMethod.Size = new System.Drawing.Size(184, 19);
            this.lblPaymentMethod.TabIndex = 0;
            this.lblPaymentMethod.Text = "label1";
            this.lblPaymentMethod.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPaymentValue
            // 
            this.lblPaymentValue.AutoSize = true;
            this.lblPaymentValue.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanel1.SetColumnSpan(this.lblPaymentValue, 2);
            this.lblPaymentValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPaymentValue.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPaymentValue.Location = new System.Drawing.Point(203, 0);
            this.lblPaymentValue.Name = "lblPaymentValue";
            this.lblPaymentValue.Size = new System.Drawing.Size(158, 19);
            this.lblPaymentValue.TabIndex = 1;
            this.lblPaymentValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bPanel_Rounded1
            // 
            this.bPanel_Rounded1.BackColor = System.Drawing.Color.White;
            this.bPanel_Rounded1.BackgroundColor = System.Drawing.Color.Transparent;
            this.bPanel_Rounded1.BorderColor = System.Drawing.Color.Gray;
            this.bPanel_Rounded1.BorderRadius = 5;
            this.bPanel_Rounded1.BorderSize = 1;
            this.bPanel_Rounded1.Controls.Add(this.panel1);
            this.bPanel_Rounded1.Controls.Add(this.tableLayoutPanel1);
            this.bPanel_Rounded1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bPanel_Rounded1.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bPanel_Rounded1.Location = new System.Drawing.Point(5, 5);
            this.bPanel_Rounded1.Name = "bPanel_Rounded1";
            this.bPanel_Rounded1.Padding = new System.Windows.Forms.Padding(3);
            this.bPanel_Rounded1.Size = new System.Drawing.Size(370, 130);
            this.bPanel_Rounded1.TabIndex = 5;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.btnUpdate);
            this.panel1.Controls.Add(this.pnlSearch);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 79);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(364, 50);
            this.panel1.TabIndex = 6;
            // 
            // btnUpdate
            // 
            this.btnUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUpdate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(26)))), ((int)(((byte)(98)))));
            this.btnUpdate.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(26)))), ((int)(((byte)(98)))));
            this.btnUpdate.BorderColor = System.Drawing.Color.Gray;
            this.btnUpdate.BorderRadius = 5;
            this.btnUpdate.BorderSize = 1;
            this.btnUpdate.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Bold);
            this.btnUpdate.ForeColor = System.Drawing.Color.White;
            this.btnUpdate.Location = new System.Drawing.Point(232, 5);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(129, 40);
            this.btnUpdate.TabIndex = 7;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnUpdate.TextColor = System.Drawing.Color.White;
            // 
            // pnlSearch
            // 
            this.pnlSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlSearch.BackColor = System.Drawing.Color.White;
            this.pnlSearch.BackgroundColor = System.Drawing.Color.Transparent;
            this.pnlSearch.BorderColor = System.Drawing.Color.Gray;
            this.pnlSearch.BorderRadius = 5;
            this.pnlSearch.BorderSize = 1;
            this.pnlSearch.Controls.Add(this.txtGratuity);
            this.pnlSearch.Location = new System.Drawing.Point(56, 5);
            this.pnlSearch.Name = "pnlSearch";
            this.pnlSearch.Size = new System.Drawing.Size(170, 40);
            this.pnlSearch.TabIndex = 6;
            // 
            // txtGratuity
            // 
            this.txtGratuity.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtGratuity.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtGratuity.FormatSpecifier = "c2";
            this.txtGratuity.Location = new System.Drawing.Point(10, 11);
            this.txtGratuity.MaxValue = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.txtGratuity.Name = "txtGratuity";
            this.txtGratuity.Size = new System.Drawing.Size(150, 20);
            this.txtGratuity.TabIndex = 0;
            this.txtGratuity.Text = "0.00";
            this.txtGratuity.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtGratuity.Value = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 50);
            this.label1.TabIndex = 5;
            this.label1.Text = "Tip";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PaymentDetailView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.bPanel_Rounded1);
            this.Name = "PaymentDetailView";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Size = new System.Drawing.Size(380, 140);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.bPanel_Rounded1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.pnlSearch.ResumeLayout(false);
            this.pnlSearch.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        public System.Windows.Forms.Label lblTerminalValue;
        public System.Windows.Forms.Label lblTerminal;
        public System.Windows.Forms.Label lblBatchValue;
        public System.Windows.Forms.Label lblBatch;
        public System.Windows.Forms.Label lblApprovalValue;
        public System.Windows.Forms.Label lblApproval;
        public System.Windows.Forms.Label lblEntryMode;
        public System.Windows.Forms.Label lblCardType;
        public System.Windows.Forms.Label lblTotalValue;
        public System.Windows.Forms.Label lblTotal;
        public System.Windows.Forms.Label lblCardValue;
        public System.Windows.Forms.Label lblCard;
        public System.Windows.Forms.Label lblPaymentMethod;
        public System.Windows.Forms.Label lblPaymentValue;
        public BPanel_Rounded bPanel_Rounded1;
        public System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private BPanel_Rounded pnlSearch;
        public NumericTextBox txtGratuity;
        public BLabel_Rounded btnUpdate;
    }
}
