using Autofac;
using Autofac.Extensions.DependencyInjection;
using AMLSystem.AutofacModules;
using AMLSystem.DAL.Migrations;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

IConfiguration configuration = new ConfigurationBuilder()
    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

var defaultConnectionString = configuration.GetConnectionString("DefaultConnection");
if (string.IsNullOrEmpty(defaultConnectionString))
    throw new InvalidOperationException("DefaultConnection string is null or empty. Check appsettings.json.");

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>(containerBuilder =>
    {
        containerBuilder.RegisterModule(new DatabaseModule(defaultConnectionString));
        containerBuilder.RegisterModule(new MigrationModule(defaultConnectionString));
        containerBuilder.RegisterModule(new ServiceModule());
    });

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("MediaService", new OpenApiInfo
    {
        Title = "Media Service API",
        Description = "API for managing media items, borrowing and returning them."
    });
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
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Media Service API v1"));
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();