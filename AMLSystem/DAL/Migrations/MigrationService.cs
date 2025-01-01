using System.Reflection;
using DbUp;
using DbUp.Engine;
using Microsoft.EntityFrameworkCore;

namespace AMLSystem.DAL.Migrations;

public class MigrationService
{
    private readonly string _connectionString;

    public MigrationService(string connectionString)
    {
        _connectionString = connectionString;
    }

    public void UpdateDatabase()
    {
        var upgradeEngine = BuildUpgradeEngine();

        if (upgradeEngine.IsUpgradeRequired())
        {
            LogScriptsToUpdate(upgradeEngine);
            PerformUpgradeAndLog(upgradeEngine);
            return;
        }

        Console.WriteLine("Database is updated.");
    }

    public void ApplyMigrations()
    {
        var optionsBuilder = new DbContextOptionsBuilder<AmlContext>();
        optionsBuilder.UseMySql(_connectionString, ServerVersion.AutoDetect(_connectionString));

        using var context = new AmlContext(optionsBuilder.Options);
        context.Database.Migrate();
    }

    private UpgradeEngine BuildUpgradeEngine()
    {
        return DeployChanges.To
            .MySqlDatabase(_connectionString)
            .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
            .LogToConsole()
            .WithTransactionPerScript()
            .Build();
    }

    private static void LogScriptsToUpdate(UpgradeEngine upgradeEngine)
    {
        var scripts = upgradeEngine.GetScriptsToExecute();
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine("Database needs update.");
        Console.WriteLine($"Scripts to execute: {scripts.Count}");
        Console.ResetColor();
    }

    private static bool PerformUpgradeAndLog(UpgradeEngine upgradeEngine)
    {
        var result = upgradeEngine.PerformUpgrade();
        LogUpgradeResult(result);

        return result.Successful;
    }

    private static void LogUpgradeResult(DatabaseUpgradeResult result)
    {
        switch (result.Successful)
        {
            case true:
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Database updated successfully.");
                Console.ResetColor();
                break;
            default:
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(result.Error);
                Console.ResetColor();
                break;
        }
    }
}