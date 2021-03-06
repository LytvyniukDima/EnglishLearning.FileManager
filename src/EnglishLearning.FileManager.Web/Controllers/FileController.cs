using System;
using System.IO;
using System.Threading.Tasks;
using EnglishLearning.FileManager.Application.Abstract;
using EnglishLearning.FileManager.Application.Models;
using EnglishLearning.FileManager.Web.ViewModels;
using EnglishLearning.Utilities.Identity.Abstractions;
using Microsoft.AspNetCore.Mvc;
using static EnglishLearning.FileManager.Web.Constants.ContentTypeConstants;
using static EnglishLearning.FileManager.Web.Infrastructure.WebMapper;

namespace EnglishLearning.FileManager.Web.Controllers
{
    [Route("/api/file-manager/file")]
    [ApiController]
    public class FileController : Controller
    {
        private readonly IFileService _fileService;
        private readonly IJwtInfoProvider _jwtInfoProvider;
        private readonly IFileUpdateService _fileUpdateService;
        
        public FileController(
            IFileService fileService,
            IJwtInfoProvider jwtInfoProvider,
            IFileUpdateService fileUpdateService)
        {
            _fileService = fileService;
            _jwtInfoProvider = jwtInfoProvider;
            _fileUpdateService = fileUpdateService;
        }

        [EnglishLearningAuthorize(AuthorizeRole.Admin)]
        [HttpPost]
        public async Task<ActionResult> CreateFile([FromForm] FileCreateViewModel file)
        {
            var fileCreateModel = MapViewModelToApplicationModel(file, _jwtInfoProvider.UserId);
            await using var memoryStream = new MemoryStream();
            await file.UploadedFile.CopyToAsync(memoryStream);
            
            await _fileService.CreateFileAsync(memoryStream, fileCreateModel);
            
            return Ok();
        }
        
        [EnglishLearningAuthorize(AuthorizeRole.Admin)]
        [HttpGet("{id}")]
        public async Task<ActionResult> GetFile(Guid id)
        {
            var fileInfo = await _fileService.GetFileDetailedModelAsync(id);
            var contentType = FileExtensionContentTypeMap[fileInfo.Extension];
            var fileName = $"{fileInfo.Name}.{fileInfo.Extension}";
            var fileStream = await _fileService.GetFileContentAsync(id);
            
            return new FileStreamResult(fileStream, contentType)
            {
                FileDownloadName = fileName,
            };
        }
        
        [EnglishLearningAuthorize(AuthorizeRole.Admin)]
        [HttpGet("{id}/details")]
        public async Task<ActionResult> GetFileInfo(Guid id)
        {
            var fileDetails = await _fileService.GetFileDetailedModelAsync(id);

            return Ok(fileDetails);
        }
        
        [EnglishLearningAuthorize(AuthorizeRole.Admin)]
        [HttpGet("~/api/file-manager/folder/file/info")]
        public async Task<ActionResult> GetFilesInFolderInfo([FromQuery] int? folderId)
        {
            var fileInfos = await _fileService.GetAllFromFolderAsync(folderId);

            return Ok(fileInfos);
        }
        
        [EnglishLearningAuthorize(AuthorizeRole.Admin)]
        [HttpPost("remove-text")]
        public async Task<ActionResult> RemoveTextFromFile([FromBody] RemoveTextFromFileModel removeTextFromFileModel)
        {
            await _fileUpdateService.RemoveTextFromFileAsync(removeTextFromFileModel);

            return Ok();
        }
    }
}
