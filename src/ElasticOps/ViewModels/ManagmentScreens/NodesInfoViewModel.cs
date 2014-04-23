using System;
using Caliburn.Micro;
using ElasticOps.Model;
using NLog;
using LogManager = NLog.LogManager;

namespace ElasticOps.ViewModels.ManagmentScreens
{
    public class NodesInfoViewModel : ClusterConnectedAutorefreashScreen
    {
        public IObservableCollection<NodeInfoViewModel> NodesInfo { get; set; }
        private ClusterInfo clusterInfo;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public NodesInfoViewModel(Settings settings, IEventAggregator eventAggregator, ClusterInfo clusterInfo)
            : base(settings.ClusterUri, eventAggregator)
        {
            NodesInfo = new BindableCollection<NodeInfoViewModel>();
            this.clusterInfo = clusterInfo;
        }

        public override void RefreshData()
        {
            try
            {
                var nodesInfo = clusterInfo.GetNodesInfo(clusterUri);
                NodesInfo.Clear();
                foreach (var node in nodesInfo)
                {
                    NodesInfo.Add(new NodeInfoViewModel(node));
                }
            }
            catch (Exception ex)
            {
                logger.Warn(ex);
            }
        }
    }
}
