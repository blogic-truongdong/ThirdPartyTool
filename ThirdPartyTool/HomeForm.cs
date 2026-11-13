using System;
using System.Drawing;
using System.Windows.Forms;

namespace ThirdPartyTool
{
    public enum HomeTab
    {
        Default,
        Home,
        Employee,
        Receipt,
        Merchant,
        HotFix,
        Item,
        EmployeePaycor,
        Receipt_v2,
        Shifts,
        AdvancedFuntion
    }
    public partial class HomeForm : Form
    {
        private Color DefaultBackColor = Color.White;
        private Color Tab_BackColorDefault = Color.White;
        private Color Tab_FoceColorDefault = Color.Black;
        private Color Tab_BackColorSelected = ColorTranslator.FromHtml("#2A6CE40");
        private Color Tab_FoceColorSelected = Color.White;
        private HomeTab CurrentTab = HomeTab.Default;

        public HomeForm()
        {
            InitializeComponent();
            label4.Text = Helper.VersionText;
            label3.Text = Helper.ReleaseText;

            this.Load += HomeForm_Load;

            btnEmployee.Click += BtnOption_Click;
            btnHome.Click += BtnOption_Click;
            btnMerchant.Click += BtnOption_Click;
            btnHotFix.Click += BtnOption_Click;
            btnItem.Click += BtnOption_Click;
            btnEmployeePaycor.Click += BtnOption_Click;
            btnReceipt_V2.Click += BtnOption_Click;
            btnShifts.Click += BtnOption_Click;
            btnAdvancedFuntion.Click += BtnOption_Click;

            InitNavButtons();
            InitChrome();
        }

        private void BtnOption_Click(object sender, EventArgs e)
        {
            try
            {
                System.Windows.Forms.Control control = sender as System.Windows.Forms.Control;

                if (control == null)
                    return;

                if (control.Name == btnAdvancedFuntion.Name)
                {
                    if (CurrentTab == HomeTab.AdvancedFuntion)
                        return;

                    CurrentTab = HomeTab.AdvancedFuntion;

                    ResetTabColor();
                    ActiveTabColor(btnAdvancedFuntion);

                    advancedFuntionView.BringToFront();
                }
                else if (control.Name == btnShifts.Name)
                {
                    if (CurrentTab == HomeTab.Shifts)
                        return;

                    CurrentTab = HomeTab.Shifts;

                    ResetTabColor();
                    ActiveTabColor(btnShifts);

                    shiftView.BringToFront();
                }
                else if (control.Name == btnReceipt_V2.Name)
                {
                    if (CurrentTab == HomeTab.Receipt_v2)
                        return;

                    CurrentTab = HomeTab.Receipt_v2;

                    ResetTabColor();
                    ActiveTabColor(btnReceipt_V2);

                    receiptView_v2.BringToFront();
                }
                else if (control.Name == btnEmployeePaycor.Name)
                {
                    if (CurrentTab == HomeTab.EmployeePaycor)
                        return;

                    CurrentTab = HomeTab.EmployeePaycor;

                    ResetTabColor();
                    ActiveTabColor(btnEmployeePaycor);

                    employeeLinkPaycorView21.BringToFront();

                    employeeLinkPaycorView21.BtnRefresh_Click(sender, e);

                    employeeLinkPaycorView21.txtFocus.Focus();
                }
                else if (control.Name == btnItem.Name)
                {
                    if (CurrentTab == HomeTab.Item)
                        return;

                    CurrentTab = HomeTab.Item;

                    ResetTabColor();
                    ActiveTabColor(btnItem);

                    itemView1.BringToFront();
                }
                else if (control.Name == btnHotFix.Name)
                {
                    if (CurrentTab == HomeTab.HotFix)
                        return;

                    CurrentTab = HomeTab.HotFix;

                    ResetTabColor();
                    ActiveTabColor(btnHotFix);

                    hotFixView.BringToFront();
                }
                else if (control.Name == btnMerchant.Name)
                {
                    if (CurrentTab == HomeTab.Merchant)
                        return;

                    CurrentTab = HomeTab.Merchant;

                    ResetTabColor();
                    ActiveTabColor(btnMerchant);

                    settleBatchOfflineView.BringToFront();

                    settleBatchOfflineView.BtnRefresh_Click(sender, e);
                }
                else if (control.Name == btnHome.Name)
                {
                    if (CurrentTab == HomeTab.Home)
                        return;

                    CurrentTab = HomeTab.Home;

                    ResetTabColor();
                    ActiveTabColor(btnHome);

                    welcomeView.BringToFront();
                }
                else if (control.Name == btnEmployee.Name)
                {
                    if (CurrentTab == HomeTab.Employee)
                        return;

                    receiptView.txtFocus.Focus();

                    CurrentTab = HomeTab.Employee;

                    ResetTabColor();
                    ActiveTabColor(btnEmployee);

                    employeeView.BringToFront();

                    employeeView.BtnRefresh_Click(sender, e);

                    employeeView.txtFocus.Focus();
                }
            }
            catch (Exception ex)
            {
                BLogicLogger.Exception("BtnOption_Click in HomeForm", ex);
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void HomeForm_Load(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(Helper.BLOGICPOS7_Connection) || string.IsNullOrWhiteSpace(Helper.Merchant_Connection))
                    this.Close();

                btnHome.PerformClick();
            }
            catch (Exception ex)
            {
                BLogicLogger.Exception("HomeForm_Load in HomeForm", ex);
            }

            //SettleBatchOfflineForm form = new SettleBatchOfflineForm();
            //form.ShowDialog();
            //Application.Exit();
        }

        // Sidebar look
        private static readonly Color NavBack = Color.FromArgb(248, 249, 251);
        private static readonly Color NavText = Color.FromArgb(55, 65, 81);
        private static readonly Color NavHover = Color.FromArgb(238, 240, 244);
        private static readonly Color NavActiveBack = Color.FromArgb(232, 240, 254);
        private static readonly Color NavActiveText = Color.FromArgb(42, 108, 228);
        private Button activeNavButton;

        private Button[] NavButtons
        {
            get
            {
                return new[] { btnHome, btnReceipt_V2, btnEmployee, btnMerchant, btnItem, btnEmployeePaycor, btnShifts, btnAdvancedFuntion };
            }
        }

        private void InitNavButtons()
        {
            foreach (Button button in NavButtons)
                button.Paint += NavButton_Paint;
            ResetTabColor();
        }

        /// <summary>Draws the accent bar on the left edge of the active nav item.</summary>
        private void NavButton_Paint(object sender, PaintEventArgs e)
        {
            if (sender != activeNavButton)
                return;
            Button button = (Button)sender;
            using (SolidBrush brush = new SolidBrush(NavActiveText))
                e.Graphics.FillRectangle(brush, 0, 8, 3, button.Height - 16);
        }

        private void ResetTabColor()
        {
            foreach (Button button in NavButtons)
            {
                button.BackColor = NavBack;
                button.ForeColor = NavText;
                button.FlatAppearance.MouseOverBackColor = NavHover;
            }
            Button previous = activeNavButton;
            activeNavButton = null;
            if (previous != null)
                previous.Invalidate();
        }

        private void ActiveTabColor(System.Windows.Forms.Control control)
        {
            Button button = control as Button;
            if (button == null)
                return;
            button.BackColor = NavActiveBack;
            button.ForeColor = NavActiveText;
            button.FlatAppearance.MouseOverBackColor = NavActiveBack;
            activeNavButton = button;
            button.Invalidate();
        }
    }
}
