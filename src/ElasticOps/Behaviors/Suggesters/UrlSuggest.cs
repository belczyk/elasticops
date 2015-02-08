using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ElasticOps.Configuration;
using ElasticOps.Extensions;
using ElasticOps.Services;

namespace ElasticOps.Behaviors.Suggesters
{
    public class UrlSuggest : ObservableCollection<SuggestItem>
    {
        private readonly ElasticOpsConfig _config;
        private readonly ClusterDataCache _clusterData;

        public UrlSuggest(ElasticOpsConfig config, ClusterDataCache clusterData)
        {
            _config = config;
            _clusterData = clusterData;
        }

        public void UpdateSuggestions(string text)
        {
            if (this.Any(x => x.Text == text)) return;

            text = text.Replace('\\', '/');

            if (text.StartsWith("/"))
                text = text.Substring(1);

            var parts = text.Split('/').ToList();

            if (parts.Count() < 2)
                SuggestIndex(parts);

            if (parts.Count() == 2)
                SuggestType(parts);

            if (parts.Count() == 3)
                SuggestTypeEndpoint(parts);
        }

        private void SuggestTypeEndpoint(List<string> parts)
        {
            var prefix = parts[2];

            Clear();
            _config.URLSuggest.Endpoints.Type
                .Where(x => x.StartsWith(prefix))
                .Select(x => new SuggestItem(String.Format("{0}/{1}/{2}",parts[0],parts[1],x), SugegestionMode.Endpoint))
                .ForEach(Add);
        }

        private void SuggestType(IEnumerable<string> parts)
        {
            var index = parts.First().ToLower();
            var typePrefix = parts.ElementAt(1);

            if (!_clusterData.Types.ContainsKey(index))
            {
                _clusterData.UpdateTypes(index);
            }

            if (!_clusterData.Types.ContainsKey(index)) return;

            Clear();
            _clusterData.Types[index]
                .Types
                .Where(x => x.StartsWith(typePrefix))
                .Select(type => new SuggestItem(string.Format("{0}/{1}", index, type), SugegestionMode.Type))
                .ForEach(Add);

            _config.URLSuggest.Endpoints.Indices
                .Where(x => x.StartsWith(typePrefix))
                .Select(x => new SuggestItem(string.Format("{0}/{1}", index, x), SugegestionMode.Endpoint))
                .ForEach(Add);

        }

        private void SuggestIndex(IEnumerable<string> parts)
        {
            var text = parts.First();
            Clear();
            _clusterData.Indices
                .Where(x=> _config.URLSuggest.IncludeMarvelIndices || !x.ToLower().StartsWith(".marvel"))
                .Where(x => x.StartsWith(text)).Select(x => new SuggestItem(x, SugegestionMode.Index)).ForEach(Add);

            _config.URLSuggest.Endpoints.Cluster
                .Where(x => x.StartsWith(text))
                .Select(x => new SuggestItem(x, SugegestionMode.Endpoint))
                .ForEach(Add);
        }


    }
}
