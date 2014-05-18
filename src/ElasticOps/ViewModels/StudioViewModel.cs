using System.Collections.Generic;
using System.Linq;
using Caliburn.Micro;
using ElasticOps.Extensions;
using ElasticOps.ViewModels.ManagmentScreens;

namespace ElasticOps.ViewModels
{
    public class StudioViewModel : Conductor<IManagmentScreen>.Collection.OneActive
    {
        private IEventAggregator eventAggregator;
        private Settings settings;

        public StudioViewModel(Settings settings, IEventAggregator eventAggregator, IEnumerable<IManagmentScreen> managmentScreens, ClusterConnectionViewModel clusterConnectionViewModel)
        {
            ClusterConnectionViewModel = clusterConnectionViewModel;
            ManagmentScreens = new BindableCollection<IManagmentScreen>();
            ManagmentScreens.AddRange(managmentScreens.OrderByPriority());
            this.settings = settings;
            this.eventAggregator = eventAggregator;

            ManagmentScreens.ToList().ForEach(x => x.ConductWith(this));
            

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
