using BLogicPOS7SupportTool.Core;
using BLogicPOS7SupportTool.Models;
using BLogicPOS7SupportTool.Services;
using System;
using System.Data;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BLogicPOS7SupportTool.Dashboard.Pages
{
    public partial class ClearTableStatusPage : UserControl
    {
        private readonly TableService _service = new TableService();

        public ClearTableStatusPage()
        {
            InitializeComponent();
            StyleGrid(dgvTables);

            dgvTables.CellClick += DgvTables_CellClick;
        }

        // Don't keep a row highlighted after it's clicked.
        private void DgvTables_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvTables.ClearSelection();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            LoadAreas();
        }

        private void LoadAreas()
        {
            pnlLoadingAreas.Visible = true;
            pnlLoadingAreas.BringToFront();

            Task.Run(() => _service.GetAreas())
                .ContinueWith(t =>
                {
                    lstAreas.DataSource = t.Result;
                    lstAreas.DisplayMember = "AreaName";
                    lstAreas.ValueMember = "AreaID";
                    pnlLoadingAreas.Visible = false;
                }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private void lstAreas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstAreas.SelectedItem is TableAreaEntity area)
                LoadTables(area.AreaID, txtSearch.Text.Trim());
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (lstAreas.SelectedItem is TableAreaEntity area)
                LoadTables(area.AreaID, txtSearch.Text.Trim());
        }

        private void LoadTables(string areaId, string search)
        {
            pnlLoading.Visible = true;
            pnlLoading.BringToFront();

            Task.Run(() => _service.GetTables(areaId, search))
                .ContinueWith(t =>
                {
                    dgvTables.DataSource = t.Result;
                    pnlLoading.Visible = false;
                }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private void dgvTables_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || dgvTables.Columns[e.ColumnIndex].Name != "colClearButton")
                return;

            var drv = dgvTables.Rows[e.RowIndex].DataBoundItem as DataRowView;
            if (drv == null) return;

            string tableId = drv.Row["TableID"]?.ToString();
            string tableName = drv.Row["TableName"]?.ToString();

            var confirm = MessageBox.Show(
                $"Clear table \"{tableName}\"?\n\n" +
                "This resets the table to Available: it creates a fresh copy of the table " +
                "(new internal ID, Status = Available) and archives the current record.\n\n" +
                "Use this to release a table that is stuck as occupied. This cannot be undone.",
                "Clear Table Status", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm != DialogResult.Yes) return;

            try
            {
                bool ok = _service.ClearTable(tableId);
                if (ok)
                {
                    MessageBox.Show($"Table \"{tableName}\" has been cleared.", "Done",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ReloadCurrentArea();
                }
                else
                {
                    MessageBox.Show("The table could not be cleared — it may have already been removed.",
                        "Clear Table Status", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                AppLogger.Error($"ClearTable failed for TableID={tableId}", ex);
                MessageBox.Show($"Clear failed:\n{ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ReloadCurrentArea()
        {
            if (lstAreas.SelectedItem is TableAreaEntity area)
                LoadTables(area.AreaID, txtSearch.Text.Trim());
        }

        private void lstAreas_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;

            bool selected = (e.State & DrawItemState.Selected) == DrawItemState.Selected;

            using (var brush = new SolidBrush(selected ? Theme.PrimaryBlue : Theme.SidebarBg))
                e.Graphics.FillRectangle(brush, e.Bounds);

            var text = lstAreas.Items[e.Index]?.ToString() ?? "";
            using (var brush = new SolidBrush(selected ? Color.White : Theme.NavText))
            {
                var fmt = new StringFormat { LineAlignment = StringAlignment.Center };
                e.Graphics.DrawString(text, e.Font, brush,
                    new RectangleF(e.Bounds.X + 12, e.Bounds.Y, e.Bounds.Width - 12, e.Bounds.Height),
                    fmt);
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
