using ThirdPartyTool.CustomControl;

namespace ThirdPartyTool
{
    partial class ReceiptView
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
            this.pnlContainer = new System.Windows.Forms.Panel();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.btnRefund = new ThirdPartyTool.CustomControl.BLabel_Rounded();
            this.btnUpdateStatusReceipt = new ThirdPartyTool.CustomControl.BLabel_Rounded();
            this.btnAddTipForPaymentDetail = new ThirdPartyTool.CustomControl.BLabel_Rounded();
            this.btnAddTipBySaleToken = new ThirdPartyTool.CustomControl.BLabel_Rounded();
            this.txtFocus = new ThirdPartyTool.CustomControl.BTextBox();
            this.addTipBySaleTokenView = new ThirdPartyTool.SaleReceipt.AddTipBySaleTokenView();
            this.addTipForPaymentDetailView = new ThirdPartyTool.SaleReceipt.AddTipForPaymentDetailView();
            this.updateStatusReceiptView = new ThirdPartyTool.SaleReceipt.UpdateStatusReceiptView();
            this.refundTicketView = new ThirdPartyTool.SaleReceipt.RefundTicketView();
            this.pnlLoading = new System.Windows.Forms.PictureBox();
            this.pnlContainer.SuspendLayout();
            this.pnlTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlLoading)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlContainer
            // 
            this.pnlContainer.Controls.Add(this.addTipBySaleTokenView);
            this.pnlContainer.Controls.Add(this.addTipForPaymentDetailView);
            this.pnlContainer.Controls.Add(this.updateStatusReceiptView);
            this.pnlContainer.Controls.Add(this.refundTicketView);
            this.pnlContainer.Controls.Add(this.pnlTop);
            this.pnlContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContainer.Location = new System.Drawing.Point(10, 10);
            this.pnlContainer.Name = "pnlContainer";
            this.pnlContainer.Size = new System.Drawing.Size(880, 580);
            this.pnlContainer.TabIndex = 0;
            // 
            // pnlTop
            // 
            this.pnlTop.Controls.Add(this.btnRefund);
            this.pnlTop.Controls.Add(this.btnUpdateStatusReceipt);
            this.pnlTop.Controls.Add(this.btnAddTipForPaymentDetail);
            this.pnlTop.Controls.Add(this.btnAddTipBySaleToken);
            this.pnlTop.Controls.Add(this.txtFocus);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(880, 580);
            this.pnlTop.TabIndex = 2;
            // 
            // btnRefund
            // 
            this.btnRefund.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnRefund.BackColor = System.Drawing.Color.White;
            this.btnRefund.BackgroundColor = System.Drawing.Color.White;
            this.btnRefund.BorderColor = System.Drawing.Color.Gray;
            this.btnRefund.BorderRadius = 5;
            this.btnRefund.BorderSize = 1;
            this.btnRefund.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Bold);
            this.btnRefund.ForeColor = System.Drawing.Color.Black;
            this.btnRefund.Location = new System.Drawing.Point(450, 300);
            this.btnRefund.Margin = new System.Windows.Forms.Padding(10);
            this.btnRefund.Name = "btnRefund";
            this.btnRefund.Size = new System.Drawing.Size(300, 150);
            this.btnRefund.TabIndex = 7;
            this.btnRefund.Text = "Refund Ticket";
            this.btnRefund.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnRefund.TextColor = System.Drawing.Color.Black;
            // 
            // btnUpdateStatusReceipt
            // 
            this.btnUpdateStatusReceipt.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnUpdateStatusReceipt.BackColor = System.Drawing.Color.White;
            this.btnUpdateStatusReceipt.BackgroundColor = System.Drawing.Color.White;
            this.btnUpdateStatusReceipt.BorderColor = System.Drawing.Color.Gray;
            this.btnUpdateStatusReceipt.BorderRadius = 5;
            this.btnUpdateStatusReceipt.BorderSize = 1;
            this.btnUpdateStatusReceipt.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Bold);
            this.btnUpdateStatusReceipt.ForeColor = System.Drawing.Color.Black;
            this.btnUpdateStatusReceipt.Location = new System.Drawing.Point(130, 300);
            this.btnUpdateStatusReceipt.Margin = new System.Windows.Forms.Padding(10);
            this.btnUpdateStatusReceipt.Name = "btnUpdateStatusReceipt";
            this.btnUpdateStatusReceipt.Size = new System.Drawing.Size(300, 150);
            this.btnUpdateStatusReceipt.TabIndex = 6;
            this.btnUpdateStatusReceipt.Text = "Close Ticket";
            this.btnUpdateStatusReceipt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnUpdateStatusReceipt.TextColor = System.Drawing.Color.Black;
            // 
            // btnAddTipForPaymentDetail
            // 
            this.btnAddTipForPaymentDetail.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnAddTipForPaymentDetail.BackColor = System.Drawing.Color.White;
            this.btnAddTipForPaymentDetail.BackgroundColor = System.Drawing.Color.White;
            this.btnAddTipForPaymentDetail.BorderColor = System.Drawing.Color.Gray;
            this.btnAddTipForPaymentDetail.BorderRadius = 5;
            this.btnAddTipForPaymentDetail.BorderSize = 1;
            this.btnAddTipForPaymentDetail.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Bold);
            this.btnAddTipForPaymentDetail.ForeColor = System.Drawing.Color.Black;
            this.btnAddTipForPaymentDetail.Location = new System.Drawing.Point(450, 130);
            this.btnAddTipForPaymentDetail.Margin = new System.Windows.Forms.Padding(10);
            this.btnAddTipForPaymentDetail.Name = "btnAddTipForPaymentDetail";
            this.btnAddTipForPaymentDetail.Size = new System.Drawing.Size(300, 150);
            this.btnAddTipForPaymentDetail.TabIndex = 5;
            this.btnAddTipForPaymentDetail.Text = "Add Tip for Payment";
            this.btnAddTipForPaymentDetail.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnAddTipForPaymentDetail.TextColor = System.Drawing.Color.Black;
            // 
            // btnAddTipBySaleToken
            // 
            this.btnAddTipBySaleToken.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnAddTipBySaleToken.BackColor = System.Drawing.Color.White;
            this.btnAddTipBySaleToken.BackgroundColor = System.Drawing.Color.White;
            this.btnAddTipBySaleToken.BorderColor = System.Drawing.Color.Gray;
            this.btnAddTipBySaleToken.BorderRadius = 5;
            this.btnAddTipBySaleToken.BorderSize = 1;
            this.btnAddTipBySaleToken.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Bold);
            this.btnAddTipBySaleToken.ForeColor = System.Drawing.Color.Black;
            this.btnAddTipBySaleToken.Location = new System.Drawing.Point(130, 130);
            this.btnAddTipBySaleToken.Margin = new System.Windows.Forms.Padding(10);
            this.btnAddTipBySaleToken.Name = "btnAddTipBySaleToken";
            this.btnAddTipBySaleToken.Size = new System.Drawing.Size(300, 150);
            this.btnAddTipBySaleToken.TabIndex = 4;
            this.btnAddTipBySaleToken.Text = "Add Tip by Ticket Sale Token";
            this.btnAddTipBySaleToken.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnAddTipBySaleToken.TextColor = System.Drawing.Color.Black;
            // 
            // txtFocus
            // 
            this.txtFocus.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtFocus.Font = new System.Drawing.Font("Roboto", 12F);
            this.txtFocus.ForeColor = System.Drawing.Color.Gray;
            this.txtFocus.Location = new System.Drawing.Point(321, -25);
            this.txtFocus.Name = "txtFocus";
            this.txtFocus.PlaceholderColor = System.Drawing.Color.Gray;
            this.txtFocus.PlaceholderText = "Search by Ticket #";
            this.txtFocus.Size = new System.Drawing.Size(247, 20);
            this.txtFocus.TabIndex = 3;
            this.txtFocus.Text = "Search by Ticket #";
            // 
            // addTipBySaleTokenView
            // 
            this.addTipBySaleTokenView.BackColor = System.Drawing.Color.White;
            this.addTipBySaleTokenView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.addTipBySaleTokenView.Location = new System.Drawing.Point(0, 0);
            this.addTipBySaleTokenView.Name = "addTipBySaleTokenView";
            this.addTipBySaleTokenView.Padding = new System.Windows.Forms.Padding(10);
            this.addTipBySaleTokenView.Size = new System.Drawing.Size(880, 580);
            this.addTipBySaleTokenView.TabIndex = 5;
            // 
            // addTipForPaymentDetailView
            // 
            this.addTipForPaymentDetailView.BackColor = System.Drawing.Color.White;
            this.addTipForPaymentDetailView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.addTipForPaymentDetailView.Location = new System.Drawing.Point(0, 0);
            this.addTipForPaymentDetailView.Name = "addTipForPaymentDetailView";
            this.addTipForPaymentDetailView.Padding = new System.Windows.Forms.Padding(10);
            this.addTipForPaymentDetailView.Size = new System.Drawing.Size(880, 580);
            this.addTipForPaymentDetailView.TabIndex = 7;
            // 
            // updateStatusReceiptView
            // 
            this.updateStatusReceiptView.BackColor = System.Drawing.Color.White;
            this.updateStatusReceiptView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.updateStatusReceiptView.Location = new System.Drawing.Point(0, 0);
            this.updateStatusReceiptView.Name = "updateStatusReceiptView";
            this.updateStatusReceiptView.Size = new System.Drawing.Size(880, 580);
            this.updateStatusReceiptView.TabIndex = 8;
            // 
            // refundTicketView
            // 
            this.refundTicketView.BackColor = System.Drawing.Color.White;
            this.refundTicketView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.refundTicketView.Location = new System.Drawing.Point(0, 0);
            this.refundTicketView.Name = "refundTicketView";
            this.refundTicketView.Size = new System.Drawing.Size(880, 580);
            this.refundTicketView.TabIndex = 9;
            // 
            // pnlLoading
            // 
            this.pnlLoading.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pnlLoading.Image = global::ThirdPartyTool.Properties.Resources.Loading;
            this.pnlLoading.Location = new System.Drawing.Point(300, 250);
            this.pnlLoading.Name = "pnlLoading";
            this.pnlLoading.Size = new System.Drawing.Size(100, 100);
            this.pnlLoading.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pnlLoading.TabIndex = 3;
            this.pnlLoading.TabStop = false;
            this.pnlLoading.Visible = false;
            // 
            // ReceiptView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.pnlContainer);
            this.Controls.Add(this.pnlLoading);
            this.Name = "ReceiptView";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.Size = new System.Drawing.Size(900, 600);
            this.pnlContainer.ResumeLayout(false);
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlLoading)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlContainer;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.PictureBox pnlLoading;
        public BTextBox txtFocus;
        private SaleReceipt.AddTipBySaleTokenView addTipBySaleTokenView;
        private BLabel_Rounded btnAddTipBySaleToken;
        private BLabel_Rounded btnAddTipForPaymentDetail;
        private SaleReceipt.AddTipForPaymentDetailView addTipForPaymentDetailView;
        private BLabel_Rounded btnUpdateStatusReceipt;
        private SaleReceipt.UpdateStatusReceiptView updateStatusReceiptView;
        private BLabel_Rounded btnRefund;
        private SaleReceipt.RefundTicketView refundTicketView;
    }
}
