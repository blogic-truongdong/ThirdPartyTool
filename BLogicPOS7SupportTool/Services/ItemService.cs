using BLogicPOS7SupportTool.Core;
using BLogicPOS7SupportTool.Models;
using System;
using System.Collections.Generic;
using System.Data;

namespace BLogicPOS7SupportTool.Services
{
    class ItemService
    {
        public List<DepartmentEntity> GetDepartments()
        {
            const string sql = @"
                SELECT DepartmentID, DepartmentName
                FROM dbo.Departments
                WHERE Level = '1' AND IsDeleted = '0' AND DepartmentID > 0 AND DepartmentOrder > 0
                ORDER BY DepartmentOrder";

            var result = new List<DepartmentEntity>();
            var dt = AppSession.ProcessQuery(true, sql, "Departments");

            foreach (DataRow row in dt.Rows)
                result.Add(new DepartmentEntity
                {
                    DepartmentID = Convert.ToInt32(row["DepartmentID"]),
                    DepartmentName = row["DepartmentName"]?.ToString() ?? ""
                });

            AppLogger.Debug($"GetDepartments: {result.Count} rows");
            return result;
        }

        public DataTable GetItems(int departmentId, string search)
        {
            string where = string.IsNullOrWhiteSpace(search)
                ? ""
                : $" AND a.ItemName LIKE '%{search.Replace("'", "''")}%'";

            string sql = $@"
                SELECT a.ItemID, a.ItemName, a.LookupCode, a.RegularPrice,
                       a.ItemGroupID, b.ItemGroupName
                FROM dbo.Items a
                LEFT JOIN dbo.ItemGroups b ON a.ItemGroupID = b.ItemGroupID
                WHERE a.IsDeleted = '0'
                  AND a.DeptDeptID = '{departmentId}'{where}
                ORDER BY a.ItemName";

            var dt = AppSession.ProcessQuery(true, sql, "Items");
            AppLogger.Debug($"GetItems(deptId={departmentId}, search='{search}'): {dt.Rows.Count} rows");
            return dt;
        }

        public void SyncItemGroup(string itemId, string itemGroupId, DateTime startDate, DateTime endDate)
        {
            string sql = $@"
                UPDATE srd
                SET srd.ItemGroupID = '{itemGroupId}'
                FROM dbo.SaleReceiptDetails srd
                INNER JOIN dbo.SaleReceipts sr ON srd.ReceiptID = sr.ReceiptID
                WHERE srd.ItemID = '{itemId}'
                  AND CAST(sr.BusinessDate AS DATE) >= '{startDate:yyyy-MM-dd}'
                  AND CAST(sr.BusinessDate AS DATE) <= '{endDate:yyyy-MM-dd}'";

            AppSession.ProcessQuery(true, sql, "SyncItemGroup");
            AppLogger.Info($"SyncItemGroup: ItemID={itemId}, ItemGroupID={itemGroupId}, {startDate:yyyy-MM-dd} → {endDate:yyyy-MM-dd}");
        }
    }
}
