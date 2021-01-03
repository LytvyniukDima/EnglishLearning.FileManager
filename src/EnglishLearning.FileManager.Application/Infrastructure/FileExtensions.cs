using System.IO;

namespace EnglishLearning.FileManager.Application.Infrastructure
{
    public static class FileExtensions
    {
        public static string GetFileExtension(string fileName)
        {
            var fileExtension = Path
                .GetExtension(fileName);
            fileExtension = string.IsNullOrEmpty(fileExtension)
                ? fileExtension
                : fileExtension.Substring(1);

            return fileExtension;
        }
    }
}