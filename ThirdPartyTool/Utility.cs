using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ThirdPartyTool.Models;
using static System.Collections.Specialized.BitVector32;

namespace ThirdPartyTool
{
    public class Utility
    {
        public static string BLOGICPOS7_Connection = "";
        public static string Merchant_Connection = "";

        public static async Task<EmployeeClockInInfoEntity> GetEmployeeClockInInfo(SqlConnection conn, object employeeID, DateTime startDate)
        {
            EmployeeClockInInfoEntity employeeClockInInfoEntity = new EmployeeClockInInfoEntity();
            try
            {
                string sql = $"select TipRate, TipRateOT, RolesID from TimeClock where EmployeeID = '{employeeID}' and (ClockStatus = 1 OR (DateTimeIn <= '{startDate}' and '{startDate}' <= DateTimeOut )) order by DateTimeIn desc";
                TimeClockEntity timeClockEntity = await conn.QueryFirstOrDefaultAsync<TimeClockEntity>(sql, null, null, 180, System.Data.CommandType.Text);

                if (timeClockEntity != null)
                {
                    employeeClockInInfoEntity.RolesID = timeClockEntity.RolesID;
                    employeeClockInInfoEntity.TipRate = timeClockEntity.TipRate;
                    employeeClockInInfoEntity.TipRateOT = timeClockEntity.TipRateOT;
                }
                else
                {
                    sql = $"select IsUsingRoleTipPool,EnableTrainingMode,TipRate,TipRateOT,TrainingTipRate,TrainingTipRateOT from Employees where EmployeeID = '{employeeID}'";
                    EmployeeEntity employeeEntity = await conn.QueryFirstOrDefaultAsync<EmployeeEntity>(sql, null, null, 180, System.Data.CommandType.Text);

                    sql = $"select top 1 r.RolesID,TipRate,TipRateOT,TrainingTipRate,TrainingTipRateOT  from EmployeeRoles er left join Roles r on er.RolesID = r.RolesID where er.EmployeeID = '{employeeID}' order by TipRate";
                    var roleEntities = await conn.QueryAsync<RoleEntity>(sql, null, null, 180, System.Data.CommandType.Text);
                    RoleEntity roleEntity = null;
                    if (employeeEntity != null)
                    {
                        if (roleEntities?.Count() > 0)
                        {
                            roleEntity = roleEntities.ToList()[0];
                            employeeClockInInfoEntity.RolesID = roleEntity.RolesID;
                        }

                        if (employeeEntity.IsUsingRoleTipPool && roleEntity != null)
                        {
                            if (employeeEntity.EnableTrainingMode)
                            {
                                employeeClockInInfoEntity.TrainingTipRate = roleEntity.TrainingTipRate;
                                employeeClockInInfoEntity.TrainingTipRateOT = roleEntity.TrainingTipRateOT;
                            }
                            else
                            {
                                employeeClockInInfoEntity.TipRate = roleEntity.TipRate;
                                employeeClockInInfoEntity.TipRateOT = roleEntity.TipRateOT;
                            }
                        }
                        else
                        {
                            if (employeeEntity.EnableTrainingMode)
                            {

                                employeeClockInInfoEntity.TrainingTipRate = employeeEntity.TrainingTipRate;
                                employeeClockInInfoEntity.TrainingTipRateOT = employeeEntity.TrainingTipRateOT;
                            }
                            else
                            {
                                employeeClockInInfoEntity.TipRate = employeeEntity.TipRate;
                                employeeClockInInfoEntity.TipRateOT = employeeEntity.TipRateOT;
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("GetEmployeeClockInInfo Exception");
            }

            return employeeClockInInfoEntity;
        }
    }
}
