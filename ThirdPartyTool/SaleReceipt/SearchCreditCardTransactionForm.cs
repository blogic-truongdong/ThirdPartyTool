using System.Data;
using System.Windows.Forms;

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
            }
            catch (System.Exception ex)
            {
                BLogicLogger.Exception("BindingData in SearchCreditCardTransactionForm", ex);
            }
        }
    }
}
