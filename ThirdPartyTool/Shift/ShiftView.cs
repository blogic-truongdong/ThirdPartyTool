using BLogicSystems.XPos.ClientUtilities;
using BLogicSystems.XPos.Utilites;
using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ThirdPartyTool.Shift
{
    public partial class ShiftView : UserControl
    {
        private const string EmptyText = "—"; // em dash for missing values

        private static readonly Color MutedText = Color.FromArgb(158, 158, 158);
        private static readonly Color OpenPillBack = Color.FromArgb(232, 245, 233);
        private static readonly Color OpenPillText = Color.FromArgb(46, 125, 50);
        private static readonly Color ClosedPillBack = Color.FromArgb(238, 238, 238);
        private static readonly Color ClosedPillText = Color.FromArgb(97, 97, 97);

        DataTable dtShifts = null;

        public ShiftView()
        {
            InitializeComponent();

            btnView.Click += BtnView_Click;
            dgvShifts.CellClick += DgvShifts_CellClick;
            dgvShifts.CellDoubleClick += DgvShifts_CellDoubleClick;
            dgvShifts.CellFormatting += DgvShifts_CellFormatting;
            dgvShifts.CellPainting += DgvShifts_CellPainting;
            dgvShifts.CellMouseEnter += DgvShifts_CellMouseEnter;
            dgvShifts.CellMouseLeave += DgvShifts_CellMouseLeave;
            this.Load += ShiftView_Load;
        }

        private void ShiftView_Load(object sender, EventArgs e)
        {
            startDate.DateTimeValues = DateTime.Today;
            endDate.DateTimeValues = DateTime.Today;
            BtnView_Click(null, null);
        }

        #region Data

        private void BtnView_Click(object sender, System.EventArgs e)
        {
            try
            {
                // Use the dates shown in the pickers (DateTimeValues). The pickers' StartDate/EndDate
                // fields are fixed at control creation time and do not follow the selected day.
                DateTime from = startDate.DateTimeValues.Date;
                DateTime to = endDate.DateTimeValues.Date.AddDays(1).AddSeconds(-1);
                if (from > to)
                {
                    MessageBox.Show("From date must be on or before To date.", "Shifts",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                ProcessForm processForm = new ProcessForm();
                processForm.CurrentProcessName = ProcessNameEnum.ViewShifts;
                processForm.requestModel.StartDate = from;
                processForm.requestModel.EndDate = to;

                BLogicMessageBox.ShowPopUpFormWithDIMBackground(processForm);

                BindShifts(processForm.DataTableReturn);
            }
            catch (System.Exception ex)
            {
                BLogicLogger.Exception("ShiftView.BtnView_Click", ex);
            }
        }

        private void BindShifts(DataTable table)
        {
            dtShifts = table;
            dgvShifts.DataSource = dtShifts;
            dgvShifts.ClearSelection();
            UpdateSummary();
        }

        private void UpdateSummary()
        {
            int total = dtShifts?.Rows.Count ?? 0;
            int open = 0;
            if (dtShifts != null && dtShifts.Columns.Contains("ClosingTime"))
            {
                foreach (DataRow row in dtShifts.Rows)
                {
                    if (row["ClosingTime"] == DBNull.Value)
                        open++;
                }
            }

            lblCount.Text = total == 0
                ? ""
                : $"{total} {(total == 1 ? "shift" : "shifts")}  ·  {open} open";
            lblEmpty.Visible = total == 0;
            if (lblEmpty.Visible)
                lblEmpty.BringToFront();
        }

        #endregion

        #region Edit

        private void DgvShifts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != EditShift.Index)
                return;
            EditShiftAt(e.RowIndex);
        }

        private void DgvShifts_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex == EditShift.Index)
                return;
            EditShiftAt(e.RowIndex);
        }

        private void EditShiftAt(int rowIndex)
        {
            try
            {
                DataGridViewRow gridRow = dgvShifts.Rows[rowIndex];
                string shiftID = Utility.GetStringFromObject(gridRow.Cells["ShiftID"].Value);
                decimal openingAmount = Utility.GetDecimalFromObject(gridRow.Cells["OpeningAmount"].Value);
                decimal closingAmount = Utility.GetDecimalFromObject(gridRow.Cells["ClosingAmount"].Value);
                UpdateShiftForm updateShift = new UpdateShiftForm();
                updateShift.txtOpeningAmount.Value = openingAmount;
                updateShift.txtClosingAmount.Value = closingAmount;
                if (BLogicMessageBox.ShowPopUpFormWithDIMBackground(updateShift, Helper.MainForm) == DialogResult.OK)
                {
                    ProcessForm processForm = new ProcessForm();
                    processForm.CurrentProcessName = ProcessNameEnum.UpdateShift;
                    processForm.requestModel.ShiftID = shiftID;
                    processForm.requestModel.OpeningAmount = updateShift.txtOpeningAmount.Value;
                    processForm.requestModel.ClosingAmount = updateShift.txtClosingAmount.Value;
                    BLogicMessageBox.ShowPopUpFormWithDIMBackground(processForm);

                    if (processForm.IsSuccess)
                    {
                        DataRow[] drs = dtShifts.Select($"ShiftID = '{shiftID}'", "");
                        if (drs.Length > 0)
                        {
                            drs[0]["OpeningAmount"] = updateShift.txtOpeningAmount.Value;
                            drs[0]["ClosingAmount"] = updateShift.txtClosingAmount.Value;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                BLogicLogger.Exception("ShiftView.EditShiftAt", ex);
            }
        }

        #endregion

        #region Grid look

        private static bool IsBlank(object value)
        {
            return value == null || value == DBNull.Value || string.IsNullOrWhiteSpace(value.ToString());
        }

        private bool IsOpenShift(int rowIndex)
        {
            return IsBlank(dgvShifts.Rows[rowIndex].Cells["ClosingTime"].Value);
        }

        private void DgvShifts_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            if (e.ColumnIndex == Status.Index)
            {
                e.Value = IsOpenShift(e.RowIndex) ? "Open" : "Closed";
                e.FormattingApplied = true;
            }
            else if ((e.ColumnIndex == WorkstationName.Index || e.ColumnIndex == ClosingTime.Index) && IsBlank(e.Value))
            {
                e.Value = EmptyText;
                e.CellStyle.ForeColor = MutedText;
                e.CellStyle.SelectionForeColor = MutedText;
                e.CellStyle.Font = dgvShifts.DefaultCellStyle.Font;
                e.FormattingApplied = true;
            }
        }

        private void DgvShifts_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != Status.Index)
                return;

            e.Paint(e.CellBounds, DataGridViewPaintParts.Background | DataGridViewPaintParts.SelectionBackground | DataGridViewPaintParts.Border);

            bool isOpen = IsOpenShift(e.RowIndex);
            string text = isOpen ? "Open" : "Closed";
            Color back = isOpen ? OpenPillBack : ClosedPillBack;
            Color fore = isOpen ? OpenPillText : ClosedPillText;

            Font font = e.CellStyle.Font;
            Size textSize = TextRenderer.MeasureText(e.Graphics, text, font, Size.Empty, TextFormatFlags.NoPadding);
            int pillWidth = textSize.Width + 20;
            int pillHeight = textSize.Height + 8;
            Rectangle pill = new Rectangle(
                e.CellBounds.X + (e.CellBounds.Width - pillWidth) / 2,
                e.CellBounds.Y + (e.CellBounds.Height - pillHeight) / 2,
                pillWidth,
                pillHeight);

            SmoothingMode oldMode = e.Graphics.SmoothingMode;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            using (GraphicsPath path = RoundedRect(pill, pillHeight / 2))
            using (SolidBrush brush = new SolidBrush(back))
            {
                e.Graphics.FillPath(brush, path);
            }
            e.Graphics.SmoothingMode = oldMode;

            TextRenderer.DrawText(e.Graphics, text, font, pill, fore,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.NoPadding);

            e.Handled = true;
        }

        private static GraphicsPath RoundedRect(Rectangle r, int radius)
        {
            int d = Math.Max(1, radius * 2);
            GraphicsPath path = new GraphicsPath();
            path.AddArc(r.X, r.Y, d, d, 180, 90);
            path.AddArc(r.Right - d, r.Y, d, d, 270, 90);
            path.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90);
            path.AddArc(r.X, r.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }

        private void DgvShifts_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == EditShift.Index)
                dgvShifts.Cursor = Cursors.Hand;
        }

        private void DgvShifts_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            dgvShifts.Cursor = Cursors.Default;
        }

        #endregion
    }
}
