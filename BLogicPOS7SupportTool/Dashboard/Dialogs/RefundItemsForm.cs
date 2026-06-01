using BLogicPOS7SupportTool.Core;
using BLogicPOS7SupportTool.Models;
using BLogicPOS7SupportTool.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace BLogicPOS7SupportTool.Dashboard.Dialogs
{
    partial class RefundItemsForm : Form
    {
        private readonly ReceiptService _service;
        private readonly string _receiptId;
        private readonly string _receiptNumber;
        private Point _dragStart;

        private static readonly Color DarkText = Color.FromArgb(50, 50, 50);
        private static readonly Color ModifierText = Color.FromArgb(140, 140, 140);
        private static readonly Color MutedText = Color.FromArgb(150, 150, 150);
        private static readonly Color SelectedBg = Color.FromArgb(253, 232, 232);
        private static readonly Color RefundRed = Color.FromArgb(200, 50, 50);

        private sealed class RowEntry
        {
            public RefundDetailItem Item;
            public Panel Panel;
        }

        private readonly List<RowEntry> _rows = new List<RowEntry>();
        private readonly HashSet<string> _selectedGroups = new HashSet<string>();

        public RefundItemsForm(ReceiptService service, string receiptId, string receiptNumber)
        {
            InitializeComponent();
            _service = service;
            _receiptId = receiptId;
            _receiptNumber = receiptNumber;

            pnlTitleBar.MouseDown += (s, e) => { if (e.Button == MouseButtons.Left) _dragStart = e.Location; };
            pnlTitleBar.MouseMove += (s, e) => { if (e.Button == MouseButtons.Left) Location = new Point(Location.X + e.X - _dragStart.X, Location.Y + e.Y - _dragStart.Y); };

            btnCloseForm.Click += (s, e) => { DialogResult = DialogResult.Cancel; Close(); };
            btnCancel.Click += (s, e) => { DialogResult = DialogResult.Cancel; Close(); };
            btnRefund.Click += BtnRefund_Click;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            LoadItems();
            RefreshTotals();
        }

        private void LoadItems()
        {
            var items = _service.GetRefundableItems(_receiptId);

            pnlContent.SuspendLayout();
            pnlContent.Controls.Clear();
            _rows.Clear();

            if (items.Count == 0)
            {
                pnlContent.Controls.Add(new Label
                {
                    Text = "There are no items available to refund on this ticket.",
                    Dock = DockStyle.Top,
                    Height = 40,
                    Font = new Font("Segoe UI", 9F),
                    ForeColor = MutedText,
                    TextAlign = ContentAlignment.MiddleCenter
                });
                pnlContent.ResumeLayout();
                return;
            }

            // Build in order, then add reversed so Dock=Top keeps the first line
            // (parent item) at the top with its modifiers directly underneath.
            var built = new List<Panel>();
            foreach (var item in items)
            {
                var entry = new RowEntry { Item = item };
                entry.Panel = BuildRow(entry);
                _rows.Add(entry);
                built.Add(entry.Panel);
            }

            for (int i = built.Count - 1; i >= 0; i--)
                pnlContent.Controls.Add(built[i]);

            pnlContent.ResumeLayout();
        }

        private Panel BuildRow(RowEntry entry)
        {
            var item = entry.Item;
            bool isModifier = item.IsModifier;

            var row = new Panel
            {
                Height = 26,
                Dock = DockStyle.Top,
                BackColor = Color.White,
                Cursor = Cursors.Hand,
                Tag = entry
            };

            var lblDesc = new Label
            {
                Text = item.Description,
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 9F, isModifier ? FontStyle.Regular : FontStyle.Bold),
                ForeColor = isModifier ? ModifierText : DarkText,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(isModifier ? 28 : 10, 0, 0, 0),
                Cursor = Cursors.Hand,
                AutoSize = false
            };

            var lblQty = new Label
            {
                Text = "$" + item.Price.ToString("F2") + " x " + item.Quantity.ToString("0.##"),
                Width = 90,
                Dock = DockStyle.Right,
                Font = new Font("Segoe UI", 9F),
                ForeColor = MutedText,
                TextAlign = ContentAlignment.MiddleRight,
                Cursor = Cursors.Hand,
                AutoSize = false
            };

            var lblTotal = new Label
            {
                Text = Money(item.ExtendedPrice + item.TaxAmount),
                Width = 80,
                Dock = DockStyle.Right,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = DarkText,
                TextAlign = ContentAlignment.MiddleRight,
                Padding = new Padding(0, 0, 8, 0),
                Cursor = Cursors.Hand,
                AutoSize = false
            };

            // Fill first, then right labels (last added = rightmost).
            row.Controls.Add(lblDesc);
            row.Controls.Add(lblQty);
            row.Controls.Add(lblTotal);

            EventHandler click = (s, e) => ToggleGroup(item.GroupKey);
            row.Click += click;
            lblDesc.Click += click;
            lblQty.Click += click;
            lblTotal.Click += click;

            return row;
        }

        private void ToggleGroup(string groupKey)
        {
            if (_selectedGroups.Contains(groupKey))
                _selectedGroups.Remove(groupKey);
            else
                _selectedGroups.Add(groupKey);

            RefreshSelectionVisuals();
            RefreshTotals();
        }

        private void RefreshSelectionVisuals()
        {
            foreach (var entry in _rows)
            {
                bool selected = _selectedGroups.Contains(entry.Item.GroupKey);
                var bg = selected ? SelectedBg : Color.White;
                entry.Panel.BackColor = bg;
                foreach (Control c in entry.Panel.Controls)
                    c.BackColor = bg;
            }
        }

        private void RefreshTotals()
        {
            decimal subtotal = 0m, tax = 0m;
            foreach (var entry in _rows)
            {
                if (!_selectedGroups.Contains(entry.Item.GroupKey)) continue;
                subtotal += entry.Item.ExtendedPrice;
                tax += entry.Item.TaxAmount;
            }

            lblSubtotal.Text = "Subtotal: " + Money(subtotal);
            lblTax.Text = "Tax: " + Money(tax);
            lblTotal.Text = "Total Refund: " + Money(subtotal + tax);
        }

        private void BtnRefund_Click(object sender, EventArgs e)
        {
            var selected = _rows
                .Where(r => _selectedGroups.Contains(r.Item.GroupKey))
                .ToList();

            if (selected.Count == 0)
            {
                MessageBox.Show("Please select at least one item to refund.", "Refund Items",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // At least one item must remain on the ticket — a full refund of every
            // item is not allowed (modifiers don't count, only parent items).
            int totalItems = _rows.Count(r => !r.Item.IsModifier);
            int refundedItems = _rows.Count(r => !r.Item.IsModifier && _selectedGroups.Contains(r.Item.GroupKey));
            if (totalItems > 0 && refundedItems >= totalItems)
            {
                MessageBox.Show("You cannot refund all items. At least one item must remain on the ticket.",
                    "Refund Items", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var detailIds = selected.Select(r => r.Item.SaleReceiptDetailID).ToList();
            decimal totalAmount = selected.Sum(r => r.Item.ExtendedPrice + r.Item.TaxAmount);
            decimal taxAmount = selected.Sum(r => r.Item.TaxAmount);

            var payments = _service.GetPaymentDetails(_receiptId);
            if (payments.Count == 0)
            {
                MessageBox.Show("This ticket has no payment available to refund.", "Refund Items",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ReceiptPaymentDetail payment;
            using (var selectForm = new SelectPaymentForm(payments))
            {
                if (DimOverlay.ShowDialog(this, selectForm) != DialogResult.OK)
                    return;
                payment = selectForm.SelectedPayment;
            }

            if (payment == null) return;

            var confirm = MessageBox.Show(
                $"Refund {Money(totalAmount)} for ticket #{_receiptNumber}?\n\nThis action cannot be undone.",
                "Confirm Refund", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            bool ok = _service.ProcessPartialRefund(
                _receiptId, payment.SaleReceiptPaymentDetailID, detailIds, totalAmount, taxAmount);

            if (ok)
            {
                MessageBox.Show($"Successfully refunded {Money(totalAmount)} for ticket #{_receiptNumber}.",
                    "Refund Items", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show("The refund could not be completed. No changes were made.", "Refund Items",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static string Money(decimal v)
        {
            return (v < 0 ? "-$" : "$") + Math.Abs(v).ToString("F2");
        }
    }
}
