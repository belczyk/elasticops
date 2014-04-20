
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Caliburn.Micro;
using ElasticOps.ViewModels;
using ElasticOps.ViewModels.ManagmentScreens;
using SimpleInjector;

namespace ElasticOps
{
    public class AppBootstrapper : Bootstrapper<ShellViewModel>
    {
        private Container container;

        /// <summary>
        /// Override to configure the framework and setup your IoC container.
        /// </summary>
        protected override void Configure()
        {
            container = new Container();
            container.Register<IWindowManager, WindowManager>();
            container.Register<IEventAggregator, EventAggregator>();
            var viewModels =
                Assembly.GetExecutingAssembly()
                    .DefinedTypes.Where(x => x.GetInterface(typeof(IManagmentScreen).Name) != null && !x.IsAbstract && x.IsClass);
            container.RegisterAll(typeof(IManagmentScreen), viewModels);
            container.Verify();
        }

        protected override object GetInstance(Type service, string key)
        {
            if (service == null)
            {
                var typeName = Assembly.GetExecutingAssembly().DefinedTypes.Where(x => x.Name.Contains(key)).Select(x => x.AssemblyQualifiedName).Single();

                service = Type.GetType(typeName);
            }
            return container.GetInstance(service);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            container.InjectProperties(instance);
        }
    }
}
