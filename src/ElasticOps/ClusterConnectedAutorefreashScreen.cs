using System;
using System.Threading.Tasks;
using Caliburn.Micro;
using ElasticOps.Events;

namespace ElasticOps
{
    public abstract class ClusterConnectedAutorefreashScreen : Screen, IHandle<RefreashEvent>, IHandle<ClusterUriChanged>
    {
        protected ClusterConnectedAutorefreashScreen(Uri clusterUri, IEventAggregator eventAggregator)
        {
            this.clusterUri = clusterUri;
            this.eventAggregator = eventAggregator;
        }

        protected IEventAggregator eventAggregator;
        protected Uri clusterUri;

        private bool _isRefreshing;

        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set
            {
                _isRefreshing = value;
                NotifyOfPropertyChange(() => IsRefreshing);
            }
        }

        protected override void OnDeactivate(bool close)
        {
            eventAggregator.Unsubscribe(this);
            base.OnDeactivate(close);
        }

        protected override void OnActivate()
        {
            eventAggregator.Subscribe(this);
            StartRefreshingData();
            base.OnActivate();
        }

        public void Handle(RefreashEvent message)
        {
            if (!IsRefreshing)
                StartRefreshingData();
        }

        public void Handle(ClusterUriChanged message)
        {
            if (clusterUri != message.ClusterUri && message.ClusterUri !=null)
            {
                clusterUri = message.ClusterUri;
                StartRefreshingData();
            }
        }

        private void StartRefreshingData()
        {
            IsRefreshing = true;
            var task = new Task(RefreshData);
            task.ContinueWith(t => IsRefreshing = false);
            task.Start();
        }

        public abstract void RefreshData();

    }
}
