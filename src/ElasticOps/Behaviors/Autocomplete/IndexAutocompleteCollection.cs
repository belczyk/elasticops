using System.Collections.ObjectModel;
using System.Linq;
using ElasticOps.Extensions;
using ElasticOps.Services;

namespace ElasticOps.Behaviors.AutoComplete
{
    public class IndexAutoCompleteCollection : ObservableCollection<AutoCompleteItem>
    {
        private readonly ClusterDataCache _clusterData;

        public IndexAutoCompleteCollection(ClusterDataCache clusterData)
        {
            _clusterData = clusterData;
        }

        public void UpdateSuggestions(string text)
        {
            if (this.Any(x => x.Label == text)) return;

            if (string.IsNullOrEmpty(text))
            {
                Clear();
                _clusterData.Indices.Select(x => new AutoCompleteItem(x, AutoCompleteMode.Index)).ForEach(Add);
                return;
            }

            text = text.Replace('\\', '/');

            if (text.StartsWithIgnoreCase("/"))
                text = text.Substring(1);

            Clear();
            _clusterData.Indices.Where(x => x.StartsWithIgnoreCase(text))
                .Select(x => new AutoCompleteItem(x, AutoCompleteMode.Index))
                .ForEach(Add);
        }
    }
}