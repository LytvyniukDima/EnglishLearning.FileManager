using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EnglishLearning.FileManager.Persistence.Abstract;
using EnglishLearning.FileManager.Persistence.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EnglishLearning.FileManager.Web.Controllers
{
    [Route("/api/file-manager/file")]
    public class FileController : Controller
    {
        private readonly IFileEntityRepository _fileRepository;
        private readonly IFolderEntityRepository _folderRepository;
        
        public FileController(
            IFileEntityRepository fileRepository,
            IFolderEntityRepository folderRepository)
        {
            _fileRepository = fileRepository;
            _folderRepository = folderRepository;
        }
        
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            //var newFile = await _fileRepository.GetAsync(new Guid("02b4af32-4f97-4f6e-f2ee-08d8a9cd7a91"));
            var files = await _fileRepository.GetAllAsync();

            return Ok(files);
        }
    }
}
