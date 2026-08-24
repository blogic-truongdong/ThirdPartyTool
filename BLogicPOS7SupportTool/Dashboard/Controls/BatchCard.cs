using BLogicPOS7SupportTool.Core;
using System;
using System.Data;
using System.Windows.Forms;

namespace BLogicPOS7SupportTool.Dashboard.Controls
{
    // One open merchant batch on the Batch Manager page. The 1px gray frame is the
    // control's own BackColor showing through its Padding around the white body.
    public partial class BatchCard : UserControl
    {
        public event EventHandler SettleClicked;

        public string MerchantID { get; private set; }
        public string BatchID { get; private set; }

        public BatchCard()
        {
            InitializeComponent();
        }

        // Fills the card from one row of BatchService.GetOpenBatches.
        public void Bind(DataRow row)
        {
            MerchantID = AppSession.GetString(row, "MerchantID");
            BatchID = AppSession.GetString(row, "BatchID");

            lblBatchTitle.Text = "Batch #" + BatchID;
            lblTotal.Text = Money(AppSession.GetDecimal(row, "Total"));
            lblMerchant.Text = MerchantID;
            lblWorkstation.Text = AppSession.GetString(row, "WorkstationName");
            lblTerminalIp.Text = AppSession.GetString(row, "TerminalIpAddress");
            lblTransactions.Text = AppSession.GetInt(row, "CountTicket").ToString();
            lblPeriod.Text = DateRange(AppSession.GetDateTime(row, "FirstDate"), AppSession.GetDateTime(row, "LastDate"));
        }

        private void btnSettle_Click(object sender, EventArgs e)
        {
            SettleClicked?.Invoke(this, EventArgs.Empty);
        }

        // "10/29 11:10 AM – 11:36 AM" within one day, both full dates otherwise.
        private static string DateRange(DateTime first, DateTime last)
        {
            if (first.Date == last.Date)
                return first.ToString("M/d h:mm tt") + " – " + last.ToString("h:mm tt");
            return first.ToString("M/d h:mm tt") + " – " + last.ToString("M/d h:mm tt");
        }

        private static string Money(decimal v)
        {
            return (v < 0 ? "-$" : "$") + Math.Abs(v).ToString("F2");
        }
    }
}
