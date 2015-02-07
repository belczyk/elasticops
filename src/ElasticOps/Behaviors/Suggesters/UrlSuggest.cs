using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ElasticOps.Extensions;
using ElasticOps.Services;

namespace ElasticOps.Behaviors.Suggesters
{
    public class UrlSuggest : ObservableCollection<SuggestItem>
    {
        private readonly Infrastructure _infrastructure;
        private readonly ClusterDataCache _clusterData;

        public UrlSuggest(Infrastructure infrastructure, ClusterDataCache clusterData)
        {
            _infrastructure = infrastructure;
            _clusterData = clusterData;
        }

        public void UpdateSuggestions(string text)
        {
            if (this.Any(x => x.Text == text)) return;

            text = text.Replace('\\', '/');

            if (text.StartsWith("/"))
                text = text.Substring(1);

            var parts = text.Split('/').ToList();

            if (parts.Count() == 1)
                SuggestIndex(parts);

            if (parts.Count() == 2)
                SuggestType(parts);

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

            _infrastructure.Config.Endpoints.Indices
                .Where(x => x.StartsWith(typePrefix))
                .Select(x => new SuggestItem(string.Format("{0}/{1}", index, x), SugegestionMode.Endpoint))
                .ForEach(Add);

        }


        private void SuggestIndex(IEnumerable<string> parts)
        {
            var text = parts.First();
            Clear();
            _clusterData.Indices.Where(x => x.StartsWith(text)).Select(x => new SuggestItem(x, SugegestionMode.Index)).ForEach(Add);
        }


    }
}
