using System;
using EnglishLearning.FileManager.SqlMigrations.Configuration;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;

namespace EnglishLearning.FileManager.SqlMigrations
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = ServiceCollectionExtensions.CreateServices();
            
            using (var scope = serviceProvider.CreateScope())
            {
                UpdateDatabase(scope.ServiceProvider);
            }
        }
        
        private static void UpdateDatabase(IServiceProvider serviceProvider)
        {
            var runner = serviceProvider.GetRequiredService<IMigrationRunner>();

            runner.MigrateUp();
        }
    }
}