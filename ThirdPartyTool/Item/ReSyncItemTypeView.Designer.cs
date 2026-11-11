namespace ThirdPartyTool.Item
{
    partial class ReSyncItemTypeView
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
            this.pnlCard = new ThirdPartyTool.CustomControl.BPanel_Rounded();
            this.lblCardTitle = new System.Windows.Forms.Label();
            this.lblCardDesc = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.bPanel_Rounded4 = new ThirdPartyTool.CustomControl.BPanel_Rounded();
            this.pnlStartDateClip = new System.Windows.Forms.Panel();
            this.startDate = new System.Windows.Forms.DateTimePicker();
            this.bPanel_Rounded5 = new ThirdPartyTool.CustomControl.BPanel_Rounded();
            this.pnlEndDateClip = new System.Windows.Forms.Panel();
            this.endDate = new System.Windows.Forms.DateTimePicker();
            this.btnSyncRoles = new ThirdPartyTool.CustomControl.BLabel_Rounded();
            this.pnlCard.SuspendLayout();
            this.bPanel_Rounded4.SuspendLayout();
            this.pnlStartDateClip.SuspendLayout();
            this.bPanel_Rounded5.SuspendLayout();
            this.pnlEndDateClip.SuspendLayout();
            this.SuspendLayout();
            //
            // pnlCard
            //
            this.pnlCard.BackColor = System.Drawing.Color.White;
            this.pnlCard.BackgroundColor = System.Drawing.Color.White;
            this.pnlCard.BorderColor = System.Drawing.Color.FromArgb(224, 224, 224);
            this.pnlCard.BorderRadius = 8;
            this.pnlCard.BorderSize = 1;
            this.pnlCard.Controls.Add(this.lblCardTitle);
            this.pnlCard.Controls.Add(this.lblCardDesc);
            this.pnlCard.Controls.Add(this.label5);
            this.pnlCard.Controls.Add(this.label7);
            this.pnlCard.Controls.Add(this.bPanel_Rounded4);
            this.pnlCard.Controls.Add(this.bPanel_Rounded5);
            this.pnlCard.Controls.Add(this.btnSyncRoles);
            this.pnlCard.Location = new System.Drawing.Point(20, 16);
            this.pnlCard.Name = "pnlCard";
            this.pnlCard.Size = new System.Drawing.Size(580, 176);
            this.pnlCard.TabIndex = 0;
            //
            // lblCardTitle
            //
            this.lblCardTitle.AutoSize = true;
            this.lblCardTitle.BackColor = System.Drawing.Color.White;
            this.lblCardTitle.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Bold);
            this.lblCardTitle.ForeColor = System.Drawing.Color.FromArgb(33, 33, 33);
            this.lblCardTitle.Location = new System.Drawing.Point(20, 18);
            this.lblCardTitle.Name = "lblCardTitle";
            this.lblCardTitle.TabIndex = 0;
            this.lblCardTitle.Text = "Update item type for tickets";
            //
            // lblCardDesc
            //
            this.lblCardDesc.BackColor = System.Drawing.Color.White;
            this.lblCardDesc.Font = new System.Drawing.Font("Roboto", 10F);
            this.lblCardDesc.ForeColor = System.Drawing.Color.FromArgb(97, 97, 97);
            this.lblCardDesc.Location = new System.Drawing.Point(20, 44);
            this.lblCardDesc.Name = "lblCardDesc";
            this.lblCardDesc.Size = new System.Drawing.Size(540, 36);
            this.lblCardDesc.TabIndex = 1;
            this.lblCardDesc.Text = "Fills in the missing item type on ticket lines with the item\'s current type. Applies to tickets started in the date range.";
            //
            // label5
            //
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.White;
            this.label5.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Bold);
            this.label5.ForeColor = System.Drawing.Color.FromArgb(97, 97, 97);
            this.label5.Location = new System.Drawing.Point(20, 92);
            this.label5.Name = "label5";
            this.label5.TabIndex = 2;
            this.label5.Text = "From";
            //
            // label7
            //
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.White;
            this.label7.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Bold);
            this.label7.ForeColor = System.Drawing.Color.FromArgb(97, 97, 97);
            this.label7.Location = new System.Drawing.Point(200, 92);
            this.label7.Name = "label7";
            this.label7.TabIndex = 3;
            this.label7.Text = "To";
            //
            // bPanel_Rounded4
            //
            this.bPanel_Rounded4.BackColor = System.Drawing.Color.White;
            this.bPanel_Rounded4.BackgroundColor = System.Drawing.Color.White;
            this.bPanel_Rounded4.BorderColor = System.Drawing.Color.FromArgb(189, 189, 189);
            this.bPanel_Rounded4.BorderRadius = 5;
            this.bPanel_Rounded4.BorderSize = 1;
            this.bPanel_Rounded4.Controls.Add(this.pnlStartDateClip);
            this.bPanel_Rounded4.Location = new System.Drawing.Point(20, 114);
            this.bPanel_Rounded4.Name = "bPanel_Rounded4";
            this.bPanel_Rounded4.Size = new System.Drawing.Size(168, 40);
            this.bPanel_Rounded4.TabIndex = 4;
            //
            // pnlStartDateClip
            //
            this.pnlStartDateClip.Controls.Add(this.startDate);
            this.pnlStartDateClip.Location = new System.Drawing.Point(8, 10);
            this.pnlStartDateClip.Name = "pnlStartDateClip";
            this.pnlStartDateClip.Size = new System.Drawing.Size(152, 20);
            this.pnlStartDateClip.TabIndex = 0;
            //
            // startDate
            //
            this.startDate.CustomFormat = "MM/dd/yyyy";
            this.startDate.Font = new System.Drawing.Font("Roboto", 11F);
            this.startDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.startDate.Location = new System.Drawing.Point(-2, -2);
            this.startDate.Name = "startDate";
            this.startDate.Size = new System.Drawing.Size(156, 25);
            this.startDate.TabIndex = 0;
            //
            // bPanel_Rounded5
            //
            this.bPanel_Rounded5.BackColor = System.Drawing.Color.White;
            this.bPanel_Rounded5.BackgroundColor = System.Drawing.Color.White;
            this.bPanel_Rounded5.BorderColor = System.Drawing.Color.FromArgb(189, 189, 189);
            this.bPanel_Rounded5.BorderRadius = 5;
            this.bPanel_Rounded5.BorderSize = 1;
            this.bPanel_Rounded5.Controls.Add(this.pnlEndDateClip);
            this.bPanel_Rounded5.Location = new System.Drawing.Point(200, 114);
            this.bPanel_Rounded5.Name = "bPanel_Rounded5";
            this.bPanel_Rounded5.Size = new System.Drawing.Size(168, 40);
            this.bPanel_Rounded5.TabIndex = 5;
            //
            // pnlEndDateClip
            //
            this.pnlEndDateClip.Controls.Add(this.endDate);
            this.pnlEndDateClip.Location = new System.Drawing.Point(8, 10);
            this.pnlEndDateClip.Name = "pnlEndDateClip";
            this.pnlEndDateClip.Size = new System.Drawing.Size(152, 20);
            this.pnlEndDateClip.TabIndex = 0;
            //
            // endDate
            //
            this.endDate.CustomFormat = "MM/dd/yyyy";
            this.endDate.Font = new System.Drawing.Font("Roboto", 11F);
            this.endDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.endDate.Location = new System.Drawing.Point(-2, -2);
            this.endDate.Name = "endDate";
            this.endDate.Size = new System.Drawing.Size(156, 25);
            this.endDate.TabIndex = 0;
            //
            // btnSyncRoles
            //
            this.btnSyncRoles.BackColor = System.Drawing.Color.FromArgb(42, 108, 228);
            this.btnSyncRoles.BackgroundColor = System.Drawing.Color.FromArgb(42, 108, 228);
            this.btnSyncRoles.BorderColor = System.Drawing.Color.FromArgb(42, 108, 228);
            this.btnSyncRoles.BorderRadius = 5;
            this.btnSyncRoles.BorderSize = 1;
            this.btnSyncRoles.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSyncRoles.Font = new System.Drawing.Font("Roboto", 11F, System.Drawing.FontStyle.Bold);
            this.btnSyncRoles.ForeColor = System.Drawing.Color.White;
            this.btnSyncRoles.Location = new System.Drawing.Point(380, 114);
            this.btnSyncRoles.Name = "btnSyncRoles";
            this.btnSyncRoles.Size = new System.Drawing.Size(180, 40);
            this.btnSyncRoles.TabIndex = 6;
            this.btnSyncRoles.Text = "Update tickets";
            this.btnSyncRoles.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnSyncRoles.TextColor = System.Drawing.Color.White;
            //
            // ReSyncItemTypeView
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.pnlCard);
            this.Name = "ReSyncItemTypeView";
            this.Size = new System.Drawing.Size(823, 482);
            this.pnlCard.ResumeLayout(false);
            this.pnlCard.PerformLayout();
            this.bPanel_Rounded4.ResumeLayout(false);
            this.pnlStartDateClip.ResumeLayout(false);
            this.bPanel_Rounded5.ResumeLayout(false);
            this.pnlEndDateClip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private CustomControl.BPanel_Rounded pnlCard;
        private System.Windows.Forms.Label lblCardTitle;
        private System.Windows.Forms.Label lblCardDesc;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private CustomControl.BPanel_Rounded bPanel_Rounded4;
        private System.Windows.Forms.Panel pnlStartDateClip;
        private System.Windows.Forms.DateTimePicker startDate;
        private CustomControl.BPanel_Rounded bPanel_Rounded5;
        private System.Windows.Forms.Panel pnlEndDateClip;
        private System.Windows.Forms.DateTimePicker endDate;
        private CustomControl.BLabel_Rounded btnSyncRoles;
    }
}
