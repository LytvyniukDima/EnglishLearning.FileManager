using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using EnglishLearning.FileManager.Application.Abstract;
using EnglishLearning.FileManager.Application.Configuration;
using EnglishLearning.FileManager.Application.Infrastructure;
using EnglishLearning.FileManager.Application.Models;
using EnglishLearning.FileManager.Persistence.Abstract;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using static EnglishLearning.FileManager.Application.Constants.FileConstants;
using static EnglishLearning.FileManager.Application.Infrastructure.ApplicationMapper;

namespace EnglishLearning.FileManager.Application.Services
{
    internal class FileService : IFileService
    {
        private readonly IFileEntityRepository _fileRepository;

        private readonly IFolderService _folderService;
        
        private readonly FileShareConfiguration _fileShareConfiguration;

        private readonly ILogger<FileService> _logger;

        public FileService(
            IFileEntityRepository fileRepository,
            IFolderService folderService,
            IOptions<FileShareConfiguration> fileShareConfiguration,
            ILogger<FileService> logger)
        {
            _fileRepository = fileRepository;
            _folderService = folderService;
            _fileShareConfiguration = fileShareConfiguration.Value;
            _logger = logger;
        }
        
        public async Task<IReadOnlyList<FileModel>> GetAllByFolderId(int folderId)
        {
            var files = await _fileRepository.FindAllAsync(x => x.FolderId == folderId);

            return files.MapFileEntitiesToModels();
        }

        public async Task<FileModel> GetAsync(Guid id)
        {
            var file = await _fileRepository.GetAsync(id);

            return MapFileEntityToModel(file);
        }

        public async Task<IReadOnlyList<FileModel>> GetAllFromFolderAsync(int folderId)
        {
            var allChildFolders = await _folderService.GetAllChildFoldersAsync(folderId);
            var folderIds = allChildFolders
                .Select(x => x.Id)
                .ToList();
            folderIds.Add(folderId);

            var files = await _fileRepository
                .FindAllAsync(x => folderIds.Contains(x.FolderId));
            
            return files.MapFileEntitiesToModels();
        }

        public async Task CreateFileAsync(Stream fileStream, FileCreateModel fileCreateModel)
        {
            if (TextFileExtensions.Contains(fileCreateModel.Extension))
            {
                await CreatFromTextFile(fileStream, fileCreateModel);
            }
            else
            {
                await CreateFromArchiveFile(fileStream, fileCreateModel);
            }
        }

        private async Task CreatFromTextFile(Stream fileStream, FileCreateModel fileCreateModel)
        {
            var fileEntity = MapFileCreateModelToEntity(fileCreateModel);
            
            var createdFile = await _fileRepository.AddAsync(fileEntity);
            await SaveFile(fileStream, createdFile.Id);
        }

        private async Task CreateFromArchiveFile(Stream fileStream, FileCreateModel fileCreateModel)
        {
            var folderDictionary = new Dictionary<string, FolderModel>();
            
            using (ZipArchive archive = new ZipArchive(fileStream, ZipArchiveMode.Read))
            {
                var textEntries = archive.Entries
                    .Where(x =>
                    {
                        var fileExtension = FileExtensions.GetFileExtension(x.FullName);
                        return TextFileExtensions.Contains(fileExtension);
                    })
                    .ToList();

                if (textEntries.Any())
                {
                    var folderCreateModel = new FolderCreateModel
                    {
                        Name = fileCreateModel.Name,
                        ParentId = fileCreateModel.FolderId,
                    };
                
                    var createdFolder = await _folderService.CreateAsync(folderCreateModel);
                    folderDictionary.Add(createdFolder.Name, createdFolder);
                }
                
                foreach (ZipArchiveEntry entry in textEntries)
                {
                    var fullPath = Path.GetDirectoryName(entry.FullName);
                    
                    var splittedPath = fullPath.Split(Path.DirectorySeparatorChar);
                    splittedPath[0] = fileCreateModel.Name;
                    var newPath = string.Join(Path.DirectorySeparatorChar, splittedPath);

                    if (!folderDictionary.TryGetValue(newPath, out var folderModel))
                    {
                        await CreateFoldersIfNotExists(newPath, folderDictionary);
                        folderModel = folderDictionary[newPath];
                    }
                    
                    var newFile = new FileCreateModel
                    {
                        CreatedBy = fileCreateModel.CreatedBy,
                        Extension = FileExtensions.GetFileExtension(entry.FullName),
                        FolderId = folderModel.Id,
                        LastModified = fileCreateModel.LastModified,
                        Metadata = fileCreateModel.Metadata,
                        Name = Path.GetFileNameWithoutExtension(entry.Name),
                    };

                    await using var newFileStream = entry.Open();
                    await CreatFromTextFile(newFileStream, newFile);
                }
            }   
        }

        private async Task SaveFile(Stream fileStream, Guid id)
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

        private async Task CreateFoldersIfNotExists(
            string path,
            Dictionary<string, FolderModel> folderDictionary)
        {
            var splittedPath = path.Split(Path.DirectorySeparatorChar);
            var folderPath = splittedPath[0];
            
            for (var i = 1; i < splittedPath.Length; i++)
            {
                var newPath = Path.Combine(folderPath, splittedPath[i]);
                if (!folderDictionary.ContainsKey(newPath))
                {
                    var parentFolder = folderDictionary[folderPath];
                    
                    var newFolder = new FolderCreateModel
                    {
                        Name = splittedPath[i],
                        ParentId = parentFolder.Id,
                    };
                    var createdFolder = await _folderService.CreateAsync(newFolder);
                    
                    folderDictionary.Add(newPath, createdFolder);
                }

                folderPath = newPath;
            }
        }
    }
}
