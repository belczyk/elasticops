using System.Linq;
using Caliburn.Micro;
using ElasticOps.Com;

namespace ElasticOps.ViewModels
{
    public class TypesListViewModel : PropertyChangedBase
    {
        private readonly Infrastructure _infrastructure;
        private readonly Connection _connection;

        public TypesListViewModel(Infrastructure infrastructure)
        {
            _infrastructure = infrastructure;
            _connection = infrastructure.Connection;
            AllIndices = new BindableCollection<string>();
            TypesForSelectedIndex = new BindableCollection<string>();
        }
        private IObservableCollection<string> _allIndices;
        private IObservableCollection<string> _typesForSelectedIndex;
        private string _selectedIndex;
        private string _selectedType;


        public void RefreashData()
        {
            IsRefreshing = true;

            var indices = _infrastructure.CommandBus.Execute(new ClusterInfo.ListIndicesCommand(_infrastructure.Connection));
            var selectedIndex = SelectedIndex;
            AllIndices.Clear();
            indices.Result.OrderBy(x=>x).Where(x => !x.StartsWith(Predef.MarvelIndexPrefix) || _ShowMarvelIndices).ToList().ForEach(AllIndices.Add);
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

        public IObservableCollection<string> AllIndices
        {
            get { return _allIndices; }
            set
            {
                _allIndices = value;
                NotifyOfPropertyChange(() => AllIndices);
            }
        }

        public IObservableCollection<string> TypesForSelectedIndex
        {
            get { return _typesForSelectedIndex; }
            set
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

        private void ReloadTypes()
        {
            if (string.IsNullOrEmpty(SelectedIndex)) return;
            IsRefreshing = true;

            var result = _infrastructure.CommandBus.Execute(new ClusterInfo.ListTypesCommand(_infrastructure.Connection,
                SelectedIndex));

            var selectedType = SelectedType;
            TypesForSelectedIndex.Clear();
            result.Result.OrderBy(x=>x).ToList().ForEach(TypesForSelectedIndex.Add);

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

        public string SelectedType
        {
            get { return _selectedType; }
            set
            {
                _selectedType = value;
                NotifyOfPropertyChange(() => SelectedType);

            }
        }

        private bool _ShowMarvelIndices;

        public bool ShowMarvelIndices
        {
            get { return _ShowMarvelIndices; }
            set
            {
                _ShowMarvelIndices = value;
                NotifyOfPropertyChange(() => ShowMarvelIndices);
                RefreashData();
            }
        }

        private bool _isRefreashing;

        public bool IsRefreshing
        {
            get { return _isRefreashing; }
            set { _isRefreashing = value; }
        }
    }
}
