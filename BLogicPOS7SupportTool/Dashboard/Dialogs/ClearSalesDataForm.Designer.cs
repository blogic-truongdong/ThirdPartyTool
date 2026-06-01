namespace BLogicPOS7SupportTool.Dashboard.Dialogs
{
    partial class ClearSalesDataForm
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
            this.pnlFooter = new System.Windows.Forms.Panel();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.pnlFooterDivider = new System.Windows.Forms.Panel();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.dtpEnd = new System.Windows.Forms.DateTimePicker();
            this.lblEnd = new System.Windows.Forms.Label();
            this.dtpStart = new System.Windows.Forms.DateTimePicker();
            this.lblStart = new System.Windows.Forms.Label();
            this.lblInfo = new System.Windows.Forms.Label();
            this.pnlTitleBar.SuspendLayout();
            this.pnlFooter.SuspendLayout();
            this.pnlContent.SuspendLayout();
            this.SuspendLayout();
            //
            // pnlTitleBar
            //
            this.pnlTitleBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(87)))), ((int)(((byte)(208)))));
            this.pnlTitleBar.Controls.Add(this.btnCloseForm);
            this.pnlTitleBar.Controls.Add(this.lblFormTitle);
            this.pnlTitleBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTitleBar.Location = new System.Drawing.Point(0, 0);
            this.pnlTitleBar.Name = "pnlTitleBar";
            this.pnlTitleBar.Size = new System.Drawing.Size(460, 40);
            this.pnlTitleBar.TabIndex = 0;
            //
            // btnCloseForm
            //
            this.btnCloseForm.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnCloseForm.FlatAppearance.BorderSize = 0;
            this.btnCloseForm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCloseForm.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnCloseForm.ForeColor = System.Drawing.Color.White;
            this.btnCloseForm.Location = new System.Drawing.Point(420, 0);
            this.btnCloseForm.Name = "btnCloseForm";
            this.btnCloseForm.Size = new System.Drawing.Size(40, 40);
            this.btnCloseForm.TabIndex = 1;
            this.btnCloseForm.Text = "X";
            this.btnCloseForm.UseVisualStyleBackColor = false;
            //
            // lblFormTitle
            //
            this.lblFormTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblFormTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblFormTitle.ForeColor = System.Drawing.Color.White;
            this.lblFormTitle.Location = new System.Drawing.Point(0, 0);
            this.lblFormTitle.Name = "lblFormTitle";
            this.lblFormTitle.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.lblFormTitle.Size = new System.Drawing.Size(420, 40);
            this.lblFormTitle.TabIndex = 0;
            this.lblFormTitle.Text = "Clear Sales Data";
            this.lblFormTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // pnlFooter
            //
            this.pnlFooter.BackColor = System.Drawing.Color.White;
            this.pnlFooter.Controls.Add(this.btnClear);
            this.pnlFooter.Controls.Add(this.btnCancel);
            this.pnlFooter.Controls.Add(this.pnlFooterDivider);
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Location = new System.Drawing.Point(0, 228);
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Size = new System.Drawing.Size(460, 52);
            this.pnlFooter.TabIndex = 2;
            //
            // btnClear
            //
            this.btnClear.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnClear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(87)))), ((int)(((byte)(208)))));
            this.btnClear.FlatAppearance.BorderSize = 0;
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnClear.ForeColor = System.Drawing.Color.White;
            this.btnClear.Location = new System.Drawing.Point(320, 12);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(124, 28);
            this.btnClear.TabIndex = 1;
            this.btnClear.Text = "Clear Sales Data";
            this.btnClear.UseVisualStyleBackColor = false;
            //
            // btnCancel
            //
            this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnCancel.Location = new System.Drawing.Point(212, 12);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 28);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            //
            // pnlFooterDivider
            //
            this.pnlFooterDivider.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            this.pnlFooterDivider.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFooterDivider.Location = new System.Drawing.Point(0, 0);
            this.pnlFooterDivider.Name = "pnlFooterDivider";
            this.pnlFooterDivider.Size = new System.Drawing.Size(460, 1);
            this.pnlFooterDivider.TabIndex = 0;
            //
            // pnlContent
            //
            this.pnlContent.BackColor = System.Drawing.Color.White;
            this.pnlContent.Controls.Add(this.dtpEnd);
            this.pnlContent.Controls.Add(this.lblEnd);
            this.pnlContent.Controls.Add(this.dtpStart);
            this.pnlContent.Controls.Add(this.lblStart);
            this.pnlContent.Controls.Add(this.lblInfo);
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(0, 40);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Padding = new System.Windows.Forms.Padding(16);
            this.pnlContent.Size = new System.Drawing.Size(460, 188);
            this.pnlContent.TabIndex = 1;
            //
            // dtpEnd
            //
            this.dtpEnd.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.dtpEnd.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEnd.Location = new System.Drawing.Point(120, 128);
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.Size = new System.Drawing.Size(160, 27);
            this.dtpEnd.TabIndex = 4;
            //
            // lblEnd
            //
            this.lblEnd.AutoSize = true;
            this.lblEnd.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblEnd.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.lblEnd.Location = new System.Drawing.Point(16, 132);
            this.lblEnd.Name = "lblEnd";
            this.lblEnd.Size = new System.Drawing.Size(68, 19);
            this.lblEnd.TabIndex = 3;
            this.lblEnd.Text = "End date:";
            //
            // dtpStart
            //
            this.dtpStart.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.dtpStart.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpStart.Location = new System.Drawing.Point(120, 88);
            this.dtpStart.Name = "dtpStart";
            this.dtpStart.Size = new System.Drawing.Size(160, 27);
            this.dtpStart.TabIndex = 2;
            //
            // lblStart
            //
            this.lblStart.AutoSize = true;
            this.lblStart.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblStart.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.lblStart.Location = new System.Drawing.Point(16, 92);
            this.lblStart.Name = "lblStart";
            this.lblStart.Size = new System.Drawing.Size(72, 19);
            this.lblStart.TabIndex = 1;
            this.lblStart.Text = "Start date:";
            //
            // lblInfo
            //
            this.lblInfo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.lblInfo.Location = new System.Drawing.Point(16, 16);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(428, 60);
            this.lblInfo.TabIndex = 0;
            this.lblInfo.Text = "Closes open (OPER) tickets that have $0 paid and were created within the selected" +
    " date range. Their status becomes Closed (CLO). This cannot be undone.";
            //
            // ClearSalesDataForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(460, 280);
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.pnlFooter);
            this.Controls.Add(this.pnlTitleBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ClearSalesDataForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "";
            this.pnlTitleBar.ResumeLayout(false);
            this.pnlFooter.ResumeLayout(false);
            this.pnlContent.ResumeLayout(false);
            this.pnlContent.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlTitleBar;
        private System.Windows.Forms.Button btnCloseForm;
        private System.Windows.Forms.Label lblFormTitle;
        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.Panel pnlFooterDivider;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.Label lblStart;
        private System.Windows.Forms.DateTimePicker dtpStart;
        private System.Windows.Forms.Label lblEnd;
        private System.Windows.Forms.DateTimePicker dtpEnd;
    }
}
