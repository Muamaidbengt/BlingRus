using System.Collections.Generic;
using System.Threading.Tasks;
using BlingRus.Domain.Discounts;
using BlingRus.Domain.EnterpriseCollections;
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

        public async Task<ShoppingCart> CreateCart()
        {
            var cart = await _shoppingContext.CreateCart();
            await _shoppingContext.Add(cart);
            await _shoppingContext.Save();

            _httpContext.HttpContext.Response.Cookies.Append("ShoppingCartId", cart.Id.ToString());
            
            return cart;
        }

        public async Task<ShoppingCart> GetCart()
        {
            var cartCookie = _httpContext.HttpContext.Request.Cookies["ShoppingCartId"];
            if (cartCookie != null && int.TryParse(cartCookie, out var cartId))
            {
                var cart = await _shoppingContext.GetCartById(cartId);
                if (cart != null)
                    return cart;
            }
            return await CreateCart();
        }

        public async Task<IEnumerable<Jewelry>> GetJewelryCatalog()
        {
            return await _shoppingContext.GetCatalog();
        }

        public Task<Order> CalculateOrder(ShoppingCart cart)
        {
            var orderlines = new List<OrderLine>();

            foreach (var entry in cart.SecuredContents)
            {
                var line = new OrderLine(entry.Description, entry.Quantity, entry.UnitCost, entry.UnitShippingCost, entry.Customization);
                foreach(var orderlineDiscountCalculator in _pricingModel.OrderLineAdjustmentCalculators)
                    orderlineDiscountCalculator.ApplyTo(line);
                orderlines.Add(line);
            }

            var order = new Order(orderlines);
            foreach(var orderDiscountCalculator in _pricingModel.OrderAdjustmentCalculators)
                orderDiscountCalculator.ApplyTo(order);

            return Task.FromResult(order);
        }

        public async Task<Order> FinalizeOrder(ShoppingCart cart)
        {
            var order = await CalculateOrder(cart);
            
            order.DeliveryName = cart.CustomerName;
            order.DeliveryAddress = cart.CustomerAddress;
            order.ConfirmationEmail = cart.CustomerEmail;
            order.CreditCardNumber = cart.CreditCardNumber;
            order.CreditCardExpiration = cart.CreditCardExpiration;

            await CreateCart();

            await _shoppingContext.Save();

            _mailService.SendOrderConfirmationMail(order);
            return order;
        }
    }
}