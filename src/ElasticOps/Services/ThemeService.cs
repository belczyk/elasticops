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
        }

        public void Handle(AccentChangedEvent message)
        {
            var theme = ThemeManager.DetectAppStyle(Application.Current);
            var accent = ThemeManager.GetAccent(message.Accent);
            ThemeManager.ChangeAppStyle(Application.Current, accent, theme.Item1);

        }

        public void Handle(ThemeChangedEvent message)
        {
            var theme = ThemeManager.DetectAppStyle(Application.Current);
            var appTheme = ThemeManager.GetAppTheme(message.Theme);
            ThemeManager.ChangeAppStyle(Application.Current, theme.Item2, appTheme);
        }
    }
}
