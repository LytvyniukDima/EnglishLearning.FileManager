using System.Collections.Generic;
using System.Threading.Tasks;
using EnglishLearning.FileManager.Application.Models;
using EnglishLearning.FileManager.Application.Models.Tree;

namespace EnglishLearning.FileManager.Application.Abstract
{
    public interface ITreeService
    {
        Task<FileTree> GetTreeAsync();
    }
}