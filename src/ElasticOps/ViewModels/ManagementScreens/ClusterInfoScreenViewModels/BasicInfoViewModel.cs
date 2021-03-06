﻿using System.Collections.Generic;
using System.Linq;
using Caliburn.Micro;
using ElasticOps.Commands;

namespace ElasticOps.ViewModels.ManagementScreens
{
    internal class BasicInfoViewModel : ClusterConnectedAutoRefreshScreen
    {
        private IEnumerable<ElasticPropertyViewModel> _clusterHealthProperties;

        public BasicInfoViewModel(Infrastructure infrastructure)
            : base(infrastructure)
        {
            ClusterHealthProperties = new BindableCollection<ElasticPropertyViewModel>();
        }

        public IEnumerable<ElasticPropertyViewModel> ClusterHealthProperties
        {
            get { return _clusterHealthProperties; }
            set
            {
                if (Equals(value, _clusterHealthProperties)) return;
                _clusterHealthProperties = value;
                NotifyOfPropertyChange(() => ClusterHealthProperties);
            }
        }

        public override void RefreshData()
        {
            var result = CommandBus.Execute(new ClusterInfo.HealthCommand(Connection));

            if (result.Failed) return;

            ClusterHealthProperties =
                result.Result.Select(
                    element => new ElasticPropertyViewModel {Label = element.Key, Value = element.Value});
        }
    }
}