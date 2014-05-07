using Caliburn.Micro;
using ElasticOps.Com.Models;

namespace ElasticOps.ViewModels.ManagmentScreens
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

        private string _Hostname;

        public string Hostname
        {
            get { return _Hostname; }
            set
            {
                _Hostname = value;
                NotifyOfPropertyChange(() => Hostname);
            }
        }

        private string _HttpAddress;

        public string HttpAddress
        {
            get { return _HttpAddress; }
            set
            {
                _HttpAddress = value;
                NotifyOfPropertyChange(() => HttpAddress);
            }
        }

        private IObservableCollection<ElasticPropertyViewModel> _OS;

        public IObservableCollection<ElasticPropertyViewModel> OS
        {
            get { return _OS; }
            set
            {
                _OS = value;
                NotifyOfPropertyChange(() => OS);
            }
        }

        private IObservableCollection<ElasticPropertyViewModel> _CPU;

        public IObservableCollection<ElasticPropertyViewModel> CPU
        {
            get { return _CPU; }
            set
            {
                _CPU = value;
                NotifyOfPropertyChange(() => CPU);
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

        public NodeInfoViewModel(NodeInfo nodeInfo)
        {
            Name = nodeInfo.Name;
            Hostname = nodeInfo.Hostname;
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
