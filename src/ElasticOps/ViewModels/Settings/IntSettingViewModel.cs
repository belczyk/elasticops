
namespace ElasticOps.ViewModels
{
    public class IntSettingViewModel : SettingViewModel
    {
        private int _value;

        public int Value
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
