using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EnglishLearning.FileManager.Persistence.Entities;

namespace EnglishLearning.FileManager.Persistence.Abstract
{
    public interface IFileEntityRepository
    {
        Task AddAsync(FileEntity file);

        Task<FileEntity> GetAsync(Guid id);

        Task<IReadOnlyList<FileEntity>> GetAllAsync();

        Task<IReadOnlyList<FileEntity>> FindAllAsync(Expression<Func<FileEntity, bool>> predicate);
    }
}
