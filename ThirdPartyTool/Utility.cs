using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThirdPartyTool
{
    public class Utility
    {
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
                BLogicLogger.Error("GetStringFromRow object Error : " + oe.ToString());
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

        public static decimal CalculateMerchantFeeWithPercent(decimal total, decimal percent)
        {
            return RoundUp(total * (percent / 100));
        }

        public static decimal CalculateSVCFeeWithPercent(decimal total, decimal percent)
        {
            return RoundUp(total * (percent / 100));
        }

        public static decimal RoundUp(decimal dInValue)
        {
            if (dInValue >= 0)
            {
                decimal dReturn = Math.Round(dInValue, 2);
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
