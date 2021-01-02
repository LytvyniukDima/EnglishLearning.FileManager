using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using EnglishLearning.FileManager.Application.Abstract;
using EnglishLearning.FileManager.Application.Configuration;
using EnglishLearning.FileManager.Application.Models;
using EnglishLearning.FileManager.Persistence.Abstract;
using Microsoft.Extensions.Options;
using static EnglishLearning.FileManager.Application.Infrastructure.ApplicationMapper;

namespace EnglishLearning.FileManager.Application.Services
{
    internal class FileService : IFileService
    {
        private readonly IFileEntityRepository _fileRepository;

        private readonly FileShareConfiguration _fileShareConfiguration;

        public FileService(
            IFileEntityRepository fileRepository,
            IOptions<FileShareConfiguration> fileShareConfiguration)
        {
            _fileRepository = fileRepository;
            _fileShareConfiguration = fileShareConfiguration.Value;
        }
        
        public async Task CreateFileAsync(Stream fileStream, FileCreateModel fileCreateModel)
        {
            var fileEntity = MapFileCreateModelToEntity(fileCreateModel);
            
            var createdFile = await _fileRepository.AddAsync(fileEntity);
            await SaveFile(fileStream, createdFile.Id);
        }

        public async Task<IReadOnlyList<FileModel>> GetAllByFolderId(int folderId)
        {
            var files = await _fileRepository.FindAllAsync(x => x.FolderId == folderId);

            return files.MapFileEntitiesToModels();
        }
        
        private async Task SaveFile(Stream fileStream, Guid id)
        {
            var fileName = id.ToString().ToUpper();
            var filePath = Path.Combine(_fileShareConfiguration.Path, fileName);

            using (var stream = File.Create(filePath))
            {
                fileStream.Seek(0, SeekOrigin.Begin);
                await fileStream.CopyToAsync(stream);
            }
        }
    }
}
