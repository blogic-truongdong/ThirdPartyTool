namespace BLogicPOS7SupportTool.Dashboard.Pages
{
    partial class AdvancedPage
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlHeaderDivider = new System.Windows.Forms.Panel();
            this.pnlBody = new System.Windows.Forms.Panel();
            this.pnlClearSales = new System.Windows.Forms.Panel();
            this.btnClearSales = new System.Windows.Forms.Button();
            this.lblClearSalesDesc = new System.Windows.Forms.Label();
            this.lblClearSalesTitle = new System.Windows.Forms.Label();
            this.pnlInsertTran = new System.Windows.Forms.Panel();
            this.btnInsertTran = new System.Windows.Forms.Button();
            this.lblInsertTranDesc = new System.Windows.Forms.Label();
            this.lblInsertTranTitle = new System.Windows.Forms.Label();
            this.pnlHeader.SuspendLayout();
            this.pnlBody.SuspendLayout();
            this.pnlClearSales.SuspendLayout();
            this.pnlInsertTran.SuspendLayout();
            this.SuspendLayout();
            //
            // pnlHeader
            //
            this.pnlHeader.BackColor = System.Drawing.Color.White;
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Controls.Add(this.pnlHeaderDivider);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(784, 64);
            this.pnlHeader.TabIndex = 0;
            //
            // lblTitle
            //
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.lblTitle.Location = new System.Drawing.Point(24, 16);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(107, 32);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "Advanced";
            //
            // pnlHeaderDivider
            //
            this.pnlHeaderDivider.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            this.pnlHeaderDivider.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlHeaderDivider.Location = new System.Drawing.Point(0, 63);
            this.pnlHeaderDivider.Name = "pnlHeaderDivider";
            this.pnlHeaderDivider.Size = new System.Drawing.Size(784, 1);
            this.pnlHeaderDivider.TabIndex = 0;
            //
            // pnlBody
            //
            this.pnlBody.AutoScroll = true;
            this.pnlBody.BackColor = System.Drawing.Color.White;
            this.pnlBody.Controls.Add(this.pnlInsertTran);
            this.pnlBody.Controls.Add(this.pnlClearSales);
            this.pnlBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBody.Location = new System.Drawing.Point(0, 64);
            this.pnlBody.Name = "pnlBody";
            this.pnlBody.Size = new System.Drawing.Size(784, 664);
            this.pnlBody.TabIndex = 1;
            //
            // pnlClearSales
            //
            this.pnlClearSales.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlClearSales.BackColor = System.Drawing.Color.White;
            this.pnlClearSales.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlClearSales.Controls.Add(this.btnClearSales);
            this.pnlClearSales.Controls.Add(this.lblClearSalesDesc);
            this.pnlClearSales.Controls.Add(this.lblClearSalesTitle);
            this.pnlClearSales.Location = new System.Drawing.Point(24, 24);
            this.pnlClearSales.Name = "pnlClearSales";
            this.pnlClearSales.Size = new System.Drawing.Size(736, 96);
            this.pnlClearSales.TabIndex = 0;
            //
            // btnClearSales
            //
            this.btnClearSales.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClearSales.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(87)))), ((int)(((byte)(208)))));
            this.btnClearSales.FlatAppearance.BorderSize = 0;
            this.btnClearSales.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearSales.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnClearSales.ForeColor = System.Drawing.Color.White;
            this.btnClearSales.Location = new System.Drawing.Point(600, 30);
            this.btnClearSales.Name = "btnClearSales";
            this.btnClearSales.Size = new System.Drawing.Size(120, 36);
            this.btnClearSales.TabIndex = 0;
            this.btnClearSales.Text = "Open";
            this.btnClearSales.UseVisualStyleBackColor = false;
            //
            // lblClearSalesDesc
            //
            this.lblClearSalesDesc.AutoSize = true;
            this.lblClearSalesDesc.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblClearSalesDesc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.lblClearSalesDesc.Location = new System.Drawing.Point(16, 48);
            this.lblClearSalesDesc.Name = "lblClearSalesDesc";
            this.lblClearSalesDesc.Size = new System.Drawing.Size(447, 17);
            this.lblClearSalesDesc.TabIndex = 1;
            this.lblClearSalesDesc.Text = "Close open (OPER) tickets that have $0 paid within a selected date range.";
            //
            // lblClearSalesTitle
            //
            this.lblClearSalesTitle.AutoSize = true;
            this.lblClearSalesTitle.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblClearSalesTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.lblClearSalesTitle.Location = new System.Drawing.Point(15, 16);
            this.lblClearSalesTitle.Name = "lblClearSalesTitle";
            this.lblClearSalesTitle.Size = new System.Drawing.Size(139, 25);
            this.lblClearSalesTitle.TabIndex = 0;
            this.lblClearSalesTitle.Text = "Clear Sales Data";
            //
            // pnlInsertTran
            //
            this.pnlInsertTran.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlInsertTran.BackColor = System.Drawing.Color.White;
            this.pnlInsertTran.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlInsertTran.Controls.Add(this.btnInsertTran);
            this.pnlInsertTran.Controls.Add(this.lblInsertTranDesc);
            this.pnlInsertTran.Controls.Add(this.lblInsertTranTitle);
            this.pnlInsertTran.Location = new System.Drawing.Point(24, 136);
            this.pnlInsertTran.Name = "pnlInsertTran";
            this.pnlInsertTran.Size = new System.Drawing.Size(736, 96);
            this.pnlInsertTran.TabIndex = 1;
            //
            // btnInsertTran
            //
            this.btnInsertTran.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnInsertTran.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(87)))), ((int)(((byte)(208)))));
            this.btnInsertTran.FlatAppearance.BorderSize = 0;
            this.btnInsertTran.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInsertTran.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnInsertTran.ForeColor = System.Drawing.Color.White;
            this.btnInsertTran.Location = new System.Drawing.Point(600, 30);
            this.btnInsertTran.Name = "btnInsertTran";
            this.btnInsertTran.Size = new System.Drawing.Size(120, 36);
            this.btnInsertTran.TabIndex = 0;
            this.btnInsertTran.Text = "Open";
            this.btnInsertTran.UseVisualStyleBackColor = false;
            //
            // lblInsertTranDesc
            //
            this.lblInsertTranDesc.AutoSize = true;
            this.lblInsertTranDesc.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblInsertTranDesc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.lblInsertTranDesc.Location = new System.Drawing.Point(16, 48);
            this.lblInsertTranDesc.Name = "lblInsertTranDesc";
            this.lblInsertTranDesc.Size = new System.Drawing.Size(470, 17);
            this.lblInsertTranDesc.TabIndex = 1;
            this.lblInsertTranDesc.Text = "Insert a new credit-card transaction row into the Merchant TransactionDetail table.";
            //
            // lblInsertTranTitle
            //
            this.lblInsertTranTitle.AutoSize = true;
            this.lblInsertTranTitle.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblInsertTranTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.lblInsertTranTitle.Location = new System.Drawing.Point(15, 16);
            this.lblInsertTranTitle.Name = "lblInsertTranTitle";
            this.lblInsertTranTitle.Size = new System.Drawing.Size(196, 25);
            this.lblInsertTranTitle.TabIndex = 0;
            this.lblInsertTranTitle.Text = "Insert Transaction Detail";
            //
            // AdvancedPage
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.pnlBody);
            this.Controls.Add(this.pnlHeader);
            this.Name = "AdvancedPage";
            this.Size = new System.Drawing.Size(784, 728);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlBody.ResumeLayout(false);
            this.pnlClearSales.ResumeLayout(false);
            this.pnlClearSales.PerformLayout();
            this.pnlInsertTran.ResumeLayout(false);
            this.pnlInsertTran.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlHeaderDivider;
        private System.Windows.Forms.Panel pnlBody;
        private System.Windows.Forms.Panel pnlClearSales;
        private System.Windows.Forms.Label lblClearSalesTitle;
        private System.Windows.Forms.Label lblClearSalesDesc;
        private System.Windows.Forms.Button btnClearSales;
        private System.Windows.Forms.Panel pnlInsertTran;
        private System.Windows.Forms.Label lblInsertTranTitle;
        private System.Windows.Forms.Label lblInsertTranDesc;
        private System.Windows.Forms.Button btnInsertTran;
    }
}
