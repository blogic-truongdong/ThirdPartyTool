namespace ThirdPartyTool.Transactions
{
    partial class SettleBatchOfflineForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblRemainingBatch = new System.Windows.Forms.Label();
            this.btnSettle = new ThirdPartyTool.CustomControl.BLabel_Rounded();
            this.btnClose = new ThirdPartyTool.CustomControl.BLabel_Rounded();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.lblRemainingBatch);
            this.panel1.Controls.Add(this.btnSettle);
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(400, 200);
            this.panel1.TabIndex = 0;
            // 
            // lblRemainingBatch
            // 
            this.lblRemainingBatch.BackColor = System.Drawing.Color.Transparent;
            this.lblRemainingBatch.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRemainingBatch.ForeColor = System.Drawing.Color.Red;
            this.lblRemainingBatch.Location = new System.Drawing.Point(40, 129);
            this.lblRemainingBatch.Name = "lblRemainingBatch";
            this.lblRemainingBatch.Size = new System.Drawing.Size(320, 54);
            this.lblRemainingBatch.TabIndex = 3;
            this.lblRemainingBatch.Text = "There are still 0 batches that haven’t been settled yet.";
            this.lblRemainingBatch.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnSettle
            // 
            this.btnSettle.BackColor = System.Drawing.Color.White;
            this.btnSettle.BackgroundColor = System.Drawing.Color.White;
            this.btnSettle.BorderColor = System.Drawing.Color.Gray;
            this.btnSettle.BorderRadius = 5;
            this.btnSettle.BorderSize = 1;
            this.btnSettle.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSettle.ForeColor = System.Drawing.Color.Black;
            this.btnSettle.Location = new System.Drawing.Point(210, 44);
            this.btnSettle.Name = "btnSettle";
            this.btnSettle.Size = new System.Drawing.Size(150, 70);
            this.btnSettle.TabIndex = 2;
            this.btnSettle.Text = "Settle Batch Offline";
            this.btnSettle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnSettle.TextColor = System.Drawing.Color.Black;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.White;
            this.btnClose.BackgroundColor = System.Drawing.Color.White;
            this.btnClose.BorderColor = System.Drawing.Color.Gray;
            this.btnClose.BorderRadius = 5;
            this.btnClose.BorderSize = 1;
            this.btnClose.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.Black;
            this.btnClose.Location = new System.Drawing.Point(40, 44);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(150, 70);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Close";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnClose.TextColor = System.Drawing.Color.Black;
            // 
            // SettleBatchOfflineForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(400, 200);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SettleBatchOfflineForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SettleBatchOfflineForm";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private CustomControl.BLabel_Rounded btnClose;
        private CustomControl.BLabel_Rounded btnSettle;
        private System.Windows.Forms.Label lblRemainingBatch;
    }
}