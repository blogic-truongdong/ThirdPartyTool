using BLogicSystems.XPos.ClientUtilities;
using BLogicSystems.XPos.Utilites;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using ThirdPartyTool.Dialog;
using ThirdPartyTool.Models._7Shifts;

namespace ThirdPartyTool.AdvancedFunctions
{
    public partial class ReSyncDataFor7ShiftsForm : Form
    {
        private BackgroundWorker backgroundWorker;
        public ReSyncDataFor7ShiftsForm()
        {
            InitializeComponent();

            this.Load += CloseUnPaidTicketForm_Load;

            btnCancel.Click += BtnCancel_Click;
            btnOK.Click += BtnOK_Click;

            backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += BackgroundWorker_DoWork;
            backgroundWorker.RunWorkerCompleted += BackgroundWorker_RunWorkerCompleted;
        }

        private async void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }

        private async void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                string sql = " SELECT * FROM Configurations " +
                   " where ConfigurationName ='SystemConfigValue.SevenShiftAPIKey' " +
                   " OR ConfigurationName ='SystemConfigValue.SevenShiftLocation' " +
                   " OR ConfigurationName ='SystemConfigValue.SevenShiftCompanyID' " +
                   " OR ConfigurationName ='SystemConfigValue.CurrentSevenShiftLocation' " +
                   " OR ConfigurationName ='SystemConfigValue.SevenShiftAccessToken'";

                DataTable dtConfiguration = Helper.ProcessQuery(true, sql, "Configurations");

                POSConfig.Configuration.ConfigValue.SystemConfigValue.SevenShiftAPIKey = dtConfiguration.Select("ConfigurationName ='SystemConfigValue.SevenShiftAPIKey'")[0]["ConfigurationValue"].ToString();
                POSConfig.Configuration.ConfigValue.SystemConfigValue.SevenShiftCompanyID = dtConfiguration.Select("ConfigurationName ='SystemConfigValue.SevenShiftCompanyID'")[0]["ConfigurationValue"].ToString();
                POSConfig.Configuration.ConfigValue.SystemConfigValue.SevenShiftLocation = Utility.GetIntFromObject(dtConfiguration.Select("ConfigurationName ='SystemConfigValue.SevenShiftLocation'")[0]["ConfigurationValue"]);
                POSConfig.Configuration.ConfigValue.SystemConfigValue.SevenShiftAccessToken = Utility.GetStringFromObject(dtConfiguration.Select("ConfigurationName ='SystemConfigValue.SevenShiftAccessToken'")[0]["ConfigurationValue"]);

                sql = "select * from Stores";
                DataTable dtStores = Helper.ProcessQuery(true, sql, "Stores");
                if (dtStores?.Rows.Count > 0)
                {
                    string storeID = Utility.GetStringFromRow("StoreID", dtStores.Rows[0]);

                    switch (storeID)
                    {
                        case "20201742":
                            POSConfig.Configuration.ConfigValue.SystemConfigValue.SevenShiftAPIKey = "";
                            POSConfig.Configuration.ConfigValue.SystemConfigValue.SevenShiftCompanyID = "337570";
                            POSConfig.Configuration.ConfigValue.SystemConfigValue.SevenShiftLocation = 411980;
                            POSConfig.Configuration.ConfigValue.SystemConfigValue.SevenShiftAccessToken = "30393837636664642d633865302d346237372d613839332d666332653633373861366238";
                            break;
                        case "20201568":
                            POSConfig.Configuration.ConfigValue.SystemConfigValue.SevenShiftAPIKey = "";
                            POSConfig.Configuration.ConfigValue.SystemConfigValue.SevenShiftCompanyID = "337570";
                            POSConfig.Configuration.ConfigValue.SystemConfigValue.SevenShiftLocation = 434330;
                            POSConfig.Configuration.ConfigValue.SystemConfigValue.SevenShiftAccessToken = "30393837636664642d633865302d346237372d613839332d666332653633373861366238";
                            break;
                        case "20201413":
                            POSConfig.Configuration.ConfigValue.SystemConfigValue.SevenShiftAPIKey = "";
                            POSConfig.Configuration.ConfigValue.SystemConfigValue.SevenShiftCompanyID = "337570";
                            POSConfig.Configuration.ConfigValue.SystemConfigValue.SevenShiftLocation = 411979;
                            POSConfig.Configuration.ConfigValue.SystemConfigValue.SevenShiftAccessToken = "30393837636664642d633865302d346237372d613839332d666332653633373861366238";
                            break;
                        default:
                            ErrorDialogForm errorDialogForm = new ErrorDialogForm("Can not Sync Data for this Store");
                            BLogicMessageBox.ShowPopUpFormWithDIMBackground(errorDialogForm);
                            return;
                    }
                }

                sql = $"select top 3 * from SaleReceipts a where a.DateCreated >= '{StartDate.Value}' and a.DateCreated <= '{EndDate.Value}' and a.TransactionStatusID in ('FIN','VOID','RET') and a.TotalAmount > '0.01' order by a.DateCreated";

                DataTable dtSaleReceipts = Helper.ProcessQuery(true, sql, "SaleReceipts");

                if (!string.IsNullOrWhiteSpace(POSConfig.Configuration.ConfigValue.SystemConfigValue.SevenShiftAccessToken))
                {
                    foreach (DataRow row in dtSaleReceipts.Rows)
                    {
                        sql = $"select c.CategoryIDUnique, a.* from SaleReceiptDetails a left join Items b on a.ItemID = b.ItemID left join Departments c on b.DeptDeptID = c.DepartmentID where a.SaleReceiptID = '{row["SaleReceiptID"]}'";
                        var dtSaleReceiptDetails = Helper.ProcessQuery(true, sql, "SaleReceiptPaymentDetails");

                        sql = $"select * from SaleReceiptPaymentDetails where SaleReceiptID = '{row["SaleReceiptID"]}'";
                        var dtSaleReceiptPaymentDetails = Helper.ProcessQuery(true, sql, "SaleReceiptPaymentDetails");

                        var gratuity = Utility.GetDecimalFromObject(dtSaleReceiptPaymentDetails.Compute("SUM(Gratuity)", "")) * 100;
                        string TransactionStatusID = Utility.GetStringFromRow("TransactionStatusID", row);

                        string SaleReceiptID = Utility.GetStringFromRow("SaleReceiptID", row);
                        int SaleReceiptType = Utility.GetIntFromRow("SaleReceiptType", row);

                        string status = TransactionStatusID == "FIN" || TransactionStatusID == "RET" ? "closed" : "voided";
                        decimal net_total = (Utility.GetDecimalFromRow("TotalAmount", row) - Utility.GetDecimalFromRow("TaxAmount", row) - Utility.GetDecimalFromRow("SalesOrderFee", row)) * 100;
                        decimal gross_total = (Utility.GetDecimalFromRow("TotalAmount", row) - Utility.GetDecimalFromRow("TaxAmount", row)) * 100;
                        decimal total_receipt_discounts = (Utility.GetDecimalFromRow("TotalDiscountAmount", row) * 100);

                        if (TransactionStatusID == "RET")
                            net_total = net_total * (-1);

                        CreateReceiptFor7ShiftsModel createReceiptModel = new CreateReceiptFor7ShiftsModel()
                        {
                            receipt_id = SaleReceiptID,
                            location_id = POSConfig.Configuration.ConfigValue.SystemConfigValue.SevenShiftLocation,
                            receipt_date = Utility.GetDateTimeFromRow("DateCreated", row).ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ssZ"),
                            net_total = (int)net_total,
                            status = status,
                            total_receipt_discounts = (int)total_receipt_discounts,
                            gross_total = (int)gross_total,
                            tips = (int)gratuity,
                            order_type = SaleReceiptType == 16 ? "dine_in" : "take_out"
                        };

                        foreach (DataRow item in dtSaleReceiptPaymentDetails.Rows)
                        {
                            int paymentMethod = Utility.GetIntFromRow("PaymentMethodID", item);
                            int tempGratuity = (int)(Utility.GetDecimalFromRow("Gratuity", item) * 100);

                            createReceiptModel.tip_details.Add(new CreateReceiptFor7ShiftsModel_TipDetail()
                            {
                                type = paymentMethod == 2 ? "cc" : "cash",
                                value = tempGratuity
                            });

                        }

                        foreach (DataRow item in dtSaleReceiptDetails.Rows)
                        {
                            int paymentMethod = Utility.GetIntFromRow("PaymentMethodID", item);
                            int tempGratuity = (int)(Utility.GetDecimalFromRow("Gratuity", item) * 100);
                            int tempStatus = Utility.GetIntFromRow("Status", item);
                            decimal tempDiscount = (Utility.GetDecimalFromRow("Quantity", item) * Utility.GetDecimalFromRow("Price", item) - Utility.GetDecimalFromRow("ExtendedPrice", item)) * 100;

                            if (tempDiscount < 0)
                                tempDiscount = 0;

                            var newLines = new CreateReceiptFor7ShiftsModel_SaleReceiptDetail()
                            {
                                created = Utility.GetDateTimeFromRow("OrderDateTime", item).ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ssZ"),
                                external_item_id = Utility.GetStringFromRow("ItemID", item),
                                gross_item_price = (int)(Utility.GetDecimalFromRow("ExtendedPrice", item) * 100),
                                net_item_price = (int)(Utility.GetDecimalFromRow("ExtendedPrice", item) * 100),
                                price = (int)(Utility.GetDecimalFromRow("Price", item) * 100),
                                item_discount = (int)tempDiscount,
                                quantity = Utility.GetIntFromRow("Quantity", item),
                                status = tempStatus == 2 ? "deleted" : (TransactionStatusID == "VOID" ? "voided" : "closed"),
                            };

                            newLines.external_category_ids = new List<string>();
                            newLines.external_category_ids.Add(Utility.GetStringFromRow("CategoryIDUnique", item));

                            createReceiptModel.receipt_lines.Add(newLines);
                        }

                        string jsonData = JsonConvert.SerializeObject(createReceiptModel);
                        var response = await Helper.ProcessRequest_7Shifts("receipts", System.Net.Http.HttpMethod.Post, jsonData);

                        try
                        {
                            if (response.IsSuccess)
                            {
                                var responseModel = JsonConvert.DeserializeObject<CreateReceiptFor7ShiftsResponseModel>(response.Message);

                                if (!string.IsNullOrWhiteSpace(responseModel?.data?.receipt_id))
                                {
                                    sql = $"insert into SaleReceiptThirdPartyMappings(SaleReceiptID,SaleReceiptID_7Shifts ) values ('{SaleReceiptID}', '{responseModel.data.uuid}')";
                                    var resultUpdate = Helper.RunQueryUpdate(true, sql);
                                }
                            }
                        }
                        catch (Exception)
                        {

                        }
                    }

                }
            }
            catch (Exception ex)
            {
                BLogicLogger.Exception("ReSyncDataFor7ShiftsForm.BackgroundWorker_DoWork", ex);
            }
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            try
            {

                ConfirmDialogForm confirmFirmDialog = new ConfirmDialogForm(Const.ConfirmTitle, $"Are you sure you want to Sync Sales data for 7Shifts?");
                var result = BLogicMessageBox.ShowPopUpFormWithDIMBackground(confirmFirmDialog, this);
                if (result == DialogResult.No)
                    return;

                backgroundWorker.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                ErrorDialogForm errorDialog = new ErrorDialogForm("Unable to close the ticket due to a system error. Please try again later or contact TruongDS.");
                BLogicMessageBox.ShowPopUpFormWithDIMBackground(errorDialog, this);
                BLogicLogger.Exception("BtnOK_Click in CloseUnPaidTicketForm", ex);
            }

        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void CloseUnPaidTicketForm_Load(object sender, EventArgs e)
        {
            StartDate.Value = new DateTime(DateTime.Now.Year, 1, 1);
            EndDate.Value = DateTime.Now;
        }
    }
}
