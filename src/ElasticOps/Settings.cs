using System;


namespace ElasticOps
{
    public class Settings
    {
        private object _lock = new object();
        private Uri _clusterUri;

        public Uri ClusterUri
        {
            get
            {
                if (_clusterUri==null)
                    lock (_lock)
                    {
                        if (_clusterUri == null)
                        {
                            _clusterUri = new Uri("http://localhost:9200");
                        }
                    }
                return _clusterUri;
            }
            set
            {
                lock (_lock)
                {
                    _clusterUri = value;
                }
            }
        }
    }
}
