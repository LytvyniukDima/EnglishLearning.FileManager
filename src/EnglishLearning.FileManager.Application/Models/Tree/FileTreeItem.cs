using System;
using System.Collections.Generic;

namespace EnglishLearning.FileManager.Application.Models.Tree
{
    public class FileTreeItem : TreeItem
    {
        public Guid Id { get; set; }

        public string Extension { get; set; }
            
        public DateTime LastModified { get; set; }
        
        public Guid CreatedBy { get; set; }
        
        public Dictionary<string, string> Metadata { get; set; }
    }
}
