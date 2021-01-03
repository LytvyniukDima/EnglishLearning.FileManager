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

        Task<IReadOnlyList<FileModel>> GetAllByFolderId(int folderId);

        Task<FileModel> GetAsync(Guid id);
    }
}
