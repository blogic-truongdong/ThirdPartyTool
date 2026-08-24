using BLogicPOS7SupportTool.Models;
using BLogicPOS7SupportTool.Services;
using System;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace BLogicPOS7SupportTool.Dashboard.Dialogs
{
    // Adds one SaleReceiptPaymentDetails row to the loaded ticket. Card methods
    // (Credit / EBT Cash / EBT Food / Pin Debit) also need a batch and the card
    // details, and get a matching TransactionDetail row in that batch.
    // In Finish mode (open tickets only) the payment covers the whole balance
    // and the ticket is then set to FIN.
    // In Add Tip mode (completed tickets only) the payment only carries a
    // gratuity, booked to the chosen employee + role in PaymentRoleDetails.
    partial class InsertPaymentForm : Form
    {
        private readonly string Title;

        private readonly ReceiptService _service;
        private readonly string _receiptId;
        private readonly bool _finishTicket;
        private readonly bool _tipOnly;
        private string _receiptNumber;
        private Point _dragStart;
        private InsertPaymentInfo _info;
        private decimal _currentMerchantFeePercent;

        public InsertPaymentForm(ReceiptService service, string receiptId, string receiptNumber, InsertPaymentMode mode = InsertPaymentMode.Insert)
        {
            InitializeComponent();
            _service = service;
            _receiptId = receiptId;
            _receiptNumber = receiptNumber;
            _finishTicket = mode == InsertPaymentMode.Finish;
            _tipOnly = mode == InsertPaymentMode.AddTip;

            Title = _finishTicket ? "Finish Ticket" : _tipOnly ? "Add Tip" : "Insert Payment";
            lblFormTitle.Text = Title;
            if (_finishTicket)
            {
                btnOK.Text = "Finish";
                txtAmountTendered.ReadOnly = true;
                txtAmountTendered.BackColor = txtSVCFeeAmountValue.BackColor;
                txtAmountTendered.TabStop = false;
            }
            if (_tipOnly) SetupTipOnlyLayout();

            pnlTitleBar.MouseDown += (s, e) => { if (e.Button == MouseButtons.Left) _dragStart = e.Location; };
            pnlTitleBar.MouseMove += (s, e) => { if (e.Button == MouseButtons.Left) Location = new Point(Location.X + e.X - _dragStart.X, Location.Y + e.Y - _dragStart.Y); };

            btnCloseForm.Click += (s, e) => { DialogResult = DialogResult.Cancel; Close(); };
            btnCancel.Click += (s, e) => { DialogResult = DialogResult.Cancel; Close(); };
            btnOK.Click += (s, e) => Insert();

            cmbPaymentMethod.SelectedIndexChanged += (s, e) => ApplyPaymentMethodDefaults();
            cmbBatch.SelectedIndexChanged += (s, e) =>
            {
                var batch = cmbBatch.SelectedItem as BatchOption;
                if (batch != null) txtMerchantID.Text = batch.MerchantID;
            };

            foreach (var txt in new[] { txtAmountTendered, txtAmountReturn, txtGratuity })
            {
                txt.KeyPress += MoneyKeyPress;
                txt.TextChanged += (s, e) => RefreshFees();
            }
            foreach (var txt in new[] { txtSVCFeeNew, txtCashDiscountNew, txtMerchantFeeNew,
                txtSVCFeeAmountManual, txtSVCFeeForGratuityManual, txtCashDiscountAmountManual, txtMerchantFeeManual, txtMerchantFeeForTipManual,
                txtSVCFeeTaxPercent, txtCashDiscountTaxPercent, txtSVCFeeTaxAmountManual, txtCashDiscountTaxAmountManual })
            {
                txt.KeyPress += MoneyKeyPress;
                txt.TextChanged += (s, e) => RefreshFees();
            }
            foreach (var chk in new[] { chkApplySVCFee, chkApplyCashDiscount, chkApplyMerchantFee })
                chk.CheckedChanged += (s, e) => RefreshFees();

            // Switching to manual entry starts from the calculated amounts.
            chkSVCFeeManual.CheckedChanged += (s, e) => ToggleManual(chkSVCFeeManual,
                Tuple.Create(txtSVCFeeAmountManual, lblSVCFeeAmount), Tuple.Create(txtSVCFeeTaxAmountManual, lblSVCFeeTaxAmount),
                Tuple.Create(txtSVCFeeForGratuityManual, lblSVCFeeForGratuity));
            chkCashDiscountManual.CheckedChanged += (s, e) => ToggleManual(chkCashDiscountManual,
                Tuple.Create(txtCashDiscountAmountManual, lblCashDiscountAmount), Tuple.Create(txtCashDiscountTaxAmountManual, lblCashDiscountTaxAmount));
            chkMerchantFeeManual.CheckedChanged += (s, e) => ToggleManual(chkMerchantFeeManual,
                Tuple.Create(txtMerchantFeeManual, lblMerchantFee), Tuple.Create(txtMerchantFeeForTipManual, lblMerchantFeeForTip));
            foreach (var rb in new[] { rbSVCFeeCurrent, rbSVCFeeNew, rbCashDiscountCurrent, rbCashDiscountNew, rbMerchantFeeCurrent, rbMerchantFeeNew })
                rb.CheckedChanged += (s, e) => RefreshFees();
            rbSVCFeeNew.CheckedChanged += (s, e) => { if (rbSVCFeeNew.Checked) txtSVCFeeNew.Focus(); };
            rbCashDiscountNew.CheckedChanged += (s, e) => { if (rbCashDiscountNew.Checked) txtCashDiscountNew.Focus(); };
            rbMerchantFeeNew.CheckedChanged += (s, e) => { if (rbMerchantFeeNew.Checked) txtMerchantFeeNew.Focus(); };
        }

        // Add Tip: the amount rows are always 0, so they make way for the Role
        // row (Created Date and Gratuity move down one row) and the cash
        // discount group is disabled.
        private void SetupTipOnlyLayout()
        {
            btnOK.Text = "Add Tip";

            foreach (Control c in new Control[] { lblAmountTendered, txtAmountTendered, lblAmountReturn, txtAmountReturn,
                lblSVCFeeAmountValueCaption, txtSVCFeeAmountValue, lblCashDiscountAmountValueCaption, txtCashDiscountAmountValue })
                c.Visible = false;

            lblRole.Visible = cmbRole.Visible = true;
            lblCreatedDate.Top = lblAmountTendered.Top;
            dtpCreatedDate.Top = txtAmountTendered.Top;
            lblGratuity.Top = lblAmountReturn.Top;
            txtGratuity.Top = txtAmountReturn.Top;
            cmbRole.TabIndex = dtpCreatedDate.TabIndex;
            txtGratuity.TabIndex = dtpCreatedDate.TabIndex + 1;

            grpCashDiscount.Enabled = false;

            // The default role depends on who served and when (time clock).
            cmbEmployee.SelectedIndexChanged += (s, e) => LoadRoles();
            dtpCreatedDate.ValueChanged += (s, e) => LoadRoles();
        }

        private void LoadRoles()
        {
            if (!_tipOnly || _info == null) return;

            var employee = cmbEmployee.SelectedItem as EmployeeOption;
            var keepRoleId = (cmbRole.SelectedItem as RoleOption)?.RolesID;
            cmbRole.DataSource = null;
            if (employee == null) return;

            var roles = _service.GetEmployeeRoleOptions(employee.EmployeeID, dtpCreatedDate.Value, out int defaultRoleId);
            cmbRole.DataSource = roles;
            var selected = roles.FirstOrDefault(x => x.RolesID == defaultRoleId)
                ?? roles.FirstOrDefault(x => x.RolesID == keepRoleId)
                ?? roles.FirstOrDefault();
            if (selected != null) cmbRole.SelectedItem = selected;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            _info = _service.GetInsertPaymentInfo(_receiptId);
            if (!_info.Found)
            {
                MessageBox.Show("The ticket could not be loaded.", Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.Cancel;
                Close();
                return;
            }
            if (_finishTicket && _info.TransactionStatusID != "OPER")
            {
                MessageBox.Show("Only open (OPER) tickets can be finished.", Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.Cancel;
                Close();
                return;
            }
            if (_tipOnly && _info.TransactionStatusID != "FIN")
            {
                MessageBox.Show("Tips can only be added to completed (FIN) tickets.", Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.Cancel;
                Close();
                return;
            }

            _receiptNumber = string.IsNullOrEmpty(_info.SaleReceiptNumber) ? _receiptNumber : _info.SaleReceiptNumber;
            lblTicket.Text = "Ticket #" + _receiptNumber;
            lblBalance.Text = "Balance due: " + Money(_info.Balance) + "    Status: " + _info.TransactionStatusID;

            dtpCreatedDate.Value = _info.DateCreated == default(DateTime) ? DateTime.Now : _info.DateCreated;
            // Balance = TotalAmount - PaidAmount; in Finish mode it is the fixed tendered amount.
            txtAmountTendered.Text = (_info.Balance > 0 && !_tipOnly ? _info.Balance : 0m).ToString("F2");

            var employees = _service.GetActiveEmployees();
            cmbEmployee.DataSource = employees;
            var cashier = employees.FirstOrDefault(x => x.EmployeeID == _info.CashierID);
            if (cashier != null) cmbEmployee.SelectedItem = cashier;

            cmbBatch.DataSource = _service.GetSettledBatches();

            rbSVCFeeCurrent.Text = "Use current SVC Fee: " + Percent(_info.SVCFeePercent) + "%";
            rbCashDiscountCurrent.Text = "Use current Cash Discount: " + Percent(_info.CashDiscountPercent) + "%";
            txtSVCFeeTaxPercent.Text = Percent(_info.SVCFeeTaxRate);
            txtCashDiscountTaxPercent.Text = "0";

            var methods = _service.GetPaymentMethodOptions();
            cmbPaymentMethod.DataSource = methods;
            var credit = methods.FirstOrDefault(x => ReceiptService.GetCardPaymentKind(x.PaymentMethodName)?.TenderType == "CreditCard");
            if (credit != null) cmbPaymentMethod.SelectedItem = credit;

            ApplyPaymentMethodDefaults();
            LoadRoles();
            if (_tipOnly) ActiveControl = txtGratuity;
        }

        private PaymentMethodOption SelectedMethod => cmbPaymentMethod.SelectedItem as PaymentMethodOption;

        private ReceiptService.CardPaymentKind SelectedKind
            => SelectedMethod == null ? null : ReceiptService.GetCardPaymentKind(SelectedMethod.PaymentMethodName);

        // Resets the card panel and both fee groups to what the POS would use
        // for the chosen method; the user can still override every value.
        private void ApplyPaymentMethodDefaults()
        {
            if (_info == null) return;

            var kind = SelectedKind;
            bool isCard = kind != null;
            pnlCard.Visible = isCard;

            if (isCard)
            {
                if (kind.TenderType == "DebitCard") cmbCardType.Text = "DEBIT";
                else if (kind.TenderType.StartsWith("EBT")) cmbCardType.Text = "EBT";
                else if (cmbCardType.Text == "DEBIT" || cmbCardType.Text == "EBT") cmbCardType.Text = "";
            }

            chkApplySVCFee.Checked = false;
            chkApplyCashDiscount.Checked = false;
            chkApplyMerchantFee.Checked = false;
            chkSVCFeeManual.Checked = false;
            chkCashDiscountManual.Checked = false;
            chkMerchantFeeManual.Checked = false;

            _currentMerchantFeePercent = ReceiptService.GetDefaultMerchantFeePercent(_info.CreditCardFeeMode, SelectedMethod?.PaymentMethodName);
            rbMerchantFeeCurrent.Text = "Use current Merchant Fee: " + Percent(_currentMerchantFeePercent) + "%";

            RefreshFees();
        }

        // Builds the request from the current inputs and recalculates the fees.
        private InsertPaymentRequest BuildRequest()
        {
            var req = new InsertPaymentRequest
            {
                PaymentMethodID = SelectedMethod?.PaymentMethodID ?? 0,
                PaymentMethodName = SelectedMethod?.PaymentMethodName,
                AmountTendered = ParseMoney(txtAmountTendered.Text),
                AmountReturn = ParseMoney(txtAmountReturn.Text),
                Gratuity = ParseMoney(txtGratuity.Text),
                TipOnly = _tipOnly,
                CreatedDate = dtpCreatedDate.Value,
                ApplySVCFee = chkApplySVCFee.Checked,
                SVCFeePercent = rbSVCFeeNew.Checked ? ParseMoney(txtSVCFeeNew.Text) : _info.SVCFeePercent,
                SVCFeeTaxPercent = ParseMoney(txtSVCFeeTaxPercent.Text),
                ApplyCashDiscount = chkApplyCashDiscount.Checked,
                CashDiscountPercent = rbCashDiscountNew.Checked ? ParseMoney(txtCashDiscountNew.Text) : _info.CashDiscountPercent,
                CashDiscountTaxPercent = ParseMoney(txtCashDiscountTaxPercent.Text),
                ApplyMerchantFee = chkApplyMerchantFee.Checked,
                MerchantFeePercent = rbMerchantFeeNew.Checked ? ParseMoney(txtMerchantFeeNew.Text) : _currentMerchantFeePercent,
                SVCFeeManual = chkApplySVCFee.Checked && chkSVCFeeManual.Checked,
                CashDiscountManual = chkApplyCashDiscount.Checked && chkCashDiscountManual.Checked,
                MerchantFeeManual = chkApplyMerchantFee.Checked && chkMerchantFeeManual.Checked
            };
            if (req.SVCFeeManual)
            {
                req.SVCFeeAmount = ParseMoney(txtSVCFeeAmountManual.Text);
                req.SVCFeeForGratuity = ParseMoney(txtSVCFeeForGratuityManual.Text);
                req.SVCFeeTaxAmount = ParseMoney(txtSVCFeeTaxAmountManual.Text);
            }
            if (req.CashDiscountManual)
            {
                req.CashDiscountAmount = ParseMoney(txtCashDiscountAmountManual.Text);
                req.CashDiscountTaxAmount = ParseMoney(txtCashDiscountTaxAmountManual.Text);
            }
            if (req.MerchantFeeManual)
            {
                req.MerchantFee = ParseMoney(txtMerchantFeeManual.Text);
                req.MerchantFeeForTip = ParseMoney(txtMerchantFeeForTipManual.Text);
            }
            ReceiptService.CalculateInsertPaymentFees(req);
            return req;
        }

        // Shows the amount textboxes in place of the calculated labels while
        // manual entry is on, pre-filled with the amounts calculated so far.
        private void ToggleManual(CheckBox chk, params Tuple<TextBox, Label>[] fields)
        {
            // Read every calculated amount first: setting a textbox refreshes the
            // fees in manual mode, which would reset the labels not copied yet.
            var amounts = fields.Select(f => ParseMoney(f.Item2.Text.Replace("$", "")).ToString("F2")).ToArray();
            for (int i = 0; i < fields.Length; i++)
            {
                if (chk.Checked)
                    fields[i].Item1.Text = amounts[i];
                fields[i].Item1.Visible = chk.Checked;
                fields[i].Item2.Visible = !chk.Checked;
            }
            RefreshFees();
            if (chk.Checked) fields.FirstOrDefault(f => f.Item1.Enabled)?.Item1.Focus();
        }

        private void RefreshFees()
        {
            // The percent stays editable in manual mode too; only the amounts
            // stop being calculated from it.
            bool svcPct = chkApplySVCFee.Checked;
            bool cashDiscountPct = chkApplyCashDiscount.Checked;
            bool merchantPct = chkApplyMerchantFee.Checked;

            chkSVCFeeManual.Enabled = txtSVCFeeAmountManual.Enabled = txtSVCFeeForGratuityManual.Enabled = chkApplySVCFee.Checked;
            txtSVCFeeTaxPercent.Enabled = txtSVCFeeTaxAmountManual.Enabled = chkApplySVCFee.Checked;
            rbSVCFeeCurrent.Enabled = rbSVCFeeNew.Enabled = svcPct;
            txtSVCFeeNew.Enabled = svcPct && rbSVCFeeNew.Checked;
            chkCashDiscountManual.Enabled = txtCashDiscountAmountManual.Enabled = chkApplyCashDiscount.Checked;
            txtCashDiscountTaxPercent.Enabled = txtCashDiscountTaxAmountManual.Enabled = chkApplyCashDiscount.Checked;
            rbCashDiscountCurrent.Enabled = rbCashDiscountNew.Enabled = cashDiscountPct;
            txtCashDiscountNew.Enabled = cashDiscountPct && rbCashDiscountNew.Checked;
            chkMerchantFeeManual.Enabled = txtMerchantFeeManual.Enabled = txtMerchantFeeForTipManual.Enabled = chkApplyMerchantFee.Checked;
            rbMerchantFeeCurrent.Enabled = rbMerchantFeeNew.Enabled = merchantPct;
            txtMerchantFeeNew.Enabled = merchantPct && rbMerchantFeeNew.Checked;
            if (_tipOnly)
            {
                // No amount: only the gratuity parts of the fees can be entered.
                txtSVCFeeAmountManual.Enabled = txtSVCFeeTaxPercent.Enabled = txtSVCFeeTaxAmountManual.Enabled = false;
                txtMerchantFeeManual.Enabled = false;
                if (chkMerchantFeeManual.Checked) txtMerchantFeeManual.Text = txtMerchantFeeForTipManual.Text;
            }

            var req = BuildRequest();
            lblSVCFeeAmount.Text = Money(req.SVCFeeAmount);
            lblSVCFeeForGratuity.Text = Money(req.SVCFeeForGratuity);
            lblSVCFeeTaxAmount.Text = Money(req.SVCFeeTaxAmount);
            lblCashDiscountAmount.Text = Money(req.CashDiscountAmount);
            lblCashDiscountTaxAmount.Text = Money(req.CashDiscountTaxAmount);
            txtSVCFeeAmountValue.Text = req.SVCFeeAmount.ToString("F2");
            txtCashDiscountAmountValue.Text = req.CashDiscountAmount.ToString("F2");
            lblMerchantFee.Text = Money(req.MerchantFee);
            lblMerchantFeeForTip.Text = Money(req.MerchantFeeForTip);

            decimal net = req.AmountTendered - req.AmountReturn;
            lblSumAmount.Text = Money(net);
            lblSumSVCFee.Text = Money(req.SVCFeeAmount);
            lblSumCashDiscount.Text = Money(-req.CashDiscountAmount);
            lblSumGratuity.Text = Money(req.Gratuity);
            lblSumTotal.Text = Money(PaymentTotal(req));
        }

        private void Insert()
        {
            var method = SelectedMethod;
            if (method == null)
            {
                Warn("Please select a payment method.");
                return;
            }

            if (!IsMoney(txtAmountTendered.Text) || !IsMoney(txtAmountReturn.Text) || !IsMoney(txtGratuity.Text))
            {
                Warn("Amount Tendered, Amount Return and Gratuity must be valid amounts (0 or more).");
                return;
            }

            var employee = cmbEmployee.SelectedItem as EmployeeOption;
            if (employee == null)
            {
                Warn("Please select an employee.");
                return;
            }

            var req = BuildRequest();
            req.EmployeeID = employee.EmployeeID;
            req.EmployeeName = employee.EmployeeName;

            if (_tipOnly)
            {
                req.Role = cmbRole.SelectedItem as RoleOption;
                if (req.Role == null)
                {
                    Warn(cmbRole.Items.Count == 0
                        ? $"{employee.EmployeeName} has no role. Assign a role to this employee first, or choose another employee."
                        : "Please select the role of the employee.");
                    return;
                }
                if (req.Gratuity <= 0)
                {
                    Warn("Please enter the tip amount (greater than 0).");
                    return;
                }
            }

            if (req.AmountReturn > req.AmountTendered)
            {
                Warn("Amount Return cannot be greater than Amount Tendered.");
                return;
            }
            if (req.AmountTendered - req.AmountReturn + req.Gratuity <= 0)
            {
                Warn("Please enter an amount or a gratuity for the payment.");
                return;
            }
            if ((req.ApplySVCFee && !IsMoney(txtSVCFeeTaxPercent.Text))
                || (req.ApplyCashDiscount && !IsMoney(txtCashDiscountTaxPercent.Text)))
            {
                Warn("The tax percent must be a valid number (0 or more).");
                return;
            }
            if ((req.ApplySVCFee && req.SVCFeeTaxPercent >= 100) || (req.ApplyCashDiscount && req.CashDiscountTaxPercent >= 100))
            {
                Warn("A tax percent must be less than 100.");
                return;
            }
            if ((req.SVCFeeManual && (!IsMoney(txtSVCFeeAmountManual.Text) || !IsMoney(txtSVCFeeForGratuityManual.Text) || !IsMoney(txtSVCFeeTaxAmountManual.Text)))
                || (req.CashDiscountManual && (!IsMoney(txtCashDiscountAmountManual.Text) || !IsMoney(txtCashDiscountTaxAmountManual.Text)))
                || (req.MerchantFeeManual && (!IsMoney(txtMerchantFeeManual.Text) || !IsMoney(txtMerchantFeeForTipManual.Text))))
            {
                Warn("The fee amounts entered manually must be valid amounts (0 or more).");
                return;
            }
            if (req.SVCFeeManual && req.SVCFeeAmount + req.SVCFeeForGratuity <= 0)
            {
                Warn(_tipOnly
                    ? "Please enter the SVC fee on gratuity, or untick Apply SVC Fee."
                    : "Please enter the SVC fee amounts, or untick Apply SVC Fee.");
                return;
            }
            if (req.CashDiscountManual && req.CashDiscountAmount <= 0)
            {
                Warn("Please enter the cash discount amount, or untick Apply Cash Discount.");
                return;
            }
            if (req.CashDiscountAmount > req.AmountTendered - req.AmountReturn)
            {
                Warn("Cash Discount cannot be greater than the amount (Tendered - Return).");
                return;
            }
            if (req.MerchantFeeManual && req.MerchantFee + req.MerchantFeeForTip <= 0)
            {
                Warn("Please enter the merchant fee amounts, or untick Apply Merchant Fee.");
                return;
            }
            if (req.MerchantFeeManual && req.MerchantFeeForTip > req.MerchantFee)
            {
                Warn("Merchant Fee on Gratuity cannot be greater than Merchant Fee (Amount + Tip).");
                return;
            }
            if (req.ApplySVCFee && !req.SVCFeeManual && req.SVCFeePercent <= 0)
            {
                Warn(rbSVCFeeNew.Checked
                    ? "Please enter the new SVC fee percent (greater than 0)."
                    : "The current SVC fee percent is 0. Enter a new percent, or untick Apply SVC Fee.");
                return;
            }
            if (req.ApplyMerchantFee && !req.MerchantFeeManual && req.MerchantFeePercent <= 0)
            {
                Warn(rbMerchantFeeNew.Checked
                    ? "Please enter the new merchant fee percent (greater than 0)."
                    : "The current merchant fee percent is 0. Enter a new percent, or untick Apply Merchant Fee.");
                return;
            }
            if (req.ApplyCashDiscount && !req.CashDiscountManual && req.CashDiscountPercent <= 0)
            {
                Warn(rbCashDiscountNew.Checked
                    ? "Please enter the new cash discount percent (greater than 0)."
                    : "The current cash discount percent is 0. Enter a new percent, or untick Apply Cash Discount.");
                return;
            }
            if ((req.ApplySVCFee && req.SVCFeePercent >= 100)
                || (req.ApplyCashDiscount && req.CashDiscountPercent >= 100)
                || (req.ApplyMerchantFee && req.MerchantFeePercent >= 100))
            {
                Warn("A fee percent must be less than 100.");
                return;
            }

            var kind = SelectedKind;
            BatchOption batch = null;
            if (kind != null)
            {
                batch = cmbBatch.SelectedItem as BatchOption;
                if (batch == null)
                {
                    Warn($"Please select the batch to insert the {method.PaymentMethodName} transaction into.");
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtMerchantID.Text))
                {
                    Warn("Please enter the Merchant ID.");
                    return;
                }

                req.BatchUnique = batch.BatchUnique;
                req.BatchID = batch.BatchID;
                req.CardType = cmbCardType.Text.Trim();
                req.TransactionNumber = txtTransactionNumber.Text.Trim();
                req.TransactionCardNumber = txtCardNumber.Text.Trim();
                req.RefNo = txtRefNo.Text.Trim();
                req.AuthCode = txtAuthCode.Text.Trim();
                req.MerchantID = txtMerchantID.Text.Trim();
                req.ExpDate = txtExpDate.Text.Trim();
                req.CCEntry = txtCCEntry.Text.Trim();
                req.BankApprovalCode = txtBankApprovalCode.Text.Trim();
                req.BankTransactionID = txtBankTransactionID.Text.Trim();
                req.AID = txtAID.Text.Trim();
                req.CardHolderName = txtCardHolderName.Text.Trim();
            }

            req.FinishTicket = _finishTicket;

            decimal net = req.AmountTendered - req.AmountReturn;
            var confirm = _tipOnly ? ConfirmAddTip(req, method, employee, batch) : MessageBox.Show(
                (_finishTicket
                    ? $"Finish ticket #{_receiptNumber} with a {method.PaymentMethodName} payment?\n\n"
                    : $"Insert a {method.PaymentMethodName} payment into ticket #{_receiptNumber}?\n\n") +
                $"    Amount:  {Money(net)}\n" +
                $"    SVC Fee:  {Money(req.SVCFeeAmount)}   (on gratuity {Money(req.SVCFeeForGratuity)}, tax {Money(req.SVCFeeTaxAmount)})\n" +
                $"    Cash Discount:  {Money(-req.CashDiscountAmount)}   (tax {Money(req.CashDiscountTaxAmount)})\n" +
                $"    Gratuity:  {Money(req.Gratuity)}\n" +
                $"    Merchant Fee:  {Money(req.MerchantFee)}   (on gratuity {Money(req.MerchantFeeForTip)})" +
                (req.ApplyMerchantFee ? $"   at {Percent(req.MerchantFeePercent)}%" : "") + "\n" +
                $"    Payment Total:  {Money(PaymentTotal(req))}\n" +
                $"    Employee:  {employee.EmployeeName}\n" +
                $"    Date:  {req.CreatedDate:M/d/yyyy h:mm:ss tt}\n" +
                (batch != null ? $"    Batch:  {batch}\n" : "") +
                (_finishTicket
                    ? "\nThe ticket's paid amount is increased by this payment and its status is set to FIN (Completed).\n"
                    : "\nThe ticket's paid amount is increased by this payment; its status is not changed.\n") +
                "This action cannot be undone.",
                Title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            bool ok = _service.InsertPaymentDetail(_receiptId, req);
            if (ok)
            {
                MessageBox.Show(_finishTicket
                        ? $"Ticket #{_receiptNumber} has been finished with a {method.PaymentMethodName} payment."
                        : _tipOnly
                            ? $"A {method.PaymentMethodName} tip of {Money(req.Gratuity)} has been added to ticket #{_receiptNumber} for {employee.EmployeeName} ({req.Role.RolesName})."
                            : $"The {method.PaymentMethodName} payment has been added to ticket #{_receiptNumber}.",
                    Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show("The payment could not be inserted. No changes were made.\n\nSee the log for details.",
                    Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private DialogResult ConfirmAddTip(InsertPaymentRequest req, PaymentMethodOption method, EmployeeOption employee, BatchOption batch)
        {
            return MessageBox.Show(
                $"Add a {method.PaymentMethodName} tip to ticket #{_receiptNumber}?\n\n" +
                $"    Tip:  {Money(req.Gratuity)}\n" +
                $"    SVC Fee on tip:  {Money(req.SVCFeeForGratuity)}\n" +
                $"    Merchant Fee on tip:  {Money(req.MerchantFeeForTip)}" +
                (req.ApplyMerchantFee ? $"   at {Percent(req.MerchantFeePercent)}%" : "") + "\n" +
                $"    Employee:  {employee.EmployeeName}\n" +
                $"    Role:  {req.Role.RolesName}\n" +
                $"    Date:  {req.CreatedDate:M/d/yyyy h:mm:ss tt}\n" +
                (batch != null ? $"    Batch:  {batch}\n" : "") +
                "\nA tip-only payment line (amount $0.00) is added to the ticket and the tip is booked to this " +
                "employee and role (PaymentRoleDetails). The ticket's paid amount and status are not changed.\n" +
                "This action cannot be undone.",
                Title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }

        // What the customer actually pays: amount + SVC fee - cash discount + gratuity.
        private static decimal PaymentTotal(InsertPaymentRequest req)
        {
            return req.AmountTendered - req.AmountReturn + req.SVCFeeAmount - req.CashDiscountAmount + req.Gratuity;
        }

        private void Warn(string message)
        {
            MessageBox.Show(message, Title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private static bool IsMoney(string text)
        {
            return string.IsNullOrWhiteSpace(text)
                || (decimal.TryParse(text, NumberStyles.Number, CultureInfo.InvariantCulture, out var v) && v >= 0);
        }

        private static decimal ParseMoney(string text)
        {
            return decimal.TryParse(text, NumberStyles.Number, CultureInfo.InvariantCulture, out var v) && v > 0 ? v : 0m;
        }

        private void MoneyKeyPress(object sender, KeyPressEventArgs e)
        {
            var text = ((Control)sender).Text;
            if (char.IsControl(e.KeyChar)) return;
            if (char.IsDigit(e.KeyChar)) return;
            if (e.KeyChar == '.' && !text.Contains(".")) return;
            e.Handled = true;
        }

        private static string Percent(decimal v)
        {
            return v.ToString("0.####", CultureInfo.InvariantCulture);
        }

        private static string Money(decimal v)
        {
            return (v < 0 ? "-$" : "$") + Math.Abs(v).ToString("F2");
        }
    }
}
