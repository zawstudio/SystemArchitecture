using Autofac;
using AMLSystem.DAL;
using Microsoft.EntityFrameworkCore;

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
                var optionsBuilder = new DbContextOptionsBuilder();
                optionsBuilder.UseMySql(_connectionString, ServerVersion.AutoDetect(_connectionString));
                return new AmlContext(optionsBuilder.Options);
            })
            .AsSelf()
            .InstancePerLifetimeScope();
    }
}