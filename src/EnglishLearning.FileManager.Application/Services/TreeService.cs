using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnglishLearning.FileManager.Application.Abstract;
using EnglishLearning.FileManager.Application.Models;
using EnglishLearning.FileManager.Persistence.Abstract;
using EnglishLearning.FileManager.Persistence.Entities;

namespace EnglishLearning.FileManager.Application.Services
{
    internal class TreeService : ITreeService
    {
        private readonly IFolderEntityRepository _folderRepository;

        private readonly IFileEntityRepository _fileRepository;

        public TreeService(
            IFolderEntityRepository folderRepository,
            IFileEntityRepository fileRepository)
        {
            _folderRepository = folderRepository;
            _fileRepository = fileRepository;
        }
        
        public async Task<FileTree> GetTreeAsync()
        {
            var folders = await _folderRepository.GetAllAsync();
            var files = await _fileRepository.GetAllAsync();

            var folderItems = GetFolderItems(folders);
            var fileItems = GetFileItems(files, folderItems);

            return new FileTree
            {
                Folders = folderItems,
                Files = fileItems,
            };
        }

        private static IReadOnlyList<FolderTreeItem> GetFolderItems(IReadOnlyList<FolderEntity> folders)
        {
            var folderDictionary = folders.ToDictionary(x => x.Id);
            return folders
                .Select(x => new FolderTreeItem
                {
                    Id = x.Id,
                    Name = x.Name,
                    Path = GetFolderPath(x.ParentId, folderDictionary),
                })
                .ToList();
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
                    LastModified = x.LastModified,
                    CreatedBy = x.CreatedBy,
                    Metadata = x.Metadata,
                    Path = GetFilePath(x.FolderId, folderDictionary),
                })
                .ToList();
        }
        
        private static IReadOnlyList<string> GetFolderPath(
            int? parentId,
            IReadOnlyDictionary<int, FolderEntity> folderDictionary)
        {
            var path = new List<string>();
            while (parentId.HasValue)
            {
                var parent = folderDictionary[parentId.Value];
                path.Add(parent.Name);
                parentId = parent.ParentId;
            }

            path.Reverse();
            return path;
        }

        private static IReadOnlyList<string> GetFilePath(
            int folderId,
            IReadOnlyDictionary<int, FolderTreeItem> folderDictionary)
        {
            var folder = folderDictionary[folderId];
            return folder
                .Path
                .Concat(new[] { folder.Name })
                .ToList();
        }
    }
}
