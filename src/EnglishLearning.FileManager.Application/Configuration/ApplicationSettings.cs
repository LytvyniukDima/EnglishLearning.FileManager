using EnglishLearning.FileManager.Application.Abstract;
using EnglishLearning.FileManager.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace EnglishLearning.FileManager.Application.Configuration
{
    public static class ApplicationSettings
    {
        public static IServiceCollection AddApplicationSettings(this IServiceCollection services)
        {
            services.AddTransient<ITreeService, TreeService>();
            
            return services;
        }
    }
}