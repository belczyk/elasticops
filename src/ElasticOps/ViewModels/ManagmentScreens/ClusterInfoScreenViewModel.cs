using Caliburn.Micro;

namespace ElasticOps.ViewModels.ManagmentScreens
{
    public class ClusterInfoScreenViewModel : Conductor<object>, IManagmentScreen
    {
        public ClusterInfoScreenViewModel()
        {
            DisplayName = "Cluster";
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
    }
}
