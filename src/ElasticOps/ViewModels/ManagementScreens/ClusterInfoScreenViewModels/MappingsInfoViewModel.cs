using System.Windows;
using ElasticOps.Commands;

namespace ElasticOps.ViewModels.ManagementScreens
{
    internal class MappingsInfoViewModel : ClusterConnectedAutoRefreshScreen
    {
        private readonly Infrastructure _infrastructure;

        private string _mapping;

        public MappingsInfoViewModel(TypesListViewModel typesListViewModel, Infrastructure infrastructure)
            : base(infrastructure)
        {
            _infrastructure = infrastructure;
            TypesList = typesListViewModel;
            TypesList.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName != "SelectedType") return;

                LoadMapping();
            };
        }

        public TypesListViewModel TypesList { get; set; }

        public string Mapping
        {
            get { return _mapping; }
            set
            {
                _mapping = value;
                NotifyOfPropertyChange(() => Mapping);
            }
        }

        public override void RefreshData()
        {
            TypesList.RefreshData();
        }

        private void LoadMapping()
        {
            if (string.IsNullOrEmpty(TypesList.SelectedIndex) || string.IsNullOrEmpty(TypesList.SelectedType)) return;

            var res = _infrastructure.CommandBus.Execute(new ClusterInfo.GetMappingCommand(_infrastructure.Connection,
                TypesList.SelectedIndex, TypesList.SelectedType));

            if (res.Success) Mapping = res.Result;
        }

        public void CopyToCliboard()
        {
            Clipboard.SetText(Mapping);
        }
    }
}