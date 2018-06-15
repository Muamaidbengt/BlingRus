namespace BlingRus.Domain.Discounts
{
    public class FreeShippingDiscountCalculator : IOrderPriceAdjustmentCalculator
    {
        public decimal MinimumGoodsValue { get; }

        public FreeShippingDiscountCalculator(decimal minValue)
        {
            MinimumGoodsValue = minValue;
        }

        public void ApplyTo(Order order)
        {
            if (order.TotalGoodsValue < MinimumGoodsValue)
                return;

            order.Apply(new OrderPriceAdjustment($"Free shipping for orders above {MinimumGoodsValue}!", order.TotalShippingCost, 0));
        }
    }
}
