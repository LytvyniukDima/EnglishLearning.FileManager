using System.Collections.Generic;
using System.Threading.Tasks;
using EnglishLearning.FileManager.Application.Models;
using EnglishLearning.FileManager.Application.Models.Tree;

namespace EnglishLearning.FileManager.Application.Abstract
{
    public interface IFolderService
    {
        Task<IReadOnlyList<FolderModel>> GetChildFoldersAsync(int? folderId);

        Task<FolderModel> CreateAsync(FolderCreateModel folderModel);

        Task<IReadOnlyList<FolderModel>> GetAllChildFoldersAsync(int? folderId);

        Task<FolderInfoModel> GetFolderInfoAsync(int folderId);

        Task<IReadOnlyList<FolderTreeItem>> GetFolderTreeItemsAsync();
    }
}
