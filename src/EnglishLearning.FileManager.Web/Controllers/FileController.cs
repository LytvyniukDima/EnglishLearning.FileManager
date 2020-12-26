using System.Threading.Tasks;
using EnglishLearning.FileManager.Persistence.Abstract;
using EnglishLearning.FileManager.Persistence.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EnglishLearning.FileManager.Web.Controllers
{
    [Route("/api/file-manager/file")]
    public class FileController : Controller
    {
        private readonly IFolderRepository _folderRepository;

        public FileController(IFolderRepository folderRepository)
        {
            _folderRepository = folderRepository;
        }
        
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var folders = await _folderRepository.GetAsync(2);

            return Ok(folders);
        }
    }
}
