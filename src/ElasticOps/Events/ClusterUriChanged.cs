using System;

namespace ElasticOps.Events
{
    public class ClusterUriChanged
    {
        public ClusterUriChanged(Uri uri)
        {
            ClusterUri = uri;
        }
        public Uri ClusterUri { get; set; }
    }
}