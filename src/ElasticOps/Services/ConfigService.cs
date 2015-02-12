using Caliburn.Micro;
using ElasticOps.Com;

namespace ElasticOps.Services
{
    public class ConfigService : IHandle<ThemeChangedEvent>, IHandle<AccentChangedEvent>
    {
        private readonly Infrastructure _infrastructure;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0")]
        public ConfigService(Infrastructure infrastructure)
        {
            _infrastructure = infrastructure;
            infrastructure.EventAggregator.Subscribe(this);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0")]
        public void Handle(ThemeChangedEvent message)
        {
            _infrastructure.Config.Appearance.Theme = message.Theme;
            SaveConfig();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0")]
        public void Handle(AccentChangedEvent message)
        {
            _infrastructure.Config.Appearance.Accent = message.Accent;

        }

        private void SaveConfig()
        {
            _infrastructure.Config.Save("config.yaml");
        }
    }
}
