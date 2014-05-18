using Caliburn.Micro;

namespace ElasticOps.ViewModels
{
    public class SettingPageViewModel : Screen
    {
        private IObservableCollection<SettingViewModel> _settings;

        public IObservableCollection<SettingViewModel> Settings
        {
            get { return _settings; }
            set
            {
                _settings = value;
                NotifyOfPropertyChange(() => Settings);
            }
        }

        public UserSettings UserSettings { get; set; }

    }
}
