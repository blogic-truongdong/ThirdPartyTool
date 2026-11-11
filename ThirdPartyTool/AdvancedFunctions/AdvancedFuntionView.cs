using BLogicSystems.XPos.ClientUtilities;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Windows.Forms;
using ThirdPartyTool.Models;
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

            btnClearSalesData7Shifts.Click += BtnClearSalesData7Shifts_Click;

            btnCorrectDataStreamOrder.Click += BtnCorrectDataStreamOrder_Click;
#if DEBUG
            btnCancelProcess.Click += BtnCancelProcess_Click;
            btnTest.Visible = true;
            btnSyncDataFor7Shifts_ClearTip.Visible = true;
            btnTest.Click += BtnTest_Click;
            backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += BackgroundWorker_DoWork;
            backgroundWorker.RunWorkerCompleted += BackgroundWorker_RunWorkerCompleted;

            btnSyncDataFor7Shifts_ClearTip.Click += BtnSyncDataFor7Shifts_ClearTip_Click;

#endif
            flpContent.Resize += (s, e) => LayoutCards();
            UpdateSections();
            LayoutCards();
        }

        /// <summary>Shows a section heading only when at least one of its tools is listed.</summary>
        private void UpdateSections()
        {
            lblSectionTickets.Visible = btnCloseUnPaidTicket.IsListed || btnClearTableStatus.IsListed || btnUpdateDSPTax.IsListed || btnCorrectDataStreamOrder.IsListed;
            lblSection7Shifts.Visible = btnSyncDataFor7Shifts.IsListed || btnSyncDataFor7Shifts_ClearTip.IsListed || btnClearSalesData7Shifts.IsListed;
            lblSectionDeveloper.Visible = btnTest.IsListed;
        }

        /// <summary>Stretches cards to the content width (capped so lines stay readable).</summary>
        private void LayoutCards()
        {
            int width = Math.Min(flpContent.ClientSize.Width - flpContent.Padding.Horizontal, 720);
            if (width < 200)
                return;
            flpContent.SuspendLayout();
            foreach (Control control in flpContent.Controls)
                control.Width = width;
            flpContent.ResumeLayout();
        }

        private void BtnCorrectDataStreamOrder_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = "SELECT * FROM dbo.SaleReceiptDetails WHERE SaleReceiptID in ( '640BCC63-D87F-4B30-B19A-765E1515BE91', 'C816C34E-E116-479A-94CF-5893F80D2B75', 'B54709F9-96F4-4435-9EEF-D108E4A4DA92', 'E760F357-8457-4345-80E3-C099D79A627B', 'A13023F3-444D-44C9-BE05-ADD98A09DD19', '16FE8D8A-934E-4A0E-8DE5-FF725E36CD1A', 'E0B118E9-FDE0-429A-BC79-842B5BA0FE70', 'BEE1D03E-287A-4A7C-A81B-BE64FB0E3E8C', 'E12C9C36-2DA9-4B59-ABD4-109DDAE1952B', '441499D9-3E8A-4E69-80B4-199F830F24F2', 'B47D2890-C0E0-434E-BE0E-2B4570060498', '3D047570-7F4E-4728-AABA-5971D10E2967', 'E7ECEAAF-61AA-4DCF-AA92-6742B81C7EF3', 'BD3B7AFD-89AE-414F-81CF-80387EB720FB', '171DF45F-006C-466B-B7A8-88F12314AAC2', 'BEB10A19-145E-4E82-9B4F-8B08EB670AD8', '52C769A2-3D71-4611-A58B-8E18B2078EF4', '66B5BBFF-731C-42EA-BEDB-9435DB8E9948', 'A4BB8497-597F-4CA5-B237-98FF76E71691', '6A449EF2-27DA-4F8C-9F08-A96BB89276BF', 'CED024FC-CB95-4A91-998E-CA41ADFF999B', '87A3224B-535E-43DE-BDE4-CA6D001B1ABF', '21A2AACD-3608-43A8-A86E-CAE4F67CD44A', '15DF08C2-8105-4B43-9EC9-F68E0D0F4DC9', 'B91D5446-E682-4673-AA4B-F917E4E658C7', '0EF4C9DE-C767-4B02-AFBE-00ED41D501A5', '29FA1795-9764-49D8-B1DF-152EF8DB4DED', '6F89B990-307D-42F2-B8B2-2870867D0288', '63A063D2-9912-4E44-90E4-3455478948F1', '54A26A46-1EFB-4EAD-955E-48CB104F737E', '660084FB-7159-4055-8162-522ED6C27241', 'FC131A71-B372-48FF-8446-544FB8986F90', '633E1DC4-DFA1-4AFC-9A00-6BAC6F7567E5', 'E0B118E9-FDE0-429A-BC79-842B5BA0FE70', '46BA27A1-E1AD-4BC9-BB42-99A6C2CC0079', '61176B92-42A8-4B78-9225-BC70AAF74805', 'A88396F2-3C90-4CB8-A58F-C4D2D36B9E65', '0F4504DE-B492-465E-9BF1-C5632CBB2963', '958F7FE8-805A-4814-87F5-D8FA2D125DD3', '0C0DFF7F-197B-4CE4-ADF5-DE78781BB3E4', '740A75EE-F2E0-4C2F-BBDD-DECD9C5951E3', '33660122-D791-4DF0-BFDF-EC07604671C0', '0230A2B2-784E-4C36-A498-F25A5EC3CCFF', 'B4FF9564-BEC1-4CEE-BCC8-F43704B63957', '16FE8D8A-934E-4A0E-8DE5-FF725E36CD1A', 'BE911537-50F7-44F2-A5EF-02A47C52071D', '598E2CCF-D162-42B9-9955-10FFF81F2066', '0E202B9F-D7A2-4892-B170-1157F842FA3C', 'C3380DF2-BD0F-4C7A-ACBC-154235C9F2D9', '39AD1BBF-4EA8-47EF-8CFA-1A7BE0643048', 'D0523CDA-916C-4DEC-90BC-348321582622', 'A1EC1ECB-C32F-46D0-93BE-35E1122A6B90', 'BBA245AC-100B-41B0-A8F5-531FEA17416E', '3C0BE07C-DC7B-4CB5-9527-6CA30B1E0F75', '44FBFA39-606E-46EA-B9A6-7C29D70DD94E', 'A0AA09DF-8062-444B-8C63-7E0F02FC761F', 'BF47F7AE-8961-4805-ADB1-862A19F92E8D', '97445EB2-317A-4B6D-896B-956D90D71036', '31CC6B24-CF32-47C9-B5E9-9B0E18BB050B', 'AB740C53-3E8E-44CE-8CB7-BB4FC2B4541C', 'BEE1D03E-287A-4A7C-A81B-BE64FB0E3E8C', '76C8768E-FB3F-4A85-965F-C84A7AD31731', '76587637-4100-49E9-A0DC-D0AA5A344F25', 'C182EA03-55D1-45B4-A4C0-D4F8D21B8A65', '71018125-DD98-4314-AC00-D72921C382A7', '1197BAF5-333A-43FC-AD85-D90F9446823D', 'E9CB8EB0-4B3D-44BA-BB39-DA880F0EAA27', '815B833E-A0DA-4BFB-8F6E-DAC592CB29E7', '1782A063-39E6-4883-A3A2-DD1F595F6F25', '6489792E-4C1A-4F7C-84DB-F28CD733B317', 'CACB324F-41F9-49C1-B042-F6F66784135C', '160710F1-EF4C-4320-A43C-03CE934F357A', '6FE4EDDA-3799-458C-BA83-0AD3380233F5', 'C5286302-A521-4973-98DE-144734D46DEE', 'B705B933-AC75-4FC5-8BF8-191C2F46F5CA', 'F4647A0B-E52F-4801-AF48-21BBA2E8253E', '47B1BBF9-2382-4259-AB14-4E653353C822', '29F33ECE-B49A-4139-8149-599E7BC39F61', '07C817E5-6B20-454D-979A-60A6A35A4642', '10FBB1FB-D9A4-4D1B-B92A-6809BDBF7978', '63E1D1DE-A8FA-47A5-B0ED-68F14C8CFF12', '81E0C401-5951-414B-AF9F-6F2102A6FB91', 'C023DCD4-E1DA-4A58-AE8C-73FA73F47022', '2B14DE0E-08FA-4A94-900A-77A1CD7417DF', '5CCC7F2F-E534-49B7-AE0F-77DEB093E0C4', '432F8B4D-174A-4975-A699-7A08907D1E44', '10A96D50-3B40-4483-84CF-8ACB0B014264', '15A786E6-E1AF-4009-9F38-8DCDEFCF94BF', '785C2D32-4FCC-4928-84AF-A505CA232B12', '328B3236-9F63-43D7-8E8D-AEB3D0640C05', 'A7B04D36-D7B5-4C84-850D-AFE3FE668B71', '1EFB9F8D-A35E-421E-A7E8-AFFB7269CABC', 'E760F357-8457-4345-80E3-C099D79A627B', '8C424139-0178-4900-BA81-C160FEF46EB5', '9A43069C-8B74-490B-A14D-D75D14BB5E79', '45DC096A-938B-42F8-B91B-F4AA396BE2EE', 'E4F8E1A2-9C2B-4487-8F71-196BF1489D00', '1E60BC9D-FD92-4C36-9150-1B9FADC485CF', 'B9EEDB76-D792-44AD-9382-1FFEA1A75E4F', '683B70FE-15D8-42E6-889C-26A849A01E37', '8D751AC7-1555-45AD-80DE-343FBE8F021C', 'E947D8C4-38E6-4ACA-BFCE-3634DB3FA64D', 'B07D4D14-B4EC-42BD-B191-4365768617BE', 'D2DC83EC-4F3E-44C7-8CE7-557169F033DF', 'C11DFA4F-EBE9-44B9-A1B7-55F2AC924D50', 'FC6F152B-825D-4E1D-8680-791FB9849E89', '57217113-FC4B-4313-94A7-94996BC2F667', '304F5B3A-355A-4163-B0C2-9A4CDBB33543', 'AF9B2C81-5B73-4D61-80E0-9A894F0C2F00', '70807E62-B641-4766-A125-A828EAAF9F92', 'F83C174A-39B4-4C76-8A60-BB12A2F5F913', '4C48E457-9C13-4BD9-B558-DDC8C396C1A7', 'A9D35E49-A831-4D4D-B2EC-DFC71D122E5A', '1BE8227E-E469-46CB-8A55-E76BD562F804', 'B654FEBB-3089-436E-AEEB-F7EC974B08A2', '49BB6BB6-091F-41EC-9E0A-F9E1F9EA6850', '2F382D3A-005A-4F28-96D3-02F29040ABB4', '2BF850C8-F76A-47B0-998F-0E3C36F552C3', '549164A4-B659-47F5-81A8-11EAAE7CCEB8', 'AAF99550-8BF4-4286-975A-13FDFF998921', 'A8238FC3-23FD-4C5E-8158-186309E52265', 'E5541EBC-A5BE-45C8-9E71-18733962C00C', '091E3D60-DBCD-4D03-AAA7-1B412DD45B74', '306033A4-34C1-4587-B02A-1F5A27403D87', '93DE81CB-DE46-4B8E-9887-31587A20EE1E', '347E0345-AD38-42CA-B376-338B90F844B0', '3C880C4B-81B8-42F2-BD71-3559065A4229', 'DAEEA63C-9DEE-4E61-90FA-388AC65ECB82', 'C52BA3D6-1F84-485E-8B25-3A0E91C771A9', '619D3444-4310-4AB9-9A88-3E9787E039E1', '6A6005A1-53FD-47F7-AEDA-5E0C645FF4FF', 'C410DA22-0839-4122-ACCA-651C3FFD4D86', 'EA06077C-7B41-4A92-84EF-6AFB6BC10444', '644B6CF6-8BFB-4BC5-ADC2-6BD9E1DCF4BB', '9AFB4566-8CFA-4A65-8F9E-74D3070165FB', '39B15E88-7CFE-434E-94CC-7F900EE51679', 'B595C9B4-BDEA-4576-A4FA-84F0865054ED', 'A13023F3-444D-44C9-BE05-ADD98A09DD19', 'D262FFF6-DA55-485E-AFBC-BAD2AB52B748', '7D7911D5-A920-4FE3-B621-D60402415C29', '69AA05A7-A00B-4370-B3B0-D9CA87B11B89', 'EEB11E9A-AF9A-40B6-BE16-E1F29B94A71F', '15D87D85-D14D-40B5-9891-E26183E362DC', 'DD96052C-E297-4728-8D6B-FEC143ED23BB', 'D5C4E36B-7EC2-40E3-A2D0-00C92FD53EF0', '2945DB0E-E8A5-4822-B6B3-028D81FEED1F', '696DAE71-EDC2-4698-8394-0B2408A31068', '55C2EE22-2E0D-4C51-B30D-2FE7479BABB8', '8D825BDF-4C61-4F27-B308-30204BB99BB2', 'D7C37A1F-0916-40A0-9C69-341E3A593972', 'D388E4B3-41CC-4FE0-98D9-349AA1698C0A', '7B3107C9-212C-4B6E-B4D9-393C2AF82067', 'A819F5E0-1125-4768-AF45-4222B667FA5A', 'EE753A35-8576-48B8-BD6A-565DFAE15EAE', 'C816C34E-E116-479A-94CF-5893F80D2B75', '640BCC63-D87F-4B30-B19A-765E1515BE91', '2A4C3F70-E0AE-4CBE-ACF2-7ECE7288EF2D', '6FE0A663-DBE7-4A5A-A4B9-8BBA3523471A', '10B71431-9218-4E26-9C23-906890A718A1', '0BBA0712-E4FA-4473-89E3-930CE7609B78', '0D89AFD3-3259-4659-9519-C131EE1A23E4', '488193D0-8403-4DB2-9779-EA7D6868F3CA', '4695983C-5D4F-44BC-B85E-EAB2B880DB07', '06D091F9-7931-43A3-9A18-F55DFD9841DC', 'D0029235-86D8-4B2A-B20A-F6AEDE1750BC', 'C71D271E-7AA2-453F-A7ED-05B2563D06CD', 'FBCDCED7-D7C7-48C8-90DB-0F496FF93CEF', '9EB71E40-2075-40C4-A54F-2CB3DB1E4FDD', '580D50F5-6E8C-44E0-AB73-474AC71CE511', '33A61CBD-C3D8-4CC6-A42A-4A34052F3697', '799B184C-EF4A-4230-86A5-53456881F743', '13C9A739-BEA2-4766-91B1-61F9A9A577DD', 'F493B6A5-A386-44CF-9698-6C200AF43B93', '07013F1E-0632-4B32-840D-74BF710E79F7', 'C01A6B73-1A7F-4586-8038-7943B64EF212', '769744BC-755C-4F5F-8377-7DBAC854C830', '802241FF-C859-450F-A11D-81FEA4EF66C0', '2FBE3D65-A8DC-4184-B008-8C298A133ABE', 'F882F00C-4079-4308-A441-9A68B2B8A836', '6C9ED7DA-4C3D-4B13-8EAB-9B1F63B0F99E', 'DFAEB7DF-BBE0-4E07-819F-9EBED6311562', '226F7B3D-C17A-4D88-816E-B5B65DF696AB', 'F8DBB19C-E7D6-475F-AC32-CC703DD171B1', 'DE570400-8597-43BF-9345-D089A1990550', 'B54709F9-96F4-4435-9EEF-D108E4A4DA92', 'D9C1DA3B-4132-4154-AAC1-F22A49E68BE7') ORDER BY SaleReceiptID, Range";

                var listSaleReceiptDetails = Helper.ProcessQueryEntity<SaleReceiptDetailEntity>(true, sql);

                var groupBySaleReceiptID = listSaleReceiptDetails.GroupBy(x => x.SaleReceiptID);
            }
            catch (Exception ex)
            {

            }
        }

        private void BtnClearSalesData7Shifts_Click(object sender, EventArgs e)
        {
            ClearDataFor7ShiftsForm form = new ClearDataFor7ShiftsForm();
            BLogicMessageBox.ShowPopUpFormWithDIMBackground(form, Helper.MainForm);
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
                    $"&receipt_date[gte]=2026-05-01" +
                    $"&receipt_date[lte]=2026-05-02" +
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
                    $"&receipt_date[gte]=2026-05-01" +
                    $"&receipt_date[lte]=2026-05-02" +
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
            if (isProcessing)
                pnlLoadingContainer.BringToFront();
        }

    }

    /// <summary>
    /// Clickable tool card: title, one-line description, optional badge and a chevron.
    /// Derives from Label so existing Click wiring keeps working.
    /// </summary>
    public class ActionCard : Label
    {
        private static readonly Color Primary = Color.FromArgb(42, 108, 228);
        private static readonly Color Border = Color.FromArgb(224, 224, 224);
        private static readonly Color HoverBack = Color.FromArgb(247, 250, 255);
        private static readonly Color TitleColor = Color.FromArgb(33, 33, 33);
        private static readonly Color MutedColor = Color.FromArgb(117, 117, 117);
        private static readonly Color BadgeBack = Color.FromArgb(255, 243, 205);
        private static readonly Color BadgeText = Color.FromArgb(133, 77, 14);
        private static readonly Font TitleFont = new Font("Roboto", 11F, FontStyle.Bold);
        private static readonly Font DescriptionFont = new Font("Roboto", 9.75F);
        private static readonly Font BadgeFont = new Font("Roboto", 7.5F, FontStyle.Bold);

        private bool hover;

        public ActionCard()
        {
            AutoSize = false;
            BackColor = Color.White;
            Cursor = Cursors.Hand;
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw, true);
        }

        [DefaultValue("")]
        public string Description { get; set; } = "";

        [DefaultValue("")]
        public string Badge { get; set; } = "";

        /// <summary>The visibility this card was given (Visible reads false while the parent is hidden).</summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsListed { get; private set; } = true;

        protected override void SetVisibleCore(bool value)
        {
            IsListed = value;
            base.SetVisibleCore(value);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            hover = true;
            Invalidate();
            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            hover = false;
            Invalidate();
            base.OnMouseLeave(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(Parent?.BackColor ?? Color.White);
            g.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle box = new Rectangle(0, 0, Width - 1, Height - 1);
            using (GraphicsPath path = RoundedRect(box, 8))
            {
                using (SolidBrush fill = new SolidBrush(hover ? HoverBack : Color.White))
                    g.FillPath(fill, path);
                using (Pen pen = new Pen(hover ? Primary : Border, 1))
                    g.DrawPath(pen, path);
            }

            const int left = 20;
            int textRight = Width - 48;
            TextFormatFlags flags = TextFormatFlags.NoPrefix | TextFormatFlags.SingleLine | TextFormatFlags.EndEllipsis;

            Size titleSize = TextRenderer.MeasureText(g, Text, TitleFont, new Size(textRight - left, 24), flags);
            TextRenderer.DrawText(g, Text, TitleFont, new Rectangle(left, 14, textRight - left, 24), hover ? Primary : TitleColor, flags);

            if (!string.IsNullOrEmpty(Badge))
            {
                Size badgeSize = TextRenderer.MeasureText(g, Badge, BadgeFont);
                Rectangle badge = new Rectangle(left + titleSize.Width + 6, 17, badgeSize.Width + 4, 18);
                using (GraphicsPath path = RoundedRect(badge, 4))
                using (SolidBrush brush = new SolidBrush(BadgeBack))
                    g.FillPath(brush, path);
                TextRenderer.DrawText(g, Badge, BadgeFont, badge, BadgeText, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.NoPrefix);
            }

            TextRenderer.DrawText(g, Description, DescriptionFont, new Rectangle(left, 40, textRight - left, 20), MutedColor, flags);

            int cx = Width - 26;
            int cy = Height / 2;
            using (Pen chevron = new Pen(hover ? Primary : MutedColor, 2f))
            {
                chevron.StartCap = chevron.EndCap = LineCap.Round;
                g.DrawLines(chevron, new[] { new Point(cx - 3, cy - 6), new Point(cx + 3, cy), new Point(cx - 3, cy + 6) });
            }
        }

        private static GraphicsPath RoundedRect(Rectangle r, int radius)
        {
            int d = radius * 2;
            GraphicsPath path = new GraphicsPath();
            path.AddArc(r.X, r.Y, d, d, 180, 90);
            path.AddArc(r.Right - d, r.Y, d, d, 270, 90);
            path.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90);
            path.AddArc(r.X, r.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }
    }
}
