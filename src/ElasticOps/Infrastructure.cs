using Caliburn.Micro;
using ElasticOps.Com.Infrastructure;

namespace ElasticOps
{
    public class Infrastructure
    {
        public Infrastructure(Settings settings, 
            CommandBus commandBus, 
            IEventAggregator eventAggregator, 
            IWindowManager windowManager,
            DialogManager dialogManager)
        {
            Settings = settings;
            CommandBus = commandBus;
            EventAggregator = eventAggregator;
            WindowManager = windowManager;
            DialogManager = dialogManager;
        }

        public Settings Settings { get; set; }
        public CommandBus CommandBus { get; set; }
        public IEventAggregator EventAggregator { get; set; }
        public IWindowManager WindowManager { get; set; }
        public DialogManager DialogManager { get; set; }
    }
}
