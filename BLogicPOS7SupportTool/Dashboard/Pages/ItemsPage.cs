using BLogicPOS7SupportTool.Core;
using BLogicPOS7SupportTool.Dashboard.Dialogs;
using BLogicPOS7SupportTool.Models;
using BLogicPOS7SupportTool.Services;
using System;
using System.Data;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BLogicPOS7SupportTool.Dashboard.Pages
{
    public partial class ItemsPage : UserControl
    {
        private readonly ItemService _service = new ItemService();

        public ItemsPage()
        {
            InitializeComponent();
            StyleGrid(dgvItems);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            LoadDepartments();
        }

        private void LoadDepartments()
        {
            pnlLoadingDepts.Visible = true;
            pnlLoadingDepts.BringToFront();

            Task.Run(() => _service.GetDepartments())
                .ContinueWith(t =>
                {
                    lstDepartments.DataSource = t.Result;
                    lstDepartments.DisplayMember = "DepartmentName";
                    lstDepartments.ValueMember = "DepartmentID";
                    pnlLoadingDepts.Visible = false;
                }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private void lstDepartments_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstDepartments.SelectedItem is DepartmentEntity dept)
                LoadItems(dept.DepartmentID, txtSearch.Text.Trim());
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (lstDepartments.SelectedItem is DepartmentEntity dept)
                LoadItems(dept.DepartmentID, txtSearch.Text.Trim());
        }

        private void LoadItems(int departmentId, string search)
        {
            pnlLoading.Visible = true;
            pnlLoading.BringToFront();

            Task.Run(() => _service.GetItems(departmentId, search))
                .ContinueWith(t =>
                {
                    dgvItems.DataSource = t.Result;
                    pnlLoading.Visible = false;
                }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private void dgvItems_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || dgvItems.Columns[e.ColumnIndex].Name != "colSyncButton")
                return;

            var drv = dgvItems.Rows[e.RowIndex].DataBoundItem as DataRowView;
            if (drv == null) return;

            string itemId = drv.Row["ItemID"]?.ToString();
            string itemGroupId = drv.Row["ItemGroupID"]?.ToString();
            string itemName = drv.Row["ItemName"]?.ToString();

            using (var dlg = new SyncItemGroupDialog())
            {
                dlg.Text = $"Sync — {itemName}";
                if (DimOverlay.ShowDialog(ParentForm, dlg) != DialogResult.OK) return;

                try
                {
                    _service.SyncItemGroup(itemId, itemGroupId, dlg.StartDate, dlg.EndDate);
                    MessageBox.Show("Sync completed successfully.", "Done",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    AppLogger.Error($"SyncItemGroup failed for ItemID={itemId}", ex);
                    MessageBox.Show($"Sync failed:\n{ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void lstDepartments_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;

            bool selected = (e.State & DrawItemState.Selected) == DrawItemState.Selected;

            using (var brush = new SolidBrush(selected ? Theme.PrimaryBlue : Theme.SidebarBg))
                e.Graphics.FillRectangle(brush, e.Bounds);

            var text = lstDepartments.Items[e.Index]?.ToString() ?? "";
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
