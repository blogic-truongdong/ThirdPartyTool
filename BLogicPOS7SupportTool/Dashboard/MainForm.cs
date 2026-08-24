using BLogicPOS7SupportTool.Core;
using BLogicPOS7SupportTool.Dashboard.Pages;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace BLogicPOS7SupportTool.Dashboard
{
    public partial class MainForm : Form
    {
        HomePage homePage = new HomePage();
        ItemsPage itemsPage = new ItemsPage();
        ReceiptPage receiptPage = new ReceiptPage();
        ShiftPage shiftPage = new ShiftPage();
        CashInOutPage cashInOutPage = new CashInOutPage();
        ClearTableStatusPage clearTableStatusPage = new ClearTableStatusPage();
        AdvancedPage advancedPage = new AdvancedPage();
        InsertTicketPage insertTicketPage = new InsertTicketPage();
        BatchManagerPage batchManagerPage = new BatchManagerPage();
        private Panel _activeNav;
        private Panel[] _navPanels;
        private Point _dragScreenStart;

        public MainForm()
        {
            InitializeComponent();
            _navPanels = new[] { navHome, navReceipts, navItems, navShifts, navCashInOut, navClearTable, navEmployees, navBatchMgr, navTransactions, navAdvanced, navInsertTicket };
            SetActiveNav(navHome);

            homePage.Dock = DockStyle.Fill;
            pnlContent.Controls.Add(homePage);

            itemsPage.Dock = DockStyle.Fill;
            pnlContent.Controls.Add(itemsPage);

            receiptPage.Dock = DockStyle.Fill;
            pnlContent.Controls.Add(receiptPage);

            shiftPage.Dock = DockStyle.Fill;
            pnlContent.Controls.Add(shiftPage);

            cashInOutPage.Dock = DockStyle.Fill;
            pnlContent.Controls.Add(cashInOutPage);

            clearTableStatusPage.Dock = DockStyle.Fill;
            pnlContent.Controls.Add(clearTableStatusPage);

            advancedPage.Dock = DockStyle.Fill;
            pnlContent.Controls.Add(advancedPage);

            insertTicketPage.Dock = DockStyle.Fill;
            pnlContent.Controls.Add(insertTicketPage);

            batchManagerPage.Dock = DockStyle.Fill;
            pnlContent.Controls.Add(batchManagerPage);

            NavigateTo(homePage);
        }

        private void NavigateTo(UserControl page)
        {
            page.BringToFront();
        }

        private void SetActiveNav(Panel target)
        {
            foreach (var nav in _navPanels)
            {
                bool isActive = nav == target;
                nav.BackColor = isActive ? Theme.PrimaryBlue : Theme.SidebarBg;
                foreach (Control c in nav.Controls)
                    if (c is Label lbl)
                        lbl.ForeColor = isActive ? Theme.NavTextActive : Theme.NavText;
            }
            _activeNav = target;
        }

        private Panel GetNavPanel(object sender)
        {
            if (sender is Panel p) return p;
            return ((Control)sender).Parent as Panel;
        }

        private void NavItem_MouseEnter(object sender, EventArgs e)
        {
            var nav = GetNavPanel(sender);
            if (nav != null && nav != _activeNav)
                nav.BackColor = Theme.NavHover;
        }

        private void NavItem_MouseLeave(object sender, EventArgs e)
        {
            var nav = GetNavPanel(sender);
            if (nav != null && nav != _activeNav &&
                !nav.ClientRectangle.Contains(nav.PointToClient(Cursor.Position)))
                nav.BackColor = Theme.SidebarBg;
        }

        private void navHome_Click(object sender, EventArgs e)
        {
            SetActiveNav(navHome);
            NavigateTo(homePage);
        }

        private void navReceipts_Click(object sender, EventArgs e)
        {
            SetActiveNav(navReceipts);
            NavigateTo(receiptPage);
        }

        private void navItems_Click(object sender, EventArgs e)
        {
            SetActiveNav(navItems);
            NavigateTo(itemsPage);
        }

        private void navShifts_Click(object sender, EventArgs e)
        {
            SetActiveNav(navShifts);
            NavigateTo(shiftPage);
        }

        private void navCashInOut_Click(object sender, EventArgs e)
        {
            SetActiveNav(navCashInOut);
            NavigateTo(cashInOutPage);
        }

        private void navClearTable_Click(object sender, EventArgs e)
        {
            SetActiveNav(navClearTable);
            NavigateTo(clearTableStatusPage);
        }

        private void navEmployees_Click(object sender, EventArgs e)
        {
            SetActiveNav(navEmployees);
        }

        private void navBatchMgr_Click(object sender, EventArgs e)
        {
            SetActiveNav(navBatchMgr);
            NavigateTo(batchManagerPage);
            batchManagerPage.LoadBatches();
        }

        private void navTransactions_Click(object sender, EventArgs e)
        {
            SetActiveNav(navTransactions);
        }

        private void navAdvanced_Click(object sender, EventArgs e)
        {
            SetActiveNav(navAdvanced);
            NavigateTo(advancedPage);
        }

        private void navInsertTicket_Click(object sender, EventArgs e)
        {
            SetActiveNav(navInsertTicket);
            NavigateTo(insertTicketPage);
        }
    }
}
