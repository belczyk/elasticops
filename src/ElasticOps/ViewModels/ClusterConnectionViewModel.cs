using System;
using System.Reactive.Linq;
using Caliburn.Micro;
using ElasticOps.Com;
using ElasticOps.Events;
using Humanizer;
using Serilog;

namespace ElasticOps.ViewModels
{
    public class ClusterConnectionViewModel : PropertyChangedBase
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly Infrastructure _infrastructure;

        private string clusterUri;

        public ClusterConnectionViewModel(Infrastructure infrastructure)
        {
            _infrastructure = infrastructure;
            _eventAggregator = infrastructure.EventAggregator;

            var observable = Observable.Interval(_infrastructure.Config.RefreshInterval.Seconds()).TimeInterval();
            observable.Subscribe(o =>
            {
                if (infrastructure.Connection.IsConnected)
                    _eventAggregator.PublishOnUIThread(new RefreshEvent());
            });
            ClusterUri = infrastructure.Config.DefaultClusterUrl.ToString();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings")]
        public string ClusterUri
        {
            get { return clusterUri; }
            set
            {
                var wasConnected = IsConnected;
                clusterUri = value;
                _infrastructure.Connection.SetClusterUri(clusterUri);

                NotifyOfPropertyChange(() => ClusterUri);
                NotifyOfPropertyChange(() => IsValid);
                NotifyOfPropertyChange(() => IsConnected);
                NotifyOfPropertyChange(() => Version);

                if (IsConnected)
                    _eventAggregator.PublishOnUIThread(new NewConnectionEvent(ClusterUri));
                else
                    Log.Logger.Error("Could not connect to: {clusterUri}", ClusterUri);

                if (!wasConnected && IsConnected)
                    _eventAggregator.PublishOnUIThread(new RefreshEvent());
            }
        }

        public bool IsConnected
        {
            get { return _infrastructure.Connection.IsConnected; }
        }

        public string Version
        {
            get
            {
                if (_infrastructure.Connection.IsConnected && _infrastructure.Connection.Version != null)
                    return _infrastructure.Connection.Version.ToString();

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