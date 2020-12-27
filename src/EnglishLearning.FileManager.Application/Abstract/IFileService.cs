using System.IO;
using System.Threading.Tasks;
using EnglishLearning.FileManager.Application.Models;

namespace EnglishLearning.FileManager.Application.Abstract
{
    public interface IFileService
    {
        Task CreateFileAsync(Stream fileStream, FileCreateModel fileCreateModel);
    }
}