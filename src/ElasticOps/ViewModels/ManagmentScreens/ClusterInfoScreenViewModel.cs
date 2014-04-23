using System;
using System;
using Autofac;
using Caliburn.Micro;
using ElasticOps.Attributes;
using ElasticOps.Events;
using ElasticOps.Model;
using NLog;
using LogManager = NLog.LogManager;

namespace ElasticOps.ViewModels.ManagmentScreens
{
    [Priority(1)]
    public class ClusterInfoScreenViewModel : Conductor<object>.Collection.OneActive, IManagmentScreen
    {
        private IEventAggregator eventAggregator;
        private Settings settings;
        private ClusterInfo clusterInfo;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public ClusterInfoScreenViewModel(Settings settings,
            IEventAggregator eventAggregator, 
            BasicInfoViewModel basicInfoViewModel, 
            NodesInfoViewModel nodesInfoViewModel,
            IndicesInfoViewModel indicesInfoViewModel,
            ClusterInfo clusterInfo )
        {
            this.eventAggregator = eventAggregator;
            this.settings = settings;
            this.clusterInfo = clusterInfo;

            BasicInfoViewModel = basicInfoViewModel;
            NodesInfoViewModel = nodesInfoViewModel;
            IndicesInfoViewModel = indicesInfoViewModel;

            BasicInfoViewModel.ConductWith(this);
            NodesInfoViewModel.ConductWith(this);
            IndicesInfoViewModel.ConductWith(this);

            try
            {
                ClusterCounters = clusterInfo.GetClusterCounters(settings.ClusterUri);
            }
            catch (Exception ex)
            {
                logger.Warn(ex);
            }

            ActivateItem(BasicInfoViewModel);
            DisplayName = "Cluster";

        }

        private BasicInfoViewModel BasicInfoViewModel { get; set; }
        private NodesInfoViewModel NodesInfoViewModel { get; set; }
        private IndicesInfoViewModel IndicesInfoViewModel { get; set; }

        public void ShowBasicInfo()
        {
            ActivateItem(BasicInfoViewModel);
        }

        public void ShowNodesInfo()
        {
            ActivateItem(NodesInfoViewModel);
        }

        public void ShowIndicesInfo()
        {
            ActivateItem(IndicesInfoViewModel);
        }

        private ClusterCounters _clusterCounters;

        public ClusterCounters ClusterCounters
        {
            get { return _clusterCounters; }
            set
            {
                _clusterCounters = value;
                NotifyOfPropertyChange(() => ClusterCounters);
            }
        }

        public void Handle(RefreashEvent message)
        {
            try
            {
                ClusterCounters = clusterInfo.GetClusterCounters(settings.ClusterUri);
            }
            catch (Exception ex)
            {
                logger.Warn(ex);
            }
        }
    }
}
