using Caliburn.Micro;
using ElasticOps.Com.Models;
using Humanizer;

namespace ElasticOps.ViewModels.ManagmentScreens
{
    public class IndexInfoViewModel : PropertyChangedBase
    {
        private string _Name;

        public string Name
        {
            get { return _Name; }
            set
            {
                _Name = value;
                NotifyOfPropertyChange(() => Name);
            }
        }

        private IObservableCollection<ElasticPropertyViewModel> _Settings;

        public IObservableCollection<ElasticPropertyViewModel> Settings
        {
            get { return _Settings; }
            set
            {
                _Settings = value;
                NotifyOfPropertyChange(() => Settings);
            }
        }

        private IObservableCollection<ElasticPropertyViewModel> _Types;

        public IObservableCollection<ElasticPropertyViewModel> Types
        {
            get { return _Types; }
            set
            {
                _Types = value;
                NotifyOfPropertyChange(() => Types);
            }
        }

        public IndexInfoViewModel(IndexInfo indexInfo)
        {
            Name = indexInfo.Name;
            Settings = new BindableCollection<ElasticPropertyViewModel>();
            foreach (var setting in indexInfo.Settings)
                Settings.Add(new ElasticPropertyViewModel
                    {
                        Label = setting.Key.Humanize(LetterCasing.Sentence), 
                        Value = setting.Value
                    });
            Types = new BindableCollection<ElasticPropertyViewModel>();
            foreach (var type in indexInfo.Types)
                Types.Add(new ElasticPropertyViewModel
                    {
                        Label = type.Key.Humanize(LetterCasing.Sentence),
                        Value = type.Value
                    });
        }
    }
}
