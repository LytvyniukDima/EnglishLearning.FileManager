using System.IO;
using System.Threading.Tasks;
using EnglishLearning.FileManager.Application.Abstract;
using EnglishLearning.FileManager.Web.ViewModels;
using EnglishLearning.Utilities.Identity.Abstractions;
using Microsoft.AspNetCore.Mvc;
using static EnglishLearning.FileManager.Web.Infrastructure.WebMapper;

namespace EnglishLearning.FileManager.Web.Controllers
{
    [Route("/api/file-manager/file")]
    [ApiController]
    public class FileController : Controller
    {
        private readonly IFileService _fileService;
        private readonly IJwtInfoProvider _jwtInfoProvider;
        
        public FileController(
            IFileService fileService,
            IJwtInfoProvider jwtInfoProvider)
        {
            _fileService = fileService;
            _jwtInfoProvider = jwtInfoProvider;
        }

        [EnglishLearningAuthorize(AuthorizeRole.Admin)]
        [HttpPost]
        public async Task<ActionResult> CreateFile([FromForm] FileCreateViewModel file)
        {
            var fileCreateModel = MapViewModelToApplicationModel(file, _jwtInfoProvider.UserId);
            await using var memoryStream = new MemoryStream();
            await file.UploadedFile.CopyToAsync(memoryStream);
            
            await _fileService.CreateFileAsync(memoryStream, fileCreateModel);
            
            return Ok(file.UploadedFile.ContentType);
        }
    }
}
