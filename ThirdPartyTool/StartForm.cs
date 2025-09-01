using System;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Windows.Forms;
using ThirdPartyTool.CustomReceipt;

namespace ThirdPartyTool
{
    public partial class StartForm : Form
    {
        private bool isConnectPosSuccess = false;
        private bool isConnectMerchantSuccess = false;

        private BackgroundWorker backgroundWorker;
        private string ProcessBackgroundWorker = string.Empty;
        public StartForm()
        {
            StartPosition = FormStartPosition.CenterParent;

            InitializeComponent();

            this.backgroundWorker = new BackgroundWorker();
            this.backgroundWorker.DoWork += BackgroundWorker_DoWork;
            this.backgroundWorker.RunWorkerCompleted += BackgroundWorker_RunWorkerCompleted;

            this.backgroundWorker.RunWorkerAsync();
        }

        private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (!isConnectPosSuccess)
                {
                    MessageBox.Show("Connect database BLogicPOS7 error -> Exit program");
                    Application.Exit();
                }

                if (!isConnectMerchantSuccess)
                {
                    MessageBox.Show("Connect database Merchant error -> Exit program");
                    Application.Exit();
                }

                this.Hide();

                CustomReceiptForm customReceiptForm = new CustomReceiptForm();
                customReceiptForm.ShowDialog();
            }
            catch (Exception ex)
            {
                BLogicLogger.Error("BackgroundWorker_RunWorkerCompleted in StartForm", ex);
            }
        }

        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                //try
                //{
                //    string connectionString = $@"data source=.;Initial Catalog=BLogicPOS7;integrated security = SSPI";
                //    using (var connection = new SqlConnection(connectionString))
                //    {
                //        connection.Open();

                //        connection.Close();

                //        isConnectPosSuccess = true;

                //        Utility.BLOGICPOS7_Connection = connectionString;
                //    }
                //}
                //catch (Exception) { }

                if (!isConnectPosSuccess)
                {
                    try
                    {
                        string connectionString = $"data source=.\\SQLEXPRESS;Initial Catalog=BLogicPOS7;integrated security = SSPI";
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

                //try
                //{
                //    string connectionString = $@"data source=.;Initial Catalog=Merchant;integrated security = SSPI";
                //    using (var connection = new SqlConnection(connectionString))
                //    {
                //        connection.Open();

                //        connection.Close();

                //        isConnectMerchantSuccess = true;

                //        Utility.Merchant_Connection = connectionString;
                //    }
                //}
                //catch (Exception) { }

                if (!isConnectMerchantSuccess)
                {
                    try
                    {
                        string connectionString = $"data source=.\\SQLEXPRESS;Initial Catalog=Merchant;integrated security = SSPI";
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
            }
            catch (Exception ex)
            {
                BLogicLogger.Error("BackgroundWorker_DoWork in StartForm", ex);
            }
        }
    }
}
