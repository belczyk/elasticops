
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using Autofac;
using Autofac.Core;
using Caliburn.Micro;
using Caliburn.Micro.Autofac;
using ElasticOps.Com;
using ElasticOps.ViewModels;
using ElasticOps.ViewModels.ManagmentScreens;
using Serilog;
using Serilog.Events;

namespace ElasticOps
{
    public class AppBootstrapper : AutofacBootstrapper<ShellViewModel>
    {
        private static IContainer AContainer { get; set; }
        public AppBootstrapper()
        {
            Initialize();
            AContainer = Container;
        }

        public static IEnumerable<T> GetAllInstances<T>()
        {
            return AContainer.Resolve<IEnumerable<T>>();
        }

        public static T GetInstance<T>()
        {
            return AContainer.Resolve<T>();
        }

        protected override void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterType<WindowManager>().As<IWindowManager>();
            builder.RegisterType<EventAggregator>().As<IEventAggregator>().SingleInstance();

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(x => !x.IsAbstract && x.IsClass && x.GetInterface(typeof(IManagmentScreen).Name) != null)
                .As<IManagmentScreen>();

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
            builder.RegisterType<RESTClient>().As<IRESTClient>();
            builder.RegisterType<Settings>().AsSelf().SingleInstance();

            builder.RegisterType<Infrastructure>().AsSelf();

        }

        protected override void ConfigureBootstrapper()
        {
            base.ConfigureBootstrapper();
            EnforceNamespaceConvention = false;
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.RollingFile("logs/log-{Date}.log")
                .CreateLogger();

            DisplayRootViewFor<ShellViewModel>();
        }
    }

}

