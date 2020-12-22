using Microsoft.AspNetCore.Mvc;

namespace EnglishLearning.FileManager.Web.Controllers
{
    [Route("/api/file-manager/file")]
    public class FileController : Controller
    {
        [HttpGet]
        public ActionResult Get()
        {
            return Ok("file");
        }
    }
}
