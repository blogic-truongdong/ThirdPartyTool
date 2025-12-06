namespace ThirdPartyTool.Employee
{
    partial class EmployeeLinkPaycorForm
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

        #region Windows Form Designer generated code

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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.txtSearch = new ThirdPartyTool.CustomControl.BTextBox();
            this.pnlSearch = new ThirdPartyTool.CustomControl.BPanel_Rounded();
            this.btnSearch = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dataGridViewImageColumn1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.bPanel_Rounded1 = new ThirdPartyTool.CustomControl.BPanel_Rounded();
            this.dgvEmployees = new System.Windows.Forms.DataGridView();
            this.txtFocus = new ThirdPartyTool.CustomControl.BTextBox();
            this.btnClose = new ThirdPartyTool.CustomControl.BLabel_Rounded();
            this.PaycorEmployeeID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EmployeeNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EmployeeName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AlternateEmployeeNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BadgeNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FirstName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LastName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.JobTitle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlSearch.SuspendLayout();
            this.panel1.SuspendLayout();
            this.bPanel_Rounded1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmployees)).BeginInit();
            this.SuspendLayout();
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
            this.txtSearch.Size = new System.Drawing.Size(444, 20);
            this.txtSearch.TabIndex = 2;
            this.txtSearch.Text = "Search";
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
            this.pnlSearch.Size = new System.Drawing.Size(497, 40);
            this.pnlSearch.TabIndex = 2;
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.Image = global::ThirdPartyTool.Properties.Resources.Icon_Search;
            this.btnSearch.Location = new System.Drawing.Point(460, 3);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(34, 34);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pnlSearch);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(500, 60);
            this.panel1.TabIndex = 7;
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
            this.bPanel_Rounded1.Size = new System.Drawing.Size(500, 340);
            this.bPanel_Rounded1.TabIndex = 8;
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
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvEmployees.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvEmployees.ColumnHeadersHeight = 40;
            this.dgvEmployees.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.PaycorEmployeeID,
            this.EmployeeNumber,
            this.EmployeeName,
            this.AlternateEmployeeNumber,
            this.BadgeNumber,
            this.FirstName,
            this.LastName,
            this.JobTitle});
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
            this.dgvEmployees.Size = new System.Drawing.Size(494, 334);
            this.dgvEmployees.TabIndex = 3;
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
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.White;
            this.btnClose.BackgroundColor = System.Drawing.Color.White;
            this.btnClose.BorderColor = System.Drawing.Color.Gray;
            this.btnClose.BorderRadius = 5;
            this.btnClose.BorderSize = 1;
            this.btnClose.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnClose.Font = new System.Drawing.Font("Roboto", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.Black;
            this.btnClose.Location = new System.Drawing.Point(0, 400);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(500, 50);
            this.btnClose.TabIndex = 9;
            this.btnClose.Text = "Close";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnClose.TextColor = System.Drawing.Color.Black;
            // 
            // PaycorEmployeeID
            // 
            this.PaycorEmployeeID.DataPropertyName = "PaycorEmployeeID";
            this.PaycorEmployeeID.HeaderText = "Column1";
            this.PaycorEmployeeID.Name = "PaycorEmployeeID";
            this.PaycorEmployeeID.ReadOnly = true;
            this.PaycorEmployeeID.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.PaycorEmployeeID.Visible = false;
            // 
            // EmployeeNumber
            // 
            this.EmployeeNumber.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.EmployeeNumber.DataPropertyName = "EmployeeNumber";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            this.EmployeeNumber.DefaultCellStyle = dataGridViewCellStyle2;
            this.EmployeeNumber.HeaderText = "ID";
            this.EmployeeNumber.MinimumWidth = 100;
            this.EmployeeNumber.Name = "EmployeeNumber";
            this.EmployeeNumber.ReadOnly = true;
            this.EmployeeNumber.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // EmployeeName
            // 
            this.EmployeeName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.EmployeeName.DataPropertyName = "EmployeeName";
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EmployeeName.DefaultCellStyle = dataGridViewCellStyle3;
            this.EmployeeName.HeaderText = "Name";
            this.EmployeeName.Name = "EmployeeName";
            this.EmployeeName.ReadOnly = true;
            // 
            // AlternateEmployeeNumber
            // 
            this.AlternateEmployeeNumber.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.AlternateEmployeeNumber.DataPropertyName = "AlternateEmployeeNumber";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Roboto", 12F);
            this.AlternateEmployeeNumber.DefaultCellStyle = dataGridViewCellStyle4;
            this.AlternateEmployeeNumber.HeaderText = "AlternateEmployeeNumber";
            this.AlternateEmployeeNumber.Name = "AlternateEmployeeNumber";
            this.AlternateEmployeeNumber.ReadOnly = true;
            this.AlternateEmployeeNumber.Visible = false;
            // 
            // BadgeNumber
            // 
            this.BadgeNumber.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.BadgeNumber.DataPropertyName = "BadgeNumber";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Roboto", 12F);
            this.BadgeNumber.DefaultCellStyle = dataGridViewCellStyle5;
            this.BadgeNumber.HeaderText = "BadgeNumber";
            this.BadgeNumber.Name = "BadgeNumber";
            this.BadgeNumber.ReadOnly = true;
            this.BadgeNumber.Visible = false;
            // 
            // FirstName
            // 
            this.FirstName.DataPropertyName = "FirstName";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Roboto", 12F);
            this.FirstName.DefaultCellStyle = dataGridViewCellStyle6;
            this.FirstName.HeaderText = "FirstName";
            this.FirstName.Name = "FirstName";
            this.FirstName.ReadOnly = true;
            this.FirstName.Visible = false;
            // 
            // LastName
            // 
            this.LastName.DataPropertyName = "LastName";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Roboto", 12F);
            this.LastName.DefaultCellStyle = dataGridViewCellStyle7;
            this.LastName.HeaderText = "LastName";
            this.LastName.Name = "LastName";
            this.LastName.ReadOnly = true;
            this.LastName.Visible = false;
            // 
            // JobTitle
            // 
            this.JobTitle.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.JobTitle.DataPropertyName = "JobTitle";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Roboto", 12F);
            this.JobTitle.DefaultCellStyle = dataGridViewCellStyle8;
            this.JobTitle.HeaderText = "Job";
            this.JobTitle.MinimumWidth = 200;
            this.JobTitle.Name = "JobTitle";
            this.JobTitle.ReadOnly = true;
            this.JobTitle.Width = 200;
            // 
            // EmployeeLinkPaycorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(500, 450);
            this.Controls.Add(this.bPanel_Rounded1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "EmployeeLinkPaycorForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "EmployeeLinkPaycorForm";
            this.pnlSearch.ResumeLayout(false);
            this.pnlSearch.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.bPanel_Rounded1.ResumeLayout(false);
            this.bPanel_Rounded1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmployees)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn1;
        private CustomControl.BTextBox txtSearch;
        private System.Windows.Forms.Label btnSearch;
        private CustomControl.BPanel_Rounded pnlSearch;
        private System.Windows.Forms.Panel panel1;
        private CustomControl.BPanel_Rounded bPanel_Rounded1;
        private System.Windows.Forms.DataGridView dgvEmployees;
        public CustomControl.BTextBox txtFocus;
        private CustomControl.BLabel_Rounded btnClose;
        private System.Windows.Forms.DataGridViewTextBoxColumn PaycorEmployeeID;
        private System.Windows.Forms.DataGridViewTextBoxColumn EmployeeNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn EmployeeName;
        private System.Windows.Forms.DataGridViewTextBoxColumn AlternateEmployeeNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn BadgeNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn FirstName;
        private System.Windows.Forms.DataGridViewTextBoxColumn LastName;
        private System.Windows.Forms.DataGridViewTextBoxColumn JobTitle;
    }
}