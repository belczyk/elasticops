using System.Collections.Generic;
using System.Linq;
using Caliburn.Micro;
using ElasticOps.Extensions;
using ElasticOps.ViewModels.ManagementScreens;

namespace ElasticOps.ViewModels
{
    public class StudioViewModel : Conductor<IManagementScreen>.Collection.OneActive
    {

        public StudioViewModel(IEnumerable<IManagementScreen> managmentScreens, ClusterConnectionViewModel clusterConnectionViewModel)
        {
            ClusterConnectionViewModel = clusterConnectionViewModel;
            ManagmentScreens = new BindableCollection<IManagementScreen>();
            ManagmentScreens.AddRange(managmentScreens.OrderByPriority());

            ManagmentScreens.ToList().ForEach(x => x.ConductWith(this));
            

        }

        private ClusterConnectionViewModel _clusterConnectionViewModel;
        public ClusterConnectionViewModel ClusterConnectionViewModel
        {
            get { return _clusterConnectionViewModel; }
            set { _clusterConnectionViewModel = value; NotifyOfPropertyChange(() => ClusterConnectionViewModel); }
        }

        public BindableCollection<IManagementScreen> ManagmentScreens { get; set; }
    }
}
