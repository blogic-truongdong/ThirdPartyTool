using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ThirdPartyTool.CustomControl
{
    /// <summary>
    /// Amber "read this first" box: a bold title and a bullet list.
    /// A line starting with "**" is drawn bold (the marker is not shown).
    /// The height follows the text, so it can be docked to the top.
    /// </summary>
    public class NoticeBox : Control
    {
        private static readonly Color Back = Color.FromArgb(255, 248, 225);
        private static readonly Color Accent = Color.FromArgb(245, 158, 11);
        private static readonly Color TitleColor = Color.FromArgb(133, 77, 14);
        private static readonly Color TextColor = Color.FromArgb(120, 53, 15);

        private const int PadLeft = 18, PadRight = 16, PadTop = 12, PadBottom = 12;
        private const int BulletIndent = 14, LineGap = 6, TitleGap = 8;
        private const TextFormatFlags Flags = TextFormatFlags.WordBreak | TextFormatFlags.NoPrefix | TextFormatFlags.Left | TextFormatFlags.Top;

        private string title = "";
        private string[] lines = new string[0];

        public NoticeBox()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true);
            Font = new Font("Roboto", 10F);
            TitleFont = new Font("Roboto", 10.5F, FontStyle.Bold);
            BoldFont = new Font("Roboto", 10F, FontStyle.Bold);
            BackColor = Back;
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Font TitleFont { get; set; }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Font BoldFont { get; set; }

        [DefaultValue("")]
        public string Title
        {
            get { return title; }
            set { title = value ?? ""; FitHeight(); Invalidate(); }
        }

        public string[] Lines
        {
            get { return lines; }
            set { lines = value ?? new string[0]; FitHeight(); Invalidate(); }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            FitHeight();
        }

        private int TextWidth
        {
            get { return Math.Max(80, Width - PadLeft - BulletIndent - PadRight); }
        }

        private static bool IsBold(string line, out string text)
        {
            bool bold = line != null && line.StartsWith("**");
            text = bold ? line.Substring(2) : (line ?? "");
            return bold;
        }

        private int Measure(Graphics g)
        {
            int y = PadTop;
            if (title.Length > 0)
                y += TextRenderer.MeasureText(g, title, TitleFont, new Size(Width - PadLeft - PadRight, int.MaxValue), Flags).Height + TitleGap;
            foreach (string line in lines)
            {
                string text;
                Font font = IsBold(line, out text) ? BoldFont : Font;
                y += TextRenderer.MeasureText(g, text, font, new Size(TextWidth, int.MaxValue), Flags).Height + LineGap;
            }
            return y - (lines.Length > 0 ? LineGap : 0) + PadBottom;
        }

        private void FitHeight()
        {
            if (!IsHandleCreated && Parent == null)
                return;
            using (Graphics g = CreateGraphics())
            {
                int height = Measure(g);
                if (height != Height)
                    Height = height;
            }
        }

        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);
            FitHeight();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(BackColor);
            using (SolidBrush bar = new SolidBrush(Accent))
                g.FillRectangle(bar, 0, 0, 4, Height);

            int y = PadTop;
            if (title.Length > 0)
            {
                Size size = TextRenderer.MeasureText(g, title, TitleFont, new Size(Width - PadLeft - PadRight, int.MaxValue), Flags);
                TextRenderer.DrawText(g, title, TitleFont, new Rectangle(PadLeft, y, Width - PadLeft - PadRight, size.Height), TitleColor, Flags);
                y += size.Height + TitleGap;
            }

            foreach (string line in lines)
            {
                string text;
                Font font = IsBold(line, out text) ? BoldFont : Font;
                Size size = TextRenderer.MeasureText(g, text, font, new Size(TextWidth, int.MaxValue), Flags);
                using (SolidBrush dot = new SolidBrush(TextColor))
                    g.FillEllipse(dot, PadLeft + 2, y + 7, 4, 4);
                TextRenderer.DrawText(g, text, font, new Rectangle(PadLeft + BulletIndent, y, TextWidth, size.Height), TextColor, Flags);
                y += size.Height + LineGap;
            }
        }
    }
}
