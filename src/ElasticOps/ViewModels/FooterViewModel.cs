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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design",
            "CA1062:Validate arguments of public methods", MessageId = "0")]
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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design",
            "CA1062:Validate arguments of public methods", MessageId = "0")]
        public void Handle(ErrorOccuredEvent message)
        {
            ErrorMessage = message.ErrorMessage;
            Parallel.Invoke(() =>
            {
                Thread.Sleep(_infrastructure.Config.Appearance.Footer.ErrorTimout.Seconds());
                ErrorMessage = string.Empty;
            });
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design",
            "CA1062:Validate arguments of public methods", MessageId = "0")]
        public void Handle(NewConnectionEvent message)
        {
            CurrentClusterUri = message.URL;
        }
    }
}