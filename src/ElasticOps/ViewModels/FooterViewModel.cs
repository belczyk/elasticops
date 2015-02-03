using System.Threading;
using System.Threading.Tasks;
using Caliburn.Micro;
using ElasticOps.Com;
using Humanizer;

namespace ElasticOps.ViewModels
{
    public class FooterViewModel : PropertyChangedBase, IHandle<ErrorOccuredEvent>, IHandle<NewConnectionEvent>
    {
        private readonly Infrastructure _infrastructure;

        public FooterViewModel(Infrastructure infrastructure)
        {
            _infrastructure = infrastructure;
            infrastructure.EventAggregator.Subscribe(this);

            CurrentClusterUri = infrastructure.Connection.ClusterUri.ToString();

        }

        private string _errorMessage;
        private string _currentClusterUri;

        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                _errorMessage = value;
                NotifyOfPropertyChange(() => ErrorMessage);
            }
        }

        public string CurrentClusterUri
        {
            get { return _currentClusterUri; }
            set
            {
                if (value == _currentClusterUri) return;
                _currentClusterUri = value;
                NotifyOfPropertyChange(() => CurrentClusterUri);
            }
        }

        public void Handle(ErrorOccuredEvent @event)
        {
            ErrorMessage = @event.ErrorMessage;
            var task = new Task(() =>
            {
                Thread.Sleep(_infrastructure.Config.Appearance.Footer.ErrorTimout.Seconds());
                ErrorMessage = string.Empty;
            });

            task.Start();
        }

        public void Handle(NewConnectionEvent message)
        {
            CurrentClusterUri = message.URL;
        }

    }
}
