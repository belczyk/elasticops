using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Caliburn.Micro;
using ElasticOps.Com;
using ElasticOps.Extensions;

namespace ElasticOps.Behaviors 
{
    public class IndexSuggest : ObservableCollection<string>, IHandle<RefreashEvent>
    {
        private readonly Infrastructure _infrastructure;
        private readonly Action<string> _onTextChanged;
        private readonly List<string> _indices = new List<string>();
        public IndexSuggest(Infrastructure infrastructure)
        {
            _infrastructure = infrastructure;
            _infrastructure.EventAggregator.Subscribe(this);
        }

        public void UpdateSuggestions(string text)
        {
            Clear();
            _indices.Where(x => x.StartsWith(text)).ForEach(Add);

        }

        public void Handle(RefreashEvent message)
        {
            var result =
                _infrastructure.CommandBus.Execute(new ClusterInfo.ListIndicesCommand(_infrastructure.Settings.Connection));
            if (result.Success)
            {
                _indices.Clear();
                _indices.AddRange(result.Result);
            }
        }
    }
}
