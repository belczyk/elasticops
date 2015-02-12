using Autofac;
using Caliburn.Micro;

namespace ElasticOps.DIModules
{
    internal class CaliburnMicroModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<WindowManager>().As<IWindowManager>();
            builder.RegisterType<EventAggregator>().As<IEventAggregator>().SingleInstance();

            base.Load(builder);
        }
    }
}
