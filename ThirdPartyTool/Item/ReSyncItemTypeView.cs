using System;
using System.Windows.Forms;
using ThirdPartyTool.Dialog;

namespace ThirdPartyTool.Item
{
    public partial class ReSyncItemTypeView : UserControl
    {
        public ReSyncItemTypeView()
        {
            InitializeComponent();

            this.Load += ReSyncItemTypeView_Load;

            btnSyncRoles.Click += BtnSyncRoles_Click;
        }

        private void BtnSyncRoles_Click(object sender, EventArgs e)
        {
            try
            {
                if (startDate.Value.Date > endDate.Value.Date)
                {
                    InformationDialogForm invalidRange = new InformationDialogForm("The From date must be on or before the To date.");
                    invalidRange.ShowDialog();
                    return;
                }

                ConfirmDialogForm confirmFirmDialog = new ConfirmDialogForm(Const.ConfirmTitle, $"Are you sure you want to update Item Type for all Tickets from '{startDate.Value.Month}/{startDate.Value.Day}/{startDate.Value.Year}' to '{endDate.Value.Month}/{endDate.Value.Day}/{endDate.Value.Year}'?");
                var result = confirmFirmDialog.ShowDialog();
                if (result == DialogResult.No)
                    return;

                ProcessForm processForm = new ProcessForm();
                processForm.CurrentProcessName = ProcessNameEnum.ReSyncItemType;
                processForm.Item_StartDate = new DateTime(startDate.Value.Year, startDate.Value.Month, startDate.Value.Day, 0, 0, 0);
                processForm.Item_EndDate = new DateTime(endDate.Value.Year, endDate.Value.Month, endDate.Value.Day, 23, 59, 59, 999);
                processForm.ShowDialog();

                if (processForm.IsSuccess)
                {
                    InformationDialogForm informationDialog = new InformationDialogForm("Update Item Type for all tickets successfully.");
                    informationDialog.ShowDialog();
                }
                else
                {
                    InformationDialogForm informationDialog = new InformationDialogForm("Update Item Type for all tickets failed. Please contact TruongDS.");
                    informationDialog.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                BLogicLogger.Exception("BtnSyncRoles_Click in ReSyncItemTypeView", ex);
            }
        }

        private void ReSyncItemTypeView_Load(object sender, EventArgs e)
        {
            try
            {
                startDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                endDate.Value = DateTime.Now;
            }
            catch (Exception ex)
            {
                BLogicLogger.Exception("ReSyncItemTypeView_Load", ex);
            }
        }
    }
}
