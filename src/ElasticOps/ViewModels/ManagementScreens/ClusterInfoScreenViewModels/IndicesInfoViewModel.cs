using System;
using System.Collections.Generic;
using System.Linq;
using Caliburn.Micro;
using ElasticOps.Com;
using ElasticOps.Extensions;
using Serilog;

namespace ElasticOps.ViewModels.ManagementScreens
{
    internal class IndicesInfoViewModel : ClusterConnectedAutoRefreshScreen
    {
        private readonly List<IndexInfoViewModel> AllIndicesInfo = new List<IndexInfoViewModel>();
        private readonly Infrastructure _infrastructure;
        private IEnumerable<IndexInfoViewModel> _indicesInfo;
        private bool _showMarvelIndices;

        public IndicesInfoViewModel(Infrastructure infrastructure)
            : base(infrastructure)
        {
            _infrastructure = infrastructure;
            IndicesInfo = new BindableCollection<IndexInfoViewModel>();
        }

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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        public override void RefreshData()
        {
            try
            {
                var result = CommandBus.Execute(new ClusterInfo.IndicesInfoCommand(Connection));
                if (result.Failed) return;

                AllIndicesInfo.Clear();
                foreach (var indexInfo in result.Result)
                    AllIndicesInfo.Add(new IndexInfoViewModel(indexInfo, _infrastructure, RefreshData));

                FilterIndices();
            }
            catch (Exception ex)
            {
                Log.Logger.Warning(ex, "Exception while refreshing data.");
            }
        }

        private void FilterIndices()
        {
            IndicesInfo =
                AllIndicesInfo.Where(x => ShowMarvelIndices || !x.Name.StartsWithIgnoreCase(Predef.MarvelIndexPrefix));
        }
    }
}