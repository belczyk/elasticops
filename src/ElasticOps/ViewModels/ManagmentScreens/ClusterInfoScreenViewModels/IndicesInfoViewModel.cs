using System;
using System.Collections.Generic;
using System.Linq;
using Caliburn.Micro;
using ElasticOps.Com;
using Serilog;


namespace ElasticOps.ViewModels.ManagmentScreens
{
    public class IndicesInfoViewModel : ClusterConnectedAutorefreashScreen
    {
        private List<IndexInfoViewModel> AllIndicesInfo = new List<IndexInfoViewModel>();
        public IObservableCollection<IndexInfoViewModel> IndicesInfo { get; set; }

        public IndicesInfoViewModel(Infrastructure infrastructure)
            : base(infrastructure)
        {
            IndicesInfo = new BindableCollection<IndexInfoViewModel>();
        }

        public override void RefreshData()
        {
            try
            {
                var result = commandBus.Execute(new ClusterInfo.IndicesInfoCommand(connection));
                if (result.Failed) return;

                AllIndicesInfo.Clear();
                foreach (var indexInfo in result.Result)
                    AllIndicesInfo.Add(new IndexInfoViewModel(indexInfo));

                FilterIndices();
            }
            catch (Exception ex)
            {
                Log.Logger.Warning(ex,"Exception while refreshing data.");
            }
        }

        private void FilterIndices()
        {
            IndicesInfo.Clear();
            IndicesInfo.AddRange(AllIndicesInfo.Where(x=>ShowMarvelIndices || !x.Name.StartsWith(Predef.MarvelIndexPrefix)));
        }

        private bool _showMarvelIndices;

        public bool ShowMarvelIndices
        {
            get { return _showMarvelIndices; }
            set
            {
                _showMarvelIndices = value;
                NotifyOfPropertyChange(() => ShowMarvelIndices);
                FilterIndices();
            }
        }
    }
}
