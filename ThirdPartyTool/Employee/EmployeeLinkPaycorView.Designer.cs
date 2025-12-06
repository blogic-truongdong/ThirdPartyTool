namespace ThirdPartyTool.Employee
{
    partial class EmployeeLinkPaycorView
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
            this.btnUpdate = new ThirdPartyTool.CustomControl.BLabel_Rounded();
            this.pnlContainer = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // btnUpdate
            // 
            this.btnUpdate.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnUpdate.BackColor = System.Drawing.Color.White;
            this.btnUpdate.BackgroundColor = System.Drawing.Color.White;
            this.btnUpdate.BorderColor = System.Drawing.Color.Gray;
            this.btnUpdate.BorderRadius = 5;
            this.btnUpdate.BorderSize = 1;
            this.btnUpdate.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdate.ForeColor = System.Drawing.Color.Black;
            this.btnUpdate.Location = new System.Drawing.Point(100, 100);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(300, 100);
            this.btnUpdate.TabIndex = 0;
            this.btnUpdate.Text = "Sync Employee Paycor Now";
            this.btnUpdate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnUpdate.TextColor = System.Drawing.Color.Black;
            this.btnUpdate.Visible = false;
            // 
            // pnlContainer
            // 
            this.pnlContainer.AutoScroll = true;
            this.pnlContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContainer.Location = new System.Drawing.Point(0, 0);
            this.pnlContainer.Name = "pnlContainer";
            this.pnlContainer.Size = new System.Drawing.Size(500, 300);
            this.pnlContainer.TabIndex = 1;
            // 
            // EmployeeLinkPaycorView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.pnlContainer);
            this.Name = "EmployeeLinkPaycorView";
            this.Size = new System.Drawing.Size(500, 300);
            this.ResumeLayout(false);

        }

        #endregion

        private CustomControl.BLabel_Rounded btnUpdate;
        private System.Windows.Forms.Panel pnlContainer;
    }
}
