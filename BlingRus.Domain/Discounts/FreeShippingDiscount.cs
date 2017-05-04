using System;
using System.Collections.Generic;
using System.Text;

namespace BlingRus.Domain.Discounts
{
    public class FreeShippingDiscount : IOrderDiscountCalculator
    {
        public decimal MinimumGoodsValue { get; }

        public FreeShippingDiscount(decimal minValue)
        {
            MinimumGoodsValue = minValue;
        }

        public void ApplyTo(Order order)
        {
            if (order.TotalGoodsValue < MinimumGoodsValue)
                return;

            order.Apply(new Discount($"Free shipping for orders above {MinimumGoodsValue}!", order.TotalShipping));
        }
    }
}
