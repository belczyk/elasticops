using System;
using Nest;

namespace ElasticOps.Model
{
    public class ClusterInfo
    {
        public IHealthResponse Load(Uri clusterUri)
        {
            var client = new ElasticClient(new ConnectionSettings(clusterUri));

            return client.ClusterHealth();
        }

    }
}
