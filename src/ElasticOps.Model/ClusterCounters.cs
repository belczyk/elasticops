namespace ElasticOps.Model
{
    public class ClusterCounters
    {
        public int Nodes { get; set; }
        public int Indices { get; set; }
        public long Documents { get; set; }
    }
}
