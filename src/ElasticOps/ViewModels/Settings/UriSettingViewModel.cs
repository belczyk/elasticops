using System;
using ElasticOps.Com;

namespace ElasticOps.ViewModels
{
    public class UriSettingViewModel : SettingViewModel
    {
        private Infrastructure infrastructure;

        public UriSettingViewModel(Infrastructure infrastructure)
        {
            this.infrastructure = infrastructure;
        }

        private Uri _value;

        public Uri Value
        {
            get
            {
                Uri uri;
                Uri.TryCreate(StringValue, UriKind.Absolute, out uri);

                return uri;
            }
        }

        public bool IsValid
        {
            get
            {
                Uri uri;
                return Uri.TryCreate(StringValue,UriKind.Absolute, out uri);
            }
        }

        private string _stringValue;

        public string StringValue
        {
            get { return _stringValue; }
            set
            {
                _stringValue = value;
                NotifyOfPropertyChange(() => StringValue);
                NotifyOfPropertyChange(() => Value);
                NotifyOfPropertyChange(() => IsValid);
            }
        }


    }
}
