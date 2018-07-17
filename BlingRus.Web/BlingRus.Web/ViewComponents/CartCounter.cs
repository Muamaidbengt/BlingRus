using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlingRus.Domain.Ordering;
using BlingRus.Domain.Shopping;
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
            var cart = await _checkoutService.GetCart();
            var model = new Tuple<int?, decimal?>(cart?.AggregatedQuantity, cart?.AggregatedCost + cart?.AggregatedShippingCost);
            
            return View(model);
        }
    }
}