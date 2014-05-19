
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
                NotifyOfPropertyChange(() => Value);
            }
        }


        private int minimum;

        public int Minimum
        {
            get { return minimum; }
            set
            {
                minimum = value;
                NotifyOfPropertyChange(() => Minimum);
            }
        }


        private int maximum;

        public int Maximum
        {
            get { return maximum; }
            set
            {
                maximum = value;
                NotifyOfPropertyChange(() => Maximum);
            }
        }




    }
}
