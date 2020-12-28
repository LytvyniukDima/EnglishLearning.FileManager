using System.Collections.Generic;
using System.Linq;
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

        public static FileModel MapFileEntityToModel(FileEntity fileEntity)
        {
            return new FileModel
            {
                Id = fileEntity.Id,
                Name = fileEntity.Name,
                CreatedBy = fileEntity.CreatedBy,
                FolderId = fileEntity.FolderId,
                LastModified = fileEntity.LastModified,
                Metadata = fileEntity.Metadata,
            };
        }
        
        public static IReadOnlyList<FileModel> MapFileEntitiesToModels(this IEnumerable<FileEntity> files)
        {
            return files
                .Select(MapFileEntityToModel)
                .ToList();
        }
    }
}
