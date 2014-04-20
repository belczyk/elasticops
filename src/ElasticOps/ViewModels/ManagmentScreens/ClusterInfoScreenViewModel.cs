using System;
using Autofac;
using Caliburn.Micro;
using ElasticOps.Model;

namespace ElasticOps.ViewModels.ManagmentScreens
{
    public class ClusterInfoScreenViewModel : Conductor<object>, IManagmentScreen
    {


        public ClusterInfoScreenViewModel()
        {
            DisplayName = "Cluster";
            GeneralClusterInfo = new ClusterInfo().GetTabsData(new Uri("http://localhost:9200"));
        }

        public void ShowBasicInfo()
        {

            ActivateItem(new BasicInfoViewModel());
        }

        public void ShowNodesInfo()
        {
            ActivateItem(new NodesInfoViewModel());
        }

        public void ShowIndicesInfo()
        {
            //ActivateItem(new BasicInfoViewModel());
        }

        public GeneralClusterInfo GeneralClusterInfo { get; set; }
    }
}
