using ElasticOps.Com.CommonTypes;

namespace ElasticOps
{
    public class Settings
    {
        public Settings()
        {
            Connection = new Connection();
        }

        public Connection Connection { get; set; }
    }

}
