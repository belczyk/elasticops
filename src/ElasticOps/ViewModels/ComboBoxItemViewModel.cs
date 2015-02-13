using Caliburn.Micro;

namespace ElasticOps.ViewModels
{
    public class ComboBoxItemViewModel : PropertyChangedBase
    {
        private string _displayText;

        private object _Value;

        public string DisplayText
        {
            get { return _displayText; }
            set
            {
                _displayText = value;
                NotifyOfPropertyChange(() => DisplayText);
            }
        }

        public object Value
        {
            get { return _Value; }
            set
            {
                _Value = value;
                NotifyOfPropertyChange(() => Value);
            }
        }

        public static ComboBoxItemViewModel FromString(string value)
        {
            return new ComboBoxItemViewModel
            {
                Value = value,
                DisplayText = value,
            };
        }
    }
}