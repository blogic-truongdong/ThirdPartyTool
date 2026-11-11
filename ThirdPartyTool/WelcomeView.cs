using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ThirdPartyTool
{
    public partial class WelcomeView : UserControl
    {
        public WelcomeView()
        {
            InitializeComponent();

            // Newest first. Add new releases at the top.
            timeline.SetReleases(new[]
            {
                new ReleaseNote("2.00", "11/07/2025", "Update item type for tickets."),
                new ReleaseNote("1.03", "10/28/2025", "Close ticket by Ticket# (Receipt › Close Ticket)."),
                new ReleaseNote("1.00", "", "Adjust tip.", "Sync role for employee.", "Settle batch offline."),
            });
        }
    }

    public class ReleaseNote
    {
        public ReleaseNote(string version, string date, params string[] items)
        {
            Version = version;
            Date = date;
            Items = items;
        }

        public string Version { get; }
        public string Date { get; }
        public string[] Items { get; }
    }

    /// <summary>Vertical timeline of release notes, painted in one pass and scrollable.</summary>
    public class ReleaseTimeline : Panel
    {
        private static readonly Color Primary = Color.FromArgb(42, 108, 228);
        private static readonly Color PrimaryTint = Color.FromArgb(232, 240, 254);
        private static readonly Color LineColor = Color.FromArgb(224, 224, 224);
        private static readonly Color TitleColor = Color.FromArgb(33, 33, 33);
        private static readonly Color BodyColor = Color.FromArgb(66, 66, 66);
        private static readonly Color MutedColor = Color.FromArgb(117, 117, 117);
        private static readonly Font VersionFont = new Font("Roboto", 12F, FontStyle.Bold);
        private static readonly Font DateFont = new Font("Roboto", 9.75F);
        private static readonly Font ItemFont = new Font("Roboto", 10.5F);
        private static readonly Font BadgeFont = new Font("Roboto", 8F, FontStyle.Bold);

        private const int PadLeft = 24, PadTop = 24, PadRight = 24, PadBottom = 24;
        private const int DotX = PadLeft + 6;          // center of the timeline dot
        private const int TextX = PadLeft + 32;        // left edge of the entry text
        private const int TitleHeight = 24, ItemGap = 6, EntryGap = 28, MaxTextWidth = 640;

        private const TextFormatFlags ItemFlags = TextFormatFlags.WordBreak | TextFormatFlags.NoPrefix | TextFormatFlags.Left | TextFormatFlags.Top;

        private IList<ReleaseNote> releases = new ReleaseNote[0];

        public ReleaseTimeline()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true);
        }

        public void SetReleases(IList<ReleaseNote> notes)
        {
            releases = notes ?? new ReleaseNote[0];
            UpdateScrollSize();
            Invalidate();
        }

        protected override void OnResize(EventArgs eventargs)
        {
            base.OnResize(eventargs);
            UpdateScrollSize();
        }

        protected override void OnScroll(ScrollEventArgs se)
        {
            base.OnScroll(se);
            Invalidate();
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);
            Invalidate();
        }

        private int TextWidth
        {
            get { return Math.Max(160, Math.Min(MaxTextWidth, ClientSize.Width - TextX - PadRight)); }
        }

        private int ItemHeight(Graphics g, string text, int width)
        {
            return TextRenderer.MeasureText(g, text, ItemFont, new Size(width, int.MaxValue), ItemFlags).Height;
        }

        private void UpdateScrollSize()
        {
            using (Graphics g = CreateGraphics())
            {
                int y = PadTop;
                int width = TextWidth - 14;
                foreach (ReleaseNote note in releases)
                {
                    y += TitleHeight + 8;
                    foreach (string item in note.Items)
                        y += ItemHeight(g, item, width) + ItemGap;
                    y += EntryGap;
                }
                AutoScrollMinSize = new Size(0, y - EntryGap + PadBottom);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(BackColor);
            g.TranslateTransform(AutoScrollPosition.X, AutoScrollPosition.Y);

            int width = TextWidth;
            int y = PadTop;
            List<int> dotYs = new List<int>();

            for (int i = 0; i < releases.Count; i++)
            {
                ReleaseNote note = releases[i];
                bool latest = i == 0;
                int titleTop = y;
                dotYs.Add(titleTop + TitleHeight / 2);

                string version = "Version " + note.Version;
                Size versionSize = TextRenderer.MeasureText(g, version, VersionFont);
                TextRenderer.DrawText(g, version, VersionFont, new Point(TextX, titleTop), TitleColor, TextFormatFlags.NoPrefix);
                int x = TextX + versionSize.Width + 4;

                if (latest)
                {
                    Size badgeSize = TextRenderer.MeasureText(g, "LATEST", BadgeFont);
                    Rectangle badge = new Rectangle(x, titleTop + 3, badgeSize.Width + 6, 18);
                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    using (GraphicsPath path = RoundedRect(badge, 4))
                    using (SolidBrush brush = new SolidBrush(PrimaryTint))
                        g.FillPath(brush, path);
                    g.SmoothingMode = SmoothingMode.Default;
                    TextRenderer.DrawText(g, "LATEST", BadgeFont, badge, Primary, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.NoPrefix);
                    x = badge.Right + 8;
                }

                if (!string.IsNullOrEmpty(note.Date))
                    TextRenderer.DrawText(g, note.Date, DateFont, new Rectangle(x, titleTop, 200, TitleHeight), MutedColor, TextFormatFlags.VerticalCenter | TextFormatFlags.NoPrefix);

                y = titleTop + TitleHeight + 8;
                foreach (string item in note.Items)
                {
                    int h = ItemHeight(g, item, width - 14);
                    using (SolidBrush bullet = new SolidBrush(MutedColor))
                        g.FillEllipse(bullet, TextX + 1, y + 8, 4, 4);
                    TextRenderer.DrawText(g, item, ItemFont, new Rectangle(TextX + 14, y, width - 14, h), BodyColor, ItemFlags);
                    y += h + ItemGap;
                }
                y += EntryGap;
            }

            // Timeline: a line through all dots, then the dots on top.
            g.SmoothingMode = SmoothingMode.AntiAlias;
            if (dotYs.Count > 1)
                using (Pen line = new Pen(LineColor, 2))
                    g.DrawLine(line, DotX, dotYs[0], DotX, dotYs[dotYs.Count - 1]);
            for (int i = 0; i < dotYs.Count; i++)
            {
                Rectangle dot = new Rectangle(DotX - 6, dotYs[i] - 6, 12, 12);
                using (SolidBrush fill = new SolidBrush(i == 0 ? Primary : Color.White))
                    g.FillEllipse(fill, dot);
                using (Pen ring = new Pen(i == 0 ? Primary : Color.FromArgb(189, 189, 189), 2))
                    g.DrawEllipse(ring, dot);
            }
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
    }
}
