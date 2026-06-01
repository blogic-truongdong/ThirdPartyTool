namespace BLogicPOS7SupportTool.Dashboard.Dialogs
{
    partial class AdjustPaymentForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.pnlTitleBar = new System.Windows.Forms.Panel();
            this.btnCloseForm = new System.Windows.Forms.Button();
            this.lblFormTitle = new System.Windows.Forms.Label();
            this.lblMenu = new System.Windows.Forms.Label();
            this.pnlTitleDivider = new System.Windows.Forms.Panel();
            this.pnlColumnHeader = new System.Windows.Forms.Panel();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.pnlTitleBar.SuspendLayout();
            this.SuspendLayout();
            //
            // pnlTitleBar
            //
            this.pnlTitleBar.BackColor = System.Drawing.Color.White;
            this.pnlTitleBar.Controls.Add(this.btnCloseForm);
            this.pnlTitleBar.Controls.Add(this.lblFormTitle);
            this.pnlTitleBar.Controls.Add(this.lblMenu);
            this.pnlTitleBar.Controls.Add(this.pnlTitleDivider);
            this.pnlTitleBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTitleBar.Location = new System.Drawing.Point(0, 0);
            this.pnlTitleBar.Name = "pnlTitleBar";
            this.pnlTitleBar.Size = new System.Drawing.Size(1000, 52);
            this.pnlTitleBar.TabIndex = 0;
            //
            // btnCloseForm
            //
            this.btnCloseForm.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnCloseForm.FlatAppearance.BorderSize = 0;
            this.btnCloseForm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCloseForm.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnCloseForm.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(87)))), ((int)(((byte)(208)))));
            this.btnCloseForm.Location = new System.Drawing.Point(952, 0);
            this.btnCloseForm.Name = "btnCloseForm";
            this.btnCloseForm.Size = new System.Drawing.Size(48, 52);
            this.btnCloseForm.TabIndex = 2;
            this.btnCloseForm.Text = "X";
            this.btnCloseForm.UseVisualStyleBackColor = false;
            //
            // lblFormTitle
            //
            this.lblFormTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblFormTitle.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblFormTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.lblFormTitle.Location = new System.Drawing.Point(56, 0);
            this.lblFormTitle.Name = "lblFormTitle";
            this.lblFormTitle.Size = new System.Drawing.Size(896, 52);
            this.lblFormTitle.TabIndex = 1;
            this.lblFormTitle.Text = "Payments";
            this.lblFormTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // lblMenu
            //
            this.lblMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblMenu.Font = new System.Drawing.Font("Segoe UI", 15F);
            this.lblMenu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(87)))), ((int)(((byte)(208)))));
            this.lblMenu.Location = new System.Drawing.Point(0, 0);
            this.lblMenu.Name = "lblMenu";
            this.lblMenu.Size = new System.Drawing.Size(56, 52);
            this.lblMenu.TabIndex = 0;
            this.lblMenu.Text = "☰";
            this.lblMenu.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // pnlTitleDivider
            //
            this.pnlTitleDivider.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(230)))));
            this.pnlTitleDivider.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlTitleDivider.Location = new System.Drawing.Point(0, 51);
            this.pnlTitleDivider.Name = "pnlTitleDivider";
            this.pnlTitleDivider.Size = new System.Drawing.Size(1000, 1);
            this.pnlTitleDivider.TabIndex = 3;
            //
            // pnlColumnHeader
            //
            this.pnlColumnHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(240)))), ((int)(((byte)(250)))));
            this.pnlColumnHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlColumnHeader.Location = new System.Drawing.Point(0, 52);
            this.pnlColumnHeader.Name = "pnlColumnHeader";
            this.pnlColumnHeader.Size = new System.Drawing.Size(1000, 40);
            this.pnlColumnHeader.TabIndex = 1;
            //
            // pnlContent
            //
            this.pnlContent.AutoScroll = true;
            this.pnlContent.BackColor = System.Drawing.Color.White;
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(0, 92);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Size = new System.Drawing.Size(1000, 468);
            this.pnlContent.TabIndex = 2;
            //
            // AdjustPaymentForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1000, 560);
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.pnlColumnHeader);
            this.Controls.Add(this.pnlTitleBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "AdjustPaymentForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "";
            this.pnlTitleBar.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlTitleBar;
        private System.Windows.Forms.Label lblMenu;
        private System.Windows.Forms.Label lblFormTitle;
        private System.Windows.Forms.Button btnCloseForm;
        private System.Windows.Forms.Panel pnlTitleDivider;
        private System.Windows.Forms.Panel pnlColumnHeader;
        private System.Windows.Forms.Panel pnlContent;
    }
}
