using EnglishLearning.FileManager.Application.Abstract;
using EnglishLearning.FileManager.Application.Services;
using EnglishLearning.FileManager.Application.Services.FileManipulation;
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
                .AddTransient<IFileService, FileService>()
                .AddTransient<IFolderService, FolderService>()
                .AddTransient<IFileUpdateService, FileUpdateService>()
                .AddTransient<IHtmlParser, HtmlParser>();

            services.Configure<FileShareConfiguration>(configuration.GetSection(nameof(FileShareConfiguration)));

            services
                .AddTransient<IFileManipulationServiceFactory, FileManipulationServiceFactory>()
                .AddTransient<TextFileManipulationService>()
                .AddTransient<ZipFileManipulationService>()
                .AddTransient<FromCsvColumnFileManipulationService>();
            
            return services;
        }
    }
}
