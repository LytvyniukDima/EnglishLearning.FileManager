using System;
using EnglishLearning.FileManager.SqlMigrations.Migrations;
using FluentMigrator.Runner;
using FluentMigrator.Runner.Initialization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EnglishLearning.FileManager.SqlMigrations.Configuration
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceProvider CreateServices()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();
            
            var connectionString = configuration.GetConnectionString("SqlServer");

            Console.WriteLine($"Connection string: {connectionString}");
            
            var services = new ServiceCollection();
            services.AddSqlServerServices(connectionString);

            return services.BuildServiceProvider(true);
        }
        
        private static IServiceCollection AddSqlServerServices(this IServiceCollection services, string connectionString)
        {
            return services
                .AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    .AddSqlServer()
                    .WithGlobalConnectionString(connectionString)
                    .ScanIn(typeof(Migration_001_AddFoldersTable).Assembly).For.Migrations())
                .AddSingleton<IAssemblySourceItem>(new AssemblySourceItem(typeof(Migration_001_AddFoldersTable).Assembly))
                .AddLogging(lb => lb.AddFluentMigratorConsole());
        }
    }
}