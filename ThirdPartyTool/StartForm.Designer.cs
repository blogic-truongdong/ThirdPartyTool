namespace ThirdPartyTool
{
    partial class StartForm
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
            this.PictureLogo = new System.Windows.Forms.PictureBox();
            this.PictureLoading = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureLogo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureLoading)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.PictureLogo);
            this.panel1.Controls.Add(this.PictureLoading);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(711, 413);
            this.panel1.TabIndex = 0;
            // 
            // PictureLogo
            // 
            this.PictureLogo.BackColor = System.Drawing.Color.Transparent;
            this.PictureLogo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.PictureLogo.Image = global::ThirdPartyTool.Properties.Resources.BlogicSystems_White;
            this.PictureLogo.Location = new System.Drawing.Point(100, 100);
            this.PictureLogo.Name = "PictureLogo";
            this.PictureLogo.Size = new System.Drawing.Size(511, 143);
            this.PictureLogo.TabIndex = 9;
            this.PictureLogo.TabStop = false;
            // 
            // PictureLoading
            // 
            this.PictureLoading.BackColor = System.Drawing.Color.Transparent;
            this.PictureLoading.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.PictureLoading.Image = global::ThirdPartyTool.Properties.Resources.BLogic_Start_Loading;
            this.PictureLoading.Location = new System.Drawing.Point(325, 253);
            this.PictureLoading.Name = "PictureLoading";
            this.PictureLoading.Size = new System.Drawing.Size(60, 60);
            this.PictureLoading.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PictureLoading.TabIndex = 8;
            this.PictureLoading.TabStop = false;
            // 
            // StartForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(711, 413);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "StartForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "StartForm";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureLogo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureLoading)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox PictureLogo;
        private System.Windows.Forms.PictureBox PictureLoading;
    }
}