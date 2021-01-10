using Microsoft.AspNetCore.Http;

namespace EnglishLearning.FileManager.Web.ViewModels
{
    public class FileCreateViewModel
    {
        public string Name { get; set; }
        
        public int? FolderId { get; set; }
        
        public string Metadata { get; set; }
        
        public IFormFile UploadedFile { get; set; }
    }
}
