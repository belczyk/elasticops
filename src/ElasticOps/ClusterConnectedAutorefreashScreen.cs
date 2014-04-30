﻿using System.Threading.Tasks;
using Caliburn.Micro;
using ElasticOps.Com.CommonTypes;
using ElasticOps.Com.Infrastructure;
using ElasticOps.Events;
using Humanizer;

namespace ElasticOps
{
    public abstract class ClusterConnectedAutorefreashScreen : Conductor<object>.Collection.OneActive, IHandle<RefreashEvent>
    {
        protected ClusterConnectedAutorefreashScreen(Infrastructure infrastructure)
        {
            this.settings = infrastructure.Settings;
            this.eventAggregator = infrastructure.EventAggregator;
            this.commandBus = infrastructure.CommandBus;
            this.connection = infrastructure.Settings.Connection;

        }

        protected IEventAggregator eventAggregator;
        protected Settings settings;
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
            var task = new Task(RefreshData);
            task.ContinueWith(t => IsRefreshing = false);
            task.Start();
        }

        public abstract void RefreshData();

    }
}
