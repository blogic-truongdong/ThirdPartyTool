namespace ThirdPartyTool.Transactions
{
    partial class SettleBatchOfflineView
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
            this.lblRemainingBatch = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnRefresh = new ThirdPartyTool.CustomControl.BLabel_Rounded();
            this.btnSettle = new ThirdPartyTool.CustomControl.BLabel_Rounded();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblRemainingBatch
            // 
            this.lblRemainingBatch.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblRemainingBatch.BackColor = System.Drawing.Color.Transparent;
            this.lblRemainingBatch.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRemainingBatch.ForeColor = System.Drawing.Color.Red;
            this.lblRemainingBatch.Location = new System.Drawing.Point(95, 310);
            this.lblRemainingBatch.Name = "lblRemainingBatch";
            this.lblRemainingBatch.Size = new System.Drawing.Size(620, 50);
            this.lblRemainingBatch.TabIndex = 3;
            this.lblRemainingBatch.Text = "There are still 0 batches that haven’t been settled yet.";
            this.lblRemainingBatch.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btnRefresh);
            this.panel1.Controls.Add(this.lblRemainingBatch);
            this.panel1.Controls.Add(this.btnSettle);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 510);
            this.panel1.TabIndex = 1;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnRefresh.BackColor = System.Drawing.Color.White;
            this.btnRefresh.BackgroundColor = System.Drawing.Color.White;
            this.btnRefresh.BorderColor = System.Drawing.Color.Gray;
            this.btnRefresh.BorderRadius = 5;
            this.btnRefresh.BorderSize = 1;
            this.btnRefresh.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefresh.ForeColor = System.Drawing.Color.Black;
            this.btnRefresh.Location = new System.Drawing.Point(325, 440);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(150, 50);
            this.btnRefresh.TabIndex = 4;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnRefresh.TextColor = System.Drawing.Color.Black;
            // 
            // btnSettle
            // 
            this.btnSettle.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnSettle.BackColor = System.Drawing.Color.White;
            this.btnSettle.BackgroundColor = System.Drawing.Color.White;
            this.btnSettle.BorderColor = System.Drawing.Color.Gray;
            this.btnSettle.BorderRadius = 5;
            this.btnSettle.BorderSize = 1;
            this.btnSettle.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSettle.ForeColor = System.Drawing.Color.Black;
            this.btnSettle.Location = new System.Drawing.Point(250, 150);
            this.btnSettle.Name = "btnSettle";
            this.btnSettle.Size = new System.Drawing.Size(300, 150);
            this.btnSettle.TabIndex = 2;
            this.btnSettle.Text = "Settle Batch Offline Now";
            this.btnSettle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnSettle.TextColor = System.Drawing.Color.Black;
            // 
            // SettleBatchOfflineView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "SettleBatchOfflineView";
            this.Size = new System.Drawing.Size(800, 510);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblRemainingBatch;
        private System.Windows.Forms.Panel panel1;
        private CustomControl.BLabel_Rounded btnSettle;
        private CustomControl.BLabel_Rounded btnRefresh;
    }
}
