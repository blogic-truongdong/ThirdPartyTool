namespace BLogicPOS7SupportTool.Dashboard.Pages
{
    partial class ClearTableStatusPage
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
            System.Windows.Forms.DataGridViewTextBoxColumn colTableName;
            System.Windows.Forms.DataGridViewTextBoxColumn colStatus;
            System.Windows.Forms.DataGridViewButtonColumn colClearButton;
            System.Windows.Forms.DataGridViewCellStyle colClearButtonStyle = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlHeaderDivider = new System.Windows.Forms.Panel();
            this.pnlToolbar = new System.Windows.Forms.Panel();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblSearch = new System.Windows.Forms.Label();
            this.pnlToolbarDivider = new System.Windows.Forms.Panel();
            this.pnlBody = new System.Windows.Forms.Panel();
            this.pnlTablesArea = new System.Windows.Forms.Panel();
            this.dgvTables = new System.Windows.Forms.DataGridView();
            this.pnlLoading = new System.Windows.Forms.Panel();
            this.lblLoading = new System.Windows.Forms.Label();
            this.pbLoading = new System.Windows.Forms.ProgressBar();
            this.pnlAreaDivider = new System.Windows.Forms.Panel();
            this.pnlAreaList = new System.Windows.Forms.Panel();
            this.lstAreas = new System.Windows.Forms.ListBox();
            this.pnlLoadingAreas = new System.Windows.Forms.Panel();
            this.lblLoadingAreas = new System.Windows.Forms.Label();
            colTableName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colClearButton = new System.Windows.Forms.DataGridViewButtonColumn();
            this.pnlHeader.SuspendLayout();
            this.pnlToolbar.SuspendLayout();
            this.pnlBody.SuspendLayout();
            this.pnlTablesArea.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTables)).BeginInit();
            this.pnlLoading.SuspendLayout();
            this.pnlAreaList.SuspendLayout();
            this.pnlLoadingAreas.SuspendLayout();
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
            this.lblTitle.Size = new System.Drawing.Size(224, 32);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "Clear Table Status";
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
            this.txtSearch.Text = "";
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
            this.pnlBody.Controls.Add(this.pnlTablesArea);
            this.pnlBody.Controls.Add(this.pnlAreaDivider);
            this.pnlBody.Controls.Add(this.pnlAreaList);
            this.pnlBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBody.Location = new System.Drawing.Point(0, 120);
            this.pnlBody.Name = "pnlBody";
            this.pnlBody.Size = new System.Drawing.Size(784, 608);
            this.pnlBody.TabIndex = 2;
            //
            // pnlTablesArea
            //
            this.pnlTablesArea.BackColor = System.Drawing.Color.White;
            this.pnlTablesArea.Controls.Add(this.dgvTables);
            this.pnlTablesArea.Controls.Add(this.pnlLoading);
            this.pnlTablesArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTablesArea.Location = new System.Drawing.Point(151, 0);
            this.pnlTablesArea.Name = "pnlTablesArea";
            this.pnlTablesArea.Size = new System.Drawing.Size(633, 608);
            this.pnlTablesArea.TabIndex = 2;
            //
            // dgvTables
            //
            this.dgvTables.AllowUserToAddRows = false;
            this.dgvTables.AllowUserToDeleteRows = false;
            this.dgvTables.AutoGenerateColumns = false;
            this.dgvTables.BackgroundColor = System.Drawing.Color.White;
            this.dgvTables.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvTables.ColumnHeadersHeight = 40;
            this.dgvTables.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvTables.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                colTableName,
                colStatus,
                colClearButton});
            this.dgvTables.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTables.Location = new System.Drawing.Point(0, 0);
            this.dgvTables.Name = "dgvTables";
            this.dgvTables.ReadOnly = true;
            this.dgvTables.RowHeadersVisible = false;
            this.dgvTables.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTables.Size = new System.Drawing.Size(633, 608);
            this.dgvTables.TabIndex = 0;
            this.dgvTables.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTables_CellContentClick);
            //
            // colTableName
            //
            colTableName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            colTableName.DataPropertyName = "TableName";
            colTableName.HeaderText = "Table Name";
            colTableName.MinimumWidth = 10;
            colTableName.Name = "colTableName";
            colTableName.ReadOnly = true;
            //
            // colStatus
            //
            colStatus.DataPropertyName = "StatusName";
            colStatus.HeaderText = "Status";
            colStatus.Name = "colStatus";
            colStatus.ReadOnly = true;
            colStatus.Width = 160;
            //
            // colClearButton
            //
            colClearButtonStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            colClearButtonStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            colClearButton.DefaultCellStyle = colClearButtonStyle;
            colClearButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            colClearButton.HeaderText = "";
            colClearButton.Name = "colClearButton";
            colClearButton.ReadOnly = true;
            colClearButton.Text = "Clear";
            colClearButton.UseColumnTextForButtonValue = true;
            colClearButton.Width = 120;
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
            // pnlAreaDivider
            //
            this.pnlAreaDivider.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            this.pnlAreaDivider.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlAreaDivider.Location = new System.Drawing.Point(150, 0);
            this.pnlAreaDivider.Name = "pnlAreaDivider";
            this.pnlAreaDivider.Size = new System.Drawing.Size(1, 608);
            this.pnlAreaDivider.TabIndex = 1;
            //
            // pnlAreaList
            //
            this.pnlAreaList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.pnlAreaList.Controls.Add(this.lstAreas);
            this.pnlAreaList.Controls.Add(this.pnlLoadingAreas);
            this.pnlAreaList.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlAreaList.Location = new System.Drawing.Point(0, 0);
            this.pnlAreaList.Name = "pnlAreaList";
            this.pnlAreaList.Size = new System.Drawing.Size(150, 608);
            this.pnlAreaList.TabIndex = 0;
            //
            // lstAreas
            //
            this.lstAreas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.lstAreas.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstAreas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstAreas.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lstAreas.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lstAreas.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.lstAreas.ItemHeight = 40;
            this.lstAreas.Location = new System.Drawing.Point(0, 0);
            this.lstAreas.Name = "lstAreas";
            this.lstAreas.Size = new System.Drawing.Size(150, 608);
            this.lstAreas.TabIndex = 0;
            this.lstAreas.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.lstAreas_DrawItem);
            this.lstAreas.SelectedIndexChanged += new System.EventHandler(this.lstAreas_SelectedIndexChanged);
            //
            // pnlLoadingAreas
            //
            this.pnlLoadingAreas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.pnlLoadingAreas.Controls.Add(this.lblLoadingAreas);
            this.pnlLoadingAreas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLoadingAreas.Location = new System.Drawing.Point(0, 0);
            this.pnlLoadingAreas.Name = "pnlLoadingAreas";
            this.pnlLoadingAreas.Size = new System.Drawing.Size(150, 608);
            this.pnlLoadingAreas.TabIndex = 1;
            this.pnlLoadingAreas.Visible = false;
            //
            // lblLoadingAreas
            //
            this.lblLoadingAreas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLoadingAreas.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblLoadingAreas.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.lblLoadingAreas.Location = new System.Drawing.Point(0, 0);
            this.lblLoadingAreas.Name = "lblLoadingAreas";
            this.lblLoadingAreas.Size = new System.Drawing.Size(150, 608);
            this.lblLoadingAreas.TabIndex = 0;
            this.lblLoadingAreas.Text = "Loading...";
            this.lblLoadingAreas.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // ClearTableStatusPage
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.pnlBody);
            this.Controls.Add(this.pnlToolbar);
            this.Controls.Add(this.pnlHeader);
            this.Name = "ClearTableStatusPage";
            this.Size = new System.Drawing.Size(784, 728);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlToolbar.ResumeLayout(false);
            this.pnlToolbar.PerformLayout();
            this.pnlBody.ResumeLayout(false);
            this.pnlTablesArea.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTables)).EndInit();
            this.pnlLoading.ResumeLayout(false);
            this.pnlAreaList.ResumeLayout(false);
            this.pnlLoadingAreas.ResumeLayout(false);
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
        private System.Windows.Forms.Panel pnlAreaList;
        private System.Windows.Forms.ListBox lstAreas;
        private System.Windows.Forms.Panel pnlLoadingAreas;
        private System.Windows.Forms.Label lblLoadingAreas;
        private System.Windows.Forms.Panel pnlAreaDivider;
        private System.Windows.Forms.Panel pnlTablesArea;
        private System.Windows.Forms.DataGridView dgvTables;
        private System.Windows.Forms.Panel pnlLoading;
        private System.Windows.Forms.ProgressBar pbLoading;
        private System.Windows.Forms.Label lblLoading;
    }
}
