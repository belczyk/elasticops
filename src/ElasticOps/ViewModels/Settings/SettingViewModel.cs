using Caliburn.Micro;

namespace ElasticOps.ViewModels
{

    public class SettingViewModel : PropertyChangedBase
    {
        private string _displayName;

        public string DisplayName
        {
            get { return _displayName; }
            set
            {
                _displayName = value;
                NotifyOfPropertyChange(() => DisplayName);
            }
        }

        private int _proiority;

        public int Proiority
        {
            get { return _proiority; }
            set
            {
                _proiority = value;
                NotifyOfPropertyChange(() => Proiority);
            }
        }


    }
}
