using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using ElasticOps.Configuration;
using ElasticOps.Extensions;
using ElasticOps.Services;

namespace ElasticOps.Behaviors.AutoComplete
{
    public class UrlAutoCompleteCollection : ObservableCollection<AutoCompleteItem>
    {
        private readonly ElasticOpsConfig _config;
        private readonly ClusterDataCache _clusterData;

        public UrlAutoCompleteCollection(ElasticOpsConfig config, ClusterDataCache clusterData)
        {
            _config = config;
            _clusterData = clusterData;
        }

        public void UpdateSuggestions(string text)
        {
            if (this.Any(x => x.Label == text)) return;

            text = text.Replace('\\', '/');

            if (text.StartsWithIgnoreCase("/"))
                text = text.Substring(1);

            var parts = text.Split('/').ToList();

            if (parts.Count() < 2)
                SuggestIndex(parts);

            if (parts.Count() == 2)
                SuggestType(parts);

            if (parts.Count() == 3)
                SuggestTypeEndpoint(parts);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization","CA1303:Do not pass literals as localized parameters",MessageId ="ElasticOps.Behaviors.Suggesters.AutocompleteItem.#ctor(System.String,ElasticOps.Behaviors.Suggesters.AutocompleteMode)")]
        private void SuggestTypeEndpoint(List<string> parts)
        {
            var prefix = parts[2];

            Clear();
            _config.URLSuggest.Endpoints.Type
                .Where(x => x.StartsWithIgnoreCase(prefix))
                .Select(
                    x =>
                        new AutoCompleteItem(
                            String.Format(CultureInfo.InvariantCulture, "{0}/{1}/{2}", parts[0], parts[1], x),
                            AutoCompleteMode.Endpoint))
                .ForEach(Add);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase"),System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization","CA1303:Do not pass literals as localized parameters",MessageId ="ElasticOps.Behaviors.Suggesters.AutocompleteItem.#ctor(System.String,ElasticOps.Behaviors.Suggesters.AutocompleteMode)")]
        private void SuggestType(IEnumerable<string> parts)
        {
            var index = parts.First().ToLower(CultureInfo.InvariantCulture);
            var typePrefix = parts.ElementAt(1);

            if (!_clusterData.IndexData.ContainsKey(index))
            {
                _clusterData.UpdateTypes(index);
            }

            if (!_clusterData.IndexData.ContainsKey(index)) return;

            Clear();
            _clusterData.IndexData[index]
                .Types
                .Where(x => x.StartsWithIgnoreCase(typePrefix))
                .Select(
                    type =>
                        new AutoCompleteItem(string.Format(CultureInfo.InvariantCulture, "{0}/{1}", index, type),
                            AutoCompleteMode.Type))
                .ForEach(Add);

            _config.URLSuggest.Endpoints.Indices
                .Where(x => x.StartsWithIgnoreCase(typePrefix))
                .Select(
                    x =>
                        new AutoCompleteItem(string.Format(CultureInfo.InvariantCulture, "{0}/{1}", index, x),
                            AutoCompleteMode.Endpoint))
                .ForEach(Add);
        }

        private void SuggestIndex(IEnumerable<string> parts)
        {
            var text = parts.First();
            Clear();
            _clusterData.Indices
                .Where(x => _config.URLSuggest.IncludeMarvelIndices || !x.StartsWithIgnoreCase(Predef.MarvelIndexPrefix))
                .Where(x => x.StartsWithIgnoreCase(text))
                .Select(x => new AutoCompleteItem(x, AutoCompleteMode.Index))
                .ForEach(Add);

            _config.URLSuggest.Endpoints.Cluster
                .Where(x => x.StartsWithIgnoreCase(text))
                .Select(x => new AutoCompleteItem(x, AutoCompleteMode.Endpoint))
                .ForEach(Add);
        }
    }
}