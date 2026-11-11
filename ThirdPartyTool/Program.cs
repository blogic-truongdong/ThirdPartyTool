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

            //Application.Run(new Form1());
            //return;

            //Icon appIcon = new Icon(Application.StartupPath + @"\Resources\Icon_BLogic_Tool.ico");

            LoginForm loginForm = new LoginForm();
            if (loginForm.ShowDialog() != DialogResult.OK)
            {
                Application.Exit();
                return;
            }

            // The databases are chosen on the login screen ("Change"); default BLogicPOS7 / Merchant.
            StartForm connectDatabaseForm = new StartForm(loginForm.PosDatabase, loginForm.MerchantDatabase);
            //connectDatabaseForm.Icon = appIcon;
            connectDatabaseForm.ShowDialog();

            Helper.BLOGICPOS7_Name = loginForm.PosDatabase;
            Helper.Merchant_Name = loginForm.MerchantDatabase;

            HomeForm homeForm = new HomeForm();
            Helper.MainForm = homeForm;
            //homeForm.Icon = appIcon;
            Application.Run(homeForm);
        }
    }
}
