 using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using Caliburn.Micro;
using ElasticOps.Com;
using Humanizer;

namespace ElasticOps.ViewModels
{
    public class FooterViewModel : PropertyChangedBase, IHandle<ErrorOccuredEvent>
    {
        private IEventAggregator eventAggregator;

        public FooterViewModel(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
            eventAggregator.Subscribe(this);
        }

        private string _errorMessage;

        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                _errorMessage = value;
                NotifyOfPropertyChange(() => ErrorMessage);
            }
        }

        public void Handle(ErrorOccuredEvent @event)
        {
            ErrorMessage = @event.ErrorMessage;
            var task = new Task(() =>
            {
                Thread.Sleep(2.Seconds());
                ErrorMessage = string.Empty;
            });

            task.Start();
        }
    }
}
