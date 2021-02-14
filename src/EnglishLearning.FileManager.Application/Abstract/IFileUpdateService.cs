using System.Threading.Tasks;
using EnglishLearning.FileManager.Application.Models;

namespace EnglishLearning.FileManager.Application.Abstract
{
    public interface IFileUpdateService
    {
        Task RemoveTextFromFileAsync(RemoveTextFromFileModel removeTextFromFileModel);
    }
}