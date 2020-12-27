using System;
using System.Threading.Tasks;
using EnglishLearning.FileManager.Application.Abstract;
using EnglishLearning.Utilities.Identity.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace EnglishLearning.FileManager.Web.Controllers
{
    [Route("/api/file-manager/tree")]
    public class TreeController : Controller
    {
        private readonly ITreeService _treeService;

        public TreeController(ITreeService treeService)
        {
            _treeService = treeService;
        }
        
        [EnglishLearningAuthorize(AuthorizeRole.Admin)]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var tree = await _treeService.GetTreeAsync();
            
            return Ok(tree);
        }
    }
}