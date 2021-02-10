using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EnglishLearning.FileManager.Application.Abstract;
using EnglishLearning.FileManager.Application.Configuration;
using EnglishLearning.FileManager.Application.Infrastructure;
using EnglishLearning.FileManager.Application.Models;
using EnglishLearning.FileManager.Persistence.Abstract;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using static EnglishLearning.FileManager.Application.Infrastructure.ApplicationMapper;

namespace EnglishLearning.FileManager.Application.Services
{
    internal class FileService : IFileService
    {
        private readonly IFileEntityRepository _fileRepository;

        private readonly IFolderService _folderService;
        
        private readonly FileShareConfiguration _fileShareConfiguration;

        private readonly ILogger<FileService> _logger;

        private readonly IFileManipulationServiceFactory _fileManipulationServiceFactory;
        
        public FileService(
            IFileEntityRepository fileRepository,
            IFolderService folderService,
            IOptions<FileShareConfiguration> fileShareConfiguration,
            IFileManipulationServiceFactory fileManipulationServiceFactory,
            ILogger<FileService> logger)
        {
            _fileRepository = fileRepository;
            _folderService = folderService;
            _fileShareConfiguration = fileShareConfiguration.Value;
            _fileManipulationServiceFactory = fileManipulationServiceFactory;
            _logger = logger;
        }
        
        public async Task<IReadOnlyList<FileModel>> GetAllByFolderId(int? folderId)
        {
            var files = await _fileRepository.FindAllAsync(x => x.FolderId == folderId);

            return files.MapFileEntitiesToModels();
        }

        public async Task<Stream> GetFileContentAsync(Guid id)
        {
            var fileName = id.ToString().ToUpper();
            var filePath = Path.Combine(_fileShareConfiguration.Path, fileName);
            var memoryStream = new MemoryStream();
            
            using (var stream = File.OpenRead(filePath))
            {
                await stream.CopyToAsync(memoryStream);
            }

            memoryStream.Seek(0, SeekOrigin.Begin);
            
            return memoryStream;
        }

        public async Task<FileDetailedModel> GetFileDetailedModelAsync(Guid id)
        {
            var file = await _fileRepository.GetAsync(id);
            IReadOnlyList<string> path;

            if (!file.FolderId.HasValue)
            {
                path = Array.Empty<string>();
            }
            else
            {
                var folderInfo = await _folderService.GetFolderInfoAsync(file.FolderId.Value);
                path = folderInfo.Path.Concat(new [] { folderInfo.Name }).ToList();
            }

            return MapFileEntityToDetailedModel(file, path);
        }

        public async Task<IReadOnlyList<FileModel>> GetAllFromFolderAsync(int? folderId)
        {
            var allChildFolders = await _folderService.GetAllChildFoldersAsync(folderId);
            var folderIds = allChildFolders
                .Select(x => x.Id as int?)
                .ToList();
            folderIds.Add(folderId);

            var files = await _fileRepository
                .FindAllAsync(x => folderIds.Contains(x.FolderId));
            
            return files.MapFileEntitiesToModels();
        }

        public async Task CreateFileAsync(Stream fileStream, FileCreateModel fileCreateModel)
        {
            var fileManipulationService = _fileManipulationServiceFactory.GetFileManipulationService(fileCreateModel.Extension);

            await fileManipulationService.CreateFile(fileStream, fileCreateModel);
        }
    }
}
