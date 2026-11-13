namespace ThirdPartyTool.AdvancedFunctions
{
    partial class ClearDataFor7ShiftsForm
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
            this.pnlContainer = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlLoadingContainer = new System.Windows.Forms.Panel();
            this.pnlLoading = new System.Windows.Forms.PictureBox();
            this.btnCancelProcess = new System.Windows.Forms.Button();
            this.EndDate = new ThirdPartyTool.CustomControl.DateField();
            this.StartDate = new ThirdPartyTool.CustomControl.DateField();
            this.pnlContainer.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pnlLoadingContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlLoading)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlContainer
            // 
            this.pnlContainer.BackColor = System.Drawing.Color.White;
            this.pnlContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlContainer.Controls.Add(this.panel3);
            this.pnlContainer.Controls.Add(this.panel2);
            this.pnlContainer.Controls.Add(this.panel1);
            this.pnlContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContainer.Location = new System.Drawing.Point(0, 0);
            this.pnlContainer.Name = "pnlContainer";
            this.pnlContainer.Size = new System.Drawing.Size(500, 300);
            this.pnlContainer.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.Controls.Add(this.EndDate);
            this.panel3.Controls.Add(this.StartDate);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 56);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(498, 172);
            this.panel3.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("BLogic", 12F);
            this.label3.Location = new System.Drawing.Point(101, 90);
            this.label3.Margin = new System.Windows.Forms.Padding(10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 35);
            this.label3.TabIndex = 1;
            this.label3.Text = "End Date";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("BLogic", 12F);
            this.label2.Location = new System.Drawing.Point(101, 45);
            this.label2.Margin = new System.Windows.Forms.Padding(10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 35);
            this.label2.TabIndex = 0;
            this.label2.Text = "Start Date";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.btnOK);
            this.panel2.Controls.Add(this.btnCancel);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 228);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(498, 70);
            this.panel2.TabIndex = 1;
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(186)))));
            this.btnOK.FlatAppearance.BorderSize = 0;
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOK.Font = new System.Drawing.Font("BLogic", 12F, System.Drawing.FontStyle.Bold);
            this.btnOK.ForeColor = System.Drawing.Color.White;
            this.btnOK.Location = new System.Drawing.Point(260, 10);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(150, 50);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = false;
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.White;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("BLogic", 12F, System.Drawing.FontStyle.Bold);
            this.btnCancel.ForeColor = System.Drawing.Color.Black;
            this.btnCancel.Location = new System.Drawing.Point(90, 10);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(150, 50);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(498, 56);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("BLogic", 14F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(498, 56);
            this.label1.TabIndex = 0;
            this.label1.Text = "Sync Sales Data For 7 Shifts";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlLoadingContainer
            // 
            this.pnlLoadingContainer.BackColor = System.Drawing.Color.White;
            this.pnlLoadingContainer.Controls.Add(this.btnCancelProcess);
            this.pnlLoadingContainer.Controls.Add(this.pnlLoading);
            this.pnlLoadingContainer.Location = new System.Drawing.Point(150, 50);
            this.pnlLoadingContainer.Name = "pnlLoadingContainer";
            this.pnlLoadingContainer.Size = new System.Drawing.Size(200, 200);
            this.pnlLoadingContainer.TabIndex = 8;
            this.pnlLoadingContainer.Visible = false;
            // 
            // pnlLoading
            // 
            this.pnlLoading.BackColor = System.Drawing.Color.White;
            this.pnlLoading.Image = global::ThirdPartyTool.Properties.Resources.loading51;
            this.pnlLoading.Location = new System.Drawing.Point(35, 1);
            this.pnlLoading.Name = "pnlLoading";
            this.pnlLoading.Size = new System.Drawing.Size(130, 130);
            this.pnlLoading.TabIndex = 8;
            this.pnlLoading.TabStop = false;
            // 
            // btnCancelProcess
            // 
            this.btnCancelProcess.BackColor = System.Drawing.Color.White;
            this.btnCancelProcess.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelProcess.Font = new System.Drawing.Font("BLogic", 12F, System.Drawing.FontStyle.Bold);
            this.btnCancelProcess.ForeColor = System.Drawing.Color.Black;
            this.btnCancelProcess.Location = new System.Drawing.Point(25, 149);
            this.btnCancelProcess.Name = "btnCancelProcess";
            this.btnCancelProcess.Size = new System.Drawing.Size(150, 50);
            this.btnCancelProcess.TabIndex = 9;
            this.btnCancelProcess.Text = "Cancel";
            this.btnCancelProcess.UseVisualStyleBackColor = false;
            // 
            // EndDate
            // 
            this.EndDate.Location = new System.Drawing.Point(194, 90);
            this.EndDate.Name = "EndDate";
            this.EndDate.Size = new System.Drawing.Size(200, 40);
            this.EndDate.TabIndex = 5;
            // 
            // StartDate
            // 
            this.StartDate.Location = new System.Drawing.Point(194, 45);
            this.StartDate.Name = "StartDate";
            this.StartDate.Size = new System.Drawing.Size(200, 40);
            this.StartDate.TabIndex = 4;
            // 
            // ReSyncDataFor7ShiftsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 300);
            this.Controls.Add(this.pnlLoadingContainer);
            this.Controls.Add(this.pnlContainer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ReSyncDataFor7ShiftsForm";
            this.Text = "CloseUnPaidTicketForm";
            this.pnlContainer.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.pnlLoadingContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlLoading)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlContainer;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private CustomControl.DateField StartDate;
        private CustomControl.DateField EndDate;
        private System.Windows.Forms.Panel pnlLoadingContainer;
        private System.Windows.Forms.Button btnCancelProcess;
        private System.Windows.Forms.PictureBox pnlLoading;
    }
}