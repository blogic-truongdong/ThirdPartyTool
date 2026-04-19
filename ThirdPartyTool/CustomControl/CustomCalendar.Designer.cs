namespace ThirdPartyTool.CustomControl
{
    partial class CustomCalendar
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
            this.monthCalendar = new ThirdPartyTool.CustomControl.CustomMonthCalendar();
            this.dayCalendar = new ThirdPartyTool.CustomControl.CustomDayCalendar();
            this.yearCalendar = new ThirdPartyTool.CustomControl.CustomYearCalendar();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnPrevious = new System.Windows.Forms.Button();
            this.btnDateRange = new System.Windows.Forms.Button();
            this.txtFocus = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // monthCalendar
            // 
            this.monthCalendar.BackColor = System.Drawing.Color.White;
            this.monthCalendar.Location = new System.Drawing.Point(0, 40);
            this.monthCalendar.Name = "monthCalendar";
            this.monthCalendar.Size = new System.Drawing.Size(420, 280);
            this.monthCalendar.TabIndex = 0;
            this.monthCalendar.TabStop = false;
            // 
            // dayCalendar
            // 
            this.dayCalendar.BackColor = System.Drawing.Color.White;
            this.dayCalendar.Location = new System.Drawing.Point(0, 40);
            this.dayCalendar.Name = "dayCalendar";
            this.dayCalendar.Size = new System.Drawing.Size(420, 280);
            this.dayCalendar.TabIndex = 1;
            this.dayCalendar.TabStop = false;
            // 
            // yearCalendar
            // 
            this.yearCalendar.BackColor = System.Drawing.Color.White;
            this.yearCalendar.Location = new System.Drawing.Point(0, 40);
            this.yearCalendar.Name = "yearCalendar";
            this.yearCalendar.Size = new System.Drawing.Size(420, 280);
            this.yearCalendar.TabIndex = 2;
            this.yearCalendar.TabStop = false;
            // 
            // btnNext
            // 
            this.btnNext.BackColor = System.Drawing.Color.White;
            this.btnNext.FlatAppearance.BorderSize = 0;
            this.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNext.Image = global::ThirdPartyTool.Properties.Resources.icon_next_16x16;
            this.btnNext.Location = new System.Drawing.Point(380, 0);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(40, 40);
            this.btnNext.TabIndex = 4;
            this.btnNext.TabStop = false;
            this.btnNext.UseVisualStyleBackColor = false;
            // 
            // btnPrevious
            // 
            this.btnPrevious.BackColor = System.Drawing.Color.White;
            this.btnPrevious.FlatAppearance.BorderSize = 0;
            this.btnPrevious.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrevious.Image = global::ThirdPartyTool.Properties.Resources.icon_previous_16x16;
            this.btnPrevious.Location = new System.Drawing.Point(0, 0);
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Size = new System.Drawing.Size(40, 40);
            this.btnPrevious.TabIndex = 3;
            this.btnPrevious.TabStop = false;
            this.btnPrevious.UseVisualStyleBackColor = false;
            // 
            // btnDateRange
            // 
            this.btnDateRange.BackColor = System.Drawing.Color.White;
            this.btnDateRange.FlatAppearance.BorderSize = 0;
            this.btnDateRange.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDateRange.Font = new System.Drawing.Font("Roboto", 12F);
            this.btnDateRange.Location = new System.Drawing.Point(40, 0);
            this.btnDateRange.Margin = new System.Windows.Forms.Padding(0);
            this.btnDateRange.Name = "btnDateRange";
            this.btnDateRange.Size = new System.Drawing.Size(340, 40);
            this.btnDateRange.TabIndex = 5;
            this.btnDateRange.TabStop = false;
            this.btnDateRange.UseVisualStyleBackColor = false;
            // 
            // txtFocus
            // 
            this.txtFocus.Location = new System.Drawing.Point(197, -100);
            this.txtFocus.Name = "txtFocus";
            this.txtFocus.Size = new System.Drawing.Size(100, 20);
            this.txtFocus.TabIndex = 6;
            // 
            // CustomCalendar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.txtFocus);
            this.Controls.Add(this.btnDateRange);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnPrevious);
            this.Controls.Add(this.dayCalendar);
            this.Controls.Add(this.monthCalendar);
            this.Controls.Add(this.yearCalendar);
            this.Name = "CustomCalendar";
            this.Size = new System.Drawing.Size(420, 318);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CustomMonthCalendar monthCalendar;
        private CustomDayCalendar dayCalendar;
        private CustomYearCalendar yearCalendar;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnPrevious;
        private System.Windows.Forms.Button btnDateRange;
        private System.Windows.Forms.TextBox txtFocus;
    }
}
