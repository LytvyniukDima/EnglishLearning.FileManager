using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnglishLearning.FileManager.Application.Abstract;
using EnglishLearning.FileManager.Application.Models.Tree;
using EnglishLearning.FileManager.Persistence.Abstract;
using EnglishLearning.FileManager.Persistence.Entities;

namespace EnglishLearning.FileManager.Application.Services
{
    internal class TreeService : ITreeService
    {
        private readonly IFolderService _folderService;

        private readonly IFileEntityRepository _fileRepository;

        public TreeService(
            IFolderService folderService,
            IFileEntityRepository fileRepository)
        {
            _folderService = folderService;
            _fileRepository = fileRepository;
        }
        
        public async Task<FileTree> GetTreeAsync()
        {
            var files = await _fileRepository.GetAllAsync();

            var folderItems = await _folderService.GetFolderTreeItemsAsync();
            var fileItems = GetFileItems(files, folderItems);

            return new FileTree
            {
                Folders = folderItems,
                Files = fileItems,
            };
        }

        private static IReadOnlyList<FileTreeItem> GetFileItems(
            IReadOnlyList<FileEntity> files,
            IReadOnlyList<FolderTreeItem> mappedFolders)
        {
            var folderDictionary = mappedFolders.ToDictionary(x => x.Id);

            return files
                .Select(x => new FileTreeItem
                {
                    Id = x.Id,
                    Name = x.Name,
                    Extension = x.Extension,
                    LastModified = x.LastModified,
                    CreatedBy = x.CreatedBy,
                    Metadata = x.Metadata,
                    Path = GetFilePath(x.FolderId, folderDictionary),
                })
                .ToList();
        }

        private static IReadOnlyList<string> GetFilePath(
            int? folderId,
            IReadOnlyDictionary<int, FolderTreeItem> folderDictionary)
        {
            if (folderId.HasValue)
            {
                var folder = folderDictionary[folderId.Value];
                return folder
                    .Path
                    .Concat(new[] { folder.Name })
                    .ToList();   
            }
            else
            {
                return Array.Empty<string>();
            }
        }
    }
}
