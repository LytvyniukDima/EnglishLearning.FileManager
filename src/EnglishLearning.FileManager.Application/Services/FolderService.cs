using System.Collections.Generic;
using System.Threading.Tasks;
using EnglishLearning.FileManager.Application.Abstract;
using EnglishLearning.FileManager.Application.Models;
using EnglishLearning.FileManager.Persistence.Abstract;
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
        
        public async Task<IReadOnlyList<FolderModel>> GetChildFoldersAsync(int folderId)
        {
            var folders = await _folderRepository.FindAllAsync(x => x.ParentId == folderId);

            return folders.MapFileEntitiesToModels();
        }

        public async Task<FolderModel> CreateAsync(FolderCreateModel folderModel)
        {
            var folderEntity = MapFolderCreateModelToEntity(folderModel);
            var createdEntity = await _folderRepository.AddAsync(folderEntity);

            return MapFolderEntityToModel(createdEntity);
        }
    }
}
