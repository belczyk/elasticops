using System;
using System.Reactive.Linq;
using Caliburn.Micro;
using ElasticOps.Com;
using Humanizer;

namespace ElasticOps.ViewModels
{
    public class ClusterConnectionViewModel : PropertyChangedBase
    {
        private Settings settings;
        public ClusterConnectionViewModel(Settings settings, IEventAggregator eventAggregator)
        {
            this.settings = settings;

            var observable = Observable.Interval(10.Seconds()).TimeInterval();
            observable.Subscribe((o) =>
            {
                if (settings.Connection.IsConnected)
                    eventAggregator.Publish(new RefreashEvent());
            });
            ClusterUri = "http://localhost:9200";
        }

        public string clusterUri;
        public string ClusterUri
        {
            get { return clusterUri; }
            set
            {
                clusterUri = value;
                settings.Connection.SetClusterUri(clusterUri);

                NotifyOfPropertyChange(() => ClusterUri);
                NotifyOfPropertyChange(() => IsValid);
                NotifyOfPropertyChange(() => IsConnected);
                NotifyOfPropertyChange(() => Version);
            }
        }

        public bool IsConnected
        {
            get { return settings.Connection.IsConnected; }

        }

        public string Version
        {
            get
            {
                if (settings.Connection.IsConnected && settings.Connection.Version != null)
                    return settings.Connection.Version.ToString();

                return string.Empty;
            }
        }

        public bool IsValid
        {
            get
            {
                Uri uri;
                return Uri.TryCreate(ClusterUri, UriKind.Absolute, out uri);
            }
        
        }

    }
}
