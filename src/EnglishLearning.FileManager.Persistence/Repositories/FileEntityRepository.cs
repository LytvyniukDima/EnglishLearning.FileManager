using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EnglishLearning.FileManager.Persistence.Abstract;
using EnglishLearning.FileManager.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace EnglishLearning.FileManager.Persistence.Repositories
{
    internal class FileEntityRepository : IFileEntityRepository
    {
        private readonly FileManagerContext _dbContext;

        public FileEntityRepository(FileManagerContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task AddAsync(FileEntity file)
        {
            await _dbContext.AddAsync(file);
            await _dbContext.SaveChangesAsync();
        }

        public Task<FileEntity> GetAsync(Guid id)
        {
            return _dbContext.Files
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IReadOnlyList<FileEntity>> GetAllAsync()
        {
            return await _dbContext.Files
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IReadOnlyList<FileEntity>> FindAllAsync(Expression<Func<FileEntity, bool>> predicate)
        {
            return await _dbContext.Files
                .AsNoTracking()
                .Where(predicate)
                .ToListAsync();
        }
    }
}
