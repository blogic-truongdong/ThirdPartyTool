using System;

namespace ThirdPartyTool.Models
{
    public class ConfigurationEntity
    {
        public Guid ConfigurationID { get; set; }

        public string ConfigurationName { get; set; }

        public string ConfigurationValue { get; set; }

        public string WorkstationID { get; set; }

        public int ConfigurationLevel { get; set; }

        public bool Enable { get; set; }

    }
}
