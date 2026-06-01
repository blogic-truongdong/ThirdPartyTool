using BLogicPOS7SupportTool.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace BLogicPOS7SupportTool.Dashboard.Dialogs
{
    // Hiển thị chi tiết một ticket (store info + items + totals) giống ReceiptPage,
    // dùng lại được trong các dialog. Gọi Populate(...) để nạp dữ liệu, Clear() để xoá.
    partial class ReceiptDetailView : UserControl
    {
        public ReceiptDetailView()
        {
            InitializeComponent();
        }

        public void Populate(ReceiptHeader header, List<ReceiptLineItem> items,
            List<ReceiptTaxLine> taxLines, List<ReceiptPaymentLine> paymentLines)
        {
            PopulateStoreInfo(header ?? new ReceiptHeader());
            PopulateItems(items ?? new List<ReceiptLineItem>());
            PopulateTotals(header ?? new ReceiptHeader(), taxLines ?? new List<ReceiptTaxLine>(),
                paymentLines ?? new List<ReceiptPaymentLine>());
        }

        public void Clear()
        {
            PopulateStoreInfo(new ReceiptHeader());
            PopulateItems(new List<ReceiptLineItem>());
            PopulateTotals(new ReceiptHeader(), new List<ReceiptTaxLine>(), new List<ReceiptPaymentLine>());
        }

        private void PopulateStoreInfo(ReceiptHeader header)
        {
            SetLabel(lblStoreName, header.StoreName);
            SetLabel(lblStoreAddress, header.StoreAddress);
            SetLabel(lblStorePhone, header.StorePhone);
            SetLabel(lblStoreEmail, header.StoreEmail);
            SetLabel(lblStoreWebsite, header.StoreWebsite);

            SetReceiptStatus(header.TransactionStatusID);
            SetLabel(lblTableName, header.TableName);

            lblCashierInfo.Text = string.IsNullOrEmpty(header.CashierName) ? "" : "Server: " + header.CashierName;
            lblDateCreated.Text = header.DateCreated == default(DateTime) ? "" : header.DateCreated.ToString("M/d/yyyy h:mm tt");
            pnlCashierRow.Visible = !string.IsNullOrWhiteSpace(lblCashierInfo.Text) || !string.IsNullOrWhiteSpace(lblDateCreated.Text);

            lblTicketInfo.Text = string.IsNullOrEmpty(header.SaleReceiptNumber) ? "" : "Ticket #: " + header.SaleReceiptNumber;
            lblWorkstation.Text = header.WorkstationName ?? "";
            pnlTicketRow.Visible = !string.IsNullOrWhiteSpace(lblTicketInfo.Text) || !string.IsNullOrWhiteSpace(lblWorkstation.Text);

            lblCustomerName.Text = header.CurrentCustomerName ?? "";
            lblCustomerPhone.Text = header.CurrentCustomerPhone ?? "";
            pnlCustomerRow.Visible = !string.IsNullOrWhiteSpace(header.CurrentCustomerName) || !string.IsNullOrWhiteSpace(header.CurrentCustomerPhone);
        }

        private void SetReceiptStatus(string statusId)
        {
            string text;
            Color color;
            switch (statusId)
            {
                case "FIN": text = "Completed"; color = Color.FromArgb(34, 139, 34); break;
                case "OPER": text = "UnPaid"; color = Color.FromArgb(210, 120, 0); break;
                case "VOID": text = "Voided"; color = Color.FromArgb(120, 120, 120); break;
                case "RET": text = "Refund"; color = Color.FromArgb(0, 102, 204); break;
                case "CANR": text = "Cancelled"; color = Color.FromArgb(200, 50, 50); break;
                default: text = ""; color = Color.FromArgb(50, 50, 50); break;
            }
            lblReceiptStatus.Text = text;
            lblReceiptStatus.ForeColor = color;
            lblReceiptStatus.Visible = !string.IsNullOrEmpty(text);
        }

        private static void SetLabel(Label lbl, string text)
        {
            lbl.Text = text ?? "";
            lbl.Visible = !string.IsNullOrWhiteSpace(text);
        }

        private void PopulateItems(List<ReceiptLineItem> items)
        {
            pnlItemsList.SuspendLayout();
            pnlItemsList.Controls.Clear();

            var allRows = BuildItemRows(items);

            // Add in reverse for Dock=Top (last added = topmost)
            for (int i = allRows.Count - 1; i >= 0; i--)
                pnlItemsList.Controls.Add(allRows[i]);

            pnlItemsList.ResumeLayout();
        }

        private static List<Panel> BuildItemRows(List<ReceiptLineItem> items)
        {
            var rows = new List<Panel>();
            ReceiptLineItem currentParent = null;
            var currentModifiers = new List<ReceiptLineItem>();

            void FlushGroup()
            {
                if (currentParent == null) return;

                rows.Add(CreateItemRow(currentParent));
                foreach (var mod in currentModifiers)
                    rows.Add(CreateItemRow(mod));

                if (currentParent.IsCompItem)
                {
                    rows.Add(CreateItemRow(currentParent, compLabel: true));
                    foreach (var mod in currentModifiers)
                        rows.Add(CreateItemRow(mod, isCompMod: true));
                }
            }

            foreach (var item in items)
            {
                if (!item.IsModifier)
                {
                    FlushGroup();
                    currentParent = item;
                    currentModifiers = new List<ReceiptLineItem>();
                }
                else
                {
                    currentModifiers.Add(item);
                }
            }

            FlushGroup();
            return rows;
        }

        private static Panel CreateItemRow(ReceiptLineItem item, bool compLabel = false, bool isCompMod = false)
        {
            bool isModifier = item.IsModifier;
            decimal displayExtendedPrice = (compLabel || isCompMod)
                ? -Math.Abs(item.ExtendedPrice)
                : item.ExtendedPrice;
            bool isNegative = displayExtendedPrice < 0;
            bool hasTax = item.TaxAmount > 0 || item.Taxable;
            string description = compLabel ? "COMP - " + item.Description : item.Description;

            var rowForeColor = isCompMod
                ? Color.FromArgb(200, 50, 50)
                : isModifier
                    ? Color.FromArgb(150, 150, 150)
                    : Color.FromArgb(50, 50, 50);

            var row = new Panel
            {
                Height = 24,
                Dock = DockStyle.Top,
                BackColor = Color.White,
                BorderStyle = BorderStyle.None
            };

            var lblDesc = new Label
            {
                Text = description,
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 9F, isModifier ? FontStyle.Regular : FontStyle.Bold),
                ForeColor = rowForeColor,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(8, 0, 0, 0),
                AutoSize = false
            };

            var lblPriceQty = new Label
            {
                Text = "$" + item.Price.ToString("F2") + " x " + item.Quantity,
                Width = 80,
                Dock = DockStyle.Right,
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.FromArgb(150, 150, 150),
                TextAlign = ContentAlignment.MiddleRight,
                AutoSize = false
            };

            string totalText = (isNegative ? "-$" : "$") + Math.Abs(displayExtendedPrice).ToString("F2")
                + (hasTax && !isModifier && !isCompMod ? "T" : "");
            var lblTotal = new Label
            {
                Text = totalText,
                Width = 80,
                Dock = DockStyle.Right,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = isNegative ? Color.FromArgb(200, 50, 50) : Color.FromArgb(50, 50, 50),
                TextAlign = ContentAlignment.MiddleRight,
                Padding = new Padding(0, 0, 8, 0),
                AutoSize = false
            };

            row.Controls.Add(lblDesc);
            row.Controls.Add(lblPriceQty);
            row.Controls.Add(lblTotal);

            return row;
        }

        private void PopulateTotals(ReceiptHeader header, List<ReceiptTaxLine> taxLines, List<ReceiptPaymentLine> paymentLines)
        {
            pnlTotals.SuspendLayout();
            pnlTotals.Controls.Clear();

            var rows = new List<Panel>();

            if (header.SubTotal > 0)
                rows.Add(CreateTotalsRow("SUBTOTAL", header.SubTotal));

            if (header.TotalDiscountAmount > 0)
                rows.Add(CreateTotalsRow("DISCOUNT", -header.TotalDiscountAmount));

            if (header.ServiceFee > 0)
                rows.Add(CreateTotalsRow("SERVICE CHARGE", header.ServiceFee + header.ExtraFeeAmount));

            if (header.AreaFeeAmount > 0)
                rows.Add(CreateTotalsRow("AREA GRATUITY", header.AreaFeeAmount));

            if (header.PartyServiceFeeAmount > 0)
                rows.Add(CreateTotalsRow("PARTY GRATUITY", header.PartyServiceFeeAmount));

            if (header.AutoGratuityAmount > 0)
                rows.Add(CreateTotalsRow("AUTO SERVICE CHARGE", header.AutoGratuityAmount));

            if (header.SVCFeeAmount > 0)
                rows.Add(CreateTotalsRow("SVC FEE", header.SVCFeePaidAmount - header.CashDiscountPaidAmount));

            for (int i = 0; i < taxLines.Count; i++)
            {
                decimal rate = taxLines[i].TaxRate < 1 ? taxLines[i].TaxRate * 100 : taxLines[i].TaxRate;
                string pct = rate.ToString("0.##") + "%";
                string name = i == 0 ? "SALES TAX (" + pct + ")" : "SALES TAX " + (i + 1) + " (" + pct + ")";
                rows.Add(CreateTotalsRow(name, taxLines[i].TaxAmount));
            }

            if (header.FinalTotal > 0)
                rows.Add(CreateTotalsRow("TOTAL", header.FinalTotal, isBold: true));

            if (header.TipAmount > 0)
            {
                rows.Add(CreateTotalsRow("TIP", header.TipAmount));
                rows.Add(CreateTotalsRow("FINAL TOTAL", header.FinalTotal + header.TipAmount, isBold: true));
            }

            if (paymentLines != null && paymentLines.Count > 0)
            {
                rows.Add(new Panel { Height = 1, Dock = DockStyle.Top, BackColor = Color.FromArgb(210, 210, 210) });
                rows.Add(CreateTotalsRow("PAYMENT", 0, isBold: true, labelOnly: true));
                foreach (var p in paymentLines)
                    rows.Add(CreateTotalsRow(p.PaymentMethodName, p.Amount));
            }

            int totalHeight = 0;
            for (int i = rows.Count - 1; i >= 0; i--)
            {
                pnlTotals.Controls.Add(rows[i]);
                totalHeight += rows[i].Height;
            }

            pnlTotals.Height = totalHeight;
            pnlTotals.ResumeLayout();
        }

        private static Panel CreateTotalsRow(string label, decimal amount, bool isBold = false, bool labelOnly = false)
        {
            bool isNeg = amount < 0;
            int rowHeight = isBold ? 28 : 24;

            var row = new Panel { Height = rowHeight, Dock = DockStyle.Top, BackColor = Color.White };

            var lblName = new Label
            {
                Text = label,
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 9F, isBold ? FontStyle.Bold : FontStyle.Regular),
                ForeColor = Color.FromArgb(50, 50, 50),
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(8, 0, 0, 0),
                AutoSize = false
            };

            if (labelOnly)
            {
                row.Controls.Add(lblName);
                return row;
            }

            var lblAmount = new Label
            {
                Text = (isNeg ? "-$" : "$") + Math.Abs(amount).ToString("F2"),
                Width = 120,
                Dock = DockStyle.Right,
                Font = new Font("Segoe UI", 9F, isBold ? FontStyle.Bold : FontStyle.Regular),
                ForeColor = isNeg ? Color.FromArgb(200, 50, 50) : Color.FromArgb(50, 50, 50),
                TextAlign = ContentAlignment.MiddleRight,
                Padding = new Padding(0, 0, 8, 0),
                AutoSize = false
            };

            row.Controls.Add(lblName);
            row.Controls.Add(lblAmount);
            return row;
        }
    }
}
