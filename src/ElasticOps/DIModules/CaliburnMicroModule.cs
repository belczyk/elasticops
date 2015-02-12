using Autofac;
using Caliburn.Micro;

namespace ElasticOps.DIModules
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
        "CA1812:AvoidUninstantiatedInternalClasses")]
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