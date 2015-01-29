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

            RandomBooks(100000, name, client);
            RandomCDs(10000, name, client);
        }

        private static void RandomBooks(int n,string indexName, ElasticClient client)
        {
            for (var i = 0; i < n; i++)
            {
                var book = new Book
                {
                    Id = i,
                    Description = LoremNET.Lorem.Paragraph(20,10),
                    Title =  LoremNET.Lorem.Words(1,5),
                    Year = (int)LoremNET.Lorem.Number(1965, 2015)
                };

                client.Index(book, ind => ind.Index(indexName));
            }
        }


        private static void RandomCDs(int n, string indexName, ElasticClient client)
        {
            for (var i = 0; i < n; i++)
            {
                var book = new CD
                {
                    Id = i,
                    Title = LoremNET.Lorem.Words(1, 5),
                    Genere= LoremNET.Lorem.Words(1, 2),
                    ReleaseDate = LoremNET.Lorem.DateTime(1990)
                };

                client.Index(book, ind => ind.Index(indexName));
            }
        }
    }
}
