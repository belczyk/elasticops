using System;
using System.Diagnostics.Tracing;
using ElasticOps.Com.CommonTypes;
using Version = ElasticOps.Com.CommonTypes.Version;

namespace ElasticOps
{
    public class Settings
    {
        public Settings()
        {
            Connection = new Connection();
        }

        public Connection Connection { get; set; }
    }
}
