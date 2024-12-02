namespace ThirdPartyTool.AddTipForPayment
{
    partial class AddTipForPaymentForm
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
            this.lblTicketNumber = new System.Windows.Forms.Label();
            this.txtScript = new System.Windows.Forms.RichTextBox();
            this.lblStatusPOS = new System.Windows.Forms.Label();
            this.lblStatusMerchant = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.pnlDetail = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnCopy = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.pnlPaymentDetails = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlDetail.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtPOSConnection
            // 
            this.txtPOSConnection.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPOSConnection.Font = new System.Drawing.Font("Roboto", 10F);
            this.txtPOSConnection.Location = new System.Drawing.Point(160, 64);
            this.txtPOSConnection.Margin = new System.Windows.Forms.Padding(5);
            this.txtPOSConnection.Name = "txtPOSConnection";
            this.txtPOSConnection.Size = new System.Drawing.Size(828, 24);
            this.txtPOSConnection.TabIndex = 0;
            this.txtPOSConnection.Text = "data source=.\\SQLEXPRESS;Initial Catalog=BLogicPOS7;integrated security = SSPI";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Roboto", 10F);
            this.label1.Location = new System.Drawing.Point(7, 64);
            this.label1.Margin = new System.Windows.Forms.Padding(5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(127, 21);
            this.label1.TabIndex = 1;
            this.label1.Text = "POS Connection";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Roboto", 10F);
            this.label2.Location = new System.Drawing.Point(7, 91);
            this.label2.Margin = new System.Windows.Forms.Padding(5);
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
            this.txtMerchantConnection.Location = new System.Drawing.Point(160, 91);
            this.txtMerchantConnection.Margin = new System.Windows.Forms.Padding(5);
            this.txtMerchantConnection.Name = "txtMerchantConnection";
            this.txtMerchantConnection.Size = new System.Drawing.Size(828, 24);
            this.txtMerchantConnection.TabIndex = 2;
            this.txtMerchantConnection.Text = "data source=.\\SQLEXPRESS;Initial Catalog=Merchant;integrated security = SSPI";
            // 
            // btnConnect
            // 
            this.btnConnect.BackColor = System.Drawing.Color.White;
            this.btnConnect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConnect.Font = new System.Drawing.Font("Roboto", 12F);
            this.btnConnect.Location = new System.Drawing.Point(160, 121);
            this.btnConnect.Margin = new System.Windows.Forms.Padding(5);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(200, 40);
            this.btnConnect.TabIndex = 4;
            this.btnConnect.Text = "Connect Database";
            this.btnConnect.UseVisualStyleBackColor = false;
            // 
            // txtTicketNumber
            // 
            this.txtTicketNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTicketNumber.Font = new System.Drawing.Font("Roboto", 10F);
            this.txtTicketNumber.Location = new System.Drawing.Point(160, 179);
            this.txtTicketNumber.Margin = new System.Windows.Forms.Padding(5);
            this.txtTicketNumber.Name = "txtTicketNumber";
            this.txtTicketNumber.Size = new System.Drawing.Size(200, 24);
            this.txtTicketNumber.TabIndex = 6;
            this.txtTicketNumber.Visible = false;
            // 
            // lblTicketNumber
            // 
            this.lblTicketNumber.Font = new System.Drawing.Font("Roboto", 10F);
            this.lblTicketNumber.Location = new System.Drawing.Point(7, 180);
            this.lblTicketNumber.Margin = new System.Windows.Forms.Padding(5);
            this.lblTicketNumber.Name = "lblTicketNumber";
            this.lblTicketNumber.Size = new System.Drawing.Size(98, 21);
            this.lblTicketNumber.TabIndex = 1;
            this.lblTicketNumber.Text = "Ticket Number";
            this.lblTicketNumber.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblTicketNumber.Visible = false;
            // 
            // txtScript
            // 
            this.txtScript.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.txtScript.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtScript.Font = new System.Drawing.Font("Roboto", 10F);
            this.txtScript.Location = new System.Drawing.Point(0, 0);
            this.txtScript.Margin = new System.Windows.Forms.Padding(5);
            this.txtScript.Name = "txtScript";
            this.txtScript.Size = new System.Drawing.Size(840, 101);
            this.txtScript.TabIndex = 9;
            this.txtScript.Text = "";
            // 
            // lblStatusPOS
            // 
            this.lblStatusPOS.Font = new System.Drawing.Font("Roboto", 10F);
            this.lblStatusPOS.ForeColor = System.Drawing.Color.Green;
            this.lblStatusPOS.Location = new System.Drawing.Point(370, 121);
            this.lblStatusPOS.Margin = new System.Windows.Forms.Padding(5);
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
            this.lblStatusMerchant.Location = new System.Drawing.Point(370, 141);
            this.lblStatusMerchant.Margin = new System.Windows.Forms.Padding(5);
            this.lblStatusMerchant.Name = "lblStatusMerchant";
            this.lblStatusMerchant.Size = new System.Drawing.Size(333, 20);
            this.lblStatusMerchant.TabIndex = 5;
            this.lblStatusMerchant.Text = "...";
            this.lblStatusMerchant.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Roboto", 20F);
            this.label5.Location = new System.Drawing.Point(33, 16);
            this.label5.Margin = new System.Windows.Forms.Padding(5);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(600, 32);
            this.label5.TabIndex = 10;
            this.label5.Text = "Generate script update tip for Ticket by Ticket Number";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.White;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Roboto", 20F);
            this.btnClose.Location = new System.Drawing.Point(400, 650);
            this.btnClose.Margin = new System.Windows.Forms.Padding(5);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(200, 40);
            this.btnClose.TabIndex = 11;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            // 
            // pnlDetail
            // 
            this.pnlDetail.Controls.Add(this.panel2);
            this.pnlDetail.Controls.Add(this.btnCopy);
            this.pnlDetail.Controls.Add(this.button1);
            this.pnlDetail.Location = new System.Drawing.Point(7, 534);
            this.pnlDetail.Margin = new System.Windows.Forms.Padding(5);
            this.pnlDetail.Name = "pnlDetail";
            this.pnlDetail.Size = new System.Drawing.Size(981, 110);
            this.pnlDetail.TabIndex = 12;
            this.pnlDetail.Visible = false;
            // 
            // panel2
            // 
            this.panel2.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.txtScript);
            this.panel2.Location = new System.Drawing.Point(5, 4);
            this.panel2.Margin = new System.Windows.Forms.Padding(5);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(840, 103);
            this.panel2.TabIndex = 10;
            // 
            // btnCopy
            // 
            this.btnCopy.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnCopy.BackColor = System.Drawing.Color.White;
            this.btnCopy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCopy.Font = new System.Drawing.Font("Roboto", 15F);
            this.btnCopy.Location = new System.Drawing.Point(851, 4);
            this.btnCopy.Margin = new System.Windows.Forms.Padding(5);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(127, 102);
            this.btnCopy.TabIndex = 8;
            this.btnCopy.Text = "Copy Script";
            this.btnCopy.UseVisualStyleBackColor = false;
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button1.BackColor = System.Drawing.Color.White;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Roboto", 12F);
            this.button1.Location = new System.Drawing.Point(10, -322);
            this.button1.Margin = new System.Windows.Forms.Padding(5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(249, 30);
            this.button1.TabIndex = 8;
            this.button1.Text = "Get Payment Detail";
            this.button1.UseVisualStyleBackColor = false;
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.White;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("Roboto", 10F, System.Drawing.FontStyle.Bold);
            this.btnSearch.Location = new System.Drawing.Point(370, 171);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(5);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(100, 40);
            this.btnSearch.TabIndex = 11;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Visible = false;
            // 
            // pnlPaymentDetails
            // 
            this.pnlPaymentDetails.AutoScroll = true;
            this.pnlPaymentDetails.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlPaymentDetails.Location = new System.Drawing.Point(10, 219);
            this.pnlPaymentDetails.Name = "pnlPaymentDetails";
            this.pnlPaymentDetails.Size = new System.Drawing.Size(980, 307);
            this.pnlPaymentDetails.TabIndex = 13;
            this.pnlPaymentDetails.Visible = false;
            // 
            // AddTipForPaymentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 700);
            this.Controls.Add(this.pnlPaymentDetails);
            this.Controls.Add(this.pnlDetail);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblTicketNumber);
            this.Controls.Add(this.lblStatusMerchant);
            this.Controls.Add(this.lblStatusPOS);
            this.Controls.Add(this.txtTicketNumber);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtMerchantConnection);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtPOSConnection);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "AddTipForPaymentForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AddTipForPaymentForm";
            this.pnlDetail.ResumeLayout(false);
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
        private System.Windows.Forms.Label lblTicketNumber;
        private System.Windows.Forms.RichTextBox txtScript;
        private System.Windows.Forms.Label lblStatusPOS;
        private System.Windows.Forms.Label lblStatusMerchant;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Panel pnlDetail;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnCopy;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.FlowLayoutPanel pnlPaymentDetails;
    }
}