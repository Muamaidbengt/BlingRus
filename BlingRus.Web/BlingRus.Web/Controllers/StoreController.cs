using System.Linq;
using BlingRus.Domain;
using BlingRus.Web.Models;
using Microsoft.AspNetCore.Mvc;


namespace BlingRus.Web.Controllers
{
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
            {
                var cart =_checkoutService.GetCart();
                var order = _checkoutService.CalculateOrder(cart);
                model.Order = order;
                return View(model);
            }

            _checkoutService.CreateCart();

            return View("Confirm");
        }
    }
}