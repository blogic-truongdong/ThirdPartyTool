using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ThirdPartyTool
{
    public partial class ConnectDatabaseForm : Form
    {
        public ConnectDatabaseForm()
        {
            StartPosition = FormStartPosition.CenterParent;

            InitializeComponent();

            btnConnect.Click += BtnConnect_Click;
            btnCancel.Click += BtnCancel_Click;
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                string posConnection = $"data source={txtDataSource.Text.Trim()};Initial Catalog={txtPOSName.Text.Trim()};integrated security = SSPI";
                using (var connection = new SqlConnection())
                {
                    connection.Open();

                    connection.Close();
                }
                Utility.BLOGICPOS7_Connection = posConnection;

                string merchantConnection = $"data source={txtDataSource.Text.Trim()};Initial Catalog={txtMerchantName.Text.Trim()};integrated security = SSPI";
                using (var connection = new SqlConnection())
                {
                    connection.Open();

                    connection.Close();
                }

                Utility.Merchant_Connection = merchantConnection;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Connection Database Error");
                return;
            }

            this.Close();
        }
    }
}
