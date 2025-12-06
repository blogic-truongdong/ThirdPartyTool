namespace ThirdPartyTool.Employee
{
    partial class EmployeeLinkPaycorView2
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnSearch = new System.Windows.Forms.Label();
            this.pnlSearch = new ThirdPartyTool.CustomControl.BPanel_Rounded();
            this.txtSearch = new ThirdPartyTool.CustomControl.BTextBox();
            this.txtFocus = new ThirdPartyTool.CustomControl.BTextBox();
            this.btnRefresh = new System.Windows.Forms.Label();
            this.dgvEmployees = new System.Windows.Forms.DataGridView();
            this.EmployeeID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EmployeeCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EmployeeName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RoleName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PaycorEmployeeDetail = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Delete = new System.Windows.Forms.DataGridViewImageColumn();
            this.PaycorEmployeeID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LinkPaycor = new System.Windows.Forms.DataGridViewButtonColumn();
            this.bPanel_Rounded1 = new ThirdPartyTool.CustomControl.BPanel_Rounded();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnReSync = new ThirdPartyTool.CustomControl.BLabel_Rounded();
            this.dataGridViewImageColumn1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.pnlSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmployees)).BeginInit();
            this.bPanel_Rounded1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.Image = global::ThirdPartyTool.Properties.Resources.Icon_Search;
            this.btnSearch.Location = new System.Drawing.Point(719, 3);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(34, 34);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.TextAlign = System.Drawing.ContentAlignment.TopCenter;
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
            this.pnlSearch.Size = new System.Drawing.Size(756, 40);
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
            this.txtSearch.Size = new System.Drawing.Size(703, 20);
            this.txtSearch.TabIndex = 2;
            this.txtSearch.Text = "Search";
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
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Bold);
            this.btnRefresh.Image = global::ThirdPartyTool.Properties.Resources.Icon_Refresh;
            this.btnRefresh.Location = new System.Drawing.Point(766, 11);
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
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvEmployees.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvEmployees.ColumnHeadersHeight = 40;
            this.dgvEmployees.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.EmployeeID,
            this.EmployeeCode,
            this.EmployeeName,
            this.RoleName,
            this.PaycorEmployeeDetail,
            this.Delete,
            this.PaycorEmployeeID,
            this.LinkPaycor});
            this.dgvEmployees.Cursor = System.Windows.Forms.Cursors.Default;
            this.dgvEmployees.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvEmployees.EnableHeadersVisualStyles = false;
            this.dgvEmployees.Location = new System.Drawing.Point(3, 3);
            this.dgvEmployees.MultiSelect = false;
            this.dgvEmployees.Name = "dgvEmployees";
            this.dgvEmployees.ReadOnly = true;
            this.dgvEmployees.RowHeadersVisible = false;
            this.dgvEmployees.RowTemplate.Height = 40;
            this.dgvEmployees.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvEmployees.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvEmployees.Size = new System.Drawing.Size(985, 512);
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
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            this.EmployeeCode.DefaultCellStyle = dataGridViewCellStyle2;
            this.EmployeeCode.HeaderText = "ID";
            this.EmployeeCode.Name = "EmployeeCode";
            this.EmployeeCode.ReadOnly = true;
            this.EmployeeCode.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // EmployeeName
            // 
            this.EmployeeName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.EmployeeName.DataPropertyName = "EmployeeName";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
            this.EmployeeName.DefaultCellStyle = dataGridViewCellStyle3;
            this.EmployeeName.HeaderText = "Name";
            this.EmployeeName.Name = "EmployeeName";
            this.EmployeeName.ReadOnly = true;
            this.EmployeeName.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // RoleName
            // 
            this.RoleName.DataPropertyName = "RoleName";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Roboto", 12F);
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.White;
            this.RoleName.DefaultCellStyle = dataGridViewCellStyle4;
            this.RoleName.HeaderText = "Roles";
            this.RoleName.Name = "RoleName";
            this.RoleName.ReadOnly = true;
            this.RoleName.Visible = false;
            // 
            // PaycorEmployeeDetail
            // 
            this.PaycorEmployeeDetail.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.PaycorEmployeeDetail.DataPropertyName = "PaycorEmployeeDetail";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Roboto", 12F);
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.White;
            this.PaycorEmployeeDetail.DefaultCellStyle = dataGridViewCellStyle5;
            this.PaycorEmployeeDetail.HeaderText = "Paycor Employee";
            this.PaycorEmployeeDetail.MinimumWidth = 300;
            this.PaycorEmployeeDetail.Name = "PaycorEmployeeDetail";
            this.PaycorEmployeeDetail.ReadOnly = true;
            this.PaycorEmployeeDetail.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.PaycorEmployeeDetail.Width = 300;
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
            // PaycorEmployeeID
            // 
            this.PaycorEmployeeID.DataPropertyName = "PaycorEmployeeID";
            this.PaycorEmployeeID.HeaderText = "PaycorEmployeeID";
            this.PaycorEmployeeID.Name = "PaycorEmployeeID";
            this.PaycorEmployeeID.ReadOnly = true;
            this.PaycorEmployeeID.Visible = false;
            // 
            // LinkPaycor
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LinkPaycor.DefaultCellStyle = dataGridViewCellStyle6;
            this.LinkPaycor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LinkPaycor.HeaderText = "";
            this.LinkPaycor.Name = "LinkPaycor";
            this.LinkPaycor.ReadOnly = true;
            this.LinkPaycor.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // bPanel_Rounded1
            // 
            this.bPanel_Rounded1.BackColor = System.Drawing.Color.White;
            this.bPanel_Rounded1.BackgroundColor = System.Drawing.Color.Transparent;
            this.bPanel_Rounded1.BorderColor = System.Drawing.Color.Gray;
            this.bPanel_Rounded1.BorderRadius = 5;
            this.bPanel_Rounded1.BorderSize = 1;
            this.bPanel_Rounded1.Controls.Add(this.dgvEmployees);
            this.bPanel_Rounded1.Controls.Add(this.txtFocus);
            this.bPanel_Rounded1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bPanel_Rounded1.Location = new System.Drawing.Point(0, 60);
            this.bPanel_Rounded1.Name = "bPanel_Rounded1";
            this.bPanel_Rounded1.Padding = new System.Windows.Forms.Padding(3);
            this.bPanel_Rounded1.Size = new System.Drawing.Size(991, 518);
            this.bPanel_Rounded1.TabIndex = 6;
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 60);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.panel2.Size = new System.Drawing.Size(991, 518);
            this.panel2.TabIndex = 8;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnReSync);
            this.panel1.Controls.Add(this.pnlSearch);
            this.panel1.Controls.Add(this.btnRefresh);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(991, 60);
            this.panel1.TabIndex = 7;
            // 
            // btnReSync
            // 
            this.btnReSync.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReSync.BackColor = System.Drawing.Color.Red;
            this.btnReSync.BackgroundColor = System.Drawing.Color.Red;
            this.btnReSync.BorderColor = System.Drawing.Color.Gray;
            this.btnReSync.BorderRadius = 5;
            this.btnReSync.BorderSize = 0;
            this.btnReSync.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Bold);
            this.btnReSync.ForeColor = System.Drawing.Color.White;
            this.btnReSync.Location = new System.Drawing.Point(832, 11);
            this.btnReSync.Name = "btnReSync";
            this.btnReSync.Size = new System.Drawing.Size(150, 40);
            this.btnReSync.TabIndex = 4;
            this.btnReSync.Text = "Re-sync Paycor";
            this.btnReSync.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnReSync.TextColor = System.Drawing.Color.White;
            // 
            // dataGridViewImageColumn1
            // 
            this.dataGridViewImageColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewImageColumn1.HeaderText = "";
            this.dataGridViewImageColumn1.Image = global::ThirdPartyTool.Properties.Resources.Icon_Delete;
            this.dataGridViewImageColumn1.Name = "dataGridViewImageColumn1";
            this.dataGridViewImageColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewImageColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dataGridViewImageColumn1.Visible = false;
            this.dataGridViewImageColumn1.Width = 50;
            // 
            // EmployeeLinkPaycorView2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.bPanel_Rounded1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "EmployeeLinkPaycorView2";
            this.Size = new System.Drawing.Size(991, 578);
            this.pnlSearch.ResumeLayout(false);
            this.pnlSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmployees)).EndInit();
            this.bPanel_Rounded1.ResumeLayout(false);
            this.bPanel_Rounded1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label btnSearch;
        private CustomControl.BPanel_Rounded pnlSearch;
        private CustomControl.BTextBox txtSearch;
        public CustomControl.BTextBox txtFocus;
        private System.Windows.Forms.Label btnRefresh;
        private System.Windows.Forms.DataGridView dgvEmployees;
        private CustomControl.BPanel_Rounded bPanel_Rounded1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn1;
        private CustomControl.BLabel_Rounded btnReSync;
        private System.Windows.Forms.DataGridViewTextBoxColumn EmployeeID;
        private System.Windows.Forms.DataGridViewTextBoxColumn EmployeeCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn EmployeeName;
        private System.Windows.Forms.DataGridViewTextBoxColumn RoleName;
        private System.Windows.Forms.DataGridViewTextBoxColumn PaycorEmployeeDetail;
        private System.Windows.Forms.DataGridViewImageColumn Delete;
        private System.Windows.Forms.DataGridViewTextBoxColumn PaycorEmployeeID;
        private System.Windows.Forms.DataGridViewButtonColumn LinkPaycor;
    }
}
