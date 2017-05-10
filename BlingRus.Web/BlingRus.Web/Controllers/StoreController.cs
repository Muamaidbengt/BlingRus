using System.Linq;
using BlingRus.Domain;
using BlingRus.Web.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BlingRus.Web.Controllers
{
    [Route("~/")]
    [Route("[controller]")]
    public class StoreController : Controller
    {
        private readonly IShoppingContext _shoppingContext;
        private readonly CheckoutService _checkoutService;

        public StoreController(IShoppingContext shoppingContext, CheckoutService checkoutService)
        {
            _shoppingContext = shoppingContext;
            _checkoutService = checkoutService;
        }

        [HttpGet]
        [HttpGet("index")]
        public IActionResult Index()
        {
            var cart = _checkoutService.GetCart();
            var catalog = _shoppingContext.Catalog.ToList();

            var model = new CatalogModel
            {
                CartId = cart.Id,
                Catalog = catalog
            };
            return View(model);
        }

        [HttpGet("checkout")]
        public IActionResult Checkout()
        {
            var cart = _checkoutService.GetCart();
            var order = _checkoutService.CalculateOrder(cart);
            var model = new SubmitOrderModel {Order = order};
            return View(model);
        }

        [HttpPost("checkout")]
        public IActionResult Checkout(SubmitOrderModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var cart = _checkoutService.CreateCart();
            return View("Confirm");
        }
    }
}
