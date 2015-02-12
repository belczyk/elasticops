using System;
using System.Collections.Generic;
using System.Linq;
using Caliburn.Micro;
using ElasticOps.Com;
using Serilog;

namespace ElasticOps.ViewModels.ManagementScreens
{
    public class IndicesInfoViewModel : ClusterConnectedAutorefreashScreen
    {
        private readonly Infrastructure _infrastructure;
        private List<IndexInfoViewModel> AllIndicesInfo = new List<IndexInfoViewModel>();
        private IEnumerable<IndexInfoViewModel> _indicesInfo;

        public IEnumerable<IndexInfoViewModel> IndicesInfo
        {
            get { return _indicesInfo; }
            set
            {
                if (Equals(value, _indicesInfo)) return;
                _indicesInfo = value;
                NotifyOfPropertyChange(() => IndicesInfo);
            }
        }

        public IndicesInfoViewModel(Infrastructure infrastructure)
            : base(infrastructure)
        {
            _infrastructure = infrastructure;
            IndicesInfo = new BindableCollection<IndexInfoViewModel>();
        }

        public override void RefreshData()
        {
            try
            {
                var result = commandBus.Execute(new ClusterInfo.IndicesInfoCommand(connection));
                if (result.Failed) return;

                AllIndicesInfo.Clear();
                foreach (var indexInfo in result.Result)
                    AllIndicesInfo.Add(new IndexInfoViewModel(indexInfo, _infrastructure, RefreshData));

                FilterIndices();
            }
            catch (Exception ex)
            {
                Log.Logger.Warning(ex,"Exception while refreshing data.");
            }
        }

        private void FilterIndices()
        {
            IndicesInfo = AllIndicesInfo.Where(x=>ShowMarvelIndices || !x.Name.StartsWith(Predef.MarvelIndexPrefix));
        }

        private bool _showMarvelIndices;

        public bool ShowMarvelIndices
        {
            get { return _showMarvelIndices; }
            set
            {
                _showMarvelIndices = value;
                NotifyOfPropertyChange(() => ShowMarvelIndices);
                FilterIndices();
            }
        }
    }
}
