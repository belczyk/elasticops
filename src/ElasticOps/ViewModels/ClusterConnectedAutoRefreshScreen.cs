using System.Threading.Tasks;
using Caliburn.Micro;
using ElasticOps.Com;
using ElasticOps.Events;

namespace ElasticOps.ViewModels
{
    internal abstract class ClusterConnectedAutoRefreshScreen : Screen, IHandle<RefreshEvent>
    {
        private bool _isRefreshing;

        protected ClusterConnectedAutoRefreshScreen(Infrastructure infrastructure)
        {
            Ensure.ArgumentNotNull(infrastructure, "infrastructure");

            EventAggregator = infrastructure.EventAggregator;
            CommandBus = infrastructure.CommandBus;
            Connection = infrastructure.Connection;
        }

        protected IEventAggregator EventAggregator { get; set; }
        protected CommandBus CommandBus { get; set; }
        protected Connection Connection { get; set; }

        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set
            {
                _isRefreshing = value;
                NotifyOfPropertyChange(() => IsRefreshing);
            }
        }

        public void Handle(RefreshEvent message)
        {
            if (!IsRefreshing)
                StartRefreshingData();
        }

        protected override void OnDeactivate(bool close)
        {
            Deactivate(close);
        }

        public void Deactivate(bool close)
        {
            EventAggregator.Unsubscribe(this);
            base.OnDeactivate(close);
        }

        protected override void OnActivate()
        {
            EventAggregator.Subscribe(this);
            StartRefreshingData();
            base.OnActivate();
        }

        private void StartRefreshingData()
        {
            IsRefreshing = true;
            Task.Factory.StartNew(RefreshData)
                .ContinueWith(t => IsRefreshing = false);
        }

        public abstract void RefreshData();
    }
}