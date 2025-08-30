namespace ThirdPartyTool
{
    partial class MainForm
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
            this.btnAddTipBySaleToken = new System.Windows.Forms.Button();
            this.btnAddTipForTicket = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnAddTipBySaleToken
            // 
            this.btnAddTipBySaleToken.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddTipBySaleToken.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddTipBySaleToken.Location = new System.Drawing.Point(12, 12);
            this.btnAddTipBySaleToken.Name = "btnAddTipBySaleToken";
            this.btnAddTipBySaleToken.Size = new System.Drawing.Size(300, 100);
            this.btnAddTipBySaleToken.TabIndex = 0;
            this.btnAddTipBySaleToken.Text = "Add Tip by Sale Token Ticket";
            this.btnAddTipBySaleToken.UseVisualStyleBackColor = true;
            // 
            // btnAddTipForTicket
            // 
            this.btnAddTipForTicket.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddTipForTicket.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddTipForTicket.Location = new System.Drawing.Point(318, 12);
            this.btnAddTipForTicket.Name = "btnAddTipForTicket";
            this.btnAddTipForTicket.Size = new System.Drawing.Size(300, 100);
            this.btnAddTipForTicket.TabIndex = 0;
            this.btnAddTipForTicket.Text = "Add Tip For Ticket File 4-4";
            this.btnAddTipForTicket.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(12, 118);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(300, 100);
            this.button1.TabIndex = 1;
            this.button1.Text = "Settle Batch By Merchant ID";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(916, 461);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnAddTipForTicket);
            this.Controls.Add(this.btnAddTipBySaleToken);
            this.Name = "MainForm";
            this.Text = "FormMain";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnAddTipBySaleToken;
        private System.Windows.Forms.Button btnAddTipForTicket;
        private System.Windows.Forms.Button button1;
    }
}