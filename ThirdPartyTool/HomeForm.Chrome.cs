using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ThirdPartyTool
{
    /// <summary>
    /// Window chrome of the main window: no Windows title bar. The logo sits at the top of the sidebar and the
    /// minimize / maximize / close buttons in a slim bar above the page. Drag that bar (or the logo) to move,
    /// double-click it to maximize, drag the window edges to resize.
    /// </summary>
    public partial class HomeForm
    {
        private const int ResizeGrip = 6;
        private Panel pnlChrome;
        private ChromeButton btnMaximize;
        private ResizeFilter resizeFilter;

        private static readonly Color WindowBorder = Color.FromArgb(200, 203, 209);

        private void InitChrome()
        {
            FormBorderStyle = FormBorderStyle.None;
            BackColor = WindowBorder;
            Padding = new Padding(1);
            MinimumSize = new Size(960, 640);

            // Old title separator is no longer needed.
            Line2.Visible = false;

            // Sidebar header: logo + product name instead of "Settings".
            label1.Text = "";
            label1.Height = 64;
            label1.Paint += SidebarLogo_Paint;
            label1.MouseDown += DragWindow;
            label1.DoubleClick += (s, e) => ToggleMaximize();

            // Slim bar above the page with the window buttons.
            pnlChrome = new Panel { Dock = DockStyle.Top, Height = 32, BackColor = Color.White };
            ChromeButton btnClose = new ChromeButton(ChromeButtonKind.Close) { Dock = DockStyle.Right };
            btnMaximize = new ChromeButton(ChromeButtonKind.Maximize) { Dock = DockStyle.Right };
            ChromeButton btnMinimize = new ChromeButton(ChromeButtonKind.Minimize) { Dock = DockStyle.Right };
            btnClose.Click += (s, e) => Close();
            btnMaximize.Click += (s, e) => ToggleMaximize();
            btnMinimize.Click += (s, e) => WindowState = FormWindowState.Minimized;
            pnlChrome.Controls.Add(btnMinimize);
            pnlChrome.Controls.Add(btnMaximize);
            pnlChrome.Controls.Add(btnClose);
            pnlChrome.MouseDown += DragWindow;
            pnlChrome.DoubleClick += (s, e) => ToggleMaximize();

            // Dock order: sidebar and its divider first, then this bar across the page column, then the page.
            panel1.Controls.Add(pnlChrome);
            panel1.Controls.SetChildIndex(pnlChrome, panel1.Controls.GetChildIndex(pnlContainer) + 1);

            Resize += (s, e) => OnWindowStateChanged();
            resizeFilter = new ResizeFilter(this);
            Application.AddMessageFilter(resizeFilter);
            FormClosed += (s, e) => Application.RemoveMessageFilter(resizeFilter);
        }

        protected override CreateParams CreateParams
        {
            get
            {
                const int CS_DROPSHADOW = 0x20000;
                const int WS_MINIMIZEBOX = 0x20000, WS_MAXIMIZEBOX = 0x10000, WS_SYSMENU = 0x80000;
                CreateParams cp = base.CreateParams;
                cp.ClassStyle |= CS_DROPSHADOW;
                // Keeps taskbar click-to-minimize / restore and Win+arrow working without a title bar.
                cp.Style |= WS_MINIMIZEBOX | WS_MAXIMIZEBOX | WS_SYSMENU;
                return cp;
            }
        }

        private void ToggleMaximize()
        {
            if (WindowState == FormWindowState.Maximized)
            {
                WindowState = FormWindowState.Normal;
                return;
            }
            // Maximize to the working area so the taskbar stays visible.
            Screen screen = Screen.FromControl(this);
            MaximizedBounds = new Rectangle(screen.WorkingArea.X - screen.Bounds.X, screen.WorkingArea.Y - screen.Bounds.Y,
                screen.WorkingArea.Width, screen.WorkingArea.Height);
            WindowState = FormWindowState.Maximized;
        }

        private void OnWindowStateChanged()
        {
            bool maximized = WindowState == FormWindowState.Maximized;
            Padding = maximized ? Padding.Empty : new Padding(1);
            btnMaximize?.SetMaximized(maximized);
        }

        [DllImport("user32.dll")] private static extern bool ReleaseCapture();
        [DllImport("user32.dll")] private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        private void DragWindow(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left || e.Clicks > 1) return;
            ReleaseCapture();
            SendMessage(Handle, 0xA1 /* WM_NCLBUTTONDOWN */, (IntPtr)2 /* HTCAPTION */, IntPtr.Zero);
        }

        private void SidebarLogo_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.Clear(NavBack);
            Image logo = Properties.Resources.BlogicSystems_Black;
            int logoHeight = 24;
            int logoWidth = logo.Width * logoHeight / logo.Height;
            g.DrawImage(logo, new Rectangle(22, 14, logoWidth, logoHeight));
            using (Font font = new Font("Roboto", 7.5F, FontStyle.Bold))
                TextRenderer.DrawText(g, "THIRD PARTY TOOL", font, new Point(22, 42), Color.FromArgb(107, 114, 128));
        }

        /// <summary>Edge resizing for the borderless window: works over any control near the window edge.</summary>
        private sealed class ResizeFilter : IMessageFilter
        {
            private const int WM_MOUSEMOVE = 0x200, WM_LBUTTONDOWN = 0x201;
            private readonly HomeForm form;

            public ResizeFilter(HomeForm form) { this.form = form; }

            public bool PreFilterMessage(ref Message m)
            {
                if (m.Msg != WM_MOUSEMOVE && m.Msg != WM_LBUTTONDOWN) return false;
                if (form.WindowState != FormWindowState.Normal || !form.Visible) return false;
                Control target = Control.FromHandle(m.HWnd);
                if (target == null || target.FindForm() != form) return false;

                int hit = HitTest(form.PointToScreen(Point.Empty), form.Size, Cursor.Position);
                if (hit == 0) return false;

                if (m.Msg == WM_MOUSEMOVE)
                {
                    Cursor.Current = CursorFor(hit);
                    return false;
                }
                ReleaseCapture();
                SendMessage(form.Handle, 0xA1 /* WM_NCLBUTTONDOWN */, (IntPtr)hit, IntPtr.Zero);
                return true;
            }

            private static int HitTest(Point origin, Size size, Point cursor)
            {
                int x = cursor.X - origin.X, y = cursor.Y - origin.Y;
                bool left = x < ResizeGrip, right = x >= size.Width - ResizeGrip;
                bool top = y < ResizeGrip, bottom = y >= size.Height - ResizeGrip;
                if (top && left) return 13;      // HTTOPLEFT
                if (top && right) return 14;     // HTTOPRIGHT
                if (bottom && left) return 16;   // HTBOTTOMLEFT
                if (bottom && right) return 17;  // HTBOTTOMRIGHT
                if (left) return 10;             // HTLEFT
                if (right) return 11;            // HTRIGHT
                if (top) return 12;              // HTTOP
                if (bottom) return 15;           // HTBOTTOM
                return 0;
            }

            private static Cursor CursorFor(int hit)
            {
                switch (hit)
                {
                    case 10: case 11: return Cursors.SizeWE;
                    case 12: case 15: return Cursors.SizeNS;
                    case 13: case 17: return Cursors.SizeNWSE;
                    default: return Cursors.SizeNESW;
                }
            }
        }

        private enum ChromeButtonKind { Minimize, Maximize, Close }

        /// <summary>Window button drawn like the Windows 11 caption buttons.</summary>
        private sealed class ChromeButton : Control
        {
            private readonly ChromeButtonKind kind;
            private bool hover, pressed, maximized;

            public ChromeButton(ChromeButtonKind kind)
            {
                this.kind = kind;
                Size = new Size(46, 32);
                Cursor = Cursors.Default;
                SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint, true);
                BackColor = Color.White;
            }

            public void SetMaximized(bool value) { maximized = value; Invalidate(); }

            protected override void OnMouseEnter(EventArgs e) { hover = true; Invalidate(); base.OnMouseEnter(e); }
            protected override void OnMouseLeave(EventArgs e) { hover = pressed = false; Invalidate(); base.OnMouseLeave(e); }
            protected override void OnMouseDown(MouseEventArgs e) { pressed = true; Invalidate(); base.OnMouseDown(e); }
            protected override void OnMouseUp(MouseEventArgs e) { pressed = false; Invalidate(); base.OnMouseUp(e); }

            protected override void OnPaint(PaintEventArgs e)
            {
                Graphics g = e.Graphics;
                Color back = Color.White, glyph = Color.FromArgb(55, 65, 81);
                if (kind == ChromeButtonKind.Close && (hover || pressed))
                {
                    back = pressed ? Color.FromArgb(241, 112, 122) : Color.FromArgb(196, 43, 28);
                    glyph = Color.White;
                }
                else if (hover)
                    back = pressed ? Color.FromArgb(218, 221, 226) : Color.FromArgb(233, 235, 238);
                g.Clear(back);

                int cx = Width / 2, cy = Height / 2;
                using (Pen pen = new Pen(glyph, 1f))
                {
                    switch (kind)
                    {
                        case ChromeButtonKind.Minimize:
                            g.DrawLine(pen, cx - 5, cy, cx + 5, cy);
                            break;
                        case ChromeButtonKind.Maximize:
                            if (maximized)
                            {
                                g.DrawRectangle(pen, cx - 5, cy - 3, 8, 8);
                                g.DrawLine(pen, cx - 3, cy - 5, cx + 5, cy - 5);
                                g.DrawLine(pen, cx + 5, cy - 5, cx + 5, cy + 3);
                            }
                            else
                                g.DrawRectangle(pen, cx - 5, cy - 5, 10, 10);
                            break;
                        case ChromeButtonKind.Close:
                            g.SmoothingMode = SmoothingMode.AntiAlias;
                            g.DrawLine(pen, cx - 5, cy - 5, cx + 5, cy + 5);
                            g.DrawLine(pen, cx + 5, cy - 5, cx - 5, cy + 5);
                            break;
                    }
                }
            }
        }
    }
}
