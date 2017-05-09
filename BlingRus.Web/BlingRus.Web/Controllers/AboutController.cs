using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BlingRus.Web.Controllers
{
    [Route("[controller]")]
    public class AboutController : Controller
    {
        [HttpGet]
        [HttpGet("index")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("contact")]
        public IActionResult Contact()
        {

            return View();
        }
    }
}
