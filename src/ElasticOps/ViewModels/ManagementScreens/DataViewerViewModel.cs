using System.Collections.Generic;
using System.Linq;
using ElasticOps.Attributes;
using ElasticOps.Com;
using Newtonsoft.Json;

namespace ElasticOps.ViewModels.ManagementScreens
{
    [Priority(40)]
    internal class DataViewerViewModel : ClusterConnectedAutoRefreshScreen, IManagementScreen
    {
        private readonly Infrastructure _infrastructure;

        public PaggerViewModel PaggerModel { get; set; }

        public DataViewerViewModel(Infrastructure infrastructure, TypesListViewModel typesListViewModel,
            PaggerViewModel paggerModel)
            : base(infrastructure)
        {
            _infrastructure = infrastructure;
            TypesList = typesListViewModel;
            base.DisplayName = "Data Viewer";
            PaggerModel = paggerModel;
            paggerModel.OnPageChanged = View;
        }


        public TypesListViewModel TypesList { get; set; }

        private IEnumerable<dynamic> _documents;

        public IEnumerable<dynamic> Documents
        {
            get { return _documents; }
            set
            {
                if (Equals(value, _documents)) return;
                _documents = value;
                NotifyOfPropertyChange(() => Documents);
            }
        }

        public void View()
        {
            var res =
                _infrastructure.CommandBus.Execute(new DataView.PageCommand(_infrastructure.Connection,
                    TypesList.SelectedIndex, TypesList.SelectedType, PaggerModel.PageSize, PaggerModel.Page));
            if (res.Success)
            {
                Documents = res.Result.Documents.Select(x => JsonConvert.DeserializeObject(x.ToString()));
                PaggerModel.Total = res.Result.Hits;
            }
        }

        public override void RefreshData()
        {
            TypesList.RefreshData();
        }
    }
}