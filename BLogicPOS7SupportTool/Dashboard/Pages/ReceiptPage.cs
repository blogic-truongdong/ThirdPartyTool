using BLogicPOS7SupportTool.Core;
using BLogicPOS7SupportTool.Dashboard.Dialogs;
using BLogicPOS7SupportTool.Models;
using BLogicPOS7SupportTool.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BLogicPOS7SupportTool.Dashboard.Pages
{
    public partial class ReceiptPage : UserControl
    {
        private readonly ReceiptService _service = new ReceiptService();
        private string _currentReceiptId;
        private string _currentReceiptNumber;
        private string _currentStatusId;

        public ReceiptPage()
        {
            InitializeComponent();

            btnSearch.Click += BtnSearch_Click;
            txtSearch.KeyDown += TxtSearch_KeyDown;
            txtSearch.TextChanged += TxtSearch_TextChanged;
            btnAdjustPayment.Click += BtnAdjustPayment_Click;
            btnRefundItems.Click += BtnRefundItems_Click;
            btnVoidTicket.Click += BtnVoidTicket_Click;
            btnCloseTicket.Click += BtnCloseTicket_Click;
            btnAddTipByToken.Click += BtnAddTipByToken_Click;
            btnUpdateServiceCharges.Click += BtnUpdateServiceCharges_Click;
            btnFinishTicket.Click += BtnFinishTicket_Click;
            btnEditItems.Click += BtnEditItems_Click;
            btnMergeRefund.Click += BtnMergeRefund_Click;
            btnAddItem.Click += BtnAddItem_Click;
            btnCancelTicket.Click += BtnCancelTicket_Click;
            btnSyncPaymentTime.Click += BtnSyncPaymentTime_Click;
            btnInsertPayment.Click += BtnInsertPayment_Click;
            btnAddTip.Click += BtnAddTip_Click;
            this.Load += ReceiptPage_Load;
        }

        private void BtnCancelTicket_Click(object sender, EventArgs e)
        {
            const string title = "Cancel Ticket";
            var confirm = MessageBox.Show(
                $"Cancel ticket #{_currentReceiptNumber}?\n\n" +
                $"Current status:  {StatusName(_currentStatusId)}\n\n" +
                "Cancelling this ticket will:\n" +
                "    • set its status to Cancelled (CANR)\n" +
                "    • reverse any credit-card charge(s) linked to it and rebuild the\n" +
                "      affected merchant batch total(s)\n\n" +
                "This action cannot be undone.",
                "Confirm Cancel Ticket", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm != DialogResult.Yes) return;

            RunStatusChange(() => _service.CancelReceipt(_currentReceiptId), title,
                $"Ticket #{_currentReceiptNumber} has been cancelled.\n\n" +
                "Status is now Cancelled (CANR), and any linked credit-card charge has been reversed.");
        }

        private void ReceiptPage_Load(object sender, EventArgs e)
        {
            ClearAll();
        }

        private void TxtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                DoSearch();
            }
        }

        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
                ClearAll();
        }

        private void BtnSearch_Click(object sender, EventArgs e) => DoSearch();

        private void DoSearch()
        {
            var searchText = txtSearch.Text.Trim();

            if (string.IsNullOrWhiteSpace(searchText))
                return;

            Task.Run(() =>
            {
                var receipts = _service.GetReceipts(searchText);
                if (receipts.Rows.Count == 0)
                    return (Header: (ReceiptHeaderEntity)null, Items: new List<ReceiptLineItemEntity>(), TaxLines: new List<ReceiptTaxLineEntity>(), PaymentLines: new List<ReceiptPaymentLineEntity>(), ReceiptId: (string)null);

                var id = receipts.Rows[0]["SaleReceiptID"].ToString();
                var detail = _service.GetReceiptDetail(id);
                return (Header: detail.Header, Items: detail.Items, TaxLines: detail.TaxLines, PaymentLines: detail.PaymentLines, ReceiptId: id);
            })
            .ContinueWith(t =>
            {
                if (t.Result.Header == null)
                {
                    MessageBox.Show("Ticket not found.", "Search", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearAll();
                    _currentReceiptId = null;
                    btnAdjustPayment.Visible = false;
                    btnRefundItems.Visible = false;
                    btnVoidTicket.Visible = false;
                    btnCloseTicket.Visible = false;
                    btnAddTipByToken.Visible = false;
                    btnUpdateServiceCharges.Visible = false;
                    btnFinishTicket.Visible = false;
                    btnEditItems.Visible = false;
                    btnMergeRefund.Visible = false;
                    btnAddItem.Visible = false;
                    btnCancelTicket.Visible = false;
                    btnSyncPaymentTime.Visible = false;
                    btnInsertPayment.Visible = false;
                    btnAddTip.Visible = false;
                    return;
                }
                _currentReceiptId = t.Result.ReceiptId;
                _currentReceiptNumber = t.Result.Header.SaleReceiptNumber;
                _currentStatusId = t.Result.Header.TransactionStatusID;
                btnAdjustPayment.Visible = true;
                btnRefundItems.Visible = true;
                btnVoidTicket.Visible = true;
                btnCloseTicket.Visible = true;
                btnAddTipByToken.Visible = true;
                btnUpdateServiceCharges.Visible = true;
                btnFinishTicket.Visible = true;
                btnEditItems.Visible = true;
                btnMergeRefund.Visible = true;
                btnAddItem.Visible = true;
                btnCancelTicket.Visible = true;
                btnSyncPaymentTime.Visible = true;
                btnInsertPayment.Visible = true;
                btnAddTip.Visible = true;
                PopulateStoreInfo(t.Result.Header);
                PopulateItems(t.Result.Items);
                PopulateTotals(t.Result.Header, t.Result.TaxLines, t.Result.PaymentLines);
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private void BtnAdjustPayment_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_currentReceiptId))
            {
                MessageBox.Show("Please search and load a ticket first.", "Payments",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (var dlg = new AdjustPaymentForm(_service, _currentReceiptId))
            {
                // OK means at least one payment was edited — reload to reflect it.
                if (DimOverlay.ShowDialog(ParentForm, dlg) == DialogResult.OK)
                    DoSearch();
            }
        }

        private void BtnRefundItems_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_currentReceiptId)) return;

            if (_currentStatusId != "FIN")
            {
                MessageBox.Show("Only completed tickets can be refunded.", "Refund Items",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (var dlg = new RefundItemsForm(_service, _currentReceiptId, _currentReceiptNumber))
            {
                if (DimOverlay.ShowDialog(ParentForm, dlg) == DialogResult.OK)
                    DoSearch();
            }
        }

        private void BtnMergeRefund_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_currentReceiptId)) return;

            if (_currentStatusId != "FIN")
            {
                MessageBox.Show("Refund payments can only be merged into a Completed (FIN) ticket.", "Merge Refund",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (var dlg = new MergeRefundForm(_service, _currentReceiptId, _currentReceiptNumber))
            {
                if (DimOverlay.ShowDialog(ParentForm, dlg) == DialogResult.OK)
                    DoSearch();
            }
        }

        // Aligns the payment time of the loaded ticket with the ticket's own time.
        private void BtnSyncPaymentTime_Click(object sender, EventArgs e)
        {
            const string title = "Sync Payment Time";

            if (string.IsNullOrEmpty(_currentReceiptId))
            {
                MessageBox.Show("Please search and load a ticket before syncing.", title,
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var confirm = MessageBox.Show(
                $"Sync the payment time of ticket #{_currentReceiptNumber} with the ticket time?\n\n" +
                "Every payment line of this ticket gets the ticket's own date/time (the time it was finished).\n\n" +
                "This action cannot be undone.",
                title, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm != DialogResult.Yes) return;

            int rows;
            try
            {
                rows = _service.SyncPaymentTime(_currentReceiptId);
            }
            catch (Exception ex)
            {
                AppLogger.Error($"{title} failed", ex);
                MessageBox.Show(
                    $"{title} for ticket #{_currentReceiptNumber} failed.\n\n{ex.Message}\n\nNo changes were made.",
                    title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (rows < 0)
            {
                MessageBox.Show(
                    $"The payment time of ticket #{_currentReceiptNumber} could not be synced — no changes were made.",
                    title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show(
                $"{rows} payment line(s) of ticket #{_currentReceiptNumber} now match the ticket time.",
                title, MessageBoxButtons.OK, MessageBoxIcon.Information);
            DoSearch();
        }

        // Adds a payment line (optionally a card transaction in a batch) to the loaded ticket.
        private void BtnInsertPayment_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_currentReceiptId)) return;

            if (_currentStatusId == "VOID" || _currentStatusId == "CANR")
            {
                MessageBox.Show(
                    $"Ticket #{_currentReceiptNumber} is currently \"{StatusName(_currentStatusId)}\".\n\n" +
                    "Payments cannot be inserted into voided or cancelled tickets.",
                    "Insert Payment", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (var dlg = new InsertPaymentForm(_service, _currentReceiptId, _currentReceiptNumber))
            {
                if (DimOverlay.ShowDialog(ParentForm, dlg) == DialogResult.OK)
                    DoSearch();
            }
        }

        // Adds a tip-only payment line to a completed ticket, booked to the chosen employee + role.
        private void BtnAddTip_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_currentReceiptId)) return;

            if (_currentStatusId != "FIN")
            {
                MessageBox.Show("Tips can only be added to completed tickets.", "Add Tip",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (var dlg = new InsertPaymentForm(_service, _currentReceiptId, _currentReceiptNumber, InsertPaymentMode.AddTip))
            {
                if (DimOverlay.ShowDialog(ParentForm, dlg) == DialogResult.OK)
                    DoSearch();
            }
        }

        private void BtnAddTipByToken_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_currentReceiptId)) return;

            if (_currentStatusId != "FIN")
            {
                MessageBox.Show("Tips can only be added to completed tickets.", "Add Tip By Sale Token",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (var dlg = new AddTipByTokenForm(_service, _currentReceiptId, _currentReceiptNumber))
            {
                if (DimOverlay.ShowDialog(ParentForm, dlg) == DialogResult.OK)
                    DoSearch();
            }
        }

        private void BtnUpdateServiceCharges_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_currentReceiptId)) return;

            if (_currentStatusId != "FIN")
            {
                MessageBox.Show("Service charges can only be updated on completed tickets.", "Update Service Charges",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (var dlg = new ServiceChargeForm(_service, _currentReceiptId, _currentReceiptNumber))
            {
                if (DimOverlay.ShowDialog(ParentForm, dlg) == DialogResult.OK)
                {
                    AppSession.CorrectDataSaleReceiptServiceChargeTaxDetails(_currentReceiptId);
                    DoSearch();
                }
            }
        }

        private void BtnFinishTicket_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_currentReceiptId)) return;

            if (_currentStatusId != "OPER")
            {
                MessageBox.Show("Only unpaid (open) tickets can be finished.", "Finish Ticket",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (var dlg = new InsertPaymentForm(_service, _currentReceiptId, _currentReceiptNumber, InsertPaymentMode.Finish))
            {
                if (DimOverlay.ShowDialog(ParentForm, dlg) == DialogResult.OK)
                    DoSearch();
            }
        }

        private void BtnEditItems_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_currentReceiptId)) return;

            if (_currentStatusId != "FIN")
            {
                MessageBox.Show(
                    $"Ticket #{_currentReceiptNumber} is currently \"{StatusName(_currentStatusId)}\".\n\n" +
                    "Items can only be edited on Completed (FIN) tickets.",
                    "Edit Items", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (var dlg = new EditItemsForm(_service, _currentReceiptId, _currentReceiptNumber))
            {
                if (DimOverlay.ShowDialog(ParentForm, dlg) == DialogResult.OK)
                    DoSearch();
            }
        }

        private void BtnAddItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_currentReceiptId)) return;

            if (_currentStatusId != "FIN")
            {
                MessageBox.Show("Items can only be added to Completed (FIN) tickets.", "Add Item",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (var dlg = new AddItemForm(_service, _currentReceiptId, _currentReceiptNumber))
            {
                if (DimOverlay.ShowDialog(ParentForm, dlg) == DialogResult.OK)
                    DoSearch();
            }
        }

        private void BtnVoidTicket_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_currentReceiptId))
            {
                MessageBox.Show("Please search and load a ticket before voiding or cancelling.",
                    "Void / Cancel Ticket", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            const string title = "Void Ticket";
            var confirm = MessageBox.Show(
                $"Void ticket #{_currentReceiptNumber}?\n\n" +
                $"Current status:  {StatusName(_currentStatusId)}\n\n" +
                "Voiding this ticket will:\n" +
                "    • set its status to Voided (VOID)\n" +
                "    • reverse the credit-card charge(s): mark the transaction VOIDED and add a VoidSale reversal entry\n\n" +
                "    • rebuild the affected merchant batch total(s)\n\n" +
                "This action cannot be undone.",
                "Confirm Void Ticket", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm != DialogResult.Yes) return;

            RunStatusChange(() => _service.VoidReceipt(_currentReceiptId), title,
                $"Ticket #{_currentReceiptNumber} has been voided.\n\n" +
                "Status is now Voided (VOID), and any linked credit-card charge has been reversed " +
                "in the merchant batch.");

            //MessageBox.Show(
            //    $"Ticket #{_currentReceiptNumber} is currently \"{StatusName(_currentStatusId)}\" — it cannot be voided or cancelled.\n\n" +
            //    "This action only applies to:\n" +
            //    "    • Completed (FIN) tickets  →  Void\n" +
            //    "    • Unpaid (OPER) tickets  →  Cancel",
            //    "Void / Cancel Ticket", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Runs a status-changing mutation (void/cancel) that keeps the ticket
        // searchable, then reloads it so the new status is shown.
        private void RunStatusChange(Func<bool> action, string title, string successMessage)
        {
            bool ok;
            try
            {
                ok = action();
            }
            catch (Exception ex)
            {
                AppLogger.Error($"{title} failed", ex);
                MessageBox.Show(
                    $"{title} for ticket #{_currentReceiptNumber} failed.\n\n{ex.Message}\n\nNo changes were made.",
                    title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (ok)
            {
                MessageBox.Show(successMessage, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                DoSearch();
            }
            else
            {
                MessageBox.Show(
                    $"Ticket #{_currentReceiptNumber} could not be processed — no changes were made.\n\n" +
                    "Please try again, or contact support if the problem persists.",
                    title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnCloseTicket_Click(object sender, EventArgs e)
        {
            const string title = "Close Ticket";

            if (string.IsNullOrEmpty(_currentReceiptId))
            {
                MessageBox.Show("Please search and load a ticket before closing.", title,
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (_currentStatusId != "FIN" && _currentStatusId != "OPER" && _currentStatusId != "RET" && _currentStatusId != "VOID")
            {
                MessageBox.Show(
                    $"Ticket #{_currentReceiptNumber} is currently \"{StatusName(_currentStatusId)}\" and cannot be closed.\n\n" +
                    "Only tickets in one of these statuses can be closed:\n" +
                    "    • Completed (FIN)\n" +
                    "    • Unpaid (OPER)\n" +
                    "    • Refund (RET)" +
                    "    • Voided (VOID)",
                    title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var confirm = MessageBox.Show(
                $"Close ticket #{_currentReceiptNumber}?\n\n" +
                $"Current status:  {StatusName(_currentStatusId)}\n\n" +
                "Closing this ticket will:\n" +
                "    • set its status to Closed (CLO)\n" +
                "    • settle and close the credit-card batch(es) linked to it\n\n" +
                "After closing, the ticket will no longer appear in search results.\n" +
                "This action cannot be undone.",
                "Confirm Close Ticket", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm != DialogResult.Yes) return;

            bool ok;
            try
            {
                ok = _service.CloseReceipt(_currentReceiptId);
            }
            catch (Exception ex)
            {
                AppLogger.Error("Close Ticket failed", ex);
                MessageBox.Show(
                    $"Closing ticket #{_currentReceiptNumber} failed.\n\n{ex.Message}\n\nNo changes were made.",
                    title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (ok)
            {
                MessageBox.Show(
                    $"Ticket #{_currentReceiptNumber} has been closed.\n\n" +
                    "Status is now Closed (CLO) and any linked credit-card batch(es) have been settled. " +
                    "The ticket has been cleared from the view because closed tickets are no longer searchable.",
                    title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSearch.Clear();
                ClearAll();
            }
            else
            {
                MessageBox.Show(
                    $"Ticket #{_currentReceiptNumber} could not be closed — no changes were made.\n\n" +
                    "Please try again, or contact support if the problem persists.",
                    title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Friendly name for a TransactionStatusID (matches the receipt status label).
        private static string StatusName(string statusId)
        {
            switch (statusId)
            {
                case "FIN": return "Completed";
                case "OPER": return "UnPaid";
                case "VOID": return "Voided";
                case "RET": return "Refund";
                case "CANR": return "Cancelled";
                case "CLO": return "Closed";
                default: return string.IsNullOrEmpty(statusId) ? "Unknown" : statusId;
            }
        }

        private void ClearAll()
        {
            _currentReceiptId = null;
            _currentReceiptNumber = null;
            _currentStatusId = null;
            btnAdjustPayment.Visible = false;
            btnRefundItems.Visible = false;
            btnVoidTicket.Visible = false;
            btnCloseTicket.Visible = false;
            btnAddTipByToken.Visible = false;
            btnUpdateServiceCharges.Visible = false;
            btnFinishTicket.Visible = false;
            btnEditItems.Visible = false;
            btnMergeRefund.Visible = false;
            btnAddItem.Visible = false;
            btnCancelTicket.Visible = false;
            btnSyncPaymentTime.Visible = false;
            btnInsertPayment.Visible = false;
            btnAddTip.Visible = false;
            PopulateStoreInfo(new ReceiptHeaderEntity());
            PopulateItems(new List<ReceiptLineItemEntity>());
            PopulateTotals(new ReceiptHeaderEntity(), new List<ReceiptTaxLineEntity>(), new List<ReceiptPaymentLineEntity>());
        }

        private void PopulateStoreInfo(ReceiptHeaderEntity header)
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

        private void PopulateItems(List<ReceiptLineItemEntity> items)
        {
            pnlItemsList.SuspendLayout();
            pnlItemsList.Controls.Clear();

            var allRows = BuildItemRows(items);

            // Add in reverse for Dock=Top (last added = topmost)
            for (int i = allRows.Count - 1; i >= 0; i--)
                pnlItemsList.Controls.Add(allRows[i]);

            pnlItemsList.ResumeLayout();
        }

        private static List<Panel> BuildItemRows(List<ReceiptLineItemEntity> items)
        {
            var rows = new List<Panel>();
            ReceiptLineItemEntity currentParent = null;
            var currentModifiers = new List<ReceiptLineItemEntity>();

            void FlushGroup()
            {
                if (currentParent == null) return;

                // Normal section
                rows.Add(CreateItemRow(currentParent));
                foreach (var mod in currentModifiers)
                    rows.Add(CreateItemRow(mod));

                // COMP section: repeat parent as "COMP - Name" with negative total,
                // then repeat modifiers with red forecolor and negative total
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
                    currentModifiers = new List<ReceiptLineItemEntity>();
                }
                else
                {
                    currentModifiers.Add(item);
                }
            }

            FlushGroup();
            return rows;
        }

        private static Panel CreateItemRow(ReceiptLineItemEntity item, bool compLabel = false, bool isCompMod = false)
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
                Font = new Font("Segoe UI", 10F, isModifier ? FontStyle.Regular : FontStyle.Bold),
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
                Font = new Font("Segoe UI", 10F),
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
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = isNegative ? Color.FromArgb(200, 50, 50) : Color.FromArgb(50, 50, 50),
                TextAlign = ContentAlignment.MiddleRight,
                Padding = new Padding(0, 0, 8, 0),
                AutoSize = false
            };

            // Fill first, then right labels; last added (lblTotal) = rightmost
            row.Controls.Add(lblDesc);
            row.Controls.Add(lblPriceQty);
            row.Controls.Add(lblTotal);

            return row;
        }

        private void PopulateTotals(ReceiptHeaderEntity header, List<ReceiptTaxLineEntity> taxLines, List<ReceiptPaymentLineEntity> paymentLines)
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
                Dock = labelOnly ? DockStyle.Fill : DockStyle.Fill,
                Font = new Font("Segoe UI", 10F, isBold ? FontStyle.Bold : FontStyle.Regular),
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
                Font = new Font("Segoe UI", 10F, isBold ? FontStyle.Bold : FontStyle.Regular),
                ForeColor = isNeg ? Color.FromArgb(200, 50, 50) : Color.FromArgb(50, 50, 50),
                TextAlign = ContentAlignment.MiddleRight,
                Padding = new Padding(0, 0, 8, 0),
                AutoSize = false
            };

            row.Controls.Add(lblName);
            row.Controls.Add(lblAmount);
            return row;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }
    }
}
