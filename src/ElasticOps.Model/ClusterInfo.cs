using System;
using System.Collections.Generic;
using System.Linq;
using Humanizer;
using Nest;

namespace ElasticOps.Model
{
    public class ClusterInfo
    {
        public IEnumerable<KeyValuePair<string, string>> Load(Uri clusterUri)
        {
            var clusterHealthData = new ElasticClient(new ConnectionSettings(clusterUri)).Raw.ClusterHealth().Response;
            return clusterHealthData.Keys.Select(key => new KeyValuePair<string, string>(
                                                 key.Humanize(LetterCasing.Sentence),
                                                 clusterHealthData[key]));
        }
    }
}
