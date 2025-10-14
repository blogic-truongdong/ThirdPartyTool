using BLogicSystems.XPos.Utilites;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using ThirdPartyTool.Dialog;

namespace ThirdPartyTool
{

    public partial class StartForm : Form
    {
        private string Merchant = "Merchant";
        private string BLogicPOS7 = "BLogicPOS7";
        private string CurrentStep = string.Empty;
        BackgroundWorker _backgroundWorker;

        public StartForm(string blogicPOS7DB, string merchantDB)
        {
            InitializeComponent();
            BLogicPOS7 = blogicPOS7DB;
            Merchant = merchantDB;

            _backgroundWorker = new BackgroundWorker();
            _backgroundWorker.DoWork += _backgroundWorker_DoWork;
            _backgroundWorker.RunWorkerCompleted += _backgroundWorker_RunWorkerCompleted;
            this.Load += MainForm_Load;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            var dt = DateTime.MinValue;

            CurrentStep = Const.PingToServerStep;
            _backgroundWorker.RunWorkerAsync();

        }
        private void _backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                switch (CurrentStep)
                {
                    case Const.PingToServerStep:
                        Helper.GetKioskConnectionFromRegistry();
                        e.Result = Helper.PingServer();
                        break;
                    case Const.GetConnectionString:
                        bool isConnectPosSuccess = false;
                        bool isConnectMerchantSuccess = false;
                        try
                        {
                            string connectionString = $@"data source=.;Initial Catalog={BLogicPOS7};integrated security = SSPI";
                            using (var connection = new SqlConnection(connectionString))
                            {
                                connection.Open();

                                connection.Close();

                                isConnectPosSuccess = true;

                                Helper.BLOGICPOS7_Connection = connectionString;
                            }
                        }
                        catch (Exception) { }

                        if (!isConnectPosSuccess)
                        {
                            try
                            {
                                string connectionString = $"data source=.\\SQLEXPRESS;Initial Catalog={BLogicPOS7};integrated security = SSPI";
                                using (var connection = new SqlConnection(connectionString))
                                {
                                    connection.Open();

                                    connection.Close();

                                    isConnectPosSuccess = true;

                                    Helper.BLOGICPOS7_Connection = connectionString;
                                }
                            }
                            catch (Exception) { }
                        }

                        try
                        {
                            string connectionString = $@"data source=.;Initial Catalog={Merchant};integrated security = SSPI";
                            using (var connection = new SqlConnection(connectionString))
                            {
                                connection.Open();

                                connection.Close();

                                isConnectMerchantSuccess = true;

                                Helper.Merchant_Connection = connectionString;
                            }
                        }
                        catch (Exception) { }

                        if (!isConnectMerchantSuccess)
                        {
                            try
                            {
                                string connectionString = $"data source=.\\SQLEXPRESS;Initial Catalog={Merchant};integrated security = SSPI";
                                using (var connection = new SqlConnection(connectionString))
                                {
                                    connection.Open();

                                    connection.Close();

                                    isConnectMerchantSuccess = true;

                                    Helper.Merchant_Connection = connectionString;
                                }
                            }
                            catch (Exception) { }
                        }
                        e.Result = isConnectPosSuccess && isConnectMerchantSuccess;

                        if (isConnectPosSuccess && isConnectMerchantSuccess)
                        {
                            DataTable dtConfigurations = new DataTable("Configurations");
                            using (SqlCommand sqlCommand = new SqlCommand())
                            using (var conn = new SqlConnection(Helper.BLOGICPOS7_Connection))
                            {
                                if (conn.State == ConnectionState.Closed)
                                    conn.Open();

                                sqlCommand.Connection = conn;
                                sqlCommand.CommandType = CommandType.Text;
                                sqlCommand.CommandText = "select * from Configurations";
                                var reader = sqlCommand.ExecuteReader();

                                dtConfigurations.Load(reader);
                                reader.Close();


                            }
                            POSConfig.Configuration.ConfigValue.GetValueFromTable(dtConfigurations);
                            Utility.IsCredit = true;
                        }

                        break;
                    default:
                        break;
                }

            }
            catch (Exception ex)
            {

            }

        }

        private async void _backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {

                switch (CurrentStep)
                {
                    case Const.PingToServerStep:
                        if (e.Result is bool result)
                        {
                            if (!result)
                            {
                                MessageBox.Show("Unable to connect to POS Server. Please check your POS Server setting.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                CloseApplication();
                            }
                            else
                            {
                                CurrentStep = Const.GetConnectionString;
                                _backgroundWorker.RunWorkerAsync();
                            }
                        }
                        else
                        {
                            CloseApplication();
                        }
                        break;

                    case Const.GetConnectionString:
                        if (e.Result is bool GetConnectionStringResult)
                        {
                            if (!GetConnectionStringResult)
                            {
                                ErrorDialogForm errorDialog = new ErrorDialogForm("Unable to connect to BLogicPOS7 & Merchant database. Please check your database again.");
                                errorDialog.ShowDialog();
                                CloseApplication();
                            }
                            else
                            {
                                CloseForm();
                            }
                        }
                        else
                        {
                            CloseApplication();
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorDialogForm errorDialog = new ErrorDialogForm("Unable to connect to POS Server. Please check your POS Server setting.");
                errorDialog.ShowDialog();
                CloseApplication();
            }
        }


        private void CloseApplication()
        {
            Application.Exit();
        }

        private void CloseForm()
        {
            this.Close();
        }
    }
}
