using System;
using System.Collections.Generic;
using System.Linq;
using Caliburn.Micro;
using ElasticOps.Model;
using NLog;
using LogManager = NLog.LogManager;

namespace ElasticOps.ViewModels.ManagmentScreens
{
    public class IndicesInfoViewModel : ClusterConnectedAutorefreashScreen
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private ClusterInfo clusterInfo;

        private List<IndexInfoViewModel> AllIndicesInfo = new List<IndexInfoViewModel>();
        public IObservableCollection<IndexInfoViewModel> IndicesInfo { get; set; }

        public IndicesInfoViewModel(Settings settings, IEventAggregator eventAggregator, ClusterInfo clusterInfo)
            : base(settings.ClusterUri, eventAggregator)
        {
            IndicesInfo = new BindableCollection<IndexInfoViewModel>();
            this.clusterInfo = clusterInfo;
        }

        public override void RefreshData()
        {
            try
            {
                var info = clusterInfo.GetIndicesInfo(clusterUri);
                AllIndicesInfo.Clear();
                foreach (var indexInfo in info)
                    AllIndicesInfo.Add(new IndexInfoViewModel(indexInfo));

                FilterIndices();
            }
            catch (Exception ex)
            {
                logger.Warn(ex);
            }
        }

        private void FilterIndices()
        {
            IndicesInfo.Clear();
            IndicesInfo.AddRange(AllIndicesInfo.Where(x=>ShowMarvelIndices || !x.Name.StartsWith(".marvel")));
        }

        private bool _ShowMarvelIndices;

        public bool ShowMarvelIndices
        {
            get { return _ShowMarvelIndices; }
            set
            {
                _ShowMarvelIndices = value;
                NotifyOfPropertyChange(() => ShowMarvelIndices);
                FilterIndices();
            }
        }
    }
}
