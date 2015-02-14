using System;
using Caliburn.Micro;
using ElasticOps.Attributes;
using ElasticOps.Commands;
using ElasticOps.Events;
using Action = System.Action;

namespace ElasticOps.ViewModels.ManagementScreens
{
    [Priority(0)]
    internal class ClusterInfoScreenViewModel : Conductor<object>, IManagementScreen, IHandle<RefreshEvent>
    {
        private readonly Infrastructure _infrastructure;
        private ClusterCounters _clusterCounters;
        private bool _isClusterSelected;
        private bool _isNodesSelected;
        private bool _isIndicesSelected;
        private bool _isMappingsSelected;
        private bool _isDocumentsSelected;

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
            IsClusterSelected = true;
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

        public bool IsClusterSelected
        {
            get { return _isClusterSelected; }
            set
            {
                if (value.Equals(_isClusterSelected)) return;
                _isClusterSelected = value;
                NotifyOfPropertyChange(() => IsClusterSelected);
            }
        }

        public bool IsNodesSelected
        {
            get { return _isNodesSelected; }
            set
            {
                if (value.Equals(_isNodesSelected)) return;
                _isNodesSelected = value;
                NotifyOfPropertyChange(() => IsNodesSelected);
            }
        }

        public bool IsIndicesSelected
        {
            get { return _isIndicesSelected; }
            set
            {
                if (value.Equals(_isIndicesSelected)) return;
                _isIndicesSelected = value;
                NotifyOfPropertyChange(() => IsIndicesSelected);
            }
        }

        public bool IsMappingsSelected
        {
            get { return _isMappingsSelected; }
            set
            {
                if (value.Equals(_isMappingsSelected)) return;
                _isMappingsSelected = value;
                NotifyOfPropertyChange(() => IsMappingsSelected);
            }
        }

        public bool IsDocumentsSelected
        {
            get { return _isDocumentsSelected; }
            set
            {
                if (value.Equals(_isDocumentsSelected)) return;
                _isDocumentsSelected = value;
                NotifyOfPropertyChange(() => IsDocumentsSelected);
            }
        }

        public void Handle(RefreshEvent message)
        {
            LoadCounters();
        }

        public void ShowBasicInfo()
        {
            ActivateItem(BasicInfoViewModel);
            UpdateSelected(() => IsClusterSelected = true);
        }

        public void ShowNodesInfo()
        {
            ActivateItem(NodesInfoViewModel);
            UpdateSelected(() => IsNodesSelected = true);
        }

        public void ShowIndicesInfo()
        {
            ActivateItem(IndicesInfoViewModel);
            UpdateSelected(() => IsIndicesSelected = true);
        }

        public void ShowDocumentsInfo()
        {
            ActivateItem(DocumentsInfoViewModel);
            UpdateSelected(() => IsDocumentsSelected = true);
        }

        public void ShowMappingsInfo()
        {
            ActivateItem(MappingsInfoViewModel);
            UpdateSelected(() => IsMappingsSelected = true);
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
        private void UpdateSelected(Action selected)
        {
            IsClusterSelected = false;
            IsNodesSelected = false;
            IsIndicesSelected = false;
            IsDocumentsSelected = false;
            IsMappingsSelected = false;

            selected();
        }
    }
}