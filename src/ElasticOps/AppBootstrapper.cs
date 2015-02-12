using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reflection;
using System.Windows;
using Autofac;
using Caliburn.Micro;
using Caliburn.Micro.Autofac;
using ElasticOps.Behaviors.Suggesters;
using ElasticOps.Com;
using ElasticOps.Configuration;
using ElasticOps.Services;
using ElasticOps.ViewModels;
using ElasticOps.ViewModels.ManagementScreens;
using Serilog;
using Serilog.Events;

namespace ElasticOps
{
    public class AppBootstrapper : AutofacBootstrapper<ShellViewModel>
    {
        private static IContainer AutofacContainer { get; set; }

        public AppBootstrapper()
        {
            Initialize();
            AutofacContainer = Container;
        }

        public static IEnumerable<T> GetAllInstances<T>()
        {
            return AutofacContainer.Resolve<IEnumerable<T>>();
        }

        public static T GetInstance<T>()
        {
            return AutofacContainer.Resolve<T>();
        }

        protected override void ConfigureContainer(ContainerBuilder builder)
        {

 

        }

        protected override void ConfigureBootstrapper()
        {
            base.ConfigureBootstrapper();
            EnforceNamespaceConvention = false;
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            var eventsAggr = AutofacContainer.Resolve<IEventAggregator>();

            Log.Logger = new LoggerConfiguration()
                .WriteTo.RollingFile("logs/log-{Date}.log")
                .WriteTo.Observers(o => o.Subscribe(Observer.Create<LogEvent>((le) =>
                {
                    eventsAggr.PublishOnUIThread(new LogEntryCreatedEvent(le));
                    if(le.Level == LogEventLevel.Error || le.Level == LogEventLevel.Fatal)
                        eventsAggr.PublishOnUIThread(new ErrorOccuredEvent(le.RenderMessage()));
                })))
                .CreateLogger();

            AutofacContainer.Resolve<ThemeService>();
            AutofacContainer.Resolve<ConfigService>();

            DisplayRootViewFor<ShellViewModel>();
        }
    }

}

