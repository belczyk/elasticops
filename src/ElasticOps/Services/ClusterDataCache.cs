using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Caliburn.Micro;
using ElasticOps.Com;
using ElasticOps.Events;
using ElasticOps.Extensions;

namespace ElasticOps.Services
{
    public class ClusterDataCache : IHandle<NewConnectionEvent>, IHandle<RefreshEvent>
    {
        private readonly Infrastructure _infrastructure;
        private readonly List<string> _indices = new List<string>();
        private readonly Dictionary<string, Index> _indexData = new Dictionary<string, Index>();

        public ClusterDataCache(Infrastructure infrastructure)
        {
            Ensure.ArgumentNotNull(infrastructure, "infrastructure");

            _infrastructure = infrastructure;
            _infrastructure.EventAggregator.Subscribe(this);

            _indices = new List<string>();
            _indexData = new Dictionary<string, Index>();

            if (_infrastructure.Connection.IsConnected)
                RefreshData();
        }

        public IEnumerable<string> Indices
        {
            get { return _indices; }
        }

        public IDictionary<string, Index> IndexData
        {
            get { return _indexData; }
        }

        public void Handle(RefreshEvent message)
        {
            RefreshData();
        }

        public void Handle(NewConnectionEvent message)
        {
            RefreshData();
        }

        public void UpdateTypes(string index)
        {
            if (!_indices.Contains(index)) return;

            if (_indexData.ContainsKey(index) && DateTime.Now - _indexData[index].LastUpdated < TimeSpan.FromSeconds(5))
                return;

            var result =
                _infrastructure.CommandBus.Execute(new ClusterInfo.ListTypesCommand(_infrastructure.Connection, index));
            if (result.Success)
            {
                _indexData[index] = new Index(result.Result);
            }
        }

        private void RefreshData()
        {
            Parallel.Invoke(() =>
            {
                var result =
                    _infrastructure.CommandBus.Execute(new ClusterInfo.ListIndicesCommand(_infrastructure.Connection));
                if (result.Success)
                {
                    _indices.Clear();
                    var marvelIndices = result.Result.Where(x => x.StartsWithIgnoreCase(Predef.MarvelIndexPrefix)).OrderBy(x => x);
                    var otherIndices = result.Result.Where(x => !x.StartsWithIgnoreCase(Predef.MarvelIndexPrefix)).OrderBy(x => x);
                    _indices.AddRange(otherIndices.Union(marvelIndices));
                }

                _indices.Intersect(_indices.Where(x => !x.StartsWithIgnoreCase(Predef.MarvelIndexPrefix))).ToList()
                    .ForEach(i =>
                    {
                        UpdateTypes(i);
                        UpdateAnalyzers(i);
                    });
            });
        }

        private void UpdateAnalyzers(string index)
        {
            if (!_indices.Contains(index)) return;

            var result = _infrastructure.CommandBus.Execute(new Analyze.ListAnalyzers(_infrastructure.Connection, index));

            if (result.Failed) return;

            IndexData[index].Analyzers = result.Result;
        }
    }
}