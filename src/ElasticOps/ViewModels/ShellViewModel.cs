using System.Collections.Generic;
using System.Linq;
using Caliburn.Micro;
using ElasticOps.Extensions;
using ElasticOps.ViewModels.ManagmentScreens;

namespace ElasticOps.ViewModels
{
    public class ShellViewModel : Conductor<IManagmentScreen>.Collection.OneActive
    {

        public ShellViewModel(IEnumerable<IManagmentScreen> managmentScreens, ClusterConnectionViewModel clusterConnectionViewModel)
        {
            ClusterConnectionViewModel = clusterConnectionViewModel;

            ManagmentScreens = new BindableCollection<IManagmentScreen>();
            ManagmentScreens.AddRange(managmentScreens.OrderByPriority());

            ManagmentScreens.ToList().ForEach(x => x.ConductWith(this));
            

            DisplayName = "Elastic Ops";
        }

        private ClusterConnectionViewModel _clusterConnectionViewModel;
        public ClusterConnectionViewModel ClusterConnectionViewModel
        {
            get { return _clusterConnectionViewModel; }
            set { _clusterConnectionViewModel = value; NotifyOfPropertyChange(() => ClusterConnectionViewModel); }
        }

        public BindableCollection<IManagmentScreen> ManagmentScreens { get; set; }

    }
}
