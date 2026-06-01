namespace BLogicPOS7SupportTool.Dashboard.Dialogs
{
    partial class InsertTransactionForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.pnlTitleBar = new System.Windows.Forms.Panel();
            this.btnCloseForm = new System.Windows.Forms.Button();
            this.lblFormTitle = new System.Windows.Forms.Label();
            this.pnlFooter = new System.Windows.Forms.Panel();
            this.btnInsert = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.pnlFooterDivider = new System.Windows.Forms.Panel();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.lblInfo = new System.Windows.Forms.Label();
            this.lblAmount = new System.Windows.Forms.Label();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.lblGratuity = new System.Windows.Forms.Label();
            this.txtGratuity = new System.Windows.Forms.TextBox();
            this.lblCardNumber = new System.Windows.Forms.Label();
            this.txtCardNumber = new System.Windows.Forms.TextBox();
            this.lblCardType = new System.Windows.Forms.Label();
            this.txtCardType = new System.Windows.Forms.TextBox();
            this.lblAuthCode = new System.Windows.Forms.Label();
            this.txtAuthCode = new System.Windows.Forms.TextBox();
            this.lblRefNo = new System.Windows.Forms.Label();
            this.txtRefNo = new System.Windows.Forms.TextBox();
            this.lblBatchID = new System.Windows.Forms.Label();
            this.txtBatchID = new System.Windows.Forms.TextBox();
            this.lblMerchantID = new System.Windows.Forms.Label();
            this.txtMerchantID = new System.Windows.Forms.TextBox();
            this.lblTerminalIP = new System.Windows.Forms.Label();
            this.txtTerminalIP = new System.Windows.Forms.TextBox();
            this.pnlTitleBar.SuspendLayout();
            this.pnlFooter.SuspendLayout();
            this.pnlContent.SuspendLayout();
            this.SuspendLayout();
            //
            // pnlTitleBar
            //
            this.pnlTitleBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(87)))), ((int)(((byte)(208)))));
            this.pnlTitleBar.Controls.Add(this.btnCloseForm);
            this.pnlTitleBar.Controls.Add(this.lblFormTitle);
            this.pnlTitleBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTitleBar.Location = new System.Drawing.Point(0, 0);
            this.pnlTitleBar.Name = "pnlTitleBar";
            this.pnlTitleBar.Size = new System.Drawing.Size(460, 40);
            this.pnlTitleBar.TabIndex = 0;
            //
            // btnCloseForm
            //
            this.btnCloseForm.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnCloseForm.FlatAppearance.BorderSize = 0;
            this.btnCloseForm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCloseForm.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnCloseForm.ForeColor = System.Drawing.Color.White;
            this.btnCloseForm.Location = new System.Drawing.Point(420, 0);
            this.btnCloseForm.Name = "btnCloseForm";
            this.btnCloseForm.Size = new System.Drawing.Size(40, 40);
            this.btnCloseForm.TabIndex = 1;
            this.btnCloseForm.Text = "X";
            this.btnCloseForm.UseVisualStyleBackColor = false;
            //
            // lblFormTitle
            //
            this.lblFormTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblFormTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblFormTitle.ForeColor = System.Drawing.Color.White;
            this.lblFormTitle.Location = new System.Drawing.Point(0, 0);
            this.lblFormTitle.Name = "lblFormTitle";
            this.lblFormTitle.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.lblFormTitle.Size = new System.Drawing.Size(420, 40);
            this.lblFormTitle.TabIndex = 0;
            this.lblFormTitle.Text = "Insert Transaction Detail";
            this.lblFormTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // pnlFooter
            //
            this.pnlFooter.BackColor = System.Drawing.Color.White;
            this.pnlFooter.Controls.Add(this.btnInsert);
            this.pnlFooter.Controls.Add(this.btnCancel);
            this.pnlFooter.Controls.Add(this.pnlFooterDivider);
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Location = new System.Drawing.Point(0, 452);
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Size = new System.Drawing.Size(460, 52);
            this.pnlFooter.TabIndex = 2;
            //
            // btnInsert
            //
            this.btnInsert.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnInsert.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(87)))), ((int)(((byte)(208)))));
            this.btnInsert.FlatAppearance.BorderSize = 0;
            this.btnInsert.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInsert.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnInsert.ForeColor = System.Drawing.Color.White;
            this.btnInsert.Location = new System.Drawing.Point(320, 12);
            this.btnInsert.Name = "btnInsert";
            this.btnInsert.Size = new System.Drawing.Size(124, 28);
            this.btnInsert.TabIndex = 1;
            this.btnInsert.Text = "Insert";
            this.btnInsert.UseVisualStyleBackColor = false;
            //
            // btnCancel
            //
            this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnCancel.Location = new System.Drawing.Point(212, 12);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 28);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            //
            // pnlFooterDivider
            //
            this.pnlFooterDivider.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            this.pnlFooterDivider.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFooterDivider.Location = new System.Drawing.Point(0, 0);
            this.pnlFooterDivider.Name = "pnlFooterDivider";
            this.pnlFooterDivider.Size = new System.Drawing.Size(460, 1);
            this.pnlFooterDivider.TabIndex = 0;
            //
            // pnlContent
            //
            this.pnlContent.BackColor = System.Drawing.Color.White;
            this.pnlContent.Controls.Add(this.txtTerminalIP);
            this.pnlContent.Controls.Add(this.lblTerminalIP);
            this.pnlContent.Controls.Add(this.txtMerchantID);
            this.pnlContent.Controls.Add(this.lblMerchantID);
            this.pnlContent.Controls.Add(this.txtBatchID);
            this.pnlContent.Controls.Add(this.lblBatchID);
            this.pnlContent.Controls.Add(this.txtRefNo);
            this.pnlContent.Controls.Add(this.lblRefNo);
            this.pnlContent.Controls.Add(this.txtAuthCode);
            this.pnlContent.Controls.Add(this.lblAuthCode);
            this.pnlContent.Controls.Add(this.txtCardType);
            this.pnlContent.Controls.Add(this.lblCardType);
            this.pnlContent.Controls.Add(this.txtCardNumber);
            this.pnlContent.Controls.Add(this.lblCardNumber);
            this.pnlContent.Controls.Add(this.txtGratuity);
            this.pnlContent.Controls.Add(this.lblGratuity);
            this.pnlContent.Controls.Add(this.txtAmount);
            this.pnlContent.Controls.Add(this.lblAmount);
            this.pnlContent.Controls.Add(this.lblInfo);
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(0, 40);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Size = new System.Drawing.Size(460, 412);
            this.pnlContent.TabIndex = 1;
            //
            // lblInfo
            //
            this.lblInfo.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.lblInfo.Location = new System.Drawing.Point(16, 12);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(428, 20);
            this.lblInfo.TabIndex = 0;
            this.lblInfo.Text = "Insert a new credit-card transaction into the Merchant database.";
            //
            // lblAmount
            //
            this.lblAmount.AutoSize = true;
            this.lblAmount.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblAmount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.lblAmount.Location = new System.Drawing.Point(16, 48);
            this.lblAmount.Name = "lblAmount";
            this.lblAmount.Size = new System.Drawing.Size(62, 19);
            this.lblAmount.TabIndex = 1;
            this.lblAmount.Text = "Amount:";
            //
            // txtAmount
            //
            this.txtAmount.BackColor = System.Drawing.Color.White;
            this.txtAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAmount.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtAmount.Location = new System.Drawing.Point(150, 44);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(290, 27);
            this.txtAmount.TabIndex = 2;
            this.txtAmount.Text = "0.00";
            //
            // lblGratuity
            //
            this.lblGratuity.AutoSize = true;
            this.lblGratuity.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblGratuity.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.lblGratuity.Location = new System.Drawing.Point(16, 86);
            this.lblGratuity.Name = "lblGratuity";
            this.lblGratuity.Size = new System.Drawing.Size(66, 19);
            this.lblGratuity.TabIndex = 3;
            this.lblGratuity.Text = "Gratuity:";
            //
            // txtGratuity
            //
            this.txtGratuity.BackColor = System.Drawing.Color.White;
            this.txtGratuity.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtGratuity.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtGratuity.Location = new System.Drawing.Point(150, 82);
            this.txtGratuity.Name = "txtGratuity";
            this.txtGratuity.Size = new System.Drawing.Size(290, 27);
            this.txtGratuity.TabIndex = 4;
            this.txtGratuity.Text = "0.00";
            //
            // lblCardNumber
            //
            this.lblCardNumber.AutoSize = true;
            this.lblCardNumber.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblCardNumber.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.lblCardNumber.Location = new System.Drawing.Point(16, 124);
            this.lblCardNumber.Name = "lblCardNumber";
            this.lblCardNumber.Size = new System.Drawing.Size(96, 19);
            this.lblCardNumber.TabIndex = 5;
            this.lblCardNumber.Text = "Card Number:";
            //
            // txtCardNumber
            //
            this.txtCardNumber.BackColor = System.Drawing.Color.White;
            this.txtCardNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCardNumber.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtCardNumber.Location = new System.Drawing.Point(150, 120);
            this.txtCardNumber.Name = "txtCardNumber";
            this.txtCardNumber.Size = new System.Drawing.Size(290, 27);
            this.txtCardNumber.TabIndex = 6;
            this.txtCardNumber.Text = "";
            //
            // lblCardType
            //
            this.lblCardType.AutoSize = true;
            this.lblCardType.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblCardType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.lblCardType.Location = new System.Drawing.Point(16, 162);
            this.lblCardType.Name = "lblCardType";
            this.lblCardType.Size = new System.Drawing.Size(76, 19);
            this.lblCardType.TabIndex = 7;
            this.lblCardType.Text = "Card Type:";
            //
            // txtCardType
            //
            this.txtCardType.BackColor = System.Drawing.Color.White;
            this.txtCardType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCardType.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtCardType.Location = new System.Drawing.Point(150, 158);
            this.txtCardType.Name = "txtCardType";
            this.txtCardType.Size = new System.Drawing.Size(290, 27);
            this.txtCardType.TabIndex = 8;
            this.txtCardType.Text = "";
            //
            // lblAuthCode
            //
            this.lblAuthCode.AutoSize = true;
            this.lblAuthCode.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblAuthCode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.lblAuthCode.Location = new System.Drawing.Point(16, 200);
            this.lblAuthCode.Name = "lblAuthCode";
            this.lblAuthCode.Size = new System.Drawing.Size(78, 19);
            this.lblAuthCode.TabIndex = 9;
            this.lblAuthCode.Text = "Auth Code:";
            //
            // txtAuthCode
            //
            this.txtAuthCode.BackColor = System.Drawing.Color.White;
            this.txtAuthCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAuthCode.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtAuthCode.Location = new System.Drawing.Point(150, 196);
            this.txtAuthCode.Name = "txtAuthCode";
            this.txtAuthCode.Size = new System.Drawing.Size(290, 27);
            this.txtAuthCode.TabIndex = 10;
            this.txtAuthCode.Text = "";
            //
            // lblRefNo
            //
            this.lblRefNo.AutoSize = true;
            this.lblRefNo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblRefNo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.lblRefNo.Location = new System.Drawing.Point(16, 238);
            this.lblRefNo.Name = "lblRefNo";
            this.lblRefNo.Size = new System.Drawing.Size(56, 19);
            this.lblRefNo.TabIndex = 11;
            this.lblRefNo.Text = "Ref No:";
            //
            // txtRefNo
            //
            this.txtRefNo.BackColor = System.Drawing.Color.White;
            this.txtRefNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRefNo.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtRefNo.Location = new System.Drawing.Point(150, 234);
            this.txtRefNo.Name = "txtRefNo";
            this.txtRefNo.Size = new System.Drawing.Size(290, 27);
            this.txtRefNo.TabIndex = 12;
            this.txtRefNo.Text = "";
            //
            // lblBatchID
            //
            this.lblBatchID.AutoSize = true;
            this.lblBatchID.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblBatchID.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.lblBatchID.Location = new System.Drawing.Point(16, 276);
            this.lblBatchID.Name = "lblBatchID";
            this.lblBatchID.Size = new System.Drawing.Size(66, 19);
            this.lblBatchID.TabIndex = 13;
            this.lblBatchID.Text = "Batch ID:";
            //
            // txtBatchID
            //
            this.txtBatchID.BackColor = System.Drawing.Color.White;
            this.txtBatchID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBatchID.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtBatchID.Location = new System.Drawing.Point(150, 272);
            this.txtBatchID.Name = "txtBatchID";
            this.txtBatchID.Size = new System.Drawing.Size(290, 27);
            this.txtBatchID.TabIndex = 14;
            this.txtBatchID.Text = "";
            //
            // lblMerchantID
            //
            this.lblMerchantID.AutoSize = true;
            this.lblMerchantID.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblMerchantID.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.lblMerchantID.Location = new System.Drawing.Point(16, 314);
            this.lblMerchantID.Name = "lblMerchantID";
            this.lblMerchantID.Size = new System.Drawing.Size(90, 19);
            this.lblMerchantID.TabIndex = 15;
            this.lblMerchantID.Text = "Merchant ID:";
            //
            // txtMerchantID
            //
            this.txtMerchantID.BackColor = System.Drawing.Color.White;
            this.txtMerchantID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMerchantID.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtMerchantID.Location = new System.Drawing.Point(150, 310);
            this.txtMerchantID.Name = "txtMerchantID";
            this.txtMerchantID.Size = new System.Drawing.Size(290, 27);
            this.txtMerchantID.TabIndex = 16;
            this.txtMerchantID.Text = "";
            //
            // lblTerminalIP
            //
            this.lblTerminalIP.AutoSize = true;
            this.lblTerminalIP.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTerminalIP.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.lblTerminalIP.Location = new System.Drawing.Point(16, 352);
            this.lblTerminalIP.Name = "lblTerminalIP";
            this.lblTerminalIP.Size = new System.Drawing.Size(122, 19);
            this.lblTerminalIP.TabIndex = 17;
            this.lblTerminalIP.Text = "Terminal IP Address:";
            //
            // txtTerminalIP
            //
            this.txtTerminalIP.BackColor = System.Drawing.Color.White;
            this.txtTerminalIP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTerminalIP.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtTerminalIP.Location = new System.Drawing.Point(150, 348);
            this.txtTerminalIP.Name = "txtTerminalIP";
            this.txtTerminalIP.Size = new System.Drawing.Size(290, 27);
            this.txtTerminalIP.TabIndex = 18;
            this.txtTerminalIP.Text = "";
            //
            // InsertTransactionForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(460, 504);
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.pnlFooter);
            this.Controls.Add(this.pnlTitleBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "InsertTransactionForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "";
            this.pnlTitleBar.ResumeLayout(false);
            this.pnlFooter.ResumeLayout(false);
            this.pnlContent.ResumeLayout(false);
            this.pnlContent.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlTitleBar;
        private System.Windows.Forms.Button btnCloseForm;
        private System.Windows.Forms.Label lblFormTitle;
        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.Panel pnlFooterDivider;
        private System.Windows.Forms.Button btnInsert;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.Label lblAmount;
        private System.Windows.Forms.TextBox txtAmount;
        private System.Windows.Forms.Label lblGratuity;
        private System.Windows.Forms.TextBox txtGratuity;
        private System.Windows.Forms.Label lblCardNumber;
        private System.Windows.Forms.TextBox txtCardNumber;
        private System.Windows.Forms.Label lblCardType;
        private System.Windows.Forms.TextBox txtCardType;
        private System.Windows.Forms.Label lblAuthCode;
        private System.Windows.Forms.TextBox txtAuthCode;
        private System.Windows.Forms.Label lblRefNo;
        private System.Windows.Forms.TextBox txtRefNo;
        private System.Windows.Forms.Label lblBatchID;
        private System.Windows.Forms.TextBox txtBatchID;
        private System.Windows.Forms.Label lblMerchantID;
        private System.Windows.Forms.TextBox txtMerchantID;
        private System.Windows.Forms.Label lblTerminalIP;
        private System.Windows.Forms.TextBox txtTerminalIP;
    }
}
