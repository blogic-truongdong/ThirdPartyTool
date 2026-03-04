namespace ThirdPartyTool.AdvancedFunctions
{
    partial class AdvancedFuntionView
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
            this.btnCloseUnPaidTicket = new System.Windows.Forms.Label();
            this.btnClearTableStatus = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnCloseUnPaidTicket
            // 
            this.btnCloseUnPaidTicket.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.btnCloseUnPaidTicket.Font = new System.Drawing.Font("BLogic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCloseUnPaidTicket.Location = new System.Drawing.Point(20, 20);
            this.btnCloseUnPaidTicket.Margin = new System.Windows.Forms.Padding(5);
            this.btnCloseUnPaidTicket.Name = "btnCloseUnPaidTicket";
            this.btnCloseUnPaidTicket.Size = new System.Drawing.Size(200, 50);
            this.btnCloseUnPaidTicket.TabIndex = 0;
            this.btnCloseUnPaidTicket.Text = "Close UnPaid Ticket by Date Range";
            this.btnCloseUnPaidTicket.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnClearTableStatus
            // 
            this.btnClearTableStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.btnClearTableStatus.Font = new System.Drawing.Font("BLogic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClearTableStatus.Location = new System.Drawing.Point(230, 20);
            this.btnClearTableStatus.Margin = new System.Windows.Forms.Padding(5);
            this.btnClearTableStatus.Name = "btnClearTableStatus";
            this.btnClearTableStatus.Size = new System.Drawing.Size(200, 50);
            this.btnClearTableStatus.TabIndex = 1;
            this.btnClearTableStatus.Text = "Clear Table Status";
            this.btnClearTableStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // AdvancedFuntionView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.btnClearTableStatus);
            this.Controls.Add(this.btnCloseUnPaidTicket);
            this.Name = "AdvancedFuntionView";
            this.Size = new System.Drawing.Size(628, 470);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Label btnCloseUnPaidTicket;
        public System.Windows.Forms.Label btnClearTableStatus;
    }
}
