using System.Collections.Generic;
using Caliburn.Micro;
using System.Linq;
using ElasticOps.Com;
using ElasticOps.Com.Infrastructure;
using ElasticOps.Com.Models;
using NLog;
using LogManager = NLog.LogManager;

namespace ElasticOps.ViewModels.ManagmentScreens
{
    public class MappingsInfoViewModel : ClusterConnectedAutorefreashScreen
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private IEnumerable<Com.Models.IndexInfo> indices;

        public MappingsInfoViewModel(Infrastructure infrastructure)
            : base(infrastructure)
        {
            AllIndices = new BindableCollection<string>();
            TypesForSelectedIndex = new BindableCollection<string>();

            var result = commandBus.Execute(new ClusterInfo.IndicesInfoCommand(connection));

            if (result.Failed) return;
            indices = result.Result;
            indices = indices.OrderByDescending(index => index.Name);
            AllIndices.Clear();
            foreach (var indexInfo in indices)
            {
                AllIndices.Add(indexInfo.Name);
            }

            if (SelectedIndex == null)
            {
                SelectedIndex = AllIndices.FirstOrDefault();
            }
        }

        public override void RefreshData()
        {
            
        }

        private void DisplayMapping()
        {
            var ind = indices.SingleOrDefault(index => index.Name.Equals(SelectedIndex));
            Mapping = ind.Types.SingleOrDefault(type => type.Key.Equals((SelectedType))).Value;
        }

        private void FilterTypes()
        {
            var types = indices
                .Where(index => index.Name.Equals(SelectedIndex))
                .SelectMany(index => index.Types);
            TypesForSelectedIndex.Clear();
            foreach (var type in types)
            {
                TypesForSelectedIndex.Add(type.Key);
            }
        }

        private IObservableCollection<string> _AllIndices;

        public IObservableCollection<string> AllIndices
        {
            get { return _AllIndices; }
            set
            {
                _AllIndices = value;
                NotifyOfPropertyChange(() => AllIndices);
            }
        }

        private IObservableCollection<string> _TypesForSelectedIndex;

        public IObservableCollection<string> TypesForSelectedIndex
        {
            get { return _TypesForSelectedIndex; }
            set
            {
                _TypesForSelectedIndex = value;
                NotifyOfPropertyChange(() => TypesForSelectedIndex);
            }
        }

        private string _SelectedIndex;

        public string SelectedIndex
        {
            get { return _SelectedIndex; }
            set
            {
                _SelectedIndex = value;
                NotifyOfPropertyChange(() => SelectedIndex);
                FilterTypes();
                SelectedType = TypesForSelectedIndex.FirstOrDefault();
                DisplayMapping();
            }
        }

        private string _SelectedType;

        public string SelectedType
        {
            get { return _SelectedType; }
            set
            {
                _SelectedType = value;
                NotifyOfPropertyChange(() => SelectedType);
                DisplayMapping();
            }
        }

        private string _Mapping;

        public string Mapping
        {
            get { return _Mapping; }
            set
            {
                _Mapping = value;
                NotifyOfPropertyChange(() => Mapping);
            }
        }
    }
}
