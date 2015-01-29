﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Caliburn.Micro;
using ElasticOps.Com;
using ElasticOps.Extensions;

namespace ElasticOps.Behaviors
{
    public class UrlSuggest : ObservableCollection<SuggestItem>, IHandle<RefreashEvent>
    {
        private readonly Infrastructure _infrastructure;
        private readonly List<string> _indices = new List<string>();
        private readonly Dictionary<string, List<string>> _types = new Dictionary<string, List<string>>();
        public UrlSuggest(Infrastructure infrastructure)
        {
            _infrastructure = infrastructure;
            _infrastructure.EventAggregator.Subscribe(this);
        }

        public void UpdateSuggestions(string text)
        {
            if (this.Any(x => x.Text == text)) return;

            text = text.Replace('\\', '/');

            var parts = text.Split('/').ToList();

            if (parts.Count() == 1)
                SuggestIndex(parts);

            if (parts.Count() == 2)
                SuggestType(parts);

        }

        private void SuggestType(IEnumerable<string> parts)
        {
            var index = parts.First();
            var typePrefix = parts.ElementAt(1);

            if (!_types.ContainsKey(index.ToLower()))
            {
                UpdateTypes(index);
            }

            if (!_types.ContainsKey(index.ToLower())) return;

            Clear();
            _types[index.ToLower()]
                .Where(x => x.StartsWith(typePrefix))
                .Select(type => new SuggestItem(string.Format("{0}/{1}", index, type), SugegestionMode.Type))
                .ForEach(Add);

            _infrastructure.Config.Endpoints.Indices
                .Where(x => x.StartsWith(typePrefix))
                .Select(x => new SuggestItem(string.Format("{0}/{1}", index, x), SugegestionMode.Endpoint))
                .ForEach(Add);

        }

        private void UpdateTypes(string index)
        {
            var result = _infrastructure.CommandBus.Execute(new ClusterInfo.ListTypesCommand(_infrastructure.Connection, index));
            if (result.Success)
            {
                _types[index] = result.Result;
            }
        }

        private void SuggestIndex(IEnumerable<string> parts)
        {
            var text = parts.First();
            Clear();
            _indices.Where(x => x.StartsWith(text)).Select(x => new SuggestItem(x, SugegestionMode.Index)).ForEach(Add);
        }

        public void Handle(RefreashEvent message)
        {
            var task = new Task(() =>
            {
                var result =
                    _infrastructure.CommandBus.Execute(new ClusterInfo.ListIndicesCommand(_infrastructure.Connection));
                if (result.Success)
                {
                    _indices.Clear();
                    _indices.AddRange(result.Result);
                }

                _types.Keys.Intersect(_indices.Where(x => !x.StartsWith(".marvel"))).ToList().ForEach(UpdateTypes);
            });
            task.Start();
        }
    }
}