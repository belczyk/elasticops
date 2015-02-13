using System.Linq;
using Caliburn.Micro;
using ElasticOps.Commands;
using ElasticOps.Extensions;

namespace ElasticOps.ViewModels
{
    public class TypesListViewModel : PropertyChangedBase
    {
        private readonly Infrastructure _infrastructure;

        private IObservableCollection<string> _allIndices;
        private string _selectedIndex;
        private string _selectedType;
        private bool _showMarvelIndices;
        private IObservableCollection<string> _typesForSelectedIndex;

        public TypesListViewModel(Infrastructure infrastructure)
        {
            _infrastructure = infrastructure;
            AllIndices = new BindableCollection<string>();
            TypesForSelectedIndex = new BindableCollection<string>();
        }


        public IObservableCollection<string> AllIndices
        {
            get { return _allIndices; }
            private set
            {
                _allIndices = value;
                NotifyOfPropertyChange(() => AllIndices);
            }
        }

        public IObservableCollection<string> TypesForSelectedIndex
        {
            get { return _typesForSelectedIndex; }
            private set
            {
                _typesForSelectedIndex = value;
                NotifyOfPropertyChange(() => TypesForSelectedIndex);
            }
        }

        public string SelectedIndex
        {
            get { return _selectedIndex; }
            set
            {
                _selectedIndex = value;
                NotifyOfPropertyChange(() => SelectedIndex);
                ReloadTypes();
            }
        }

        public string SelectedType
        {
            get { return _selectedType; }
            set
            {
                _selectedType = value;
                NotifyOfPropertyChange(() => SelectedType);
            }
        }

        public bool ShowMarvelIndices
        {
            get { return _showMarvelIndices; }
            set
            {
                _showMarvelIndices = value;
                NotifyOfPropertyChange(() => ShowMarvelIndices);
                RefreshData();
            }
        }

        public bool IsRefreshing { get; set; }

        public void RefreshData()
        {
            IsRefreshing = true;

            var indices =
                _infrastructure.CommandBus.Execute(new ClusterInfo.ListIndicesCommand(_infrastructure.Connection));
            var selectedIndex = SelectedIndex;
            AllIndices.Clear();
            indices.Result.OrderBy(x => x)
                .Where(x => !x.StartsWithIgnoreCase(Predef.MarvelIndexPrefix) || _showMarvelIndices)
                .ToList()
                .ForEach(AllIndices.Add);
            if (AllIndices.Contains(selectedIndex))
            {
                SelectedIndex = selectedIndex;
            }
            else if (AllIndices.Any())
            {
                SelectedIndex = AllIndices.First();
            }
            IsRefreshing = false;
        }

        private void ReloadTypes()
        {
            if (string.IsNullOrEmpty(SelectedIndex)) return;
            IsRefreshing = true;

            var result = _infrastructure.CommandBus.Execute(new ClusterInfo.ListTypesCommand(_infrastructure.Connection,
                SelectedIndex));

            var selectedType = SelectedType;
            TypesForSelectedIndex.Clear();
            result.Result.OrderBy(x => x).ToList().ForEach(TypesForSelectedIndex.Add);

            if (TypesForSelectedIndex.Contains(selectedType))
            {
                SelectedType = selectedType;
            }
            else if (TypesForSelectedIndex.Any())
            {
                SelectedType = TypesForSelectedIndex.First();
            }
            IsRefreshing = false;
        }
    }
}