namespace EnglishLearning.FileManager.Application.Abstract
{
    public interface IFileManipulationServiceFactory
    {
        IFileManipulationService GetFileManipulationService(string fileExtension);
    }
}