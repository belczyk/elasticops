using System;
using Caliburn.Micro;
using NLog;
using LogManager = NLog.LogManager;

namespace ElasticOps.ViewModels.ManagmentScreens
{
    public class DocumentsInfoViewModel : ClusterConnectedAutorefreashScreen
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public DocumentsInfoViewModel(Settings settings, IEventAggregator eventAggregator)
            : base(settings.ClusterUri, eventAggregator)
        {
            Documents = new BindableCollection<ElasticPropertyViewModel>();
        }

        public override void RefreshData()
        {
            try
            {
                var docsInfo = Com.ClusterInfo.DocumentsInfo(clusterUri);
                Documents.Clear();
                foreach (var docInfo in docsInfo)
                {
                    Documents.Add(new ElasticPropertyViewModel
                        {
                            Label = docInfo.Key,
                            Value = docInfo.Value
                        });
                }
            }
            catch (Exception ex)
            {
                logger.Warn(ex);
            }
        }

        private IObservableCollection<ElasticPropertyViewModel> _Documents;

        public IObservableCollection<ElasticPropertyViewModel> Documents
        {
            get { return _Documents; }
            set
            {
                _Documents = value;
                NotifyOfPropertyChange(() => Documents);
            }
        }
    }
}
