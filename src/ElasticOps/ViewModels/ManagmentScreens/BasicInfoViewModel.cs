using System.Collections.Generic;
using Caliburn.Micro;
using ElasticOps.Model;

namespace ElasticOps.ViewModels.ManagmentScreens
{
    public class BasicInfoViewModel : ClusterConnectedAutorefreashScreen
    {
        public IObservableCollection<KeyValuePair<string, string>> ClusterHealthProperties { get; set; }


        public BasicInfoViewModel(Settings settings, IEventAggregator eventAggregator)
            : base(settings.ClusterUri, eventAggregator)
        {
            ClusterHealthProperties = new BindableCollection<KeyValuePair<string, string>>();
        }

        public override void RefreshData()
        {
            var clusterHealthInfo = new ClusterInfo().Load(clusterUri);
            ClusterHealthProperties.Clear();
            foreach (var keyValuePair in clusterHealthInfo)
                ClusterHealthProperties.Add(keyValuePair);
        }

    }
}
