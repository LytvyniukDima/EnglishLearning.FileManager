using System.Collections.Generic;

namespace EnglishLearning.FileManager.Application.Models
{
    public class FileTree
    {
        public IReadOnlyList<FolderTreeItem> Folders { get; set; }
        
        public IReadOnlyList<FileTreeItem> Files { get; set; }
    }
}