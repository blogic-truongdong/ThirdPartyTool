using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ThirdPartyTool.CustomControl
{
    public class BPanel_Rounded : Panel
    {
        public int BorderRadius { get; set; } = 5;
        public int BorderSize { get; set; } = 1;
        public Color BorderColor { get; set; } = Color.Gray;
        public Color BackgroundColor { get; set; } = Color.Transparent;

        public BPanel_Rounded()
        {
            this.DoubleBuffered = true;
            this.Resize += (s, e) => this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle rectSurface = this.ClientRectangle;
            Rectangle rectBorder = Rectangle.Inflate(rectSurface, -BorderSize, -BorderSize);

            using (GraphicsPath pathSurface = GetRoundPath(rectSurface, BorderRadius))
            using (GraphicsPath pathBorder = GetRoundPath(rectBorder, BorderRadius - BorderSize))
            using (Pen penBorder = new Pen(BorderColor, BorderSize))
            using (SolidBrush brush = new SolidBrush(BackgroundColor))
            {
                // Fill nền
                e.Graphics.FillPath(brush, pathSurface);

                // Áp dụng Region (bo góc luôn cho control + children)
                this.Region = new Region(pathSurface);

                // Vẽ border
                if (BorderSize > 0)
                    e.Graphics.DrawPath(penBorder, pathBorder);
            }
        }

        private GraphicsPath GetRoundPath(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            if (radius <= 0) { path.AddRectangle(rect); return path; }

            int d = radius * 2;
            path.StartFigure();
            path.AddArc(rect.X, rect.Y, d, d, 180, 90);
            path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
            path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
            path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }
    }
}
