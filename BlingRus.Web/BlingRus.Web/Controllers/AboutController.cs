using System;
using System.Threading.Tasks;
using BlingRus.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlingRus.Web.Controllers
{
    [Route("[controller]")]
    public class AboutController : Controller
    {
        [HttpGet("index")]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpGet("contact")]
        public async Task<IActionResult> Contact()
        {
            var model = new ContactRequestModel();
            return View(model);
        }

        [HttpPost("contact")]
        public async Task<IActionResult> Contact(ContactRequestModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            return View("ThankYou");
        }

        [HttpGet("creditcards")]
        public async Task<IActionResult> CreditCards()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Faq()
        {
            return View();
        }
    }
}
