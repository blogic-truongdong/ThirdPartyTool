using System;
using System.Data;
using System.Windows.Forms;
using ThirdPartyTool.Dialog;

namespace ThirdPartyTool.Item
{
    public partial class ReSyncItemTypeView : UserControl
    {
        private DataTable dtEmployees = null;
        private DataTable dtRoles = null;
        private DataTable dtEmployeeRoles = null;
        private int CurrentEmployeeID = -1;
        private string CurrentEmployeeName = "";

        public ReSyncItemTypeView()
        {
            InitializeComponent();

            this.Load += EmployeeView_Load;

            startDate.ValueChanged += StartDate_ValueChanged;
            endDate.ValueChanged += EndDate_ValueChanged;

            lblStartDate.Click += LblStartDate_Click;
            lblEndDate.Click += LblEndDate_Click;

            btnSyncRoles.Click += BtnSyncRoles_Click;

            btnBack.Click += BtnBack_Click;
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        private void BtnSyncRoles_Click(object sender, EventArgs e)
        {
            try
            {
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
                BLogicLogger.Exception("BtnSyncRoles_Click in EmployeeView", ex);
            }
        }

        private void LblEndDate_Click(object sender, EventArgs e)
        {
            endDate.Select(); // focus vào control
            SendKeys.Send("%{DOWN}");
        }

        private void LblStartDate_Click(object sender, EventArgs e)
        {
            startDate.Select(); // focus vào control
            SendKeys.Send("%{DOWN}");
        }

        private void EndDate_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                lblEndDate.Text = endDate.Value.ToShortDateString();
            }
            catch (Exception ex)
            {
                BLogicLogger.Exception("EndDate_ValueChanged in EmployeeView", ex);

            }
        }

        private void StartDate_ValueChanged(object sender, EventArgs e)
        {
            try
            {

                lblStartDate.Text = startDate.Value.ToShortDateString();
            }
            catch (Exception ex)
            {
                BLogicLogger.Exception("StartDate_ValueChanged in EmployeeView", ex);
            }
        }

        private void EmployeeView_Load(object sender, EventArgs e)
        {
            try
            {
                startDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                endDate.Value = DateTime.Now;
            }
            catch (Exception ex)
            {
                BLogicLogger.Exception("EmployeeView_Load in EmployeeView", ex);
            }
        }

    }
}
