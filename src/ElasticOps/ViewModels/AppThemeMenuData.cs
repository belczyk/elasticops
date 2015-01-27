using System.Windows;
using Caliburn.Micro;
using MahApps.Metro;
using ElasticOps.Com;

namespace ElasticOps.ViewModels
{
    public class AppThemeMenuData : AccentColorMenuData
    {
        protected override void DoChangeTheme(object sender)
        {
            var theme = ThemeManager.DetectAppStyle(Application.Current);
            var appTheme = ThemeManager.GetAppTheme(Name);
            ThemeManager.ChangeAppStyle(Application.Current, theme.Item2, appTheme);

            AppBootstrapper.GetInstance<IEventAggregator>().PublishOnUIThread(new ThemeChangedEvent(Name.Contains("Dark")));
        }
    }
}