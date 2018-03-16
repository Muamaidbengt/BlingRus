using System.Collections.Generic;
using System.Linq;
using BlingRus.Domain.Discounts;
using Microsoft.AspNetCore.Http;

namespace BlingRus.Domain
{
    public class CheckoutService
    {
        private readonly IHttpContextAccessor _httpContext;
        private readonly IShoppingContext _shoppingContext;

        public CheckoutService(IHttpContextAccessor httpContext, IShoppingContext shoppingContext)
        {
            _httpContext = httpContext;
            _shoppingContext = shoppingContext;
        }

        public ShoppingCart CreateCart()
        {
            var cart = _shoppingContext.CreateCart();
            _shoppingContext.Add(cart);
            _shoppingContext.Save();

            _httpContext.HttpContext.Response.Cookies.Append("ShoppingCartId", cart.Id.ToString());
            
            return cart;
        }

        public ShoppingCart GetCart()
        {
            var cartCookie = _httpContext.HttpContext.Request.Cookies["ShoppingCartId"];
            if (cartCookie != null && int.TryParse(cartCookie, out var cartId))
            {
                var cart = _shoppingContext.Carts.FirstOrDefault(c => c.Id == cartId);
                if (cart != null)
                    return cart;
            }
            return CreateCart();
        }

        public Order CalculateOrder(ShoppingCart cart)
        {
            var orderlines = new List<OrderLine>();
            var everyNthDiscount = new NthFreeDiscountCalculator(5);
            var orderamountDiscount = new OrderAmountDiscountCalculator(4, 10);
            var freeShippingDiscount = new FreeShippingDiscountCalculator(250);

            foreach (var entry in cart.Contents)
            {
                var line = new OrderLine(entry.Description, entry.Amount, entry.UnitCost, entry.UnitShippingCost);
                everyNthDiscount.ApplyTo(line);
                orderlines.Add(line);
            }

            var order = new Order(orderlines);
            orderamountDiscount.ApplyTo(order);
            freeShippingDiscount.ApplyTo(order);

            return order;
        }
    }
}
