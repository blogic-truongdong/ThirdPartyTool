using System;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ThirdPartyTool
{
    public partial class ConnectDatabaseForm : Form
    {
        private const string ConnectDatabase = "ConnectDatabase";
        private const string ConnectDatabaseFail = "ConnectDatabaseFail";
        private const string ConnectDatabaseSuccess = "ConnectDatabaseSuccess";

        private BackgroundWorker backgroundWorker;
        private string ProcessBackgroundWorker = string.Empty;
        public ConnectDatabaseForm()
        {
            StartPosition = FormStartPosition.CenterParent;

            InitializeComponent();

            this.backgroundWorker = new BackgroundWorker();
            this.backgroundWorker.DoWork += BackgroundWorker_DoWork;
            this.backgroundWorker.RunWorkerCompleted += BackgroundWorker_RunWorkerCompleted;

            btnConnect.Click += BtnConnect_Click;
            btnCancel.Click += BtnCancel_Click;
        }

        private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (ProcessBackgroundWorker == ConnectDatabaseSuccess)
                this.Close();

            if (ProcessBackgroundWorker == ConnectDatabaseFail || ProcessBackgroundWorker == ConnectDatabase)
            {
                HideLoading();
                MessageBox.Show("Connect Database Error");
            }
        }

        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (ProcessBackgroundWorker == ConnectDatabase)
            {
                bool isConnectPosSuccess = false;
                bool isConnectMerchantSuccess = false;
                try
                {
                    string connectionString = $@"data source=.;Initial Catalog={txtPOSName.Text.Trim()};integrated security = SSPI";
                    using (var connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                
                        connection.Close();
                
                        isConnectPosSuccess = true;
                
                        Utility.BLOGICPOS7_Connection = connectionString;
                    }
                }
                catch (Exception) { }

                if (!isConnectPosSuccess)
                {
                    try
                    {
                        string connectionString = $"data source=.\\SQLEXPRESS;Initial Catalog={txtPOSName.Text.Trim()};integrated security = SSPI";
                        using (var connection = new SqlConnection(connectionString))
                        {
                            connection.Open();

                            connection.Close();

                            isConnectPosSuccess = true;

                            Utility.BLOGICPOS7_Connection = connectionString;
                        }
                    }
                    catch (Exception) { }
                }

                try
                {
                    string connectionString = $@"data source=.;Initial Catalog={txtMerchantName.Text.Trim()};integrated security = SSPI";
                    using (var connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        connection.Close();

                        isConnectMerchantSuccess = true;

                        Utility.Merchant_Connection = connectionString;
                    }
                }
                catch (Exception) { }

                if (!isConnectMerchantSuccess)
                {
                    try
                    {
                        string connectionString = $"data source=.\\SQLEXPRESS;Initial Catalog={txtMerchantName.Text.Trim()};integrated security = SSPI";
                        using (var connection = new SqlConnection(connectionString))
                        {
                            connection.Open();

                            connection.Close();

                            isConnectMerchantSuccess = true;

                            Utility.Merchant_Connection = connectionString;
                        }
                    }
                    catch (Exception) { }
                }

                if (isConnectPosSuccess && isConnectMerchantSuccess)
                    ProcessBackgroundWorker = ConnectDatabaseSuccess;
                else
                    ProcessBackgroundWorker = ConnectDatabaseFail;
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnConnect_Click(object sender, EventArgs e)
        {
            ShowLoading();
            ProcessBackgroundWorker = ConnectDatabase;
            backgroundWorker.RunWorkerAsync();
        }

        private void ShowLoading()
        {
            pnlContainer.Enabled = false;
            picLoading.Visible = true;
        }

        private void HideLoading()
        {
            pnlContainer.Enabled = true;
            picLoading.Visible = false;
        }
    }
}
