namespace ThirdPartyTool
{
    partial class HotFixView
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
            this.btnUpgradeTaxPercentForTickets = new ThirdPartyTool.CustomControl.BLabel_Rounded();
            this.btnSyncRole = new ThirdPartyTool.CustomControl.BLabel_Rounded();
            this.SuspendLayout();
            // 
            // btnUpgradeTaxPercentForTickets
            // 
            this.btnUpgradeTaxPercentForTickets.BackColor = System.Drawing.Color.White;
            this.btnUpgradeTaxPercentForTickets.BackgroundColor = System.Drawing.Color.White;
            this.btnUpgradeTaxPercentForTickets.BorderColor = System.Drawing.Color.Gray;
            this.btnUpgradeTaxPercentForTickets.BorderRadius = 5;
            this.btnUpgradeTaxPercentForTickets.BorderSize = 1;
            this.btnUpgradeTaxPercentForTickets.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Bold);
            this.btnUpgradeTaxPercentForTickets.ForeColor = System.Drawing.Color.Black;
            this.btnUpgradeTaxPercentForTickets.Location = new System.Drawing.Point(20, 20);
            this.btnUpgradeTaxPercentForTickets.Margin = new System.Windows.Forms.Padding(10);
            this.btnUpgradeTaxPercentForTickets.Name = "btnUpgradeTaxPercentForTickets";
            this.btnUpgradeTaxPercentForTickets.Size = new System.Drawing.Size(300, 150);
            this.btnUpgradeTaxPercentForTickets.TabIndex = 5;
            this.btnUpgradeTaxPercentForTickets.Text = "Update Tax 7.75% -> 10.5% for all ticket of Americana (20202920) & Le Shrimp - Am" +
    "ericana (20201742) on 23 & 24/10/2025";
            this.btnUpgradeTaxPercentForTickets.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnUpgradeTaxPercentForTickets.TextColor = System.Drawing.Color.Black;
            this.btnUpgradeTaxPercentForTickets.UseMnemonic = false;
            // 
            // btnSyncRole
            // 
            this.btnSyncRole.BackColor = System.Drawing.Color.White;
            this.btnSyncRole.BackgroundColor = System.Drawing.Color.White;
            this.btnSyncRole.BorderColor = System.Drawing.Color.Gray;
            this.btnSyncRole.BorderRadius = 5;
            this.btnSyncRole.BorderSize = 1;
            this.btnSyncRole.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Bold);
            this.btnSyncRole.ForeColor = System.Drawing.Color.Black;
            this.btnSyncRole.Location = new System.Drawing.Point(340, 20);
            this.btnSyncRole.Margin = new System.Windows.Forms.Padding(10);
            this.btnSyncRole.Name = "btnSyncRole";
            this.btnSyncRole.Size = new System.Drawing.Size(300, 150);
            this.btnSyncRole.TabIndex = 5;
            this.btnSyncRole.Text = "Sync Employee Role for All Employee";
            this.btnSyncRole.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnSyncRole.TextColor = System.Drawing.Color.Black;
            this.btnSyncRole.UseMnemonic = false;
            // 
            // HotFixView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnSyncRole);
            this.Controls.Add(this.btnUpgradeTaxPercentForTickets);
            this.Name = "HotFixView";
            this.Size = new System.Drawing.Size(723, 365);
            this.ResumeLayout(false);

        }

        #endregion
        private CustomControl.BLabel_Rounded btnUpgradeTaxPercentForTickets;
        private CustomControl.BLabel_Rounded btnSyncRole;
    }
}
