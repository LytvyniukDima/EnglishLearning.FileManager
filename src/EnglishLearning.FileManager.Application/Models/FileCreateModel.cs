using System;
using System.Collections.Generic;

namespace EnglishLearning.FileManager.Application.Models
{
    public class FileCreateModel
    {
        public string Name { get; set; }
        
        public string Extension { get; set; }
        
        public DateTime LastModified { get; set; }
        
        public Guid CreatedBy { get; set; }
        
        public Dictionary<string, string> Metadata { get; set; }
        
        public int FolderId { get; set; }
    }
}
