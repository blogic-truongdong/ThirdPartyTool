using BLogicSystems.XPos.Utilites;
using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using ThirdPartyTool.Dialog;

namespace ThirdPartyTool.Transactions
{
    public partial class SettleBatchOfflineView : UserControl
    {
        DataTable dtBatchDetail = null;

        private static readonly Color Primary = Color.FromArgb(42, 108, 228);
        private static readonly Color NoticeAccent = Color.FromArgb(245, 158, 11);
        private static readonly Color DisabledBack = Color.FromArgb(229, 231, 235);
        private static readonly Color DisabledText = Color.FromArgb(156, 163, 175);
        private int hoverActionRow = -1;

        public SettleBatchOfflineView()
        {
            InitializeComponent();

            btnSettle.Click += BtnSettle_Click;

            this.Load += SettleBatchOfflineForm_Load;

            btnRefresh.Click += BtnRefresh_Click;

            dgvBatch.CellClick += DgvBatch_CellClick;
            dgvBatch.CellPainting += DgvBatch_CellPainting;
            dgvBatch.CellMouseEnter += DgvBatch_CellMouseEnter;
            dgvBatch.CellMouseLeave += DgvBatch_CellMouseLeave;
            dgvBatch.DataBindingComplete += (s, e) => dgvBatch.ClearSelection();

            pnlNotice.Paint += PnlNotice_Paint;

            UpdateState();
        }

        private void PnlNotice_Paint(object sender, PaintEventArgs e)
        {
            using (SolidBrush brush = new SolidBrush(NoticeAccent))
                e.Graphics.FillRectangle(brush, 0, 0, 4, pnlNotice.Height);
        }

        private bool IsActionCell(int rowIndex, int columnIndex)
        {
            return rowIndex >= 0 && columnIndex == SettleBatch.Index;
        }

        private void DgvBatch_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (!IsActionCell(e.RowIndex, e.ColumnIndex))
                return;
            hoverActionRow = e.RowIndex;
            dgvBatch.Cursor = Cursors.Hand;
            dgvBatch.InvalidateCell(e.ColumnIndex, e.RowIndex);
        }

        private void DgvBatch_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (!IsActionCell(e.RowIndex, e.ColumnIndex))
                return;
            hoverActionRow = -1;
            dgvBatch.Cursor = Cursors.Default;
            dgvBatch.InvalidateCell(e.ColumnIndex, e.RowIndex);
        }

        /// <summary>Draws the per-row "Settle" action as an outlined button (filled on hover).</summary>
        private void DgvBatch_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (!IsActionCell(e.RowIndex, e.ColumnIndex))
                return;

            e.Paint(e.CellBounds, DataGridViewPaintParts.Background | DataGridViewPaintParts.Border | DataGridViewPaintParts.SelectionBackground);

            bool hover = e.RowIndex == hoverActionRow;
            Rectangle r = new Rectangle(e.CellBounds.X + (e.CellBounds.Width - 80) / 2, e.CellBounds.Y + (e.CellBounds.Height - 30) / 2, 80, 30);
            Graphics g = e.Graphics;
            SmoothingMode oldMode = g.SmoothingMode;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            using (GraphicsPath path = RoundedRect(r, 5))
            {
                using (SolidBrush fill = new SolidBrush(hover ? Primary : Color.White))
                    g.FillPath(fill, path);
                using (Pen pen = new Pen(Primary, 1))
                    g.DrawPath(pen, path);
            }
            g.SmoothingMode = oldMode;

            using (Font font = new Font("Roboto", 10F, FontStyle.Bold))
                TextRenderer.DrawText(g, "Settle", font, r, hover ? Color.White : Primary,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.SingleLine);

            e.Handled = true;
        }

        private static GraphicsPath RoundedRect(Rectangle r, int radius)
        {
            int d = radius * 2;
            GraphicsPath path = new GraphicsPath();
            path.AddArc(r.X, r.Y, d, d, 180, 90);
            path.AddArc(r.Right - d, r.Y, d, d, 270, 90);
            path.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90);
            path.AddArc(r.X, r.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }

        /// <summary>Refreshes the header count, empty state and the "Settle all" button look.</summary>
        private void UpdateState()
        {
            int merchants = dtBatchDetail?.Rows.Count ?? 0;
            int transactions = 0;
            if (dtBatchDetail != null && dtBatchDetail.Columns.Contains("CountTicket"))
                foreach (DataRow row in dtBatchDetail.Rows)
                    transactions += Utility.GetIntFromObject(row["CountTicket"]);

            lblCount.Text = dtBatchDetail == null
                ? ""
                : $"{merchants} {(merchants == 1 ? "merchant" : "merchants")}  ·  {transactions} {(transactions == 1 ? "transaction" : "transactions")}";

            bool hasRows = merchants > 0;
            lblEmpty.Visible = !hasRows;
            lblEmpty.BringToFront();

            Color back = hasRows ? Primary : DisabledBack;
            Color text = hasRows ? Color.White : DisabledText;
            btnSettle.BackColor = btnSettle.BackgroundColor = btnSettle.BorderColor = back;
            btnSettle.ForeColor = btnSettle.TextColor = text;
            btnSettle.Cursor = hasRows ? Cursors.Hand : Cursors.Default;
            btnSettle.Invalidate();
        }

        private void DgvBatch_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0)
                    return;

                if (e.ColumnIndex == dgvBatch.Columns["SettleBatch"].Index)
                {
                    ConfirmDialogForm confirmFirmDialog = new ConfirmDialogForm(Const.ConfirmTitle, "Are you sure you want to settle this batch?");
                    var result = confirmFirmDialog.ShowDialog();
                    if (result == DialogResult.No)
                        return;

                    ProcessForm processForm = new ProcessForm();
                    processForm.CurrentProcessName = ProcessNameEnum.SettleBatchOfflineByMerchantID;
                    processForm.MerchantID = Utility.GetStringFromObject(dgvBatch.Rows[e.RowIndex].Cells["MerchantID"].Value);
                    processForm.BatchID = Utility.GetStringFromObject(dgvBatch.Rows[e.RowIndex].Cells["BatchID"].Value);
                    processForm.ShowDialog();

                    if (processForm.IsSuccess)
                    {
                        InformationDialogForm infoDialog = new InformationDialogForm("Settle batch successfully.");
                        infoDialog.ShowDialog();

                        BtnRefresh_Click(sender, e);
                    }
                    else
                    {
                        InformationDialogForm infoDialog = new InformationDialogForm("Settle batch Fail. Please contact TruongDS");
                        infoDialog.ShowDialog();
                    }
                }
            }
            catch (Exception ex)
            {
                BLogicLogger.Exception("DgvBatch_CellClick in SettleBatchOfflineForm", ex);
            }
        }

        public void BtnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                ProcessForm processForm = new ProcessForm();
                processForm.InitGetRemainingBatch();
                processForm.ShowDialog();
                if (processForm.IsSuccess)
                {
                    dtBatchDetail = processForm.DataTableReturn.Copy();
                    dgvBatch.DataSource = dtBatchDetail;
                    UpdateState();
                }
                else
                {
                    InformationDialogForm infoDialog = new InformationDialogForm("Get Remaining Batch Fail. Please contact TruongDS");
                    infoDialog.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                BLogicLogger.Exception("BtnSettle_Click in SettleBatchOfflineForm", ex);
            }
        }

        private void SettleBatchOfflineForm_Load(object sender, EventArgs e)
        {

        }

        private void BtnSettle_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtBatchDetail?.Rows.Count > 0)
                {
                    ConfirmDialogForm confirmFirmDialog = new ConfirmDialogForm(Const.ConfirmTitle, "Are you sure you want to settle all batches?");
                    var result = confirmFirmDialog.ShowDialog();
                    if (result == DialogResult.No)
                        return;

                    ProcessForm processForm = new ProcessForm();
                    processForm.InitSettleBatchOffline();
                    processForm.ShowDialog();

                    if (processForm.IsSuccess)
                    {
                        InformationDialogForm infoDialog = new InformationDialogForm("All batches settled successfully.");
                        infoDialog.ShowDialog();

                        BtnRefresh_Click(sender, e);
                    }
                    else
                    {
                        InformationDialogForm infoDialog = new InformationDialogForm("Settle batches failed. Please contact TruongDS");
                        infoDialog.ShowDialog();
                    }
                }

            }
            catch (Exception ex)
            {
                BLogicLogger.Exception("BtnSettle_Click in SettleBatchOfflineForm", ex);
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {

        }


    }
}
