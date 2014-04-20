using Caliburn.Micro;

namespace ElasticOps.ViewModels
{
    public class ClusterConnectionViewModel : PropertyChangedBase
    {

        public string clusterURL;
        public string ClusterURL
        {
            get { return clusterURL; }
            set
            {
                clusterURL = value;
                NotifyOfPropertyChange(() => ClusterURL);
            }
        }

    }
}
