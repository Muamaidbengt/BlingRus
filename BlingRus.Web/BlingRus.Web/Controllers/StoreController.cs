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
        

        public StoreController(IShoppingContext shoppingContext
            , CheckoutService checkoutService
            )
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
            var model = new SubmitOrderModel 
            {
                Order = order, 
                CreditCardNumber = cart.CreditCardNumber,
                CreditCardExpiration = cart.CreditCardExpiration,
                CustomerPhone = cart.CustomerPhone,
                CustomerAddress = cart.CustomerAddress,
                CustomerName = cart.CustomerName
            };
            return View(model);
        }

        [HttpPost("checkout")]
        public IActionResult Checkout(SubmitOrderModel model)
        {
            var cart =_checkoutService.GetCart();
            
            if (!ModelState.IsValid)
            {
                var originalOrder = _checkoutService.CalculateOrder(cart);
                model.Order = originalOrder;
                return View(model);
            }

            cart.CustomerName = model.CustomerName;
            cart.CustomerAddress = model.CustomerAddress;
            cart.CustomerEmail = model.CustomerEmail;
            cart.CustomerPhone = model.CustomerPhone;
            cart.CreditCardNumber = model.CreditCardNumber;
            cart.CreditCardExpiration = model.CreditCardExpiration;

            _checkoutService.FinalizeOrder(cart);

            return View("Confirm");
        }
    }
}