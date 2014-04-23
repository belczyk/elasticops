using System;
using System.Collections.Generic;
using Elasticsearch.Net;
using Elasticsearch.Net.Connection;
using Humanizer;
using Nest;
using Newtonsoft.Json;

namespace ElasticOps.Model
{
    public class ClusterInfo
    {
        private ElasticClientProvider elasticClientProvider;

        public ClusterInfo(ElasticClientProvider elasticClientProvider)
        {
            this.elasticClientProvider = elasticClientProvider;
        }

        public IDictionary<string, string> GetClusterHealthInfo(Uri clusterUri)
        {
            var elasticClient = elasticClientProvider.GetElasticClient(clusterUri);

            var clusterHealthData = elasticClient.Raw.ClusterHealth().Response;

            var dictionary = new Dictionary<string, string>();
            foreach (var key in clusterHealthData.Keys)
                dictionary.Add(key.Humanize(LetterCasing.Sentence), clusterHealthData[key]);

            return dictionary;
        }

        public ClusterCounters GetClusterCounters(Uri clusterUri)
        {
            var elasticClient = elasticClientProvider.GetElasticClient(clusterUri);

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
            var elasticClient = elasticClientProvider.GetElasticClient(clusterUri);

            var nodesInfo = elasticClient.NodesInfo().Nodes.Values;
            var result = new List<NodeInfo>();
            foreach (var node in nodesInfo)
            {
                result.Add(new NodeInfo(node));
            }

            return result;
        }

        public bool IsAlive(Uri clusterUri)
        {
            try
            {
                var client = elasticClientProvider.GetElasticClient(clusterUri);

                var res = client.ClusterHealth();

                return res.ConnectionStatus.Success;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public IEnumerable<IndexInfo> GetIndicesInfo(Uri clusterUri)
        {
            var client = elasticClientProvider.GetElasticNetClient(clusterUri);

            var state = client.ClusterState();
            var indices = state.Response["metadata"]["indices"];
            var ret = new List<IndexInfo>();
            foreach (var index in indices.Value)
            {
                ret.Add(new IndexInfo
                {
                    Name = index.Key,
                    Types = GetTypes(index.Value["mappings"]),
                    Settings = GetSettings(index.Value["settings"]),
                    State = index.Value["state"].ToString()
                });
            }

            return ret;
        }

        private Dictionary<string, string> GetSettings(dynamic settings)
        {
            var ret = new Dictionary<string, string>();

            foreach (var setting in settings)
            {
                ret.Add(setting.Key,setting.Value.ToString());
            }

            return ret;
        }

        private List<ESTypeInfo> GetTypes(dynamic types)
        {
            var ret = new List<ESTypeInfo>();
            foreach (var type in types)
            {
                ret.Add(new ESTypeInfo
                {
                    Name = type.Key,
                    Mapping = JsonConvert.SerializeObject(type.Value,Formatting.Indented)
                });
            }

            return ret;
        }
    }
}
