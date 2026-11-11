namespace ThirdPartyTool.SaleReceipt
{
    partial class SearchBatchSettledForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgvBatchs = new System.Windows.Forms.DataGridView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.endDate = new BLogicSystems.POSBackOffice.Control.BUserControl.BDateTimePickerCustom();
            this.startDate = new BLogicSystems.POSBackOffice.Control.BUserControl.BDateTimePickerCustom();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnSearch = new ThirdPartyTool.CustomControl.BLabel_Rounded();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnCancel = new RJCodeAdvance.RJControls.RJButton();
            this.label1 = new System.Windows.Forms.Label();
            this.BatchUnique = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DateCreated = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WorkStationName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MerchantID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Quantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Total = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SelectColumn = new System.Windows.Forms.DataGridViewButtonColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBatchs)).BeginInit();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            
            this.panel1.Controls.Add(this.dgvBatchs);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 450);
            this.panel1.TabIndex = 0;
            // 
            // dgvBatchs
            // 
            this.dgvBatchs.AllowUserToAddRows = false;
            this.dgvBatchs.AllowUserToDeleteRows = false;
            this.dgvBatchs.AllowUserToResizeColumns = false;
            this.dgvBatchs.AllowUserToResizeRows = false;
            this.dgvBatchs.BackgroundColor = System.Drawing.Color.White;
            this.dgvBatchs.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvBatchs.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvBatchs.GridColor = System.Drawing.Color.FromArgb(235, 235, 235);
            this.dgvBatchs.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(245, 247, 250);
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Roboto", 10F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(97, 97, 97);
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(245, 247, 250);
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(97, 97, 97);
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvBatchs.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvBatchs.ColumnHeadersHeight = 40;
            this.dgvBatchs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvBatchs.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.BatchUnique,
            this.DateCreated,
            this.WorkStationName,
            this.MerchantID,
            this.ID,
            this.Quantity,
            this.Total,
            this.SelectColumn});
            this.dgvBatchs.Cursor = System.Windows.Forms.Cursors.Default;
            this.dgvBatchs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvBatchs.EnableHeadersVisualStyles = false;
            this.dgvBatchs.Location = new System.Drawing.Point(0, 150);
            this.dgvBatchs.MultiSelect = false;
            this.dgvBatchs.Name = "dgvBatchs";
            this.dgvBatchs.ReadOnly = true;
            this.dgvBatchs.RowHeadersVisible = false;
            dataGridViewCellStyle9.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvBatchs.RowsDefaultCellStyle = dataGridViewCellStyle9;
            this.dgvBatchs.RowTemplate.Height = 40;
            this.dgvBatchs.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvBatchs.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvBatchs.Size = new System.Drawing.Size(798, 228);
            this.dgvBatchs.TabIndex = 682;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.Controls.Add(this.endDate);
            this.panel3.Controls.Add(this.startDate);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.label6);
            this.panel3.Controls.Add(this.btnSearch);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 50);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(798, 100);
            this.panel3.TabIndex = 23;
            // 
            // endDate
            // 
            this.endDate.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.endDate.DateTimeValues = new System.DateTime(2015, 12, 14, 11, 10, 0, 0);
            this.endDate.Location = new System.Drawing.Point(243, 53);
            this.endDate.Margin = new System.Windows.Forms.Padding(4);
            this.endDate.Name = "endDate";
            this.endDate.ShowCheckBox = true;
            this.endDate.ShowDate = true;
            this.endDate.Size = new System.Drawing.Size(248, 39);
            this.endDate.TabIndex = 687;
            this.endDate.WrongValues = false;
            // 
            // startDate
            // 
            this.startDate.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.startDate.DateTimeValues = new System.DateTime(2015, 12, 14, 11, 10, 0, 0);
            this.startDate.Location = new System.Drawing.Point(243, 8);
            this.startDate.Margin = new System.Windows.Forms.Padding(4);
            this.startDate.Name = "startDate";
            this.startDate.ShowCheckBox = true;
            this.startDate.ShowDate = true;
            this.startDate.Size = new System.Drawing.Size(248, 39);
            this.startDate.TabIndex = 688;
            this.startDate.WrongValues = false;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Roboto", 10F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(97, 97, 97);
            this.label2.Location = new System.Drawing.Point(151, 64);
            this.label2.Margin = new System.Windows.Forms.Padding(5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 19);
            this.label2.TabIndex = 685;
            this.label2.Text = "To";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Roboto", 10F, System.Drawing.FontStyle.Bold);
            this.label6.ForeColor = System.Drawing.Color.FromArgb(97, 97, 97);
            this.label6.Location = new System.Drawing.Point(151, 19);
            this.label6.Margin = new System.Windows.Forms.Padding(5);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(83, 19);
            this.label6.TabIndex = 686;
            this.label6.Text = "From";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(42, 108, 228);
            this.btnSearch.BackgroundColor = System.Drawing.Color.FromArgb(42, 108, 228);
            this.btnSearch.BorderColor = System.Drawing.Color.FromArgb(42, 108, 228);
            this.btnSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSearch.BorderRadius = 5;
            this.btnSearch.BorderSize = 1;
            this.btnSearch.Font = new System.Drawing.Font("Roboto", 11F, System.Drawing.FontStyle.Bold);
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(498, 8);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(150, 80);
            this.btnSearch.TabIndex = 684;
            this.btnSearch.Text = "Search";
            this.btnSearch.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnSearch.TextColor = System.Drawing.Color.White;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.btnCancel);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 378);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(798, 70);
            this.panel2.TabIndex = 22;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnCancel.BackColor = System.Drawing.Color.White;
            this.btnCancel.BackgroundColor = System.Drawing.Color.White;
            this.btnCancel.BorderColor = System.Drawing.Color.FromArgb(189, 189, 189);
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.BorderRadius = 8;
            this.btnCancel.BorderSize = 1;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Roboto", 11F, System.Drawing.FontStyle.Bold);
            this.btnCancel.ForeColor = System.Drawing.Color.FromArgb(66, 66, 66);
            this.btnCancel.Location = new System.Drawing.Point(329, 13);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(140, 44);
            this.btnCancel.TabIndex = 21;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.TextColor = System.Drawing.Color.FromArgb(66, 66, 66);
            this.btnCancel.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Bold);
            this.label1.Padding = new System.Windows.Forms.Padding(18, 0, 0, 0);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(33, 33, 33);
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(798, 50);
            this.label1.TabIndex = 1;
            this.label1.Text = "Find a settled batch";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // BatchUnique
            // 
            this.BatchUnique.DataPropertyName = "BatchUnique";
            this.BatchUnique.HeaderText = "BatchUnique";
            this.BatchUnique.Name = "BatchUnique";
            this.BatchUnique.ReadOnly = true;
            this.BatchUnique.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.BatchUnique.Visible = false;
            // 
            // DateCreated
            // 
            this.DateCreated.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.DateCreated.DataPropertyName = "DateCreated";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Roboto", 10F);
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DateCreated.DefaultCellStyle = dataGridViewCellStyle2;
            this.DateCreated.HeaderText = "Settled Date";
            this.DateCreated.Name = "DateCreated";
            this.DateCreated.ReadOnly = true;
            this.DateCreated.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // WorkStationName
            // 
            this.WorkStationName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.WorkStationName.DataPropertyName = "WorkStationName";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Roboto", 10F);
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.WorkStationName.DefaultCellStyle = dataGridViewCellStyle3;
            this.WorkStationName.HeaderText = "Workstation";
            this.WorkStationName.Name = "WorkStationName";
            this.WorkStationName.ReadOnly = true;
            this.WorkStationName.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // MerchantID
            // 
            this.MerchantID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.MerchantID.DataPropertyName = "MerchantID";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Roboto", 10F);
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.MerchantID.DefaultCellStyle = dataGridViewCellStyle4;
            this.MerchantID.HeaderText = "Terminal #";
            this.MerchantID.Name = "MerchantID";
            this.MerchantID.ReadOnly = true;
            this.MerchantID.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.MerchantID.Width = 110;
            // 
            // ID
            // 
            this.ID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ID.DataPropertyName = "ID";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Roboto", 10F);
            dataGridViewCellStyle5.Format = "C0";
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ID.DefaultCellStyle = dataGridViewCellStyle5;
            this.ID.HeaderText = "Batch #";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // Quantity
            // 
            this.Quantity.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Quantity.DataPropertyName = "Quantity";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Roboto", 10F);
            dataGridViewCellStyle6.Format = "c0";
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Quantity.DefaultCellStyle = dataGridViewCellStyle6;
            this.Quantity.HeaderText = "# Trans";
            this.Quantity.Name = "Quantity";
            this.Quantity.ReadOnly = true;
            this.Quantity.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // Total
            // 
            this.Total.DataPropertyName = "Total";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.Format = "C2";
            this.Total.DefaultCellStyle = dataGridViewCellStyle7;
            this.Total.HeaderText = "Total";
            this.Total.Name = "Total";
            this.Total.ReadOnly = true;
            // 
            // SelectColumn
            // 
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SelectColumn.DefaultCellStyle = dataGridViewCellStyle8;
            this.SelectColumn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SelectColumn.HeaderText = "";
            this.SelectColumn.Name = "SelectColumn";
            this.SelectColumn.ReadOnly = true;
            this.SelectColumn.Text = "Select";
            // 
            // SearchBatchSettledForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel1);
            this.BackColor = System.Drawing.Color.FromArgb(189, 189, 189);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Padding = new System.Windows.Forms.Padding(1);
            this.Name = "SearchBatchSettledForm";
            this.Text = "SearchBatchSettledForm";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBatchs)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private RJCodeAdvance.RJControls.RJButton btnCancel;
        public BLogicSystems.POSBackOffice.Control.BUserControl.BDateTimePickerCustom endDate;
        public BLogicSystems.POSBackOffice.Control.BUserControl.BDateTimePickerCustom startDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
        public CustomControl.BLabel_Rounded btnSearch;
        private System.Windows.Forms.DataGridView dgvBatchs;
        private System.Windows.Forms.DataGridViewTextBoxColumn BatchUnique;
        private System.Windows.Forms.DataGridViewTextBoxColumn DateCreated;
        private System.Windows.Forms.DataGridViewTextBoxColumn WorkStationName;
        private System.Windows.Forms.DataGridViewTextBoxColumn MerchantID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Quantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn Total;
        private System.Windows.Forms.DataGridViewButtonColumn SelectColumn;
    }
}