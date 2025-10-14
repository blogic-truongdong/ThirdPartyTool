using BLogicSystems.XPos.Utilites;
using DevExpress.XtraReports.UI;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using ThirdPartyTool.Dialog;

namespace ThirdPartyTool.SaleReceipt
{
    public partial class AddTipForPaymentDetailView : UserControl
    {
        private string MainSaleReceiptID = string.Empty;
        private string MainSaleReceiptNumber = string.Empty;

        public AddTipForPaymentDetailView()
        {
            InitializeComponent();


            txtSearch.KeyUp += TxtSearch_KeyUp;
            btnSearch.Click += BtnSearch_Click;

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

                XtraReport xReport = processForm.ReceiptTemplate;
                if (xReport == null)
                    return;

                MainSaleReceiptID = processForm.GetReceiptTemplateByTicketNumber_SaleReceiptID;
                MainSaleReceiptNumber = processForm.GetReceiptTemplateByTicketNumber_SaleReceiptNumber;

                DataTable dtSaleReceiptPaymentDetails = new DataTable("SaleReceiptPaymentDetails");
                DataTable dtTransactionDetail = new DataTable("TransactionDetail");

                using (SqlCommand sqlCommand = new SqlCommand())
                using (var conn = new SqlConnection(Helper.BLOGICPOS7_Connection))
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();

                    sqlCommand.Connection = conn;
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.CommandText = $"select  a.*, b.PaymentMethodName " +
                        $"from SaleReceiptPaymentDetails a left join PaymentMethods b on a.PaymentMethodID = b.PaymentMethodID " +
                        $"where SaleReceiptID = '{MainSaleReceiptID}'";
                    var reader = sqlCommand.ExecuteReader();
                    dtSaleReceiptPaymentDetails.Load(reader);
                    reader.Close();

                    sqlCommand.CommandText = $"select * from {Helper.Merchant_Name}.dbo.TransactionDetail where ID in (select CreditCardTransactionID from {Helper.BLOGICPOS7_Name}.dbo.SaleReceiptPaymentDetails a where a.SaleReceiptID = '{MainSaleReceiptID}')";
                    reader = sqlCommand.ExecuteReader();
                    dtTransactionDetail.Load(reader);
                    reader.Close();
                }

                pnlPaymentDetails.Controls.Clear();

                foreach (DataRow item in dtSaleReceiptPaymentDetails.Rows)
                {
                    PaymentDetailView paymentDetailView = new PaymentDetailView();
                    paymentDetailView.Dock = DockStyle.Top;
                    paymentDetailView.SaleReceiptID = Utility.GetStringFromRow("SaleReceiptID", item);
                    paymentDetailView.SaleReceiptPaymentDetailID = Utility.GetStringFromRow("SaleReceiptPaymentDetailID", item);
                    paymentDetailView.lblPaymentMethod.Text = Utility.GetStringFromRow("PaymentMethodName", item);
                    paymentDetailView.lblPaymentValue.Text = Utility.ConvertDecimalToMoneyString(Utility.GetDecimalFromRow("AmountTendered", item) -
                        Utility.GetDecimalFromRow("AmountReturn", item) + Utility.GetDecimalFromRow("Gratuity", item) +
                        Utility.GetDecimalFromRow("SVCFeeAmount", item) - Utility.GetDecimalFromRow("CashDiscountAmount", item));
                    paymentDetailView.lblCardValue.Text = Utility.GetStringFromRow("CreditCardTransactionCardNumber", item);
                    paymentDetailView.lblCardType.Text = Utility.GetStringFromRow("CreditCardCardType", item);
                    paymentDetailView.lblApprovalValue.Text = Utility.GetStringFromRow("CreditCardAuthCode", item);
                    paymentDetailView.lblTotalValue.Text = Utility.ConvertDecimalToMoneyString(Utility.GetDecimalFromRow("AmountTendered", item) -
                        Utility.GetDecimalFromRow("AmountReturn", item) + Utility.GetDecimalFromRow("Gratuity", item) +
                        Utility.GetDecimalFromRow("SVCFeeAmount", item) - Utility.GetDecimalFromRow("CashDiscountAmount", item));
                    paymentDetailView.lblTerminalValue.Text = Utility.GetStringFromRow("CreditCardTransactionMerchantID", item);
                    paymentDetailView.txtGratuity.Value = Utility.GetDecimalFromRow("Gratuity", item);
                    paymentDetailView.OldGratuity = Utility.GetDecimalFromRow("Gratuity", item);
                    DataRow[] drs = dtTransactionDetail.Select($"ID = '{item["CreditCardTransactionID"]}'", "");
                    if (drs.Length > 0)
                    {
                        paymentDetailView.lblEntryMode.Text = Utility.GetStringFromRow("CCEntry", drs[0]);
                        paymentDetailView.lblBatchValue.Text = Utility.GetStringFromRow("BatchID", drs[0]);
                    }
                    paymentDetailView.btnUpdate.Click += BtnUpdate_Click;
                    pnlPaymentDetails.Controls.Add(paymentDetailView);
                }

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
                PaymentDetailView paymentDetailView = (sender as Control).Parent.Parent.Parent as PaymentDetailView;
                if (paymentDetailView != null)
                {
                    ConfirmDialogForm confirmFirmDialog = new ConfirmDialogForm(Const.ConfirmTitle, $"Are you sure you want to update ${paymentDetailView.txtGratuity.Value:N2} for this payment detail?");
                    var result = confirmFirmDialog.ShowDialog();
                    if (result == DialogResult.No)
                        return;

                    ProcessForm processForm = new ProcessForm();
                    processForm.InitAddTipForPaymentDetail(paymentDetailView.SaleReceiptID, paymentDetailView.SaleReceiptPaymentDetailID, Utility.GetDecimalFromObject(paymentDetailView.txtGratuity.Text));
                    processForm.ShowDialog();

                    if (processForm.IsSuccess)
                    {
                        InformationDialogForm informationDialogForm = new InformationDialogForm($"Update ${paymentDetailView.txtGratuity.Value} to this payment detail Done. Refresh now.");
                        informationDialogForm.ShowDialog();

                        txtSearch.Text = MainSaleReceiptNumber;
                        BtnSearch_Click(sender, e);
                    }
                    else
                    {
                        ErrorDialogForm errorDialogForm = new ErrorDialogForm($"Update ${paymentDetailView.txtGratuity.Value} to this payment detail Fail. Please contact TruongDS");
                        errorDialogForm.ShowDialog();
                    }
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
