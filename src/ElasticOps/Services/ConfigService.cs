using Caliburn.Micro;
using ElasticOps.Com;

namespace ElasticOps.Services
{
    public class ConfigService : IHandle<ThemeChangedEvent>, IHandle<AccentChangedEvent>
    {
        private readonly Infrastructure _infrastructure;


        public ConfigService(Infrastructure infrastructure)
        {
            Ensure.ArgumentNotNull(infrastructure, "infrastructure");

            _infrastructure = infrastructure;
            infrastructure.EventAggregator.Subscribe(this);
        }


        public void Handle(ThemeChangedEvent message)
        {
            Ensure.ArgumentNotNull(message, "message");

            _infrastructure.Config.Appearance.Theme = message.Theme;
            SaveConfig();
        }


        public void Handle(AccentChangedEvent message)
        {
            Ensure.ArgumentNotNull(message, "message");

            _infrastructure.Config.Appearance.Accent = message.Accent;
        }

        private void SaveConfig()
        {
            _infrastructure.Config.Save(Predef.ConfigPath);
        }
    }
}