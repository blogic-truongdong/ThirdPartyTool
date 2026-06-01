using BLogicPOS7SupportTool.Core;
using BLogicPOS7SupportTool.Dashboard.Dialogs;
using BLogicPOS7SupportTool.Services;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace BLogicPOS7SupportTool.Dashboard.Pages
{
    public partial class CashInOutPage : UserControl
    {
        private readonly CashInOutService _service = new CashInOutService();

        public CashInOutPage()
        {
            InitializeComponent();
            StyleGrid(dgvCashInOut);

            btnSearch.Click += BtnSearch_Click;
            dgvCashInOut.CellContentClick += DgvCashInOut_CellContentClick;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            dtpStart.Value = DateTime.Today;
            dtpEnd.Value = DateTime.Today;
            LoadEntries();
        }

        private void BtnSearch_Click(object sender, EventArgs e) => LoadEntries();

        private void LoadEntries()
        {
            var start = dtpStart.Value.Date;
            var end = dtpEnd.Value.Date;

            if (start > end)
            {
                MessageBox.Show("Start Date must be before or equal to End Date.",
                    "Invalid Range", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            pnlLoading.Visible = true;
            pnlLoading.BringToFront();

            Task.Run(() => _service.GetCashInOuts(start, end))
                .ContinueWith(t =>
                {
                    if (t.IsFaulted)
                    {
                        AppLogger.Error("LoadEntries (Cash In/Out) failed", t.Exception);
                        pnlLoading.Visible = false;
                        MessageBox.Show("Failed to load Cash In/Out entries. Please try again.",
                            "Cash In / Out", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    dgvCashInOut.DataSource = t.Result;
                    lblCount.Text = t.Result.Rows.Count + (t.Result.Rows.Count == 1 ? " entry" : " entries");
                    pnlLoading.Visible = false;
                }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private void DgvCashInOut_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || dgvCashInOut.Columns[e.ColumnIndex].Name != "colEditButton")
                return;

            var drv = dgvCashInOut.Rows[e.RowIndex].DataBoundItem as DataRowView;
            if (drv == null) return;

            var row = drv.Row;
            string dropPayoutId = row["DropPayoutID"]?.ToString();
            string employee = row["Employee"]?.ToString();
            short type = row["Type"] == DBNull.Value ? CashInOutService.TypeCashOut : Convert.ToInt16(row["Type"]);
            decimal amount = row["Amount"] == DBNull.Value ? 0m : Convert.ToDecimal(row["Amount"]);
            string comment = row["Comment"]?.ToString();

            using (var dlg = new EditCashInOutForm(employee, type, amount, comment))
            {
                if (DimOverlay.ShowDialog(ParentForm, dlg) != DialogResult.OK)
                    return;

                try
                {
                    bool ok = _service.UpdateCashInOut(dropPayoutId, dlg.Type, dlg.Amount, dlg.Comment);
                    if (ok)
                    {
                        MessageBox.Show("Entry updated successfully.", "Done",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadEntries();
                    }
                    else
                    {
                        MessageBox.Show("No changes were made — the entry could not be found.", "Update Cash In/Out",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    AppLogger.Error($"UpdateCashInOut failed for DropPayoutID={dropPayoutId}", ex);
                    MessageBox.Show($"Update failed:\n{ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private static void StyleGrid(DataGridView grid)
        {
            grid.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            grid.GridColor = Theme.BorderLine;
            grid.BackgroundColor = Color.White;
            grid.DefaultCellStyle.Font = new Font("Segoe UI", 11F);
            grid.DefaultCellStyle.SelectionBackColor = Theme.NavHover;
            grid.DefaultCellStyle.SelectionForeColor = Theme.NavText;
            grid.ColumnHeadersDefaultCellStyle.BackColor = Theme.SidebarBg;
            grid.ColumnHeadersDefaultCellStyle.SelectionBackColor = Theme.SidebarBg;
            grid.ColumnHeadersDefaultCellStyle.SelectionForeColor = Theme.NavText;
            grid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            grid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            grid.EnableHeadersVisualStyles = false;
            grid.RowTemplate.Height = 40;
            grid.RowTemplate.MinimumHeight = 40;
        }
    }
}
