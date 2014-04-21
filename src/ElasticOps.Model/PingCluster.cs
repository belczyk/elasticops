using System;
using Elasticsearch.Net;
using Elasticsearch.Net.Connection;

namespace ElasticOps.Model
{
    public class PingCluster
    {
        public bool IsAlive(Uri clusterUri)
        {
            var client = new ElasticsearchClient(new ConnectionConfiguration(clusterUri));
            var res = client.Ping();


            return res.Success;
        }
    }
}
