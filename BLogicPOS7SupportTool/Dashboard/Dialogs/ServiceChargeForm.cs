using BLogicPOS7SupportTool.Models;
using BLogicPOS7SupportTool.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace BLogicPOS7SupportTool.Dashboard.Dialogs
{
    partial class ServiceChargeForm : Form
    {
        private readonly ReceiptService _service;
        private readonly string _receiptId;
        private readonly string _receiptNumber;
        private Point _dragStart;
        private bool _changed;

        // One entry per charge row (control set + which fee it maps to).
        private readonly (ServiceChargeKind Kind, string Title, TextBox Amount, TextBox Tax, Label Total)[] _rows;

        // The values currently in the database, used to detect what actually changed.
        private ServiceChargeSet _loaded = new ServiceChargeSet();

        public ServiceChargeForm(ReceiptService service, string receiptId, string receiptNumber)
        {
            InitializeComponent();
            _service = service;
            _receiptId = receiptId;
            _receiptNumber = receiptNumber;

            _rows = new[]
            {
                (ServiceChargeKind.ManualServiceFee, "Service Charge", txtServiceAmount, txtServiceTax, lblServiceTotal),
                (ServiceChargeKind.AreaServiceFee, "Area Gratuity", txtAreaAmount, txtAreaTax, lblAreaTotal),
                (ServiceChargeKind.PartyServiceFee, "Party Gratuity", txtPartyAmount, txtPartyTax, lblPartyTotal),
                (ServiceChargeKind.AutoServiceFee, "Auto Service Charge", txtAutoAmount, txtAutoTax, lblAutoTotal),
                (ServiceChargeKind.ExtraServiceFee, "Extra Fee", txtExtraAmount, txtExtraTax, lblExtraTotal),
                (ServiceChargeKind.SVCFee, "SVC Fee", txtSVCFeeAmount, txtSVCFeeTax, lblSVCFeeTotal),
                (ServiceChargeKind.CashDiscount, "Cash Discount", txtCashDiscAmount, txtCashDiscTax, lblCashDiscTotal),
            };

            pnlTitleBar.MouseDown += (s, e) => { if (e.Button == MouseButtons.Left) _dragStart = e.Location; };
            pnlTitleBar.MouseMove += (s, e) => { if (e.Button == MouseButtons.Left) Location = new Point(Location.X + e.X - _dragStart.X, Location.Y + e.Y - _dragStart.Y); };

            btnCloseForm.Click += (s, e) => CloseWithResult();
            btnDone.Click += (s, e) => CloseWithResult();
            btnUpdate.Click += (s, e) => UpdateAll();

            foreach (var r in _rows)
            {
                r.Amount.KeyPress += MoneyKeyPress;
                r.Amount.Leave += MoneyLeave;
                r.Amount.TextChanged += MoneyTextChanged;
                r.Tax.KeyPress += MoneyKeyPress;
                r.Tax.Leave += MoneyLeave;
                r.Tax.TextChanged += MoneyTextChanged;
            }
        }

        private void CloseWithResult()
        {
            DialogResult = _changed ? DialogResult.OK : DialogResult.Cancel;
            Close();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            LoadCharges();
        }

        private void LoadCharges()
        {
            _loaded = _service.GetServiceCharges(_receiptId);

            txtServiceAmount.Text = _loaded.ServiceFee.ToString("F2");
            txtServiceTax.Text = _loaded.ServiceFeeTaxAmount.ToString("F2");
            txtAreaAmount.Text = _loaded.AreaFeeAmount.ToString("F2");
            txtAreaTax.Text = _loaded.AreaFeeTaxAmount.ToString("F2");
            txtPartyAmount.Text = _loaded.PartyServiceFeeAmount.ToString("F2");
            txtPartyTax.Text = _loaded.PartyServiceFeeTaxAmount.ToString("F2");
            txtAutoAmount.Text = _loaded.AutoGratuityAmount.ToString("F2");
            txtAutoTax.Text = _loaded.AutoGratuityTaxAmount.ToString("F2");
            txtExtraAmount.Text = _loaded.ExtraFeeAmount.ToString("F2");
            txtExtraTax.Text = _loaded.ExtraFeeTaxAmount.ToString("F2");
            txtSVCFeeAmount.Text = _loaded.SVCFeeAmount.ToString("F2");
            txtSVCFeeTax.Text = _loaded.SVCFeeTaxAmount.ToString("F2");
            txtCashDiscAmount.Text = _loaded.CashDiscountAmount.ToString("F2");
            txtCashDiscTax.Text = _loaded.CashDiscountTaxAmount.ToString("F2");

            RecomputeSummary();
        }

        // Recomputes each row's Total (= Amount + Tax) and the summary totals.
        private void RecomputeSummary()
        {
            decimal amount = 0, tax = 0, total = 0;
            foreach (var r in _rows)
            {
                decimal.TryParse(r.Amount.Text, out var a);
                decimal.TryParse(r.Tax.Text, out var t);
                r.Total.Text = Money(a + t);
                amount += a;
                tax += t;
                total += a + t;
            }
            lblSummaryAmount.Text = Money(amount);
            lblSummaryTax.Text = Money(tax);
            lblSummaryTotal.Text = Money(total);
        }

        // Applies every charge whose amount or tax differs from what was loaded,
        // in one confirmation. Each change rebalances the receipt independently.
        private void UpdateAll()
        {
            var edits = new List<(ServiceChargeKind Kind, string Title, decimal Amount, decimal Tax)>();

            foreach (var r in _rows)
            {
                if (!decimal.TryParse(r.Amount.Text, out var amount) || amount < 0 ||
                    !decimal.TryParse(r.Tax.Text, out var tax) || tax < 0)
                {
                    MessageBox.Show($"Please enter valid amount and tax values for '{r.Title}'.",
                        "Update Service Charges", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var original = Original(r.Kind);
                if (amount != original.Amount || tax != original.Tax)
                    edits.Add((r.Kind, r.Title, amount, tax));
            }

            if (edits.Count == 0)
            {
                MessageBox.Show("There are no changes to update.", "Update Service Charges",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                DialogResult = DialogResult.Cancel;
                this.Close();
                return;
            }

            string lines = string.Join("\n", edits.Select(e => $"    • {e.Title}: {Money(e.Amount)} (tax {Money(e.Tax)})"));
            var confirm = MessageBox.Show(
                $"Apply these changes to ticket #{_receiptNumber}?\n\n{lines}",
                "Update Service Charges", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            var failed = new List<string>();
            foreach (var e in edits)
            {
                if (_service.UpdateServiceCharge(_receiptId, e.Kind, e.Amount, e.Tax))
                    _changed = true;
                else
                    failed.Add(e.Title);
            }

            if (failed.Count == 0)
                MessageBox.Show("Service charges updated.", "Update Service Charges",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("These service charges could not be updated:\n    • " + string.Join("\n    • ", failed),
                    "Update Service Charges", MessageBoxButtons.OK, MessageBoxIcon.Error);

            DialogResult = DialogResult.OK;

            this.Close();
        }

        // The database values for one charge, used to detect edits.
        private (decimal Amount, decimal Tax) Original(ServiceChargeKind kind)
        {
            switch (kind)
            {
                case ServiceChargeKind.AreaServiceFee: return (_loaded.AreaFeeAmount, _loaded.AreaFeeTaxAmount);
                case ServiceChargeKind.PartyServiceFee: return (_loaded.PartyServiceFeeAmount, _loaded.PartyServiceFeeTaxAmount);
                case ServiceChargeKind.AutoServiceFee: return (_loaded.AutoGratuityAmount, _loaded.AutoGratuityTaxAmount);
                case ServiceChargeKind.ExtraServiceFee: return (_loaded.ExtraFeeAmount, _loaded.ExtraFeeTaxAmount);
                case ServiceChargeKind.SVCFee: return (_loaded.SVCFeeAmount, _loaded.SVCFeeTaxAmount);
                case ServiceChargeKind.CashDiscount: return (_loaded.CashDiscountAmount, _loaded.CashDiscountTaxAmount);
                default: return (_loaded.ServiceFee, _loaded.ServiceFeeTaxAmount);
            }
        }

        // Re-format a money field to two decimals and refresh the summary once
        // the user leaves it.
        private void MoneyLeave(object sender, EventArgs e)
        {
            var txt = (TextBox)sender;
            txt.Text = decimal.TryParse(txt.Text, out var v) && v >= 0 ? v.ToString("F2") : "0.00";
            RecomputeSummary();
        }

        // Keeps the Total column and summary live while the user types.
        private void MoneyTextChanged(object sender, EventArgs e)
        {
            RecomputeSummary();
        }

        private static void MoneyKeyPress(object sender, KeyPressEventArgs e)
        {
            var txt = (TextBox)sender;
            if (char.IsControl(e.KeyChar)) return;
            if (char.IsDigit(e.KeyChar)) return;
            if (e.KeyChar == '.' && !txt.Text.Contains(".")) return;
            e.Handled = true;
        }

        private static string Money(decimal v)
        {
            return (v < 0 ? "-$" : "$") + Math.Abs(v).ToString("F2");
        }
    }
}
