using Autofac;
using Microsoft.EntityFrameworkCore;
using AMLSystem.DAL;

namespace AMLSystem.AutofacModules;

public class DatabaseModule : Module
{
    private readonly string _connectionString;

    public DatabaseModule(string connectionString)
    {
        _connectionString = connectionString;
    }

    protected override void Load(ContainerBuilder builder)
    {
        builder.Register(_ =>
            {
                var optionsBuilder = new DbContextOptionsBuilder<AmlContext>();
                optionsBuilder.UseMySql(_connectionString, ServerVersion.AutoDetect(_connectionString));
                return new AmlContext(optionsBuilder.Options);
            })
            .As<AmlContext>()
            .InstancePerLifetimeScope();
    }
}