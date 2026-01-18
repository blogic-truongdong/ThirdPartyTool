namespace ThirdPartyTool.SaleReceipt
{
    partial class RefundTicketView
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
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
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
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.BtnUpdate);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(397, 3);
            this.panel1.Name = "panel1";
            this.tableLayoutPanel1.SetRowSpan(this.panel1, 3);
            this.panel1.Size = new System.Drawing.Size(488, 429);
            this.panel1.TabIndex = 29;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Roboto", 10F);
            this.label10.ForeColor = System.Drawing.Color.Red;
            this.label10.Location = new System.Drawing.Point(51, 345);
            this.label10.Margin = new System.Windows.Forms.Padding(3);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(234, 17);
            this.label10.TabIndex = 32;
            this.label10.Text = "4. Ko thể Refund thêm 1 lần nào nữa";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Roboto", 10F);
            this.label9.ForeColor = System.Drawing.Color.Red;
            this.label9.Location = new System.Drawing.Point(51, 322);
            this.label9.Margin = new System.Windows.Forms.Padding(3);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(248, 17);
            this.label9.TabIndex = 32;
            this.label9.Text = "3. Ticket Refund có cùng Amount, Date";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Roboto", 10F);
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(51, 224);
            this.label5.Margin = new System.Windows.Forms.Padding(3);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(399, 17);
            this.label5.TabIndex = 32;
            this.label5.Text = "3. Chỉ có thể Refund Full Amount, ko thể Refund Partial Amount";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Roboto", 10F);
            this.label8.ForeColor = System.Drawing.Color.Red;
            this.label8.Location = new System.Drawing.Point(51, 297);
            this.label8.Margin = new System.Windows.Forms.Padding(3);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(216, 17);
            this.label8.TabIndex = 32;
            this.label8.Text = "2. Sẽ Create thêm 1 ticket Refund ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Roboto", 10F);
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(51, 199);
            this.label4.Margin = new System.Windows.Forms.Padding(3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(212, 17);
            this.label4.TabIndex = 32;
            this.label4.Text = "2. Batch trên Host đã được settle";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Roboto", 10F);
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(51, 272);
            this.label7.Margin = new System.Windows.Forms.Padding(3);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(242, 17);
            this.label7.TabIndex = 32;
            this.label7.Text = "1. Ticket gốc vẫn còn trạng thái Finish";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Roboto", 10F);
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(51, 174);
            this.label2.Margin = new System.Windows.Forms.Padding(3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(186, 17);
            this.label2.TabIndex = 32;
            this.label2.Text = "1. Ticket đã Refund trên host";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Bold);
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(15, 247);
            this.label6.Margin = new System.Windows.Forms.Padding(3);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(211, 19);
            this.label6.TabIndex = 31;
            this.label6.Text = "Sau Khi Refund Thành Công";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(15, 149);
            this.label3.Margin = new System.Windows.Forms.Padding(3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(226, 19);
            this.label3.TabIndex = 31;
            this.label3.Text = "Điều kiện dùng chức năng này";
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
            this.BtnUpdate.Text = "Refund Ticket Now";
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
            // RefundTicketView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.bTextBox1);
            this.Controls.Add(this.txtFocus);
            this.Name = "RefundTicketView";
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
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
    }
}
