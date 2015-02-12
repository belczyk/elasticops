using System.Collections.Generic;
using System.Linq;
using Caliburn.Micro;
using ElasticOps.Extensions;
using ElasticOps.ViewModels.ManagementScreens;

namespace ElasticOps.ViewModels
{
    public class StudioViewModel : Conductor<IManagementScreen>.Collection.OneActive
    {
        public StudioViewModel(IEnumerable<IManagementScreen> managementScreens,
            ClusterConnectionViewModel clusterConnectionViewModel)
        {
            ClusterConnectionViewModel = clusterConnectionViewModel;
            ManagementScreens = new BindableCollection<IManagementScreen>();
            ManagementScreens.AddRange(managementScreens.OrderByPriority());

            ManagementScreens.ToList().ForEach(x => x.ConductWith(this));
        }

        private ClusterConnectionViewModel _clusterConnectionViewModel;

        public ClusterConnectionViewModel ClusterConnectionViewModel
        {
            get { return _clusterConnectionViewModel; }
            set
            {
                _clusterConnectionViewModel = value;
                NotifyOfPropertyChange(() => ClusterConnectionViewModel);
            }
        }

        public IObservableCollection<IManagementScreen> ManagementScreens { get; private set; }
    }
}