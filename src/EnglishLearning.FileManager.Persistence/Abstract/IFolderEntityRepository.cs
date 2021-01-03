using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EnglishLearning.FileManager.Persistence.Entities;

namespace EnglishLearning.FileManager.Persistence.Abstract
{
    public interface IFolderEntityRepository
    {
        Task<FolderEntity> AddAsync(FolderEntity folderEntity);

        Task<FolderEntity> GetAsync(int id);

        Task<IReadOnlyList<FolderEntity>> GetAllAsync();
        
        Task<IReadOnlyList<FolderEntity>> FindAllAsync(Expression<Func<FolderEntity, bool>> predicate);
    }
}
