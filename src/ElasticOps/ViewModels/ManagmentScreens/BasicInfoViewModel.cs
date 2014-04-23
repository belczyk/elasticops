using System;
using Caliburn.Micro;
using ElasticOps.Model;

namespace ElasticOps.ViewModels.ManagmentScreens
{
    public class BasicInfoViewModel : ClusterConnectedAutorefreashScreen
    {
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
                var clusterHealthInfo = new ClusterInfo().GetClusterHealthInfo(clusterUri);
                ClusterHealthProperties.Clear();
                foreach (var element in clusterHealthInfo)
                {
                    ClusterHealthProperties.Add(
                        new ElasticPropertyViewModel {Label = element.Key, Value = element.Value});
                }
            }
            catch (Exception ex)
            {
                
            }
        }
    }
}
