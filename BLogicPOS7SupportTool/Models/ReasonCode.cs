namespace BLogicPOS7SupportTool.Models
{
    // A row from dbo.ReasonCodes, used to populate the Reason combobox on the
    // Item Detail form (filtered by ReasonType per the chosen status).
    class ReasonCode
    {
        public int ReasonCodeID { get; set; }
        public string ReasonCodeName { get; set; }
    }
}
