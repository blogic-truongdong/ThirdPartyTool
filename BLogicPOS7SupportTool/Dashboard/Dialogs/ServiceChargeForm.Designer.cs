namespace BLogicPOS7SupportTool.Dashboard.Dialogs
{
    partial class ServiceChargeForm
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
            this.pnlColumnHeader = new System.Windows.Forms.Panel();
            this.lblColName = new System.Windows.Forms.Label();
            this.lblColAmount = new System.Windows.Forms.Label();
            this.lblColTax = new System.Windows.Forms.Label();
            this.lblColTotal = new System.Windows.Forms.Label();
            this.pnlFooter = new System.Windows.Forms.Panel();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnDone = new System.Windows.Forms.Button();
            this.pnlFooterDivider = new System.Windows.Forms.Panel();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.lblServiceCap = new System.Windows.Forms.Label();
            this.txtServiceAmount = new System.Windows.Forms.TextBox();
            this.txtServiceTax = new System.Windows.Forms.TextBox();
            this.lblServiceTotal = new System.Windows.Forms.Label();
            this.lblAreaCap = new System.Windows.Forms.Label();
            this.txtAreaAmount = new System.Windows.Forms.TextBox();
            this.txtAreaTax = new System.Windows.Forms.TextBox();
            this.lblAreaTotal = new System.Windows.Forms.Label();
            this.lblPartyCap = new System.Windows.Forms.Label();
            this.txtPartyAmount = new System.Windows.Forms.TextBox();
            this.txtPartyTax = new System.Windows.Forms.TextBox();
            this.lblPartyTotal = new System.Windows.Forms.Label();
            this.lblAutoCap = new System.Windows.Forms.Label();
            this.txtAutoAmount = new System.Windows.Forms.TextBox();
            this.txtAutoTax = new System.Windows.Forms.TextBox();
            this.lblAutoTotal = new System.Windows.Forms.Label();
            this.lblExtraCap = new System.Windows.Forms.Label();
            this.txtExtraAmount = new System.Windows.Forms.TextBox();
            this.txtExtraTax = new System.Windows.Forms.TextBox();
            this.lblExtraTotal = new System.Windows.Forms.Label();
            this.lblSVCFeeCap = new System.Windows.Forms.Label();
            this.txtSVCFeeAmount = new System.Windows.Forms.TextBox();
            this.txtSVCFeeTax = new System.Windows.Forms.TextBox();
            this.lblSVCFeeTotal = new System.Windows.Forms.Label();
            this.lblCashDiscCap = new System.Windows.Forms.Label();
            this.txtCashDiscAmount = new System.Windows.Forms.TextBox();
            this.txtCashDiscTax = new System.Windows.Forms.TextBox();
            this.lblCashDiscTotal = new System.Windows.Forms.Label();
            this.pnlSummaryDivider = new System.Windows.Forms.Panel();
            this.lblSummaryCap = new System.Windows.Forms.Label();
            this.lblSummaryAmount = new System.Windows.Forms.Label();
            this.lblSummaryTax = new System.Windows.Forms.Label();
            this.lblSummaryTotal = new System.Windows.Forms.Label();
            this.pnlTitleBar.SuspendLayout();
            this.pnlColumnHeader.SuspendLayout();
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
            this.pnlTitleBar.Size = new System.Drawing.Size(571, 40);
            this.pnlTitleBar.TabIndex = 0;
            //
            // btnCloseForm
            //
            this.btnCloseForm.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnCloseForm.FlatAppearance.BorderSize = 0;
            this.btnCloseForm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCloseForm.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnCloseForm.ForeColor = System.Drawing.Color.White;
            this.btnCloseForm.Location = new System.Drawing.Point(531, 0);
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
            this.lblFormTitle.Size = new System.Drawing.Size(571, 40);
            this.lblFormTitle.TabIndex = 0;
            this.lblFormTitle.Text = "Update Service Charges";
            this.lblFormTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // pnlColumnHeader
            //
            this.pnlColumnHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(240)))), ((int)(((byte)(250)))));
            this.pnlColumnHeader.Controls.Add(this.lblColName);
            this.pnlColumnHeader.Controls.Add(this.lblColAmount);
            this.pnlColumnHeader.Controls.Add(this.lblColTax);
            this.pnlColumnHeader.Controls.Add(this.lblColTotal);
            this.pnlColumnHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlColumnHeader.Location = new System.Drawing.Point(0, 40);
            this.pnlColumnHeader.Name = "pnlColumnHeader";
            this.pnlColumnHeader.Size = new System.Drawing.Size(571, 36);
            this.pnlColumnHeader.TabIndex = 1;
            //
            // lblColName
            //
            this.lblColName.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblColName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(110)))));
            this.lblColName.Location = new System.Drawing.Point(24, 0);
            this.lblColName.Name = "lblColName";
            this.lblColName.Size = new System.Drawing.Size(170, 36);
            this.lblColName.TabIndex = 0;
            this.lblColName.Text = "SERVICE CHARGE";
            this.lblColName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // lblColAmount
            //
            this.lblColAmount.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblColAmount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(110)))));
            this.lblColAmount.Location = new System.Drawing.Point(200, 0);
            this.lblColAmount.Name = "lblColAmount";
            this.lblColAmount.Size = new System.Drawing.Size(120, 36);
            this.lblColAmount.TabIndex = 1;
            this.lblColAmount.Text = "AMOUNT";
            this.lblColAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            //
            // lblColTax
            //
            this.lblColTax.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblColTax.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(110)))));
            this.lblColTax.Location = new System.Drawing.Point(328, 0);
            this.lblColTax.Name = "lblColTax";
            this.lblColTax.Size = new System.Drawing.Size(110, 36);
            this.lblColTax.TabIndex = 2;
            this.lblColTax.Text = "TAX";
            this.lblColTax.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            //
            // lblColTotal
            //
            this.lblColTotal.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblColTotal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(110)))));
            this.lblColTotal.Location = new System.Drawing.Point(448, 0);
            this.lblColTotal.Name = "lblColTotal";
            this.lblColTotal.Size = new System.Drawing.Size(110, 36);
            this.lblColTotal.TabIndex = 3;
            this.lblColTotal.Text = "TOTAL";
            this.lblColTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            //
            // pnlFooter
            //
            this.pnlFooter.BackColor = System.Drawing.Color.White;
            this.pnlFooter.Controls.Add(this.btnUpdate);
            this.pnlFooter.Controls.Add(this.btnDone);
            this.pnlFooter.Controls.Add(this.pnlFooterDivider);
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Location = new System.Drawing.Point(0, 476);
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Size = new System.Drawing.Size(571, 52);
            this.pnlFooter.TabIndex = 3;
            //
            // btnUpdate
            //
            this.btnUpdate.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnUpdate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(87)))), ((int)(((byte)(208)))));
            this.btnUpdate.FlatAppearance.BorderSize = 0;
            this.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdate.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnUpdate.ForeColor = System.Drawing.Color.White;
            this.btnUpdate.Location = new System.Drawing.Point(408, 12);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(150, 30);
            this.btnUpdate.TabIndex = 1;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = false;
            //
            // btnDone
            //
            this.btnDone.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnDone.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            this.btnDone.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDone.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnDone.Location = new System.Drawing.Point(250, 12);
            this.btnDone.Name = "btnDone";
            this.btnDone.Size = new System.Drawing.Size(150, 30);
            this.btnDone.TabIndex = 0;
            this.btnDone.Text = "Close";
            this.btnDone.UseVisualStyleBackColor = true;
            //
            // pnlFooterDivider
            //
            this.pnlFooterDivider.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            this.pnlFooterDivider.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFooterDivider.Location = new System.Drawing.Point(0, 0);
            this.pnlFooterDivider.Name = "pnlFooterDivider";
            this.pnlFooterDivider.Size = new System.Drawing.Size(571, 1);
            this.pnlFooterDivider.TabIndex = 0;
            //
            // pnlContent
            //
            this.pnlContent.AutoScroll = true;
            this.pnlContent.BackColor = System.Drawing.Color.White;
            this.pnlContent.Controls.Add(this.lblServiceCap);
            this.pnlContent.Controls.Add(this.txtServiceAmount);
            this.pnlContent.Controls.Add(this.txtServiceTax);
            this.pnlContent.Controls.Add(this.lblServiceTotal);
            this.pnlContent.Controls.Add(this.lblAreaCap);
            this.pnlContent.Controls.Add(this.txtAreaAmount);
            this.pnlContent.Controls.Add(this.txtAreaTax);
            this.pnlContent.Controls.Add(this.lblAreaTotal);
            this.pnlContent.Controls.Add(this.lblPartyCap);
            this.pnlContent.Controls.Add(this.txtPartyAmount);
            this.pnlContent.Controls.Add(this.txtPartyTax);
            this.pnlContent.Controls.Add(this.lblPartyTotal);
            this.pnlContent.Controls.Add(this.lblAutoCap);
            this.pnlContent.Controls.Add(this.txtAutoAmount);
            this.pnlContent.Controls.Add(this.txtAutoTax);
            this.pnlContent.Controls.Add(this.lblAutoTotal);
            this.pnlContent.Controls.Add(this.lblExtraCap);
            this.pnlContent.Controls.Add(this.txtExtraAmount);
            this.pnlContent.Controls.Add(this.txtExtraTax);
            this.pnlContent.Controls.Add(this.lblExtraTotal);
            this.pnlContent.Controls.Add(this.lblSVCFeeCap);
            this.pnlContent.Controls.Add(this.txtSVCFeeAmount);
            this.pnlContent.Controls.Add(this.txtSVCFeeTax);
            this.pnlContent.Controls.Add(this.lblSVCFeeTotal);
            this.pnlContent.Controls.Add(this.lblCashDiscCap);
            this.pnlContent.Controls.Add(this.txtCashDiscAmount);
            this.pnlContent.Controls.Add(this.txtCashDiscTax);
            this.pnlContent.Controls.Add(this.lblCashDiscTotal);
            this.pnlContent.Controls.Add(this.pnlSummaryDivider);
            this.pnlContent.Controls.Add(this.lblSummaryCap);
            this.pnlContent.Controls.Add(this.lblSummaryAmount);
            this.pnlContent.Controls.Add(this.lblSummaryTax);
            this.pnlContent.Controls.Add(this.lblSummaryTotal);
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(0, 76);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Size = new System.Drawing.Size(571, 400);
            this.pnlContent.TabIndex = 2;
            //
            // lblServiceCap
            //
            this.lblServiceCap.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblServiceCap.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.lblServiceCap.Location = new System.Drawing.Point(24, 18);
            this.lblServiceCap.Name = "lblServiceCap";
            this.lblServiceCap.Size = new System.Drawing.Size(170, 22);
            this.lblServiceCap.TabIndex = 0;
            this.lblServiceCap.Text = "Service Charge";
            this.lblServiceCap.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // txtServiceAmount
            //
            this.txtServiceAmount.BackColor = System.Drawing.Color.White;
            this.txtServiceAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtServiceAmount.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtServiceAmount.Location = new System.Drawing.Point(200, 16);
            this.txtServiceAmount.Name = "txtServiceAmount";
            this.txtServiceAmount.Size = new System.Drawing.Size(120, 25);
            this.txtServiceAmount.TabIndex = 1;
            this.txtServiceAmount.Text = "0.00";
            this.txtServiceAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            //
            // txtServiceTax
            //
            this.txtServiceTax.BackColor = System.Drawing.Color.White;
            this.txtServiceTax.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtServiceTax.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtServiceTax.Location = new System.Drawing.Point(328, 16);
            this.txtServiceTax.Name = "txtServiceTax";
            this.txtServiceTax.Size = new System.Drawing.Size(110, 25);
            this.txtServiceTax.TabIndex = 2;
            this.txtServiceTax.Text = "0.00";
            this.txtServiceTax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            //
            // lblServiceTotal
            //
            this.lblServiceTotal.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblServiceTotal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(87)))), ((int)(((byte)(208)))));
            this.lblServiceTotal.Location = new System.Drawing.Point(448, 18);
            this.lblServiceTotal.Name = "lblServiceTotal";
            this.lblServiceTotal.Size = new System.Drawing.Size(110, 22);
            this.lblServiceTotal.TabIndex = 3;
            this.lblServiceTotal.Text = "$0.00";
            this.lblServiceTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            //
            // lblAreaCap
            //
            this.lblAreaCap.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblAreaCap.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.lblAreaCap.Location = new System.Drawing.Point(24, 66);
            this.lblAreaCap.Name = "lblAreaCap";
            this.lblAreaCap.Size = new System.Drawing.Size(170, 22);
            this.lblAreaCap.TabIndex = 4;
            this.lblAreaCap.Text = "Area Gratuity";
            this.lblAreaCap.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // txtAreaAmount
            //
            this.txtAreaAmount.BackColor = System.Drawing.Color.White;
            this.txtAreaAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAreaAmount.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtAreaAmount.Location = new System.Drawing.Point(200, 64);
            this.txtAreaAmount.Name = "txtAreaAmount";
            this.txtAreaAmount.Size = new System.Drawing.Size(120, 25);
            this.txtAreaAmount.TabIndex = 5;
            this.txtAreaAmount.Text = "0.00";
            this.txtAreaAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            //
            // txtAreaTax
            //
            this.txtAreaTax.BackColor = System.Drawing.Color.White;
            this.txtAreaTax.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAreaTax.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtAreaTax.Location = new System.Drawing.Point(328, 64);
            this.txtAreaTax.Name = "txtAreaTax";
            this.txtAreaTax.Size = new System.Drawing.Size(110, 25);
            this.txtAreaTax.TabIndex = 6;
            this.txtAreaTax.Text = "0.00";
            this.txtAreaTax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            //
            // lblAreaTotal
            //
            this.lblAreaTotal.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblAreaTotal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(87)))), ((int)(((byte)(208)))));
            this.lblAreaTotal.Location = new System.Drawing.Point(448, 66);
            this.lblAreaTotal.Name = "lblAreaTotal";
            this.lblAreaTotal.Size = new System.Drawing.Size(110, 22);
            this.lblAreaTotal.TabIndex = 7;
            this.lblAreaTotal.Text = "$0.00";
            this.lblAreaTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            //
            // lblPartyCap
            //
            this.lblPartyCap.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblPartyCap.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.lblPartyCap.Location = new System.Drawing.Point(24, 114);
            this.lblPartyCap.Name = "lblPartyCap";
            this.lblPartyCap.Size = new System.Drawing.Size(170, 22);
            this.lblPartyCap.TabIndex = 8;
            this.lblPartyCap.Text = "Party Gratuity";
            this.lblPartyCap.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // txtPartyAmount
            //
            this.txtPartyAmount.BackColor = System.Drawing.Color.White;
            this.txtPartyAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPartyAmount.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtPartyAmount.Location = new System.Drawing.Point(200, 112);
            this.txtPartyAmount.Name = "txtPartyAmount";
            this.txtPartyAmount.Size = new System.Drawing.Size(120, 25);
            this.txtPartyAmount.TabIndex = 9;
            this.txtPartyAmount.Text = "0.00";
            this.txtPartyAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            //
            // txtPartyTax
            //
            this.txtPartyTax.BackColor = System.Drawing.Color.White;
            this.txtPartyTax.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPartyTax.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtPartyTax.Location = new System.Drawing.Point(328, 112);
            this.txtPartyTax.Name = "txtPartyTax";
            this.txtPartyTax.Size = new System.Drawing.Size(110, 25);
            this.txtPartyTax.TabIndex = 10;
            this.txtPartyTax.Text = "0.00";
            this.txtPartyTax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            //
            // lblPartyTotal
            //
            this.lblPartyTotal.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblPartyTotal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(87)))), ((int)(((byte)(208)))));
            this.lblPartyTotal.Location = new System.Drawing.Point(448, 114);
            this.lblPartyTotal.Name = "lblPartyTotal";
            this.lblPartyTotal.Size = new System.Drawing.Size(110, 22);
            this.lblPartyTotal.TabIndex = 11;
            this.lblPartyTotal.Text = "$0.00";
            this.lblPartyTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            //
            // lblAutoCap
            //
            this.lblAutoCap.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblAutoCap.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.lblAutoCap.Location = new System.Drawing.Point(24, 162);
            this.lblAutoCap.Name = "lblAutoCap";
            this.lblAutoCap.Size = new System.Drawing.Size(170, 22);
            this.lblAutoCap.TabIndex = 12;
            this.lblAutoCap.Text = "Auto Service Charge";
            this.lblAutoCap.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // txtAutoAmount
            //
            this.txtAutoAmount.BackColor = System.Drawing.Color.White;
            this.txtAutoAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAutoAmount.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtAutoAmount.Location = new System.Drawing.Point(200, 160);
            this.txtAutoAmount.Name = "txtAutoAmount";
            this.txtAutoAmount.Size = new System.Drawing.Size(120, 25);
            this.txtAutoAmount.TabIndex = 13;
            this.txtAutoAmount.Text = "0.00";
            this.txtAutoAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            //
            // txtAutoTax
            //
            this.txtAutoTax.BackColor = System.Drawing.Color.White;
            this.txtAutoTax.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAutoTax.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtAutoTax.Location = new System.Drawing.Point(328, 160);
            this.txtAutoTax.Name = "txtAutoTax";
            this.txtAutoTax.Size = new System.Drawing.Size(110, 25);
            this.txtAutoTax.TabIndex = 14;
            this.txtAutoTax.Text = "0.00";
            this.txtAutoTax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            //
            // lblAutoTotal
            //
            this.lblAutoTotal.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblAutoTotal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(87)))), ((int)(((byte)(208)))));
            this.lblAutoTotal.Location = new System.Drawing.Point(448, 162);
            this.lblAutoTotal.Name = "lblAutoTotal";
            this.lblAutoTotal.Size = new System.Drawing.Size(110, 22);
            this.lblAutoTotal.TabIndex = 15;
            this.lblAutoTotal.Text = "$0.00";
            this.lblAutoTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            //
            // lblExtraCap
            //
            this.lblExtraCap.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblExtraCap.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.lblExtraCap.Location = new System.Drawing.Point(24, 210);
            this.lblExtraCap.Name = "lblExtraCap";
            this.lblExtraCap.Size = new System.Drawing.Size(170, 22);
            this.lblExtraCap.TabIndex = 16;
            this.lblExtraCap.Text = "Extra Fee";
            this.lblExtraCap.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // txtExtraAmount
            //
            this.txtExtraAmount.BackColor = System.Drawing.Color.White;
            this.txtExtraAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtExtraAmount.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtExtraAmount.Location = new System.Drawing.Point(200, 208);
            this.txtExtraAmount.Name = "txtExtraAmount";
            this.txtExtraAmount.Size = new System.Drawing.Size(120, 25);
            this.txtExtraAmount.TabIndex = 17;
            this.txtExtraAmount.Text = "0.00";
            this.txtExtraAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            //
            // txtExtraTax
            //
            this.txtExtraTax.BackColor = System.Drawing.Color.White;
            this.txtExtraTax.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtExtraTax.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtExtraTax.Location = new System.Drawing.Point(328, 208);
            this.txtExtraTax.Name = "txtExtraTax";
            this.txtExtraTax.Size = new System.Drawing.Size(110, 25);
            this.txtExtraTax.TabIndex = 18;
            this.txtExtraTax.Text = "0.00";
            this.txtExtraTax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            //
            // lblExtraTotal
            //
            this.lblExtraTotal.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblExtraTotal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(87)))), ((int)(((byte)(208)))));
            this.lblExtraTotal.Location = new System.Drawing.Point(448, 210);
            this.lblExtraTotal.Name = "lblExtraTotal";
            this.lblExtraTotal.Size = new System.Drawing.Size(110, 22);
            this.lblExtraTotal.TabIndex = 19;
            this.lblExtraTotal.Text = "$0.00";
            this.lblExtraTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            //
            // lblSVCFeeCap
            //
            this.lblSVCFeeCap.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblSVCFeeCap.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.lblSVCFeeCap.Location = new System.Drawing.Point(24, 258);
            this.lblSVCFeeCap.Name = "lblSVCFeeCap";
            this.lblSVCFeeCap.Size = new System.Drawing.Size(170, 22);
            this.lblSVCFeeCap.TabIndex = 20;
            this.lblSVCFeeCap.Text = "SVC Fee";
            this.lblSVCFeeCap.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // txtSVCFeeAmount
            //
            this.txtSVCFeeAmount.BackColor = System.Drawing.Color.White;
            this.txtSVCFeeAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSVCFeeAmount.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSVCFeeAmount.Location = new System.Drawing.Point(200, 256);
            this.txtSVCFeeAmount.Name = "txtSVCFeeAmount";
            this.txtSVCFeeAmount.Size = new System.Drawing.Size(120, 25);
            this.txtSVCFeeAmount.TabIndex = 21;
            this.txtSVCFeeAmount.Text = "0.00";
            this.txtSVCFeeAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            //
            // txtSVCFeeTax
            //
            this.txtSVCFeeTax.BackColor = System.Drawing.Color.White;
            this.txtSVCFeeTax.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSVCFeeTax.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSVCFeeTax.Location = new System.Drawing.Point(328, 256);
            this.txtSVCFeeTax.Name = "txtSVCFeeTax";
            this.txtSVCFeeTax.Size = new System.Drawing.Size(110, 25);
            this.txtSVCFeeTax.TabIndex = 22;
            this.txtSVCFeeTax.Text = "0.00";
            this.txtSVCFeeTax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            //
            // lblSVCFeeTotal
            //
            this.lblSVCFeeTotal.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblSVCFeeTotal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(87)))), ((int)(((byte)(208)))));
            this.lblSVCFeeTotal.Location = new System.Drawing.Point(448, 258);
            this.lblSVCFeeTotal.Name = "lblSVCFeeTotal";
            this.lblSVCFeeTotal.Size = new System.Drawing.Size(110, 22);
            this.lblSVCFeeTotal.TabIndex = 23;
            this.lblSVCFeeTotal.Text = "$0.00";
            this.lblSVCFeeTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            //
            // lblCashDiscCap
            //
            this.lblCashDiscCap.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblCashDiscCap.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.lblCashDiscCap.Location = new System.Drawing.Point(24, 306);
            this.lblCashDiscCap.Name = "lblCashDiscCap";
            this.lblCashDiscCap.Size = new System.Drawing.Size(170, 22);
            this.lblCashDiscCap.TabIndex = 24;
            this.lblCashDiscCap.Text = "Cash Discount";
            this.lblCashDiscCap.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // txtCashDiscAmount
            //
            this.txtCashDiscAmount.BackColor = System.Drawing.Color.White;
            this.txtCashDiscAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCashDiscAmount.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtCashDiscAmount.Location = new System.Drawing.Point(200, 304);
            this.txtCashDiscAmount.Name = "txtCashDiscAmount";
            this.txtCashDiscAmount.Size = new System.Drawing.Size(120, 25);
            this.txtCashDiscAmount.TabIndex = 25;
            this.txtCashDiscAmount.Text = "0.00";
            this.txtCashDiscAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            //
            // txtCashDiscTax
            //
            this.txtCashDiscTax.BackColor = System.Drawing.Color.White;
            this.txtCashDiscTax.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCashDiscTax.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtCashDiscTax.Location = new System.Drawing.Point(328, 304);
            this.txtCashDiscTax.Name = "txtCashDiscTax";
            this.txtCashDiscTax.Size = new System.Drawing.Size(110, 25);
            this.txtCashDiscTax.TabIndex = 26;
            this.txtCashDiscTax.Text = "0.00";
            this.txtCashDiscTax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            //
            // lblCashDiscTotal
            //
            this.lblCashDiscTotal.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblCashDiscTotal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(87)))), ((int)(((byte)(208)))));
            this.lblCashDiscTotal.Location = new System.Drawing.Point(448, 306);
            this.lblCashDiscTotal.Name = "lblCashDiscTotal";
            this.lblCashDiscTotal.Size = new System.Drawing.Size(110, 22);
            this.lblCashDiscTotal.TabIndex = 27;
            this.lblCashDiscTotal.Text = "$0.00";
            this.lblCashDiscTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            //
            // pnlSummaryDivider
            //
            this.pnlSummaryDivider.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(214)))), ((int)(((byte)(230)))));
            this.pnlSummaryDivider.Location = new System.Drawing.Point(24, 352);
            this.pnlSummaryDivider.Name = "pnlSummaryDivider";
            this.pnlSummaryDivider.Size = new System.Drawing.Size(534, 2);
            this.pnlSummaryDivider.TabIndex = 28;
            //
            // lblSummaryCap
            //
            this.lblSummaryCap.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblSummaryCap.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.lblSummaryCap.Location = new System.Drawing.Point(24, 366);
            this.lblSummaryCap.Name = "lblSummaryCap";
            this.lblSummaryCap.Size = new System.Drawing.Size(170, 22);
            this.lblSummaryCap.TabIndex = 29;
            this.lblSummaryCap.Text = "SUMMARY";
            this.lblSummaryCap.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // lblSummaryAmount
            //
            this.lblSummaryAmount.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblSummaryAmount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.lblSummaryAmount.Location = new System.Drawing.Point(200, 366);
            this.lblSummaryAmount.Name = "lblSummaryAmount";
            this.lblSummaryAmount.Size = new System.Drawing.Size(118, 22);
            this.lblSummaryAmount.TabIndex = 30;
            this.lblSummaryAmount.Text = "$0.00";
            this.lblSummaryAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            //
            // lblSummaryTax
            //
            this.lblSummaryTax.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblSummaryTax.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.lblSummaryTax.Location = new System.Drawing.Point(326, 366);
            this.lblSummaryTax.Name = "lblSummaryTax";
            this.lblSummaryTax.Size = new System.Drawing.Size(112, 22);
            this.lblSummaryTax.TabIndex = 31;
            this.lblSummaryTax.Text = "$0.00";
            this.lblSummaryTax.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            //
            // lblSummaryTotal
            //
            this.lblSummaryTotal.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblSummaryTotal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(87)))), ((int)(((byte)(208)))));
            this.lblSummaryTotal.Location = new System.Drawing.Point(448, 366);
            this.lblSummaryTotal.Name = "lblSummaryTotal";
            this.lblSummaryTotal.Size = new System.Drawing.Size(110, 22);
            this.lblSummaryTotal.TabIndex = 32;
            this.lblSummaryTotal.Text = "$0.00";
            this.lblSummaryTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            //
            // ServiceChargeForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(571, 528);
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.pnlColumnHeader);
            this.Controls.Add(this.pnlFooter);
            this.Controls.Add(this.pnlTitleBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ServiceChargeForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.pnlTitleBar.ResumeLayout(false);
            this.pnlColumnHeader.ResumeLayout(false);
            this.pnlFooter.ResumeLayout(false);
            this.pnlContent.ResumeLayout(false);
            this.pnlContent.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTitleBar;
        private System.Windows.Forms.Button btnCloseForm;
        private System.Windows.Forms.Label lblFormTitle;
        private System.Windows.Forms.Panel pnlColumnHeader;
        private System.Windows.Forms.Label lblColName;
        private System.Windows.Forms.Label lblColAmount;
        private System.Windows.Forms.Label lblColTax;
        private System.Windows.Forms.Label lblColTotal;
        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.Panel pnlFooterDivider;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDone;
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.Label lblServiceCap;
        private System.Windows.Forms.TextBox txtServiceAmount;
        private System.Windows.Forms.TextBox txtServiceTax;
        private System.Windows.Forms.Label lblServiceTotal;
        private System.Windows.Forms.Label lblAreaCap;
        private System.Windows.Forms.TextBox txtAreaAmount;
        private System.Windows.Forms.TextBox txtAreaTax;
        private System.Windows.Forms.Label lblAreaTotal;
        private System.Windows.Forms.Label lblPartyCap;
        private System.Windows.Forms.TextBox txtPartyAmount;
        private System.Windows.Forms.TextBox txtPartyTax;
        private System.Windows.Forms.Label lblPartyTotal;
        private System.Windows.Forms.Label lblAutoCap;
        private System.Windows.Forms.TextBox txtAutoAmount;
        private System.Windows.Forms.TextBox txtAutoTax;
        private System.Windows.Forms.Label lblAutoTotal;
        private System.Windows.Forms.Label lblExtraCap;
        private System.Windows.Forms.TextBox txtExtraAmount;
        private System.Windows.Forms.TextBox txtExtraTax;
        private System.Windows.Forms.Label lblExtraTotal;
        private System.Windows.Forms.Label lblSVCFeeCap;
        private System.Windows.Forms.TextBox txtSVCFeeAmount;
        private System.Windows.Forms.TextBox txtSVCFeeTax;
        private System.Windows.Forms.Label lblSVCFeeTotal;
        private System.Windows.Forms.Label lblCashDiscCap;
        private System.Windows.Forms.TextBox txtCashDiscAmount;
        private System.Windows.Forms.TextBox txtCashDiscTax;
        private System.Windows.Forms.Label lblCashDiscTotal;
        private System.Windows.Forms.Panel pnlSummaryDivider;
        private System.Windows.Forms.Label lblSummaryCap;
        private System.Windows.Forms.Label lblSummaryAmount;
        private System.Windows.Forms.Label lblSummaryTax;
        private System.Windows.Forms.Label lblSummaryTotal;
    }
}
