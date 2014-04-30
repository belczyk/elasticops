using System;
using ElasticOps.Com.CommonTypes;
using Version = ElasticOps.Com.CommonTypes.Version;

namespace ElasticOps
{
    public class Settings
    {
        private Connection _connection;

        public Connection Connection
        {
            get
            {
                if (_connection==null)
                    Connection = new Connection(new Uri("http://localhost:9200"), Version.FromString("1.0.0.0"));
                return _connection;
            }
            set { _connection = value; }
        }
    }
}
