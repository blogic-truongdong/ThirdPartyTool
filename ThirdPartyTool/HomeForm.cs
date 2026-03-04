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
        Shifts,
        AdvancedFuntion
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

            btnEmployee.Click += BtnOption_Click;
            btnClose.Click += BtnClose_Click;
            btnHome.Click += BtnOption_Click;
            btnMerchant.Click += BtnOption_Click;
            btnHotFix.Click += BtnOption_Click;
            btnItem.Click += BtnOption_Click;
            btnEmployeePaycor.Click += BtnOption_Click;
            btnReceipt_V2.Click += BtnOption_Click;
            btnShifts.Click += BtnOption_Click;
            btnAdvancedFuntion.Click += BtnOption_Click;
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

                    btnEmployeePaycor.BackColor = Tab_BackColorSelected;
                    btnEmployeePaycor.ForeColor = Tab_FoceColorSelected;

                    employeeLinkPaycorView21.BringToFront();

                    employeeLinkPaycorView21.BtnRefresh_Click(sender, e);

                    employeeLinkPaycorView21.txtFocus.Focus();
                }
                else if (control.Name == "btnItem")
                {
                    if (CurrentTab == HomeTab.Item)
                        return;

                    CurrentTab = HomeTab.Item;

                    ResetTabColor();

                    btnItem.BackColor = Tab_BackColorSelected;
                    btnItem.ForeColor = Tab_FoceColorSelected;

                    itemView1.BringToFront();
                }
                else if (control.Name == btnHotFix.Name)
                {
                    if (CurrentTab == HomeTab.HotFix)
                        return;

                    CurrentTab = HomeTab.HotFix;

                    ResetTabColor();

                    hotFixView.BringToFront();
                }
                else if (control.Name == btnMerchant.Name)
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
                else if (control.Name == btnHome.Name)
                {
                    if (CurrentTab == HomeTab.Home)
                        return;

                    CurrentTab = HomeTab.Home;

                    ResetTabColor();

                    btnHome.BackColor = Tab_BackColorSelected;
                    btnHome.ForeColor = Tab_FoceColorSelected;

                    welcomeView.BringToFront();
                }
                else if (control.Name == btnEmployee.Name)
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
            btnShifts.BackColor = Tab_BackColorDefault;
            btnShifts.ForeColor = Tab_FoceColorDefault;
            btnAdvancedFuntion.BackColor = Tab_BackColorDefault;
            btnAdvancedFuntion.ForeColor = Tab_FoceColorDefault;
        }

        private void ActiveTabColor(System.Windows.Forms.Control control)
        {
            control.BackColor = Tab_BackColorSelected;
            control.ForeColor = Tab_FoceColorSelected;
        }
    }
}
