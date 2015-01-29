using System;
using System.Configuration;
using Nest;

namespace ElasticOps.TestData
{
    public class Program
    {
        static void Main(string[] args)
        {
            var client = GetClient();

            CreateIndex("products", client);
        }

        private static ElasticClient GetClient()
        {
            var node = new Uri(ConfigurationManager.AppSettings["clusterUri"]);

            var settings = new ConnectionSettings(node);

            return new ElasticClient(settings);
        }

        private static void CreateIndex(string name, ElasticClient client)
        {
            client.CreateIndex(name, c => c
            .NumberOfReplicas(0)
            .NumberOfShards(1)
            .AddMapping<Book>(m => m.MapFromAttributes())
            .AddMapping<CD>(m => m.MapFromAttributes()));
        }
    }
}
