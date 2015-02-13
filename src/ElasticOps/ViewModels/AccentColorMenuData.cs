using System.Windows.Input;
using System.Windows.Media;
using Caliburn.Micro;
using ElasticOps.Com;

namespace ElasticOps.ViewModels
{
    public class AccentColorMenuData
    {
        private ICommand _changeAccentCommand;
        public string Name { get; set; }
        public Brush BorderColorBrush { get; set; }
        public Brush ColorBrush { get; set; }

        public ICommand ChangeAccentCommand
        {
            get
            {
                return this._changeAccentCommand ??
                       (_changeAccentCommand =
                           new Command
                           {
                               CanExecuteDelegate = x => true,
                               ExecuteDelegate = x => this.DoChangeTheme(x)
                           });
            }
        }

        public virtual void DoChangeTheme(object sender)
        {
            AppBootstrapper.GetInstance<IEventAggregator>().PublishOnUIThread(new AccentChangedEvent(this.Name));
        }
    }
}