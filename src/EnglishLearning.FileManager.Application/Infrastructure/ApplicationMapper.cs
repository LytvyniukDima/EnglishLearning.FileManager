using EnglishLearning.FileManager.Application.Models;
using EnglishLearning.FileManager.Persistence.Entities;

namespace EnglishLearning.FileManager.Application.Infrastructure
{
    internal static class ApplicationMapper
    {
        public static FileEntity MapFileCreateModelToEntity(FileCreateModel fileCreateModel)
        {
            return new FileEntity
            {
                Id = fileCreateModel.Id,
                CreatedBy = fileCreateModel.CreatedBy,
                FolderId = fileCreateModel.FolderId,
                LastModified = fileCreateModel.LastModified,
                Metadata = fileCreateModel.Metadata,
                Name = fileCreateModel.Name,
            };
        }
    }
}