namespace BLogicPOS7SupportTool.Dashboard.Pages
{
    partial class ItemsPage
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
            System.Windows.Forms.DataGridViewTextBoxColumn colItemName;
            System.Windows.Forms.DataGridViewTextBoxColumn colItemGroupName;
            System.Windows.Forms.DataGridViewTextBoxColumn colItemTypeName;
            System.Windows.Forms.DataGridViewTextBoxColumn colLookupCode;
            System.Windows.Forms.DataGridViewTextBoxColumn colPrice;
            System.Windows.Forms.DataGridViewButtonColumn colSyncButton;
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlHeaderDivider = new System.Windows.Forms.Panel();
            this.pnlToolbar = new System.Windows.Forms.Panel();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblSearch = new System.Windows.Forms.Label();
            this.pnlToolbarDivider = new System.Windows.Forms.Panel();
            this.pnlBody = new System.Windows.Forms.Panel();
            this.pnlItemsArea = new System.Windows.Forms.Panel();
            this.dgvItems = new System.Windows.Forms.DataGridView();
            this.pnlLoading = new System.Windows.Forms.Panel();
            this.lblLoading = new System.Windows.Forms.Label();
            this.pbLoading = new System.Windows.Forms.ProgressBar();
            this.pnlDeptDivider = new System.Windows.Forms.Panel();
            this.pnlDeptList = new System.Windows.Forms.Panel();
            this.lstDepartments = new System.Windows.Forms.ListBox();
            this.pnlLoadingDepts = new System.Windows.Forms.Panel();
            this.lblLoadingDepts = new System.Windows.Forms.Label();
            colItemName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colItemGroupName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colItemTypeName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colLookupCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colSyncButton = new System.Windows.Forms.DataGridViewButtonColumn();
            this.pnlHeader.SuspendLayout();
            this.pnlToolbar.SuspendLayout();
            this.pnlBody.SuspendLayout();
            this.pnlItemsArea.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).BeginInit();
            this.pnlLoading.SuspendLayout();
            this.pnlDeptList.SuspendLayout();
            this.pnlLoadingDepts.SuspendLayout();
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
            this.lblTitle.Size = new System.Drawing.Size(72, 32);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "Items";
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
            // pnlToolbar
            // 
            this.pnlToolbar.BackColor = System.Drawing.Color.White;
            this.pnlToolbar.Controls.Add(this.txtSearch);
            this.pnlToolbar.Controls.Add(this.lblSearch);
            this.pnlToolbar.Controls.Add(this.pnlToolbarDivider);
            this.pnlToolbar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolbar.Location = new System.Drawing.Point(0, 64);
            this.pnlToolbar.Name = "pnlToolbar";
            this.pnlToolbar.Size = new System.Drawing.Size(784, 56);
            this.pnlToolbar.TabIndex = 1;
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
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
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
            // pnlBody
            // 
            this.pnlBody.BackColor = System.Drawing.Color.White;
            this.pnlBody.Controls.Add(this.pnlItemsArea);
            this.pnlBody.Controls.Add(this.pnlDeptDivider);
            this.pnlBody.Controls.Add(this.pnlDeptList);
            this.pnlBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBody.Location = new System.Drawing.Point(0, 120);
            this.pnlBody.Name = "pnlBody";
            this.pnlBody.Size = new System.Drawing.Size(784, 608);
            this.pnlBody.TabIndex = 2;
            // 
            // pnlItemsArea
            // 
            this.pnlItemsArea.BackColor = System.Drawing.Color.White;
            this.pnlItemsArea.Controls.Add(this.dgvItems);
            this.pnlItemsArea.Controls.Add(this.pnlLoading);
            this.pnlItemsArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlItemsArea.Location = new System.Drawing.Point(151, 0);
            this.pnlItemsArea.Name = "pnlItemsArea";
            this.pnlItemsArea.Size = new System.Drawing.Size(633, 608);
            this.pnlItemsArea.TabIndex = 2;
            // 
            // dgvItems
            // 
            this.dgvItems.AllowUserToAddRows = false;
            this.dgvItems.AllowUserToDeleteRows = false;
            this.dgvItems.AutoGenerateColumns = false;
            this.dgvItems.BackgroundColor = System.Drawing.Color.White;
            this.dgvItems.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvItems.ColumnHeadersHeight = 40;
            this.dgvItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvItems.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                colItemName,
                colItemGroupName,
                colItemTypeName,
                colLookupCode,
                colPrice,
                colSyncButton});
            this.dgvItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvItems.Location = new System.Drawing.Point(0, 0);
            this.dgvItems.Name = "dgvItems";
            this.dgvItems.ReadOnly = true;
            this.dgvItems.RowHeadersVisible = false;
            this.dgvItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvItems.Size = new System.Drawing.Size(633, 608);
            this.dgvItems.TabIndex = 0;
            this.dgvItems.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvItems_CellContentClick);
            //
            // colItemName
            //
            colItemName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            colItemName.DataPropertyName = "ItemName";
            colItemName.HeaderText = "Item Name";
            colItemName.MinimumWidth = 10;
            colItemName.Name = "colItemName";
            colItemName.ReadOnly = true;
            //
            // colItemGroupName
            //
            colItemGroupName.DataPropertyName = "ItemGroupName";
            colItemGroupName.HeaderText = "Item Group";
            colItemGroupName.Name = "colItemGroupName";
            colItemGroupName.ReadOnly = true;
            colItemGroupName.Width = 140;
            //
            // colItemTypeName
            //
            colItemTypeName.DataPropertyName = "ItemTypeName";
            colItemTypeName.HeaderText = "Item Type";
            colItemTypeName.Name = "colItemTypeName";
            colItemTypeName.ReadOnly = true;
            colItemTypeName.Visible = false;
            colItemTypeName.Width = 120;
            //
            // colLookupCode
            //
            colLookupCode.DataPropertyName = "LookupCode";
            colLookupCode.HeaderText = "Barcode / PLU";
            colLookupCode.Name = "colLookupCode";
            colLookupCode.ReadOnly = true;
            colLookupCode.Width = 140;
            //
            // colPrice
            //
            System.Windows.Forms.DataGridViewCellStyle colPriceStyle = new System.Windows.Forms.DataGridViewCellStyle();
            colPriceStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            colPriceStyle.Format = "N2";
            colPrice.DataPropertyName = "RegularPrice";
            colPrice.DefaultCellStyle = colPriceStyle;
            colPrice.HeaderText = "Price";
            colPrice.Name = "colPrice";
            colPrice.ReadOnly = true;
            colPrice.Width = 100;
            //
            // colSyncButton
            //
            colSyncButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            colSyncButton.HeaderText = "";
            colSyncButton.Name = "colSyncButton";
            colSyncButton.Text = "Sync Item Group";
            colSyncButton.UseColumnTextForButtonValue = true;
            colSyncButton.Width = 140;
            //
            // pnlLoading
            // 
            this.pnlLoading.BackColor = System.Drawing.Color.White;
            this.pnlLoading.Controls.Add(this.lblLoading);
            this.pnlLoading.Controls.Add(this.pbLoading);
            this.pnlLoading.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLoading.Location = new System.Drawing.Point(0, 0);
            this.pnlLoading.Name = "pnlLoading";
            this.pnlLoading.Size = new System.Drawing.Size(633, 608);
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
            this.lblLoading.Size = new System.Drawing.Size(633, 604);
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
            this.pbLoading.Size = new System.Drawing.Size(633, 4);
            this.pbLoading.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.pbLoading.TabIndex = 0;
            // 
            // pnlDeptDivider
            // 
            this.pnlDeptDivider.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            this.pnlDeptDivider.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlDeptDivider.Location = new System.Drawing.Point(150, 0);
            this.pnlDeptDivider.Name = "pnlDeptDivider";
            this.pnlDeptDivider.Size = new System.Drawing.Size(1, 608);
            this.pnlDeptDivider.TabIndex = 1;
            // 
            // pnlDeptList
            // 
            this.pnlDeptList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.pnlDeptList.Controls.Add(this.lstDepartments);
            this.pnlDeptList.Controls.Add(this.pnlLoadingDepts);
            this.pnlDeptList.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlDeptList.Location = new System.Drawing.Point(0, 0);
            this.pnlDeptList.Name = "pnlDeptList";
            this.pnlDeptList.Size = new System.Drawing.Size(150, 608);
            this.pnlDeptList.TabIndex = 0;
            // 
            // lstDepartments
            // 
            this.lstDepartments.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.lstDepartments.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstDepartments.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstDepartments.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lstDepartments.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lstDepartments.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.lstDepartments.ItemHeight = 40;
            this.lstDepartments.Location = new System.Drawing.Point(0, 0);
            this.lstDepartments.Name = "lstDepartments";
            this.lstDepartments.Size = new System.Drawing.Size(150, 608);
            this.lstDepartments.TabIndex = 0;
            this.lstDepartments.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.lstDepartments_DrawItem);
            this.lstDepartments.SelectedIndexChanged += new System.EventHandler(this.lstDepartments_SelectedIndexChanged);
            // 
            // pnlLoadingDepts
            // 
            this.pnlLoadingDepts.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.pnlLoadingDepts.Controls.Add(this.lblLoadingDepts);
            this.pnlLoadingDepts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLoadingDepts.Location = new System.Drawing.Point(0, 0);
            this.pnlLoadingDepts.Name = "pnlLoadingDepts";
            this.pnlLoadingDepts.Size = new System.Drawing.Size(150, 608);
            this.pnlLoadingDepts.TabIndex = 1;
            this.pnlLoadingDepts.Visible = false;
            // 
            // lblLoadingDepts
            // 
            this.lblLoadingDepts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLoadingDepts.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblLoadingDepts.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.lblLoadingDepts.Location = new System.Drawing.Point(0, 0);
            this.lblLoadingDepts.Name = "lblLoadingDepts";
            this.lblLoadingDepts.Size = new System.Drawing.Size(150, 608);
            this.lblLoadingDepts.TabIndex = 0;
            this.lblLoadingDepts.Text = "Loading...";
            this.lblLoadingDepts.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ItemsPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.pnlBody);
            this.Controls.Add(this.pnlToolbar);
            this.Controls.Add(this.pnlHeader);
            this.Name = "ItemsPage";
            this.Size = new System.Drawing.Size(784, 728);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlToolbar.ResumeLayout(false);
            this.pnlToolbar.PerformLayout();
            this.pnlBody.ResumeLayout(false);
            this.pnlItemsArea.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).EndInit();
            this.pnlLoading.ResumeLayout(false);
            this.pnlDeptList.ResumeLayout(false);
            this.pnlLoadingDepts.ResumeLayout(false);
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
        private System.Windows.Forms.Panel pnlBody;
        private System.Windows.Forms.Panel pnlDeptList;
        private System.Windows.Forms.ListBox lstDepartments;
        private System.Windows.Forms.Panel pnlLoadingDepts;
        private System.Windows.Forms.Label lblLoadingDepts;
        private System.Windows.Forms.Panel pnlDeptDivider;
        private System.Windows.Forms.Panel pnlItemsArea;
        private System.Windows.Forms.DataGridView dgvItems;
        private System.Windows.Forms.Panel pnlLoading;
        private System.Windows.Forms.ProgressBar pbLoading;
        private System.Windows.Forms.Label lblLoading;
    }
}
