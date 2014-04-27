using Caliburn.Micro;
using ElasticOps.Com.Models;
using Humanizer;
using ClusterInfo = ElasticOps.Com.ClusterInfo;

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
        }
    }
}
