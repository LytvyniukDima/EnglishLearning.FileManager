using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using EnglishLearning.FileManager.Application.Extensions;
using EnglishLearning.FileManager.Application.Models;
using EnglishLearning.FileManager.Web.ViewModels;

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
                Extension = file.UploadedFile.FileName.GetFileExtension(),
                FolderId = file.FolderId,
                LastModified = DateTime.UtcNow,
                Metadata = metadata,
                Name = file.Name,
                CsvColumnToRead = file.CsvColumnToRead,
            };
        }
    }
}