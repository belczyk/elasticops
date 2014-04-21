using System;
using System.Reactive.Linq;
using Caliburn.Micro;
using ElasticOps.Events;
using Humanizer;

namespace ElasticOps.ViewModels
{
    public class ClusterConnectionViewModel : PropertyChangedBase
    {
        private Settings settings;

        public ClusterConnectionViewModel(Settings settings, IEventAggregator eventAggregator)
        {
            this.settings = settings;
            ClusterUri = settings.ClusterUri.ToString();
            var observable = Observable.Interval(10.Seconds()).TimeInterval();
            observable.Subscribe((o) => eventAggregator.Publish(new RefreashEvent()));
        }

        public string clusterUri;
        public string ClusterUri
        {
            get { return clusterUri; }
            set
            {
                clusterUri = value;
                NotifyOfPropertyChange(() => ClusterUri);
                settings.ClusterUri = new Uri(value);
            }
        }

    }
}
