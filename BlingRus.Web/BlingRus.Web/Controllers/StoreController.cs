using System.IO;
using System.Linq;
using BlingRus.Domain;
using BlingRus.Web.Models;
using BlingRus.Web.Services;
using BlingRus.Web.ViewComponents;
using Microsoft.AspNetCore.Mvc;


namespace BlingRus.Web.Controllers
{
    [Route("[controller]")]
    public class StoreController : Controller
    {
        private readonly IShoppingContext _shoppingContext;
        private readonly CheckoutService _checkoutService;
        private readonly IMailService _mailService;
        private readonly IViewRenderService _viewRenderService;

        public StoreController(IShoppingContext shoppingContext
            , CheckoutService checkoutService
            , IMailService mailService
            , IViewRenderService viewRenderService)
        {
            _shoppingContext = shoppingContext;
            _checkoutService = checkoutService;
            _mailService = mailService;
            _viewRenderService = viewRenderService;
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

            var finalizedOrder = _checkoutService.FinalizeOrder(cart, 
                model.CustomerName, model.CustomerAddress, model.CustomerEmail, 
                model.CreditCardNumber, model.CreditCardExpiration);

            var mailContents =
                _viewRenderService.RenderToString(@"~/Views/Shared/Components/ConfirmationMail/Default.cshtml");

            _mailService.SendOrderConfirmationMail(model.CustomerEmail, mailContents);

            _checkoutService.CreateCart();

            return View("Confirm");
        }
    }
}