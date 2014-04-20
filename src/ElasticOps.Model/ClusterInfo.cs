using System;
using System.Collections.Generic;
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

        public GeneralClusterInfo GetTabsData(Uri clusterUri)
        {
            var elasticClient = new ElasticClient(new ConnectionSettings(clusterUri));
            var documentsCount = elasticClient.IndicesStats().Stats.Total.Documents.Count;
            var indicesCount = elasticClient.IndicesStats().Indices.Count;
            var nodesCount = elasticClient.NodesInfo().Nodes.Count;
            return new GeneralClusterInfo
                {
                    NodesDisplayInfo = Convert.ToString(nodesCount),
                    IndicesDisplayInfo = Convert.ToString(indicesCount),
                    DocumentsDisplayInfo = documentsCount.HumanizeNumber()
                };
        }
    }
}
