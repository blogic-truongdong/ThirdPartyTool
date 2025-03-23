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

            if (string.IsNullOrWhiteSpace(Utility.BLOGICPOS7_Connection) || string.IsNullOrWhiteSpace(Utility.Merchant_Connection))
                this.Close();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {

        }
    }
}
