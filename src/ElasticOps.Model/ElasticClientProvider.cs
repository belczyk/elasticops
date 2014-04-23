using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Elasticsearch.Net.Connection;
using Nest;
using NLog;

namespace ElasticOps.Model
{
    public class ElasticClientProvider
    {
        private static Logger logger = LogManager.GetLogger("Nest.ElasticClient");


        public ElasticClient GetElasticClient(Uri uri)
        {
            var settings = new ConnectionSettings(uri);
            settings.ExposeRawResponse();
            settings.EnableTrace();
            settings.SetConnectionStatusHandler(HandleElasticResponse);

            var client = new ElasticClient(settings);

            return client;
        }

        private static void HandleElasticResponse(IElasticsearchResponse c)
        {
            if (c.Success) return;

            try
            {
                var requestBody = UTF8Encoding.UTF8.GetString(c.Request);
                var responseBody = UTF8Encoding.UTF8.GetString(c.ResponseRaw);

                logger.Warn("Error: {0}. Response code: {1}. Requested url: {2}{3}. \n Request body: {4}. Response body: {5}",
                    c.OriginalException.Message, c.RequestMethod, c.RequestUrl, requestBody, responseBody);
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            };
        }

        public ElasticsearchClient GetElasticNetClient(Uri uri)
        {
            var config = new ConnectionConfiguration(uri);
            config.EnableTrace();
            config.ExposeRawResponse();
            config.SetConnectionStatusHandler(HandleElasticResponse);

            var client = new ElasticsearchClient(config);
            return client;
        }
    }
}
