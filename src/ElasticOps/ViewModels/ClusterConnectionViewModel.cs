using System;
using System.Reactive.Linq;
using Caliburn.Micro;
using ElasticOps.Com.CommonTypes;
using ElasticOps.Events;
using Version = ElasticOps.Com.CommonTypes.Version;
using Humanizer;
using NLog;
using LogManager = NLog.LogManager;

namespace ElasticOps.ViewModels
{
    public class ClusterConnectionViewModel : PropertyChangedBase
    {
        private Settings settings;
        private IEventAggregator eventAggregator;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public ClusterConnectionViewModel(Settings settings, IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
            this.settings = settings;
            ClusterUri = "http://localhost:9200";

            var observable = Observable.Interval(1.Minutes()).TimeInterval();
            observable.Subscribe((o) =>
            {
                if (IsConnected)
                    eventAggregator.Publish(new RefreashEvent());
            }
            );
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
                if (IsValid)
                {
                    IsConnectedLastUpdate = null;
                    IsConnectedLastValue = null;
                }
                NotifyOfPropertyChange(() => IsConnected);
                if (IsConnected)
                {
                    eventAggregator.Publish(new RefreashEvent());
                }
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

        private DateTime? IsConnectedLastUpdate { get; set; }
        private bool? IsConnectedLastValue { get; set; }
        public bool IsConnected
        {
            get
            {
                if (!IsValid) return false;

                if (IsConnectedLastValue.HasValue && IsConnectedLastUpdate.HasValue &&
                    (DateTime.Now - IsConnectedLastUpdate) > 30.Seconds())
                {
                    return IsConnectedLastValue.Value;
                }

                try
                {
                    var heartBeat = Com.ClusterInfo.IsAlive(new Uri(ClusterUri));
                    IsConnectedLastValue = heartBeat.IsAlive;
                    IsConnectedLastUpdate = DateTime.Now;
                    if (heartBeat.IsAlive)
                    {
                        settings.Connection = new Connection(new Uri(ClusterUri), Version.FromString(heartBeat.Version));
                    }
                    return heartBeat.IsAlive;
                }
                catch (Exception ex)
                {
                    IsConnectedLastValue = false;
                    IsConnectedLastUpdate = DateTime.Now;
                    logger.Warn(ex);
                }

                return false;
            }
        }

    }
}
