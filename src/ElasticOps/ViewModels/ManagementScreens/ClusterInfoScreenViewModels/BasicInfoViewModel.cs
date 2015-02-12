using System.Collections.Generic;
using System.Linq;
using Caliburn.Micro;
using ElasticOps.Com;

namespace ElasticOps.ViewModels.ManagementScreens
{
    internal class BasicInfoViewModel : ClusterConnectedAutorefreashScreen
    {
        private IEnumerable<ElasticPropertyViewModel> _clusterHealthProperties;

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

        public BasicInfoViewModel(Infrastructure infrastructure)
            : base(infrastructure)
        {
            ClusterHealthProperties = new BindableCollection<ElasticPropertyViewModel>();
        }

        public override void RefreshData()
        {
           var result = commandBus.Execute(new ClusterInfo.HealthCommand(connection));

            if (result.Failed) return;

            ClusterHealthProperties = result.Result.Select(element => new ElasticPropertyViewModel {Label = element.Key, Value = element.Value});
        }
    }
}
