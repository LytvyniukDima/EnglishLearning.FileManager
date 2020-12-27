using System.Collections.Generic;
using System.Threading.Tasks;
using EnglishLearning.FileManager.Persistence.Abstract;
using EnglishLearning.FileManager.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace EnglishLearning.FileManager.Persistence.Repositories
{
    internal class FolderEntityRepository : IFolderEntityRepository
    {
        private readonly FileManagerContext _dbContext;

        public FolderEntityRepository(FileManagerContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task AddAsync(FolderEntity folderEntity)
        {
            await _dbContext.Folders.AddAsync(folderEntity);
            await _dbContext.SaveChangesAsync();
        }

        public Task<FolderEntity> GetAsync(int id)
        {
            return _dbContext.Folders
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IReadOnlyList<FolderEntity>> GetAllAsync()
        {
            return await _dbContext.Folders
                .AsNoTracking()
                .ToListAsync();
        }
    }
}