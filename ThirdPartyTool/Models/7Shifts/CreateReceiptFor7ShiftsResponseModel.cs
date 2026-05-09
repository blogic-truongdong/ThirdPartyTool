namespace ThirdPartyTool.Models._7Shifts
{
    public class CreateReceiptFor7ShiftsResponseModel
    {
        public CreateReceiptFor7ShiftsResponseDetail data { get; set; }
    }

    public class CreateReceiptFor7ShiftsResponseDetail
    {
        public string uuid { get; set; }
        public string receipt_id { get; set; }
    }
}
