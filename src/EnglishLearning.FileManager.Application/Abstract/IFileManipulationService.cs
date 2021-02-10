using System.IO;
using System.Threading.Tasks;
using EnglishLearning.FileManager.Application.Models;

namespace EnglishLearning.FileManager.Application.Abstract
{
    public interface IFileManipulationService
    {
        Task CreateFile(Stream fileStream, FileCreateModel fileCreateModel);
    }
}