using System;
using System.Drawing;
using System.Linq;
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
            Application.OpenForms
    .OfType<Form>()
    .ToList()
    .ForEach(f => f.Icon = new Icon("Resources/Icon_BLogic_Tool.ico"));
            Application.Run(new HomeForm());
        }
    }
}
