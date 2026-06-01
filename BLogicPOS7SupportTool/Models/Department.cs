namespace BLogicPOS7SupportTool.Models
{
    class Department
    {
        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }

        public override string ToString() => DepartmentName;
    }
}
