using System;
using System.Collections.Generic;
using System.Text.Json;
using EnglishLearning.FileManager.Application.Models;
using EnglishLearning.FileManager.Web.Constants;
using EnglishLearning.FileManager.Web.ViewModels;

namespace EnglishLearning.FileManager.Web.Infrastructure
{
    internal static class WebMapper
    {
        public static readonly IReadOnlyDictionary<string, string> ContentTypeFileExtensionMap = new Dictionary<string, string>()
        {
            { ContentTypes.Txt, "txt" },
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
    }
}