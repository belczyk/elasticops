using System;
using Caliburn.Micro;
using ElasticOps.Attributes;
using ElasticOps.Com.Models;
using NLog;
using LogManager = NLog.LogManager;

namespace ElasticOps.ViewModels.ManagmentScreens
{
    [Priority(1)]
    public class ClusterInfoScreenViewModel : ClusterConnectedAutorefreashScreen, IManagmentScreen
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public ClusterInfoScreenViewModel(Settings settings,
            IEventAggregator eventAggregator, 
            BasicInfoViewModel basicInfoViewModel, 
            NodesInfoViewModel nodesInfoViewModel,
            IndicesInfoViewModel indicesInfoViewModel
           )
            : base(settings.ClusterUri,eventAggregator)
        {
            this.eventAggregator = eventAggregator;

            BasicInfoViewModel = basicInfoViewModel;
            NodesInfoViewModel = nodesInfoViewModel;
            IndicesInfoViewModel = indicesInfoViewModel;

            BasicInfoViewModel.ConductWith(this);
            NodesInfoViewModel.ConductWith(this);
            IndicesInfoViewModel.ConductWith(this);

            try
            {
                ClusterCounters = Com.ClusterInfo.ClusterCounters(settings.ClusterUri);
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

        public override void RefreshData()
        {
            try
            {
                ClusterCounters = Com.ClusterInfo.ClusterCounters(clusterUri);
            }
            catch (Exception ex)
            {
                logger.Warn(ex);
            }
        }
    }
}
