using System;
using Autofac;
using Caliburn.Micro;

namespace ElasticOps.ViewModels.ManagmentScreens
{
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

    }
}
