namespace BLogicPOS7SupportTool.Dashboard.Dialogs
{
    partial class FinishTicketForm
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
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.pnlFooterDivider = new System.Windows.Forms.Panel();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.pnlCard = new System.Windows.Forms.Panel();
            this.cmbBatch = new System.Windows.Forms.ComboBox();
            this.lblBatch = new System.Windows.Forms.Label();
            this.txtNote = new System.Windows.Forms.TextBox();
            this.lblNote = new System.Windows.Forms.Label();
            this.txtReference = new System.Windows.Forms.TextBox();
            this.lblReference = new System.Windows.Forms.Label();
            this.txtAuthCode = new System.Windows.Forms.TextBox();
            this.lblAuthCode = new System.Windows.Forms.Label();
            this.txtExpiry = new System.Windows.Forms.TextBox();
            this.lblExpiry = new System.Windows.Forms.Label();
            this.txtCardType = new System.Windows.Forms.TextBox();
            this.lblCardType = new System.Windows.Forms.Label();
            this.txtCardNumber = new System.Windows.Forms.TextBox();
            this.lblCardNumber = new System.Windows.Forms.Label();
            this.lblCardHeader = new System.Windows.Forms.Label();
            this.dtpDateTime = new System.Windows.Forms.DateTimePicker();
            this.lblDateTime = new System.Windows.Forms.Label();
            this.txtGratuity = new System.Windows.Forms.TextBox();
            this.lblGratuity = new System.Windows.Forms.Label();
            this.cmbPaymentMethod = new System.Windows.Forms.ComboBox();
            this.lblPaymentMethod = new System.Windows.Forms.Label();
            this.rbIncludeCashDiscountNoTax = new System.Windows.Forms.RadioButton();
            this.rbIncludeCashDiscount = new System.Windows.Forms.RadioButton();
            this.rbIncludeSVCFeeNoTax = new System.Windows.Forms.RadioButton();
            this.rbIncludeSVCFee = new System.Windows.Forms.RadioButton();
            this.rbBaseNoTax = new System.Windows.Forms.RadioButton();
            this.rbBase = new System.Windows.Forms.RadioButton();
            this.lblTicket = new System.Windows.Forms.Label();
            this.pnlTitleBar.SuspendLayout();
            this.pnlFooter.SuspendLayout();
            this.pnlContent.SuspendLayout();
            this.pnlCard.SuspendLayout();
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
            this.pnlTitleBar.Size = new System.Drawing.Size(480, 40);
            this.pnlTitleBar.TabIndex = 0;
            //
            // btnCloseForm
            //
            this.btnCloseForm.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnCloseForm.FlatAppearance.BorderSize = 0;
            this.btnCloseForm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCloseForm.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnCloseForm.ForeColor = System.Drawing.Color.White;
            this.btnCloseForm.Location = new System.Drawing.Point(440, 0);
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
            this.lblFormTitle.Size = new System.Drawing.Size(440, 40);
            this.lblFormTitle.TabIndex = 0;
            this.lblFormTitle.Text = "Finish Ticket";
            this.lblFormTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // pnlFooter
            //
            this.pnlFooter.BackColor = System.Drawing.Color.White;
            this.pnlFooter.Controls.Add(this.btnOK);
            this.pnlFooter.Controls.Add(this.btnCancel);
            this.pnlFooter.Controls.Add(this.pnlFooterDivider);
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Location = new System.Drawing.Point(0, 608);
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Size = new System.Drawing.Size(480, 52);
            this.pnlFooter.TabIndex = 2;
            //
            // btnOK
            //
            this.btnOK.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnOK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(87)))), ((int)(((byte)(208)))));
            this.btnOK.FlatAppearance.BorderSize = 0;
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOK.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnOK.ForeColor = System.Drawing.Color.White;
            this.btnOK.Location = new System.Drawing.Point(364, 12);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(100, 28);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "Finish";
            this.btnOK.UseVisualStyleBackColor = false;
            //
            // btnCancel
            //
            this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnCancel.Location = new System.Drawing.Point(256, 12);
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
            this.pnlFooterDivider.Size = new System.Drawing.Size(480, 1);
            this.pnlFooterDivider.TabIndex = 0;
            //
            // pnlContent
            //
            this.pnlContent.AutoScroll = true;
            this.pnlContent.BackColor = System.Drawing.Color.White;
            this.pnlContent.Controls.Add(this.pnlCard);
            this.pnlContent.Controls.Add(this.dtpDateTime);
            this.pnlContent.Controls.Add(this.lblDateTime);
            this.pnlContent.Controls.Add(this.txtGratuity);
            this.pnlContent.Controls.Add(this.lblGratuity);
            this.pnlContent.Controls.Add(this.cmbPaymentMethod);
            this.pnlContent.Controls.Add(this.lblPaymentMethod);
            this.pnlContent.Controls.Add(this.rbIncludeCashDiscountNoTax);
            this.pnlContent.Controls.Add(this.rbIncludeCashDiscount);
            this.pnlContent.Controls.Add(this.rbIncludeSVCFeeNoTax);
            this.pnlContent.Controls.Add(this.rbIncludeSVCFee);
            this.pnlContent.Controls.Add(this.rbBaseNoTax);
            this.pnlContent.Controls.Add(this.rbBase);
            this.pnlContent.Controls.Add(this.lblTicket);
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(0, 40);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Padding = new System.Windows.Forms.Padding(16, 12, 16, 12);
            this.pnlContent.Size = new System.Drawing.Size(480, 568);
            this.pnlContent.TabIndex = 1;
            //
            // pnlCard
            //
            this.pnlCard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(248)))));
            this.pnlCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCard.Controls.Add(this.cmbBatch);
            this.pnlCard.Controls.Add(this.lblBatch);
            this.pnlCard.Controls.Add(this.txtNote);
            this.pnlCard.Controls.Add(this.lblNote);
            this.pnlCard.Controls.Add(this.txtReference);
            this.pnlCard.Controls.Add(this.lblReference);
            this.pnlCard.Controls.Add(this.txtAuthCode);
            this.pnlCard.Controls.Add(this.lblAuthCode);
            this.pnlCard.Controls.Add(this.txtExpiry);
            this.pnlCard.Controls.Add(this.lblExpiry);
            this.pnlCard.Controls.Add(this.txtCardType);
            this.pnlCard.Controls.Add(this.lblCardType);
            this.pnlCard.Controls.Add(this.txtCardNumber);
            this.pnlCard.Controls.Add(this.lblCardNumber);
            this.pnlCard.Controls.Add(this.lblCardHeader);
            this.pnlCard.Location = new System.Drawing.Point(16, 300);
            this.pnlCard.Name = "pnlCard";
            this.pnlCard.Size = new System.Drawing.Size(432, 256);
            this.pnlCard.TabIndex = 13;
            this.pnlCard.Visible = false;
            //
            // cmbBatch
            //
            this.cmbBatch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBatch.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cmbBatch.Location = new System.Drawing.Point(120, 216);
            this.cmbBatch.Name = "cmbBatch";
            this.cmbBatch.Size = new System.Drawing.Size(300, 23);
            this.cmbBatch.TabIndex = 14;
            //
            // lblBatch
            //
            this.lblBatch.AutoSize = true;
            this.lblBatch.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblBatch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.lblBatch.Location = new System.Drawing.Point(12, 219);
            this.lblBatch.Name = "lblBatch";
            this.lblBatch.Size = new System.Drawing.Size(38, 15);
            this.lblBatch.TabIndex = 13;
            this.lblBatch.Text = "Batch:";
            //
            // txtNote
            //
            this.txtNote.BackColor = System.Drawing.Color.White;
            this.txtNote.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNote.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtNote.Location = new System.Drawing.Point(120, 184);
            this.txtNote.Name = "txtNote";
            this.txtNote.Size = new System.Drawing.Size(300, 23);
            this.txtNote.TabIndex = 12;
            this.txtNote.Text = "";
            //
            // lblNote
            //
            this.lblNote.AutoSize = true;
            this.lblNote.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblNote.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.lblNote.Location = new System.Drawing.Point(12, 187);
            this.lblNote.Name = "lblNote";
            this.lblNote.Size = new System.Drawing.Size(35, 15);
            this.lblNote.TabIndex = 11;
            this.lblNote.Text = "Note:";
            //
            // txtReference
            //
            this.txtReference.BackColor = System.Drawing.Color.White;
            this.txtReference.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtReference.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtReference.Location = new System.Drawing.Point(120, 152);
            this.txtReference.Name = "txtReference";
            this.txtReference.Size = new System.Drawing.Size(300, 23);
            this.txtReference.TabIndex = 10;
            this.txtReference.Text = "";
            //
            // lblReference
            //
            this.lblReference.AutoSize = true;
            this.lblReference.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblReference.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.lblReference.Location = new System.Drawing.Point(12, 155);
            this.lblReference.Name = "lblReference";
            this.lblReference.Size = new System.Drawing.Size(67, 15);
            this.lblReference.TabIndex = 9;
            this.lblReference.Text = "Reference:";
            //
            // txtAuthCode
            //
            this.txtAuthCode.BackColor = System.Drawing.Color.White;
            this.txtAuthCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAuthCode.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtAuthCode.Location = new System.Drawing.Point(120, 120);
            this.txtAuthCode.Name = "txtAuthCode";
            this.txtAuthCode.Size = new System.Drawing.Size(300, 23);
            this.txtAuthCode.TabIndex = 8;
            this.txtAuthCode.Text = "";
            //
            // lblAuthCode
            //
            this.lblAuthCode.AutoSize = true;
            this.lblAuthCode.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblAuthCode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.lblAuthCode.Location = new System.Drawing.Point(12, 123);
            this.lblAuthCode.Name = "lblAuthCode";
            this.lblAuthCode.Size = new System.Drawing.Size(67, 15);
            this.lblAuthCode.TabIndex = 7;
            this.lblAuthCode.Text = "Auth Code:";
            //
            // txtExpiry
            //
            this.txtExpiry.BackColor = System.Drawing.Color.White;
            this.txtExpiry.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtExpiry.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtExpiry.Location = new System.Drawing.Point(120, 88);
            this.txtExpiry.Name = "txtExpiry";
            this.txtExpiry.Size = new System.Drawing.Size(300, 23);
            this.txtExpiry.TabIndex = 6;
            this.txtExpiry.Text = "";
            //
            // lblExpiry
            //
            this.lblExpiry.AutoSize = true;
            this.lblExpiry.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblExpiry.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.lblExpiry.Location = new System.Drawing.Point(12, 91);
            this.lblExpiry.Name = "lblExpiry";
            this.lblExpiry.Size = new System.Drawing.Size(74, 15);
            this.lblExpiry.TabIndex = 5;
            this.lblExpiry.Text = "Expiry Date:";
            //
            // txtCardType
            //
            this.txtCardType.BackColor = System.Drawing.Color.White;
            this.txtCardType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCardType.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtCardType.Location = new System.Drawing.Point(120, 56);
            this.txtCardType.Name = "txtCardType";
            this.txtCardType.Size = new System.Drawing.Size(300, 23);
            this.txtCardType.TabIndex = 4;
            this.txtCardType.Text = "";
            //
            // lblCardType
            //
            this.lblCardType.AutoSize = true;
            this.lblCardType.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblCardType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.lblCardType.Location = new System.Drawing.Point(12, 59);
            this.lblCardType.Name = "lblCardType";
            this.lblCardType.Size = new System.Drawing.Size(64, 15);
            this.lblCardType.TabIndex = 3;
            this.lblCardType.Text = "Card Type:";
            //
            // txtCardNumber
            //
            this.txtCardNumber.BackColor = System.Drawing.Color.White;
            this.txtCardNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCardNumber.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtCardNumber.Location = new System.Drawing.Point(120, 24);
            this.txtCardNumber.Name = "txtCardNumber";
            this.txtCardNumber.Size = new System.Drawing.Size(300, 23);
            this.txtCardNumber.TabIndex = 2;
            this.txtCardNumber.Text = "";
            //
            // lblCardNumber
            //
            this.lblCardNumber.AutoSize = true;
            this.lblCardNumber.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblCardNumber.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.lblCardNumber.Location = new System.Drawing.Point(12, 27);
            this.lblCardNumber.Name = "lblCardNumber";
            this.lblCardNumber.Size = new System.Drawing.Size(85, 15);
            this.lblCardNumber.TabIndex = 1;
            this.lblCardNumber.Text = "Card Number:";
            //
            // lblCardHeader
            //
            this.lblCardHeader.AutoSize = true;
            this.lblCardHeader.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblCardHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.lblCardHeader.Location = new System.Drawing.Point(12, 4);
            this.lblCardHeader.Name = "lblCardHeader";
            this.lblCardHeader.Size = new System.Drawing.Size(120, 15);
            this.lblCardHeader.TabIndex = 0;
            this.lblCardHeader.Text = "Credit Card Details";
            //
            // dtpDateTime
            //
            this.dtpDateTime.CustomFormat = "MM/dd/yyyy  hh:mm tt";
            this.dtpDateTime.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpDateTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDateTime.Location = new System.Drawing.Point(136, 264);
            this.dtpDateTime.Name = "dtpDateTime";
            this.dtpDateTime.Size = new System.Drawing.Size(300, 23);
            this.dtpDateTime.TabIndex = 12;
            //
            // lblDateTime
            //
            this.lblDateTime.AutoSize = true;
            this.lblDateTime.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblDateTime.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.lblDateTime.Location = new System.Drawing.Point(16, 267);
            this.lblDateTime.Name = "lblDateTime";
            this.lblDateTime.Size = new System.Drawing.Size(99, 15);
            this.lblDateTime.TabIndex = 11;
            this.lblDateTime.Text = "Payment Date:";
            //
            // txtGratuity
            //
            this.txtGratuity.BackColor = System.Drawing.Color.White;
            this.txtGratuity.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtGratuity.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtGratuity.Location = new System.Drawing.Point(136, 232);
            this.txtGratuity.Name = "txtGratuity";
            this.txtGratuity.Size = new System.Drawing.Size(160, 23);
            this.txtGratuity.TabIndex = 10;
            this.txtGratuity.Text = "0.00";
            this.txtGratuity.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            //
            // lblGratuity
            //
            this.lblGratuity.AutoSize = true;
            this.lblGratuity.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblGratuity.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.lblGratuity.Location = new System.Drawing.Point(16, 235);
            this.lblGratuity.Name = "lblGratuity";
            this.lblGratuity.Size = new System.Drawing.Size(54, 15);
            this.lblGratuity.TabIndex = 9;
            this.lblGratuity.Text = "Gratuity:";
            //
            // cmbPaymentMethod
            //
            this.cmbPaymentMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPaymentMethod.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cmbPaymentMethod.Location = new System.Drawing.Point(136, 200);
            this.cmbPaymentMethod.Name = "cmbPaymentMethod";
            this.cmbPaymentMethod.Size = new System.Drawing.Size(300, 23);
            this.cmbPaymentMethod.TabIndex = 8;
            //
            // lblPaymentMethod
            //
            this.lblPaymentMethod.AutoSize = true;
            this.lblPaymentMethod.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblPaymentMethod.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.lblPaymentMethod.Location = new System.Drawing.Point(16, 203);
            this.lblPaymentMethod.Name = "lblPaymentMethod";
            this.lblPaymentMethod.Size = new System.Drawing.Size(106, 15);
            this.lblPaymentMethod.TabIndex = 7;
            this.lblPaymentMethod.Text = "Payment Method:";
            //
            // rbIncludeCashDiscountNoTax
            //
            this.rbIncludeCashDiscountNoTax.AutoSize = true;
            this.rbIncludeCashDiscountNoTax.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.rbIncludeCashDiscountNoTax.Location = new System.Drawing.Point(20, 156);
            this.rbIncludeCashDiscountNoTax.Name = "rbIncludeCashDiscountNoTax";
            this.rbIncludeCashDiscountNoTax.Size = new System.Drawing.Size(180, 19);
            this.rbIncludeCashDiscountNoTax.TabIndex = 6;
            this.rbIncludeCashDiscountNoTax.Text = "No Tax + Cash Discount";
            this.rbIncludeCashDiscountNoTax.UseVisualStyleBackColor = true;
            //
            // rbIncludeCashDiscount
            //
            this.rbIncludeCashDiscount.AutoSize = true;
            this.rbIncludeCashDiscount.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.rbIncludeCashDiscount.Location = new System.Drawing.Point(20, 134);
            this.rbIncludeCashDiscount.Name = "rbIncludeCashDiscount";
            this.rbIncludeCashDiscount.Size = new System.Drawing.Size(140, 19);
            this.rbIncludeCashDiscount.TabIndex = 5;
            this.rbIncludeCashDiscount.Text = "With Cash Discount";
            this.rbIncludeCashDiscount.UseVisualStyleBackColor = true;
            //
            // rbIncludeSVCFeeNoTax
            //
            this.rbIncludeSVCFeeNoTax.AutoSize = true;
            this.rbIncludeSVCFeeNoTax.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.rbIncludeSVCFeeNoTax.Location = new System.Drawing.Point(20, 112);
            this.rbIncludeSVCFeeNoTax.Name = "rbIncludeSVCFeeNoTax";
            this.rbIncludeSVCFeeNoTax.Size = new System.Drawing.Size(150, 19);
            this.rbIncludeSVCFeeNoTax.TabIndex = 4;
            this.rbIncludeSVCFeeNoTax.Text = "No Tax + SVC Fee";
            this.rbIncludeSVCFeeNoTax.UseVisualStyleBackColor = true;
            //
            // rbIncludeSVCFee
            //
            this.rbIncludeSVCFee.AutoSize = true;
            this.rbIncludeSVCFee.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.rbIncludeSVCFee.Location = new System.Drawing.Point(20, 90);
            this.rbIncludeSVCFee.Name = "rbIncludeSVCFee";
            this.rbIncludeSVCFee.Size = new System.Drawing.Size(120, 19);
            this.rbIncludeSVCFee.TabIndex = 3;
            this.rbIncludeSVCFee.Text = "With SVC Fee";
            this.rbIncludeSVCFee.UseVisualStyleBackColor = true;
            //
            // rbBaseNoTax
            //
            this.rbBaseNoTax.AutoSize = true;
            this.rbBaseNoTax.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.rbBaseNoTax.Location = new System.Drawing.Point(20, 68);
            this.rbBaseNoTax.Name = "rbBaseNoTax";
            this.rbBaseNoTax.Size = new System.Drawing.Size(80, 19);
            this.rbBaseNoTax.TabIndex = 2;
            this.rbBaseNoTax.Text = "No Tax";
            this.rbBaseNoTax.UseVisualStyleBackColor = true;
            //
            // rbBase
            //
            this.rbBase.AutoSize = true;
            this.rbBase.Checked = true;
            this.rbBase.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.rbBase.Location = new System.Drawing.Point(20, 46);
            this.rbBase.Name = "rbBase";
            this.rbBase.Size = new System.Drawing.Size(80, 19);
            this.rbBase.TabIndex = 1;
            this.rbBase.TabStop = true;
            this.rbBase.Text = "Base";
            this.rbBase.UseVisualStyleBackColor = true;
            //
            // lblTicket
            //
            this.lblTicket.AutoSize = true;
            this.lblTicket.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTicket.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.lblTicket.Location = new System.Drawing.Point(16, 14);
            this.lblTicket.Name = "lblTicket";
            this.lblTicket.Size = new System.Drawing.Size(80, 20);
            this.lblTicket.TabIndex = 0;
            this.lblTicket.Text = "Ticket #";
            //
            // FinishTicketForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(480, 660);
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.pnlFooter);
            this.Controls.Add(this.pnlTitleBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FinishTicketForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "";
            this.pnlTitleBar.ResumeLayout(false);
            this.pnlFooter.ResumeLayout(false);
            this.pnlContent.ResumeLayout(false);
            this.pnlContent.PerformLayout();
            this.pnlCard.ResumeLayout(false);
            this.pnlCard.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlTitleBar;
        private System.Windows.Forms.Button btnCloseForm;
        private System.Windows.Forms.Label lblFormTitle;
        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.Panel pnlFooterDivider;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.Label lblTicket;
        private System.Windows.Forms.RadioButton rbBase;
        private System.Windows.Forms.RadioButton rbBaseNoTax;
        private System.Windows.Forms.RadioButton rbIncludeSVCFee;
        private System.Windows.Forms.RadioButton rbIncludeSVCFeeNoTax;
        private System.Windows.Forms.RadioButton rbIncludeCashDiscount;
        private System.Windows.Forms.RadioButton rbIncludeCashDiscountNoTax;
        private System.Windows.Forms.Label lblPaymentMethod;
        private System.Windows.Forms.ComboBox cmbPaymentMethod;
        private System.Windows.Forms.Label lblGratuity;
        private System.Windows.Forms.TextBox txtGratuity;
        private System.Windows.Forms.Label lblDateTime;
        private System.Windows.Forms.DateTimePicker dtpDateTime;
        private System.Windows.Forms.Panel pnlCard;
        private System.Windows.Forms.Label lblCardHeader;
        private System.Windows.Forms.Label lblCardNumber;
        private System.Windows.Forms.TextBox txtCardNumber;
        private System.Windows.Forms.Label lblCardType;
        private System.Windows.Forms.TextBox txtCardType;
        private System.Windows.Forms.Label lblExpiry;
        private System.Windows.Forms.TextBox txtExpiry;
        private System.Windows.Forms.Label lblAuthCode;
        private System.Windows.Forms.TextBox txtAuthCode;
        private System.Windows.Forms.Label lblReference;
        private System.Windows.Forms.TextBox txtReference;
        private System.Windows.Forms.Label lblNote;
        private System.Windows.Forms.TextBox txtNote;
        private System.Windows.Forms.Label lblBatch;
        private System.Windows.Forms.ComboBox cmbBatch;
    }
}
