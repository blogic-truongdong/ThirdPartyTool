namespace ThirdPartyTool
{
    partial class VoidSaleReceipt
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
            this.btnGetScript = new System.Windows.Forms.Button();
            this.txtScript = new System.Windows.Forms.RichTextBox();
            this.lblStatusPOS = new System.Windows.Forms.Label();
            this.lblStatusMerchant = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtPOSConnection
            // 
            this.txtPOSConnection.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtPOSConnection.Location = new System.Drawing.Point(155, 35);
            this.txtPOSConnection.Name = "txtPOSConnection";
            this.txtPOSConnection.Size = new System.Drawing.Size(594, 23);
            this.txtPOSConnection.TabIndex = 0;
            this.txtPOSConnection.Text = "data source=.;Initial Catalog=BLogicPOS7;integrated security = SSPI";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "POS ConnectionString";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(136, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Merchant ConnectionString";
            // 
            // txtMerchantConnection
            // 
            this.txtMerchantConnection.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtMerchantConnection.Location = new System.Drawing.Point(155, 64);
            this.txtMerchantConnection.Name = "txtMerchantConnection";
            this.txtMerchantConnection.Size = new System.Drawing.Size(594, 23);
            this.txtMerchantConnection.TabIndex = 2;
            this.txtMerchantConnection.Text = "data source=.;Initial Catalog=Merchant;integrated security = SSPI";
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(155, 105);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(151, 23);
            this.btnConnect.TabIndex = 4;
            this.btnConnect.Text = "Connect Database";
            this.btnConnect.UseVisualStyleBackColor = true;
            // 
            // txtTicketNumber
            // 
            this.txtTicketNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtTicketNumber.Location = new System.Drawing.Point(155, 145);
            this.txtTicketNumber.Name = "txtTicketNumber";
            this.txtTicketNumber.Size = new System.Drawing.Size(169, 26);
            this.txtTicketNumber.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label3.Location = new System.Drawing.Point(12, 148);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(111, 20);
            this.label3.TabIndex = 1;
            this.label3.Text = "Ticket Number";
            // 
            // btnGetScript
            // 
            this.btnGetScript.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnGetScript.Location = new System.Drawing.Point(155, 209);
            this.btnGetScript.Name = "btnGetScript";
            this.btnGetScript.Size = new System.Drawing.Size(169, 27);
            this.btnGetScript.TabIndex = 8;
            this.btnGetScript.Text = "Get script Void";
            this.btnGetScript.UseVisualStyleBackColor = true;
            // 
            // txtScript
            // 
            this.txtScript.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtScript.Location = new System.Drawing.Point(16, 283);
            this.txtScript.Name = "txtScript";
            this.txtScript.Size = new System.Drawing.Size(1173, 155);
            this.txtScript.TabIndex = 9;
            this.txtScript.Text = "";
            // 
            // lblStatusPOS
            // 
            this.lblStatusPOS.AutoSize = true;
            this.lblStatusPOS.ForeColor = System.Drawing.Color.Green;
            this.lblStatusPOS.Location = new System.Drawing.Point(755, 40);
            this.lblStatusPOS.Name = "lblStatusPOS";
            this.lblStatusPOS.Size = new System.Drawing.Size(105, 13);
            this.lblStatusPOS.TabIndex = 5;
            this.lblStatusPOS.Text = "Connection Success";
            this.lblStatusPOS.Visible = false;
            // 
            // lblStatusMerchant
            // 
            this.lblStatusMerchant.AutoSize = true;
            this.lblStatusMerchant.ForeColor = System.Drawing.Color.Green;
            this.lblStatusMerchant.Location = new System.Drawing.Point(755, 69);
            this.lblStatusMerchant.Name = "lblStatusMerchant";
            this.lblStatusMerchant.Size = new System.Drawing.Size(105, 13);
            this.lblStatusMerchant.TabIndex = 5;
            this.lblStatusMerchant.Text = "Connection Success";
            this.lblStatusMerchant.Visible = false;
            // 
            // VoidSaleReceipt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1201, 450);
            this.Controls.Add(this.txtScript);
            this.Controls.Add(this.btnGetScript);
            this.Controls.Add(this.txtTicketNumber);
            this.Controls.Add(this.lblStatusMerchant);
            this.Controls.Add(this.lblStatusPOS);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtMerchantConnection);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtPOSConnection);
            this.Name = "VoidSaleReceipt";
            this.Text = "VoidSaleReceipt";
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
        private System.Windows.Forms.Button btnGetScript;
        private System.Windows.Forms.RichTextBox txtScript;
        private System.Windows.Forms.Label lblStatusPOS;
        private System.Windows.Forms.Label lblStatusMerchant;
    }
}