namespace ThirdPartyTool.Item
{
    partial class ItemView
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
            this.btnSyncItemType = new ThirdPartyTool.CustomControl.BLabel_Rounded();
            this.reSyncItemTypeView1 = new ThirdPartyTool.Item.ReSyncItemTypeView();
            this.SuspendLayout();
            // 
            // btnSyncItemType
            // 
            this.btnSyncItemType.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnSyncItemType.BackColor = System.Drawing.Color.White;
            this.btnSyncItemType.BackgroundColor = System.Drawing.Color.White;
            this.btnSyncItemType.BorderColor = System.Drawing.Color.Gray;
            this.btnSyncItemType.BorderRadius = 5;
            this.btnSyncItemType.BorderSize = 1;
            this.btnSyncItemType.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Bold);
            this.btnSyncItemType.ForeColor = System.Drawing.Color.Black;
            this.btnSyncItemType.Location = new System.Drawing.Point(300, 175);
            this.btnSyncItemType.Margin = new System.Windows.Forms.Padding(10);
            this.btnSyncItemType.Name = "btnSyncItemType";
            this.btnSyncItemType.Size = new System.Drawing.Size(300, 150);
            this.btnSyncItemType.TabIndex = 6;
            this.btnSyncItemType.Text = "Update Item Type for Tickets by date range";
            this.btnSyncItemType.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnSyncItemType.TextColor = System.Drawing.Color.Black;
            this.btnSyncItemType.UseMnemonic = false;
            // 
            // reSyncItemTypeView1
            // 
            this.reSyncItemTypeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reSyncItemTypeView1.Location = new System.Drawing.Point(0, 0);
            this.reSyncItemTypeView1.Name = "reSyncItemTypeView1";
            this.reSyncItemTypeView1.Size = new System.Drawing.Size(900, 500);
            this.reSyncItemTypeView1.TabIndex = 7;
            this.reSyncItemTypeView1.Visible = false;
            // 
            // ItemView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnSyncItemType);
            this.Controls.Add(this.reSyncItemTypeView1);
            this.Name = "ItemView";
            this.Size = new System.Drawing.Size(900, 500);
            this.ResumeLayout(false);

        }

        #endregion

        private CustomControl.BLabel_Rounded btnSyncItemType;
        private ReSyncItemTypeView reSyncItemTypeView1;
    }
}
