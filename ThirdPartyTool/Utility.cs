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

        public static decimal GetDecimalFromObject(object oValue)
        {
            return GetDecimalFromObjectWithDefault(oValue, 0);
        }
        public static decimal GetDecimalFromObjectWithDefault(object oValue, decimal iDefault)
        {
            decimal iReturn = iDefault;
            string sTemp = GetStringFromObject(oValue);
            if (string.IsNullOrEmpty(sTemp))
                return iReturn;
            if (!decimal.TryParse(sTemp, out iReturn))
                return iDefault;

            return iReturn;
        }

        public static string GetStringFromObject(object oValue)
        {
            string sReturn = string.Empty;
            try
            {
                if (oValue != null && oValue != DBNull.Value)
                    sReturn = oValue.ToString();
            }
            catch (Exception oe)
            {
                sReturn = string.Empty;
            }
            return sReturn;
        }

        public static int GetIntFromObject(object oValue)
        {
            return GetIntFromObjectWithDefault(oValue, 0);
        }
        public static int GetIntFromObjectWithDefault(object oValue, int iDefault)
        {
            int iReturn = iDefault;
            string sTemp = GetStringFromObject(oValue);
            if (string.IsNullOrEmpty(sTemp))
                return iReturn;
            if (!int.TryParse(sTemp, out iReturn))
                return iDefault;

            return iReturn;
        }

        public static decimal CalculateSVCFeeAdjustTipWithPara(decimal total, decimal svcFee, decimal merchantFee)
        {
            if (total <= 0 || svcFee == 0)
                return 0;
            return RoundUp(total * (merchantFee / 100));

        }

        public static decimal CalculateMerchantFeeForTipWithParams(decimal total, decimal svcFee, decimal merchantFee, bool SVCFeeType)
        {
            if (total <= 0 || !SVCFeeType || merchantFee == 0)
                return 0;
            return RoundUp(total * (merchantFee / 100));
        }

        public static decimal CalculateMerchantFeeWithParams(decimal total, decimal svcFee, decimal merchantFee, bool SVCFeeType)
        {
            if (total <= 0 || (svcFee == 0 && merchantFee == 0))
                return 0;
            if (!SVCFeeType)
                return merchantFee;
            return RoundUp(total * (merchantFee / 100));
        }

        public static decimal RoundUp(decimal dInValue, int intPlaces = 2)
        {
            if (dInValue >= 0)
            {
                decimal dReturn = Math.Round(dInValue, intPlaces);
                if (dReturn >= dInValue)
                    return dReturn;
                //dReturn < dInValue
                decimal dSub = dInValue - dReturn;
                if (dSub >= (decimal)0.005)
                    dReturn += (decimal)0.01;
                return dReturn;
            }
            else
            {
                dInValue = dInValue * -1;
                decimal dReturn = Math.Round(dInValue, 2);
                if (dReturn >= dInValue)
                {
                    dReturn = dReturn * -1;
                    return dReturn;
                }
                //dReturn < dInValue
                decimal dSub = dInValue - dReturn;
                if (dSub >= (decimal)0.005)
                    dReturn += (decimal)0.01;

                dReturn = dReturn * -1;
                return dReturn;
            }
        }
    }
}
