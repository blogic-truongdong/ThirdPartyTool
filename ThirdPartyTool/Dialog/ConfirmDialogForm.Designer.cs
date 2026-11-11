namespace ThirdPartyTool.Dialog
{
    partial class ConfirmDialogForm
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
            this.lblMessage = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnYes = new ThirdPartyTool.CustomControl.BLabel_Rounded();
            this.btnNo = new ThirdPartyTool.CustomControl.BLabel_Rounded();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            //
            // panel1
            //
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.lblMessage);
            this.panel1.Controls.Add(this.lblTitle);
            this.panel1.Controls.Add(this.btnYes);
            this.panel1.Controls.Add(this.btnNo);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(1, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(438, 198);
            this.panel1.TabIndex = 1;
            //
            // lblTitle
            //
            this.lblTitle.Font = new System.Drawing.Font("Roboto", 13F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(33, 33, 33);
            this.lblTitle.Location = new System.Drawing.Point(80, 22);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(334, 26);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "Confirm";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.lblTitle.UseMnemonic = false;
            //
            // lblMessage
            //
            this.lblMessage.Font = new System.Drawing.Font("Roboto", 10.5F);
            this.lblMessage.ForeColor = System.Drawing.Color.FromArgb(66, 66, 66);
            this.lblMessage.Location = new System.Drawing.Point(80, 54);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(334, 60);
            this.lblMessage.TabIndex = 2;
            this.lblMessage.Text = "Are you sure you want to update this receipt?";
            this.lblMessage.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.lblMessage.UseMnemonic = false;
            //
            // btnYes
            //
            this.btnYes.BackColor = System.Drawing.Color.FromArgb(42, 108, 228);
            this.btnYes.BackgroundColor = System.Drawing.Color.FromArgb(42, 108, 228);
            this.btnYes.BorderColor = System.Drawing.Color.FromArgb(42, 108, 228);
            this.btnYes.BorderRadius = 5;
            this.btnYes.BorderSize = 1;
            this.btnYes.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnYes.Font = new System.Drawing.Font("Roboto", 11F, System.Drawing.FontStyle.Bold);
            this.btnYes.ForeColor = System.Drawing.Color.White;
            this.btnYes.Location = new System.Drawing.Point(314, 136);
            this.btnYes.Name = "btnYes";
            this.btnYes.Size = new System.Drawing.Size(100, 40);
            this.btnYes.TabIndex = 0;
            this.btnYes.Text = "Yes";
            this.btnYes.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnYes.TextColor = System.Drawing.Color.White;
            //
            // btnNo
            //
            this.btnNo.BackColor = System.Drawing.Color.White;
            this.btnNo.BackgroundColor = System.Drawing.Color.White;
            this.btnNo.BorderColor = System.Drawing.Color.FromArgb(189, 189, 189);
            this.btnNo.BorderRadius = 5;
            this.btnNo.BorderSize = 1;
            this.btnNo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNo.Font = new System.Drawing.Font("Roboto", 11F, System.Drawing.FontStyle.Bold);
            this.btnNo.ForeColor = System.Drawing.Color.FromArgb(66, 66, 66);
            this.btnNo.Location = new System.Drawing.Point(204, 136);
            this.btnNo.Name = "btnNo";
            this.btnNo.Size = new System.Drawing.Size(100, 40);
            this.btnNo.TabIndex = 0;
            this.btnNo.Text = "No";
            this.btnNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnNo.TextColor = System.Drawing.Color.FromArgb(66, 66, 66);
            //
            // ConfirmDialogForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(189, 189, 189);
            this.ClientSize = new System.Drawing.Size(440, 200);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ConfirmDialogForm";
            this.Padding = new System.Windows.Forms.Padding(1);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ConfirmFirmDialog";
            this.TopMost = true;
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private CustomControl.BLabel_Rounded btnNo;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.Label lblTitle;
        private CustomControl.BLabel_Rounded btnYes;
    }
}
