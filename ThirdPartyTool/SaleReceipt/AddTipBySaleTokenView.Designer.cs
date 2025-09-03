using ThirdPartyTool.CustomControl;

namespace ThirdPartyTool.SaleReceipt
{
    partial class AddTipBySaleTokenView
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
            this.pnlSearch = new ThirdPartyTool.CustomControl.BPanel_Rounded();
            this.txtSearch = new ThirdPartyTool.CustomControl.BTextBox();
            this.btnSearch = new System.Windows.Forms.Label();
            this.PrintControl_Receipt = new DevExpress.XtraPrinting.Control.PrintControl();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.PrintControl_TokenReceipt = new DevExpress.XtraPrinting.Control.PrintControl();
            this.bPanel_Rounded1 = new ThirdPartyTool.CustomControl.BPanel_Rounded();
            this.txtSearchTokenReceipt = new ThirdPartyTool.CustomControl.BTextBox();
            this.btnSearchTokenReceipt = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtFocus = new ThirdPartyTool.CustomControl.BTextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnUpdate = new ThirdPartyTool.CustomControl.BLabel_Rounded();
            this.btnBack = new ThirdPartyTool.CustomControl.BLabel_Rounded();
            this.pnlSearch.SuspendLayout();
            this.bPanel_Rounded1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
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
            this.pnlSearch.Size = new System.Drawing.Size(334, 40);
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
            this.txtSearch.Size = new System.Drawing.Size(281, 20);
            this.txtSearch.TabIndex = 2;
            this.txtSearch.Text = "Search by Ticket #";
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.Image = global::ThirdPartyTool.Properties.Resources.Icon_Search;
            this.btnSearch.Location = new System.Drawing.Point(297, 3);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(34, 34);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // PrintControl_Receipt
            // 
            this.PrintControl_Receipt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(109)))), ((int)(((byte)(119)))));
            this.PrintControl_Receipt.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.PrintControl_Receipt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PrintControl_Receipt.ForeColor = System.Drawing.Color.White;
            this.PrintControl_Receipt.IsMetric = false;
            this.PrintControl_Receipt.Location = new System.Drawing.Point(3, 79);
            this.PrintControl_Receipt.Name = "PrintControl_Receipt";
            this.PrintControl_Receipt.Size = new System.Drawing.Size(334, 341);
            this.PrintControl_Receipt.TabIndex = 27;
            this.PrintControl_Receipt.TabStop = false;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(334, 30);
            this.label1.TabIndex = 28;
            this.label1.Text = "Receipt";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Roboto", 50F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(343, 0);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.tableLayoutPanel1.SetRowSpan(this.label2, 3);
            this.label2.Size = new System.Drawing.Size(94, 423);
            this.label2.TabIndex = 29;
            this.label2.Text = "+";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PrintControl_TokenReceipt
            // 
            this.PrintControl_TokenReceipt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(109)))), ((int)(((byte)(119)))));
            this.PrintControl_TokenReceipt.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.PrintControl_TokenReceipt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PrintControl_TokenReceipt.ForeColor = System.Drawing.Color.White;
            this.PrintControl_TokenReceipt.IsMetric = false;
            this.PrintControl_TokenReceipt.Location = new System.Drawing.Point(443, 79);
            this.PrintControl_TokenReceipt.Name = "PrintControl_TokenReceipt";
            this.PrintControl_TokenReceipt.Size = new System.Drawing.Size(334, 341);
            this.PrintControl_TokenReceipt.TabIndex = 31;
            this.PrintControl_TokenReceipt.TabStop = false;
            // 
            // bPanel_Rounded1
            // 
            this.bPanel_Rounded1.BackgroundColor = System.Drawing.Color.Transparent;
            this.bPanel_Rounded1.BorderColor = System.Drawing.Color.Gray;
            this.bPanel_Rounded1.BorderRadius = 5;
            this.bPanel_Rounded1.BorderSize = 1;
            this.bPanel_Rounded1.Controls.Add(this.txtSearchTokenReceipt);
            this.bPanel_Rounded1.Controls.Add(this.btnSearchTokenReceipt);
            this.bPanel_Rounded1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bPanel_Rounded1.Location = new System.Drawing.Point(443, 33);
            this.bPanel_Rounded1.Name = "bPanel_Rounded1";
            this.bPanel_Rounded1.Size = new System.Drawing.Size(334, 40);
            this.bPanel_Rounded1.TabIndex = 30;
            // 
            // txtSearchTokenReceipt
            // 
            this.txtSearchTokenReceipt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearchTokenReceipt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSearchTokenReceipt.Font = new System.Drawing.Font("Roboto", 12F);
            this.txtSearchTokenReceipt.ForeColor = System.Drawing.Color.Gray;
            this.txtSearchTokenReceipt.Location = new System.Drawing.Point(10, 12);
            this.txtSearchTokenReceipt.Name = "txtSearchTokenReceipt";
            this.txtSearchTokenReceipt.PlaceholderColor = System.Drawing.Color.Gray;
            this.txtSearchTokenReceipt.PlaceholderText = "Search by Ticket #";
            this.txtSearchTokenReceipt.Size = new System.Drawing.Size(284, 20);
            this.txtSearchTokenReceipt.TabIndex = 2;
            this.txtSearchTokenReceipt.Text = "Search by Ticket #";
            // 
            // btnSearchTokenReceipt
            // 
            this.btnSearchTokenReceipt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearchTokenReceipt.Image = global::ThirdPartyTool.Properties.Resources.Icon_Search;
            this.btnSearchTokenReceipt.Location = new System.Drawing.Point(297, 3);
            this.btnSearchTokenReceipt.Name = "btnSearchTokenReceipt";
            this.btnSearchTokenReceipt.Size = new System.Drawing.Size(34, 34);
            this.btnSearchTokenReceipt.TabIndex = 1;
            this.btnSearchTokenReceipt.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label4
            // 
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(443, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(334, 30);
            this.label4.TabIndex = 28;
            this.label4.Text = "Token Receipt";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtFocus
            // 
            this.txtFocus.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtFocus.Font = new System.Drawing.Font("Roboto", 12F);
            this.txtFocus.ForeColor = System.Drawing.Color.Gray;
            this.txtFocus.Location = new System.Drawing.Point(283, -100);
            this.txtFocus.Name = "txtFocus";
            this.txtFocus.PlaceholderColor = System.Drawing.Color.Gray;
            this.txtFocus.PlaceholderText = "Search by Ticket #";
            this.txtFocus.Size = new System.Drawing.Size(247, 20);
            this.txtFocus.TabIndex = 33;
            this.txtFocus.Text = "Search by Ticket #";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.label4, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.bPanel_Rounded1, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.PrintControl_TokenReceipt, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.pnlSearch, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.PrintControl_Receipt, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label2, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(10, 10);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(0, 0, 0, 10);
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(780, 433);
            this.tableLayoutPanel1.TabIndex = 34;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnUpdate);
            this.panel1.Controls.Add(this.btnBack);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(10, 443);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(780, 50);
            this.panel1.TabIndex = 35;
            // 
            // btnUpdate
            // 
            this.btnUpdate.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnUpdate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(26)))), ((int)(((byte)(98)))));
            this.btnUpdate.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(26)))), ((int)(((byte)(98)))));
            this.btnUpdate.BorderColor = System.Drawing.Color.Gray;
            this.btnUpdate.BorderRadius = 5;
            this.btnUpdate.BorderSize = 1;
            this.btnUpdate.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Bold);
            this.btnUpdate.ForeColor = System.Drawing.Color.White;
            this.btnUpdate.Location = new System.Drawing.Point(400, 0);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(200, 50);
            this.btnUpdate.TabIndex = 1;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnUpdate.TextColor = System.Drawing.Color.White;
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
            this.btnBack.Location = new System.Drawing.Point(180, 0);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(200, 50);
            this.btnBack.TabIndex = 2;
            this.btnBack.Text = "Back";
            this.btnBack.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnBack.TextColor = System.Drawing.Color.Black;
            // 
            // AddTipBySaleTokenView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.txtFocus);
            this.Name = "AddTipBySaleTokenView";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.Size = new System.Drawing.Size(800, 503);
            this.pnlSearch.ResumeLayout(false);
            this.pnlSearch.PerformLayout();
            this.bPanel_Rounded1.ResumeLayout(false);
            this.bPanel_Rounded1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CustomControl.BPanel_Rounded pnlSearch;
        private CustomControl.BTextBox txtSearch;
        private System.Windows.Forms.Label btnSearch;
        public DevExpress.XtraPrinting.Control.PrintControl PrintControl_Receipt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        public DevExpress.XtraPrinting.Control.PrintControl PrintControl_TokenReceipt;
        private CustomControl.BPanel_Rounded bPanel_Rounded1;
        private CustomControl.BTextBox txtSearchTokenReceipt;
        private System.Windows.Forms.Label btnSearchTokenReceipt;
        private System.Windows.Forms.Label label4;
        private CustomControl.BTextBox txtFocus;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private BLabel_Rounded btnUpdate;
        public System.Windows.Forms.Panel panel1;
        public BLabel_Rounded btnBack;
    }
}
