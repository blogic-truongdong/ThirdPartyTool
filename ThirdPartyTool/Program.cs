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

            // Change history of this tool: dbo.UpdateToolAuditLog in the POS database (created here when missing).
            if (!string.IsNullOrWhiteSpace(Helper.BLOGICPOS7_Connection) && !AuditLog.EnsureTable(Helper.BLOGICPOS7_Connection))
            {
                new Dialog.InformationDialogForm($"The change history table ({AuditLog.TableName}) could not be created, so changes are only written to the log file." +
                    Environment.NewLine + AuditLog.LastError).ShowDialog();
            }

            HomeForm homeForm = new HomeForm();
            Helper.MainForm = homeForm;
            //homeForm.Icon = appIcon;
            Application.Run(homeForm);
        }
    }
}
