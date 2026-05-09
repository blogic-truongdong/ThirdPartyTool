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

            btnSyncDataFor7Shifts.Click += BtnSyncDataFor7Shifts_Click;
        }

        private void BtnSyncDataFor7Shifts_Click(object sender, EventArgs e)
        {
            ReSyncDataFor7ShiftsForm form = new ReSyncDataFor7ShiftsForm();
            BLogicMessageBox.ShowPopUpFormWithDIMBackground(form, Helper.MainForm);
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
