using System.Collections.ObjectModel;
using Caliburn.Micro;
using MahApps.Metro.Behaviours;


namespace ElasticOps.ViewModels
{
    public class TypesListViewModel : PropertyChangedBase
    {
        private readonly Infrastructure _infrastructure;

        public TypesListViewModel(Infrastructure infrastructure)
        {
            _infrastructure = infrastructure;
            AllIndices = new ObservableCollection<string>();
            TypesForSelectedIndex = new ObservableCollection<string>();
        }
        private ObservableCollection<string> _allIndices;
        private ObservableCollection<string> _typesForSelectedIndex;
        private string _selectedIndex;
        private string _selectedType;

        public ObservableCollection<string> AllIndices
        {
            get { return _allIndices; }
            set
            {
                _allIndices = value;
                NotifyOfPropertyChange(()=>AllIndices);
            }
        }

        public ObservableCollection<string> TypesForSelectedIndex
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
            if (string.IsNullOrEmpty(SelectedType)) return;


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
    }
}
