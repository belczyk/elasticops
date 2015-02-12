using System.Windows.Input;
using System.Windows.Media;
using Caliburn.Micro;
using ElasticOps.Com;

namespace ElasticOps.ViewModels
{
    public class AccentColorMenuData
    {
        public string Name { get; set; }
        public Brush BorderColorBrush { get; set; }
        public Brush ColorBrush { get; set; }

        private ICommand _changeAccentCommand;

        public ICommand ChangeAccentCommand
        {
            get
            {
                return this._changeAccentCommand ??
                       (_changeAccentCommand =
                           new SimpleCommand
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