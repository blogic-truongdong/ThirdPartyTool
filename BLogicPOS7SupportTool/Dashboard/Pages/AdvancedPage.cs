using BLogicPOS7SupportTool.Core;
using BLogicPOS7SupportTool.Dashboard.Dialogs;
using System;
using System.Windows.Forms;

namespace BLogicPOS7SupportTool.Dashboard.Pages
{
    public partial class AdvancedPage : UserControl
    {
        public AdvancedPage()
        {
            InitializeComponent();
            btnClearSales.Click += btnClearSales_Click;
            btnInsertTran.Click += btnInsertTran_Click;
        }

        private void btnClearSales_Click(object sender, EventArgs e)
        {
            using (var dlg = new ClearSalesDataForm())
                DimOverlay.ShowDialog(ParentForm, dlg);
        }

        private void btnInsertTran_Click(object sender, EventArgs e)
        {
            using (var dlg = new InsertTransactionForm())
                DimOverlay.ShowDialog(ParentForm, dlg);
        }
    }
}
