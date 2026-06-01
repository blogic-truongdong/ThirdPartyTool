namespace BLogicPOS7SupportTool.Models
{
    // Per-item status applied from the Edit Items form.
    //   Completed -> Status = 0, ReturnCode = NULL, IsCompItem = 0 (active, normal)
    //   Cancel    -> Status = 2, ReturnCode = reason (excluded from totals)
    //   Refund    -> Status = 2, ReturnCode = reason (same effect as Partial Refund)
    //   Comp      -> Status = 0, IsCompItem = 1, ReturnCode = reason (excluded via IsCompItem)
    // Cancel/Refund use ReasonType 3/13, Comp uses ReasonType 3 for its reason list.
    enum ItemEditStatus
    {
        Completed,
        Cancel,
        Refund,
        Comp
    }
}
