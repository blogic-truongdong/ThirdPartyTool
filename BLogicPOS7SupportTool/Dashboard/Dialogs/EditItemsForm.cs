using BLogicPOS7SupportTool.Core;
using BLogicPOS7SupportTool.Models;
using BLogicPOS7SupportTool.Services;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace BLogicPOS7SupportTool.Dashboard.Dialogs
{
    partial class EditItemsForm : Form
    {
        private readonly ReceiptService _service;
        private readonly string _receiptId;
        private readonly string _receiptNumber;
        private Point _dragStart;
        private bool _changed;

        private static readonly Color ModifierGray = Color.FromArgb(120, 120, 120);
        private static readonly Color TextDark = Color.FromArgb(50, 50, 50);
        private static readonly Color DiscountColor = Color.FromArgb(210, 120, 0);
        private static readonly Color PencilBlue = Color.FromArgb(11, 87, 208);

        // Index of the pencil (edit) column in lvItems.
        private const int EditColumnIndex = 6;

        public EditItemsForm(ReceiptService service, string receiptId, string receiptNumber)
        {
            InitializeComponent();
            _service = service;
            _receiptId = receiptId;
            _receiptNumber = receiptNumber;

            lblFormTitle.Text = string.IsNullOrEmpty(receiptNumber)
                ? "Edit Items" : "Edit Items  —  Ticket #" + receiptNumber;

            pnlTitleBar.MouseDown += (s, e) => { if (e.Button == MouseButtons.Left) _dragStart = e.Location; };
            pnlTitleBar.MouseMove += (s, e) => { if (e.Button == MouseButtons.Left) Location = new Point(Location.X + e.X - _dragStart.X, Location.Y + e.Y - _dragStart.Y); };

            btnCloseForm.Click += (s, e) => CloseWithResult();
            btnDone.Click += (s, e) => CloseWithResult();

            lvItems.MouseClick += LvItems_MouseClick;
            lvItems.DoubleClick += LvItems_DoubleClick;
        }

        private void CloseWithResult()
        {
            DialogResult = _changed ? DialogResult.OK : DialogResult.Cancel;
            Close();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            LoadItems();
        }

        private void LoadItems()
        {
            var items = _service.GetEditableItems(_receiptId);

            lvItems.BeginUpdate();
            lvItems.Items.Clear();
            foreach (var item in items)
            {
                decimal total = item.ExtendedPrice + item.TaxAmount;

                var lvi = new ListViewItem(item.Description ?? "");
                lvi.UseItemStyleForSubItems = false;
                lvi.SubItems.Add(Money(item.Price));                       // PRICE
                lvi.SubItems.Add(item.Quantity.ToString("0.##"));          // QTY
                lvi.SubItems.Add(Money(item.TaxAmount));                   // TAX
                var discSub = lvi.SubItems.Add(Money(item.TotalDiscount)); // DISCOUNT
                var totalSub = lvi.SubItems.Add(Money(total));             // TOTAL
                var editSub = lvi.SubItems.Add("✎");                  // pencil ✎

                // Discount stands out when non-zero.
                discSub.ForeColor = item.TotalDiscount > 0 ? DiscountColor : ModifierGray;
                totalSub.Font = new Font(lvItems.Font, FontStyle.Bold);
                editSub.ForeColor = PencilBlue;
                editSub.Font = new Font(lvItems.Font, FontStyle.Bold);

                lvi.ForeColor = item.IsModifier ? ModifierGray : TextDark;
                if (!item.IsModifier)
                    lvi.Font = new Font(lvItems.Font, FontStyle.Bold);
                lvi.Tag = item;
                lvItems.Items.Add(lvi);
            }
            lvItems.EndUpdate();
        }

        private void LvItems_MouseClick(object sender, MouseEventArgs e)
        {
            var hit = lvItems.HitTest(e.Location);
            if (hit.Item == null || hit.SubItem == null) return;

            int col = hit.Item.SubItems.IndexOf(hit.SubItem);
            if (col == EditColumnIndex)
                OpenDetail(hit.Item.Tag as RefundDetailItem);
        }

        private void LvItems_DoubleClick(object sender, EventArgs e)
        {
            if (lvItems.SelectedItems.Count > 0)
                OpenDetail(lvItems.SelectedItems[0].Tag as RefundDetailItem);
        }

        private void OpenDetail(RefundDetailItem item)
        {
            if (item == null) return;

            using (var dlg = new ItemDetailForm(_service, _receiptId, _receiptNumber, item))
            {
                if (DimOverlay.ShowDialog(this, dlg) == DialogResult.OK)
                {
                    _changed = true;
                    LoadItems();
                }
            }
        }

        private static string Money(decimal v)
        {
            return (v < 0 ? "-$" : "$") + Math.Abs(v).ToString("F2");
        }
    }
}
