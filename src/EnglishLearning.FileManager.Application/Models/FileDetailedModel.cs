using System.Collections.Generic;

namespace EnglishLearning.FileManager.Application.Models
{
    public class FileDetailedModel : FileModel
    {
        public IReadOnlyList<string> Path { get; set; }
    }
}
