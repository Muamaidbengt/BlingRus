using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BlingRus.Web.Controllers
{
    [Route("~/")]
    [Route("[controller]")]
    public class LandingController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}