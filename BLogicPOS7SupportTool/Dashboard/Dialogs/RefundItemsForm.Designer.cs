namespace BLogicPOS7SupportTool.Dashboard.Dialogs
{
    partial class RefundItemsForm
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
            this.btnRefund = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblTotal = new System.Windows.Forms.Label();
            this.lblTax = new System.Windows.Forms.Label();
            this.lblSubtotal = new System.Windows.Forms.Label();
            this.pnlFooterDivider = new System.Windows.Forms.Panel();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.pnlTitleBar.SuspendLayout();
            this.pnlFooter.SuspendLayout();
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
            this.lblFormTitle.Size = new System.Drawing.Size(460, 40);
            this.lblFormTitle.TabIndex = 0;
            this.lblFormTitle.Text = "Refund Items";
            this.lblFormTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // pnlFooter
            //
            this.pnlFooter.BackColor = System.Drawing.Color.White;
            this.pnlFooter.Controls.Add(this.btnRefund);
            this.pnlFooter.Controls.Add(this.btnCancel);
            this.pnlFooter.Controls.Add(this.lblTotal);
            this.pnlFooter.Controls.Add(this.lblTax);
            this.pnlFooter.Controls.Add(this.lblSubtotal);
            this.pnlFooter.Controls.Add(this.pnlFooterDivider);
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Location = new System.Drawing.Point(0, 472);
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Size = new System.Drawing.Size(460, 128);
            this.pnlFooter.TabIndex = 2;
            //
            // btnRefund
            //
            this.btnRefund.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            this.btnRefund.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.btnRefund.FlatAppearance.BorderSize = 0;
            this.btnRefund.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefund.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnRefund.ForeColor = System.Drawing.Color.White;
            this.btnRefund.Location = new System.Drawing.Point(344, 86);
            this.btnRefund.Name = "btnRefund";
            this.btnRefund.Size = new System.Drawing.Size(100, 30);
            this.btnRefund.TabIndex = 1;
            this.btnRefund.Text = "Refund";
            this.btnRefund.UseVisualStyleBackColor = false;
            //
            // btnCancel
            //
            this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnCancel.Location = new System.Drawing.Point(236, 86);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 30);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            //
            // lblTotal
            //
            this.lblTotal.AutoSize = true;
            this.lblTotal.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTotal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.lblTotal.Location = new System.Drawing.Point(12, 60);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(150, 20);
            this.lblTotal.TabIndex = 3;
            this.lblTotal.Text = "Total Refund: $0.00";
            //
            // lblTax
            //
            this.lblTax.AutoSize = true;
            this.lblTax.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblTax.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.lblTax.Location = new System.Drawing.Point(13, 38);
            this.lblTax.Name = "lblTax";
            this.lblTax.Size = new System.Drawing.Size(100, 15);
            this.lblTax.TabIndex = 4;
            this.lblTax.Text = "Tax: $0.00";
            //
            // lblSubtotal
            //
            this.lblSubtotal.AutoSize = true;
            this.lblSubtotal.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSubtotal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.lblSubtotal.Location = new System.Drawing.Point(13, 16);
            this.lblSubtotal.Name = "lblSubtotal";
            this.lblSubtotal.Size = new System.Drawing.Size(100, 15);
            this.lblSubtotal.TabIndex = 5;
            this.lblSubtotal.Text = "Subtotal: $0.00";
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
            this.pnlContent.AutoScroll = true;
            this.pnlContent.BackColor = System.Drawing.Color.White;
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(0, 40);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Padding = new System.Windows.Forms.Padding(12, 8, 12, 8);
            this.pnlContent.Size = new System.Drawing.Size(460, 432);
            this.pnlContent.TabIndex = 1;
            //
            // RefundItemsForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(460, 600);
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.pnlFooter);
            this.Controls.Add(this.pnlTitleBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "RefundItemsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "";
            this.pnlTitleBar.ResumeLayout(false);
            this.pnlFooter.ResumeLayout(false);
            this.pnlFooter.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlTitleBar;
        private System.Windows.Forms.Button btnCloseForm;
        private System.Windows.Forms.Label lblFormTitle;
        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.Panel pnlFooterDivider;
        private System.Windows.Forms.Button btnRefund;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblSubtotal;
        private System.Windows.Forms.Label lblTax;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Panel pnlContent;
    }
}
