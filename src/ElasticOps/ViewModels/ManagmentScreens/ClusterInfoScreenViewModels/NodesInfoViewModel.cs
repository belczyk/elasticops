using Caliburn.Micro;
using ElasticOps.Com;

namespace ElasticOps.ViewModels.ManagmentScreens
{
    public class NodesInfoViewModel : ClusterConnectedAutorefreashScreen
    {
        public IObservableCollection<NodeInfoViewModel> NodesInfo { get; set; }

        public NodesInfoViewModel(Infrastructure infrastructure)
            : base(infrastructure)
        {
            NodesInfo = new BindableCollection<NodeInfoViewModel>();
        }

        public override void RefreshData()
        {
            var result =
                commandBus.Execute(new ClusterInfo.NodesInfoCommand(connection));

            if (!result.Success) return;

            NodesInfo.Clear();
            foreach (var node in result.Result)
            {
                NodesInfo.Add(new NodeInfoViewModel(node));
            }

        }
    }
}
