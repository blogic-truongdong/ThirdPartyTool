namespace ThirdPartyTool
{
    partial class HomeForm
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
            this.pnlContainer = new System.Windows.Forms.Panel();
            this.pnlLeft = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.btnMerchant = new System.Windows.Forms.Label();
            this.btnEmployee = new System.Windows.Forms.Label();
            this.btnReceipt = new System.Windows.Forms.Label();
            this.btnHome = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.welcomeView = new ThirdPartyTool.WelcomeView();
            this.receiptView = new ThirdPartyTool.ReceiptView();
            this.employeeView = new ThirdPartyTool.Employee.EmployeeView();
            this.settleBatchOfflineView = new ThirdPartyTool.Transactions.SettleBatchOfflineView();
            this.panel1.SuspendLayout();
            this.pnlContainer.SuspendLayout();
            this.pnlLeft.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.pnlContainer);
            this.panel1.Controls.Add(this.pnlLeft);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1000, 700);
            this.panel1.TabIndex = 1;
            // 
            // pnlContainer
            // 
            this.pnlContainer.Controls.Add(this.welcomeView);
            this.pnlContainer.Controls.Add(this.receiptView);
            this.pnlContainer.Controls.Add(this.employeeView);
            this.pnlContainer.Controls.Add(this.settleBatchOfflineView);
            this.pnlContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContainer.Location = new System.Drawing.Point(200, 0);
            this.pnlContainer.Name = "pnlContainer";
            this.pnlContainer.Size = new System.Drawing.Size(798, 698);
            this.pnlContainer.TabIndex = 2;
            // 
            // pnlLeft
            // 
            this.pnlLeft.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(63)))), ((int)(((byte)(84)))));
            this.pnlLeft.Controls.Add(this.panel6);
            this.pnlLeft.Controls.Add(this.panel5);
            this.pnlLeft.Controls.Add(this.panel4);
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlLeft.Location = new System.Drawing.Point(0, 0);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Size = new System.Drawing.Size(200, 698);
            this.pnlLeft.TabIndex = 1;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.btnMerchant);
            this.panel6.Controls.Add(this.btnEmployee);
            this.panel6.Controls.Add(this.btnReceipt);
            this.panel6.Controls.Add(this.btnHome);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(0, 170);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(200, 463);
            this.panel6.TabIndex = 2;
            // 
            // btnMerchant
            // 
            this.btnMerchant.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnMerchant.Font = new System.Drawing.Font("Roboto", 14.25F, System.Drawing.FontStyle.Bold);
            this.btnMerchant.ForeColor = System.Drawing.Color.White;
            this.btnMerchant.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMerchant.Location = new System.Drawing.Point(0, 150);
            this.btnMerchant.Margin = new System.Windows.Forms.Padding(0);
            this.btnMerchant.Name = "btnMerchant";
            this.btnMerchant.Padding = new System.Windows.Forms.Padding(30, 0, 0, 0);
            this.btnMerchant.Size = new System.Drawing.Size(200, 50);
            this.btnMerchant.TabIndex = 4;
            this.btnMerchant.Text = "         Merchant";
            this.btnMerchant.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnEmployee
            // 
            this.btnEmployee.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnEmployee.Font = new System.Drawing.Font("Roboto", 14.25F, System.Drawing.FontStyle.Bold);
            this.btnEmployee.ForeColor = System.Drawing.Color.White;
            this.btnEmployee.Image = global::ThirdPartyTool.Properties.Resources.Icon_Employee;
            this.btnEmployee.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEmployee.Location = new System.Drawing.Point(0, 100);
            this.btnEmployee.Margin = new System.Windows.Forms.Padding(0);
            this.btnEmployee.Name = "btnEmployee";
            this.btnEmployee.Padding = new System.Windows.Forms.Padding(30, 0, 0, 0);
            this.btnEmployee.Size = new System.Drawing.Size(200, 50);
            this.btnEmployee.TabIndex = 2;
            this.btnEmployee.Text = "         Employee";
            this.btnEmployee.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnReceipt
            // 
            this.btnReceipt.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnReceipt.Font = new System.Drawing.Font("Roboto", 14.25F, System.Drawing.FontStyle.Bold);
            this.btnReceipt.ForeColor = System.Drawing.Color.White;
            this.btnReceipt.Image = global::ThirdPartyTool.Properties.Resources.Icon_Receipt;
            this.btnReceipt.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnReceipt.Location = new System.Drawing.Point(0, 50);
            this.btnReceipt.Margin = new System.Windows.Forms.Padding(0);
            this.btnReceipt.Name = "btnReceipt";
            this.btnReceipt.Padding = new System.Windows.Forms.Padding(30, 0, 0, 0);
            this.btnReceipt.Size = new System.Drawing.Size(200, 50);
            this.btnReceipt.TabIndex = 1;
            this.btnReceipt.Text = "         Receipt";
            this.btnReceipt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnHome
            // 
            this.btnHome.BackColor = System.Drawing.Color.Blue;
            this.btnHome.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnHome.Font = new System.Drawing.Font("Roboto", 14.25F, System.Drawing.FontStyle.Bold);
            this.btnHome.ForeColor = System.Drawing.Color.White;
            this.btnHome.Image = global::ThirdPartyTool.Properties.Resources.Icon_Home;
            this.btnHome.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnHome.Location = new System.Drawing.Point(0, 0);
            this.btnHome.Margin = new System.Windows.Forms.Padding(0);
            this.btnHome.Name = "btnHome";
            this.btnHome.Padding = new System.Windows.Forms.Padding(30, 0, 0, 0);
            this.btnHome.Size = new System.Drawing.Size(200, 50);
            this.btnHome.TabIndex = 3;
            this.btnHome.Text = "         Home";
            this.btnHome.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.btnClose);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel5.Location = new System.Drawing.Point(0, 633);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(200, 65);
            this.panel5.TabIndex = 1;
            // 
            // btnClose
            // 
            this.btnClose.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnClose.Font = new System.Drawing.Font("Roboto", 14.25F);
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Image = global::ThirdPartyTool.Properties.Resources.Icon_Exit;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(0, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Padding = new System.Windows.Forms.Padding(50, 0, 0, 0);
            this.btnClose.Size = new System.Drawing.Size(200, 65);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "        Close";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.label2);
            this.panel4.Controls.Add(this.label1);
            this.panel4.Controls.Add(this.pictureBox1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(200, 170);
            this.panel4.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(25, 136);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(150, 23);
            this.label2.TabIndex = 1;
            this.label2.Text = "Administrator";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(25, 113);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(150, 23);
            this.label1.TabIndex = 1;
            this.label1.Text = "Admin";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::ThirdPartyTool.Properties.Resources.Icon_User;
            this.pictureBox1.Location = new System.Drawing.Point(50, 10);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 100);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // welcomeView
            // 
            this.welcomeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.welcomeView.Location = new System.Drawing.Point(0, 0);
            this.welcomeView.Name = "welcomeView";
            this.welcomeView.Size = new System.Drawing.Size(798, 698);
            this.welcomeView.TabIndex = 1;
            // 
            // receiptView
            // 
            this.receiptView.BackColor = System.Drawing.Color.White;
            this.receiptView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.receiptView.Location = new System.Drawing.Point(0, 0);
            this.receiptView.Name = "receiptView";
            this.receiptView.Padding = new System.Windows.Forms.Padding(10);
            this.receiptView.Size = new System.Drawing.Size(798, 698);
            this.receiptView.TabIndex = 0;
            // 
            // employeeView
            // 
            this.employeeView.BackColor = System.Drawing.Color.White;
            this.employeeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.employeeView.Location = new System.Drawing.Point(0, 0);
            this.employeeView.Name = "employeeView";
            this.employeeView.Padding = new System.Windows.Forms.Padding(10, 0, 10, 10);
            this.employeeView.Size = new System.Drawing.Size(798, 698);
            this.employeeView.TabIndex = 2;
            // 
            // settleBatchOfflineView
            // 
            this.settleBatchOfflineView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.settleBatchOfflineView.Location = new System.Drawing.Point(0, 0);
            this.settleBatchOfflineView.Name = "settleBatchOfflineView";
            this.settleBatchOfflineView.Size = new System.Drawing.Size(798, 698);
            this.settleBatchOfflineView.TabIndex = 3;
            // 
            // HomeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1000, 700);
            this.Controls.Add(this.panel1);
            this.Name = "HomeForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "HomeForm";
            this.panel1.ResumeLayout(false);
            this.pnlContainer.ResumeLayout(false);
            this.pnlLeft.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel pnlContainer;
        private System.Windows.Forms.Panel pnlLeft;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label btnEmployee;
        private System.Windows.Forms.Label btnReceipt;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label btnClose;
        private System.Windows.Forms.Panel panel4;
        private ReceiptView receiptView;
        private WelcomeView welcomeView;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label btnHome;
        private Employee.EmployeeView employeeView;
        private System.Windows.Forms.Label btnMerchant;
        private Transactions.SettleBatchOfflineView settleBatchOfflineView;
    }
}