namespace ThirdPartyTool.CustomControl
{
    partial class CustomDateTimePicker
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
            this.lblDateRange = new System.Windows.Forms.Label();
            this.btnDateRange = new System.Windows.Forms.Button();
            this.txtFocus = new System.Windows.Forms.TextBox();
            this.btnPrevious = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblDateRange
            // 
            this.lblDateRange.BackColor = System.Drawing.Color.White;
            this.lblDateRange.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDateRange.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDateRange.Location = new System.Drawing.Point(30, 0);
            this.lblDateRange.Name = "lblDateRange";
            this.lblDateRange.Size = new System.Drawing.Size(110, 40);
            this.lblDateRange.TabIndex = 2;
            this.lblDateRange.Text = "01/01/2026";
            this.lblDateRange.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnDateRange
            // 
            this.btnDateRange.BackColor = System.Drawing.Color.White;
            this.btnDateRange.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnDateRange.FlatAppearance.BorderSize = 0;
            this.btnDateRange.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDateRange.Image = global::ThirdPartyTool.Properties.Resources.icon_calendar_16x16;
            this.btnDateRange.Location = new System.Drawing.Point(140, 0);
            this.btnDateRange.Name = "btnDateRange";
            this.btnDateRange.Size = new System.Drawing.Size(30, 40);
            this.btnDateRange.TabIndex = 3;
            this.btnDateRange.TabStop = false;
            this.btnDateRange.UseVisualStyleBackColor = false;
            // 
            // txtFocus
            // 
            this.txtFocus.Location = new System.Drawing.Point(72, -100);
            this.txtFocus.Name = "txtFocus";
            this.txtFocus.Size = new System.Drawing.Size(100, 20);
            this.txtFocus.TabIndex = 4;
            // 
            // btnPrevious
            // 
            this.btnPrevious.BackColor = System.Drawing.Color.White;
            this.btnPrevious.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnPrevious.FlatAppearance.BorderSize = 0;
            this.btnPrevious.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrevious.Image = global::ThirdPartyTool.Properties.Resources.icon_previous_16x16;
            this.btnPrevious.Location = new System.Drawing.Point(0, 0);
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Size = new System.Drawing.Size(30, 40);
            this.btnPrevious.TabIndex = 0;
            this.btnPrevious.TabStop = false;
            this.btnPrevious.UseVisualStyleBackColor = false;
            // 
            // btnNext
            // 
            this.btnNext.BackColor = System.Drawing.Color.White;
            this.btnNext.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnNext.FlatAppearance.BorderSize = 0;
            this.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNext.Image = global::ThirdPartyTool.Properties.Resources.icon_next_16x16;
            this.btnNext.Location = new System.Drawing.Point(170, 0);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(30, 40);
            this.btnNext.TabIndex = 1;
            this.btnNext.TabStop = false;
            this.btnNext.UseVisualStyleBackColor = false;
            // 
            // CustomDateTimePicker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.txtFocus);
            this.Controls.Add(this.lblDateRange);
            this.Controls.Add(this.btnDateRange);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnPrevious);
            this.Name = "CustomDateTimePicker";
            this.Size = new System.Drawing.Size(200, 40);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblDateRange;
        private System.Windows.Forms.Button btnDateRange;
        private System.Windows.Forms.TextBox txtFocus;
        private System.Windows.Forms.Button btnPrevious;
        private System.Windows.Forms.Button btnNext;
    }
}
