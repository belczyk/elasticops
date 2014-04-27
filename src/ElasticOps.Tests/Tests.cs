
using System;
using ElasticOps.Com.CommonTypes;
using Version = ElasticOps.Com.CommonTypes.Version;
using ElasticOps.Com.Infrastructure;
using ElasticOps.Com.Models;
using Microsoft.FSharp.Collections;
using NUnit.Framework;

namespace ElasticOps.Tests
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void CommandBus()
        {
            var bus = new RequestBus();

          var result =  bus.Execute<FSharpList<NodeInfo>>(new Connection(new Uri("http://localhost:9200"),new Version(1) ));
        }
    }
}
