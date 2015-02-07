using System;
using System.Collections.Generic;
using System.Linq;

namespace ElasticOps.Services
{
    public  class Index
    {
        public IEnumerable<string> Types { get; set; }
        public IEnumerable<string> Analyzers { get; set; }

        public DateTime LastUpdated { get; set; }

        public Index(IEnumerable<string> types)
        {
            Types = types.OrderBy(x=>x);
            LastUpdated = DateTime.Now;
        }
    }
}