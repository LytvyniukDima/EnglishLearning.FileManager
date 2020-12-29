using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using EnglishLearning.FileManager.Application.Abstract;
using EnglishLearning.FileManager.Application.Models;
using EnglishLearning.FileManager.Web.ViewModels;
using EnglishLearning.Utilities.Identity.Abstractions;
using Microsoft.AspNetCore.Mvc;

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
            var fileCreateModel = MapFileCreateViewModelToModel(file);
            await using var memoryStream = new MemoryStream();
            await file.UploadedFile.CopyToAsync(memoryStream);
            
            await _fileService.CreateFileAsync(memoryStream, fileCreateModel);
            
            return Ok(file.UploadedFile.ContentType);
        }

        private FileCreateModel MapFileCreateViewModelToModel(FileCreateViewModel viewModel)
        {
            var metadata = JsonSerializer.Deserialize<Dictionary<string, string>>(viewModel.Metadata, new JsonSerializerOptions() { IgnoreNullValues = true });

            return new FileCreateModel
            {
                Id = Guid.NewGuid(),
                Name = viewModel.Name,
                CreatedBy = _jwtInfoProvider.UserId,
                FolderId = viewModel.FolderId,
                LastModified = DateTime.UtcNow,
                Metadata = metadata,
            };
        }
    }
}
