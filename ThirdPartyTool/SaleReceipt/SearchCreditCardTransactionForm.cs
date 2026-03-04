using BLogicSystems.XPos.ClientUtilities;
using System.Data;
using System.Windows.Forms;
using ThirdPartyTool.Dialog;

namespace ThirdPartyTool.SaleReceipt
{
    public partial class SearchCreditCardTransactionForm : Form
    {
        public SearchCreditCardTransactionForm()
        {
            InitializeComponent();

            btnCancel.Click += BtnCancel_Click;
            btnOK.Click += BtnOK_Click;
        }

        private void BtnOK_Click(object sender, System.EventArgs e)
        {
            ConfirmDialogForm confirmFirmDialog = new ConfirmDialogForm(Const.ConfirmTitle, $"Are you sure you want to add all transactions to this ticket?");
            var result = BLogicMessageBox.ShowPopUpFormWithDIMBackground(confirmFirmDialog, this);
            if (result == DialogResult.No)
                return;

            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void BtnCancel_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        public void BindingData(DataTable dtSource)
        {
            try
            {
                dgvTransactions.DataSource = dtSource;

                foreach (DataGridViewColumn col in dgvTransactions.Columns)
                {
                    switch (col.Name)
                    {
                        case "Date":
                        case "CardNumber":
                        case "ExpDate":
                        case "AuthCode":
                        case "Amount":
                        case "Gratuity":
                        case "Total":
                        case "MerchantID":
                        case "WorkstationName":
                        case "Cashier":
                            col.Visible = true;
                            break;
                        default:
                            col.Visible = false;
                            break;
                    }
                }

                label2.Text = $"We found {dtSource.Rows.Count} credit card transaction. Would you like to add it to this ticket?";
            }
            catch (System.Exception ex)
            {
                BLogicLogger.Exception("BindingData in SearchCreditCardTransactionForm", ex);
            }
        }
    }
}
