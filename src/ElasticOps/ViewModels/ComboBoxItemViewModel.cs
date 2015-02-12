using Caliburn.Micro;

namespace ElasticOps.ViewModels
{
    public class ComboBoxItemViewModel : PropertyChangedBase
    {
        private string _DisplayText;

        public string DisplayText
        {
            get { return _DisplayText; }
            set
            {
                _DisplayText = value;
                NotifyOfPropertyChange(() => DisplayText);
            }
        }

        private object _Value;

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