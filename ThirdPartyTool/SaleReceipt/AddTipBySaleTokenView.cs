using BLogicSystems.XPos.Utilites;
using DevExpress.XtraReports.UI;
using System;
using System.Data;
using System.Windows.Forms;
using ThirdPartyTool.Dialog;

namespace ThirdPartyTool.SaleReceipt
{
    public partial class AddTipBySaleTokenView : UserControl
    {
        private string MainSaleReceiptID = string.Empty;
        private string TokenSaleReceiptID = string.Empty;
        private string MainSaleReceiptNumber = string.Empty;
        private string TokenSaleReceiptNumber = string.Empty;
        private decimal TokenTotalAmount = 0;

        public AddTipBySaleTokenView()
        {
            InitializeComponent();

            txtSearch.KeyUp += TxtSearch_KeyUp;
            btnSearch.Click += BtnSearch_Click;

            btnSearchTokenReceipt.Click += BtnSearchTokenReceipt_Click;
            txtSearchTokenReceipt.KeyUp += TxtSearchTokenReceipt_KeyUp;

            this.Load += AddTipBySaleTokenView_Load;

            btnUpdate.Click += BtnUpdate_Click;

        }

        private void BtnUpdate_Click(object sender, EventArgs e)
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

                BtnSearch_Click(sender, e);
            }
        }

        private void AddTipBySaleTokenView_Load(object sender, EventArgs e)
        {
            txtFocus.Focus();
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

                XtraReport xReport = processForm.ReceiptTemplate;
                if (xReport == null)
                {
                    TokenSaleReceiptID = string.Empty;
                    TokenSaleReceiptNumber = string.Empty;

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

        public XtraReport PreviewSaleReceipt(object saleID)
        {
            try
            {

                return null;
            }
            catch (Exception ex)
            {
                Utility.IsUseConfigDualPrice = false;
                Utility.IsCurrentPriceUseDualPrice = false;
                return null;
            }
        }

        public void AddSuggestedTip(DataSet ds)
        {
            DataTable dtReceipt = ds.Tables["SaleReceipts"];
            string sStatus = Utility.GetStringFromRow("TransactionStatusID", dtReceipt.Rows[0]);
            if (sStatus == TransactionStatus.CANCEL || sStatus == TransactionStatus.RB_CANCEL
                || sStatus == TransactionStatus.REFUND || sStatus == TransactionStatus.RETURN)
            {
                return;
            }
            DataTable dt = CreateSuggestedTipsTable();
            ds.Tables.Add(dt);
        }
        private DataTable CreateSuggestedTipsTable()
        {
            //string sSuggestedTips = POSConfig.Configuration.ConfigValue.CreditCardConfigValue.CCSuggestedTips;

            //if (string.IsNullOrEmpty(sSuggestedTips))
            //{
            //    sSuggestedTips = System.Configuration.ConfigurationManager.AppSettings.Get("SuggestedTips");
            //}
            string sSuggestedTips = POSConfig.Configuration.ConfigValue.CreditCardConfigValue.CCSuggestedTips;
            DataTable dt = new DataTable("SuggestedTips");
            dt.Columns.Add("SuggestedTips", typeof(string));
            dt.Rows.Add(new object[] { sSuggestedTips });
            return dt;
        }

        private void AddCustomerCompany(DataTable dtReceipt, string sCustomerCompany)
        {
            if (dtReceipt == null || dtReceipt.Rows.Count == 0)
                return;

            if (!dtReceipt.Columns.Contains("CustomerCompany"))
                dtReceipt.Columns.Add("CustomerCompany", typeof(string));
            dtReceipt.Rows[0]["CustomerCompany"] = sCustomerCompany;
        }

        public object GetValueFromColumn(DataColumn column, DataRow row)
        {
            try
            {
                object _value;

                switch (column.DataType.Name)
                {
                    case "String":
                        _value = Utility.GetStringFromObject(row[column.ColumnName]);
                        if (string.IsNullOrEmpty(_value.ToString()))
                            _value = " ";
                        break;
                    case "Int":
                    case "Int32":
                    case "Int16":
                    case "Short":
                        _value = Utility.GetIntFromObject(row[column.ColumnName]);
                        break;
                    case "Boolean":
                        _value = Utility.GetBoolFromObject(row[column.ColumnName]);
                        break;
                    case "Decimal":
                        _value = Utility.GetDecimalFromObject(row[column.ColumnName]);
                        break;
                    case "Double":
                        _value = Utility.GetDoubleFromObject(row[column.ColumnName]);
                        break;
                    case "DateTime":
                        _value = Utility.GetDateTimeFromObject(row[column.ColumnName]);
                        break;
                    case "Guid":
                        _value = Utility.GetStringFromObject(row[column.ColumnName]);
                        if (_value.ToString() == Guid.Empty.ToString() || string.IsNullOrEmpty(_value.ToString()))
                            _value = DBNull.Value;
                        break;
                    default:
                        _value = Utility.GetStringFromObject(row[column.ColumnName]);
                        if (string.IsNullOrEmpty(_value.ToString()))
                            _value = " ";
                        break;
                }
                return _value;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
