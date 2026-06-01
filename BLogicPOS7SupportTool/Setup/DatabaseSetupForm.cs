using BLogicPOS7SupportTool.Core;
using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace BLogicPOS7SupportTool.Setup
{
    public partial class DatabaseSetupForm : Form
    {
        private Point _dragScreenStart;

        public DatabaseSetupForm()
        {
            InitializeComponent();
            this.Load += DatabaseSetupForm_Load;
        }

        private void DatabaseSetupForm_Load(object sender, EventArgs e)
        {
#if DEBUG
            txtDatabase1.Text = "BLogicPOS7_MicroPantryFrisco";
            txtDatabase2.Text = "Merchant_MicroPantryFrisco";
            return;
#endif
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnContinue_Click(object sender, EventArgs e)
        {
            string server = txtServer.Text.Trim();
            string db1 = txtDatabase1.Text.Trim();
            string db2 = txtDatabase2.Text.Trim();

            AppLogger.Info($"Testing connection — server: {server}, BLogicPOS7: {db1}, Merchant: {db2}");

            string conn1 = $"Data Source={server};Initial Catalog={db1};Trusted_Connection=True;integrated security = SSPI;Connection Timeout= 600;TrustServerCertificate=true;";
            if (!TryConnect(conn1, out string err1))
            {
                AppLogger.Error($"Connection failed for BLogicPOS7 ({db1}): {err1}");
                MessageBox.Show($"Cannot connect to BLogicPOS7:\n{err1}", "Connection Failed",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string conn2 = $"Data Source={server};Initial Catalog={db2};Trusted_Connection=True;integrated security = SSPI;Connection Timeout= 600;TrustServerCertificate=true;";
            if (!TryConnect(conn2, out string err2))
            {
                AppLogger.Error($"Connection failed for Merchant ({db2}): {err2}");
                MessageBox.Show($"Cannot connect to Merchant:\n{err2}", "Connection Failed",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            AppSession.SetBLogicPOS7(conn1, db1);
            AppSession.SetMerchant(conn2, db2);

            AppLogger.Info("Both databases connected successfully");

            DialogResult = DialogResult.OK;
            Close();
        }

        private static bool TryConnect(string connStr, out string error)
        {
            try
            {
                using (var conn = new SqlConnection(connStr))
                    conn.Open();
                error = null;
                return true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }
    }
}
