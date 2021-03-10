using System.IO;
using System.Threading.Tasks;
using EnglishLearning.FileManager.Application.Abstract;
using EnglishLearning.FileManager.Web.Constants;
using EnglishLearning.FileManager.Web.ViewModels;
using EnglishLearning.Utilities.Identity.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EnglishLearning.FileManager.Web.Controllers
{
    [Route("/api/file-manager/parser")]
    [ApiController]
    public class ParserController : Controller
    {
        private readonly IHtmlParser _htmlParser;

        public ParserController(IHtmlParser htmlParser)
        {
            _htmlParser = htmlParser;
        }
        
        [EnglishLearningAuthorize(AuthorizeRole.Admin)]
        [HttpPost("html/tree")]
        public async Task<ActionResult> CreateFile([FromForm] IFormFile uploadedFile)
        {
            await using var memoryStream = new MemoryStream();
            await uploadedFile.CopyToAsync(memoryStream);
            
            var fileStream = await _htmlParser.ParseHtmlTableToCsv(memoryStream);
            
            return new FileStreamResult(fileStream, ContentTypeConstants.Csv)
            {
                FileDownloadName = "parsed_html.csv",
            };
        }
    }
}