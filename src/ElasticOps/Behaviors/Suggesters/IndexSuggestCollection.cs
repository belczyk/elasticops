using System.Collections.ObjectModel;
using System.Linq;
using ElasticOps.Extensions;
using ElasticOps.Services;

namespace ElasticOps.Behaviors.Suggesters
{
    public class IndexSuggestCollection : ObservableCollection<SuggestItem>
    {
        private readonly ClusterDataCache _clusterData;

        public IndexSuggestCollection(ClusterDataCache clusterData)
        {
            _clusterData = clusterData;
        }

        public void UpdateSuggestions(string text)
        {
            if (this.Any(x => x.Text == text)) return;

            if (string.IsNullOrEmpty(text))
            {
                Clear();
                _clusterData.Indices.Select(x => new SuggestItem(x, SuggestionMode.Index)).ForEach(Add);
                return;
            }

            text = text.Replace('\\', '/');

            if (text.StartsWithIgnoreCase("/"))
                text = text.Substring(1);

            Clear();
            _clusterData.Indices.Where(x => x.StartsWithIgnoreCase(text))
                .Select(x => new SuggestItem(x, SuggestionMode.Index))
                .ForEach(Add);
        }
    }
}