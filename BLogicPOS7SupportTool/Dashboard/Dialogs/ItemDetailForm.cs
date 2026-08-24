using BLogicPOS7SupportTool.Core;
using BLogicPOS7SupportTool.Models;
using BLogicPOS7SupportTool.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace BLogicPOS7SupportTool.Dashboard.Dialogs
{
    // Item Detail edit form: edits one ticket line's pricing, tax and the six
    // discount amounts, with live-computed Total Price / Total Discount / Final Total.
    partial class ItemDetailForm : Form
    {
        private readonly ReceiptService _service;
        private readonly string _receiptId;
        private readonly string _receiptNumber;
        private readonly RefundDetailItemEntity _item;
        private Point _dragStart;
        private bool _loading;

        // Reason lists cached per ReasonType so switching status does not re-query.
        private readonly Dictionary<int, List<ReasonCodeEntity>> _reasonCache = new Dictionary<int, List<ReasonCodeEntity>>();

        public ItemDetailForm(ReceiptService service, string receiptId, string receiptNumber, RefundDetailItemEntity item)
        {
            InitializeComponent();
            _service = service;
            _receiptId = receiptId;
            _receiptNumber = receiptNumber;
            _item = item;

            cmbStatus.Items.AddRange(new object[] { "Completed", "Cancel", "Refund", "COMP" });
            cmbStatus.SelectedIndex = 0;
            cmbStatus.SelectedIndexChanged += (s, e) => OnStatusChanged();

            pnlTitleBar.MouseDown += TitleBar_MouseDown;
            pnlTitleBar.MouseMove += TitleBar_MouseMove;
            lblFormTitle.MouseDown += TitleBar_MouseDown;
            lblFormTitle.MouseMove += TitleBar_MouseMove;

            btnCloseForm.Click += (s, e) => { DialogResult = DialogResult.Cancel; Close(); };
            btnSave.Click += (s, e) => Save();

            // Editable numeric fields: restrict input and recompute the totals live.
            foreach (var txt in new[] { txtPrice, txtQty, txtTax, txtItemDisc, txtTicketDisc,
                txtStoreDisc, txtCategoryDisc, txtCustomerDisc, txtCustomerLevelDisc })
            {
                AttachNumberValidation(txt);
                txt.TextChanged += (s, e) => { if (!_loading) Recompute(); };
            }
        }

        private void TitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) _dragStart = e.Location;
        }

        private void TitleBar_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                Location = new Point(Location.X + e.X - _dragStart.X, Location.Y + e.Y - _dragStart.Y);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            LoadItem();
        }

        private void LoadItem()
        {
            _loading = true;

            lblFormTitle.Text = _item.IsModifier ? "Item Detail (Modifier)" : "Item Detail";

            txtName.Text = _item.Description ?? "";
            txtPrice.Text = _item.Price.ToString("F2");
            txtQty.Text = _item.Quantity.ToString("0.##", CultureInfo.InvariantCulture);
            txtTax.Text = _item.TaxAmount.ToString("F2");

            txtItemDisc.Text = _item.ItemDiscountValue.ToString("F2");
            txtTicketDisc.Text = _item.TotalSaleDiscountAmount.ToString("F2");
            txtStoreDisc.Text = _item.StoreDiscountValue.ToString("F2");
            txtCategoryDisc.Text = _item.DiscountAmount.ToString("F2");
            txtCustomerDisc.Text = _item.TotalSaleDiscountAmountByCustomer.ToString("F2");
            txtCustomerLevelDisc.Text = _item.ItemDiscountByPriceLevelAmount.ToString("F2");

            cmbStatus.SelectedIndex = 0;

            _loading = false;
            Recompute();
        }

        // Shows/populates the Reason combobox based on the chosen status:
        //   Completed -> hidden (no reason)   Cancel/Comp -> ReasonType 3   Refund -> ReasonType 13
        private void OnStatusChanged()
        {
            int? reasonType = ReasonTypeFor((ItemEditStatus)cmbStatus.SelectedIndex);

            if (reasonType == null)
            {
                cmbReason.DataSource = null;
                lblReasonCap.Visible = false;
                cmbReason.Visible = false;
                return;
            }

            var list = GetReasons(reasonType.Value);
            cmbReason.DataSource = null;
            cmbReason.DisplayMember = "ReasonCodeName";
            cmbReason.ValueMember = "ReasonCodeID";
            cmbReason.DataSource = list;
            cmbReason.SelectedIndex = list.Count > 0 ? 0 : -1;
            lblReasonCap.Visible = true;
            cmbReason.Visible = true;
        }

        private static int? ReasonTypeFor(ItemEditStatus status)
        {
            switch (status)
            {
                case ItemEditStatus.Cancel: return 3;
                case ItemEditStatus.Comp: return 3;
                case ItemEditStatus.Refund: return 13;
                default: return null; // Completed
            }
        }

        private List<ReasonCodeEntity> GetReasons(int reasonType)
        {
            if (!_reasonCache.TryGetValue(reasonType, out var list))
            {
                list = _service.GetReasonCodes(reasonType);
                _reasonCache[reasonType] = list;
            }
            return list;
        }

        private void Recompute()
        {
            decimal price = ParseDec(txtPrice.Text);
            decimal qty = ParseDec(txtQty.Text);
            decimal tax = ParseDec(txtTax.Text);
            decimal gross = price * qty;
            decimal totalDisc = ParseDec(txtItemDisc.Text) + ParseDec(txtTicketDisc.Text)
                + ParseDec(txtStoreDisc.Text) + ParseDec(txtCategoryDisc.Text)
                + ParseDec(txtCustomerDisc.Text) + ParseDec(txtCustomerLevelDisc.Text);

            txtTotalPrice.Text = gross.ToString("F2");
            txtTotalDiscount.Text = totalDisc.ToString("F2");
            txtFinalTotal.Text = (gross + tax - totalDisc).ToString("F2");
        }

        private void Save()
        {
            string name = txtName.Text.Trim();
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Item name cannot be empty.", "Item Detail",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!TryDec(txtPrice.Text, out var price) || price < 0)
            {
                MessageBox.Show("Please enter a valid price.", "Item Detail",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!TryDec(txtQty.Text, out var qty) || qty <= 0)
            {
                MessageBox.Show("Please enter a valid quantity (greater than 0).", "Item Detail",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!TryDec(txtTax.Text, out var tax) || tax < 0)
            {
                MessageBox.Show("Please enter a valid tax amount.", "Item Detail",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal itemDisc, ticketDisc, storeDisc, categoryDisc, customerDisc, customerLevelDisc;
            if (!TryDec(txtItemDisc.Text, out itemDisc) || itemDisc < 0
                || !TryDec(txtTicketDisc.Text, out ticketDisc) || ticketDisc < 0
                || !TryDec(txtStoreDisc.Text, out storeDisc) || storeDisc < 0
                || !TryDec(txtCategoryDisc.Text, out categoryDisc) || categoryDisc < 0
                || !TryDec(txtCustomerDisc.Text, out customerDisc) || customerDisc < 0
                || !TryDec(txtCustomerLevelDisc.Text, out customerLevelDisc) || customerLevelDisc < 0)
            {
                MessageBox.Show("Please enter valid (non-negative) discount values.", "Item Detail",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var status = (ItemEditStatus)cmbStatus.SelectedIndex;
            int reasonId = 0;
            string reasonName = "";
            if (status != ItemEditStatus.Completed)
            {
                if (cmbReason.SelectedValue == null)
                {
                    MessageBox.Show("Please select a reason.", "Item Detail",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                reasonId = Convert.ToInt32(cmbReason.SelectedValue);
                reasonName = cmbReason.Text;
            }

            var confirm = MessageBox.Show(
                $"Save changes to \"{name}\" on ticket #{_receiptNumber}?\n\n" +
                $"    Final Total:  {txtFinalTotal.Text}\n" +
                $"    Status:       {cmbStatus.SelectedItem}\n" +
                (status != ItemEditStatus.Completed ? $"    Reason:       {reasonName}\n" : "") +
                "\nThe ticket totals will be recalculated. This action cannot be undone.",
                "Item Detail", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            var edit = new ItemEditDetailEntity
            {
                SaleReceiptDetailID = _item.SaleReceiptDetailID,
                Name = name,
                Price = price,
                Quantity = (double)qty,
                TaxAmount = tax,
                Status = status,
                ReasonCodeId = reasonId,
                ItemDiscount = itemDisc,
                TicketDiscount = ticketDisc,
                StoreDiscount = storeDisc,
                CategoryDiscount = categoryDisc,
                CustomerDiscount = customerDisc,
                CustomerLevelDiscount = customerLevelDisc
            };

            bool ok = _service.EditTicketItemDetail(_receiptId, edit);
            if (ok)
            {
                MessageBox.Show($"\"{name}\" has been updated and the ticket totals recalculated.",
                    "Item Detail", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                AppSession.CorrectDataSaleReceiptDetailTaxDetailsByDetailID(_item.SaleReceiptDetailID);
                Close();
            }
            else
            {
                MessageBox.Show("The item could not be updated. No changes were made.",
                    "Item Detail", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void AttachNumberValidation(TextBox txt)
        {
            txt.KeyPress += (s, e) =>
            {
                if (char.IsControl(e.KeyChar)) return;
                if (char.IsDigit(e.KeyChar)) return;
                if (e.KeyChar == '.' && !txt.Text.Contains(".")) return;
                e.Handled = true;
            };
        }

        private static decimal ParseDec(string s)
        {
            return decimal.TryParse(s, NumberStyles.Number, CultureInfo.InvariantCulture, out var v) ? v : 0m;
        }

        private static bool TryDec(string s, out decimal v)
        {
            return decimal.TryParse(s, NumberStyles.Number, CultureInfo.InvariantCulture, out v);
        }
    }
}
