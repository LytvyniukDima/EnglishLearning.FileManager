using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using EnglishLearning.FileManager.Application.Models;

namespace EnglishLearning.FileManager.Application.Abstract
{
    public interface IFileService
    {
        Task CreateFileAsync(Stream fileStream, FileCreateModel fileCreateModel);

        Task<Stream> GetFileContentAsync(Guid id);
        
        Task<FileDetailedModel> GetFileDetailedModelAsync(Guid id);

        Task<IReadOnlyList<FileModel>> GetAllByFolderId(int? folderId);

        Task<IReadOnlyList<FileModel>> GetAllFromFolderAsync(int? folderId);
    }
}
