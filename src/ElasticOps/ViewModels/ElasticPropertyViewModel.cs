using System.Windows;
using Caliburn.Micro;

namespace ElasticOps.ViewModels
{
    public class ElasticPropertyViewModel : PropertyChangedBase
    {
        private string _Label;

        private string _Value;

        public string Label
        {
            get { return _Label; }
            set
            {
                _Label = value;
                NotifyOfPropertyChange(() => Label);
            }
        }

        public string Value
        {
            get { return _Value; }
            set
            {
                _Value = value;
                NotifyOfPropertyChange(() => Value);
            }
        }

        public void CopyToClipboard()
        {
            Clipboard.SetText(Value);
        }
    }
}