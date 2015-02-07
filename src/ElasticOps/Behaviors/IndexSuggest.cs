using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Caliburn.Micro;
using ElasticOps.Com;
using ElasticOps.Extensions;

namespace ElasticOps.Behaviors
{
    public class IndexSuggest : ObservableCollection<SuggestItem>, IHandle<RefreashEvent>, IHandle<NewConnectionEvent>
    {
        private readonly Infrastructure _infrastructure;
        private readonly List<string> _indices = new List<string>();
        public IndexSuggest(Infrastructure infrastructure)
        {
            _infrastructure = infrastructure;
            _infrastructure.EventAggregator.Subscribe(this);

            if(_infrastructure.Connection.IsConnected)
                RefreashIndexList();
        }

        public void UpdateSuggestions(string text)
        {
            if (this.Any(x => x.Text == text)) return;

            if (string.IsNullOrEmpty(text))
            {
                Clear();
                _indices.Select(x => new SuggestItem(x, SugegestionMode.Index)).ForEach(Add);
                return;
            }

            text = text.Replace('\\', '/');

            if (text.StartsWith("/"))
                text = text.Substring(1);

            Clear();
            _indices.Where(x => x.StartsWith(text)).Select(x => new SuggestItem(x, SugegestionMode.Index)).ForEach(Add);

        }

        public void Handle(RefreashEvent message)
        {
            RefreashIndexList();
        }


        public void Handle(NewConnectionEvent message)
        {
            RefreashIndexList();
        }

        private void RefreashIndexList()
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
            });
            task.Start();
        }
    }
}
