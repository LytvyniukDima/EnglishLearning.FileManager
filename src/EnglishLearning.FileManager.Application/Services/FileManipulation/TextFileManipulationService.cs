using System.IO;
using System.Threading.Tasks;
using EnglishLearning.FileManager.Application.Configuration;
using EnglishLearning.FileManager.Application.Models;
using EnglishLearning.FileManager.Persistence.Abstract;
using Microsoft.Extensions.Options;
using static EnglishLearning.FileManager.Application.Infrastructure.ApplicationMapper;

namespace EnglishLearning.FileManager.Application.Services.FileManipulation
{
    internal class TextFileManipulationService : BaseFileManipulationService
    {
        private readonly IFileEntityRepository _fileRepository;
        
        public TextFileManipulationService(
            IOptions<FileShareConfiguration> fileShareConfiguration,
            IFileEntityRepository fileRepository)
            : base(fileShareConfiguration)
        {
            _fileRepository = fileRepository;
        }

        public override async Task CreateFile(Stream fileStream, FileCreateModel fileCreateModel)
        {
            var fileEntity = MapFileCreateModelToEntity(fileCreateModel);
            
            var createdFile = await _fileRepository.AddAsync(fileEntity);
            await SaveFile(fileStream, createdFile.Id);
        }
    }
}