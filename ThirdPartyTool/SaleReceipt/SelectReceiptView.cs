using System.Windows.Forms;

namespace ThirdPartyTool.SaleReceipt
{
    public partial class SelectReceiptView : UserControl
    {
        public string SaleReceiptID { get; set; } = string.Empty;
        private string saleReceiptNumber = string.Empty;
        private decimal discount = 0m;
        private decimal serviceCharge = 0m;
        private decimal areaServiceCharge = 0m;
        private decimal partyServiceCharge = 0m;
        private decimal autoServiceCharge = 0m;
        private decimal svcFee = 0m;
        private decimal gratuity = 0m;
        private decimal taxAmount = 0m;
        public string SaleReceiptNumber
        {
            get { return saleReceiptNumber; }
            set
            {
                saleReceiptNumber = value;
                txtTicketNumber.Text = $"Ticket #: {saleReceiptNumber}";
            }
        }

        public decimal Discount
        {
            get { return discount; }
            set
            {
                discount = value;
                txtDiscount.Text = $"-${discount}";
                txtDiscount.Parent.Visible = discount > 0;
            }
        }
        public decimal ServiceCharge
        {
            get { return serviceCharge; }
            set
            {
                serviceCharge = value;
                txtServiceCharge.Text = $"${serviceCharge}";
                txtServiceCharge.Parent.Visible = serviceCharge > 0;
            }
        }
        public decimal AreaServiceCharge
        {
            get { return areaServiceCharge; }
            set
            {
                areaServiceCharge = value;
                txtAreaServiceCharge.Text = $"${areaServiceCharge}";
                txtAreaServiceCharge.Parent.Visible = areaServiceCharge > 0;
            }
        }
        public decimal PartyServiceCharge
        {
            get { return partyServiceCharge; }
            set
            {
                partyServiceCharge = value;
                txtPartyServiceCharge.Text = $"${partyServiceCharge}";
                txtPartyServiceCharge.Parent.Visible = partyServiceCharge > 0;
            }
        }
        public decimal AutoServiceCharge
        {
            get { return autoServiceCharge; }
            set
            {
                autoServiceCharge = value;
                txtAutoServiceCharge.Text = $"${autoServiceCharge}";
                txtAutoServiceCharge.Parent.Visible = autoServiceCharge > 0;
            }
        }
        public decimal SvcFee
        {
            get { return svcFee; }
            set
            {
                svcFee = value;
                txtSVCFee.Text = $"${svcFee}";
                txtSVCFee.Parent.Visible = svcFee > 0;
            }
        }
        public decimal Gratuity
        {
            get { return gratuity; }
            set
            {
                gratuity = value;
                txtTip.Text = $"${gratuity}";
                txtTip.Parent.Visible = gratuity > 0;
            }
        }
        public decimal TaxAmount
        {
            get { return taxAmount; }
            set
            {
                taxAmount = value;
                txtTaxAmount.Text = $"${taxAmount}";
                txtTaxAmount.Parent.Visible = taxAmount > 0;
            }
        }

        public SelectReceiptView()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
