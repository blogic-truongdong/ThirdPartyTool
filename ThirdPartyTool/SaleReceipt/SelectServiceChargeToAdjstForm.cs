using BLogicSystems.XPos.ClientUtilities;
using BLogicSystems.XPos.Utilites;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using ThirdPartyTool.Models;

namespace ThirdPartyTool.SaleReceipt
{
    public partial class SelectServiceChargeToAdjstForm : Form
    {
        private ServiceChargeType EditServiceChargeType;
        private decimal Amount = 0;
        private decimal Tax = 0;
        public bool IsChangedServiceCharges = false;
        public string SaleReceiptID;

        private BackgroundWorker backgroundWorker;

        public SelectServiceChargeToAdjstForm()
        {
            InitializeComponent();

            backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += BackgroundWorker_DoWork;
            backgroundWorker.RunWorkerCompleted += BackgroundWorker_RunWorkerCompleted;

            pnlServiceCharge.Click += PnlServiceCharge_Click;
            titleServiceCharge.Click += PnlServiceCharge_Click;
            detailServiceCharge.Click += PnlServiceCharge_Click;
            amountServiceChargeLabel.Click += PnlServiceCharge_Click;
            amountServiceChargeValue.Click += PnlServiceCharge_Click;
            taxServiceChargeLabel.Click += PnlServiceCharge_Click;
            taxServiceChargeValue.Click += PnlServiceCharge_Click;

            pnlPartyGratuity.Click += PnlPartyGratuity_Click;
            titlePartyGratuity.Click += PnlPartyGratuity_Click;
            detailPartyGratuity.Click += PnlPartyGratuity_Click;
            amountPartyGratuityLabel.Click += PnlPartyGratuity_Click;
            amountPartyGratuityValue.Click += PnlPartyGratuity_Click;
            taxPartyGratuityLabel.Click += PnlPartyGratuity_Click;
            taxPartyGratuityValue.Click += PnlPartyGratuity_Click;

            pnlAutoServiceCharge.Click += PnlAutoServiceCharge_Click;
            titleAutoServiceCharge.Click += PnlAutoServiceCharge_Click;
            detailAutoServiceCharge.Click += PnlAutoServiceCharge_Click;
            amountAutoServiceChargeLabel.Click += PnlAutoServiceCharge_Click;
            amountAutoServiceChargeValue.Click += PnlAutoServiceCharge_Click;
            taxAutoServiceChargeLabel.Click += PnlAutoServiceCharge_Click;
            taxAutoServiceChargeValue.Click += PnlAutoServiceCharge_Click;

            pnlAreaGratuity.Click += PnlAreaGratuity_Click;
            titleAreaGratuity.Click += PnlAreaGratuity_Click;
            detailAreaGratuity.Click += PnlAreaGratuity_Click;
            amountAreaGratuityLabel.Click += PnlAreaGratuity_Click;
            amountAreaGratuityValue.Click += PnlAreaGratuity_Click;
            taxAreaGratuityLabel.Click += PnlAreaGratuity_Click;
            taxAreaGratuityValue.Click += PnlAreaGratuity_Click;

            btnClose.Click += BtnClose_Click;

            this.Load += SelectServiceChargeToAdjstForm_Load;

            InitCardStyle();
        }

        /// <summary>Hint under the title, and a blue border on the card under the mouse so it reads as clickable.</summary>
        private void InitCardStyle()
        {
            Label hint = new Label
            {
                AutoSize = true,
                BackColor = System.Drawing.Color.White,
                Font = new System.Drawing.Font("Roboto", 9.75F),
                ForeColor = System.Drawing.Color.FromArgb(117, 117, 117),
                Location = new System.Drawing.Point(20, 42),
                Text = "Click a charge to change its amount.",
            };
            pnlContainer.Controls.Add(hint);
            hint.BringToFront();
            btnClose.BringToFront();

            foreach (CustomControl.BPanel_Rounded card in new[] { pnlServiceCharge, pnlPartyGratuity, pnlAutoServiceCharge, pnlAreaGratuity })
            {
                CustomControl.BPanel_Rounded target = card;
                EventHandler enter = (s, e) => SetCardHover(target, true);
                EventHandler leave = (s, e) =>
                {
                    // Leaving into a child label still counts as inside the card.
                    if (!target.ClientRectangle.Contains(target.PointToClient(Cursor.Position)))
                        SetCardHover(target, false);
                };
                target.MouseEnter += enter;
                target.MouseLeave += leave;
                foreach (Control child in target.Controls)
                {
                    child.Cursor = Cursors.Hand;
                    child.MouseEnter += enter;
                    child.MouseLeave += leave;
                }
            }
        }

        private static void SetCardHover(CustomControl.BPanel_Rounded card, bool hover)
        {
            card.BorderColor = hover ? System.Drawing.Color.FromArgb(42, 108, 228) : System.Drawing.Color.FromArgb(224, 224, 224);
            card.Invalidate();
        }

        private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }

        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                string sql = $@"select * from SaleReceipts where SaleReceiptID = '{SaleReceiptID}'";

                var saleReceiptEntity = Helper.ProcessQueryEntity<SaleReceiptEntity>(true, sql).First();

                sql = $@"select * from SaleReceiptPaymentDetails where SaleReceiptID = '{SaleReceiptID}'";
                var saleReceiptPaymentDetailEntity = Helper.ProcessQueryEntity<SaleReceiptPaymentDetailEntity>(true, sql).First();

                decimal new_ServiceChargeAmount = 0;
                decimal new_ServiceChargeTaxAmount = 0;

                decimal balance_ServiceChargeAmount = 0;
                decimal balance_ServiceChargeTaxAmount = 0;

                decimal new_TotalAmount_In_SaleReceipts = 0;
                decimal new_SalesOrderFee_In_SaleReceipts = 0;
                decimal new_ServiceChargeTaxAmount_In_SaleReceipts = 0;
                decimal new_TaxAmount_In_SaleReceipts = 0;

                decimal new_AmountTendered_In_SaleReceiptPaymentDetails = 0;
                decimal new_ServiceChargeAmount_InSaleReceiptPaymentDetails = 0;
                decimal new_ServiceChargeTaxAmount_InSaleReceiptPaymentDetails = 0;
                decimal new_PaidServiceFee_InSaleReceiptPaymentDetails = 0;

                new_ServiceChargeAmount = Amount;
                new_ServiceChargeTaxAmount = Tax;

                switch (EditServiceChargeType)
                {
                    case ServiceChargeType.AreaServiceFee:

                        balance_ServiceChargeAmount = saleReceiptEntity.AreaFeeAmount - new_ServiceChargeAmount;
                        balance_ServiceChargeTaxAmount = saleReceiptEntity.AreaFeeTaxAmount - new_ServiceChargeTaxAmount;

                        new_TotalAmount_In_SaleReceipts = saleReceiptEntity.TotalAmount - balance_ServiceChargeAmount - balance_ServiceChargeTaxAmount;
                        new_SalesOrderFee_In_SaleReceipts = saleReceiptEntity.SalesOrderFee - balance_ServiceChargeAmount;
                        new_ServiceChargeTaxAmount_In_SaleReceipts = saleReceiptEntity.ServiceChargeTaxAmount - balance_ServiceChargeTaxAmount;
                        new_TaxAmount_In_SaleReceipts = saleReceiptEntity.TaxAmount - balance_ServiceChargeTaxAmount;

                        new_PaidServiceFee_InSaleReceiptPaymentDetails = saleReceiptPaymentDetailEntity.PaidServiceFee - balance_ServiceChargeAmount - balance_ServiceChargeTaxAmount;
                        new_ServiceChargeAmount_InSaleReceiptPaymentDetails = saleReceiptPaymentDetailEntity.AreaFeeAmount - balance_ServiceChargeAmount - balance_ServiceChargeTaxAmount;
                        new_ServiceChargeTaxAmount_InSaleReceiptPaymentDetails = saleReceiptPaymentDetailEntity.AreaFeeTaxAmount - balance_ServiceChargeTaxAmount;
                        new_AmountTendered_In_SaleReceiptPaymentDetails = saleReceiptPaymentDetailEntity.AmountTendered - balance_ServiceChargeAmount - balance_ServiceChargeTaxAmount;

                        break;
                    case ServiceChargeType.AutoServiceFee:

                        balance_ServiceChargeAmount = saleReceiptEntity.AutoGratuityAmount - new_ServiceChargeAmount;
                        balance_ServiceChargeTaxAmount = saleReceiptEntity.AutoGratuityTaxAmount - new_ServiceChargeTaxAmount;

                        new_TotalAmount_In_SaleReceipts = saleReceiptEntity.TotalAmount - balance_ServiceChargeAmount - balance_ServiceChargeTaxAmount;
                        new_SalesOrderFee_In_SaleReceipts = saleReceiptEntity.SalesOrderFee - balance_ServiceChargeAmount;
                        new_ServiceChargeTaxAmount_In_SaleReceipts = saleReceiptEntity.ServiceChargeTaxAmount - balance_ServiceChargeTaxAmount;
                        new_TaxAmount_In_SaleReceipts = saleReceiptEntity.TaxAmount - balance_ServiceChargeTaxAmount;

                        new_PaidServiceFee_InSaleReceiptPaymentDetails = saleReceiptPaymentDetailEntity.PaidServiceFee - balance_ServiceChargeAmount - balance_ServiceChargeTaxAmount;
                        new_ServiceChargeAmount_InSaleReceiptPaymentDetails = saleReceiptPaymentDetailEntity.AutoGratuityAmount - balance_ServiceChargeAmount - balance_ServiceChargeTaxAmount;
                        new_ServiceChargeTaxAmount_InSaleReceiptPaymentDetails = saleReceiptPaymentDetailEntity.AutoGratuityTaxAmount - balance_ServiceChargeTaxAmount;
                        new_AmountTendered_In_SaleReceiptPaymentDetails = saleReceiptPaymentDetailEntity.AmountTendered - balance_ServiceChargeAmount - balance_ServiceChargeTaxAmount;

                        break;
                    case ServiceChargeType.PartyServiceFee:

                        balance_ServiceChargeAmount = saleReceiptEntity.PartyServiceFeeAmount - new_ServiceChargeAmount;
                        balance_ServiceChargeTaxAmount = saleReceiptEntity.PartyServiceFeeTaxAmount - new_ServiceChargeTaxAmount;

                        new_TotalAmount_In_SaleReceipts = saleReceiptEntity.TotalAmount - balance_ServiceChargeAmount - balance_ServiceChargeTaxAmount;
                        new_SalesOrderFee_In_SaleReceipts = saleReceiptEntity.SalesOrderFee - balance_ServiceChargeAmount;
                        new_ServiceChargeTaxAmount_In_SaleReceipts = saleReceiptEntity.ServiceChargeTaxAmount - balance_ServiceChargeTaxAmount;
                        new_TaxAmount_In_SaleReceipts = saleReceiptEntity.TaxAmount - balance_ServiceChargeTaxAmount;

                        new_PaidServiceFee_InSaleReceiptPaymentDetails = saleReceiptPaymentDetailEntity.PaidServiceFee - balance_ServiceChargeAmount - balance_ServiceChargeTaxAmount;
                        new_ServiceChargeAmount_InSaleReceiptPaymentDetails = saleReceiptPaymentDetailEntity.PartyServiceFeeAmount - balance_ServiceChargeAmount - balance_ServiceChargeTaxAmount;
                        new_ServiceChargeTaxAmount_InSaleReceiptPaymentDetails = saleReceiptPaymentDetailEntity.PartyServiceFeeTaxAmount - balance_ServiceChargeTaxAmount;
                        new_AmountTendered_In_SaleReceiptPaymentDetails = saleReceiptPaymentDetailEntity.AmountTendered - balance_ServiceChargeAmount - balance_ServiceChargeTaxAmount;

                        break;
                    case ServiceChargeType.ManualServiceFee:

                        balance_ServiceChargeAmount = saleReceiptEntity.ServiceFee - new_ServiceChargeAmount;
                        balance_ServiceChargeTaxAmount = saleReceiptEntity.ServiceFeeTaxAmount - new_ServiceChargeTaxAmount;

                        new_TotalAmount_In_SaleReceipts = saleReceiptEntity.TotalAmount - balance_ServiceChargeAmount - balance_ServiceChargeTaxAmount;
                        new_SalesOrderFee_In_SaleReceipts = saleReceiptEntity.SalesOrderFee - balance_ServiceChargeAmount;
                        new_ServiceChargeTaxAmount_In_SaleReceipts = saleReceiptEntity.ServiceChargeTaxAmount - balance_ServiceChargeTaxAmount;
                        new_TaxAmount_In_SaleReceipts = saleReceiptEntity.TaxAmount - balance_ServiceChargeTaxAmount;

                        new_PaidServiceFee_InSaleReceiptPaymentDetails = saleReceiptPaymentDetailEntity.PaidServiceFee - balance_ServiceChargeAmount - balance_ServiceChargeTaxAmount;
                        new_ServiceChargeAmount_InSaleReceiptPaymentDetails = saleReceiptPaymentDetailEntity.ServiceFee - balance_ServiceChargeAmount - balance_ServiceChargeTaxAmount;
                        new_ServiceChargeTaxAmount_InSaleReceiptPaymentDetails = saleReceiptPaymentDetailEntity.ServiceFeeTaxAmount - balance_ServiceChargeTaxAmount;
                        new_AmountTendered_In_SaleReceiptPaymentDetails = saleReceiptPaymentDetailEntity.AmountTendered - balance_ServiceChargeAmount - balance_ServiceChargeTaxAmount;

                        break;

                    case ServiceChargeType.SVCFee:

                        //if (requestModel.serviceChargesValues.SVCFeePaidAmount == saleReceiptEntity.SVCFeePaidAmount &&
                        //    requestModel.serviceChargesValues.SVCFeeTaxPaidAmount == saleReceiptEntity.SVCFeeTaxPaidAmount)
                        //    return;

                        break;

                    case ServiceChargeType.CashDiscount:

                        //if (requestModel.serviceChargesValues.CashDiscountPaidAmount == saleReceiptEntity.CashDiscountPaidAmount &&
                        //    requestModel.serviceChargesValues.CashDiscountTaxPaidAmount == saleReceiptEntity.CashDiscountTaxPaidAmount)
                        //    return;

                        break;

                    default:
                        return;
                }

                switch (EditServiceChargeType)
                {
                    case ServiceChargeType.AreaServiceFee:
                        sql = $"update SaleReceipts set " +
                            $"AreaFeeAmount = '{new_ServiceChargeAmount}', " +
                            $"TotalAmount = '{new_TotalAmount_In_SaleReceipts}', " +
                            $"PaidAmount = '{new_TotalAmount_In_SaleReceipts}', " +
                            $"SalesOrderFee = '{new_SalesOrderFee_In_SaleReceipts}', " +
                            $"TaxAmount = '{new_TaxAmount_In_SaleReceipts}' " +
                            $"where SaleReceiptID = '{saleReceiptEntity.SaleReceiptID}';";

                        Helper.RunQueryUpdate(true, sql);

                        sql = $"update SaleReceiptPaymentDetails set " +
                            $"AmountTendered = '{new_AmountTendered_In_SaleReceiptPaymentDetails}', " +
                            $"PaidServiceFee = '{new_PaidServiceFee_InSaleReceiptPaymentDetails}', " +
                            $"AreaFeeAmount = '{new_ServiceChargeAmount_InSaleReceiptPaymentDetails}' " +
                            $"where SaleReceiptPaymentDetailID = '{saleReceiptPaymentDetailEntity.SaleReceiptPaymentDetailID}';";
                        Helper.RunQueryUpdate(true, sql);

                        sql = $"update SaleReceipts set AreaFeeTaxAmount = '{new_ServiceChargeTaxAmount}', ServiceChargeTaxAmount = '{new_ServiceChargeTaxAmount_In_SaleReceipts}' where SaleReceiptID = '{saleReceiptEntity.SaleReceiptID}';";
                        Helper.RunQueryUpdate(true, sql);

                        sql = $"update SaleReceiptPaymentDetails set AreaFeeTaxAmount = '{new_ServiceChargeTaxAmount_InSaleReceiptPaymentDetails}' where SaleReceiptPaymentDetailID = '{saleReceiptPaymentDetailEntity.SaleReceiptPaymentDetailID}';";
                        Helper.RunQueryUpdate(true, sql);

                        if (saleReceiptEntity.SVCFeeTaxPaidAmount != 0)
                        {
                            sql = $"update SaleReceiptPaymentDetails set TaxAmountByPaymentModeTaxCustomFee = '{new_TaxAmount_In_SaleReceipts}' where SaleReceiptPaymentDetailID = '{saleReceiptPaymentDetailEntity.SaleReceiptPaymentDetailID}';";
                            Helper.RunQueryUpdate(true, sql);
                        }

                        break;
                    case ServiceChargeType.AutoServiceFee:
                        sql = $"update SaleReceipts set " +
                            $"AutoGratuityAmount = '{new_ServiceChargeAmount}', " +
                            $"TotalAmount = '{new_TotalAmount_In_SaleReceipts}', " +
                            $"PaidAmount = '{new_TotalAmount_In_SaleReceipts}', " +
                            $"SalesOrderFee = '{new_SalesOrderFee_In_SaleReceipts}', " +
                            $"TaxAmount = '{new_TaxAmount_In_SaleReceipts}' " +
                            $"where SaleReceiptID = '{saleReceiptEntity.SaleReceiptID}';";
                        Helper.RunQueryUpdate(true, sql);

                        sql = $"update SaleReceiptPaymentDetails set " +
                                $"AmountTendered = '{new_AmountTendered_In_SaleReceiptPaymentDetails}', " +
                                $"PaidServiceFee = '{new_PaidServiceFee_InSaleReceiptPaymentDetails}', " +
                                $"AutoGratuityAmount = '{new_ServiceChargeAmount_InSaleReceiptPaymentDetails}' " +
                                $"where SaleReceiptPaymentDetailID = '{saleReceiptPaymentDetailEntity.SaleReceiptPaymentDetailID}';";
                        Helper.RunQueryUpdate(true, sql);

                        sql = $"update SaleReceipts set AutoGratuityTaxAmount = '{new_ServiceChargeTaxAmount}', ServiceChargeTaxAmount = '{new_ServiceChargeTaxAmount_In_SaleReceipts}' where SaleReceiptID = '{saleReceiptEntity.SaleReceiptID}';";
                        Helper.RunQueryUpdate(true, sql);

                        sql = $"update SaleReceiptPaymentDetails set AutoGratuityTaxAmount = '{new_ServiceChargeTaxAmount_InSaleReceiptPaymentDetails}' where SaleReceiptPaymentDetailID = '{saleReceiptPaymentDetailEntity.SaleReceiptPaymentDetailID}';";
                        Helper.RunQueryUpdate(true, sql);
                        if (saleReceiptEntity.SVCFeeTaxPaidAmount != 0)
                        {
                            sql = $"update SaleReceiptPaymentDetails set TaxAmountByPaymentModeTaxCustomFee = '{new_TaxAmount_In_SaleReceipts}' where SaleReceiptPaymentDetailID = '{saleReceiptPaymentDetailEntity.SaleReceiptPaymentDetailID}';";
                            Helper.RunQueryUpdate(true, sql);
                        }

                        break;
                    case ServiceChargeType.PartyServiceFee:
                        sql = $"update SaleReceipts set " +
                            $"PartyServiceFeeAmount = '{new_ServiceChargeAmount}', " +
                            $"TotalAmount = '{new_TotalAmount_In_SaleReceipts}', " +
                            $"PaidAmount = '{new_TotalAmount_In_SaleReceipts}', " +
                            $"SalesOrderFee = '{new_SalesOrderFee_In_SaleReceipts}', " +
                            $"TaxAmount = '{new_TaxAmount_In_SaleReceipts}' " +
                            $"where SaleReceiptID = '{saleReceiptEntity.SaleReceiptID}';";
                        Helper.RunQueryUpdate(true, sql);

                        sql = $"update SaleReceiptPaymentDetails set " +
                            $"AmountTendered = '{new_AmountTendered_In_SaleReceiptPaymentDetails}', " +
                            $"PaidServiceFee = '{new_PaidServiceFee_InSaleReceiptPaymentDetails}', " +
                            $"PartyServiceFeeAmount = '{new_ServiceChargeAmount_InSaleReceiptPaymentDetails}' " +
                            $"where SaleReceiptPaymentDetailID = '{saleReceiptPaymentDetailEntity.SaleReceiptPaymentDetailID}';";
                        Helper.RunQueryUpdate(true, sql);

                        sql = $"update SaleReceipts set PartyServiceFeeTaxAmount = '{new_ServiceChargeTaxAmount}', ServiceChargeTaxAmount = '{new_ServiceChargeTaxAmount_In_SaleReceipts}' where SaleReceiptID = '{saleReceiptEntity.SaleReceiptID}';";
                        Helper.RunQueryUpdate(true, sql);

                        sql = $"update SaleReceiptPaymentDetails set PartyServiceFeeTaxAmount = '{new_ServiceChargeTaxAmount_InSaleReceiptPaymentDetails}' where SaleReceiptPaymentDetailID = '{saleReceiptPaymentDetailEntity.SaleReceiptPaymentDetailID}';";
                        Helper.RunQueryUpdate(true, sql);

                        if (saleReceiptEntity.SVCFeeTaxPaidAmount != 0)
                        {
                            sql = $"update SaleReceiptPaymentDetails set TaxAmountByPaymentModeTaxCustomFee = '{new_TaxAmount_In_SaleReceipts}' where SaleReceiptPaymentDetailID = '{saleReceiptPaymentDetailEntity.SaleReceiptPaymentDetailID}';";
                            Helper.RunQueryUpdate(true, sql);
                        }

                        break;
                    case ServiceChargeType.ManualServiceFee:

                        sql = $"update SaleReceipts set " +
                            $"ServiceFee = '{new_ServiceChargeAmount}', " +
                            $"TotalAmount = '{new_TotalAmount_In_SaleReceipts}', " +
                            $"PaidAmount = '{new_TotalAmount_In_SaleReceipts}', " +
                            $"SalesOrderFee = '{new_SalesOrderFee_In_SaleReceipts}', " +
                            $"TaxAmount = '{new_TaxAmount_In_SaleReceipts}' " +
                            $"where SaleReceiptID = '{saleReceiptEntity.SaleReceiptID}';";
                        Helper.RunQueryUpdate(true, sql);

                        sql = $"update SaleReceiptPaymentDetails set " +
                            $"AmountTendered = '{new_AmountTendered_In_SaleReceiptPaymentDetails}', " +
                            $"PaidServiceFee = '{new_PaidServiceFee_InSaleReceiptPaymentDetails}', " +
                            $"ServiceFee = '{new_ServiceChargeAmount_InSaleReceiptPaymentDetails}' " +
                            $"where SaleReceiptPaymentDetailID = '{saleReceiptPaymentDetailEntity.SaleReceiptPaymentDetailID}';";
                        Helper.RunQueryUpdate(true, sql);

                        sql = $"update SaleReceipts set ServiceFeeTaxAmount = '{new_ServiceChargeTaxAmount}', ServiceChargeTaxAmount = '{new_ServiceChargeTaxAmount_In_SaleReceipts}' where SaleReceiptID = '{saleReceiptEntity.SaleReceiptID}';";
                        Helper.RunQueryUpdate(true, sql);

                        sql = $"update SaleReceiptPaymentDetails set ServiceFeeTaxAmount = '{new_ServiceChargeTaxAmount_InSaleReceiptPaymentDetails}' where SaleReceiptPaymentDetailID = '{saleReceiptPaymentDetailEntity.SaleReceiptPaymentDetailID}';";
                        Helper.RunQueryUpdate(true, sql);

                        if (saleReceiptEntity.SVCFeeTaxPaidAmount != 0)
                        {
                            sql = $"update SaleReceiptPaymentDetails set TaxAmountByPaymentModeTaxCustomFee = '{new_TaxAmount_In_SaleReceipts}' where SaleReceiptPaymentDetailID = '{saleReceiptPaymentDetailEntity.SaleReceiptPaymentDetailID}';";
                            Helper.RunQueryUpdate(true, sql);
                        }

                        break;

                    case ServiceChargeType.SVCFee:

                        sql = $"update SaleReceipts set " +
                            $"SVCFeeAmount = '{Amount + Tax}', " +
                            $"SVCFeePaidAmount = '{Amount + Tax}' " +
                            $"where SaleReceiptID = '{saleReceiptEntity.SaleReceiptID}';";
                        Helper.RunQueryUpdate(true, sql);

                        sql = $"update SaleReceipts set " +
                            $"SVCFeeTaxAmount = '{Tax}', " +
                            $"SVCFeeTaxPaidAmount = '{Tax}' " +
                            $"where SaleReceiptID = '{saleReceiptEntity.SaleReceiptID}';";
                        Helper.RunQueryUpdate(true, sql);

                        sql = $"update SaleReceiptPaymentDetails set " +
                            $"SVCFeeAmount = '{Amount + Tax}' " +
                            $"where SaleReceiptPaymentDetailID = '{saleReceiptPaymentDetailEntity.SaleReceiptPaymentDetailID}';";
                        Helper.RunQueryUpdate(true, sql);

                        sql = $"update SaleReceiptPaymentDetails set " +
                            $"SVCFeeTaxAmount = '{Tax}' " +
                            $"where SaleReceiptPaymentDetailID = '{saleReceiptPaymentDetailEntity.SaleReceiptPaymentDetailID}';";
                        Helper.RunQueryUpdate(true, sql);

                        break;

                    case ServiceChargeType.CashDiscount:

                        sql = $"update SaleReceipts set " +
                            $"CashDiscountAmount = '{Amount + Tax}', " +
                            $"CashDiscountPaidAmount = '{Amount + Tax}' " +
                            $"where SaleReceiptID = '{saleReceiptEntity.SaleReceiptID}';";
                        Helper.RunQueryUpdate(true, sql);

                        sql = $"update SaleReceipts set " +
                           $"CashDiscountTaxAmount = '{Tax}', " +
                           $"CashDiscountTaxPaidAmount = '{Tax}' " +
                           $"where SaleReceiptID = '{saleReceiptEntity.SaleReceiptID}';";
                        Helper.RunQueryUpdate(true, sql);

                        sql = $"update SaleReceiptPaymentDetails set " +
                           $"CashDiscountAmount = '{Amount + Tax}' " +
                           $"where SaleReceiptPaymentDetailID = '{saleReceiptPaymentDetailEntity.SaleReceiptPaymentDetailID}';";
                        Helper.RunQueryUpdate(true, sql);

                        sql = $"update SaleReceiptPaymentDetails set " +
                           $"CashDiscountTaxAmount = '{Tax}' " +
                           $"where SaleReceiptPaymentDetailID = '{saleReceiptPaymentDetailEntity.SaleReceiptPaymentDetailID}';";
                        Helper.RunQueryUpdate(true, sql);

                        break;
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void SelectServiceChargeToAdjstForm_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable dtSaleReceipts = new DataTable("SaleReceipts");
                using (SqlCommand sqlCommand = new SqlCommand())
                using (var conn = new SqlConnection(Helper.BLOGICPOS7_Connection))
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();

                    sqlCommand.Connection = conn;
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.CommandText = $"select * from SaleReceipts where SaleReceiptID = '{SaleReceiptID}' ";
                    var reader = sqlCommand.ExecuteReader();
                    dtSaleReceipts.Load(reader);

                    reader.Close();
                }

                decimal _ServiceFee = Utility.GetDecimalFromRow("ServiceFee", dtSaleReceipts.Rows[0]);
                decimal _ServiceFeeTaxAmount = Utility.GetDecimalFromRow("ServiceFeeTaxAmount", dtSaleReceipts.Rows[0]);
                decimal _AutoGratuityAmount = Utility.GetDecimalFromRow("AutoGratuityAmount", dtSaleReceipts.Rows[0]);
                decimal _AutoGratuityTaxAmount = Utility.GetDecimalFromRow("AutoGratuityTaxAmount", dtSaleReceipts.Rows[0]);
                decimal _AreaFeeAmount = Utility.GetDecimalFromRow("AreaFeeAmount", dtSaleReceipts.Rows[0]);
                decimal _AreaFeeTaxAmount = Utility.GetDecimalFromRow("AreaFeeTaxAmount", dtSaleReceipts.Rows[0]);
                decimal _PartyServiceFeeAmount = Utility.GetDecimalFromRow("PartyServiceFeeAmount", dtSaleReceipts.Rows[0]);
                decimal _PartyServiceFeeTaxAmount = Utility.GetDecimalFromRow("PartyServiceFeeTaxAmount", dtSaleReceipts.Rows[0]);
                decimal _SVCFeePaidAmount = Utility.GetDecimalFromRow("SVCFeePaidAmount", dtSaleReceipts.Rows[0]);
                decimal _SVCFeeTaxPaidAmount = Utility.GetDecimalFromRow("SVCFeeTaxPaidAmount", dtSaleReceipts.Rows[0]);
                decimal _CashDiscountPaidAmount = Utility.GetDecimalFromRow("CashDiscountPaidAmount", dtSaleReceipts.Rows[0]);
                decimal _CashDiscountTaxPaidAmount = Utility.GetDecimalFromRow("CashDiscountTaxPaidAmount", dtSaleReceipts.Rows[0]);

                amountServiceChargeValue.Text = (_ServiceFee).ToString("c2");
                taxServiceChargeValue.Text = _ServiceFeeTaxAmount.ToString("c2");

                amountAreaGratuityValue.Text = (_AreaFeeAmount).ToString("c2");
                taxAreaGratuityValue.Text = _AreaFeeTaxAmount.ToString("c2");

                amountPartyGratuityValue.Text = (_PartyServiceFeeAmount).ToString("c2");
                taxPartyGratuityValue.Text = _PartyServiceFeeTaxAmount.ToString("c2");

                amountAutoServiceChargeValue.Text = (_AutoGratuityAmount).ToString("c2");
                taxAutoServiceChargeValue.Text = _AutoGratuityTaxAmount.ToString("c2");
            }
            catch (System.Exception ex)
            {

            }
        }

        private void PnlCashDiscount_Click(object sender, EventArgs e)
        {
            ShowEnterServiceChargeForm(ServiceChargeType.CashDiscount);
        }

        private void PnlSVCFee_Click(object sender, EventArgs e)
        {
            ShowEnterServiceChargeForm(ServiceChargeType.SVCFee);
        }

        private void PnlAreaGratuity_Click(object sender, EventArgs e)
        {
            ShowEnterServiceChargeForm(ServiceChargeType.AreaServiceFee);
        }

        private void PnlAutoServiceCharge_Click(object sender, EventArgs e)
        {
            ShowEnterServiceChargeForm(ServiceChargeType.AutoServiceFee);
        }

        private void PnlPartyGratuity_Click(object sender, EventArgs e)
        {
            ShowEnterServiceChargeForm(ServiceChargeType.PartyServiceFee);
        }

        private void PnlServiceCharge_Click(object sender, EventArgs e)
        {
            ShowEnterServiceChargeForm(ServiceChargeType.ManualServiceFee);
        }

        private void ShowEnterServiceChargeForm(ServiceChargeType serviceChargeType)
        {
            try
            {
                EditServiceChargeType = serviceChargeType;

                EnterServiceChargeForm enterServiceChargeForm = new EnterServiceChargeForm();

                switch (serviceChargeType)
                {
                    case ServiceChargeType.ManualServiceFee:
                        enterServiceChargeForm.ChargeName = titleServiceCharge.Text;
                        enterServiceChargeForm.txtAmount.Value = Utility.ConvertMoneyStringToDecimal(amountServiceChargeValue.Text);
                        enterServiceChargeForm.txtTax.Value = Utility.ConvertMoneyStringToDecimal(taxServiceChargeValue.Text);
                        break;
                    case ServiceChargeType.AreaServiceFee:
                        enterServiceChargeForm.ChargeName = titleAreaGratuity.Text;
                        enterServiceChargeForm.txtAmount.Value = Utility.ConvertMoneyStringToDecimal(amountAreaGratuityValue.Text);
                        enterServiceChargeForm.txtTax.Value = Utility.ConvertMoneyStringToDecimal(taxAreaGratuityValue.Text);
                        break;
                    case ServiceChargeType.PartyServiceFee:
                        enterServiceChargeForm.ChargeName = titlePartyGratuity.Text;
                        enterServiceChargeForm.txtAmount.Value = Utility.ConvertMoneyStringToDecimal(amountPartyGratuityValue.Text);
                        enterServiceChargeForm.txtTax.Value = Utility.ConvertMoneyStringToDecimal(taxPartyGratuityValue.Text);
                        break;
                    case ServiceChargeType.AutoServiceFee:
                        enterServiceChargeForm.ChargeName = titleAutoServiceCharge.Text;
                        enterServiceChargeForm.txtAmount.Value = Utility.ConvertMoneyStringToDecimal(amountAutoServiceChargeValue.Text);
                        enterServiceChargeForm.txtTax.Value = Utility.ConvertMoneyStringToDecimal(taxAutoServiceChargeValue.Text);
                        break;
                        //case ServiceChargeType.SVCFee:
                        //    enterServiceChargeForm.txtAmount.Value = Utility.ConvertMoneyStringToDecimal(amountSVCFeeValue.Text);
                        //    enterServiceChargeForm.txtTax.Value = Utility.ConvertMoneyStringToDecimal(taxSVCFeeValue.Text);
                        //    break;
                        //case ServiceChargeType.CashDiscount:
                        //    enterServiceChargeForm.txtAmount.Value = Utility.ConvertMoneyStringToDecimal(amountCashDiscountValue.Text);
                        //    enterServiceChargeForm.txtTax.Value = Utility.ConvertMoneyStringToDecimal(taxCashDiscountValue.Text);
                        //    break;
                }

                if (BLogicMessageBox.ShowPopUpFormWithDIMBackground(enterServiceChargeForm, this) == DialogResult.OK)
                {
                    Amount = enterServiceChargeForm.txtAmount.Value;
                    Tax = enterServiceChargeForm.txtTax.Value;

                    switch (serviceChargeType)
                    {
                        case ServiceChargeType.ManualServiceFee:
                            amountServiceChargeValue.Text = Utility.ConvertDecimalToMoneyString(Amount);
                            taxServiceChargeValue.Text = Utility.ConvertDecimalToMoneyString(Tax);
                            break;
                        case ServiceChargeType.AreaServiceFee:
                            amountAreaGratuityValue.Text = Utility.ConvertDecimalToMoneyString(Amount);
                            taxAreaGratuityValue.Text = Utility.ConvertDecimalToMoneyString(Tax);
                            break;
                        case ServiceChargeType.PartyServiceFee:
                            amountPartyGratuityValue.Text = Utility.ConvertDecimalToMoneyString(Amount);
                            taxPartyGratuityValue.Text = Utility.ConvertDecimalToMoneyString(Tax);
                            break;
                        case ServiceChargeType.AutoServiceFee:
                            amountAutoServiceChargeValue.Text = Utility.ConvertDecimalToMoneyString(Amount);
                            taxAutoServiceChargeValue.Text = Utility.ConvertDecimalToMoneyString(Tax);
                            break;
                            //case ServiceChargeType.SVCFee:
                            //    amountSVCFeeValue.Text = Utility.ConvertDecimalToMoneyString(Amount);
                            //    taxSVCFeeValue.Text = Utility.ConvertDecimalToMoneyString(Tax);
                            //    break;
                            //case ServiceChargeType.CashDiscount:
                            //    amountCashDiscountValue.Text = Utility.ConvertDecimalToMoneyString(Amount);
                            //    taxCashDiscountValue.Text = Utility.ConvertDecimalToMoneyString(Tax);
                            //    break;
                    }

                    IsChangedServiceCharges = true;

                    backgroundWorker.RunWorkerAsync();
                }
            }
            catch (Exception ex)
            {

            }

        }
    }
}
