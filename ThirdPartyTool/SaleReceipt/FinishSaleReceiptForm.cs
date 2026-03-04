using BLogicSystems.XPos.ClientUtilities;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using ThirdPartyTool.Dialog;

namespace ThirdPartyTool.SaleReceipt
{
    public partial class FinishSaleReceiptForm : Form
    {
        public string SaleReceiptID;

        public FinishSaleReceiptForm()
        {
            InitializeComponent();

            btnFindCCTransactionByInvoiNum.Click += BtnFindCCTransactionByInvoiNum_Click;

            btnClose.Click += BtnClose_Click;
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnFindCCTransactionByInvoiNum_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtTransactionDetail = new DataTable("TransactionDetail");

                using (SqlCommand sqlCommand = new SqlCommand())
                using (SqlConnection conn = new SqlConnection(Helper.Merchant_Connection))
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();

                    sqlCommand.Connection = conn;
                    sqlCommand.CommandType = CommandType.Text;

                    var sql = $"select * from {Helper.Merchant_Name}.dbo.TransactionDetail where InvoiceNo = " +
                        $"(select InvoiceNum from {Helper.BLOGICPOS7_Name}.dbo.SaleReceipts where SaleReceiptID = '{SaleReceiptID}')";
                    sqlCommand.CommandText = sql;
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    dtTransactionDetail.Load(reader);
                    reader.Close();

                }

                if (dtTransactionDetail?.Rows.Count > 0)
                {
                    SearchCreditCardTransactionForm searchCreditCard = new SearchCreditCardTransactionForm();
                    searchCreditCard.BindingData(dtTransactionDetail);
                    if (BLogicMessageBox.ShowPopUpFormWithDIMBackground(searchCreditCard) == DialogResult.OK)
                    {

                    }
                }
                else
                {
                    InformationDialogForm form = new InformationDialogForm("Credit Card Transaction not found.");
                    BLogicMessageBox.ShowPopUpFormWithDIMBackground(form, this);

                    FinishTicketWithPaymentDetailForm finishTicketWithPaymentDetailForm = new FinishTicketWithPaymentDetailForm();
                    finishTicketWithPaymentDetailForm.SaleReceiptID = SaleReceiptID;
                    BLogicMessageBox.ShowPopUpFormWithDIMBackground(finishTicketWithPaymentDetailForm, this);
                }
            }
            catch (Exception ex)
            {
                BLogicLogger.Exception("FinishSaleReceiptForm.BtnFindCCTransactionByInvoiNum_Click", ex);
            }
        }
    }
}
