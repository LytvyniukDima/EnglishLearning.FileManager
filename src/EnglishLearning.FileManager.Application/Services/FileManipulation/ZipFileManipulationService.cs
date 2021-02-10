using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using EnglishLearning.FileManager.Application.Abstract;
using EnglishLearning.FileManager.Application.Configuration;
using EnglishLearning.FileManager.Application.Extensions;
using EnglishLearning.FileManager.Application.Models;
using Microsoft.Extensions.Options;
using static EnglishLearning.FileManager.Application.Constants.FileConstants;

namespace EnglishLearning.FileManager.Application.Services.FileManipulation
{
    internal class ZipFileManipulationService : BaseFileManipulationService
    {
        private readonly IFolderService _folderService;

        private readonly TextFileManipulationService _textFileManipulationService;
        
        public ZipFileManipulationService(
            IOptions<FileShareConfiguration> fileShareConfiguration,
            IFolderService folderService,
            TextFileManipulationService textFileManipulationService)
            : base(fileShareConfiguration)
        {
            _folderService = folderService;
            _textFileManipulationService = textFileManipulationService;
        }

        public override async Task CreateFile(Stream fileStream, FileCreateModel fileCreateModel)
        {
            var folderDictionary = new Dictionary<string, FolderModel>();
            
            using (ZipArchive archive = new ZipArchive(fileStream, ZipArchiveMode.Read))
            {
                var textEntries = archive.Entries
                    .Where(x =>
                    {
                        var fileExtension = x.FullName.GetFileExtension();
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
                        Extension = entry.FullName.GetFileExtension(),
                        FolderId = folderModel.Id,
                        LastModified = fileCreateModel.LastModified,
                        Metadata = fileCreateModel.Metadata,
                        Name = Path.GetFileNameWithoutExtension(entry.Name),
                    };

                    await using var newFileStream = entry.Open();
                    await _textFileManipulationService.CreateFile(newFileStream, newFile);
                }
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