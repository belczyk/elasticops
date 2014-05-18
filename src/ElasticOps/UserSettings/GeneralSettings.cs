
using System;
using ElasticOps.Attributes;

namespace ElasticOps
{
    [Priority(0)]
    public class GeneralSettings : UserSettings
    {
        public GeneralSettings()
        {
            DisplayName = "General";
            DefaultClusterUri = new Uri("http://localhost:9200");
            PrimaryFontSize = 16;
        }

        [Setting("Default cluster URL",0)]
        public Uri DefaultClusterUri { get; set; }

        [Setting("Primary font size", 1)]
        public int PrimaryFontSize { get; set; }
    }
}
