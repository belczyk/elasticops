using System.Threading;
using System.Threading.Tasks;
using Caliburn.Micro;
using ElasticOps.Commands;
using ElasticOps.Events;
using Humanizer;

namespace ElasticOps.ViewModels
{
    public class FooterViewModel : PropertyChangedBase, IHandle<ErrorOccurredEvent>, IHandle<NewConnectionEvent>
    {
        private readonly Infrastructure _infrastructure;

        private string _currentClusterUri;
        private string _errorMessage;

        public FooterViewModel(Infrastructure infrastructure)
        {
            Ensure.ArgumentNotNull(infrastructure, "infrastructure");

            _infrastructure = infrastructure;
            infrastructure.EventAggregator.Subscribe(this);

            CurrentClusterUri = infrastructure.Connection.ClusterUri.ToString();
        }

        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                _errorMessage = value;
                NotifyOfPropertyChange(() => ErrorMessage);
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings")]
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


        public void Handle(ErrorOccurredEvent message)
        {
            Ensure.ArgumentNotNull(message, "message");

            ErrorMessage = message.ErrorMessage;
            Parallel.Invoke(() =>
            {
                Thread.Sleep(_infrastructure.Config.Appearance.Footer.ErrorTimout.Seconds());
                ErrorMessage = string.Empty;
            });
        }


        public void Handle(NewConnectionEvent message)
        {
            Ensure.ArgumentNotNull(message, "message");

            CurrentClusterUri = message.URL;
        }
    }
}