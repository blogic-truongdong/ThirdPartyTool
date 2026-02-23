

using BLogicSystems.XPos.ClientUtilities;
using BLogicSystems.XPos.Utilites;
using System;
using System.Data;
using System.Windows.Forms;

namespace ThirdPartyTool.Shift
{
    public partial class ShiftView : UserControl
    {
        DataTable dtShifts = null;
        public ShiftView()
        {
            InitializeComponent();

            btnView.Click += BtnView_Click;
            dgvShifts.CellClick += DgvShifts_CellClick;
            this.Load += ShiftView_Load;
        }

        private void ShiftView_Load(object sender, EventArgs e)
        {
            BtnView_Click(null, null);
        }

        private void DgvShifts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0)
                    return;
                string shiftID = Utility.GetStringFromObject(dgvShifts.Rows[e.RowIndex].Cells["ShiftID"].Value);
                decimal openingAmount = Utility.GetDecimalFromObject(dgvShifts.Rows[e.RowIndex].Cells["OpeningAmount"].Value);
                decimal closingAmount = Utility.GetDecimalFromObject(dgvShifts.Rows[e.RowIndex].Cells["ClosingAmount"].Value);
                UpdateShiftForm updateShift = new UpdateShiftForm();
                updateShift.txtOpeningAmount.Value = openingAmount;
                updateShift.txtClosingAmount.Value = closingAmount;
                if (BLogicMessageBox.ShowPopUpFormWithDIMBackground(updateShift, Helper.MainForm) == DialogResult.OK)
                {
                    ProcessForm processForm = new ProcessForm();
                    processForm.CurrentProcessName = ProcessNameEnum.UpdateShift;
                    processForm.requestModel.ShiftID = shiftID;
                    processForm.requestModel.OpeningAmount = updateShift.txtOpeningAmount.Value;
                    processForm.requestModel.ClosingAmount = updateShift.txtClosingAmount.Value;
                    BLogicMessageBox.ShowPopUpFormWithDIMBackground(processForm);

                    if (processForm.IsSuccess)
                    {
                        DataRow[] drs = dtShifts.Select($"ShiftID = '{shiftID}'", "");
                        if (drs.Length > 0)
                        {
                            drs[0]["OpeningAmount"] = updateShift.txtOpeningAmount.Value;
                            drs[0]["ClosingAmount"] = updateShift.txtClosingAmount.Value;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                BLogicLogger.Exception("DgvShifts_CellClick", ex);
            }
        }

        private void BtnView_Click(object sender, System.EventArgs e)
        {
            try
            {

                ProcessForm processForm = new ProcessForm();
                processForm.CurrentProcessName = ProcessNameEnum.ViewShifts;
                processForm.requestModel.StartDate = startDate.StartDate;
                processForm.requestModel.EndDate = endDate.EndDate;

                BLogicMessageBox.ShowPopUpFormWithDIMBackground(processForm);

                dtShifts = processForm.DataTableReturn;

                dgvShifts.DataSource = dtShifts;
            }
            catch (System.Exception ex)
            {

            }
        }

        private void BtnSyncRoles_Click(object sender, System.EventArgs e)
        {

        }

        private void pnlShifts_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}
