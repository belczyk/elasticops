using Caliburn.Micro;
using ElasticOps.Commands;
using ElasticOps.Events;

namespace ElasticOps.ViewModels
{
    public class AppThemeMenuData : AccentColorMenuData
    {
        public override void DoChangeTheme(object sender)
        {
            AppBootstrapper.GetInstance<IEventAggregator>()
                .PublishOnUIThread(new ThemeChangedEvent(Name, Name.Contains("Dark")));
        }
    }
}