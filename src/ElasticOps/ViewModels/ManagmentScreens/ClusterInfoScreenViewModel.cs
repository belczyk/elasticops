using System;
using Caliburn.Micro;
using ElasticOps.Attributes;
using ElasticOps.Com;
using ElasticOps.Com.Infrastructure;
using ElasticOps.Com.Models;
using ProviderImplementation;

namespace ElasticOps.ViewModels.ManagmentScreens
{
    [Priority(1)]
    public class ClusterInfoScreenViewModel : ClusterConnectedAutorefreashScreen, IManagmentScreen
    {

        public ClusterInfoScreenViewModel(
            BasicInfoViewModel basicInfoViewModel, 
            NodesInfoViewModel nodesInfoViewModel,
            IndicesInfoViewModel indicesInfoViewModel,
            DocumentsInfoViewModel documentsInfoViewModel,
            MappingsInfoViewModel mappingsInfoViewModel,
            Infrastructure infrastructure
           )
            : base(infrastructure)
        {

            BasicInfoViewModel = basicInfoViewModel;
            NodesInfoViewModel = nodesInfoViewModel;
            IndicesInfoViewModel = indicesInfoViewModel;
            DocumentsInfoViewModel = documentsInfoViewModel;
            MappingsInfoViewModel = mappingsInfoViewModel;

            BasicInfoViewModel.ConductWith(this);
            NodesInfoViewModel.ConductWith(this);
            IndicesInfoViewModel.ConductWith(this);
            DocumentsInfoViewModel.ConductWith(this);
            MappingsInfoViewModel.ConductWith(this);

            var result = commandBus.Execute(new ClusterInfo.ClusterCountersCommand(connection));
            if (result.Success)
                ClusterCounters = result.Result;
            

            ActivateItem(BasicInfoViewModel);
            DisplayName = "Cluster";

        }

        private BasicInfoViewModel BasicInfoViewModel { get; set; }
        private NodesInfoViewModel NodesInfoViewModel { get; set; }
        private IndicesInfoViewModel IndicesInfoViewModel { get; set; }
        private DocumentsInfoViewModel DocumentsInfoViewModel { get; set; }
        private MappingsInfoViewModel MappingsInfoViewModel { get; set; }  

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

        public void ShowDocumentsInfo()
        {
            ActivateItem(DocumentsInfoViewModel);
        }

        public void ShowMappingsInfo()
        {
            ActivateItem(MappingsInfoViewModel);
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
            var result = commandBus.Execute(new ClusterInfo.ClusterCountersCommand(connection));

            if (result.Failed) return;

            ClusterCounters = result.Result;
        }
    }
}
