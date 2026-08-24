namespace BLogicPOS7SupportTool.Dashboard.Pages
{
    partial class ReceiptPage
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
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlHeaderDivider = new System.Windows.Forms.Panel();
            this.pnlItemsList = new System.Windows.Forms.Panel();
            this.pnlToolbar = new System.Windows.Forms.Panel();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblSearch = new System.Windows.Forms.Label();
            this.pnlToolbarDivider = new System.Windows.Forms.Panel();
            this.btnAdjustPayment = new System.Windows.Forms.Button();
            this.btnRefundItems = new System.Windows.Forms.Button();
            this.btnVoidTicket = new System.Windows.Forms.Button();
            this.btnCloseTicket = new System.Windows.Forms.Button();
            this.btnAddTipByToken = new System.Windows.Forms.Button();
            this.btnUpdateServiceCharges = new System.Windows.Forms.Button();
            this.btnFinishTicket = new System.Windows.Forms.Button();
            this.btnEditItems = new System.Windows.Forms.Button();
            this.btnMergeRefund = new System.Windows.Forms.Button();
            this.btnAddItem = new System.Windows.Forms.Button();
            this.pnlReceipt = new System.Windows.Forms.Panel();
            this.pnlTotals = new System.Windows.Forms.Panel();
            this.pnlCustomerRow = new System.Windows.Forms.Panel();
            this.lblCustomerPhone = new System.Windows.Forms.Label();
            this.lblCustomerName = new System.Windows.Forms.Label();
            this.pnlTicketRow = new System.Windows.Forms.Panel();
            this.lblWorkstation = new System.Windows.Forms.Label();
            this.lblTicketInfo = new System.Windows.Forms.Label();
            this.pnlCashierRow = new System.Windows.Forms.Panel();
            this.lblDateCreated = new System.Windows.Forms.Label();
            this.lblCashierInfo = new System.Windows.Forms.Label();
            this.lblTableName = new System.Windows.Forms.Label();
            this.lblReceiptStatus = new System.Windows.Forms.Label();
            this.lblStoreWebsite = new System.Windows.Forms.Label();
            this.lblStoreEmail = new System.Windows.Forms.Label();
            this.lblStorePhone = new System.Windows.Forms.Label();
            this.lblStoreAddress = new System.Windows.Forms.Label();
            this.lblStoreName = new System.Windows.Forms.Label();
            this.btnCancelTicket = new System.Windows.Forms.Button();
            this.btnSyncPaymentTime = new System.Windows.Forms.Button();
            this.btnInsertPayment = new System.Windows.Forms.Button();
            this.btnAddTip = new System.Windows.Forms.Button();
            this.pnlHeader.SuspendLayout();
            this.pnlToolbar.SuspendLayout();
            this.pnlReceipt.SuspendLayout();
            this.pnlCustomerRow.SuspendLayout();
            this.pnlTicketRow.SuspendLayout();
            this.pnlCashierRow.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.White;
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Controls.Add(this.pnlHeaderDivider);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(784, 64);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.lblTitle.Location = new System.Drawing.Point(24, 16);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(102, 32);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "Receipts";
            // 
            // pnlHeaderDivider
            // 
            this.pnlHeaderDivider.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            this.pnlHeaderDivider.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlHeaderDivider.Location = new System.Drawing.Point(0, 63);
            this.pnlHeaderDivider.Name = "pnlHeaderDivider";
            this.pnlHeaderDivider.Size = new System.Drawing.Size(784, 1);
            this.pnlHeaderDivider.TabIndex = 0;
            // 
            // pnlItemsList
            // 
            this.pnlItemsList.AutoScroll = true;
            this.pnlItemsList.AutoSize = true;
            this.pnlItemsList.BackColor = System.Drawing.Color.White;
            this.pnlItemsList.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlItemsList.Location = new System.Drawing.Point(0, 240);
            this.pnlItemsList.Name = "pnlItemsList";
            this.pnlItemsList.Size = new System.Drawing.Size(281, 0);
            this.pnlItemsList.TabIndex = 10;
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.BackColor = System.Drawing.Color.White;
            this.pnlToolbar.Controls.Add(this.btnSearch);
            this.pnlToolbar.Controls.Add(this.txtSearch);
            this.pnlToolbar.Controls.Add(this.lblSearch);
            this.pnlToolbar.Controls.Add(this.pnlToolbarDivider);
            this.pnlToolbar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolbar.Location = new System.Drawing.Point(0, 64);
            this.pnlToolbar.Name = "pnlToolbar";
            this.pnlToolbar.Size = new System.Drawing.Size(784, 56);
            this.pnlToolbar.TabIndex = 1;
            // 
            // btnSearch
            // 
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.btnSearch.Location = new System.Drawing.Point(363, 12);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(80, 27);
            this.btnSearch.TabIndex = 3;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            // 
            // txtSearch
            // 
            this.txtSearch.BackColor = System.Drawing.Color.White;
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtSearch.Location = new System.Drawing.Point(84, 12);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(280, 27);
            this.txtSearch.TabIndex = 2;
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.lblSearch.Location = new System.Drawing.Point(16, 18);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(52, 19);
            this.lblSearch.TabIndex = 1;
            this.lblSearch.Text = "Search:";
            // 
            // pnlToolbarDivider
            // 
            this.pnlToolbarDivider.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            this.pnlToolbarDivider.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlToolbarDivider.Location = new System.Drawing.Point(0, 55);
            this.pnlToolbarDivider.Name = "pnlToolbarDivider";
            this.pnlToolbarDivider.Size = new System.Drawing.Size(784, 1);
            this.pnlToolbarDivider.TabIndex = 0;
            // 
            // btnAdjustPayment
            // 
            this.btnAdjustPayment.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdjustPayment.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.btnAdjustPayment.Location = new System.Drawing.Point(306, 310);
            this.btnAdjustPayment.Margin = new System.Windows.Forms.Padding(5);
            this.btnAdjustPayment.Name = "btnAdjustPayment";
            this.btnAdjustPayment.Size = new System.Drawing.Size(200, 50);
            this.btnAdjustPayment.TabIndex = 4;
            this.btnAdjustPayment.Text = "Adjust Payments";
            this.btnAdjustPayment.UseVisualStyleBackColor = true;
            this.btnAdjustPayment.Visible = false;
            // 
            // btnRefundItems
            // 
            this.btnRefundItems.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefundItems.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.btnRefundItems.Location = new System.Drawing.Point(306, 430);
            this.btnRefundItems.Margin = new System.Windows.Forms.Padding(5);
            this.btnRefundItems.Name = "btnRefundItems";
            this.btnRefundItems.Size = new System.Drawing.Size(200, 50);
            this.btnRefundItems.TabIndex = 5;
            this.btnRefundItems.Text = "Refund Items";
            this.btnRefundItems.UseVisualStyleBackColor = true;
            this.btnRefundItems.Visible = false;
            // 
            // btnVoidTicket
            // 
            this.btnVoidTicket.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVoidTicket.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.btnVoidTicket.Location = new System.Drawing.Point(306, 190);
            this.btnVoidTicket.Margin = new System.Windows.Forms.Padding(5);
            this.btnVoidTicket.Name = "btnVoidTicket";
            this.btnVoidTicket.Size = new System.Drawing.Size(200, 50);
            this.btnVoidTicket.TabIndex = 6;
            this.btnVoidTicket.Text = "Void Ticket";
            this.btnVoidTicket.UseVisualStyleBackColor = true;
            this.btnVoidTicket.Visible = false;
            // 
            // btnCloseTicket
            // 
            this.btnCloseTicket.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCloseTicket.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.btnCloseTicket.Location = new System.Drawing.Point(306, 130);
            this.btnCloseTicket.Margin = new System.Windows.Forms.Padding(5);
            this.btnCloseTicket.Name = "btnCloseTicket";
            this.btnCloseTicket.Size = new System.Drawing.Size(200, 50);
            this.btnCloseTicket.TabIndex = 7;
            this.btnCloseTicket.Text = "Close Ticket";
            this.btnCloseTicket.UseVisualStyleBackColor = true;
            this.btnCloseTicket.Visible = false;
            // 
            // btnAddTipByToken
            // 
            this.btnAddTipByToken.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddTipByToken.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.btnAddTipByToken.Location = new System.Drawing.Point(516, 130);
            this.btnAddTipByToken.Margin = new System.Windows.Forms.Padding(5);
            this.btnAddTipByToken.Name = "btnAddTipByToken";
            this.btnAddTipByToken.Size = new System.Drawing.Size(200, 50);
            this.btnAddTipByToken.TabIndex = 9;
            this.btnAddTipByToken.Text = "Add Tip By Sale Token";
            this.btnAddTipByToken.UseVisualStyleBackColor = true;
            this.btnAddTipByToken.Visible = false;
            // 
            // btnUpdateServiceCharges
            // 
            this.btnUpdateServiceCharges.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdateServiceCharges.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.btnUpdateServiceCharges.Location = new System.Drawing.Point(306, 370);
            this.btnUpdateServiceCharges.Margin = new System.Windows.Forms.Padding(5);
            this.btnUpdateServiceCharges.Name = "btnUpdateServiceCharges";
            this.btnUpdateServiceCharges.Size = new System.Drawing.Size(200, 50);
            this.btnUpdateServiceCharges.TabIndex = 10;
            this.btnUpdateServiceCharges.Text = "Update Service Charges";
            this.btnUpdateServiceCharges.UseVisualStyleBackColor = true;
            this.btnUpdateServiceCharges.Visible = false;
            // 
            // btnFinishTicket
            // 
            this.btnFinishTicket.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFinishTicket.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.btnFinishTicket.Location = new System.Drawing.Point(516, 250);
            this.btnFinishTicket.Margin = new System.Windows.Forms.Padding(5);
            this.btnFinishTicket.Name = "btnFinishTicket";
            this.btnFinishTicket.Size = new System.Drawing.Size(200, 50);
            this.btnFinishTicket.TabIndex = 11;
            this.btnFinishTicket.Text = "Finish Ticket";
            this.btnFinishTicket.UseVisualStyleBackColor = true;
            this.btnFinishTicket.Visible = false;
            // 
            // btnEditItems
            // 
            this.btnEditItems.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditItems.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.btnEditItems.Location = new System.Drawing.Point(560, 514);
            this.btnEditItems.Margin = new System.Windows.Forms.Padding(5);
            this.btnEditItems.Name = "btnEditItems";
            this.btnEditItems.Size = new System.Drawing.Size(200, 50);
            this.btnEditItems.TabIndex = 12;
            this.btnEditItems.Text = "Edit Items";
            this.btnEditItems.UseVisualStyleBackColor = true;
            this.btnEditItems.Visible = false;
            // 
            // btnMergeRefund
            // 
            this.btnMergeRefund.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMergeRefund.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.btnMergeRefund.Location = new System.Drawing.Point(306, 490);
            this.btnMergeRefund.Margin = new System.Windows.Forms.Padding(5);
            this.btnMergeRefund.Name = "btnMergeRefund";
            this.btnMergeRefund.Size = new System.Drawing.Size(200, 50);
            this.btnMergeRefund.TabIndex = 13;
            this.btnMergeRefund.Text = "Merge Refund Sale Token";
            this.btnMergeRefund.UseVisualStyleBackColor = true;
            this.btnMergeRefund.Visible = false;
            // 
            // btnAddItem
            // 
            this.btnAddItem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddItem.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.btnAddItem.Location = new System.Drawing.Point(560, 418);
            this.btnAddItem.Margin = new System.Windows.Forms.Padding(5);
            this.btnAddItem.Name = "btnAddItem";
            this.btnAddItem.Size = new System.Drawing.Size(200, 50);
            this.btnAddItem.TabIndex = 14;
            this.btnAddItem.Text = "Add Item";
            this.btnAddItem.UseVisualStyleBackColor = true;
            this.btnAddItem.Visible = false;
            // 
            // pnlReceipt
            // 
            this.pnlReceipt.AutoScroll = true;
            this.pnlReceipt.BackColor = System.Drawing.Color.White;
            this.pnlReceipt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlReceipt.Controls.Add(this.pnlTotals);
            this.pnlReceipt.Controls.Add(this.pnlItemsList);
            this.pnlReceipt.Controls.Add(this.pnlCustomerRow);
            this.pnlReceipt.Controls.Add(this.pnlTicketRow);
            this.pnlReceipt.Controls.Add(this.pnlCashierRow);
            this.pnlReceipt.Controls.Add(this.lblTableName);
            this.pnlReceipt.Controls.Add(this.lblReceiptStatus);
            this.pnlReceipt.Controls.Add(this.lblStoreWebsite);
            this.pnlReceipt.Controls.Add(this.lblStoreEmail);
            this.pnlReceipt.Controls.Add(this.lblStorePhone);
            this.pnlReceipt.Controls.Add(this.lblStoreAddress);
            this.pnlReceipt.Controls.Add(this.lblStoreName);
            this.pnlReceipt.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlReceipt.Location = new System.Drawing.Point(0, 120);
            this.pnlReceipt.Name = "pnlReceipt";
            this.pnlReceipt.Padding = new System.Windows.Forms.Padding(0, 12, 0, 0);
            this.pnlReceipt.Size = new System.Drawing.Size(300, 608);
            this.pnlReceipt.TabIndex = 2;
            // 
            // pnlTotals
            // 
            this.pnlTotals.BackColor = System.Drawing.Color.White;
            this.pnlTotals.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTotals.Location = new System.Drawing.Point(0, 240);
            this.pnlTotals.Name = "pnlTotals";
            this.pnlTotals.Size = new System.Drawing.Size(281, 608);
            this.pnlTotals.TabIndex = 16;
            // 
            // pnlCustomerRow
            // 
            this.pnlCustomerRow.BackColor = System.Drawing.Color.White;
            this.pnlCustomerRow.Controls.Add(this.lblCustomerPhone);
            this.pnlCustomerRow.Controls.Add(this.lblCustomerName);
            this.pnlCustomerRow.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCustomerRow.Location = new System.Drawing.Point(0, 218);
            this.pnlCustomerRow.Name = "pnlCustomerRow";
            this.pnlCustomerRow.Size = new System.Drawing.Size(281, 22);
            this.pnlCustomerRow.TabIndex = 9;
            // 
            // lblCustomerPhone
            // 
            this.lblCustomerPhone.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCustomerPhone.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblCustomerPhone.ForeColor = System.Drawing.Color.Black;
            this.lblCustomerPhone.Location = new System.Drawing.Point(137, 0);
            this.lblCustomerPhone.Name = "lblCustomerPhone";
            this.lblCustomerPhone.Padding = new System.Windows.Forms.Padding(0, 0, 6, 0);
            this.lblCustomerPhone.Size = new System.Drawing.Size(144, 22);
            this.lblCustomerPhone.TabIndex = 1;
            this.lblCustomerPhone.Text = "000-000-0000";
            this.lblCustomerPhone.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCustomerName
            // 
            this.lblCustomerName.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblCustomerName.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblCustomerName.ForeColor = System.Drawing.Color.Black;
            this.lblCustomerName.Location = new System.Drawing.Point(0, 0);
            this.lblCustomerName.Name = "lblCustomerName";
            this.lblCustomerName.Padding = new System.Windows.Forms.Padding(6, 0, 0, 0);
            this.lblCustomerName.Size = new System.Drawing.Size(137, 22);
            this.lblCustomerName.TabIndex = 0;
            this.lblCustomerName.Text = "John Wick";
            this.lblCustomerName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlTicketRow
            // 
            this.pnlTicketRow.BackColor = System.Drawing.Color.White;
            this.pnlTicketRow.Controls.Add(this.lblWorkstation);
            this.pnlTicketRow.Controls.Add(this.lblTicketInfo);
            this.pnlTicketRow.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTicketRow.Location = new System.Drawing.Point(0, 196);
            this.pnlTicketRow.Name = "pnlTicketRow";
            this.pnlTicketRow.Size = new System.Drawing.Size(281, 22);
            this.pnlTicketRow.TabIndex = 8;
            // 
            // lblWorkstation
            // 
            this.lblWorkstation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblWorkstation.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblWorkstation.ForeColor = System.Drawing.Color.Black;
            this.lblWorkstation.Location = new System.Drawing.Point(137, 0);
            this.lblWorkstation.Name = "lblWorkstation";
            this.lblWorkstation.Padding = new System.Windows.Forms.Padding(0, 0, 6, 0);
            this.lblWorkstation.Size = new System.Drawing.Size(144, 22);
            this.lblWorkstation.TabIndex = 1;
            this.lblWorkstation.Text = "DESKTOP-BS9GF4D";
            this.lblWorkstation.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTicketInfo
            // 
            this.lblTicketInfo.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblTicketInfo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTicketInfo.ForeColor = System.Drawing.Color.Black;
            this.lblTicketInfo.Location = new System.Drawing.Point(0, 0);
            this.lblTicketInfo.Name = "lblTicketInfo";
            this.lblTicketInfo.Padding = new System.Windows.Forms.Padding(6, 0, 0, 0);
            this.lblTicketInfo.Size = new System.Drawing.Size(137, 22);
            this.lblTicketInfo.TabIndex = 0;
            this.lblTicketInfo.Text = "#10628";
            this.lblTicketInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlCashierRow
            // 
            this.pnlCashierRow.BackColor = System.Drawing.Color.White;
            this.pnlCashierRow.Controls.Add(this.lblDateCreated);
            this.pnlCashierRow.Controls.Add(this.lblCashierInfo);
            this.pnlCashierRow.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCashierRow.Location = new System.Drawing.Point(0, 174);
            this.pnlCashierRow.Name = "pnlCashierRow";
            this.pnlCashierRow.Size = new System.Drawing.Size(281, 22);
            this.pnlCashierRow.TabIndex = 7;
            // 
            // lblDateCreated
            // 
            this.lblDateCreated.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDateCreated.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblDateCreated.ForeColor = System.Drawing.Color.Black;
            this.lblDateCreated.Location = new System.Drawing.Point(137, 0);
            this.lblDateCreated.Name = "lblDateCreated";
            this.lblDateCreated.Padding = new System.Windows.Forms.Padding(0, 0, 6, 0);
            this.lblDateCreated.Size = new System.Drawing.Size(144, 22);
            this.lblDateCreated.TabIndex = 1;
            this.lblDateCreated.Text = "05/30/2026 07:22 PM";
            this.lblDateCreated.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCashierInfo
            // 
            this.lblCashierInfo.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblCashierInfo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblCashierInfo.ForeColor = System.Drawing.Color.Black;
            this.lblCashierInfo.Location = new System.Drawing.Point(0, 0);
            this.lblCashierInfo.Name = "lblCashierInfo";
            this.lblCashierInfo.Padding = new System.Windows.Forms.Padding(6, 0, 0, 0);
            this.lblCashierInfo.Size = new System.Drawing.Size(137, 22);
            this.lblCashierInfo.TabIndex = 0;
            this.lblCashierInfo.Text = "Server: Emily, Carter";
            this.lblCashierInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTableName
            // 
            this.lblTableName.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTableName.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTableName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.lblTableName.Location = new System.Drawing.Point(0, 144);
            this.lblTableName.Name = "lblTableName";
            this.lblTableName.Padding = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.lblTableName.Size = new System.Drawing.Size(281, 30);
            this.lblTableName.TabIndex = 6;
            this.lblTableName.Text = "Table New3";
            this.lblTableName.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblReceiptStatus
            // 
            this.lblReceiptStatus.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblReceiptStatus.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            this.lblReceiptStatus.Location = new System.Drawing.Point(0, 114);
            this.lblReceiptStatus.Name = "lblReceiptStatus";
            this.lblReceiptStatus.Size = new System.Drawing.Size(281, 30);
            this.lblReceiptStatus.TabIndex = 7;
            this.lblReceiptStatus.Text = "Completed";
            this.lblReceiptStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblStoreWebsite
            // 
            this.lblStoreWebsite.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblStoreWebsite.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblStoreWebsite.ForeColor = System.Drawing.Color.Black;
            this.lblStoreWebsite.Location = new System.Drawing.Point(0, 95);
            this.lblStoreWebsite.Name = "lblStoreWebsite";
            this.lblStoreWebsite.Size = new System.Drawing.Size(281, 19);
            this.lblStoreWebsite.TabIndex = 4;
            this.lblStoreWebsite.Text = "www.blogicsystems.com";
            this.lblStoreWebsite.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblStoreEmail
            // 
            this.lblStoreEmail.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblStoreEmail.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblStoreEmail.ForeColor = System.Drawing.Color.Black;
            this.lblStoreEmail.Location = new System.Drawing.Point(0, 76);
            this.lblStoreEmail.Name = "lblStoreEmail";
            this.lblStoreEmail.Size = new System.Drawing.Size(281, 19);
            this.lblStoreEmail.TabIndex = 3;
            this.lblStoreEmail.Text = "truongdongtest@blogiclab.com";
            this.lblStoreEmail.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblStorePhone
            // 
            this.lblStorePhone.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblStorePhone.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblStorePhone.ForeColor = System.Drawing.Color.Black;
            this.lblStorePhone.Location = new System.Drawing.Point(0, 57);
            this.lblStorePhone.Name = "lblStorePhone";
            this.lblStorePhone.Size = new System.Drawing.Size(281, 19);
            this.lblStorePhone.TabIndex = 2;
            this.lblStorePhone.Text = "702-331-1144";
            this.lblStorePhone.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblStoreAddress
            // 
            this.lblStoreAddress.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblStoreAddress.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblStoreAddress.ForeColor = System.Drawing.Color.Black;
            this.lblStoreAddress.Location = new System.Drawing.Point(0, 38);
            this.lblStoreAddress.Name = "lblStoreAddress";
            this.lblStoreAddress.Size = new System.Drawing.Size(281, 19);
            this.lblStoreAddress.TabIndex = 1;
            this.lblStoreAddress.Text = "65442 OR-31, OR, Silver Lake, 97638";
            this.lblStoreAddress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblStoreName
            // 
            this.lblStoreName.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblStoreName.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblStoreName.ForeColor = System.Drawing.Color.Black;
            this.lblStoreName.Location = new System.Drawing.Point(0, 12);
            this.lblStoreName.Name = "lblStoreName";
            this.lblStoreName.Size = new System.Drawing.Size(281, 26);
            this.lblStoreName.TabIndex = 0;
            this.lblStoreName.Text = "BLOGIC_DEV_TEAM_TruongDS";
            this.lblStoreName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnCancelTicket
            // 
            this.btnCancelTicket.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelTicket.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.btnCancelTicket.Location = new System.Drawing.Point(306, 250);
            this.btnCancelTicket.Margin = new System.Windows.Forms.Padding(5);
            this.btnCancelTicket.Name = "btnCancelTicket";
            this.btnCancelTicket.Size = new System.Drawing.Size(200, 50);
            this.btnCancelTicket.TabIndex = 15;
            this.btnCancelTicket.Text = "Cancel Ticket";
            this.btnCancelTicket.UseVisualStyleBackColor = true;
            this.btnCancelTicket.Visible = false;
            // 
            // btnSyncPaymentTime
            // 
            this.btnSyncPaymentTime.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSyncPaymentTime.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.btnSyncPaymentTime.Location = new System.Drawing.Point(306, 550);
            this.btnSyncPaymentTime.Margin = new System.Windows.Forms.Padding(5);
            this.btnSyncPaymentTime.Name = "btnSyncPaymentTime";
            this.btnSyncPaymentTime.Size = new System.Drawing.Size(200, 50);
            this.btnSyncPaymentTime.TabIndex = 16;
            this.btnSyncPaymentTime.Text = "Clear Paid Balance For Sales Report";
            this.btnSyncPaymentTime.UseVisualStyleBackColor = true;
            this.btnSyncPaymentTime.Visible = false;
            // 
            // btnInsertPayment
            // 
            this.btnInsertPayment.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInsertPayment.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.btnInsertPayment.Location = new System.Drawing.Point(516, 190);
            this.btnInsertPayment.Margin = new System.Windows.Forms.Padding(5);
            this.btnInsertPayment.Name = "btnInsertPayment";
            this.btnInsertPayment.Size = new System.Drawing.Size(200, 50);
            this.btnInsertPayment.TabIndex = 17;
            this.btnInsertPayment.Text = "Insert Payment";
            this.btnInsertPayment.UseVisualStyleBackColor = true;
            this.btnInsertPayment.Visible = false;
            //
            // btnAddTip
            //
            this.btnAddTip.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddTip.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.btnAddTip.Location = new System.Drawing.Point(516, 310);
            this.btnAddTip.Margin = new System.Windows.Forms.Padding(5);
            this.btnAddTip.Name = "btnAddTip";
            this.btnAddTip.Size = new System.Drawing.Size(200, 50);
            this.btnAddTip.TabIndex = 18;
            this.btnAddTip.Text = "Add Tip";
            this.btnAddTip.UseVisualStyleBackColor = true;
            this.btnAddTip.Visible = false;
            //
            // ReceiptPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.btnAddTip);
            this.Controls.Add(this.btnInsertPayment);
            this.Controls.Add(this.btnSyncPaymentTime);
            this.Controls.Add(this.btnCancelTicket);
            this.Controls.Add(this.btnRefundItems);
            this.Controls.Add(this.btnVoidTicket);
            this.Controls.Add(this.btnCloseTicket);
            this.Controls.Add(this.btnAddTipByToken);
            this.Controls.Add(this.btnUpdateServiceCharges);
            this.Controls.Add(this.btnFinishTicket);
            this.Controls.Add(this.btnEditItems);
            this.Controls.Add(this.btnMergeRefund);
            this.Controls.Add(this.btnAddItem);
            this.Controls.Add(this.btnAdjustPayment);
            this.Controls.Add(this.pnlReceipt);
            this.Controls.Add(this.pnlToolbar);
            this.Controls.Add(this.pnlHeader);
            this.Name = "ReceiptPage";
            this.Size = new System.Drawing.Size(784, 728);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlToolbar.ResumeLayout(false);
            this.pnlToolbar.PerformLayout();
            this.pnlReceipt.ResumeLayout(false);
            this.pnlReceipt.PerformLayout();
            this.pnlCustomerRow.ResumeLayout(false);
            this.pnlTicketRow.ResumeLayout(false);
            this.pnlCashierRow.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Panel pnlHeaderDivider;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlToolbar;
        private System.Windows.Forms.Panel pnlToolbarDivider;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnAdjustPayment;
        private System.Windows.Forms.Button btnRefundItems;
        private System.Windows.Forms.Button btnVoidTicket;
        private System.Windows.Forms.Button btnCloseTicket;
        private System.Windows.Forms.Button btnAddTipByToken;
        private System.Windows.Forms.Button btnUpdateServiceCharges;
        private System.Windows.Forms.Button btnFinishTicket;
        private System.Windows.Forms.Button btnEditItems;
        private System.Windows.Forms.Button btnMergeRefund;
        private System.Windows.Forms.Button btnAddItem;
        private System.Windows.Forms.Panel pnlReceipt;
        private System.Windows.Forms.Label lblStoreName;
        private System.Windows.Forms.Label lblStoreAddress;
        private System.Windows.Forms.Label lblStorePhone;
        private System.Windows.Forms.Label lblStoreEmail;
        private System.Windows.Forms.Label lblStoreWebsite;
        private System.Windows.Forms.Label lblTableName;
        private System.Windows.Forms.Label lblReceiptStatus;
        private System.Windows.Forms.Panel pnlCashierRow;
        private System.Windows.Forms.Label lblCashierInfo;
        private System.Windows.Forms.Label lblDateCreated;
        private System.Windows.Forms.Panel pnlTicketRow;
        private System.Windows.Forms.Label lblTicketInfo;
        private System.Windows.Forms.Label lblWorkstation;
        private System.Windows.Forms.Panel pnlCustomerRow;
        private System.Windows.Forms.Label lblCustomerName;
        private System.Windows.Forms.Label lblCustomerPhone;
        private System.Windows.Forms.Panel pnlItemsList;
        private System.Windows.Forms.Panel pnlTotals;
        private System.Windows.Forms.Button btnCancelTicket;
        private System.Windows.Forms.Button btnSyncPaymentTime;
        private System.Windows.Forms.Button btnInsertPayment;
        private System.Windows.Forms.Button btnAddTip;
    }
}
