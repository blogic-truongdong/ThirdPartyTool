using BLogicSystems.XPos.Utilites;
using DevExpress.XtraReports.UI;
using Microsoft.Practices.CompositeUI.Utility;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using ThirdPartyTool.Dialog;
using ThirdPartyTool.Models;

namespace ThirdPartyTool.SaleReceipt
{
    public partial class ReceiptView_v2 : UserControl
    {
        private string MainSaleReceiptID = string.Empty;
        private string MainSaleReceiptNumber = string.Empty;
        private string TransactionStatusID = string.Empty;

        private string TokenSaleReceiptID = string.Empty;
        private string TokenSaleReceiptNumber = string.Empty;
        private decimal TokenTotalAmount = 0;

        public ReceiptView_v2()
        {
            InitializeComponent();

            txtSearch.KeyUp += TxtSearch_KeyUp;
            btnSearch.Click += BtnSearch_Click;

            btnSearchTokenReceipt.Click += BtnSearchTokenReceipt_Click;
            txtSearchTokenReceipt.KeyUp += TxtSearchTokenReceipt_KeyUp;

            btnBackCloseTicket.Click += BtnBackCloseTicket_Click;
            btnBack2.Click += BtnBackCloseTicket_Click;
            btnBack3.Click += BtnBackCloseTicket_Click;
            btnBack4.Click += BtnBackCloseTicket_Click;
            btnBack5.Click += BtnBackCloseTicket_Click;
            btnBack6.Click += BtnBackCloseTicket_Click;

            btnShowCloseTicket.Click += BtnShow_Click;
            btnShowVoidTicket.Click += BtnShow_Click;
            btnShowPartialRefundTicket.Click += BtnShow_Click;
            btnShowAddTipBySaleToken.Click += BtnShow_Click;
            btnShowAddTipForPaymentDetail.Click += BtnShow_Click;

            btnProcessCloseTicket.Click += BtnProcessCloseTicket_Click;
            btnProcessVoidTicket.Click += BtnProcessVoidTicket_Click;
            btnProcessCancelTicket.Click += BtnProcessCancelTicket_Click;
            btnProcessRefundTicket.Click += BtnProcessRefundTicket_Click;
            btnProcessAddTipBySaleToken.Click += BtnProcessAddTipBySaleToken_Click;
            BtnProcessRefundPartialItem.Click += BtnProcessRefundPartialItem_Click;
        }

        private void BtnProcessRefundPartialItem_Click(object sender, EventArgs e)
        {
            try
            {
                RequestModel requestModel = new RequestModel();
                requestModel.SaleReceiptID = MainSaleReceiptID;

                foreach (Control item in pnlRefundItemContainer.Controls)
                {
                    RefundModiferRow refundModifier = item as RefundModiferRow;

                    if (refundModifier != null)
                    {
                        if (refundModifier.IsSelectedItem)
                        {
                            requestModel.objectValues.Add(new ObjectValues()
                            {
                                SaleReceiptDetailID = refundModifier.SaleReceiptDetailID,
                            });

                            requestModel.TotalAmount += refundModifier.ExtendedPrice + refundModifier.TaxAmount;
                            requestModel.TaxAmount += refundModifier.TaxAmount;
                        }
                    }

                    RefundItemRow refundItem = item as RefundItemRow;

                    if (refundItem != null)
                    {
                        if (refundItem.IsSelectedItem)
                        {
                            requestModel.objectValues.Add(new ObjectValues()
                            {
                                SaleReceiptDetailID = refundItem.SaleReceiptDetailID,
                            });

                            requestModel.TotalAmount += refundItem.ExtendedPrice + refundItem.TaxAmount;
                            requestModel.TaxAmount += refundItem.TaxAmount;
                        }
                    }
                }

                DataTable dtSaleReceiptPaymentDetails = new DataTable("SaleReceiptPaymentDetails");
                string sql = "";
                using (SqlCommand sqlCommand = new SqlCommand())
                using (var conn = new SqlConnection(Helper.BLOGICPOS7_Connection))
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();

                    sqlCommand.Connection = conn;
                    sqlCommand.CommandType = CommandType.Text;

                    sql = $"select *, b.PaymentMethodName " +
                        $"from SaleReceiptPaymentDetails a left join PaymentMethods b on a.PaymentMethodID = b.PaymentMethodID " +
                        $"where SaleReceiptID = '{MainSaleReceiptID}'";

                    sqlCommand.CommandText = sql;
                    var reader = sqlCommand.ExecuteReader();
                    dtSaleReceiptPaymentDetails.Load(reader);
                    reader.Close();
                }

                if (dtSaleReceiptPaymentDetails?.Rows.Count > 1)
                {
                    SelectSaleReceiptPaymentDetailForm form = new SelectSaleReceiptPaymentDetailForm();
                    form.BindingData(dtSaleReceiptPaymentDetails);
                    if (form.ShowDialog() == DialogResult.OK)
                    {

                    }
                    else
                    {
                        return;
                    }
                }

                if (requestModel.objectValues.Count == 0)
                    return;

                ConfirmDialogForm confirmFirmDialog = new ConfirmDialogForm(Const.ConfirmTitle, $"Are you sure you want to Refund Items of ticket #{MainSaleReceiptNumber}?");
                var result = confirmFirmDialog.ShowDialog();
                if (result == DialogResult.No)
                    return;

                ProcessForm processForm = new ProcessForm();
                processForm.CurrentProcessName = ProcessNameEnum.PartialRefundItems;
                processForm.requestModel = requestModel;
                processForm.ShowDialog();

                if (processForm.IsSuccess)
                {
                    TokenSaleReceiptID = string.Empty;
                    TokenSaleReceiptNumber = string.Empty;

                    PrintControl_TokenReceipt.PrintingSystem?.Dispose();
                    PrintControl_TokenReceipt.PrintingSystem = new DevExpress.XtraPrinting.PrintingSystem();
                    PrintControl_TokenReceipt.Refresh();

                    txtSearch.Text = MainSaleReceiptNumber;

                    ReloadCurrentReceipt();
                }
                else
                {
                    InformationDialogForm errorDialog = new InformationDialogForm("Add Tip Fail. Please contact TruongDS");
                    errorDialog.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                BLogicLogger.Exception("BtnProcessRefundPartialItem_Click", ex);
            }
        }

        private void BtnProcessAddTipBySaleToken_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(MainSaleReceiptNumber) || string.IsNullOrWhiteSpace(TokenSaleReceiptNumber))
            {
                InformationDialogForm informationDialog = new InformationDialogForm("Please search 2 tickets before processing the update.");
                informationDialog.ShowDialog();
                return;
            }

            ConfirmDialogForm confirmFirmDialog = new ConfirmDialogForm(Const.ConfirmTitle, $"Are you sure you want to update ${TokenTotalAmount:N2} tip to ticket #{MainSaleReceiptNumber}?");
            var result = confirmFirmDialog.ShowDialog();
            if (result == DialogResult.No)
                return;

            ProcessForm processForm = new ProcessForm();
            processForm.InitAddTipByTokenReceipt(MainSaleReceiptID, MainSaleReceiptNumber, TokenSaleReceiptID, TokenSaleReceiptNumber);
            processForm.ShowDialog();

            if (processForm.IsSuccess)
            {
                TokenSaleReceiptID = string.Empty;
                TokenSaleReceiptNumber = string.Empty;

                PrintControl_TokenReceipt.PrintingSystem?.Dispose();
                PrintControl_TokenReceipt.PrintingSystem = new DevExpress.XtraPrinting.PrintingSystem();
                PrintControl_TokenReceipt.Refresh();

                txtSearch.Text = MainSaleReceiptNumber;

                ReloadCurrentReceipt();
            }
            else
            {
                InformationDialogForm errorDialog = new InformationDialogForm("Add Tip Fail. Please contact TruongDS");
                errorDialog.ShowDialog();
            }
        }

        private void TxtSearchTokenReceipt_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    BtnSearchTokenReceipt_Click(sender, e);
                }
            }
            catch (Exception ex)
            {
                BLogicLogger.Exception("TxtSearchTokenReceipt_KeyUp in ReceiptView_v2", ex);
            }
        }

        private void BtnSearchTokenReceipt_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtSearchTokenReceipt.Text.Trim()))
                {
                    return;
                }

                ProcessForm processForm = new ProcessForm();
                processForm.InitGetReceiptTemplate(txtSearchTokenReceipt.Text.Trim());
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
                    TokenSaleReceiptID = string.Empty;
                    TokenSaleReceiptNumber = string.Empty;
                    TokenTotalAmount = 0;

                    PrintControl_TokenReceipt.PrintingSystem?.Dispose();
                    PrintControl_TokenReceipt.PrintingSystem = new DevExpress.XtraPrinting.PrintingSystem();
                    PrintControl_TokenReceipt.Refresh();

                    return;
                }

                TokenSaleReceiptID = processForm.GetReceiptTemplateByTicketNumber_SaleReceiptID;
                TokenSaleReceiptNumber = processForm.GetReceiptTemplateByTicketNumber_SaleReceiptNumber;
                TokenTotalAmount = processForm.GetReceiptTemplateByTicketNumber_TotalAmount;
                xReport.PrintingSystem.SetCommandVisibility(DevExpress.XtraPrinting.PrintingSystemCommand.DocumentMap, DevExpress.XtraPrinting.CommandVisibility.All);
                xReport.PrintingSystem.ShowMarginsWarning = false;
                PrintControl_TokenReceipt.PrintingSystem = xReport.PrintingSystem;
                xReport.CreateDocument();
                PrintControl_TokenReceipt.ExecCommand(DevExpress.XtraPrinting.PrintingSystemCommand.ZoomToPageWidth);
            }
            catch (Exception ex)
            {
                BLogicLogger.Exception("BtnSearchTokenReceipt_Click", ex, new System.Collections.Generic.List<object>() { txtSearchTokenReceipt.Text });
                ErrorDialogForm errorDialog = new ErrorDialogForm($"Something went wrong while searching for the token ticket.{Environment.NewLine}Please try again or contact TruongDS.");
                errorDialog.ShowDialog();
            }
        }

        private void BtnProcessRefundTicket_Click(object sender, EventArgs e)
        {
            try
            {
                if (TransactionStatusID != "FIN")
                {
                    InformationDialogForm informationDialogForm = new InformationDialogForm($"Chỉ có thể CANCEL những ticket đang ở trạng thái OPEN.");
                    informationDialogForm.ShowDialog();
                    return;
                }

                ConfirmDialogForm confirmFirmDialog = new ConfirmDialogForm(Const.ConfirmTitle, $"Are you sure you want to CANCEL Ticket #{MainSaleReceiptNumber}?");
                var result = confirmFirmDialog.ShowDialog();
                if (result == DialogResult.No)
                    return;

                ProcessForm processForm = new ProcessForm();
                processForm.SaleReceiptID = MainSaleReceiptID;
                processForm.CurrentProcessName = ProcessNameEnum.CancelReceipt;
                processForm.ShowDialog();

                if (processForm.IsSuccess)
                {
                    InformationDialogForm informationDialogForm = new InformationDialogForm($"CANCEL Ticket#{MainSaleReceiptNumber} Done.");
                    informationDialogForm.ShowDialog();

                    ClearCurrentReceipt();
                }
                else
                {
                    ErrorDialogForm errorDialogForm = new ErrorDialogForm($"CANCEL Ticket# {MainSaleReceiptNumber} Fail. Please contact TruongDS");
                    errorDialogForm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                BLogicLogger.Exception("BtnProcessRefundTicket_Click in ReceiptView_v2", ex);
            }
        }

        private void BtnProcessCancelTicket_Click(object sender, EventArgs e)
        {
            try
            {
                if (TransactionStatusID != "OPER")
                {
                    InformationDialogForm informationDialogForm = new InformationDialogForm($"Chỉ có thể CANCEL những ticket đang ở trạng thái OPER.");
                    informationDialogForm.ShowDialog();
                    return;
                }

                ConfirmDialogForm confirmFirmDialog = new ConfirmDialogForm(Const.ConfirmTitle, $"Are you sure you want to CANCEL Ticket #{MainSaleReceiptNumber}?");
                var result = confirmFirmDialog.ShowDialog();
                if (result == DialogResult.No)
                    return;

                ProcessForm processForm = new ProcessForm();
                processForm.SaleReceiptID = MainSaleReceiptID;
                processForm.CurrentProcessName = ProcessNameEnum.CancelReceipt;
                processForm.ShowDialog();

                if (processForm.IsSuccess)
                {
                    ClearCurrentReceipt();
                }
                else
                {
                    ErrorDialogForm errorDialogForm = new ErrorDialogForm($"VOID Ticket# {MainSaleReceiptNumber} Fail. Please contact TruongDS");
                    errorDialogForm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                BLogicLogger.Exception("BtnProcessCancelTicket_Click in ReceiptView_v2", ex);
            }
        }

        private void BtnProcessVoidTicket_Click(object sender, EventArgs e)
        {
            try
            {
                if (TransactionStatusID != "FIN")
                {
                    InformationDialogForm informationDialogForm = new InformationDialogForm($"Chỉ có thể VOID những ticket đang ở trạng thái FINISH.");
                    informationDialogForm.ShowDialog();
                    return;
                }

                ConfirmDialogForm confirmFirmDialog = new ConfirmDialogForm(Const.ConfirmTitle, $"Are you sure you want to VOID Ticket #{MainSaleReceiptNumber}?");
                var result = confirmFirmDialog.ShowDialog();
                if (result == DialogResult.No)
                    return;

                ProcessForm processForm = new ProcessForm();
                processForm.SaleReceiptID = MainSaleReceiptID;
                processForm.CurrentProcessName = ProcessNameEnum.VoidReceipt;
                processForm.ShowDialog();

                if (processForm.IsSuccess)
                {
                    InformationDialogForm informationDialogForm = new InformationDialogForm($"VOID Ticket# {MainSaleReceiptNumber} Done. Refresh now.");
                    informationDialogForm.ShowDialog();

                    ReloadCurrentReceipt();
                }
                else
                {
                    ErrorDialogForm errorDialogForm = new ErrorDialogForm($"VOID Ticket# {MainSaleReceiptNumber} Fail. Please contact TruongDS");
                    errorDialogForm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                BLogicLogger.Exception("BtnProcessVoidTicket_Click in ReceiptView_v2", ex);
            }
        }

        private void BtnProcessCloseTicket_Click(object sender, EventArgs e)
        {
            try
            {
                if (TransactionStatusID != "FIN" && TransactionStatusID != "OPER" && TransactionStatusID != "RET")
                {
                    InformationDialogForm informationDialogForm = new InformationDialogForm($"Chỉ có thể CLOSE những ticket đang ở trạng thái FINISH hoặc OPEN hoặc REFUND.");
                    informationDialogForm.ShowDialog();
                    return;
                }

                ConfirmDialogForm confirmFirmDialog = new ConfirmDialogForm(Const.ConfirmTitle, $"Are you sure you want to CLOSE Ticket#{MainSaleReceiptNumber}?");
                var result = confirmFirmDialog.ShowDialog();
                if (result == DialogResult.No)
                    return;

                ProcessForm processForm = new ProcessForm();
                processForm.CloseReceipt_SaleReceiptID = MainSaleReceiptID;
                processForm.CurrentProcessName = ProcessNameEnum.CloseReceipt;
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

                    BtnBackCloseTicket_Click(null, null);
                }
                else
                {
                    ErrorDialogForm errorDialogForm = new ErrorDialogForm($"ticket# ${MainSaleReceiptNumber} to CLOSE Fail. Please contact TruongDS");
                    errorDialogForm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                BLogicLogger.Exception("BtnProcessCloseTicket_Click", ex);
            }
        }

        private void BtnShow_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(MainSaleReceiptID))
                {
                    InformationDialogForm informationDialog = new InformationDialogForm("Please search ticket before use this function.");
                    informationDialog.ShowDialog();
                    return;
                }

                System.Windows.Forms.Control control = sender as System.Windows.Forms.Control;

                if (control.Name == btnShowCloseTicket.Name)
                {
                    if (TransactionStatusID != "FIN" && TransactionStatusID != "OPER" && TransactionStatusID != "RET")
                    {
                        InformationDialogForm informationDialogForm = new InformationDialogForm($"Chỉ có thể CLOSE những ticket đang ở trạng thái FINISH hoặc OPEN hoặc REFUND.");
                        informationDialogForm.ShowDialog();
                        return;
                    }

                    pnlCloseTicket.BringToFront();
                }
                else if (control.Name == btnShowVoidTicket.Name)
                {
                    if (TransactionStatusID != "FIN" && TransactionStatusID != "OPER")
                    {
                        InformationDialogForm informationDialogForm = new InformationDialogForm($"Chỉ có thể VOID / CANCEL những ticket đang ở trạng thái FINISH hoặc OPEN.");
                        informationDialogForm.ShowDialog();
                        return;
                    }

                    pnlVoidTicket.BringToFront();
                }
                else if (control.Name == btnShowPartialRefundTicket.Name)
                {
                    if (TransactionStatusID != "FIN")
                    {
                        InformationDialogForm informationDialogForm = new InformationDialogForm($"Chỉ có thể Refund những ticket đang ở trạng thái FINISH.");
                        informationDialogForm.ShowDialog();
                        return;
                    }

                    BindingPartialRefundItems();

                    pnlRefundItems.BringToFront();
                }
                else if (control.Name == btnShowAddTipBySaleToken.Name)
                {
                    if (TransactionStatusID != "FIN")
                    {
                        InformationDialogForm informationDialogForm = new InformationDialogForm($"Chỉ có thể Add Tip cho những ticket đang ở trạng thái FINISH.");
                        informationDialogForm.ShowDialog();
                        return;
                    }

                    pnlAddTipBySaleToken.BringToFront();
                }
                else if (control.Name == btnShowAddTipForPaymentDetail.Name)
                {
                    if (TransactionStatusID != "FIN")
                    {
                        InformationDialogForm informationDialogForm = new InformationDialogForm($"Chỉ có thể Add Tip cho những ticket đang ở trạng thái FINISH.");
                        informationDialogForm.ShowDialog();
                        return;
                    }

                    pnlAddTipForPaymentDetail.BringToFront();
                }
            }
            catch (Exception ex)
            {
                BLogicLogger.Exception("BtnShow_Click in ReceiptView_v2", ex);
            }

        }

        private void BtnBackCloseTicket_Click(object sender, EventArgs e)
        {
            pnlOption.BringToFront();
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            try
            {

                if (string.IsNullOrWhiteSpace(txtSearch.Text.Trim()))
                {
                    return;
                }

                pnlOption.BringToFront();

                DataTable dtSaleReceipt = new DataTable("SaleReceipt");
                DataTable dtSaleReceiptPaymentDetails = new DataTable("SaleReceiptPaymentDetails");

                using (SqlCommand sqlCommand = new SqlCommand())
                using (var conn = new SqlConnection(Helper.BLOGICPOS7_Connection))
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();

                    sqlCommand.Connection = conn;
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.CommandText = $"select * from SaleReceipts where SaleReceiptNumber = '{txtSearch.Text.Trim()}' and TransactionStatusID in ('OPER','VOID','FIN','RET') order by DateCreated desc";
                    var reader = sqlCommand.ExecuteReader();
                    dtSaleReceipt.Load(reader);
                    reader.Close();

                    sqlCommand.CommandText = $"select a.*, b.PaymentMethodName " +
                        $" from SaleReceiptPaymentDetails a " +
                        $" left join PaymentMethods b on a.PaymentMethodID = b.PaymentMethodID " +
                        $" where SaleReceiptID in (select a.SaleReceiptID from SaleReceipts a where a.SaleReceiptNumber = '{txtSearch.Text.Trim()}')";
                    reader = sqlCommand.ExecuteReader();
                    dtSaleReceiptPaymentDetails.Load(reader);
                    reader.Close();
                }

                if (dtSaleReceipt == null || dtSaleReceipt.Rows.Count <= 0)
                {
                    InformationDialogForm errorDialog = new InformationDialogForm("Receipt Not Found");
                    errorDialog.ShowDialog();
                    return;
                }

                if (dtSaleReceipt.Rows.Count > 1)
                {
                    SelectSaleReceiptForm selectSaleReceiptForm = new SelectSaleReceiptForm();
                    selectSaleReceiptForm.BindingReceiptList(dtSaleReceipt, dtSaleReceiptPaymentDetails);

                    Helper.ShowPopUpFormWithDIMBackground(selectSaleReceiptForm);

                    MainSaleReceiptID = selectSaleReceiptForm.SaleReceiptID;
                    TransactionStatusID = selectSaleReceiptForm.TransactionStatusID;
                    MainSaleReceiptNumber = selectSaleReceiptForm.SaleReceiptNumber;
                }
                else
                {
                    MainSaleReceiptID = Utility.GetStringFromRow("SaleReceiptID", dtSaleReceipt.Rows[0]);
                    TransactionStatusID = Utility.GetStringFromRow("TransactionStatusID", dtSaleReceipt.Rows[0]);
                    MainSaleReceiptNumber = Utility.GetStringFromRow("SaleReceiptNumber", dtSaleReceipt.Rows[0]);
                }

                ReloadCurrentReceipt();

                DataTable dtTransactionDetail = new DataTable("TransactionDetail");

                using (SqlCommand sqlCommand = new SqlCommand())
                using (var conn = new SqlConnection(Helper.BLOGICPOS7_Connection))
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();

                    sqlCommand.Connection = conn;
                    sqlCommand.CommandType = CommandType.Text;

                    sqlCommand.CommandText = $"select * from {Helper.Merchant_Name}.dbo.TransactionDetail where ID in (select CreditCardTransactionID from {Helper.BLOGICPOS7_Name}.dbo.SaleReceiptPaymentDetails a where a.SaleReceiptID = '{MainSaleReceiptID}')";
                    var reader = sqlCommand.ExecuteReader();
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

                    paymentDetailView.lblCardValue.Text = Utility.GetStringFromRow("CreditCardTransactionCardNumber", item);
                    paymentDetailView.lblCardType.Text = Utility.GetStringFromRow("CreditCardCardType", item);
                    paymentDetailView.lblApprovalValue.Text = Utility.GetStringFromRow("CreditCardAuthCode", item);

                    paymentDetailView.txtGratuity.Value = Utility.GetDecimalFromRow("Gratuity", item);
                    paymentDetailView.OldGratuity = Utility.GetDecimalFromRow("Gratuity", item);

                    paymentDetailView.lblPaymentValue.Text = Utility.ConvertDecimalToMoneyString(Utility.GetDecimalFromRow("AmountTendered", item) -
                            Utility.GetDecimalFromRow("AmountReturn", item) + Utility.GetDecimalFromRow("Gratuity", item) +
                            Utility.GetDecimalFromRow("SVCFeeAmount", item) - Utility.GetDecimalFromRow("CashDiscountAmount", item) -
                            Utility.GetDecimalFromRow("CoinDiscountAmount", item));

                    int paymentMethodID = Utility.GetIntFromRow("PaymentMethodID", item);
                    if (paymentMethodID == 1 || paymentMethodID >= 3 || paymentMethodID < -3)
                    {
                        paymentDetailView.lblCard.Visible = false;
                        paymentDetailView.lblCardValue.Visible = false;
                        paymentDetailView.lblTotal.Visible = false;
                        paymentDetailView.lblTotalValue.Visible = false;
                        paymentDetailView.lblCardType.Visible = false;
                        paymentDetailView.lblEntryMode.Visible = false;
                        paymentDetailView.lblApproval.Visible = false;
                        paymentDetailView.lblApprovalValue.Visible = false;
                        paymentDetailView.lblBatch.Visible = false;
                        paymentDetailView.lblBatchValue.Visible = false;
                        paymentDetailView.lblTerminal.Visible = false;
                        paymentDetailView.lblTerminalValue.Visible = false;
                    }
                    else
                    {
                        paymentDetailView.lblCard.Visible = true;
                        paymentDetailView.lblCardValue.Visible = true;
                        paymentDetailView.lblTotal.Visible = true;
                        paymentDetailView.lblTotalValue.Visible = true;
                        paymentDetailView.lblCardType.Visible = true;
                        paymentDetailView.lblEntryMode.Visible = true;
                        paymentDetailView.lblApproval.Visible = true;
                        paymentDetailView.lblApprovalValue.Visible = true;
                        paymentDetailView.lblBatch.Visible = true;
                        paymentDetailView.lblBatchValue.Visible = true;
                        paymentDetailView.lblTerminal.Visible = true;
                        paymentDetailView.lblTerminalValue.Visible = true;
                    }
                    paymentDetailView.lblTotalValue.Text = paymentDetailView.lblPaymentValue.Text;

                    DataRow[] drs = dtTransactionDetail.Select($"ID = '{item["CreditCardTransactionID"]}'", "");
                    if (drs.Length > 0)
                    {
                        paymentDetailView.lblEntryMode.Text = Utility.GetStringFromRow("CCEntry", drs[0]);
                        paymentDetailView.lblBatchValue.Text = Utility.GetStringFromRow("BatchID", drs[0]);
                    }
                    paymentDetailView.btnUpdate.Click += BtnUpdateTip_Click;
                    pnlPaymentDetails.Controls.Add(paymentDetailView);
                }
            }
            catch (Exception ex)
            {
                BLogicLogger.Exception("BtnSearch_Click", ex, new System.Collections.Generic.List<object>() { txtSearch.Text });
                ErrorDialogForm errorDialog = new ErrorDialogForm($"Something went wrong while searching for the ticket.{Environment.NewLine}Please try again or contact TruongDS.");
                errorDialog.ShowDialog();
            }
        }

        private void BtnUpdateTip_Click(object sender, EventArgs e)
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

        private void ReloadCurrentReceipt()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(MainSaleReceiptID))
                    return;

                ProcessForm processForm = new ProcessForm();
                processForm.InitGetReceiptTemplateBySaleReceiptID(MainSaleReceiptID);
                processForm.ShowDialog();

                if (!processForm.IsSuccess)
                {
                    InformationDialogForm errorDialog = new InformationDialogForm("Receipt Not Found");
                    errorDialog.ShowDialog();
                    return;
                }

                XtraReport xReport = processForm.ReceiptTemplate;
                if (xReport == null)
                    return;

                BtnBackCloseTicket_Click(null, null);

                xReport.PrintingSystem.SetCommandVisibility(DevExpress.XtraPrinting.PrintingSystemCommand.DocumentMap, DevExpress.XtraPrinting.CommandVisibility.All);
                xReport.PrintingSystem.ShowMarginsWarning = false;
                PrintControl_Receipt.PrintingSystem = xReport.PrintingSystem;
                xReport.CreateDocument();
                PrintControl_Receipt.ExecCommand(DevExpress.XtraPrinting.PrintingSystemCommand.ZoomToPageWidth);

                DataTable dtSaleReceipt = new DataTable("SaleReceipt");

                using (SqlCommand sqlCommand = new SqlCommand())
                using (var conn = new SqlConnection(Helper.BLOGICPOS7_Connection))
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();

                    sqlCommand.Connection = conn;
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.CommandText = $"select *from SaleReceipts where SaleReceiptID = '{MainSaleReceiptID}' order by DateCreated desc";
                    var reader = sqlCommand.ExecuteReader();
                    dtSaleReceipt.Load(reader);
                    reader.Close();
                }

                if (dtSaleReceipt.Rows.Count > 0)
                {
                    MainSaleReceiptID = Utility.GetStringFromRow("SaleReceiptID", dtSaleReceipt.Rows[0]);
                    TransactionStatusID = Utility.GetStringFromRow("TransactionStatusID", dtSaleReceipt.Rows[0]);
                    MainSaleReceiptNumber = Utility.GetStringFromRow("SaleReceiptNumber", dtSaleReceipt.Rows[0]);
                }

            }
            catch (Exception ex)
            {
                BLogicLogger.Exception("ReloadCurrentReceipt in ReceiptView_v2", ex);
            }
        }

        private void ClearCurrentReceipt()
        {
            try
            {
                MainSaleReceiptID = string.Empty;
                MainSaleReceiptNumber = string.Empty;
                TransactionStatusID = string.Empty;

                PrintControl_Receipt.PrintingSystem?.Dispose();
                PrintControl_Receipt.PrintingSystem = new DevExpress.XtraPrinting.PrintingSystem();
                PrintControl_Receipt.Refresh();
            }
            catch (Exception ex)
            {
                BLogicLogger.Exception("ClearCurrentReceipt in ReceiptView_v2", ex);
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
                BLogicLogger.Exception("TxtSearch_KeyUp", ex);
            }
        }

        private void BindingPartialRefundItems()
        {
            try
            {
                DataTable dtSaleReceiptDetails = new DataTable("SaleReceiptDetails");
                string sql = "";
                using (SqlCommand sqlCommand = new SqlCommand())
                using (var conn = new SqlConnection(Helper.BLOGICPOS7_Connection))
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();

                    sqlCommand.Connection = conn;
                    sqlCommand.CommandType = CommandType.Text;

                    sql = $"select * from SaleReceiptDetails where SaleReceiptID = '{MainSaleReceiptID}' and ISNULL(ReturnCode,'') = '' and Status <> '2' order by Range desc";

                    sqlCommand.CommandText = sql;
                    var reader = sqlCommand.ExecuteReader();
                    dtSaleReceiptDetails.Load(reader);
                    reader.Close();
                }

                pnlRefundItemContainer.Controls.Clear();

                foreach (DataRow item in dtSaleReceiptDetails.Rows)
                {
                    var PriceInitial = Utility.GetStringFromRow("PriceInitial", item);
                    if (string.IsNullOrWhiteSpace(PriceInitial) || PriceInitial == "P")
                    {
                        RefundItemRow refundItem = new RefundItemRow();
                        refundItem.SelectedItemChanges += RefundItem_SelectedItemChanges;
                        refundItem.Dock = DockStyle.Top;
                        refundItem.BindingValue(
                            Utility.GetStringFromRow("SaleReceiptDetailID", item),
                            Utility.GetStringFromRow("Description", item),
                            Utility.GetDecimalFromRow("Price", item),
                            Utility.GetDecimalFromRow("Quantity", item),
                            Utility.GetDecimalFromRow("ExtendedPrice", item),
                             Utility.GetDecimalFromRow("TaxAmount", item),
                             Utility.GetStringFromRow("GroupID", item));
                        pnlRefundItemContainer.Controls.Add(refundItem);
                    }
                    else
                    {
                        RefundModiferRow refundModifier = new RefundModiferRow();
                        refundModifier.SelectedItemChanges += RefundItem_SelectedItemChanges;
                        refundModifier.Dock = DockStyle.Top;
                        refundModifier.BindingValue(
                            Utility.GetStringFromRow("SaleReceiptDetailID", item),
                            Utility.GetStringFromRow("Description", item),
                            Utility.GetDecimalFromRow("Price", item),
                            Utility.GetDecimalFromRow("Quantity", item),
                            Utility.GetDecimalFromRow("ExtendedPrice", item),
                             Utility.GetDecimalFromRow("TaxAmount", item),
                             Utility.GetStringFromRow("GroupID", item));
                        pnlRefundItemContainer.Controls.Add(refundModifier);
                    }

                }
            }
            catch (Exception ex)
            {
                BLogicLogger.Exception("BindingPartialRefundItems", ex);
            }
        }

        private void RefundItem_SelectedItemChanges(object sender, DataEventArgs<RequestModel> e)
        {
            try
            {
                decimal totalAmount = 0;

                foreach (Control item in pnlRefundItemContainer.Controls)
                {
                    RefundItemRow refundItem = item as RefundItemRow;
                    RefundModiferRow refundModifier = item as RefundModiferRow;

                    if (refundItem != null)
                    {
                        if (refundItem.GroupID == e.Data.GroupID)
                        {
                            refundItem.IsSelectedItem = e.Data.IsSelectedItem;
                            refundItem.chkCheck.Checked = e.Data.IsSelectedItem;
                            refundItem.BackColor = e.Data.IsSelectedItem ? Color.Red : Color.White;
                        }

                        if (refundItem.IsSelectedItem)
                        {
                            totalAmount += refundItem.ExtendedPrice + refundItem.TaxAmount;
                        }
                        continue;
                    }

                    if (refundModifier != null)
                    {
                        if (refundModifier.GroupID == e.Data.GroupID)
                        {
                            refundModifier.IsSelectedItem = e.Data.IsSelectedItem;
                            refundModifier.BackColor = e.Data.IsSelectedItem ? Color.Red : Color.White;
                        }

                        if (refundModifier.IsSelectedItem)
                        {
                            totalAmount += refundModifier.ExtendedPrice + refundModifier.TaxAmount;
                        }
                        continue;
                    }
                }

                lblTotalRefundItemAmount.Text = $"Total Refund Amount: {Utility.ConvertDecimalToMoneyString(totalAmount)}";
            }
            catch (Exception ex)
            {
                BLogicLogger.Exception("RefundItem_SelectedItemChanges", ex);
            }
        }

    }
}
