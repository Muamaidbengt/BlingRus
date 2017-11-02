using Microsoft.AspNetCore.Mvc;

namespace BlingRus.Web.Controllers
{

    [Route("~/")]
    [Route("[controller]")]
    public class LandingController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}