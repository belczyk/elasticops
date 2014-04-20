using Caliburn.Micro;

namespace ElasticOps.ViewModels
{
    public class ClusterConnection : PropertyChangedBase
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
