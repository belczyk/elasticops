using System;
using System.Collections.Generic;
using System.Linq;
using Caliburn.Micro;
using ElasticOps.Com;
using Serilog;

namespace ElasticOps.ViewModels.ManagementScreens
{
    internal class DocumentsInfoViewModel : ClusterConnectedAutoRefreshScreen
    {
        public DocumentsInfoViewModel(Infrastructure infrastructure)
            : base(infrastructure)
        {
            Documents = new BindableCollection<ElasticPropertyViewModel>();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        public override void RefreshData()
        {
            try
            {
                var result = CommandBus.Execute(new ClusterInfo.DocumentsInfoCommand(Connection));

                if (result.Failed) return;

                var docs = result.Result.Select(docInfo => new ElasticPropertyViewModel
                {
                    Label = docInfo.Key,
                    Value = docInfo.Value
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