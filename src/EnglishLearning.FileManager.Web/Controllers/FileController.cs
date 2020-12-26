using System.Threading.Tasks;
using EnglishLearning.FileManager.Persistence.Abstract;
using EnglishLearning.FileManager.Persistence.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EnglishLearning.FileManager.Web.Controllers
{
    [Route("/api/file-manager/file")]
    public class FileController : Controller
    {
        private readonly IFolderEntityRepository _folderRepository;

        public FileController(IFolderEntityRepository folderRepository)
        {
            _folderRepository = folderRepository;
        }
        
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var folder = new FolderEntity
            {
                Name = "Some new dir",
                ParentId = 2,
            };

            await _folderRepository.AddAsync(folder);
            var folders = await _folderRepository.GetAllAsync();

            return Ok(folders);
        }
    }
}
