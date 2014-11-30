
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Autofac;
using Caliburn.Micro;
using ElasticOps.Com;
using ElasticOps.ViewModels;
using ElasticOps.ViewModels.ManagmentScreens;
using Logary.Configuration;
using Logary.Targets;
using Console = System.Console;
using Logary;
using TextWriter = Logary.Targets.TextWriter;

namespace ElasticOps
{
    public class AppBootstrapper : Bootstrapper<ShellViewModel>
    {
        public static IContainer Container { get; private set; }

        protected override void Configure()
        {
            ConfigureContainer();
            ConfigureLogger();

            var logger = Logging.GetCurrentLogger();

            logger.Info("ElasticOps starts.");
        }

        private void ConfigureLogger()
        {
            
            LogaryFactory.New("ElasticOps",
                with => with.Target<TextWriter.Builder>(
                    "console1",
                    conf =>
                    conf.Target.WriteTo(File.CreateText("log.log"), File.CreateText("log-err.log"))
                        .MinLevel(LogLevel.Info)
                        .AcceptIf(line => true)
                        .SourceMatching(new Regex(".*"))
                    )
                    .Target<Debugger.Builder>("debugger")
                    .Target<Logstash.Builder>("logstash")
                );

        }

        private void ConfigureContainer()
        {

            var builder = new ContainerBuilder();
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

        public static IEnumerable<T> GetAllInstances<T>()
        {
            return Container.Resolve(typeof(IEnumerable<>).MakeGenericType(typeof(T))) as IEnumerable<T>;
        }

        public static T GetInstance<T>()
        {
            return Container.Resolve<T>();
        }

    }
}

