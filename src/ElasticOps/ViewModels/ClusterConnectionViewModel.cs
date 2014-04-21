using System;
using System.Reactive.Linq;
using Caliburn.Micro;
using ElasticOps.Events;
using ElasticOps.Model;
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
                NotifyOfPropertyChange(() => IsValid);
                NotifyOfPropertyChange(() => IsConnected);
                if (IsValid)
                {
                    settings.ClusterUri = new Uri(value);
                }
            }
        }

        public bool IsValid
        {
            get
            {
                Uri uri;
                return Uri.TryCreate(ClusterUri,UriKind.Absolute,out uri);
            }
        }

        public bool IsConnected
        {
            get
            {
                if (!IsValid) return false;

                var ping = new PingCluster();

                return ping.IsAlive(new Uri(ClusterUri));
            }
        }

    }
}
