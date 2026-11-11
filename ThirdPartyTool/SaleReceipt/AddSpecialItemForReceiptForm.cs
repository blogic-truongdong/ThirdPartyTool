using System.Windows.Forms;

namespace ThirdPartyTool.SaleReceipt
{
    public partial class AddSpecialItemForReceiptForm : Form
    {
        public string SaleReceiptID = string.Empty;
        public AddSpecialItemForReceiptForm()
        {
            InitializeComponent();

            btnClose.Click += BtnClose_Click;
            this.Load += AddSpecialItemForReceiptForm_Load;
        }

        private void BtnClose_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void AddSpecialItemForReceiptForm_Load(object sender, System.EventArgs e)
        {
            lblEmpty.Visible = flowLayoutPanel1.Controls.Count == 0;
        }
    }
}
