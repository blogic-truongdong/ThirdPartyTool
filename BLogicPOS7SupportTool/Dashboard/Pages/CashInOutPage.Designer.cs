namespace BLogicPOS7SupportTool.Dashboard.Pages
{
    partial class CashInOutPage
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
            System.Windows.Forms.DataGridViewTextBoxColumn colTypeRaw;
            System.Windows.Forms.DataGridViewTextBoxColumn colTypeName;
            System.Windows.Forms.DataGridViewTextBoxColumn colEmployee;
            System.Windows.Forms.DataGridViewTextBoxColumn colAmount;
            System.Windows.Forms.DataGridViewTextBoxColumn colComment;
            System.Windows.Forms.DataGridViewButtonColumn colEditButton;
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlHeaderDivider = new System.Windows.Forms.Panel();
            this.pnlToolbar = new System.Windows.Forms.Panel();
            this.lblCount = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.dtpEnd = new System.Windows.Forms.DateTimePicker();
            this.lblTo = new System.Windows.Forms.Label();
            this.dtpStart = new System.Windows.Forms.DateTimePicker();
            this.lblFrom = new System.Windows.Forms.Label();
            this.pnlToolbarDivider = new System.Windows.Forms.Panel();
            this.pnlBody = new System.Windows.Forms.Panel();
            this.dgvCashInOut = new System.Windows.Forms.DataGridView();
            this.pnlLoading = new System.Windows.Forms.Panel();
            this.lblLoading = new System.Windows.Forms.Label();
            this.pbLoading = new System.Windows.Forms.ProgressBar();
            colTypeRaw = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colTypeName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colEmployee = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colComment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colEditButton = new System.Windows.Forms.DataGridViewButtonColumn();
            this.pnlHeader.SuspendLayout();
            this.pnlToolbar.SuspendLayout();
            this.pnlBody.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCashInOut)).BeginInit();
            this.pnlLoading.SuspendLayout();
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
            this.lblTitle.Size = new System.Drawing.Size(148, 32);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "Cash In / Out";
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
            this.pnlToolbar.Controls.Add(this.lblCount);
            this.pnlToolbar.Controls.Add(this.btnSearch);
            this.pnlToolbar.Controls.Add(this.dtpEnd);
            this.pnlToolbar.Controls.Add(this.lblTo);
            this.pnlToolbar.Controls.Add(this.dtpStart);
            this.pnlToolbar.Controls.Add(this.lblFrom);
            this.pnlToolbar.Controls.Add(this.pnlToolbarDivider);
            this.pnlToolbar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolbar.Location = new System.Drawing.Point(0, 64);
            this.pnlToolbar.Name = "pnlToolbar";
            this.pnlToolbar.Size = new System.Drawing.Size(784, 56);
            this.pnlToolbar.TabIndex = 1;
            //
            // lblCount
            //
            this.lblCount.AutoSize = true;
            this.lblCount.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblCount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.lblCount.Location = new System.Drawing.Point(512, 18);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(0, 19);
            this.lblCount.TabIndex = 6;
            this.lblCount.Text = "";
            //
            // btnSearch
            //
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(87)))), ((int)(((byte)(208)))));
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(400, 12);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(100, 32);
            this.btnSearch.TabIndex = 5;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = false;
            //
            // dtpEnd
            //
            this.dtpEnd.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.dtpEnd.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEnd.Location = new System.Drawing.Point(256, 14);
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.Size = new System.Drawing.Size(130, 27);
            this.dtpEnd.TabIndex = 4;
            //
            // lblTo
            //
            this.lblTo.AutoSize = true;
            this.lblTo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.lblTo.Location = new System.Drawing.Point(226, 18);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(25, 19);
            this.lblTo.TabIndex = 3;
            this.lblTo.Text = "To:";
            //
            // dtpStart
            //
            this.dtpStart.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.dtpStart.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpStart.Location = new System.Drawing.Point(74, 14);
            this.dtpStart.Name = "dtpStart";
            this.dtpStart.Size = new System.Drawing.Size(130, 27);
            this.dtpStart.TabIndex = 2;
            //
            // lblFrom
            //
            this.lblFrom.AutoSize = true;
            this.lblFrom.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblFrom.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.lblFrom.Location = new System.Drawing.Point(16, 18);
            this.lblFrom.Name = "lblFrom";
            this.lblFrom.Size = new System.Drawing.Size(43, 19);
            this.lblFrom.TabIndex = 1;
            this.lblFrom.Text = "From:";
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
            this.pnlBody.Controls.Add(this.dgvCashInOut);
            this.pnlBody.Controls.Add(this.pnlLoading);
            this.pnlBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBody.Location = new System.Drawing.Point(0, 120);
            this.pnlBody.Name = "pnlBody";
            this.pnlBody.Size = new System.Drawing.Size(784, 608);
            this.pnlBody.TabIndex = 2;
            //
            // dgvCashInOut
            //
            this.dgvCashInOut.AllowUserToAddRows = false;
            this.dgvCashInOut.AllowUserToDeleteRows = false;
            this.dgvCashInOut.AutoGenerateColumns = false;
            this.dgvCashInOut.BackgroundColor = System.Drawing.Color.White;
            this.dgvCashInOut.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvCashInOut.ColumnHeadersHeight = 40;
            this.dgvCashInOut.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvCashInOut.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                colTypeRaw,
                colTypeName,
                colEmployee,
                colAmount,
                colComment,
                colEditButton});
            this.dgvCashInOut.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCashInOut.Location = new System.Drawing.Point(0, 0);
            this.dgvCashInOut.Name = "dgvCashInOut";
            this.dgvCashInOut.ReadOnly = true;
            this.dgvCashInOut.RowHeadersVisible = false;
            this.dgvCashInOut.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCashInOut.Size = new System.Drawing.Size(784, 608);
            this.dgvCashInOut.TabIndex = 0;
            //
            // colTypeRaw
            //
            colTypeRaw.DataPropertyName = "Type";
            colTypeRaw.HeaderText = "TypeRaw";
            colTypeRaw.Name = "colTypeRaw";
            colTypeRaw.ReadOnly = true;
            colTypeRaw.Visible = false;
            //
            // colTypeName
            //
            colTypeName.DataPropertyName = "TypeName";
            colTypeName.HeaderText = "Type";
            colTypeName.Name = "colTypeName";
            colTypeName.ReadOnly = true;
            colTypeName.Width = 110;
            //
            // colEmployee
            //
            colEmployee.DataPropertyName = "Employee";
            colEmployee.HeaderText = "Employee";
            colEmployee.MinimumWidth = 120;
            colEmployee.Name = "colEmployee";
            colEmployee.ReadOnly = true;
            colEmployee.Width = 200;
            //
            // colAmount
            //
            System.Windows.Forms.DataGridViewCellStyle colAmountStyle = new System.Windows.Forms.DataGridViewCellStyle();
            colAmountStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            colAmountStyle.Format = "N2";
            colAmount.DataPropertyName = "Amount";
            colAmount.DefaultCellStyle = colAmountStyle;
            colAmount.HeaderText = "Amount";
            colAmount.Name = "colAmount";
            colAmount.ReadOnly = true;
            colAmount.Width = 120;
            //
            // colComment
            //
            colComment.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            colComment.DataPropertyName = "Comment";
            colComment.HeaderText = "Comment";
            colComment.MinimumWidth = 120;
            colComment.Name = "colComment";
            colComment.ReadOnly = true;
            //
            // colEditButton
            //
            colEditButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            colEditButton.HeaderText = "";
            colEditButton.Name = "colEditButton";
            colEditButton.ReadOnly = true;
            colEditButton.Text = "Edit";
            colEditButton.UseColumnTextForButtonValue = true;
            colEditButton.Width = 90;
            //
            // pnlLoading
            //
            this.pnlLoading.BackColor = System.Drawing.Color.White;
            this.pnlLoading.Controls.Add(this.lblLoading);
            this.pnlLoading.Controls.Add(this.pbLoading);
            this.pnlLoading.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLoading.Location = new System.Drawing.Point(0, 0);
            this.pnlLoading.Name = "pnlLoading";
            this.pnlLoading.Size = new System.Drawing.Size(784, 608);
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
            this.lblLoading.Size = new System.Drawing.Size(784, 604);
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
            this.pbLoading.Size = new System.Drawing.Size(784, 4);
            this.pbLoading.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.pbLoading.TabIndex = 0;
            //
            // CashInOutPage
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.pnlBody);
            this.Controls.Add(this.pnlToolbar);
            this.Controls.Add(this.pnlHeader);
            this.Name = "CashInOutPage";
            this.Size = new System.Drawing.Size(784, 728);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlToolbar.ResumeLayout(false);
            this.pnlToolbar.PerformLayout();
            this.pnlBody.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCashInOut)).EndInit();
            this.pnlLoading.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlHeaderDivider;
        private System.Windows.Forms.Panel pnlToolbar;
        private System.Windows.Forms.Label lblFrom;
        private System.Windows.Forms.DateTimePicker dtpStart;
        private System.Windows.Forms.Label lblTo;
        private System.Windows.Forms.DateTimePicker dtpEnd;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.Panel pnlToolbarDivider;
        private System.Windows.Forms.Panel pnlBody;
        private System.Windows.Forms.DataGridView dgvCashInOut;
        private System.Windows.Forms.Panel pnlLoading;
        private System.Windows.Forms.ProgressBar pbLoading;
        private System.Windows.Forms.Label lblLoading;
    }
}
