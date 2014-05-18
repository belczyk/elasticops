using Caliburn.Micro;

namespace ElasticOps.ViewModels
{
    public class StringSettingViewModel : SettingViewModel
    {
        private string _value;

        public string Value
        {
            get { return _value; }
            set
            {
                _value = value;
                NotifyOfPropertyChange(() => value);
            }
        }

    }
}
