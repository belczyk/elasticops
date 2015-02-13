using Caliburn.Micro;
using ElasticOps.Attributes;
using ElasticOps.Com;
using ElasticOps.Events;

namespace ElasticOps.ViewModels.ManagementScreens
{
    [Priority(0)]
    internal class ClusterInfoScreenViewModel : Conductor<object>, IManagementScreen, IHandle<RefreshEvent>
    {
        private readonly Infrastructure _infrastructure;
        private ClusterCounters _clusterCounters;

        public ClusterInfoScreenViewModel(
            BasicInfoViewModel basicInfoViewModel,
            NodesInfoViewModel nodesInfoViewModel,
            IndicesInfoViewModel indicesInfoViewModel,
            DocumentsInfoViewModel documentsInfoViewModel,
            MappingsInfoViewModel mappingsInfoViewModel,
            Infrastructure infrastructure
            )
        {
            _infrastructure = infrastructure;
            _infrastructure.EventAggregator.Subscribe(this);
            BasicInfoViewModel = basicInfoViewModel;
            NodesInfoViewModel = nodesInfoViewModel;
            IndicesInfoViewModel = indicesInfoViewModel;
            DocumentsInfoViewModel = documentsInfoViewModel;
            MappingsInfoViewModel = mappingsInfoViewModel;

            base.ActivateItem(BasicInfoViewModel);
            base.DisplayName = "Cluster";
        }

        private BasicInfoViewModel BasicInfoViewModel { get; set; }
        private NodesInfoViewModel NodesInfoViewModel { get; set; }
        private IndicesInfoViewModel IndicesInfoViewModel { get; set; }
        private DocumentsInfoViewModel DocumentsInfoViewModel { get; set; }
        private MappingsInfoViewModel MappingsInfoViewModel { get; set; }

        public ClusterCounters ClusterCounters
        {
            get { return _clusterCounters; }
            set
            {
                _clusterCounters = value;
                NotifyOfPropertyChange(() => ClusterCounters);
            }
        }

        public void Handle(RefreshEvent message)
        {
            LoadCounters();
        }

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

        protected override void OnDeactivate(bool close)
        {
            DeactivateChildren(close);
            _infrastructure.EventAggregator.Unsubscribe(this);
            base.OnDeactivate(close);
        }

        protected override void OnActivate()
        {
            LoadCounters();

            base.OnActivate();
        }

        private void DeactivateChildren(bool close)
        {
            BasicInfoViewModel.Deactivate(close);
            NodesInfoViewModel.Deactivate(close);
            IndicesInfoViewModel.Deactivate(close);
            DocumentsInfoViewModel.Deactivate(close);
            MappingsInfoViewModel.Deactivate(close);
        }

        private void LoadCounters()
        {
            var result =
                _infrastructure.CommandBus.Execute(new ClusterInfo.ClusterCountersCommand(_infrastructure.Connection));

            if (result.Failed) return;

            ClusterCounters = result.Result;
        }
    }
}