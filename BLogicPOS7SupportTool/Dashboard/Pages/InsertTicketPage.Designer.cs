namespace BLogicPOS7SupportTool.Dashboard.Pages
{
    partial class InsertTicketPage
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCheckBoxColumn colSelected;
            System.Windows.Forms.DataGridViewTextBoxColumn colId;
            System.Windows.Forms.DataGridViewTextBoxColumn colState;
            System.Windows.Forms.DataGridViewTextBoxColumn colMessage;
            System.Windows.Forms.DataGridViewTextBoxColumn colDate;
            System.Windows.Forms.DataGridViewTextBoxColumn colWorkstation;
            System.Windows.Forms.DataGridViewTextBoxColumn colEmployeeCode;
            System.Windows.Forms.DataGridViewTextBoxColumn colEmployee;
            System.Windows.Forms.DataGridViewTextBoxColumn colItem;
            System.Windows.Forms.DataGridViewTextBoxColumn colAmount;
            System.Windows.Forms.DataGridViewTextBoxColumn colGratuity;
            System.Windows.Forms.DataGridViewTextBoxColumn colTotal;
            System.Windows.Forms.DataGridViewTextBoxColumn colTicketTotal;
            System.Windows.Forms.DataGridViewTextBoxColumn colTax;
            System.Windows.Forms.DataGridViewTextBoxColumn colTaxRate;
            System.Windows.Forms.DataGridViewTextBoxColumn colSVCFee;
            System.Windows.Forms.DataGridViewTextBoxColumn colMerchantFee;
            System.Windows.Forms.DataGridViewTextBoxColumn colCard;
            System.Windows.Forms.DataGridViewTextBoxColumn colCardType;
            System.Windows.Forms.DataGridViewTextBoxColumn colAuthCode;
            System.Windows.Forms.DataGridViewTextBoxColumn colInvoiceNo;
            System.Windows.Forms.DataGridViewTextBoxColumn colRefNo;
            System.Windows.Forms.DataGridViewTextBoxColumn colTerminal;
            System.Windows.Forms.DataGridViewCellStyle amountStyle = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.pnlHeaderDivider = new System.Windows.Forms.Panel();
            this.pnlToolbar = new System.Windows.Forms.Panel();
            this.lblFile = new System.Windows.Forms.Label();
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.btnDownload = new System.Windows.Forms.Button();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.btnReload = new System.Windows.Forms.Button();
            this.btnSelectAll = new System.Windows.Forms.Button();
            this.btnSelectNone = new System.Windows.Forms.Button();
            this.btnInsert = new System.Windows.Forms.Button();
            this.pnlToolbarDivider = new System.Windows.Forms.Panel();
            this.pnlBody = new System.Windows.Forms.Panel();
            this.dgvRows = new System.Windows.Forms.DataGridView();
            this.pnlLoading = new System.Windows.Forms.Panel();
            this.lblLoading = new System.Windows.Forms.Label();
            this.pbLoading = new System.Windows.Forms.ProgressBar();
            this.pnlFooter = new System.Windows.Forms.Panel();
            this.pnlFooterDivider = new System.Windows.Forms.Panel();
            this.lblSummary = new System.Windows.Forms.Label();
            colSelected = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            colId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colState = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colMessage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colWorkstation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colEmployeeCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colEmployee = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colItem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colGratuity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colTicketTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colTax = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colTaxRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colSVCFee = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colMerchantFee = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colCard = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colCardType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colAuthCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colInvoiceNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colRefNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colTerminal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlHeader.SuspendLayout();
            this.pnlToolbar.SuspendLayout();
            this.pnlBody.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRows)).BeginInit();
            this.pnlLoading.SuspendLayout();
            this.pnlFooter.SuspendLayout();
            this.SuspendLayout();
            //
            // pnlHeader
            //
            this.pnlHeader.BackColor = System.Drawing.Color.White;
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Controls.Add(this.lblSubtitle);
            this.pnlHeader.Controls.Add(this.pnlHeaderDivider);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(984, 78);
            this.pnlHeader.TabIndex = 0;
            //
            // lblTitle
            //
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.lblTitle.Location = new System.Drawing.Point(24, 12);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(316, 32);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "Insert Tickets From Template";
            //
            // lblSubtitle
            //
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.lblSubtitle.Location = new System.Drawing.Point(26, 48);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(700, 17);
            this.lblSubtitle.TabIndex = 2;
            this.lblSubtitle.Text = "Each row becomes one settled credit-card ticket: SaleReceipts, SaleReceiptDetails, SaleReceiptDetailTaxDetails, SaleReceiptPaymentDetails, PaymentRoleDetails, Batch and TransactionDetail.";
            //
            // pnlHeaderDivider
            //
            this.pnlHeaderDivider.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            this.pnlHeaderDivider.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlHeaderDivider.Location = new System.Drawing.Point(0, 77);
            this.pnlHeaderDivider.Name = "pnlHeaderDivider";
            this.pnlHeaderDivider.Size = new System.Drawing.Size(984, 1);
            this.pnlHeaderDivider.TabIndex = 0;
            //
            // pnlToolbar
            //
            this.pnlToolbar.BackColor = System.Drawing.Color.White;
            this.pnlToolbar.Controls.Add(this.lblFile);
            this.pnlToolbar.Controls.Add(this.txtFilePath);
            this.pnlToolbar.Controls.Add(this.btnDownload);
            this.pnlToolbar.Controls.Add(this.btnBrowse);
            this.pnlToolbar.Controls.Add(this.btnReload);
            this.pnlToolbar.Controls.Add(this.btnSelectAll);
            this.pnlToolbar.Controls.Add(this.btnSelectNone);
            this.pnlToolbar.Controls.Add(this.btnInsert);
            this.pnlToolbar.Controls.Add(this.pnlToolbarDivider);
            this.pnlToolbar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolbar.Location = new System.Drawing.Point(0, 78);
            this.pnlToolbar.Name = "pnlToolbar";
            this.pnlToolbar.Size = new System.Drawing.Size(984, 60);
            this.pnlToolbar.TabIndex = 1;
            //
            // lblFile
            //
            this.lblFile.AutoSize = true;
            this.lblFile.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblFile.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.lblFile.Location = new System.Drawing.Point(24, 20);
            this.lblFile.Name = "lblFile";
            this.lblFile.Size = new System.Drawing.Size(66, 19);
            this.lblFile.TabIndex = 0;
            this.lblFile.Text = "Template:";
            //
            // txtFilePath
            //
            this.txtFilePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFilePath.BackColor = System.Drawing.Color.White;
            this.txtFilePath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFilePath.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtFilePath.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.txtFilePath.Location = new System.Drawing.Point(96, 16);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.ReadOnly = true;
            this.txtFilePath.Size = new System.Drawing.Size(232, 25);
            this.txtFilePath.TabIndex = 1;
            this.txtFilePath.Text = "";
            //
            // btnDownload
            //
            this.btnDownload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDownload.BackColor = System.Drawing.Color.White;
            this.btnDownload.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            this.btnDownload.FlatAppearance.BorderSize = 1;
            this.btnDownload.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDownload.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnDownload.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.btnDownload.Location = new System.Drawing.Point(336, 14);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(150, 30);
            this.btnDownload.TabIndex = 2;
            this.btnDownload.Text = "Download template";
            this.btnDownload.UseVisualStyleBackColor = false;
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            //
            // btnBrowse
            //
            this.btnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowse.BackColor = System.Drawing.Color.White;
            this.btnBrowse.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            this.btnBrowse.FlatAppearance.BorderSize = 1;
            this.btnBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowse.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnBrowse.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.btnBrowse.Location = new System.Drawing.Point(494, 14);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(92, 30);
            this.btnBrowse.TabIndex = 3;
            this.btnBrowse.Text = "Browse...";
            this.btnBrowse.UseVisualStyleBackColor = false;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            //
            // btnReload
            //
            this.btnReload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReload.BackColor = System.Drawing.Color.White;
            this.btnReload.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            this.btnReload.FlatAppearance.BorderSize = 1;
            this.btnReload.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReload.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnReload.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.btnReload.Location = new System.Drawing.Point(592, 14);
            this.btnReload.Name = "btnReload";
            this.btnReload.Size = new System.Drawing.Size(80, 30);
            this.btnReload.TabIndex = 4;
            this.btnReload.Text = "Reload";
            this.btnReload.UseVisualStyleBackColor = false;
            this.btnReload.Click += new System.EventHandler(this.btnReload_Click);
            //
            // btnSelectAll
            //
            this.btnSelectAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelectAll.BackColor = System.Drawing.Color.White;
            this.btnSelectAll.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            this.btnSelectAll.FlatAppearance.BorderSize = 1;
            this.btnSelectAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelectAll.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnSelectAll.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.btnSelectAll.Location = new System.Drawing.Point(678, 14);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(72, 30);
            this.btnSelectAll.TabIndex = 5;
            this.btnSelectAll.Text = "Select all";
            this.btnSelectAll.UseVisualStyleBackColor = false;
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
            //
            // btnSelectNone
            //
            this.btnSelectNone.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelectNone.BackColor = System.Drawing.Color.White;
            this.btnSelectNone.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            this.btnSelectNone.FlatAppearance.BorderSize = 1;
            this.btnSelectNone.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelectNone.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnSelectNone.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.btnSelectNone.Location = new System.Drawing.Point(756, 14);
            this.btnSelectNone.Name = "btnSelectNone";
            this.btnSelectNone.Size = new System.Drawing.Size(72, 30);
            this.btnSelectNone.TabIndex = 6;
            this.btnSelectNone.Text = "Clear all";
            this.btnSelectNone.UseVisualStyleBackColor = false;
            this.btnSelectNone.Click += new System.EventHandler(this.btnSelectNone_Click);
            //
            // btnInsert
            //
            this.btnInsert.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnInsert.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(87)))), ((int)(((byte)(208)))));
            this.btnInsert.FlatAppearance.BorderSize = 0;
            this.btnInsert.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInsert.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnInsert.ForeColor = System.Drawing.Color.White;
            this.btnInsert.Location = new System.Drawing.Point(834, 14);
            this.btnInsert.Name = "btnInsert";
            this.btnInsert.Size = new System.Drawing.Size(126, 30);
            this.btnInsert.TabIndex = 7;
            this.btnInsert.Text = "Insert selected";
            this.btnInsert.UseVisualStyleBackColor = false;
            this.btnInsert.Click += new System.EventHandler(this.btnInsert_Click);
            //
            // pnlToolbarDivider
            //
            this.pnlToolbarDivider.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            this.pnlToolbarDivider.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlToolbarDivider.Location = new System.Drawing.Point(0, 59);
            this.pnlToolbarDivider.Name = "pnlToolbarDivider";
            this.pnlToolbarDivider.Size = new System.Drawing.Size(984, 1);
            this.pnlToolbarDivider.TabIndex = 8;
            //
            // pnlBody
            //
            this.pnlBody.BackColor = System.Drawing.Color.White;
            this.pnlBody.Controls.Add(this.dgvRows);
            this.pnlBody.Controls.Add(this.pnlLoading);
            this.pnlBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBody.Location = new System.Drawing.Point(0, 138);
            this.pnlBody.Name = "pnlBody";
            this.pnlBody.Size = new System.Drawing.Size(984, 546);
            this.pnlBody.TabIndex = 2;
            //
            // dgvRows
            //
            this.dgvRows.AllowUserToAddRows = false;
            this.dgvRows.AllowUserToDeleteRows = false;
            this.dgvRows.AllowUserToResizeRows = false;
            this.dgvRows.AutoGenerateColumns = false;
            this.dgvRows.BackgroundColor = System.Drawing.Color.White;
            this.dgvRows.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvRows.ColumnHeadersHeight = 40;
            this.dgvRows.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            // Status and Note come first: when a row cannot be inserted, the reason has
            // to be on screen without scrolling.
            this.dgvRows.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                colSelected,
                colId,
                colState,
                colMessage,
                colDate,
                colWorkstation,
                colEmployeeCode,
                colEmployee,
                colItem,
                colAmount,
                colGratuity,
                colTotal,
                colTicketTotal,
                colTax,
                colTaxRate,
                colSVCFee,
                colMerchantFee,
                colCard,
                colCardType,
                colAuthCode,
                colInvoiceNo,
                colRefNo,
                colTerminal});
            this.dgvRows.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvRows.Location = new System.Drawing.Point(0, 0);
            this.dgvRows.MultiSelect = false;
            this.dgvRows.Name = "dgvRows";
            this.dgvRows.RowHeadersVisible = false;
            this.dgvRows.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRows.Size = new System.Drawing.Size(984, 546);
            this.dgvRows.TabIndex = 0;
            this.dgvRows.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvRows_CellContentClick);
            this.dgvRows.CurrentCellDirtyStateChanged += new System.EventHandler(this.dgvRows_CurrentCellDirtyStateChanged);
            this.dgvRows.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvRows_DataBindingComplete);
            this.dgvRows.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.dgvRows_RowPrePaint);
            //
            // colSelected
            //
            colSelected.DataPropertyName = "Selected";
            colSelected.HeaderText = "Insert";
            colSelected.Name = "colSelected";
            colSelected.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            colSelected.Width = 56;
            //
            // colId
            //
            colId.DataPropertyName = "Id";
            colId.HeaderText = "ID";
            colId.Name = "colId";
            colId.ReadOnly = true;
            colId.Width = 50;
            //
            // colState
            //
            colState.DataPropertyName = "StateText";
            colState.HeaderText = "Status";
            colState.Name = "colState";
            colState.ReadOnly = true;
            colState.Width = 110;
            //
            // colMessage
            //
            colMessage.DataPropertyName = "Message";
            colMessage.HeaderText = "Note";
            colMessage.MinimumWidth = 200;
            colMessage.Name = "colMessage";
            colMessage.ReadOnly = true;
            colMessage.Width = 320;
            //
            // colDate
            //
            colDate.DataPropertyName = "DateText";
            colDate.HeaderText = "Date / Time";
            colDate.Name = "colDate";
            colDate.ReadOnly = true;
            colDate.Width = 150;
            //
            // colWorkstation
            //
            colWorkstation.DataPropertyName = "WorkstationName";
            colWorkstation.HeaderText = "Workstation";
            colWorkstation.Name = "colWorkstation";
            colWorkstation.ReadOnly = true;
            colWorkstation.Width = 110;
            //
            // colEmployeeCode
            //
            colEmployeeCode.DataPropertyName = "EmployeeCode";
            colEmployeeCode.HeaderText = "Emp Code";
            colEmployeeCode.Name = "colEmployeeCode";
            colEmployeeCode.ReadOnly = true;
            colEmployeeCode.Width = 90;
            //
            // colEmployee
            //
            colEmployee.DataPropertyName = "EmployeeFullName";
            colEmployee.HeaderText = "Employee";
            colEmployee.Name = "colEmployee";
            colEmployee.ReadOnly = true;
            colEmployee.Width = 120;
            //
            // colItem
            //
            colItem.DataPropertyName = "ItemName";
            colItem.HeaderText = "Item";
            colItem.Name = "colItem";
            colItem.ReadOnly = true;
            colItem.Width = 160;
            //
            // colAmount
            //
            amountStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            colAmount.DataPropertyName = "AmountText";
            colAmount.DefaultCellStyle = amountStyle;
            colAmount.HeaderText = "Amount";
            colAmount.Name = "colAmount";
            colAmount.ReadOnly = true;
            colAmount.ToolTipText = "Charged on the card, tip excluded (Templates.Amount)";
            colAmount.Width = 85;
            //
            // colGratuity
            //
            colGratuity.DataPropertyName = "GratuityText";
            colGratuity.DefaultCellStyle = amountStyle;
            colGratuity.HeaderText = "Tip";
            colGratuity.Name = "colGratuity";
            colGratuity.ReadOnly = true;
            colGratuity.Width = 70;
            //
            // colTotal
            //
            colTotal.DataPropertyName = "TotalText";
            colTotal.DefaultCellStyle = amountStyle;
            colTotal.HeaderText = "Total";
            colTotal.Name = "colTotal";
            colTotal.ReadOnly = true;
            colTotal.ToolTipText = "Charged on the card including the tip (Templates.Total)";
            colTotal.Width = 85;
            //
            // colTicketTotal
            //
            colTicketTotal.DataPropertyName = "TicketTotalText";
            colTicketTotal.DefaultCellStyle = amountStyle;
            colTicketTotal.HeaderText = "Ticket Total";
            colTicketTotal.Name = "colTicketTotal";
            colTicketTotal.ReadOnly = true;
            colTicketTotal.ToolTipText = "SaleReceipts.TotalAmount - the charged amount less the custom fee";
            colTicketTotal.Width = 95;
            //
            // colTax
            //
            colTax.DataPropertyName = "TaxText";
            colTax.DefaultCellStyle = amountStyle;
            colTax.HeaderText = "Tax";
            colTax.Name = "colTax";
            colTax.ReadOnly = true;
            colTax.ToolTipText = "SaleReceipts.TaxAmount - taken out of the ticket total";
            colTax.Width = 75;
            //
            // colTaxRate
            //
            colTaxRate.DataPropertyName = "TaxRateText";
            colTaxRate.DefaultCellStyle = amountStyle;
            colTaxRate.HeaderText = "Tax %";
            colTaxRate.Name = "colTaxRate";
            colTaxRate.ReadOnly = true;
            colTaxRate.Width = 65;
            //
            // colSVCFee
            //
            colSVCFee.DataPropertyName = "SVCFeeText";
            colSVCFee.DefaultCellStyle = amountStyle;
            colSVCFee.HeaderText = "SVC Fee";
            colSVCFee.Name = "colSVCFee";
            colSVCFee.ReadOnly = true;
            colSVCFee.Width = 80;
            //
            // colMerchantFee
            //
            colMerchantFee.DataPropertyName = "MerchantFeeText";
            colMerchantFee.DefaultCellStyle = amountStyle;
            colMerchantFee.HeaderText = "Merchant Fee";
            colMerchantFee.Name = "colMerchantFee";
            colMerchantFee.ReadOnly = true;
            colMerchantFee.Width = 100;
            //
            // colCard
            //
            colCard.DataPropertyName = "CardNumber";
            colCard.HeaderText = "Card Number";
            colCard.Name = "colCard";
            colCard.ReadOnly = true;
            colCard.Width = 130;
            //
            // colCardType
            //
            colCardType.DataPropertyName = "CardType";
            colCardType.HeaderText = "Card Type";
            colCardType.Name = "colCardType";
            colCardType.ReadOnly = true;
            colCardType.Width = 110;
            //
            // colAuthCode
            //
            colAuthCode.DataPropertyName = "AuthCode";
            colAuthCode.HeaderText = "Auth Code";
            colAuthCode.Name = "colAuthCode";
            colAuthCode.ReadOnly = true;
            colAuthCode.Width = 95;
            //
            // colInvoiceNo
            //
            colInvoiceNo.DataPropertyName = "InvoiceNo";
            colInvoiceNo.HeaderText = "Invoice No";
            colInvoiceNo.Name = "colInvoiceNo";
            colInvoiceNo.ReadOnly = true;
            colInvoiceNo.Width = 105;
            //
            // colRefNo
            //
            colRefNo.DataPropertyName = "RefNo";
            colRefNo.HeaderText = "Ref No";
            colRefNo.Name = "colRefNo";
            colRefNo.ReadOnly = true;
            colRefNo.Width = 115;
            //
            // colTerminal
            //
            colTerminal.DataPropertyName = "TerminalID";
            colTerminal.HeaderText = "Terminal ID";
            colTerminal.Name = "colTerminal";
            colTerminal.ReadOnly = true;
            colTerminal.Width = 95;
            //
            // pnlLoading
            //
            this.pnlLoading.BackColor = System.Drawing.Color.White;
            this.pnlLoading.Controls.Add(this.lblLoading);
            this.pnlLoading.Controls.Add(this.pbLoading);
            this.pnlLoading.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLoading.Location = new System.Drawing.Point(0, 0);
            this.pnlLoading.Name = "pnlLoading";
            this.pnlLoading.Size = new System.Drawing.Size(984, 546);
            this.pnlLoading.TabIndex = 1;
            this.pnlLoading.Visible = false;
            //
            // lblLoading
            //
            this.lblLoading.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLoading.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblLoading.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.lblLoading.Location = new System.Drawing.Point(0, 4);
            this.lblLoading.Name = "lblLoading";
            this.lblLoading.Size = new System.Drawing.Size(984, 542);
            this.lblLoading.TabIndex = 1;
            this.lblLoading.Text = "Loading...";
            this.lblLoading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // pbLoading
            //
            this.pbLoading.Dock = System.Windows.Forms.DockStyle.Top;
            this.pbLoading.Location = new System.Drawing.Point(0, 0);
            this.pbLoading.MarqueeAnimationSpeed = 20;
            this.pbLoading.Name = "pbLoading";
            this.pbLoading.Size = new System.Drawing.Size(984, 4);
            this.pbLoading.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.pbLoading.TabIndex = 0;
            //
            // pnlFooter
            //
            this.pnlFooter.BackColor = System.Drawing.Color.White;
            this.pnlFooter.Controls.Add(this.lblSummary);
            this.pnlFooter.Controls.Add(this.pnlFooterDivider);
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Location = new System.Drawing.Point(0, 684);
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Size = new System.Drawing.Size(984, 44);
            this.pnlFooter.TabIndex = 3;
            //
            // pnlFooterDivider
            //
            this.pnlFooterDivider.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            this.pnlFooterDivider.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFooterDivider.Location = new System.Drawing.Point(0, 0);
            this.pnlFooterDivider.Name = "pnlFooterDivider";
            this.pnlFooterDivider.Size = new System.Drawing.Size(984, 1);
            this.pnlFooterDivider.TabIndex = 0;
            //
            // lblSummary
            //
            this.lblSummary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSummary.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSummary.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.lblSummary.Location = new System.Drawing.Point(0, 1);
            this.lblSummary.Name = "lblSummary";
            this.lblSummary.Padding = new System.Windows.Forms.Padding(24, 0, 24, 0);
            this.lblSummary.Size = new System.Drawing.Size(984, 43);
            this.lblSummary.TabIndex = 1;
            this.lblSummary.Text = "";
            this.lblSummary.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // InsertTicketPage
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.pnlBody);
            this.Controls.Add(this.pnlFooter);
            this.Controls.Add(this.pnlToolbar);
            this.Controls.Add(this.pnlHeader);
            this.Name = "InsertTicketPage";
            this.Size = new System.Drawing.Size(984, 728);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlToolbar.ResumeLayout(false);
            this.pnlToolbar.PerformLayout();
            this.pnlBody.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRows)).EndInit();
            this.pnlLoading.ResumeLayout(false);
            this.pnlFooter.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Panel pnlHeaderDivider;
        private System.Windows.Forms.Panel pnlToolbar;
        private System.Windows.Forms.Label lblFile;
        private System.Windows.Forms.TextBox txtFilePath;
        private System.Windows.Forms.Button btnDownload;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Button btnReload;
        private System.Windows.Forms.Button btnSelectAll;
        private System.Windows.Forms.Button btnSelectNone;
        private System.Windows.Forms.Button btnInsert;
        private System.Windows.Forms.Panel pnlToolbarDivider;
        private System.Windows.Forms.Panel pnlBody;
        private System.Windows.Forms.DataGridView dgvRows;
        private System.Windows.Forms.Panel pnlLoading;
        private System.Windows.Forms.ProgressBar pbLoading;
        private System.Windows.Forms.Label lblLoading;
        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.Panel pnlFooterDivider;
        private System.Windows.Forms.Label lblSummary;
    }
}
