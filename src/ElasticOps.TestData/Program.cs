using System;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using Nest;
using Serilog;

namespace ElasticOps.TestData
{
    public class Program
    {
        private static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.ColoredConsole()
                .CreateLogger();


            var section = ConfigurationManager.GetSection("Nodes") as NameValueCollection;
            foreach (var key in section.AllKeys)
            {
                var uri = section[key];
                Log.Logger.Information("Start creating data for: {clusterUri}", uri);
                var client = GetClient(uri);
                CreateIndex("products", client);
            }


            Log.Logger.Information("DONE!");
            Console.ReadLine();
        }

        private static ElasticClient GetClient(string uri)
        {
            var node = new Uri(uri);

            var settings = new ConnectionSettings(node);

            return new ElasticClient(settings);
        }

        private static void CreateIndex(string name, ElasticClient client)
        {
            if (client.IndexExists(x => x.Index(name)).Exists)
            {
                Log.Logger.Information("Delete index {indexName}", name);

                client.DeleteIndex(x => x.Index(name));
            }

            client.OpenIndex(x => x.Index(name));

            Log.Logger.Information("Create index {indexName}", name);
            client.CreateIndex(name, c => c
                .NumberOfReplicas(0)
                .NumberOfShards(1)
                .AddMapping<Book>(m => m.MapFromAttributes())
                .AddMapping<CD>(m => m.MapFromAttributes()));
            Log.Logger.Information("Index {indexName} created", name);

            Log.Logger.Information("Closing index {indexName}", name);
            client.CloseIndex(x => x.Index(name));

            Log.Logger.Information("Add analyzer to index {indexName}", name);
            var res = client.Raw.IndicesPutSettings(name, File.ReadAllText("Analyzer.json"));

            if (!res.Success)
                Log.Logger.Error("Could not create analyzer: {error}", res.OriginalException.ToString());

            Log.Logger.Information("Open index {indexName}", name);

            RandomBooks(1000, name, client);
            RandomCDs(1000, name, client);
        }

        private static void RandomBooks(int n, string indexName, ElasticClient client)
        {
            Log.Logger.Information("Creating {numberOfDocs} book documents in index {indexName}", n, indexName);

            for (var i = 0; i < n; i++)
            {
                var book = new Book
                {
                    Id = i,
                    Description = LoremNET.Lorem.Paragraph(20, 10),
                    Title = LoremNET.Lorem.Words(1, 5),
                    Year = (int) LoremNET.Lorem.Number(1965, 2015)
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
                    Genere = LoremNET.Lorem.Words(1, 2),
                    ReleaseDate = LoremNET.Lorem.DateTime(1990)
                };

                client.Index(book, ind => ind.Index(indexName));

                if (i%1000 == 0)
                    Log.Logger.Information("Created {numberOfDocs} CD documents in index {indexName}", i, indexName);
            }
        }
    }
}