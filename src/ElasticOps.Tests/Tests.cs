
using System;
using System.Collections.Generic;
using ElasticOps.Com;
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
            var bus = new CommandBus();
            var uri = new Uri("http://localhost:9200");
            var version = new Version(1);
            var connection = new Connection(uri, version);

            var result = bus.Execute(new ClusterInfo.HealthCommand(connection));

            Assert.AreEqual(result.Result["Status"],"yellow");
        }
    }
}
