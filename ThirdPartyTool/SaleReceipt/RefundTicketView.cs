using DevExpress.XtraReports.UI;
using System;
using System.Windows.Forms;
using ThirdPartyTool.Dialog;

namespace ThirdPartyTool.SaleReceipt
{
    public partial class RefundTicketView : UserControl
    {
        private string MainSaleReceiptID = string.Empty;
        private string MainSaleReceiptNumber = string.Empty;

        public RefundTicketView()
        {
            InitializeComponent();

            txtSearch.KeyUp += TxtSearch_KeyUp;
            btnSearch.Click += BtnSearch_Click;

            BtnUpdate.Click += BtnUpdate_Click;
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtSearch.Text.Trim()))
                {
                    return;
                }

                ProcessForm processForm = new ProcessForm();
                processForm.InitGetReceiptTemplate(txtSearch.Text.Trim());
                processForm.ShowDialog();

                if (!processForm.IsSuccess)
                {
                    InformationDialogForm errorDialog = new InformationDialogForm("Receipt Not Found");
                    errorDialog.ShowDialog();
                    return;
                }

                XtraReport xReport = processForm.ReceiptTemplate;
                if (xReport == null)
                {
                    MainSaleReceiptID = string.Empty;
                    MainSaleReceiptNumber = string.Empty;

                    PrintControl_Receipt.PrintingSystem?.Dispose();
                    PrintControl_Receipt.PrintingSystem = new DevExpress.XtraPrinting.PrintingSystem();
                    PrintControl_Receipt.Refresh();

                    return;
                }

                MainSaleReceiptID = processForm.SaleReceiptID;
                MainSaleReceiptNumber = processForm.SaleReceiptNumber;

                xReport.PrintingSystem.SetCommandVisibility(DevExpress.XtraPrinting.PrintingSystemCommand.DocumentMap, DevExpress.XtraPrinting.CommandVisibility.All);
                xReport.PrintingSystem.ShowMarginsWarning = false;
                PrintControl_Receipt.PrintingSystem = xReport.PrintingSystem;
                xReport.CreateDocument();
                PrintControl_Receipt.ExecCommand(DevExpress.XtraPrinting.PrintingSystemCommand.ZoomToPageWidth);
            }
            catch (Exception ex)
            {
                BLogicLogger.Exception("BtnSearch_Click", ex, new System.Collections.Generic.List<object>() { txtSearch.Text });
                ErrorDialogForm errorDialog = new ErrorDialogForm($"Something went wrong while searching for the ticket.{Environment.NewLine}Please try again or contact TruongDS.");
                errorDialog.ShowDialog();
            }
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(MainSaleReceiptID))
                {
                    InformationDialogForm informationDialog = new InformationDialogForm("Please search ticket before processing the update.");
                    informationDialog.ShowDialog();
                    return;
                }

                ConfirmDialogForm confirmFirmDialog = new ConfirmDialogForm(Const.ConfirmTitle, $"Are you sure you want to update ticket #{MainSaleReceiptNumber} to CLOSE?");
                var result = confirmFirmDialog.ShowDialog();
                if (result == DialogResult.No)
                    return;

                ProcessForm processForm = new ProcessForm();
                processForm.SaleReceiptID = MainSaleReceiptID;
                processForm.CurrentProcessName = ProcessNameEnum.SaleReceipt_RefundFullAmount;
                processForm.ShowDialog();

                if (processForm.IsSuccess)
                {
                    InformationDialogForm informationDialogForm = new InformationDialogForm($"Update ticket #{MainSaleReceiptNumber} to CLOSE Done. Refresh now.");
                    informationDialogForm.ShowDialog();

                    MainSaleReceiptID = string.Empty;
                    MainSaleReceiptNumber = string.Empty;

                    PrintControl_Receipt.PrintingSystem?.Dispose();
                    PrintControl_Receipt.PrintingSystem = new DevExpress.XtraPrinting.PrintingSystem();
                    PrintControl_Receipt.Refresh();
                }
                else
                {
                    ErrorDialogForm errorDialogForm = new ErrorDialogForm($"ticket# ${MainSaleReceiptNumber} to CLOSE Fail. Please contact TruongDS");
                    errorDialogForm.ShowDialog();
                }

            }
            catch (Exception ex)
            {
                BLogicLogger.Exception("BtnUpdate_Click In AddTipForPaymentDetailView", ex);
            }
        }

        private void TxtSearch_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    BtnSearch_Click(sender, e);
                }
            }
            catch (Exception ex)
            {

            }

        }
    }
}
