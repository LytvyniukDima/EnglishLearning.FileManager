namespace EnglishLearning.FileManager.Application.Models
{
    public class FolderCreateModel
    {
        public string Name { get; set; }
        
        public int? ParentId { get; set; }
    }
}