using System;
using Caliburn.Micro;
using Elasticsearch.Net.Connection;
using NLog;
using LogManager = NLog.LogManager;

namespace ElasticOps.ViewModels.ManagmentScreens
{
    public class BasicInfoViewModel : ClusterConnectedAutorefreashScreen
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public IObservableCollection<ElasticPropertyViewModel> ClusterHealthProperties { get; set; }
        

        public BasicInfoViewModel(Settings settings, IEventAggregator eventAggregator)
            : base(settings.ClusterUri, eventAggregator)
        {
            ClusterHealthProperties = new BindableCollection<ElasticPropertyViewModel>();
        }

        public override void RefreshData()
        {
            try
            {
                var clusterHealthInfo = Com.ClusterInfo.Health(clusterUri);
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
