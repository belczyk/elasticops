
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Primitives;
using System.Linq;
using System.Reflection;
using System.Windows;
using Autofac;
using Caliburn.Micro;
using ElasticOps.Com.Infrastructure;
using ElasticOps.ViewModels;
using ElasticOps.ViewModels.ManagmentScreens;
using MahApps.Metro.Controls;
using Nest;
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
                .Where(x => x.BaseType == typeof (UserSettings))
                .As<UserSettings>();


            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(x => x.IsClass && !x.IsAbstract && x.Name.EndsWith("ViewModel"))
                .AsSelf().InstancePerDependency();

            builder.RegisterAssemblyTypes(AssemblySource.Instance.ToArray())
              .Where(type => type.Name.EndsWith("View"))
              .AsSelf().InstancePerDependency();
            builder.RegisterType<CommandBus>().AsSelf();
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



//type CommandResult<'T when 'T : null> (result : 'T, success : Boolean, errorMessage : String) =
//    member x.Result = result
//    member x.Success = success 
//    member x.ErrorMessage = errorMessage 
//    member x.Failed 
//        with get() = not x.Success
        
//    new (result : 'T) = CommandResult(result,true,null)
//    new (errorMessage : string ) = CommandResult(null,false,errorMessage)