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
            System.Windows.Forms.DataGridViewCellStyle headerStyle = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle rowStyle = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle workstationStyle = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle statusStyle = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle openingTimeStyle = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle closingTimeStyle = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle openingAmountStyle = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle closingAmountStyle = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle editStyle = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.lblCount = new System.Windows.Forms.Label();
            this.lblFrom = new System.Windows.Forms.Label();
            this.startDate = new ThirdPartyTool.CustomControl.DateField();
            this.lblTo = new System.Windows.Forms.Label();
            this.endDate = new ThirdPartyTool.CustomControl.DateField();
            this.btnView = new ThirdPartyTool.CustomControl.BLabel_Rounded();
            this.pnlDivider = new System.Windows.Forms.Panel();
            this.pnlGrid = new System.Windows.Forms.Panel();
            this.lblEmpty = new System.Windows.Forms.Label();
            this.dgvShifts = new System.Windows.Forms.DataGridView();
            this.ShiftID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WorkstationName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OpeningTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ClosingTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OpeningAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ClosingAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EditShift = new System.Windows.Forms.DataGridViewImageColumn();
            this.pnlTop.SuspendLayout();
            this.pnlGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvShifts)).BeginInit();
            this.SuspendLayout();
            //
            // pnlTop
            //
            this.pnlTop.BackColor = System.Drawing.Color.White;
            this.pnlTop.Controls.Add(this.lblTitle);
            this.pnlTop.Controls.Add(this.lblSubtitle);
            this.pnlTop.Controls.Add(this.lblCount);
            this.pnlTop.Controls.Add(this.lblFrom);
            this.pnlTop.Controls.Add(this.startDate);
            this.pnlTop.Controls.Add(this.lblTo);
            this.pnlTop.Controls.Add(this.endDate);
            this.pnlTop.Controls.Add(this.btnView);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(837, 128);
            this.pnlTop.TabIndex = 0;
            //
            // lblTitle
            //
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.lblTitle.Location = new System.Drawing.Point(18, 14);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(66, 26);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Shifts";
            //
            // lblSubtitle
            //
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new System.Drawing.Font("Roboto", 9.75F);
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(117)))), ((int)(((byte)(117)))));
            this.lblSubtitle.Location = new System.Drawing.Point(21, 44);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(420, 16);
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "Shifts opened in the selected range. Open shifts are always listed.";
            //
            // lblCount
            //
            this.lblCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCount.Font = new System.Drawing.Font("Roboto", 10F);
            this.lblCount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(117)))), ((int)(((byte)(117)))));
            this.lblCount.Location = new System.Drawing.Point(557, 18);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(260, 22);
            this.lblCount.TabIndex = 2;
            this.lblCount.Text = "";
            this.lblCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            //
            // lblFrom
            //
            this.lblFrom.AutoSize = true;
            this.lblFrom.Font = new System.Drawing.Font("Roboto", 11F);
            this.lblFrom.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.lblFrom.Location = new System.Drawing.Point(18, 82);
            this.lblFrom.Name = "lblFrom";
            this.lblFrom.Size = new System.Drawing.Size(42, 18);
            this.lblFrom.TabIndex = 3;
            this.lblFrom.Text = "From";
            //
            // startDate
            //
            this.startDate.Location = new System.Drawing.Point(66, 72);
            this.startDate.Name = "startDate";
            this.startDate.Size = new System.Drawing.Size(170, 40);
            this.startDate.TabIndex = 4;
            //
            // lblTo
            //
            this.lblTo.AutoSize = true;
            this.lblTo.Font = new System.Drawing.Font("Roboto", 11F);
            this.lblTo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.lblTo.Location = new System.Drawing.Point(254, 82);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(24, 18);
            this.lblTo.TabIndex = 5;
            this.lblTo.Text = "To";
            //
            // endDate
            //
            this.endDate.Location = new System.Drawing.Point(286, 72);
            this.endDate.Name = "endDate";
            this.endDate.Size = new System.Drawing.Size(170, 40);
            this.endDate.TabIndex = 6;
            //
            // btnView
            //
            this.btnView.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(108)))), ((int)(((byte)(228)))));
            this.btnView.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(108)))), ((int)(((byte)(228)))));
            this.btnView.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(108)))), ((int)(((byte)(228)))));
            this.btnView.BorderRadius = 5;
            this.btnView.BorderSize = 1;
            this.btnView.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnView.Font = new System.Drawing.Font("Roboto", 11F, System.Drawing.FontStyle.Bold);
            this.btnView.ForeColor = System.Drawing.Color.White;
            this.btnView.Location = new System.Drawing.Point(472, 72);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(110, 40);
            this.btnView.TabIndex = 7;
            this.btnView.Text = "View";
            this.btnView.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnView.TextColor = System.Drawing.Color.White;
            //
            // pnlDivider
            //
            this.pnlDivider.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.pnlDivider.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlDivider.Location = new System.Drawing.Point(0, 128);
            this.pnlDivider.Name = "pnlDivider";
            this.pnlDivider.Size = new System.Drawing.Size(837, 1);
            this.pnlDivider.TabIndex = 1;
            //
            // pnlGrid
            //
            this.pnlGrid.BackColor = System.Drawing.Color.White;
            this.pnlGrid.Controls.Add(this.lblEmpty);
            this.pnlGrid.Controls.Add(this.dgvShifts);
            this.pnlGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGrid.Location = new System.Drawing.Point(0, 129);
            this.pnlGrid.Name = "pnlGrid";
            this.pnlGrid.Size = new System.Drawing.Size(837, 599);
            this.pnlGrid.TabIndex = 2;
            //
            // lblEmpty
            //
            this.lblEmpty.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblEmpty.BackColor = System.Drawing.Color.White;
            this.lblEmpty.Font = new System.Drawing.Font("Roboto", 11F);
            this.lblEmpty.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(117)))), ((int)(((byte)(117)))));
            this.lblEmpty.Location = new System.Drawing.Point(0, 44);
            this.lblEmpty.Name = "lblEmpty";
            this.lblEmpty.Size = new System.Drawing.Size(837, 555);
            this.lblEmpty.TabIndex = 1;
            this.lblEmpty.Text = "No shifts found for this date range.";
            this.lblEmpty.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblEmpty.Visible = false;
            //
            // dgvShifts
            //
            this.dgvShifts.AllowUserToAddRows = false;
            this.dgvShifts.AllowUserToDeleteRows = false;
            this.dgvShifts.AllowUserToResizeColumns = false;
            this.dgvShifts.AllowUserToResizeRows = false;
            this.dgvShifts.AutoGenerateColumns = false;
            this.dgvShifts.BackgroundColor = System.Drawing.Color.White;
            this.dgvShifts.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvShifts.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvShifts.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            headerStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            headerStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            headerStyle.Font = new System.Drawing.Font("Roboto", 10F, System.Drawing.FontStyle.Bold);
            headerStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(97)))), ((int)(((byte)(97)))), ((int)(((byte)(97)))));
            headerStyle.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            headerStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            headerStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(97)))), ((int)(((byte)(97)))), ((int)(((byte)(97)))));
            headerStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvShifts.ColumnHeadersDefaultCellStyle = headerStyle;
            this.dgvShifts.ColumnHeadersHeight = 44;
            this.dgvShifts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvShifts.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ShiftID,
            this.WorkstationName,
            this.Status,
            this.OpeningTime,
            this.ClosingTime,
            this.OpeningAmount,
            this.ClosingAmount,
            this.EditShift});
            this.dgvShifts.Cursor = System.Windows.Forms.Cursors.Default;
            rowStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            rowStyle.BackColor = System.Drawing.Color.White;
            rowStyle.Font = new System.Drawing.Font("Roboto", 10F);
            rowStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            rowStyle.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            rowStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(240)))), ((int)(((byte)(254)))));
            rowStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            rowStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvShifts.DefaultCellStyle = rowStyle;
            this.dgvShifts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvShifts.EnableHeadersVisualStyles = false;
            this.dgvShifts.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(235)))), ((int)(((byte)(235)))));
            this.dgvShifts.Location = new System.Drawing.Point(0, 0);
            this.dgvShifts.MultiSelect = false;
            this.dgvShifts.Name = "dgvShifts";
            this.dgvShifts.ReadOnly = true;
            this.dgvShifts.RowHeadersVisible = false;
            this.dgvShifts.RowTemplate.Height = 44;
            this.dgvShifts.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvShifts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvShifts.Size = new System.Drawing.Size(837, 599);
            this.dgvShifts.TabIndex = 0;
            //
            // ShiftID
            //
            this.ShiftID.DataPropertyName = "ShiftID";
            this.ShiftID.HeaderText = "ShiftID";
            this.ShiftID.Name = "ShiftID";
            this.ShiftID.ReadOnly = true;
            this.ShiftID.Visible = false;
            //
            // WorkstationName
            //
            this.WorkstationName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.WorkstationName.DataPropertyName = "WorkstationName";
            workstationStyle.Font = new System.Drawing.Font("Roboto", 10F);
            this.WorkstationName.DefaultCellStyle = workstationStyle;
            this.WorkstationName.HeaderText = "Workstation";
            this.WorkstationName.MinimumWidth = 140;
            this.WorkstationName.Name = "WorkstationName";
            this.WorkstationName.ReadOnly = true;
            this.WorkstationName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            //
            // Status
            //
            statusStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            statusStyle.Font = new System.Drawing.Font("Roboto", 9F, System.Drawing.FontStyle.Bold);
            this.Status.DefaultCellStyle = statusStyle;
            this.Status.HeaderText = "Status";
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
            this.Status.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Status.Width = 80;
            //
            // OpeningTime
            //
            this.OpeningTime.DataPropertyName = "OpeningTime";
            openingTimeStyle.Format = "M/d/yyyy h:mm tt";
            openingTimeStyle.Padding = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.OpeningTime.DefaultCellStyle = openingTimeStyle;
            this.OpeningTime.HeaderText = "Opening Time";
            this.OpeningTime.Name = "OpeningTime";
            this.OpeningTime.ReadOnly = true;
            this.OpeningTime.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.OpeningTime.Width = 160;
            //
            // ClosingTime
            //
            this.ClosingTime.DataPropertyName = "ClosingTime";
            closingTimeStyle.Format = "M/d/yyyy h:mm tt";
            closingTimeStyle.Padding = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.ClosingTime.DefaultCellStyle = closingTimeStyle;
            this.ClosingTime.HeaderText = "Closing Time";
            this.ClosingTime.Name = "ClosingTime";
            this.ClosingTime.ReadOnly = true;
            this.ClosingTime.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ClosingTime.Width = 160;
            //
            // OpeningAmount
            //
            this.OpeningAmount.DataPropertyName = "OpeningAmount";
            openingAmountStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            openingAmountStyle.Format = "N2";
            this.OpeningAmount.DefaultCellStyle = openingAmountStyle;
            this.OpeningAmount.HeaderCell.Style.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.OpeningAmount.HeaderText = "Opening Amount";
            this.OpeningAmount.Name = "OpeningAmount";
            this.OpeningAmount.ReadOnly = true;
            this.OpeningAmount.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.OpeningAmount.Width = 104;
            //
            // ClosingAmount
            //
            this.ClosingAmount.DataPropertyName = "ClosingAmount";
            closingAmountStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            closingAmountStyle.Format = "N2";
            this.ClosingAmount.DefaultCellStyle = closingAmountStyle;
            this.ClosingAmount.HeaderCell.Style.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.ClosingAmount.HeaderText = "Closing Amount";
            this.ClosingAmount.Name = "ClosingAmount";
            this.ClosingAmount.ReadOnly = true;
            this.ClosingAmount.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ClosingAmount.Width = 104;
            //
            // EditShift
            //
            editStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            editStyle.NullValue = global::ThirdPartyTool.Properties.Resources.icon_edit_2;
            editStyle.Padding = new System.Windows.Forms.Padding(0);
            this.EditShift.DefaultCellStyle = editStyle;
            this.EditShift.HeaderText = "";
            this.EditShift.Image = global::ThirdPartyTool.Properties.Resources.icon_edit_2;
            this.EditShift.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Normal;
            this.EditShift.Name = "EditShift";
            this.EditShift.ReadOnly = true;
            this.EditShift.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.EditShift.ToolTipText = "Edit amounts";
            this.EditShift.Width = 52;
            //
            // ShiftView
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.pnlGrid);
            this.Controls.Add(this.pnlDivider);
            this.Controls.Add(this.pnlTop);
            this.Name = "ShiftView";
            this.Size = new System.Drawing.Size(837, 728);
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.pnlGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvShifts)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.Label lblFrom;
        public ThirdPartyTool.CustomControl.DateField startDate;
        private System.Windows.Forms.Label lblTo;
        public ThirdPartyTool.CustomControl.DateField endDate;
        public CustomControl.BLabel_Rounded btnView;
        private System.Windows.Forms.Panel pnlDivider;
        private System.Windows.Forms.Panel pnlGrid;
        private System.Windows.Forms.Label lblEmpty;
        private System.Windows.Forms.DataGridView dgvShifts;
        private System.Windows.Forms.DataGridViewTextBoxColumn ShiftID;
        private System.Windows.Forms.DataGridViewTextBoxColumn WorkstationName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
        private System.Windows.Forms.DataGridViewTextBoxColumn OpeningTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn ClosingTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn OpeningAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn ClosingAmount;
        private System.Windows.Forms.DataGridViewImageColumn EditShift;
    }
}
