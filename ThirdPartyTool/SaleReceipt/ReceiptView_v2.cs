using BLogicSystems.XPos.ClientUtilities;
using BLogicSystems.XPos.Utilites;
using Dapper;
using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ThirdPartyTool.CustomControl;
using ThirdPartyTool.Dialog;

namespace ThirdPartyTool.SaleReceipt
{
    public partial class ReceiptView_v2 : UserControl
    {
        private string MainSaleReceiptID = string.Empty;
        private string MainSaleReceiptNumber = string.Empty;
        private string TransactionStatusID = string.Empty;
        private string MainSaleReceiptPaymentDetailID = string.Empty;

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
            btnBack4.Click += BtnBackCloseTicket_Click;
            btnBack5.Click += BtnBackCloseTicket_Click;

            btnScanCreditCardTransaction.Click += BtnScanCreditCardTransaction_Click;

            btnShowCloseTicket.Click += BtnShow_Click;
            btnShowVoidTicket.Click += BtnShow_Click;
            btnShowPartialRefundTicket.Click += BtnShow_Click;
            btnShowAddTipBySaleToken.Click += BtnShow_Click;
            btnShowAddTipForPaymentDetail.Click += BtnShow_Click;
            btnShowUpdateServiceCharges.Click += BtnShow_Click;
            btnShowFinishTicket.Click += BtnShow_Click;
            btnShowAddSpecialItem.Click += BtnShow_Click;

            btnProcessCloseTicket.Click += BtnProcessCloseTicket_Click;
            btnProcessVoidTicket.Click += BtnProcessVoidTicket_Click;
            btnProcessCancelTicket.Click += BtnProcessCancelTicket_Click;
            btnProcessAddTipBySaleToken.Click += BtnProcessAddTipBySaleToken_Click;

            InitActionButtons();
            UpdateTicketHeader(null);

            pnlOption.Resize += (s, e) => LayoutActionPanel();
            LayoutActionPanel();
        }

        private const int ActionPanelMaxWidth = 640;

        /// <summary>Stretch the action grid with the panel, capped so buttons don't get overly wide.</summary>
        private void LayoutActionPanel()
        {
            int available = pnlOption.ClientSize.Width - tlpOption.Left - 24;
            tlpOption.Width = Math.Max(360, Math.Min(ActionPanelMaxWidth, available));
        }

        #region Ticket header & action availability

        private static readonly Color ActionText = Color.FromArgb(33, 33, 33);
        private static readonly Color ActionBorder = Color.FromArgb(208, 211, 217);
        private static readonly Color ActionHover = Color.FromArgb(243, 246, 252);
        private static readonly Color DangerText = Color.FromArgb(198, 40, 40);
        private static readonly Color DangerBorder = Color.FromArgb(239, 154, 154);
        private static readonly Color DangerHover = Color.FromArgb(255, 244, 244);
        private static readonly Color MutedText = Color.FromArgb(176, 176, 176);
        private static readonly Color MutedBorder = Color.FromArgb(230, 230, 230);

        private class ActionRule
        {
            public string[] Statuses;
            public string Requirement;
            public bool IsDanger;
        }

        // Mirrors the status checks in BtnShow_Click; only used for visual hints.
        private Dictionary<BLabel_Rounded, ActionRule> actionRules;

        private void InitActionButtons()
        {
            actionRules = new Dictionary<BLabel_Rounded, ActionRule>
            {
                { btnShowAddTipForPaymentDetail, new ActionRule { Statuses = new[] { "FIN" }, Requirement = "FINISHED tickets" } },
                { btnShowAddTipBySaleToken, new ActionRule { Statuses = new[] { "FIN" }, Requirement = "FINISHED tickets" } },
                { btnShowUpdateServiceCharges, new ActionRule { Statuses = new[] { "FIN" }, Requirement = "FINISHED tickets with a single payment" } },
                { btnShowPartialRefundTicket, new ActionRule { Statuses = new[] { "FIN" }, Requirement = "FINISHED tickets" } },
                { btnShowAddSpecialItem, new ActionRule { Statuses = new[] { "FIN", "VOID" }, Requirement = "FINISHED or VOIDED tickets" } },
                { btnShowFinishTicket, new ActionRule { Statuses = new[] { "OPER" }, Requirement = "OPEN tickets" } },
                { btnShowCloseTicket, new ActionRule { Statuses = new[] { "OPER", "FIN", "RET" }, Requirement = "OPEN, FINISHED or REFUND tickets" } },
                { btnShowVoidTicket, new ActionRule { Statuses = new[] { "OPER", "FIN" }, Requirement = "OPEN or FINISHED tickets", IsDanger = true } },
            };

            foreach (BLabel_Rounded button in actionRules.Keys)
            {
                button.MouseEnter += ActionButton_MouseEnter;
                button.MouseLeave += ActionButton_MouseLeave;
            }
        }

        private bool IsActionAvailable(BLabel_Rounded button)
        {
            ActionRule rule;
            if (string.IsNullOrWhiteSpace(MainSaleReceiptID) || !actionRules.TryGetValue(button, out rule))
                return false;
            return rule.Statuses.Contains(TransactionStatusID);
        }

        private void ApplyActionStates()
        {
            bool hasTicket = !string.IsNullOrWhiteSpace(MainSaleReceiptID);
            foreach (KeyValuePair<BLabel_Rounded, ActionRule> pair in actionRules)
            {
                BLabel_Rounded button = pair.Key;
                ActionRule rule = pair.Value;
                bool available = IsActionAvailable(button);

                button.BackColor = Color.White;
                button.ForeColor = available ? (rule.IsDanger ? DangerText : ActionText) : MutedText;
                button.BorderColor = available ? (rule.IsDanger ? DangerBorder : ActionBorder) : MutedBorder;

                string tip = "Available for " + rule.Requirement + ".";
                if (!hasTicket)
                    tip = "Search a ticket first. " + tip;
                else if (!available)
                    tip = "Not available for this ticket. " + tip;
                toolTipActions.SetToolTip(button, tip);
            }
        }

        private void ActionButton_MouseEnter(object sender, EventArgs e)
        {
            BLabel_Rounded button = sender as BLabel_Rounded;
            if (button == null || !IsActionAvailable(button))
                return;
            button.BackColor = actionRules[button].IsDanger ? DangerHover : ActionHover;
        }

        private void ActionButton_MouseLeave(object sender, EventArgs e)
        {
            BLabel_Rounded button = sender as BLabel_Rounded;
            if (button != null)
                button.BackColor = Color.White;
        }

        /// <summary>Refreshes the ticket summary on the right panel. Pass the SaleReceipts row when available.</summary>
        private void UpdateTicketHeader(DataRow saleReceipt)
        {
            if (string.IsNullOrWhiteSpace(MainSaleReceiptID))
            {
                lblTicketNumber.Text = "No ticket selected";
                lblTicketStatus.Visible = false;
                lblTicketMeta.Text = "Search a ticket number on the left to see the available actions.";
                ApplyActionStates();
                return;
            }

            lblTicketNumber.Text = "Ticket #" + MainSaleReceiptNumber;

            string statusText;
            Color back, fore;
            switch (TransactionStatusID)
            {
                case "OPER": statusText = "OPEN"; back = Color.FromArgb(227, 242, 253); fore = Color.FromArgb(21, 101, 192); break;
                case "FIN": statusText = "FINISHED"; back = Color.FromArgb(232, 245, 233); fore = Color.FromArgb(46, 125, 50); break;
                case "VOID": statusText = "VOIDED"; back = Color.FromArgb(255, 235, 238); fore = Color.FromArgb(198, 40, 40); break;
                case "RET": statusText = "REFUND"; back = Color.FromArgb(255, 243, 224); fore = Color.FromArgb(230, 81, 0); break;
                default: statusText = string.IsNullOrWhiteSpace(TransactionStatusID) ? "UNKNOWN" : TransactionStatusID; back = Color.FromArgb(238, 238, 238); fore = Color.FromArgb(97, 97, 97); break;
            }
            lblTicketStatus.Text = statusText;
            lblTicketStatus.BackColor = back;
            lblTicketStatus.BorderColor = back;
            lblTicketStatus.ForeColor = fore;
            lblTicketStatus.Width = TextRenderer.MeasureText(statusText, lblTicketStatus.Font).Width + 24;
            lblTicketStatus.Visible = true;

            if (saleReceipt != null)
            {
                List<string> parts = new List<string>();
                if (saleReceipt.Table.Columns.Contains("DateCreated") && saleReceipt["DateCreated"] is DateTime)
                    parts.Add(((DateTime)saleReceipt["DateCreated"]).ToString("M/d/yyyy h:mm tt"));
                string workstation = saleReceipt.Table.Columns.Contains("WorkstationName") ? Utility.GetStringFromRow("WorkstationName", saleReceipt) : string.Empty;
                if (!string.IsNullOrWhiteSpace(workstation))
                    parts.Add(workstation);
                lblTicketMeta.Text = string.Join("  ·  ", parts);
            }

            ApplyActionStates();
        }

        #endregion

        private async void BtnScanCreditCardTransaction_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlCommand sqlCommand = new SqlCommand())
                using (var conn = new SqlConnection(Helper.BLOGICPOS7_Connection))
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();

                    int _svcFeeType = 0;
                    decimal _svcFee = 0;
                    decimal _merchantFee = 0;
                    decimal _cashDiscount = 0;
                    int _dTaxIncludeInSVCFee = 0;
                    string _ApplyCustomFeeWhenPurchaseGiftCard = "1";
                    string _EnableDualPrice = "0";
                    decimal _DualPricePercent = 0;
                    string sqlEmployeePaidID = string.Empty;

                    string sql = "select * from Configurations " +
                        " where ConfigurationName in ('CreditCardConfigValue.SVCFee', 'CreditCardConfigValue.SVCFeeType', 'CreditCardConfigValue.MerchantFee', 'CreditCardConfigValue.CashDiscount', " +
                        " 'CreditCardConfigValue.TaxIncludeInSVCFee','CreditCardConfigValue.ApplyCustomFeeWhenPurchaseGiftCard','CreditCardConfigValue.DualPricePercent','SystemConfigValue.EnableDualPrice')";

                    var configuratios = await conn.QueryAsync<Models.ConfigurationsEntity>(sql, null, null, 180, System.Data.CommandType.Text);

                    foreach (var item in configuratios)
                    {
                        string _name = item.ConfigurationName;
                        string _value = item.ConfigurationValue;

                        switch (_name)
                        {
                            case "CreditCardConfigValue.SVCFee":
                                _svcFee = Utility.GetDecimalFromObject(_value);
                                break;
                            case "CreditCardConfigValue.SVCFeeType":
                                _svcFeeType = Utility.GetIntFromObject(_value);
                                break;
                            case "CreditCardConfigValue.MerchantFee":
                                _merchantFee = Utility.GetDecimalFromObject(_value);
                                break;
                            case "CreditCardConfigValue.CashDiscount":
                                _cashDiscount = Utility.GetDecimalFromObject(_value);
                                break;
                            case "CreditCardConfigValue.TaxIncludeInSVCFee":
                                _dTaxIncludeInSVCFee = Utility.GetIntFromObject(_value);
                                break;
                            case "CreditCardConfigValue.ApplyCustomFeeWhenPurchaseGiftCard":
                                _ApplyCustomFeeWhenPurchaseGiftCard = Utility.GetStringFromObject(_value);
                                break;
                            case "SystemConfigValue.EnableDualPrice":
                                _EnableDualPrice = Utility.GetStringFromObject(_value);
                                break;
                            case "CreditCardConfigValue.DualPricePercent":
                                _DualPricePercent = Utility.GetDecimalFromObject(_value);
                                break;
                        }
                    }

                    sql = $"select * from SaleReceipts where SaleReceiptID = '{MainSaleReceiptID}'";

                    var saleReceiptEntity = await conn.QueryFirstOrDefaultAsync<Models.SaleReceiptEntity>(sql, null, null, 180, System.Data.CommandType.Text);

                    if (saleReceiptEntity != null)
                    {
                        sql = $"select * " +
                            $"from {Helper.Merchant_Name}.dbo.TransactionDetail where InvoiceNo = '0303192703780' and ID not in (select CreditCardTransactionID from SaleReceiptPaymentDetails)";

                        var transactions = await conn.QueryAsync<Models.TransactionDetailEntity>(sql, null, null, 180, System.Data.CommandType.Text);

                        if (transactions?.Count() > 0)
                        {
                            DataTable dtTransactionDetails = await Helper.QueryToDataTableAsync(conn, sql, "TransactionDetails", null, 180);

                            sql = $"select * " +
                                $"from SaleReceiptPaymentDetails where SaleReceiptID = '{saleReceiptEntity.SaleReceiptID}'";

                            DataTable dtSaleReceiptPaymentDetails = await Helper.QueryToDataTableAsync(conn, sql, "SaleReceiptPaymentDetails", null, 180);

                            SearchCreditCardTransactionForm searchCreditCard = new SearchCreditCardTransactionForm();
                            searchCreditCard.BindingData(dtTransactionDetails);
                            if (BLogicMessageBox.ShowPopUpFormWithDIMBackground(searchCreditCard) == DialogResult.OK)
                            {
                                foreach (var item in transactions)
                                {
                                    //sql = Helper.GenerateQueryInsert_BLogicPOS7_SaleReceiptPaymentDetails(dtSaleReceiptPaymentDetails, saleReceiptEntity.SaleReceiptID,
                                    //    2, item.Amount - item.SVCFee, item.Date, item.UserID, saleReceiptEntity.ShiftID, item.Gratuity, item.ID, item.Number, item.CardNumber,
                                    //    item.TranType, 0, item.RefNo, item.AuthCode, item.SVCFee, item.SVCFeeForGratuity, 0, 0, item.MerchantFee,
                                    //    0, 0, 0, 0, 0, 0, item.MerchantFeeForTip,
                                    //    0, 0, 0, 0, 0,
                                    //    0, item.CardType, 0, 0, 0, 0, 0, 0, 0, saleReceiptEntity.SVCFeeTaxAmount > 0, item.MerchantID);

                                    //await Helper.RunQueryUpdateAsync(conn, sql, null);
                                }
                            }
                        }


                    }
                }
            }
            catch (Exception ex)
            {
                BLogicLogger.Exception("BtnScanCreditCardTransaction_Click", ex);
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
                txtSearchTokenReceipt.Text = string.Empty;

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

                TokenSaleReceiptID = processForm.SaleReceiptID;
                TokenSaleReceiptNumber = processForm.SaleReceiptNumber;
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

                ConfirmDialogForm confirmFirmDialog = new ConfirmDialogForm(Const.ConfirmTitle, $"Are you sure you want to CLOSE Ticket#{MainSaleReceiptNumber}?");
                var result = confirmFirmDialog.ShowDialog();
                if (result == DialogResult.No)
                    return;

                ProcessForm processForm = new ProcessForm();
                processForm.SaleReceiptID = MainSaleReceiptID;
                processForm.CurrentProcessName = ProcessNameEnum.CloseReceipt;
                processForm.ShowDialog();

                if (processForm.IsSuccess)
                {
                    InformationDialogForm informationDialogForm = new InformationDialogForm($"Update ticket #{MainSaleReceiptNumber} to CLOSE Done. Refresh now.");
                    informationDialogForm.ShowDialog();

                    MainSaleReceiptID = string.Empty;
                    MainSaleReceiptNumber = string.Empty;
                    UpdateTicketHeader(null);

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
                if (control.Name == btnShowAddSpecialItem.Name)
                {
                    if (TransactionStatusID != "FIN" && TransactionStatusID != "VOID")
                    {
                        InformationDialogForm informationDialogForm = new InformationDialogForm($"Chỉ có thể Add Special Item cho những ticket đang ở trạng thái FINISH or VOID");
                        informationDialogForm.ShowDialog();
                        return;
                    }

                    AddSpecialItemForReceiptForm addSpecialItemForReceiptForm = new AddSpecialItemForReceiptForm();
                    addSpecialItemForReceiptForm.SaleReceiptID = MainSaleReceiptID;
                    if (BLogicMessageBox.ShowPopUpFormWithDIMBackground(addSpecialItemForReceiptForm, Helper.MainForm) == DialogResult.OK)
                    {
                        ReloadCurrentReceipt();
                    }
                }
                else if (control.Name == btnShowFinishTicket.Name)
                {
                    if (TransactionStatusID != "OPER")
                    {
                        InformationDialogForm informationDialogForm = new InformationDialogForm($"Chỉ có thể Finish những ticket đang ở trạng thái OPEN.");
                        informationDialogForm.ShowDialog();
                        return;
                    }

                    FinishTicketWithPaymentDetailForm finishTicketWithPaymentDetailForm = new FinishTicketWithPaymentDetailForm();
                    finishTicketWithPaymentDetailForm.SaleReceiptID = MainSaleReceiptID;
                    BLogicMessageBox.ShowPopUpFormWithDIMBackground(finishTicketWithPaymentDetailForm, Helper.MainForm);
                }
                else if (control.Name == btnShowCloseTicket.Name)
                {

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

                    PartialRefundItemForm partialRefundItemForm = new PartialRefundItemForm();
                    partialRefundItemForm.MainSaleReceiptID = MainSaleReceiptID;
                    if (BLogicMessageBox.ShowPopUpFormWithDIMBackground(partialRefundItemForm, Helper.MainForm) == DialogResult.OK)
                        ReloadCurrentReceipt();
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
                else if (control.Name == btnShowUpdateServiceCharges.Name)
                {
                    if (TransactionStatusID != "FIN")
                    {
                        InformationDialogForm informationDialogForm = new InformationDialogForm($"Chỉ có thể Update Service Charges cho những ticket đang ở trạng thái FINISH.");
                        informationDialogForm.ShowDialog();
                        return;
                    }

                    DataTable dtSaleReceiptPaymentDetails = new DataTable("SaleReceiptPaymentDetails");
                    using (SqlCommand sqlCommand = new SqlCommand())
                    using (var conn = new SqlConnection(Helper.BLOGICPOS7_Connection))
                    {
                        if (conn.State == ConnectionState.Closed)
                            conn.Open();

                        sqlCommand.Connection = conn;
                        sqlCommand.CommandType = CommandType.Text;
                        sqlCommand.CommandText = $"select * from SaleReceiptPaymentDetails where SaleReceiptID = '{MainSaleReceiptID}' ";
                        var reader = sqlCommand.ExecuteReader();
                        dtSaleReceiptPaymentDetails.Load(reader);
                        reader.Close();
                    }

                    if (dtSaleReceiptPaymentDetails?.Rows.Count != 1)
                    {
                        InformationDialogForm informationDialogForm = new InformationDialogForm($"Chỉ có thể Update Service Charges cho những ticket thanh toán 1 lần duy nhất. Ko support multi payment.");
                        informationDialogForm.ShowDialog();
                        return;
                    }

                    SelectServiceChargeToAdjstForm selectServiceChargeToAdjstForm = new SelectServiceChargeToAdjstForm();
                    selectServiceChargeToAdjstForm.SaleReceiptID = MainSaleReceiptID;
                    BLogicMessageBox.ShowPopUpFormWithDIMBackground(selectServiceChargeToAdjstForm, Helper.MainForm);
                    if (selectServiceChargeToAdjstForm.IsChangedServiceCharges)
                    {
                        ReloadCurrentReceipt();
                    }
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
                processForm.CurrentProcessName = ProcessNameEnum.GetReceiptTemplateBySaleReceiptID;
                processForm.SaleReceiptID = MainSaleReceiptID;
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
                    UpdateTicketHeader(dtSaleReceipt.Rows[0]);
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
                UpdateTicketHeader(null);
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


        private void bLabel_Rounded1_Click(object sender, EventArgs e)
        {

        }

        //        private async void SavePaymentDetail()
        //        {
        //            try
        //            {
        //                using (SqlCommand sqlCommand = new SqlCommand())
        //                using (var conn = new SqlConnection(Helper.BLOGICPOS7_Connection))
        //                {
        //                    if (conn.State == ConnectionState.Closed)
        //                        conn.Open();

        //                    int _svcFeeType = 0;
        //                    decimal _svcFee = 0;
        //                    decimal _merchantFee = 0;
        //                    decimal _cashDiscount = 0;
        //                    int _dTaxIncludeInSVCFee = 0;
        //                    string _ApplyCustomFeeWhenPurchaseGiftCard = "1";
        //                    string _EnableDualPrice = "0";
        //                    decimal _DualPricePercent = 0;
        //                    string sqlEmployeePaidID = string.Empty;

        //                    string sql = "select * from Configurations " +
        //                        " where ConfigurationName in ('CreditCardConfigValue.SVCFee', 'CreditCardConfigValue.SVCFeeType', 'CreditCardConfigValue.MerchantFee', 'CreditCardConfigValue.CashDiscount', " +
        //                        " 'CreditCardConfigValue.TaxIncludeInSVCFee','CreditCardConfigValue.ApplyCustomFeeWhenPurchaseGiftCard','CreditCardConfigValue.DualPricePercent','SystemConfigValue.EnableDualPrice')";

        //                    var configuratios = await conn.QueryAsync<Models.ConfigurationEntity>(sql, null, null, 180, System.Data.CommandType.Text);

        //                    foreach (var item in configuratios)
        //                    {
        //                        string _name = item.ConfigurationName;
        //                        string _value = item.ConfigurationValue;

        //                        switch (_name)
        //                        {
        //                            case "CreditCardConfigValue.SVCFee":
        //                                _svcFee = Utility.GetDecimalFromObject(_value);
        //                                break;
        //                            case "CreditCardConfigValue.SVCFeeType":
        //                                _svcFeeType = Utility.GetIntFromObject(_value);
        //                                break;
        //                            case "CreditCardConfigValue.MerchantFee":
        //                                _merchantFee = Utility.GetDecimalFromObject(_value);
        //                                break;
        //                            case "CreditCardConfigValue.CashDiscount":
        //                                _cashDiscount = Utility.GetDecimalFromObject(_value);
        //                                break;
        //                            case "CreditCardConfigValue.TaxIncludeInSVCFee":
        //                                _dTaxIncludeInSVCFee = Utility.GetIntFromObject(_value);
        //                                break;
        //                            case "CreditCardConfigValue.ApplyCustomFeeWhenPurchaseGiftCard":
        //                                _ApplyCustomFeeWhenPurchaseGiftCard = Utility.GetStringFromObject(_value);
        //                                break;
        //                            case "SystemConfigValue.EnableDualPrice":
        //                                _EnableDualPrice = Utility.GetStringFromObject(_value);
        //                                break;
        //                            case "CreditCardConfigValue.DualPricePercent":
        //                                _DualPricePercent = Utility.GetDecimalFromObject(_value);
        //                                break;
        //                        }
        //                    }

        //                    sql = $"select * from SaleReceipts where SaleReceiptID = '{MainSaleReceiptID}'";

        //                    var saleReceiptEntity = await conn.QueryFirstOrDefaultAsync<Models.SaleReceiptEntity>(sql, null, null, 180, System.Data.CommandType.Text);

        //                    if (saleReceiptEntity != null)
        //                    {
        //                        sql = $"select * " +
        //                            $"from {Helper.Merchant_Name}.dbo.TransactionDetail where InvoiceNo = '{saleReceiptEntity.InvoiceNum}'";

        //                        var transactions = await conn.QueryAsync<Models.TransactionDetailEntity>(sql, null, null, 180, System.Data.CommandType.Text);

        //                        DataTable dtTransactionDetails = await Helper.QueryToDataTableAsync(conn, sql, "TransactionDetails", null, 180);

        //                        sql = $"select * " +
        //                            $"from SaleReceiptPaymentDetails where SaleReceiptID = '{saleReceiptEntity.SaleReceiptID}'";

        //                        DataTable dtSaleReceiptPaymentDetails = await Helper.QueryToDataTableAsync(conn, sql, "SaleReceiptPaymentDetails", null, 180);

        //                        SearchCreditCardTransactionForm searchCreditCard = new SearchCreditCardTransactionForm();
        //                        searchCreditCard.BindingData(dtTransactionDetails);
        //                        if (BLogicMessageBox.ShowPopUpFormWithDIMBackground(searchCreditCard) == DialogResult.OK)
        //                        {
        //                            foreach (var item in transactions)
        //                            {
        //                                var paidServiceFee = item.ServiceCharge + (item.ServiceCharge / saleReceiptEntity.SalesOrderFee * saleReceiptEntity.ServiceChargeTaxAmount);
        //                                var svcFeeForServiceCharge = Helper.CalculateSVCFee(item.ServiceCharge, saleReceiptEntity.SVCFeeTaxAmount > 0, saleReceiptEntity.SVCFeeTaxRate, saleReceiptEntity.SVCFeeTaxAmount, _dTaxIncludeInSVCFee, _svcFee, _svcFeeType);
        //                                var PartyServiceFeeAmount = item.PartyServiceFeeAmount + (item.PartyServiceFeeAmount / saleReceiptEntity.PartyServiceFeeAmount * saleReceiptEntity.PartyServiceFeeTaxAmount);
        //                                var SVCFeeForPartyServiceFeeAmount = Helper.CalculateSVCFee(item.PartyServiceFeeAmount, saleReceiptEntity.SVCFeeTaxAmount > 0, saleReceiptEntity.SVCFeeTaxRate, saleReceiptEntity.SVCFeeTaxAmount, _dTaxIncludeInSVCFee, _svcFee, _svcFeeType);
        //                                var AreaFeeAmount = item.AreaFeeAmount + (item.AreaFeeAmount / saleReceiptEntity.AreaFeeAmount * saleReceiptEntity.AreaFeeTaxAmount);
        //                                var SVCFeeForAreaFeeAmount = Helper.CalculateSVCFee(item.AreaFeeAmount, saleReceiptEntity.SVCFeeTaxAmount > 0, saleReceiptEntity.SVCFeeTaxRate, saleReceiptEntity.SVCFeeTaxAmount, _dTaxIncludeInSVCFee, _svcFee, _svcFeeType);
        //                                var ServiceFee = item.ServiceFee + (item.ServiceFee / saleReceiptEntity.ServiceFee * saleReceiptEntity.ServiceChargeTaxAmount);
        //                                var SVCFeeForServiceFee = Helper.CalculateSVCFee(item.ServiceFee, saleReceiptEntity.SVCFeeTaxAmount > 0, saleReceiptEntity.SVCFeeTaxRate, saleReceiptEntity.SVCFeeTaxAmount, _dTaxIncludeInSVCFee, _svcFee, _svcFeeType);
        //                                var AutoGratuityAmount = item.AutoGratuityAmount + (item.AutoGratuityAmount / saleReceiptEntity.AutoGratuityAmount * saleReceiptEntity.AutoGratuityTaxAmount);
        //                                var SVCFeeForAutoGratuityAmount = Helper.CalculateSVCFee(item.AutoGratuityAmount, saleReceiptEntity.SVCFeeTaxAmount > 0, saleReceiptEntity.SVCFeeTaxRate, saleReceiptEntity.SVCFeeTaxAmount, _dTaxIncludeInSVCFee, _svcFee, _svcFeeType);
        //                                var SVCFeeTaxAmount = Helper.CalculateSVCFee(item.SVCFee, saleReceiptEntity.SVCFeeTaxAmount > 0, saleReceiptEntity.SVCFeeTaxRate, saleReceiptEntity.SVCFeeTaxAmount, _dTaxIncludeInSVCFee, _svcFee, _svcFeeType);

        //                                var ServiceFeeTaxAmount = (item.ServiceFee / saleReceiptEntity.ServiceFee * saleReceiptEntity.PartyServiceFeeTaxAmount);
        //                                var AreaFeeTaxAmount = (item.AreaFeeAmount / saleReceiptEntity.AreaFeeAmount * saleReceiptEntity.PartyServiceFeeTaxAmount);
        //                                var PartyServiceFeeTaxAmount = (item.PartyServiceFeeAmount / saleReceiptEntity.PartyServiceFeeAmount * saleReceiptEntity.PartyServiceFeeTaxAmount);
        //                                var AutoGratuityTaxAmount = (item.AutoGratuityAmount / saleReceiptEntity.AutoGratuityAmount * saleReceiptEntity.PartyServiceFeeTaxAmount);

        //                                var MerchantFeeForServiceFee = Helper.CalculateMerchantFee(ServiceFee + SVCFeeForServiceFee, _merchantFee);
        //                                var MerchantFeeForAreaFeeAmount = Helper.CalculateMerchantFee(AreaFeeAmount + SVCFeeForAreaFeeAmount, _merchantFee);
        //                                var MerchantFeeForPartyServiceFeeAmount = Helper.CalculateMerchantFee(PartyServiceFeeAmount + SVCFeeForPartyServiceFeeAmount, _merchantFee);
        //                                var MerchantFeeForAutoGratuityAmount = Helper.CalculateMerchantFee(AutoGratuityAmount + SVCFeeForAutoGratuityAmount, _merchantFee);

        //                                sql = Helper.GenerateQueryInsert_BLogicPOS7_SaleReceiptPaymentDetails(dtSaleReceiptPaymentDetails, saleReceiptEntity.SaleReceiptID,
        //                                    2, item.Amount - item.SVCFee, item.Date, item.UserID, saleReceiptEntity.ShiftID, item.Gratuity, item.ID, item.Number, item.CardNumber,
        //                                    item.TranType, paidServiceFee, item.RefNo, item.AuthCode, item.SVCFee, item.SVCFeeForGratuity, svcFeeForServiceCharge, 0, item.MerchantFee,
        //                                    PartyServiceFeeAmount, SVCFeeForPartyServiceFeeAmount, AreaFeeAmount, SVCFeeForAreaFeeAmount, ServiceFee, SVCFeeForServiceFee, item.MerchantFeeForTip,
        //                                    MerchantFeeForServiceFee, MerchantFeeForAreaFeeAmount, MerchantFeeForPartyServiceFeeAmount, AutoGratuityAmount, SVCFeeForAutoGratuityAmount,
        //                                    MerchantFeeForAutoGratuityAmount, item.CardType, SVCFeeTaxAmount, 0, ServiceFeeTaxAmount, AreaFeeTaxAmount, PartyServiceFeeTaxAmount, AutoGratuityTaxAmount, SVCFeeTaxAmount, SVCFeeTaxAmount > 0, item.MerchantID);

        //                            }
        //                        }

        //                    }

        //                    decimal dTotalAmountTender = 0;
        //                    decimal dTotalPaidServiceFee = 0;
        //                    decimal dTotalServiceFee = 0;
        //                    decimal dPartyServiceFeeAmount = 0;
        //                    decimal dAreaFeeAmount = 0;
        //                    decimal dServiceFee = 0;
        //                    decimal dOpenTabAmount = 0;
        //                    decimal dAutoGratuityAmount = 0;

        //                    decimal dServiceFeeTaxAmount = 0;
        //                    decimal dAreaFeeTaxAmount = 0;
        //                    decimal dPartyServiceFeeTaxAmount = 0;
        //                    decimal dAutoGratuityTaxAmount = 0;

        //                    decimal tempPaidAmount = 0;
        //                    bool isPreAuth = false;
        //                    //Create salePaymentDetail and add to receipt
        //                    SaleReceiptPaymentDetailEntity oPaymentDetail = new SaleReceiptPaymentDetailEntity();
        //                    oPaymentDetail.PaidServiceFee = 0;
        //                    oPaymentDetail.AreaFeeAmount = 0;
        //                    oPaymentDetail.PartyServiceFeeAmount = 0;
        //                    oPaymentDetail.ServiceFee = 0;

        //                    if (tbPaymentTable.Columns.Contains("CreditCardRefNo"))
        //                    {
        //                        oPaymentDetail.CreditCardRefNo = rPaymentRow["CreditCardRefNo"]?.ToString();
        //                    }
        //                    tempPaidAmount = dTotalAmountTender;
        //                    dTotalAmountTender += oPaymentDetail.AmountTendered - oPaymentDetail.AmountReturn;

        //                    //add by tuanph.11.18.2019
        //                    if (oPaymentDetail.CreditCardTransactionType == "PreAuth")
        //                    {
        //                        dOpenTabAmount = oPaymentDetail.AmountTendered - oPaymentDetail.AmountReturn;
        //                        isPreAuth = true;
        //                    }

        //                    decimal dTempAmountTotal = oReceipt.TotalAmount;
        //                    if (IsPayBySplitEvenly && !IsFinishSale(oReceipt)) // Pay by split evenly
        //                    {
        //                        dTempAmountTotal = dBaseAmount + (decimal)oReceipt.SalesOrderFee + oReceipt.ServiceChargeTaxAmount;
        //                    }

        //                    if (dTempAmountTotal > dTotalAmountTender)
        //                        dTempAmountTotal = dTotalAmountTender;

        //                    if (dTotalAmountExcludeServiceFee <= dTotalAmountTender && !isPreAuth)
        //                    {
        //                        decimal dCurrentServiceFee = 0;

        //                        oPaymentDetail.PaidServiceFee = dTempAmountTotal - dTotalAmountExcludeServiceFee - dTotalPaidServiceFee;// + oReceipt.ServiceChargeTaxAmount;
        //                                                                                                                                //if (IsFinishSale(oReceipt) && sCountRows == tbPaymentTable.Rows.Count)
        //                                                                                                                                //    oPaymentDetail.PaidServiceFee = dTempAmountTotal - dTotalAmountExcludeServiceFee - dTotalPaidServiceFee - taxitem;
        //                                                                                                                                //else
        //                                                                                                                                //{
        //                                                                                                                                //    oPaymentDetail.PaidServiceFee = dTempAmountTotal - dTotalAmountExcludeServiceFee - dTotalPaidServiceFee;
        //                                                                                                                                //    if (oPaymentDetail.PaidServiceFee > oReceipt.SalesOrderFee + oReceipt.ServiceChargeTaxAmount)
        //                                                                                                                                //        oPaymentDetail.PaidServiceFee = oReceipt.SalesOrderFee + oReceipt.ServiceChargeTaxAmount;
        //                                                                                                                                //}

        //                        dTotalPaidServiceFee += oPaymentDetail.PaidServiceFee;

        //                        //add by tuanph.09.23.2019
        //                        dCurrentServiceFee = oPaymentDetail.PaidServiceFee;

        //                        if (oReceipt.PartyServiceFeeAmount > 0 && oReceipt.PartyServiceFeeAmount + oReceipt.PartyServiceFeeTaxAmount > dPartyServiceFeeAmount && dCurrentServiceFee > 0)
        //                        {
        //                            decimal tempPartyFeeAmountNeeded = RoundingUtility.Rounding.RoundUp(oReceipt.PartyServiceFeeAmount - dPartyServiceFeeAmount);
        //                            decimal tempPartyFeeAmount = dCurrentServiceFee > tempPartyFeeAmountNeeded ? tempPartyFeeAmountNeeded : dCurrentServiceFee;

        //                            oPaymentDetail.PartyServiceFeeAmount = tempPartyFeeAmount;
        //                            dPartyServiceFeeAmount += tempPartyFeeAmount;
        //                            dCurrentServiceFee -= tempPartyFeeAmount;

        //                            if (!IsPartyServiceFeeAmount && dCurrentServiceFee > 0)
        //                            {
        //                                tempTaxServiceCharge = dCurrentServiceFee > oReceipt.PartyServiceFeeTaxAmount ? oReceipt.PartyServiceFeeTaxAmount : dCurrentServiceFee;
        //                                oPaymentDetail.PartyServiceFeeAmount += tempTaxServiceCharge;// oReceipt.PartyServiceFeeTaxAmount;
        //                                dPartyServiceFeeAmount += tempTaxServiceCharge;
        //                                dCurrentServiceFee -= tempTaxServiceCharge;

        //                                if (oPaymentDetail.PartyServiceFeeAmount >= oReceipt.PartyServiceFeeAmount + oReceipt.PartyServiceFeeTaxAmount)
        //                                    IsPartyServiceFeeAmount = true;
        //                            }

        //                            //oPaymentDetail.PartyServiceFeeTaxAmount = CalculateTaxServiceCharge(oPaymentDetail.PartyServiceFeeAmount, ratioSC, oReceipt.PartyServiceFeeTaxAmount);
        //                            oPaymentDetail.PartyServiceFeeTaxAmount = CalculateTaxServiceCharge(oPaymentDetail.PartyServiceFeeAmount, sServiceChargeTaxRate, oReceipt.PartyServiceFeeTaxAmount, dsSaleReceipt);
        //                            dPartyServiceFeeTaxAmount += oPaymentDetail.PartyServiceFeeTaxAmount;
        //                        }

        //                        if (oReceipt.AreaFeeAmount > 0 && oReceipt.AreaFeeAmount + oReceipt.AreaFeeTaxAmount > dAreaFeeAmount && dCurrentServiceFee > 0)
        //                        {
        //                            decimal tempAreaFeeAmountNeeded = RoundingUtility.Rounding.RoundUp(oReceipt.AreaFeeAmount - dAreaFeeAmount);
        //                            decimal tempAreaFeeAmount = dCurrentServiceFee > tempAreaFeeAmountNeeded ? tempAreaFeeAmountNeeded : dCurrentServiceFee;

        //                            oPaymentDetail.AreaFeeAmount = tempAreaFeeAmount;
        //                            dAreaFeeAmount += tempAreaFeeAmount;
        //                            dCurrentServiceFee -= tempAreaFeeAmount;

        //                            if (!IsAreaFeeAmount && dCurrentServiceFee > 0)
        //                            {
        //                                tempTaxServiceCharge = dCurrentServiceFee > oReceipt.AreaFeeTaxAmount ? oReceipt.AreaFeeTaxAmount : dCurrentServiceFee;
        //                                oPaymentDetail.AreaFeeAmount += tempTaxServiceCharge;// oReceipt.AreaFeeTaxAmount;
        //                                dAreaFeeAmount += tempTaxServiceCharge;
        //                                dCurrentServiceFee -= tempTaxServiceCharge;

        //                                if (oPaymentDetail.AreaFeeAmount >= oReceipt.AreaFeeAmount + oReceipt.AreaFeeTaxAmount)
        //                                    IsAreaFeeAmount = true;
        //                            }

        //                            //oPaymentDetail.AreaFeeTaxAmount = CalculateTaxServiceCharge(oPaymentDetail.AreaFeeAmount, ratioSC, oReceipt.AreaFeeTaxAmount);
        //                            oPaymentDetail.AreaFeeTaxAmount = CalculateTaxServiceCharge(oPaymentDetail.AreaFeeAmount, sServiceChargeTaxRate, oReceipt.AreaFeeTaxAmount, dsSaleReceipt);
        //                            dAreaFeeTaxAmount += oPaymentDetail.AreaFeeTaxAmount;
        //                        }

        //                        if (dServiceFeeNew > 0 && dServiceFeeNew + oReceipt.ServiceFeeTaxAmount + oReceipt.ExtraFeeTaxAmount > dServiceFee && dCurrentServiceFee > 0)
        //                        {
        //                            decimal tempServiceFeeAmountNeeded = RoundingUtility.Rounding.RoundUp((decimal)dServiceFeeNew - dServiceFee);
        //                            decimal tempServiceFeeAmount = dCurrentServiceFee > tempServiceFeeAmountNeeded ? tempServiceFeeAmountNeeded : dCurrentServiceFee;

        //                            oPaymentDetail.ServiceFee = tempServiceFeeAmount;
        //                            dServiceFee += tempServiceFeeAmount;
        //                            dCurrentServiceFee -= tempServiceFeeAmount;

        //                            if (!IsServiceFee && dCurrentServiceFee > 0)
        //                            {
        //                                tempTaxServiceCharge = dCurrentServiceFee > oReceipt.ServiceFeeTaxAmount + oReceipt.ExtraFeeTaxAmount ? oReceipt.ServiceFeeTaxAmount + oReceipt.ExtraFeeTaxAmount : dCurrentServiceFee;
        //                                oPaymentDetail.ServiceFee += tempTaxServiceCharge; // oReceipt.ServiceFeeTaxAmount + oReceipt.ExtraFeeTaxAmount;
        //                                dServiceFee += tempTaxServiceCharge;
        //                                dCurrentServiceFee -= tempTaxServiceCharge;

        //                                if (oPaymentDetail.ServiceFee >= dServiceFeeNew + oReceipt.ServiceFeeTaxAmount + oReceipt.ExtraFeeTaxAmount)
        //                                    IsServiceFee = true;
        //                            }

        //                            //oPaymentDetail.ServiceFeeTaxAmount = CalculateTaxServiceCharge(oPaymentDetail.ServiceFee, ratioSC, oReceipt.ServiceFeeTaxAmount + oReceipt.ExtraFeeTaxAmount);
        //                            oPaymentDetail.ServiceFeeTaxAmount = CalculateTaxServiceCharge(oPaymentDetail.ServiceFee, sServiceChargeTaxRate, oReceipt.ServiceFeeTaxAmount + oReceipt.ExtraFeeTaxAmount, dsSaleReceipt);
        //                            dServiceFeeTaxAmount += oPaymentDetail.ServiceFeeTaxAmount;
        //                        }

        //                        if (oReceipt.AutoGratuityAmount > 0 && oReceipt.AutoGratuityAmount + oReceipt.AutoGratuityTaxAmount > dAutoGratuityAmount && dCurrentServiceFee > 0)
        //                        {
        //                            decimal tempAutoGratuityNeeded = RoundingUtility.Rounding.RoundUp(oReceipt.AutoGratuityAmount - dAutoGratuityAmount);
        //                            decimal tempAutoGratuityAmount = dCurrentServiceFee > tempAutoGratuityNeeded ? tempAutoGratuityNeeded : dCurrentServiceFee;

        //                            oPaymentDetail.AutoGratuityAmount = tempAutoGratuityAmount;

        //                            dAutoGratuityAmount += tempAutoGratuityAmount;
        //                            dCurrentServiceFee -= tempAutoGratuityAmount;

        //                            if (!IsAutoGratuityAmount && dCurrentServiceFee > 0)
        //                            {
        //                                tempTaxServiceCharge = dCurrentServiceFee > oReceipt.AutoGratuityTaxAmount ? oReceipt.AutoGratuityTaxAmount : dCurrentServiceFee;
        //                                oPaymentDetail.AutoGratuityAmount += tempTaxServiceCharge;// oReceipt.AutoGratuityTaxAmount;
        //                                dAutoGratuityAmount += tempTaxServiceCharge;
        //                                dCurrentServiceFee -= tempTaxServiceCharge;

        //                                if (oPaymentDetail.AutoGratuityAmount >= oReceipt.AutoGratuityAmount + oReceipt.AutoGratuityTaxAmount)
        //                                    IsAutoGratuityAmount = true;
        //                            }

        //                            //oPaymentDetail.AutoGratuityTaxAmount = CalculateTaxServiceCharge(oPaymentDetail.AutoGratuityAmount, ratioSC, oReceipt.AutoGratuityTaxAmount);
        //                            oPaymentDetail.AutoGratuityTaxAmount = CalculateTaxServiceCharge(oPaymentDetail.AutoGratuityAmount, sServiceChargeTaxRate, oReceipt.AutoGratuityTaxAmount, dsSaleReceipt);
        //                            dAutoGratuityTaxAmount += oPaymentDetail.AutoGratuityTaxAmount;
        //                        }

        //#endregion
        //                    }

        //                    if (oPaymentDetail.CreatedDate == null || oPaymentDetail.CreatedDate == DateTime.MinValue)
        //                        oPaymentDetail.CreatedDate = DateTime.Now;

        //                    //Trung Add
        //                    vLastTransDate = oPaymentDetail.CreatedDate;

        //                    oReceipt.SaleReceiptPaymentList.Add(oPaymentDetail);

        //                    if (IsFinishSale(oReceipt) && sCountRows == tbPaymentTable.Rows.Count) // Finish sale
        //                    {
        //                        UpdateTaxServiceChargeinPayment(dAutoGratuityTaxAmount, dAreaFeeTaxAmount, dPartyServiceFeeTaxAmount, dServiceFeeTaxAmount, oReceipt);
        //                    }

        //                    //get giftCardPayment 
        //                    //if (oPaymentDetail.PaymentMethod.PaymentMethodid == (int)PaymentTypes.GIFTCARD
        //                    //    && rPaymentRow["GiftCardCode"] != null && rPaymentRow["GiftCardCode"].ToString() != "")
        //                    if (oPaymentDetail.PaymentMethod.PaymentMethodid == (int)PaymentTypes.GIFTCARD)
        //                    {
        //                        oPaymentDetail.GiftCardCode = Utility.GetStringFromRow("GiftCardCode", rPaymentRow);
        //                        srpdeGiftCardPayment = oPaymentDetail;
        //                    }
        //                    if (oReceipt.TransactionStatus.TransactionStatusID == TransactionStatus.RETURN)
        //                        UpdateValuePaymentDetailsForRETURNSaleReceipt(oPaymentDetail);
        //                    //dTotalAmountTender += oPaymentDetail.AmountTendered;

        //                    if (dTotalAmountTender >= oReceipt.TotalAmount)
        //                        oPaymentDetail.PaidPayout = GetPaidPayout(dsSaleReceipt);

        //                    int cashAdjust = 1;
        //                    if (_EnableDualPrice == "1" && _DualPricePercent > 0)
        //                    {
        //                        sql = $"select IsDualPrice from PaymentMethods where PaymentMethodID = '{oPaymentDetail.PaymentMethod.PaymentMethodid}'";
        //                        var tempResult = DBUtility.CreateQuerySelectUnique(session, sql);
        //                        cashAdjust = Utility.GetBoolFromObject(tempResult) ? 2 : 1;
        //                    }
        //                    else
        //                    {
        //                        sql = $"select CashAdjust from PaymentMethods where PaymentMethodID = '{oPaymentDetail.PaymentMethod.PaymentMethodid}'";
        //                        var tempResult = DBUtility.CreateQuerySelectUnique(session, sql);
        //                        cashAdjust = Utility.GetIntFromObject(tempResult);
        //                    }

        //                    //Calculate SVC Fee for Gratuity, Service Charge
        //                    CalculateCustomFeeForServiceCharge(oPaymentDetail, oReceipt, isApplyTaxCustomFee, cashAdjust, _svcFee, _cashDiscount, _merchantFee, _svcFeeType, _ApplyCustomFeeWhenPurchaseGiftCard, _dTaxIncludeInSVCFee);

        //                    // tuanph.11.18.2019 delete PreAuth when captured
        //                    if (oPaymentDetail.CreditCardTransactionType == "PreAuthCaptured")
        //                    {
        //                        //RemoveCreditCardSaleReceiptPaymentDetails(session, oPaymentDetail.CreditCardAuthCode);
        //                        oPaymentDetail.CreatedDate = DateTime.Now;
        //                        BLogicServerLogger.Error($"# ProccessPayment => SavePaymentDetail => DeletePreAuthTransaction");
        //                        DeletePreAuthTransaction(session, oPaymentDetail.CreditCardAuthCode, oReceipt, oPaymentDetail.CreditCardRefNo);
        //                    }
        //                    BLogicServerLogger.Error($"# ProccessPayment => SavePaymentDetail => session.Save(oPaymentDetail)");
        //                    //Trung Add 2024.06.03 Update Balance (House Account)
        //                    if (!string.IsNullOrEmpty(oPaymentDetail.HouseAccountNumber) && oPaymentDetail.PaymentMethod.PaymentMethodid == -5 && oReceipt.SaleTypeID != Utility.HouseAccountSaleType)
        //                    {
        //                        string sStoreId = oPaymentDetail.CreditCardAuthCode;
        //                        string sStoreIDProcess = oPaymentDetail.CreditCardAuthCode;
        //                        string sStoreNameProcess = oReceipt.Store?.StoreName;
        //                        string sTicketNumber = oReceipt.SaleReceiptNumber;
        //                        string sEmployeeID = oReceipt.EmployeePaid.Employeeid.ToString();
        //                        string employeeNameOnReceipt = HouseAccount.GetPrintEmployeeNameOnReceipt(session);
        //                        string sEmployeeName = HouseAccount.FormatEmployeeNameByPrintConfig(oReceipt.EmployeePaid?.EmployeeFirstName, oReceipt.EmployeePaid?.EmployeeLastName, employeeNameOnReceipt);
        //                        if (sEmployeeName != null)
        //                            sEmployeeName = sEmployeeName.Trim();

        //                        //StoreName null
        //                        if (string.IsNullOrWhiteSpace(sStoreNameProcess) && !string.IsNullOrWhiteSpace(sStoreIDProcess))
        //                        {
        //                            IList storeList = DBUtility.CreateQueryList(session, string.Format("From StoreEntity where StoreID = '{0}'", sStoreIDProcess));
        //                            if (storeList.Count > 0)
        //                            {
        //                                StoreEntity storeEntity = storeList[0] as StoreEntity;
        //                                sStoreNameProcess = storeEntity.StoreName;
        //                            }
        //                        }

        //                        if (string.IsNullOrWhiteSpace(sEmployeeName))
        //                        {
        //                            IList employeesList = DBUtility.CreateQueryList(session, string.Format("From EmployeeEntity where EmployeeID = '{0}'", sEmployeeID));
        //                            if (employeesList.Count > 0)
        //                            {
        //                                EmployeeEntity employeeEntity = employeesList[0] as EmployeeEntity;
        //                                sEmployeeName = HouseAccount.FormatEmployeeNameByPrintConfig(employeeEntity.EmployeeFirstName, employeeEntity.EmployeeLastName, employeeNameOnReceipt);
        //                            }
        //                        }

        //                        string sWorkstationID = oReceipt.WorkstationID;
        //                        string sWorkstationName = oReceipt.WorkstationName;
        //                        if (oPaymentDetail.Shift != null)
        //                        {
        //                            sWorkstationID = oPaymentDetail.Shift.Workstationid.Workstationid.ToString();
        //                            sWorkstationName = oPaymentDetail.Shift.Workstationid.WorkstationName.ToString();
        //                        }
        //                        string sTransactionType = SharedModule.Models.HouseAccount.HouseAccountUtility.GetTransactionTypeByID(SharedModule.Models.HouseAccount.HouseAccountTransactionType.REDEEM);//  "Redeem";
        //                        string sEntryMode = "";
        //                        string sAppName = "POS";
        //                        string sTenderType = "";
        //                        if (oPaymentDetail.PaymentMethod.PaymentMethodName != null)
        //                            sTenderType = oPaymentDetail.PaymentMethod.PaymentMethodName;
        //                        else
        //                        {
        //                            IList paymentMethod = DBUtility.CreateQueryList(session, string.Format("From PaymentMethodEntity where PaymentMethodid = {0}", oPaymentDetail.PaymentMethod.PaymentMethodid));
        //                            if (paymentMethod.Count > 0)
        //                            {
        //                                PaymentMethodEntity paymentMethodEntity = paymentMethod[0] as PaymentMethodEntity;
        //                                sTenderType = paymentMethodEntity.PaymentMethodName;
        //                            }
        //                        }

        //                        bool bQOrder = false;
        //                        if (dsSaleReceipt.Tables.Contains("IsQOrder"))
        //                        {
        //                            bQOrder = true;
        //                            sAppName = "QOrder";
        //                        }

        //                        if (dsSaleReceipt.Tables.Contains("IsSOrder"))
        //                        {
        //                            bQOrder = true;
        //                            sAppName = "SOrder";
        //                        }

        //                        if (!bQOrder && !string.IsNullOrWhiteSpace(sWorkstationID))
        //                        {
        //                            IList workstationList = DBUtility.CreateQueryList(session, string.Format("From WorkstationEntity where WorkstationID = '{0}'", sWorkstationID));
        //                            if (workstationList.Count > 0)
        //                            {
        //                                WorkstationEntity workstationEntity = workstationList[0] as WorkstationEntity;
        //                                if (string.IsNullOrWhiteSpace(sWorkstationName))
        //                                    sWorkstationName = workstationEntity.WorkstationName;

        //                                if (workstationEntity.WorkstationType == 4)
        //                                {
        //                                    bQOrder = true;
        //                                    sAppName = "QOrder";
        //                                }
        //                                if (workstationEntity.WorkstationType == 1)
        //                                {
        //                                    bQOrder = true;
        //                                    sAppName = "SOrder";
        //                                }
        //                            }
        //                        }

        //                        DataTable dtInfor = HouseAccount.CreateTableHouseAccountTransactionInfo(Guid.Empty, sTransactionType, sTenderType, sEntryMode, sAppName, sStoreId, sStoreIDProcess, sStoreNameProcess,
        //                                                                                                sTicketNumber, sEmployeeID, sEmployeeName, sWorkstationID, sWorkstationName);
        //                        HouseAccountTransactionsEntity transactionsEntity = new HouseAccount().UpdateHouseAccountByOutstandingBalance(session, dtInfor, oPaymentDetail, false);

        //                        if (transactionsEntity != null && transactionsEntity.ID != Guid.Empty)
        //                        {
        //                            oPaymentDetail.HouseAccountTransactionID = transactionsEntity.ID.ToString();

        //                            DataRow dr = dtHouseAccountTransactions.NewRow();
        //                            transactionsEntity.TranslateEntityToRow(dr);
        //                            dtHouseAccountTransactions.Rows.Add(dr);
        //                        }
        //                    }

        //                    if (bIssueHouseAccount)
        //                    {
        //                        dTotalBaland += oPaymentDetail.AmountTendered - oPaymentDetail.AmountReturn; // + oPaymentDetail.SVCFeeAmount - oPaymentDetail.CashDiscountAmount;// - oPaymentDetail.CoinDiscountAmount;
        //                        oPaymentDetail.HouseAccountTransactionID = vID.ToString();

        //                        string sTenderType = "";

        //                        if (oPaymentDetail.PaymentMethod.PaymentMethodName != null)
        //                            sTenderType = oPaymentDetail.PaymentMethod.PaymentMethodName;
        //                        else
        //                        {
        //                            IList paymentMethod = DBUtility.CreateQueryList(session, string.Format("From PaymentMethodEntity where PaymentMethodid = {0}", oPaymentDetail.PaymentMethod.PaymentMethodid));
        //                            if (paymentMethod.Count > 0)
        //                            {
        //                                PaymentMethodEntity paymentMethodEntity = paymentMethod[0] as PaymentMethodEntity;
        //                                sTenderType = paymentMethodEntity.PaymentMethodName;
        //                            }
        //                        }

        //                        if (!string.IsNullOrWhiteSpace(sTenderType))
        //                        {
        //                            if (string.IsNullOrWhiteSpace(sIssueTenderType))
        //                                sIssueTenderType = sTenderType;
        //                            else
        //                                sIssueTenderType += $", {sTenderType}";
        //                        }
        //                    }

        //                    session.Save(oPaymentDetail);


        //                    #region add new row to SaleReceiptPaymentDetailsReturn
        //                    dtPaymentReturn.Rows.Add(oPaymentDetail.SaleReceiptPaymentDetailid,
        //                        oPaymentDetail.Gratuity,
        //                        oPaymentDetail.SVCFeeForGratuity,
        //                        oPaymentDetail.MerchantFeeForTip,
        //                        oPaymentDetail.PaymentMethod.PaymentMethodid,
        //                        oPaymentDetail.PartyServiceFeeAmount,
        //                        oPaymentDetail.PartyServiceFeeTaxAmount,
        //                        oPaymentDetail.SVCFeeForPartyServiceFeeAmount,
        //                        oPaymentDetail.MerchantFeeForPartyServiceFeeAmount,
        //                        oPaymentDetail.AutoGratuityAmount,
        //                        oPaymentDetail.AutoGratuityTaxAmount,
        //                        oPaymentDetail.SVCFeeForAutoGratuityAmount,
        //                        oPaymentDetail.MerchantFeeForAutoGratuityAmount,
        //                        oPaymentDetail.AreaFeeAmount,
        //                        oPaymentDetail.AreaFeeTaxAmount,
        //                        oPaymentDetail.SVCFeeForAreaFeeAmount,
        //                        oPaymentDetail.MerchantFeeForAreaFeeAmount,
        //                        oPaymentDetail.ServiceFee,
        //                        oPaymentDetail.ServiceFeeTaxAmount,
        //                        oPaymentDetail.SVCFeeForServiceFee,
        //                        oPaymentDetail.MerchantFeeForServiceFee,
        //                        oPaymentDetail.CreatedDate);
        //                    #endregion

        //                    int sEmployeePaidID = 0;
        //                    if (oReceipt.EmployeePaid != null)
        //                        sEmployeePaidID = oReceipt.EmployeePaid.Employeeid;
        //                    else
        //                        sEmployeePaidID = oReceipt.Cashier == null ? 0 : oReceipt.Cashier.Employeeid;

        //                    if (oPaymentDetail != null)
        //                        sqlEmployeePaidID += $"UPDATE SaleReceiptPaymentDetails set EmployeePaidID='{sEmployeePaidID}' WHERE SaleReceiptPaymentDetailID='{oPaymentDetail.SaleReceiptPaymentDetailid}'; ";

        //                }
        //            catch (Exception ex)
        //            {

        //            }
        //        }


    }
}
