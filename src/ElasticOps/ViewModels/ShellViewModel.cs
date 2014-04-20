using System.Collections.Generic;
using Caliburn.Micro;
using ElasticOps.ViewModels.ManagmentScreens;

namespace ElasticOps.ViewModels
{
    public class ShellViewModel : Conductor<IManagmentScreen>.Collection.OneActive
    {


        public ShellViewModel(IEnumerable<IManagmentScreen> managmentScreens)
        {
            ClusterConnection = new ClusterConnection { ClusterURL = "http://localhost9200" };
            ManagmentScreens = new BindableCollection<IManagmentScreen>();
            ManagmentScreens.AddRange(managmentScreens);
            DisplayName = "Elastic Ops";
        }

        private ClusterConnection _clusterConnection;
        public ClusterConnection ClusterConnection
        {
            get { return _clusterConnection; }
            set { _clusterConnection = value; NotifyOfPropertyChange(() => ClusterConnection); }
        }

        public BindableCollection<IManagmentScreen> ManagmentScreens { get; set; }

    }
}
