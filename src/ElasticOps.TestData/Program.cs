using System;
using System.Configuration;
using Nest;
using Serilog;

namespace ElasticOps.TestData
{
    public class Program
    {

        static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.ColoredConsole()
                .CreateLogger();

            var uri = ConfigurationManager.AppSettings["clusterUri"];
            Log.Logger.Information("Start creating data for: {clusterUri}", uri);
            var client = GetClient(uri);

            CreateIndex("products", client);
        }

        private static ElasticClient GetClient(string uri)
        {
            var node = new Uri(uri);

            var settings = new ConnectionSettings(node);

            return new ElasticClient(settings);
        }

        private static void CreateIndex(string name, ElasticClient client)
        {
            Log.Logger.Information("Create index {indexName}",name);
            client.CreateIndex(name, c => c
            .NumberOfReplicas(0)
            .NumberOfShards(1)
            .AddMapping<Book>(m => m.MapFromAttributes())
            .AddMapping<CD>(m => m.MapFromAttributes()));
            Log.Logger.Information("Index {indexName} created", name);

            RandomBooks(100000, name, client);
            RandomCDs(10000, name, client);
        }

        private static void RandomBooks(int n,string indexName, ElasticClient client)
        {
            Log.Logger.Information("Creating {numberOfDocs} book documents in index {indexName}",n, indexName);

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

                if (i%1000 == 0)
                    Log.Logger.Information("Created {numberOfDocs} book documents in index {indexName}", i, indexName);

            }
        }


        private static void RandomCDs(int n, string indexName, ElasticClient client)
        {
            Log.Logger.Information("Creating {numberOfDocs} CD documents in index {indexName}", n, indexName);

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

                if (i % 1000 == 0)
                    Log.Logger.Information("Created {numberOfDocs} CD documents in index {indexName}", i, indexName);

            }
        }
    }
}
