using System.Collections.Generic;

namespace EnglishLearning.FileManager.Application.Models
{
    public abstract class TreeItem
    {
        public string Name { get; set; }
        
        public IReadOnlyCollection<string> Path { get; set; }
    }
}
