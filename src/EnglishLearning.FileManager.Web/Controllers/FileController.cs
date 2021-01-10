using System;
using System.IO;
using System.Threading.Tasks;
using EnglishLearning.FileManager.Application.Abstract;
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
            
            return Ok();
        }
        
        [EnglishLearningAuthorize(AuthorizeRole.Admin)]
        [HttpGet("{id}")]
        public async Task<ActionResult> GetFile(Guid id)
        {
            var fileInfo = await _fileService.GetInfoAsync(id);
            var contentType = FileExtensionContentTypeMap[fileInfo.Extension];
            var fileName = $"{fileInfo.Name}.{fileInfo.Extension}";
            var fileStream = await _fileService.GetFileContentAsync(id);
            
            return new FileStreamResult(fileStream, contentType)
            {
                FileDownloadName = fileName,
            };
        }
        
        [EnglishLearningAuthorize(AuthorizeRole.Admin)]
        [HttpGet("{id}/info")]
        public async Task<ActionResult> GetFileInfo(Guid id)
        {
            var fileInfo = await _fileService.GetInfoAsync(id);
            var fileViewModel = MapFileModelToFileInfoViewModel(fileInfo);
            
            return Ok(fileViewModel);
        }
        
        [EnglishLearningAuthorize(AuthorizeRole.Admin)]
        [HttpGet("~/api/file-manager/folder/file/info")]
        public async Task<ActionResult> GetFilesInFolderInfo([FromQuery] int? folderId)
        {
            var fileInfos = await _fileService.GetAllFromFolderAsync(folderId);
            var fileInfoViewModels = fileInfos.MapFileModelsToFileInfoViewModels();
            
            return Ok(fileInfoViewModels);
        }
    }
}
