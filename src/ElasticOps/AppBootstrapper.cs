
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using Caliburn.Micro;
using ElasticOps.ViewModels;
using ElasticOps.ViewModels.ManagmentScreens;
using NLog;
using LogManager = NLog.LogManager;

namespace ElasticOps
{
    public class AppBootstrapper : Bootstrapper<ShellViewModel>
    {
        public static IContainer Container { get; private set; }
        private static Logger logger = LogManager.GetCurrentClassLogger();

        protected override void Configure()
        {
            logger.Info("App starts");

            var builder = new ContainerBuilder();

            builder.RegisterType<WindowManager>().As<IWindowManager>();
            builder.RegisterType<EventAggregator>().As<IEventAggregator>().SingleInstance();

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(x => !x.IsAbstract && x.IsClass && x.GetInterface(typeof(IManagmentScreen).Name) != null)
                .As<IManagmentScreen>();

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(x => x.IsClass && !x.IsAbstract && x.Name.EndsWith("ViewModel"))
                .AsSelf().InstancePerDependency();

            builder.RegisterAssemblyTypes(AssemblySource.Instance.ToArray())
              .Where(type => type.Name.EndsWith("View"))
              .AsSelf().InstancePerDependency();

            builder.RegisterType<Settings>().AsSelf().SingleInstance();

            Container = builder.Build();
        }

        protected override object GetInstance(System.Type service, string key)
        {
            object instance;
            if (string.IsNullOrWhiteSpace(key))
            {
                if (Container.TryResolve(service, out instance))
                    return instance;
            }
            else
            {
                if (Container.TryResolveNamed(key, service, out instance))
                    return instance;
            }
            throw new Exception(string.Format("Could not locate any instances of contract {0}.", key ?? service.Name));
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return Container.Resolve(typeof(IEnumerable<>).MakeGenericType(service)) as IEnumerable<object>;
        }

        protected override void BuildUp(object instance)
        {
            Container.InjectProperties(instance);
        }


    }
}
