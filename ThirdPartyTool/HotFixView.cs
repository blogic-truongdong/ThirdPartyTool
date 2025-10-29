using System.Windows.Forms;
using ThirdPartyTool.Dialog;

namespace ThirdPartyTool
{
    public partial class HotFixView : UserControl
    {
        public HotFixView()
        {
            InitializeComponent();

            btnUpdataTax775Percent.Click += BtnUpdataTax775Percent_Click;
            btnUpgradeTaxPercentForTickets.Click += BtnUpgradeTaxPercentForTickets_Click;
        }

        private void BtnUpgradeTaxPercentForTickets_Click(object sender, System.EventArgs e)
        {
            try
            {
                ConfirmDialogForm confirmFirmDialog = new ConfirmDialogForm(Const.ConfirmTitle, $"Are you sure you want to update Tax 7.75% -> 10.5% for all ticket from 10/23/2025 to 10/24/2025?");
                var result = confirmFirmDialog.ShowDialog();
                if (result == DialogResult.No)
                    return;

                ProcessForm processForm = new ProcessForm();
                processForm.CurrentProcessName = ProcessNameEnum.HotFix_UpgradeTaxForTicket;
                processForm.ShowDialog();

                if (processForm.IsSuccess)
                {
                    InformationDialogForm informationDialogForm = new InformationDialogForm($"Update Tax 7.75% -> 10.5% for all ticket from 10/23/2025 to 10/24/2025 Done.");
                    informationDialogForm.ShowDialog();
                }
                else
                {
                    ErrorDialogForm errorDialogForm = new ErrorDialogForm($"Update Tax 7.75% -> 10.5% for all ticket from 10/23/2025 to 10/24/2025 Fail. Please contact TruongDS");
                    errorDialogForm.ShowDialog();
                }

            }
            catch (System.Exception ex)
            {
                // log exception
                BLogicLogger.Exception("BtnUpdownTaxPercentForTickets_Click in HotFixView", ex);
            }
        }

        private void BtnUpdataTax775Percent_Click(object sender, System.EventArgs e)
        {
            try
            {
                ConfirmDialogForm confirmFirmDialog = new ConfirmDialogForm(Const.ConfirmTitle, $"Are you sure you want to update Tax 7.75% for all ticket from 10/23/2025 to 10/24/2025?");
                var result = confirmFirmDialog.ShowDialog();
                if (result == DialogResult.No)
                    return;

                ProcessForm processForm = new ProcessForm();
                processForm.CurrentProcessName = ProcessNameEnum.HotFix_UpgradeTaxForTicket;
                processForm.ShowDialog();

                if (processForm.IsSuccess)
                {
                    InformationDialogForm informationDialogForm = new InformationDialogForm($"Update Tax 7.75% for all ticket from 10/23/2025 to 10/24/2025 Done.");
                    informationDialogForm.ShowDialog();
                }
                else
                {
                    ErrorDialogForm errorDialogForm = new ErrorDialogForm($"Update Tax 7.75% for all ticket from 10/23/2025 to 10/24/2025 Fail. Please contact TruongDS");
                    errorDialogForm.ShowDialog();
                }

            }
            catch (System.Exception ex)
            {
                // log exception
                BLogicLogger.Exception("BtnUpdataTax775Percent_Click in HotFixView", ex);
            }
        }
    }
}
