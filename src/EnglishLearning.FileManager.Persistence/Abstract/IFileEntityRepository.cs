using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EnglishLearning.FileManager.Persistence.Entities;

namespace EnglishLearning.FileManager.Persistence.Abstract
{
    public interface IFileEntityRepository
    {
        Task AddAsync(FileEntity file);

        Task<FileEntity> Get(Guid id);

        Task<IReadOnlyList<FileEntity>> GetAll();
    }
}
