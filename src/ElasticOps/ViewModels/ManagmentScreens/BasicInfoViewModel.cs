using System.Collections.Generic;
using Caliburn.Micro;
using ElasticOps.Com;
using ElasticOps.Com.Infrastructure;

namespace ElasticOps.ViewModels.ManagmentScreens
{
    public class BasicInfoViewModel : ClusterConnectedAutorefreashScreen
    {

        public IObservableCollection<ElasticPropertyViewModel> ClusterHealthProperties { get; set; }

        public BasicInfoViewModel(Infrastructure infrastructure)
            : base(infrastructure)
        {
            ClusterHealthProperties = new BindableCollection<ElasticPropertyViewModel>();
        }

        public override void RefreshData()
        {
            var result = commandBus.Execute(new ClusterInfo.HealthCommand(connection));

            if (result.Failed) return;

            ClusterHealthProperties.Clear();
            foreach (var element in result.Result)
            {
                ClusterHealthProperties.Add(
                    new ElasticPropertyViewModel {Label = element.Key, Value = element.Value});
            }
    
        }

      
    }
}
