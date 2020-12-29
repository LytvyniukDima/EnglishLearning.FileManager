using System.Collections.Generic;
using System.Threading.Tasks;
using EnglishLearning.FileManager.Application.Models;

namespace EnglishLearning.FileManager.Application.Abstract
{
    public interface IFolderService
    {
        Task<IReadOnlyList<FolderModel>> GetChildFoldersAsync(int folderId);
    }
}
