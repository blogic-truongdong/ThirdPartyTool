namespace ThirdPartyTool
{
    partial class UpdateTipForSaleReceiptModeDualPrice
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtPOSConnection = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtMerchantConnection = new System.Windows.Forms.TextBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.txtTicketNumber = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtGratuity = new System.Windows.Forms.TextBox();
            this.btnGetScript = new System.Windows.Forms.Button();
            this.txtScript = new System.Windows.Forms.RichTextBox();
            this.lblStatusPOS = new System.Windows.Forms.Label();
            this.lblStatusMerchant = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.pnlDetail = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnCopy = new System.Windows.Forms.Button();
            this.pnlDetail.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtPOSConnection
            // 
            this.txtPOSConnection.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPOSConnection.Font = new System.Drawing.Font("Roboto", 10F);
            this.txtPOSConnection.Location = new System.Drawing.Point(160, 74);
            this.txtPOSConnection.Name = "txtPOSConnection";
            this.txtPOSConnection.Size = new System.Drawing.Size(501, 24);
            this.txtPOSConnection.TabIndex = 0;
            this.txtPOSConnection.Text = "data source=.\\SQLEXPRESS;Initial Catalog=BLogicPOS7;integrated security = SSPI";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Roboto", 10F);
            this.label1.Location = new System.Drawing.Point(7, 74);
            this.label1.Margin = new System.Windows.Forms.Padding(3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(127, 21);
            this.label1.TabIndex = 1;
            this.label1.Text = "POS Connection";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Roboto", 10F);
            this.label2.Location = new System.Drawing.Point(7, 101);
            this.label2.Margin = new System.Windows.Forms.Padding(3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(147, 21);
            this.label2.TabIndex = 3;
            this.label2.Text = "Merchant Connection";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtMerchantConnection
            // 
            this.txtMerchantConnection.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMerchantConnection.Font = new System.Drawing.Font("Roboto", 10F);
            this.txtMerchantConnection.Location = new System.Drawing.Point(160, 101);
            this.txtMerchantConnection.Name = "txtMerchantConnection";
            this.txtMerchantConnection.Size = new System.Drawing.Size(501, 24);
            this.txtMerchantConnection.TabIndex = 2;
            this.txtMerchantConnection.Text = "data source=.\\SQLEXPRESS;Initial Catalog=Merchant;integrated security = SSPI";
            // 
            // btnConnect
            // 
            this.btnConnect.BackColor = System.Drawing.Color.White;
            this.btnConnect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConnect.Font = new System.Drawing.Font("Roboto", 12F);
            this.btnConnect.Location = new System.Drawing.Point(233, 128);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(200, 32);
            this.btnConnect.TabIndex = 4;
            this.btnConnect.Text = "Connect Database";
            this.btnConnect.UseVisualStyleBackColor = false;
            // 
            // txtTicketNumber
            // 
            this.txtTicketNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTicketNumber.Font = new System.Drawing.Font("Roboto", 10F);
            this.txtTicketNumber.Location = new System.Drawing.Point(133, 10);
            this.txtTicketNumber.Name = "txtTicketNumber";
            this.txtTicketNumber.Size = new System.Drawing.Size(167, 24);
            this.txtTicketNumber.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Roboto", 10F);
            this.label3.Location = new System.Drawing.Point(29, 10);
            this.label3.Margin = new System.Windows.Forms.Padding(3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 21);
            this.label3.TabIndex = 1;
            this.label3.Text = "Ticket Number";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Roboto", 10F);
            this.label4.Location = new System.Drawing.Point(355, 10);
            this.label4.Margin = new System.Windows.Forms.Padding(3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 21);
            this.label4.TabIndex = 1;
            this.label4.Text = "Total Tip";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtGratuity
            // 
            this.txtGratuity.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtGratuity.Font = new System.Drawing.Font("Roboto", 10F);
            this.txtGratuity.Location = new System.Drawing.Point(421, 10);
            this.txtGratuity.Name = "txtGratuity";
            this.txtGratuity.Size = new System.Drawing.Size(167, 24);
            this.txtGratuity.TabIndex = 7;
            // 
            // btnGetScript
            // 
            this.btnGetScript.BackColor = System.Drawing.Color.White;
            this.btnGetScript.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGetScript.Font = new System.Drawing.Font("Roboto", 12F);
            this.btnGetScript.Location = new System.Drawing.Point(227, 37);
            this.btnGetScript.Name = "btnGetScript";
            this.btnGetScript.Size = new System.Drawing.Size(200, 32);
            this.btnGetScript.TabIndex = 8;
            this.btnGetScript.Text = "Get script";
            this.btnGetScript.UseVisualStyleBackColor = false;
            // 
            // txtScript
            // 
            this.txtScript.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtScript.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtScript.Font = new System.Drawing.Font("Roboto", 10F);
            this.txtScript.Location = new System.Drawing.Point(0, 0);
            this.txtScript.Name = "txtScript";
            this.txtScript.Size = new System.Drawing.Size(512, 101);
            this.txtScript.TabIndex = 9;
            this.txtScript.Text = "";
            // 
            // lblStatusPOS
            // 
            this.lblStatusPOS.Font = new System.Drawing.Font("Roboto", 10F);
            this.lblStatusPOS.ForeColor = System.Drawing.Color.Green;
            this.lblStatusPOS.Location = new System.Drawing.Point(167, 167);
            this.lblStatusPOS.Margin = new System.Windows.Forms.Padding(3);
            this.lblStatusPOS.Name = "lblStatusPOS";
            this.lblStatusPOS.Size = new System.Drawing.Size(333, 20);
            this.lblStatusPOS.TabIndex = 5;
            this.lblStatusPOS.Text = "...";
            this.lblStatusPOS.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblStatusMerchant
            // 
            this.lblStatusMerchant.Font = new System.Drawing.Font("Roboto", 10F);
            this.lblStatusMerchant.ForeColor = System.Drawing.Color.Green;
            this.lblStatusMerchant.Location = new System.Drawing.Point(167, 193);
            this.lblStatusMerchant.Margin = new System.Windows.Forms.Padding(3);
            this.lblStatusMerchant.Name = "lblStatusMerchant";
            this.lblStatusMerchant.Size = new System.Drawing.Size(333, 20);
            this.lblStatusMerchant.TabIndex = 5;
            this.lblStatusMerchant.Text = "...";
            this.lblStatusMerchant.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Roboto", 18F);
            this.label5.Location = new System.Drawing.Point(33, 10);
            this.label5.Margin = new System.Windows.Forms.Padding(3);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(600, 58);
            this.label5.TabIndex = 10;
            this.label5.Text = "Generate script update tip for Ticket by Ticket Number mode Dual Price";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.White;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Roboto", 20F);
            this.btnClose.Location = new System.Drawing.Point(233, 406);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(200, 40);
            this.btnClose.TabIndex = 11;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            // 
            // pnlDetail
            // 
            this.pnlDetail.Controls.Add(this.panel2);
            this.pnlDetail.Controls.Add(this.label3);
            this.pnlDetail.Controls.Add(this.label4);
            this.pnlDetail.Controls.Add(this.txtTicketNumber);
            this.pnlDetail.Controls.Add(this.txtGratuity);
            this.pnlDetail.Controls.Add(this.btnCopy);
            this.pnlDetail.Controls.Add(this.btnGetScript);
            this.pnlDetail.Location = new System.Drawing.Point(7, 218);
            this.pnlDetail.Name = "pnlDetail";
            this.pnlDetail.Size = new System.Drawing.Size(653, 182);
            this.pnlDetail.TabIndex = 12;
            this.pnlDetail.Visible = false;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.txtScript);
            this.panel2.Location = new System.Drawing.Point(3, 76);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(514, 103);
            this.panel2.TabIndex = 10;
            // 
            // btnCopy
            // 
            this.btnCopy.BackColor = System.Drawing.Color.White;
            this.btnCopy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCopy.Font = new System.Drawing.Font("Roboto", 15F);
            this.btnCopy.Location = new System.Drawing.Point(523, 76);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(127, 102);
            this.btnCopy.TabIndex = 8;
            this.btnCopy.Text = "Copy Script";
            this.btnCopy.UseVisualStyleBackColor = false;
            // 
            // UpdateTipForSaleReceiptModeDualPrice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(667, 455);
            this.Controls.Add(this.pnlDetail);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblStatusMerchant);
            this.Controls.Add(this.lblStatusPOS);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtMerchantConnection);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtPOSConnection);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "UpdateTipForSaleReceiptModeDualPrice";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "UpdateTipForSaleReceiptModeDualPrice";
            this.pnlDetail.ResumeLayout(false);
            this.pnlDetail.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtPOSConnection;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtMerchantConnection;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.TextBox txtTicketNumber;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtGratuity;
        private System.Windows.Forms.Button btnGetScript;
        private System.Windows.Forms.RichTextBox txtScript;
        private System.Windows.Forms.Label lblStatusPOS;
        private System.Windows.Forms.Label lblStatusMerchant;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Panel pnlDetail;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnCopy;
    }
}