using System.Collections.Generic;
using System.Threading.Tasks;
using EnglishLearning.FileManager.Persistence.Entities;

namespace EnglishLearning.FileManager.Persistence.Abstract
{
    public interface IFolderEntityRepository
    {
        Task AddAsync(FolderEntity folderEntity);

        Task<FolderEntity> GetAsync(int id);

        Task<IReadOnlyList<FolderEntity>> GetAllAsync();
    }
}