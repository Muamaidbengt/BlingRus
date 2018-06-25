using System;
using BlingRus.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlingRus.Web.Controllers
{
    [Route("[controller]")]
    public class AboutController : Controller
    {
        [HttpGet("index")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("contact")]
        public IActionResult Contact()
        {
            var model = new ContactRequestModel();
            return View(model);
        }

        [HttpPost("contact")]
        public IActionResult Contact(ContactRequestModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            return View("ThankYou");
        }

        [HttpGet("creditcards")]
        public IActionResult CreditCards()
        {
            return View();
        }
    }
}
