using BLogicPOS7SupportTool.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace BLogicPOS7SupportTool.Dashboard.Dialogs
{
    partial class SelectPaymentForm : Form
    {
        private readonly List<ReceiptPaymentDetail> _payments;
        private Point _dragStart;

        private static readonly Color HeaderBlue = Color.FromArgb(11, 87, 208);
        private static readonly Color SelectedBg = Color.FromArgb(225, 236, 255);
        private static readonly Color DarkText = Color.FromArgb(50, 50, 50);
        private static readonly Color LabelGray = Color.FromArgb(120, 120, 120);

        private readonly List<Panel> _cards = new List<Panel>();

        public ReceiptPaymentDetail SelectedPayment { get; private set; }

        public SelectPaymentForm(List<ReceiptPaymentDetail> payments)
        {
            InitializeComponent();
            _payments = payments ?? new List<ReceiptPaymentDetail>();

            pnlTitleBar.MouseDown += (s, e) => { if (e.Button == MouseButtons.Left) _dragStart = e.Location; };
            pnlTitleBar.MouseMove += (s, e) => { if (e.Button == MouseButtons.Left) Location = new Point(Location.X + e.X - _dragStart.X, Location.Y + e.Y - _dragStart.Y); };

            btnCloseForm.Click += (s, e) => { DialogResult = DialogResult.Cancel; Close(); };
            btnCancel.Click += (s, e) => { DialogResult = DialogResult.Cancel; Close(); };
            btnConfirm.Click += BtnConfirm_Click;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            BuildCards();
        }

        private void BuildCards()
        {
            pnlContent.SuspendLayout();
            pnlContent.Controls.Clear();
            _cards.Clear();

            // Build top-to-bottom: add in reverse so Dock=Top keeps the first
            // payment at the top.
            var built = new List<Panel>();
            foreach (var p in _payments)
                built.Add(BuildCard(p));

            for (int i = built.Count - 1; i >= 0; i--)
                pnlContent.Controls.Add(built[i]);

            pnlContent.ResumeLayout();

            // Pre-select when there is only one payment.
            if (_payments.Count == 1)
                Select(_payments[0]);
        }

        private Panel BuildCard(ReceiptPaymentDetail detail)
        {
            decimal amount = detail.AmountTendered - detail.AmountReturn
                + detail.SVCFeeAmount - detail.CashDiscountAmount;
            decimal tip = detail.Gratuity;

            var card = new Panel
            {
                Dock = DockStyle.Top,
                Height = 62,
                Margin = new Padding(0, 0, 0, 10),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Cursor = Cursors.Hand,
                Tag = detail
            };
            // Spacing between cards (BorderStyle panels ignore Margin under Dock).
            card.Padding = new Padding(0);

            var lblName = new Label
            {
                Text = detail.PaymentMethodName,
                Dock = DockStyle.Top,
                Height = 30,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = DarkText,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(12, 0, 0, 0),
                Cursor = Cursors.Hand
            };

            var lblAmount = new Label
            {
                Text = "Total: " + Money(amount) + (tip > 0 ? "    Tip: " + Money(tip) : ""),
                Dock = DockStyle.Top,
                Height = 28,
                Font = new Font("Segoe UI", 9F),
                ForeColor = LabelGray,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(12, 0, 0, 0),
                Cursor = Cursors.Hand
            };

            card.Controls.Add(lblAmount);
            card.Controls.Add(lblName);

            EventHandler click = (s, e) => Select(detail);
            card.Click += click;
            lblName.Click += click;
            lblAmount.Click += click;

            // Wrapper adds the vertical gap between cards.
            var wrapper = new Panel
            {
                Dock = DockStyle.Top,
                Height = card.Height + 10,
                BackColor = Color.Transparent,
                Padding = new Padding(0, 0, 0, 10)
            };
            wrapper.Controls.Add(card);
            _cards.Add(card);
            return wrapper;
        }

        private void Select(ReceiptPaymentDetail detail)
        {
            SelectedPayment = detail;
            foreach (var card in _cards)
            {
                var bg = ReferenceEquals(card.Tag, detail) ? SelectedBg : Color.White;
                card.BackColor = bg;
                foreach (Control c in card.Controls)
                    c.BackColor = bg;
            }
        }

        private void BtnConfirm_Click(object sender, EventArgs e)
        {
            if (SelectedPayment == null)
            {
                MessageBox.Show("Please select a payment to refund.", "Select Payment",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        private static string Money(decimal v)
        {
            return (v < 0 ? "-$" : "$") + Math.Abs(v).ToString("F2");
        }
    }
}
