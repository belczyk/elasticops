using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Elasticsearch.Net;
using Elasticsearch.Net.Connection;
using Humanizer;
using Nest;

namespace ElasticOps.Model
{
    public class ClusterInfo
    {
        public IDictionary<string, string> GetClusterHealthInfo(Uri clusterUri)
        {
            var elasticClient = new ElasticClient(new ConnectionSettings(clusterUri));
            var clusterHealthData = elasticClient.Raw.ClusterHealth().Response;

            var dictionary = new Dictionary<string, string>();
            foreach (var key in clusterHealthData.Keys)
                dictionary.Add(key.Humanize(LetterCasing.Sentence), clusterHealthData[key]);

            Thread.Sleep(3000);
            return dictionary;
        }

        public ClusterCounters GetClusterCounters(Uri clusterUri)
        {
            var elasticClient = new ElasticClient(new ConnectionSettings(clusterUri));
            var documentsCount = elasticClient.IndicesStats().Stats.Total.Documents.Count;
            var indicesCount = elasticClient.IndicesStats().Indices.Count;
            var nodesCount = elasticClient.NodesInfo().Nodes.Count;
            Thread.Sleep(3000);


            return new ClusterCounters
                {
                    Nodes = nodesCount,
                    Indices = indicesCount,
                    Documents = documentsCount
                };
        }

        public IEnumerable<NodeInfo> GetNodesInfo(Uri clusterUri)
        {
            var elasticClient = new ElasticClient(new ConnectionSettings(clusterUri));
            var nodesInfo = elasticClient.NodesInfo().Nodes.Values;
            var result = new List<NodeInfo>();
            foreach (var node in nodesInfo)
            {
                result.Add(new NodeInfo(node));
            }
            Thread.Sleep(3000);

            return result;
        }

        public bool IsAlive(Uri clusterUri)
        {
            var client = new ElasticsearchClient(new ConnectionConfiguration(clusterUri));
            var res = client.Ping();


            return res.Success;
        }
    }
}
