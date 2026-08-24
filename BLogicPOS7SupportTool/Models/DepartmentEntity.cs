namespace BLogicPOS7SupportTool.Models
{
    class DepartmentEntity
    {
        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }

        public override string ToString() => DepartmentName;
    }
}
