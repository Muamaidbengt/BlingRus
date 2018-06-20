using System;
using System.Linq;
using System.Threading.Tasks;
using BlingRus.Domain;
using BlingRus.Domain.Ordering;
using BlingRus.Domain.Services;
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
            var model = cart == null
                ? new Tuple<int?, decimal?>(null, null)
                : new Tuple<int?, decimal?>(cart.Contents.Sum(thing => thing.Quantity), cart.AggregatedCost + cart.AggregatedShippingCost);
            
            return View(model);
        }
    }
}