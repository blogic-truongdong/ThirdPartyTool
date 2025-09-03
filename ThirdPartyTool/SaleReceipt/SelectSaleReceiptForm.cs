using BLogicSystems.XPos.Utilites;
using System.Data;
using System.Windows.Forms;

namespace ThirdPartyTool.SaleReceipt
{
    public partial class SelectSaleReceiptForm : Form
    {
        public SelectSaleReceiptForm()
        {
            InitializeComponent();
        }

        public void BindingSaleReceipts(DataTable dtSaleReceipts)
        {
            try
            {
                foreach (DataRow row in dtSaleReceipts.Rows)
                {
                    SelectReceiptView receiptView = new SelectReceiptView();
                    receiptView.SaleReceiptID = Utility.GetStringFromRow("SaleReceiptID", row);
                    receiptView.SaleReceiptNumber = Utility.GetStringFromRow("SaleReceiptNumber", row);
                }
            }
            catch (System.Exception ex)
            {

                throw;
            }
        }
    }
}
