
using ElasticOps.Attributes;

namespace ElasticOps
{
    [Priority(1)]
    public class ViewSettings : UserSettings
    {
        public ViewSettings()
        {
            DisplayName = "View";
        }
    }
}
