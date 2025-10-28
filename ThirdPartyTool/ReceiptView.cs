using System;

namespace ThirdPartyTool
{
    public partial class ReceiptView : System.Windows.Forms.UserControl
    {
        private string CurrentOption = string.Empty;

        public ReceiptView()
        {
            InitializeComponent();

            RegisterEvent();
        }

        private void RegisterEvent()
        {
            this.Load += ReceiptView_Load;

            btnAddTipBySaleToken.Click += BtnAddTipBySaleToken_Click;
            btnAddTipForPaymentDetail.Click += BtnAddTipForPaymentDetail_Click;
            btnUpdateStatusReceipt.Click += BtnUpdateStatusReceipt_Click;

            addTipBySaleTokenView.btnBack.Click += BtnBack_Click;
            addTipForPaymentDetailView.btnBack.Click += BtnBack_Click;
            updateStatusReceiptView.btnBack.Click += BtnBack_Click;

        }

        private void BtnUpdateStatusReceipt_Click(object sender, EventArgs e)
        {
            if (CurrentOption == "UpdateStatusReceipt")
            {
                return;
            }
            CurrentOption = "UpdateStatusReceipt";

            updateStatusReceiptView.Visible = true;
            updateStatusReceiptView.BringToFront();
        }

        private void BtnAddTipForPaymentDetail_Click(object sender, EventArgs e)
        {
            if (CurrentOption == "AddTipForPaymentDetailView")
            {
                return;
            }
            CurrentOption = "AddTipForPaymentDetailView";

            addTipForPaymentDetailView.Visible = true;
            addTipForPaymentDetailView.BringToFront();
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            CurrentOption = string.Empty;
            addTipBySaleTokenView.Visible = false;
            addTipForPaymentDetailView.Visible = false;
            updateStatusReceiptView.Visible = false;
        }

        private void BtnAddTipBySaleToken_Click(object sender, EventArgs e)
        {
            if (CurrentOption == "AddTipBySaleToken")
            {
                return;
            }
            CurrentOption = "AddTipBySaleToken";

            addTipBySaleTokenView.Visible = true;
            addTipBySaleTokenView.BringToFront();
        }

        private void ReceiptView_Load(object sender, EventArgs e)
        {

        }

    }
}
