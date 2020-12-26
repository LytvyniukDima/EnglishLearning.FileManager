namespace EnglishLearning.FileManager.Persistence.Entities
{
    public class FolderEntity
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public int? ParentId { get; set; }
        
        public FolderEntity Parent { get; set; }
    }
}
