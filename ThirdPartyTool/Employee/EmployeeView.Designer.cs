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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnRefresh = new System.Windows.Forms.Label();
            this.bPanel_Rounded3 = new ThirdPartyTool.CustomControl.BPanel_Rounded();
            this.bPanel_Rounded1 = new ThirdPartyTool.CustomControl.BPanel_Rounded();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlSearch = new ThirdPartyTool.CustomControl.BPanel_Rounded();
            this.txtSearch = new ThirdPartyTool.CustomControl.BTextBox();
            this.btnSearch = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.bPanel_Rounded1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.pnlSearch.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnRefresh);
            this.panel1.Controls.Add(this.pnlSearch);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(10, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(644, 60);
            this.panel1.TabIndex = 3;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Bold);
            this.btnRefresh.Location = new System.Drawing.Point(342, 5);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(177, 40);
            this.btnRefresh.TabIndex = 3;
            this.btnRefresh.Text = "Reload";
            this.btnRefresh.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // bPanel_Rounded3
            // 
            this.bPanel_Rounded3.BackgroundColor = System.Drawing.Color.Transparent;
            this.bPanel_Rounded3.BorderColor = System.Drawing.Color.Gray;
            this.bPanel_Rounded3.BorderRadius = 5;
            this.bPanel_Rounded3.BorderSize = 1;
            this.bPanel_Rounded3.Location = new System.Drawing.Point(417, 136);
            this.bPanel_Rounded3.Name = "bPanel_Rounded3";
            this.bPanel_Rounded3.Size = new System.Drawing.Size(200, 100);
            this.bPanel_Rounded3.TabIndex = 1;
            // 
            // bPanel_Rounded1
            // 
            this.bPanel_Rounded1.BackColor = System.Drawing.Color.White;
            this.bPanel_Rounded1.BackgroundColor = System.Drawing.Color.Transparent;
            this.bPanel_Rounded1.BorderColor = System.Drawing.Color.Gray;
            this.bPanel_Rounded1.BorderRadius = 5;
            this.bPanel_Rounded1.BorderSize = 1;
            this.bPanel_Rounded1.Controls.Add(this.dataGridView1);
            this.bPanel_Rounded1.Controls.Add(this.label3);
            this.bPanel_Rounded1.Controls.Add(this.label1);
            this.bPanel_Rounded1.Dock = System.Windows.Forms.DockStyle.Left;
            this.bPanel_Rounded1.Location = new System.Drawing.Point(10, 60);
            this.bPanel_Rounded1.Name = "bPanel_Rounded1";
            this.bPanel_Rounded1.Padding = new System.Windows.Forms.Padding(3, 50, 3, 3);
            this.bPanel_Rounded1.Size = new System.Drawing.Size(300, 454);
            this.bPanel_Rounded1.TabIndex = 0;
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Cursor = System.Windows.Forms.Cursors.Default;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 50);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(294, 401);
            this.dataGridView1.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(103, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(177, 40);
            this.label3.TabIndex = 2;
            this.label3.Text = "Name";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 40);
            this.label1.TabIndex = 0;
            this.label1.Text = "label1";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlSearch
            // 
            this.pnlSearch.BackgroundColor = System.Drawing.Color.Transparent;
            this.pnlSearch.BorderColor = System.Drawing.Color.Gray;
            this.pnlSearch.BorderRadius = 5;
            this.pnlSearch.BorderSize = 1;
            this.pnlSearch.Controls.Add(this.txtSearch);
            this.pnlSearch.Controls.Add(this.btnSearch);
            this.pnlSearch.Location = new System.Drawing.Point(8, 8);
            this.pnlSearch.Name = "pnlSearch";
            this.pnlSearch.Size = new System.Drawing.Size(292, 40);
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
            this.txtSearch.Size = new System.Drawing.Size(239, 20);
            this.txtSearch.TabIndex = 2;
            this.txtSearch.Text = "Search";
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.Image = global::ThirdPartyTool.Properties.Resources.Icon_Search;
            this.btnSearch.Location = new System.Drawing.Point(255, 3);
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
            this.Controls.Add(this.bPanel_Rounded3);
            this.Controls.Add(this.bPanel_Rounded1);
            this.Controls.Add(this.panel1);
            this.Name = "EmployeeView";
            this.Padding = new System.Windows.Forms.Padding(10, 0, 10, 10);
            this.Size = new System.Drawing.Size(664, 524);
            this.panel1.ResumeLayout(false);
            this.bPanel_Rounded1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
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
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label btnRefresh;
    }
}
