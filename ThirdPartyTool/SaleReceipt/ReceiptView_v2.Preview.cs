using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Reflection;
using System.Windows.Forms;

namespace ThirdPartyTool.SaleReceipt
{
    /// <summary>
    /// Left column of the receipt screen: the search box and the receipt preview. The preview sits in a rounded
    /// light-gray card so the receipt reads as a sheet of paper; when no ticket is loaded the card shows a hint
    /// instead of an empty white box.
    /// </summary>
    public partial class ReceiptView_v2
    {
        private static readonly Color PreviewBack = Color.FromArgb(243, 244, 246);
        private static readonly Color PreviewBorder = Color.FromArgb(229, 231, 235);
        private static readonly Color FieldBorder = Color.FromArgb(209, 213, 219);
        private static readonly Color PreviewMuted = Color.FromArgb(107, 114, 128);
        private static readonly Color AccentBlue = Color.FromArgb(42, 108, 228);

        private PreviewCard previewCard;
        private EmptyReceiptState previewEmpty;

        private void InitReceiptPreview()
        {
            // Search box: same border and radius as the other inputs, flat search glyph instead of the big bitmap.
            pnlSearch.BorderColor = FieldBorder;
            pnlSearch.BorderRadius = 8;
            txtSearch.Font = new Font("Roboto", 11F);
            btnSearch.Image = null;
            btnSearch.Font = new Font("Segoe MDL2 Assets", 12F);
            btnSearch.Text = "";
            btnSearch.TextAlign = ContentAlignment.MiddleCenter;
            btnSearch.ForeColor = PreviewMuted;
            btnSearch.MouseEnter += (s, e) => btnSearch.ForeColor = AccentBlue;
            btnSearch.MouseLeave += (s, e) => btnSearch.ForeColor = PreviewMuted;

            // Move the preview into the card.
            TableLayoutPanelCellPosition cell = tableLayoutPanel1.GetCellPosition(PrintControl_Receipt);
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel1.Controls.Remove(PrintControl_Receipt);

            previewCard = new PreviewCard
            {
                Dock = DockStyle.Fill,
                Margin = new Padding(16, 0, 14, 16),
                Padding = new Padding(10)
            };

            PrintControl_Receipt.Dock = DockStyle.Fill;
            PrintControl_Receipt.Margin = Padding.Empty;
            PrintControl_Receipt.BorderStyle = BorderStyle.None;
            PrintControl_Receipt.BackColor = PreviewBack;
            PrintControl_Receipt.ShowPageMargins = false;
            PrintControl_Receipt.SelectedPageBorderWidth = 1;
            PrintControl_Receipt.SelectedPageBorderColor = FieldBorder;
            PrintControl_Receipt.LookAndFeel.UseDefaultLookAndFeel = false;
            PrintControl_Receipt.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            PrintControl_Receipt.LookAndFeel.UseWindowsXPTheme = true;
            HidePreviewScrollBar("hPanel");

            previewEmpty = new EmptyReceiptState { Dock = DockStyle.Fill };

            previewCard.Controls.Add(PrintControl_Receipt);
            previewCard.Controls.Add(previewEmpty);
            previewEmpty.BringToFront();

            tableLayoutPanel1.Controls.Add(previewCard, cell.Column, cell.Row);
            tableLayoutPanel1.ResumeLayout(true);
        }

        /// <summary>The receipt is zoomed to the page width, so the horizontal bar (and the strip holding it) only takes space.</summary>
        private void HidePreviewScrollBar(string fieldName)
        {
            try
            {
                FieldInfo field = typeof(DevExpress.XtraPrinting.Control.PrintControl).GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic);
                Control bar = field?.GetValue(PrintControl_Receipt) as Control;
                if (bar == null)
                    return;
                bar.Visible = false;
                bar.VisibleChanged += (s, e) => { if (bar.Visible) bar.Visible = false; };
            }
            catch (Exception ex)
            {
                BLogicLogger.Exception("HidePreviewScrollBar in ReceiptView_v2", ex);
            }
        }

        /// <summary>Shows the hint when no receipt is loaded, the receipt otherwise.</summary>
        private void SetPreviewEmpty(bool empty)
        {
            if (previewEmpty == null)
                return;
            previewEmpty.Visible = empty;
            if (empty)
                previewEmpty.BringToFront();
        }

        internal static GraphicsPath RoundedPath(RectangleF rect, float radius)
        {
            GraphicsPath path = new GraphicsPath();
            float d = radius * 2;
            path.AddArc(rect.X, rect.Y, d, d, 180, 90);
            path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
            path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
            path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }

        /// <summary>Rounded light-gray card; children sit inside its padding so the corners stay smooth.</summary>
        private sealed class PreviewCard : Panel
        {
            public PreviewCard()
            {
                SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.ResizeRedraw, true);
                BackColor = Color.White;
            }

            protected override void OnPaint(PaintEventArgs e)
            {
                Graphics g = e.Graphics;
                g.Clear(Parent?.BackColor ?? Color.White);
                g.SmoothingMode = SmoothingMode.AntiAlias;
                RectangleF rect = new RectangleF(0.5f, 0.5f, Width - 1.5f, Height - 1.5f);
                using (GraphicsPath path = RoundedPath(rect, 10))
                using (SolidBrush fill = new SolidBrush(PreviewBack))
                using (Pen border = new Pen(PreviewBorder))
                {
                    g.FillPath(fill, path);
                    g.DrawPath(border, path);
                }
            }
        }

        /// <summary>Placeholder drawn in the card before a ticket is searched.</summary>
        private sealed class EmptyReceiptState : Control
        {
            public EmptyReceiptState()
            {
                SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.ResizeRedraw, true);
                BackColor = PreviewBack;
            }

            protected override void OnPaint(PaintEventArgs e)
            {
                Graphics g = e.Graphics;
                g.Clear(PreviewBack);
                g.SmoothingMode = SmoothingMode.AntiAlias;

                int paperW = 56, paperH = 68;
                int top = Math.Max(16, Height / 2 - 90);
                int x = (Width - paperW) / 2;

                // Paper with a torn (zigzag) bottom edge.
                using (GraphicsPath paper = new GraphicsPath())
                {
                    paper.AddLine(x, top + 6, x, top + paperH);
                    int teeth = 6;
                    float step = paperW / (float)teeth;
                    for (int i = 0; i < teeth; i++)
                    {
                        float x0 = x + i * step;
                        paper.AddLine(x0, top + paperH, x0 + step / 2, top + paperH - 6);
                        paper.AddLine(x0 + step / 2, top + paperH - 6, x0 + step, top + paperH);
                    }
                    paper.AddLine(x + paperW, top + paperH, x + paperW, top + 6);
                    paper.AddArc(x + paperW - 12, top, 12, 12, 0, -90);
                    paper.AddArc(x, top, 12, 12, 270, -90);
                    paper.CloseFigure();
                    using (SolidBrush fill = new SolidBrush(Color.White))
                    using (Pen border = new Pen(FieldBorder, 1.5f))
                    {
                        g.FillPath(fill, paper);
                        g.DrawPath(border, paper);
                    }
                }
                using (Pen line = new Pen(FieldBorder, 3f) { StartCap = LineCap.Round, EndCap = LineCap.Round })
                {
                    g.DrawLine(line, x + 12, top + 18, x + paperW - 12, top + 18);
                    g.DrawLine(line, x + 12, top + 28, x + paperW - 20, top + 28);
                    g.DrawLine(line, x + 12, top + 38, x + paperW - 16, top + 38);
                }
                using (Pen accent = new Pen(AccentBlue, 3f) { StartCap = LineCap.Round, EndCap = LineCap.Round })
                    g.DrawLine(accent, x + 12, top + 50, x + 26, top + 50);

                int textTop = top + paperH + 18;
                Rectangle titleRect = new Rectangle(12, textTop, Width - 24, 24);
                Rectangle hintRect = new Rectangle(20, textTop + 28, Width - 40, 44);
                using (Font title = new Font("Roboto", 11F, FontStyle.Bold))
                    TextRenderer.DrawText(g, "No receipt yet", title, titleRect, Color.FromArgb(55, 65, 81),
                        TextFormatFlags.HorizontalCenter | TextFormatFlags.Top);
                using (Font hint = new Font("Roboto", 9F))
                    TextRenderer.DrawText(g, "Search a ticket number above to preview its receipt.", hint, hintRect, PreviewMuted,
                        TextFormatFlags.HorizontalCenter | TextFormatFlags.Top | TextFormatFlags.WordBreak);
            }
        }
    }
}
