using EnglishLearning.FileManager.Persistence.Abstract;
using EnglishLearning.FileManager.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EnglishLearning.FileManager.Persistence.Configuration
{
    public static class PersistenceSettings
    {
        public static IServiceCollection AddPersistenceSettings(this IServiceCollection services, IConfiguration configuration)
        {
            var databaseConfiguration = configuration.GetDatabaseConfiguration();
            services.AddSingleton(databaseConfiguration);

            services.AddDbContext<FileManagerContext>((sp, options) =>
            {
                var dbConfiguration = sp.GetRequiredService<DatabaseConfiguration>();
                options.UseSqlServer(dbConfiguration.ConnectionString);
            });

            services.AddScoped<IFolderRepository, FolderRepository>(); 
            
            return services;
        }
        
        private static DatabaseConfiguration GetDatabaseConfiguration(this IConfiguration configuration)
        {
            var databaseSettings = new DatabaseConfiguration();
            databaseSettings.ConnectionString = configuration.GetConnectionString("SqlServer");
            
            return databaseSettings;
        }
    }
}