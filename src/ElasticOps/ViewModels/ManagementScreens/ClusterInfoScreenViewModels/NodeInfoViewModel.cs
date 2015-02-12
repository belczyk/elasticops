using Caliburn.Micro;
using ElasticOps.Com;

namespace ElasticOps.ViewModels.ManagementScreens
{
    public class NodeInfoViewModel : PropertyChangedBase
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

        private string _hostName;

        public string HostName
        {
            get { return _hostName; }
            set
            {
                _hostName = value;
                NotifyOfPropertyChange(() => HostName);
            }
        }

        private string _httpAddress;

        public string HttpAddress
        {
            get { return _httpAddress; }
            set
            {
                _httpAddress = value;
                NotifyOfPropertyChange(() => HttpAddress);
            }
        }

        private IObservableCollection<ElasticPropertyViewModel> _OS;

        public IObservableCollection<ElasticPropertyViewModel> OS
        {
            get { return _OS; }
            private set
            {
                _OS = value;
                NotifyOfPropertyChange(() => OS);
            }
        }

        private IObservableCollection<ElasticPropertyViewModel> _CPU;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "CPU")]
        public IObservableCollection<ElasticPropertyViewModel> CPU
        {
            get { return _CPU; }
            private set
            {
                _CPU = value;
                NotifyOfPropertyChange(() => CPU);
            }
        }

        private IObservableCollection<ElasticPropertyViewModel> _Settings;

        public IObservableCollection<ElasticPropertyViewModel> Settings
        {
            get { return _Settings; }
            private set
            {
                _Settings = value;
                NotifyOfPropertyChange(() => Settings);
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0")]
        public NodeInfoViewModel(NodeInfo nodeInfo)
        {
            Name = nodeInfo.Name;
            HostName = nodeInfo.Hostname;
            HttpAddress = nodeInfo.HttpAddress;
            OS = new BindableCollection<ElasticPropertyViewModel>();
            if (nodeInfo.OS != null)
                foreach (var val in nodeInfo.OS)
                    OS.Add(new ElasticPropertyViewModel { Label = val.Key, Value = val.Value });
            Settings = new BindableCollection<ElasticPropertyViewModel>();
            if (nodeInfo.Settings != null)
                foreach (var val in nodeInfo.Settings)
                    Settings.Add(new ElasticPropertyViewModel { Label = val.Key, Value = val.Value });
            CPU = new BindableCollection<ElasticPropertyViewModel>();
            if (nodeInfo.CPU != null)
                foreach (var val in nodeInfo.CPU)
                    CPU.Add(new ElasticPropertyViewModel { Label = val.Key, Value = val.Value });
        }
    }
}
