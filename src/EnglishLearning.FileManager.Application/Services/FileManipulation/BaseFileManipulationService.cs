using System;
using System.IO;
using System.Threading.Tasks;
using EnglishLearning.FileManager.Application.Abstract;
using EnglishLearning.FileManager.Application.Configuration;
using EnglishLearning.FileManager.Application.Models;
using Microsoft.Extensions.Options;

namespace EnglishLearning.FileManager.Application.Services.FileManipulation
{
    internal abstract class BaseFileManipulationService : IFileManipulationService
    {
        protected readonly FileShareConfiguration _fileShareConfiguration;
        
        public BaseFileManipulationService(IOptions<FileShareConfiguration> fileShareConfiguration)
        {
            _fileShareConfiguration = fileShareConfiguration.Value;
        }
        
        public abstract Task CreateFile(Stream fileStream, FileCreateModel fileCreateModel);
        
        protected async Task SaveFile(Stream fileStream, Guid id)
        {
            var fileName = id.ToString().ToUpper();
            var filePath = Path.Combine(_fileShareConfiguration.Path, fileName);

            using (var stream = File.Create(filePath))
            {
                if (fileStream.CanSeek)
                {
                    fileStream.Seek(0, SeekOrigin.Begin);    
                }
                
                await fileStream.CopyToAsync(stream);
            }
        }
    }
}