using EnglishLearning.FileManager.Application.Abstract;
using EnglishLearning.FileManager.Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EnglishLearning.FileManager.Application.Configuration
{
    public static class ApplicationSettings
    {
        public static IServiceCollection AddApplicationSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddTransient<ITreeService, TreeService>()
                .AddTransient<IFileService, FileService>();

            services.Configure<FileShareConfiguration>(configuration.GetSection(nameof(FileShareConfiguration)));
            
            return services;
        }
    }
}
