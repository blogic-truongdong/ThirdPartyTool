using BLogicSystems.XPos.ClientUtilities;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Windows.Forms;
using ThirdPartyTool.Models._7Shifts;

namespace ThirdPartyTool.AdvancedFunctions
{
    public partial class AdvancedFuntionView : UserControl
    {
        private BackgroundWorker backgroundWorker;
        private bool IsCancelProccess = false;
        public AdvancedFuntionView()
        {
            InitializeComponent();

            this.btnCloseUnPaidTicket.Click += BtnCloseUnPaidTicket_Click;

            btnClearTableStatus.Click += BtnClearTableStatus_Click;

            btnSyncDataFor7Shifts.Click += BtnSyncDataFor7Shifts_Click;

            btnUpdateDSPTax.Click += BtnUpdateDSPTax_Click;

#if DEBUG
            btnCancelProcess.Click += BtnCancelProcess_Click;
            btnTest.Visible = true;
            btnTest.Click += BtnTest_Click;
            backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += BackgroundWorker_DoWork;
            backgroundWorker.RunWorkerCompleted += BackgroundWorker_RunWorkerCompleted;

            btnSyncDataFor7Shifts_ClearTip.Click += BtnSyncDataFor7Shifts_ClearTip_Click;

#endif  
        }

        private void BtnUpdateDSPTax_Click(object sender, EventArgs e)
        {
            UpdateDSPTaxForAllTicketsOfStreamForm form = new UpdateDSPTaxForAllTicketsOfStreamForm();
            BLogicMessageBox.ShowPopUpFormWithDIMBackground(form, Helper.MainForm);
        }

        private void BtnSyncDataFor7Shifts_ClearTip_Click(object sender, EventArgs e)
        {
            ReSyncDataFor7Shifts_ClearTip_Form form = new ReSyncDataFor7Shifts_ClearTip_Form();
            BLogicMessageBox.ShowPopUpFormWithDIMBackground(form, Helper.MainForm);
        }

        private void BtnCancelProcess_Click(object sender, EventArgs e)
        {
            IsCancelProccess = true;
        }

        private void BtnTest_Click(object sender, EventArgs e)
        {
            StartProcess();
            IsCancelProccess = false;
            Task.Run(() => backgroundWorker.RunWorkerAsync());
        }

        private async void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            decimal sumTotal = 0;
            decimal sumTip = 0;
            int totalReceipt = 0;
            try
            {
                string accessToken = "37303131323238622d336636632d343733362d616531312d343261343533343261666136";

                var _httpClient = new HttpClient();
                _httpClient.BaseAddress = new Uri("https://api.7shifts.com/");
                _httpClient.DefaultRequestHeaders.Accept.Clear();
                _httpClient.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", accessToken);

                string companyId = "337570";
                string locationId = "411979";

                string url =
                    $"v2/company/{companyId}/receipts" +
                    $"?location_id={locationId}" +
                    $"&receipt_date[gte]=2026-05-02" +
                    $"&receipt_date[lte]=2026-05-03" +
                    $"&limit=100";

            ProcessRetry:

                HttpResponseMessage response = await _httpClient.GetAsync(url);

                string responseBody = await response.Content.ReadAsStringAsync();

                var listSaleReceipt = JsonConvert.DeserializeObject<SevenShifts_ListReceipt>(responseBody);

                sumTotal += ((decimal)listSaleReceipt?.data?.Where(x => x.NetTotal > 0).Sum(x => x.NetTotal)) / 100;

                sumTip += ((decimal)listSaleReceipt?.data?.Where(x => x.Tips > 0).Sum(x => x.Tips)) / 100;

                totalReceipt += listSaleReceipt.data.Count;

                if (!string.IsNullOrWhiteSpace(listSaleReceipt?.meta?.cursor?.next))
                {
                    url =
                    $"v2/company/{companyId}/receipts" +
                    $"?location_id={locationId}" +
                    $"&receipt_date[gte]=2026-05-02" +
                    $"&receipt_date[lte]=2026-05-03" +
                    $"&limit=100" +
                    $"&cursor={listSaleReceipt?.meta?.cursor?.next.Substring(15)}";

                    goto ProcessRetry;

                }
                else
                {

                }

            }
            catch (Exception ex)
            {
                BLogicLogger.Exception("ReSyncDataFor7ShiftsForm.BackgroundWorker_DoWork", ex);
            }
        }

        private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                EndProcess();
            }
            catch (Exception ex)
            {
                BLogicLogger.Exception("ReSyncDataFor7ShiftsForm.BackgroundWorker_RunWorkerCompleted", ex);
            }
        }

        private void BtnSyncDataFor7Shifts_Click(object sender, EventArgs e)
        {
            ReSyncDataFor7ShiftsForm form = new ReSyncDataFor7ShiftsForm();
            BLogicMessageBox.ShowPopUpFormWithDIMBackground(form, Helper.MainForm);
        }

        private void BtnClearTableStatus_Click(object sender, EventArgs e)
        {
            ClearTableStatusForm clearTableStatusForm = new ClearTableStatusForm();
            BLogicMessageBox.ShowPopUpFormWithDIMBackground(clearTableStatusForm, Helper.MainForm);
        }

        private void BtnCloseUnPaidTicket_Click(object sender, EventArgs e)
        {
            CloseUnPaidTicketForm closeUnPaidTicketForm = new CloseUnPaidTicketForm();
            BLogicMessageBox.ShowPopUpFormWithDIMBackground(closeUnPaidTicketForm, Helper.MainForm);
        }

        private void StartProcess()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(delegate
                {
                    SetProcessingState(true);
                }));
            }
            else
            {
                SetProcessingState(true);
            }
        }

        private void EndProcess()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(delegate
                {
                    SetProcessingState(false);
                }));
            }
            else
            {
                SetProcessingState(false);
            }
        }

        private void SetProcessingState(bool isProcessing)
        {
            this.Enabled = !isProcessing;
            pnlLoadingContainer.Visible = isProcessing;
        }

    }
}
