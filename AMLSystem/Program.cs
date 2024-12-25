using Autofac;
using Autofac.Extensions.DependencyInjection;
using AMLSystem.AutofacModules;
using AMLSystem.DAL.Migrations;

var builder = WebApplication.CreateBuilder(args);

IConfiguration configuration = new ConfigurationBuilder()
    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
    .AddJsonFile("appsettings.json")
    .Build();
var defaultConnectionString = configuration.GetConnectionString("DefaultConnection");
if (string.IsNullOrEmpty(defaultConnectionString))
    throw new InvalidOperationException("DefaultConnection string is null or empty. Check appsettings.json.");

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>(containerBuilder =>
    {
        containerBuilder.RegisterModule(new DatabaseModule(defaultConnectionString));
        containerBuilder.RegisterModule(new MigrationModule(defaultConnectionString));
    });

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var migrationService = scope.ServiceProvider.GetRequiredService<MigrationService>();
    migrationService.UpdateDatabase();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();