using BLogicPOS7SupportTool.Core;
using BLogicPOS7SupportTool.Models;
using BLogicPOS7SupportTool.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BLogicPOS7SupportTool.Dashboard.Pages
{
    // Loads TemplateInsertSalesData.xlsx, shows every row with the references it
    // resolved against, and once the operator confirms writes each selected row
    // as a complete settled ticket into BLOGICPOS7 and Merchant.
    public partial class InsertTicketPage : UserControl
    {
        private const string DialogTitle = "Insert Tickets From Template";

        private readonly TicketTemplateService _service = new TicketTemplateService();
        private readonly BindingSource _source = new BindingSource();
        private List<TicketTemplateRow> _rows = new List<TicketTemplateRow>();
        private bool _busy;

        public InsertTicketPage()
        {
            InitializeComponent();
            StyleGrid(dgvRows);

            _source.DataSource = _rows;
            dgvRows.DataSource = _source;
        }

        // Nothing is read until the operator picks a file: this page writes tickets, so
        // it should never come up already holding data nobody asked for.
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            UpdateSummary();
        }

        // ---- loading -------------------------------------------------------

        // Hands the operator the workbook to fill in, so a first-time insert does not
        // start by guessing sheet and column names.
        private void btnDownload_Click(object sender, EventArgs e)
        {
            using (var dlg = new SaveFileDialog())
            {
                dlg.Title = DialogTitle;
                dlg.Filter = "Excel workbook (*.xlsx)|*.xlsx|All files (*.*)|*.*";
                dlg.FileName = TicketTemplateService.DefaultTemplateFileName;
                dlg.DefaultExt = "xlsx";
                if (dlg.ShowDialog(ParentForm) != DialogResult.OK) return;

                try
                {
                    TicketTemplateService.SaveTemplateTo(dlg.FileName);
                }
                catch (Exception ex)
                {
                    AppLogger.Error($"Download ticket template failed: {dlg.FileName}", ex);
                    MessageBox.Show($"The template could not be saved.\n\n{ex.Message}",
                        DialogTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (MessageBox.Show(BuildTemplateSavedText(dlg.FileName), DialogTitle,
                        MessageBoxButtons.YesNo, MessageBoxIcon.Information) != DialogResult.Yes)
                    return;

                txtFilePath.Text = dlg.FileName;
                LoadTemplate(dlg.FileName);
            }
        }

        private string BuildTemplateSavedText(string path)
        {
            return $"Template saved to:\n{path}\n\n" +
                   "Fill in the Templates sheet - one ticket per row, keeping the ID column. " +
                   "The SaleReceipts, SaleReceiptDetails, SaleReceiptPaymentDetails and " +
                   "TransactionDetail sheets work themselves out from it by formula; the tool " +
                   "inserts exactly the figures they show, it does not recalculate them.\n\n" +
                   "WorkstationName and ItemName are matched by name against this database, " +
                   "EmployeeCode against Employees.EmployeeCode.\n\n" +
                   "Open it now?";
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (var dlg = new OpenFileDialog())
            {
                dlg.Title = DialogTitle;
                dlg.Filter = "Excel workbook (*.xlsx)|*.xlsx|All files (*.*)|*.*";
                dlg.InitialDirectory = Path.GetDirectoryName(TicketTemplateService.DefaultTemplatePath);
                if (dlg.ShowDialog(ParentForm) != DialogResult.OK) return;

                txtFilePath.Text = dlg.FileName;
                LoadTemplate(dlg.FileName);
            }
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFilePath.Text))
            {
                MessageBox.Show("Pick a template file first with Browse...",
                    DialogTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            LoadTemplate(txtFilePath.Text);
        }

        private void LoadTemplate(string path)
        {
            if (_busy) return;
            if (!File.Exists(path))
            {
                MessageBox.Show($"The template file could not be found:\n{path}",
                    DialogTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SetBusy(true, "Loading template...");

            Task.Run(() =>
            {
                var rows = _service.Load(path);
                _service.Validate(rows);
                return rows;
            })
            .ContinueWith(t =>
            {
                SetBusy(false, "");

                if (t.Exception != null)
                {
                    var ex = t.Exception.GetBaseException();
                    AppLogger.Error($"Load ticket template failed: {path}", ex);
                    MessageBox.Show($"The template could not be read.\n\n{ex.Message}",
                        DialogTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Rebind(t.Result);
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private void Rebind(List<TicketTemplateRow> rows)
        {
            _rows = rows;
            _source.DataSource = _rows;
            _source.ResetBindings(false);
            dgvRows.ClearSelection();
            UpdateSummary();
        }

        // ---- selection -----------------------------------------------------

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            SetSelectionOnReadyRows(true);
        }

        private void btnSelectNone_Click(object sender, EventArgs e)
        {
            SetSelectionOnReadyRows(false);
        }

        // Only rows that passed validation can be selected - ticking an invalid or
        // already-imported row would have no effect anyway. When nothing is selectable
        // the button would look broken, so say why instead of silently doing nothing.
        private void SetSelectionOnReadyRows(bool selected)
        {
            if (selected && !_rows.Any(r => r.State == TicketTemplateRowState.Ready))
            {
                MessageBox.Show(BuildNothingToSelectText(), DialogTitle,
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            foreach (var row in _rows)
                row.Selected = selected && row.State == TicketTemplateRowState.Ready;

            _source.ResetBindings(false);
            UpdateSummary();
        }

        // Explains why no row can be inserted, grouped by the actual blocking reason.
        private string BuildNothingToSelectText()
        {
            if (_rows.Count == 0)
                return "No template is loaded yet. Use Browse... to pick a workbook.";

            // Employees never block a row (unknown codes fall back to BLogic), so the
            // blocker is always a workstation or an item name.

            var reasons = _rows
                .Where(r => r.State != TicketTemplateRowState.Ready)
                .GroupBy(r => r.StateText + ": " + (string.IsNullOrEmpty(r.Message) ? "(no detail)" : r.Message))
                .OrderByDescending(g => g.Count())
                .Select(g => $"  {g.Count()} row(s) - {g.Key}")
                .Take(6)
                .ToList();

            return $"None of the {_rows.Count} row(s) can be inserted:\n\n" +
                   string.Join("\n", reasons) +
                   $"\n\nWorkstations and items are looked up by name (and employees by " +
                   $"EmployeeCode) in {AppSession.BLogicPOS7DatabaseName}. If those belong to a " +
                   "different store, log in again against that store's database.";
        }

        // Lock the tick box on rows that cannot be inserted, so the grid itself shows
        // what is and is not selectable.
        private void dgvRows_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewRow gridRow in dgvRows.Rows)
            {
                var row = gridRow.DataBoundItem as TicketTemplateRow;
                if (row == null) continue;
                gridRow.Cells["colSelected"].ReadOnly = row.State != TicketTemplateRowState.Ready;
            }
        }

        private void dgvRows_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || dgvRows.Columns[e.ColumnIndex].Name != "colSelected") return;

            var row = dgvRows.Rows[e.RowIndex].DataBoundItem as TicketTemplateRow;
            if (row == null) return;

            // Refuse the tick on rows that can't be inserted, then put it back.
            if (row.State != TicketTemplateRowState.Ready)
            {
                row.Selected = false;
                _source.ResetBindings(false);
            }
            UpdateSummary();
        }

        // Commit the checkbox as soon as it is clicked so the summary stays truthful.
        private void dgvRows_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvRows.IsCurrentCellDirty)
                dgvRows.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void dgvRows_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvRows.Rows.Count) return;

            var row = dgvRows.Rows[e.RowIndex].DataBoundItem as TicketTemplateRow;
            if (row == null) return;

            Color fore;
            switch (row.State)
            {
                case TicketTemplateRowState.Invalid:
                case TicketTemplateRowState.Failed:
                    fore = Color.FromArgb(200, 50, 50);
                    break;
                case TicketTemplateRowState.Inserted:
                    fore = Color.FromArgb(20, 130, 70);
                    break;
                default:
                    fore = Theme.NavText;
                    break;
            }

            dgvRows.Rows[e.RowIndex].DefaultCellStyle.ForeColor = fore;
        }

        // ---- insert --------------------------------------------------------

        private void btnInsert_Click(object sender, EventArgs e)
        {
            if (_busy) return;

            if (string.IsNullOrEmpty(AppSession.BLogicPOS7ConnectionString) ||
                string.IsNullOrEmpty(AppSession.MerchantConnectionString))
            {
                MessageBox.Show("Both the BLOGICPOS7 and the Merchant database must be connected before tickets can be inserted.",
                    DialogTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var todo = _rows.Where(r => r.Selected && r.State == TicketTemplateRowState.Ready).ToList();
            if (todo.Count == 0)
            {
                MessageBox.Show("No rows are selected. Tick the rows you want to insert first.",
                    DialogTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show(BuildConfirmationText(todo), DialogTitle,
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;

            SetBusy(true, $"Inserting 0 of {todo.Count} ticket(s)...");

            Task.Run(() => _service.Insert(_rows, (done, total) => ReportProgress(done, total)))
                .ContinueWith(t =>
                {
                    SetBusy(false, "");
                    _source.ResetBindings(false);
                    UpdateSummary();

                    if (t.Exception != null)
                    {
                        var ex = t.Exception.GetBaseException();
                        AppLogger.Error("Insert tickets from template failed", ex);
                        MessageBox.Show($"The insert stopped with an error.\n\n{ex.Message}\n\n" +
                                        "Check the Status column to see which rows made it in.",
                            DialogTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    ShowResult(t.Result);
                }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private string BuildConfirmationText(List<TicketTemplateRow> todo)
        {
            DateTime from = todo.Min(r => r.TransactionDateTime);
            DateTime to = todo.Max(r => r.TransactionDateTime);
            int batches = todo.Select(r => r.TerminalID + "|" + r.TransactionDateTime.Date.ToString("yyyyMMdd"))
                              .Distinct().Count();

            // Any column this database does not have is skipped rather than failing the
            // insert - the operator has to see that before committing.
            string schema = _service.SchemaNotes.Count == 0
                ? ""
                : "Schema differences in this database:\n  " +
                  string.Join("\n  ", _service.SchemaNotes) + "\n\n";

            return $"Insert {todo.Count} ticket(s) totalling {Money(todo.Sum(r => r.TranTotal))} " +
                   $"({from:MM/dd/yyyy} – {to:MM/dd/yyyy})?\n\n" +
                   $"This writes, per ticket, into {AppSession.BLogicPOS7DatabaseName}:\n" +
                   "  • SaleReceipts (1 row, status FIN)\n" +
                   "  • SaleReceiptDetails (1 row)\n" +
                   "  • SaleReceiptDetailTaxDetails (1 row)\n" +
                   "  • SaleReceiptPaymentDetails (1 row, Credit)\n" +
                   "  • SaleReceiptServiceChargeTaxDetails (no rows — the template has no service charges)\n" +
                   "  • PaymentRoleDetails (1 row)\n\n" +
                   $"and into {AppSession.MerchantDatabaseName}:\n" +
                   "  • TransactionDetail (1 row)\n" +
                   $"  • Batch ({batches} closed batch(es), one per terminal and business date)\n\n" +
                   schema +
                   "This cannot be undone.";
        }

        private void ReportProgress(int done, int total)
        {
            if (!IsHandleCreated) return;
            BeginInvoke((MethodInvoker)delegate
            {
                lblLoading.Text = $"Inserting {done} of {total} ticket(s)...";
            });
        }

        private void ShowResult(TicketTemplateService.InsertResult result)
        {
            var text = $"{result.Inserted} ticket(s) inserted across {result.Batches} batch(es).";
            if (result.Failed > 0)
                text += $"\n{result.Failed} row(s) failed — see the Note column for each one.";
            if (result.Errors.Count > 0)
                text += "\n\n" + string.Join("\n", result.Errors.Take(10));
            if (result.Errors.Count > 10)
                text += $"\n… and {result.Errors.Count - 10} more (see the log).";

            if (string.IsNullOrEmpty(result.ScriptFile))
            {
                MessageBox.Show(text, DialogTitle, MessageBoxButtons.OK,
                    result.Failed > 0 ? MessageBoxIcon.Warning : MessageBoxIcon.Information);
                return;
            }

            // The replay script is the point of the run for anyone rebuilding the same
            // tickets on another machine, so offer to go straight to it.
            text += $"\n\nEvery statement that succeeded was written to:\n{result.ScriptFile}\n\n" +
                    "Run section 1 against another store's BLogicPOS7 database and section 2 " +
                    "against its Merchant database to re-create the same tickets there.\n\n" +
                    "Open the folder now?";

            if (MessageBox.Show(text, DialogTitle, MessageBoxButtons.YesNo,
                    result.Failed > 0 ? MessageBoxIcon.Warning : MessageBoxIcon.Information) != DialogResult.Yes)
                return;

            try
            {
                System.Diagnostics.Process.Start("explorer.exe", "/select,\"" + result.ScriptFile + "\"");
            }
            catch (Exception ex)
            {
                AppLogger.Error("Could not open the script folder", ex);
            }
        }

        // ---- shared --------------------------------------------------------

        private void SetBusy(bool busy, string message)
        {
            _busy = busy;
            lblLoading.Text = message;
            pnlLoading.Visible = busy;
            if (busy) pnlLoading.BringToFront(); else dgvRows.BringToFront();

            btnDownload.Enabled = !busy;
            btnBrowse.Enabled = !busy;
            btnReload.Enabled = !busy;
            btnSelectAll.Enabled = !busy;
            btnSelectNone.Enabled = !busy;
            btnInsert.Enabled = !busy;
        }

        private void UpdateSummary()
        {
            // Name the two databases: every lookup and every insert goes there, and a
            // template built for another store resolves to nothing.
            string target = $"{AppSession.BLogicPOS7DatabaseName} + {AppSession.MerchantDatabaseName}";

            if (_rows.Count == 0)
            {
                lblSummary.Text = $"{target}   ·   no rows loaded   ·   " +
                                  "Download template to start a new file, or Browse... to open one.";
                return;
            }

            int ready = _rows.Count(r => r.State == TicketTemplateRowState.Ready);
            int selected = _rows.Count(r => r.Selected && r.State == TicketTemplateRowState.Ready);
            int invalid = _rows.Count(r => r.State == TicketTemplateRowState.Invalid);
            int inserted = _rows.Count(r => r.State == TicketTemplateRowState.Inserted);
            int failed = _rows.Count(r => r.State == TicketTemplateRowState.Failed);

            var parts = new List<string>
            {
                target,
                $"{_rows.Count} row(s)",
                $"{ready} ready",
                $"{selected} selected ({Money(_rows.Where(r => r.Selected && r.State == TicketTemplateRowState.Ready).Sum(r => r.TranTotal))})"
            };
            if (_service.SchemaNotes.Count > 0) parts.Add($"{_service.SchemaNotes.Count} schema warning(s)");
            if (invalid > 0) parts.Add($"{invalid} invalid");
            if (inserted > 0) parts.Add($"{inserted} inserted");
            if (failed > 0) parts.Add($"{failed} failed");

            lblSummary.Text = string.Join("   ·   ", parts);
        }

        private static string Money(decimal value)
        {
            return (value < 0 ? "-$" : "$") + Math.Abs(value).ToString("N2");
        }

        private static void StyleGrid(DataGridView grid)
        {
            grid.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            grid.GridColor = Theme.BorderLine;
            grid.BackgroundColor = Color.White;
            grid.DefaultCellStyle.Font = new Font("Segoe UI", 10F);
            grid.DefaultCellStyle.SelectionBackColor = Theme.NavHover;
            grid.DefaultCellStyle.SelectionForeColor = Theme.NavText;
            grid.ColumnHeadersDefaultCellStyle.BackColor = Theme.SidebarBg;
            grid.ColumnHeadersDefaultCellStyle.SelectionBackColor = Theme.SidebarBg;
            grid.ColumnHeadersDefaultCellStyle.SelectionForeColor = Theme.NavText;
            grid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            grid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            grid.EnableHeadersVisualStyles = false;
            grid.RowTemplate.Height = 34;
            grid.RowTemplate.MinimumHeight = 34;
        }
    }
}
