using Caliburn.Micro;
using ElasticOps.Com.CommonTypes;
using ElasticOps.Com.Infrastructure;
using ElasticOps.Com.Models;
using Microsoft.FSharp.Collections;

namespace ElasticOps.ViewModels.ManagmentScreens
{
    public class NodesInfoViewModel : ClusterConnectedAutorefreashScreen
    {
        public IObservableCollection<NodeInfoViewModel> NodesInfo { get; set; }
        private RequestBus requestBus;

        public NodesInfoViewModel(Settings settings, IEventAggregator eventAggregator, RequestBus requestBus)
            : base(settings.ClusterUri, eventAggregator)
        {
            NodesInfo = new BindableCollection<NodeInfoViewModel>();
            this.requestBus = requestBus;
        }

        public override void RefreshData()
        {
            var result =
                requestBus.Execute<FSharpList<NodeInfo>>(new Connection(clusterUri, new Com.CommonTypes.Version(0)));

            if (!result.Success) return;

            NodesInfo.Clear();
            foreach (var node in result.Result)
            {
                NodesInfo.Add(new NodeInfoViewModel(node));
            }

        }
    }
}
