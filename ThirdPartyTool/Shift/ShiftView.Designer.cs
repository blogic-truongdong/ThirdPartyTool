namespace ThirdPartyTool.Shift
{
    partial class ShiftView
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShiftView));
            this.pnlShifts = new System.Windows.Forms.Panel();
            this.endDate = new BLogicSystems.POSBackOffice.Control.BUserControl.BDateTimePickerCustom();
            this.startDate = new BLogicSystems.POSBackOffice.Control.BUserControl.BDateTimePickerCustom();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dgvShifts = new System.Windows.Forms.DataGridView();
            this.ShiftID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WorkstationName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OpeningTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ClosingTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OpeningAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ClosingAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EditShift = new System.Windows.Forms.DataGridViewImageColumn();
            this.label6 = new System.Windows.Forms.Label();
            this.btnView = new ThirdPartyTool.CustomControl.BLabel_Rounded();
            this.pnlShifts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvShifts)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlShifts
            // 
            this.pnlShifts.BackColor = System.Drawing.Color.White;
            this.pnlShifts.Controls.Add(this.endDate);
            this.pnlShifts.Controls.Add(this.startDate);
            this.pnlShifts.Controls.Add(this.label1);
            this.pnlShifts.Controls.Add(this.label2);
            this.pnlShifts.Controls.Add(this.dgvShifts);
            this.pnlShifts.Controls.Add(this.label6);
            this.pnlShifts.Controls.Add(this.btnView);
            this.pnlShifts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlShifts.Location = new System.Drawing.Point(0, 0);
            this.pnlShifts.Name = "pnlShifts";
            this.pnlShifts.Padding = new System.Windows.Forms.Padding(0, 120, 0, 0);
            this.pnlShifts.Size = new System.Drawing.Size(800, 612);
            this.pnlShifts.TabIndex = 11;
            this.pnlShifts.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlShifts_Paint);
            // 
            // endDate
            // 
            this.endDate.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.endDate.DateTimeValues = new System.DateTime(2015, 12, 14, 11, 10, 0, 0);
            this.endDate.Location = new System.Drawing.Point(245, 65);
            this.endDate.Margin = new System.Windows.Forms.Padding(4);
            this.endDate.Name = "endDate";
            this.endDate.ShowCheckBox = true;
            this.endDate.ShowDate = true;
            this.endDate.Size = new System.Drawing.Size(248, 39);
            this.endDate.TabIndex = 683;
            this.endDate.WrongValues = false;
            // 
            // startDate
            // 
            this.startDate.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.startDate.DateTimeValues = new System.DateTime(2015, 12, 14, 11, 10, 0, 0);
            this.startDate.Location = new System.Drawing.Point(245, 20);
            this.startDate.Margin = new System.Windows.Forms.Padding(4);
            this.startDate.Name = "startDate";
            this.startDate.ShowCheckBox = true;
            this.startDate.ShowDate = true;
            this.startDate.Size = new System.Drawing.Size(248, 39);
            this.startDate.TabIndex = 683;
            this.startDate.WrongValues = false;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.BackColor = System.Drawing.Color.Gray;
            this.label1.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(0, 119);
            this.label1.Margin = new System.Windows.Forms.Padding(5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(800, 1);
            this.label1.TabIndex = 682;
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(153, 76);
            this.label2.Margin = new System.Windows.Forms.Padding(5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 19);
            this.label2.TabIndex = 678;
            this.label2.Text = "From Date";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label2.Click += new System.EventHandler(this.label6_Click);
            // 
            // dgvShifts
            // 
            this.dgvShifts.AllowUserToAddRows = false;
            this.dgvShifts.AllowUserToDeleteRows = false;
            this.dgvShifts.AllowUserToResizeColumns = false;
            this.dgvShifts.AllowUserToResizeRows = false;
            this.dgvShifts.BackgroundColor = System.Drawing.Color.White;
            this.dgvShifts.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvShifts.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgvShifts.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvShifts.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvShifts.ColumnHeadersHeight = 40;
            this.dgvShifts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvShifts.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ShiftID,
            this.WorkstationName,
            this.OpeningTime,
            this.ClosingTime,
            this.OpeningAmount,
            this.ClosingAmount,
            this.EditShift});
            this.dgvShifts.Cursor = System.Windows.Forms.Cursors.Default;
            this.dgvShifts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvShifts.EnableHeadersVisualStyles = false;
            this.dgvShifts.Location = new System.Drawing.Point(0, 120);
            this.dgvShifts.MultiSelect = false;
            this.dgvShifts.Name = "dgvShifts";
            this.dgvShifts.ReadOnly = true;
            this.dgvShifts.RowHeadersVisible = false;
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvShifts.RowsDefaultCellStyle = dataGridViewCellStyle8;
            this.dgvShifts.RowTemplate.Height = 40;
            this.dgvShifts.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvShifts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvShifts.Size = new System.Drawing.Size(800, 492);
            this.dgvShifts.TabIndex = 681;
            // 
            // ShiftID
            // 
            this.ShiftID.DataPropertyName = "ShiftID";
            this.ShiftID.HeaderText = "Column1";
            this.ShiftID.Name = "ShiftID";
            this.ShiftID.ReadOnly = true;
            this.ShiftID.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ShiftID.Visible = false;
            // 
            // WorkstationName
            // 
            this.WorkstationName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.WorkstationName.DataPropertyName = "WorkstationName";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Roboto", 10F);
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.WorkstationName.DefaultCellStyle = dataGridViewCellStyle2;
            this.WorkstationName.HeaderText = "Workstation";
            this.WorkstationName.Name = "WorkstationName";
            this.WorkstationName.ReadOnly = true;
            this.WorkstationName.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // OpeningTime
            // 
            this.OpeningTime.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.OpeningTime.DataPropertyName = "OpeningTime";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Roboto", 10F);
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.OpeningTime.DefaultCellStyle = dataGridViewCellStyle3;
            this.OpeningTime.HeaderText = "Opening Time";
            this.OpeningTime.Name = "OpeningTime";
            this.OpeningTime.ReadOnly = true;
            this.OpeningTime.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.OpeningTime.Width = 140;
            // 
            // ClosingTime
            // 
            this.ClosingTime.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ClosingTime.DataPropertyName = "ClosingTime";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Roboto", 10F);
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ClosingTime.DefaultCellStyle = dataGridViewCellStyle4;
            this.ClosingTime.HeaderText = "Closing Time";
            this.ClosingTime.Name = "ClosingTime";
            this.ClosingTime.ReadOnly = true;
            this.ClosingTime.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ClosingTime.Width = 140;
            // 
            // OpeningAmount
            // 
            this.OpeningAmount.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.OpeningAmount.DataPropertyName = "OpeningAmount";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Roboto", 10F);
            dataGridViewCellStyle5.Format = "c2";
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.OpeningAmount.DefaultCellStyle = dataGridViewCellStyle5;
            this.OpeningAmount.HeaderText = "Opening Amount";
            this.OpeningAmount.Name = "OpeningAmount";
            this.OpeningAmount.ReadOnly = true;
            this.OpeningAmount.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.OpeningAmount.Width = 120;
            // 
            // ClosingAmount
            // 
            this.ClosingAmount.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ClosingAmount.DataPropertyName = "ClosingAmount";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Roboto", 10F);
            dataGridViewCellStyle6.Format = "c2";
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ClosingAmount.DefaultCellStyle = dataGridViewCellStyle6;
            this.ClosingAmount.HeaderText = "Closing Amount";
            this.ClosingAmount.Name = "ClosingAmount";
            this.ClosingAmount.ReadOnly = true;
            this.ClosingAmount.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ClosingAmount.Width = 120;
            // 
            // EditShift
            // 
            this.EditShift.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.NullValue = ((object)(resources.GetObject("dataGridViewCellStyle7.NullValue")));
            this.EditShift.DefaultCellStyle = dataGridViewCellStyle7;
            this.EditShift.HeaderText = "";
            this.EditShift.Image = global::ThirdPartyTool.Properties.Resources.icon_edit_2;
            this.EditShift.Name = "EditShift";
            this.EditShift.ReadOnly = true;
            this.EditShift.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.EditShift.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.EditShift.Width = 80;
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(153, 31);
            this.label6.Margin = new System.Windows.Forms.Padding(5);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(83, 19);
            this.label6.TabIndex = 678;
            this.label6.Text = "From Date";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // btnView
            // 
            this.btnView.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnView.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnView.BackgroundColor = System.Drawing.SystemColors.Highlight;
            this.btnView.BorderColor = System.Drawing.Color.Gray;
            this.btnView.BorderRadius = 5;
            this.btnView.BorderSize = 1;
            this.btnView.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Bold);
            this.btnView.ForeColor = System.Drawing.Color.White;
            this.btnView.Location = new System.Drawing.Point(500, 20);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(150, 80);
            this.btnView.TabIndex = 676;
            this.btnView.Text = "View";
            this.btnView.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnView.TextColor = System.Drawing.Color.White;
            // 
            // ShiftView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlShifts);
            this.Name = "ShiftView";
            this.Size = new System.Drawing.Size(800, 612);
            this.pnlShifts.ResumeLayout(false);
            this.pnlShifts.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvShifts)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlShifts;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridView dgvShifts;
        private System.Windows.Forms.Label label1;
        public BLogicSystems.POSBackOffice.Control.BUserControl.BDateTimePickerCustom startDate;
        public BLogicSystems.POSBackOffice.Control.BUserControl.BDateTimePickerCustom endDate;
        private System.Windows.Forms.Label label2;
        public CustomControl.BLabel_Rounded btnView;
        private System.Windows.Forms.DataGridViewTextBoxColumn ShiftID;
        private System.Windows.Forms.DataGridViewTextBoxColumn WorkstationName;
        private System.Windows.Forms.DataGridViewTextBoxColumn OpeningTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn ClosingTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn OpeningAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn ClosingAmount;
        private System.Windows.Forms.DataGridViewImageColumn EditShift;
    }
}
