using System.Collections.Generic;
using System.Threading.Tasks;
using EnglishLearning.FileManager.Persistence.Abstract;
using EnglishLearning.FileManager.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace EnglishLearning.FileManager.Persistence.Repositories
{
    internal class FolderRepository : IFolderRepository
    {
        private readonly FileManagerContext _dbContext;

        public FolderRepository(FileManagerContext dbContext)
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
                .Include(x => x.Parent)
                .ThenInclude(x => x.Parent.Parent)
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IReadOnlyList<FolderEntity>> GetAllAsync()
        {
            return await _dbContext.Folders.ToListAsync();
        }
    }
}