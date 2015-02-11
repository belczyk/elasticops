using System.Windows;
using Caliburn.Micro;
using ElasticOps.Com;
using MahApps.Metro;

namespace ElasticOps.Services
{
    public class ThemeService : IHandle<ThemeChangedEvent>, IHandle<AccentChangedEvent>
    {
        private readonly Infrastructure _infrastructure;

        public ThemeService(Infrastructure infrastructure)
        {
            _infrastructure = infrastructure;
            _infrastructure.EventAggregator.Subscribe(this);

            ChangeTheme(_infrastructure.Config.Appearance.Theme);
            ChangeAccent(_infrastructure.Config.Appearance.Accent);
        }

        private void ChangeAccent(string accentName)
        {
            var theme = ThemeManager.DetectAppStyle(Application.Current);
            var accent = ThemeManager.GetAccent(accentName);
            ThemeManager.ChangeAppStyle(Application.Current, accent, theme.Item1);
            _infrastructure.Config.Appearance.Accent = accentName;
            _infrastructure.Config.Save("Config.yaml");
        }

        private void ChangeTheme(string themeName)
        {
            var theme = ThemeManager.DetectAppStyle(Application.Current);
            var appTheme = ThemeManager.GetAppTheme(themeName);
            ThemeManager.ChangeAppStyle(Application.Current, theme.Item2, appTheme);
            _infrastructure.Config.Appearance.Theme = themeName;
            _infrastructure.Config.Save("Config.yaml");
        }

        public void Handle(AccentChangedEvent message)
        {
            ChangeAccent(message.Accent);
        }

        public void Handle(ThemeChangedEvent message)
        {
            ChangeTheme(message.Theme);
        }

    }
}
