using System;
using Caliburn.Micro;
using ElasticOps.Model;
using NLog;
using LogManager = NLog.LogManager;

namespace ElasticOps.ViewModels.ManagmentScreens
{
    public class BasicInfoViewModel : ClusterConnectedAutorefreashScreen
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public IObservableCollection<ElasticPropertyViewModel> ClusterHealthProperties { get; set; }

        private ClusterInfo clusterInfo;

        public BasicInfoViewModel(Settings settings, IEventAggregator eventAggregator, ClusterInfo clusterInfo)
            : base(settings.ClusterUri, eventAggregator)
        {
            ClusterHealthProperties = new BindableCollection<ElasticPropertyViewModel>();
            this.clusterInfo = clusterInfo;
        }

        public override void RefreshData()
        {
            try
            {
                var clusterHealthInfo = clusterInfo.GetClusterHealthInfo(clusterUri);
                ClusterHealthProperties.Clear();
                foreach (var element in clusterHealthInfo)
                {
                    ClusterHealthProperties.Add(
                        new ElasticPropertyViewModel {Label = element.Key, Value = element.Value});
                }
            }
            catch (Exception ex)
            {
                logger.Warn(ex);
            }
        }
    }
}
