using System;
using System.Collections.Generic;

namespace ElasticOps.Behaviors
{
    public  class IndexTypes
    {
        public List<string> Types { get; set; }
        public DateTime LastUpdated { get; set; }

        public IndexTypes(List<string> types)
        {
            Types = types;
            LastUpdated = DateTime.Now;
        }
    }
}