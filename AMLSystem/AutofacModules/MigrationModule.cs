using AMLSystem.DAL.Migrations;
using Autofac;

namespace AMLSystem.AutofacModules;

public class MigrationModule : Module
{
    private readonly string _connectionString;

    public MigrationModule(string connectionString)
    {
        _connectionString = connectionString;
    }

    protected override void Load(ContainerBuilder builder)
    {
        builder.Register(_ =>
                new MigrationService(_connectionString))
            .AsSelf()
            .InstancePerLifetimeScope();
    }
}