using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ThirdPartyTool
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            StartPosition = FormStartPosition.CenterScreen;
            InitializeComponent();

            this.Load += MainForm_Load;

            btnAddTipBySaleToken.Click += BtnAddTipBySaleToken_Click;

            btnAddTipForTicket.Click += BtnAddTipForTicket_Click;
        }

        private void BtnAddTipForTicket_Click(object sender, EventArgs e)
        {
            AddTipForTicketForm form = new AddTipForTicketForm();
            this.Hide();
            form.ShowDialog();
            this.Show();
        }

        private void BtnAddTipBySaleToken_Click(object sender, EventArgs e)
        {
            AddTipForTicketByPaymentDetailForm form = new AddTipForTicketByPaymentDetailForm();
            this.Hide();
            form.ShowDialog();
            this.Show();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
           ConnectDatabaseForm connectDatabaseForm = new ConnectDatabaseForm();
            connectDatabaseForm.ShowDialog();

            if (string.IsNullOrWhiteSpace(Helper.BLOGICPOS7_Connection) || string.IsNullOrWhiteSpace(Helper.Merchant_Connection))
                this.Close();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {

        }
    }
}
