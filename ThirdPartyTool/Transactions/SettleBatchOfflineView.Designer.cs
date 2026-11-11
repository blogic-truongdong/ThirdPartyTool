namespace ThirdPartyTool.Transactions
{
    partial class SettleBatchOfflineView
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
            System.Windows.Forms.DataGridViewCellStyle mutedStyle = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle countStyle = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle actionStyle = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.lblCount = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Label();
            this.pnlHeaderDivider = new System.Windows.Forms.Panel();
            this.pnlBody = new System.Windows.Forms.Panel();
            this.pnlGrid = new System.Windows.Forms.Panel();
            this.lblEmpty = new System.Windows.Forms.Label();
            this.dgvBatch = new System.Windows.Forms.DataGridView();
            this.MerchantID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BatchID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WorkstationName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TerminalIpAddress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CountTicket = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SettleBatch = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlNoticeGap = new System.Windows.Forms.Panel();
            this.pnlNotice = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlFooterDivider = new System.Windows.Forms.Panel();
            this.pnlFooter = new System.Windows.Forms.Panel();
            this.lblFooterHint = new System.Windows.Forms.Label();
            this.btnSettle = new ThirdPartyTool.CustomControl.BLabel_Rounded();
            this.pnlHeader.SuspendLayout();
            this.pnlBody.SuspendLayout();
            this.pnlGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBatch)).BeginInit();
            this.pnlNotice.SuspendLayout();
            this.pnlFooter.SuspendLayout();
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
            this.lblTitle.Size = new System.Drawing.Size(178, 26);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Offline batches";
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
            this.lblSubtitle.Text = "Card transactions not yet settled, grouped by merchant.";
            //
            // lblCount
            //
            this.lblCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCount.Font = new System.Drawing.Font("Roboto", 10F);
            this.lblCount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(117)))), ((int)(((byte)(117)))));
            this.lblCount.Location = new System.Drawing.Point(447, 21);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(320, 22);
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
            this.toolTip.SetToolTip(this.btnRefresh, "Reload batches");
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
            // pnlBody
            //
            this.pnlBody.BackColor = System.Drawing.Color.White;
            this.pnlBody.Controls.Add(this.pnlGrid);
            this.pnlBody.Controls.Add(this.pnlNoticeGap);
            this.pnlBody.Controls.Add(this.pnlNotice);
            this.pnlBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBody.Location = new System.Drawing.Point(0, 65);
            this.pnlBody.Name = "pnlBody";
            this.pnlBody.Padding = new System.Windows.Forms.Padding(16, 12, 16, 12);
            this.pnlBody.Size = new System.Drawing.Size(837, 594);
            this.pnlBody.TabIndex = 2;
            //
            // pnlNotice
            //
            this.pnlNotice.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(248)))), ((int)(((byte)(225)))));
            this.pnlNotice.Controls.Add(this.label1);
            this.pnlNotice.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlNotice.Location = new System.Drawing.Point(16, 12);
            this.pnlNotice.Name = "pnlNotice";
            this.pnlNotice.Padding = new System.Windows.Forms.Padding(16, 0, 12, 0);
            this.pnlNotice.Size = new System.Drawing.Size(805, 44);
            this.pnlNotice.TabIndex = 0;
            //
            // label1
            //
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Roboto", 10.5F);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(133)))), ((int)(((byte)(77)))), ((int)(((byte)(14)))));
            this.label1.Location = new System.Drawing.Point(16, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(777, 44);
            this.label1.TabIndex = 0;
            this.label1.Text = "Settle the batch on the HOST first, then settle it here.";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // pnlNoticeGap
            //
            this.pnlNoticeGap.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlNoticeGap.Location = new System.Drawing.Point(16, 56);
            this.pnlNoticeGap.Name = "pnlNoticeGap";
            this.pnlNoticeGap.Size = new System.Drawing.Size(805, 12);
            this.pnlNoticeGap.TabIndex = 1;
            //
            // pnlGrid
            //
            this.pnlGrid.BackColor = System.Drawing.Color.White;
            this.pnlGrid.Controls.Add(this.lblEmpty);
            this.pnlGrid.Controls.Add(this.dgvBatch);
            this.pnlGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGrid.Location = new System.Drawing.Point(16, 68);
            this.pnlGrid.Name = "pnlGrid";
            this.pnlGrid.Size = new System.Drawing.Size(805, 514);
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
            this.lblEmpty.Location = new System.Drawing.Point(0, 41);
            this.lblEmpty.Name = "lblEmpty";
            this.lblEmpty.Size = new System.Drawing.Size(805, 473);
            this.lblEmpty.TabIndex = 1;
            this.lblEmpty.Text = "No offline batches waiting to be settled.";
            this.lblEmpty.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblEmpty.Visible = false;
            //
            // dgvBatch
            //
            this.dgvBatch.AllowUserToAddRows = false;
            this.dgvBatch.AllowUserToDeleteRows = false;
            this.dgvBatch.AllowUserToResizeColumns = false;
            this.dgvBatch.AllowUserToResizeRows = false;
            this.dgvBatch.AutoGenerateColumns = false;
            this.dgvBatch.BackgroundColor = System.Drawing.Color.White;
            this.dgvBatch.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvBatch.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvBatch.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            headerStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            headerStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            headerStyle.Font = new System.Drawing.Font("Roboto", 10F, System.Drawing.FontStyle.Bold);
            headerStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(97)))), ((int)(((byte)(97)))), ((int)(((byte)(97)))));
            headerStyle.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            headerStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            headerStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(97)))), ((int)(((byte)(97)))), ((int)(((byte)(97)))));
            headerStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvBatch.ColumnHeadersDefaultCellStyle = headerStyle;
            this.dgvBatch.ColumnHeadersHeight = 40;
            this.dgvBatch.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvBatch.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.MerchantID,
            this.BatchID,
            this.WorkstationName,
            this.TerminalIpAddress,
            this.CountTicket,
            this.SettleBatch});
            this.dgvBatch.Cursor = System.Windows.Forms.Cursors.Default;
            rowStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            rowStyle.BackColor = System.Drawing.Color.White;
            rowStyle.Font = new System.Drawing.Font("Roboto", 10.5F);
            rowStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            rowStyle.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            rowStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(240)))), ((int)(((byte)(254)))));
            rowStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            rowStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvBatch.DefaultCellStyle = rowStyle;
            this.dgvBatch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvBatch.EnableHeadersVisualStyles = false;
            this.dgvBatch.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(235)))), ((int)(((byte)(235)))));
            this.dgvBatch.Location = new System.Drawing.Point(0, 0);
            this.dgvBatch.MultiSelect = false;
            this.dgvBatch.Name = "dgvBatch";
            this.dgvBatch.ReadOnly = true;
            this.dgvBatch.RowHeadersVisible = false;
            this.dgvBatch.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvBatch.RowTemplate.Height = 48;
            this.dgvBatch.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvBatch.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvBatch.Size = new System.Drawing.Size(805, 514);
            this.dgvBatch.TabIndex = 0;
            //
            // MerchantID
            //
            this.MerchantID.DataPropertyName = "MerchantID";
            this.MerchantID.HeaderText = "Merchant ID";
            this.MerchantID.Name = "MerchantID";
            this.MerchantID.ReadOnly = true;
            this.MerchantID.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.MerchantID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.MerchantID.Width = 140;
            //
            // BatchID
            //
            this.BatchID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.BatchID.DataPropertyName = "BatchID";
            this.BatchID.FillWeight = 40F;
            this.BatchID.HeaderText = "Batch";
            this.BatchID.MinimumWidth = 80;
            this.BatchID.Name = "BatchID";
            this.BatchID.ReadOnly = true;
            this.BatchID.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.BatchID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            //
            // WorkstationName
            //
            this.WorkstationName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.WorkstationName.DataPropertyName = "WorkstationName";
            this.WorkstationName.FillWeight = 60F;
            this.WorkstationName.HeaderText = "Workstation";
            this.WorkstationName.MinimumWidth = 120;
            this.WorkstationName.Name = "WorkstationName";
            this.WorkstationName.ReadOnly = true;
            this.WorkstationName.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.WorkstationName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            //
            // TerminalIpAddress
            //
            this.TerminalIpAddress.DataPropertyName = "TerminalIpAddress";
            mutedStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(97)))), ((int)(((byte)(97)))), ((int)(((byte)(97)))));
            mutedStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(97)))), ((int)(((byte)(97)))), ((int)(((byte)(97)))));
            this.TerminalIpAddress.DefaultCellStyle = mutedStyle;
            this.TerminalIpAddress.HeaderText = "Terminal IP";
            this.TerminalIpAddress.Name = "TerminalIpAddress";
            this.TerminalIpAddress.ReadOnly = true;
            this.TerminalIpAddress.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.TerminalIpAddress.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.TerminalIpAddress.Width = 140;
            //
            // CountTicket
            //
            this.CountTicket.DataPropertyName = "CountTicket";
            countStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.CountTicket.DefaultCellStyle = countStyle;
            this.CountTicket.HeaderText = "Transactions";
            this.CountTicket.Name = "CountTicket";
            this.CountTicket.ReadOnly = true;
            this.CountTicket.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.CountTicket.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.CountTicket.Width = 116;
            //
            // SettleBatch
            //
            actionStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.SettleBatch.DefaultCellStyle = actionStyle;
            this.SettleBatch.HeaderText = "";
            this.SettleBatch.Name = "SettleBatch";
            this.SettleBatch.ReadOnly = true;
            this.SettleBatch.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.SettleBatch.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.SettleBatch.Width = 104;
            //
            // pnlFooterDivider
            //
            this.pnlFooterDivider.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.pnlFooterDivider.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooterDivider.Location = new System.Drawing.Point(0, 659);
            this.pnlFooterDivider.Name = "pnlFooterDivider";
            this.pnlFooterDivider.Size = new System.Drawing.Size(837, 1);
            this.pnlFooterDivider.TabIndex = 3;
            //
            // pnlFooter
            //
            this.pnlFooter.BackColor = System.Drawing.Color.White;
            this.pnlFooter.Controls.Add(this.lblFooterHint);
            this.pnlFooter.Controls.Add(this.btnSettle);
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Location = new System.Drawing.Point(0, 660);
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Size = new System.Drawing.Size(837, 68);
            this.pnlFooter.TabIndex = 4;
            //
            // lblFooterHint
            //
            this.lblFooterHint.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFooterHint.Font = new System.Drawing.Font("Roboto", 9.75F);
            this.lblFooterHint.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(117)))), ((int)(((byte)(117)))));
            this.lblFooterHint.Location = new System.Drawing.Point(16, 14);
            this.lblFooterHint.Name = "lblFooterHint";
            this.lblFooterHint.Size = new System.Drawing.Size(560, 40);
            this.lblFooterHint.TabIndex = 0;
            this.lblFooterHint.Text = "Settles every merchant in the list at once.";
            this.lblFooterHint.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // btnSettle
            //
            this.btnSettle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSettle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(108)))), ((int)(((byte)(228)))));
            this.btnSettle.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(108)))), ((int)(((byte)(228)))));
            this.btnSettle.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(108)))), ((int)(((byte)(228)))));
            this.btnSettle.BorderRadius = 5;
            this.btnSettle.BorderSize = 1;
            this.btnSettle.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSettle.Font = new System.Drawing.Font("Roboto", 11F, System.Drawing.FontStyle.Bold);
            this.btnSettle.ForeColor = System.Drawing.Color.White;
            this.btnSettle.Location = new System.Drawing.Point(601, 12);
            this.btnSettle.Name = "btnSettle";
            this.btnSettle.Size = new System.Drawing.Size(220, 44);
            this.btnSettle.TabIndex = 1;
            this.btnSettle.Text = "Settle all batches";
            this.btnSettle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnSettle.TextColor = System.Drawing.Color.White;
            //
            // SettleBatchOfflineView
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.pnlBody);
            this.Controls.Add(this.pnlFooterDivider);
            this.Controls.Add(this.pnlFooter);
            this.Controls.Add(this.pnlHeaderDivider);
            this.Controls.Add(this.pnlHeader);
            this.Name = "SettleBatchOfflineView";
            this.Size = new System.Drawing.Size(837, 728);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlBody.ResumeLayout(false);
            this.pnlGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBatch)).EndInit();
            this.pnlNotice.ResumeLayout(false);
            this.pnlFooter.ResumeLayout(false);
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
        private System.Windows.Forms.Panel pnlBody;
        private System.Windows.Forms.Panel pnlNotice;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnlNoticeGap;
        private System.Windows.Forms.Panel pnlGrid;
        private System.Windows.Forms.Label lblEmpty;
        private System.Windows.Forms.DataGridView dgvBatch;
        private System.Windows.Forms.Panel pnlFooterDivider;
        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.Label lblFooterHint;
        private CustomControl.BLabel_Rounded btnSettle;
        private System.Windows.Forms.DataGridViewTextBoxColumn MerchantID;
        private System.Windows.Forms.DataGridViewTextBoxColumn BatchID;
        private System.Windows.Forms.DataGridViewTextBoxColumn WorkstationName;
        private System.Windows.Forms.DataGridViewTextBoxColumn TerminalIpAddress;
        private System.Windows.Forms.DataGridViewTextBoxColumn CountTicket;
        private System.Windows.Forms.DataGridViewTextBoxColumn SettleBatch;
    }
}
