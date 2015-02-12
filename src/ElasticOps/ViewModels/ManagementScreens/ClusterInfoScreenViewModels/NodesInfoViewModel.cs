using System.Collections.Generic;
using System.Linq;
using Caliburn.Micro;
using ElasticOps.Com;

namespace ElasticOps.ViewModels.ManagementScreens
{
    internal class NodesInfoViewModel : ClusterConnectedAutoRefreshScreen
    {
        private IEnumerable<NodeInfoViewModel> _nodesInfo;

        public IEnumerable<NodeInfoViewModel> NodesInfo
        {
            get { return _nodesInfo; }
            set
            {
                if (Equals(value, _nodesInfo)) return;
                _nodesInfo = value;
                NotifyOfPropertyChange(() => NodesInfo);
            }
        }

        public NodesInfoViewModel(Infrastructure infrastructure)
            : base(infrastructure)
        {
            NodesInfo = new BindableCollection<NodeInfoViewModel>();
        }

        public override void RefreshData()
        {
            var result =
                commandBus.Execute(new ClusterInfo.NodesInfoCommand(connection));

            if (!result.Success) return;

            NodesInfo = result.Result.Select(node => new NodeInfoViewModel(node));
        }
    }
}