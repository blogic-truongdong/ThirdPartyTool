using BLogicPOS7SupportTool.Services;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace BLogicPOS7SupportTool.Dashboard.Dialogs
{
    partial class ClearSalesDataForm : Form
    {
        private readonly AdvancedService _service = new AdvancedService();
        private Point _dragStart;

        public ClearSalesDataForm()
        {
            InitializeComponent();

            // Default to "today only"; the user narrows/widens the range.
            dtpStart.Value = DateTime.Today;
            dtpEnd.Value = DateTime.Today;

            pnlTitleBar.MouseDown += (s, e) => { if (e.Button == MouseButtons.Left) _dragStart = e.Location; };
            pnlTitleBar.MouseMove += (s, e) => { if (e.Button == MouseButtons.Left) Location = new Point(Location.X + e.X - _dragStart.X, Location.Y + e.Y - _dragStart.Y); };

            btnCloseForm.Click += (s, e) => { DialogResult = DialogResult.Cancel; Close(); };
            btnCancel.Click += (s, e) => { DialogResult = DialogResult.Cancel; Close(); };
            btnClear.Click += (s, e) => ClearSalesData();
        }

        private void ClearSalesData()
        {
            DateTime start = dtpStart.Value.Date;
            DateTime end = dtpEnd.Value.Date;

            if (start > end)
            {
                MessageBox.Show("The start date must be on or before the end date.",
                    "Clear Sales Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Include the whole end day (up to 23:59:59) in the BETWEEN range.
            DateTime endInclusive = end.AddDays(1).AddSeconds(-1);

            var confirm = MessageBox.Show(
                $"Close all open (OPER) tickets with $0 paid created between " +
                $"{start:d} and {end:d}?\n\n" +
                "Their status will be set to Closed (CLO). This cannot be undone.",
                "Clear Sales Data", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm != DialogResult.Yes) return;

            int rows = _service.ClearSalesData(start, endInclusive);

            if (rows >= 0)
            {
                MessageBox.Show(
                    $"{rows} ticket(s) were closed.",
                    "Clear Sales Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show("The sales data could not be cleared. No changes were made.",
                    "Clear Sales Data", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
