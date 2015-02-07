using System.Collections.ObjectModel;
using System.Linq;
using ElasticOps.Extensions;
using ElasticOps.Services;

namespace ElasticOps.Behaviors.Suggesters
{
    public class IndexSuggest : ObservableCollection<SuggestItem>
    {
        private readonly ClusterDataCache _clusterData;
        public IndexSuggest(ClusterDataCache clusterData)
        {
            _clusterData = clusterData;
        }

        public void UpdateSuggestions(string text)
        {
            if (this.Any(x => x.Text == text)) return;

            if (string.IsNullOrEmpty(text))
            {
                Clear();
                _clusterData.Indices.Select(x => new SuggestItem(x, SugegestionMode.Index)).ForEach(Add);
                return;
            }

            text = text.Replace('\\', '/');

            if (text.StartsWith("/"))
                text = text.Substring(1);

            Clear();
            _clusterData.Indices.Where(x => x.StartsWith(text)).Select(x => new SuggestItem(x, SugegestionMode.Index)).ForEach(Add);
        }

    }
}
