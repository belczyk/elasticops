using System;
using Caliburn.Micro;
using ElasticOps.Com;
using NLog;
using LogManager = NLog.LogManager;

namespace ElasticOps.ViewModels.ManagmentScreens
{
    public class DocumentsInfoViewModel : ClusterConnectedAutorefreashScreen
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

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

                Documents.Clear();
                foreach (var docInfo in result.Result)
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
