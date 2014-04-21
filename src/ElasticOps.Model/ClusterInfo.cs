using System;
using System.Collections.Generic;
using System.Linq;
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
            return dictionary;
        }

        public ClusterCounters GetClusterCounters(Uri clusterUri)
        {
            var elasticClient = new ElasticClient(new ConnectionSettings(clusterUri));
            var documentsCount = elasticClient.IndicesStats().Stats.Total.Documents.Count;
            var indicesCount = elasticClient.IndicesStats().Indices.Count;
            var nodesCount = elasticClient.NodesInfo().Nodes.Count;
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
            return result;
        }
    }
}
