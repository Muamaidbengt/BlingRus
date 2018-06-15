using System.Linq;
using System.Threading.Tasks;
using BlingRus.Domain;
using Microsoft.AspNetCore.Mvc;

namespace BlingRus.Web.ViewComponents
{
    public class CartCounter : ViewComponent
    {
        private readonly CheckoutService _checkoutService;

        public CartCounter(CheckoutService checkoutService)
        {
            _checkoutService = checkoutService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var cart = _checkoutService.GetCart();
            return View(cart?.Contents.Count());
        }
    }
}
