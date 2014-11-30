using System;
using Caliburn.Micro;
using ElasticOps.Com;
using Logary;


namespace ElasticOps.ViewModels.ManagmentScreens
{
    public class DocumentsInfoViewModel : ClusterConnectedAutorefreashScreen
    {

        private static Logger logger = Logging.GetCurrentLogger();
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
                logger.WarnException("Error while refreshing data.",ex);
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
