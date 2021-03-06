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
                CreatedBy = fileCreateModel.CreatedBy,
                FolderId = fileCreateModel.FolderId,
                LastModified = fileCreateModel.LastModified,
                Metadata = fileCreateModel.Metadata,
                Name = fileCreateModel.Name,
                Extension = fileCreateModel.Extension,
            };
        }

        public static FileModel MapFileEntityToModel(FileEntity fileEntity)
        {
            return new FileModel
            {
                Id = fileEntity.Id,
                Name = fileEntity.Name,
                Extension = fileEntity.Extension,
                CreatedBy = fileEntity.CreatedBy,
                FolderId = fileEntity.FolderId,
                LastModified = fileEntity.LastModified,
                Metadata = fileEntity.Metadata,
            };
        }

        public static FileDetailedModel MapFileEntityToDetailedModel(FileEntity fileEntity, IReadOnlyList<string> path)
        {
            return new FileDetailedModel
            {
                Id = fileEntity.Id,
                Name = fileEntity.Name,
                Extension = fileEntity.Extension,
                CreatedBy = fileEntity.CreatedBy,
                FolderId = fileEntity.FolderId,
                LastModified = fileEntity.LastModified,
                Metadata = fileEntity.Metadata,
                Path = path,
            };
        }
        
        public static FolderModel MapFolderEntityToModel(FolderEntity folderEntity)
        {
            return new FolderModel
            {
                Id = folderEntity.Id,
                Name = folderEntity.Name,
                ParentId = folderEntity.ParentId,
            };
        }

        public static FolderEntity MapFolderCreateModelToEntity(FolderCreateModel folderCreateModel)
        {
            return new FolderEntity
            {
                Name = folderCreateModel.Name,
                ParentId = folderCreateModel.ParentId,
            };
        }
        
        public static IReadOnlyList<FileModel> MapFileEntitiesToModels(this IEnumerable<FileEntity> files)
        {
            return files
                .Select(MapFileEntityToModel)
                .ToList();
        }
        
        public static IReadOnlyList<FolderModel> MapFolderEntitiesToModels(this IEnumerable<FolderEntity> folders)
        {
            return folders
                .Select(MapFolderEntityToModel)
                .ToList();
        }
    }
}
