using System;

namespace BlingRus.Domain.Discounts
{
    public class OrderAmountDiscountCalculator : IOrderDiscountCalculator
    {
        public int MinimumAmount { get; }
        public decimal Percent { get; }

        public OrderAmountDiscountCalculator(int minimumAmount, decimal percent)
        {
            if (percent < 0 || percent > 100)
                throw new ArgumentException("Percent must be between 0 and 100");
            if (minimumAmount < 0)
                throw new ArgumentException("Minimum amount must be non-negative");

            MinimumAmount = minimumAmount;
            Percent = percent;
        }

        public void ApplyTo(Order order)
        {
            if (order.TotalAmountOrdered <= MinimumAmount)
                return;
            var discountedAmount = order.TotalGoodsValue * Percent / 100;

            order.Apply(new OrderDiscount($"Supersaver deal! {Percent}% off since you ordered more than {MinimumAmount} items.", discountedAmount));
        }
    }
}
