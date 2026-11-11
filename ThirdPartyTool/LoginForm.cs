using BLogicSystems.XPos.ClientUtilities;
using BLogicSystems.XPos.Utilites.Languages;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ThirdPartyTool
{
    public partial class LoginForm : Form
    {
        const string userName = "admin";
        const string password = "8559";

        /// <summary>Databases the tool works on. Default BLogicPOS7 / Merchant, changed with "Change".</summary>
        public string PosDatabase { get; private set; } = "BLogicPOS7";
        public string MerchantDatabase { get; private set; } = "Merchant";

        private static readonly Color ErrorColor = Color.FromArgb(220, 38, 38);
        private static readonly Color WarningColor = Color.FromArgb(180, 83, 9);
        private bool showingError;

        public LoginForm()
        {
            InitializeComponent();

            txtUserName.Icon = Properties.Resources.icon_user_24x24;
            txtPassword.Icon = Properties.Resources.icon_password_2;

            btnOK.Click += BtnLogin_Click;
            btnClose.Click += BtnCancel_Click;
            dbCard.SetDatabases(PosDatabase, MerchantDatabase);
            dbCard.CheckDatabases = CheckDatabases;
            dbCard.Applied += (s, e) => { PosDatabase = dbCard.PosDatabase; MerchantDatabase = dbCard.MerchantDatabase; };
            dbCard.Collapsed += (s, e) => txtUserName.Input.Focus();
            dbCard.SizeChanged += (s, e) => FitToDatabaseCard();

            txtUserName.Input.TextChanged += (s, e) => ClearError();
            txtPassword.Input.TextChanged += (s, e) => ClearError();
            txtPassword.Input.Enter += (s, e) => ShowCapsLockHint();
            txtPassword.Input.Leave += (s, e) => ShowCapsLockHint();
            txtPassword.Input.KeyUp += (s, e) => ShowCapsLockHint();

            KeyDown += LoginForm_KeyDown;
            this.Shown += LoginForm_Shown;

            lblVersion.Text = $"{Helper.VersionText}  ·  {Helper.ReleaseText}";

            // Borderless window: drag it by the logo or the empty part of the form.
            picLogo.MouseDown += DragWindow;
            pnlMain.MouseDown += DragWindow;
        }

        protected override CreateParams CreateParams
        {
            get
            {
                const int CS_DROPSHADOW = 0x20000;
                CreateParams cp = base.CreateParams;
                cp.ClassStyle |= CS_DROPSHADOW;
                return cp;
            }
        }

        [DllImport("user32.dll")] private static extern bool ReleaseCapture();
        [DllImport("user32.dll")] private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        private void DragWindow(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            ReleaseCapture();
            SendMessage(Handle, 0xA1 /* WM_NCLBUTTONDOWN */, (IntPtr)2 /* HTCAPTION */, IntPtr.Zero);
        }

        private void LoginForm_Shown(object sender, EventArgs e)
        {
            string dateTime = Languages.ConvertDateTimeToStringWithCulture(DateTime.Now.ToString());
            BLogicLogger.Exception($"BtnLogin_Click in LoginForm: {dateTime}", null);
            txtUserName.Input.Focus();
        }

        /// <summary>Enter on the username moves to the password; Enter on the password signs in; Esc closes.</summary>
        private void LoginForm_KeyDown(object sender, KeyEventArgs e)
        {
            // Inside the open database card, Enter applies and Esc closes the card, not the window.
            if (dbCard.IsExpanded && dbCard.ContainsFocus && (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Escape))
            {
                e.SuppressKeyPress = true;
                if (e.KeyCode == Keys.Enter) dbCard.Apply();
                else dbCard.Collapse();
                return;
            }

            if (e.KeyCode == Keys.Escape)
            {
                e.SuppressKeyPress = true;
                BtnCancel_Click(sender, e);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                if (txtUserName.Input.Focused && string.IsNullOrWhiteSpace(txtPassword.Input.Text))
                    txtPassword.Input.Focus();
                else
                    BtnLogin_Click(sender, e);
            }
        }

        /// <summary>The window grows and shrinks with the database card; the version line stays under it.</summary>
        private void FitToDatabaseCard()
        {
            lblVersion.Top = dbCard.Bottom + 16;
            int centerY = Top + Height / 2;
            ClientSize = new Size(ClientSize.Width, lblVersion.Bottom + 18 + Padding.Vertical);

            // Grow around the middle and stay inside the screen (a 1366x768 laptop still fits when expanded).
            if (Visible)
            {
                Rectangle area = Screen.FromControl(this).WorkingArea;
                Top = Math.Max(area.Top, Math.Min(centerY - Height / 2, area.Bottom - Height));
            }
        }

        /// <summary>
        /// Looks the two names up on the local SQL Server (same servers StartForm tries: "." then ".\SQLEXPRESS").
        /// Returns why they cannot be used, or null when they can (or when the server cannot be reached here,
        /// in which case the start-up connection reports it as before).
        /// </summary>
        private static string CheckDatabases(string pos, string merchant)
        {
            foreach (string server in new[] { ".", @".\SQLEXPRESS" })
            {
                try
                {
                    using (var conn = new System.Data.SqlClient.SqlConnection($"data source={server};Initial Catalog=master;Integrated Security=SSPI;Connection Timeout=3;TrustServerCertificate=true"))
                    using (var cmd = conn.CreateCommand())
                    {
                        conn.Open();
                        cmd.CommandText = "select case when DB_ID(@p) is null then 0 else 1 end, case when DB_ID(@m) is null then 0 else 1 end";
                        cmd.Parameters.AddWithValue("@p", pos);
                        cmd.Parameters.AddWithValue("@m", merchant);
                        using (var reader = cmd.ExecuteReader())
                        {
                            reader.Read();
                            bool posOk = reader.GetInt32(0) == 1, merchantOk = reader.GetInt32(1) == 1;
                            if (!posOk && !merchantOk) return $"\"{pos}\" and \"{merchant}\" are not on this SQL Server.";
                            if (!posOk) return $"\"{pos}\" is not on this SQL Server.";
                            if (!merchantOk) return $"\"{merchant}\" is not on this SQL Server.";
                            return null;
                        }
                    }
                }
                catch (System.Data.SqlClient.SqlException)
                {
                    // try the next server name
                }
            }
            return null;
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void ShowError(string message, LoginFieldBox field)
        {
            showingError = true;
            lblMessage.ForeColor = ErrorColor;
            lblMessage.Text = message;
            txtUserName.HasError = field == txtUserName;
            txtPassword.HasError = field == txtPassword;
            field.Input.Focus();
            field.Input.SelectAll();
        }

        private void ClearError()
        {
            if (!showingError) return;
            showingError = false;
            txtUserName.HasError = txtPassword.HasError = false;
            lblMessage.Text = "";
            ShowCapsLockHint();
        }

        private void ShowCapsLockHint()
        {
            if (showingError) return;
            bool caps = txtPassword.Input.Focused && Control.IsKeyLocked(Keys.CapsLock);
            lblMessage.ForeColor = WarningColor;
            lblMessage.Text = caps ? "Caps Lock is on." : "";
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            try
            {
#if DEBUG

#else
                if (string.IsNullOrWhiteSpace(txtUserName.Input.Text))
                {
                    ShowError("Enter your username.", txtUserName);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtPassword.Input.Text))
                {
                    ShowError("Enter your password.", txtPassword);
                    return;
                }

                // Username ignores case and surrounding spaces; the password must match exactly.
                if (!string.Equals(txtUserName.Input.Text.Trim(), userName, StringComparison.OrdinalIgnoreCase) || txtPassword.Input.Text != password)
                {
                    ShowError("The username or password is incorrect.", txtPassword);
                    return;
                }
#endif

                DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                BLogicLogger.Exception("BtnLogin_Click in LoginForm", ex);
            }
        }
    }

    /// <summary>
    /// Rounded input with a leading icon, a cue-banner placeholder, a focus/error border and, for passwords,
    /// a Show/Hide toggle.
    /// </summary>
    public class LoginFieldBox : Panel
    {
        public readonly TextBox Input = new TextBox();
        private readonly Label toggle = new Label();
        private bool focused;
        private bool hasError;
        private bool isPassword;
        private string placeholder = "";

        private static readonly Color Primary = Color.FromArgb(42, 108, 228);

        public Image Icon { get; set; }

        public bool HasError
        {
            get { return hasError; }
            set { hasError = value; Invalidate(); }
        }

        public string Placeholder
        {
            get { return placeholder; }
            set { placeholder = value ?? ""; ApplyCueBanner(); }
        }

        public bool IsPassword
        {
            get { return isPassword; }
            set
            {
                isPassword = value;
                Input.UseSystemPasswordChar = value;
                toggle.Visible = value;
                PerformLayout();
            }
        }

        public LoginFieldBox()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.ResizeRedraw, true);
            BackColor = Color.White;
            Cursor = Cursors.IBeam;

            Input.BorderStyle = BorderStyle.None;
            Input.Font = new Font("Roboto", 12F);
            Input.ForeColor = Color.FromArgb(17, 24, 39);
            Input.BackColor = Color.White;
            Input.Enter += (s, e) => { focused = true; Invalidate(); };
            Input.Leave += (s, e) => { focused = false; Invalidate(); };
            Input.HandleCreated += (s, e) => ApplyCueBanner();
            Controls.Add(Input);

            toggle.Text = "Show";
            toggle.AutoSize = false;
            toggle.Size = new Size(52, 24);
            toggle.TextAlign = ContentAlignment.MiddleCenter;
            toggle.Font = new Font("Roboto", 9F, FontStyle.Bold);
            toggle.ForeColor = Primary;
            toggle.BackColor = Color.White;
            toggle.Cursor = Cursors.Hand;
            toggle.Visible = false;
            toggle.Click += (s, e) =>
            {
                Input.UseSystemPasswordChar = !Input.UseSystemPasswordChar;
                toggle.Text = Input.UseSystemPasswordChar ? "Show" : "Hide";
                Input.Focus();
                Input.SelectionStart = Input.TextLength;
            };
            Controls.Add(toggle);
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            Input.Focus();
        }

        protected override void OnLayout(LayoutEventArgs levent)
        {
            base.OnLayout(levent);
            int right = isPassword ? toggle.Width + 14 : 14;
            int left = Icon != null ? 46 : 14;
            Input.Location = new Point(left, (Height - Input.Height) / 2);
            Input.Width = Math.Max(20, Width - left - right);
            toggle.Location = new Point(Width - toggle.Width - 10, (Height - toggle.Height) / 2);
        }

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, string lParam);

        private void ApplyCueBanner()
        {
            if (Input.IsHandleCreated)
                SendMessage(Input.Handle, 0x1501 /* EM_SETCUEBANNER */, (IntPtr)1, placeholder);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Clear(Parent?.BackColor ?? Color.White);

            Color border = hasError ? Color.FromArgb(220, 38, 38) : focused ? Primary : Color.FromArgb(209, 213, 219);
            float width = hasError || focused ? 2f : 1f;
            RectangleF r = new RectangleF(width / 2, width / 2, Width - width - 0.5f, Height - width - 0.5f);
            using (GraphicsPath path = LoginPaint.Rounded(r, 8))
            {
                using (SolidBrush b = new SolidBrush(Color.White)) g.FillPath(b, path);
                using (Pen p = new Pen(border, width)) g.DrawPath(p, path);
            }

            if (Icon != null)
            {
                Color tint = hasError ? Color.FromArgb(220, 38, 38) : focused ? Primary : Color.FromArgb(107, 114, 128);
                LoginPaint.DrawTinted(g, Icon, new Rectangle(14, (Height - 20) / 2, 20, 20), tint);
            }
        }
    }

    /// <summary>
    /// Card at the bottom of the login screen. Collapsed: which databases the tool will open, with "Change".
    /// Expanded (in place, no popup): two inputs, "Use default", Cancel / Apply. Apply checks the names first.
    /// </summary>
    public class LoginDatabaseCard : Control
    {
        public const string DefaultPos = "BLogicPOS7";
        public const string DefaultMerchant = "Merchant";
        private const int CollapsedHeight = 96;
        private const int ExpandedHeight = 226;

        public event EventHandler Applied;
        public event EventHandler Collapsed;

        /// <summary>Returns why the names cannot be used, or null when they are fine.</summary>
        public Func<string, string, string> CheckDatabases;

        public string PosDatabase { get; private set; } = DefaultPos;
        public string MerchantDatabase { get; private set; } = DefaultMerchant;
        public bool IsExpanded { get; private set; }

        private readonly CustomControl.BLabel_Rounded btnChange = new CustomControl.BLabel_Rounded();
        private readonly CustomControl.BLabel_Rounded btnCancel = new CustomControl.BLabel_Rounded();
        private readonly CustomControl.BLabel_Rounded btnApply = new CustomControl.BLabel_Rounded();
        private readonly Label lnkDefault = new Label();
        private readonly Label lblPos = new Label();
        private readonly Label lblMerchant = new Label();
        private readonly Label lblError = new Label();
        private readonly LoginFieldBox fldPos = new LoginFieldBox();
        private readonly LoginFieldBox fldMerchant = new LoginFieldBox();
        private readonly ToolTip tip = new ToolTip();

        private static readonly Color Primary = Color.FromArgb(42, 108, 228);
        private static readonly Color Muted = Color.FromArgb(107, 114, 128);
        private static readonly Color WarningText = Color.FromArgb(180, 83, 9);

        private bool IsDefault => PosDatabase == DefaultPos && MerchantDatabase == DefaultMerchant;

        public LoginDatabaseCard()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.ResizeRedraw, true);
            BackColor = Color.White;
            Height = CollapsedHeight;

            SetupButton(btnChange, "Change", false);
            btnChange.Click += (s, e) => Expand();
            SetupButton(btnCancel, "Cancel", false);
            btnCancel.Click += (s, e) => Collapse();
            SetupButton(btnApply, "Apply", true);
            btnApply.Click += (s, e) => Apply();

            foreach (Label caption in new[] { lblPos, lblMerchant })
            {
                caption.AutoSize = true;
                caption.Font = new Font("Roboto", 9F, FontStyle.Bold);
                caption.ForeColor = Color.FromArgb(55, 65, 81);
                caption.BackColor = Color.Transparent;
            }
            lblPos.Text = "POS database";
            lblMerchant.Text = "Merchant database";

            foreach (LoginFieldBox field in new[] { fldPos, fldMerchant })
            {
                field.Height = 38;
                field.Input.Font = new Font("Roboto", 11F);
                field.Input.TextChanged += (s, e) => { lblError.Text = ""; fldPos.HasError = fldMerchant.HasError = false; UpdateDefaultLink(); };
            }
            fldPos.Placeholder = DefaultPos;
            fldMerchant.Placeholder = DefaultMerchant;

            lnkDefault.Text = "Use default";
            lnkDefault.AutoSize = true;
            lnkDefault.Font = new Font("Roboto", 9F, FontStyle.Bold);
            lnkDefault.ForeColor = Primary;
            lnkDefault.BackColor = Color.Transparent;
            lnkDefault.Cursor = Cursors.Hand;
            lnkDefault.Click += (s, e) => { fldPos.Input.Text = DefaultPos; fldMerchant.Input.Text = DefaultMerchant; fldPos.Input.Focus(); };

            lblError.AutoSize = false;
            lblError.Font = new Font("Roboto", 9F);
            lblError.ForeColor = Color.FromArgb(220, 38, 38);
            lblError.BackColor = Color.Transparent;
            lblError.AutoEllipsis = true;

            Controls.AddRange(new Control[] { btnChange, lblPos, fldPos, lblMerchant, fldMerchant, lnkDefault, lblError, btnCancel, btnApply });
            ShowState();
        }

        private static void SetupButton(CustomControl.BLabel_Rounded button, string text, bool primary)
        {
            button.Text = text;
            button.Font = new Font("Roboto", 10F, FontStyle.Bold);
            button.TextAlign = ContentAlignment.MiddleCenter;
            button.BorderRadius = 6;
            button.BorderSize = 1;
            button.Size = new Size(84, 34);
            button.Cursor = Cursors.Hand;
            Action<bool> style = hover =>
            {
                Color back = primary ? (hover ? Color.FromArgb(29, 78, 216) : Primary) : (hover ? Color.FromArgb(239, 246, 255) : Color.White);
                button.BackColor = button.BackgroundColor = back;
                button.BorderColor = primary ? back : (hover ? Primary : Color.FromArgb(209, 213, 219));
                button.ForeColor = button.TextColor = primary ? Color.White : (text == "Cancel" ? Color.FromArgb(55, 65, 81) : Primary);
                button.Invalidate();
            };
            button.MouseEnter += (s, e) => style(true);
            button.MouseLeave += (s, e) => style(false);
            style(false);
        }

        public void SetDatabases(string posDatabase, string merchantDatabase)
        {
            PosDatabase = posDatabase;
            MerchantDatabase = merchantDatabase;
            tip.SetToolTip(this, $"POS: {PosDatabase}{Environment.NewLine}Merchant: {MerchantDatabase}");
            Invalidate();
        }

        public void Expand()
        {
            IsExpanded = true;
            fldPos.Input.Text = PosDatabase;
            fldMerchant.Input.Text = MerchantDatabase;
            lblError.Text = "";
            ShowState();
            fldPos.Input.Focus();
            fldPos.Input.SelectAll();
        }

        public void Collapse()
        {
            if (!IsExpanded) return;
            IsExpanded = false;
            ShowState();
            Collapsed?.Invoke(this, EventArgs.Empty);
        }

        public void Apply()
        {
            string pos = fldPos.Input.Text.Trim();
            string merchant = fldMerchant.Input.Text.Trim();
            if (pos.Length == 0) { ShowFieldError(fldPos, "Enter the POS database name."); return; }
            if (merchant.Length == 0) { ShowFieldError(fldMerchant, "Enter the Merchant database name."); return; }

            Cursor = Cursors.WaitCursor;
            string problem;
            try { problem = CheckDatabases?.Invoke(pos, merchant); }
            finally { Cursor = Cursors.Default; }
            if (problem != null)
            {
                ShowFieldError(problem.Contains("\"" + pos + "\"") ? fldPos : fldMerchant, problem);
                return;
            }

            SetDatabases(pos, merchant);
            Applied?.Invoke(this, EventArgs.Empty);
            Collapse();
        }

        private void ShowFieldError(LoginFieldBox field, string message)
        {
            lblError.Text = message;
            field.HasError = true;
            field.Input.Focus();
            field.Input.SelectAll();
        }

        private void UpdateDefaultLink()
        {
            lnkDefault.Visible = IsExpanded && (fldPos.Input.Text.Trim() != DefaultPos || fldMerchant.Input.Text.Trim() != DefaultMerchant);
        }

        private void ShowState()
        {
            btnChange.Visible = !IsExpanded;
            foreach (Control c in new Control[] { lblPos, fldPos, lblMerchant, fldMerchant, lblError, btnCancel, btnApply })
                c.Visible = IsExpanded;
            UpdateDefaultLink();
            Height = IsExpanded ? ExpandedHeight : CollapsedHeight;
            PerformLayout();
            Invalidate();
        }

        protected override void OnLayout(LayoutEventArgs levent)
        {
            base.OnLayout(levent);
            int inner = Width - 32;
            btnChange.Location = new Point(Width - btnChange.Width - 16, (CollapsedHeight - btnChange.Height) / 2);

            lnkDefault.Location = new Point(Width - 16 - lnkDefault.Width, 14);
            lblPos.Location = new Point(14, 36);
            fldPos.SetBounds(16, 54, inner, 38);
            lblMerchant.Location = new Point(14, 98);
            fldMerchant.SetBounds(16, 116, inner, 38);
            btnApply.Location = new Point(Width - 16 - btnApply.Width, 178);
            btnCancel.Location = new Point(btnApply.Left - 8 - btnCancel.Width, 178);
            lblError.SetBounds(16, 158, inner, 18);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Clear(Parent?.BackColor ?? Color.White);

            bool warn = !IsDefault && !IsExpanded;
            Color fill = IsExpanded ? Color.White : warn ? Color.FromArgb(255, 248, 225) : Color.FromArgb(248, 249, 251);
            Color border = IsExpanded ? Color.FromArgb(209, 213, 219) : warn ? Color.FromArgb(245, 158, 11) : Color.FromArgb(229, 231, 235);
            using (GraphicsPath path = LoginPaint.Rounded(new RectangleF(0.5f, 0.5f, Width - 1.5f, Height - 1.5f), 10))
            {
                using (SolidBrush b = new SolidBrush(fill)) g.FillPath(b, path);
                using (Pen p = new Pen(border)) g.DrawPath(p, path);
            }

            using (Font caption = new Font("Roboto", 8.25F, FontStyle.Bold))
            {
                if (IsExpanded)
                {
                    TextRenderer.DrawText(g, "DATABASES ON THIS SQL SERVER", caption, new Point(14, 14), Muted);
                    return;
                }

                Color accent = IsDefault ? Primary : Color.FromArgb(217, 119, 6);
                LoginPaint.DrawDatabaseGlyph(g, new Rectangle(16, (Height - 30) / 2, 26, 30), accent);

                int x = 58, valueX = 128, valueWidth = btnChange.Left - valueX - 10;
                TextRenderer.DrawText(g, "DATABASES", caption, new Point(x - 2, 14), Muted);
                if (!IsDefault)
                {
                    Rectangle badge = new Rectangle(x + 74, 12, 56, 18);
                    using (GraphicsPath p = LoginPaint.Rounded(badge, 9))
                    using (SolidBrush b = new SolidBrush(Color.FromArgb(254, 243, 199)))
                        g.FillPath(b, p);
                    TextRenderer.DrawText(g, "CUSTOM", caption, badge, WarningText, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
                }

                Color valueColor = IsDefault ? Color.FromArgb(17, 24, 39) : WarningText;
                TextFormatFlags flags = TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis | TextFormatFlags.NoPrefix;
                using (Font key = new Font("Roboto", 9.5F))
                using (Font value = new Font("Roboto", 10F, FontStyle.Bold))
                {
                    TextRenderer.DrawText(g, "POS", key, new Rectangle(x, 36, 70, 22), Muted, flags);
                    TextRenderer.DrawText(g, PosDatabase, value, new Rectangle(valueX, 36, valueWidth, 22), valueColor, flags);
                    TextRenderer.DrawText(g, "Merchant", key, new Rectangle(x, 60, 70, 22), Muted, flags);
                    TextRenderer.DrawText(g, MerchantDatabase, value, new Rectangle(valueX, 60, valueWidth, 22), valueColor, flags);
                }
            }
        }
    }

    internal static class LoginPaint
    {
        public static GraphicsPath Rounded(RectangleF r, float radius)
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

        /// <summary>Draws a black glyph icon in the given colour.</summary>
        public static void DrawTinted(Graphics g, Image image, Rectangle bounds, Color color)
        {
            ColorMatrix m = new ColorMatrix(new[]
            {
                new float[] { 0, 0, 0, 0, 0 },
                new float[] { 0, 0, 0, 0, 0 },
                new float[] { 0, 0, 0, 0, 0 },
                new float[] { 0, 0, 0, 1, 0 },
                new float[] { color.R / 255f, color.G / 255f, color.B / 255f, 0, 1 },
            });
            using (ImageAttributes attributes = new ImageAttributes())
            {
                attributes.SetColorMatrix(m);
                g.DrawImage(image, bounds, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, attributes);
            }
        }

        /// <summary>A small stacked-cylinder "database" icon.</summary>
        public static void DrawDatabaseGlyph(Graphics g, Rectangle r, Color color)
        {
            float ry = r.Height * 0.16f;
            using (Pen pen = new Pen(color, 2f))
            {
                g.DrawEllipse(pen, r.X, r.Y, r.Width, ry * 2);
                g.DrawLine(pen, r.X, r.Y + ry, r.X, r.Bottom - ry);
                g.DrawLine(pen, r.Right, r.Y + ry, r.Right, r.Bottom - ry);
                g.DrawArc(pen, r.X, r.Y + (r.Height - ry * 2) / 2f, r.Width, ry * 2, 0, 180);
                g.DrawArc(pen, r.X, r.Bottom - ry * 2, r.Width, ry * 2, 0, 180);
            }
        }
    }
}
