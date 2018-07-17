using System.Threading.Tasks;
using BlingRus.Domain.Ordering;
using BlingRus.Domain.Shopping;
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
        public async Task<IActionResult> Index()
        {
            var cart = await _checkoutService.GetCart();
            var catalog = await _shoppingContext.GetCatalog();

            var model = new CatalogModel
            {
                CartId = cart.Id,
                Catalog = catalog
            };
            return View(model);
        }

        [HttpGet("checkout")]
        public async Task<IActionResult> Checkout()
        {
            var cart = await _checkoutService.GetCart();
            var order = await _checkoutService.CalculateOrder(cart);
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
        public async Task<IActionResult> Checkout(SubmitOrderModel model)
        {
            var cart = await _checkoutService.GetCart();
            
            if (!ModelState.IsValid)
            {
                var originalOrder = await _checkoutService.CalculateOrder(cart);
                model.Order = originalOrder;
                return View(model);
            }

            cart.CustomerName = model.CustomerName;
            cart.CustomerAddress = model.CustomerAddress;
            cart.CustomerEmail = model.CustomerEmail;
            cart.CustomerPhone = model.CustomerPhone;
            cart.CreditCardNumber = model.CreditCardNumber;
            cart.CreditCardExpiration = model.CreditCardExpiration;

            await _checkoutService.FinalizeOrder(cart);

            return View("Confirm");
        }
    }
}