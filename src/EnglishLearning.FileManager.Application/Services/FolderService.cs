using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnglishLearning.FileManager.Application.Abstract;
using EnglishLearning.FileManager.Application.Models;
using EnglishLearning.FileManager.Application.Models.Tree;
using EnglishLearning.FileManager.Persistence.Abstract;
using EnglishLearning.FileManager.Persistence.Entities;
using static EnglishLearning.FileManager.Application.Infrastructure.ApplicationMapper;

namespace EnglishLearning.FileManager.Application.Services
{
    public class FolderService : IFolderService
    {
        private readonly IFolderEntityRepository _folderRepository;

        public FolderService(IFolderEntityRepository folderRepository)
        {
            _folderRepository = folderRepository;
        }
        
        public async Task<IReadOnlyList<FolderModel>> GetChildFoldersAsync(int? folderId)
        {
            var folders = await _folderRepository.FindAllAsync(x => x.ParentId == folderId);

            return folders.MapFolderEntitiesToModels();
        }

        public async Task<FolderModel> CreateAsync(FolderCreateModel folderModel)
        {
            var folderEntity = MapFolderCreateModelToEntity(folderModel);
            var createdEntity = await _folderRepository.AddAsync(folderEntity);

            return MapFolderEntityToModel(createdEntity);
        }
        
        public async Task<FolderInfoModel> GetFolderInfoAsync(int folderId)
        {
            var folders = await _folderRepository.GetAllAsync();
            var foldersDictionary = folders.ToDictionary(x => x.Id);
            var folder = foldersDictionary[folderId];
            
            return new FolderInfoModel
            {
                Id = folder.Id,
                Name = folder.Name,
                Path = GetFolderPath(folder.ParentId, foldersDictionary),
            };
        }

        public async Task<IReadOnlyList<FolderModel>> GetAllChildFoldersAsync(int? folderId)
        {
            static IReadOnlyList<FolderEntity> GetAllChildFolders(
                int? currentFolderId,
                ILookup<int?, FolderEntity> folderLookup)
            {
                var allFolders = new List<FolderEntity>();
                IReadOnlyList<FolderEntity> currentFolders = Array.Empty<FolderEntity>();
                if (folderLookup.Contains(currentFolderId))
                {
                    currentFolders = folderLookup[currentFolderId].ToList();
                }
                
                allFolders.AddRange(currentFolders);
                foreach (var folder in currentFolders)
                {
                    var childFolders = GetAllChildFolders(folder.Id, folderLookup);
                    allFolders.AddRange(childFolders);
                }

                return allFolders;
            }
            
            var folders = await _folderRepository.GetAllAsync();
            var folderLookup = folders
                .ToLookup(x => x.ParentId);

            var allFolders = GetAllChildFolders(folderId, folderLookup); 
            
            return allFolders.MapFolderEntitiesToModels();
        }
        
        public async Task<IReadOnlyList<FolderTreeItem>> GetFolderTreeItemsAsync()
        {
            var folders = await _folderRepository.GetAllAsync();
            
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
    }
}
