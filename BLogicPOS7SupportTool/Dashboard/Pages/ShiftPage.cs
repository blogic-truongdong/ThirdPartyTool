using BLogicPOS7SupportTool.Core;
using BLogicPOS7SupportTool.Dashboard.Dialogs;
using BLogicPOS7SupportTool.Services;
using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BLogicPOS7SupportTool.Dashboard.Pages
{
    public partial class ShiftPage : UserControl
    {
        private readonly ShiftService _service = new ShiftService();

        public ShiftPage()
        {
            InitializeComponent();
            StyleGrid(dgvShifts);

            btnSearch.Click += BtnSearch_Click;
            dgvShifts.CellContentClick += DgvShifts_CellContentClick;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            dtpStart.Value = DateTime.Today;
            dtpEnd.Value = DateTime.Today;
            LoadShifts();
        }

        private void BtnSearch_Click(object sender, EventArgs e) => LoadShifts();

        private void LoadShifts()
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

            Task.Run(() => _service.GetShifts(start, end))
                .ContinueWith(t =>
                {
                    if (t.IsFaulted)
                    {
                        AppLogger.Error("LoadShifts failed", t.Exception);
                        pnlLoading.Visible = false;
                        MessageBox.Show("Failed to load shifts. Please try again.",
                            "Shifts", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    dgvShifts.DataSource = t.Result;
                    lblCount.Text = t.Result.Rows.Count + (t.Result.Rows.Count == 1 ? " shift" : " shifts");
                    pnlLoading.Visible = false;
                }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private void DgvShifts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || dgvShifts.Columns[e.ColumnIndex].Name != "colEditButton")
                return;

            var drv = dgvShifts.Rows[e.RowIndex].DataBoundItem as DataRowView;
            if (drv == null) return;

            var row = drv.Row;
            string shiftId = row["ShiftID"]?.ToString();
            string workstation = row["WorkstationName"]?.ToString();
            string openingTime = FormatDate(row["OpeningTime"]);
            string closingTime = FormatDate(row["ClosingTime"]);
            decimal openingAmount = row["OpeningAmount"] == DBNull.Value ? 0m : Convert.ToDecimal(row["OpeningAmount"]);
            decimal closingAmount = row["ClosingAmount"] == DBNull.Value ? 0m : Convert.ToDecimal(row["ClosingAmount"]);

            using (var dlg = new EditShiftForm(workstation, openingTime, closingTime, openingAmount, closingAmount))
            {
                if (DimOverlay.ShowDialog(ParentForm, dlg) != DialogResult.OK)
                    return;

                try
                {
                    bool ok = _service.UpdateShift(shiftId, dlg.OpeningAmount, dlg.ClosingAmount);
                    if (ok)
                    {
                        MessageBox.Show("Shift updated successfully.", "Done",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadShifts();
                    }
                    else
                    {
                        MessageBox.Show("No changes were made — the shift could not be found.", "Update Shift",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    AppLogger.Error($"UpdateShift failed for ShiftID={shiftId}", ex);
                    MessageBox.Show($"Update failed:\n{ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private static string FormatDate(object value)
        {
            return value is DateTime dt ? dt.ToString("M/d/yyyy h:mm tt", CultureInfo.CurrentCulture) : "";
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
