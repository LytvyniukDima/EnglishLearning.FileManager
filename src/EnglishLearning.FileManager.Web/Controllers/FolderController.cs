using System;
using System.Threading.Tasks;
using EnglishLearning.FileManager.Application.Abstract;
using EnglishLearning.Utilities.Identity.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace EnglishLearning.FileManager.Web.Controllers
{
    [Route("/api/file-manager/folder")]
    [ApiController]
    public class FolderController : Controller
    {
        private readonly IFolderService _folderService;

        public FolderController(IFolderService folderService)
        {
            _folderService = folderService;
        }

        [EnglishLearningAuthorize(AuthorizeRole.Admin)]
        [HttpGet("{id}/info")]
        public async Task<ActionResult> GetFolder(int id)
        {
            var folderInfo = await _folderService.GetFolderInfoAsync(id);
            
            return Ok(folderInfo);
        }
    }
}