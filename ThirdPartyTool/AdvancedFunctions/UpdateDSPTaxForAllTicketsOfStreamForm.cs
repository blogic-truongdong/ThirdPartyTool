using BLogicSystems.XPos.ClientUtilities;
using BLogicSystems.XPos.SharedModule.Models.Stream;
using BLogicSystems.XPos.Utilites;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using ThirdPartyTool.Dialog;
using ThirdPartyTool.Models;

namespace ThirdPartyTool.AdvancedFunctions
{
    public partial class UpdateDSPTaxForAllTicketsOfStreamForm : Form
    {
        #region Constants
        private bool IsCancelBackgroundWorker = false;
        #endregion

        #region Fields

        private BackgroundWorker backgroundWorker;

        #endregion

        #region Constructor

        public UpdateDSPTaxForAllTicketsOfStreamForm()
        {
            InitializeComponent();

            InitializeEvents();
            InitializeBackgroundWorker();
        }

        #endregion

        #region Initialization

        private void InitializeEvents()
        {
            this.Load += CloseUnPaidTicketForm_Load;
            btnCancel.Click += BtnCancel_Click;
            btnOK.Click += BtnOK_Click;
            btnCancelProcess.Click += BtnCancelProcess_Click;
        }

        private void BtnCancelProcess_Click(object sender, EventArgs e)
        {
            IsCancelBackgroundWorker = true;
        }

        private void InitializeBackgroundWorker()
        {
            backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += BackgroundWorker_DoWork;
            backgroundWorker.RunWorkerCompleted += BackgroundWorker_RunWorkerCompleted;
        }

        #endregion

        #region Event Handlers

        private void CloseUnPaidTicketForm_Load(object sender, EventArgs e)
        {

        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            try
            {
                IsCancelBackgroundWorker = false;

                ConfirmDialogForm confirmDialog = new ConfirmDialogForm(
                Const.ConfirmTitle,
                $"Are you sure you want to Update DSP Tax for All Ticket of Stream?"
            );

                var result = BLogicMessageBox.ShowPopUpFormWithDIMBackground(confirmDialog, this);
                if (result == DialogResult.Yes)
                {
                    StartProcess();
                    Task.Run(() => backgroundWorker.RunWorkerAsync());
                }
            }
            catch (Exception ex)
            {
                ShowErrorDialog("Unable to Update DSP Tax for all tickets of Stream. Please try again later or contact TruongDS.");
                BLogicLogger.Exception("BtnOK_Click in UpdateDSPTaxForAllTicketsOfStreamForm", ex);
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                string sql = "select SaleReceiptID, TotalAmount, TaxAmount, SaleReceiptJson from SaleReceipts where SaleTypeID = '10' and TransactionStatusID in ('FIN','VOID')";

                DataTable dtSaleReceipts = Helper.ProcessQuery(true, sql, "SaleReceipts");

                sql = $"select top 1 TaxCodeID,TaxRate from SaleReceipts a left join SaleReceiptDetails b on a.SaleReceiptID = b.SaleReceiptID where a.TransactionStatusID = 'FIN' and SaleTypeID = '10' and b.TaxRate > 0 order by DateCreated desc";

                DataTable dtTaxCodes = Helper.ProcessQuery(true, sql, "TaxCodes");

                int TaxCodeID = 0;
                decimal TaxRate = 0;
                int Taxable = 0;

                if (dtTaxCodes?.Rows.Count > 0)
                {
                    TaxCodeID = Utility.GetIntFromRow("TaxCodeID", dtTaxCodes.Rows[0]);
                    TaxRate = Utility.GetDecimalFromRow("TaxRate", dtTaxCodes.Rows[0]);
                    Taxable = TaxRate > 0 ? 1 : 0;
                }

                foreach (DataRow rowReceipt in dtSaleReceipts.Rows)
                {
                    if (IsCancelBackgroundWorker)
                        return;

                    string jsonData = Utility.GetStringFromRow("SaleReceiptJson", rowReceipt);
                    if (string.IsNullOrWhiteSpace(jsonData))
                        return;

                    Stream_NewOrderEventDto streamOrderEvent = JsonConvert.DeserializeObject<Stream_NewOrderEventDto>(jsonData);

                    decimal newTax = Utility.GetDecimalFromObject(streamOrderEvent._Object.Tax) / 100;

                    decimal TaxAmount = Utility.GetDecimalFromRow("TaxAmount", rowReceipt);
                    decimal TotalAmount = Utility.GetDecimalFromRow("TotalAmount", rowReceipt);

                    decimal balance = newTax - TaxAmount;

                    if (balance < 0)
                    {
                        sql = $"select a.* from SaleReceiptDetails a where SaleReceiptID = '{rowReceipt["SaleReceiptID"]}' order by Range";
                        var listSaleReceiptDetails = Helper.ProcessQueryEntity<SaleReceiptDetailEntity>(true, sql);
                        var dtSaleReceiptDetails = Helper.ProcessQuery(true, sql, "SaleReceiptDetails");

                        sql = $"select * from SaleReceiptDetailTaxDetails where SaleReceiptID = '{rowReceipt["SaleReceiptID"]}' order by Range";
                        var listSaleReceiptDetailTaxDetails = Helper.ProcessQueryEntity<SaleReceiptDetailTaxDetailEntity>(true, sql);

                        var sumTotalPrice = listSaleReceiptDetails.Sum(x => x.Quantity * x.Price);

                        List<CalculateModel> listItems = new List<CalculateModel>();
                        List<CalculateModel> listItemTaxDetail = new List<CalculateModel>();

                        if (newTax > 0)
                        {
                            if (dtTaxCodes?.Rows.Count > 0)
                            {
                                TaxCodeID = Utility.GetIntFromRow("TaxCodeID", dtTaxCodes.Rows[0]);
                                TaxRate = Utility.GetDecimalFromRow("TaxRate", dtTaxCodes.Rows[0]);
                                Taxable = TaxRate > 0 ? 1 : 0;
                            }
                        }
                        else
                        {
                            TaxCodeID = 0;
                            TaxRate = 0;
                            Taxable = 0;
                        }

                        foreach (var item in listSaleReceiptDetails)
                        {
                            item.TaxAmount = Utility.RoundUp(item.Quantity * item.Price / sumTotalPrice * newTax, 2);

                            listItems.Add(new CalculateModel
                            {
                                SaleReceiptDetailID = item.SaleReceiptDetailID,
                                TaxAmount = item.TaxAmount
                            });
                        }

                        decimal balanceTax = newTax - listItems.Sum(x => x.TaxAmount);

                        if (balanceTax != 0)
                        {

                            for (int i = listSaleReceiptDetails.Count - 1; i >= 0; i--)
                            {
                                var item = listSaleReceiptDetails[i];

                                if (item.TaxAmount != 0 && item.TaxAmount > balanceTax)
                                {
                                    item.TaxAmount += balanceTax;

                                    var temp = listItems.FirstOrDefault(x => x.SaleReceiptDetailID == item.SaleReceiptDetailID);
                                    if (temp != null)
                                        temp.TaxAmount = item.TaxAmount;
                                    break;
                                }
                            }
                        }

                        foreach (var item in listSaleReceiptDetails)
                        {
                            var FinalTotalSales = item.ExtendedPrice + item.TaxAmount - item.CashDiscountPerItem - item.CoinDiscountPerItem;
                            var FinalGrossSales = (item.ExtendedPrice + item.ItemDiscountValue + item.TotalSaleDiscountAmount
                                             + item.StoreDiscountValue + item.DiscountAmount + item.TotalSaleDiscountAmountByCustomer
                                             + item.ItemDiscountByPriceLevelAmount);

                            sql = $"update SaleReceiptDetails set TaxCodeID = '{TaxCodeID}', TaxRate = '{TaxRate}', TaxAmount = '{item.TaxAmount}', Taxable = '{Taxable}', " +
                            $"FinalTaxAmount = '{item.TaxAmount}', FinalTotalSales = '{FinalTotalSales}', FinalGrossSales = '{FinalGrossSales}', " +
                            $"FinalNetSales = '{item.ExtendedPrice}', FinalNetSalesTax = '{item.ExtendedPrice + item.TaxAmount}' where SaleReceiptDetailID = '{item.SaleReceiptDetailID}'";

                            var result = Helper.RunQueryUpdate(true, sql);

                            if (result > 0)
                            {
                                sql = $"select * from SaleReceiptDetailTaxDetails where SaleReceiptDetailID = '{item.SaleReceiptDetailID}'";
                                DataTable dtSaleReceiptDetailTaxDetails = Helper.ProcessQuery(true, sql, "SaleReceiptDetailTaxDetails");

                                if (dtSaleReceiptDetailTaxDetails?.Rows.Count > 0)
                                {
                                    if (dtSaleReceiptDetailTaxDetails.Rows.Count > 1)
                                    {
                                        sql = $"update SaleReceiptDetailTaxDetails set TaxCodeID = '{TaxCodeID}',TaxRate = '{TaxRate}',TaxAmount = '0' where SaleReceiptDetailID = '{item.SaleReceiptDetailID}';";
                                        result = Helper.RunQueryUpdate(true, sql);
                                    }

                                    sql = $"update SaleReceiptDetailTaxDetails set TaxCodeID = '{TaxCodeID}',TaxRate = '{TaxRate}',TaxAmount = '{item.TaxAmount}' where SaleReceiptDetailTaxDetailID = '{dtSaleReceiptDetailTaxDetails.Rows[0]["SaleReceiptDetailTaxDetailID"]}';";
                                    result = Helper.RunQueryUpdate(true, sql);
                                }
                            }
                        }

                        sql = $"update SaleReceipts set TotalAmount = '{TotalAmount + balance}', TaxAmount = '{newTax}' where SaleReceiptID = '{rowReceipt["SaleReceiptID"]}';";
                        Helper.RunQueryUpdate(true, sql);
                    }
                    else
                    {

                    }
                }
            }
            catch (Exception ex)
            {
                BLogicLogger.Exception("UpdateDSPTaxForAllTicketsOfStreamForm.BackgroundWorker_DoWork", ex);
            }
            finally
            {

            }
        }

        private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                EndProcess();

                if (pnlContainer.InvokeRequired)
                {
                    pnlContainer.Invoke(new MethodInvoker(delegate
                    {
                        InformationDialogForm informationDialogForm = new InformationDialogForm("Update DSP Tax for all tickets of Stream completed.");
                        BLogicMessageBox.ShowPopUpFormWithDIMBackground(informationDialogForm, this);

                        this.Close();
                    }));
                }
                else
                {
                    InformationDialogForm informationDialogForm = new InformationDialogForm("Update DSP Tax for all tickets of Stream completed.");
                    BLogicMessageBox.ShowPopUpFormWithDIMBackground(informationDialogForm, this);

                    this.Close();
                }

            }
            catch (Exception ex)
            {
                BLogicLogger.Exception("UpdateDSPTaxForAllTicketsOfStreamForm.BackgroundWorker_RunWorkerCompleted", ex);
            }
        }

        #endregion

        #region UI State Management

        private void StartProcess()
        {
            if (pnlContainer.InvokeRequired)
            {
                pnlContainer.Invoke(new MethodInvoker(delegate
                {
                    SetProcessingState(true);
                }));
            }
            else
            {
                SetProcessingState(true);
            }
        }

        private void EndProcess()
        {
            if (pnlContainer.InvokeRequired)
            {
                pnlContainer.Invoke(new MethodInvoker(delegate
                {
                    SetProcessingState(false);
                }));
            }
            else
            {
                SetProcessingState(false);
            }
        }

        private void SetProcessingState(bool isProcessing)
        {
            pnlContainer.Enabled = !isProcessing;
            pnlLoadingContainer.Visible = isProcessing;
        }

        private void ShowErrorDialog(string message)
        {
            ErrorDialogForm errorDialog = new ErrorDialogForm(message);
            BLogicMessageBox.ShowPopUpFormWithDIMBackground(errorDialog, this);
        }

        #endregion
    }
}
