using BLogicSystems.XPos.Utilites;
using System;
using System.Data;

namespace ThirdPartyTool.Models
{
    public class PaymentRoleDetailEntity
    {
        public Guid ID { get; set; }

        public Guid SaleReceiptID { get; set; }

        public long SaleReceiptPaymentDetailID { get; set; }

        public short PaymentMethodID { get; set; }

        public short PaymentMethodType { get; set; }

        public short EmployeeID { get; set; }

        public short RolesID { get; set; }

        public decimal TipRate { get; set; }

        public decimal TipRateOT { get; set; }

        public decimal Gratuity { get; set; }

        public decimal SVCFeeForGratuity { get; set; }

        public decimal MerchantFeeForTip { get; set; }

        public DateTime DateCreated { get; set; }

        public decimal PartyServiceFeeAmount { get; set; }

        public decimal PartyServiceFeeTaxAmount { get; set; }

        public decimal SVCFeeForPartyServiceFeeAmount { get; set; }

        public decimal MerchantFeeForPartyServiceFeeAmount { get; set; }

        public decimal AutoGratuityAmount { get; set; }

        public decimal AutoGratuityTaxAmount { get; set; }

        public decimal SVCFeeForAutoGratuityAmount { get; set; }

        public decimal MerchantFeeForAutoGratuityAmount { get; set; }

        public decimal AreaFeeAmount { get; set; }

        public decimal AreaFeeTaxAmount { get; set; }

        public decimal SVCFeeForAreaFeeAmount { get; set; }

        public decimal MerchantFeeForAreaFeeAmount { get; set; }

        public decimal ServiceFee { get; set; }

        public decimal ServiceFeeTaxAmount { get; set; }

        public decimal SVCFeeForServiceFee { get; set; }

        public decimal MerchantFeeForServiceFee { get; set; }

        /// <summary>
        /// Updates the properties of this instance from a DataRow
        /// </summary>
        /// <param name="row">The DataRow containing the data</param>
        public void UpdateFromDataRow(DataRow row)
        {
            try
            {
                if (row == null) return;

                ID = Utility.GetGuidFromObject(row["ID"]);
                SaleReceiptID = Utility.GetGuidFromObject(row["SaleReceiptID"]);
                SaleReceiptPaymentDetailID = (long)Utility.GetIntFromObject(row["SaleReceiptPaymentDetailID"]);
                PaymentMethodID = (short)Utility.GetIntFromObject(row["PaymentMethodID"]);
                PaymentMethodType = (short)Utility.GetIntFromObject(row["PaymentMethodType"]);
                EmployeeID = (short)Utility.GetIntFromObject(row["EmployeeID"]);
                RolesID = (short)Utility.GetIntFromObject(row["RolesID"]);
                TipRate = Utility.GetDecimalFromObject(row["TipRate"]);
                TipRateOT = Utility.GetDecimalFromObject(row["TipRateOT"]);
                Gratuity = Utility.GetDecimalFromObject(row["Gratuity"]);
                SVCFeeForGratuity = Utility.GetDecimalFromObject(row["SVCFeeForGratuity"]);
                MerchantFeeForTip = Utility.GetDecimalFromObject(row["MerchantFeeForTip"]);
                DateCreated = Utility.GetDateTimeFromObject(row["DateCreated"]);
                PartyServiceFeeAmount = Utility.GetDecimalFromObject(row["PartyServiceFeeAmount"]);
                PartyServiceFeeTaxAmount = Utility.GetDecimalFromObject(row["PartyServiceFeeTaxAmount"]);
                SVCFeeForPartyServiceFeeAmount = Utility.GetDecimalFromObject(row["SVCFeeForPartyServiceFeeAmount"]);
                MerchantFeeForPartyServiceFeeAmount = Utility.GetDecimalFromObject(row["MerchantFeeForPartyServiceFeeAmount"]);
                AutoGratuityAmount = Utility.GetDecimalFromObject(row["AutoGratuityAmount"]);
                AutoGratuityTaxAmount = Utility.GetDecimalFromObject(row["AutoGratuityTaxAmount"]);
                SVCFeeForAutoGratuityAmount = Utility.GetDecimalFromObject(row["SVCFeeForAutoGratuityAmount"]);
                MerchantFeeForAutoGratuityAmount = Utility.GetDecimalFromObject(row["MerchantFeeForAutoGratuityAmount"]);
                AreaFeeAmount = Utility.GetDecimalFromObject(row["AreaFeeAmount"]);
                AreaFeeTaxAmount = Utility.GetDecimalFromObject(row["AreaFeeTaxAmount"]);
                SVCFeeForAreaFeeAmount = Utility.GetDecimalFromObject(row["SVCFeeForAreaFeeAmount"]);
                MerchantFeeForAreaFeeAmount = Utility.GetDecimalFromObject(row["MerchantFeeForAreaFeeAmount"]);
                ServiceFee = Utility.GetDecimalFromObject(row["ServiceFee"]);
                ServiceFeeTaxAmount = Utility.GetDecimalFromObject(row["ServiceFeeTaxAmount"]);
                SVCFeeForServiceFee = Utility.GetDecimalFromObject(row["SVCFeeForServiceFee"]);
                MerchantFeeForServiceFee = Utility.GetDecimalFromObject(row["MerchantFeeForServiceFee"]);
            }
            catch (Exception ex)
            {
                BLogicLogger.Error("Error updating PaymentRoleDetailEntity from DataRow", ex);
            }
        }

        /// <summary>
        /// Creates a new instance of PaymentRoleDetailEntity from a DataRow
        /// </summary>
        /// <param name="row">The DataRow containing the data</param>
        /// <returns>A new PaymentRoleDetailEntity instance</returns>
        public static PaymentRoleDetailEntity FromDataRow(DataRow row)
        {
            var entity = new PaymentRoleDetailEntity();
            entity.UpdateFromDataRow(row);
            return entity;
        }
    }
}
