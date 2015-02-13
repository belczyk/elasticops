using Caliburn.Micro;
using ElasticOps.Com;

namespace ElasticOps.ViewModels.ManagementScreens
{
    public class NodeInfoViewModel : PropertyChangedBase
    {
        private IObservableCollection<ElasticPropertyViewModel> _cpu;
        private string _name;
        private IObservableCollection<ElasticPropertyViewModel> _os;
        private IObservableCollection<ElasticPropertyViewModel> _settings;

        private string _hostName;

        private string _httpAddress;

        public NodeInfoViewModel(NodeInfo nodeInfo)
        {
            Ensure.ArgumentNotNull(nodeInfo, "nodeInfo");

            Name = nodeInfo.Name;
            HostName = nodeInfo.Hostname;
            HttpAddress = nodeInfo.HttpAddress;
            OS = new BindableCollection<ElasticPropertyViewModel>();
            if (nodeInfo.OS != null)
                foreach (var val in nodeInfo.OS)
                    OS.Add(new ElasticPropertyViewModel {Label = val.Key, Value = val.Value});
            Settings = new BindableCollection<ElasticPropertyViewModel>();
            if (nodeInfo.Settings != null)
                foreach (var val in nodeInfo.Settings)
                    Settings.Add(new ElasticPropertyViewModel {Label = val.Key, Value = val.Value});
            CPU = new BindableCollection<ElasticPropertyViewModel>();
            if (nodeInfo.CPU != null)
                foreach (var val in nodeInfo.CPU)
                    CPU.Add(new ElasticPropertyViewModel {Label = val.Key, Value = val.Value});
        }

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                NotifyOfPropertyChange(() => Name);
            }
        }

        public string HostName
        {
            get { return _hostName; }
            set
            {
                _hostName = value;
                NotifyOfPropertyChange(() => HostName);
            }
        }

        public string HttpAddress
        {
            get { return _httpAddress; }
            set
            {
                _httpAddress = value;
                NotifyOfPropertyChange(() => HttpAddress);
            }
        }

        public IObservableCollection<ElasticPropertyViewModel> OS
        {
            get { return _os; }
            private set
            {
                _os = value;
                NotifyOfPropertyChange(() => OS);
            }
        }

        public IObservableCollection<ElasticPropertyViewModel> CPU
        {
            get { return _cpu; }
            private set
            {
                _cpu = value;
                NotifyOfPropertyChange(() => CPU);
            }
        }

        public IObservableCollection<ElasticPropertyViewModel> Settings
        {
            get { return _settings; }
            private set
            {
                _settings = value;
                NotifyOfPropertyChange(() => Settings);
            }
        }
    }
}