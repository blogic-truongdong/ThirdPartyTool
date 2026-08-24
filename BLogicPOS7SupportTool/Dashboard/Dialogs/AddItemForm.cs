using BLogicPOS7SupportTool.Models;
using BLogicPOS7SupportTool.Services;
using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace BLogicPOS7SupportTool.Dashboard.Dialogs
{
    // Add Item: pick an item (department -2), tweak name/price/quantity, then
    // insert it as a new SaleReceiptDetails line on the ticket.
    partial class AddItemForm : Form
    {
        private readonly ReceiptService _service;
        private readonly string _receiptId;
        private readonly string _receiptNumber;
        private Point _dragStart;

        private AddableItemEntity _selected;

        public AddItemForm(ReceiptService service, string receiptId, string receiptNumber)
        {
            InitializeComponent();
            _service = service;
            _receiptId = receiptId;
            _receiptNumber = receiptNumber;

            pnlTitleBar.MouseDown += (s, e) => { if (e.Button == MouseButtons.Left) _dragStart = e.Location; };
            pnlTitleBar.MouseMove += (s, e) => { if (e.Button == MouseButtons.Left) Location = new Point(Location.X + e.X - _dragStart.X, Location.Y + e.Y - _dragStart.Y); };

            btnCloseForm.Click += (s, e) => { DialogResult = DialogResult.Cancel; Close(); };
            btnCancel.Click += (s, e) => { DialogResult = DialogResult.Cancel; Close(); };
            btnAdd.Click += (s, e) => AddItem();
            lvItems.SelectedIndexChanged += (s, e) => BindSelection();

            AttachNumberValidation(txtPrice);
            AttachNumberValidation(txtQty);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            LoadItems();
        }

        private void LoadItems()
        {
            var items = _service.GetAddableItems();

            lvItems.BeginUpdate();
            lvItems.Items.Clear();
            foreach (var item in items)
            {
                var lvi = new ListViewItem(item.ItemName ?? "");
                lvi.SubItems.Add(item.LookupCode ?? "");
                lvi.SubItems.Add(Money(item.RegularPrice));
                lvi.Tag = item;
                lvItems.Items.Add(lvi);
            }
            lvItems.EndUpdate();
        }

        private void BindSelection()
        {
            if (lvItems.SelectedItems.Count == 0)
            {
                _selected = null;
                btnAdd.Enabled = false;
                return;
            }

            _selected = lvItems.SelectedItems[0].Tag as AddableItemEntity;
            if (_selected == null) return;

            txtName.Text = _selected.ItemName ?? "";
            txtPrice.Text = _selected.RegularPrice.ToString("F2");
            txtQty.Text = "1";
            btnAdd.Enabled = true;
        }

        private void AddItem()
        {
            if (_selected == null) return;

            string name = txtName.Text.Trim();
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Item name cannot be empty.", "Add Item",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!TryDec(txtPrice.Text, out var price) || price < 0)
            {
                MessageBox.Show("Please enter a valid price.", "Add Item",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!TryDec(txtQty.Text, out var qty) || qty <= 0)
            {
                MessageBox.Show("Please enter a valid quantity (greater than 0).", "Add Item",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show(
                $"Add \"{name}\" ({Money(price)} x {qty.ToString("0.##", CultureInfo.InvariantCulture)}) to ticket #{_receiptNumber}?\n\n" +
                "The ticket totals will be recalculated.",
                "Add Item", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            bool ok = _service.AddTicketItem(_receiptId, _selected.ItemID, name, price,
                (double)qty, _selected.LookupCode, _selected.TaxCodeID, _selected.Taxable);
            if (ok)
            {
                MessageBox.Show($"\"{name}\" has been added to the ticket.",
                    "Add Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show("The item could not be added. No changes were made.",
                    "Add Item", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private static bool TryDec(string s, out decimal v)
        {
            return decimal.TryParse(s, NumberStyles.Number, CultureInfo.InvariantCulture, out v);
        }

        private static string Money(decimal v)
        {
            return (v < 0 ? "-$" : "$") + Math.Abs(v).ToString("F2");
        }
    }
}
