using System;
using System.Linq;
using EnglishLearning.FileManager.Application.Abstract;
using Microsoft.Extensions.DependencyInjection;
using static EnglishLearning.FileManager.Application.Constants.FileConstants;

namespace EnglishLearning.FileManager.Application.Services.FileManipulation
{
    internal class FileManipulationServiceFactory : IFileManipulationServiceFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public FileManipulationServiceFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IFileManipulationService GetFileManipulationService(string fileExtension)
        {
            if (TextFileExtensions.Contains(fileExtension))
            {
                return _serviceProvider.GetRequiredService<TextFileManipulationService>();
            }
            else
            {
                return _serviceProvider.GetRequiredService<ZipFileManipulationService>();
            }
        }
    }
}