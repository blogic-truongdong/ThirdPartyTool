using BLogicSystems.XPos.ClientUtilities;
using BLogicSystems.XPos.Utilites;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using ThirdPartyTool.Dialog;

namespace ThirdPartyTool.AdvancedFunctions
{
    public partial class CloseUnPaidTicketForm : Form
    {
        public CloseUnPaidTicketForm()
        {
            InitializeComponent();

            this.Load += CloseUnPaidTicketForm_Load;

            btnCancel.Click += BtnCancel_Click;
            btnOK.Click += BtnOK_Click;
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            // One audit row for everything this action changes.
            using (AuditLog.Group("Advanced: close unpaid tickets", singleTicket: false))
                BtnOK_ClickCore(sender, e);
        }

        private void BtnOK_ClickCore(object sender, EventArgs e)
        {
            try
            {
                DateTime startDate = new DateTime(StartDate.Value.Year, StartDate.Value.Month, StartDate.Value.Day, 0, 0, 0);
                DateTime endDate = new DateTime(EndDate.Value.Year, EndDate.Value.Month, EndDate.Value.Day, 23, 59, 59);

                DataTable dtSaleReceipts = new DataTable("SaleReceipts");
                using (SqlCommand sqlCommand = new SqlCommand())
                using (var conn = new SqlConnection(Helper.BLOGICPOS7_Connection))
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();

                    sqlCommand.Connection = conn;
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.CommandText = $"select count(SaleReceiptID) as Count from SaleReceipts where DateCreated between '{startDate}' and '{endDate}' and TransactionStatusID = 'OPER' ";
                    var reader = sqlCommand.ExecuteReader();
                    dtSaleReceipts.Load(reader);
                    reader.Close();
                }

                var count = 0;
                if (dtSaleReceipts?.Rows.Count > 0)
                    count = Utility.GetIntFromRow("Count", dtSaleReceipts.Rows[0]);

                ConfirmDialogForm confirmFirmDialog = new ConfirmDialogForm(Const.ConfirmTitle, $"Are you sure you want to CLOSE {count} open tickets?");
                var result = BLogicMessageBox.ShowPopUpFormWithDIMBackground(confirmFirmDialog, this);
                if (result == DialogResult.No)
                    return;

                using (SqlCommand sqlCommand = new SqlCommand())
                using (var conn = new SqlConnection(Helper.BLOGICPOS7_Connection))
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();

                    sqlCommand.Connection = conn;

                    var sql = $"update SaleReceipts set TransactionStatusID = 'CLO' where TransactionStatusID = 'OPER' and DateCreated between '{startDate}' and '{endDate}' and PaidAmount = '0'";

                    Helper.RunQueryUpdate(sqlCommand, sql);
                }

                DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                ErrorDialogForm errorDialog = new ErrorDialogForm("Unable to close the ticket due to a system error. Please try again later or contact TruongDS.");
                BLogicMessageBox.ShowPopUpFormWithDIMBackground(errorDialog, this);
                BLogicLogger.Exception("BtnOK_Click in CloseUnPaidTicketForm", ex);
            }

        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void CloseUnPaidTicketForm_Load(object sender, EventArgs e)
        {
            StartDate.Value = new DateTime(DateTime.Now.Year, 1, 1);
            EndDate.Value = DateTime.Now;
        }
    }
}
