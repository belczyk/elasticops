using Autofac;

namespace ElasticOps.Model
{
    public class ModelsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterType<ClusterInfo>().AsSelf();

        }
    }
}
