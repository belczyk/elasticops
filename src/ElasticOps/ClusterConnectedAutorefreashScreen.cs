using System.Threading.Tasks;
using Caliburn.Micro;
using ElasticOps.Com;

namespace ElasticOps
{
    public abstract class ClusterConnectedAutorefreashScreen : Screen, IHandle<RefreashEvent>
    {
        protected ClusterConnectedAutorefreashScreen(Infrastructure infrastructure)
        {
            this.eventAggregator = infrastructure.EventAggregator;
            this.commandBus = infrastructure.CommandBus;
            this.connection = infrastructure.Connection;

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

        public void Handle(RefreashEvent message)
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
