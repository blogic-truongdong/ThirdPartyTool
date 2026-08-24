namespace BLogicPOS7SupportTool.Dashboard.Dialogs
{
    partial class EditShiftForm
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
            this.btnCloseDlg = new System.Windows.Forms.Button();
            this.lblDlgTitle = new System.Windows.Forms.Label();
            this.lblWorkstationCaption = new System.Windows.Forms.Label();
            this.lblWorkstationValue = new System.Windows.Forms.Label();
            this.lblOpeningTimeCaption = new System.Windows.Forms.Label();
            this.lblOpeningTimeValue = new System.Windows.Forms.Label();
            this.lblClosingTimeCaption = new System.Windows.Forms.Label();
            this.lblClosingTimeValue = new System.Windows.Forms.Label();
            this.pnlDivider = new System.Windows.Forms.Panel();
            this.lblOpeningAmount = new System.Windows.Forms.Label();
            this.txtOpeningAmount = new System.Windows.Forms.TextBox();
            this.lblClosingAmount = new System.Windows.Forms.Label();
            this.txtClosingAmount = new System.Windows.Forms.TextBox();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.pnlTitleBar.SuspendLayout();
            this.SuspendLayout();
            //
            // pnlTitleBar
            //
            this.pnlTitleBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(87)))), ((int)(((byte)(208)))));
            this.pnlTitleBar.Controls.Add(this.btnCloseDlg);
            this.pnlTitleBar.Controls.Add(this.lblDlgTitle);
            this.pnlTitleBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTitleBar.Location = new System.Drawing.Point(0, 0);
            this.pnlTitleBar.Name = "pnlTitleBar";
            this.pnlTitleBar.Size = new System.Drawing.Size(420, 40);
            this.pnlTitleBar.TabIndex = 0;
            this.pnlTitleBar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TitleBar_MouseDown);
            this.pnlTitleBar.MouseMove += new System.Windows.Forms.MouseEventHandler(this.TitleBar_MouseMove);
            //
            // btnCloseDlg
            //
            this.btnCloseDlg.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(87)))), ((int)(((byte)(208)))));
            this.btnCloseDlg.FlatAppearance.BorderSize = 0;
            this.btnCloseDlg.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.btnCloseDlg.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCloseDlg.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnCloseDlg.ForeColor = System.Drawing.Color.White;
            this.btnCloseDlg.Location = new System.Drawing.Point(384, 8);
            this.btnCloseDlg.Name = "btnCloseDlg";
            this.btnCloseDlg.Size = new System.Drawing.Size(28, 24);
            this.btnCloseDlg.TabIndex = 1;
            this.btnCloseDlg.TabStop = false;
            this.btnCloseDlg.Text = "✕";
            this.btnCloseDlg.UseVisualStyleBackColor = false;
            this.btnCloseDlg.Click += new System.EventHandler(this.btnCloseDlg_Click);
            //
            // lblDlgTitle
            //
            this.lblDlgTitle.AutoSize = true;
            this.lblDlgTitle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblDlgTitle.ForeColor = System.Drawing.Color.White;
            this.lblDlgTitle.Location = new System.Drawing.Point(16, 10);
            this.lblDlgTitle.Name = "lblDlgTitle";
            this.lblDlgTitle.Size = new System.Drawing.Size(75, 20);
            this.lblDlgTitle.TabIndex = 0;
            this.lblDlgTitle.Text = "Edit Shift";
            this.lblDlgTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TitleBar_MouseDown);
            this.lblDlgTitle.MouseMove += new System.Windows.Forms.MouseEventHandler(this.TitleBar_MouseMove);
            //
            // lblWorkstationCaption
            //
            this.lblWorkstationCaption.AutoSize = true;
            this.lblWorkstationCaption.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblWorkstationCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.lblWorkstationCaption.Location = new System.Drawing.Point(24, 60);
            this.lblWorkstationCaption.Name = "lblWorkstationCaption";
            this.lblWorkstationCaption.Size = new System.Drawing.Size(77, 15);
            this.lblWorkstationCaption.TabIndex = 1;
            this.lblWorkstationCaption.Text = "Workstation:";
            //
            // lblWorkstationValue
            //
            this.lblWorkstationValue.AutoSize = true;
            this.lblWorkstationValue.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblWorkstationValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.lblWorkstationValue.Location = new System.Drawing.Point(150, 60);
            this.lblWorkstationValue.Name = "lblWorkstationValue";
            this.lblWorkstationValue.Size = new System.Drawing.Size(14, 15);
            this.lblWorkstationValue.TabIndex = 2;
            this.lblWorkstationValue.Text = "-";
            //
            // lblOpeningTimeCaption
            //
            this.lblOpeningTimeCaption.AutoSize = true;
            this.lblOpeningTimeCaption.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblOpeningTimeCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.lblOpeningTimeCaption.Location = new System.Drawing.Point(24, 88);
            this.lblOpeningTimeCaption.Name = "lblOpeningTimeCaption";
            this.lblOpeningTimeCaption.Size = new System.Drawing.Size(83, 15);
            this.lblOpeningTimeCaption.TabIndex = 3;
            this.lblOpeningTimeCaption.Text = "Opening Time:";
            //
            // lblOpeningTimeValue
            //
            this.lblOpeningTimeValue.AutoSize = true;
            this.lblOpeningTimeValue.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblOpeningTimeValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.lblOpeningTimeValue.Location = new System.Drawing.Point(150, 88);
            this.lblOpeningTimeValue.Name = "lblOpeningTimeValue";
            this.lblOpeningTimeValue.Size = new System.Drawing.Size(14, 15);
            this.lblOpeningTimeValue.TabIndex = 4;
            this.lblOpeningTimeValue.Text = "-";
            //
            // lblClosingTimeCaption
            //
            this.lblClosingTimeCaption.AutoSize = true;
            this.lblClosingTimeCaption.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblClosingTimeCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.lblClosingTimeCaption.Location = new System.Drawing.Point(24, 116);
            this.lblClosingTimeCaption.Name = "lblClosingTimeCaption";
            this.lblClosingTimeCaption.Size = new System.Drawing.Size(80, 15);
            this.lblClosingTimeCaption.TabIndex = 5;
            this.lblClosingTimeCaption.Text = "Closing Time:";
            //
            // lblClosingTimeValue
            //
            this.lblClosingTimeValue.AutoSize = true;
            this.lblClosingTimeValue.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblClosingTimeValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.lblClosingTimeValue.Location = new System.Drawing.Point(150, 116);
            this.lblClosingTimeValue.Name = "lblClosingTimeValue";
            this.lblClosingTimeValue.Size = new System.Drawing.Size(14, 15);
            this.lblClosingTimeValue.TabIndex = 6;
            this.lblClosingTimeValue.Text = "-";
            //
            // pnlDivider
            //
            this.pnlDivider.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            this.pnlDivider.Location = new System.Drawing.Point(24, 148);
            this.pnlDivider.Name = "pnlDivider";
            this.pnlDivider.Size = new System.Drawing.Size(372, 1);
            this.pnlDivider.TabIndex = 7;
            //
            // lblOpeningAmount
            //
            this.lblOpeningAmount.AutoSize = true;
            this.lblOpeningAmount.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblOpeningAmount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.lblOpeningAmount.Location = new System.Drawing.Point(24, 164);
            this.lblOpeningAmount.Name = "lblOpeningAmount";
            this.lblOpeningAmount.Size = new System.Drawing.Size(101, 15);
            this.lblOpeningAmount.TabIndex = 8;
            this.lblOpeningAmount.Text = "Opening Amount";
            //
            // txtOpeningAmount
            //
            this.txtOpeningAmount.BackColor = System.Drawing.Color.White;
            this.txtOpeningAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtOpeningAmount.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtOpeningAmount.Location = new System.Drawing.Point(24, 184);
            this.txtOpeningAmount.Name = "txtOpeningAmount";
            this.txtOpeningAmount.Size = new System.Drawing.Size(372, 27);
            this.txtOpeningAmount.TabIndex = 0;
            this.txtOpeningAmount.Text = "";
            //
            // lblClosingAmount
            //
            this.lblClosingAmount.AutoSize = true;
            this.lblClosingAmount.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblClosingAmount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.lblClosingAmount.Location = new System.Drawing.Point(24, 228);
            this.lblClosingAmount.Name = "lblClosingAmount";
            this.lblClosingAmount.Size = new System.Drawing.Size(97, 15);
            this.lblClosingAmount.TabIndex = 9;
            this.lblClosingAmount.Text = "Closing Amount";
            //
            // txtClosingAmount
            //
            this.txtClosingAmount.BackColor = System.Drawing.Color.White;
            this.txtClosingAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtClosingAmount.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtClosingAmount.Location = new System.Drawing.Point(24, 248);
            this.txtClosingAmount.Name = "txtClosingAmount";
            this.txtClosingAmount.Size = new System.Drawing.Size(372, 27);
            this.txtClosingAmount.TabIndex = 1;
            this.txtClosingAmount.Text = "";
            //
            // btnConfirm
            //
            this.btnConfirm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(87)))), ((int)(((byte)(208)))));
            this.btnConfirm.FlatAppearance.BorderSize = 0;
            this.btnConfirm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfirm.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnConfirm.ForeColor = System.Drawing.Color.White;
            this.btnConfirm.Location = new System.Drawing.Point(24, 300);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(180, 40);
            this.btnConfirm.TabIndex = 2;
            this.btnConfirm.Text = "Save";
            this.btnConfirm.UseVisualStyleBackColor = false;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            //
            // btnCancel
            //
            this.btnCancel.BackColor = System.Drawing.Color.White;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.btnCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.btnCancel.Location = new System.Drawing.Point(216, 300);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(180, 40);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            //
            // EditShiftForm
            //
            this.AcceptButton = this.btnConfirm;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(420, 360);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.txtClosingAmount);
            this.Controls.Add(this.lblClosingAmount);
            this.Controls.Add(this.txtOpeningAmount);
            this.Controls.Add(this.lblOpeningAmount);
            this.Controls.Add(this.pnlDivider);
            this.Controls.Add(this.lblClosingTimeValue);
            this.Controls.Add(this.lblClosingTimeCaption);
            this.Controls.Add(this.lblOpeningTimeValue);
            this.Controls.Add(this.lblOpeningTimeCaption);
            this.Controls.Add(this.lblWorkstationValue);
            this.Controls.Add(this.lblWorkstationCaption);
            this.Controls.Add(this.pnlTitleBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "EditShiftForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Edit Shift";
            this.pnlTitleBar.ResumeLayout(false);
            this.pnlTitleBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlTitleBar;
        private System.Windows.Forms.Label lblDlgTitle;
        private System.Windows.Forms.Button btnCloseDlg;
        private System.Windows.Forms.Label lblWorkstationCaption;
        private System.Windows.Forms.Label lblWorkstationValue;
        private System.Windows.Forms.Label lblOpeningTimeCaption;
        private System.Windows.Forms.Label lblOpeningTimeValue;
        private System.Windows.Forms.Label lblClosingTimeCaption;
        private System.Windows.Forms.Label lblClosingTimeValue;
        private System.Windows.Forms.Panel pnlDivider;
        private System.Windows.Forms.Label lblOpeningAmount;
        private System.Windows.Forms.TextBox txtOpeningAmount;
        private System.Windows.Forms.Label lblClosingAmount;
        private System.Windows.Forms.TextBox txtClosingAmount;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Button btnCancel;
    }
}
