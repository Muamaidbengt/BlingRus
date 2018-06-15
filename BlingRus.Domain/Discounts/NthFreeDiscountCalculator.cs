using System;

namespace BlingRus.Domain.Discounts
{
    public class NthFreeDiscountCalculator : IOrderLinePriceAdjustmentCalculator
    {
        protected int Counter { get; set; }
        public int N { get; }
        public NthFreeDiscountCalculator(int n)
        {
            if (n < 1)
                throw new ArgumentException("n must be greater than 1");

            N = n;
        }

        public void ApplyTo(OrderLine line)
        {
            Counter += line.QuantityOrdered;

            var times = Counter / N;
            if (times <= 0)
                return;

            Counter = Counter % N;
            line.Apply(new PriceLineAdjustment($"Every {N}:th is free", line.UnitGoodsValue, 0));
        }
    }
}
