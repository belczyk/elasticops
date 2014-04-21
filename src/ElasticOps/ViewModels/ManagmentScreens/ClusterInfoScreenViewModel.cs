using System;
using System;
using Autofac;
using Caliburn.Micro;
using ElasticOps.Attributes;
using ElasticOps.Events;
using ElasticOps.Model;

namespace ElasticOps.ViewModels.ManagmentScreens
{
    [Priority(1)]
    public class ClusterInfoScreenViewModel : Conductor<object>.Collection.OneActive, IManagmentScreen
    {
        private IEventAggregator eventAggregator;
        private Settings settings;

        public ClusterInfoScreenViewModel(Settings settings,IEventAggregator eventAggregator, BasicInfoViewModel basicInfoViewModel, NodesInfoViewModel nodesInfoViewModel)
        {
            this.eventAggregator = eventAggregator;
            this.settings = settings;

            BasicInfoViewModel = basicInfoViewModel;
            NodesInfoViewModel = nodesInfoViewModel;

            BasicInfoViewModel.ConductWith(this);
            NodesInfoViewModel.ConductWith(this);
            ClusterCounters = new ClusterInfo().GetClusterCounters(settings.ClusterUri);
            ActivateItem(BasicInfoViewModel);
            DisplayName = "Cluster";

        }
        private BasicInfoViewModel BasicInfoViewModel { get; set; }
        private NodesInfoViewModel NodesInfoViewModel { get; set; }

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
            //ActivateItem(new BasicInfoViewModel());
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
            ClusterCounters = new ClusterInfo().GetClusterCounters(settings.ClusterUri);
        }
    }
}
