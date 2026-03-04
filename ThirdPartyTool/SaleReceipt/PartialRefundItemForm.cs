using BLogicSystems.XPos.ClientUtilities;
using BLogicSystems.XPos.Utilites;
using Microsoft.Practices.CompositeUI.Utility;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using ThirdPartyTool.Dialog;
using ThirdPartyTool.Models;

namespace ThirdPartyTool.SaleReceipt
{
    public partial class PartialRefundItemForm : Form
    {
        public string MainSaleReceiptNumber { get; set; }
        public string MainSaleReceiptPaymentDetailID { get; set; }
        public string MainSaleReceiptID { get; set; }
        public PartialRefundItemForm()
        {
            InitializeComponent();

            this.Load += PartialRefundItemForm_Load;

            btnProcessRefundPartialItem.Click += BtnProcessRefundPartialItem_Click;

            btnClose.Click += BtnClose_Click;
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void PartialRefundItemForm_Load(object sender, EventArgs e)
        {
            BindingPartialRefundItems();
        }

        private void BindingPartialRefundItems()
        {
            try
            {
                lblTotalRefundItem_Amount.Text = $"Subtotal: 0";
                lblTotalRefundItem_Tax.Text = $"Tax amount: 0";
                lblTotalRefundItem_TotalAmount.Text = $"Total amount: 0";

                DataTable dtSaleReceiptDetails = new DataTable("SaleReceiptDetails");
                DataTable dtSaleReceiptPaymentDetails = new DataTable("SaleReceiptPaymentDetails");

                string sql = "";
                using (SqlCommand sqlCommand = new SqlCommand())
                using (var conn = new SqlConnection(Helper.BLOGICPOS7_Connection))
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();

                    sqlCommand.Connection = conn;
                    sqlCommand.CommandType = CommandType.Text;

                    sql = $"select * from SaleReceiptDetails where SaleReceiptID = '{MainSaleReceiptID}' and ISNULL(ReturnCode,'') = '' and Status <> '2' order by Range desc";

                    sqlCommand.CommandText = sql;
                    var reader = sqlCommand.ExecuteReader();
                    dtSaleReceiptDetails.Load(reader);
                    reader.Close();

                    sqlCommand.CommandText = $"select a.*, b.PaymentMethodName " +
                        $" from SaleReceiptPaymentDetails a " +
                        $" left join PaymentMethods b on a.PaymentMethodID = b.PaymentMethodID " +
                        $" where SaleReceiptID in (select a.SaleReceiptID from SaleReceipts a where a.SaleReceiptID = '{MainSaleReceiptID}')";
                    reader = sqlCommand.ExecuteReader();
                    dtSaleReceiptPaymentDetails.Load(reader);
                    reader.Close();
                }

                pnlRefundItemContainer.Controls.Clear();

                foreach (DataRow item in dtSaleReceiptDetails.Rows)
                {
                    var PriceInitial = Utility.GetStringFromRow("PriceInitial", item);
                    if (string.IsNullOrWhiteSpace(PriceInitial) || PriceInitial == "P")
                    {
                        RefundItemRow refundItem = new RefundItemRow();
                        refundItem.SelectedItemChanges += RefundItem_SelectedItemChanges;
                        refundItem.Dock = DockStyle.Top;
                        refundItem.BindingValue(
                            Utility.GetStringFromRow("SaleReceiptDetailID", item),
                            Utility.GetStringFromRow("Description", item),
                            Utility.GetDecimalFromRow("Price", item),
                            Utility.GetDecimalFromRow("Quantity", item),
                            Utility.GetDecimalFromRow("ExtendedPrice", item),
                             Utility.GetDecimalFromRow("TaxAmount", item),
                             Utility.GetStringFromRow("GroupID", item));
                        pnlRefundItemContainer.Controls.Add(refundItem);
                    }
                    else
                    {
                        RefundModiferRow refundModifier = new RefundModiferRow();
                        refundModifier.SelectedItemChanges += RefundItem_SelectedItemChanges;
                        refundModifier.Dock = DockStyle.Top;
                        refundModifier.BindingValue(
                            Utility.GetStringFromRow("SaleReceiptDetailID", item),
                            Utility.GetStringFromRow("Description", item),
                            Utility.GetDecimalFromRow("Price", item),
                            Utility.GetDecimalFromRow("Quantity", item),
                            Utility.GetDecimalFromRow("ExtendedPrice", item),
                             Utility.GetDecimalFromRow("TaxAmount", item),
                             Utility.GetStringFromRow("GroupID", item));
                        pnlRefundItemContainer.Controls.Add(refundModifier);
                    }

                }
            }
            catch (Exception ex)
            {
                BLogicLogger.Exception("BindingPartialRefundItems", ex);
            }
        }

        private void RefundItem_SelectedItemChanges(object sender, DataEventArgs<RequestModel> e)
        {
            try
            {
                decimal totalAmount = 0;
                decimal amount = 0;
                decimal tax = 0;

                foreach (System.Windows.Forms.Control item in pnlRefundItemContainer.Controls)
                {
                    RefundItemRow refundItem = item as RefundItemRow;
                    RefundModiferRow refundModifier = item as RefundModiferRow;

                    if (refundItem != null)
                    {
                        if (refundItem.GroupID == e.Data.GroupID)
                        {
                            refundItem.IsSelectedItem = e.Data.IsSelectedItem;
                            refundItem.chkCheck.Checked = e.Data.IsSelectedItem;
                            refundItem.BackColor = e.Data.IsSelectedItem ? Color.Red : Color.White;
                        }

                        if (refundItem.IsSelectedItem)
                        {
                            totalAmount += refundItem.ExtendedPrice + refundItem.TaxAmount;
                            amount += refundItem.ExtendedPrice;
                            tax += refundItem.TaxAmount;
                        }
                        continue;
                    }

                    if (refundModifier != null)
                    {
                        if (refundModifier.GroupID == e.Data.GroupID)
                        {
                            refundModifier.IsSelectedItem = e.Data.IsSelectedItem;
                            refundModifier.BackColor = e.Data.IsSelectedItem ? Color.Red : Color.White;
                        }

                        if (refundModifier.IsSelectedItem)
                        {
                            totalAmount += refundModifier.ExtendedPrice + refundModifier.TaxAmount;
                            amount += refundModifier.ExtendedPrice;
                            tax += refundModifier.TaxAmount;
                        }
                        continue;
                    }
                }

                lblTotalRefundItem_Amount.Text = $"Subtotal: {Utility.ConvertDecimalToMoneyString(amount)}";
                lblTotalRefundItem_Tax.Text = $"Tax amount: {Utility.ConvertDecimalToMoneyString(tax)}";
                lblTotalRefundItem_TotalAmount.Text = $"Total Amount: {Utility.ConvertDecimalToMoneyString(totalAmount)}";
            }
            catch (Exception ex)
            {
                BLogicLogger.Exception("RefundItem_SelectedItemChanges", ex);
            }
        }

        private void BtnProcessRefundPartialItem_Click(object sender, EventArgs e)
        {
            try
            {
                RequestModel requestModel = new RequestModel();
                requestModel.SaleReceiptID = MainSaleReceiptID;
                requestModel.SaleReceiptPaymentDetailID = MainSaleReceiptPaymentDetailID;

                foreach (System.Windows.Forms.Control item in pnlRefundItemContainer.Controls)
                {
                    RefundModiferRow refundModifier = item as RefundModiferRow;

                    if (refundModifier != null)
                    {
                        if (refundModifier.IsSelectedItem)
                        {
                            requestModel.objectValues.Add(new ObjectValues()
                            {
                                SaleReceiptDetailID = refundModifier.SaleReceiptDetailID,
                            });

                            requestModel.TotalAmount += refundModifier.ExtendedPrice + refundModifier.TaxAmount;
                            requestModel.TaxAmount += refundModifier.TaxAmount;
                        }
                    }

                    RefundItemRow refundItem = item as RefundItemRow;

                    if (refundItem != null)
                    {
                        if (refundItem.IsSelectedItem)
                        {
                            requestModel.objectValues.Add(new ObjectValues()
                            {
                                SaleReceiptDetailID = refundItem.SaleReceiptDetailID,
                            });

                            requestModel.TotalAmount += refundItem.ExtendedPrice + refundItem.TaxAmount;
                            requestModel.TaxAmount += refundItem.TaxAmount;
                        }
                    }
                }

                DataTable dtSaleReceiptPaymentDetails = new DataTable("SaleReceiptPaymentDetails");
                string sql = "";
                using (SqlCommand sqlCommand = new SqlCommand())
                using (var conn = new SqlConnection(Helper.BLOGICPOS7_Connection))
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();

                    sqlCommand.Connection = conn;
                    sqlCommand.CommandType = CommandType.Text;

                    sql = $"select *, b.PaymentMethodName " +
                        $"from SaleReceiptPaymentDetails a left join PaymentMethods b on a.PaymentMethodID = b.PaymentMethodID " +
                        $"where SaleReceiptID = '{MainSaleReceiptID}'";

                    sqlCommand.CommandText = sql;
                    var reader = sqlCommand.ExecuteReader();
                    dtSaleReceiptPaymentDetails.Load(reader);
                    reader.Close();
                }

                if (dtSaleReceiptPaymentDetails?.Rows.Count == 1)
                    requestModel.SaleReceiptPaymentDetailID = Utility.GetStringFromRow("SaleReceiptPaymentDetailID", dtSaleReceiptPaymentDetails.Rows[0]);

                if (dtSaleReceiptPaymentDetails?.Rows.Count > 1)
                {
                    SelectSaleReceiptPaymentDetailForm form = new SelectSaleReceiptPaymentDetailForm();
                    form.BindingData(dtSaleReceiptPaymentDetails);
                    if (BLogicMessageBox.ShowPopUpFormWithDIMBackground(form, Helper.MainForm) != DialogResult.OK)
                    {
                        return;
                    }
                    requestModel.SaleReceiptPaymentDetailID = form.SaleReceiptPaymentDetailID;
                }

                if (requestModel.objectValues.Count == 0)
                    return;

                ConfirmDialogForm confirmFirmDialog = new ConfirmDialogForm(Const.ConfirmTitle, $"Are you sure you want to Refund ${requestModel.TotalAmount} of ticket #{MainSaleReceiptNumber}?");
                var result = confirmFirmDialog.ShowDialog();
                if (result == DialogResult.No)
                    return;

                ProcessForm processForm = new ProcessForm();
                processForm.CurrentProcessName = ProcessNameEnum.PartialRefundItems;
                processForm.requestModel = requestModel;
                processForm.ShowDialog();

                if (processForm.IsSuccess)
                {
                    DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    InformationDialogForm errorDialog = new InformationDialogForm("Add Tip Fail. Please contact TruongDS");
                    errorDialog.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                BLogicLogger.Exception("BtnProcessRefundPartialItem_Click", ex);
            }
        }

    }
}
