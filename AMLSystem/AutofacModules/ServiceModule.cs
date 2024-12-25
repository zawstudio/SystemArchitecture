using AMLSystem.DAL;
using Autofac;
using AMLSystem.DAL.Interfaces;
using AMLSystem.DAL.Repositories;
using AMLSystem.Services;

namespace AMLSystem.AutofacModules;

public class ServiceModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<MediaRepository>().As<IMediaRepository>().InstancePerLifetimeScope();
        builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
        builder.RegisterType<MediaService>().AsSelf().InstancePerLifetimeScope();
    }
}