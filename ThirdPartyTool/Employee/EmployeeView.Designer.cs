namespace ThirdPartyTool.Employee
{
    partial class EmployeeView
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle headerStyle = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle rowStyle = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle roleStyle = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.lblCount = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Label();
            this.pnlHeaderDivider = new System.Windows.Forms.Panel();
            this.pnlList = new System.Windows.Forms.Panel();
            this.dgvEmployees = new System.Windows.Forms.DataGridView();
            this.EmployeeID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EmployeeCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EmployeeName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RoleName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Delete = new System.Windows.Forms.DataGridViewImageColumn();
            this.pnlSearchGap = new System.Windows.Forms.Panel();
            this.pnlSearch = new ThirdPartyTool.CustomControl.BPanel_Rounded();
            this.txtSearch = new ThirdPartyTool.CustomControl.BTextBox();
            this.btnSearch = new System.Windows.Forms.Label();
            this.txtFocus = new ThirdPartyTool.CustomControl.BTextBox();
            this.pnlSyncDivider = new System.Windows.Forms.Panel();
            this.pnlSync = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.lblSyncHint = new System.Windows.Forms.Label();
            this.pnlEmployeeCard = new ThirdPartyTool.CustomControl.BPanel_Rounded();
            this.lblEmployeeCaption = new System.Windows.Forms.Label();
            this.lblCurrentEmployeeName = new System.Windows.Forms.Label();
            this.lblCurrentEmployeeMeta = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.bPanel_Rounded2 = new ThirdPartyTool.CustomControl.BPanel_Rounded();
            this.lblSyncRoles = new System.Windows.Forms.Label();
            this.cbbSyncRoles = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.bPanel_Rounded4 = new ThirdPartyTool.CustomControl.BPanel_Rounded();
            this.pnlStartDateClip = new System.Windows.Forms.Panel();
            this.startDate = new System.Windows.Forms.DateTimePicker();
            this.bPanel_Rounded5 = new ThirdPartyTool.CustomControl.BPanel_Rounded();
            this.pnlEndDateClip = new System.Windows.Forms.Panel();
            this.endDate = new System.Windows.Forms.DateTimePicker();
            this.btnSyncRoles = new ThirdPartyTool.CustomControl.BLabel_Rounded();
            this.lblStartDate = new System.Windows.Forms.Label();
            this.lblEndDate = new System.Windows.Forms.Label();
            this.pnlHeader.SuspendLayout();
            this.pnlList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmployees)).BeginInit();
            this.pnlSearch.SuspendLayout();
            this.pnlSync.SuspendLayout();
            this.pnlEmployeeCard.SuspendLayout();
            this.bPanel_Rounded2.SuspendLayout();
            this.bPanel_Rounded4.SuspendLayout();
            this.pnlStartDateClip.SuspendLayout();
            this.bPanel_Rounded5.SuspendLayout();
            this.pnlEndDateClip.SuspendLayout();
            this.SuspendLayout();
            //
            // pnlHeader
            //
            this.pnlHeader.BackColor = System.Drawing.Color.White;
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Controls.Add(this.lblSubtitle);
            this.pnlHeader.Controls.Add(this.lblCount);
            this.pnlHeader.Controls.Add(this.btnRefresh);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(837, 64);
            this.pnlHeader.TabIndex = 0;
            //
            // lblTitle
            //
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.lblTitle.Location = new System.Drawing.Point(14, 12);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(115, 26);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Employees";
            //
            // lblSubtitle
            //
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new System.Drawing.Font("Roboto", 9.75F);
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(117)))), ((int)(((byte)(117)))));
            this.lblSubtitle.Location = new System.Drawing.Point(17, 42);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(330, 16);
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "Select an employee, then sync a job role for a date range.";
            //
            // lblCount
            //
            this.lblCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCount.Font = new System.Drawing.Font("Roboto", 10F);
            this.lblCount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(117)))), ((int)(((byte)(117)))));
            this.lblCount.Location = new System.Drawing.Point(527, 21);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(240, 22);
            this.lblCount.TabIndex = 2;
            this.lblCount.Text = "";
            this.lblCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            //
            // btnRefresh
            //
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefresh.Image = global::ThirdPartyTool.Properties.Resources.Icon_Refresh;
            this.btnRefresh.Location = new System.Drawing.Point(779, 14);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(40, 36);
            this.btnRefresh.TabIndex = 3;
            this.btnRefresh.Text = "";
            this.toolTip.SetToolTip(this.btnRefresh, "Reload employees");
            //
            // pnlHeaderDivider
            //
            this.pnlHeaderDivider.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.pnlHeaderDivider.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeaderDivider.Location = new System.Drawing.Point(0, 64);
            this.pnlHeaderDivider.Name = "pnlHeaderDivider";
            this.pnlHeaderDivider.Size = new System.Drawing.Size(837, 1);
            this.pnlHeaderDivider.TabIndex = 1;
            //
            // pnlList
            //
            this.pnlList.BackColor = System.Drawing.Color.White;
            this.pnlList.Controls.Add(this.dgvEmployees);
            this.pnlList.Controls.Add(this.pnlSearchGap);
            this.pnlList.Controls.Add(this.pnlSearch);
            this.pnlList.Controls.Add(this.txtFocus);
            this.pnlList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlList.Location = new System.Drawing.Point(0, 65);
            this.pnlList.Name = "pnlList";
            this.pnlList.Padding = new System.Windows.Forms.Padding(16, 12, 16, 16);
            this.pnlList.Size = new System.Drawing.Size(516, 663);
            this.pnlList.TabIndex = 2;
            //
            // dgvEmployees
            //
            this.dgvEmployees.AllowUserToAddRows = false;
            this.dgvEmployees.AllowUserToDeleteRows = false;
            this.dgvEmployees.AllowUserToResizeColumns = false;
            this.dgvEmployees.AllowUserToResizeRows = false;
            this.dgvEmployees.AutoGenerateColumns = false;
            this.dgvEmployees.BackgroundColor = System.Drawing.Color.White;
            this.dgvEmployees.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvEmployees.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvEmployees.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            headerStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            headerStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            headerStyle.Font = new System.Drawing.Font("Roboto", 10F, System.Drawing.FontStyle.Bold);
            headerStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(97)))), ((int)(((byte)(97)))), ((int)(((byte)(97)))));
            headerStyle.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            headerStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            headerStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(97)))), ((int)(((byte)(97)))), ((int)(((byte)(97)))));
            headerStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvEmployees.ColumnHeadersDefaultCellStyle = headerStyle;
            this.dgvEmployees.ColumnHeadersHeight = 40;
            this.dgvEmployees.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvEmployees.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.EmployeeID,
            this.EmployeeCode,
            this.EmployeeName,
            this.RoleName,
            this.Delete});
            this.dgvEmployees.Cursor = System.Windows.Forms.Cursors.Default;
            rowStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            rowStyle.BackColor = System.Drawing.Color.White;
            rowStyle.Font = new System.Drawing.Font("Roboto", 10.5F);
            rowStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            rowStyle.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            rowStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(240)))), ((int)(((byte)(254)))));
            rowStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            rowStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvEmployees.DefaultCellStyle = rowStyle;
            this.dgvEmployees.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvEmployees.EnableHeadersVisualStyles = false;
            this.dgvEmployees.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(235)))), ((int)(((byte)(235)))));
            this.dgvEmployees.Location = new System.Drawing.Point(16, 64);
            this.dgvEmployees.MultiSelect = false;
            this.dgvEmployees.Name = "dgvEmployees";
            this.dgvEmployees.ReadOnly = true;
            this.dgvEmployees.RowHeadersVisible = false;
            this.dgvEmployees.RowTemplate.Height = 40;
            this.dgvEmployees.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvEmployees.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvEmployees.Size = new System.Drawing.Size(484, 583);
            this.dgvEmployees.TabIndex = 2;
            //
            // EmployeeID
            //
            this.EmployeeID.DataPropertyName = "EmployeeID";
            this.EmployeeID.HeaderText = "EmployeeID";
            this.EmployeeID.Name = "EmployeeID";
            this.EmployeeID.ReadOnly = true;
            this.EmployeeID.Visible = false;
            //
            // EmployeeCode
            //
            this.EmployeeCode.DataPropertyName = "EmployeeCode";
            this.EmployeeCode.HeaderText = "ID";
            this.EmployeeCode.Name = "EmployeeCode";
            this.EmployeeCode.ReadOnly = true;
            this.EmployeeCode.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.EmployeeCode.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.EmployeeCode.Width = 90;
            //
            // EmployeeName
            //
            this.EmployeeName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.EmployeeName.DataPropertyName = "EmployeeName";
            this.EmployeeName.HeaderText = "Name";
            this.EmployeeName.MinimumWidth = 120;
            this.EmployeeName.Name = "EmployeeName";
            this.EmployeeName.ReadOnly = true;
            this.EmployeeName.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.EmployeeName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            //
            // RoleName
            //
            this.RoleName.DataPropertyName = "RoleName";
            roleStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(97)))), ((int)(((byte)(97)))), ((int)(((byte)(97)))));
            roleStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(97)))), ((int)(((byte)(97)))), ((int)(((byte)(97)))));
            this.RoleName.DefaultCellStyle = roleStyle;
            this.RoleName.HeaderText = "Role";
            this.RoleName.Name = "RoleName";
            this.RoleName.ReadOnly = true;
            this.RoleName.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.RoleName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.RoleName.Width = 160;
            //
            // Delete
            //
            this.Delete.HeaderText = "";
            this.Delete.Image = global::ThirdPartyTool.Properties.Resources.Icon_Delete;
            this.Delete.Name = "Delete";
            this.Delete.ReadOnly = true;
            this.Delete.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Delete.Visible = false;
            this.Delete.Width = 50;
            //
            // pnlSearchGap
            //
            this.pnlSearchGap.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSearchGap.Location = new System.Drawing.Point(16, 52);
            this.pnlSearchGap.Name = "pnlSearchGap";
            this.pnlSearchGap.Size = new System.Drawing.Size(484, 12);
            this.pnlSearchGap.TabIndex = 1;
            //
            // pnlSearch
            //
            this.pnlSearch.BackColor = System.Drawing.Color.White;
            this.pnlSearch.BackgroundColor = System.Drawing.Color.Transparent;
            this.pnlSearch.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(189)))));
            this.pnlSearch.BorderRadius = 5;
            this.pnlSearch.BorderSize = 1;
            this.pnlSearch.Controls.Add(this.txtSearch);
            this.pnlSearch.Controls.Add(this.btnSearch);
            this.pnlSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSearch.Location = new System.Drawing.Point(16, 12);
            this.pnlSearch.Name = "pnlSearch";
            this.pnlSearch.Size = new System.Drawing.Size(484, 40);
            this.pnlSearch.TabIndex = 0;
            //
            // txtSearch
            //
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSearch.Font = new System.Drawing.Font("Roboto", 12F);
            this.txtSearch.ForeColor = System.Drawing.Color.Gray;
            this.txtSearch.Location = new System.Drawing.Point(12, 11);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.PlaceholderColor = System.Drawing.Color.Gray;
            this.txtSearch.PlaceholderText = "Search by ID or name";
            this.txtSearch.Size = new System.Drawing.Size(424, 20);
            this.txtSearch.TabIndex = 2;
            this.txtSearch.Text = "Search by ID or name";
            //
            // btnSearch
            //
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.Image = global::ThirdPartyTool.Properties.Resources.Icon_Search;
            this.btnSearch.Location = new System.Drawing.Point(446, 3);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(34, 34);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.Text = "";
            //
            // txtFocus
            //
            this.txtFocus.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtFocus.Font = new System.Drawing.Font("Roboto", 12F);
            this.txtFocus.ForeColor = System.Drawing.Color.Gray;
            this.txtFocus.Location = new System.Drawing.Point(40, 200);
            this.txtFocus.Name = "txtFocus";
            this.txtFocus.PlaceholderColor = System.Drawing.Color.Gray;
            this.txtFocus.PlaceholderText = "";
            this.txtFocus.Size = new System.Drawing.Size(10, 20);
            this.txtFocus.TabIndex = 4;
            this.txtFocus.Text = "";
            //
            // pnlSyncDivider
            //
            this.pnlSyncDivider.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.pnlSyncDivider.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlSyncDivider.Location = new System.Drawing.Point(516, 65);
            this.pnlSyncDivider.Name = "pnlSyncDivider";
            this.pnlSyncDivider.Size = new System.Drawing.Size(1, 663);
            this.pnlSyncDivider.TabIndex = 3;
            //
            // pnlSync
            //
            this.pnlSync.BackColor = System.Drawing.Color.White;
            this.pnlSync.Controls.Add(this.label2);
            this.pnlSync.Controls.Add(this.lblSyncHint);
            this.pnlSync.Controls.Add(this.pnlEmployeeCard);
            this.pnlSync.Controls.Add(this.label4);
            this.pnlSync.Controls.Add(this.label6);
            this.pnlSync.Controls.Add(this.bPanel_Rounded2);
            this.pnlSync.Controls.Add(this.label5);
            this.pnlSync.Controls.Add(this.label7);
            this.pnlSync.Controls.Add(this.bPanel_Rounded4);
            this.pnlSync.Controls.Add(this.bPanel_Rounded5);
            this.pnlSync.Controls.Add(this.btnSyncRoles);
            this.pnlSync.Controls.Add(this.lblStartDate);
            this.pnlSync.Controls.Add(this.lblEndDate);
            this.pnlSync.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlSync.Location = new System.Drawing.Point(517, 65);
            this.pnlSync.Name = "pnlSync";
            this.pnlSync.Size = new System.Drawing.Size(320, 663);
            this.pnlSync.TabIndex = 4;
            //
            // label2
            //
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Roboto", 13F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.label2.Location = new System.Drawing.Point(22, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(125, 21);
            this.label2.TabIndex = 0;
            this.label2.Text = "Sync job role";
            //
            // lblSyncHint
            //
            this.lblSyncHint.Font = new System.Drawing.Font("Roboto", 9.75F);
            this.lblSyncHint.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(117)))), ((int)(((byte)(117)))));
            this.lblSyncHint.Location = new System.Drawing.Point(23, 44);
            this.lblSyncHint.Name = "lblSyncHint";
            this.lblSyncHint.Size = new System.Drawing.Size(274, 50);
            this.lblSyncHint.TabIndex = 1;
            this.lblSyncHint.Text = "Applies the new role\'s wage and tip rates to the employee\'s clock-ins and tip records in the date range.";
            //
            // pnlEmployeeCard
            //
            this.pnlEmployeeCard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.pnlEmployeeCard.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.pnlEmployeeCard.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(232)))), ((int)(((byte)(236)))));
            this.pnlEmployeeCard.BorderRadius = 6;
            this.pnlEmployeeCard.BorderSize = 1;
            this.pnlEmployeeCard.Controls.Add(this.lblEmployeeCaption);
            this.pnlEmployeeCard.Controls.Add(this.lblCurrentEmployeeName);
            this.pnlEmployeeCard.Controls.Add(this.lblCurrentEmployeeMeta);
            this.pnlEmployeeCard.Location = new System.Drawing.Point(24, 102);
            this.pnlEmployeeCard.Name = "pnlEmployeeCard";
            this.pnlEmployeeCard.Size = new System.Drawing.Size(272, 78);
            this.pnlEmployeeCard.TabIndex = 2;
            //
            // lblEmployeeCaption
            //
            this.lblEmployeeCaption.AutoSize = true;
            this.lblEmployeeCaption.BackColor = System.Drawing.Color.Transparent;
            this.lblEmployeeCaption.Font = new System.Drawing.Font("Roboto", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblEmployeeCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(117)))), ((int)(((byte)(117)))));
            this.lblEmployeeCaption.Location = new System.Drawing.Point(12, 10);
            this.lblEmployeeCaption.Name = "lblEmployeeCaption";
            this.lblEmployeeCaption.Size = new System.Drawing.Size(68, 13);
            this.lblEmployeeCaption.TabIndex = 0;
            this.lblEmployeeCaption.Text = "EMPLOYEE";
            //
            // lblCurrentEmployeeName
            //
            this.lblCurrentEmployeeName.AutoEllipsis = true;
            this.lblCurrentEmployeeName.BackColor = System.Drawing.Color.Transparent;
            this.lblCurrentEmployeeName.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Bold);
            this.lblCurrentEmployeeName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.lblCurrentEmployeeName.Location = new System.Drawing.Point(11, 27);
            this.lblCurrentEmployeeName.Name = "lblCurrentEmployeeName";
            this.lblCurrentEmployeeName.Size = new System.Drawing.Size(250, 22);
            this.lblCurrentEmployeeName.TabIndex = 1;
            this.lblCurrentEmployeeName.Text = "No employee selected";
            //
            // lblCurrentEmployeeMeta
            //
            this.lblCurrentEmployeeMeta.AutoEllipsis = true;
            this.lblCurrentEmployeeMeta.BackColor = System.Drawing.Color.Transparent;
            this.lblCurrentEmployeeMeta.Font = new System.Drawing.Font("Roboto", 9.75F);
            this.lblCurrentEmployeeMeta.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(117)))), ((int)(((byte)(117)))));
            this.lblCurrentEmployeeMeta.Location = new System.Drawing.Point(12, 51);
            this.lblCurrentEmployeeMeta.Name = "lblCurrentEmployeeMeta";
            this.lblCurrentEmployeeMeta.Size = new System.Drawing.Size(250, 18);
            this.lblCurrentEmployeeMeta.TabIndex = 2;
            this.lblCurrentEmployeeMeta.Text = "Pick one from the list.";
            //
            // label4
            //
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Roboto", 10F, System.Drawing.FontStyle.Bold);
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.label4.Location = new System.Drawing.Point(22, 198);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 17);
            this.label4.TabIndex = 3;
            this.label4.Text = "New role";
            //
            // label6
            //
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Roboto", 10F, System.Drawing.FontStyle.Bold);
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(47)))), ((int)(((byte)(47)))));
            this.label6.Location = new System.Drawing.Point(86, 198);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(14, 17);
            this.label6.TabIndex = 4;
            this.label6.Text = "*";
            //
            // bPanel_Rounded2
            //
            this.bPanel_Rounded2.BackColor = System.Drawing.Color.White;
            this.bPanel_Rounded2.BackgroundColor = System.Drawing.Color.Transparent;
            this.bPanel_Rounded2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(189)))));
            this.bPanel_Rounded2.BorderRadius = 5;
            this.bPanel_Rounded2.BorderSize = 1;
            this.bPanel_Rounded2.Controls.Add(this.lblSyncRoles);
            this.bPanel_Rounded2.Controls.Add(this.cbbSyncRoles);
            this.bPanel_Rounded2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bPanel_Rounded2.Location = new System.Drawing.Point(24, 222);
            this.bPanel_Rounded2.Name = "bPanel_Rounded2";
            this.bPanel_Rounded2.Size = new System.Drawing.Size(272, 40);
            this.bPanel_Rounded2.TabIndex = 5;
            //
            // lblSyncRoles
            //
            this.lblSyncRoles.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSyncRoles.BackColor = System.Drawing.Color.White;
            this.lblSyncRoles.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblSyncRoles.Font = new System.Drawing.Font("Roboto", 11F);
            this.lblSyncRoles.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.lblSyncRoles.Image = global::ThirdPartyTool.Properties.Resources.Icon_Arrow_Down;
            this.lblSyncRoles.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblSyncRoles.Location = new System.Drawing.Point(6, 5);
            this.lblSyncRoles.Name = "lblSyncRoles";
            this.lblSyncRoles.Size = new System.Drawing.Size(258, 30);
            this.lblSyncRoles.TabIndex = 4;
            this.lblSyncRoles.Text = "";
            this.lblSyncRoles.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // cbbSyncRoles
            //
            this.cbbSyncRoles.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cbbSyncRoles.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbSyncRoles.Font = new System.Drawing.Font("Roboto", 11F);
            this.cbbSyncRoles.FormattingEnabled = true;
            this.cbbSyncRoles.Location = new System.Drawing.Point(6, 7);
            this.cbbSyncRoles.Name = "cbbSyncRoles";
            this.cbbSyncRoles.Size = new System.Drawing.Size(258, 26);
            this.cbbSyncRoles.TabIndex = 2;
            //
            // label5
            //
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Roboto", 10F, System.Drawing.FontStyle.Bold);
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.label5.Location = new System.Drawing.Point(22, 280);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(40, 17);
            this.label5.TabIndex = 6;
            this.label5.Text = "From";
            //
            // label7
            //
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Roboto", 10F, System.Drawing.FontStyle.Bold);
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.label7.Location = new System.Drawing.Point(162, 280);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(24, 17);
            this.label7.TabIndex = 7;
            this.label7.Text = "To";
            //
            // bPanel_Rounded4
            //
            this.bPanel_Rounded4.BackColor = System.Drawing.Color.White;
            this.bPanel_Rounded4.BackgroundColor = System.Drawing.Color.Transparent;
            this.bPanel_Rounded4.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(189)))));
            this.bPanel_Rounded4.BorderRadius = 5;
            this.bPanel_Rounded4.BorderSize = 1;
            this.bPanel_Rounded4.Controls.Add(this.pnlStartDateClip);
            this.bPanel_Rounded4.Location = new System.Drawing.Point(24, 304);
            this.bPanel_Rounded4.Name = "bPanel_Rounded4";
            this.bPanel_Rounded4.Size = new System.Drawing.Size(132, 40);
            this.bPanel_Rounded4.TabIndex = 8;
            //
            // pnlStartDateClip
            //
            this.pnlStartDateClip.BackColor = System.Drawing.Color.White;
            this.pnlStartDateClip.Controls.Add(this.startDate);
            this.pnlStartDateClip.Location = new System.Drawing.Point(6, 10);
            this.pnlStartDateClip.Name = "pnlStartDateClip";
            this.pnlStartDateClip.Size = new System.Drawing.Size(120, 20);
            this.pnlStartDateClip.TabIndex = 0;
            //
            // startDate
            //
            this.startDate.CustomFormat = "MM/dd/yyyy";
            this.startDate.Font = new System.Drawing.Font("Roboto", 11F);
            this.startDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.startDate.Location = new System.Drawing.Point(-2, -2);
            this.startDate.Name = "startDate";
            this.startDate.Size = new System.Drawing.Size(124, 25);
            this.startDate.TabIndex = 0;
            //
            // bPanel_Rounded5
            //
            this.bPanel_Rounded5.BackColor = System.Drawing.Color.White;
            this.bPanel_Rounded5.BackgroundColor = System.Drawing.Color.Transparent;
            this.bPanel_Rounded5.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(189)))));
            this.bPanel_Rounded5.BorderRadius = 5;
            this.bPanel_Rounded5.BorderSize = 1;
            this.bPanel_Rounded5.Controls.Add(this.pnlEndDateClip);
            this.bPanel_Rounded5.Location = new System.Drawing.Point(164, 304);
            this.bPanel_Rounded5.Name = "bPanel_Rounded5";
            this.bPanel_Rounded5.Size = new System.Drawing.Size(132, 40);
            this.bPanel_Rounded5.TabIndex = 9;
            //
            // pnlEndDateClip
            //
            this.pnlEndDateClip.BackColor = System.Drawing.Color.White;
            this.pnlEndDateClip.Controls.Add(this.endDate);
            this.pnlEndDateClip.Location = new System.Drawing.Point(6, 10);
            this.pnlEndDateClip.Name = "pnlEndDateClip";
            this.pnlEndDateClip.Size = new System.Drawing.Size(120, 20);
            this.pnlEndDateClip.TabIndex = 0;
            //
            // endDate
            //
            this.endDate.CustomFormat = "MM/dd/yyyy";
            this.endDate.Font = new System.Drawing.Font("Roboto", 11F);
            this.endDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.endDate.Location = new System.Drawing.Point(-2, -2);
            this.endDate.Name = "endDate";
            this.endDate.Size = new System.Drawing.Size(124, 25);
            this.endDate.TabIndex = 0;
            //
            // btnSyncRoles
            //
            this.btnSyncRoles.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(108)))), ((int)(((byte)(228)))));
            this.btnSyncRoles.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(108)))), ((int)(((byte)(228)))));
            this.btnSyncRoles.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(108)))), ((int)(((byte)(228)))));
            this.btnSyncRoles.BorderRadius = 5;
            this.btnSyncRoles.BorderSize = 1;
            this.btnSyncRoles.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSyncRoles.Font = new System.Drawing.Font("Roboto", 11F, System.Drawing.FontStyle.Bold);
            this.btnSyncRoles.ForeColor = System.Drawing.Color.White;
            this.btnSyncRoles.Location = new System.Drawing.Point(24, 368);
            this.btnSyncRoles.Name = "btnSyncRoles";
            this.btnSyncRoles.Size = new System.Drawing.Size(272, 44);
            this.btnSyncRoles.TabIndex = 10;
            this.btnSyncRoles.Text = "Sync role";
            this.btnSyncRoles.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnSyncRoles.TextColor = System.Drawing.Color.White;
            //
            // lblStartDate
            //
            this.lblStartDate.Font = new System.Drawing.Font("Roboto", 11F);
            this.lblStartDate.Location = new System.Drawing.Point(24, 440);
            this.lblStartDate.Name = "lblStartDate";
            this.lblStartDate.Size = new System.Drawing.Size(120, 20);
            this.lblStartDate.TabIndex = 11;
            this.lblStartDate.Text = "";
            this.lblStartDate.Visible = false;
            //
            // lblEndDate
            //
            this.lblEndDate.Font = new System.Drawing.Font("Roboto", 11F);
            this.lblEndDate.Location = new System.Drawing.Point(164, 440);
            this.lblEndDate.Name = "lblEndDate";
            this.lblEndDate.Size = new System.Drawing.Size(120, 20);
            this.lblEndDate.TabIndex = 12;
            this.lblEndDate.Text = "";
            this.lblEndDate.Visible = false;
            //
            // EmployeeView
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.pnlList);
            this.Controls.Add(this.pnlSyncDivider);
            this.Controls.Add(this.pnlSync);
            this.Controls.Add(this.pnlHeaderDivider);
            this.Controls.Add(this.pnlHeader);
            this.Name = "EmployeeView";
            this.Size = new System.Drawing.Size(837, 728);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlList.ResumeLayout(false);
            this.pnlList.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmployees)).EndInit();
            this.pnlSearch.ResumeLayout(false);
            this.pnlSearch.PerformLayout();
            this.pnlSync.ResumeLayout(false);
            this.pnlSync.PerformLayout();
            this.pnlEmployeeCard.ResumeLayout(false);
            this.pnlEmployeeCard.PerformLayout();
            this.bPanel_Rounded2.ResumeLayout(false);
            this.bPanel_Rounded4.ResumeLayout(false);
            this.pnlStartDateClip.ResumeLayout(false);
            this.bPanel_Rounded5.ResumeLayout(false);
            this.pnlEndDateClip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.Label btnRefresh;
        private System.Windows.Forms.Panel pnlHeaderDivider;
        private System.Windows.Forms.Panel pnlList;
        private System.Windows.Forms.DataGridView dgvEmployees;
        private System.Windows.Forms.DataGridViewTextBoxColumn EmployeeID;
        private System.Windows.Forms.DataGridViewTextBoxColumn EmployeeCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn EmployeeName;
        private System.Windows.Forms.DataGridViewTextBoxColumn RoleName;
        private System.Windows.Forms.DataGridViewImageColumn Delete;
        private System.Windows.Forms.Panel pnlSearchGap;
        private CustomControl.BPanel_Rounded pnlSearch;
        private CustomControl.BTextBox txtSearch;
        private System.Windows.Forms.Label btnSearch;
        public CustomControl.BTextBox txtFocus;
        private System.Windows.Forms.Panel pnlSyncDivider;
        private System.Windows.Forms.Panel pnlSync;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblSyncHint;
        private CustomControl.BPanel_Rounded pnlEmployeeCard;
        private System.Windows.Forms.Label lblEmployeeCaption;
        private System.Windows.Forms.Label lblCurrentEmployeeName;
        private System.Windows.Forms.Label lblCurrentEmployeeMeta;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private CustomControl.BPanel_Rounded bPanel_Rounded2;
        private System.Windows.Forms.Label lblSyncRoles;
        private System.Windows.Forms.ComboBox cbbSyncRoles;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private CustomControl.BPanel_Rounded bPanel_Rounded4;
        private System.Windows.Forms.Panel pnlStartDateClip;
        private System.Windows.Forms.DateTimePicker startDate;
        private CustomControl.BPanel_Rounded bPanel_Rounded5;
        private System.Windows.Forms.Panel pnlEndDateClip;
        private System.Windows.Forms.DateTimePicker endDate;
        private CustomControl.BLabel_Rounded btnSyncRoles;
        private System.Windows.Forms.Label lblStartDate;
        private System.Windows.Forms.Label lblEndDate;
    }
}
