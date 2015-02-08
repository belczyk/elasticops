using System;
using System.Collections.Generic;
using System.Linq;
using Caliburn.Micro;
using ElasticOps.Com;
using Serilog;

namespace ElasticOps.ViewModels.ManagmentScreens
{
    public class DocumentsInfoViewModel : ClusterConnectedAutorefreashScreen
    {

        public DocumentsInfoViewModel(Infrastructure infrastructure)
            : base(infrastructure)
        {
            Documents = new BindableCollection<ElasticPropertyViewModel>();
        }

        public override void RefreshData()
        {
            try
            {
                var result = commandBus.Execute(new ClusterInfo.DocumentsInfoCommand(connection));

                if (result.Failed) return;

                var docs = result.Result.Select(docInfo => new ElasticPropertyViewModel
                {
                    Label = docInfo.Key, Value = docInfo.Value
                });

                Documents = docs;
            }
            catch (Exception ex)
            {
                Log.Logger.Warning(ex, "Exception while refreshing data.");
            }
        }

        private IEnumerable<ElasticPropertyViewModel> _documents;

        public IEnumerable<ElasticPropertyViewModel> Documents
        {
            get { return _documents; }
            set
            {
                _documents = value;
                NotifyOfPropertyChange(() => Documents);
            }
        }
    }
}
