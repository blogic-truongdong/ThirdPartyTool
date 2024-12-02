using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ThirdPartyTool.AddTipForPayment
{
    public partial class AddTipForPaymentRow : UserControl
    {
        public int PaymentMethodID = 0;
        public string CreditCardTransactionID = string.Empty;
        public AddTipForPaymentRow()
        {
            InitializeComponent();

            this.Size = new Size(292, 243);
            this.Margin = new Padding(5);

            this.Load += AddTipForPaymentRow_Load;
        }

        private void AddTipForPaymentRow_Load(object sender, EventArgs e)
        {
            Total.Text = (Utility.GetDecimalFromObject(AmountTendered.Text) + Utility.GetDecimalFromObject(Gratuity.Text)).ToString("c2");
        }
    }
}
