using System;
using System.Drawing;
using System.Windows.Forms;

namespace ThirdPartyTool
{
    public enum HomeTab
    {
        Home,
        Employee,
        Receipt,
        Merchant,
        HotFix,
        Item,
        EmployeePaycor,
        Receipt_v2,
    }
    public partial class HomeForm : Form
    {
        private Color DefaultBackColor = Color.White;
        private Color Tab_BackColorDefault = Color.FromArgb(41, 63, 84);
        private Color Tab_FoceColorDefault = Color.White;
        private Color Tab_BackColorSelected = Color.Blue;
        private Color Tab_FoceColorSelected = Color.White;
        private HomeTab CurrentTab = HomeTab.Home;

        public HomeForm()
        {
            InitializeComponent();

            this.Load += HomeForm_Load;

            btnEmployee.Click += BtnEmployee_Click;
            btnClose.Click += BtnClose_Click;
            btnHome.Click += BtnHome_Click;
            btnMerchant.Click += BtnMerchant_Click;
            btnHotFix.Click += BtnHotFix_Click;
            btnItem.Click += BtnItem_Click;
            btnEmployeePaycor.Click += BtnEmployeePaycor_Click;
            btnReceipt_V2.Click += BtnReceipt_V2_Click;
        }

        private void BtnReceipt_V2_Click(object sender, EventArgs e)
        {
            if (CurrentTab == HomeTab.Receipt_v2)
                return;

            CurrentTab = HomeTab.Receipt_v2;

            ResetTabColor();

            btnReceipt_V2.BackColor = Tab_BackColorSelected;
            btnReceipt_V2.ForeColor = Tab_FoceColorSelected;

            receiptView_v2.BringToFront();
        }

        private void BtnEmployeePaycor_Click(object sender, EventArgs e)
        {
            if (CurrentTab == HomeTab.EmployeePaycor)
                return;

            CurrentTab = HomeTab.EmployeePaycor;

            ResetTabColor();

            btnEmployeePaycor.BackColor = Tab_BackColorSelected;
            btnEmployeePaycor.ForeColor = Tab_FoceColorSelected;

            employeeLinkPaycorView21.BringToFront();

            employeeLinkPaycorView21.BtnRefresh_Click(sender, e);

            employeeLinkPaycorView21.txtFocus.Focus();
        }

        private void BtnItem_Click(object sender, EventArgs e)
        {
            if (CurrentTab == HomeTab.Item)
                return;

            CurrentTab = HomeTab.Item;

            ResetTabColor();

            btnItem.BackColor = Tab_BackColorSelected;
            btnItem.ForeColor = Tab_FoceColorSelected;

            itemView1.BringToFront();
        }

        private void BtnHotFix_Click(object sender, EventArgs e)
        {
            if (CurrentTab == HomeTab.HotFix)
                return;

            CurrentTab = HomeTab.HotFix;

            ResetTabColor();

            hotFixView.BringToFront();
        }

        private void BtnMerchant_Click(object sender, EventArgs e)
        {
            if (CurrentTab == HomeTab.Merchant)
                return;

            CurrentTab = HomeTab.Merchant;

            ResetTabColor();

            btnMerchant.BackColor = Tab_BackColorSelected;
            btnMerchant.ForeColor = Tab_FoceColorSelected;

            settleBatchOfflineView.BringToFront();

            settleBatchOfflineView.BtnRefresh_Click(sender, e);
        }

        private void BtnHome_Click(object sender, EventArgs e)
        {
            if (CurrentTab == HomeTab.Home)
                return;

            CurrentTab = HomeTab.Home;

            ResetTabColor();

            btnHome.BackColor = Tab_BackColorSelected;
            btnHome.ForeColor = Tab_FoceColorSelected;

            welcomeView.BringToFront();
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void BtnEmployee_Click(object sender, EventArgs e)
        {
            if (CurrentTab == HomeTab.Employee)
                return;

            receiptView.txtFocus.Focus();

            CurrentTab = HomeTab.Employee;

            ResetTabColor();

            btnEmployee.BackColor = Tab_BackColorSelected;
            btnEmployee.ForeColor = Tab_FoceColorSelected;

            employeeView.BringToFront();

            employeeView.BtnRefresh_Click(sender, e);

            employeeView.txtFocus.Focus();
        }

        private void HomeForm_Load(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(Helper.BLOGICPOS7_Connection) || string.IsNullOrWhiteSpace(Helper.Merchant_Connection))
                    this.Close();
            }
            catch (Exception ex)
            {
                BLogicLogger.Exception("HomeForm_Load in HomeForm", ex);
            }

            //SettleBatchOfflineForm form = new SettleBatchOfflineForm();
            //form.ShowDialog();
            //Application.Exit();
        }

        private void ResetTabColor()
        {
            btnHome.BackColor = Tab_BackColorDefault;
            btnHome.ForeColor = Tab_FoceColorDefault;
            btnEmployee.BackColor = Tab_BackColorDefault;
            btnEmployee.ForeColor = Tab_FoceColorDefault;
            btnMerchant.BackColor = Tab_BackColorDefault;
            btnMerchant.ForeColor = Tab_FoceColorDefault;
            btnEmployeePaycor.BackColor = Tab_BackColorDefault;
            btnEmployeePaycor.ForeColor = Tab_FoceColorDefault;
            btnItem.BackColor = Tab_BackColorDefault;
            btnItem.ForeColor = Tab_FoceColorDefault;
            btnReceipt_V2.BackColor = Tab_BackColorDefault;
            btnReceipt_V2.ForeColor = Tab_FoceColorDefault;

        }
    }
}
