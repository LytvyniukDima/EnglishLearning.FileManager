using System;
using System.Collections.Generic;
using System.Text.Json;
using EnglishLearning.FileManager.Application.Models;
using EnglishLearning.FileManager.Web.ViewModels;
using static EnglishLearning.FileManager.Web.Constants.ContentTypeConstants;

namespace EnglishLearning.FileManager.Web.Infrastructure
{
    internal static class WebMapper
    {
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
    }
}