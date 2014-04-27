
using ElasticOps.Com.Infrastructure;
using ElasticOps.Com.Models;
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
            bus.Execute<NodeInfo>();

        }
    }
}
