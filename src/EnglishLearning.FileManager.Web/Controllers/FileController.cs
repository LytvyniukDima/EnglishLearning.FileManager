using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EnglishLearning.FileManager.Application.Abstract;
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
        private readonly ITreeService _treeService;
        
        public FileController(
            IFileEntityRepository fileRepository,
            IFolderEntityRepository folderRepository,
            ITreeService treeService)
        {
            _fileRepository = fileRepository;
            _folderRepository = folderRepository;
            _treeService = treeService;
        }
        
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            // var file = await _fileRepository.GetAsync(new Guid("4E4C15CD-0255-4E50-1155-08D8AA5CDCEB"));
            // var files = await _fileRepository.GetAllAsync();
            // var folders = await _folderRepository.GetAllAsync();
            // var folder = await _folderRepository.GetAsync(14);

            var tree = await _treeService.GetTreeAsync();
            
            return Ok(tree);
        }
    }
}
