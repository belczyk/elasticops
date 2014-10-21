using Caliburn.Micro;
using ElasticOps.Com;

namespace ElasticOps
{
    public class Infrastructure
    {
        public Infrastructure(Settings settings, 
            CommandBus commandBus, 
            IEventAggregator eventAggregator, 
            IWindowManager windowManager)
        {
            Settings = settings;
            CommandBus = commandBus;
            EventAggregator = eventAggregator;
            WindowManager = windowManager;
        }

        public Settings Settings { get; set; }
        public CommandBus CommandBus { get; set; }
        public IEventAggregator EventAggregator { get; set; }
        public IWindowManager WindowManager { get; set; }
    }
}
