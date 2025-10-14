using System;
using System.Windows.Forms;

namespace ThirdPartyTool
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //Icon appIcon = new Icon(Application.StartupPath + @"\Resources\Icon_BLogic_Tool.ico");

            EnterDatabaseForm enterDatabaseForm = new EnterDatabaseForm();
            //enterDatabaseForm.Icon = appIcon;
            var result = enterDatabaseForm.ShowDialog();
            if (result != DialogResult.OK)
            {
                Application.Exit();
                return;
            }

            StartForm connectDatabaseForm = new StartForm(enterDatabaseForm.txtBLogicPOS7.Text.Trim(), enterDatabaseForm.txtMerchant.Text.Trim());
            //connectDatabaseForm.Icon = appIcon;
            connectDatabaseForm.ShowDialog();

            Helper.BLOGICPOS7_Name = enterDatabaseForm.txtBLogicPOS7.Text.Trim();
            Helper.Merchant_Name = enterDatabaseForm.txtMerchant.Text.Trim();

            HomeForm homeForm = new HomeForm();
            //homeForm.Icon = appIcon;
            Application.Run(homeForm);
        }
    }
}
