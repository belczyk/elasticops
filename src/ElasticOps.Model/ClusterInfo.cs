using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Humanizer;
using Nest;

namespace ElasticOps.Model
{
    public class ClusterInfo
    {
        public Dictionary<string, string> Load(Uri clusterUri)
        {
            var clusterHealthData = new ElasticClient(new ConnectionSettings(clusterUri)).Raw.ClusterHealth().Response;
            var dictionary = clusterHealthData.Keys.Select(key => new KeyValuePair<string, string>(
                key.Humanize(LetterCasing.Sentence),
                clusterHealthData[key])).ToDictionary(x => x.Key, x => x.Value);
            Thread.Sleep(3000);
            return dictionary;
        }
    }
}
