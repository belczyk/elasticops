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
            builder.RegisterType<WindowManager>().As<IWindowManager>();
            builder.RegisterType<EventAggregator>().As<IEventAggregator>().SingleInstance();

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(x => !x.IsAbstract && x.IsClass && x.GetInterface(typeof(IManagementScreen).Name) != null)
                .As<IManagementScreen>();

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(x => x.BaseType == typeof(UserSettings))
                .As<UserSettings>();


            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(x => x.IsClass && !x.IsAbstract && x.Name.EndsWith("ViewModel"))
                .AsSelf().InstancePerDependency();

            builder.RegisterAssemblyTypes(AssemblySource.Instance.ToArray())
              .Where(type => type.Name.EndsWith("View"))
              .AsSelf().InstancePerDependency();
            builder.RegisterType<CommandBus>().AsSelf();
            builder.RegisterType<ThemeService>().AsSelf().SingleInstance();
            builder.RegisterType<ConfigService>().AsSelf().SingleInstance();
            builder.RegisterType<ClusterDataCache>().AsSelf().SingleInstance();
            builder.RegisterType<UrlSuggest>().AsSelf().SingleInstance();
            builder.RegisterType<IndexSuggest>().AsSelf().SingleInstance();
            builder.Register((c) =>
            {
                var config = ConfigLoaders.LoadElasticOpsConfig();
                return config;
            }).As<ElasticOpsConfig>().SingleInstance();

            builder.Register((c) =>
            {
                var config = ConfigLoaders.LoadIntellisenseConfig();
                return config;
            }).As<IntellisenseConfig>().SingleInstance();

            builder.RegisterType<Infrastructure>().AsSelf().SingleInstance();

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

