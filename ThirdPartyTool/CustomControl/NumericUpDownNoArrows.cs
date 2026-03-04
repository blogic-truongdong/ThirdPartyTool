using System;
using System.Windows.Forms;

namespace ThirdPartyTool.CustomControl
{
    public partial class NumericUpDownNoArrows : NumericUpDown
    {
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

            // Hide arrow buttons
            if (Controls.Count > 0)
            {
                Controls[0].Visible = false;
            }
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            // Disable mouse wheel change
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            // Disable Up/Down key change
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
            {
                e.SuppressKeyPress = true;
                return;
            }

            base.OnKeyDown(e);
        }
    }
}
