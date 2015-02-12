using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Caliburn.Micro;
using ElasticOps.Com;

namespace ElasticOps.Services
{
    public class ClusterDataCache : IHandle<NewConnectionEvent>, IHandle<RefreashEvent>
    {
        private const string _marvel = ".marvel";
        private readonly Infrastructure _infrastructure;

        public ClusterDataCache(Infrastructure infrastructure)
        {
            _infrastructure = infrastructure;
            _infrastructure.EventAggregator.Subscribe(this);

            _indices = new List<string>();
            _indexData = new Dictionary<string, Index>();

            if (_infrastructure.Connection.IsConnected)
                RefreashData();
        }

        private readonly List<string> _indices = new List<string>();
        private readonly Dictionary<string, Index> _indexData = new Dictionary<string, Index>();

        public IEnumerable<string> Indices { get { return _indices; } }
        public IDictionary<string, Index> IndexData { get { return _indexData; } }

        public void Handle(RefreashEvent message)
        {
            RefreashData();
        }

        public void Handle(NewConnectionEvent message)
        {
            RefreashData();
        }

        public void UpdateTypes(string index)
        {
            if (!_indices.Contains(index)) return;

            if (_indexData.ContainsKey(index) && DateTime.Now - _indexData[index].LastUpdated < TimeSpan.FromSeconds(5)) return;

            var result = _infrastructure.CommandBus.Execute(new ClusterInfo.ListTypesCommand(_infrastructure.Connection, index));
            if (result.Success)
            {
                _indexData[index] = new Index(result.Result);
            }
        }

        private void RefreashData()
        {
            Parallel.Invoke(() =>
            {
                var result =
                    _infrastructure.CommandBus.Execute(new ClusterInfo.ListIndicesCommand(_infrastructure.Connection));
                if (result.Success)
                {
                    _indices.Clear();
                    var marvelIndices = result.Result.Where(x => x.StartsWith(_marvel)).OrderBy(x => x);
                    var otherIndices = result.Result.Where(x => !x.StartsWith(_marvel)).OrderBy(x => x);
                    _indices.AddRange(otherIndices.Union(marvelIndices));
                }

                _indices.Intersect(_indices.Where(x => !x.StartsWith(_marvel))).ToList()
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
