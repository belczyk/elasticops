
using System;
using System.Collections.Generic;
using Caliburn.Micro;
using ElasticOps.Model;

namespace ElasticOps.ViewModels.ManagmentScreens
{
    public class BasicInfoViewModel : PropertyChangedBase 
    {
        public IObservableCollection<KeyValuePair<string, string>> ClusterHealthProperties { get; set; }

        public BasicInfoViewModel()
        {
            var clusterHealthInfo = new ClusterInfo().GetClusterHealthInfo(new Uri("http://localhost:9200"));
            ClusterHealthProperties = new BindableCollection<KeyValuePair<string, string>>();
            foreach (var keyValuePair in clusterHealthInfo)
                ClusterHealthProperties.Add(keyValuePair);
        }
    }
}
