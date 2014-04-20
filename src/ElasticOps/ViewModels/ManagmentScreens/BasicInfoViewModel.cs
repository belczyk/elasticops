
using System;
using Caliburn.Micro;
using ElasticOps.Model;

namespace ElasticOps.ViewModels.ManagmentScreens
{
    public class BasicInfoViewModel : PropertyChangedBase 
    {
        public BasicInfoViewModel()
        {
            var clusterInfo = new ClusterInfo();
            Status = clusterInfo.Load(new Uri("http://localhost:9200")).Status;
        }

        private string _Status;
        public string Status
        {
            get { return _Status; }
            set
            {
                _Status = value;
                NotifyOfPropertyChange(() => Status);
            }
        }

    }
}
