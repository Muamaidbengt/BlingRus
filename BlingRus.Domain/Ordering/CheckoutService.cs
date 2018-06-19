using System.Collections.Generic;
using System.Linq;
using BlingRus.Domain.Discounts;
using BlingRus.Domain.Services;
using BlingRus.Domain.Shopping;
using Microsoft.AspNetCore.Http;

namespace BlingRus.Domain.Ordering
{
    public class CheckoutService
    {
        private readonly IHttpContextAccessor _httpContext;
        private readonly IShoppingContext _shoppingContext;
        private readonly PricingModel _pricingModel;
        private readonly IMailService _mailService;
        
        public CheckoutService(
            IHttpContextAccessor httpContext
            , IShoppingContext shoppingContext
            , PricingModel pricingModel
            , IMailService mailService)
        {
            _httpContext = httpContext;
            _shoppingContext = shoppingContext;
            _pricingModel = pricingModel;
            _mailService = mailService;
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

            foreach (var entry in cart.Contents)
            {
                var line = new OrderLine(entry.Description, entry.Quantity, entry.UnitCost, entry.UnitShippingCost, entry.Customization);
                foreach(var orderlineDiscountCalculator in _pricingModel.OrderLineAdjustmentCalculators)
                    orderlineDiscountCalculator.ApplyTo(line);
                orderlines.Add(line);
            }

            var order = new Order(orderlines);
            foreach(var orderDiscountCalculator in _pricingModel.OrderAdjustmentCalculators)
                orderDiscountCalculator.ApplyTo(order);

            return order;
        }

        public Order FinalizeOrder(ShoppingCart cart)
        {
            var order = CalculateOrder(cart);
            
            order.DeliveryName = cart.CustomerName;
            order.DeliveryAddress = cart.CustomerAddress;
            order.ConfirmationEmail = cart.CustomerEmail;
            order.CreditCardNumber = cart.CreditCardNumber;
            order.CreditCardExpiration = cart.CreditCardExpiration;

            CreateCart();

            _shoppingContext.Save();

            _mailService.SendOrderConfirmationMail(order);
            return order;
        }
    }
}