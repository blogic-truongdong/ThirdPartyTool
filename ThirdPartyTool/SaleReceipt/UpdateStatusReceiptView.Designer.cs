namespace ThirdPartyTool.SaleReceipt
{
    partial class UpdateStatusReceiptView
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
            this.label1 = new System.Windows.Forms.Label();
            this.PrintControl_Receipt = new DevExpress.XtraPrinting.Control.PrintControl();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.pnlSearch = new ThirdPartyTool.CustomControl.BPanel_Rounded();
            this.txtSearch = new ThirdPartyTool.CustomControl.BTextBox();
            this.btnSearch = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.BtnUpdate = new ThirdPartyTool.CustomControl.BLabel_Rounded();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnBack = new ThirdPartyTool.CustomControl.BLabel_Rounded();
            this.bTextBox1 = new ThirdPartyTool.CustomControl.BTextBox();
            this.txtFocus = new ThirdPartyTool.CustomControl.BTextBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.pnlSearch.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(388, 30);
            this.label1.TabIndex = 28;
            this.label1.Text = "Receipt";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PrintControl_Receipt
            // 
            this.PrintControl_Receipt.BackColor = System.Drawing.Color.White;
            this.PrintControl_Receipt.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.PrintControl_Receipt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PrintControl_Receipt.ForeColor = System.Drawing.Color.White;
            this.PrintControl_Receipt.IsMetric = false;
            this.PrintControl_Receipt.Location = new System.Drawing.Point(3, 79);
            this.PrintControl_Receipt.Name = "PrintControl_Receipt";
            this.PrintControl_Receipt.Size = new System.Drawing.Size(388, 353);
            this.PrintControl_Receipt.TabIndex = 27;
            this.PrintControl_Receipt.TabStop = false;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.pnlSearch, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.PrintControl_Receipt, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(0, 0, 0, 10);
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(888, 445);
            this.tableLayoutPanel1.TabIndex = 42;
            // 
            // pnlSearch
            // 
            this.pnlSearch.BackgroundColor = System.Drawing.Color.Transparent;
            this.pnlSearch.BorderColor = System.Drawing.Color.Gray;
            this.pnlSearch.BorderRadius = 5;
            this.pnlSearch.BorderSize = 1;
            this.pnlSearch.Controls.Add(this.txtSearch);
            this.pnlSearch.Controls.Add(this.btnSearch);
            this.pnlSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSearch.Location = new System.Drawing.Point(3, 33);
            this.pnlSearch.Name = "pnlSearch";
            this.pnlSearch.Size = new System.Drawing.Size(388, 40);
            this.pnlSearch.TabIndex = 1;
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
            this.txtSearch.PlaceholderText = "Search by Ticket #";
            this.txtSearch.Size = new System.Drawing.Size(335, 20);
            this.txtSearch.TabIndex = 2;
            this.txtSearch.Text = "Search by Ticket #";
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.Image = global::ThirdPartyTool.Properties.Resources.Icon_Search;
            this.btnSearch.Location = new System.Drawing.Point(351, 3);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(34, 34);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // panel1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.panel1, 2);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.BtnUpdate);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(397, 3);
            this.panel1.Name = "panel1";
            this.tableLayoutPanel1.SetRowSpan(this.panel1, 3);
            this.panel1.Size = new System.Drawing.Size(488, 429);
            this.panel1.TabIndex = 29;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Roboto", 12F);
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(30, 226);
            this.label4.Margin = new System.Windows.Forms.Padding(5);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(389, 38);
            this.label4.TabIndex = 31;
            this.label4.Text = "- Sale Report, Batch Report, and Transaction Report will not include this receipt" +
    ".";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Roboto", 12F);
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(30, 178);
            this.label2.Margin = new System.Windows.Forms.Padding(5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(389, 38);
            this.label2.TabIndex = 31;
            this.label2.Text = "- Once the status is changed to CLOSE, it cannot be reverted to Finish, Canceled," +
    " or Void.";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(15, 149);
            this.label3.Margin = new System.Windows.Forms.Padding(5);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(396, 19);
            this.label3.TabIndex = 31;
            this.label3.Text = "Notes when changing the status of a receipt to CLOSE";
            // 
            // BtnUpdate
            // 
            this.BtnUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnUpdate.BackColor = System.Drawing.Color.Red;
            this.BtnUpdate.BackgroundColor = System.Drawing.Color.Red;
            this.BtnUpdate.BorderColor = System.Drawing.Color.Gray;
            this.BtnUpdate.BorderRadius = 5;
            this.BtnUpdate.BorderSize = 0;
            this.BtnUpdate.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Bold);
            this.BtnUpdate.ForeColor = System.Drawing.Color.White;
            this.BtnUpdate.Location = new System.Drawing.Point(50, 30);
            this.BtnUpdate.Margin = new System.Windows.Forms.Padding(5);
            this.BtnUpdate.Name = "BtnUpdate";
            this.BtnUpdate.Size = new System.Drawing.Size(388, 100);
            this.BtnUpdate.TabIndex = 30;
            this.BtnUpdate.Text = "Change Status receipt to CLOSE";
            this.BtnUpdate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.BtnUpdate.TextColor = System.Drawing.Color.White;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnBack);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 445);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(888, 50);
            this.panel2.TabIndex = 43;
            // 
            // btnBack
            // 
            this.btnBack.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnBack.BackColor = System.Drawing.Color.White;
            this.btnBack.BackgroundColor = System.Drawing.Color.White;
            this.btnBack.BorderColor = System.Drawing.Color.Gray;
            this.btnBack.BorderRadius = 5;
            this.btnBack.BorderSize = 1;
            this.btnBack.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Bold);
            this.btnBack.ForeColor = System.Drawing.Color.Black;
            this.btnBack.Location = new System.Drawing.Point(344, 0);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(200, 50);
            this.btnBack.TabIndex = 2;
            this.btnBack.Text = "Back";
            this.btnBack.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnBack.TextColor = System.Drawing.Color.Black;
            // 
            // bTextBox1
            // 
            this.bTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.bTextBox1.Font = new System.Drawing.Font("Roboto", 12F);
            this.bTextBox1.ForeColor = System.Drawing.Color.Gray;
            this.bTextBox1.Location = new System.Drawing.Point(283, -25);
            this.bTextBox1.Name = "bTextBox1";
            this.bTextBox1.PlaceholderColor = System.Drawing.Color.Gray;
            this.bTextBox1.PlaceholderText = "Search by Ticket #";
            this.bTextBox1.Size = new System.Drawing.Size(247, 20);
            this.bTextBox1.TabIndex = 41;
            this.bTextBox1.Text = "Search by Ticket #";
            // 
            // txtFocus
            // 
            this.txtFocus.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtFocus.Font = new System.Drawing.Font("Roboto", 12F);
            this.txtFocus.ForeColor = System.Drawing.Color.Gray;
            this.txtFocus.Location = new System.Drawing.Point(93, -25);
            this.txtFocus.Name = "txtFocus";
            this.txtFocus.PlaceholderColor = System.Drawing.Color.Gray;
            this.txtFocus.PlaceholderText = "Search by Ticket #";
            this.txtFocus.Size = new System.Drawing.Size(247, 20);
            this.txtFocus.TabIndex = 40;
            this.txtFocus.Text = "Search by Ticket #";
            // 
            // UpdateStatusReceiptView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.bTextBox1);
            this.Controls.Add(this.txtFocus);
            this.Name = "UpdateStatusReceiptView";
            this.Size = new System.Drawing.Size(888, 495);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.pnlSearch.ResumeLayout(false);
            this.pnlSearch.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        public DevExpress.XtraPrinting.Control.PrintControl PrintControl_Receipt;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private CustomControl.BPanel_Rounded pnlSearch;
        private CustomControl.BTextBox txtSearch;
        private System.Windows.Forms.Label btnSearch;
        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.Panel panel2;
        public CustomControl.BLabel_Rounded btnBack;
        private CustomControl.BTextBox bTextBox1;
        private CustomControl.BTextBox txtFocus;
        private CustomControl.BLabel_Rounded BtnUpdate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
    }
}
