using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Windows.Forms;

namespace ThirdPartyTool.CustomControl
{
    /// <summary>
    /// Date picker used on every screen: a rounded field (calendar icon, date, chevron) that opens a month calendar.
    /// Up / Down or the mouse wheel move one day, Page Up / Page Down one month.
    /// Exposes Value / EndValue (like CustomDateTimePicker) and DateTimeValues (like BDateTimePickerCustom) so the
    /// screens that used those keep their code.
    /// </summary>
    [DefaultEvent("ValueChanged")]
    public class DateField : Control
    {
        internal static readonly Color Primary = Color.FromArgb(42, 108, 228);
        internal static readonly Color TextColor = Color.FromArgb(17, 24, 39);
        internal static readonly Color Muted = Color.FromArgb(107, 114, 128);
        private static readonly Color Border = Color.FromArgb(209, 213, 219);
        private static readonly Color BorderHover = Color.FromArgb(156, 163, 175);

        private DateTime value = DateTime.Today;
        private bool hover, open;
        private CalendarDropDown dropDown;

        public event EventHandler ValueChanged;

        public DateField()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint
                | ControlStyles.ResizeRedraw | ControlStyles.Selectable, true);
            Size = new Size(160, 40);
            Font = new Font("Roboto", 11F);
            BackColor = Color.White;
            Cursor = Cursors.Hand;
            TabStop = true;
        }

        /// <summary>Selected day (time part is always 00:00), or with ShowTime the selected time (seconds dropped).</summary>
        public DateTime Value
        {
            get { return value; }
            set
            {
                DateTime v = ShowTime ? new DateTime(value.Year, value.Month, value.Day, value.Hour, value.Minute, 0) : value.Date;
                if (v == this.value) return;
                this.value = v;
                Invalidate();
                ValueChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        private bool showTime;

        /// <summary>Time picker mode: clock icon, "hh:mm tt", a list of times every 15 minutes; Up / Down move one minute.</summary>
        [DefaultValue(false)]
        public bool ShowTime
        {
            get { return showTime; }
            set
            {
                showTime = value;
                if (value && Format == "MM/dd/yyyy") Format = "hh:mm tt";
                Invalidate();
            }
        }

        /// <summary>Last moment of the selected day, for "to" dates in a range.</summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DateTime EndValue
        {
            get { return value.AddDays(1).AddMilliseconds(-1); }
            set { Value = value; }
        }

        /// <summary>Same as Value (name used by the old BDateTimePickerCustom).</summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DateTime DateTimeValues
        {
            get { return Value; }
            set { Value = value; }
        }

        [DefaultValue("MM/dd/yyyy")]
        public string Format { get; set; } = "MM/dd/yyyy";

        protected override void OnMouseEnter(EventArgs e) { hover = true; Invalidate(); base.OnMouseEnter(e); }
        protected override void OnMouseLeave(EventArgs e) { hover = false; Invalidate(); base.OnMouseLeave(e); }
        protected override void OnGotFocus(EventArgs e) { Invalidate(); base.OnGotFocus(e); }
        protected override void OnLostFocus(EventArgs e) { Invalidate(); base.OnLostFocus(e); }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            Focus();
            if (e.Button == MouseButtons.Left)
                ToggleCalendar();
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);
            if (Focused && !open)
                Value = ShowTime ? Value.AddMinutes(e.Delta > 0 ? 1 : -1) : Value.AddDays(e.Delta > 0 ? 1 : -1);
        }

        protected override bool IsInputKey(Keys keyData)
        {
            switch (keyData & Keys.KeyCode)
            {
                case Keys.Up: case Keys.Down: case Keys.PageUp: case Keys.PageDown: return true;
            }
            return base.IsInputKey(keyData);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            switch (e.KeyCode)
            {
                case Keys.Up: Value = ShowTime ? Value.AddMinutes(1) : Value.AddDays(1); e.Handled = true; break;
                case Keys.Down: Value = ShowTime ? Value.AddMinutes(-1) : Value.AddDays(-1); e.Handled = true; break;
                case Keys.PageUp: Value = ShowTime ? Value.AddHours(1) : Value.AddMonths(1); e.Handled = true; break;
                case Keys.PageDown: Value = ShowTime ? Value.AddHours(-1) : Value.AddMonths(-1); e.Handled = true; break;
                case Keys.Space:
                case Keys.F4: ToggleCalendar(); e.Handled = true; break;
            }
        }

        private void ToggleCalendar()
        {
            if (open)
            {
                dropDown?.Close();
                return;
            }
            dropDown = new CalendarDropDown(ShowTime ? (Control)new TimeList(Value, Width) : new CalendarPanel(Value));
            dropDown.DayPicked += (s, day) => Value = ShowTime ? Value.Date + day.TimeOfDay : day;
            dropDown.Closed += (s, e) => { open = false; Invalidate(); };
            open = true;
            Invalidate();
            dropDown.Show(this, new Point(0, Height + 4));
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Clear(Parent?.BackColor ?? Color.White);

            bool active = open || Focused;
            Color border = !Enabled ? Color.FromArgb(229, 231, 235) : active ? Primary : hover ? BorderHover : Border;
            float width = active ? 2f : 1f;
            RectangleF r = new RectangleF(width / 2, width / 2, Width - width - 0.5f, Height - width - 0.5f);
            using (GraphicsPath path = Rounded(r, 8))
            {
                using (SolidBrush fill = new SolidBrush(Enabled ? Color.White : Color.FromArgb(249, 250, 251)))
                    g.FillPath(fill, path);
                using (Pen pen = new Pen(border, width))
                    g.DrawPath(pen, path);
            }

            // calendar icon (clock in time mode)
            Color iconColor = active ? Primary : Muted;
            Rectangle cal = new Rectangle(13, (Height - 16) / 2, 16, 15);
            using (Pen pen = new Pen(iconColor, 1.5f))
            {
                if (ShowTime)
                {
                    Rectangle clock = new Rectangle(13, (Height - 16) / 2, 16, 16);
                    g.DrawEllipse(pen, clock);
                    int ccx = clock.X + 8, ccy = clock.Y + 8;
                    g.DrawLine(pen, ccx, ccy, ccx, ccy - 5);
                    g.DrawLine(pen, ccx, ccy, ccx + 4, ccy + 2);
                }
                else
                {
                    using (GraphicsPath p = Rounded(cal, 2.5f))
                        g.DrawPath(pen, p);
                    g.DrawLine(pen, cal.Left, cal.Top + 5, cal.Right, cal.Top + 5);
                    g.DrawLine(pen, cal.Left + 4, cal.Top - 2, cal.Left + 4, cal.Top + 2);
                    g.DrawLine(pen, cal.Right - 4, cal.Top - 2, cal.Right - 4, cal.Top + 2);
                }
            }

            // Narrow fields (side by side in a small panel) drop the chevron to keep the whole date visible.
            bool showChevron = Width >= 150;
            string text = value.ToString(Format, CultureInfo.InvariantCulture);
            TextRenderer.DrawText(g, text, Font, new Rectangle(38, 0, Width - 38 - (showChevron ? 28 : 6), Height), Enabled ? TextColor : Muted,
                TextFormatFlags.VerticalCenter | TextFormatFlags.Left | TextFormatFlags.NoPrefix | TextFormatFlags.EndEllipsis);
            if (!showChevron)
                return;

            // chevron (points up while the calendar is open)
            int cx = Width - 18, cy = Height / 2;
            using (Pen pen = new Pen(active ? Primary : Muted, 1.6f))
            {
                pen.StartCap = pen.EndCap = LineCap.Round;
                if (open)
                    g.DrawLines(pen, new[] { new Point(cx - 5, cy + 2), new Point(cx, cy - 3), new Point(cx + 5, cy + 2) });
                else
                    g.DrawLines(pen, new[] { new Point(cx - 5, cy - 2), new Point(cx, cy + 3), new Point(cx + 5, cy - 2) });
            }
        }

        internal static GraphicsPath Rounded(RectangleF r, float radius)
        {
            float d = radius * 2;
            GraphicsPath path = new GraphicsPath();
            path.AddArc(r.X, r.Y, d, d, 180, 90);
            path.AddArc(r.Right - d, r.Y, d, d, 270, 90);
            path.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90);
            path.AddArc(r.X, r.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }

        /// <summary>The month calendar shown under the field.</summary>
        private sealed class CalendarDropDown : ToolStripDropDown
        {
            public event EventHandler<DateTime> DayPicked;

            public CalendarDropDown(Control panel)
            {
                EventHandler<DateTime> picked = (s, day) => { DayPicked?.Invoke(this, day); Close(); };
                if (panel is CalendarPanel calendar) calendar.DayPicked += picked;
                if (panel is TimeList times) times.TimePicked += picked;
                ToolStripControlHost host = new ToolStripControlHost(panel) { Margin = Padding.Empty, Padding = Padding.Empty, AutoSize = false, Size = panel.Size };
                Padding = Padding.Empty;
                Items.Add(host);
                DropShadowEnabled = true;
                Renderer = new ToolStripProfessionalRenderer(new CalendarColors());
                Opened += (s, e) => panel.Focus();
            }

            private sealed class CalendarColors : ProfessionalColorTable
            {
                public override Color ToolStripDropDownBackground => Color.White;
                public override Color MenuBorder => Color.FromArgb(209, 213, 219);
            }
        }

        /// <summary>Times every 15 minutes (plus the current one), the current one highlighted and scrolled into view.</summary>
        private sealed class TimeList : ListBox
        {
            public event EventHandler<DateTime> TimePicked;

            public TimeList(DateTime current, int width)
            {
                DrawMode = DrawMode.OwnerDrawFixed;
                ItemHeight = 32;
                BorderStyle = BorderStyle.None;
                IntegralHeight = false;
                Font = new Font("Roboto", 10.5F);
                Size = new Size(Math.Max(width, 150), ItemHeight * 8);

                DateTime now = new DateTime(2000, 1, 1, current.Hour, current.Minute, 0);
                for (int m = 0; m < 24 * 60; m += 15)
                {
                    DateTime t = new DateTime(2000, 1, 1).AddMinutes(m);
                    Items.Add(t);
                    if (now > t && now < t.AddMinutes(15))
                        Items.Add(now); // the current time sits between two quarter hours
                }
                SelectedItem = now;
            }

            protected override void OnHandleCreated(EventArgs e)
            {
                base.OnHandleCreated(e);
                TopIndex = Math.Max(0, SelectedIndex - 3); // current time in the middle of the list
            }

            protected override void OnDrawItem(DrawItemEventArgs e)
            {
                if (e.Index < 0) return;
                bool selected = (e.State & DrawItemState.Selected) != 0;
                using (SolidBrush back = new SolidBrush(selected ? Color.FromArgb(235, 242, 255) : Color.White))
                    e.Graphics.FillRectangle(back, e.Bounds);
                string text = ((DateTime)Items[e.Index]).ToString("hh:mm tt", CultureInfo.InvariantCulture);
                using (Font font = new Font(Font, selected ? FontStyle.Bold : FontStyle.Regular))
                    TextRenderer.DrawText(e.Graphics, text, font, new Rectangle(e.Bounds.X + 14, e.Bounds.Y, e.Bounds.Width - 14, e.Bounds.Height),
                        selected ? Primary : TextColor, TextFormatFlags.VerticalCenter | TextFormatFlags.Left);
            }

            protected override void OnMouseClick(MouseEventArgs e)
            {
                base.OnMouseClick(e);
                if (SelectedItem is DateTime t) TimePicked?.Invoke(this, t);
            }

            protected override void OnKeyDown(KeyEventArgs e)
            {
                base.OnKeyDown(e);
                if (e.KeyCode == Keys.Enter && SelectedItem is DateTime t) TimePicked?.Invoke(this, t);
            }
        }

        /// <summary>Month view: header with ‹ ›, weekday row, 6 x 7 days, "Today" link. Arrow keys move, Enter picks.</summary>
        private sealed class CalendarPanel : Control
        {
            private const int CellW = 38, CellH = 34, Pad = 12, HeaderH = 44, WeekH = 26, FooterH = 40;
            public event EventHandler<DateTime> DayPicked;

            private DateTime month;      // first day of the shown month
            private DateTime cursor;     // keyboard / selected day
            private readonly DateTime selected;
            private Point hoverPoint = new Point(-1, -1);

            private Rectangle PrevButton => new Rectangle(Width - Pad - 64, 10, 28, 28);
            private Rectangle NextButton => new Rectangle(Width - Pad - 30, 10, 28, 28);
            private Rectangle TodayButton => new Rectangle(Pad, HeaderH + WeekH + CellH * 6 + 8, 64, 26);

            public CalendarPanel(DateTime selected)
            {
                SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.Selectable, true);
                this.selected = selected.Date;
                cursor = this.selected;
                month = new DateTime(cursor.Year, cursor.Month, 1);
                Size = new Size(Pad * 2 + CellW * 7, HeaderH + WeekH + CellH * 6 + FooterH);
                BackColor = Color.White;
                Font = new Font("Roboto", 10F);
            }

            private DateTime FirstCell => month.AddDays(-(int)month.DayOfWeek);

            private Rectangle CellRect(int index) =>
                new Rectangle(Pad + (index % 7) * CellW, HeaderH + WeekH + (index / 7) * CellH, CellW, CellH);

            private int CellAt(Point p)
            {
                for (int i = 0; i < 42; i++)
                    if (CellRect(i).Contains(p)) return i;
                return -1;
            }

            protected override bool IsInputKey(Keys keyData)
            {
                switch (keyData & Keys.KeyCode)
                {
                    case Keys.Left: case Keys.Right: case Keys.Up: case Keys.Down: case Keys.Enter: case Keys.PageUp: case Keys.PageDown: return true;
                }
                return base.IsInputKey(keyData);
            }

            protected override void OnKeyDown(KeyEventArgs e)
            {
                base.OnKeyDown(e);
                switch (e.KeyCode)
                {
                    case Keys.Left: MoveCursor(cursor.AddDays(-1)); break;
                    case Keys.Right: MoveCursor(cursor.AddDays(1)); break;
                    case Keys.Up: MoveCursor(cursor.AddDays(-7)); break;
                    case Keys.Down: MoveCursor(cursor.AddDays(7)); break;
                    case Keys.PageUp: MoveCursor(cursor.AddMonths(-1)); break;
                    case Keys.PageDown: MoveCursor(cursor.AddMonths(1)); break;
                    case Keys.Enter: DayPicked?.Invoke(this, cursor); break;
                }
            }

            private void MoveCursor(DateTime day)
            {
                cursor = day;
                month = new DateTime(day.Year, day.Month, 1);
                Invalidate();
            }

            protected override void OnMouseMove(MouseEventArgs e) { hoverPoint = e.Location; Invalidate(); base.OnMouseMove(e); }
            protected override void OnMouseLeave(EventArgs e) { hoverPoint = new Point(-1, -1); Invalidate(); base.OnMouseLeave(e); }

            protected override void OnMouseClick(MouseEventArgs e)
            {
                base.OnMouseClick(e);
                if (PrevButton.Contains(e.Location)) { month = month.AddMonths(-1); Invalidate(); return; }
                if (NextButton.Contains(e.Location)) { month = month.AddMonths(1); Invalidate(); return; }
                if (TodayButton.Contains(e.Location)) { DayPicked?.Invoke(this, DateTime.Today); return; }
                int cell = CellAt(e.Location);
                if (cell >= 0)
                    DayPicked?.Invoke(this, FirstCell.AddDays(cell));
            }

            protected override void OnPaint(PaintEventArgs e)
            {
                Graphics g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.Clear(Color.White);

                using (Font title = new Font("Roboto", 11.5F, FontStyle.Bold))
                    TextRenderer.DrawText(g, month.ToString("MMMM yyyy", CultureInfo.InvariantCulture), title,
                        new Rectangle(Pad + 4, 10, 180, 28), TextColor, TextFormatFlags.VerticalCenter | TextFormatFlags.Left);
                DrawNavButton(g, PrevButton, left: true);
                DrawNavButton(g, NextButton, left: false);

                using (Font week = new Font("Roboto", 8.5F, FontStyle.Bold))
                {
                    string[] names = { "Su", "Mo", "Tu", "We", "Th", "Fr", "Sa" };
                    for (int i = 0; i < 7; i++)
                        TextRenderer.DrawText(g, names[i], week, new Rectangle(Pad + i * CellW, HeaderH, CellW, WeekH), Muted,
                            TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
                }

                DateTime first = FirstCell;
                int hoverCell = CellAt(hoverPoint);
                using (Font bold = new Font("Roboto", 10F, FontStyle.Bold))
                {
                    for (int i = 0; i < 42; i++)
                    {
                        DateTime day = first.AddDays(i);
                        Rectangle cell = CellRect(i);
                        Rectangle dot = new Rectangle(cell.X + (CellW - 30) / 2, cell.Y + (CellH - 30) / 2, 30, 30);
                        bool inMonth = day.Month == month.Month;
                        bool isSelected = day == selected;
                        bool isToday = day == DateTime.Today;
                        bool isCursor = day == cursor && Focused && !isSelected;

                        if (isSelected)
                            using (SolidBrush b = new SolidBrush(Primary)) g.FillEllipse(b, dot);
                        else if (i == hoverCell || isCursor)
                            using (SolidBrush b = new SolidBrush(Color.FromArgb(235, 242, 255))) g.FillEllipse(b, dot);
                        if (isToday && !isSelected)
                            using (Pen p = new Pen(Primary, 1.5f)) g.DrawEllipse(p, dot);

                        Color color = isSelected ? Color.White : !inMonth ? Color.FromArgb(189, 193, 199) : isToday ? Primary : TextColor;
                        TextRenderer.DrawText(g, day.Day.ToString(), isSelected || isToday ? bold : Font, cell, color,
                            TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
                    }
                }

                // footer
                int footerTop = HeaderH + WeekH + CellH * 6 + 2;
                using (Pen p = new Pen(Color.FromArgb(235, 236, 239))) g.DrawLine(p, Pad, footerTop, Width - Pad, footerTop);
                bool todayHover = TodayButton.Contains(hoverPoint);
                using (Font f = new Font("Roboto", 9.5F, todayHover ? FontStyle.Bold | FontStyle.Underline : FontStyle.Bold))
                    TextRenderer.DrawText(g, "Today", f, TodayButton, Primary, TextFormatFlags.VerticalCenter | TextFormatFlags.Left);
                using (Font f = new Font("Roboto", 9F))
                    TextRenderer.DrawText(g, DateTime.Today.ToString("ddd, MMM d", CultureInfo.InvariantCulture), f,
                        new Rectangle(Width - Pad - 140, TodayButton.Y, 140, TodayButton.Height), Muted, TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
            }

            private void DrawNavButton(Graphics g, Rectangle r, bool left)
            {
                if (r.Contains(hoverPoint))
                    using (GraphicsPath p = Rounded(r, 6))
                    using (SolidBrush b = new SolidBrush(Color.FromArgb(241, 243, 246))) g.FillPath(b, p);
                int cx = r.X + r.Width / 2, cy = r.Y + r.Height / 2;
                using (Pen pen = new Pen(TextColor, 1.6f))
                {
                    pen.StartCap = pen.EndCap = LineCap.Round;
                    if (left)
                        g.DrawLines(pen, new[] { new Point(cx + 2, cy - 5), new Point(cx - 3, cy), new Point(cx + 2, cy + 5) });
                    else
                        g.DrawLines(pen, new[] { new Point(cx - 2, cy - 5), new Point(cx + 3, cy), new Point(cx - 2, cy + 5) });
                }
            }
        }
    }
}
