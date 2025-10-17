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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dataGridViewImageColumn1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.bPanel_Rounded3 = new ThirdPartyTool.CustomControl.BPanel_Rounded();
            this.bPanel_Rounded5 = new ThirdPartyTool.CustomControl.BPanel_Rounded();
            this.lblEndDate = new System.Windows.Forms.Label();
            this.endDate = new System.Windows.Forms.DateTimePicker();
            this.bPanel_Rounded4 = new ThirdPartyTool.CustomControl.BPanel_Rounded();
            this.lblStartDate = new System.Windows.Forms.Label();
            this.startDate = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblCurrentEmployeeName = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnSyncRoles = new ThirdPartyTool.CustomControl.BLabel_Rounded();
            this.bPanel_Rounded2 = new ThirdPartyTool.CustomControl.BPanel_Rounded();
            this.lblSyncRoles = new System.Windows.Forms.Label();
            this.cbbSyncRoles = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.bPanel_Rounded1 = new ThirdPartyTool.CustomControl.BPanel_Rounded();
            this.label8 = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Label();
            this.dgvEmployees = new System.Windows.Forms.DataGridView();
            this.EmployeeID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EmployeeCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EmployeeName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RoleName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Delete = new System.Windows.Forms.DataGridViewImageColumn();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtFocus = new ThirdPartyTool.CustomControl.BTextBox();
            this.pnlSearch = new ThirdPartyTool.CustomControl.BPanel_Rounded();
            this.txtSearch = new ThirdPartyTool.CustomControl.BTextBox();
            this.btnSearch = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.bPanel_Rounded3.SuspendLayout();
            this.bPanel_Rounded5.SuspendLayout();
            this.bPanel_Rounded4.SuspendLayout();
            this.bPanel_Rounded2.SuspendLayout();
            this.bPanel_Rounded1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmployees)).BeginInit();
            this.pnlSearch.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pnlSearch);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(10, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(829, 60);
            this.panel1.TabIndex = 3;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.bPanel_Rounded3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(480, 60);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.panel2.Size = new System.Drawing.Size(359, 454);
            this.panel2.TabIndex = 5;
            // 
            // dataGridViewImageColumn1
            // 
            this.dataGridViewImageColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewImageColumn1.HeaderText = "";
            this.dataGridViewImageColumn1.Image = global::ThirdPartyTool.Properties.Resources.Icon_Delete;
            this.dataGridViewImageColumn1.Name = "dataGridViewImageColumn1";
            this.dataGridViewImageColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewImageColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dataGridViewImageColumn1.Width = 50;
            // 
            // bPanel_Rounded3
            // 
            this.bPanel_Rounded3.BackgroundColor = System.Drawing.Color.Transparent;
            this.bPanel_Rounded3.BorderColor = System.Drawing.Color.Gray;
            this.bPanel_Rounded3.BorderRadius = 5;
            this.bPanel_Rounded3.BorderSize = 1;
            this.bPanel_Rounded3.Controls.Add(this.bPanel_Rounded5);
            this.bPanel_Rounded3.Controls.Add(this.bPanel_Rounded4);
            this.bPanel_Rounded3.Controls.Add(this.label6);
            this.bPanel_Rounded3.Controls.Add(this.label7);
            this.bPanel_Rounded3.Controls.Add(this.label5);
            this.bPanel_Rounded3.Controls.Add(this.lblCurrentEmployeeName);
            this.bPanel_Rounded3.Controls.Add(this.label4);
            this.bPanel_Rounded3.Controls.Add(this.btnSyncRoles);
            this.bPanel_Rounded3.Controls.Add(this.bPanel_Rounded2);
            this.bPanel_Rounded3.Controls.Add(this.label2);
            this.bPanel_Rounded3.Dock = System.Windows.Forms.DockStyle.Top;
            this.bPanel_Rounded3.Location = new System.Drawing.Point(10, 0);
            this.bPanel_Rounded3.Name = "bPanel_Rounded3";
            this.bPanel_Rounded3.Size = new System.Drawing.Size(349, 390);
            this.bPanel_Rounded3.TabIndex = 1;
            // 
            // bPanel_Rounded5
            // 
            this.bPanel_Rounded5.BackgroundColor = System.Drawing.Color.Transparent;
            this.bPanel_Rounded5.BorderColor = System.Drawing.Color.Gray;
            this.bPanel_Rounded5.BorderRadius = 5;
            this.bPanel_Rounded5.BorderSize = 1;
            this.bPanel_Rounded5.Controls.Add(this.lblEndDate);
            this.bPanel_Rounded5.Controls.Add(this.endDate);
            this.bPanel_Rounded5.Location = new System.Drawing.Point(30, 268);
            this.bPanel_Rounded5.Margin = new System.Windows.Forms.Padding(5);
            this.bPanel_Rounded5.Name = "bPanel_Rounded5";
            this.bPanel_Rounded5.Size = new System.Drawing.Size(250, 40);
            this.bPanel_Rounded5.TabIndex = 675;
            // 
            // lblEndDate
            // 
            this.lblEndDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblEndDate.Font = new System.Drawing.Font("Roboto", 12F);
            this.lblEndDate.Image = global::ThirdPartyTool.Properties.Resources.Icon_Arrow_Down;
            this.lblEndDate.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblEndDate.Location = new System.Drawing.Point(5, 5);
            this.lblEndDate.Name = "lblEndDate";
            this.lblEndDate.Size = new System.Drawing.Size(240, 30);
            this.lblEndDate.TabIndex = 674;
            this.lblEndDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // endDate
            // 
            this.endDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.endDate.Font = new System.Drawing.Font("Roboto", 12F);
            this.endDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.endDate.Location = new System.Drawing.Point(5, 7);
            this.endDate.Name = "endDate";
            this.endDate.Size = new System.Drawing.Size(240, 27);
            this.endDate.TabIndex = 673;
            // 
            // bPanel_Rounded4
            // 
            this.bPanel_Rounded4.BackgroundColor = System.Drawing.Color.Transparent;
            this.bPanel_Rounded4.BorderColor = System.Drawing.Color.Gray;
            this.bPanel_Rounded4.BorderRadius = 5;
            this.bPanel_Rounded4.BorderSize = 1;
            this.bPanel_Rounded4.Controls.Add(this.lblStartDate);
            this.bPanel_Rounded4.Controls.Add(this.startDate);
            this.bPanel_Rounded4.Location = new System.Drawing.Point(30, 184);
            this.bPanel_Rounded4.Margin = new System.Windows.Forms.Padding(5);
            this.bPanel_Rounded4.Name = "bPanel_Rounded4";
            this.bPanel_Rounded4.Size = new System.Drawing.Size(250, 40);
            this.bPanel_Rounded4.TabIndex = 675;
            // 
            // lblStartDate
            // 
            this.lblStartDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStartDate.Font = new System.Drawing.Font("Roboto", 12F);
            this.lblStartDate.Image = global::ThirdPartyTool.Properties.Resources.Icon_Arrow_Down;
            this.lblStartDate.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblStartDate.Location = new System.Drawing.Point(5, 5);
            this.lblStartDate.Name = "lblStartDate";
            this.lblStartDate.Size = new System.Drawing.Size(240, 30);
            this.lblStartDate.TabIndex = 4;
            this.lblStartDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // startDate
            // 
            this.startDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.startDate.Font = new System.Drawing.Font("Roboto", 12F);
            this.startDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.startDate.Location = new System.Drawing.Point(5, 7);
            this.startDate.Name = "startDate";
            this.startDate.Size = new System.Drawing.Size(240, 27);
            this.startDate.TabIndex = 673;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Arial", 11F);
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(232, 73);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(14, 17);
            this.label6.TabIndex = 672;
            this.label6.Text = "*";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(30, 239);
            this.label7.Margin = new System.Windows.Forms.Padding(5);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(83, 19);
            this.label7.TabIndex = 671;
            this.label7.Text = "From Date";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(30, 155);
            this.label5.Margin = new System.Windows.Forms.Padding(5);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(83, 19);
            this.label5.TabIndex = 671;
            this.label5.Text = "From Date";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCurrentEmployeeName
            // 
            this.lblCurrentEmployeeName.AutoSize = true;
            this.lblCurrentEmployeeName.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentEmployeeName.Location = new System.Drawing.Point(30, 39);
            this.lblCurrentEmployeeName.Margin = new System.Windows.Forms.Padding(5);
            this.lblCurrentEmployeeName.Name = "lblCurrentEmployeeName";
            this.lblCurrentEmployeeName.Size = new System.Drawing.Size(127, 19);
            this.lblCurrentEmployeeName.TabIndex = 671;
            this.lblCurrentEmployeeName.Text = "Employee Name";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(30, 73);
            this.label4.Margin = new System.Windows.Forms.Padding(5);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(202, 19);
            this.label4.TabIndex = 671;
            this.label4.Text = "New Role to Synchronize to";
            // 
            // btnSyncRoles
            // 
            this.btnSyncRoles.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnSyncRoles.BackgroundColor = System.Drawing.SystemColors.Highlight;
            this.btnSyncRoles.BorderColor = System.Drawing.Color.Gray;
            this.btnSyncRoles.BorderRadius = 5;
            this.btnSyncRoles.BorderSize = 1;
            this.btnSyncRoles.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Bold);
            this.btnSyncRoles.ForeColor = System.Drawing.Color.White;
            this.btnSyncRoles.Location = new System.Drawing.Point(30, 323);
            this.btnSyncRoles.Name = "btnSyncRoles";
            this.btnSyncRoles.Size = new System.Drawing.Size(250, 40);
            this.btnSyncRoles.TabIndex = 4;
            this.btnSyncRoles.Text = "Sync";
            this.btnSyncRoles.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnSyncRoles.TextColor = System.Drawing.Color.White;
            // 
            // bPanel_Rounded2
            // 
            this.bPanel_Rounded2.BackgroundColor = System.Drawing.Color.Transparent;
            this.bPanel_Rounded2.BorderColor = System.Drawing.Color.Gray;
            this.bPanel_Rounded2.BorderRadius = 5;
            this.bPanel_Rounded2.BorderSize = 1;
            this.bPanel_Rounded2.Controls.Add(this.lblSyncRoles);
            this.bPanel_Rounded2.Controls.Add(this.cbbSyncRoles);
            this.bPanel_Rounded2.Location = new System.Drawing.Point(30, 100);
            this.bPanel_Rounded2.Margin = new System.Windows.Forms.Padding(5);
            this.bPanel_Rounded2.Name = "bPanel_Rounded2";
            this.bPanel_Rounded2.Size = new System.Drawing.Size(250, 40);
            this.bPanel_Rounded2.TabIndex = 3;
            // 
            // lblSyncRoles
            // 
            this.lblSyncRoles.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSyncRoles.Font = new System.Drawing.Font("Roboto", 12F);
            this.lblSyncRoles.Image = global::ThirdPartyTool.Properties.Resources.Icon_Arrow_Down;
            this.lblSyncRoles.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblSyncRoles.Location = new System.Drawing.Point(5, 5);
            this.lblSyncRoles.Name = "lblSyncRoles";
            this.lblSyncRoles.Size = new System.Drawing.Size(240, 30);
            this.lblSyncRoles.TabIndex = 4;
            this.lblSyncRoles.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cbbSyncRoles
            // 
            this.cbbSyncRoles.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cbbSyncRoles.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbSyncRoles.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbbSyncRoles.FormattingEnabled = true;
            this.cbbSyncRoles.Location = new System.Drawing.Point(5, 7);
            this.cbbSyncRoles.Name = "cbbSyncRoles";
            this.cbbSyncRoles.Size = new System.Drawing.Size(240, 27);
            this.cbbSyncRoles.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(10, 10);
            this.label2.Margin = new System.Windows.Forms.Padding(5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(238, 19);
            this.label2.TabIndex = 1;
            this.label2.Text = "Job Role Synchronization Setup";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // bPanel_Rounded1
            // 
            this.bPanel_Rounded1.BackColor = System.Drawing.Color.White;
            this.bPanel_Rounded1.BackgroundColor = System.Drawing.Color.Transparent;
            this.bPanel_Rounded1.BorderColor = System.Drawing.Color.Gray;
            this.bPanel_Rounded1.BorderRadius = 5;
            this.bPanel_Rounded1.BorderSize = 1;
            this.bPanel_Rounded1.Controls.Add(this.label8);
            this.bPanel_Rounded1.Controls.Add(this.btnRefresh);
            this.bPanel_Rounded1.Controls.Add(this.dgvEmployees);
            this.bPanel_Rounded1.Controls.Add(this.label3);
            this.bPanel_Rounded1.Controls.Add(this.label1);
            this.bPanel_Rounded1.Controls.Add(this.txtFocus);
            this.bPanel_Rounded1.Dock = System.Windows.Forms.DockStyle.Left;
            this.bPanel_Rounded1.Location = new System.Drawing.Point(10, 60);
            this.bPanel_Rounded1.Name = "bPanel_Rounded1";
            this.bPanel_Rounded1.Padding = new System.Windows.Forms.Padding(3, 43, 3, 3);
            this.bPanel_Rounded1.Size = new System.Drawing.Size(470, 454);
            this.bPanel_Rounded1.TabIndex = 0;
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Bold);
            this.label8.Location = new System.Drawing.Point(300, 3);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(78, 40);
            this.label8.TabIndex = 5;
            this.label8.Text = "Roles";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Bold);
            this.btnRefresh.Image = global::ThirdPartyTool.Properties.Resources.Icon_Refresh;
            this.btnRefresh.Location = new System.Drawing.Point(407, 3);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(60, 40);
            this.btnRefresh.TabIndex = 3;
            this.btnRefresh.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dgvEmployees
            // 
            this.dgvEmployees.AllowUserToAddRows = false;
            this.dgvEmployees.AllowUserToDeleteRows = false;
            this.dgvEmployees.AllowUserToResizeColumns = false;
            this.dgvEmployees.AllowUserToResizeRows = false;
            this.dgvEmployees.BackgroundColor = System.Drawing.Color.White;
            this.dgvEmployees.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvEmployees.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.RaisedHorizontal;
            this.dgvEmployees.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvEmployees.ColumnHeadersVisible = false;
            this.dgvEmployees.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.EmployeeID,
            this.EmployeeCode,
            this.EmployeeName,
            this.RoleName,
            this.Delete});
            this.dgvEmployees.Cursor = System.Windows.Forms.Cursors.Default;
            this.dgvEmployees.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvEmployees.EnableHeadersVisualStyles = false;
            this.dgvEmployees.Location = new System.Drawing.Point(3, 43);
            this.dgvEmployees.MultiSelect = false;
            this.dgvEmployees.Name = "dgvEmployees";
            this.dgvEmployees.ReadOnly = true;
            this.dgvEmployees.RowHeadersVisible = false;
            this.dgvEmployees.RowTemplate.Height = 40;
            this.dgvEmployees.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvEmployees.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvEmployees.Size = new System.Drawing.Size(464, 408);
            this.dgvEmployees.TabIndex = 3;
            // 
            // EmployeeID
            // 
            this.EmployeeID.DataPropertyName = "EmployeeID";
            this.EmployeeID.HeaderText = "Column1";
            this.EmployeeID.Name = "EmployeeID";
            this.EmployeeID.ReadOnly = true;
            this.EmployeeID.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.EmployeeID.Visible = false;
            // 
            // EmployeeCode
            // 
            this.EmployeeCode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.EmployeeCode.DataPropertyName = "EmployeeCode";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            this.EmployeeCode.DefaultCellStyle = dataGridViewCellStyle1;
            this.EmployeeCode.HeaderText = "ID";
            this.EmployeeCode.Name = "EmployeeCode";
            this.EmployeeCode.ReadOnly = true;
            this.EmployeeCode.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // EmployeeName
            // 
            this.EmployeeName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.EmployeeName.DataPropertyName = "EmployeeName";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            this.EmployeeName.DefaultCellStyle = dataGridViewCellStyle2;
            this.EmployeeName.HeaderText = "Name";
            this.EmployeeName.Name = "EmployeeName";
            this.EmployeeName.ReadOnly = true;
            this.EmployeeName.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // RoleName
            // 
            this.RoleName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.RoleName.DataPropertyName = "RoleName";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Roboto", 12F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
            this.RoleName.DefaultCellStyle = dataGridViewCellStyle3;
            this.RoleName.HeaderText = "Roles";
            this.RoleName.Name = "RoleName";
            this.RoleName.ReadOnly = true;
            this.RoleName.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.RoleName.Width = 150;
            // 
            // Delete
            // 
            this.Delete.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Delete.HeaderText = "";
            this.Delete.Image = global::ThirdPartyTool.Properties.Resources.Icon_Delete;
            this.Delete.Name = "Delete";
            this.Delete.ReadOnly = true;
            this.Delete.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Delete.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Delete.Visible = false;
            this.Delete.Width = 50;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(103, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(128, 40);
            this.label3.TabIndex = 2;
            this.label3.Text = "Name";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 40);
            this.label1.TabIndex = 0;
            this.label1.Text = "ID";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtFocus
            // 
            this.txtFocus.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtFocus.Font = new System.Drawing.Font("Roboto", 12F);
            this.txtFocus.ForeColor = System.Drawing.Color.Gray;
            this.txtFocus.Location = new System.Drawing.Point(113, 140);
            this.txtFocus.Name = "txtFocus";
            this.txtFocus.PlaceholderColor = System.Drawing.Color.Gray;
            this.txtFocus.PlaceholderText = "Search";
            this.txtFocus.Size = new System.Drawing.Size(65, 20);
            this.txtFocus.TabIndex = 4;
            this.txtFocus.Text = "Search";
            // 
            // pnlSearch
            // 
            this.pnlSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlSearch.BackgroundColor = System.Drawing.Color.Transparent;
            this.pnlSearch.BorderColor = System.Drawing.Color.Gray;
            this.pnlSearch.BorderRadius = 5;
            this.pnlSearch.BorderSize = 1;
            this.pnlSearch.Controls.Add(this.txtSearch);
            this.pnlSearch.Controls.Add(this.btnSearch);
            this.pnlSearch.Location = new System.Drawing.Point(0, 10);
            this.pnlSearch.Name = "pnlSearch";
            this.pnlSearch.Size = new System.Drawing.Size(826, 40);
            this.pnlSearch.TabIndex = 2;
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSearch.Font = new System.Drawing.Font("Roboto", 12F);
            this.txtSearch.ForeColor = System.Drawing.Color.Gray;
            this.txtSearch.Location = new System.Drawing.Point(10, 12);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.PlaceholderColor = System.Drawing.Color.Gray;
            this.txtSearch.PlaceholderText = "Search";
            this.txtSearch.Size = new System.Drawing.Size(773, 20);
            this.txtSearch.TabIndex = 2;
            this.txtSearch.Text = "Search";
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.Image = global::ThirdPartyTool.Properties.Resources.Icon_Search;
            this.btnSearch.Location = new System.Drawing.Point(789, 3);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(34, 34);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // EmployeeView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.bPanel_Rounded1);
            this.Controls.Add(this.panel1);
            this.Name = "EmployeeView";
            this.Padding = new System.Windows.Forms.Padding(10, 0, 10, 10);
            this.Size = new System.Drawing.Size(849, 524);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.bPanel_Rounded3.ResumeLayout(false);
            this.bPanel_Rounded3.PerformLayout();
            this.bPanel_Rounded5.ResumeLayout(false);
            this.bPanel_Rounded4.ResumeLayout(false);
            this.bPanel_Rounded2.ResumeLayout(false);
            this.bPanel_Rounded1.ResumeLayout(false);
            this.bPanel_Rounded1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmployees)).EndInit();
            this.pnlSearch.ResumeLayout(false);
            this.pnlSearch.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private CustomControl.BPanel_Rounded bPanel_Rounded1;
        private CustomControl.BPanel_Rounded bPanel_Rounded3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private CustomControl.BPanel_Rounded pnlSearch;
        private CustomControl.BTextBox txtSearch;
        private System.Windows.Forms.Label btnSearch;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label btnRefresh;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView dgvEmployees;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn1;
        private System.Windows.Forms.ComboBox cbbSyncRoles;
        private System.Windows.Forms.Label label2;
        private CustomControl.BPanel_Rounded bPanel_Rounded2;
        private System.Windows.Forms.Label lblSyncRoles;
        private CustomControl.BLabel_Rounded btnSyncRoles;
        private CustomControl.BPanel_Rounded bPanel_Rounded5;
        private System.Windows.Forms.DateTimePicker endDate;
        private CustomControl.BPanel_Rounded bPanel_Rounded4;
        private System.Windows.Forms.DateTimePicker startDate;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblEndDate;
        private System.Windows.Forms.Label lblStartDate;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblCurrentEmployeeName;
        private System.Windows.Forms.DataGridViewTextBoxColumn EmployeeID;
        private System.Windows.Forms.DataGridViewTextBoxColumn EmployeeCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn EmployeeName;
        private System.Windows.Forms.DataGridViewTextBoxColumn RoleName;
        private System.Windows.Forms.DataGridViewImageColumn Delete;
        public CustomControl.BTextBox txtFocus;
    }
}
