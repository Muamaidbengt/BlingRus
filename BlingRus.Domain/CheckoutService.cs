using System.Collections.Generic;
using BlingRus.Domain.Discounts;

namespace BlingRus.Domain
{
    public class CheckoutService
    {
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
