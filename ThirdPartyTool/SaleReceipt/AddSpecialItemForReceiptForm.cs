using BLogicSystems.XPos.ClientUtilities;
using BLogicSystems.XPos.Utilites;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ThirdPartyTool.Dialog;
using ThirdPartyTool.Models;

namespace ThirdPartyTool.SaleReceipt
{
    /// <summary>
    /// Adds an Open Price or Negative Price item to a finished ticket. The line has no tax and the
    /// chosen payment moves by the same amount (see ProcessForm.AddSpecialItem).
    /// </summary>
    public partial class AddSpecialItemForReceiptForm : Form
    {
        public string SaleReceiptID = string.Empty;
        public string SaleReceiptNumber = string.Empty;

        private const int DeptOpenPrice = -2;
        private const int DeptNegativePrice = -3;

        private static readonly Color Primary = Color.FromArgb(42, 108, 228);
        private static readonly Color Danger = Color.FromArgb(220, 38, 38);
        private static readonly Color Muted = Color.FromArgb(97, 97, 97);
        private static readonly Color DisabledBack = Color.FromArgb(229, 231, 235);
        private static readonly Color DisabledText = Color.FromArgb(156, 163, 175);

        private decimal ticketTotal;
        private SpecialItemCard selected;

        public AddSpecialItemForReceiptForm()
        {
            InitializeComponent();

            btnClose.Click += BtnClose_Click;
            btnCancel.Click += BtnClose_Click;
            btnAdd.Click += BtnAdd_Click;
            this.Load += AddSpecialItemForReceiptForm_Load;

            txtAmount.KeyPress += TxtAmount_KeyPress;
            txtAmount.TextChanged += (s, e) => UpdateState();
            cboPayment.SelectedIndexChanged += (s, e) => UpdateState();
            pnlAmount.Click += (s, e) => txtAmount.Focus();
            lblCurrency.Click += (s, e) => txtAmount.Focus();
            txtAmount.Enter += (s, e) => pnlAmount.BackColor = Primary;
            txtAmount.Leave += (s, e) => pnlAmount.BackColor = Color.FromArgb(189, 189, 189);
            flowLayoutPanel1.Resize += (s, e) => LayoutCards();

            KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Escape) BtnClose_Click(s, e);
                else if (e.KeyCode == Keys.Enter) { e.SuppressKeyPress = true; BtnAdd_Click(s, e); }
            };
        }

        private void BtnClose_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void AddSpecialItemForReceiptForm_Load(object sender, System.EventArgs e)
        {
            try
            {
                DataTable items = new DataTable();
                DataTable payments = new DataTable();
                decimal total = 0;
                using (SqlConnection conn = new SqlConnection(Helper.BLOGICPOS7_Connection))
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    cmd.CommandText = "select ItemID, ItemName, DeptDeptID, RegularPrice from Items " +
                        "where DeptDeptID in (-2, -3) and ISNULL(IsDeleted, 0) = 0";
                    using (SqlDataReader reader = cmd.ExecuteReader()) items.Load(reader);

                    cmd.Parameters.AddWithValue("@id", SaleReceiptID);
                    cmd.CommandText = "select a.SaleReceiptPaymentDetailID, a.PaymentMethodID, a.AmountTendered, b.PaymentMethodName " +
                        "from SaleReceiptPaymentDetails a left join PaymentMethods b on a.PaymentMethodID = b.PaymentMethodID " +
                        "where a.SaleReceiptID = @id order by a.SaleReceiptPaymentDetailID";
                    using (SqlDataReader reader = cmd.ExecuteReader()) payments.Load(reader);

                    cmd.CommandText = "select TotalAmount from SaleReceipts where SaleReceiptID = @id";
                    total = Utility.GetDecimalFromObject(cmd.ExecuteScalar());
                }
                BindingData(items, payments, total);
            }
            catch (Exception ex)
            {
                BLogicLogger.Exception("AddSpecialItemForReceiptForm_Load", ex);
                lblEmpty.Text = "Could not load the special items.";
                lblEmpty.Visible = true;
                lblEmpty.BringToFront();
            }
        }

        /// <summary>Fills the item list (Open Price first, then Negative Price) and the payment picker.</summary>
        public void BindingData(DataTable items, DataTable payments, decimal ticketTotalAmount)
        {
            ticketTotal = ticketTotalAmount;
            if (!string.IsNullOrWhiteSpace(SaleReceiptNumber))
                lblSubtitle.Text = $"Ticket #{SaleReceiptNumber}  ·  Added without tax. The payment changes by the same amount.";

            flowLayoutPanel1.SuspendLayout();
            flowLayoutPanel1.Controls.Clear();
            foreach (var section in new[] { new { Dept = DeptOpenPrice, Title = "OPEN PRICE" }, new { Dept = DeptNegativePrice, Title = "NEGATIVE PRICE" } })
            {
                List<DataRow> rows = items.Rows.Cast<DataRow>()
                    .Where(r => Utility.GetIntFromRow("DeptDeptID", r) == section.Dept)
                    .OrderBy(r => Utility.GetStringFromRow("ItemName", r), NaturalComparer.Instance)
                    .ToList();
                if (rows.Count == 0)
                    continue;

                flowLayoutPanel1.Controls.Add(new Label
                {
                    Text = section.Title,
                    Font = new Font("Roboto", 8.5F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(117, 117, 117),
                    TextAlign = ContentAlignment.BottomLeft,
                    Height = 28,
                    Margin = new Padding(2, 4, 0, 4),
                });

                foreach (DataRow row in rows)
                {
                    SpecialItemCard card = new SpecialItemCard
                    {
                        ItemID = Utility.GetStringFromRow("ItemID", row),
                        ItemName = Utility.GetStringFromRow("ItemName", row),
                        IsNegative = section.Dept == DeptNegativePrice,
                        DefaultPrice = Math.Abs(Utility.GetDecimalFromRow("RegularPrice", row)),
                        Margin = new Padding(0, 0, 0, 8),
                    };
                    card.Click += (s, e) => SelectCard((SpecialItemCard)s);
                    flowLayoutPanel1.Controls.Add(card);
                }
            }
            flowLayoutPanel1.ResumeLayout();
            LayoutCards();

            bool empty = !flowLayoutPanel1.Controls.OfType<SpecialItemCard>().Any();
            lblEmpty.Visible = empty;
            if (empty) lblEmpty.BringToFront();

            cboPayment.Items.Clear();
            foreach (DataRow row in payments.Rows)
            {
                cboPayment.Items.Add(new PaymentOption
                {
                    Number = payments.Rows.Count > 1 ? cboPayment.Items.Count + 1 : 0,
                    SaleReceiptPaymentDetailID = Utility.GetStringFromRow("SaleReceiptPaymentDetailID", row),
                    Name = Utility.GetStringFromRow("PaymentMethodName", row),
                    PaymentMethodID = Utility.GetIntFromRow("PaymentMethodID", row),
                    AmountTendered = Utility.GetDecimalFromRow("AmountTendered", row),
                });
            }
            if (cboPayment.Items.Count > 0)
                cboPayment.SelectedIndex = 0;
            cboPayment.Enabled = cboPayment.Items.Count > 1;

            UpdateState();
        }

        private void LayoutCards()
        {
            int width = flowLayoutPanel1.ClientSize.Width - flowLayoutPanel1.Padding.Horizontal - 2;
            foreach (Control control in flowLayoutPanel1.Controls)
                control.Width = width;
        }

        private void SelectCard(SpecialItemCard card)
        {
            if (selected != null)
                selected.Selected = false;
            selected = card;
            card.Selected = true;

            lblCurrency.Text = card.IsNegative ? "−$" : "$";
            lblCurrency.ForeColor = card.IsNegative ? Danger : Muted;
            txtAmount.Text = card.DefaultPrice > 0 ? card.DefaultPrice.ToString("0.00", CultureInfo.InvariantCulture) : "";
            txtAmount.Focus();
            txtAmount.SelectAll();
            UpdateState();
        }

        private void TxtAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar))
                return;
            string next = txtAmount.Text.Remove(txtAmount.SelectionStart, txtAmount.SelectionLength).Insert(txtAmount.SelectionStart, e.KeyChar.ToString());
            bool ok = char.IsDigit(e.KeyChar) || (e.KeyChar == '.' && !txtAmount.Text.Remove(txtAmount.SelectionStart, txtAmount.SelectionLength).Contains("."));
            int dot = next.IndexOf('.');
            if (ok && dot >= 0 && next.Length - dot - 1 > 2)
                ok = false; // cents only
            e.Handled = !ok;
        }

        /// <summary>Entered amount, always positive (the sign comes from the item type).</summary>
        private decimal EnteredAmount
        {
            get
            {
                decimal value;
                return decimal.TryParse(txtAmount.Text, NumberStyles.Number, CultureInfo.InvariantCulture, out value) ? Math.Round(value, 2) : 0;
            }
        }

        private PaymentOption SelectedPayment => cboPayment.SelectedItem as PaymentOption;

        /// <summary>Line amount as stored: negative for Negative Price items.</summary>
        private decimal SignedAmount => selected == null ? 0 : (selected.IsNegative ? -EnteredAmount : EnteredAmount);

        /// <summary>Why the item cannot be added yet, or null when it can.</summary>
        private string ValidationMessage()
        {
            if (selected == null) return "Pick an item above.";
            if (EnteredAmount <= 0) return "Enter an amount greater than $0.00.";
            if (SelectedPayment == null) return "This ticket has no payment to change.";
            if (SelectedPayment.AmountTendered + SignedAmount < 0)
                return $"The {SelectedPayment.Label} payment is only {Money(SelectedPayment.AmountTendered)}.";
            return null;
        }

        private void UpdateState()
        {
            string problem = ValidationMessage();
            bool ready = problem == null;

            if (ready)
            {
                lblPreview.ForeColor = Muted;
                lblPreview.Text = $"Ticket total {Money(ticketTotal)} → {Money(ticketTotal + SignedAmount)}   ·   " +
                    $"{SelectedPayment.Label} {Money(SelectedPayment.AmountTendered)} → {Money(SelectedPayment.AmountTendered + SignedAmount)}";
            }
            else
            {
                bool isError = selected != null && EnteredAmount > 0;
                lblPreview.ForeColor = isError ? Danger : Muted;
                lblPreview.Text = problem;
            }

            Color back = ready ? (selected.IsNegative ? Danger : Primary) : DisabledBack;
            btnAdd.BackColor = btnAdd.BackgroundColor = btnAdd.BorderColor = back;
            btnAdd.ForeColor = btnAdd.TextColor = ready ? Color.White : DisabledText;
            btnAdd.Cursor = ready ? Cursors.Hand : Cursors.Default;
            btnAdd.Text = ready ? $"Add {Money(SignedAmount)}" : "Add item";
            btnAdd.Invalidate();
        }

        private static string Money(decimal value)
        {
            return value < 0 ? "−" + Utility.ConvertDecimalToMoneyString(-value) : Utility.ConvertDecimalToMoneyString(value);
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidationMessage() != null)
                    return;

                PaymentOption payment = SelectedPayment;
                string ticket = string.IsNullOrWhiteSpace(SaleReceiptNumber) ? "this ticket" : $"ticket #{SaleReceiptNumber}";
                ConfirmDialogForm confirm = new ConfirmDialogForm(Const.ConfirmTitle,
                    $"Add {selected.ItemName} ({Money(SignedAmount)}) to {ticket}? The {payment.Label} payment changes from " +
                    $"{Money(payment.AmountTendered)} to {Money(payment.AmountTendered + SignedAmount)}.");
                if (confirm.ShowDialog() != DialogResult.Yes)
                    return;

                ProcessForm processForm = new ProcessForm();
                processForm.CurrentProcessName = ProcessNameEnum.AddSpecialItem;
                processForm.requestModel.specialItemValues = new SpecialItemValues
                {
                    SaleReceiptID = SaleReceiptID,
                    SaleReceiptPaymentDetailID = payment.SaleReceiptPaymentDetailID,
                    ItemID = selected.ItemID,
                    ItemName = selected.ItemName,
                    Amount = SignedAmount,
                };
                processForm.ShowDialog();

                if (processForm.IsSuccess)
                {
                    DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    string reason = string.IsNullOrWhiteSpace(processForm.ErrorMessage) ? "" : Environment.NewLine + processForm.ErrorMessage;
                    new InformationDialogForm("Adding the special item failed. Please contact TruongDS." + reason).ShowDialog();
                }
            }
            catch (Exception ex)
            {
                BLogicLogger.Exception("AddSpecialItemForReceiptForm.BtnAdd_Click", ex);
            }
        }

        private sealed class PaymentOption
        {
            public int Number; // 1-based position when the ticket has several payments, else 0
            public string SaleReceiptPaymentDetailID;
            public string Name;
            public int PaymentMethodID;
            public decimal AmountTendered;
            public string Label => (string.IsNullOrWhiteSpace(Name) ? "Payment" : Name) + (Number > 0 ? " #" + Number : "");
            public override string ToString() =>
                $"{(Number > 0 ? Number + ". " : "")}{(string.IsNullOrWhiteSpace(Name) ? "Payment" : Name)}  ·  {Money(AmountTendered)}";
        }

        /// <summary>"Item 2" before "Item 10", like Windows Explorer.</summary>
        private sealed class NaturalComparer : IComparer<string>
        {
            public static readonly NaturalComparer Instance = new NaturalComparer();
            [DllImport("shlwapi.dll", CharSet = CharSet.Unicode)]
            private static extern int StrCmpLogicalW(string x, string y);
            public int Compare(string x, string y) => StrCmpLogicalW(x ?? "", y ?? "");
        }

        /// <summary>One selectable item: name on the left, default price (or "Open price") on the right.</summary>
        private sealed class SpecialItemCard : Control
        {
            public string ItemID;
            public string ItemName;
            public bool IsNegative;
            public decimal DefaultPrice;

            private bool selectedState;
            private bool hover;

            public bool Selected
            {
                get { return selectedState; }
                set { selectedState = value; Invalidate(); }
            }

            public SpecialItemCard()
            {
                Height = 46;
                Cursor = Cursors.Hand;
                SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.ResizeRedraw, true);
                BackColor = Color.White;
            }

            protected override void OnMouseEnter(EventArgs e) { hover = true; Invalidate(); base.OnMouseEnter(e); }
            protected override void OnMouseLeave(EventArgs e) { hover = false; Invalidate(); base.OnMouseLeave(e); }

            protected override void OnPaint(PaintEventArgs e)
            {
                Graphics g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.Clear(Parent?.BackColor ?? Color.White);

                Rectangle r = new Rectangle(1, 1, Width - 3, Height - 3);
                Color fill = selectedState ? Color.FromArgb(247, 250, 255) : hover ? Color.FromArgb(249, 250, 251) : Color.White;
                Color border = selectedState ? Primary : hover ? Color.FromArgb(189, 189, 189) : Color.FromArgb(224, 224, 224);
                using (GraphicsPath path = Rounded(r, 6))
                {
                    using (SolidBrush b = new SolidBrush(fill)) g.FillPath(b, path);
                    using (Pen p = new Pen(border, selectedState ? 2f : 1f)) g.DrawPath(p, path);
                }

                // radio
                Rectangle ring = new Rectangle(14, (Height - 18) / 2, 18, 18);
                using (Pen p = new Pen(selectedState ? Primary : Color.FromArgb(189, 189, 189), 2f)) g.DrawEllipse(p, ring);
                if (selectedState)
                    using (SolidBrush b = new SolidBrush(Primary)) g.FillEllipse(b, ring.X + 5, ring.Y + 5, 8, 8);

                string right = DefaultPrice > 0 ? (IsNegative ? "−" : "") + Utility.ConvertDecimalToMoneyString(DefaultPrice) : (IsNegative ? "Enter amount" : "Open price");
                using (Font nameFont = new Font("Roboto", 11F, FontStyle.Bold))
                using (Font priceFont = new Font("Roboto", 10F, DefaultPrice > 0 ? FontStyle.Bold : FontStyle.Regular))
                {
                    Size priceSize = TextRenderer.MeasureText(g, right, priceFont);
                    Rectangle priceRect = new Rectangle(Width - priceSize.Width - 16, 0, priceSize.Width + 4, Height);
                    Color priceColor = DefaultPrice > 0 ? (IsNegative ? Danger : Color.FromArgb(33, 33, 33)) : Color.FromArgb(117, 117, 117);
                    TextRenderer.DrawText(g, right, priceFont, priceRect, priceColor, TextFormatFlags.VerticalCenter | TextFormatFlags.Right);

                    Rectangle nameRect = new Rectangle(44, 0, priceRect.Left - 52, Height);
                    TextRenderer.DrawText(g, ItemName, nameFont, nameRect, Color.FromArgb(33, 33, 33),
                        TextFormatFlags.VerticalCenter | TextFormatFlags.Left | TextFormatFlags.EndEllipsis | TextFormatFlags.NoPrefix);
                }
            }

            private static GraphicsPath Rounded(Rectangle r, int radius)
            {
                int d = radius * 2;
                GraphicsPath path = new GraphicsPath();
                path.AddArc(r.X, r.Y, d, d, 180, 90);
                path.AddArc(r.Right - d, r.Y, d, d, 270, 90);
                path.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90);
                path.AddArc(r.X, r.Bottom - d, d, d, 90, 90);
                path.CloseFigure();
                return path;
            }
        }
    }
}
