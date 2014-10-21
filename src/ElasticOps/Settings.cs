using ElasticOps.Com;

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
