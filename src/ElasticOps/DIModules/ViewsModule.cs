using System;
using System.Linq;
using System.Reflection;
using Autofac;
using Caliburn.Micro;
using ElasticOps.ViewModels.ManagementScreens;
using Module = Autofac.Module;

namespace ElasticOps.DIModules
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
        "CA1812:AvoidUninstantiatedInternalClasses")]
    internal class ViewsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(x => !x.IsAbstract && x.IsClass && x.GetInterface(typeof (IManagementScreen).Name) != null)
                .As<IManagementScreen>();

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(x => x.IsClass && !x.IsAbstract && x.Name.EndsWith("ViewModel", StringComparison.Ordinal))
                .AsSelf().InstancePerDependency();

            builder.RegisterAssemblyTypes(AssemblySource.Instance.ToArray())
                .Where(type => type.Name.EndsWith("View", StringComparison.Ordinal))
                .AsSelf().InstancePerDependency();

            base.Load(builder);
        }
    }
}