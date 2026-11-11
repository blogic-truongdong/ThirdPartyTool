namespace ThirdPartyTool
{
    partial class ProcessForm
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblWaitTitle = new System.Windows.Forms.Label();
            this.lblWaitHint = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            //
            // pictureBox1
            //
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Image = global::ThirdPartyTool.Properties.Resources.loading51;
            this.pictureBox1.Location = new System.Drawing.Point(119, 24);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(60, 60);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            //
            // lblWaitTitle
            //
            this.lblWaitTitle.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Bold);
            this.lblWaitTitle.ForeColor = System.Drawing.Color.FromArgb(33, 33, 33);
            this.lblWaitTitle.Location = new System.Drawing.Point(0, 96);
            this.lblWaitTitle.Name = "lblWaitTitle";
            this.lblWaitTitle.Size = new System.Drawing.Size(298, 24);
            this.lblWaitTitle.TabIndex = 1;
            this.lblWaitTitle.Text = "Please wait…";
            this.lblWaitTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // lblWaitHint
            //
            this.lblWaitHint.Font = new System.Drawing.Font("Roboto", 9.75F);
            this.lblWaitHint.ForeColor = System.Drawing.Color.FromArgb(117, 117, 117);
            this.lblWaitHint.Location = new System.Drawing.Point(0, 122);
            this.lblWaitHint.Name = "lblWaitHint";
            this.lblWaitHint.Size = new System.Drawing.Size(298, 20);
            this.lblWaitHint.TabIndex = 2;
            this.lblWaitHint.Text = "This window closes by itself when done.";
            this.lblWaitHint.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // panel1
            //
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.lblWaitTitle);
            this.panel1.Controls.Add(this.lblWaitHint);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(1, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(298, 164);
            this.panel1.TabIndex = 1;
            //
            // ProcessForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(189, 189, 189);
            this.ClientSize = new System.Drawing.Size(300, 166);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ProcessForm";
            this.Padding = new System.Windows.Forms.Padding(1);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ProcessForm";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblWaitTitle;
        private System.Windows.Forms.Label lblWaitHint;
    }
}
