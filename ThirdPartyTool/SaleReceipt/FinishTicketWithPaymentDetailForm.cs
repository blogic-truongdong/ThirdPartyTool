using BLogicSystems.XPos.ClientUtilities;
using BLogicSystems.XPos.Utilites;
using System;
using System.Data;
using System.Windows.Forms;
using ThirdPartyTool.Dialog;
using ThirdPartyTool.Models;

namespace ThirdPartyTool.SaleReceipt
{
    public partial class FinishTicketWithPaymentDetailForm : Form
    {
        private string BatchUnique = string.Empty;
        private string MerchantID = string.Empty;
        public string SaleReceiptID = string.Empty;
        public FinishTicketWithPaymentDetailForm()
        {
            InitializeComponent();

            btnCancel.Click += BtnCancel_Click;
            btnOK.Click += BtnOK_Click;

            this.Load += FinishTicketWithPaymentDetailForm_Load;

            cmbPaymentMethod.OnSelectedIndexChanged += CmbPaymentMethod_OnSelectedIndexChanged;

            btnBrowseBatch.Click += BtnBrowseBatch_Click;
        }

        private void BtnBrowseBatch_Click(object sender, EventArgs e)
        {
            SearchBatchSettledForm searchBatchSettledForm = new SearchBatchSettledForm();

            if (BLogicMessageBox.ShowPopUpFormWithDIMBackground(searchBatchSettledForm, this) == DialogResult.OK)
            {
                MerchantID = searchBatchSettledForm.SelectedMerchantID;
                BatchUnique = searchBatchSettledForm.SelectedBatchUnique;

                txtBatchID.Text = searchBatchSettledForm.SelectedBatchID;
            }
        }

        private void CmbPaymentMethod_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbPaymentMethod?.SelectedItem is DataRowView)
                {
                    if (Utility.GetIntFromRow("PaymentMethodID", (cmbPaymentMethod.SelectedItem as DataRowView).Row) == 2)
                        pnlBatchID.Visible = true;
                    else
                        pnlBatchID.Visible = false;
                }
            }
            catch (Exception ex)
            {
                BLogicLogger.Exception("FinishTicketWithPaymentDetailForm.CmbPaymentMethod_OnSelectedIndexChanged", ex);
            }
        }

        private void FinishTicketWithPaymentDetailForm_Load(object sender, EventArgs e)
        {
            try
            {
                paymentDate.Value = DateTime.Now;
                paymentTime.Value = DateTime.Now;

                BindingPaymentMethod();

                BindingSaleReceipts();
            }
            catch (Exception ex)
            {
                BLogicLogger.Exception("FinishTicketWithPaymentDetailForm.FinishTicketWithPaymentDetailForm_Load", ex);
            }
        }

        private void BindingPaymentMethod()
        {
            try
            {
                DataTable dtPaymentMethods = Helper.ProcessQuery(true, "select PaymentMethodID,PaymentMethodName from PaymentMethods", "PaymentMethods");

                // Bind payment method to combo box
                cmbPaymentMethod.DisplayMember = "PaymentMethodName";
                cmbPaymentMethod.ValueMember = "PaymentMethodID";
                cmbPaymentMethod.DataSource = dtPaymentMethods;

            }
            catch (Exception ex)
            {
                BLogicLogger.Exception("FinishTicketWithPaymentDetailForm.BindingPaymentMethod", ex);
            }
        }

        private void BindingSaleReceipts()
        {
            try
            {
                DataTable dtSaleReceipts = Helper.ProcessQuery(true, $"select * from SaleReceipts where SaleReceiptID = '{SaleReceiptID}'", "SaleReceipts");

                if (dtSaleReceipts?.Rows.Count > 0)
                {
                    label3.Text = Utility.GetStringFromRow("SaleReceiptNumber", dtSaleReceipts.Rows[0]);

                    decimal TotalAmount = Utility.GetDecimalFromRow("TotalAmount", dtSaleReceipts.Rows[0]);
                    decimal TaxAmount = Utility.GetDecimalFromRow("TaxAmount", dtSaleReceipts.Rows[0]);
                    decimal PaidAmount = Utility.GetDecimalFromRow("PaidAmount", dtSaleReceipts.Rows[0]);
                    decimal SVCFeeAmount = Utility.GetDecimalFromRow("SVCFeeAmount", dtSaleReceipts.Rows[0]);
                    decimal CashDiscountAmount = Utility.GetDecimalFromRow("CashDiscountAmount", dtSaleReceipts.Rows[0]);
                    decimal SVCFeePaidAmount = Utility.GetDecimalFromRow("SVCFeePaidAmount", dtSaleReceipts.Rows[0]);
                    decimal CashDiscountPaidAmount = Utility.GetDecimalFromRow("CashDiscountPaidAmount", dtSaleReceipts.Rows[0]);

                    txtTotalAmount_Base.Text = "$" + Utility.GetDecimalFromObject(TotalAmount + (SVCFeeAmount - CashDiscountAmount) - (SVCFeePaidAmount - CashDiscountPaidAmount) - PaidAmount).ToString("N2");

                    txtTotalAmount_NoneTax_Base.Text = "$" + Utility.GetDecimalFromObject(TotalAmount - TaxAmount - PaidAmount).ToString("N2");

                    txtTotalAmount_Include_SVCFee.Text = "$" + Utility.GetDecimalFromObject(TotalAmount + SVCFeeAmount - PaidAmount - SVCFeePaidAmount).ToString("N2");

                    txtTotalAmount_NoneTax_Include_SVCFee.Text = "$" + Utility.GetDecimalFromObject(TotalAmount - TaxAmount + SVCFeeAmount - PaidAmount - SVCFeePaidAmount).ToString("N2");

                    txtTotalAmount_Include_CashDiscount.Text = "$" + Utility.GetDecimalFromObject((TotalAmount - CashDiscountAmount) - (PaidAmount - CashDiscountPaidAmount)).ToString("N2");

                    txtTotalAmount_NoneTax_Include_CashDiscount.Text = "$" + Utility.GetDecimalFromObject((TotalAmount - TaxAmount - CashDiscountAmount) - (PaidAmount - CashDiscountPaidAmount)).ToString("N2");

                }

            }
            catch (Exception ex)
            {
                BLogicLogger.Exception("FinishTicketWithPaymentDetailForm.BindingSaleReceipts", ex);
            }
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            // All statements (POS, Merchant, batch) in one transaction and one audit row: saved together or not at all.
            using (SqlUnit unit = SqlUnit.Begin("Finish ticket", SaleReceiptID))
            {
                BtnOK_ClickCore(sender, e);
                if (!unit.Complete() && unit.Failed)
                {
                    string message = SqlSafety.IsBusy(unit.Error) ? SqlSafety.BusyMessage
                        : "Finishing the ticket failed, nothing was changed." + (unit.Error == null ? "" : Environment.NewLine + unit.Error.Message);
                    BLogicMessageBox.ShowPopUpFormWithDIMBackground(new ErrorDialogForm(message));
                }
            }
        }

        private void BtnOK_ClickCore(object sender, EventArgs e)
        {
            //FinishTicketWithPaymentDetailForm_Load(sender, e);
            //return;
            //this.Close();

            try
            {
                DataTable dtSaleReceipts = Helper.ProcessQuery(true, $"select * from SaleReceipts where SaleReceiptID = '{SaleReceiptID}'", "SaleReceipts");
                var listSaleReceipts = Helper.ProcessQueryEntity<SaleReceiptEntity>(true, $"select * from SaleReceipts where SaleReceiptID = '{SaleReceiptID}'");
                DataTable dtSaleReceiptPaymentDetails = Helper.ProcessQuery(true, $"select * from SaleReceiptPaymentDetails where SaleReceiptID = '{SaleReceiptID}'", "SaleReceiptPaymentDetails");
                DataTable dtTransactionDetail = Helper.ProcessQuery(false, $"select top 1 * from TransactionDetail where BatchUnique = '{BatchUnique}' order by Date desc", "TransactionDetail");
                DataTable dtPaymentMethods = Helper.ProcessQuery(true, $"select * from PaymentMethods", "PaymentMethods");

                if (dtSaleReceipts.Rows.Count > 0 && listSaleReceipts?.Count > 0)
                {
                    int PaymentMethodID = 0;
                    if (cmbPaymentMethod?.SelectedItem is DataRowView)
                        PaymentMethodID = Utility.GetIntFromRow("PaymentMethodID", (cmbPaymentMethod.SelectedItem as DataRowView).Row);

                    if (PaymentMethodID == 2 && string.IsNullOrWhiteSpace(txtBatchID.Text))
                    {
                        InformationDialogForm informationDialogForm = new InformationDialogForm("Please select Batch for Credit Card payment method");
                        BLogicMessageBox.ShowPopUpFormWithDIMBackground(informationDialogForm);

                        return;
                    }

                    int CashAdjust = 1;
                    var drs = dtPaymentMethods.Select($"PaymentMethodID = '{PaymentMethodID}'", "");
                    if (drs?.Length > 0)
                        CashAdjust = Utility.GetIntFromRow("CashAdjust", drs[0]);

                    DateTime CreatedDate = paymentDate.Value.Date + paymentTime.Value.TimeOfDay;
                    string CashierID = Utility.GetStringFromRow("CashierID", dtSaleReceipts.Rows[0]);
                    string ShiftID = Utility.GetStringFromRow("ShiftID", dtSaleReceipts.Rows[0]);
                    string InvoiceNum = Utility.GetStringFromRow("InvoiceNum", dtSaleReceipts.Rows[0]);
                    decimal TotalAmount = Utility.GetDecimalFromRow("TotalAmount", dtSaleReceipts.Rows[0]);
                    decimal TaxAmount = Utility.GetDecimalFromRow("TaxAmount", dtSaleReceipts.Rows[0]);
                    decimal PaidAmount = Utility.GetDecimalFromRow("PaidAmount", dtSaleReceipts.Rows[0]);
                    decimal SVCFeeAmount = Utility.GetDecimalFromRow("SVCFeeAmount", dtSaleReceipts.Rows[0]);
                    decimal CashDiscountAmount = Utility.GetDecimalFromRow("CashDiscountAmount", dtSaleReceipts.Rows[0]);
                    decimal CashDiscountTaxAmount = Utility.GetDecimalFromRow("CashDiscountTaxAmount", dtSaleReceipts.Rows[0]);
                    decimal SVCFeePaidAmount = Utility.GetDecimalFromRow("SVCFeePaidAmount", dtSaleReceipts.Rows[0]);
                    decimal CashDiscountPaidAmount = Utility.GetDecimalFromRow("CashDiscountPaidAmount", dtSaleReceipts.Rows[0]);
                    decimal CashDiscountTaxPaidAmount = Utility.GetDecimalFromRow("CashDiscountTaxPaidAmount", dtSaleReceipts.Rows[0]);
                    string WorkstationID = Utility.GetStringFromRow("WorkstationID", dtSaleReceipts.Rows[0]);
                    string WorkstationName = Utility.GetStringFromRow("WorkstationName", dtSaleReceipts.Rows[0]);
                    string SaleReceiptOrder = Utility.GetStringFromRow("SaleReceiptOrder", dtSaleReceipts.Rows[0]);
                    string SaleReceiptNumber = Utility.GetStringFromRow("SaleReceiptNumber", dtSaleReceipts.Rows[0]);
                    decimal TotalDiscountAmount = Utility.GetDecimalFromRow("TotalDiscountAmount", dtSaleReceipts.Rows[0]);
                    decimal SalesOrderFee = Utility.GetDecimalFromRow("SalesOrderFee", dtSaleReceipts.Rows[0]);
                    decimal PartyServiceFeeAmount = Utility.GetDecimalFromRow("PartyServiceFeeAmount", dtSaleReceipts.Rows[0]);
                    decimal AreaFeeAmount = Utility.GetDecimalFromRow("AreaFeeAmount", dtSaleReceipts.Rows[0]);
                    decimal ServiceFee = Utility.GetDecimalFromRow("ServiceFee", dtSaleReceipts.Rows[0]);
                    decimal AutoGratuityAmount = Utility.GetDecimalFromRow("AutoGratuityAmount", dtSaleReceipts.Rows[0]);
                    decimal SVCFeeTaxAmount = Utility.GetDecimalFromRow("SVCFeeTaxAmount", dtSaleReceipts.Rows[0]);
                    decimal SVCFeeTaxPaidAmount = Utility.GetDecimalFromRow("SVCFeeTaxPaidAmount", dtSaleReceipts.Rows[0]);

                    decimal Gratuity = Utility.GetDecimalFromObject(txtGratuity.Text);
                    decimal svcFeeForGratuity = Helper.CalculateSVCFeeAmountForTip(Gratuity);
                    string cashierName = Utility.GetStringFromObject(Helper.CreateQuerySelectUnique(true, $"select EmployeeFirstName + ' ' + EmployeeLastName from Employees where EmployeeID = '{CashierID}'"));
                    string Number = Utility.GetStringFromObject(Helper.CreateQuerySelectUnique(false, $"(select max(a.Number)+1 from TransactionDetail a)"));
                    while (Number.Length < 6)
                    {
                        Number = "0" + Number;
                    }

                    decimal _svcFeeAmount = 0;
                    decimal _svcFeeTaxAmount = 0;
                    decimal _paidAmount = 0;
                    decimal _cashDiscountAmount = 0;
                    decimal _cashDiscountTaxAmount = 0;

                    if (txtTotalAmount_Base.Checked)
                    {
                        _paidAmount = TotalAmount - PaidAmount;

                        _svcFeeAmount = SVCFeeAmount - SVCFeePaidAmount;
                        _svcFeeTaxAmount = SVCFeeTaxAmount - SVCFeeTaxPaidAmount;

                        _cashDiscountAmount = CashDiscountAmount - CashDiscountPaidAmount;
                        _cashDiscountTaxAmount = CashDiscountTaxAmount - CashDiscountTaxPaidAmount;
                    }
                    else if (txtTotalAmount_Include_SVCFee.Checked)
                    {
                        _paidAmount = TotalAmount - PaidAmount;
                        _svcFeeAmount = SVCFeeAmount - SVCFeePaidAmount;
                        _svcFeeTaxAmount = SVCFeeTaxAmount - SVCFeeTaxPaidAmount;
                    }
                    else if (txtTotalAmount_Include_CashDiscount.Checked)
                    {
                        _paidAmount = TotalAmount - PaidAmount;
                        _cashDiscountAmount = CashDiscountAmount - CashDiscountPaidAmount;
                        _cashDiscountTaxAmount = CashDiscountTaxAmount - CashDiscountTaxPaidAmount;

                        svcFeeForGratuity = 0;
                    }
                    else if (txtTotalAmount_NoneTax_Base.Checked)
                    {
                        _paidAmount = TotalAmount - PaidAmount - TaxAmount;

                        TaxAmount = 0;

                    }
                    else if (txtTotalAmount_NoneTax_Include_SVCFee.Checked)
                    {
                        _paidAmount = TotalAmount - PaidAmount - TaxAmount;

                        TaxAmount = 0;

                        _svcFeeAmount = SVCFeeAmount - SVCFeePaidAmount;
                        _svcFeeTaxAmount = SVCFeeTaxAmount - SVCFeeTaxPaidAmount;
                    }
                    else if (txtTotalAmount_NoneTax_Include_CashDiscount.Checked)
                    {
                        _paidAmount = TotalAmount - PaidAmount - TaxAmount;

                        TaxAmount = 0;

                        _cashDiscountAmount = CashDiscountAmount - CashDiscountPaidAmount;
                        _cashDiscountTaxAmount = CashDiscountTaxAmount - CashDiscountTaxPaidAmount;

                        svcFeeForGratuity = 0;
                    }

                    decimal subTotal = TotalAmount - SalesOrderFee - TaxAmount + TotalDiscountAmount;
                    decimal merchantFee = Utility.CalculateMerchantFee(_paidAmount + _svcFeeAmount + Gratuity);
                    decimal merchantFeeForTip = Utility.CalculateMerchantFee(Gratuity);
                    string cardNumber = string.IsNullOrWhiteSpace(txtCardNumber.Text) ? "xxxx0000" : (txtCardNumber.Text.Contains("xxxx") ? txtCardNumber.Text : "xxxx" + txtCardNumber.Text);
                    string TerminalIpAddress = dtTransactionDetail?.Rows.Count > 0 ? Utility.GetStringFromRow("TerminalIpAddress", dtTransactionDetail.Rows[0]) : "";
                    string TerminalName = dtTransactionDetail?.Rows.Count > 0 ? Utility.GetStringFromRow("TerminalName", dtTransactionDetail.Rows[0]) : "";

                    var newTransactionID = Guid.NewGuid();
                    string sqlInsert;
                    int resultQuery = 0;

                    if (CashAdjust == 1)
                    {
                        svcFeeForGratuity = 0;
                        merchantFeeForTip = 0;
                        merchantFee = 0;
                    }

                    if (PaymentMethodID == 2)
                    {

                        sqlInsert = GenerateQueryInsert_Merchant_TransactionDetail(dtTransactionDetail, newTransactionID, Number, CreatedDate,
                            cardNumber, txtCardType.Text.ToUpper(), txtExpiryDate.Text, InvoiceNum, txtAuthCode.Text,
                            txtReferenceNumber.Text, _paidAmount + _svcFeeAmount, Gratuity,
                            _paidAmount + _svcFeeAmount + Gratuity, txtBatchID.Text, MerchantID,
                            CashierID, cashierName, WorkstationID, WorkstationName, SaleReceiptOrder, svcFeeForGratuity,
                            _svcFeeAmount, SaleReceiptNumber, BatchUnique, subTotal, TaxAmount, TotalDiscountAmount,
                            SalesOrderFee, TotalAmount, PartyServiceFeeAmount, AreaFeeAmount, ServiceFee, TerminalIpAddress, merchantFee,
                            merchantFeeForTip, AutoGratuityAmount, _svcFeeTaxAmount,
                            POSConfig.Configuration.ConfigValue.CreditCardConfigValue.MerchantFee,
                            string.IsNullOrWhiteSpace(txtNote.Text) ? "Insert by BLogic Tools" : txtNote.Text.Trim(), TerminalName);

                        resultQuery = Helper.RunQueryUpdate(false, sqlInsert);

                        if (resultQuery <= 0)
                        {
                            ErrorDialogForm errorDialogForm = new ErrorDialogForm("Insert new Transaction to Database MERCHANT Fail, Please contact TruongDS");
                            BLogicMessageBox.ShowPopUpFormWithDIMBackground(errorDialogForm);
                            return;
                        }

                        Helper.UpdateBatchByBatchUnique(BatchUnique);

                    }
                    else
                    {

                    }

                    object SaleReceiptPaymentDetailID = Helper.CreateQuerySelectUnique(true, $"select max( SaleReceiptPaymentDetailID)+1 from SaleReceiptPaymentDetails ");

                    sqlInsert = GenerateQueryInsert_BLogicPOS7_SaleReceiptPaymentDetails(dtSaleReceiptPaymentDetails,
                       SaleReceiptPaymentDetailID, SaleReceiptID, PaymentMethodID, _paidAmount,
                       CreatedDate, CashierID, ShiftID, Gratuity, _svcFeeAmount,
                       svcFeeForGratuity, _cashDiscountAmount, _cashDiscountTaxAmount, merchantFee, merchantFeeForTip, _svcFeeTaxAmount, TaxAmount);

                    var result = Helper.RunQueryUpdate(true, sqlInsert);

                    if (result <= 0)
                    {
                        ErrorDialogForm errorDialogForm = new ErrorDialogForm("Insert new Transaction to Database MERCHANT Fail, Please contact TruongDS");
                        BLogicMessageBox.ShowPopUpFormWithDIMBackground(errorDialogForm);
                        return;
                    }

                    Helper.ReCalculateServiceChargesInSaleReceiptPaymentDetails(SaleReceiptID);

                    if (PaymentMethodID == 2)
                    {
                        Helper.RunQueryUpdate(true, $"update SaleReceiptPaymentDetails set CreditCardTransactionID = '{newTransactionID}' where SaleReceiptPaymentDetailID = '{SaleReceiptPaymentDetailID}' ");
                        Helper.RunQueryUpdate(true, $"update SaleReceiptPaymentDetails set CreditCardTransactionNumber = '{Number}' where SaleReceiptPaymentDetailID = '{SaleReceiptPaymentDetailID}' ");
                        Helper.RunQueryUpdate(true, $"update SaleReceiptPaymentDetails set CreditCardTransactionCardNumber = '{cardNumber}' where SaleReceiptPaymentDetailID = '{SaleReceiptPaymentDetailID}' ");
                        Helper.RunQueryUpdate(true, $"update SaleReceiptPaymentDetails set CreditCardTransactionType = 'Sale_Settled' where SaleReceiptPaymentDetailID = '{SaleReceiptPaymentDetailID}' ");
                        Helper.RunQueryUpdate(true, $"update SaleReceiptPaymentDetails set CreditCardRefNo = '{txtReferenceNumber.Text}' where SaleReceiptPaymentDetailID = '{SaleReceiptPaymentDetailID}' ");
                        Helper.RunQueryUpdate(true, $"update SaleReceiptPaymentDetails set CreditCardAuthCode = '{txtAuthCode.Text}' where SaleReceiptPaymentDetailID = '{SaleReceiptPaymentDetailID}' ");
                        Helper.RunQueryUpdate(true, $"update SaleReceiptPaymentDetails set CreditCardCardType = '{txtCardType.Text.ToUpper()}' where SaleReceiptPaymentDetailID = '{SaleReceiptPaymentDetailID}' ");
                        Helper.RunQueryUpdate(true, $"update SaleReceiptPaymentDetails set CreditCardTransactionMerchantID = '{MerchantID}' where SaleReceiptPaymentDetailID = '{SaleReceiptPaymentDetailID}' ");
                    }
                    return;
                    Helper.RunQueryUpdate(true, $"update SaleReceipts set PaidAmount = '{PaidAmount + _paidAmount}'," +
                        $"SVCFeePaidAmount = '{SVCFeePaidAmount + _svcFeeAmount}'," +
                        $"SVCFeeTaxPaidAmount = '{SVCFeeTaxPaidAmount + _svcFeeTaxAmount}'," +
                        $"CashDiscountPaidAmount = '{CashDiscountPaidAmount + _cashDiscountAmount}'," +
                        $"CashDiscountTaxPaidAmount = '{CashDiscountTaxPaidAmount + _cashDiscountTaxAmount}'," +
                        $"TransactionStatusID = 'FIN' where SaleReceiptID = '{SaleReceiptID}'");

                }
            }
            catch (Exception ex)
            {
                BLogicLogger.Exception("BtnOK_Click In FinishTicketWithPaymentDetailForm", ex);
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public static string GenerateQueryInsert_Merchant_TransactionDetail(DataTable dt, Guid ID, string Number, DateTime CreatedDate,
            string CardNumber, string CardType, string ExpDate, string InvoiceNo, string AuthCode,
            string RefNo, decimal Amount, decimal Gratuity, decimal Total, string BatchID, string MerchantID, string UserID,
            string Cashier, string WorkstationID, string WorkstationName, string SaleReceiptOrder, decimal SVCFeeForGratuity,
            decimal SVCFee, string SaleReceiptNumber, string BatchUnique, decimal SubTotal, decimal TaxAmount, decimal DiscountAmount,
            decimal ServiceCharge, decimal TotalAmount, decimal PartyServiceFeeAmount, decimal AreaFeeAmount, decimal ServiceFee,
            string TerminalIpAddress, decimal MerchantFee, decimal MerchantFeeForTip, decimal AutoGratuityAmount, decimal SVCFeeTaxAmount,
            decimal MerchantFeePercent, string Address, string TerminalName)
        {
            string strInsert = $"INSERT INTO {Helper.Merchant_Name}.dbo.TransactionDetail (";

            try
            {
                int colCount = dt.Columns.Count;
                for (int i = 0; i < colCount; i++)
                {
                    strInsert += "[" + dt.Columns[i].ColumnName.ToUpper() + "]";
                    if (i < colCount - 1)
                        strInsert += ", ";
                }
                strInsert += ") VALUES (";
                for (int i = 0; i < colCount; i++)
                {
                    switch (dt.Columns[i].ColumnName)
                    {
                        case "Address":
                            strInsert += $"'{Address}'";
                            break;
                        case "ID":
                            strInsert += $"'{ID}'";
                            break;
                        case "Number":
                            strInsert += $"'{Number}'";
                            break;
                        case "Date":
                            strInsert += $"'{CreatedDate}'";
                            break;
                        case "TranType":
                            strInsert += $"'Sale'";
                            break;
                        case "TenderType":
                            strInsert += $"'CreditCard'";
                            break;
                        case "CardNumber":
                            strInsert += $"'{CardNumber}'";
                            break;
                        case "CardType":
                            strInsert += $"'{CardType}'";
                            break;
                        case "ExpDate":
                            strInsert += $"'{ExpDate}'";
                            break;
                        case "Result":
                            strInsert += $"'Approved'";
                            break;
                        case "TextResponse":
                            strInsert += $"'OK'";
                            break;
                        case "InvoiceNo":
                            strInsert += $"'{InvoiceNo}'";
                            break;
                        case "CustomerName":
                            strInsert += $"''";
                            break;
                        case "AuthCode":
                            strInsert += $"'{AuthCode}'";
                            break;
                        case "RefNo":
                            strInsert += $"'{RefNo}'";
                            break;
                        case "AuthorizeAmount":
                        case "OriginalAmount":
                        case "Total":
                            strInsert += $"'{Total}'";
                            break;
                        case "Amount":
                            strInsert += $"'{Amount}'";
                            break;
                        case "Gratuity":
                        case "OriginalTip":
                            strInsert += $"'{Gratuity}'";
                            break;
                        case "BatchID":
                            strInsert += $"'{BatchID}'";
                            break;
                        case "BatchTotal":
                            strInsert += $"'0'";
                            break;
                        case "BatchStatus":
                            strInsert += $"'Closed'";
                            break;
                        case "Token":
                            strInsert += $"'DyncNueCs5MB8000'";
                            break;
                        case "CCEntry":
                            strInsert += $"'CONTACTLESS'";
                            break;
                        case "Description":
                            strInsert += $"'Success'";
                            break;
                        case "ReturnCode":
                            strInsert += $"'000000'";
                            break;
                        case "MerchantID":
                        case "RegisterID":
                        case "MerchantAccount":
                            strInsert += $"'{MerchantID}'";
                            break;
                        case "UserID":
                        case "ServerID":
                            strInsert += $"'{UserID}'";
                            break;
                        case "Cashier":
                        case "ServerName":
                            strInsert += $"'{Cashier}'";
                            break;
                        case "TrackNo":
                        case "IsTipInLine":
                            strInsert += $"null";
                            break;
                        case "TranStatus":
                            strInsert += $"'ACTIVE'";
                            break;
                        case "TerminalName":
                            strInsert += $"'{TerminalName}'";
                            break;
                        case "BankApprovalCode":
                            strInsert += $"'610815501296'";
                            break;
                        case "BankTransactionID":
                            strInsert += $"'79043291'";
                            break;
                        case "WorkstationID":
                            strInsert += $"'{WorkstationID}'";
                            break;
                        case "WorkstationName":
                            strInsert += $"'{WorkstationName}'";
                            break;
                        case "IsChecked":
                            strInsert += $"'1'";
                            break;
                        case "OrderID":
                            strInsert += $"'{SaleReceiptOrder}'";
                            break;
                        case "SVCFeeForGratuity":
                            strInsert += $"'{SVCFeeForGratuity}'";
                            break;
                        case "SVCFee":
                            strInsert += $"'{SVCFee}'";
                            break;
                        case "TicketNumber":
                            strInsert += $"'{SaleReceiptNumber}'";
                            break;
                        case "AID":
                            strInsert += $"'A0000000031010'";
                            break;
                        case "BatchUnique":
                            strInsert += $"'{BatchUnique}'";
                            break;
                        case "SubTotal":
                            strInsert += $"'{SubTotal}'";
                            break;
                        case "TaxAmount":
                            strInsert += $"'{TaxAmount}'";
                            break;
                        case "DiscountAmount":
                            strInsert += $"'{DiscountAmount}'";
                            break;
                        case "ServiceCharge":
                            strInsert += $"'{ServiceCharge}'";
                            break;
                        case "TotalAmount":
                            strInsert += $"'{TotalAmount}'";
                            break;
                        case "PartyServiceFeeAmount":
                            strInsert += $"'{PartyServiceFeeAmount}'";
                            break;
                        case "AreaFeeAmount":
                            strInsert += $"'{AreaFeeAmount}'";
                            break;
                        case "ServiceFee":
                            strInsert += $"'{ServiceFee}'";
                            break;
                        case "TerminalIpAddress":
                            strInsert += $"'{TerminalIpAddress}'";
                            break;
                        case "MerchantFee":
                            strInsert += $"'{MerchantFee}'";
                            break;
                        case "MerchantFeeForTip":
                            strInsert += $"'{MerchantFeeForTip}'";
                            break;
                        case "IsTryVoidOpenTab":
                            strInsert += $"'0'";
                            break;
                        case "AmountCaptured":
                            strInsert += $"'-1'";
                            break;
                        case "TipCaptured":
                            strInsert += $"'-1'";
                            break;
                        case "IsPhone":
                            strInsert += $"'0'";
                            break;
                        case "AutoGratuityAmount":
                            strInsert += $"'{AutoGratuityAmount}'";
                            break;
                        case "CreditCardInfo":
                            strInsert += $"null";
                            break;
                        case "TicketOrder":
                            strInsert += $"'{SaleReceiptOrder}'";
                            break;
                        case "SVCFeeTaxAmount":
                            strInsert += $"'{SVCFeeTaxAmount}'";
                            break;
                        case "SVCFeeType":
                            strInsert += $"'1'";
                            break;
                        case "MerchantFeePercent":
                            strInsert += $"'{MerchantFeePercent}'";
                            break;
                        case "CardNumberMasked":
                            strInsert += $"'{CardNumber}'";
                            break;
                        case "AdjustTipInformation":
                            strInsert += $"null";
                            break;
                        default:
                            strInsert += $"''";
                            break;
                    }

                    if (i < colCount - 1)
                        strInsert += ", ";
                }
                strInsert += $"); ";
            }
            catch (Exception ex)
            {
                BLogicLogger.Exception("GenerateQueryInsert_Merchant_TransactionDetail", ex);
            }

            return strInsert;
        }

        public static string GenerateQueryInsert_BLogicPOS7_SaleReceiptPaymentDetails(DataTable dt,
            object SaleReceiptPaymentDetailID, object SaleReceiptID, object PaymentMethodID,
            object AmountTendered, object CreatedDate, object Cashier, string ShiftID,
            object Gratuity, object SVCFeeAmount, object SVCFeeForGratuity, object CashDiscountAmount, object CashDiscountTaxAmount,
            object MerchantFee, object MerchantFeeForTip, decimal SVCFeeTaxAmount, object TaxAmount)
        {
            string strInsert = $"SET IDENTITY_INSERT SaleReceiptPaymentDetails ON {Environment.NewLine}{Environment.NewLine}" +
               $"INSERT INTO SaleReceiptPaymentDetails (";

            try
            {
                int colCount = dt.Columns.Count;
                for (int i = 0; i < colCount; i++)
                {
                    strInsert += "[" + dt.Columns[i].ColumnName.ToUpper() + "]";
                    if (i < colCount - 1)
                        strInsert += ", ";
                }
                strInsert += ") VALUES (";
                for (int i = 0; i < colCount; i++)
                {
                    switch (dt.Columns[i].ColumnName)
                    {
                        case "SaleReceiptPaymentDetailID":
                            strInsert += $"'{SaleReceiptPaymentDetailID}'";
                            break;
                        case "SaleReceiptID":
                            strInsert += $"'{SaleReceiptID}'";
                            break;
                        case "PaymentMethodID":
                            strInsert += $"'{PaymentMethodID}'";
                            break;
                        case "AmountTendered":
                            strInsert += $"'{AmountTendered}'";
                            break;
                        case "AmountReturn":
                            strInsert += $"'0'";
                            break;
                        case "CreatedDate":
                            strInsert += $"'{CreatedDate}'";
                            break;
                        case "IsDeposit":
                        case "GiftExtra":
                            strInsert += $"'0'";
                            break;
                        case "Cashier":
                            strInsert += $"'{Cashier}'";
                            break;
                        case "ShiftID":
                            strInsert += string.IsNullOrWhiteSpace(ShiftID) ? $"null" : $"'{ShiftID}'";
                            break;
                        case "GiftCardCode":
                            strInsert += $"null";
                            break;
                        case "CurrencyAmount":
                            strInsert += $"'0'";
                            break;
                        case "CurrencyID":
                            strInsert += $"'1'";
                            break;
                        case "PaymentReceiptID":
                        case "PayByAccuPoint":
                            strInsert += $"null";
                            break;
                        case "PaymentType":
                            strInsert += $"'0'";
                            break;
                        case "Gratuity":
                            strInsert += $"'{Gratuity}'";
                            break;
                        case "PaidServiceFee":
                            strInsert += $"'0'";
                            break;
                        case "PaidPayout":
                            strInsert += $"'0'";
                            break;
                        case "SVCFeeAmount":
                            strInsert += $"'{SVCFeeAmount}'";
                            break;
                        case "SVCFeeForGratuity":
                            strInsert += $"'{SVCFeeForGratuity}'";
                            break;
                        case "SVCFeeForServiceCharge":
                            strInsert += $"'0'";
                            break;
                        case "CashDiscountAmount":
                            strInsert += $"'{CashDiscountAmount}'";
                            break;
                        case "MerchantFee":
                            strInsert += $"'{MerchantFee}'";
                            break;
                        case "PartyServiceFeeAmount":
                            strInsert += $"'0'";
                            break;
                        case "SVCFeeForPartyServiceFeeAmount":
                            strInsert += $"'0'";
                            break;
                        case "AreaFeeAmount":
                            strInsert += $"'0'";
                            break;
                        case "SVCFeeForAreaFeeAmount":
                            strInsert += $"'0'";
                            break;
                        case "ServiceFee":
                            strInsert += $"'0'";
                            break;
                        case "SVCFeeForServiceFee":
                            strInsert += $"'0'";
                            break;
                        case "MerchantFeeForTip":
                            strInsert += $"'{MerchantFeeForTip}'";
                            break;
                        case "MerchantFeeForServiceFee":
                            strInsert += $"'0'";
                            break;
                        case "MerchantFeeForAreaFeeAmount":
                            strInsert += $"'0'";
                            break;
                        case "MerchantFeeForPartyServiceFeeAmount":
                            strInsert += $"'0'";
                            break;
                        case "CardBalance":
                            strInsert += $"'0'";
                            break;
                        case "AutoGratuityAmount":
                            strInsert += $"'0'";
                            break;
                        case "SVCFeeForAutoGratuityAmount":
                            strInsert += $"'0'";
                            break;
                        case "MerchantFeeForAutoGratuityAmount":
                            strInsert += $"'0'";
                            break;
                        case "IsDecline":
                            strInsert += $"'0'";
                            break;
                        case "IsDepositPayment":
                            strInsert += $"'0'";
                            break;
                        case "SVCFeeTaxAmount":
                            strInsert += $"'{SVCFeeTaxAmount}'";
                            break;
                        case "CashDiscountTaxAmount":
                            strInsert += $"'{CashDiscountTaxAmount}'";
                            break;
                        case "ServiceFeeTaxAmount":
                            strInsert += $"'0'";
                            break;
                        case "AreaFeeTaxAmount":
                            strInsert += $"'0'";
                            break;
                        case "PartyServiceFeeTaxAmount":
                            strInsert += $"'0'";
                            break;
                        case "AutoGratuityTaxAmount":
                            strInsert += $"'0'";
                            break;
                        case "TaxAmountByPaymentModeTaxCustomFee":
                            strInsert += $"'0'";
                            break;
                        case "IsCalculateTaxCustomFee":
                            strInsert += (SVCFeeTaxAmount > 0 ? "1" : "0");
                            break;
                        case "CoinDiscountAmount":
                            strInsert += $"'0'";
                            break;
                        case "EmployeePaidID":
                            strInsert += $"'{Cashier}'";
                            break;
                        case "PaidHouseAccount":
                            strInsert += $"'0'";
                            break;
                        default:
                            strInsert += $"''";
                            break;
                    }

                    if (i < colCount - 1)
                        strInsert += ", ";
                }
                strInsert += $"); {Environment.NewLine}{Environment.NewLine}" +
                    $"SET IDENTITY_INSERT SaleReceiptPaymentDetails OFF";
            }
            catch (Exception ex)
            {
                BLogicLogger.Exception("GenerateQueryInsert_Merchant_TransactionDetail", ex);
            }

            return strInsert;
        }

    }
}
