using System;
using System.Linq;
using EnglishLearning.FileManager.Application.Abstract;
using EnglishLearning.FileManager.Application.Models;
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

        public IFileManipulationService GetFileManipulationService(FileCreateModel fileCreateModel)
        {
            switch (fileCreateModel)
            {
                case { } model when model.Extension == Csv && !string.IsNullOrWhiteSpace(model.CsvColumnToRead):
                    return _serviceProvider.GetRequiredService<FromCsvColumnFileManipulationService>();
                case { } model when TextFileExtensions.Contains(model.Extension):
                    return _serviceProvider.GetRequiredService<TextFileManipulationService>();
                case { } model when model.Extension == Zip:
                    return _serviceProvider.GetRequiredService<ZipFileManipulationService>();
                default:
                    throw new ArgumentException("Incorrect fileExtension", nameof(fileCreateModel));
            }
        }
    }
}