using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using EnglishLearning.FileManager.Application.Constants;
using EnglishLearning.FileManager.Application.Models;
using EnglishLearning.FileManager.Web.Constants;
using EnglishLearning.FileManager.Web.ViewModels;

namespace EnglishLearning.FileManager.Web.Infrastructure
{
    internal static class WebMapper
    {
        public static readonly IReadOnlyDictionary<string, string> ContentTypeFileExtensionMap = new Dictionary<string, string>()
        {
            { ContentTypes.Txt, FileConstants.Txt },
            { ContentTypes.Zip, FileConstants.Zip },
        };

        public static FileCreateModel MapViewModelToApplicationModel(
            FileCreateViewModel file,
            Guid createdBy)
        {
            var metadata = JsonSerializer.Deserialize<Dictionary<string, string>>(file.Metadata, new JsonSerializerOptions() { IgnoreNullValues = true });
            
            return new FileCreateModel
            {
                CreatedBy = createdBy,
                Extension = ContentTypeFileExtensionMap[file.UploadedFile.ContentType],
                FolderId = file.FolderId,
                LastModified = DateTime.UtcNow,
                Metadata = metadata,
                Name = file.Name,
            };
        }

        public static FileInfoViewModel MapFileModelToFileInfoViewModel(FileModel fileModel)
        {
            return new FileInfoViewModel
            {
                CreatedBy = fileModel.CreatedBy,
                Extension = fileModel.Extension,
                FolderId = fileModel.FolderId,
                Id = fileModel.Id,
                LastModified = fileModel.LastModified,
                Metadata = fileModel.Metadata,
                Name = fileModel.Name,
            };
        }

        public static IReadOnlyList<FileInfoViewModel> MapFileModelsToFileInfoViewModels(this IReadOnlyList<FileModel> fileModels)
        {
            return fileModels
                .Select(MapFileModelToFileInfoViewModel)
                .ToList();
        }
    }
}