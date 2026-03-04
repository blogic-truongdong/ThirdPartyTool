using System;
using System.Data;
using ThirdPartyTool.Models;

namespace ThirdPartyTool
{
    public class HouseAccount
    {
        public static DataTable CreateHouseAccountTransactionsTable()
        {
            DataTable dt = new DataTable("HouseAccountTransactions");
            dt.Columns.Add("ID", typeof(Guid));
            dt.Columns.Add("Number", typeof(string));
            dt.Columns.Add("DateCreated", typeof(DateTime));
            dt.Columns.Add("TransactionType", typeof(string));
            dt.Columns.Add("TenderType", typeof(string));
            dt.Columns.Add("CardNumber", typeof(string));
            dt.Columns.Add("PurchaseAmount", typeof(decimal));
            dt.Columns.Add("OutstandingBalanceAmount", typeof(decimal));
            dt.Columns.Add("BalanceAmount", typeof(decimal));
            dt.Columns.Add("EntryMode", typeof(string));
            dt.Columns.Add("AppName", typeof(string));
            dt.Columns.Add("StoreID", typeof(string));
            dt.Columns.Add("StoreIDProcess", typeof(string));
            dt.Columns.Add("StoreNameProcess", typeof(string));
            dt.Columns.Add("TicketNumber", typeof(string));
            dt.Columns.Add("EmployeeID", typeof(string));
            dt.Columns.Add("EmployeeName", typeof(string));
            dt.Columns.Add("WorkstationID", typeof(string));
            dt.Columns.Add("WorkstationName", typeof(string));

            return dt;
        }

        public static SaleReceiptPaymentDetailEntity CreatePaymentDetail(DataRow rPaymentRow, SaleReceiptEntity oReceipt)
        {
            SaleReceiptPaymentDetailEntity oPaymentEntity = new SaleReceiptPaymentDetailEntity();
            return oPaymentEntity;

        }
    }
}
