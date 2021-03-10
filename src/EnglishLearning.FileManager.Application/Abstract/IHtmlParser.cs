using System.IO;
using System.Threading.Tasks;

namespace EnglishLearning.FileManager.Application.Abstract
{
    public interface IHtmlParser
    {
        Task<Stream> ParseHtmlTableToCsv(Stream inputStream);
    }
}