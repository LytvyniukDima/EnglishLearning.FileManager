using EnglishLearning.FileManager.Application.Models;

namespace EnglishLearning.FileManager.Application.Abstract
{
    public interface IFileManipulationServiceFactory
    {
        IFileManipulationService GetFileManipulationService(FileCreateModel fileCreateModel);
    }
}