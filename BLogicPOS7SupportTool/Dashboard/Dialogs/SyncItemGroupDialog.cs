using System;
using System.Drawing;
using System.Windows.Forms;

namespace BLogicPOS7SupportTool.Dashboard.Dialogs
{
    public partial class SyncItemGroupDialog : Form
    {
        public DateTime StartDate => dtpStart.Value.Date;
        public DateTime EndDate => dtpEnd.Value.Date;

        private Point _dragScreenStart;

        public SyncItemGroupDialog()
        {
            InitializeComponent();
            dtpStart.Value = DateTime.Today;
            dtpEnd.Value = DateTime.Today;
        }

        private void TitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                _dragScreenStart = Control.MousePosition;
        }

        private void TitleBar_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                var curr = Control.MousePosition;
                Location = new Point(
                    Location.X + curr.X - _dragScreenStart.X,
                    Location.Y + curr.Y - _dragScreenStart.Y);
                _dragScreenStart = curr;
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (StartDate > EndDate)
            {
                MessageBox.Show("Start Date must be before or equal to End Date.",
                    "Invalid Range", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnCloseDlg_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
