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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgvBatch = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnRefresh = new ThirdPartyTool.CustomControl.BLabel_Rounded();
            this.btnSettle = new ThirdPartyTool.CustomControl.BLabel_Rounded();
            this.label1 = new System.Windows.Forms.Label();
            this.MerchantID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BatchID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WorkstationName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CountTicket = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TerminalIpAddress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SettleBatch = new System.Windows.Forms.DataGridViewButtonColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBatch)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.dgvBatch);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(812, 510);
            this.panel1.TabIndex = 1;
            // 
            // dgvBatch
            // 
            this.dgvBatch.AllowUserToAddRows = false;
            this.dgvBatch.AllowUserToDeleteRows = false;
            this.dgvBatch.AllowUserToResizeColumns = false;
            this.dgvBatch.AllowUserToResizeRows = false;
            this.dgvBatch.BackgroundColor = System.Drawing.Color.White;
            this.dgvBatch.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvBatch.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.RaisedHorizontal;
            this.dgvBatch.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Roboto", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvBatch.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvBatch.ColumnHeadersHeight = 40;
            this.dgvBatch.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvBatch.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.MerchantID,
            this.BatchID,
            this.WorkstationName,
            this.CountTicket,
            this.TerminalIpAddress,
            this.SettleBatch});
            this.dgvBatch.Cursor = System.Windows.Forms.Cursors.Default;
            this.dgvBatch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvBatch.EnableHeadersVisualStyles = false;
            this.dgvBatch.Location = new System.Drawing.Point(0, 0);
            this.dgvBatch.MultiSelect = false;
            this.dgvBatch.Name = "dgvBatch";
            this.dgvBatch.ReadOnly = true;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Roboto Condensed", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvBatch.RowHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.dgvBatch.RowHeadersVisible = false;
            this.dgvBatch.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvBatch.RowTemplate.Height = 40;
            this.dgvBatch.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvBatch.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvBatch.Size = new System.Drawing.Size(810, 387);
            this.dgvBatch.TabIndex = 6;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.btnRefresh);
            this.panel2.Controls.Add(this.btnSettle);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 387);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(810, 121);
            this.panel2.TabIndex = 5;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnRefresh.BackColor = System.Drawing.Color.White;
            this.btnRefresh.BackgroundColor = System.Drawing.Color.White;
            this.btnRefresh.BorderColor = System.Drawing.Color.Gray;
            this.btnRefresh.BorderRadius = 5;
            this.btnRefresh.BorderSize = 1;
            this.btnRefresh.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefresh.ForeColor = System.Drawing.Color.Black;
            this.btnRefresh.Location = new System.Drawing.Point(100, 51);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(300, 50);
            this.btnRefresh.TabIndex = 4;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnRefresh.TextColor = System.Drawing.Color.Black;
            // 
            // btnSettle
            // 
            this.btnSettle.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnSettle.BackColor = System.Drawing.Color.White;
            this.btnSettle.BackgroundColor = System.Drawing.Color.White;
            this.btnSettle.BorderColor = System.Drawing.Color.Gray;
            this.btnSettle.BorderRadius = 5;
            this.btnSettle.BorderSize = 1;
            this.btnSettle.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSettle.ForeColor = System.Drawing.Color.Black;
            this.btnSettle.Location = new System.Drawing.Point(410, 51);
            this.btnSettle.Name = "btnSettle";
            this.btnSettle.Size = new System.Drawing.Size(300, 50);
            this.btnSettle.TabIndex = 2;
            this.btnSettle.Text = "Settle All Batch Offline Now";
            this.btnSettle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnSettle.TextColor = System.Drawing.Color.Black;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(164, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(466, 19);
            this.label1.TabIndex = 5;
            this.label1.Text = "Cần phải settle batch trên HOST trước khi thực hiện settle ở đây";
            // 
            // MerchantID
            // 
            this.MerchantID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.MerchantID.DataPropertyName = "MerchantID";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Roboto", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            this.MerchantID.DefaultCellStyle = dataGridViewCellStyle2;
            this.MerchantID.HeaderText = "Merchant ID";
            this.MerchantID.Name = "MerchantID";
            this.MerchantID.ReadOnly = true;
            this.MerchantID.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.MerchantID.Width = 150;
            // 
            // BatchID
            // 
            this.BatchID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.BatchID.DataPropertyName = "BatchID";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Roboto", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            this.BatchID.DefaultCellStyle = dataGridViewCellStyle3;
            this.BatchID.HeaderText = "Batch ID";
            this.BatchID.Name = "BatchID";
            this.BatchID.ReadOnly = true;
            // 
            // WorkstationName
            // 
            this.WorkstationName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.WorkstationName.DataPropertyName = "WorkstationName";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Roboto", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
            this.WorkstationName.DefaultCellStyle = dataGridViewCellStyle4;
            this.WorkstationName.HeaderText = "Workstation Name";
            this.WorkstationName.Name = "WorkstationName";
            this.WorkstationName.ReadOnly = true;
            this.WorkstationName.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // CountTicket
            // 
            this.CountTicket.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.CountTicket.DataPropertyName = "CountTicket";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Roboto", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
            this.CountTicket.DefaultCellStyle = dataGridViewCellStyle5;
            this.CountTicket.HeaderText = "Quantity";
            this.CountTicket.Name = "CountTicket";
            this.CountTicket.ReadOnly = true;
            this.CountTicket.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.CountTicket.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // TerminalIpAddress
            // 
            this.TerminalIpAddress.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.TerminalIpAddress.DataPropertyName = "TerminalIpAddress";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Roboto", 11F);
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.Black;
            this.TerminalIpAddress.DefaultCellStyle = dataGridViewCellStyle6;
            this.TerminalIpAddress.HeaderText = "IP Address";
            this.TerminalIpAddress.Name = "TerminalIpAddress";
            this.TerminalIpAddress.ReadOnly = true;
            this.TerminalIpAddress.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.TerminalIpAddress.Width = 150;
            // 
            // SettleBatch
            // 
            this.SettleBatch.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.SettleBatch.DataPropertyName = "SettleBatch";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Roboto", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle7.Padding = new System.Windows.Forms.Padding(5);
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.Black;
            this.SettleBatch.DefaultCellStyle = dataGridViewCellStyle7;
            this.SettleBatch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SettleBatch.HeaderText = "";
            this.SettleBatch.Name = "SettleBatch";
            this.SettleBatch.ReadOnly = true;
            this.SettleBatch.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.SettleBatch.Text = "Settle Now";
            // 
            // SettleBatchOfflineView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "SettleBatchOfflineView";
            this.Size = new System.Drawing.Size(812, 510);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBatch)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private CustomControl.BLabel_Rounded btnSettle;
        private CustomControl.BLabel_Rounded btnRefresh;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView dgvBatch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn MerchantID;
        private System.Windows.Forms.DataGridViewTextBoxColumn BatchID;
        private System.Windows.Forms.DataGridViewTextBoxColumn WorkstationName;
        private System.Windows.Forms.DataGridViewTextBoxColumn CountTicket;
        private System.Windows.Forms.DataGridViewTextBoxColumn TerminalIpAddress;
        private System.Windows.Forms.DataGridViewButtonColumn SettleBatch;
    }
}
