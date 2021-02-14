using System;
using System.Collections.Generic;

namespace EnglishLearning.FileManager.Application.Models
{
    public class RemoveTextFromFileModel
    {
        public Guid FileId { get; set; }
        
        public IReadOnlyList<string> RemoveItems { get; set; }
    }
}