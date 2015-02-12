using Autofac;
using ElasticOps.Behaviors.Suggesters;
using ElasticOps.Com;
using ElasticOps.Configuration;
using ElasticOps.Services;

namespace ElasticOps.DIModules
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
        "CA1812:AvoidUninstantiatedInternalClasses")]
    internal class InfrastractureModel : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CommandBus>().AsSelf();
            builder.RegisterType<ThemeService>().AsSelf().SingleInstance();
            builder.RegisterType<ConfigService>().AsSelf().SingleInstance();
            builder.RegisterType<ClusterDataCache>().AsSelf().SingleInstance();
            builder.RegisterType<UrlSuggestCollection>().AsSelf().SingleInstance();
            builder.RegisterType<IndexSuggestCollection>().AsSelf().SingleInstance();

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

            base.Load(builder);
        }
    }
}