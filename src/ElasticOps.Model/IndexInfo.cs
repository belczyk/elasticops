using System.Collections.Generic;

namespace ElasticOps.Model
{
    public class IndexInfo
    {
        public string Name { get; set; }
        public List<ESTypeInfo> Types { get; set; }
        public Dictionary<string,string> Settings { get; set; } 
        public string State { get; set; }
    }
}
