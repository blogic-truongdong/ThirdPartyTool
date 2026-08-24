namespace BLogicPOS7SupportTool.Dashboard.Dialogs
{
    partial class PaymentDetailForm
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
            this.pnlTitleDivider = new System.Windows.Forms.Panel();
            this.pnlFooter = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.pnlFooterDivider = new System.Windows.Forms.Panel();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.txtSvcGratuity = new System.Windows.Forms.TextBox();
            this.lblSvcGratuityCap = new System.Windows.Forms.Label();
            this.txtGratuity = new System.Windows.Forms.TextBox();
            this.lblGratuityCap = new System.Windows.Forms.Label();
            this.txtCashDiscTax = new System.Windows.Forms.TextBox();
            this.lblCashDiscTaxCap = new System.Windows.Forms.Label();
            this.txtCashDisc = new System.Windows.Forms.TextBox();
            this.lblCashDiscCap = new System.Windows.Forms.Label();
            this.txtSvcFeeTax = new System.Windows.Forms.TextBox();
            this.lblSvcFeeTaxCap = new System.Windows.Forms.Label();
            this.txtSvcFee = new System.Windows.Forms.TextBox();
            this.lblSvcFeeCap = new System.Windows.Forms.Label();
            this.txtReturn = new System.Windows.Forms.TextBox();
            this.lblReturnCap = new System.Windows.Forms.Label();
            this.txtTender = new System.Windows.Forms.TextBox();
            this.lblTenderCap = new System.Windows.Forms.Label();
            this.txtCard = new System.Windows.Forms.TextBox();
            this.lblCardCap = new System.Windows.Forms.Label();
            this.txtMethod = new System.Windows.Forms.TextBox();
            this.lblMethodCap = new System.Windows.Forms.Label();
            this.pnlTotal = new System.Windows.Forms.Panel();
            this.lblTotalValue = new System.Windows.Forms.Label();
            this.lblTotalCaption = new System.Windows.Forms.Label();
            this.pnlTitleBar.SuspendLayout();
            this.pnlFooter.SuspendLayout();
            this.pnlContent.SuspendLayout();
            this.pnlTotal.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTitleBar
            // 
            this.pnlTitleBar.BackColor = System.Drawing.Color.White;
            this.pnlTitleBar.Controls.Add(this.btnCloseForm);
            this.pnlTitleBar.Controls.Add(this.lblFormTitle);
            this.pnlTitleBar.Controls.Add(this.pnlTitleDivider);
            this.pnlTitleBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTitleBar.Location = new System.Drawing.Point(0, 0);
            this.pnlTitleBar.Name = "pnlTitleBar";
            this.pnlTitleBar.Size = new System.Drawing.Size(460, 48);
            this.pnlTitleBar.TabIndex = 0;
            // 
            // btnCloseForm
            // 
            this.btnCloseForm.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnCloseForm.FlatAppearance.BorderSize = 0;
            this.btnCloseForm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCloseForm.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnCloseForm.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(87)))), ((int)(((byte)(208)))));
            this.btnCloseForm.Location = new System.Drawing.Point(416, 0);
            this.btnCloseForm.Name = "btnCloseForm";
            this.btnCloseForm.Size = new System.Drawing.Size(44, 47);
            this.btnCloseForm.TabIndex = 1;
            this.btnCloseForm.Text = "X";
            this.btnCloseForm.UseVisualStyleBackColor = false;
            // 
            // lblFormTitle
            // 
            this.lblFormTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblFormTitle.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblFormTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.lblFormTitle.Location = new System.Drawing.Point(0, 0);
            this.lblFormTitle.Name = "lblFormTitle";
            this.lblFormTitle.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.lblFormTitle.Size = new System.Drawing.Size(460, 47);
            this.lblFormTitle.TabIndex = 0;
            this.lblFormTitle.Text = "Payment Details";
            this.lblFormTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlTitleDivider
            // 
            this.pnlTitleDivider.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(230)))));
            this.pnlTitleDivider.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlTitleDivider.Location = new System.Drawing.Point(0, 47);
            this.pnlTitleDivider.Name = "pnlTitleDivider";
            this.pnlTitleDivider.Size = new System.Drawing.Size(460, 1);
            this.pnlTitleDivider.TabIndex = 2;
            // 
            // pnlFooter
            // 
            this.pnlFooter.BackColor = System.Drawing.Color.White;
            this.pnlFooter.Controls.Add(this.btnCancel);
            this.pnlFooter.Controls.Add(this.btnUpdate);
            this.pnlFooter.Controls.Add(this.pnlFooterDivider);
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Location = new System.Drawing.Point(0, 428);
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Size = new System.Drawing.Size(460, 64);
            this.pnlFooter.TabIndex = 2;
            // 
            // btnCancel
            // 
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(210)))));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.btnCancel.Location = new System.Drawing.Point(16, 14);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(204, 38);
            this.btnCancel.TabIndex = 20;
            this.btnCancel.Text = "CANCEL";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnUpdate
            // 
            this.btnUpdate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(87)))), ((int)(((byte)(208)))));
            this.btnUpdate.FlatAppearance.BorderSize = 0;
            this.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdate.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnUpdate.ForeColor = System.Drawing.Color.White;
            this.btnUpdate.Location = new System.Drawing.Point(240, 14);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(204, 38);
            this.btnUpdate.TabIndex = 21;
            this.btnUpdate.Text = "UPDATE";
            this.btnUpdate.UseVisualStyleBackColor = false;
            // 
            // pnlFooterDivider
            // 
            this.pnlFooterDivider.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(230)))));
            this.pnlFooterDivider.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFooterDivider.Location = new System.Drawing.Point(0, 0);
            this.pnlFooterDivider.Name = "pnlFooterDivider";
            this.pnlFooterDivider.Size = new System.Drawing.Size(460, 1);
            this.pnlFooterDivider.TabIndex = 0;
            // 
            // pnlContent
            // 
            this.pnlContent.BackColor = System.Drawing.Color.White;
            this.pnlContent.Controls.Add(this.txtSvcGratuity);
            this.pnlContent.Controls.Add(this.lblSvcGratuityCap);
            this.pnlContent.Controls.Add(this.txtGratuity);
            this.pnlContent.Controls.Add(this.lblGratuityCap);
            this.pnlContent.Controls.Add(this.txtCashDiscTax);
            this.pnlContent.Controls.Add(this.lblCashDiscTaxCap);
            this.pnlContent.Controls.Add(this.txtCashDisc);
            this.pnlContent.Controls.Add(this.lblCashDiscCap);
            this.pnlContent.Controls.Add(this.txtSvcFeeTax);
            this.pnlContent.Controls.Add(this.lblSvcFeeTaxCap);
            this.pnlContent.Controls.Add(this.txtSvcFee);
            this.pnlContent.Controls.Add(this.lblSvcFeeCap);
            this.pnlContent.Controls.Add(this.txtReturn);
            this.pnlContent.Controls.Add(this.lblReturnCap);
            this.pnlContent.Controls.Add(this.txtTender);
            this.pnlContent.Controls.Add(this.lblTenderCap);
            this.pnlContent.Controls.Add(this.txtCard);
            this.pnlContent.Controls.Add(this.lblCardCap);
            this.pnlContent.Controls.Add(this.txtMethod);
            this.pnlContent.Controls.Add(this.lblMethodCap);
            this.pnlContent.Controls.Add(this.pnlTotal);
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(0, 48);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Size = new System.Drawing.Size(460, 380);
            this.pnlContent.TabIndex = 1;
            // 
            // txtSvcGratuity
            // 
            this.txtSvcGratuity.BackColor = System.Drawing.Color.White;
            this.txtSvcGratuity.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSvcGratuity.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSvcGratuity.Location = new System.Drawing.Point(232, 333);
            this.txtSvcGratuity.Name = "txtSvcGratuity";
            this.txtSvcGratuity.Size = new System.Drawing.Size(196, 25);
            this.txtSvcGratuity.TabIndex = 20;
            this.txtSvcGratuity.Text = "0.00";
            this.txtSvcGratuity.Visible = false;
            // 
            // lblSvcGratuityCap
            // 
            this.lblSvcGratuityCap.AutoSize = true;
            this.lblSvcGratuityCap.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblSvcGratuityCap.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(110)))));
            this.lblSvcGratuityCap.Location = new System.Drawing.Point(232, 316);
            this.lblSvcGratuityCap.Name = "lblSvcGratuityCap";
            this.lblSvcGratuityCap.Size = new System.Drawing.Size(127, 13);
            this.lblSvcGratuityCap.TabIndex = 19;
            this.lblSvcGratuityCap.Text = "SVC FEE FOR GRATUITY";
            this.lblSvcGratuityCap.Visible = false;
            // 
            // txtGratuity
            // 
            this.txtGratuity.BackColor = System.Drawing.Color.White;
            this.txtGratuity.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtGratuity.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtGratuity.Location = new System.Drawing.Point(16, 333);
            this.txtGratuity.Name = "txtGratuity";
            this.txtGratuity.Size = new System.Drawing.Size(196, 25);
            this.txtGratuity.TabIndex = 18;
            this.txtGratuity.Text = "0.00";
            // 
            // lblGratuityCap
            // 
            this.lblGratuityCap.AutoSize = true;
            this.lblGratuityCap.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblGratuityCap.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(110)))));
            this.lblGratuityCap.Location = new System.Drawing.Point(16, 316);
            this.lblGratuityCap.Name = "lblGratuityCap";
            this.lblGratuityCap.Size = new System.Drawing.Size(59, 13);
            this.lblGratuityCap.TabIndex = 17;
            this.lblGratuityCap.Text = "GRATUITY";
            // 
            // txtCashDiscTax
            // 
            this.txtCashDiscTax.BackColor = System.Drawing.Color.White;
            this.txtCashDiscTax.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCashDiscTax.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtCashDiscTax.Location = new System.Drawing.Point(232, 277);
            this.txtCashDiscTax.Name = "txtCashDiscTax";
            this.txtCashDiscTax.Size = new System.Drawing.Size(196, 25);
            this.txtCashDiscTax.TabIndex = 16;
            this.txtCashDiscTax.Text = "0.00";
            // 
            // lblCashDiscTaxCap
            // 
            this.lblCashDiscTaxCap.AutoSize = true;
            this.lblCashDiscTaxCap.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblCashDiscTaxCap.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(110)))));
            this.lblCashDiscTaxCap.Location = new System.Drawing.Point(232, 260);
            this.lblCashDiscTaxCap.Name = "lblCashDiscTaxCap";
            this.lblCashDiscTaxCap.Size = new System.Drawing.Size(117, 13);
            this.lblCashDiscTaxCap.TabIndex = 15;
            this.lblCashDiscTaxCap.Text = "CASH DISCOUNT TAX";
            // 
            // txtCashDisc
            // 
            this.txtCashDisc.BackColor = System.Drawing.Color.White;
            this.txtCashDisc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCashDisc.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtCashDisc.Location = new System.Drawing.Point(16, 277);
            this.txtCashDisc.Name = "txtCashDisc";
            this.txtCashDisc.Size = new System.Drawing.Size(196, 25);
            this.txtCashDisc.TabIndex = 14;
            this.txtCashDisc.Text = "0.00";
            // 
            // lblCashDiscCap
            // 
            this.lblCashDiscCap.AutoSize = true;
            this.lblCashDiscCap.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblCashDiscCap.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(110)))));
            this.lblCashDiscCap.Location = new System.Drawing.Point(16, 260);
            this.lblCashDiscCap.Name = "lblCashDiscCap";
            this.lblCashDiscCap.Size = new System.Drawing.Size(94, 13);
            this.lblCashDiscCap.TabIndex = 13;
            this.lblCashDiscCap.Text = "CASH DISCOUNT";
            // 
            // txtSvcFeeTax
            // 
            this.txtSvcFeeTax.BackColor = System.Drawing.Color.White;
            this.txtSvcFeeTax.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSvcFeeTax.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSvcFeeTax.Location = new System.Drawing.Point(232, 221);
            this.txtSvcFeeTax.Name = "txtSvcFeeTax";
            this.txtSvcFeeTax.Size = new System.Drawing.Size(196, 25);
            this.txtSvcFeeTax.TabIndex = 12;
            this.txtSvcFeeTax.Text = "0.00";
            // 
            // lblSvcFeeTaxCap
            // 
            this.lblSvcFeeTaxCap.AutoSize = true;
            this.lblSvcFeeTaxCap.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblSvcFeeTaxCap.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(110)))));
            this.lblSvcFeeTaxCap.Location = new System.Drawing.Point(232, 204);
            this.lblSvcFeeTaxCap.Name = "lblSvcFeeTaxCap";
            this.lblSvcFeeTaxCap.Size = new System.Drawing.Size(71, 13);
            this.lblSvcFeeTaxCap.TabIndex = 11;
            this.lblSvcFeeTaxCap.Text = "SVC FEE TAX";
            // 
            // txtSvcFee
            // 
            this.txtSvcFee.BackColor = System.Drawing.Color.White;
            this.txtSvcFee.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSvcFee.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSvcFee.Location = new System.Drawing.Point(16, 221);
            this.txtSvcFee.Name = "txtSvcFee";
            this.txtSvcFee.Size = new System.Drawing.Size(196, 25);
            this.txtSvcFee.TabIndex = 10;
            this.txtSvcFee.Text = "0.00";
            // 
            // lblSvcFeeCap
            // 
            this.lblSvcFeeCap.AutoSize = true;
            this.lblSvcFeeCap.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblSvcFeeCap.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(110)))));
            this.lblSvcFeeCap.Location = new System.Drawing.Point(16, 204);
            this.lblSvcFeeCap.Name = "lblSvcFeeCap";
            this.lblSvcFeeCap.Size = new System.Drawing.Size(48, 13);
            this.lblSvcFeeCap.TabIndex = 9;
            this.lblSvcFeeCap.Text = "SVC FEE";
            // 
            // txtReturn
            // 
            this.txtReturn.BackColor = System.Drawing.Color.White;
            this.txtReturn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtReturn.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtReturn.Location = new System.Drawing.Point(232, 165);
            this.txtReturn.Name = "txtReturn";
            this.txtReturn.Size = new System.Drawing.Size(196, 25);
            this.txtReturn.TabIndex = 8;
            this.txtReturn.Text = "0.00";
            // 
            // lblReturnCap
            // 
            this.lblReturnCap.AutoSize = true;
            this.lblReturnCap.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblReturnCap.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(110)))));
            this.lblReturnCap.Location = new System.Drawing.Point(232, 148);
            this.lblReturnCap.Name = "lblReturnCap";
            this.lblReturnCap.Size = new System.Drawing.Size(103, 13);
            this.lblReturnCap.TabIndex = 7;
            this.lblReturnCap.Text = "AMOUNT RETURN";
            // 
            // txtTender
            // 
            this.txtTender.BackColor = System.Drawing.Color.White;
            this.txtTender.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTender.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtTender.Location = new System.Drawing.Point(16, 165);
            this.txtTender.Name = "txtTender";
            this.txtTender.Size = new System.Drawing.Size(196, 25);
            this.txtTender.TabIndex = 6;
            this.txtTender.Text = "0.00";
            // 
            // lblTenderCap
            // 
            this.lblTenderCap.AutoSize = true;
            this.lblTenderCap.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblTenderCap.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(110)))));
            this.lblTenderCap.Location = new System.Drawing.Point(16, 148);
            this.lblTenderCap.Name = "lblTenderCap";
            this.lblTenderCap.Size = new System.Drawing.Size(102, 13);
            this.lblTenderCap.TabIndex = 5;
            this.lblTenderCap.Text = "AMOUNT TENDER";
            // 
            // txtCard
            // 
            this.txtCard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(248)))));
            this.txtCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCard.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtCard.Location = new System.Drawing.Point(232, 109);
            this.txtCard.Name = "txtCard";
            this.txtCard.ReadOnly = true;
            this.txtCard.Size = new System.Drawing.Size(196, 25);
            this.txtCard.TabIndex = 4;
            // 
            // lblCardCap
            // 
            this.lblCardCap.AutoSize = true;
            this.lblCardCap.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblCardCap.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(110)))));
            this.lblCardCap.Location = new System.Drawing.Point(232, 92);
            this.lblCardCap.Name = "lblCardCap";
            this.lblCardCap.Size = new System.Drawing.Size(88, 13);
            this.lblCardCap.TabIndex = 3;
            this.lblCardCap.Text = "CARD NUMBER";
            // 
            // txtMethod
            // 
            this.txtMethod.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(248)))));
            this.txtMethod.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMethod.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtMethod.Location = new System.Drawing.Point(16, 109);
            this.txtMethod.Name = "txtMethod";
            this.txtMethod.ReadOnly = true;
            this.txtMethod.Size = new System.Drawing.Size(196, 25);
            this.txtMethod.TabIndex = 2;
            // 
            // lblMethodCap
            // 
            this.lblMethodCap.AutoSize = true;
            this.lblMethodCap.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblMethodCap.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(110)))));
            this.lblMethodCap.Location = new System.Drawing.Point(16, 92);
            this.lblMethodCap.Name = "lblMethodCap";
            this.lblMethodCap.Size = new System.Drawing.Size(109, 13);
            this.lblMethodCap.TabIndex = 1;
            this.lblMethodCap.Text = "PAYMENT METHOD";
            // 
            // pnlTotal
            // 
            this.pnlTotal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(240)))), ((int)(((byte)(248)))));
            this.pnlTotal.Controls.Add(this.lblTotalCaption);
            this.pnlTotal.Controls.Add(this.lblTotalValue);
            this.pnlTotal.Location = new System.Drawing.Point(16, 12);
            this.pnlTotal.Name = "pnlTotal";
            this.pnlTotal.Size = new System.Drawing.Size(428, 64);
            this.pnlTotal.TabIndex = 0;
            // 
            // lblTotalValue
            // 
            this.lblTotalValue.AutoSize = true;
            this.lblTotalValue.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTotalValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(87)))), ((int)(((byte)(208)))));
            this.lblTotalValue.Location = new System.Drawing.Point(14, 30);
            this.lblTotalValue.Name = "lblTotalValue";
            this.lblTotalValue.Size = new System.Drawing.Size(77, 30);
            this.lblTotalValue.TabIndex = 1;
            this.lblTotalValue.Text = "$ 0.00";
            // 
            // lblTotalCaption
            // 
            this.lblTotalCaption.AutoSize = true;
            this.lblTotalCaption.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTotalCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(110)))));
            this.lblTotalCaption.Location = new System.Drawing.Point(16, 3);
            this.lblTotalCaption.Name = "lblTotalCaption";
            this.lblTotalCaption.Size = new System.Drawing.Size(379, 30);
            this.lblTotalCaption.TabIndex = 0;
            this.lblTotalCaption.Text = "TOTAL = Amount Tender - Amount Return + SVCFee + SVC Fee Tax\r\n                 - " +
    "Cash Discount - Cash Discount Tax + Gratuity";
            // 
            // PaymentDetailForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(460, 492);
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.pnlFooter);
            this.Controls.Add(this.pnlTitleBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "PaymentDetailForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.pnlTitleBar.ResumeLayout(false);
            this.pnlFooter.ResumeLayout(false);
            this.pnlContent.ResumeLayout(false);
            this.pnlContent.PerformLayout();
            this.pnlTotal.ResumeLayout(false);
            this.pnlTotal.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTitleBar;
        private System.Windows.Forms.Button btnCloseForm;
        private System.Windows.Forms.Label lblFormTitle;
        private System.Windows.Forms.Panel pnlTitleDivider;
        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.Panel pnlFooterDivider;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.Panel pnlTotal;
        private System.Windows.Forms.Label lblTotalCaption;
        private System.Windows.Forms.Label lblTotalValue;
        private System.Windows.Forms.Label lblMethodCap;
        private System.Windows.Forms.TextBox txtMethod;
        private System.Windows.Forms.Label lblCardCap;
        private System.Windows.Forms.TextBox txtCard;
        private System.Windows.Forms.Label lblTenderCap;
        private System.Windows.Forms.TextBox txtTender;
        private System.Windows.Forms.Label lblReturnCap;
        private System.Windows.Forms.TextBox txtReturn;
        private System.Windows.Forms.Label lblSvcFeeCap;
        private System.Windows.Forms.TextBox txtSvcFee;
        private System.Windows.Forms.Label lblSvcFeeTaxCap;
        private System.Windows.Forms.TextBox txtSvcFeeTax;
        private System.Windows.Forms.Label lblCashDiscCap;
        private System.Windows.Forms.TextBox txtCashDisc;
        private System.Windows.Forms.Label lblCashDiscTaxCap;
        private System.Windows.Forms.TextBox txtCashDiscTax;
        private System.Windows.Forms.Label lblGratuityCap;
        private System.Windows.Forms.TextBox txtGratuity;
        private System.Windows.Forms.Label lblSvcGratuityCap;
        private System.Windows.Forms.TextBox txtSvcGratuity;
    }
}
