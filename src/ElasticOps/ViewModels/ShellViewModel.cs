using System.Collections.Generic;
using Caliburn.Micro;
using ElasticOps.ViewModels.ManagmentScreens;

namespace ElasticOps.ViewModels
{
    public class ShellViewModel : Conductor<IManagmentScreen>.Collection.OneActive
    {

        public ShellViewModel(IEnumerable<IManagmentScreen> managmentScreens)
        {
            ClusterConnectionViewModel = new ClusterConnectionViewModel { ClusterURL = "http://localhost9200" };
            ManagmentScreens = new BindableCollection<IManagmentScreen>();
            ManagmentScreens.AddRange(managmentScreens);
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
