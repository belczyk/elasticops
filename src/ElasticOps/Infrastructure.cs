using Caliburn.Micro;
using ElasticOps.Com.Infrastructure;

namespace ElasticOps
{
    public class Infrastructure
    {
        public Infrastructure(Settings settings, CommandBus commandBus, IEventAggregator eventAggregator)
        {
            Settings = settings;
            CommandBus = commandBus;
            EventAggregator = eventAggregator;
        }

        public Settings Settings { get; set; }
        public CommandBus CommandBus { get; set; }
        public IEventAggregator EventAggregator { get; set; }
    }
}
