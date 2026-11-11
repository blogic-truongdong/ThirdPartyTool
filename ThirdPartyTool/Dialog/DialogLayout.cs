using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ThirdPartyTool.Dialog
{
    public enum DialogKind
    {
        Confirm,
        Information,
        Error
    }

    /// <summary>
    /// Shared look for the small message dialogs: round icon on the left, title and
    /// message beside it, buttons bottom-right. The height follows the message text.
    /// </summary>
    internal static class DialogLayout
    {
        public static readonly Color Primary = Color.FromArgb(42, 108, 228);
        public static readonly Color Danger = Color.FromArgb(220, 38, 38);
        private static readonly Color PrimaryTint = Color.FromArgb(232, 240, 254);
        private static readonly Color DangerTint = Color.FromArgb(254, 226, 226);

        private const int DialogWidth = 440;
        private const int Pad = 24;
        private const int IconSize = 40;
        private const int TextLeft = Pad + IconSize + 16;
        private const int ButtonHeight = 40;
        private const int ButtonGap = 10;
        private const int MaxMessageHeight = 360;

        /// <summary>Positions everything and sizes the form. Buttons are given right-to-left.</summary>
        public static void Arrange(Form form, Panel surface, Label title, Label message, DialogKind kind, params Control[] buttonsRightToLeft)
        {
            int textWidth = DialogWidth - 2 - TextLeft - Pad;

            title.AutoSize = false;
            title.Location = new Point(TextLeft, Pad - 2);
            title.Size = new Size(textWidth, 26);

            Size measured = TextRenderer.MeasureText(message.Text ?? "", message.Font, new Size(textWidth, int.MaxValue),
                TextFormatFlags.WordBreak | TextFormatFlags.NoPrefix);
            int messageHeight = Math.Min(MaxMessageHeight, Math.Max(20, measured.Height + 2));
            message.AutoSize = false;
            message.Location = new Point(TextLeft, title.Bottom + 6);
            message.Size = new Size(textWidth, messageHeight);

            int buttonsTop = Math.Max(message.Bottom, Pad + IconSize) + Pad;
            int right = DialogWidth - 2 - Pad;
            foreach (Control button in buttonsRightToLeft)
            {
                button.Location = new Point(right - button.Width, buttonsTop);
                button.Height = ButtonHeight;
                right = button.Left - ButtonGap;
            }

            form.ClientSize = new Size(DialogWidth, buttonsTop + ButtonHeight + 20 + 2);

            surface.Paint += (s, e) => DrawIcon(e.Graphics, kind);
        }

        /// <summary>Esc closes like the "No"/"OK" button; Enter only accepts on non-confirm dialogs.</summary>
        public static void BindKeys(Form form, DialogResult escapeResult, DialogResult? enterResult)
        {
            form.KeyPreview = true;
            form.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Escape)
                {
                    form.DialogResult = escapeResult;
                    form.Close();
                }
                else if (e.KeyCode == Keys.Enter && enterResult.HasValue)
                {
                    form.DialogResult = enterResult.Value;
                    form.Close();
                }
            };
        }

        private static void DrawIcon(Graphics g, DialogKind kind)
        {
            bool danger = kind == DialogKind.Error;
            string glyph = kind == DialogKind.Confirm ? "?" : kind == DialogKind.Error ? "!" : "i";
            Rectangle circle = new Rectangle(Pad, Pad, IconSize, IconSize);

            g.SmoothingMode = SmoothingMode.AntiAlias;
            using (SolidBrush brush = new SolidBrush(danger ? DangerTint : PrimaryTint))
                g.FillEllipse(brush, circle);
            using (Font font = new Font("Roboto", 15F, FontStyle.Bold))
                TextRenderer.DrawText(g, glyph, font, circle, danger ? Danger : Primary,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.NoPrefix);
        }
    }
}
