namespace ThirdPartyTool
{
    partial class EmployeeInfo
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.index = new System.Windows.Forms.Label();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.FirstName = new System.Windows.Forms.Label();
            this.LastName = new System.Windows.Forms.Label();
            this.EmployeeID = new System.Windows.Forms.TextBox();
            this.EmployeeCode = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.EmployeeCode);
            this.panel1.Controls.Add(this.EmployeeID);
            this.panel1.Controls.Add(this.LastName);
            this.panel1.Controls.Add(this.FirstName);
            this.panel1.Controls.Add(this.btnUpdate);
            this.panel1.Controls.Add(this.index);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 164);
            this.panel1.TabIndex = 1;
            // 
            // index
            // 
            this.index.Dock = System.Windows.Forms.DockStyle.Top;
            this.index.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.index.Location = new System.Drawing.Point(0, 0);
            this.index.Name = "index";
            this.index.Size = new System.Drawing.Size(198, 31);
            this.index.TabIndex = 0;
            this.index.Text = "label1";
            this.index.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnUpdate
            // 
            this.btnUpdate.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnUpdate.Location = new System.Drawing.Point(0, 134);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(198, 28);
            this.btnUpdate.TabIndex = 1;
            this.btnUpdate.Text = "Update Employee ID for 7 Shifts";
            this.btnUpdate.UseVisualStyleBackColor = true;
            // 
            // FirstName
            // 
            this.FirstName.Dock = System.Windows.Forms.DockStyle.Top;
            this.FirstName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FirstName.Location = new System.Drawing.Point(0, 31);
            this.FirstName.Name = "FirstName";
            this.FirstName.Size = new System.Drawing.Size(198, 25);
            this.FirstName.TabIndex = 2;
            this.FirstName.Text = "label1";
            this.FirstName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // LastName
            // 
            this.LastName.Dock = System.Windows.Forms.DockStyle.Top;
            this.LastName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LastName.Location = new System.Drawing.Point(0, 56);
            this.LastName.Name = "LastName";
            this.LastName.Size = new System.Drawing.Size(198, 25);
            this.LastName.TabIndex = 3;
            this.LastName.Text = "label1";
            this.LastName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // EmployeeID
            // 
            this.EmployeeID.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EmployeeID.Location = new System.Drawing.Point(55, 108);
            this.EmployeeID.Name = "EmployeeID";
            this.EmployeeID.Size = new System.Drawing.Size(100, 26);
            this.EmployeeID.TabIndex = 4;
            this.EmployeeID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // EmployeeCode
            // 
            this.EmployeeCode.Dock = System.Windows.Forms.DockStyle.Top;
            this.EmployeeCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EmployeeCode.Location = new System.Drawing.Point(0, 81);
            this.EmployeeCode.Name = "EmployeeCode";
            this.EmployeeCode.Size = new System.Drawing.Size(198, 25);
            this.EmployeeCode.TabIndex = 5;
            this.EmployeeCode.Text = "label1";
            this.EmployeeCode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // EmployeeInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "EmployeeInfo";
            this.Size = new System.Drawing.Size(200, 164);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.Button btnUpdate;
        public System.Windows.Forms.Label index;
        public System.Windows.Forms.Label LastName;
        public System.Windows.Forms.Label FirstName;
        public System.Windows.Forms.Label EmployeeCode;
        public System.Windows.Forms.TextBox EmployeeID;
    }
}
