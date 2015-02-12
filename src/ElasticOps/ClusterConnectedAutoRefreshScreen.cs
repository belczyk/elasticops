using System;
using System.Threading.Tasks;
using Caliburn.Micro;
using ElasticOps.Com;

namespace ElasticOps
{
    internal abstract class ClusterConnectedAutoRefreshScreen : Screen, IHandle<RefreshEvent>
    {
        protected ClusterConnectedAutoRefreshScreen(Infrastructure infrastructure)
        {
            if (infrastructure==null)
                throw new ArgumentNullException("infrastructure");

            eventAggregator = infrastructure.EventAggregator;
            commandBus = infrastructure.CommandBus;
            connection = infrastructure.Connection;
        }

        protected IEventAggregator eventAggregator;
        protected CommandBus commandBus;
        protected Connection connection;
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
            Deactivate(close);
        }

        public void Deactivate(bool close)
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

        public void Handle(RefreshEvent message)
        {
            if (!IsRefreshing)
                StartRefreshingData();
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
