using BLogicSystems.XPos.Utilites;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using ThirdPartyTool.Dialog;

namespace ThirdPartyTool
{
    public partial class HotFixView : UserControl
    {
        public HotFixView()
        {
            InitializeComponent();

            btnUpgradeTaxPercentForTickets.Click += BtnUpgradeTaxPercentForTickets_Click;

            btnSyncRole.Click += BtnSyncRole_Click;
        }

        private void BtnSyncRole_Click(object sender, System.EventArgs e)
        {
            try
            {
                ConfirmDialogForm confirmFirmDialog = new ConfirmDialogForm(Const.ConfirmTitle, $"Are you sure you want to re-sync all Role for Employees?");
                var result = confirmFirmDialog.ShowDialog();
                if (result == DialogResult.No)
                    return;

                DataTable dtEmployee = new DataTable();

                using (SqlCommand sqlCommand = new SqlCommand())
                using (SqlConnection conn = new SqlConnection(Helper.BLOGICPOS7_Connection))
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    sqlCommand.Connection = conn;
                    sqlCommand.CommandType = CommandType.Text;

                    sqlCommand.CommandText = $"select er.EmployeeID, er.RolesID from EmployeeRoles er left join Employees e on er.EmployeeID = e.EmployeeID " +
                        $"where (er.IsDeleted is null or er.IsDeleted = 0) and e.EmployeeID > 0 " +
                        $"group by er.EmployeeID, er.RolesID " +
                        $"having count(*) = 1 ";
                    var reader = sqlCommand.ExecuteReader();
                    dtEmployee.Load(reader);
                    reader.Close();
                }

                foreach (DataRow item in dtEmployee.Rows)
                {
                    ProcessForm processForm = new ProcessForm();
                    processForm.CurrentProcessName = ProcessNameEnum.ProcessSyncRoleForEmployee;
                    processForm.EmployeeView_EmployeeID = Utility.GetIntFromObject(item["EmployeeID"]);
                    processForm.EmployeeView_RolesID = Utility.GetIntFromObject(item["RolesID"]);
                    processForm.EmployeeView_StartDate = new DateTime(2025, 10, 1, 0, 0, 0);
                    processForm.EmployeeView_EndDate = new DateTime(2025, 12, 1, 0, 0, 0);
                    processForm.ShowDialog();
                }

                InformationDialogForm information = new InformationDialogForm($"Re-sync all Role for Employees Done");
                information.ShowDialog();
            }
            catch (System.Exception ex)
            {
                BLogicLogger.Exception("BtnSyncRole_Click in HotFixView", ex);
            }
        }

        private void BtnUpgradeTaxPercentForTickets_Click(object sender, System.EventArgs e)
        {
            try
            {
                ConfirmDialogForm confirmFirmDialog = new ConfirmDialogForm(Const.ConfirmTitle, $"Are you sure you want to update Tax 7.75% -> 10.5% for all ticket from 10/23/2025 to 10/24/2025?");
                var result = confirmFirmDialog.ShowDialog();
                if (result == DialogResult.No)
                    return;

                ProcessForm processForm = new ProcessForm();
                processForm.CurrentProcessName = ProcessNameEnum.HotFix_UpgradeTaxForTicket;
                processForm.ShowDialog();

                if (processForm.IsSuccess)
                {
                    InformationDialogForm informationDialogForm = new InformationDialogForm($"Update Tax 7.75% -> 10.5% for all ticket from 10/23/2025 to 10/24/2025 Done.");
                    informationDialogForm.ShowDialog();
                }
                else
                {
                    ErrorDialogForm errorDialogForm = new ErrorDialogForm($"Update Tax 7.75% -> 10.5% for all ticket from 10/23/2025 to 10/24/2025 Fail. Please contact TruongDS");
                    errorDialogForm.ShowDialog();
                }

            }
            catch (System.Exception ex)
            {
                // log exception
                BLogicLogger.Exception("BtnUpdownTaxPercentForTickets_Click in HotFixView", ex);
            }
        }

        private void BtnUpdataTax775Percent_Click(object sender, System.EventArgs e)
        {
            try
            {
                ConfirmDialogForm confirmFirmDialog = new ConfirmDialogForm(Const.ConfirmTitle, $"Are you sure you want to update Tax 7.75% for all ticket from 10/23/2025 to 10/24/2025?");
                var result = confirmFirmDialog.ShowDialog();
                if (result == DialogResult.No)
                    return;

                ProcessForm processForm = new ProcessForm();
                processForm.CurrentProcessName = ProcessNameEnum.HotFix_UpgradeTaxForTicket;
                processForm.ShowDialog();

                if (processForm.IsSuccess)
                {
                    InformationDialogForm informationDialogForm = new InformationDialogForm($"Update Tax 7.75% for all ticket from 10/23/2025 to 10/24/2025 Done.");
                    informationDialogForm.ShowDialog();
                }
                else
                {
                    ErrorDialogForm errorDialogForm = new ErrorDialogForm($"Update Tax 7.75% for all ticket from 10/23/2025 to 10/24/2025 Fail. Please contact TruongDS");
                    errorDialogForm.ShowDialog();
                }

            }
            catch (System.Exception ex)
            {
                // log exception
                BLogicLogger.Exception("BtnUpdataTax775Percent_Click in HotFixView", ex);
            }
        }
    }
}
