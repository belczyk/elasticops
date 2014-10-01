using Caliburn.Micro;
using ElasticOps.Attributes;
using ElasticOps.Com.Models;

namespace ElasticOps.ViewModels.ManagmentScreens
{
    [Priority(1)]
    public class ClusterInfoScreenViewModel : Conductor<object>, IManagmentScreen
    {

        public ClusterInfoScreenViewModel(
            BasicInfoViewModel basicInfoViewModel, 
            NodesInfoViewModel nodesInfoViewModel,
            IndicesInfoViewModel indicesInfoViewModel,
            DocumentsInfoViewModel documentsInfoViewModel,
            MappingsInfoViewModel mappingsInfoViewModel,
            Infrastructure infrastructure
           )
            //: base(infrastructure)
        {

            BasicInfoViewModel = basicInfoViewModel;
            NodesInfoViewModel = nodesInfoViewModel;
            IndicesInfoViewModel = indicesInfoViewModel;
            DocumentsInfoViewModel = documentsInfoViewModel;
            MappingsInfoViewModel = mappingsInfoViewModel;
            
            ActivateItem(BasicInfoViewModel);

            //var result = commandBus.Execute(new ClusterInfo.ClusterCountersCommand(connection));
            //if (result.Success)
            //    ClusterCounters = result.Result;


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

        //public override void RefreshData()
        //{
        //    var result = commandBus.Execute(new ClusterInfo.ClusterCountersCommand(connection));

        //    if (result.Failed) return;

        //    ClusterCounters = result.Result;
        //}

        protected override void OnDeactivate(bool close)
        {
            DeactivateChildren(close);
            //eventAggregator.Unsubscribe(this);
            base.OnDeactivate(close);
        }

        private void DeactivateChildren(bool close)
        {
            BasicInfoViewModel.Deactivate(close);
            NodesInfoViewModel.Deactivate(close);
            IndicesInfoViewModel.Deactivate(close);
            DocumentsInfoViewModel.Deactivate(close);
            MappingsInfoViewModel.Deactivate(close);
            
        }
    }
}
