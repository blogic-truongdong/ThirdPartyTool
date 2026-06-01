using System.Drawing;
using System.Windows.Forms;

namespace BLogicPOS7SupportTool.Core
{
    sealed class DimOverlay : Form
    {
        public DimOverlay(Form owner)
        {
            FormBorderStyle = FormBorderStyle.None;
            BackColor = Color.Black;
            Opacity = 0.45;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.Manual;
            Bounds = owner.Bounds;
            Owner = owner;
        }

        // Show the overlay, open the dialog on top, then close the overlay.
        public static DialogResult ShowDialog(Form owner, Form dialog)
        {
            using (var overlay = new DimOverlay(owner))
            {
                overlay.Show(owner);
                var result = dialog.ShowDialog(overlay);
                overlay.Close();
                return result;
            }
        }
    }
}
