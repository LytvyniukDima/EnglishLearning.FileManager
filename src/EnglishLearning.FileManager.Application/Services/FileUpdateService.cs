using System.IO;
using System.Threading.Tasks;
using EnglishLearning.FileManager.Application.Abstract;
using EnglishLearning.FileManager.Application.Configuration;
using EnglishLearning.FileManager.Application.Models;
using EnglishLearning.FileManager.Persistence.Abstract;
using Microsoft.Extensions.Options;

namespace EnglishLearning.FileManager.Application.Services
{
    internal class FileUpdateService : IFileUpdateService
    {
        private readonly IFileEntityRepository _fileRepository;

        private readonly FileShareConfiguration _fileShareConfiguration;
        
        public FileUpdateService(
            IFileEntityRepository fileRepository,
            IOptions<FileShareConfiguration> fileShareConfiguration)
        {
            _fileRepository = fileRepository;
            _fileShareConfiguration = fileShareConfiguration.Value;
        }
        
        public async Task RemoveTextFromFileAsync(RemoveTextFromFileModel removeTextFromFileModel)
        {
            var fileEntity = await _fileRepository.GetAsync(removeTextFromFileModel.FileId);
            var fileName = fileEntity.Id.ToString().ToUpper();
            var fileTmpName = $"{fileName}_tmp";
            var filePath = Path.Combine(_fileShareConfiguration.Path, fileName);
            var fileTmpPath = Path.Combine(_fileShareConfiguration.Path, fileTmpName);
            
            File.Move(filePath, fileTmpPath);
            File.Delete(filePath);

            await using var fileStream = File.OpenWrite(filePath);
            await using var fileWriteStream = new StreamWriter(fileStream);
            
            await using (var tmpFileStream = File.OpenRead(fileTmpPath))
            using (var tmpFileReadStream = new StreamReader(tmpFileStream))
            {
                while (!tmpFileReadStream.EndOfStream)
                {
                    var line = await tmpFileReadStream.ReadLineAsync();
                    foreach (var removeItem in removeTextFromFileModel.RemoveItems)
                    {
                        line = line?.Replace(removeItem, string.Empty);
                    }

                    if (!string.IsNullOrEmpty(line))
                    {
                        await fileWriteStream.WriteLineAsync(line);
                    }
                }   
            }

            File.Delete(fileTmpPath);
        }
    }
}