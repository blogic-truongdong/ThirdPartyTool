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
            btnCancel.Click += BtnClose_Click;

            chkAll.Click += ChkAll_Click;
            pnlRefundItemContainer.Layout += (s, e) => AlignColumnHeaders();
        }

        private static readonly Color RefundRed = Color.FromArgb(220, 38, 38);
        private static readonly Color DisabledBack = Color.FromArgb(229, 231, 235);
        private static readonly Color DisabledText = Color.FromArgb(156, 163, 175);

        /// <summary>Header checkbox: select every item (modifiers follow their item).</summary>
        private void ChkAll_Click(object sender, EventArgs e)
        {
            bool target = chkAll.Checked;
            foreach (System.Windows.Forms.Control item in pnlRefundItemContainer.Controls)
            {
                RefundItemRow refundItem = item as RefundItemRow;
                if (refundItem != null && refundItem.IsSelectedItem != target)
                    refundItem.ToggleSelection();
            }
            UpdateSummary();
        }

        /// <summary>Keeps the Qty/Amount headers above their columns when the list shows a scrollbar.</summary>
        private void AlignColumnHeaders()
        {
            int scroll = pnlRefundItemContainer.VerticalScroll.Visible ? SystemInformation.VerticalScrollBarWidth : 0;
            label3.Left = pnlColumns.Width - scroll - label3.Width;
            label2.Left = label3.Left - label2.Width;
        }

        private void UpdateSummary()
        {
            decimal totalAmount = 0;
            decimal amount = 0;
            decimal tax = 0;
            int items = 0;
            int selectedItems = 0;

            foreach (System.Windows.Forms.Control item in pnlRefundItemContainer.Controls)
            {
                RefundItemRow refundItem = item as RefundItemRow;
                RefundModiferRow refundModifier = item as RefundModiferRow;

                if (refundItem != null)
                {
                    items++;
                    if (refundItem.IsSelectedItem)
                    {
                        selectedItems++;
                        totalAmount += refundItem.RefundExtendedPrice + refundItem.RefundTaxAmount;
                        amount += refundItem.RefundExtendedPrice;
                        tax += refundItem.RefundTaxAmount;
                    }
                }
                else if (refundModifier != null && refundModifier.IsSelectedItem)
                {
                    totalAmount += refundModifier.RefundExtendedPrice + refundModifier.RefundTaxAmount;
                    amount += refundModifier.RefundExtendedPrice;
                    tax += refundModifier.RefundTaxAmount;
                }
            }

            lblTotalRefundItem_Amount.Text = $"Subtotal {Utility.ConvertDecimalToMoneyString(amount)}";
            lblTotalRefundItem_Tax.Text = $"Tax {Utility.ConvertDecimalToMoneyString(tax)}";
            lblTotalRefundItem_TotalAmount.Text = Utility.ConvertDecimalToMoneyString(totalAmount);

            chkAll.Checked = items > 0 && selectedItems == items;
            chkAll.Enabled = items > 0;

            bool hasSelection = HasSelection();
            Color back = hasSelection ? RefundRed : DisabledBack;
            btnProcessRefundPartialItem.BackColor = btnProcessRefundPartialItem.BackgroundColor = btnProcessRefundPartialItem.BorderColor = back;
            btnProcessRefundPartialItem.ForeColor = btnProcessRefundPartialItem.TextColor = hasSelection ? Color.White : DisabledText;
            btnProcessRefundPartialItem.Cursor = hasSelection ? Cursors.Hand : Cursors.Default;
            btnProcessRefundPartialItem.Text = hasSelection ? $"Refund {Utility.ConvertDecimalToMoneyString(totalAmount)}" : "Refund";
            btnProcessRefundPartialItem.Invalidate();
        }

        private bool HasSelection()
        {
            foreach (System.Windows.Forms.Control item in pnlRefundItemContainer.Controls)
            {
                if ((item as RefundItemRow)?.IsSelectedItem == true || (item as RefundModiferRow)?.IsSelectedItem == true)
                    return true;
            }
            return false;
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
                if (!string.IsNullOrWhiteSpace(MainSaleReceiptNumber))
                    lblSubtitle.Text = $"Ticket #{MainSaleReceiptNumber}  ·  Modifiers follow their item  ·  Use − / + to refund only part of a quantity.";

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
                        refundItem.RefundQuantityChanged += (s, e) => SyncGroup(((RefundItemRow)s).GroupID, true);
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

                if (pnlRefundItemContainer.Controls.Count == 0)
                {
                    pnlRefundItemContainer.Controls.Add(new Label
                    {
                        Dock = DockStyle.Fill,
                        Text = "No items left to refund on this ticket.",
                        TextAlign = ContentAlignment.MiddleCenter,
                        Font = new Font("Roboto", 11F),
                        ForeColor = Color.FromArgb(117, 117, 117),
                    });
                }
            }
            catch (Exception ex)
            {
                BLogicLogger.Exception("BindingPartialRefundItems", ex);
            }
            UpdateSummary();
            AlignColumnHeaders();
        }

        private void RefundItem_SelectedItemChanges(object sender, DataEventArgs<RequestModel> e)
        {
            try
            {
                SyncGroup(e.Data.GroupID, e.Data.IsSelectedItem);
            }
            catch (Exception ex)
            {
                BLogicLogger.Exception("RefundItem_SelectedItemChanges", ex);
            }
        }

        /// <summary>
        /// Applies the selection to every row of the item group and passes the item's refund share
        /// (e.g. 2 of 3) to its modifiers, then refreshes the totals.
        /// </summary>
        private void SyncGroup(string groupID, bool selected)
        {
            RefundItemRow owner = null;
            foreach (System.Windows.Forms.Control item in pnlRefundItemContainer.Controls)
            {
                RefundItemRow refundItem = item as RefundItemRow;
                if (refundItem != null && refundItem.GroupID == groupID)
                {
                    if (refundItem.IsSelectedItem != selected)
                        refundItem.SetSelected(selected);
                    if (owner == null)
                        owner = refundItem;
                }
            }

            decimal part = owner != null && selected ? owner.RefundQuantity : 1;
            decimal whole = owner != null && selected ? owner.Quantity : 1;
            if (whole <= 0) { part = whole = 1; }

            foreach (System.Windows.Forms.Control item in pnlRefundItemContainer.Controls)
            {
                RefundModiferRow refundModifier = item as RefundModiferRow;
                if (refundModifier != null && refundModifier.GroupID == groupID)
                    refundModifier.SetSelected(selected, part, whole);
            }

            UpdateSummary();
        }

        private void BtnProcessRefundPartialItem_Click(object sender, EventArgs e)
        {
            try
            {
                // Nothing picked: don't query payments or ask which payment to refund.
                if (!HasSelection())
                    return;

                RequestModel requestModel = new RequestModel();
                requestModel.SaleReceiptID = MainSaleReceiptID;
                requestModel.SaleReceiptPaymentDetailID = MainSaleReceiptPaymentDetailID;

                foreach (System.Windows.Forms.Control item in pnlRefundItemContainer.Controls)
                {
                    RefundModiferRow refundModifier = item as RefundModiferRow;

                    if (refundModifier != null && refundModifier.IsSelectedItem)
                    {
                        requestModel.objectValues.Add(new ObjectValues()
                        {
                            SaleReceiptDetailID = refundModifier.SaleReceiptDetailID,
                            Quantity = refundModifier.Quantity,
                            ExtendedPrice = refundModifier.ExtendedPrice,
                            RefundQuantity = refundModifier.RefundQuantity,
                            RefundExtendedPrice = refundModifier.RefundExtendedPrice,
                            RefundTaxAmount = refundModifier.RefundTaxAmount,
                            RefundPart = refundModifier.PortionPart,
                            RefundWhole = refundModifier.PortionWhole,
                        });

                        requestModel.TotalAmount += refundModifier.RefundExtendedPrice + refundModifier.RefundTaxAmount;
                        requestModel.TaxAmount += refundModifier.RefundTaxAmount;
                    }

                    RefundItemRow refundItem = item as RefundItemRow;

                    if (refundItem != null && refundItem.IsSelectedItem)
                    {
                        requestModel.objectValues.Add(new ObjectValues()
                        {
                            SaleReceiptDetailID = refundItem.SaleReceiptDetailID,
                            Quantity = refundItem.Quantity,
                            ExtendedPrice = refundItem.ExtendedPrice,
                            RefundQuantity = refundItem.RefundQuantity,
                            RefundExtendedPrice = refundItem.RefundExtendedPrice,
                            RefundTaxAmount = refundItem.RefundTaxAmount,
                            RefundPart = refundItem.RefundQuantity,
                            RefundWhole = refundItem.Quantity,
                        });

                        requestModel.TotalAmount += refundItem.RefundExtendedPrice + refundItem.RefundTaxAmount;
                        requestModel.TaxAmount += refundItem.RefundTaxAmount;
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

                ConfirmDialogForm confirmFirmDialog = new ConfirmDialogForm(Const.ConfirmTitle, $"Are you sure you want to refund {Utility.ConvertDecimalToMoneyString(requestModel.TotalAmount)} on ticket #{MainSaleReceiptNumber}?");
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
                    string reason = string.IsNullOrWhiteSpace(processForm.ErrorMessage) ? "Please contact TruongDS." : processForm.ErrorMessage;
                    InformationDialogForm errorDialog = new InformationDialogForm("The refund was not done. " + reason);
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
