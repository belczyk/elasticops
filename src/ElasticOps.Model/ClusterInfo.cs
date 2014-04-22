﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Elasticsearch.Net;
using Elasticsearch.Net.Connection;
using Humanizer;
using Nest;
using Newtonsoft.Json;

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

        public IEnumerable<IndexInfo> GetIndicesInfo(Uri clusterUri)
        {
            var client = new ElasticsearchClient(new ConnectionConfiguration(clusterUri));
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
