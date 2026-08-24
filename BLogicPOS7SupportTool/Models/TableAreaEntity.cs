namespace BLogicPOS7SupportTool.Models
{
    // A sitting area (SittingAreas) used to group tables on the floor.
    class TableAreaEntity
    {
        public string AreaID { get; set; }
        public string AreaName { get; set; }

        public override string ToString() => AreaName;
    }
}
