using System.IO;

namespace EnglishLearning.FileManager.Application.Extensions
{
    public static class FileNameExtensions
    {
        public static string GetFileExtension(this string fileName)
        {
            var fileExtension = Path
                .GetExtension(fileName);
            fileExtension = string.IsNullOrEmpty(fileExtension)
                ? string.Empty
                : fileExtension.Substring(1);

            return fileExtension.ToLower();
        }
    }
}