using BLogicPOS7SupportTool.Core;
using BLogicPOS7SupportTool.Dashboard;
using BLogicPOS7SupportTool.Login;
using BLogicPOS7SupportTool.Setup;
using log4net;
using log4net.Config;
using System;
using System.IO;
using System.Windows.Forms;

namespace BLogicPOS7SupportTool
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            XmlConfigurator.Configure(new FileInfo("log4net.config"));
            AppLogger.Info("Application starting");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.ThreadException += (s, e) =>
            {
                AppLogger.Error("Unhandled UI thread exception", e.Exception);
                MessageBox.Show(e.Exception.Message, "Unexpected Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            };

            using (var login = new LoginForm())
            {
                if (login.ShowDialog() != DialogResult.OK)
                {
                    AppLogger.Info("Login cancelled");
                    return;
                }
            }

            AppLogger.Info("Login succeeded");

            using (var setup = new DatabaseSetupForm())
            {
                if (setup.ShowDialog() != DialogResult.OK)
                {
                    AppLogger.Info("Database setup cancelled");
                    return;
                }
            }

            AppLogger.Info($"Connected — BLogicPOS7: {AppSession.BLogicPOS7DatabaseName}, Merchant: {AppSession.MerchantDatabaseName}");

            // Make sure the script-audit table exists before the dashboard runs.
            AppSession.EnsureScriptLogTable();

            // Cache all configurations so the dashboard can read them without DB hits.
            AppSession.LoadConfigurations();

            Application.Run(new MainForm());

            AppLogger.Info("Application exiting");
        }
    }
}
