using System.Windows;
using Caliburn.Micro;

namespace ElasticOps.ViewModels
{
    public class ElasticPropertyViewModel : PropertyChangedBase
    {
        private string _label;

        private string _value;

        public string Label
        {
            get { return _label; }
            set
            {
                _label = value;
                NotifyOfPropertyChange(() => Label);
            }
        }

        public string Value
        {
            get { return _value; }
            set
            {
                _value = value;
                NotifyOfPropertyChange(() => Value);
            }
        }

        public void CopyToClipboard()
        {
            Clipboard.SetText(Value);
        }
    }
}