using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ThirdPartyTool.Models;

namespace ThirdPartyTool
{
    public partial class SettleBatchByMerchantIDForm : Form
    {
        public SettleBatchByMerchantIDForm()
        {
            InitializeComponent();

            btnGenerateScript.Click += BtnGenerateScript_Click;

            this.btnClose.Click += BtnClose_Click;

            btnCopyScript.Click += BtnCopyScript_Click;
        }

        private void BtnCopyScript_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(txtScript.Text);
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void BtnGenerateScript_Click(object sender, EventArgs e)
        {
            string path = "SettleBatchScript.sql";

            List<string> listMerchantID = new List<string>();
            listMerchantID.Add("1851820250");
            listMerchantID.Add("1851753766");

            if (File.Exists(path))
                File.Delete(path);
            using (FileStream fs = File.Create(path))
            using (StreamWriter writer = new StreamWriter(fs))
            {
                foreach (var merchantID in listMerchantID)
                {
                    StringBuilder stringBuilder = new StringBuilder();
                    try
                    {
                        using (var conn = new SqlConnection(Utility.Merchant_Connection))
                        {
                            string sql = $"select * from TransactionDetail where MerchantID = '{merchantID}' and BatchStatus <> 'Closed'";
                            var transactionDetails = await conn.QueryAsync<TransactionDetailEntity>(sql, "", null, 180, System.Data.CommandType.Text);

                            if (transactionDetails?.Count() > 0)
                            {
                                decimal total = transactionDetails.Sum(x => x.Total);
                                decimal gratuity = transactionDetails.Sum(x => x.Gratuity);
                                decimal ticketCount = transactionDetails.Count();


                            }
                            else
                            {
                                continue;
                            }
                        }

                        using (var conn = new SqlConnection(Utility.BLOGICPOS7_Connection))
                        {

                        }


                        writer.WriteLine(stringBuilder.ToString());
                    }
                    catch (Exception ex)
                    {
                        BLogicLogger.Error($"BtnGetScript_Click in UpdateTipForSaleReceiptFile_0_0 error with:  {ex.Message}", ex);
                    }
                }
            }

        }
    }
}
