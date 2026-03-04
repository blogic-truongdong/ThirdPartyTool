using BLogicSystems.XPos.ClientUtilities;
using System;
using System.Windows.Forms;

namespace ThirdPartyTool.AdvancedFunctions
{
    public partial class AdvancedFuntionView : UserControl
    {
        public AdvancedFuntionView()
        {
            InitializeComponent();

            this.btnCloseUnPaidTicket.Click += BtnCloseUnPaidTicket_Click;

            btnClearTableStatus.Click += BtnClearTableStatus_Click;
        }

        private void BtnClearTableStatus_Click(object sender, EventArgs e)
        {
            ClearTableStatusForm clearTableStatusForm = new ClearTableStatusForm();
            BLogicMessageBox.ShowPopUpFormWithDIMBackground(clearTableStatusForm, Helper.MainForm);
        }

        private void BtnCloseUnPaidTicket_Click(object sender, EventArgs e)
        {
            CloseUnPaidTicketForm closeUnPaidTicketForm = new CloseUnPaidTicketForm();
            BLogicMessageBox.ShowPopUpFormWithDIMBackground(closeUnPaidTicketForm, Helper.MainForm);
        }
    }
}
