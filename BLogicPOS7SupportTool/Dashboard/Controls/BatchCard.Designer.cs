namespace BLogicPOS7SupportTool.Dashboard.Controls
{
    partial class BatchCard
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
            this.pnlCardBody = new System.Windows.Forms.Panel();
            this.btnSettle = new System.Windows.Forms.Button();
            this.lblPeriod = new System.Windows.Forms.Label();
            this.lblPeriodCaption = new System.Windows.Forms.Label();
            this.lblTransactions = new System.Windows.Forms.Label();
            this.lblTransactionsCaption = new System.Windows.Forms.Label();
            this.lblTerminalIp = new System.Windows.Forms.Label();
            this.lblTerminalIpCaption = new System.Windows.Forms.Label();
            this.lblWorkstation = new System.Windows.Forms.Label();
            this.lblWorkstationCaption = new System.Windows.Forms.Label();
            this.lblMerchant = new System.Windows.Forms.Label();
            this.lblMerchantCaption = new System.Windows.Forms.Label();
            this.pnlTitleDivider = new System.Windows.Forms.Panel();
            this.lblTotal = new System.Windows.Forms.Label();
            this.lblBatchTitle = new System.Windows.Forms.Label();
            this.pnlCardBody.SuspendLayout();
            this.SuspendLayout();
            //
            // pnlCardBody
            //
            this.pnlCardBody.BackColor = System.Drawing.Color.White;
            this.pnlCardBody.Controls.Add(this.btnSettle);
            this.pnlCardBody.Controls.Add(this.lblPeriod);
            this.pnlCardBody.Controls.Add(this.lblPeriodCaption);
            this.pnlCardBody.Controls.Add(this.lblTransactions);
            this.pnlCardBody.Controls.Add(this.lblTransactionsCaption);
            this.pnlCardBody.Controls.Add(this.lblTerminalIp);
            this.pnlCardBody.Controls.Add(this.lblTerminalIpCaption);
            this.pnlCardBody.Controls.Add(this.lblWorkstation);
            this.pnlCardBody.Controls.Add(this.lblWorkstationCaption);
            this.pnlCardBody.Controls.Add(this.lblMerchant);
            this.pnlCardBody.Controls.Add(this.lblMerchantCaption);
            this.pnlCardBody.Controls.Add(this.pnlTitleDivider);
            this.pnlCardBody.Controls.Add(this.lblTotal);
            this.pnlCardBody.Controls.Add(this.lblBatchTitle);
            this.pnlCardBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCardBody.Location = new System.Drawing.Point(1, 1);
            this.pnlCardBody.Name = "pnlCardBody";
            this.pnlCardBody.Size = new System.Drawing.Size(318, 232);
            this.pnlCardBody.TabIndex = 0;
            this.pnlCardBody.Text = "";
            //
            // btnSettle
            //
            this.btnSettle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(87)))), ((int)(((byte)(208)))));
            this.btnSettle.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSettle.FlatAppearance.BorderSize = 0;
            this.btnSettle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSettle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSettle.ForeColor = System.Drawing.Color.White;
            this.btnSettle.Location = new System.Drawing.Point(206, 188);
            this.btnSettle.Name = "btnSettle";
            this.btnSettle.Size = new System.Drawing.Size(96, 30);
            this.btnSettle.TabIndex = 13;
            this.btnSettle.Text = "Settle";
            this.btnSettle.UseVisualStyleBackColor = false;
            this.btnSettle.Click += new System.EventHandler(this.btnSettle_Click);
            //
            // lblPeriod
            //
            this.lblPeriod.AutoEllipsis = true;
            this.lblPeriod.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblPeriod.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.lblPeriod.Location = new System.Drawing.Point(104, 152);
            this.lblPeriod.Name = "lblPeriod";
            this.lblPeriod.Size = new System.Drawing.Size(200, 20);
            this.lblPeriod.TabIndex = 12;
            this.lblPeriod.Text = "";
            //
            // lblPeriodCaption
            //
            this.lblPeriodCaption.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblPeriodCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.lblPeriodCaption.Location = new System.Drawing.Point(16, 153);
            this.lblPeriodCaption.Name = "lblPeriodCaption";
            this.lblPeriodCaption.Size = new System.Drawing.Size(86, 18);
            this.lblPeriodCaption.TabIndex = 11;
            this.lblPeriodCaption.Text = "Period";
            //
            // lblTransactions
            //
            this.lblTransactions.AutoEllipsis = true;
            this.lblTransactions.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTransactions.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.lblTransactions.Location = new System.Drawing.Point(104, 126);
            this.lblTransactions.Name = "lblTransactions";
            this.lblTransactions.Size = new System.Drawing.Size(200, 20);
            this.lblTransactions.TabIndex = 10;
            this.lblTransactions.Text = "";
            //
            // lblTransactionsCaption
            //
            this.lblTransactionsCaption.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblTransactionsCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.lblTransactionsCaption.Location = new System.Drawing.Point(16, 127);
            this.lblTransactionsCaption.Name = "lblTransactionsCaption";
            this.lblTransactionsCaption.Size = new System.Drawing.Size(86, 18);
            this.lblTransactionsCaption.TabIndex = 9;
            this.lblTransactionsCaption.Text = "Transactions";
            //
            // lblTerminalIp
            //
            this.lblTerminalIp.AutoEllipsis = true;
            this.lblTerminalIp.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTerminalIp.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.lblTerminalIp.Location = new System.Drawing.Point(104, 100);
            this.lblTerminalIp.Name = "lblTerminalIp";
            this.lblTerminalIp.Size = new System.Drawing.Size(200, 20);
            this.lblTerminalIp.TabIndex = 8;
            this.lblTerminalIp.Text = "";
            //
            // lblTerminalIpCaption
            //
            this.lblTerminalIpCaption.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblTerminalIpCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.lblTerminalIpCaption.Location = new System.Drawing.Point(16, 101);
            this.lblTerminalIpCaption.Name = "lblTerminalIpCaption";
            this.lblTerminalIpCaption.Size = new System.Drawing.Size(86, 18);
            this.lblTerminalIpCaption.TabIndex = 7;
            this.lblTerminalIpCaption.Text = "Terminal IP";
            //
            // lblWorkstation
            //
            this.lblWorkstation.AutoEllipsis = true;
            this.lblWorkstation.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblWorkstation.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.lblWorkstation.Location = new System.Drawing.Point(104, 74);
            this.lblWorkstation.Name = "lblWorkstation";
            this.lblWorkstation.Size = new System.Drawing.Size(200, 20);
            this.lblWorkstation.TabIndex = 6;
            this.lblWorkstation.Text = "";
            //
            // lblWorkstationCaption
            //
            this.lblWorkstationCaption.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblWorkstationCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.lblWorkstationCaption.Location = new System.Drawing.Point(16, 75);
            this.lblWorkstationCaption.Name = "lblWorkstationCaption";
            this.lblWorkstationCaption.Size = new System.Drawing.Size(86, 18);
            this.lblWorkstationCaption.TabIndex = 5;
            this.lblWorkstationCaption.Text = "Workstation";
            //
            // lblMerchant
            //
            this.lblMerchant.AutoEllipsis = true;
            this.lblMerchant.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblMerchant.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.lblMerchant.Location = new System.Drawing.Point(104, 48);
            this.lblMerchant.Name = "lblMerchant";
            this.lblMerchant.Size = new System.Drawing.Size(200, 20);
            this.lblMerchant.TabIndex = 4;
            this.lblMerchant.Text = "";
            //
            // lblMerchantCaption
            //
            this.lblMerchantCaption.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblMerchantCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.lblMerchantCaption.Location = new System.Drawing.Point(16, 49);
            this.lblMerchantCaption.Name = "lblMerchantCaption";
            this.lblMerchantCaption.Size = new System.Drawing.Size(86, 18);
            this.lblMerchantCaption.TabIndex = 3;
            this.lblMerchantCaption.Text = "Merchant ID";
            //
            // pnlTitleDivider
            //
            this.pnlTitleDivider.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.pnlTitleDivider.Location = new System.Drawing.Point(16, 40);
            this.pnlTitleDivider.Name = "pnlTitleDivider";
            this.pnlTitleDivider.Size = new System.Drawing.Size(286, 1);
            this.pnlTitleDivider.TabIndex = 2;
            this.pnlTitleDivider.Text = "";
            //
            // lblTotal
            //
            this.lblTotal.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblTotal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(87)))), ((int)(((byte)(208)))));
            this.lblTotal.Location = new System.Drawing.Point(170, 8);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(132, 28);
            this.lblTotal.TabIndex = 1;
            this.lblTotal.Text = "";
            this.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            //
            // lblBatchTitle
            //
            this.lblBatchTitle.AutoEllipsis = true;
            this.lblBatchTitle.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblBatchTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.lblBatchTitle.Location = new System.Drawing.Point(12, 8);
            this.lblBatchTitle.Name = "lblBatchTitle";
            this.lblBatchTitle.Size = new System.Drawing.Size(136, 28);
            this.lblBatchTitle.TabIndex = 0;
            this.lblBatchTitle.Text = "";
            this.lblBatchTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // BatchCard
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            this.Controls.Add(this.pnlCardBody);
            this.Margin = new System.Windows.Forms.Padding(8);
            this.Name = "BatchCard";
            this.Padding = new System.Windows.Forms.Padding(1);
            this.Size = new System.Drawing.Size(320, 234);
            this.pnlCardBody.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlCardBody;
        private System.Windows.Forms.Label lblBatchTitle;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Panel pnlTitleDivider;
        private System.Windows.Forms.Label lblMerchantCaption;
        private System.Windows.Forms.Label lblMerchant;
        private System.Windows.Forms.Label lblWorkstationCaption;
        private System.Windows.Forms.Label lblWorkstation;
        private System.Windows.Forms.Label lblTerminalIpCaption;
        private System.Windows.Forms.Label lblTerminalIp;
        private System.Windows.Forms.Label lblTransactionsCaption;
        private System.Windows.Forms.Label lblTransactions;
        private System.Windows.Forms.Label lblPeriodCaption;
        private System.Windows.Forms.Label lblPeriod;
        private System.Windows.Forms.Button btnSettle;
    }
}
