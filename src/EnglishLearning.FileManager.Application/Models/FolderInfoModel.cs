using System.Collections.Generic;

namespace EnglishLearning.FileManager.Application.Models
{
    public class FolderInfoModel
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public IReadOnlyList<string> Path { get; set; }
    }
}