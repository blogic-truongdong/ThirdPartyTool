using BLogicSystems.XPos.ClientUtilities;
using BLogicSystems.XPos.Utilites;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using ThirdPartyTool.Dialog;

namespace ThirdPartyTool.AdvancedFunctions
{
    public partial class ClearTableStatusForm : Form
    {
        private DataTable dtSittingAreas = new DataTable("SittingAreas");
        private DataTable dtTables = new DataTable("Tables");

        public ClearTableStatusForm()
        {
            InitializeComponent();

            this.Load += ClearTableStatusForm_Load;

            btnCancel.Click += BtnCancel_Click;

            dgvTables.CellFormatting += DgvTables_CellFormatting;
            dgvTables.CellClick += DgvTables_CellClick;
        }

        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            UpdateFilter();
        }

        private void DgvTables_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // One audit row for everything this action changes.
            using (AuditLog.Group("Advanced: clear table status", singleTicket: false))
                DgvTables_CellClickCore(sender, e);
        }

        private void DgvTables_CellClickCore(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0)
                    return;

                var tableID = dgvTables.Rows[e.RowIndex].Cells["TableID"].Value;
                var tableName = dgvTables.Rows[e.RowIndex].Cells["TableName"].Value;

                if (tableID != null && !string.IsNullOrWhiteSpace(tableID.ToString()))
                {
                    ConfirmDialogForm confirmFirmDialog = new ConfirmDialogForm(Const.ConfirmTitle, $"Are you sure you want to Clear {tableName}?");
                    var result = BLogicMessageBox.ShowPopUpFormWithDIMBackground(confirmFirmDialog, this);
                    if (result == DialogResult.No)
                        return;

                    DataTable dtTemp = new DataTable("Tables");
                    DataTable dtNewTables = new DataTable("Tables");

                    using (SqlCommand sqlCommand = new SqlCommand())
                    using (var conn = new SqlConnection(Helper.BLOGICPOS7_Connection))
                    {
                        if (conn.State == ConnectionState.Closed)
                            conn.Open();

                        sqlCommand.Connection = conn;
                        sqlCommand.CommandType = CommandType.Text;
                        sqlCommand.CommandText = $"select * from Tables where TableID = '{tableID}'";

                        var reader = sqlCommand.ExecuteReader();
                        dtTemp.Load(reader);
                        reader.Close();

                        var script = GenerateScriptInsertNewTableBaseOldTable(dtTemp);

                        // New table row + old row marked deleted: both or neither (committed before any dialog shows).
                        SqlSafety.PrepareTransactionalSession(conn);
                        using (SqlTransaction transaction = conn.BeginTransaction())
                        {
                            sqlCommand.Transaction = transaction;
                            var resultInsert = Helper.RunQueryUpdate(sqlCommand, script);
                            if (resultInsert > 0)
                                Helper.RunQueryUpdate(sqlCommand, $"Update Tables set isDeleted = '1' where TableID = '{tableID}'");
                            transaction.Commit();
                            sqlCommand.Transaction = null;
                        }

                        sqlCommand.CommandText = $"select TableID,AreaID,TableName from Tables where isDeleted = '0' order by TableName";
                        reader = sqlCommand.ExecuteReader();
                        dtNewTables.Load(reader);
                        reader.Close();
                    }

                    InformationDialogForm informationDialog = new InformationDialogForm("Clear Table Success!");
                    BLogicMessageBox.ShowPopUpFormWithDIMBackground(informationDialog, this);

                    dtTables = dtNewTables;
                    dtTables.AcceptChanges();
                    dgvTables.DataSource = dtNewTables;

                    UpdateFilter();
                }
            }
            catch (Exception ex)
            {
                BLogicLogger.Exception("CbbArea_SelectedIndexChanged in ClearTableStatusForm", ex);
            }
        }

        private string GenerateScriptInsertNewTableBaseOldTable(DataTable dtOldTable)
        {
            string result = string.Empty;
            try
            {

                var insertInto = "INSERT INTO Tables(";
                var values = " VALUES(";

                int index = 1;
                foreach (DataColumn item in dtOldTable.Columns)
                {
                    if (index == dtOldTable.Columns.Count)
                        insertInto += "[" + item.ColumnName + "]";
                    else
                        insertInto += "[" + item.ColumnName + "],";

                    var val = dtOldTable.Rows[0][item.ColumnName];

                    switch (item.DataType.Name)
                    {
                        case "Boolean":
                            val = Utility.GetBoolFromObject(val) ? "'1'" : "'0'";
                            break;
                        default:
                            if (val == DBNull.Value)
                                val += "null";
                            else
                                val = $"'{val}'";
                            break;
                    }

                    if (item.ColumnName == "TableID")
                        val = "NEWID()";
                    if (item.ColumnName == "Status")
                        val = "'0'";

                    if (index == dtOldTable.Columns.Count)
                        values += val;
                    else
                        values += val + ",";

                    index++;

                }
                insertInto += ") ";
                values += ") ";

                result = insertInto + values;
            }
            catch (Exception ex)
            {
                BLogicLogger.Exception("GenerateScriptInsertNewTableBaseOldTable in ClearTableStatusForm", ex);
            }
            return result;
        }

        private void DgvTables_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvTables.Columns[e.ColumnIndex].Name == "ClearTable")
            {
                e.CellStyle.ForeColor = Color.Red;
                e.CellStyle.SelectionForeColor = Color.Red;
                e.Value = "Clear";
                e.FormattingApplied = true;
            }
        }

        private void CbbArea_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtSearch.TextChanged -= TxtSearch_TextChanged;
            txtSearch.Text = "";
            txtSearch.TextChanged += TxtSearch_TextChanged;

            UpdateFilter();
        }

        private void UpdateFilter()
        {
            try
            {
                if (cbbArea.SelectedIndex < 0)
                    return;

                var areaID = cbbArea.SelectedValue;

                if (!string.IsNullOrWhiteSpace(txtSearch.Text))
                    dtTables.DefaultView.RowFilter = $"AreaID = '{areaID}' and TableName like '%{txtSearch.Text}%'";
                else
                    dtTables.DefaultView.RowFilter = $"AreaID = '{areaID}'";

            }
            catch (Exception ex)
            {
                BLogicLogger.Exception("CbbArea_SelectedIndexChanged in ClearTableStatusForm", ex);
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ClearTableStatusForm_Load(object sender, EventArgs e)
        {
            try
            {
                using (SqlCommand sqlCommand = new SqlCommand())
                using (var conn = new SqlConnection(Helper.BLOGICPOS7_Connection))
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();

                    sqlCommand.Connection = conn;
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.CommandText = $"select * from SittingAreas where isEnable = '1' order by AreaName";
                    var reader = sqlCommand.ExecuteReader();
                    dtSittingAreas.Load(reader);
                    reader.Close();

                    sqlCommand.CommandText = $"select TableID,AreaID,TableName from Tables where isDeleted = '0' order by TableName";
                    reader = sqlCommand.ExecuteReader();
                    dtTables.Load(reader);
                    reader.Close();
                }

                cbbArea.DataSource = dtSittingAreas;
                cbbArea.DisplayMember = "AreaName";
                cbbArea.ValueMember = "AreaID";

                dgvTables.DataSource = dtTables;

                cbbArea.SelectedIndexChanged -= CbbArea_SelectedIndexChanged;
                cbbArea.SelectedIndexChanged += CbbArea_SelectedIndexChanged;

                txtSearch.TextChanged -= TxtSearch_TextChanged;
                txtSearch.TextChanged += TxtSearch_TextChanged;
            }
            catch (Exception ex)
            {
                BLogicLogger.Exception("ClearTableStatusForm_Load", ex);
            }
        }
    }
}
