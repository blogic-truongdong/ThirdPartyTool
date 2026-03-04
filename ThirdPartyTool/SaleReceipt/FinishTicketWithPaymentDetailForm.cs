using BLogicSystems.XPos.Utilites;
using System;
using System.Data;
using System.Windows.Forms;

namespace ThirdPartyTool.SaleReceipt
{
    public partial class FinishTicketWithPaymentDetailForm : Form
    {
        public string SaleReceiptID = string.Empty;
        public FinishTicketWithPaymentDetailForm()
        {
            InitializeComponent();

            btnCancel.Click += BtnCancel_Click;
            btnOK.Click += BtnOK_Click;

            this.Load += FinishTicketWithPaymentDetailForm_Load;

            cmbPaymentMethod.OnSelectedIndexChanged += CmbPaymentMethod_OnSelectedIndexChanged;
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

                    txtTotalAmount_Base.Text = "$" + Utility.GetDecimalFromObject((TotalAmount) - (PaidAmount)).ToString("N2");

                    txtTotalAmount_NoneTax_Base.Text = "$" + Utility.GetDecimalFromObject((TotalAmount - TaxAmount) - (PaidAmount)).ToString("N2");

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
            //FinishTicketWithPaymentDetailForm_Load(sender, e);
            //return;
            //this.Close();

            try
            {
                DataTable dtSaleReceipts = Helper.ProcessQuery(true, $"select * from SaleReceipts where SaleReceiptID = '{SaleReceiptID}'", "SaleReceipts");
                DataTable SaleReceiptPaymentDetails = Helper.ProcessQuery(true, $"select * from SaleReceiptPaymentDetails where SaleReceiptID = '{SaleReceiptID}'", "SaleReceiptPaymentDetails");
                DataTable dtTransactionDetail = Helper.ProcessQuery(false, $"select top 1 * from TransactionDetail order by Date desc", "TransactionDetail");

                if (dtSaleReceipts.Rows.Count > 0)
                {
                    int PaymentMethodID = 0;
                    if (cmbPaymentMethod?.SelectedItem is DataRowView)
                        PaymentMethodID = Utility.GetIntFromRow("PaymentMethodID", (cmbPaymentMethod.SelectedItem as DataRowView).Row);

                    decimal AmountTendered = 0;
                    if (txtTotalAmount_Base.Checked)
                    {
                        AmountTendered = Utility.GetDecimalFromRow("TotalAmount", dtSaleReceipts.Rows[0]);
                    }

                    DateTime CreatedDate = paymentDate.Value.Date + paymentTime.Value.TimeOfDay;
                    object CashierID = Utility.GetIntFromRow("CashierID", dtSaleReceipts.Rows[0]);
                    object ShiftID = Utility.GetStringFromRow("ShiftID", dtSaleReceipts.Rows[0]);
                    decimal Gratuity = Utility.GetDecimalFromObject(txtGratuity.Text);

                    //Helper.GenerateQueryInsert_BLogicPOS7_SaleReceiptPaymentDetails(SaleReceiptPaymentDetails,
                    //    SaleReceiptID, PaymentMethodID, AmountTendered, CreatedDate, CashierID, ShiftID, Gratuity,
                    //    )
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

        public static string GenerateQueryInsert_Merchant_TransactionDetail(DataTable dt, DateTime CreatedDate,
            string CardNumber, string CardType, string ExpDate, string InvoiceNo, string CustomerName, string AuthCode,
            string RefNo, decimal Amount, decimal Gratuity, decimal Total)
        {
            string strInsert = $"INSERT INTO {Helper.Merchant_Name}.dbo.TransactionDetail (";

            //try
            //{
            //    int colCount = dt.Columns.Count;
            //    for (int i = 0; i < colCount; i++)
            //    {
            //        strInsert += "[" + dt.Columns[i].ColumnName.ToUpper() + "]";
            //        if (i < colCount - 1)
            //            strInsert += ", ";
            //    }
            //    strInsert += ") VALUES (";
            //    for (int i = 0; i < colCount; i++)
            //    {
            //        switch (dt.Columns[i].ColumnName)
            //        {
            //            case "ID":
            //                strInsert += $"'NEWID()'";
            //                break;
            //            case "Number":
            //                strInsert += $"'(select max(a.Number)+1 from {Helper.Merchant_Name}.dbo.TransactionDetail a)'";
            //                break;
            //            case "Date":
            //                strInsert += $"'{CreatedDate}'";
            //                break;
            //            case "TranType":
            //                strInsert += $"'Sale'";
            //                break;
            //            case "TenderType":
            //                strInsert += $"'CreditCard'";
            //                break;
            //            case "CardNumber":
            //                strInsert += $"'{CardNumber}'";
            //                break;
            //            case "CardType":
            //                strInsert += $"'{CardType}'";
            //                break;
            //            case "ExpDate":
            //                strInsert += $"'{ExpDate}'";
            //                break;
            //            case "Result":
            //                strInsert += $"'Approved'";
            //                break;
            //            case "TextResponse":
            //                strInsert += $"'OK'";
            //                break;
            //            case "InvoiceNo":
            //                strInsert += $"'{InvoiceNo}'";
            //                break;
            //            case "CustomerName":
            //                strInsert += $"'{CustomerName}'";
            //                break;
            //            case "AuthCode":
            //                strInsert += $"'{AuthCode}'";
            //                break;
            //            case "RefNo":
            //                strInsert += $"'{RefNo}'";
            //                break;
            //            case "AuthorizeAmount":
            //                strInsert += $"'{Total}'";
            //                break;
            //            case "Amount":
            //                strInsert += $"'{Amount}'";
            //                break;
            //            case "Gratuity":
            //                strInsert += $"'{Gratuity}'";
            //                break;
            //            case "Total":
            //                strInsert += $"'{Total}'";
            //                break;
            //            case "BatchID":
            //                strInsert += $"'{BatchID}'";
            //                break;
            //            case "BatchTotal":
            //                strInsert += $"'0'";
            //                break;
            //            case "Token":
            //                strInsert += $"'DyncNueCs5MB8000'";
            //                break;
            //            case "CCEntry":
            //                strInsert += $"'CONTACTLESS'";
            //                break;
            //            case "Description":
            //                strInsert += $"'Success'";
            //                break;
            //            case "ReturnCode":
            //                strInsert += $"'000000'";
            //                break;
            //            case "MerchantID":
            //                strInsert += $"'{MerchantID}'";
            //                break;
            //            case "UserID":
            //                strInsert += $"'{UserID}'";
            //                break;
            //            case "Cashier":
            //                strInsert += $"'{Cashier}'";
            //                break;
            //            case "TrackNo":
            //                strInsert += $"null";
            //                break;
            //            case "TranStatus":
            //                strInsert += $"'ACTIVE'";
            //                break;
            //            case "":
            //                strInsert += $"'{}'";
            //                break;
            //            case "":
            //                strInsert += $"'{}'";
            //                break;
            //            case "":
            //                strInsert += $"'{}'";
            //                break;
            //            case "":
            //                strInsert += $"'{}'";
            //                break;
            //            case "":
            //                strInsert += $"'{}'";
            //                break;
            //            case "":
            //                strInsert += $"'{}'";
            //                break;
            //            case "":
            //                strInsert += $"'{}'";
            //                break;
            //            case "":
            //                strInsert += $"'{}'";
            //                break;
            //            case "":
            //                strInsert += $"'{}'";
            //                break;
            //            case "":
            //                strInsert += $"'{}'";
            //                break;
            //            case "":
            //                strInsert += $"'{}'";
            //                break;
            //            case "":
            //                strInsert += $"'{}'";
            //                break;
            //            case "":
            //                strInsert += $"'{}'";
            //                break;
            //            default:
            //                strInsert += $"''";
            //                break;
            //        }

            //        if (i < colCount - 1)
            //            strInsert += ", ";
            //    }
            //    strInsert += $"); ";
            //}
            //catch (Exception ex)
            //{
            //    BLogicLogger.Exception("GetQueryInsert_Merchant_TransactionDetail", ex);
            //}

            return strInsert;
        }
    }
}
