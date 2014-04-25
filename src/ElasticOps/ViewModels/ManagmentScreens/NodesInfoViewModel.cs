using System;
using Caliburn.Micro;

using NLog;
using LogManager = NLog.LogManager;

namespace ElasticOps.ViewModels.ManagmentScreens
{
    public class NodesInfoViewModel : ClusterConnectedAutorefreashScreen
    {
        public IObservableCollection<NodeInfoViewModel> NodesInfo { get; set; }
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public NodesInfoViewModel(Settings settings, IEventAggregator eventAggregator)
            : base(settings.ClusterUri, eventAggregator)
        {
            NodesInfo = new BindableCollection<NodeInfoViewModel>();
        }

        public override void RefreshData()
        {
            try
            {
                var nodesInfo = Com.ClusterInfo.NodesInfo(clusterUri);
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
