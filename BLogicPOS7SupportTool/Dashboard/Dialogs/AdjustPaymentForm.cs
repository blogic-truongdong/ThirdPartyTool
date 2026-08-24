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
    partial class AdjustPaymentForm : Form
    {
        private readonly ReceiptService _service;
        private readonly string _receiptId;
        private Point _dragStart;
        private bool _changed;

        private static readonly Color HeaderBlue = Color.FromArgb(11, 87, 208);
        private static readonly Color DarkText = Color.FromArgb(40, 40, 40);
        private static readonly Color AmountText = Color.FromArgb(80, 80, 90);
        private static readonly Color MutedText = Color.FromArgb(130, 130, 138);
        private static readonly Color PencilGray = Color.FromArgb(150, 150, 155);
        private static readonly Color RowDivider = Color.FromArgb(235, 235, 238);

        private const int RowHeight = 56;
        private const int LabelY = 17;

        // Column layout (x, width) shared by the header and data rows.
        private const int ColMethodX = 24, ColMethodW = 200;
        private const int ColCardX = 240, ColCardW = 276;
        private const int ColAmountX = 520, ColAmountW = 110;
        private const int ColGratuityX = 645, ColGratuityW = 105;
        private const int ColTotalX = 760, ColTotalW = 120;
        private const int ColActionX = 900, ColActionW = 80;

        public AdjustPaymentForm(ReceiptService service, string receiptId)
        {
            InitializeComponent();
            _service = service;
            _receiptId = receiptId;

            pnlTitleBar.MouseDown += (s, e) => { if (e.Button == MouseButtons.Left) _dragStart = e.Location; };
            pnlTitleBar.MouseMove += (s, e) => { if (e.Button == MouseButtons.Left) Location = new Point(Location.X + e.X - _dragStart.X, Location.Y + e.Y - _dragStart.Y); };
            lblFormTitle.MouseDown += (s, e) => { if (e.Button == MouseButtons.Left) _dragStart = new Point(e.X + lblFormTitle.Left, e.Y); };

            btnCloseForm.Click += (s, e) => CloseWithResult();
        }

        private void CloseWithResult()
        {
            DialogResult = _changed ? DialogResult.OK : DialogResult.Cancel;
            Close();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            BuildColumnHeader();
            LoadPayments();
        }

        private void BuildColumnHeader()
        {
            pnlColumnHeader.Controls.Clear();
            pnlColumnHeader.Controls.Add(HeaderLabel("PAYMENT METHOD", ColMethodX, ColMethodW, ContentAlignment.MiddleLeft));
            pnlColumnHeader.Controls.Add(HeaderLabel("CARD INFO", ColCardX, ColCardW, ContentAlignment.MiddleLeft));
            pnlColumnHeader.Controls.Add(HeaderLabel("AMOUNT", ColAmountX, ColAmountW, ContentAlignment.MiddleRight));
            pnlColumnHeader.Controls.Add(HeaderLabel("GRATUITY", ColGratuityX, ColGratuityW, ContentAlignment.MiddleRight));
            pnlColumnHeader.Controls.Add(HeaderLabel("TOTAL", ColTotalX, ColTotalW, ContentAlignment.MiddleRight));
            pnlColumnHeader.Controls.Add(HeaderLabel("ACTION", ColActionX, ColActionW, ContentAlignment.MiddleCenter));
        }

        private static Label HeaderLabel(string text, int x, int w, ContentAlignment align)
        {
            return new Label
            {
                Text = text,
                Location = new Point(x, 0),
                Size = new Size(w, 40),
                Font = new Font("Segoe UI", 8.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(90, 90, 110),
                TextAlign = align,
                AutoSize = false
            };
        }

        private void LoadPayments()
        {
            var payments = _service.GetPaymentDetails(_receiptId);

            pnlContent.SuspendLayout();
            pnlContent.Controls.Clear();

            if (payments.Count == 0)
            {
                pnlContent.Controls.Add(new Label
                {
                    Text = "This ticket has no payments.",
                    Dock = DockStyle.Top,
                    Height = 60,
                    Font = new Font("Segoe UI", 10F),
                    ForeColor = MutedText,
                    TextAlign = ContentAlignment.MiddleCenter
                });
                pnlContent.ResumeLayout();
                return;
            }

            var rows = new List<Panel>();
            foreach (var p in payments)
            {
                p.SVCFeeAmount -= p.SVCFeeTaxAmount;
                p.CashDiscountAmount -= p.CashDiscountTaxAmount;

                rows.Add(BuildRow(p));
            }

            // Added first so Dock=Top pushes it below all payment rows.
            pnlContent.Controls.Add(BuildSummaryRow(payments));

            for (int i = rows.Count - 1; i >= 0; i--)
                pnlContent.Controls.Add(rows[i]);

            pnlContent.ResumeLayout();
        }

        private Panel BuildSummaryRow(List<ReceiptPaymentDetailEntity> payments)
        {
            decimal amount = payments.Sum(p => p.NetAmount);
            decimal gratuity = payments.Sum(p => p.Gratuity);
            decimal total = payments.Sum(p => p.Total);

            var row = new Panel { Dock = DockStyle.Top, Height = RowHeight, BackColor = Color.FromArgb(247, 248, 252) };
            row.Controls.Add(new Panel { Dock = DockStyle.Top, Height = 2, BackColor = Color.FromArgb(210, 214, 230) });

            row.Controls.Add(new Label
            {
                Text = "SUMMARY",
                Location = new Point(ColMethodX, LabelY),
                Size = new Size(ColMethodW, 22),
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = DarkText,
                TextAlign = ContentAlignment.MiddleLeft,
                AutoSize = false
            });

            row.Controls.Add(AmountLabel(Money(amount), ColAmountX, ColAmountW, DarkText, true));
            row.Controls.Add(AmountLabel(Money(gratuity), ColGratuityX, ColGratuityW, DarkText, true));
            row.Controls.Add(AmountLabel(Money(total), ColTotalX, ColTotalW, HeaderBlue, true));

            return row;
        }

        private Panel BuildRow(ReceiptPaymentDetailEntity detail)
        {
            var row = new Panel { Dock = DockStyle.Top, Height = RowHeight, BackColor = Color.White };
            row.Controls.Add(new Panel { Dock = DockStyle.Bottom, Height = 1, BackColor = RowDivider });

            row.Controls.Add(new Label
            {
                Text = detail.PaymentMethodName,
                Location = new Point(ColMethodX, LabelY),
                Size = new Size(ColMethodW, 22),
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = DarkText,
                TextAlign = ContentAlignment.MiddleLeft,
                AutoSize = false
            });

            row.Controls.Add(new Label
            {
                Text = FormatCardInfo(detail),
                Location = new Point(ColCardX, LabelY),
                Size = new Size(ColCardW, 22),
                Font = new Font("Segoe UI", 10.5F),
                ForeColor = DarkText,
                TextAlign = ContentAlignment.MiddleLeft,
                AutoSize = false
            });

            row.Controls.Add(AmountLabel(Money(detail.NetAmount), ColAmountX, ColAmountW, DarkText, false));
            row.Controls.Add(AmountLabel(Money(detail.Gratuity), ColGratuityX, ColGratuityW, DarkText, false));
            row.Controls.Add(AmountLabel(Money(detail.Total), ColTotalX, ColTotalW, HeaderBlue, true));

            // ACTION column (last) — edit pencil.
            var btnEdit = new Button
            {
                Text = "✎",
                Location = new Point(ColActionX + (ColActionW - 28) / 2, (RowHeight - 24) / 2),
                Size = new Size(28, 24),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 11F),
                ForeColor = PencilGray,
                BackColor = Color.White,
                Cursor = Cursors.Hand,
                TabStop = false
            };
            btnEdit.FlatAppearance.BorderSize = 0;
            btnEdit.Click += (s, e) => OpenDetail(detail);
            row.Controls.Add(btnEdit);

            return row;
        }

        private static Label AmountLabel(string text, int x, int w, Color color, bool bold)
        {
            return new Label
            {
                Text = text,
                Location = new Point(x, LabelY),
                Size = new Size(w, 22),
                Font = new Font("Segoe UI", 10F, bold ? FontStyle.Bold : FontStyle.Regular),
                ForeColor = color,
                TextAlign = ContentAlignment.MiddleRight,
                AutoSize = false
            };
        }

        private void OpenDetail(ReceiptPaymentDetailEntity detail)
        {
            using (var dlg = new PaymentDetailForm(_service, detail))
            {
                if (DimOverlay.ShowDialog(this, dlg) == DialogResult.OK)
                {
                    AppSession.CorrectDataSaleReceiptPaymentDetails(detail.SaleReceiptID.ToString());
                    _changed = true;
                    LoadPayments();
                }
            }
        }

        // Card payments -> "**** **** **** 1234"; otherwise -> "TID: <id>".
        private static string FormatCardInfo(ReceiptPaymentDetailEntity p)
        {
            var raw = (p.CreditCardTransactionCardNumber ?? "").Trim();
            if (raw.Length == 0)
                return string.Empty;

            string digits = new string(raw.Where(char.IsDigit).ToArray());
            if (digits.Length >= 4)
                return "xxxx" + digits.Substring(digits.Length - 4);

            return raw;
        }

        private static string Money(decimal v)
        {
            return (v < 0 ? "-$" : "$") + Math.Abs(v).ToString("F2");
        }
    }
}
