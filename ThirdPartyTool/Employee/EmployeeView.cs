using System;
using System.Windows.Forms;

namespace ThirdPartyTool.Employee
{
    public partial class EmployeeView : UserControl
    {
        public EmployeeView()
        {
            InitializeComponent();

            btnRefresh.Click += BtnRefresh_Click;
        }

        private void BtnRefresh_Click(object sender, System.EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                BLogicLogger.Exception("BtnRefresh_Click in EmployeeView", ex);
            }
        }
    }
}
