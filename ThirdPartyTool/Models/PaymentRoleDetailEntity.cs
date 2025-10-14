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

                ID = Utility.GetGuidFromRow("ID", row);
                SaleReceiptID = Utility.GetGuidFromRow("SaleReceiptID", row);
                SaleReceiptPaymentDetailID = (long)Utility.GetIntFromRow("SaleReceiptPaymentDetailID", row);
                PaymentMethodID = (short)Utility.GetIntFromRow("PaymentMethodID", row);
                PaymentMethodType = (short)Utility.GetIntFromRow("PaymentMethodType", row);
                EmployeeID = (short)Utility.GetIntFromRow("EmployeeID", row);
                RolesID = (short)Utility.GetIntFromRow("RolesID", row);
                TipRate = Utility.GetDecimalFromRow("TipRate", row);
                TipRateOT = Utility.GetDecimalFromRow("TipRateOT", row);
                Gratuity = Utility.GetDecimalFromRow("Gratuity", row);
                SVCFeeForGratuity = Utility.GetDecimalFromRow("SVCFeeForGratuity", row);
                MerchantFeeForTip = Utility.GetDecimalFromRow("MerchantFeeForTip", row);
                DateCreated = Utility.GetDateTimeFromObject(row["DateCreated"]);
                PartyServiceFeeAmount = Utility.GetDecimalFromRow("PartyServiceFeeAmount", row);
                PartyServiceFeeTaxAmount = Utility.GetDecimalFromRow("PartyServiceFeeTaxAmount", row);
                SVCFeeForPartyServiceFeeAmount = Utility.GetDecimalFromRow("SVCFeeForPartyServiceFeeAmount", row);
                MerchantFeeForPartyServiceFeeAmount = Utility.GetDecimalFromRow("MerchantFeeForPartyServiceFeeAmount", row);
                AutoGratuityAmount = Utility.GetDecimalFromRow("AutoGratuityAmount", row);
                AutoGratuityTaxAmount = Utility.GetDecimalFromRow("AutoGratuityTaxAmount", row);
                SVCFeeForAutoGratuityAmount = Utility.GetDecimalFromRow("SVCFeeForAutoGratuityAmount", row);
                MerchantFeeForAutoGratuityAmount = Utility.GetDecimalFromRow("MerchantFeeForAutoGratuityAmount", row);
                AreaFeeAmount = Utility.GetDecimalFromRow("AreaFeeAmount", row);
                AreaFeeTaxAmount = Utility.GetDecimalFromRow("AreaFeeTaxAmount", row);
                SVCFeeForAreaFeeAmount = Utility.GetDecimalFromRow("SVCFeeForAreaFeeAmount", row);
                MerchantFeeForAreaFeeAmount = Utility.GetDecimalFromRow("MerchantFeeForAreaFeeAmount", row);
                ServiceFee = Utility.GetDecimalFromRow("ServiceFee", row);
                ServiceFeeTaxAmount = Utility.GetDecimalFromRow("ServiceFeeTaxAmount", row);
                SVCFeeForServiceFee = Utility.GetDecimalFromRow("SVCFeeForServiceFee", row);
                MerchantFeeForServiceFee = Utility.GetDecimalFromRow("MerchantFeeForServiceFee", row);
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
