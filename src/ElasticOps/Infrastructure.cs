using Caliburn.Micro;
using ElasticOps.Com;
using ElasticOps.Configuration;

namespace ElasticOps
{
    public class Infrastructure
    {
        public Infrastructure(CommandBus commandBus, 
            IEventAggregator eventAggregator, 
            IWindowManager windowManager,
            ElasticOpsConfig config)
        {
            CommandBus = commandBus;
            EventAggregator = eventAggregator;
            WindowManager = windowManager;
            Config = config;
            Connection = new Connection();
        }

        public Connection Connection { get; set; }

        public CommandBus CommandBus { get; set; }
        public IEventAggregator EventAggregator { get; set; }
        public IWindowManager WindowManager { get; set; }
        public ElasticOpsConfig Config { get; set; }
    }
}
