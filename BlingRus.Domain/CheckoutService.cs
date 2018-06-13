﻿using System;
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
        private readonly IMailService _mailService;
        private readonly DiscountModel _discountModel;
        
        public CheckoutService(IHttpContextAccessor httpContext, 
            IShoppingContext shoppingContext, IMailService mailService, DiscountModel discountModel)
        {
            _httpContext = httpContext;
            _shoppingContext = shoppingContext;
            _mailService = mailService;
            _discountModel = discountModel;
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
                var line = new OrderLine(entry.Description, entry.Amount, entry.UnitCost, entry.UnitShippingCost);
                foreach(var orderlineDiscountCalculator in _discountModel.OrderLineDiscountCalculators)
                    orderlineDiscountCalculator.ApplyTo(line);
                orderlines.Add(line);
            }

            var order = new Order(orderlines);
            foreach(var orderDiscountCalculator in _discountModel.OrderDiscountCalculators)
                orderDiscountCalculator.ApplyTo(order);

            return order;
        }

        public Order FinalizeOrder(ShoppingCart cart, string customerName, string customerAddress, string customerEmail, string creditCardNumber, DateTime? creditCardExpiration)
        {
            var order = CalculateOrder(cart);
            cart.CustomerName = customerName;
            cart.CustomerAddress = customerAddress;
            cart.CreditCardNumber = creditCardNumber;
            cart.CreditCardExpiration = creditCardExpiration;

            if (!string.IsNullOrEmpty(customerEmail))
                _mailService.SendOrderConfirmationMail(customerEmail, cart);

            _shoppingContext.Save();
            return order;
        }
    }

    public interface IMailService
    {
        void SendOrderConfirmationMail(string customerEmail, ShoppingCart cart);
    }
}
